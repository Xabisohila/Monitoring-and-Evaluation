using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addEditIntervention : System.Web.UI.Page
{
    cls_InterventionDAL dal = new cls_InterventionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdowns();
            if (Request.QueryString["id"] != null)
            {
                LoadIntervention(Convert.ToInt32(Request.QueryString["id"]));
                btnDelete.Visible = true;
            }
        }
    }

    private void LoadDropdowns()
    {
        ddlPOA.DataSource = dal.GetAllPOAs();
        ddlPOA.DataTextField = "POA_Name";
        ddlPOA.DataValueField = "POA_ID";
        ddlPOA.DataBind();

        ddlInstitution.DataSource = dal.GetAllInstitutions();
        ddlInstitution.DataTextField = "InstitutionName";
        ddlInstitution.DataValueField = "InstitutionID";
        ddlInstitution.DataBind();

        ddlWorkingGroup.DataSource = dal.GetAllWorkingGroups();
        ddlWorkingGroup.DataTextField = "WG_Name";
        ddlWorkingGroup.DataValueField = "WorkingGroupID";
        ddlWorkingGroup.DataBind();

        ddlMunicipality.DataSource = dal.GetAllMunicipalities();
        ddlMunicipality.DataTextField = "MunicipalityName";
        ddlMunicipality.DataValueField = "MunicipalityID";
        ddlMunicipality.DataBind();

        ddlSubOutcome.DataSource = dal.GetAllSubOutcomes();
        ddlSubOutcome.DataTextField = "SubOutcome";
        ddlSubOutcome.DataValueField = "SubOutcomeID";
        ddlSubOutcome.DataBind();
    }

    private void LoadIntervention(int id)
    {
        DataRow dr = dal.GetInterventionById(id);
        if (dr != null)
        {
            ddlPOA.SelectedValue = dr["POA_ID"].ToString();
            ddlInstitution.SelectedValue = dr["InstitutionID"].ToString();
            ddlWorkingGroup.SelectedValue = dr["WorkingGroupID"].ToString();
            ddlMunicipality.SelectedValue = dr["MunicipalityID"].ToString();
            ddlSubOutcome.SelectedValue = dr["SubOutcomeID"].ToString();
            txtInterventionName.Text = dr["InterventionName"].ToString();
            txtDescription.Text = dr["Description"].ToString();
            txtStartYear.Text = dr["StartYear"].ToString();
            txtEndYear.Text = dr["EndYear"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int id = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
        bool success = dal.SaveIntervention(id,
            Convert.ToInt32(ddlPOA.SelectedValue),
            Convert.ToInt32(ddlInstitution.SelectedValue),
            Convert.ToInt32(ddlWorkingGroup.SelectedValue),
            Convert.ToInt32(ddlMunicipality.SelectedValue),
            Convert.ToInt32(ddlSubOutcome.SelectedValue),
            txtInterventionName.Text.Trim(),
            txtDescription.Text.Trim(),
            txtStartYear.Text.Trim(),
            txtEndYear.Text.Trim());

        lblMessage.Text = success ? "Saved successfully." : "Error saving intervention.";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);
        bool success = dal.DeleteIntervention(id);
        lblMessage.Text = success ? "Deleted successfully." : "Error deleting intervention.";
    }

    protected void btnAddIndicator_Click(object sender, EventArgs e)
    {
        // Add indicator logic
    }

    protected void btnAddBudget_Click(object sender, EventArgs e)
    {
        // Add budget logic
    }

    protected void btnUploadDocument_Click(object sender, EventArgs e)
    {
        // Upload document logic
    }
}









