using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_ReportDetails : System.Web.UI.Page
{
    private readonly c_QuarterlyReportsDAL repDAL = new c_QuarterlyReportsDAL();
    private readonly c_QuarterlyTargetsDAL qtDAL = new c_QuarterlyTargetsDAL();
    private readonly c_AnnualTargetsDAL atDAL = new c_AnnualTargetsDAL();
    private readonly c_IndicatorsDAL indDAL = new c_IndicatorsDAL();
    private readonly c_UserDAL userDAL = new c_UserDAL();
    private readonly c_EvidenceDAL evDAL = new c_EvidenceDAL();
    private readonly c_WorkflowHistoryDAL wfDAL = new c_WorkflowHistoryDAL();

    protected int ReportID
    {
        get
        {
            return Convert.ToInt32(Request["rid"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* ANY authenticated user */);
        if (!IsPostBack)
            LoadReport();
    }

    private void LoadReport()
    {
        var rep = repDAL.GetByID(ReportID);
        if (rep == null)
        {
            lblHeader.Text = "Report not found.";
            return;
        }

        var qt = qtDAL.GetByID(rep.QuarterlyTargetID);
        var at = qt != null ? atDAL.GetByID(qt.AnnualTargetID) : null;
        var ind = at != null ? indDAL.GetByID(at.IndicatorID) : null;
        var submitter = rep.SubmittedByUserID.HasValue ? userDAL.GetByID(rep.SubmittedByUserID.Value) : null;

        lblHeader.Text = string.Format("Report #{0}", rep.ReportID);
        lnkHistory.NavigateUrl = string.Format("~/Workflow/History.aspx?rid={0}", ReportID);

        // CurrentRoleID and CurrentUserID not available in context; default to false for safety
        lnkBackMySubs.Visible = false;
        lnkBackMyTasks.Visible = true;

        lblIndicator.Text = ind != null ? ind.IndicatorName : string.Empty;
        lblFY.Text = at != null ? at.FinancialYear.ToString() : string.Empty;
        lblQuarter.Text = rep.QuarterNumber.ToString();
        lblPlanned.Text = qt != null ? qt.TargetValue : string.Empty;
        lblActual.Text = rep.ActualValue;

        lblAchieved.Text = rep.Achieved ? "Yes" : "No";
        lblAchievedBadge.Text = rep.Achieved
            ? "<span class='badge badge-success'>Achieved</span>"
            : "<span class='badge badge-danger'>Not Achieved</span>";

        lblSubmitter.Text = submitter != null && !string.IsNullOrEmpty(submitter.FullName) ? submitter.FullName : "Unknown";
        lblSubmittedDate.Text = rep.SubmittedDate.ToString("yyyy-MM-dd HH:mm");

        if (rep.Achieved)
        {
            pnlNoDeviation.Visible = true;
            pnlDeviation.Visible = false;
        }
        else
        {
            pnlDeviation.Visible = true;
            pnlNoDeviation.Visible = false;

            lblDeviation.Text = rep.DeviationReason;
            lblRemedial.Text = rep.RemedialActions;
            lblDueDate.Text = rep.RemedialDueDate.HasValue
                ? rep.RemedialDueDate.Value.ToString("yyyy-MM-dd")
                : "-";
        }

        // Evidence
        var ev = evDAL.ListByReport(ReportID)
            .Select(x => new
            {
                x.EvidenceID,
                x.FileName,
                x.UploadedDate
            });
        gvEvidence.DataSource = ev;
        gvEvidence.DataBind();

        // Latest workflow (fallback to listing by report and picking latest by date if available)
        var wfList = wfDAL.ListByReport(ReportID);
        var wf = wfList != null
            ? wfList.OrderByDescending(x => x.ActionDate).FirstOrDefault()
            : null;

        if (wf != null)
        {
            lblLastStage.Text = wf.Stage;
            lblLastStatus.Text = wf.StatusID.ToString();
            var lastActionBy = userDAL.GetByID(wf.ActionByUserID);
            lblLastActionBy.Text = lastActionBy != null && !string.IsNullOrEmpty(lastActionBy.FullName) ? lastActionBy.FullName : "Unknown";
            lblLastActionDate.Text = wf.ActionDate.ToString("yyyy-MM-dd HH:mm");
            lblLastComments.Text = wf.Comments;
        }
    }
}