using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Net.Mime.MediaTypeNames;

public partial class designPlanningOverview : System.Web.UI.Page
{
    // Store the full DataSet in a private field for easier access during nested binding events
    private DataSet currentPlanningOverviewDataSet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack) // This code runs only when the page is first loaded
            {
                onStart();
                PopulateDropdowns();
                BindAccordion();
                //pnlOverview.Visible = false; // Initially hide the detail panel
                //lblMessage.Visible = true;   // Show the initial message
            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    //private void BindAccordion()
    //{
    //    PlanningOverviewRepository repo = new PlanningOverviewRepository();
    //    DataTable dtAccordionItems = repo.GetAllWorkGroupsLookup();//repo.GetAccordionItems(); // Your method to get data

    //    foreach (DataRow row in dtAccordionItems.Rows)
    //    {
    //        AccordionPane pane = new AccordionPane();

    //        // Header
    //        pane.HeaderContainer.Controls.Add(new Literal
    //        {
    //            Text = "#" + row["WG_Name"].ToString() + "</a>" // Title
    //        });

    //        // Content
    //        pane.ContentContainer.Controls.Add(new Literal
    //        {
    //            Text = row["WG_Name"].ToString() // Content
    //        });

    //        MyAccordion.Panes.Add(pane);
    //    }
    //}

    //private void BindAccordion()
    //{
    //    PlanningOverviewRepository repo = new PlanningOverviewRepository();
    //    DataTable dtAccordionItems = repo.GetAllWorkGroupsLookup(); // Your method to get data

    //    int index = 0; // To ensure unique IDs

    //    foreach (DataRow row in dtAccordionItems.Rows)
    //    {
    //        AccordionPane pane = new AccordionPane();
    //        pane.ID = "AccordionPane_" + index;

    //        // Header
    //        Literal headerLiteral = new Literal();
    //        headerLiteral.ID = "Header_" + index;
    //        headerLiteral.Text =  "Hello";

    //        // Content
    //        Literal contentLiteral = new Literal();
    //        contentLiteral.ID = "Content_" + index;
    //        contentLiteral.Text = row["WG_Name"].ToString();
    //        pane.ContentContainer.Controls.Add(contentLiteral);

    //        MyAccordion.Panes.Add(pane);
    //        index++;
    //    }
    //}


    private void BindAccordion()
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        DataTable dtAccordionItems = repo.GetAllWorkGroupsLookup();

        int index = 0;

        foreach (DataRow row in dtAccordionItems.Rows)
        {
            AccordionPane pane = new AccordionPane();
            pane.ID = "AccordionPane_" + index;

            // Header
            string title = row["WG_Name"].ToString() + " - Header";
            pane.HeaderContainer.Controls.Add(new LiteralControl(
                "#" + title + "</a>"
            ));

            // Content
            string content = row["WG_Name"].ToString() + " - Content";
            pane.ContentContainer.Controls.Add(new LiteralControl(content));

            MyAccordion.Panes.Add(pane);
            index++;
        }
    }



    private void PopulateDropdowns()
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();

        // Populate Work Groups
        DataTable dtWorkGroups = repo.GetAllWorkGroupsLookup();
        ddlWorkGroups.DataSource = dtWorkGroups;
        ddlWorkGroups.DataTextField = "WG_Name";
        ddlWorkGroups.DataValueField = "WorkingGroupID";
        ddlWorkGroups.DataBind();
        ddlWorkGroups.Items.Insert(0, new ListItem("-- Select Work Group --", "0"));

        // Populate PMTDP Priorities
        DataTable dtPMTDPPriorities = repo.GetAllPMTDPPrioritiesLookup();
        ddlPMTDPPriorities.DataSource = dtPMTDPPriorities;
        ddlPMTDPPriorities.DataTextField = "PriorityName";
        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
        ddlPMTDPPriorities.DataBind();
        ddlPMTDPPriorities.Items.Insert(0, new ListItem("-- Select Priority --", "0"));

        // Populate Financial Years
        DataTable dtFinancialYears = repo.GetAllFinancialYearsLookup();
        ddlFinancialYears.DataSource = dtFinancialYears;
        ddlFinancialYears.DataTextField = "FY_Name";
        ddlFinancialYears.DataValueField = "FY_ID";
        ddlFinancialYears.DataBind();
        ddlFinancialYears.Items.Insert(0, new ListItem("-- Select Year --", "0"));
    }

    protected void onStart()
    {
        int selectedClusterId = Convert.ToInt32((string)Session["Cluster_ID"]);
        string selectedType = Session["selectType"].ToString();//"Planning";
        int selectedWorkGroupId = Convert.ToInt32((string)Session["ddlWorkingGroupID"]);
        int selectedPMTDPPriorityId = Convert.ToInt32((string)Session["PMTDP_PriorityID"]);
        int selectedFinancialYearId = Convert.ToInt32((string)Session["FY_ID"]);

        // Validate selections
        if (selectedWorkGroupId == 0 || selectedPMTDPPriorityId == 0 || selectedFinancialYearId == 0)
        {
            lblMessage.Text = "Please make a selection for Work Group, PMTDP Priority, and Financial Year.";
            lblMessage.Visible = true;
            pnlOverview.Visible = false;
            return;
        }

        LoadPlanningOverview(selectedWorkGroupId, selectedPMTDPPriorityId, selectedFinancialYearId, selectedClusterId);

    }

    protected void btnViewOverview_Click(object sender, EventArgs e)
    {
        int selectedWorkGroupId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int selectedPMTDPPriorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int selectedFinancialYearId = Convert.ToInt32(ddlFinancialYears.SelectedValue);

        int selectedClusterId = Convert.ToInt32((string)Session["Cluster_ID"]);

        // Validate selections
        if (selectedWorkGroupId == 0 || selectedPMTDPPriorityId == 0 || selectedFinancialYearId == 0 )
        {
            lblMessage.Text = "Please make a selection for Work Group, PMTDP Priority, and Financial Year.";
            lblMessage.Visible = true;
            pnlOverview.Visible = false;
            return;
        }

        LoadPlanningOverview(selectedWorkGroupId, selectedPMTDPPriorityId, selectedFinancialYearId, selectedClusterId);
    }

    private void LoadPlanningOverview(int workGroupId, int pmtdpPriorityId, int financialYearId , int clusterId)
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        currentPlanningOverviewDataSet = repo.GetPlanningOverview(workGroupId, pmtdpPriorityId, financialYearId, clusterId);

        // Check if the DataSet and its tables are as expected (at least 5 data tables + 3 lookup tables)
        if (currentPlanningOverviewDataSet != null && currentPlanningOverviewDataSet.Tables.Count >= 5)
        {

            if (currentPlanningOverviewDataSet.Tables.Count > 0 && currentPlanningOverviewDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow headerRow = currentPlanningOverviewDataSet.Tables[0].Rows[0];

                lblSelectedPriorityName.Text = headerRow["PriorityFocus"].ToString();
                lblFrameworkName.Text = headerRow["ImplementationFrameworkName"].ToString();
                lblPDPGoal.Text = headerRow["PDPGoal"].ToString(); // Assuming this column exists
                lblPriorityFocus.Text = headerRow["PriorityFocus"].ToString();
                lblOverallImpact.Text = headerRow["PriorityImpact"].ToString();
                lblPriorityDescription.Text = headerRow["PriorityDescription"].ToString(); // Assuming this column exists
            }

            else
            {
                lblMessage.Text = "No header details found for the selected filters. Please check your data.";
                pnlOverview.Visible = false;
                lblMessage.Visible = true;
                return;
            }

            // Result Set 2: Programmes of Action (POAs)
            DataTable poaTable = currentPlanningOverviewDataSet.Tables[1];
            rptPOAs.DataSource = poaTable;
            rptPOAs.DataBind();

            pnlOverview.Visible = true; // Show the detail panel
            lblMessage.Visible = false;  // Hide the message
        }
        else
        {
            lblMessage.Text = "Error: Failed to retrieve complete planning overview. Please check database configuration and data.";
            pnlOverview.Visible = false;
            lblMessage.Visible = true;
        }
    }

    // Event handler for each item in the POA Repeater (rptPOAs)
    protected void rptPOAs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // Get the current POA's ID from the DataItem
            DataRowView poaRow = (DataRowView)e.Item.DataItem;

            // Safely extract the POA ID from the DataRowView
            int currentPOAId = Convert.ToInt32(poaRow["POA_ID"]);


            // Find the nested GridView for Interventions
            GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");

            if (gvInterventions != null && currentPlanningOverviewDataSet != null && currentPlanningOverviewDataSet.Tables.Count >= 3)
            {
                // Result Set 3: Interventions table from the DataSet
                DataTable interventionsTable = currentPlanningOverviewDataSet.Tables[2];

                // Filter interventions for the current POA
                DataView dvInterventions = new DataView(interventionsTable);
                dvInterventions.RowFilter = "POA_ID = " + currentPOAId;


                gvInterventions.DataSource = dvInterventions;
                gvInterventions.DataBind();
            }
        }

        // Merge Row Content - If similar.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");
            if (gvInterventions != null && gvInterventions.Rows.Count > 1)
            {
                MergeGridViewRows(gvInterventions, new string[] { "Intervention", "Institution", "Municipality", "Spatial Ref." });
            }
        }
    }


    private void MergeGridViewRows(GridView gridView, string[] columnHeaders)
    {
        for (int colIndex = 0; colIndex < gridView.Columns.Count; colIndex++)
        {
            string headerText = gridView.Columns[colIndex].HeaderText;
            if (!columnHeaders.Contains(headerText)) continue;

            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow currentRow = gridView.Rows[rowIndex];
                GridViewRow nextRow = gridView.Rows[rowIndex + 1];

                string currentText = GetCellText(currentRow.Cells[colIndex]);
                string nextText = GetCellText(nextRow.Cells[colIndex]);

                if (currentText == nextText)
                {
                    if (nextRow.Cells[colIndex].RowSpan < 2)
                    {
                        currentRow.Cells[colIndex].RowSpan = 2;
                    }
                    else
                    {
                        currentRow.Cells[colIndex].RowSpan = nextRow.Cells[colIndex].RowSpan + 1;
                    }

                    nextRow.Cells[colIndex].Visible = false;
                    currentRow.Cells[colIndex].HorizontalAlign = HorizontalAlign.Center;
                    currentRow.Cells[colIndex].VerticalAlign = VerticalAlign.Middle;
                }
            }
        }
    }

    private string GetCellText(TableCell cell)
    {
        foreach (Control ctrl in cell.Controls)
        {
            if (ctrl is Label)
                return ((Label)ctrl).Text.Trim();
            if (ctrl is HyperLink)
                return ((HyperLink)ctrl).Text.Trim();
        }

        return cell.Text.Trim(); // fallback
    }



    // Event handler for each row in the Interventions GridView (gvInterventions)
    // This is where you bind the nested Indicators and Budgets GridViews
    protected void gvInterventions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the current InterventionID from the row's DataItem
            DataRowView interventionRow = (DataRowView)e.Row.DataItem;

            //int currentInterventionId = Convert.ToInt32(interventionRow);
            //int currentInterventionId = Convert.ToInt32(2);
            int currentInterventionId = Convert.ToInt32(interventionRow["InterventionID"]);







            // Find the nested GridViews for Indicators and Budgets
            GridView gvInterventionIndicators = (GridView)e.Row.FindControl("gvInterventionIndicators");
            GridView gvInterventionBudgets = (GridView)e.Row.FindControl("gvInterventionBudgets");

            if (currentPlanningOverviewDataSet != null && currentPlanningOverviewDataSet.Tables.Count >= 5)
            {
                // Result Set 4: Intervention Indicators table
                DataTable indicatorsTable = currentPlanningOverviewDataSet.Tables[3];
                // Result Set 5: Intervention Budgets table
                DataTable budgetsTable = currentPlanningOverviewDataSet.Tables[4];

                // Bind Intervention Indicators
                if (gvInterventionIndicators != null)
                {
                    DataView dvIndicators = new DataView(indicatorsTable);
                    dvIndicators.RowFilter = "InterventionID =" + currentInterventionId;
                    gvInterventionIndicators.DataSource = dvIndicators;
                    gvInterventionIndicators.DataBind();
                }

                // Bind Intervention Budgets
                if (gvInterventionBudgets != null)
                {
                    DataView dvBudgets = new DataView(budgetsTable);
                    dvBudgets.RowFilter = "InterventionID =" + currentInterventionId;
                    gvInterventionBudgets.DataSource = dvBudgets;
                    gvInterventionBudgets.DataBind();
                }
            }
        }
    }

}












