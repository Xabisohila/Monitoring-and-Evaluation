using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class pageAddBudget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            string backUrl = "pagePlanningOverview.aspx";
            if (Request.UrlReferrer != null &&
                (Request.UrlReferrer.AbsolutePath.Contains("pagePOADetail.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("pageInterventionsDirectDetail.aspx") ||
                 Request.UrlReferrer.AbsolutePath.Contains("pagePlanningOverview.aspx")))
                backUrl = Request.UrlReferrer.ToString();

            hlBackToOverview.NavigateUrl  = backUrl;
            hlBackToOverview2.NavigateUrl = backUrl;

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
        cls_BudgetDAL dal = new cls_BudgetDAL();

        DataTable dtInterventions = dal.GetAllInterventionsLookup();
        ddlIntervention.DataSource     = dtInterventions;
        ddlIntervention.DataTextField  = "InterventionName";
        ddlIntervention.DataValueField = "InterventionID";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new ListItem("-- Select Intervention --", "0"));

        DataTable dtFinancialYears = dal.GetAllFinancialYearsLookup();
        ddlFinancialYear.DataSource     = dtFinancialYears;
        ddlFinancialYear.DataTextField  = "FY_Name";
        ddlFinancialYear.DataValueField = "FY_ID";
        ddlFinancialYear.DataBind();
        ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int     interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
        int     fyId           = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        decimal annualBudget   = Convert.ToDecimal(txtAnnualBudget.Text.Trim());

        decimal? termBudget = null;
        if (!string.IsNullOrEmpty(txtTermBudget.Text.Trim()))
            termBudget = Convert.ToDecimal(txtTermBudget.Text.Trim());

        cls_BudgetDAL dal = new cls_BudgetDAL();
        int newBudgetId = dal.CreateInterventionBudget(interventionId, fyId, annualBudget, termBudget);

        if (newBudgetId > 0)
        {
            hlAddAnother.NavigateUrl       = "pageAddBudget.aspx?interventionId=" + interventionId;
            hlViewIntervention.NavigateUrl = "pageInterventionsDirectDetail.aspx?id=" + interventionId;

            pnlForm.Visible    = false;
            pnlSuccess.Visible = true;
        }
        else
        {
            lblDbError.Text    = "Failed to save the budget entry. Please try again.";
            lblDbError.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }
}
