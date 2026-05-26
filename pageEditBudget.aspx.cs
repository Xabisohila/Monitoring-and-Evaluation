using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TextmagicRest.Model;


public partial class pageEditBudget : System.Web.UI.Page
{
    protected void Page_Loadjh(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();

            int budgetId;
            if (int.TryParse(Request.QueryString["id"], out budgetId))
            {
                hfBudgetID.Value = budgetId.ToString();
                LoadBudgetDetails(budgetId);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns(); // Load dropdowns first

            string idParam = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(idParam))
            {
                int budgetId;
                if (int.TryParse(idParam, out budgetId))
                {
                    hfBudgetID.Value = budgetId.ToString(); // Store ID in hidden field
                    LoadBudgetDetails(budgetId); // Load data into form
                }
                else
                {
                    lblMessage.Text = "Error: Invalid Budget ID provided in the URL.";
                    lblMessage.CssClass = "error-message";
                    lblMessage.Visible = true;
                    // Optionally disable form or redirect
                    // pnlForm.Visible = false;
                }
            }
            else
            {
                lblMessage.Text = "Error: No Budget ID provided. This page requires an ID to edit.";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
                // Optionally disable form or redirect
                // pnlForm.Visible = false;
            }

            // Set the back link to the Planning Overview page, preserving filters if possible
            string backUrl = "PlanningOverview.aspx";
            if (Request.UrlReferrer != null)
            {
                string refPath = Request.UrlReferrer.AbsolutePath;
                if (refPath.Contains("PlanningOverview.aspx") || refPath.Contains("InterventionDetail.aspx"))
                {
                    backUrl = Request.UrlReferrer.ToString();
                }
            }
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }






    private void PopulateDropdowns()
    {
        cls_BudgetDAL dal = new cls_BudgetDAL();

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
    }

    private void LoadBudgetDetails(int budgetId)
    {
        cls_BudgetDAL dal = new cls_BudgetDAL();
        DataSet ds = dal.GetBudgetById(budgetId); // You need to implement this method

        // Load Target Information (Result Set 1: ds.Tables[0])
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow budgetRow = ds.Tables[0].Rows[0];

            //txtAnnualBudget.Text = budgetRow["AnnualBudget"].ToString();
            //txtTermBudget.Text = budgetRow["TermBudget"].ToString();



            txtAnnualBudget.Text = budgetRow["AnnualBudget"] != DBNull.Value
                ? budgetRow["AnnualBudget"].ToString()
                : string.Empty;

            txtTermBudget.Text = budgetRow["TermBudget"] != DBNull.Value
                ? budgetRow["TermBudget"].ToString()
                : string.Empty;



            // Select correct items in dropdowns
            if (ddlIntervention.Items.FindByValue(budgetRow["InterventionID"].ToString()) != null)
            {
                ddlIntervention.SelectedValue = budgetRow["InterventionID"].ToString();
            }
            if (ddlFinancialYear.Items.FindByValue(budgetRow["FY_ID"].ToString()) != null)
            {
                ddlFinancialYear.SelectedValue = budgetRow["FY_ID"].ToString();
            }
        }
        else 
        {
            lblMessage.Text = "Budget not found for ID: " + budgetId;
            lblMessage.CssClass = "error-message";
            lblMessage.Visible = true;
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            cls_BudgetDAL dal = new cls_BudgetDAL();

            int budgetId = Convert.ToInt32(hfBudgetID.Value); // Get BudgetID from hidden field
            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            int fyId = Convert.ToInt32(ddlFinancialYear.SelectedValue);

            // Parse Annual Budget
            decimal annualBudget = 0;
            if (!string.IsNullOrWhiteSpace(txtAnnualBudget.Text))
            {
                annualBudget = Convert.ToDecimal(txtAnnualBudget.Text.Trim());
            }

            // Parse optional Term Budget
            decimal? termBudget = null;
            if (!string.IsNullOrWhiteSpace(txtTermBudget.Text))
            {
                termBudget = Convert.ToDecimal(txtTermBudget.Text.Trim());
            }

            // Call DAL method to update
            int rowsAffected = dal.UpdateInterventionBudget(
                budgetId,
                interventionId,
                fyId,
                annualBudget,
                termBudget
            );

            if (rowsAffected > 0)
            {
                lblMessage.Text = "Budget updated successfully!";
                lblMessage.CssClass = "success-message";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Error: Failed to update budget. Please check the data or try again.";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
            }
        }
        else
        {
            lblMessage.Visible = false; // Hide any previous message
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Redirect back to the Planning Overview page or a default list page
        Response.Redirect(hlBackToOverview.NavigateUrl);
    }
}













