using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public partial class ii_DeptQuarterlyTargetsUpload : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL _indDal = new c_IndicatorsDAL();
    private readonly c_QuarterlyTargetsDAL _qDal = new c_QuarterlyTargetsDAL();

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!fuExcel.HasFile) { Show("Please choose an .xlsx file.", false); return; }

        var folder = Server.MapPath("~/Uploads/Imports/DeptQT");
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, DateTime.Now.Ticks + "_" + Path.GetFileName(fuExcel.FileName));
        fuExcel.SaveAs(path);

        // Sheet name: QuarterlyTargets$
        var dt = ReadExcel(path, "QuarterlyTargets$");
        gvPreview.DataSource = dt; gvPreview.DataBind();
        ViewState["DT"] = dt; ViewState["FILE"] = path;
        Show("Loaded " + dt.Rows.Count + " rows. Review and click Commit.", true);
        btnCommit.Visible = true;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        var dt = ViewState["DT"] as DataTable;
        if (dt == null) { Show("Nothing to commit.", false); return; }

        int ok = 0, skip = 0, missing = 0;

        foreach (DataRow r in dt.Rows)
        {
            string indName = (r["IndicatorName"] ?? "").ToString().Trim();
            string outName = (r["OutcomeName"] ?? "").ToString().Trim();
            string fyStr = (r["FinancialYear"] ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(indName) || string.IsNullOrEmpty(outName) || string.IsNullOrEmpty(fyStr))
            { skip++; continue; }

            int fy; if (!int.TryParse(fyStr, out fy)) { skip++; continue; }

            var indicatorId = _indDal.GetIndicatorIdByNameOutcome(indName, outName);
            if (indicatorId == null) { missing++; continue; }

            // Q1..Q4: overwrite only if provided (never touches annual)
            SaveQuarter(indicatorId.Value, fy, 1, (r.Table.Columns.Contains("Q1Target") ? (r["Q1Target"] ?? "").ToString() : null),
                                        (r.Table.Columns.Contains("SpatialReference") ? (r["SpatialReference"] ?? "").ToString() : null), ref ok, ref skip);
            SaveQuarter(indicatorId.Value, fy, 2, (r.Table.Columns.Contains("Q2Target") ? (r["Q2Target"] ?? "").ToString() : null),
                                        (r.Table.Columns.Contains("SpatialReference") ? (r["SpatialReference"] ?? "").ToString() : null), ref ok, ref skip);
            SaveQuarter(indicatorId.Value, fy, 3, (r.Table.Columns.Contains("Q3Target") ? (r["Q3Target"] ?? "").ToString() : null),
                                        (r.Table.Columns.Contains("SpatialReference") ? (r["SpatialReference"] ?? "").ToString() : null), ref ok, ref skip);
            SaveQuarter(indicatorId.Value, fy, 4, (r.Table.Columns.Contains("Q4Target") ? (r["Q4Target"] ?? "").ToString() : null),
                                        (r.Table.Columns.Contains("SpatialReference") ? (r["SpatialReference"] ?? "").ToString() : null), ref ok, ref skip);
        }

        Show(string.Format("Commit done. Updated: {0}, Skipped empty: {1}, Not found: {2}.", ok, skip, missing), true);
        btnCommit.Visible = false;
    }

    private void SaveQuarter(int indicatorId, int fy, int q, string target, string spatial, ref int ok, ref int skip)
    {
        var id = _qDal.UpsertDept(indicatorId, fy, q, target, spatial);
        if (id > 0) ok++; else skip++;
    }

    protected void btnDownloadTpl_Click(object sender, EventArgs e)
    {
        // You can store this file in App_Data/Templates and copy at deployment,
        // or just stream an already-generated one (we created one for you below).
        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Disposition", "attachment; filename=Dept_QuarterlyTargets_Upload_Template.xlsx");
        // Replace the path with your template location as needed:
        var templatePath = Server.MapPath("~/App_Data/Templates/Dept_QuarterlyTargets_Upload_Template.xlsx");
        if (File.Exists(templatePath)) Response.TransmitFile(templatePath);
        Response.End();
    }

    private DataTable ReadExcel(string filePath, string sheetRange)
    {
        // Same approach as your existing PMTDP/POA upload pages: ACE OLEDB + Preview + Commit
        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                        ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
        using (var cn = new OleDbConnection(conn))
        using (var cmd = new OleDbCommand("SELECT * FROM [" + sheetRange + "]", cn))
        using (var da = new OleDbDataAdapter(cmd))
        {
            var dt = new DataTable();
            cn.Open(); da.Fill(dt); return dt;
        }
    }

    private void Show(string msg, bool good) { lblMsg.Text = msg; lblMsg.ForeColor = good ? System.Drawing.Color.Green : System.Drawing.Color.Red; }
}
