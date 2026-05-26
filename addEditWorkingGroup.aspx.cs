using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addEditWorkingGroup : System.Web.UI.Page
{
    private cls_WorkingGroupDAL dal = new cls_WorkingGroupDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadClusters();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = int.Parse(Request.QueryString["id"]);
                LoadWorkingGroup(id);
            }
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
        int clusterId = int.Parse(ddlCluster.SelectedValue);
        LoadLeadInstitutions(clusterId);
    }

    private void LoadLeadInstitutions(int clusterId)
    {
        DataTable dt = dal.GetInstitutionsByCluster(clusterId);
        ddlLeadInstitution.DataSource = dt;
        ddlLeadInstitution.DataTextField = "InstitutionName";
        ddlLeadInstitution.DataValueField = "InstitutionID";
        ddlLeadInstitution.DataBind();
        ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Institution --", "0"));
    }

    private void LoadWorkingGroup(int id)
    {
        DataRow dr = dal.GetWorkingGroupById(id);
        if (dr != null)
        {
            txtWGName.Text = dr["WG_Name"].ToString();
            txtWGDescription.Text = dr["WG_Description"].ToString();
            ddlCluster.SelectedValue = dr["ClusterID"].ToString();
            LoadLeadInstitutions(int.Parse(dr["ClusterID"].ToString()));
            ddlLeadInstitution.SelectedValue = dr["LeadInstitutionID"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtWGName.Text.Trim();
        string description = txtWGDescription.Text.Trim();
        int leadInstitutionId = int.Parse(ddlLeadInstitution.SelectedValue);

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            int id = int.Parse(Request.QueryString["id"]);
            dal.UpdateWorkingGroup(id, name, description, leadInstitutionId);
            lblMessage.Text = "Working Group updated successfully.";
        }
        else
        {
            dal.InsertWorkingGroup(name, description, leadInstitutionId);
            lblMessage.Text = "Working Group added successfully.";
        }
    }
}
