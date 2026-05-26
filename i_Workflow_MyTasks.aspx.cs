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

public partial class i_Workflow_MyTasks : System.Web.UI.Page
{

    // Adjust these role IDs to match your seed data
    private const int ROLE_WG_COORDINATOR = 2; // example
    private const int ROLE_OTP_VALIDATOR = 3; // example
    private const int ROLE_HOD = 5; // example
    private const int ROLE_CEO = 6; // example
    private const int ROLE_WG_CONVENOR = 4; // example

    // Adjust to your seeded status IDs
    private const int STATUS_QA_FAIL = 4;

    public int CurrentRoleID { get; private set; }// Assume single role for simplicity; adjust if users can have multiple roles
    public object CurrentUserID { get; private set; } // Set this to the appropriate type (e.g., int) based on your UserID type

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026", "2026"));
            BindAll();
        }
    }

    protected void FiltersChanged(object sender, EventArgs e)
    {
        BindAll();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindAll();
    }

    private void BindAll()
    {
        // Show/hide sections by role
        pnlQA.Visible = (CurrentRoleID == ROLE_WG_COORDINATOR || CurrentRoleID == ROLE_OTP_VALIDATOR);
        pnlApproval.Visible = (CurrentRoleID == ROLE_HOD || CurrentRoleID == ROLE_CEO);
        pnlSignoff.Visible = (CurrentRoleID == ROLE_WG_CONVENOR);
        pnlCorrections.Visible = true; // submitter corrections always on

        if (!string.IsNullOrEmpty(ddlFY.SelectedValue))
        {
            int fy = Convert.ToInt32(ddlFY.SelectedValue);
            int q = Convert.ToInt32(ddlQuarter.SelectedValue);

            if (pnlQA.Visible)
                BindAwaiting("i_sp_Report_AwaitingQA", fy, q, gvQA);

            if (pnlApproval.Visible)
                BindAwaiting("i_sp_Report_AwaitingApproval", fy, q, gvApproval);

            if (pnlSignoff.Visible)
                BindAwaiting("sp_Report_AwaitingSignoff", fy, q, gvSignoff);

            BindCorrections(fy, q);
        }
    }

    private void BindAwaiting(string proc, int fy, int q, System.Web.UI.WebControls.GridView gv)
    {
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand(proc, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", fy);
            cmd.Parameters.AddWithValue("@QuarterNumber", q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                var dt = new DataTable();
                dt.Load(rdr);
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
    }

    // Corrections required (latest workflow is QA & Fail for current user's submissions)
    private void BindCorrections(int fy, int q)
    {
        string sql = @"
SELECT qr.ReportID, i.IndicatorName, qr.SubmittedDate, qr.ActualValue, qr.Achieved
FROM i_QuarterlyReports qr
JOIN i_QuarterlyTargets qt ON qt.QuarterlyTargetID = qr.QuarterlyTargetID
JOIN i_AnnualTargets atg ON atg.AnnualTargetID = qt.AnnualTargetID
JOIN i_Indicators i ON i.IndicatorID = atg.IndicatorID
OUTER APPLY (
    SELECT TOP 1 wh.*
    FROM i_WorkflowHistory wh
    WHERE wh.ReportID = qr.ReportID
    ORDER BY wh.ActionDate DESC
) lastwh
WHERE qr.SubmittedByUserID = @UserID
  AND atg.FinancialYear    = @FY
  AND qr.QuarterNumber     = @Q
  AND lastwh.Stage         = 'QA'
  AND lastwh.StatusID      = @StatusFail
ORDER BY qr.SubmittedDate DESC;";

        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand(sql, con))
        {
            //cmd.Parameters.AddWithValue("@UserID", CurrentUserID);
            cmd.Parameters.AddWithValue("@UserID", 1);
            cmd.Parameters.AddWithValue("@FY", fy);
            cmd.Parameters.AddWithValue("@Q", q);
            cmd.Parameters.AddWithValue("@StatusFail", STATUS_QA_FAIL);

            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                var dt = new DataTable();
                dt.Load(rdr);
                gvCorrections.DataSource = dt;
                gvCorrections.DataBind();
            }
        }
    }

    // Open actions (navigate to existing pages)
    protected void gvQA_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            // Redirect to QA Inbox (filtered by FY/Q)
            //Response.Redirect("~/Workflow/QAInbox.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
            Response.Redirect("~/i_QAInbox.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
        }
    }
    protected void gvApproval_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            Response.Redirect("~/i_Approval.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
        }
    }
    protected void gvSignoff_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            Response.Redirect("~/i_Signoff.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
        }
    }
    protected void gvCorrections_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            
            // Use DataKeys to safely retrieve the ReportID
            // Make sure DataKeyNames="ReportID" is set on the GridView in the .aspx file
            if (gvCorrections.DataKeys.Count > rowIndex)
            {
                int reportID = Convert.ToInt32(gvCorrections.DataKeys[rowIndex].Value);
                Response.Redirect("~/i_ReportDetails.aspx?rid=" + reportID);
            }
            else
            {
                // Fallback: Try to get the value from the row
                GridViewRow row = gvCorrections.Rows[rowIndex];
                // Assuming ReportID is in a hidden field or specific control
                int reportID = Convert.ToInt32(((DataBoundLiteralControl)row.Cells[0].Controls[0]).Text);
                Response.Redirect("~/i_ReportDetails.aspx?rid=" + reportID);
            }
        }
    }
}
