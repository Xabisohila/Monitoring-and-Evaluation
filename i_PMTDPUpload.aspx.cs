using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI;

public partial class i_PMTDPUpload : Page
{
    d_PMTDPUploadDAL uploadDAL = new d_PMTDPUploadDAL();
    d_PMTDPUploadDataDAL uploadDataDAL = new d_PMTDPUploadDataDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        if (!IsPostBack)
            LoadHistory();
    }

    private void LoadHistory()
    {
        int userId = (int)Session["UserID"];
        DataTable raw = uploadDAL.GetMyUploads(userId);

        if (raw == null || raw.Rows.Count == 0)
        {
            pnlHistoryEmpty.Visible = true;
            return;
        }

        // Build a clean display table regardless of which columns exist in the raw result
        var display = new DataTable();
        display.Columns.Add("UploadRequestID");
        display.Columns.Add("SubmittedDate");
        display.Columns.Add("StatusBadge");
        display.Columns.Add("ReviewComment");

        bool hasDate    = raw.Columns.Contains("UploadDate")     || raw.Columns.Contains("CreatedDate")
                       || raw.Columns.Contains("SubmittedDate")  || raw.Columns.Contains("RequestDate");
        bool hasComment = raw.Columns.Contains("ReviewComment")  || raw.Columns.Contains("Comment")
                       || raw.Columns.Contains("ReviewerComment");

        string dateCol    = hasDate    ? (raw.Columns.Contains("UploadDate")    ? "UploadDate"
                                       : raw.Columns.Contains("CreatedDate")   ? "CreatedDate"
                                       : raw.Columns.Contains("SubmittedDate") ? "SubmittedDate"
                                       : "RequestDate") : null;
        string commentCol = hasComment ? (raw.Columns.Contains("ReviewComment")   ? "ReviewComment"
                                       : raw.Columns.Contains("Comment")         ? "Comment"
                                       : "ReviewerComment") : null;

        foreach (DataRow r in raw.Rows)
        {
            string status = r["Status"] != DBNull.Value ? r["Status"].ToString() : "Pending";
            string badgeClass = status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ? "badge-approved"
                              : status.Equals("Rejected", StringComparison.OrdinalIgnoreCase) ? "badge-rejected"
                              : "badge-pending";
            string badge = "<span class='status-badge " + badgeClass + "'>" + status + "</span>";

            string date    = dateCol    != null && r[dateCol]    != DBNull.Value
                             ? Convert.ToDateTime(r[dateCol]).ToString("yyyy-MM-dd") : "—";
            string comment = commentCol != null && r[commentCol] != DBNull.Value
                             ? r[commentCol].ToString() : "";

            display.Rows.Add(r["UploadRequestID"].ToString(), date, badge, comment);
        }

        rptHistory.DataSource = display;
        rptHistory.DataBind();
        pnlHistoryGrid.Visible = true;
    }

    private void ShowError(string message)
    {
        string safe = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");
        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal",
            string.Format("document.getElementById('modalErrorMsg').innerText='{0}';$('#modalError').modal('show');", safe),
            true);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!fuPMTDP.HasFile)
        {
            ShowError("Please select an Excel file (.xlsx or .xls) first.");
            return;
        }

        string ext = Path.GetExtension(fuPMTDP.FileName).ToLowerInvariant();
        if (ext != ".xlsx" && ext != ".xls")
        {
            ShowError("Invalid file type. Only .xlsx and .xls files are accepted.");
            return;
        }

        try
        {
            string folder = Server.MapPath("~/Uploads/PMTDP/");
            Directory.CreateDirectory(folder);

            string path = folder + DateTime.Now.Ticks + "_" + fuPMTDP.FileName;
            fuPMTDP.SaveAs(path);

            DataTable dt = ReadExcel(path);

            if (dt.Rows.Count == 0)
            {
                ShowError("File uploaded but no data rows were found. Check the sheet is named 'PMTDP' and has a header row.");
                return;
            }

            gvPreview.DataSource = dt;
            gvPreview.DataBind();

            previewCard.Visible = true;
            lblRowCount.Text = dt.Rows.Count + " rows";

            ViewState["DT"] = dt;
            ViewState["FILE"] = path;

            btnSubmit.Visible = true;
            lblMsg.Text = dt.Rows.Count + " row(s) loaded. Review the preview below, then click Submit for Approval.";
        }
        catch (Exception ex)
        {
            ShowError("Upload failed: " + ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["DT"] as DataTable;
        if (dt == null) return;

        int uploadId = uploadDAL.CreateUploadRequest(
            (int)Session["UserID"],
            ViewState["FILE"].ToString()
        );

        foreach (DataRow r in dt.Rows)
        {
            uploadDataDAL.InsertUploadData(uploadId, r, "Insert");
        }

        btnSubmit.Visible = false;
        previewCard.Visible = false;

        // Show the success modal client-side after the postback
        ScriptManager.RegisterStartupScript(this, GetType(), "showSubmitModal",
            "$('#modalSubmitSuccess').modal('show');", true);
    }

    private DataTable ReadExcel(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        string props = ext == ".xlsx"
            ? "Excel 12.0 Xml;HDR=YES;IMEX=1"
            : "Excel 8.0;HDR=YES;IMEX=1";

        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path
                    + ";Extended Properties='" + props + "'";

        using (OleDbConnection cn = new OleDbConnection(conn))
        using (OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [PMTDP$]", cn))
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            NormalizeColumns(dt);
            return dt;
        }
    }

    // Maps common Excel header variations to the canonical names expected by
    // InsertUploadData() / n_sp_PMTDP_InsertUploadData.
    private static readonly Dictionary<string, string> _colMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Priority",                  "PriorityName" },
        { "Priority Name",             "PriorityName" },
        { "Priority Focus",            "PriorityName" },
        { "Integration Programme",     "ProgrammeName" },
        { "Programme",                 "ProgrammeName" },
        { "Programme Name",            "ProgrammeName" },
        { "Leading Department",        "LeaderDeptName" },
        { "Leader Dept",               "LeaderDeptName" },
        { "Desired Outcome",           "OutcomeName" },
        { "Outcome",                   "OutcomeName" },
        { "Outcome Name",              "OutcomeName" },
        { "Outcome Indicator",         "IndicatorName" },
        { "Indicator",                 "IndicatorName" },
        { "Indicator Name",            "IndicatorName" },
        { "Indicator Type",            "IndicatorType" },
        { "Baseline",                  "BaselineValue" },
        { "PDP Baseline 2019/2020",    "BaselineValue" },
        { "Baseline Value",            "BaselineValue" },
        { "PDP Target 2030",           "TermTargetValue" },
        { "Term Target",               "TermTargetValue" },
        { "Term Target Value",         "TermTargetValue" },
        { "Annual Budget",             "AnnualBudget" },
        { "Implementing Institution",  "ImplementingInstitution" },
        { "Implementing Institutions", "ImplementingInstitution" },
        { "Supporting Institutions",   "SupportingInstitutions" },
        { "Supporting Institution",    "SupportingInstitutions" },
        { "Is Cumulative",             "IsCumulative" },
        { "Cumulative",                "IsCumulative" },
        { "Is Percentage",             "IsPercentage" },
        { "Percentage",                "IsPercentage" },
        { "Intervention",              "InterventionName" },
        { "Intervention Name",         "InterventionName" },
        { "Intervention Indicator",    "InterventionIndicator" },
        { "Baseline 2023/24",          "Baseline2023_24" },
        { "Baseline 23/24",            "Baseline2023_24" },
        { "2030 Term Target",          "TermTarget2030" },
        { "Term Target 2030",          "TermTarget2030" },
        { "Term Budget",               "TermBudget" },
        { "Annual Target",             "AnnualTarget" },
        { "Annual Target 2025/26",     "AnnualTarget" },
        { "2025/26 Target",            "AnnualTarget" },
        { "Spatial Reference",         "SpatialReference" },
        { "Spatial Referencing",       "SpatialReference" },
    };

    private static readonly Dictionary<string, string> _displayNames =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "PriorityName",            "Priority Focus" },
        { "ProgrammeName",           "Integration Programme" },
        { "LeaderDeptName",          "Leading Department" },
        { "OutcomeName",             "Desired Outcome" },
        { "IndicatorName",           "Outcome Indicator" },
        { "IndicatorType",           "Indicator Type" },
        { "BaselineValue",           "Baseline Value" },
        { "TermTargetValue",         "Term Target Value" },
        { "AnnualBudget",            "Annual Budget" },
        { "ImplementingInstitution", "Implementing Institution" },
        { "SupportingInstitutions",  "Supporting Institutions" },
        { "IsCumulative",            "Is Cumulative" },
        { "IsPercentage",            "Is Percentage" },
        { "InterventionName",        "Intervention Name" },
        { "InterventionIndicator",   "Intervention Indicator" },
        { "Baseline2023_24",         "Baseline 2023/24" },
        { "TermTarget2030",          "Term Target 2030" },
        { "TermBudget",              "Term Budget" },
        { "AnnualTarget",            "Annual Target 2025/26" },
        { "SpatialReference",        "Spatial Reference" },
    };

    protected void gvPreview_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType != System.Web.UI.WebControls.DataControlRowType.Header) return;

        foreach (System.Web.UI.WebControls.TableCell cell in e.Row.Cells)
        {
            string display;
            if (_displayNames.TryGetValue(cell.Text, out display))
                cell.Text = display;
        }
    }

    private static void NormalizeColumns(DataTable dt)
    {
        foreach (DataColumn col in dt.Columns)
        {
            string trimmed = col.ColumnName.Trim();
            string canonical;
            if (_colMap.TryGetValue(trimmed, out canonical))
                col.ColumnName = canonical;
            else if (trimmed != col.ColumnName)
                col.ColumnName = trimmed;
        }
    }
}