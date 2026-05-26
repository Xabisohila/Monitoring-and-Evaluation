using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class designedAddSubOutcome : System.Web.UI.Page
{

    private cls_SubOutcomeDAL dal = new cls_SubOutcomeDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadClusters();
        }
    }

    private void LoadClusters()
    {
        DataTable dt = dal.GetAllClusters();
        ddlCluster.DataSource = dt;
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Cluster --", "0"));
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        LoadPriorities(clusterId);
    }

    private void LoadPriorities(int clusterId)
    {
        DataTable dt = dal.GetPrioritiesByCluster(clusterId);
        ddlPriority.DataSource = dt;
        ddlPriority.DataTextField = "PriorityName";
        ddlPriority.DataValueField = "PMTDP_PriorityID";
        ddlPriority.DataBind();
        ddlPriority.Items.Insert(0, new ListItem("-- Select Priority --", "0"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int clusterId = int.Parse(ddlCluster.SelectedValue);
            int priorityId = int.Parse(ddlPriority.SelectedValue);
            string subOutcome = txtSubOutcome.Text.Trim();

            bool success = dal.InsertSubOutcome(clusterId, priorityId, subOutcome);

            if (success)
            {
                lblMessage.Text = "SubOutcome added successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                txtSubOutcome.Text = "";
                ddlCluster.SelectedIndex = 0;
                ddlPriority.Items.Clear();
            }
            else
            {
                lblMessage.Text = "Error adding SubOutcome.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

}