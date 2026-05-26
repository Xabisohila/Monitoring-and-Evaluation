using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageInterventionDetails : System.Web.UI.Page
{
    Intervention Intervention = new Intervention();
    InterventionDAL InterventionDAL = new InterventionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["Fullname"] != null)
        {    
            if (!IsPostBack)
            {
                int interventionID = 801;

                // Get InterventionID from query string like ?id=123
                if (int.TryParse(Request.QueryString["id"], out interventionID))
                {
                    BindInterventionData(interventionID);
                }
                else
                {
                    // Optional: Show a message or redirect
                    // lblMessage.Text = "Invalid or missing Intervention ID.";
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void populateInterventionDetails()
    {
        try
        {
            gvIntervention1.DataSource = InterventionDAL.GetInterventionDetails(801);
            gvIntervention1.AutoGenerateColumns = false;
            gvIntervention1.DataBind();

        }
        catch (Exception ex)
        {

        }
    }

    protected void gvIntervention1_DataBound(object sender, EventArgs e)
    {

    }

  

    private void BindInterventionData(int interventionID)
    {
        DataSet ds = InterventionDAL.GetInterventionDetails(interventionID);

        if (ds.Tables.Contains("Intervention"))
        {
            GridViewIntervention.DataSource = ds.Tables["Intervention"];
            GridViewIntervention.DataBind();
        }

        if (ds.Tables.Contains("Indicators"))
        {
            GridViewIndicators.DataSource = ds.Tables["Indicators"];
            GridViewIndicators.DataBind();
        }

        if (ds.Tables.Contains("Budgets"))
        {
            GridViewBudgets.DataSource = ds.Tables["Budgets"];
            GridViewBudgets.DataBind();
        }

        if (ds.Tables.Contains("Reports"))
        {
            GridViewReports.DataSource = ds.Tables["Reports"];
            GridViewReports.DataBind();
        }
    }

}


