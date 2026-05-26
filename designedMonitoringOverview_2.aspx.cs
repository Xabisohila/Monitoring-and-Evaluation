using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OfficeOpenXml;

public partial class designedMonitoringOverview_2 : System.Web.UI.Page
{
    // Store the full DataSet in a private field for easier access during nested binding events
    private DataSet monitoringDataSet;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdowns();
        }
    }

    //private void PopulateDropdowns()
    //{
    //    ddlCluster.Items.Add(new ListItem("Select Cluster", "0"));
    //    ddlWorkGroup.Items.Add(new ListItem("Select Work Group", "0"));
    //    ddlPriority.Items.Add(new ListItem("Select Priority", "0"));
    //    ddlFinancialYear.Items.Add(new ListItem("Select Financial Year", "0"));
    //}

    private void PopulateDropdowns()
    {
        cls_MonitoringOverviewRepository repo = new cls_MonitoringOverviewRepository();

        // Populate Clusters
        System.Data.DataTable dtClusters = repo.GetAllClustersLookup();
        ddlCluster.DataSource = dtClusters;
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Year --", "0"));


        // Populate Work Groups
        System.Data.DataTable dtWorkGroups = repo.GetAllWorkGroupsLookup();
        ddlWorkGroup.DataSource = dtWorkGroups;
        ddlWorkGroup.DataTextField = "WG_Name";
        ddlWorkGroup.DataValueField = "WorkingGroupID";
        ddlWorkGroup.DataBind();
        ddlWorkGroup.Items.Insert(0, new ListItem("-- Select Work Group --", "0"));

        // Populate PMTDP Priorities
        System.Data.DataTable dtPMTDPPriorities = repo.GetAllPMTDPPrioritiesLookup();
        ddlPriority.DataSource = dtPMTDPPriorities;
        ddlPriority.DataTextField = "PriorityName";
        ddlPriority.DataValueField = "PMTDP_PriorityID";
        ddlPriority.DataBind();
        ddlPriority.Items.Insert(0, new ListItem("-- Select Priority --", "0"));

        // Populate Financial Years
        System.Data.DataTable dtFinancialYears = repo.GetAllFinancialYearsLookup();
        ddlFinancialYear.DataSource = dtFinancialYears;
        ddlFinancialYear.DataTextField = "FY_Name";
        ddlFinancialYear.DataValueField = "FY_ID";
        ddlFinancialYear.DataBind();
        ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
    }

    protected void btnViewMonitoring_Click(object sender, EventArgs e)
    {
        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        int workGroupId = Convert.ToInt32(ddlWorkGroup.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPriority.SelectedValue);
        int financialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);

        cls_MonitoringOverviewRepository repo = new cls_MonitoringOverviewRepository();
        monitoringDataSet = repo.GetMonitoringOverview_2(clusterId, workGroupId, priorityId, financialYearId);

        if (monitoringDataSet != null && monitoringDataSet.Tables.Count >= 4)
        {
            rptSubOutcomes.DataSource = monitoringDataSet.Tables[0];
            rptSubOutcomes.DataBind();
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = "No data found for selected filters.";
            lblMessage.Visible = true;
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (monitoringDataSet == null || monitoringDataSet.Tables.Count == 0)
            {
                lblMessage.Text = "No data available to export.";
                lblMessage.Visible = true;
                return;
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                AddSheet(package, "SubOutcomes", monitoringDataSet.Tables["SubOutcomes"]);
                AddSheet(package, "Interventions", monitoringDataSet.Tables["Interventions"]);
                AddSheet(package, "Indicators", monitoringDataSet.Tables["Indicators"]);
                AddSheet(package, "Budgets", monitoringDataSet.Tables["Budgets"]);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=MonitoringExport.xlsx");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error exporting to Excel: " + ex.Message;
            lblMessage.Visible = true;
        }
    }

    private void AddSheet(ExcelPackage package, string sheetName, System.Data.DataTable table)
    {
        var worksheet = package.Workbook.Worksheets.Add(sheetName);
        worksheet.Cells["A1"].LoadFromDataTable(table, true);
        worksheet.Cells.AutoFitColumns();
    }

    protected void rptSubOutcomes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // Bind nested GridViews if needed

        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    // Get the current POA's ID from the DataItem
        //    DataRowView poaRowView = (DataRowView)e.Item.DataItem;

        //    //int currentPOAId = Convert.ToInt32(poaRowView); // CORRECTED: Access by column name
        //    int currentPOAId = 702; // CORRECTED: Access by column name

        //    // Find the nested GridView for Interventions
        //    GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");

        //    if (gvInterventions != null && monitoringDataSet != null && monitoringDataSet.Tables.Count >= 3)
        //    {
        //        // Result Set 3: Interventions table from the DataSet
        //        System.Data.DataTable interventionsTable = monitoringDataSet.Tables[2];

        //        // Filter interventions for the current POA
        //        DataView dvInterventions = new DataView(interventionsTable);
        //        dvInterventions.RowFilter = "POA_ID = " + currentPOAId;

        //        gvInterventions.DataSource = dvInterventions;
        //        gvInterventions.DataBind();

        //        //MergeInstitutionCells(gvInterventions); // ✅ Add this line
        //    }
        //}

        //// Merge Row Content - If similar.
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    GridView gvInterventions = (GridView)e.Item.FindControl("gvInterventions");
        //    if (gvInterventions != null && gvInterventions.Rows.Count > 1)
        //    {
        //        //MergeGridViewRows(gvInterventions, new string[] { "Intervention", "Institution", "Municipality", "Spatial Ref." });
        //    }
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int clusterId = Convert.ToInt32(ddlCluster.SelectedValue);
        int workGroupId = Convert.ToInt32(ddlWorkGroup.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPriority.SelectedValue);
        int financialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);

        cls_MonitoringOverviewRepository repo = new cls_MonitoringOverviewRepository();
        monitoringDataSet = repo.GetMonitoringOverview_2(clusterId, workGroupId, priorityId, financialYearId);

        if (monitoringDataSet != null && monitoringDataSet.Tables.Count >= 4)
        {
            rptSubOutcomes.DataSource = monitoringDataSet.Tables["SubOutcomes"];
            rptSubOutcomes.DataBind();
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = "No data found for selected filters.";
            lblMessage.Visible = true;
        }
    }
}
