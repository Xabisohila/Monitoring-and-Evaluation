using MnE2.DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            pnlHistoryGrid.Visible  = false;
            return;
        }

        var display = new DataTable();
        display.Columns.Add("UploadRequestID");
        display.Columns.Add("SubmittedDate");
        display.Columns.Add("Status");
        display.Columns.Add("ReviewComment");

        string dateCol = raw.Columns.Contains("UploadDate")    ? "UploadDate"
                       : raw.Columns.Contains("CreatedDate")   ? "CreatedDate"
                       : raw.Columns.Contains("SubmittedDate") ? "SubmittedDate"
                       : raw.Columns.Contains("RequestDate")   ? "RequestDate" : null;

        string commentCol = raw.Columns.Contains("ReviewComment")   ? "ReviewComment"
                          : raw.Columns.Contains("Comment")         ? "Comment"
                          : raw.Columns.Contains("ReviewerComment") ? "ReviewerComment" : null;

        foreach (DataRow r in raw.Rows)
        {
            string status  = r["Status"] != DBNull.Value ? r["Status"].ToString() : "Pending";
            string date    = dateCol    != null && r[dateCol]    != DBNull.Value ? Convert.ToDateTime(r[dateCol]).ToString("yyyy-MM-dd") : "—";
            string comment = commentCol != null && r[commentCol] != DBNull.Value ? r[commentCol].ToString() : "";
            display.Rows.Add(r["UploadRequestID"].ToString(), date, status, comment);
        }

        rptHistory.DataSource = display;
        rptHistory.DataBind();
        pnlHistoryEmpty.Visible = false;
        pnlHistoryGrid.Visible  = true;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!fuPMTDP.HasFile)
        {
            ShowError("Please select an Excel file (.xlsx) first.");
            return;
        }

        string ext = Path.GetExtension(fuPMTDP.FileName).ToLowerInvariant();
        if (ext != ".xlsx" && ext != ".xls")
        {
            ShowError("Invalid file type. Only .xlsx files are accepted.");
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

            // Populate header meta literals
            litGoal.Text      = Server.HtmlEncode(GetContext(dt, "PDPGoal"));
            litPriority.Text  = Server.HtmlEncode(GetContext(dt, "PriorityName"));
            litProgramme.Text = Server.HtmlEncode(GetContext(dt, "ProgrammeName"));
            litImpact.Text    = Server.HtmlEncode(GetContext(dt, "ImpactStatement"));
            litRowCount.Text  = string.Format("<span class='row-count'>{0} rows</span>", dt.Rows.Count);

            BuildPreviewTable(dt);

            pnlUpload.Visible  = false;
            pnlPreview.Visible = true;

            ViewState["DT"]   = dt;
            ViewState["FILE"] = path;
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
            uploadDataDAL.InsertUploadData(uploadId, r, "Insert");

        ViewState.Remove("DT");
        ViewState.Remove("FILE");

        pnlPreview.Visible = false;
        pnlUpload.Visible  = true;

        ScriptManager.RegisterStartupScript(this, GetType(), "showSubmitModal",
            "$('#modalSubmitSuccess').modal('show');", true);

        LoadHistory();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState.Remove("DT");
        ViewState.Remove("FILE");
        pnlPreview.Visible = false;
        pnlUpload.Visible  = true;
    }

    private string GetContext(DataTable dt, string colName)
    {
        if (!dt.Columns.Contains(colName) || dt.Rows.Count == 0) return "";
        return (dt.Rows[0][colName] ?? "").ToString().Trim();
    }

    private void BuildPreviewTable(DataTable data)
    {
        string[] cols   = { "OutcomeName","IndicatorName","IndicatorType",
                            "ImplementingInstitution","InterventionName","InterventionIndicator",
                            "Baseline2023_24","TermTarget2030","TermBudget","SpatialReference" };
        string[] labels = { "Desired Outcome","Outcome Indicator","Type",
                            "Institution","Intervention","Indicator",
                            "Baseline 23/24","Term Target","Budget","Spatial" };

        var sb = new StringBuilder();
        sb.Append("<table class='preview-table'><thead><tr>");
        for (int i = 0; i < labels.Length; i++)
            sb.AppendFormat("<th>{0}</th>", labels[i]);
        sb.Append("</tr></thead><tbody>");

        foreach (DataRow row in data.Rows)
        {
            sb.Append("<tr>");
            foreach (string col in cols)
            {
                string val = data.Columns.Contains(col)
                    ? HttpUtility.HtmlEncode((row[col] ?? "").ToString())
                    : "";
                sb.AppendFormat("<td>{0}</td>", val);
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");

        phPreviewTable.Controls.Add(new Literal { Text = sb.ToString() });
    }

    private void ShowError(string message)
    {
        string safe = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");
        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal",
            string.Format("document.getElementById('modalErrorMsg').innerText='{0}';$('#modalError').modal('show');", safe),
            true);
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
            ExcelWorksheet sheet = null;
            foreach (var ws in package.Workbook.Worksheets)
            {
                if (ws.Name.Equals("PMTDP", StringComparison.OrdinalIgnoreCase))
                { sheet = ws; break; }
            }
            if (sheet == null)
                sheet = package.Workbook.Worksheets[0];

            if (sheet.Dimension == null)
                return new DataTable();

            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;

            // ── Step 1: scan first 10 rows for header-section context metadata ──
            var context = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var metaLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Provincial Development Plan Goal", "PDPGoal"         },
                { "Provincial Development",           "PDPGoal"         },
                { "PDP Goal",                         "PDPGoal"         },
                { "Priority Focus",                   "PriorityName"    },
                { "Priority",                         "PriorityName"    },
                { "Integration Programme",            "ProgrammeName"   },
                { "Programme",                        "ProgrammeName"   },
                { "Impact Statement",                 "ImpactStatement" },
                { "Impact",                           "ImpactStatement" },
            };
            for (int r = 1; r <= Math.Min(10, rowCount); r++)
            {
                for (int c = 1; c <= Math.Min(5, colCount); c++)
                {
                    string cellText = (sheet.Cells[r, c].Text ?? "").Trim().TrimEnd(':');
                    foreach (var kv in metaLabels)
                    {
                        if (!cellText.StartsWith(kv.Key, StringComparison.OrdinalIgnoreCase)) continue;
                        if (context.ContainsKey(kv.Value)) continue;
                        string val = "";
                        if (cellText.Contains(":"))
                            val = cellText.Substring(cellText.IndexOf(':') + 1).Trim();
                        if (string.IsNullOrEmpty(val))
                        {
                            for (int cc = c + 1; cc <= colCount; cc++)
                            {
                                val = (sheet.Cells[r, cc].Text ?? "").Trim();
                                if (!string.IsNullOrEmpty(val)) break;
                            }
                        }
                        if (!string.IsNullOrEmpty(val))
                            context[kv.Value] = val;
                        break;
                    }
                }
            }

            // ── Step 2: build raw all-string DataTable ──
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

            // ── Step 3: find data header row, build typed DataTable ──
            int headerRowIdx = FindHeaderRow(raw);
            DataTable dt = BuildFromHeaderRow(raw, headerRowIdx);
            NormalizeColumns(dt);

            // ── Step 4: inject context fields not already present as data columns ──
            foreach (var kv in context)
            {
                if (!dt.Columns.Contains(kv.Key))
                {
                    dt.Columns.Add(kv.Key, typeof(string));
                    foreach (DataRow row in dt.Rows)
                        row[kv.Key] = kv.Value;
                }
            }

            return dt;
        }
    }

    // Scans the first 10 rows to find the one with the most _colMap matches.
    private int FindHeaderRow(DataTable raw)
    {
        int bestRow = 0, bestScore = 0;
        for (int r = 0; r < Math.Min(10, raw.Rows.Count); r++)
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

    private static readonly Dictionary<string, string> _colMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Provincial Development Plan Goal",   "PDPGoal" },
        { "Provincial Development Plan Goal 1", "PDPGoal" },
        { "PDP Goal",                           "PDPGoal" },
        { "PDP Goal 1",                         "PDPGoal" },
        { "Goal",                               "PDPGoal" },
        { "Impact",                             "ImpactStatement" },
        { "Impact Statement",                   "ImpactStatement" },
        { "Priority",                           "PriorityName" },
        { "Priority Name",                      "PriorityName" },
        { "Priority Focus",                     "PriorityName" },
        { "Integration Programme",              "ProgrammeName" },
        { "Programme",                          "ProgrammeName" },
        { "Programme Name",                     "ProgrammeName" },
        { "Leading Department",                 "LeaderDeptName" },
        { "Leader Dept",                        "LeaderDeptName" },
        { "Desired Outcome",                    "OutcomeName" },
        { "Outcome",                            "OutcomeName" },
        { "Outcome Name",                       "OutcomeName" },
        { "Outcome Indicator",                  "IndicatorName" },
        { "Indicator",                          "IndicatorName" },
        { "Indicator Name",                     "IndicatorName" },
        { "Indicator Type",                     "IndicatorType" },
        { "Baseline",                           "BaselineValue" },
        { "PDP Baseline 2019/2020",             "BaselineValue" },
        { "Baseline 2019/2020",                 "BaselineValue" },
        { "Baseline Value",                     "BaselineValue" },
        { "PDP Target 2030",                    "TermTargetValue" },
        { "Target 2030",                        "TermTargetValue" },
        { "Term Target",                        "TermTargetValue" },
        { "Term Target Value",                  "TermTargetValue" },
        { "Implementing Institution",           "ImplementingInstitution" },
        { "Implementing Institutions",          "ImplementingInstitution" },
        { "Supporting Institutions",            "SupportingInstitutions" },
        { "Supporting Institution",             "SupportingInstitutions" },
        { "Dependency",                         "SupportingInstitutions" },
        { "Is Cumulative",                      "IsCumulative" },
        { "Cumulative",                         "IsCumulative" },
        { "Is Percentage",                      "IsPercentage" },
        { "Percentage",                         "IsPercentage" },
        { "Intervention",                       "InterventionName" },
        { "Intervention Name",                  "InterventionName" },
        { "Intervention Indicator",             "InterventionIndicator" },
        { "Baseline 2023/24",                   "Baseline2023_24" },
        { "Baseline 23/24",                     "Baseline2023_24" },
        { "Term Target 2030",                   "TermTarget2030" },
        { "Term Target 2025-2030",              "TermTarget2030" },
        { "Term Target 2025-30",                "TermTarget2030" },
        { "2030 Term Target",                   "TermTarget2030" },
        { "Term Budget",                        "TermBudget" },
        { "Spatial Reference",                  "SpatialReference" },
        { "Spatial Referencing",                "SpatialReference" },
    };

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
