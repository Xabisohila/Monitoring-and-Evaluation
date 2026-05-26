using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pagePOADetail : System.Web.UI.Page
{
    private DataSet currentPOADetailsDataSet;
    protected string CurrentPoaId { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string backUrl = "pagePlanningOverview.aspx";
            if (Request.UrlReferrer != null &&
                Request.UrlReferrer.AbsolutePath.Contains("pagePlanningOverview.aspx"))
                backUrl = Request.UrlReferrer.ToString();
            hlBackToOverview.NavigateUrl = backUrl;

            int poaId;
            if (Request.QueryString["id"] != null &&
                int.TryParse(Request.QueryString["id"], out poaId))
            {
                LoadPOADetails(poaId);
            }
            else
            {
                ShowError("No valid POA ID provided in the URL.");
            }
        }
    }

    // Safe column reader — never throws; returns fallback when column missing or null
    private static string Col(DataRow row, string col, string fallback = "N/A")
    {
        return row.Table.Columns.Contains(col) && row[col] != DBNull.Value
               ? row[col].ToString().Trim()
               : fallback;
    }

    // Safe field reader for Repeater inline binding — returns "" when column missing or null
    protected string SafeField(object dataItem, string fieldName)
    {
        var rv = dataItem as DataRowView;
        if (rv == null) return string.Empty;
        return rv.Row.Table.Columns.Contains(fieldName) && rv.Row[fieldName] != DBNull.Value
               ? rv.Row[fieldName].ToString().Trim()
               : string.Empty;
    }

    private void LoadPOADetails(int poaId)
    {
        POADAL dal = new POADAL();
        currentPOADetailsDataSet = dal.GetPOADetails(poaId);

        if (currentPOADetailsDataSet == null || currentPOADetailsDataSet.Tables.Count == 0)
        {
            ShowError("Failed to retrieve POA data. Check the database connection.");
            return;
        }

        // ── Table 0: POA header ────────────────────────────────
        DataTable poaTable = currentPOADetailsDataSet.Tables[0];
        if (poaTable.Rows.Count == 0)
        {
            ShowError("No Programme of Action found for ID " + poaId + ".");
            return;
        }

        DataRow r = poaTable.Rows[0];

        lblPOAName.Text        = Col(r, "POA_Name", "(Unnamed POA)");
        lblDescription.Text    = Col(r, "POA_Description");
        lblDesiredOutcome.Text = Col(r, "POADesiredOutcome", Col(r, "DesiredOutcome"));

        string startYear = Col(r, "POA_StartYear", "");
        string endYear   = Col(r, "POA_EndYear",   "");
        lblPOAPeriod.Text = (startYear != "" && endYear != "")
                            ? startYear + " &ndash; " + endYear
                            : "N/A";

        // Edit POA link
        string thisPOAId = Col(r, "POA_ID", poaId.ToString());
        CurrentPoaId = thisPOAId;
        hlEditPOA.NavigateUrl = "pageEditPOA.aspx?id=" + thisPOAId;

        // PMTDP Priority — SP returns ID only; look up the name
        string pmtdpId = Col(r, "PMTDP_PriorityID", "0");
        if (pmtdpId != "0" && pmtdpId != "N/A")
        {
            DataTable dtP  = dal.GetAllPMTDPPrioritiesLookup();
            DataRow[]  pr  = dtP.Select("PMTDP_PriorityID = " + pmtdpId);
            lblPMTDP.Text  = pr.Length > 0 ? pr[0]["PriorityName"].ToString() : pmtdpId;
        }
        else
        {
            lblPMTDP.Text = "N/A";
        }

        // Cluster — SP returns ID only; look up the name
        string clusterId = Col(r, "ClusterID", "0");
        if (clusterId != "0" && clusterId != "N/A")
        {
            DataTable dtC   = dal.GetAllClustersLookup();
            DataRow[]  cr   = dtC.Select("ClusterID = " + clusterId);
            lblCluster.Text = cr.Length > 0 ? cr[0]["ClusterName"].ToString() : clusterId;
        }
        else
        {
            lblCluster.Text = "N/A";
        }

        // PDP link
        string pdpName = Col(r, "PDP_Name", Col(r, "PDPName", ""));
        string pdpId   = Col(r, "PDP_ID",   Col(r, "PDPID", "0"));
        hlPDP.Text        = pdpName != "N/A" && pdpName != "" ? pdpName : "View PDP";
        hlPDP.NavigateUrl = pdpId != "0" ? "detailsPDP.aspx?id=" + pdpId : "#";

        // Add Intervention link
        lnkAddIntervention.NavigateUrl = string.Format(
            "pageAddIntervention.aspx?poaId={0}&clusterId={1}", thisPOAId, clusterId);

        // ── Table 1: Interventions ─────────────────────────────
        if (currentPOADetailsDataSet.Tables.Count >= 2)
        {
            DataTable interventions = currentPOADetailsDataSet.Tables[1];
            if (interventions.Rows.Count > 0)
            {
                rptInterventions.DataSource = interventions;
                rptInterventions.DataBind();
                pnlNoInterventions.Visible = false;
            }
            else
            {
                pnlNoInterventions.Visible = true;
            }
        }
        else
        {
            pnlNoInterventions.Visible = true;
        }
    }

    protected void rptInterventions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item &&
            e.Item.ItemType != ListItemType.AlternatingItem) return;

        DataRowView rv = (DataRowView)e.Item.DataItem;
        int interventionId = Convert.ToInt32(rv["InterventionID"]);

        GridView gvIndicators = (GridView)e.Item.FindControl("gvInterventionIndicators");
        GridView gvBudgets    = (GridView)e.Item.FindControl("gvInterventionBudgets");

        if (currentPOADetailsDataSet == null) return;

        // ── Indicators (Table 2) ───────────────────────────────
        if (gvIndicators != null && currentPOADetailsDataSet.Tables.Count >= 3)
        {
            DataView dv = new DataView(currentPOADetailsDataSet.Tables[2]);
            dv.RowFilter = "InterventionID = " + interventionId;
            gvIndicators.DataSource = dv;
            gvIndicators.DataBind();
        }

        // ── Budgets (Table 3) ──────────────────────────────────
        if (gvBudgets != null && currentPOADetailsDataSet.Tables.Count >= 4)
        {
            DataView dv = new DataView(currentPOADetailsDataSet.Tables[3]);
            dv.RowFilter = "InterventionID = " + interventionId;
            dv.Sort      = "FinancialYear ASC";
            gvBudgets.DataSource = dv;
            gvBudgets.DataBind();
        }

    }

    private void ShowError(string message)
    {
        pnlError.Visible   = true;
        lblError.Text      = message;
        pnlContent.Visible = false;
    }
}
