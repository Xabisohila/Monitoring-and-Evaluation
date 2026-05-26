using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class detailsPDP : System.Web.UI.Page
{
    private DataSet currentPDPDetailsDataSet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hlBackLink.NavigateUrl = Request.UrlReferrer != null
                ? Request.UrlReferrer.ToString()
                : "pagePlanningOverview.aspx";

            int pdpId;
            if (Request.QueryString["id"] != null &&
                int.TryParse(Request.QueryString["id"], out pdpId))
            {
                LoadPDPDetails(pdpId);
            }
            else
            {
                ShowError("No valid PDP ID provided in the URL.");
            }
        }
    }

    private void LoadPDPDetails(int pdpId)
    {
        PDPDAL dal = new PDPDAL();
        currentPDPDetailsDataSet = dal.GetPDPDetails(pdpId);

        if (currentPDPDetailsDataSet == null || currentPDPDetailsDataSet.Tables.Count == 0)
        {
            ShowError("Failed to retrieve PDP data. Check the database connection.");
            return;
        }

        // ── Table 0: PDP header ────────────────────────────────
        DataTable pdpTable = currentPDPDetailsDataSet.Tables[0];
        if (pdpTable.Rows.Count == 0)
        {
            ShowError("No Provincial Development Plan found for ID " + pdpId + ".");
            return;
        }

        DataRow r = pdpTable.Rows[0];
        lblPDPName.Text        = r["PDP_Name"]       != DBNull.Value ? r["PDP_Name"].ToString()        : "(Unnamed PDP)";
        lblPDPVision.Text      = r["PDP_Vision"]     != DBNull.Value ? r["PDP_Vision"].ToString()      : "N/A";
        lblPDPDescription.Text = r["DesiredOutcome"] != DBNull.Value ? r["DesiredOutcome"].ToString()  : "N/A";

        string startYear = r["PDP_StartYear"] != DBNull.Value ? r["PDP_StartYear"].ToString() : "";
        string endYear   = r["PDP_EndYear"]   != DBNull.Value ? r["PDP_EndYear"].ToString()   : "";
        lblPDPPeriod.Text = (startYear != "" && endYear != "") ? startYear + " &ndash; " + endYear : "N/A";

        // ── Table 1: PMTDP Priorities ──────────────────────────
        if (currentPDPDetailsDataSet.Tables.Count >= 2)
        {
            rptPMTDPPriorities.DataSource = currentPDPDetailsDataSet.Tables[1];
            rptPMTDPPriorities.DataBind();
        }
    }

    protected void rptPMTDPPriorities_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item &&
            e.Item.ItemType != ListItemType.AlternatingItem) return;

        DataRowView rv = (DataRowView)e.Item.DataItem;
        int pmtdpId = Convert.ToInt32(rv["PMTDP_PriorityID"]);

        Repeater rptPOAs = (Repeater)e.Item.FindControl("rptPOAs");
        if (rptPOAs == null || currentPDPDetailsDataSet == null ||
            currentPDPDetailsDataSet.Tables.Count < 3) return;

        DataView dvPOAs = new DataView(currentPDPDetailsDataSet.Tables[2]);
        dvPOAs.RowFilter = "PMTDP_PriorityID = " + pmtdpId;
        rptPOAs.DataSource = dvPOAs;
        rptPOAs.DataBind();
    }

    protected void rptPOAs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item &&
            e.Item.ItemType != ListItemType.AlternatingItem) return;

        DataRowView rv = (DataRowView)e.Item.DataItem;
        int poaId = Convert.ToInt32(rv["POA_ID"]);

        GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");
        if (gvInterventions == null || currentPDPDetailsDataSet == null ||
            currentPDPDetailsDataSet.Tables.Count < 4) return;

        DataView dvInterventions = new DataView(currentPDPDetailsDataSet.Tables[3]);
        dvInterventions.RowFilter = "POA_ID = " + poaId;
        gvInterventions.DataSource = dvInterventions;
        gvInterventions.DataBind();
    }

    private void ShowError(string message)
    {
        pnlError.Visible   = true;
        lblError.Text      = message;
        pnlContent.Visible = false;
    }
}
