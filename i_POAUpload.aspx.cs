using MnE2.DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public partial class i_POAUpload : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL indDAL = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL aDAL = new c_AnnualTargetsDAL();
    private readonly c_QuarterlyTargetsDAL qDAL = new c_QuarterlyTargetsDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Planning/Admin */);
        if (!IsPostBack)
        {
            ddlFY.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Financial Year --", ""));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025/2026", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026/2027", "2026"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2027/2028", "2027"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2028/2029", "2028"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2029/2030", "2029"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2030/2031", "2030"));
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!fuPOA.HasFile || string.IsNullOrEmpty(ddlFY.SelectedValue)) { lblMsg.Text = "Choose a file and Financial Year."; lblMsg.ForeColor = System.Drawing.Color.Red; return; }

        var folder = Server.MapPath("~/Uploads/Imports/");
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, DateTime.Now.Ticks + "_" + Path.GetFileName(fuPOA.FileName));
        fuPOA.SaveAs(path);

        var dt = ReadExcelToDataTable(path, "POA$");
        gvPreview.DataSource = dt; gvPreview.DataBind();
        ViewState["POA_DT"] = dt;
        ViewState["FY"] = ddlFY.SelectedValue;
        lblMsg.Text = "Loaded " + dt.Rows.Count + " rows. Review and Commit.";
        lblMsg.ForeColor = System.Drawing.Color.Green;
        btnCommit.Visible = true;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        var dt = ViewState["POA_DT"] as DataTable;
        if (dt == null) { lblMsg.Text = "Nothing to commit."; lblMsg.ForeColor = System.Drawing.Color.Red; return; }
        int fy = Convert.ToInt32((string)ViewState["FY"]);

        foreach (DataRow r in dt.Rows)
        {
            string indicatorName = (r["IndicatorName"] ?? "").ToString().Trim();
            string annualVal = (r["AnnualTargetValue"] ?? "").ToString().Trim();
            string q1 = (r.Table.Columns.Contains("Q1Target") ? (r["Q1Target"] ?? "").ToString().Trim() : null);
            string q2 = (r.Table.Columns.Contains("Q2Target") ? (r["Q2Target"] ?? "").ToString().Trim() : null);
            string q3 = (r.Table.Columns.Contains("Q3Target") ? (r["Q3Target"] ?? "").ToString().Trim() : null);
            string q4 = (r.Table.Columns.Contains("Q4Target") ? (r["Q4Target"] ?? "").ToString().Trim() : null);

            if (string.IsNullOrEmpty(indicatorName)) continue;

            // Find indicator by name (you may want to use a code/ID in file for stronger matching)
            var match = indDAL.GetAll().FirstOrDefault(i => i.IndicatorName.Equals(indicatorName, StringComparison.OrdinalIgnoreCase));
            if (match == null) continue;

            // Upsert Annual target
            var annual = aDAL.GetByIndicatorYear(match.IndicatorID, fy) ?? new c_AnnualTarget { AnnualTargetID = 0, IndicatorID = match.IndicatorID, FinancialYear = fy };
            annual.AnnualTargetValue = annualVal;
            int annualId = aDAL.Upsert(annual);

            // Ensure Quarterly targets exist/updated (Q1..Q4)
            UpsertQuarter(annualId, 1, q1);
            UpsertQuarter(annualId, 2, q2);
            UpsertQuarter(annualId, 3, q3);
            UpsertQuarter(annualId, 4, q4);
        }

        lblMsg.Text = "POA committed successfully.";
        lblMsg.ForeColor = System.Drawing.Color.Green;
        btnCommit.Visible = false;
    }

    private void UpsertQuarter(int annualId, int q, string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return;
        // Try to get existing by annual + quarter
        var list = qDAL.ListByAnnual(annualId);
        var qt = list.FirstOrDefault(x => x.QuarterNumber == q) ?? new c_QuarterlyTarget { QuarterlyTargetID = 0, AnnualTargetID = annualId, QuarterNumber = q };
        qt.TargetValue = value;
        qDAL.Upsert(qt);
    }

    private static DataTable ReadExcelToDataTable(string filePath, string sheetRange)
    {
        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
        using (var cn = new OleDbConnection(conn))
        using (var cmd = new OleDbCommand("SELECT * FROM [" + sheetRange + "]", cn))
        using (var da = new OleDbDataAdapter(cmd))
        {
            var dt = new DataTable();
            cn.Open(); da.Fill(dt); return dt;
        }
    }

    protected void btnDownloadPOA_Click(object sender, EventArgs e)
    {
        string templatePath = Server.MapPath("~/App_Data/Templates/POA_Upload_Template.xlsx");
        if (!System.IO.File.Exists(templatePath))
        {
            lblMsg.Text = "Template not found. Please contact the system administrator.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Disposition", "attachment; filename=POA_Upload_Template.xlsx");
        Response.TransmitFile(templatePath);
        Response.End();
    }


}