using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class pageAddIndicator : System.Web.UI.Page
{
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

            string qsPoaId = Request.QueryString["poaId"];
            if (!string.IsNullOrEmpty(qsPoaId))
                hfPoaId.Value = qsPoaId;

            string qsId = Request.QueryString["interventionId"];
            if (!string.IsNullOrEmpty(qsId))
            {
                int interventionId;
                if (int.TryParse(qsId, out interventionId))
                {
                    ListItem li = ddlIntervention.Items.FindByValue(interventionId.ToString());
                    if (li != null) ddlIntervention.SelectedValue = interventionId.ToString();
                }
            }
        }
    }

    private void PopulateDropdowns()
    {
        cls_IndicatorDAL dal = new cls_IndicatorDAL();

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int    interventionId  = Convert.ToInt32(ddlIntervention.SelectedValue);
        string indicatorName   = txtIndicatorName.Text.Trim();
        string indicatorType   = ddlIndicatorType.SelectedValue;
        string unitOfMeasure   = txtUnitOfMeasure.Text.Trim();
        decimal baselineValue  = Convert.ToDecimal(txtBaselineValue.Text);
        int    baselineYear    = Convert.ToInt32(txtBaselineYear.Text);

        decimal? targetValue = null;
        if (!string.IsNullOrEmpty(txtTargetValue.Text.Trim()))
            targetValue = Convert.ToDecimal(txtTargetValue.Text.Trim());

        int? targetYear = null;
        if (!string.IsNullOrEmpty(txtTargetYear.Text.Trim()))
            targetYear = Convert.ToInt32(txtTargetYear.Text.Trim());

        string finalUnit   = string.IsNullOrEmpty(unitOfMeasure) ? null : unitOfMeasure;
        string finalTarget = string.IsNullOrEmpty(txtTarget2030TermTarget.Text.Trim())
                             ? null : txtTarget2030TermTarget.Text.Trim();

        cls_IndicatorDAL dal = new cls_IndicatorDAL();
        int newIndicatorId = dal.CreateInterventionIndicator(
            interventionId, indicatorName, indicatorType,
            finalUnit, baselineValue, baselineYear,
            targetValue, targetYear, finalTarget);

        if (newIndicatorId > 0)
        {
            hlAddAnother.NavigateUrl      = "pageAddIndicator.aspx?interventionId=" + interventionId;
            hlViewIntervention.NavigateUrl = "pageInterventionsDirectDetail.aspx?id=" + interventionId;

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
