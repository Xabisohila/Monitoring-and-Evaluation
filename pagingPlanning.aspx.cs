using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AjaxControlToolkit;


public partial class pagingPlanning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Text = (string)Session["Cluster_ID"];
            Label2.Text = (string)Session["selectType"];
            Label3.Text = (string)Session["ddlWorkingGroupID"];
            Label4.Text = (string)Session["PMTDP_PriorityID"];
            Label5.Text = (string)Session["FY_ID"];
            Label8.Text = (string)Session["ddlQuarterID"];
        }
    }


}