using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class addEditLeadingInstitution : System.Web.UI.Page
{
    private cls_LeadingInstitutionDAL dal = new cls_LeadingInstitutionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInstitutionTypes();
            LoadClusters();

            if (Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                LoadInstitution(id);
                lblTitle.Text = "Edit Leading Institution";
                btnDelete.Visible = true;
            }
        }
    }

    private void LoadInstitutionTypes()
    {
        ddlInstitutionType.Items.Clear();
        ddlInstitutionType.Items.Add(new ListItem("-- Select Type --", "0"));
        ddlInstitutionType.Items.Add(new ListItem("Government", "Government"));
        ddlInstitutionType.Items.Add(new ListItem("Private", "Private"));
        ddlInstitutionType.Items.Add(new ListItem("NGO", "NGO"));
        ddlInstitutionType.Items.Add(new ListItem("Other", "Other"));
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

    private void LoadInstitution(int id)
    {
        DataRow dr = dal.GetInstitutionById(id);
        if (dr != null)
        {
            txtInstitutionName.Text = dr["InstitutionName"].ToString();
            ddlInstitutionType.SelectedValue = dr["InstitutionType"].ToString();
            ddlCluster.SelectedValue = dr["ClusterID"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string name = txtInstitutionName.Text.Trim();
            string type = ddlInstitutionType.SelectedValue;
            int clusterId = int.Parse(ddlCluster.SelectedValue);

            bool success;
            if (Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                success = dal.UpdateInstitution(id, name, type, clusterId);
            }
            else
            {
                success = dal.InsertInstitution(name, type, clusterId);
            }

            if (success)
            {
                lblMessage.Text = "Institution saved successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Error saving institution.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int id = int.Parse(Request.QueryString["id"]);
            bool success = dal.DeleteInstitution(id);

            if (success)
            {
                lblMessage.Text = "Institution deleted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                txtInstitutionName.Text = "";
                ddlInstitutionType.SelectedIndex = 0;
                ddlCluster.SelectedIndex = 0;
                btnDelete.Visible = false;
            }
            else
            {
                lblMessage.Text = "Error deleting institution.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
