using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Selection : System.Web.UI.Page
{
    clsMnERecord oRecord = new clsMnERecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (IsPostBack != true)
            {
                populateFinancialYear();
                populateQuarter();
                divQuarter.Visible = false;
                string cluster = Request.QueryString["cluster"];
                Session["Cluster"] = cluster;

                if (cluster == "1")
                {
                    Session["ClusterWGName"] = "Governance, State Capacity & Institutional Development";
                }
                else if (cluster == "2")
                {
                    Session["ClusterWGName"] = "Social Protection, Community And Human Development";
                }
                else if (cluster == "3")
                {
                    Session["ClusterWGName"] = "Economic Sector Investment, Employment & Infrastructure Development";

                }
                else if (cluster == "4")
                {
                    Session["ClusterWGName"] = "Justice, Crime Prevention and Security";

                }
                int urlCluster = Convert.ToInt16(cluster);
                populateStrategicPriority(urlCluster);
                populateWorkingGroup(urlCluster);
                Session["Quarter"] = ddlQuarter.SelectedItem.Text;
                lblError.Visible = false;


            }
            else
            {
                if (ddlQuarter.SelectedItem.Text == "Quarter 1")
                {
                    Session["Planned"] = "Q1-Planned-T";
                    Session["Actual"] = "Q1-Actual";
                    Session["Var"] = "Q1-Var";
                    Session["Total"] = "Q1-Total";
                    Session["YrVar"] = "Q1:Yr-Var";

                }
                else if (ddlQuarter.SelectedItem.Text == "Quarter 2")
                {
                    Session["Planned"] = "Q2 Planned-T";
                    Session["Actual"] = "Q2-Actual";
                    Session["Var"] = "Q2-Var";
                    Session["Total"] = "Q2-Total";
                    Session["YrVar"] = "Q2:Yr-Var";
                }
                else if (ddlQuarter.SelectedItem.Text == "Quarter 3")
                {
                    Session["Planned"] = "Q3 Planned-T";
                    Session["Actual"] = "Q3-Actual";
                    Session["Var"] = "Q3-Var";
                    Session["Total"] = "Q3-Total";
                    Session["YrVar"] = "Q3:Yr-Var";
                }
                else if (ddlQuarter.SelectedItem.Text == "Quarter 4")
                {
                    Session["Planned"] = "Q4 Planned-T";
                    Session["Actual"] = "Q4-Actual";
                    Session["Var"] = "Q4-Var";
                    Session["Total"] = "Q4-Total";
                    Session["YrVar"] = "Q4:Yr-Var";
                }

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
       
    }
    public void btnProceed_Click(object sender, EventArgs e)
    {
        
        if ((rbPlanning.Checked == false) && (rbMonitoring.Checked == false))
        {
            lblError.Text = "Please specified whether you want planning or Monitoring";
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
        }
        else if (ddlStrategicPriority.SelectedIndex == 0)
        {
            lblError.Text = "Please select Strategic Priority";
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
        }
        else if (ddlFinancialYear.SelectedIndex==0)
        {
            lblError.Text = "Please select Financial Year";
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
        }
        else if ((rbMonitoring.Checked == true) && (ddlQuarter.SelectedIndex == 0))
        {
            lblError.Text = "Please select Quarter";
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
        }
        else
        {
            Session["StrategicPriority"] = ddlStrategicPriority.SelectedValue;
            Session["StrategicPriorityName"] = ddlStrategicPriority.SelectedItem.Text;
            Session["Quarter"] = ddlQuarter.SelectedItem.Text;
            Session["FinancialYear"] = ddlFinancialYear.SelectedValue;
            Session["FinancialYearText"] = ddlFinancialYear.SelectedItem.Text;

            if (rbPlanning.Checked == true)
            {
                //Response.Redirect("PlanningST.aspx");
                //Response.Redirect("editPlanningST.aspx");
                //Response.Redirect("code/newPlanningST.aspx");
                //Response.Redirect("code/finalPlanningST.aspx");
                
                if (Convert.ToInt32(ddlFinancialYear.SelectedValue) >= 56)
                {
                    Response.Redirect("lastPlanningST.aspx");
                }
                else
                {
                    Response.Redirect("PlanningST.aspx");
                }
            }
            else if (rbMonitoring.Checked == true)
            {
                Response.Redirect("Monitoring.aspx");
            }
            
        }

   
    }

    public void populateFinancialYear()
    {
        ddlFinancialYear.DataSource = oRecord.Select_FinancialYear();
        ddlFinancialYear.DataValueField = "FinancialYearId";
        ddlFinancialYear.DataTextField = "FinancialYear";
        ddlFinancialYear.DataBind();
        ddlFinancialYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Financial Year", "0"));
    }

    public void populateQuarter()
    {
        ddlQuarter.DataSource = oRecord.Select_Quarter();
        ddlQuarter.DataValueField = "QuarterId";
        ddlQuarter.DataTextField = "Quarter";
        ddlQuarter.DataBind();
        ddlQuarter.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Quarter", "0"));
    }

    public void populateStrategicPriority(int urlCluster)
    {
        ddlStrategicPriority.DataSource = oRecord.Select_StrategicPriority(urlCluster);
        ddlStrategicPriority.DataValueField = "StrategicPriorityId";
        ddlStrategicPriority.DataTextField = "StrategicPriorityName";
        ddlStrategicPriority.DataBind();
        ddlStrategicPriority.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select PMTDP PRIORITY ", "0"));
    }

    public void populateWorkingGroup(int urlCluster)
    {
        ddlWorkingGroup.DataSource = oRecord.Select_WorkingGroup(urlCluster);
        ddlWorkingGroup.DataValueField = "WorkingGroupId";
        ddlWorkingGroup.DataTextField = "WorkingGroup";
        ddlWorkingGroup.DataBind();
        ddlWorkingGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select Working Group ", "0"));
    }
    protected void rbPlanning_CheckedChanged(object sender, EventArgs e)
    {
        if (rbPlanning.Checked == true)
        {
            divQuarter.Visible = false;
        }
        else if (rbMonitoring.Checked == true)
        {
            divQuarter.Visible = true;
        }
    }
    protected void rbMonitoring_CheckedChanged(object sender, EventArgs e)
    {
        if (rbMonitoring.Checked == true)
        {
            divQuarter.Visible = true;
        }
        else if (rbMonitoring.Checked == false)
        {
            divQuarter.Visible = false;
        }
        //else if ((rbMonitoring.Checked == true) && (ddlQuarter.SelectedIndex == 0))
        //{
        //    lblError.Text = "Please select Quarter";
        //    lblError.Visible = true;
        //    lblError.ForeColor = Color.Red;
        //}
    }
}