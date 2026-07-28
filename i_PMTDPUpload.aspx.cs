using MnE2.DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
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
        display.Columns.Add("Status");
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

            display.Rows.Add(r["UploadRequestID"].ToString(), date, status, badge, comment);
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

        lblMsg.Text = "Your PMTDP data has been submitted for approval. A Planning Unit reviewer will assess your submission and you will be notified of the outcome.";
        lblMsg.CssClass = "msg-bar visible";

        // Show the success modal client-side after the postback
        ScriptManager.RegisterStartupScript(this, GetType(), "showSubmitModal",
            "$('#modalSubmitSuccess').modal('show');", true);
    }

    private DataTable ReadExcel(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        if (ext == ".xls")
            throw new InvalidOperationException(
                "The old .xls format is not supported. Please open the file in Excel, " +
                "save it as .xlsx (Excel Workbook), and upload again.");

        ExcelPackage.License.SetNonCommercialPersonal("MnE Planning System");

        using (var package = new ExcelPackage(new FileInfo(path)))
        {
            // Find sheet named PMTDP (case-insensitive); fall back to first sheet
            ExcelWorksheet sheet = null;
            foreach (var ws in package.Workbook.Worksheets)
            {
                if (ws.Name.Equals("PMTDP", StringComparison.OrdinalIgnoreCase))
                { sheet = ws; break; }
            }
            if (sheet == null)
                sheet = package.Workbook.Worksheets[0];

            if (sheet.Dimension == null)
                return new DataTable(); // empty sheet

            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;

            // Build a raw all-string DataTable — same shape FindHeaderRow expects
            DataTable raw = new DataTable();
            for (int c = 0; c < colCount; c++)
                raw.Columns.Add("Col" + c, typeof(string));

            for (int r = 1; r <= rowCount; r++)
            {
                object[] vals = new object[colCount];
                for (int c = 1; c <= colCount; c++)
                {
                    object v = sheet.Cells[r, c].Value;
                    vals[c - 1] = (v == null) ? (object)DBNull.Value : v.ToString().Trim();
                }
                raw.Rows.Add(vals);
            }

            int headerRowIdx = FindHeaderRow(raw);
            DataTable dt = BuildFromHeaderRow(raw, headerRowIdx);
            NormalizeColumns(dt);
            return dt;
        }
    }

    // Scans the first 5 rows to find the one with the most cells matching
    // known column names. Handles instruction/banner rows before the real header.
    private int FindHeaderRow(DataTable raw)
    {
        int bestRow = 0, bestScore = 0;
        for (int r = 0; r < Math.Min(5, raw.Rows.Count); r++)
        {
            int score = 0;
            foreach (DataColumn col in raw.Columns)
            {
                string v = (raw.Rows[r][col] ?? "").ToString().Trim();
                if (_colMap.ContainsKey(v)) score++;
            }
            if (score > bestScore) { bestScore = score; bestRow = r; }
        }
        return bestRow;
    }

    // Builds a typed DataTable using the given row as column names,
    // then adds all subsequent non-empty rows as data.
    private static DataTable BuildFromHeaderRow(DataTable raw, int headerRowIdx)
    {
        DataTable dt = new DataTable();
        DataRow hdr = raw.Rows[headerRowIdx];
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (int c = 0; c < raw.Columns.Count; c++)
        {
            string name = (hdr[c] ?? "").ToString().Trim();
            if (string.IsNullOrEmpty(name)) name = "Col_" + c;
            if (!seen.Add(name)) name = name + "_" + c;
            dt.Columns.Add(name, typeof(string));
        }

        for (int r = headerRowIdx + 1; r < raw.Rows.Count; r++)
        {
            DataRow src = raw.Rows[r];
            bool anyValue = false;
            foreach (var cell in src.ItemArray)
                if (cell != null && !(cell is DBNull) && !string.IsNullOrWhiteSpace(cell.ToString()))
                { anyValue = true; break; }
            if (!anyValue) continue;

            object[] vals = new object[dt.Columns.Count];
            for (int c = 0; c < vals.Length; c++)
                vals[c] = (src[c] == null || src[c] is DBNull)
                    ? (object)DBNull.Value
                    : src[c].ToString().Trim();
            dt.Rows.Add(vals);
        }

        return dt;
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
        { "Implementing Institution",  "ImplementingInstitution" },
        { "Implementing Institutions", "ImplementingInstitution" },
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
        { "ImplementingInstitution", "Implementing Institution" },
        { "IsCumulative",            "Is Cumulative" },
        { "IsPercentage",            "Is Percentage" },
        { "InterventionName",        "Intervention Name" },
        { "InterventionIndicator",   "Intervention Indicator" },
        { "Baseline2023_24",         "Baseline 2023/24" },
        { "TermTarget2030",          "Term Target 2030" },
        { "TermBudget",              "Term Budget" },
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