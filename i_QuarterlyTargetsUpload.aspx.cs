using MnE2.DAL;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public partial class i_QuarterlyTargetsUpload : System.Web.UI.Page
{
 
    private const string CachePrefix = "QTR_BULK_PLAN_";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lnkPMTDP.NavigateUrl = ResolveUrl("~/Downloads/PMTDP_Upload_Template.xlsx");
            //lnkPOA.NavigateUrl = ResolveUrl("~/Downloads/POA_Upload_Template.xlsx");
            //lnkQuarterly.NavigateUrl = ResolveUrl("~/Downloads/QuarterlyTargets_Example_Template_v3.xlsx"); // plain template
        }
    }

    protected void btnUploadPreview_Click(object sender, EventArgs e)
    {
        ResetUi();

        if (!fuExcel.HasFile || !fuExcel.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
        { Fail("Please select a valid .xlsx file."); return; }

        // Save upload to disk (ACE OLE DB reads from file path)
        var folder = Server.MapPath("~/Uploads/Imports/");
        System.IO.Directory.CreateDirectory(folder);
        var path = System.IO.Path.Combine(folder, DateTime.Now.Ticks + "_" + fuExcel.FileName);
        fuExcel.SaveAs(path);

        try
        {
            // Read the Template sheet using ACE OLE DB (same pattern as PMTDP)
            var dt = ReadExcelToDataTable(path, "Template$");
            if (dt == null || dt.Rows.Count == 0)
            { Warn("No data found in the Excel file."); return; }

            // Expected plain columns: IndicatorName | FinancialYear | Q1Target | Q2Target | Q3Target | Q4Target
            var plan = new ImportPlan { Items = new List<Item>(), Errors = new List<Item>() };
            var byName = LoadIndicatorLookupByName();

            int rowNo = 1; // header is row 1
            foreach (DataRow r in dt.Rows)
            {
                rowNo++;
                var baseRow = new Item { RowNo = rowNo };
                string name = (r["IndicatorName"] ?? "").ToString().Trim();
                string fyTxt = (r["FinancialYear"] ?? "").ToString().Trim();

                if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(fyTxt) &&
                    IsNullOrEmpty(r["Q1Target"]) && IsNullOrEmpty(r["Q2Target"]) &&
                    IsNullOrEmpty(r["Q3Target"]) && IsNullOrEmpty(r["Q4Target"]))
                {
                    continue; // skip blank
                }

                if (string.IsNullOrWhiteSpace(name))
                { plan.Errors.Add(baseRow.With(name, fyTxt, "-", "-", "ERROR", "IndicatorName required.")); continue; }

                int indicatorId;
                if (!byName.TryGetValue(name.ToLowerInvariant(), out indicatorId))
                { plan.Errors.Add(baseRow.With(name, fyTxt, "-", "-", "ERROR", "Unknown IndicatorName.")); continue; }

                int fy;
                if (!int.TryParse(fyTxt, out fy))
                { plan.Errors.Add(baseRow.With(name, fyTxt, "-", "-", "ERROR", "Invalid FinancialYear.")); continue; }

                // Q1..Q4
                for (int q = 1; q <= 4; q++)
                {
                    string col = "Q" + q + "Target";
                    string target = (r.Table.Columns.Contains(col) ? (r[col] ?? "").ToString().Trim() : "");
                    if (!string.IsNullOrWhiteSpace(target))
                    {
                        var status = ValidateTargetValue(target) ? "OK" : "ERROR";
                        var msg = status == "OK" ? "Ready" : "Invalid target value.";
                        var it = new Item
                        {
                            RowNo = rowNo,
                            IndicatorID = indicatorId,
                            IndicatorName = name,
                            FinancialYear = fy.ToString(),
                            Quarter = "Q" + q,
                            TargetValue = target,
                            Status = status,
                            Message = msg
                        };
                        if (status == "OK") plan.Items.Add(it); else plan.Errors.Add(it);
                    }
                }
            }

            // Show preview (OK + errors)
            var preview = new List<Item>(); preview.AddRange(plan.Items); preview.AddRange(plan.Errors);
            if (preview.Count == 0) { Warn("Nothing to import (no rows with Q1–Q4 targets)."); return; }

            gvPreview.DataSource = preview; gvPreview.DataBind();

            // Cache plan for Confirm
            string key = Guid.NewGuid().ToString("N");
            Session[CachePrefix + key] = plan;
            hfPreviewKey.Value = key;

            btnConfirm.Visible = plan.Errors.Count == 0;
            btnErrorsCsv.Visible = plan.Errors.Count > 0;

            if (plan.Errors.Count == 0) Ok("Validation OK. Review the preview and click 'Confirm Upload' to commit.");
            else Warn("Validation completed with errors. Fix and re-upload, or download the Errors CSV.");
        }
        catch (Exception ex)
        {
            Fail("Failed to read the Excel file. " + Server.HtmlEncode(ex.Message));
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        btnConfirm.Visible = false;
        var plan = GetPlan();
        if (plan == null) { Fail("Preview expired. Please run 'Upload & Preview' again."); return; }
        if (plan.Errors.Count > 0) { Fail("There are errors. Please fix and preview again before confirming."); return; }

        try
        {
            using (var con = Database.GetConnection())
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        foreach (var group in GroupByIndicatorYear(plan.Items))
                        {
                            int indicatorId = group.Key.IndicatorID;
                            int fy = group.Key.FinancialYear;

                            int annualId = UpsertAnnualTarget(con, tran, indicatorId, fy);
                            foreach (var it in group.Value)
                            {
                                int q = int.Parse(it.Quarter.Substring(1));
                                UpsertQuarterlyTarget(con, tran, annualId, q, it.TargetValue, null);
                            }
                        }
                        tran.Commit();
                        Ok("Upload committed successfully.");
                    }
                    catch (Exception exInTran)
                    { tran.Rollback(); Fail("Commit failed. " + Server.HtmlEncode(exInTran.Message)); }
                }
            }
        }
        catch (Exception ex)
        { Fail("Database error. " + Server.HtmlEncode(ex.Message)); }
    }

    protected void btnErrorsCsv_Click(object sender, EventArgs e)
    {
        var plan = GetPlan();
        if (plan == null || plan.Errors.Count == 0) return;

        Response.Clear();
        Response.ContentType = "text/csv";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AddHeader("Content-Disposition", "attachment;filename=QuarterlyTargets_Errors.csv");
        
        var csv = new System.Text.StringBuilder();
        
        // Write header row
        csv.AppendLine("Row,IndicatorName,FinancialYear,Quarter,TargetValue,Status,Message");
        
        // Write data rows
        foreach (var eitem in plan.Errors)
        {
            csv.Append(eitem.RowNo);
            csv.Append(",");
            csv.Append("\"" + (eitem.IndicatorName ?? "").Replace("\"", "\"\"") + "\"");
            csv.Append(",");
            csv.Append(eitem.FinancialYear ?? "");
            csv.Append(",");
            csv.Append(eitem.Quarter ?? "");
            csv.Append(",");
            csv.Append("\"" + (eitem.TargetValue ?? "").Replace("\"", "\"\"") + "\"");
            csv.Append(",");
            csv.Append(eitem.Status ?? "");
            csv.Append(",");
            csv.AppendLine("\"" + (eitem.Message ?? "").Replace("\"", "\"\"") + "\"");
        }
        
        Response.Write(csv.ToString());
        Response.End();
    }

    // ---------- Data reading (ACE OLE DB like PMTDP) ----------
    private static DataTable ReadExcelToDataTable(string filePath, string sheetRange)
    {
        // Same pattern your PMTDP page uses (ACE OLE DB).
        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                      ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
        using (var cn = new OleDbConnection(conn))
        using (var cmd = new OleDbCommand("SELECT * FROM [" + sheetRange + "]", cn))
        using (var da = new OleDbDataAdapter(cmd))
        {
            var dt = new DataTable();
            cn.Open();
            da.Fill(dt);
            return dt;
        }
    }

    // ---------- Helpers / DAL ----------
    private void ResetUi() { lblStatus.Text = ""; lblStatus.CssClass = ""; gvPreview.DataSource = null; gvPreview.DataBind(); btnConfirm.Visible = false; btnErrorsCsv.Visible = false; }
    private void Ok(string m) { lblStatus.CssClass = "ok"; lblStatus.Text = m; }
    private void Warn(string m) { lblStatus.CssClass = "warn"; lblStatus.Text = m; }
    private void Fail(string m) { lblStatus.CssClass = "err"; lblStatus.Text = m; }

    private static bool IsNullOrEmpty(object v)
    {
        return string.IsNullOrWhiteSpace((v ?? "").ToString());
    }

    private static bool ValidateTargetValue(string target)
    {
        if (string.IsNullOrWhiteSpace(target)) return false;
        decimal result;
        return decimal.TryParse(target, out result);
    }

    private ImportPlan GetPlan()
    {
        string key = hfPreviewKey.Value;
        return string.IsNullOrWhiteSpace(key) ? null : (Session[CachePrefix + key] as ImportPlan);
    }

    private Dictionary<string, int> LoadIndicatorLookupByName()
    {
        var dict = new Dictionary<string, int>();
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("l_sp_Indicator_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    string name = rdr["IndicatorName"] != null ? rdr["IndicatorName"].ToString() : null;
                    int id = Convert.ToInt32(rdr["IndicatorID"]);
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        string key = name.ToLowerInvariant();
                        if (!dict.ContainsKey(key)) dict.Add(key, id);
                    }
                }
            }
        }
        return dict;
    }

    private int UpsertAnnualTarget(SqlConnection con, SqlTransaction tran, int indicatorID, int fy)
    {
        using (var cmd = new SqlCommand("g_sp_AnnualTarget_GetByIndicatorYear", con, tran))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
            cmd.Parameters.AddWithValue("@FinancialYear", fy);
            using (var rdr = cmd.ExecuteReader())
            { if (rdr.Read()) return Convert.ToInt32(rdr["AnnualTargetID"]); }
        }
        using (var cmd = new SqlCommand("i_sp_AnnualTarget_Upsert", con, tran))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnnualTargetID", DBNull.Value);
            cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
            cmd.Parameters.AddWithValue("@FinancialYear", fy);
            cmd.Parameters.AddWithValue("@AnnualTargetValue", DBNull.Value);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    private int UpsertQuarterlyTarget(SqlConnection con, SqlTransaction tran, int annualTargetID, int quarterNumber, string targetValue, string spatialRef)
    {
        using (var cmd = new SqlCommand("i_sp_QuarterlyTarget_Upsert", con, tran))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuarterlyTargetID", DBNull.Value);
            cmd.Parameters.AddWithValue("@AnnualTargetID", annualTargetID);
            cmd.Parameters.AddWithValue("@QuarterNumber", quarterNumber);
            cmd.Parameters.AddWithValue("@TargetValue", (object)targetValue ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SpatialReference", DBNull.Value); // not used in the current template
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    [Serializable] private class ImportPlan { public List<Item> Items { get; set; } public List<Item> Errors { get; set; } }
    
    [Serializable]
    private class Item
    {
        public int RowNo { get; set; }
        public int IndicatorID { get; set; }
        public string IndicatorName { get; set; }
        public string FinancialYear { get; set; }
        public string Quarter { get; set; }
        public string TargetValue { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public Item With(string indName, string fy, string q, string target, string status, string message)
        {
            return new Item
            {
                RowNo = this.RowNo,
                IndicatorID = this.IndicatorID,
                IndicatorName = indName,
                FinancialYear = fy,
                Quarter = q,
                TargetValue = target,
                Status = status,
                Message = message
            };
        }
    }

    [Serializable]
    private class IndicatorYearKey
    {
        public int IndicatorID { get; set; }
        public int FinancialYear { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as IndicatorYearKey;
            if (other == null) return false;
            return this.IndicatorID == other.IndicatorID && this.FinancialYear == other.FinancialYear;
        }

        public override int GetHashCode()
        {
            return IndicatorID.GetHashCode() ^ FinancialYear.GetHashCode();
        }
    }

    private static Dictionary<IndicatorYearKey, List<Item>> GroupByIndicatorYear(List<Item> items)
    {
        var map = new Dictionary<IndicatorYearKey, List<Item>>();
        foreach (var it in items)
        {
            int fy = int.Parse(it.FinancialYear);
            var k = new IndicatorYearKey { IndicatorID = it.IndicatorID, FinancialYear = fy };
            
            bool found = false;
            foreach (var existingKey in map.Keys)
            {
                if (existingKey.Equals(k))
                {
                    map[existingKey].Add(it);
                    found = true;
                    break;
                }
            }
            
            if (!found)
            {
                map[k] = new List<Item> { it };
            }
        }
        return map;
    }
}