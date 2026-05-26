using Microsoft.Office.Interop.Excel;
using MnE2.DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TextmagicRest.Model;
//using YourProjectName.Data;



public partial class i_Approval : System.Web.UI.Page
{
    private readonly c_QuarterlyReportsDAL repDAL = new c_QuarterlyReportsDAL();
    private readonly c_QuarterlyTargetsDAL qtDAL = new c_QuarterlyTargetsDAL();
    private readonly c_AnnualTargetsDAL atDAL = new c_AnnualTargetsDAL();
    private readonly c_IndicatorsDAL indDAL = new c_IndicatorsDAL();
    private readonly c_OutcomesDAL outDAL = new c_OutcomesDAL();
    private readonly c_IntegrationProgrammesDAL progDAL = new c_IntegrationProgrammesDAL();
    private readonly c_PrioritiesDAL priDAL = new c_PrioritiesDAL();
    private readonly c_EvidenceDAL evDAL = new c_EvidenceDAL();
    private readonly c_UserDAL userDAL = new c_UserDAL();
    private readonly c_WorkflowHistoryDAL wfDAL = new c_WorkflowHistoryDAL();

    // Workflow status constants - adjust these values based on your database
    private const int APPROVAL_APPROVED = 3;
    private const int APPROVAL_REJECTED = 4;

    // Add missing properties
    private int CurrentFY
    {
        get { return Session["CurrentFY"] != null ? Convert.ToInt32(Session["CurrentFY"]) : DateTime.Now.Year; }
    }

    private int CurrentQuarter
    {
        get { return Session["CurrentQuarter"] != null ? Convert.ToInt32(Session["CurrentQuarter"]) : (DateTime.Now.Month - 1) / 3 + 1; }
    }

    private int CurrentUserID
    {
        get { return Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : 0; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* HOD/CEO */);

        if (!IsPostBack)
            BindGrid();
    }

    private void BindGrid()
    {
        try
        {
            gv.DataSource = repDAL.ListByYearQuarter(CurrentFY, CurrentQuarter);
            gv.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }

    protected void gv_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Open") return;

            int index = Convert.ToInt32(e.CommandArgument);
            int reportID = Convert.ToInt32(gv.DataKeys[index].Value);

            LoadDetails(reportID);
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }

    private void LoadDetails(int reportID)
    {
        pnlDetails.Visible = true;

        try
        {
            var rep = repDAL.GetByID(reportID);
            var qt = qtDAL.GetByID(rep.QuarterlyTargetID);
            var at = atDAL.GetByID(qt.AnnualTargetID);
            var ind = indDAL.GetByID(at.IndicatorID);

            var outcome = outDAL.GetByID(ind.OutcomeID);
            var prog = outcome.ProgrammeID.HasValue ? progDAL.GetByID(outcome.ProgrammeID.Value) : null;
            var priority = outcome.PriorityID.HasValue ? priDAL.GetByID(outcome.PriorityID.Value) : null;

            var submitter = rep.SubmittedByUserID.HasValue ? userDAL.GetByID(rep.SubmittedByUserID.Value) : null;

            // Basic indicator information
            lblIndicator.Text = ind.IndicatorName;
            lblIndicatorType.Text = ind.IndicatorType;
            lblPriority.Text = priority != null ? priority.PriorityName : "";
            lblProgramme.Text = prog != null ? prog.ProgrammeName : "";
            lblOutcome.Text = outcome.OutcomeName;
            lblBaseline.Text = ind.BaselineValue;
            lblTermTarget.Text = ind.TermTargetValue;

            // Targets
            lblAnnualTarget.Text = at.AnnualTargetValue;
            lblQuarterlyTarget.Text = qt.TargetValue;

            // Report info
            lblActual.Text = rep.ActualValue;
            lblAchieved.Text = rep.Achieved ? "Yes" : "No";
            lblSubmitter.Text = submitter != null ? submitter.FullName : "";
            lblSubmittedDate.Text = rep.SubmittedDate != DateTime.MinValue ? rep.SubmittedDate.ToString("yyyy-MM-dd HH:mm") : "-";

            lblDeviation.Text = rep.DeviationReason;
            lblRemedial.Text = rep.RemedialActions;
            lblDueDate.Text = rep.RemedialDueDate.HasValue ? rep.RemedialDueDate.Value.ToString("yyyy-MM-dd") : "-";

            // Evidence
            var evidence = evDAL.ListByReport(reportID)
                .Select(x => new { x.EvidenceID, x.FileName, x.UploadedDate });

            gvEvidence.DataSource = evidence.ToList();
            gvEvidence.DataBind();

            // Keep ReportID in ViewState for Approve/Reject
            ViewState["ReportID"] = reportID;
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int reportID = (int)ViewState["ReportID"];

            wfDAL.SetApproval(reportID, APPROVAL_APPROVED, CurrentUserID, txtComments.Text);

            lblMsg.CssClass = "alert alert-success";
            lblMsg.Text = "Approved.";

            pnlDetails.Visible = false;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            int reportID = (int)ViewState["ReportID"];

            wfDAL.SetApproval(reportID, APPROVAL_REJECTED, CurrentUserID, txtComments.Text);

            lblMsg.CssClass = "alert alert-success";
            lblMsg.Text = "Rejected.";

            pnlDetails.Visible = false;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }
}
