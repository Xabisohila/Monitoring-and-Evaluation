using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Other : System.Web.UI.Page
{
    private DataSet planningDataSet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack)
            {
                //PopulateDropdowns();
                //RestoreSelections();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    private void PopulateDropdowns()
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();

        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
        ddlWorkGroups.DataTextField = "WG_Name";
        ddlWorkGroups.DataValueField = "WorkingGroupID";
        ddlWorkGroups.DataBind();
        ddlWorkGroups.Items.Insert(0, new ListItem("-- Select Work Group --", "0"));

        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
        ddlPMTDPPriorities.DataTextField = "PriorityName";
        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
        ddlPMTDPPriorities.DataBind();
        ddlPMTDPPriorities.Items.Insert(0, new ListItem("-- Select Priority --", "0"));

        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
        ddlFinancialYears.DataTextField = "FY_Name";
        ddlFinancialYears.DataValueField = "FY_ID";
        ddlFinancialYears.DataBind();
        ddlFinancialYears.Items.Insert(0, new ListItem("-- Select Year --", "0"));
    }

    private void RestoreSelections()
    {
        if (Session["ddlWorkingGroupID"] != null)
            ddlWorkGroups.SelectedValue = Session["ddlWorkingGroupID"].ToString();

        if (Session["PMTDP_PriorityID"] != null)
            ddlPMTDPPriorities.SelectedValue = Session["PMTDP_PriorityID"].ToString();

        if (Session["FY_ID"] != null)
            ddlFinancialYears.SelectedValue = Session["FY_ID"].ToString();
    }

    protected void btnViewOverview_Click(object sender, EventArgs e)
    {
        int workGroupId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int financialYearId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        planningDataSet = repo.GetPlanningOverview(workGroupId, priorityId, financialYearId, clusterId);

        if (planningDataSet != null && planningDataSet.Tables.Count >= 5)
        {
            DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
            DataTable interventions = planningDataSet.Tables["Interventions"];
            DataTable indicators = planningDataSet.Tables["Indicators"];
            DataTable budgets = planningDataSet.Tables["Budgets"];

            //MyAccordion.Panes.Clear();

            foreach (DataRow subOutcome in subOutcomes.Rows)
            {
                int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
                string subOutcomeName = subOutcome["SubOutcome"].ToString();

                AccordionPane pane = new AccordionPane();
                pane.ID = "Pane_" + subOutcomeId;
                pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

                DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

                foreach (DataRow iRow in relatedInterventions)
                {
                    int interventionId = Convert.ToInt32(iRow["InterventionID"]);
                    string html = "<div class='intervention-block'>" +
                        "<a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "'>" +
                        "<p style='font-size:25px;text-decoration:underline;'>" + iRow["InterventionName"] + "</p></a>" +
                        "<p><strong>Institution:</strong> " + iRow["ImplementationInstitution"] + "</p>" +
                        "<p><strong>Municipality:</strong> " + iRow["PrimaryMunicipality"] + "</p>" +
                        "<p><strong>Period:</strong> " + iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</p>" +
                        "<h6>Indicators</h6>" +
                        "<table class='table table-bordered'>" +
                        "<tr style='background-color:#80bf64; color: white;'><th>Name</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th></tr>";

                    DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
                    foreach (DataRow ind in relatedIndicators)
                    {
                        html += "<tr><td>" + ind["OutcomeIndicator"] + "</td><td>" + ind["IndicatorType"] + "</td><td>" +
                                ind["UnitOfMeasure"] + "</td><td>" + ind["BaselineValue"] + " (" + ind["BaselineYear"] + ")</td><td>" +
                                ind["TargetValue"] + " (" + ind["TargetYear"] + ")</td></tr>";
                    }

                    html += "</table><h6>Budgets</h6><table class='table table-bordered'>" +
                            "<tr style='background-color:#80bf64; color: white;'><th>Year</th><th>Annual Budget</th><th>Term Budget</th></tr>";

                    DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);
                    foreach (DataRow bud in relatedBudgets)
                    {
                        decimal annual = bud["AnnualBudget"] != DBNull.Value ? Convert.ToDecimal(bud["AnnualBudget"]) : 0;
                        decimal term = bud["TermBudget"] != DBNull.Value ? Convert.ToDecimal(bud["TermBudget"]) : 0;

                        html += "<tr><td>" + bud["FinancialYear"] + "</td><td>R" + annual.ToString("N2") + "</td><td>R" + term.ToString("N2") + "</td></tr>";
                    }

                    html += "</table></div><hr/>";
                    pane.ContentContainer.Controls.Add(new LiteralControl(html));
                }

                //MyAccordion.Panes.Add(pane);
            }
        }
        else
        {
            lblMessage.Text = "No data found for the selected filters.";
        }
    }



























    // Store the full DataSet in a private field for easier access during nested binding events
    private DataSet currentPlanningOverviewDataSet;

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Session["Fullname"] != null)
    //    {
    //        if (!IsPostBack) // This code runs only when the page is first loaded
    //        {
    //            onStart();
    //            PopulateDropdowns();
    //            //pnlOverview.Visible = false; // Initially hide the detail panel
    //            //lblMessage.Visible = true;   // Show the initial message
    //        }
    //        else
    //        {

    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("login.aspx");
    //    }
    //}

    //protected void btnViewOverview_Click(object sender, EventArgs e)
    //{
    //    int workGroupId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
    //    int pmtdpPriorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
    //    int financialYearId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
    //    int clusterId = Convert.ToInt32((string)Session["Cluster_ID"]);//Convert.ToInt32(ddlCluster.SelectedValue);

    //    LoadSubOutcomeAccordion(workGroupId, pmtdpPriorityId, financialYearId, clusterId);
    //}

    //protected void LoadSubOutcomeAccordion(int workGroupId, int pmtdpPriorityId, int financialYearId, int clusterId)
    //{
    //    PlanningOverviewRepository repo = new PlanningOverviewRepository();
    //    currentPlanningOverviewDataSet = repo.GetPlanningOverview(workGroupId, pmtdpPriorityId, financialYearId, clusterId);

    //    if (currentPlanningOverviewDataSet != null && currentPlanningOverviewDataSet.Tables.Count >= 3)
    //    {
    //        DataTable subOutcomesTable = currentPlanningOverviewDataSet.Tables[1];
    //        rptSubOutcomes.DataSource = subOutcomesTable;
    //        rptSubOutcomes.DataBind();
    //        pnlOverview.Visible = true;
    //        lblMessage.Visible = false;
    //    }
    //    else
    //    {
    //        lblMessage.Text = "No SubOutcomes found for the selected filters.";
    //        pnlOverview.Visible = false;
    //        lblMessage.Visible = true;
    //    }
    //}

    //protected void rptSubOutcomes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DataRowView subOutcomeRow = (DataRowView)e.Item.DataItem;
    //        int subOutcomeId = Convert.ToInt32(subOutcomeRow["SubOutcomeId"]);

    //        GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventionsBySubOutcome");

    //        if (gvInterventions != null && currentPlanningOverviewDataSet != null)
    //        {
    //            DataTable interventionsTable = currentPlanningOverviewDataSet.Tables[2];
    //            DataView dvInterventions = new DataView(interventionsTable);
    //            dvInterventions.RowFilter = "SubOutcomeID = " + subOutcomeId;

    //            gvInterventions.DataSource = dvInterventions;
    //            gvInterventions.DataBind();
    //        }
    //    }
    //}

    private void PopulateDropdowns11()
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
            //pnlOverview.Visible = false;
            return;
        }

        LoadPlanningOverview(selectedWorkGroupId, selectedPMTDPPriorityId, selectedFinancialYearId, selectedClusterId);

    }

    private void LoadPlanningOverview(int workGroupId, int pmtdpPriorityId, int financialYearId, int clusterId)
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        currentPlanningOverviewDataSet = repo.GetPlanningOverview(workGroupId, pmtdpPriorityId, financialYearId, clusterId);

        // Check if the DataSet and its tables are as expected (at least 5 data tables + 3 lookup tables)
        if (currentPlanningOverviewDataSet != null && currentPlanningOverviewDataSet.Tables.Count >= 5)
        {

            if (currentPlanningOverviewDataSet.Tables.Count > 0 && currentPlanningOverviewDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow headerRow = currentPlanningOverviewDataSet.Tables[0].Rows[0];

                //lblSelectedPriorityName.Text = headerRow["PriorityFocus"].ToString();
                //lblFrameworkName.Text = headerRow["ImplementationFrameworkName"].ToString();
                //lblPDPGoal.Text = headerRow["PDPGoal"].ToString(); // Assuming this column exists
                //lblPriorityFocus.Text = headerRow["PriorityFocus"].ToString();
                //lblOverallImpact.Text = headerRow["PriorityImpact"].ToString();
                //lblPriorityDescription.Text = headerRow["PriorityDescription"].ToString(); // Assuming this column exists
            }

            else
            {
                lblMessage.Text = "No header details found for the selected filters. Please check your data.";
                //pnlOverview.Visible = false;
                lblMessage.Visible = true;
                return;
            }

            // Result Set 2: Programmes of Action (POAs)
            DataTable poaTable = currentPlanningOverviewDataSet.Tables[1];
            //rptPOAs.DataSource = poaTable;
            //rptPOAs.DataBind();

            //pnlOverview.Visible = true; // Show the detail panel
            lblMessage.Visible = false;  // Hide the message
        }
        else
        {
            lblMessage.Text = "Error: Failed to retrieve complete planning overview. Please check database configuration and data.";
            //pnlOverview.Visible = false;
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

}












