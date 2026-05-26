using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pageEditIndicator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // Only load data on the initial page load
        {
            PopulateDropdowns(); // Populate dropdowns first

            if (Request.QueryString["id"] != null) // Check if Indicator ID is passed in query string
            {
                int indicatorId;
                if (int.TryParse(Request.QueryString["id"], out indicatorId))
                {
                    hfIndicatorID.Value = indicatorId.ToString(); // Store Indicator ID in a hidden field
                    LoadIndicatorDetails(indicatorId); // Load existing Indicator data into form fields
                }
                else
                {
                    lblMessage.Text = "Error: Invalid Indicator ID provided in the URL.";
                    lblMessage.CssClass = "error-message";
                    lblMessage.Visible = true;
                    // Optionally disable form or redirect
                    // pnlForm.Visible = false; // If you wrap your form in a panel
                }
            }
            else
            {
                lblMessage.Text = "Error: No Indicator ID provided. This page requires an ID to edit.";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
                // Optionally disable form or redirect
                // pnlForm.Visible = false;
            }

            // Set the back link to the Planning Overview page, preserving filters if possible
            string backUrl = "pagePlanningOverview.aspx";
            if (Request.UrlReferrer != null) // Check if there's a referring page
            {
                // If the referrer was PlanningOverview or InterventionDetail, try to go back to it
                if (Request.UrlReferrer.AbsolutePath.Contains("pagePlanningOverview.aspx") ||
                Request.UrlReferrer.AbsolutePath.Contains("pageInterventionDetail.aspx"))
                {
                    backUrl = Request.UrlReferrer.ToString();
                }
            }
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }

    private void PopulateDropdowns()
    {
        cls_IndicatorDAL dal = new cls_IndicatorDAL();

        // Populate Intervention Dropdown
        DataTable dtInterventions = dal.GetAllInterventionsLookup();
        ddlIntervention.DataSource = dtInterventions;
        ddlIntervention.DataTextField = "InterventionName";
        ddlIntervention.DataValueField = "InterventionID";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new ListItem("-- Select Intervention --", "0"));

        // Populate Indicator Type Dropdown
        //DataTable dtIndicatorTypes = dal.GetAllIndicatorTypesLookup();
        //ddlIndicatorType.DataSource = dtIndicatorTypes;
        //ddlIndicatorType.DataTextField = "Type"; // Assuming column name 'Type' from programmatic DataTable
        //ddlIndicatorType.DataValueField = "Type"; // Value and Text are the same for simple types
        //ddlIndicatorType.DataBind();
        //ddlIndicatorType.Items.Insert(0, new ListItem("-- Select Type --", "0"));

        DataTable dtIndicatorTypes = dal.GetAllIndicatorTypesLookup(); // This method is in IndicatorDAL
        ddlIndicatorType.DataSource = dtIndicatorTypes;
        ddlIndicatorType.DataTextField = "IndicatorType"; // Assuming column name 'Type' from programmatic DataTable
        ddlIndicatorType.DataValueField = "IndicatorType"; // Value and Text are the same for simple types
        ddlIndicatorType.DataBind();
        ddlIndicatorType.Items.Insert(0, new ListItem("-- Select Type --", "0"));
    }

    private void LoadIndicatorDetails(int indicatorId)
    {
        cls_IndicatorDAL dal = new cls_IndicatorDAL();
        // GetIndicatorDetails returns a DataSet:  Indicator Info, [1] Target Info
        DataSet ds = dal.GetIndicatorDetails(indicatorId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow indicatorRow = ds.Tables[0].Rows[0]; // Get the first row of the first table (Indicator Info)

            txtIndicatorName.Text = indicatorRow["IndicatorName"].ToString();

            //txtUnitOfMeasure.Text = indicatorRow != DBNull.Value ? indicatorRow.ToString() : string.Empty;
            txtUnitOfMeasure.Text = indicatorRow["UnitOfMeasure"] != DBNull.Value ? indicatorRow["UnitOfMeasure"].ToString() : string.Empty;



            txtBaselineValue.Text = indicatorRow["BaselineValue"].ToString();
            txtBaselineYear.Text = indicatorRow["BaselineYear"].ToString();

            // Select correct items in dropdowns
            if (ddlIntervention.Items.FindByValue(indicatorRow["InterventionID"].ToString()) != null)
            {
                ddlIntervention.SelectedValue = indicatorRow["InterventionID"].ToString();
            }
            if (ddlIndicatorType.Items.FindByValue(indicatorRow["IndicatorType"].ToString()) != null)
            {
                ddlIndicatorType.SelectedValue = indicatorRow["IndicatorType"].ToString();
            }





            // Load Target Information (Result Set 2: ds.Tables[1])
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                DataRow targetRow = ds.Tables[1].Rows[0]; // Get the first row of the second table (Target Info)
                txtTargetValue.Text = targetRow["TargetValue"].ToString();
                txtTargetYear.Text = targetRow["TargetYear"].ToString();


                //txtTarget2030TermTarget.Text = targetRow != DBNull.Value ? targetRow.ToString() : string.Empty;
                txtTarget2030TermTarget.Text = targetRow["Target2030_TermTarget"] != DBNull.Value ? targetRow["Target2030_TermTarget"].ToString() : string.Empty;
            }

            // pnlForm.Visible = true; // Make sure the form panel is visible if you have one
        }
        else
        {
            lblMessage.Text = "Indicator not found for ID: " + indicatorId;
            lblMessage.CssClass = "error-message";
            lblMessage.Visible = true;
            // Optionally disable form or redirect
            // pnlForm.Visible = false;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid) // Check if all ASP.NET validation controls pass
        {
            cls_IndicatorDAL dal = new cls_IndicatorDAL();

            int indicatorId = Convert.ToInt32(hfIndicatorID.Value); // Get Indicator ID from hidden field
            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            string indicatorName = txtIndicatorName.Text.Trim();
            string indicatorType = ddlIndicatorType.SelectedValue;
            string unitOfMeasure = txtUnitOfMeasure.Text.Trim();
            decimal baselineValue = Convert.ToDecimal(txtBaselineValue.Text);
            int baselineYear = Convert.ToInt32(txtBaselineYear.Text);

            // Handle optional target fields
            decimal? targetValue = null;
            if (!string.IsNullOrEmpty(txtTargetValue.Text.Trim()))
            {
                targetValue = Convert.ToDecimal(txtTargetValue.Text.Trim());
            }
            int? targetYear = null;
            if (!string.IsNullOrEmpty(txtTargetYear.Text.Trim()))
            {
                targetYear = Convert.ToInt32(txtTargetYear.Text.Trim());
            }
            string target2030TermTarget = txtTarget2030TermTarget.Text.Trim();

            // Ensure optional string fields are null if empty
            string finalUnitOfMeasure = string.IsNullOrEmpty(unitOfMeasure) ? null : unitOfMeasure;
            string finalTarget2030TermTarget = string.IsNullOrEmpty(target2030TermTarget) ? null : target2030TermTarget;

            int rowsAffected = dal.UpdateInterventionIndicator(
            indicatorId,
            interventionId,
            indicatorName,
            indicatorType,
            finalUnitOfMeasure,
            baselineValue,
            baselineYear,
            targetValue,
            targetYear,
            finalTarget2030TermTarget
            );

            if (rowsAffected > 0)
            {
                lblMessage.Text = "Indicator updated successfully!";
                lblMessage.CssClass = "success-message";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Error: Failed to update indicator. Please check data or try again.";
                lblMessage.CssClass = "error-message";
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
        // Redirect back to the Planning Overview page or a default list page
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }

}
