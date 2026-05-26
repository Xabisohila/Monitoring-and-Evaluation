using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testPlanning : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["Fullname"] != null)
        {

            lblInfo.Text = "Strategic Priority: {" + Session["testStrategicPriority"] + "},  Cluster: {" + Session["testCluster"] + "}, Year: {" + Session["testYear"] +"}";
            
            if (!IsPostBack)
            {
                labelInfo();
                gvPlanning.DataSource = GetPlanningData();
                gvPlanning.DataBind();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }



private List<dynamic> GetPlanningData()
    {
        return new List<dynamic> {
            new { Name = "Support agro-processing", Budget = 30000000 },
            new { Name = "Youth enterprise fund", Budget = 20000000 }
        };
    }


    protected void labelInfo()
    {
        lbl_cluster.Text = Session["testCluster"].ToString();
        lbl_Priority.Text = Session["testStrategicPriority"].ToString();
        lbl_PDPG.Text = string.Empty;
        lbl_WorkingGroup.Text = Session["testYWorkingGroup"].ToString();
        lbl_Year.Text = Session["testYear"].ToString();
    }



    protected void gvPlanning_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPlanning.EditIndex = e.NewEditIndex;
        gvPlanning.DataSource = GetPlanningData();
        gvPlanning.DataBind();
    }

    protected void gvPlanning_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvPlanning.EditIndex = -1;
        gvPlanning.DataSource = GetPlanningData();
        gvPlanning.DataBind();
    }

    protected void gvPlanning_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPlanning.EditIndex = -1;
        gvPlanning.DataSource = GetPlanningData();
        gvPlanning.DataBind();
    }
}


