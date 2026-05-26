using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class intervention_content : System.Web.UI.Page
{
    clsMnERecord oRecord = new clsMnERecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (IsPostBack != true)
            {



                populateCluster();

                lblError.Visible = false;


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
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            oRecord.InterventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            oRecord.SubOutcome = Convert.ToInt32(ddlSuboutcome.SelectedValue);
            oRecord.StrategicPriority = Convert.ToInt32(ddlStrategicPriority.SelectedValue);
            oRecord.Cluster = Convert.ToInt32(ddlCluster.SelectedValue);
            oRecord.KeyResults = txtKeyResultNo.Text;
            oRecord.KeyIndicator = txtKeyIndicator.Text;
            oRecord.ResponsibleInstitution = txtResponsibleInstitution.Text;
            oRecord.SubmitInterventionData();

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Information Submitted Successfully.')", true);

            ddlIntervention.SelectedIndex = 0;
            ddlSuboutcome.SelectedIndex = 0;
            ddlStrategicPriority.SelectedIndex = 0;
            ddlCluster.SelectedIndex = 0;

            txtKeyResultNo.Text = "";
            txtKeyIndicator.Text = "";
            txtResponsibleInstitution.Text = "";


        }
        catch (Exception ex){

        }


    }

    public void populateIntervention()
    {
        ddlIntervention.DataSource = oRecord.Select_Intervention(Convert.ToInt16(ddlSuboutcome.SelectedValue));
        ddlIntervention.DataValueField = "InterventionId";
        ddlIntervention.DataTextField = "Intervention";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Intervenstion", "0"));
    }

    public void populateSubOutcome()
    {
        ddlSuboutcome.DataSource = oRecord.Select_Suboutcome(Convert.ToInt16(ddlCluster.SelectedValue));
        ddlSuboutcome.DataValueField = "SuboutcomeId";
        ddlSuboutcome.DataTextField = "Suboutcome";
        ddlSuboutcome.DataBind();
        ddlSuboutcome.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select PMTDP OUTCOMES", "0"));
    }

    public void populateStrategicPriority()
    {
        ddlStrategicPriority.DataSource = oRecord.Select_StrategicPriority(Convert.ToInt16(ddlCluster.SelectedValue));
        ddlStrategicPriority.DataValueField = "StrategicPriorityId";
        ddlStrategicPriority.DataTextField = "StrategicPriorityName";
        ddlStrategicPriority.DataBind();
        ddlStrategicPriority.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select PMTDP PRIORITY", "0"));
    }

    public void populateCluster()
    {
        ddlCluster.DataSource = oRecord.Select_Cluster();
        ddlCluster.DataValueField = "ClusterId";
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Cluster", "0"));
    }


    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCluster.SelectedIndex != 0)
        {
            populateStrategicPriority();
            populateSubOutcome();
        }
    }


    protected void ddlSuboutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuboutcome.SelectedIndex != 0)
        {
            populateIntervention();
        }
    }

    public void lbAddStrategicPriority_Click(object sender, EventArgs e)
    {
        lblAdd.Text = "Add Sub outcome";
       
        lblAdd.Visible = true;
        txtAdd.Visible = true;
        btnSubmitAdd.Visible = true;
        Session["AddSuboutcome"] = "AddSuboutcome";
    }

    public void lbAddIntervention_Click(object sender, EventArgs e)
    {
        lblAdd.Text = "Add Intervention";
        
        lblAdd.Visible = true;
        txtAdd.Visible = true;
        btnSubmitAdd.Visible = true;
        Session["AddIntervention"] = "AddIntervention";
    }

    public void btnSubmitAdd_Click(object sender, EventArgs e)
    {
        if (Session["AddSuboutcome"] == "AddSuboutcome")
            if ((ddlStrategicPriority.SelectedIndex != 0) && (ddlCluster.SelectedIndex != 0))
            {
                oRecord.SuboutcomeInfo = txtAdd.Text;
                oRecord.Cluster = Convert.ToInt16(ddlCluster.SelectedValue);
                oRecord.StrategicPriority = Convert.ToInt16(ddlStrategicPriority.SelectedValue);
                oRecord.AddSubOutcome(Convert.ToInt16(ddlCluster.SelectedValue), Convert.ToInt16(ddlStrategicPriority.SelectedValue), txtAdd.Text);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Suboutcome Submitted Successfully.')", true);
                populateSubOutcome();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please select a cluster and Strategic Priority first.')", true);
            }
        else if (Session["AddIntervention"] == "AddIntervention")
        {

            if ((ddlSuboutcome.SelectedIndex != 0) && (ddlCluster.SelectedIndex != 0))
            {
                oRecord.Intervention = txtAdd.Text;
                oRecord.SubOutcome = Convert.ToInt16(ddlSuboutcome.SelectedValue);
                oRecord.AddIntervention(Convert.ToInt16(ddlSuboutcome.SelectedValue), txtAdd.Text);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Intevention Submitted Successfully.')", true);
                populateIntervention();

             }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please select a cluster and Suboutcome first.')", true);
            }
        }
    }

        
}


