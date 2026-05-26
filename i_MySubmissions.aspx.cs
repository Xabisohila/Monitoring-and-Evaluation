using MnE2.DAL;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_MySubmissions : Page
{
    private readonly c_QuarterlyReportsDAL _reports = new c_QuarterlyReportsDAL();
    private readonly c_IndicatorsDAL _indicators = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL _annual = new c_AnnualTargetsDAL();
    private readonly c_QuarterlyTargetsDAL _qtargets = new c_QuarterlyTargetsDAL();
    private readonly c_EvidenceDAL _evidence = new c_EvidenceDAL();
    private readonly c_WorkflowHistoryDAL _wf = new c_WorkflowHistoryDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        // RequireRole(/* IndicatorOwner / M&E role IDs */);
        if (!IsPostBack)
        {
            LoadSubmissions();
        }
    }

    private int CurrentUserId
    {
        get
        {
            // Try to get from Session; fallback to 0 if missing.
            var obj = Session != null ? Session["UserID"] : null;
            if (obj == null)
            {
                return 0;
            }

            int id;
            return int.TryParse(obj.ToString(), out id) ? id : 0;
        }
    }

    private void LoadSubmissions()
    {


        var myReports = _reports
            .GetAll()
            //.Where(r => r.SubmittedByUserID == CurrentUserId)
            .Where(r => r.SubmittedByUserID == 33) // Hardcoded for testing; replace with above line in production
            .OrderByDescending(r => r.SubmittedDate)
            .ToList();

        var table = new DataTable();
        table.Columns.Add("ReportID");
        table.Columns.Add("IndicatorName");
        table.Columns.Add("FinancialYear");
        table.Columns.Add("QuarterNumber");
        table.Columns.Add("ActualValue");
        table.Columns.Add("AchievedFlag");
        table.Columns.Add("SubmittedDate");
        table.Columns.Add("StatusHtml");
        table.Columns.Add("POEHtml");

        foreach (var r in myReports)
        {
            var qt = _qtargets.GetByID(r.QuarterlyTargetID);
            var at = qt != null ? _annual.GetByID(qt.AnnualTargetID) : null;
            var ind = at != null ? _indicators.GetByID(at.IndicatorID) : null;

            var poefiles = _evidence.ListByReport(r.ReportID) ?? Enumerable.Empty<dynamic>();
            var poeHtml = string.Empty;

            foreach (var f in poefiles)
            {
                // Ensure FilePath and FileName exist; build anchor tag.
                var path = f.FilePath ?? string.Empty;
                var name = f.FileName ?? "File";
                poeHtml += "<a href='" + path + "' target='_blank'>📄 " + name + "</a><br/>";
            }

            var lastStatusList = _wf.ListByReport(r.ReportID) ?? Enumerable.Empty<dynamic>();
            var lastStatus = lastStatusList.FirstOrDefault();
            var statusHtml = "<span class='status-box status-pending'>Pending</span>";

            if (lastStatus != null)
            {
                var s = lastStatus.StatusID;
                if (s == 3 || s == 5 || s == 7)
                {
                    // QA_Pass, Approval_Approved, Signoff_SignedOff
                    statusHtml = "<span class='status-box status-pass'>Approved</span>";
                }
                else if (s == 4 || s == 6 || s == 8)
                {
                    // QA_Fail, Approval_Rejected, Signoff_NotApproved
                    statusHtml = "<span class='status-box status-fail'>Rejected</span>";
                }
                else if (s == 2)
                {
                    // Submitted
                    statusHtml = "<span class='status-box status-pending'>Submitted</span>";
                }
            }

            table.Rows.Add(
                r.ReportID,
                ind != null ? ind.IndicatorName : string.Empty,
                at != null ? at.FinancialYear.ToString() : string.Empty,
                r.QuarterNumber,
                r.ActualValue ?? string.Empty,
                r.Achieved ? "Yes" : "No",
                r.SubmittedDate.ToString("yyyy-MM-dd"),
                statusHtml,
                poeHtml);
        }

        gvSubmissions.DataSource = table;
        gvSubmissions.DataBind();
        
        // Hide the ReportID column after binding
        if (gvSubmissions.Columns.Count > 0)
        {
            gvSubmissions.Columns[0].Visible = false;
        }
    }

    protected void gvSubmissions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex;
        // Replace null-propagating operator with explicit null check for C# 5 compatibility
        if (e.CommandArgument == null || !int.TryParse(e.CommandArgument.ToString(), out rowIndex))
        {
            return;
        }

        var reportIdText = gvSubmissions.Rows[rowIndex].Cells[0].Text;
        int reportID;
        if (!int.TryParse(reportIdText, out reportID))
        {
            return;
        }

        if (e.CommandName == "View")
        {
            Response.Redirect("~/i_ReportDetails.aspx?rid=" + reportID);
        }
        else if (e.CommandName == "Workflow")
        {
            Response.Redirect("~/i_History.aspx?rid=" + reportID);
        }
    }
}