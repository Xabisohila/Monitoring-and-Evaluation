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

public partial class i_MyTasks : System.Web.UI.Page
{
    // Adjust these role IDs to match your seed data
    private const int ROLEWGCOORDINATOR = 2; // example
    private const int ROLEOTPVALIDATOR = 3; // example
    private const int ROLEHOD = 5; // example
    private const int ROLECEO = 6; // example
    private const int ROLEWGCONVENOR = 4; // example

    // Adjust to your seeded status IDs
    private const int STATUSQAFAIL = 4;

    public int CurrentRoleID { get; private set; }
    public object CurrentUserID { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Get current user's role ID from Session (adjust as needed)
            //CurrentRoleID = Convert.ToInt32(Session["CurrentRoleID"]);
            CurrentRoleID = 29; // example, replace with actual logic to get current user's role ID
            CurrentUserID = 34; // example, replace with actual logic to get current user's ID

            ddlFY.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select FY --", ""));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025/2026", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026/2027", "2026"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2027/2028", "2027"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2028/2029", "2028"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2029/2030", "2029"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2030/2031", "2030"));

            BindAll();
        }
    }

    protected void FiltersChanged(object sender, EventArgs e)
    {
        BindAll();
    }

    protected void btnRefreshClick(object sender, EventArgs e)
    {
        BindAll();
    }

    private void BindAll()
    {
        //// Show/hide sections by role
        //pnlQA.Visible = (CurrentRoleID == ROLEWGCOORDINATOR || CurrentRoleID == ROLEOTPVALIDATOR);
        //pnlApproval.Visible = (CurrentRoleID == ROLEHOD || CurrentRoleID == ROLECEO);
        //pnlSignoff.Visible = (CurrentRoleID == ROLEWGCONVENOR);
        //pnlCorrections.Visible = true; // submitter corrections always on

        pnlQA.Visible = true; // For testing, show all sections; adjust as needed for production
                              //pnlApproval.Visible = true;
        pnlSignoff.Visible = false;

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
                cmd.Parameters.AddWithValue("@UserID", 1); // Hardcoded for testing; replace with above line in production
            cmd.Parameters.AddWithValue("@FY", fy);
            cmd.Parameters.AddWithValue("@Q", q);
            cmd.Parameters.AddWithValue("@StatusFail", STATUSQAFAIL);

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
    protected void gvQARowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            // Redirect to QA Inbox (filtered by FY/Q)
            Response.Redirect("~/i_QAInbox.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
        }
    }
    protected void gvApprovalRowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            Response.Redirect("~/i_Approval.aspx?fy=" + ddlFY.SelectedValue + "&q=" + ddlQuarter.SelectedValue);
        }
    }
    protected void gvSignoffRowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
            int reportID = Convert.ToInt32(gvCorrections.DataKeys[rowIndex].Value);
            Response.Redirect("~/i_ReportDetails.aspx?rid=" + reportID);
        }
    }
}
