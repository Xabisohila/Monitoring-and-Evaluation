using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YourProjectName.Data;


public partial class designedMonitoringOverview : System.Web.UI.Page
{
    // Store the full DataSet in a private field for easier access during nested binding events
    private DataSet currentMonitoringOverviewDataSet;

    private string previousInstitution = string.Empty;


    //string currentInstitution = interventionRowView["ImplementationInstitution"] != null
    //    ? interventionRowView["ImplementationInstitution"].ToString()
    //    : string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // This code runs only when the page is first loaded
        {
            PopulateDropdowns();
            selectedValues();
            //pnlOverview.Visible = false; // Initially hide the detail panel
            //lblMessage.Visible = true;   // Show the initial message
        }
    }

    private void PopulateDropdowns()
    {
        cls_MonitoringOverviewRepository repo = new cls_MonitoringOverviewRepository();

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



    private void selectedValues()
    {
        if (Session["ddlWorkingGroupID"] != null && Session["PMTDP_PriorityID"] != null && Session["FY_ID"] != null)
        {
            string selectedWGID = Session["ddlWorkingGroupID"].ToString();
            string selectedPMTDPPriorities = Session["PMTDP_PriorityID"].ToString();
            string selectedFinancialYears = Session["FY_ID"].ToString();

            // Check if the value exists in the dropdown before setting it
            if (ddlWorkGroups.Items.FindByValue(selectedWGID) != null)
            {
                ddlWorkGroups.SelectedValue = selectedWGID;
            }

            // Check if the value exists in the dropdown before setting it
            if (ddlPMTDPPriorities.Items.FindByValue(selectedPMTDPPriorities) != null)
            {
                ddlPMTDPPriorities.SelectedValue = selectedPMTDPPriorities;
            }

            // Check if the value exists in the dropdown before setting it
            if (ddlFinancialYears.Items.FindByValue(selectedFinancialYears) != null)
            {
                ddlFinancialYears.SelectedValue = selectedFinancialYears;
            }

            // Call the button's click handler directly
            //btnViewOverview_Click(this, EventArgs.Empty);
            btnViewOverview_Click(this, EventArgs.Empty);

        }
    }


















    protected void btnViewOverview_Click(object sender, EventArgs e)
    {
        int selectedWorkGroupId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int selectedPMTDPPriorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int selectedFinancialYearId = Convert.ToInt32(ddlFinancialYears.SelectedValue);

        // Validate selections
        if (selectedWorkGroupId == 0 || selectedPMTDPPriorityId == 0 || selectedFinancialYearId == 0)
        {
            lblMessage.Text = "Please make a selection for Work Group, PMTDP Priority, and Financial Year.";
            lblMessage.Visible = true;
            pnlOverview.Visible = false;
            return;
        }

        LoadMonitoringOverview(selectedWorkGroupId, selectedPMTDPPriorityId, selectedFinancialYearId);
    }

    private void LoadMonitoringOverview(int workGroupId, int pmtdpPriorityId, int financialYearId)
    {
        cls_MonitoringOverviewRepository repo = new cls_MonitoringOverviewRepository();
        currentMonitoringOverviewDataSet = repo.GetMonitoringOverview(workGroupId, pmtdpPriorityId, financialYearId);

        // Check if the DataSet and its tables are as expected (at least 5 data tables for main content)
        if (currentMonitoringOverviewDataSet != null && currentMonitoringOverviewDataSet.Tables.Count >= 5)
        {
            // Result Set 1: PMTDP Priority Header Information (ds.Tables)
            if (currentMonitoringOverviewDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow headerRow = currentMonitoringOverviewDataSet.Tables[0].Rows[0]; // Access the first row of the first table
                lblSelectedPriorityName.Text = headerRow["PriorityFocus"].ToString();
                lblFrameworkName.Text = headerRow["ImplementationFrameworkName"].ToString();
                lblPDPGoal.Text = headerRow["PDPGoal"].ToString();
                lblPriorityFocus.Text = headerRow["PriorityFocus"].ToString();
                lblOverallImpact.Text = headerRow["PriorityImpact"].ToString();
                lblPriorityDescription.Text = headerRow["PriorityDescription"].ToString();
            }
            else
            {
                lblMessage.Text = "No header details found for the selected filters. Please check your data.";
                pnlOverview.Visible = false;
                lblMessage.Visible = true;
                return;
            }

            // Result Set 2: Programmes of Action (POAs) (ds.Tables[1])
            DataTable poaTable = currentMonitoringOverviewDataSet.Tables[1];
            rptPOAs.DataSource = poaTable;
            rptPOAs.DataBind();

            pnlOverview.Visible = true; // Show the detail panel
            lblMessage.Visible = false;  // Hide the message
        }
        else
        {
            lblMessage.Text = "Error: Failed to retrieve complete monitoring overview. Please check database configuration and data.";
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
            DataRowView poaRowView = (DataRowView)e.Item.DataItem;

            //int currentPOAId = Convert.ToInt32(poaRowView); // CORRECTED: Access by column name
            int currentPOAId = 702; // CORRECTED: Access by column name

            // Find the nested GridView for Interventions
            GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");

            if (gvInterventions != null && currentMonitoringOverviewDataSet != null && currentMonitoringOverviewDataSet.Tables.Count >= 3)
            {
                // Result Set 3: Interventions table from the DataSet
                DataTable interventionsTable = currentMonitoringOverviewDataSet.Tables[2];

                // Filter interventions for the current POA
                DataView dvInterventions = new DataView(interventionsTable);
                dvInterventions.RowFilter = "POA_ID = " + currentPOAId;

                gvInterventions.DataSource = dvInterventions;
                gvInterventions.DataBind();

                //MergeInstitutionCells(gvInterventions); // ✅ Add this line
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


