using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POA : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack)
            {
                ddlStrategicPriority.Items.Add("Inclusive Growth and Job Creation");
                ddlStrategicPriority.Items.Add("Social Protection and Human Development");

                ddlWorkGroup.Items.Add("Working Group 1");
                ddlWorkGroup.Items.Add("Working Group 2");
                ddlWorkGroup.Items.Add("Working Group 3");
                ddlWorkGroup.Items.Add("Working Group 4");
            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        Session["testStrategicPriority"] = ddlStrategicPriority.SelectedValue;
        Session["testYear"] = ddlYear.SelectedValue;
        string mode = Session["testMode"].ToString();
        Session["testYWorkingGroup"] = ddlWorkGroup.SelectedValue;

        if (mode == "Planning")
            Response.Redirect("testPlanning.aspx");
        else
            Response.Redirect("testMonitoring.aspx");
    }
}


