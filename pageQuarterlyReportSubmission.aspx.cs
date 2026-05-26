using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YourProjectName.Data;
using System.Data;


public partial class pageQuarterlyReportSubmission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();
            // Pre-select Intervention and FY if IDs are passed in query string
            if (Request.QueryString["interventionId"] != null)
            {
                int interventionId;
                if (int.TryParse(Request.QueryString["interventionId"], out interventionId))
                {
                    ddlIntervention.SelectedValue = interventionId.ToString();
                    // Optionally disable the dropdown if pre-selected
                    // ddlIntervention.Enabled = false;
                }
            }
            if (Request.QueryString["fyId"] != null)
            {
                int fyId;
                if (int.TryParse(Request.QueryString["fyId"], out fyId))
                {
                    ddlFinancialYear.SelectedValue = fyId.ToString();
                    // Optionally disable the dropdown if pre-selected
                    // ddlFinancialYear.Enabled = false;
                }
            }

            // Set default reporting date to today
            txtReportingDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

            // Set the back link to the Monitoring Overview page, preserving filters if possible
            string backUrl = "MonitoringOverview.aspx";
            if (Request.UrlReferrer != null) // Check if there's a referring page
            {
                // If the referrer was MonitoringOverview or InterventionDetail, try to go back to it
                if (Request.UrlReferrer.AbsolutePath.Contains("MonitoringOverview.aspx") ||
                Request.UrlReferrer.AbsolutePath.Contains("InterventionDetail.aspx"))
                {
                    backUrl = Request.UrlReferrer.ToString();
                }
            }
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }

    private void PopulateDropdowns()
    {
        cls_QuarterlyReportDAL dal = new cls_QuarterlyReportDAL(); // Use the new QuarterlyReportDAL

        // Populate Intervention Dropdown
        DataTable dtInterventions = dal.GetAllInterventionsLookup();
        ddlIntervention.DataSource = dtInterventions;
        ddlIntervention.DataTextField = "InterventionName";
        ddlIntervention.DataValueField = "InterventionID";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new ListItem("-- Select Intervention --", "0"));

        // Populate Financial Year Dropdown
        DataTable dtFinancialYears = dal.GetAllFinancialYearsLookup();
        ddlFinancialYear.DataSource = dtFinancialYears;
        ddlFinancialYear.DataTextField = "FY_Name";
        ddlFinancialYear.DataValueField = "FY_ID";
        ddlFinancialYear.DataBind();
        ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));

        // Populate Quarter Dropdown
        DataTable dtQuarters = dal.GetAllQuartersLookup();
        ddlQuarter.DataSource = dtQuarters;
        ddlQuarter.DataTextField = "QuarterText";
        ddlQuarter.DataValueField = "QuarterValue";
        ddlQuarter.DataBind();
        ddlQuarter.Items.Insert(0, new ListItem("-- Select Quarter --", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid) // Check if all ASP.NET validation controls pass
        {
            cls_QuarterlyReportDAL dal = new cls_QuarterlyReportDAL();

            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            int fyId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            string quarter = ddlQuarter.SelectedValue;
            DateTime reportingDate = Convert.ToDateTime(txtReportingDate.Text);
            decimal actualExpenditure = Convert.ToDecimal(txtActualExpenditure.Text);
            decimal plannedExpenditure = Convert.ToDecimal(txtPlannedExpenditure.Text);

            // Handle optional Performance Indicator values
            decimal? performanceActualValue = null;
            if (!string.IsNullOrEmpty(txtPerformanceActualValue.Text.Trim()))
            {
                performanceActualValue = Convert.ToDecimal(txtPerformanceActualValue.Text.Trim());
            }
            decimal? performancePlannedValue = null;
            if (!string.IsNullOrEmpty(txtPerformancePlannedValue.Text.Trim()))
            {
                performancePlannedValue = Convert.ToDecimal(txtPerformancePlannedValue.Text.Trim());
            }
            string deviationExplanation = txtDeviationExplanation.Text.Trim();

            // Ensure optional string fields are null if empty
            string finalDeviationExplanation = string.IsNullOrEmpty(deviationExplanation) ? null : deviationExplanation;

            int newReportID = dal.CreateQuarterlyReport(
            interventionId,
            fyId,
            quarter,
            actualExpenditure,
            plannedExpenditure,
            performanceActualValue,
            performancePlannedValue,
            finalDeviationExplanation,
            reportingDate
            );

            if (newReportID > 0)
            {
                lblMessage.Text = "Quarterly Report submitted successfully! New ID: " + newReportID;
                lblMessage.CssClass = "success-message";
                lblMessage.Visible = true;
                ClearForm(); // Clear form for new entry
                             // Optionally redirect to the new report's detail page or parent intervention:
                             // Response.Redirect("QuarterlyReportDetail.aspx?id=" + newReportID);
            }
            else
            {
                lblMessage.Text = "Error: Failed to submit quarterly report. Please try again.";
                lblMessage.CssClass = "error-message"; // Use error styling
                lblMessage.Visible = true;
            }
        }
        else
        {
            // ValidationSummary control will display errors automatically
            lblMessage.Visible = false; // Hide any previous success/error message
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Redirect back to the Monitoring Overview page or a default list page
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }

    private void ClearForm()
    {
        ddlIntervention.SelectedValue = "0";
        ddlFinancialYear.SelectedValue = "0";
        ddlQuarter.SelectedValue = "0";
        txtReportingDate.Text = DateTime.Today.ToString("yyyy-MM-dd"); // Reset to current date
        txtActualExpenditure.Text = string.Empty;
        txtPlannedExpenditure.Text = string.Empty;
        txtPerformanceActualValue.Text = string.Empty;
        txtPerformancePlannedValue.Text = string.Empty;
        txtDeviationExplanation.Text = string.Empty;
    }
}
