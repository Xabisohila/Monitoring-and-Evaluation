using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageAddPOA : System.Web.UI.Page
{
    POADAL dal = new POADAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            string qs = Request.QueryString["priorityId"];
            if (!string.IsNullOrEmpty(qs))
            {
                int priorityId;
                if (int.TryParse(qs, out priorityId) && priorityId > 0)
                    PreSelectFromPriority(priorityId);
            }

            string backUrl = "PlanningOverview.aspx";
            if (Request.UrlReferrer != null &&
                Request.UrlReferrer.AbsolutePath.Contains("PlanningOverview.aspx"))
            {
                backUrl = Request.UrlReferrer.ToString();
            }
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }

    private void PopulateDropdowns()
    {
        // Load PMTDP plans
        DataTable dtPDPs = dal.GetAllPDPs();
        ddlPDP.DataSource     = dtPDPs;
        ddlPDP.DataTextField  = "PDP_Name";
        ddlPDP.DataValueField = "PDP_ID";
        ddlPDP.DataBind();
        ddlPDP.Items.Insert(0, new ListItem("-- Select PMTDP --", "0"));

        // Auto-select when only one PDP exists and populate year dropdowns
        if (dtPDPs.Rows.Count == 1)
        {
            ddlPDP.SelectedValue = dtPDPs.Rows[0]["PDP_ID"].ToString();
            int startYear = Convert.ToInt32(dtPDPs.Rows[0]["PDP_StartYear"]);
            int endYear   = Convert.ToInt32(dtPDPs.Rows[0]["PDP_EndYear"]);
            PopulateYearDropdowns(startYear, endYear);
        }

        // Load clusters
        DataTable dtClusters = dal.GetAllClustersLookup();
        ddlCluster.DataSource     = dtClusters;
        ddlCluster.DataTextField  = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Cluster --", "0"));
    }

    private void PreSelectFromPriority(int priorityId)
    {
        DataTable dt = dal.GetPriorityContext(priorityId);
        if (dt.Rows.Count == 0) return;

        int pdpId     = Convert.ToInt32(dt.Rows[0]["PDP_ID"]);
        int clusterId = dt.Rows[0]["ClusterID"] == DBNull.Value
            ? 0 : Convert.ToInt32(dt.Rows[0]["ClusterID"]);

        // Select PDP and generate year dropdowns if not already done
        ListItem liPDP = ddlPDP.Items.FindByValue(pdpId.ToString());
        if (liPDP != null)
        {
            ddlPDP.SelectedValue = pdpId.ToString();
            if (ddlStartYear.Items.Count <= 1)
            {
                DataTable dtPDPs = dal.GetAllPDPs();
                DataRow[] rows = dtPDPs.Select("PDP_ID = " + pdpId);
                if (rows.Length > 0)
                    PopulateYearDropdowns(
                        Convert.ToInt32(rows[0]["PDP_StartYear"]),
                        Convert.ToInt32(rows[0]["PDP_EndYear"]));
            }
        }

        if (clusterId > 0)
        {
            ListItem liCluster = ddlCluster.Items.FindByValue(clusterId.ToString());
            if (liCluster != null) ddlCluster.SelectedValue = clusterId.ToString();
        }

        LoadPriorities(clusterId, pdpId);

        ListItem liPriority = ddlPriority.Items.FindByValue(priorityId.ToString());
        if (liPriority != null) ddlPriority.SelectedValue = priorityId.ToString();
    }

    private void PopulateYearDropdowns(int startYear, int endYear)
    {
        ddlStartYear.Items.Clear();
        ddlEndYear.Items.Clear();
        ddlStartYear.Items.Add(new ListItem("-- Select Start Year --", "0"));
        ddlEndYear.Items.Add(new ListItem("-- Select End Year --", "0"));
        for (int y = startYear; y <= endYear; y++)
        {
            ddlStartYear.Items.Add(new ListItem(y.ToString(), y.ToString()));
            ddlEndYear.Items.Add(new ListItem(y.ToString(), y.ToString()));
        }
    }

    protected void ddlPDP_SelectedIndexChanged(object sender, EventArgs e)
    {
        int pdpId = Convert.ToInt32(ddlPDP.SelectedValue);
        if (pdpId == 0)
        {
            ddlStartYear.Items.Clear();
            ddlEndYear.Items.Clear();
            ddlStartYear.Items.Add(new ListItem("-- Select Start Year --", "0"));
            ddlEndYear.Items.Add(new ListItem("-- Select End Year --", "0"));
            ddlPriority.Items.Clear();
            ddlPriority.Items.Add(new ListItem("-- Select Priority --", "0"));
            return;
        }

        DataTable dtPDPs = dal.GetAllPDPs();
        DataRow[] rows = dtPDPs.Select("PDP_ID = " + pdpId);
        if (rows.Length > 0)
        {
            int startYear = Convert.ToInt32(rows[0]["PDP_StartYear"]);
            int endYear   = Convert.ToInt32(rows[0]["PDP_EndYear"]);
            PopulateYearDropdowns(startYear, endYear);
        }

        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        if (clusterId > 0)
            LoadPriorities(clusterId, pdpId);
        else
        {
            ddlPriority.Items.Clear();
            ddlPriority.Items.Add(new ListItem("-- Select Priority --", "0"));
        }
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        int pdpId     = Convert.ToInt32(ddlPDP.SelectedValue);
        LoadPriorities(clusterId, pdpId);
    }

    private void LoadPriorities(int clusterId, int pdpId)
    {
        DataTable dt = (pdpId > 0)
            ? dal.GetPrioritiesByClusterAndPDP(clusterId, pdpId)
            : dal.GetPrioritiesByCluster(clusterId);

        ddlPriority.DataSource     = dt;
        ddlPriority.DataTextField  = "PriorityName";
        ddlPriority.DataValueField = "PMTDP_PriorityID";
        ddlPriority.DataBind();
        ddlPriority.Items.Insert(0, new ListItem("-- Select Priority --", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) { lblMessage.Visible = false; return; }

        int startYear = Convert.ToInt32(ddlStartYear.SelectedValue);
        int endYear   = Convert.ToInt32(ddlEndYear.SelectedValue);

        if (endYear < startYear)
        {
            lblMessage.Text     = "End Year must be greater than or equal to Start Year.";
            lblMessage.CssClass = "msg-error";
            lblMessage.Visible  = true;
            return;
        }

        string poaName        = txtPOAName.Text.Trim();
        string poaDescription = txtPOADescription.Text.Trim();
        int pmtdpPriorityId   = Convert.ToInt32(ddlPriority.SelectedValue);
        int clusterId         = Convert.ToInt32(ddlCluster.SelectedValue);
        string desiredOutcome = txtDesiredOutcome.Text.Trim();

        string finalDescription   = string.IsNullOrEmpty(poaDescription) ? null : poaDescription;
        string finalDesiredOutcome = string.IsNullOrEmpty(desiredOutcome) ? null : desiredOutcome;

        int newPOAId = dal.CreatePOA(
            poaName,
            finalDescription,
            pmtdpPriorityId,
            clusterId,
            startYear,
            endYear,
            finalDesiredOutcome);

        if (newPOAId > 0)
        {
            lblMessage.Text     = "Programme of Action (POA) added successfully! New ID: " + newPOAId;
            lblMessage.CssClass = "msg-success";
            lblMessage.Visible  = true;
            ClearForm();
        }
        else
        {
            lblMessage.Text     = "Error: Failed to add POA. Please try again.";
            lblMessage.CssClass = "msg-error";
            lblMessage.Visible  = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("pagePlanningOverview.aspx");
    }

    private void ClearForm()
    {
        txtPOAName.Text        = string.Empty;
        txtPOADescription.Text = string.Empty;
        txtDesiredOutcome.Text = string.Empty;

        ddlPriority.SelectedIndex  = 0;
        ddlCluster.SelectedIndex   = 0;
        ddlStartYear.SelectedIndex = 0;
        ddlEndYear.SelectedIndex   = 0;
    }
}
