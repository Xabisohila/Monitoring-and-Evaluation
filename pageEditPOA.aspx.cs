using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageEditPOA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            hlBackToOverview.NavigateUrl = "pagePlanningOverview.aspx";

            int poaId;
            if (Request.QueryString["id"] != null &&
                int.TryParse(Request.QueryString["id"], out poaId))
            {
                hfPOAID.Value = poaId.ToString();
                LoadPOADetails(poaId);
            }
            else
            {
                lblMessage.Text     = "No valid POA ID provided.";
                lblMessage.CssClass = "msg-error";
                lblMessage.Visible  = true;
            }
        }
    }

    private void PopulateDropdowns()
    {
        POADAL dal = new POADAL();

        DataTable dtPriorities = dal.GetAllPMTDPPrioritiesLookup();
        ddlPMTDPPriority.DataSource     = dtPriorities;
        ddlPMTDPPriority.DataTextField  = "PriorityName";
        ddlPMTDPPriority.DataValueField = "PMTDP_PriorityID";
        ddlPMTDPPriority.DataBind();
        ddlPMTDPPriority.Items.Insert(0, new ListItem("-- Select PMTDP Priority --", "0"));

        DataTable dtClusters = dal.GetAllClustersLookup();
        ddlCluster.DataSource     = dtClusters;
        ddlCluster.DataTextField  = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Cluster --", "0"));
    }

    private void LoadPOADetails(int poaId)
    {
        POADAL dal = new POADAL();
        DataSet ds = dal.GetPOADetails(poaId);

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            lblMessage.Text     = "POA not found for ID " + poaId + ".";
            lblMessage.CssClass = "msg-error";
            lblMessage.Visible  = true;
            return;
        }

        DataRow r = ds.Tables[0].Rows[0];

        txtPOAName.Text        = r["POA_Name"].ToString();
        txtPOADescription.Text = r["POA_Description"]    != DBNull.Value ? r["POA_Description"].ToString()    : string.Empty;
        txtStartYear.Text      = r["POA_StartYear"]      != DBNull.Value ? r["POA_StartYear"].ToString()      : string.Empty;
        txtEndYear.Text        = r["POA_EndYear"]        != DBNull.Value ? r["POA_EndYear"].ToString()        : string.Empty;
        txtDesiredOutcome.Text = r["POADesiredOutcome"]  != DBNull.Value ? r["POADesiredOutcome"].ToString()  : string.Empty;

        ddlPMTDPPriority.SelectedValue = r["PMTDP_PriorityID"].ToString();
        ddlCluster.SelectedValue       = r["ClusterID"].ToString();

        hlBackToOverview.NavigateUrl = "pagePOADetail.aspx?id=" + poaId;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        POADAL dal = new POADAL();

        int    poaId          = Convert.ToInt32(hfPOAID.Value);
        string poaName        = txtPOAName.Text.Trim();
        string description    = string.IsNullOrEmpty(txtPOADescription.Text.Trim())  ? null : txtPOADescription.Text.Trim();
        int    pmtdpId        = Convert.ToInt32(ddlPMTDPPriority.SelectedValue);
        int    clusterId      = Convert.ToInt32(ddlCluster.SelectedValue);
        int    startYear      = Convert.ToInt32(txtStartYear.Text);
        int    endYear        = Convert.ToInt32(txtEndYear.Text);
        string desiredOutcome = string.IsNullOrEmpty(txtDesiredOutcome.Text.Trim())  ? null : txtDesiredOutcome.Text.Trim();

        int rowsAffected = dal.UpdatePOA(poaId, poaName, description,
                               pmtdpId, clusterId, startYear, endYear, desiredOutcome);

        if (rowsAffected > 0)
        {
            lblMessage.Text     = "POA updated successfully.";
            lblMessage.CssClass = "msg-success";
        }
        else
        {
            lblMessage.Text     = "Failed to update the POA. Please try again.";
            lblMessage.CssClass = "msg-error";
        }
        lblMessage.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }
}
