using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageEditIntervention : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            string backUrl = "pagePlanningOverview.aspx";
            if (Request.UrlReferrer != null &&
                (Request.UrlReferrer.AbsolutePath.Contains("pagePlanningOverview.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("pageInterventionsDirectDetail.aspx")))
                backUrl = Request.UrlReferrer.ToString();
            hlBackToOverview.NavigateUrl = backUrl;

            int interventionId;
            if (Request.QueryString["id"] != null &&
                int.TryParse(Request.QueryString["id"], out interventionId))
            {
                hfInterventionID.Value = interventionId.ToString();
                LoadInterventionDetails(interventionId);
            }
            else
            {
                lblMessage.Text      = "No valid Intervention ID provided.";
                lblMessage.CssClass  = "msg-error";
                lblMessage.Visible   = true;
            }
        }
    }

    private void PopulateDropdowns()
    {
        InterventionDAL dal = new InterventionDAL();

        DataTable dtPOAs = dal.GetAllPOAsLookup();
        ddlPOA.DataSource     = dtPOAs;
        ddlPOA.DataTextField  = "POA_Name";
        ddlPOA.DataValueField = "POA_ID";
        ddlPOA.DataBind();
        ddlPOA.Items.Insert(0, new ListItem("-- Select POA --", "0"));

        DataTable dtInstitutions = dal.GetAllLeadInstitutionsLookup();
        ddlLeadInstitution.DataSource     = dtInstitutions;
        ddlLeadInstitution.DataTextField  = "InstitutionName";
        ddlLeadInstitution.DataValueField = "InstitutionID";
        ddlLeadInstitution.DataBind();
        ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Lead Institution --", "0"));

        DataTable dtWorkingGroups = dal.GetAllWorkingGroupsLookup();
        ddlWorkingGroup.DataSource     = dtWorkingGroups;
        ddlWorkingGroup.DataTextField  = "WG_Name";
        ddlWorkingGroup.DataValueField = "WorkingGroupID";
        ddlWorkingGroup.DataBind();
        ddlWorkingGroup.Items.Insert(0, new ListItem("-- Select Working Group --", "0"));

        DataTable dtMunicipalities = dal.GetAllMunicipalitiesLookup();
        ddlMunicipality.DataSource     = dtMunicipalities;
        ddlMunicipality.DataTextField  = "MunicipalityName";
        ddlMunicipality.DataValueField = "MunicipalityID";
        ddlMunicipality.DataBind();
        ddlMunicipality.Items.Insert(0, new ListItem("-- Select Municipality --", "0"));
    }

    private void LoadInterventionDetails(int interventionId)
    {
        InterventionDAL dal = new InterventionDAL();
        DataSet ds = dal.GetInterventionDetails(interventionId);

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            lblMessage.Text     = "Intervention not found for ID " + interventionId + ".";
            lblMessage.CssClass = "msg-error";
            lblMessage.Visible  = true;
            return;
        }

        DataRow r = ds.Tables[0].Rows[0];

        txtInterventionName.Text        = r["InterventionName"].ToString();
        txtInterventionDescription.Text = r["InterventionDescription"] != DBNull.Value ? r["InterventionDescription"].ToString() : string.Empty;
        txtStartYear.Text               = r["InterventionStartYear"]   != DBNull.Value ? r["InterventionStartYear"].ToString()   : string.Empty;
        txtEndYear.Text                 = r["InterventionEndYear"]     != DBNull.Value ? r["InterventionEndYear"].ToString()     : string.Empty;
        txtSpatialReference.Text        = r["SpatialReference"]        != DBNull.Value ? r["SpatialReference"].ToString()        : string.Empty;

        ddlPOA.SelectedValue             = r["ParentPOAID"].ToString();
        ddlLeadInstitution.SelectedValue = r["LeadInstitution_ID"].ToString();
        ddlWorkingGroup.SelectedValue    = r["WorkingGroup_ID"].ToString();
        ddlMunicipality.SelectedValue    = r["PrimaryMunicipality_ID"].ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        InterventionDAL dal = new InterventionDAL();

        int     interventionId = Convert.ToInt32(hfInterventionID.Value);
        string  name           = txtInterventionName.Text.Trim();
        string  description    = string.IsNullOrEmpty(txtInterventionDescription.Text.Trim()) ? null : txtInterventionDescription.Text.Trim();
        int     poaId          = Convert.ToInt32(ddlPOA.SelectedValue);
        int     leadId         = Convert.ToInt32(ddlLeadInstitution.SelectedValue);
        int?    wgId           = ddlWorkingGroup.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlWorkingGroup.SelectedValue) : null;
        int     startYear      = Convert.ToInt32(txtStartYear.Text);
        int     endYear        = Convert.ToInt32(txtEndYear.Text);
        int?    munId          = ddlMunicipality.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlMunicipality.SelectedValue) : null;
        string  spatial        = string.IsNullOrEmpty(txtSpatialReference.Text.Trim()) ? null : txtSpatialReference.Text.Trim();

        int rowsAffected = dal.UpdateIntervention(interventionId, name, description,
                               poaId, leadId, wgId, startYear, endYear, munId, spatial);

        if (rowsAffected > 0)
        {
            lblMessage.Text     = "Intervention updated successfully.";
            lblMessage.CssClass = "msg-success";
        }
        else
        {
            lblMessage.Text     = "Failed to update the intervention. Please try again.";
            lblMessage.CssClass = "msg-error";
        }
        lblMessage.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string id = hfInterventionID.Value;
        if (!string.IsNullOrEmpty(id) && id != "0")
            Response.Redirect("pageInterventionsDirectDetail.aspx?id=" + id);
        else
            Response.Redirect("pagePlanningOverview.aspx");
    }
}
