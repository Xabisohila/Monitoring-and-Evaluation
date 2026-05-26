using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Globalization;

// Your DALs & models
using MnE2.DAL;

public partial class i_ReportSubmit : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL indicators = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL annual = new c_AnnualTargetsDAL();
    private readonly c_QuarterlyTargetsDAL qTargets = new c_QuarterlyTargetsDAL();
    private readonly c_QuarterlyReportsDAL reports = new c_QuarterlyReportsDAL();
    private readonly c_EvidenceDAL evidence = new c_EvidenceDAL();
    private readonly c_SystemPeriodDAL sysPeriod = new c_SystemPeriodDAL();
    private readonly c_WorkflowHistoryDAL wf = new c_WorkflowHistoryDAL();

    public int? CurrentUserID { get; private set; }   // TODO: set from your auth/session
    public string CurrentUserName { get; private set; } // optional display name

    // Upload policy
    private static readonly string[] AllowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg" };
    private const int MaxFileSizeBytes = 20 * 1024 * 1024; // 20 MB
    private const string PoeRootVirtual = "Uploads/POE"; // per-report subfolder under this root

    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure the main form can carry files
        if (Page.Form != null) Page.Form.Enctype = "multipart/form-data";

        if (!IsPostBack)
        {
            // Indicators
            ddlIndicator.DataSource = indicators.GetAll();
            ddlIndicator.DataTextField = "IndicatorName";
            ddlIndicator.DataValueField = "IndicatorID";
            ddlIndicator.DataBind();
            ddlIndicator.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));

            // FY
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2024/2025", "2024"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025/2026", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026/2027", "2026"));
            ddlFY.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Financial Year --", ""));

            // Default tolerance
            hfTolerance.Value = "5";
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        // Reset UI
        lblPlanned.Text = "Planned Target: -";
        lblPlanned.ForeColor = System.Drawing.Color.Black;
        hfQuarterlyTargetID.Value = "";
        hfPlannedValue.Value = "0";
        ShowHistoryPanel(""); // hide/clear history by default

        if (string.IsNullOrEmpty(ddlIndicator.SelectedValue) || string.IsNullOrEmpty(ddlFY.SelectedValue))
            return;

        int ind = Convert.ToInt32(ddlIndicator.SelectedValue);
        int fy = Convert.ToInt32(ddlFY.SelectedValue);
        int q = Convert.ToInt32(ddlQuarter.SelectedValue);

        // Period open?
        var period = sysPeriod.Get(fy, q);
        if (period == null || !period.IsOpen)
        {
            lblPlanned.Text = "Reporting is closed for the selected period.";
            lblPlanned.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // Current quarter target
        var qt = qTargets.GetByIndicatorYearQuarter(ind, fy, q);
        if (qt == null)
        {
            lblPlanned.Text = "No planned quarterly target found.";
            lblPlanned.ForeColor = System.Drawing.Color.Red;
            return;
        }

        hfQuarterlyTargetID.Value = qt.QuarterlyTargetID.ToString();
        lblPlanned.Text = "Planned Target: " + (qt.TargetValue ?? "-");
        lblPlanned.ForeColor = System.Drawing.Color.Black;

        // Send numeric planned to client for JS & server tolerance logic
        hfPlannedValue.Value = qt.TargetValue ?? "0";

        // Tolerance (default 5 if none)
        decimal tolPct = 5m;
        decimal.TryParse(hfTolerance.Value ?? "5", NumberStyles.Any, CultureInfo.InvariantCulture, out tolPct);

        // ------------------------------------------------------------
        // NEW: Pull latest report for the selected period (if any)
        // ------------------------------------------------------------
        c_QuarterlyReport currentReport = null;
        try
        {
            var allReports = reports.ListByQuarterlyTarget(qt.QuarterlyTargetID);
            if (allReports != null && allReports.Count > 0)
            {
                currentReport = allReports[allReports.Count - 1];
            }
        }
        catch
        {
            // No reports available for current period
        }

        // ------------------------------------------------------------
        // NEW: Pull previous quarter's report (if any)
        // ------------------------------------------------------------
        int prevFy = fy;
        int prevQ = q - 1;
        if (prevQ < 1) { prevQ = 4; prevFy = fy - 1; }

        var prevQt = qTargets.GetByIndicatorYearQuarter(ind, prevFy, prevQ);
        c_QuarterlyReport prevReport = null;
        if (prevQt != null)
        {
            try
            {
                var allPrevReports = reports.ListByQuarterlyTarget(prevQt.QuarterlyTargetID);
                if (allPrevReports != null && allPrevReports.Count > 0)
                {
                    prevReport = allPrevReports[allPrevReports.Count - 1];
                }
            }
            catch
            {
                // No reports available for previous period
            }
        }

        // ------------------------------------------------------------
        // Compose history HTML
        // ------------------------------------------------------------
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (currentReport != null)
        {
            sb.Append(RenderReportCard(
                string.Format("Selected Period: FY {0} • Q{1}", fy, q),
                currentReport,
                qt.TargetValue ?? "0",
                tolPct
            ));
        }

        if (prevReport != null && prevQt != null)
        {
            sb.Append(RenderReportCard(
                string.Format("Previous Period: FY {0} • Q{1}", prevFy, prevQ),
                prevReport,
                prevQt.TargetValue ?? "0",
                tolPct
            ));
        }

        ShowHistoryPanel(sb.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Must have selected target & quarter
        if (string.IsNullOrEmpty(hfQuarterlyTargetID.Value)) return;
        if (string.IsNullOrEmpty(ddlQuarter.SelectedValue)) return;

        // TODO: pull from your auth/session
        CurrentUserID = 1; // placeholder for testing
        if (!CurrentUserID.HasValue) return;

        // Parse values
        int quarter = Convert.ToInt32(ddlQuarter.SelectedValue);
        decimal planned = 0m, actual = 0m, tolPct = 5m;
        decimal.TryParse(hfPlannedValue.Value ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture, out planned);
        decimal.TryParse(txtActual.Text ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture, out actual);
        decimal.TryParse(hfTolerance.Value ?? "5", NumberStyles.Any, CultureInfo.InvariantCulture, out tolPct);

        // Deviation %
        decimal deviationPct = (planned == 0m) ? ((actual == 0m) ? 0m : 100m)
                             : ((actual - planned) / planned) * 100m;
        deviationPct = Math.Round(deviationPct, 2);

        // Achieved when within tolerance band (for status only)
        bool achieved = Math.Abs(deviationPct) <= tolPct;

        // Server-side validation remains as per your previous logic (tolerance-aware)
        if (!achieved && deviationPct < 0m)
        {
            if (string.IsNullOrWhiteSpace(txtDeviation.Text)) { ShowError("Please provide a Deviation Reason."); return; }
            if (string.IsNullOrWhiteSpace(txtRemedial.Text)) { ShowError("Please provide Remedial Actions."); return; }
            DateTime tmp;
            if (string.IsNullOrWhiteSpace(dtDue.Text) || !DateTime.TryParse(dtDue.Text, out tmp))
            {
                ShowError("Please provide a valid Remedial Due Date.");
                return;
            }
        }
        else if (!achieved && deviationPct > 0m)
        {
            if (string.IsNullOrWhiteSpace(txtOver.Text)) { ShowError("Please provide an Over‑achievement Reason."); return; }
            bool requireRemedialOnOver = true;
            if (requireRemedialOnOver && string.IsNullOrWhiteSpace(txtRemedial.Text))
            {
                ShowError("Please provide Remedial Actions for the over‑achievement.");
                return;
            }
        }

        // Build & save report
        var model = new c_QuarterlyReport
        {
            ReportID = 0,
            QuarterlyTargetID = Convert.ToInt32(hfQuarterlyTargetID.Value),
            SubmittedByUserID = 33,//CurrentUserID,
            QuarterNumber = quarter,
            ActualValue = txtActual.Text,
            Achieved = achieved,
            DeviationReason = txtDeviation.Text,
            RemedialActions = txtRemedial.Text,
            OverAchieveReason = txtOver.Text,
            RemedialDueDate = string.IsNullOrWhiteSpace(dtDue.Text) ? (DateTime?)null : Convert.ToDateTime(dtDue.Text),
            SpatialReference = txtSpatial.Text
        };

        int reportID = reports.Upsert(model);

        // Save primary report (required)
        SavePrimaryReport(reportID, "fuPrimaryReport");

        // Save additional POE files
        SaveAllEvidenceFiles(reportID, "fuPOE");

        // Workflow history
        //wf.Upsert(new c_WorkflowHistory
        //{
        //    HistoryID = 0,
        //    ReportID = reportID,
        //    StatusID = 2, // Submitted
        //    Stage = "Submitted",
        //    ActionByUserID = CurrentUserID.Value,
        //    Comments = "Report submitted by " + (string.IsNullOrWhiteSpace(CurrentUserName) ? ("User " + CurrentUserID.Value) : CurrentUserName)
        //});

        Response.Redirect("~/i_MySubmissions.aspx");
    }

    private void SavePrimaryReport(int reportID, string inputName)
    {
        if (reportID <= 0) return;

        var file = Request.Files[inputName];
        if (file == null || file.ContentLength == 0) { ShowError("Primary Report (PDF) is required."); throw new InvalidOperationException("Primary report missing"); }

        var ext = (Path.GetExtension(file.FileName) ?? string.Empty).ToLowerInvariant();
        if (!string.Equals(ext, ".pdf", StringComparison.OrdinalIgnoreCase))
        {
            ShowError("Primary Report must be a PDF file.");
            throw new InvalidOperationException("Primary report not PDF");
        }
        if (file.ContentLength > MaxFileSizeBytes) { ShowError("Primary Report exceeds 20 MB."); throw new InvalidOperationException("Primary report too large"); }

        string reportSubfolderVirtual = PoeRootVirtual.TrimEnd('/') + "/" + reportID;
        string reportSubfolderPhysical = Server.MapPath(reportSubfolderVirtual);
        Directory.CreateDirectory(reportSubfolderPhysical);

        string unique = Guid.NewGuid().ToString("N");
        string storedName = "primary_" + reportID + "_" + unique + ext;
        string fullPath = Path.Combine(reportSubfolderPhysical, storedName);
        string fileVirtual = reportSubfolderVirtual + "/" + storedName;

        file.SaveAs(fullPath);

        evidence.Upsert(new c_EvidenceFile
        {
            EvidenceID = 0,
            ReportID = reportID,
            FileName = storedName,
            FilePath = fileVirtual
        });
    }

    /// <summary>
    /// Saves all posted files whose form key equals the provided input name (e.g., "fuPOE").
    /// Each file is saved under ~/Uploads/POE/{reportId}/{reportId}_{GUID}{ext} and a row is inserted into EvidenceFiles.
    /// </summary>
    private void SaveAllEvidenceFiles(int reportID, string inputName)
    {
        if (reportID <= 0) return;

        string reportSubfolderVirtual = PoeRootVirtual.TrimEnd('/') + "/" + reportID; // ~/Uploads/POE/{reportId}
        string reportSubfolderPhysical = Server.MapPath(reportSubfolderVirtual);
        Directory.CreateDirectory(reportSubfolderPhysical);

        for (int i = 0; i < Request.Files.Count; i++)
        {
            string key = Request.Files.AllKeys[i];
            if (!string.Equals(key, inputName, StringComparison.OrdinalIgnoreCase))
                continue;

            HttpPostedFile posted = Request.Files[i];
            if (posted == null || posted.ContentLength == 0)
                continue;

            string ext = (Path.GetExtension(posted.FileName) ?? string.Empty).ToLowerInvariant();

            bool allowed = false;
            for (int a = 0; a < AllowedExtensions.Length; a++)
                if (string.Equals(AllowedExtensions[a], ext, StringComparison.OrdinalIgnoreCase)) { allowed = true; break; }

            if (!allowed) continue;
            if (posted.ContentLength > MaxFileSizeBytes) continue;

            try
            {
                string unique = Guid.NewGuid().ToString("N");
                string storedName = reportID + "_" + unique + ext;
                string fullPath = Path.Combine(reportSubfolderPhysical, storedName);
                string fileVirtual = reportSubfolderVirtual + "/" + storedName;

                posted.SaveAs(fullPath);

                evidence.Upsert(new c_EvidenceFile
                {
                    EvidenceID = 0,
                    ReportID = reportID,
                    FileName = storedName,
                    FilePath = fileVirtual
                });
            }
            catch
            {
                // Optionally log/audit
            }
        }
    }

    private void ShowError(string message)
    {
        litStatus.Text = "<div style='background:#FFF5F5;border-left:5px solid #C50F1F;padding:10px;color:#C50F1F;'>" +
                         HttpUtility.HtmlEncode(message) + "</div>";
    }

    // Renders a compact “card” with outcome, deviation %, amount, and remaining/exceeded
    private string RenderReportCard(string title, c_QuarterlyReport report, string plannedStr, decimal tolPct)
    {
        // Planned (string in DAL for some schemas) → decimal
        decimal planned = 0m; 
        decimal.TryParse(plannedStr ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture, out planned);

        // Actual Value in your schema is string; parse to decimal safely
        decimal actual = 0m; 
        string actualValue = (report != null) ? report.ActualValue : null;
        decimal.TryParse(actualValue ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture, out actual);

        // Deviation % and amount
        decimal devPct = (planned == 0m) ? ((actual == 0m) ? 0m : 100m)
                       : ((actual - planned) / planned) * 100m;
        devPct = Math.Round(devPct, 2);
        decimal devAmt = actual - planned;

        // Outcome status uses tolerance (informational)
        string status;
        string css;
        if (Math.Abs(devPct) <= tolPct) { status = "On Track"; css = "state-ok"; }
        else if (devPct < 0) { status = "Under‑achievement"; css = "state-under"; }
        else { status = "Over‑achievement"; css = "state-over"; }

        // Remaining/Exceeded text
        string remainderText = (devAmt < 0m)
            ? string.Format("Remaining to target: {0:N2}", Math.Abs(planned - actual))
            : (devAmt > 0m ? string.Format("Exceeded by: {0:N2}", devAmt) : "Met the target.");

        // Basic metadata (if your model has SubmittedOn/SubmittedBy, include them)
        string who = string.Empty;
        // Example if available:
        // who = string.Format(" • Submitted by UserID {0}", report.SubmittedByUserID);

        return string.Format(@"
<div class='hist-card'>
  <div class='hist-title'>{0}</div>
  <div>
    <span class='{1}'>{2}</span>
    <span class='badge'>Deviation {3:N2}% ({4:+0.##;-0.##;0})</span>
  </div>
  <div class='note'>Planned: {5:N2} • Actual: {6:N2} • {7}{8}</div>
</div>", 
            HttpUtility.HtmlEncode(title),
            css,
            status,
            devPct,
            devAmt,
            planned,
            actual,
            remainderText,
            who);
    }

    // Toggles the history panel visibility with filled HTML
    private void ShowHistoryPanel(string html)
    {
        pnlHistory.Visible = !string.IsNullOrWhiteSpace(html);
        // Add/remove 'hidden' CSS as a belt-and-braces approach for your existing styles
        pnlHistory.CssClass = pnlHistory.Visible
            ? pnlHistory.CssClass.Replace("hidden", "").Trim()
            : (pnlHistory.CssClass.Contains("hidden") ? pnlHistory.CssClass : (pnlHistory.CssClass + " hidden")).Trim();

        litPrevReports.Text = html ?? string.Empty;
    }
}
