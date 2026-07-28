using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class i_POAUpload : System.Web.UI.Page
{
    private readonly d_POAUploadDAL _dal = new d_POAUploadDAL();

    // Column name aliases → canonical field names
    private static readonly Dictionary<string, string> _colMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Desired Outcome",               "DesiredOutcome" },
        { "Outcome Indicator",             "OutcomeIndicator" },
        { "Indicator Type",                "IndicatorType" },
        { "Baseline 2019/2020",            "BaselinePDP" },
        { "Baseline 2019/20",              "BaselinePDP" },
        { "Target 2030",                   "TargetPDP2030" },
        { "Implementing Institution",      "ImplementingInstitution" },
        { "Implementing Institution Dependency", "ImplementingInstitution" },
        { "# IE",                          "NumberIE" },
        { "#IE",                           "NumberIE" },
        { "Intervention",                  "InterventionName" },
        { "ID: IE",                        "InterventionID_Text" },
        { "ID:IE",                         "InterventionID_Text" },
        { "Intervention Indicator",        "InterventionIndicator" },
        { "Baseline 2023/24",              "Baseline2023_24" },
        { "Baseline 23/24",                "Baseline2023_24" },
        { "Term Target 2025-2030",         "TermTarget2025_2030" },
        { "Term Target 2025 2030",         "TermTarget2025_2030" },
        { "Target 2026/27",                "Target2026_27" },
        { "Target 26/27",                  "Target2026_27" },
        { "Annual Budget",                 "AnnualBudget" },
        { "Spatial Referencing",           "SpatialReference" },
        { "Spatial Reference",             "SpatialReference" },
    };

    // Columns where merged-cell fill-down should propagate
    private static readonly HashSet<string> _fillDownCols = new HashSet<string>
    {
        "DesiredOutcome", "OutcomeIndicator", "IndicatorType",
        "BaselinePDP", "TargetPDP2030", "ImplementingInstitution",
        "NumberIE", "InterventionName", "InterventionID_Text"
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] == null) { Response.Redirect("login.aspx"); return; }
        int role = Convert.ToInt32(Session["UserType"]);
        // Allow: Admin (32), Planning Unit (37), OTP Monitoring (41)
        if (role != 32 && role != 37 && role != 41) { Response.Redirect("Index.aspx"); return; }

        if (!IsPostBack)
        {
            int resubmitId;
            if (int.TryParse(Request.QueryString["resubmit"], out resubmitId) && resubmitId > 0)
            {
                ViewState["ResubmitId"] = resubmitId;
                pnlResubmitNotice.Visible = true;
            }
            BindMyUploads();
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (!fuPOA.HasFile)
        {
            ShowError("Please select a POA Excel file (.xlsx) before previewing.");
            return;
        }
        string ext = Path.GetExtension(fuPOA.FileName).ToLower();
        if (ext != ".xlsx" && ext != ".xls")
        {
            ShowError("Only Excel files (.xlsx, .xls) are accepted.");
            return;
        }

        try
        {
            // Save to temp uploads folder
            string folder = Server.MapPath("~/Uploads/POA_Staging/");
            Directory.CreateDirectory(folder);
            string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(fuPOA.FileName);
            string path     = Path.Combine(folder, fileName);
            fuPOA.SaveAs(path);

            // Parse
            ExcelPackage.License.SetNonCommercialPersonal("MnE Planning System");
            Dictionary<string, string> header;
            DataTable data;
            ReadPOAExcel(path, out header, out data);

            if (data == null || data.Rows.Count == 0)
            {
                ShowError("No data rows were found in the file. Please check the file format and try again.");
                return;
            }

            // Store in ViewState
            ViewState["POA_Path"]   = path;
            ViewState["POA_Header"] = header;
            ViewState["POA_Data"]   = data;

            // Populate preview panel
            litGoal.Text      = Server.HtmlEncode(GetHeader(header, "GoalName"));
            litPriority.Text  = Server.HtmlEncode(GetHeader(header, "PriorityFocus"));
            litProgramme.Text = Server.HtmlEncode(GetHeader(header, "IntegrationProgramme"));
            litImpact.Text    = Server.HtmlEncode(GetHeader(header, "ImpactStatement"));
            litRowCount.Text  = string.Format(" <span class='row-count'>{0} rows</span>", data.Rows.Count);

            BuildPreviewTable(data);

            pnlUpload.Visible         = false;
            pnlPreview.Visible        = true;
            pnlResubmitNotice.Visible = (ViewState["ResubmitId"] != null);
        }
        catch (Exception ex)
        {
            ShowError("Failed to read the Excel file: " + ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var header = ViewState["POA_Header"] as Dictionary<string, string>;
        var data   = ViewState["POA_Data"]   as DataTable;
        string path = ViewState["POA_Path"]  as string;

        if (header == null || data == null)
        {
            ShowError("Session expired. Please re-upload the file.");
            pnlPreview.Visible = false;
            pnlUpload.Visible  = true;
            return;
        }

        int userId = Convert.ToInt32(Session["UserID"]);

        try
        {
            bool isResubmit = ViewState["ResubmitId"] != null;
            int requestId;

            if (isResubmit)
            {
                requestId = Convert.ToInt32(ViewState["ResubmitId"]);
                _dal.ClearAndReset(requestId, userId, path,
                    GetHeader(header, "GoalName"),
                    GetHeader(header, "PriorityFocus"),
                    GetHeader(header, "IntegrationProgramme"),
                    GetHeader(header, "ImpactStatement"));
            }
            else
            {
                requestId = _dal.CreateRequest(
                    userId, path,
                    GetHeader(header, "GoalName"),
                    GetHeader(header, "PriorityFocus"),
                    GetHeader(header, "IntegrationProgramme"),
                    GetHeader(header, "ImpactStatement"));
            }

            foreach (DataRow row in data.Rows)
            {
                _dal.InsertRow(requestId,
                    GetCell(row, "DesiredOutcome"),
                    GetCell(row, "OutcomeIndicator"),
                    GetCell(row, "IndicatorType"),
                    GetCell(row, "BaselinePDP"),
                    GetCell(row, "TargetPDP2030"),
                    GetCell(row, "ImplementingInstitution"),
                    GetCell(row, "NumberIE"),
                    GetCell(row, "InterventionName"),
                    GetCell(row, "InterventionID_Text"),
                    GetCell(row, "InterventionIndicator"),
                    GetCell(row, "Baseline2023_24"),
                    GetCell(row, "TermTarget2025_2030"),
                    GetCell(row, "Target2026_27"),
                    GetCell(row, "AnnualBudget"),
                    GetCell(row, "SpatialReference"));
            }

            ViewState.Remove("POA_Path");
            ViewState.Remove("POA_Header");
            ViewState.Remove("POA_Data");
            ViewState.Remove("ResubmitId");

            pnlPreview.Visible        = false;
            pnlUpload.Visible         = true;
            pnlResubmitNotice.Visible = false;

            string msg = isResubmit
                ? string.Format("Upload resubmitted successfully ({0} data rows). The reviewer will be notified.", data.Rows.Count)
                : string.Format("POA upload submitted successfully ({0} data rows). A reviewer will approve or reject your submission.", data.Rows.Count);
            ShowSuccess(msg);
            BindMyUploads();
        }
        catch (Exception ex)
        {
            ShowError("Failed to save the upload: " + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState.Remove("POA_Path");
        ViewState.Remove("POA_Header");
        ViewState.Remove("POA_Data");
        pnlPreview.Visible = false;
        pnlUpload.Visible  = true;
    }

    // ── Excel parsing ────────────────────────────────────────────────────

    private void ReadPOAExcel(string path, out Dictionary<string, string> header, out DataTable data)
    {
        header = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        data   = null;

        ExcelPackage.License.SetNonCommercialPersonal("MnE Planning System");
        using (var pkg = new ExcelPackage(new FileInfo(path)))
        {
            ExcelWorksheet ws = null;
            // Prefer a sheet named "POA" or similar, fall back to first sheet
            foreach (var sheet in pkg.Workbook.Worksheets)
            {
                if (sheet.Name.IndexOf("POA", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    sheet.Name.IndexOf("Programme", StringComparison.OrdinalIgnoreCase) >= 0)
                { ws = sheet; break; }
            }
            if (ws == null) ws = pkg.Workbook.Worksheets[0];

            int lastRow = ws.Dimension != null ? ws.Dimension.End.Row : 0;
            int lastCol = ws.Dimension != null ? ws.Dimension.End.Column : 0;
            if (lastRow == 0) return;

            // Scan first 8 rows for metadata labels
            var metaLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Priority Focus",          "PriorityFocus" },
                { "Integration Programme",   "IntegrationProgramme" },
                { "Impact",                  "ImpactStatement" },
                { "Provincial Development",  "GoalName" },
                { "Goal",                    "GoalName" },
            };

            for (int r = 1; r <= Math.Min(8, lastRow); r++)
            {
                for (int c = 1; c <= Math.Min(5, lastCol); c++)
                {
                    string cellText = (ws.Cells[r, c].Text ?? "").Trim();
                    foreach (var kv in metaLabels)
                    {
                        if (cellText.StartsWith(kv.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            // Value is either in the same cell (after a colon) or the next non-empty cell on the row
                            string val = "";
                            if (cellText.Contains(":"))
                                val = cellText.Substring(cellText.IndexOf(':') + 1).Trim();
                            if (string.IsNullOrEmpty(val))
                            {
                                for (int cc = c + 1; cc <= lastCol; cc++)
                                {
                                    val = (ws.Cells[r, cc].Text ?? "").Trim();
                                    if (!string.IsNullOrEmpty(val)) break;
                                }
                            }
                            if (!header.ContainsKey(kv.Value))
                                header[kv.Value] = val;
                        }
                    }
                }
            }

            // Find the primary header row (highest number of _colMap matches)
            int bestRow   = -1;
            int bestScore = 0;
            for (int r = 1; r <= Math.Min(10, lastRow); r++)
            {
                int score = 0;
                for (int c = 1; c <= lastCol; c++)
                {
                    string cell = NormalizeHeader(ws.Cells[r, c].Text);
                    if (_colMap.ContainsKey(cell)) score++;
                }
                if (score > bestScore) { bestScore = score; bestRow = r; }
            }
            if (bestRow == -1 || bestScore < 2) return;

            // Build column index → canonical field name from bestRow
            var colIndex = new Dictionary<int, string>();
            for (int c = 1; c <= lastCol; c++)
            {
                string cell = NormalizeHeader(ws.Cells[bestRow, c].Text);
                string field;
                if (_colMap.TryGetValue(cell, out field))
                    colIndex[c] = field;
            }

            // Check the NEXT row for sub-headers (e.g. "Baseline 2019/2020" under "PDP Fulfilment")
            if (bestRow + 1 <= lastRow)
            {
                for (int c = 1; c <= lastCol; c++)
                {
                    if (colIndex.ContainsKey(c)) continue; // already mapped
                    string cell = NormalizeHeader(ws.Cells[bestRow + 1, c].Text);
                    string field;
                    if (_colMap.TryGetValue(cell, out field))
                        colIndex[c] = field;
                }
            }

            // Determine first data row (skip bestRow + possible sub-header)
            int dataStartRow = bestRow + 1;
            if (bestRow + 1 <= lastRow)
            {
                // If the sub-header row had any _colMap hits, data starts one row later
                bool subHeaderFound = false;
                for (int c = 1; c <= lastCol; c++)
                {
                    string cell = NormalizeHeader(ws.Cells[bestRow + 1, c].Text);
                    if (_colMap.ContainsKey(cell)) { subHeaderFound = true; break; }
                }
                if (subHeaderFound) dataStartRow = bestRow + 2;
            }

            // Build DataTable
            data = new DataTable();
            foreach (var field in new[]
            {
                "DesiredOutcome","OutcomeIndicator","IndicatorType","BaselinePDP","TargetPDP2030",
                "ImplementingInstitution","NumberIE","InterventionName","InterventionID_Text",
                "InterventionIndicator","Baseline2023_24","TermTarget2025_2030",
                "Target2026_27","AnnualBudget","SpatialReference"
            })
                data.Columns.Add(field, typeof(string));

            // Fill-down tracking
            var lastValues = new Dictionary<string, string>();

            for (int r = dataStartRow; r <= lastRow; r++)
            {
                // Skip entirely empty rows
                bool anyValue = false;
                for (int c = 1; c <= lastCol; c++)
                {
                    if (!string.IsNullOrWhiteSpace(ws.Cells[r, c].Text)) { anyValue = true; break; }
                }
                if (!anyValue) continue;

                DataRow dr = data.NewRow();
                foreach (var kv in colIndex)
                {
                    string cellVal = (ws.Cells[r, kv.Key].Text ?? "").Trim();
                    string field   = kv.Value;

                    if (string.IsNullOrEmpty(cellVal) && _fillDownCols.Contains(field))
                    {
                        string prev;
                        if (lastValues.TryGetValue(field, out prev))
                            cellVal = prev;
                    }
                    else if (!string.IsNullOrEmpty(cellVal) && _fillDownCols.Contains(field))
                    {
                        lastValues[field] = cellVal;
                    }

                    if (data.Columns.Contains(field))
                        dr[field] = cellVal;
                }
                data.Rows.Add(dr);
            }
        }
    }

    private static string NormalizeHeader(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        // Remove superscripts, footnote markers, newlines; collapse whitespace
        s = s.Replace("\n", " ").Replace("\r", " ");
        s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
        // Remove trailing digits that look like footnote superscripts (e.g. "Indicator Type1")
        // Use lookbehind so year-suffixed headers like "Baseline 2023/24" are NOT stripped
        s = System.Text.RegularExpressions.Regex.Replace(s, @"(?<=[a-zA-Z])\d+$", "");
        return s.Trim();
    }

    private string GetHeader(Dictionary<string, string> h, string key)
    {
        string val;
        return h.TryGetValue(key, out val) ? val : "";
    }

    private string GetCell(DataRow row, string col)
    {
        return row.Table.Columns.Contains(col) ? (row[col] ?? "").ToString().Trim() : "";
    }

    private void BuildPreviewTable(DataTable data)
    {
        var sb = new StringBuilder();
        sb.Append("<table class='preview-table'><thead><tr>");
        string[] cols = { "DesiredOutcome", "OutcomeIndicator", "IndicatorType",
                          "InterventionName", "InterventionIndicator",
                          "Baseline2023_24", "Target2026_27", "TermTarget2025_2030",
                          "ImplementingInstitution", "AnnualBudget", "SpatialReference" };
        string[] labels = { "Desired Outcome", "Outcome Indicator", "Type",
                            "Intervention", "Indicator",
                            "Baseline 23/24", "Target 26/27", "Term Target",
                            "Institution", "Annual Budget", "Spatial" };
        for (int i = 0; i < cols.Length; i++)
            sb.AppendFormat("<th>{0}</th>", labels[i]);
        sb.Append("</tr></thead><tbody>");

        foreach (DataRow row in data.Rows)
        {
            sb.Append("<tr>");
            foreach (string col in cols)
            {
                string val = data.Columns.Contains(col) ? HttpUtility.HtmlEncode((row[col] ?? "").ToString()) : "";
                sb.AppendFormat("<td>{0}</td>", val);
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");

        var lit = new Literal { Text = sb.ToString() };
        phPreviewTable.Controls.Add(lit);
    }

    private void BindMyUploads()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        var dt = _dal.GetMyUploads(userId);
        if (dt.Rows.Count == 0)
        {
            pnlNoUploads.Visible = true;
            pnlMyUploads.Visible = false;
        }
        else
        {
            pnlNoUploads.Visible = false;
            pnlMyUploads.Visible = true;
            rptMyUploads.DataSource = dt;
            rptMyUploads.DataBind();
        }
    }

    private void ShowError(string msg)
    {
        litError.Text     = msg;
        pnlError.Visible  = true;
        pnlSuccess.Visible = false;
    }

    private void ShowSuccess(string msg)
    {
        litSuccess.Text    = msg;
        pnlSuccess.Visible = true;
        pnlError.Visible   = false;
    }
}
