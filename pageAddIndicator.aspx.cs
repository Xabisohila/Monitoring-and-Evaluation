using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class pageAddIndicator : System.Web.UI.Page
{
    private cls_IndicatorDAL dal = new cls_IndicatorDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            string backUrl = "pagePlanningOverview.aspx";
            if (Request.UrlReferrer != null &&
                (Request.UrlReferrer.AbsolutePath.Contains("pagePlanningOverview.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("pageInterventionsDirectDetail.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("pageAddIntervention.aspx")))
                backUrl = Request.UrlReferrer.ToString();
            hlBackToOverview.NavigateUrl  = backUrl;
            hlBackToOverview2.NavigateUrl = backUrl;
            ViewState["BackUrl"] = backUrl;

            string qsPoaId = Request.QueryString["poaId"];
            if (!string.IsNullOrEmpty(qsPoaId))
                hfPoaId.Value = qsPoaId;

            int interventionId = 0;
            string qsId = Request.QueryString["interventionId"];
            if (!string.IsNullOrEmpty(qsId) && int.TryParse(qsId, out interventionId))
            {
                ListItem li = ddlIntervention.Items.FindByValue(interventionId.ToString());
                if (li != null) ddlIntervention.SelectedValue = interventionId.ToString();
            }

            // Load PMTDP indicator if the intervention is PMTDP-sourced
            if (interventionId > 0)
                LoadPMTDPIndicator(interventionId);
            else
                RegisterNullPmtdp();
        }
    }

    private void LoadPMTDPIndicator(int interventionId)
    {
        DataRow r = dal.GetPMTDPIndicatorForIntervention(interventionId);
        if (r == null) { RegisterNullPmtdp(); return; }

        pnlPmtdpSource.Visible = true;
        hfIsPmtdp.Value        = "1";

        // Sanitise baseline — strip non-numeric so it passes the Double validator
        string rawBaseline = r["Baseline2023_24"].ToString();
        string cleanBaseline = Regex.Replace(rawBaseline, @"[^\d\.\-]", "");

        // Pre-select indicator type in the dropdown
        string indType = r["IndicatorType"].ToString();
        ListItem liType = ddlIndicatorType.Items.FindByValue(indType);
        if (liType != null) ddlIndicatorType.SelectedValue = indType;

        // Emit JSON for client-side pre-fill and read-only locking
        string json = string.Format(
            "var pmtdpIndicator={{name:{0},type:{1},baseline:{2},baselineYear:2023,termTarget:{3},isCumulative:{4},isPercentage:{5}}};",
            J(r["IndicatorName"].ToString()),
            J(indType),
            J(cleanBaseline),
            J(r["TermTarget2030"].ToString()),
            (r["IsCumulative"] != DBNull.Value && Convert.ToBoolean(r["IsCumulative"])) ? "true" : "false",
            (r["IsPercentage"]  != DBNull.Value && Convert.ToBoolean(r["IsPercentage"]))  ? "true" : "false");

        ClientScript.RegisterStartupScript(GetType(), "pmtdpInd", json, true);
    }

    private void RegisterNullPmtdp()
    {
        ClientScript.RegisterStartupScript(GetType(), "pmtdpInd", "var pmtdpIndicator=null;", true);
    }

    private static string J(string s)
    {
        return "\"" + (s ?? "")
            .Replace("\\", "\\\\").Replace("\"", "\\\"")
            .Replace("\r", "").Replace("\n", "\\n") + "\"";
    }

    // ── Dropdowns ────────────────────────────────────────────────────────────

    private void PopulateDropdowns()
    {
        DataTable dtInterventions = dal.GetAllInterventionsLookup();
        ddlIntervention.DataSource     = dtInterventions;
        ddlIntervention.DataTextField  = "InterventionName";
        ddlIntervention.DataValueField = "InterventionID";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new ListItem("-- Select Intervention --", "0"));

        DataTable dtTypes = dal.GetAllIndicatorTypesLookup();
        ddlIndicatorType.DataSource     = dtTypes;
        ddlIndicatorType.DataTextField  = "IndicatorType";
        ddlIndicatorType.DataValueField = "IndicatorType";
        ddlIndicatorType.DataBind();
        ddlIndicatorType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }

    // ── Save ─────────────────────────────────────────────────────────────────

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int    interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
        string indicatorName  = txtIndicatorName.Text.Trim();
        string indicatorType  = ddlIndicatorType.SelectedValue;
        string unitOfMeasure  = txtUnitOfMeasure.Text.Trim();
        decimal baselineValue = Convert.ToDecimal(txtBaselineValue.Text);
        int    baselineYear   = Convert.ToInt32(txtBaselineYear.Text);

        decimal? targetValue = null;
        if (!string.IsNullOrEmpty(txtTargetValue.Text.Trim()))
            targetValue = Convert.ToDecimal(txtTargetValue.Text.Trim());

        int? targetYear = null;
        if (!string.IsNullOrEmpty(txtTargetYear.Text.Trim()))
            targetYear = Convert.ToInt32(txtTargetYear.Text.Trim());

        string finalUnit      = string.IsNullOrEmpty(unitOfMeasure) ? null : unitOfMeasure;
        string finalTermTgt   = string.IsNullOrEmpty(txtTarget2030TermTarget.Text.Trim())
                                ? null : txtTarget2030TermTarget.Text.Trim();

        bool isCumulative = cbIsCumulative.Checked;
        bool isPercentage  = cbIsPercentage.Checked;

        int newIndicatorId = dal.CreateInterventionIndicator(
            interventionId, indicatorName, indicatorType,
            finalUnit, baselineValue, baselineYear,
            targetValue, targetYear, finalTermTgt,
            isCumulative, isPercentage);

        if (newIndicatorId > 0)
        {
            hlAddAnother.NavigateUrl       = "pageAddIndicator.aspx?interventionId=" + interventionId;
            hlViewIntervention.NavigateUrl = "pageInterventionsDirectDetail.aspx?id=" + interventionId;
            hlBackToOverview2.NavigateUrl  = (ViewState["BackUrl"] as string) ?? "pagePlanningOverview.aspx";

            pnlForm.Visible    = false;
            pnlSuccess.Visible = true;
        }
        else
        {
            lblDbError.Text    = "Failed to save the indicator. Please try again.";
            lblDbError.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string poaId = hfPoaId.Value;
        if (!string.IsNullOrEmpty(poaId) && poaId != "0")
            Response.Redirect("pagePOADetail.aspx?id=" + poaId);
        else
            Response.Redirect("pagePlanningOverview.aspx");
    }
}
