using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class approvalIntervention : System.Web.UI.Page
{
    InterventionDAL repo = new InterventionDAL();
    int userID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindInterventions();
            BindInterventions_Approved();

        }
    }

    private void BindInterventions()
    {
        DataTable dt = repo.GetPendingInterventions();
        gvInterventions.DataSource = dt;
        gvInterventions.DataBind();
    }

    private void BindInterventions_Approved()
    {
        DataTable dt = repo.GetApprovedInterventions();
        gvApprovedInterventions.DataSource = dt;
        gvApprovedInterventions.DataBind();
    }

    protected void gvInterventions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve" || e.CommandName == "Decline")
        {
            userID = Convert.ToInt32(Session["dbUserID"]);
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvInterventions.Rows[index];
            int interventionId = Convert.ToInt32(gvInterventions.DataKeys[index].Value);
            int approverUserId = userID; // Example user ID, replace with session/user context
            string status = e.CommandName == "Approve" ? "Approved" : "Declined";

            repo.ApproveIntervention(interventionId, approverUserId, status, "Reviewed via UI");
            BindInterventions();
        }
    }

    protected void gvInterventions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int interventionId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "InterventionID"));

            GridView gvIndicators = (GridView)e.Row.FindControl("gvIndicators");
            gvIndicators.DataSource = repo.GetIndicatorsByIntervention(interventionId);
            gvIndicators.DataBind();

            GridView gvBudgets = (GridView)e.Row.FindControl("gvBudgets");
            gvBudgets.DataSource = repo.GetBudgetsByIntervention(interventionId);
            gvBudgets.DataBind();
        }
    }

    // FIX: Robust handling of RowCommand for approved interventions (C#5 compliant)
    // Steps:
    // 1. Validate CommandName.
    // 2. Parse CommandArgument (row index) and validate bounds.
    // 3. Retrieve DataKey (InterventionID) without null-propagation (C#5).
    // 4. Get current user id; validate (>0).
    // 5. Switch on CommandName:
    //    - Decline: update status, rebind both grids.
    //    - ViewDetails: load DataSet into Session.
    // 6. Guard against invalid states.
    protected void gvApprovedInterventions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e == null) return;
        if (string.IsNullOrWhiteSpace(e.CommandName)) return;

        int index;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out index)) return;
        if (index < 0 || index >= gvApprovedInterventions.Rows.Count) return;

        // C#5-compatible retrieval of DataKey value
        DataKey dataKey = gvApprovedInterventions.DataKeys[index];
        if (dataKey == null) return;
        object key = dataKey.Value;
        if (key == null) return;

        int interventionId;
        if (!int.TryParse(key.ToString(), out interventionId)) return;

        object sessionUser = Session["dbUserID"];
        if (sessionUser == null) return;

        int approverUserId;
        if (!int.TryParse(sessionUser.ToString(), out approverUserId) || approverUserId <= 0) return;

        switch (e.CommandName)
        {
            case "Decline":
                repo.ApproveIntervention(interventionId, approverUserId, "Declined", "Declined after prior approval");
                BindInterventions();
                BindInterventions_Approved();
                break;

            case "ViewDetails":
                try
                {
                    DataSet ds = repo.GetInterventionDetails(interventionId);
                    Session["InterventionDetails"] = ds;
                    // Response.Redirect("interventionDetails.aspx?interventionId=" + interventionId);
                }
                catch
                {
                    // Optionally log exception
                }
                break;
        }
    }

    protected void gvApprovedInterventions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null) return;
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        object idObj = DataBinder.Eval(e.Row.DataItem, "InterventionID");
        int interventionId;
        if (idObj == null || !int.TryParse(idObj.ToString(), out interventionId)) return;

        GridView gvIndicators = e.Row.FindControl("gvApprovedIndicators") as GridView;
        if (gvIndicators != null)
        {
            gvIndicators.DataSource = repo.GetIndicatorsByIntervention(interventionId);
            gvIndicators.DataBind();
        }

        GridView gvBudgets = e.Row.FindControl("gvApprovedBudgets") as GridView;
        if (gvBudgets != null)
        {
            gvBudgets.DataSource = repo.GetBudgetsByIntervention(interventionId);
            gvBudgets.DataBind();
        }
    }
}






