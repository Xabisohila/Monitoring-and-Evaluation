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

            lblInfo.Text = "Strategic Priority: {" + Session["testStrategicPriority"] + "},  Cluster: {" + Session["testCluster"] + "}, Year: {" + Session["testYear"] +"}";

            
            if (!IsPostBack)
            {
                labelInfo();
                gvMonitoring.DataSource = GetMonitoringData();
                gvMonitoring.DataBind();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }




    protected void labelInfo()
    {
        lbl_cluster.Text = Session["testCluster"].ToString();
        lbl_Priority.Text = Session["testStrategicPriority"].ToString();
        lbl_PDPG.Text = string.Empty;
        lbl_WorkingGroup.Text = Session["testYWorkingGroup"].ToString();
        lbl_Year.Text = Session["testYear"].ToString();
    }

    private List<dynamic> GetMonitoringData()
    {
        return new List<dynamic> {
            new { Name = "Support agro-processing", ActualSpend = 25000000, Deviation = -5000000 },
            new { Name = "Youth enterprise fund", ActualSpend = 22000000, Deviation = 2000000 }
        };
    }

    protected void gvMonitoring_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvMonitoring.EditIndex = e.NewEditIndex;
        gvMonitoring.DataSource = GetMonitoringData();
        gvMonitoring.DataBind();
    }

    protected void gvMonitoring_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvMonitoring.EditIndex = -1;
        gvMonitoring.DataSource = GetMonitoringData();
        gvMonitoring.DataBind();
    }

    protected void gvMonitoring_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvMonitoring.EditIndex = -1;
        gvMonitoring.DataSource = GetMonitoringData();
        gvMonitoring.DataBind();
    }
}


