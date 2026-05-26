using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addEditPriority : System.Web.UI.Page
{
    private cls_PriorityDAL dal = new cls_PriorityDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPDPs();
            LoadClusters();

            if (Request.QueryString["id"] != null)
            {
                int priorityId = Convert.ToInt32(Request.QueryString["id"]);
                LoadPriority(priorityId);
                btnDelete.Visible = true;
            }
        }
    }

    private void LoadPDPs()
    {
        DataTable dt = dal.GetAllPDPs();
        ddlPDP.DataSource = dt;
        ddlPDP.DataTextField = "PDP_Name";
        ddlPDP.DataValueField = "PDP_ID";
        ddlPDP.DataBind();
        ddlPDP.Items.Insert(0, new ListItem("-- Select PDP --", "0"));
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

    private void LoadPriority(int priorityId)
    {
        DataTable dt = dal.GetPriorityById(priorityId);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            ddlPDP.SelectedValue = row["PDP_ID"].ToString();
            ddlCluster.SelectedValue = row["ClusterID"].ToString();
            txtPriorityName.Text = row["PriorityName"].ToString();
            txtDescription.Text = row["PriorityDescription"].ToString();
            txtDesiredOutcome.Text = row["DesiredOutcome"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int pdpId = Convert.ToInt32(ddlPDP.SelectedValue);
            int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
            string name = txtPriorityName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string outcome = txtDesiredOutcome.Text.Trim();

            bool success;
            if (Request.QueryString["id"] != null)
            {
                int priorityId = Convert.ToInt32(Request.QueryString["id"]);
                success = dal.UpdatePriority(priorityId, pdpId, clusterId, name, description, outcome);
            }
            else
            {
                success = dal.InsertPriority(pdpId, clusterId, name, description, outcome);
            }

            lblMessage.Text = success ? "Saved successfully." : "Error saving priority.";
            lblMessage.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int priorityId = Convert.ToInt32(Request.QueryString["id"]);
            bool success = dal.DeletePriority(priorityId);
            lblMessage.Text = success ? "Deleted successfully." : "Error deleting priority.";
            lblMessage.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }
}
