using System;
using System.Data;
using System.Web.UI;

public partial class pageInterventionsDirectDetail : System.Web.UI.Page
{
    private static string Col(DataRow row, string col, string fallback = "N/A")
    {
        return row.Table.Columns.Contains(col) && row[col] != DBNull.Value
               ? row[col].ToString().Trim()
               : fallback;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hlBackToOverview.NavigateUrl = "pagePlanningOverview.aspx";

            int interventionId;
            if (Request.QueryString["id"] != null &&
                int.TryParse(Request.QueryString["id"], out interventionId))
            {
                LoadInterventionDetails(interventionId);
            }
            else
            {
                ShowError("No valid Intervention ID provided in the URL.");
            }
        }
    }

    private void LoadInterventionDetails(int interventionId)
    {
        InterventionDAL dal = new InterventionDAL();
        DataSet ds = dal.GetInterventionDetails(interventionId);

        if (ds == null || ds.Tables.Count == 0)
        {
            ShowError("Failed to retrieve intervention data. Check the database connection.");
            return;
        }

        DataTable tbl = ds.Tables[0];
        if (tbl.Rows.Count == 0)
        {
            ShowError("No intervention found for ID " + interventionId + ".");
            return;
        }

        DataRow r = tbl.Rows[0];

        // ── Header ────────────────────────────────────────────
        lblInterventionName.Text = Col(r, "InterventionName", "(Unnamed)");
        string startYear = Col(r, "InterventionStartYear", "");
        string endYear   = Col(r, "InterventionEndYear",   "");
        lblInterventionPeriod.Text = (startYear != "" && endYear != "")
                                     ? startYear + " &ndash; " + endYear : "N/A";

        string idStr = interventionId.ToString();
        hlEditIntervention.NavigateUrl = "pageEditIntervention.aspx?id=" + idStr;
        hlAddIndicator.NavigateUrl     = "pageAddIndicator.aspx?interventionId=" + idStr;
        hlAddBudget.NavigateUrl        = "pageAddBudget.aspx?interventionId=" + idStr;

        // ── Details ───────────────────────────────────────────
        lblDescription.Text      = Col(r, "InterventionDescription");
        lblLeadInstitution.Text  = Col(r, "LeadInstitution");
        lblWorkingGroup.Text     = Col(r, "WorkingGroup");
        lblMunicipality.Text     = Col(r, "PrimaryMunicipality");
        lblSpatialReference.Text = Col(r, "SpatialReference");

        // ── Strategic alignment ───────────────────────────────
        string poaName = Col(r, "ParentPOA", "");
        string poaId   = Col(r, "ParentPOAID", "0");
        hlPOA.Text        = poaName != "" && poaName != "N/A" ? poaName : "View POA";
        hlPOA.NavigateUrl = poaId != "0" ? "pagePOADetail.aspx?id=" + poaId : "#";

        if (poaId != "0")
            hlBackToOverview.NavigateUrl = "pagePOADetail.aspx?id=" + poaId;

        lblCluster.Text = Col(r, "ParentCluster");
        lblPMTDP.Text   = Col(r, "AlignedPMTDPPriority");

        string pdpName = Col(r, "AlignedPDP", "");
        string pdpId   = Col(r, "AlignedPDPID", "0");
        hlPDP.Text        = pdpName != "" && pdpName != "N/A" ? pdpName : "View PDP";
        hlPDP.NavigateUrl = pdpId != "0" ? "detailsPDP.aspx?id=" + pdpId : "#";

        // ── Indicators (Table 1) ──────────────────────────────
        if (ds.Tables.Count >= 2)
        {
            gvIndicators.DataSource = ds.Tables[1];
            gvIndicators.DataBind();
        }

        // ── Budgets (Table 2) ─────────────────────────────────
        if (ds.Tables.Count >= 3)
        {
            gvBudgets.DataSource = ds.Tables[2];
            gvBudgets.DataBind();
        }

        // ── Quarterly Reports (Table 3) ───────────────────────
        if (ds.Tables.Count >= 4)
        {
            gvQuarterlyReports.DataSource = ds.Tables[3];
            gvQuarterlyReports.DataBind();
        }
    }

    private void ShowError(string message)
    {
        pnlError.Visible   = true;
        lblError.Text      = message;
        pnlContent.Visible = false;
    }
}
