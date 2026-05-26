using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageAddIntervention : System.Web.UI.Page
{
    private InterventionDAL dal = new InterventionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            // Pre-select cluster and filter dependent dropdowns
            string qsCluster = Request.QueryString["clusterId"];
            int preClusterId = 0;
            if (!string.IsNullOrEmpty(qsCluster))
                int.TryParse(qsCluster, out preClusterId);

            if (preClusterId > 0)
            {
                ListItem liCluster = ddlCluster.Items.FindByValue(preClusterId.ToString());
                if (liCluster != null) ddlCluster.SelectedValue = preClusterId.ToString();
                PopulateDependentDropdowns(preClusterId);
            }

            // Pre-select POA
            string qsPOA = Request.QueryString["poaId"];
            if (!string.IsNullOrEmpty(qsPOA))
            {
                int poaId;
                if (int.TryParse(qsPOA, out poaId))
                {
                    ListItem liPOA = ddlPOA.Items.FindByValue(poaId.ToString());
                    if (liPOA != null) ddlPOA.SelectedValue = poaId.ToString();
                }
            }

            string backUrl = "i_POAList.aspx";
            if (Request.UrlReferrer != null &&
                (Request.UrlReferrer.AbsolutePath.Contains("pagePOADetail.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("i_POAList.aspx")))
                backUrl = Request.UrlReferrer.ToString();
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }

    private void PopulateDropdowns()
    {
        DataTable dtClusters = dal.GetAllClustersLookup();
        ddlCluster.DataSource = dtClusters;
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Cluster --", "0"));

        DataTable dtPOAs = dal.GetAllPOAsLookup();
        ddlPOA.DataSource = dtPOAs;
        ddlPOA.DataTextField = "POA_Name";
        ddlPOA.DataValueField = "POA_ID";
        ddlPOA.DataBind();
        ddlPOA.Items.Insert(0, new ListItem("-- Select POA --", "0"));

        DataTable dtInstitutions = dal.GetAllLeadInstitutionsLookup();
        ddlLeadInstitution.DataSource = dtInstitutions;
        ddlLeadInstitution.DataTextField = "InstitutionName";
        ddlLeadInstitution.DataValueField = "InstitutionID";
        ddlLeadInstitution.DataBind();
        ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Lead Institution --", "0"));

        DataTable dtWorkingGroups = dal.GetAllWorkingGroupsLookup();
        ddlWorkingGroup.DataSource = dtWorkingGroups;
        ddlWorkingGroup.DataTextField = "WG_Name";
        ddlWorkingGroup.DataValueField = "WorkingGroupID";
        ddlWorkingGroup.DataBind();
        ddlWorkingGroup.Items.Insert(0, new ListItem("-- Select Working Group --", "0"));

        DataTable dtMunicipalities = dal.GetAllMunicipalitiesLookup();
        ddlMunicipality.DataSource = dtMunicipalities;
        ddlMunicipality.DataTextField = "MunicipalityName";
        ddlMunicipality.DataValueField = "MunicipalityID";
        ddlMunicipality.DataBind();
        ddlMunicipality.Items.Insert(0, new ListItem("-- Select Municipality --", "0"));

        DataTable dtSubOutcomes = dal.GetAllSubOutcomesLookup(0);
        ddlSubOutcome.DataSource = dtSubOutcomes;
        ddlSubOutcome.DataTextField = "SubOutcome";
        ddlSubOutcome.DataValueField = "SubOutcomeID";
        ddlSubOutcome.DataBind();
        ddlSubOutcome.Items.Insert(0, new ListItem("-- Select SubOutcome --", "0"));
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        if (clusterId > 0)
            PopulateDependentDropdowns(clusterId);
    }

    private void PopulateDependentDropdowns(int clusterId)
    {
        DataTable dtPOAs = dal.ExecuteDataTable(
            "SELECT POA_ID, POA_Name FROM new_ProgrammesOfAction WHERE ClusterID = @ClusterID ORDER BY POA_Name",
            CommandType.Text,
            new SqlParameter("@ClusterID", clusterId));
        ddlPOA.DataSource = dtPOAs;
        ddlPOA.DataTextField = "POA_Name";
        ddlPOA.DataValueField = "POA_ID";
        ddlPOA.DataBind();
        ddlPOA.Items.Insert(0, new ListItem("-- Select POA --", "0"));

        DataTable dtInstitutions = dal.ExecuteDataTable(
            "SELECT InstitutionID, InstitutionName FROM new_ImplementationInstitutions WHERE ClusterID = @ClusterID ORDER BY InstitutionName",
            CommandType.Text,
            new SqlParameter("@ClusterID", clusterId));
        ddlLeadInstitution.DataSource = dtInstitutions;
        ddlLeadInstitution.DataTextField = "InstitutionName";
        ddlLeadInstitution.DataValueField = "InstitutionID";
        ddlLeadInstitution.DataBind();
        ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Lead Institution --", "0"));

        DataTable dtWorkingGroups = dal.ExecuteDataTable(
            "SELECT WorkingGroupID, WG_Name FROM new_WorkingGroups WHERE LeadInstitutionID IN (SELECT InstitutionID FROM new_ImplementationInstitutions WHERE ClusterID = @ClusterID) ORDER BY WG_Name",
            CommandType.Text,
            new SqlParameter("@ClusterID", clusterId));
        ddlWorkingGroup.DataSource = dtWorkingGroups;
        ddlWorkingGroup.DataTextField = "WG_Name";
        ddlWorkingGroup.DataValueField = "WorkingGroupID";
        ddlWorkingGroup.DataBind();
        ddlWorkingGroup.Items.Insert(0, new ListItem("-- Select Working Group --", "0"));

        DataTable dtSubOutcomes = dal.GetAllSubOutcomesLookup(clusterId);
        ddlSubOutcome.DataSource = dtSubOutcomes;
        ddlSubOutcome.DataTextField = "SubOutcome";
        ddlSubOutcome.DataValueField = "SubOutcomeID";
        ddlSubOutcome.DataBind();
        ddlSubOutcome.Items.Insert(0, new ListItem("-- Select SubOutcome --", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        string interventionName        = txtInterventionName.Text.Trim();
        string interventionDescription = txtInterventionDescription.Text.Trim();
        int poaId             = Convert.ToInt32(ddlPOA.SelectedValue);
        int leadInstitutionId = Convert.ToInt32(ddlLeadInstitution.SelectedValue);

        int? workingGroupId = ddlWorkingGroup.SelectedValue != "0"
            ? (int?)Convert.ToInt32(ddlWorkingGroup.SelectedValue) : null;
        int? municipalityId = ddlMunicipality.SelectedValue != "0"
            ? (int?)Convert.ToInt32(ddlMunicipality.SelectedValue) : null;

        int startYear    = Convert.ToInt32(txtStartYear.Text);
        int endYear      = Convert.ToInt32(txtEndYear.Text);
        int subOutcomeId = ddlSubOutcome.SelectedValue != "0"
            ? Convert.ToInt32(ddlSubOutcome.SelectedValue) : 0;

        string finalDescription = string.IsNullOrEmpty(interventionDescription) ? null : interventionDescription;
        string finalSpatialRef  = string.IsNullOrEmpty(txtSpatialReference.Text.Trim()) ? null : txtSpatialReference.Text.Trim();

        int newInterventionId = dal.CreateIntervention(
            interventionName, finalDescription, poaId, leadInstitutionId,
            workingGroupId, startYear, endYear, municipalityId, finalSpatialRef, subOutcomeId);

        if (newInterventionId > 0)
        {
            string clusterId = Request.QueryString["clusterId"] ?? "0";

            hlAddIndicator.NavigateUrl = "pageAddIndicator.aspx?interventionId=" + newInterventionId;
            hlAddAnother.NavigateUrl   = string.Format(
                "pageAddIntervention.aspx?poaId={0}&clusterId={1}", poaId, clusterId);
            hlBackToPOA.NavigateUrl    = "pagePOADetail.aspx?id=" + poaId;

            pnlForm.Visible    = false;
            pnlSuccess.Visible = true;
        }
        else
        {
            lblDbError.Text    = "Failed to save the intervention. Please try again.";
            lblDbError.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }
}
