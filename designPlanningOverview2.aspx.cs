using AjaxControlToolkit;
using iTextSharp.tool.xml.html;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class designPlanningOverview2 : System.Web.UI.Page
{
    private DataSet planningDataSet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Cluster_ID"] = 401;
            BindDropdowns();
        }
    }

    private void BindDropdowns()
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
        ddlWorkGroups.DataTextField = "WG_Name";
        ddlWorkGroups.DataValueField = "WorkingGroupID";
        ddlWorkGroups.DataBind();

        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
        ddlPMTDPPriorities.DataTextField = "PriorityName";
        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
        ddlPMTDPPriorities.DataBind();

        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
        ddlFinancialYears.DataTextField = "FY_Name";
        ddlFinancialYears.DataValueField = "FY_ID";
        ddlFinancialYears.DataBind();
    }

    protected void btnViewOverview_Click(object sender, EventArgs e)
    {
        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        DataTable interventions = planningDataSet.Tables["Interventions"];
        DataTable indicators = planningDataSet.Tables["Indicators"];
        DataTable budgets = planningDataSet.Tables["Budgets"];

        MyAccordion.Panes.Clear();

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
//"<h5><strong>" + iRow["InterventionName"] + "</strong></h5>" +
//"<p style='font-size:25px;text-decoration:underline;'><strong>Intervention:</strong> " + iRow["InterventionName"] + "</p>" +


"<a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "' >" +
"<p style='font-size:25px;text-decoration:underline;'> " + iRow["InterventionName"] + "</p></a>" +


                //"<p style='font-size:25px;'>" +

                //"< span class='data-label' >Parent POA:</span> "+

                //"<a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "' ></a>" +














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

                //html += "<div style='margin-top:10px;'>" +
                //    "<a href='designSubmitQuarterlyReport.aspx?id=" + interventionId + " ></a> " +
                //    "</div>";



                //html += "</table></div><hr/>";
                html += "</table>";


                //html += "<div style='margin-top:10px;'>" +
                //    interventionId +
                //    "<a href='designSubmitQuarterlyReport.aspx?id=" + interventionId + "' ></a>" +

                //    "</div>";




//                html += "<div style='margin-top:10px;'><a href='designSubmitQuarterlyReport.aspx?id=" + interventionId + "' >" +
//"<p style='font-size:25px;text-decoration:underline;'> " + "Submit Quarterly Report" + "</p></a></div>"

                html +=

                    "<div style='margin-top:10px;'>" +
"<asp:button  class='my-button' onclick=" + "location.href='designSubmitQuarterlyReport.aspx?id=" + interventionId + "' " +
"style='padding:10px 20px; font-size:16px; background-color:#b3777b; color:white;'>Submit New Report</asp:button></div>"










+ "</div><hr/>";









                pane.ContentContainer.Controls.Add(new LiteralControl(html));
            }

            MyAccordion.Panes.Add(pane);
        }
        //btnViewOverview1_Click(sender, e);
    }










    // another 1 below


    protected void btnViewOverview1_Click(object sender, EventArgs e)
    {
        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        DataTable interventions = planningDataSet.Tables["Interventions"];
        DataTable indicators = planningDataSet.Tables["Indicators"];
        DataTable budgets = planningDataSet.Tables["Budgets"];

        MyAccordion.Panes.Clear();

        foreach (DataRow subOutcome in subOutcomes.Rows)
        {
            int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
            string subOutcomeName = subOutcome["SubOutcome"].ToString();

            AccordionPane pane = new AccordionPane();
            pane.ID = "Pane_" + subOutcomeId;
            pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

            string html = "<table border='1'><tr style='background-color:#80bf64; color: white;'><th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th><th>Implementation Institution</th><th>Intervention</th><th>Baseline</th><th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

            string prevOutcome = "", prevIndicator = "", prevType = "", prevInstitution = "";
            int outcomeSpan = 1, indicatorSpan = 1, typeSpan = 1, institutionSpan = 1;

            foreach (DataRow iRow in relatedInterventions)
            {
                int interventionId = Convert.ToInt32(iRow["InterventionID"]);
                string interventionName = iRow["InterventionName"].ToString();
                string institution = iRow["ImplementationInstitution"].ToString();
                string spatialRef = iRow["SpatialReference"].ToString();

                DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
                DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

                foreach (DataRow ind in relatedIndicators)
                {
                    string outcome = subOutcomeName;
                    string indicator = ind["OutcomeIndicator"].ToString();
                    string type = ind["IndicatorType"].ToString();
                    string baseline = ind["BaselineValue"].ToString();
                    string target2030 = ind["Target2030_TermTarget"].ToString();
                    string targetValue = ind["TargetValue"].ToString();

                    decimal annual = 0;
                    if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
                        annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

                    html += "<tr>";
                    html += (outcome != prevOutcome ? "<td rowspan='1'>" + outcome + "</td>" : "<td></td>");
                    html += (indicator != prevIndicator ? "<td rowspan='1'>" + indicator + "</td>" : "<td></td>");
                    html += (type != prevType ? "<td rowspan='1'>" + type + "</td>" : "<td></td>");
                    html += (institution != prevInstitution ? "<td rowspan='1'>" + institution + "</td>" : "<td></td>");
                    html += "<td>" + interventionName + "</td><td>" + baseline + "</td><td>" + target2030 + "</td><td>R" + annual.ToString("N2") + "</td><td>" + targetValue + "</td><td>" + spatialRef + "</td>";
                    html += "</tr>";

                    prevOutcome = outcome;
                    prevIndicator = indicator;
                    prevType = type;
                    prevInstitution = institution;
                }
            }

            html += "</table>";
            pane.ContentContainer.Controls.Add(new LiteralControl(html));
            MyAccordion.Panes.Add(pane);
        }
    }














}
















































//Terrible




//using AjaxControlToolkit;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class designPlanningOverview2 : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack)
//        {
//            Session["Cluster_ID"] = 401;
//            BindDropdowns();
//        }
//    }

//    private void BindDropdowns()
//    {
//        PlanningOverviewRepository repo = new PlanningOverviewRepository();

//        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
//        ddlWorkGroups.DataTextField = "WG_Name";
//        ddlWorkGroups.DataValueField = "WorkingGroupID";
//        ddlWorkGroups.DataBind();

//        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
//        ddlPMTDPPriorities.DataTextField = "PriorityName";
//        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
//        ddlPMTDPPriorities.DataBind();

//        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
//        ddlFinancialYears.DataTextField = "FY_Name";
//        ddlFinancialYears.DataValueField = "FY_ID";
//        ddlFinancialYears.DataBind();
//    }

//    protected void btnViewOverview_Click(object sender, EventArgs e)
//    {
//        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
//        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
//        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
//        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

//        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
//        DataSet ds = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

//        DataTable subOutcomes = ds.Tables["SubOutcomes"];
//        DataTable interventions = ds.Tables["Interventions"];

//        MyAccordion.Panes.Clear(); // Clear previous panes

//        foreach (DataRow row in subOutcomes.Rows)
//        {
//            int subOutcomeId = Convert.ToInt32(row["SubOutcomeId"]);
//            string subOutcomeName = row["SubOutcome"].ToString();

//            AccordionPane pane = new AccordionPane();
//            pane.ID = "Pane_" + subOutcomeId;

//            // Header
//            pane.HeaderContainer.Controls.Add(new LiteralControl("<div class='accordionHeader'>" + subOutcomeName + "</div>"));

//            // Content
//            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);
//            string html = "<div class='accordionContent'><table><tr><th>Name</th><th>Institution</th><th>Municipality</th><th>Period</th></tr>";

//            foreach (DataRow iRow in relatedInterventions)
//            {
//                html += "<tr><td>" + iRow["InterventionName"] + "</td><td>" +
//                        iRow["ImplementationInstitution"] + "</td><td>" +
//                        iRow["PrimaryMunicipality"] + "</td><td>" +
//                        iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</td></tr>";
//            }

//            html += "</table></div>";
//            pane.ContentContainer.Controls.Add(new LiteralControl(html));

//            MyAccordion.Panes.Add(pane);
//        }
//    }

//}
///










































// latest-----------------------------------------





//using AjaxControlToolkit;
//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class designPlanningOverview2 : System.Web.UI.Page
//{
//    private DataSet planningDataSet;

//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack)
//        {
//            Session["Cluster_ID"] = 401;
//            BindDropdowns();
//        }
//    }

//    private void BindDropdowns()
//    {
//        PlanningOverviewRepository repo = new PlanningOverviewRepository();
//        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
//        ddlWorkGroups.DataTextField = "WG_Name";
//        ddlWorkGroups.DataValueField = "WorkingGroupID";
//        ddlWorkGroups.DataBind();

//        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
//        ddlPMTDPPriorities.DataTextField = "PriorityName";
//        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
//        ddlPMTDPPriorities.DataBind();

//        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
//        ddlFinancialYears.DataTextField = "FY_Name";
//        ddlFinancialYears.DataValueField = "FY_ID";
//        ddlFinancialYears.DataBind();
//    }

//    protected void btnViewOverview_Click(object sender, EventArgs e)
//    {
//        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
//        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
//        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
//        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

//        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
//        planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

//        DataTable header = planningDataSet.Tables["PriorityHeader"];
//        DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
//        DataTable interventions = planningDataSet.Tables["Interventions"];
//        DataTable indicators = planningDataSet.Tables["Indicators"];
//        DataTable budgets = planningDataSet.Tables["Budgets"];

//        MyAccordion.Panes.Clear();

//        string pdpGoal = "", priorityFocus = "", impact = "", programme = "";

//        //if (header.Rows.Count > 0)
//        //{
//        DataRow h = header.Rows[0];
//        pdpGoal = h["PDPGoal"].ToString();
//        priorityFocus = h["PriorityFocus"].ToString();
//        impact = h["PriorityImpact"].ToString();
//        programme = h["ImplementationFrameworkName"].ToString();
//        //}

//        foreach (DataRow subOutcome in subOutcomes.Rows)
//        {
//            int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
//            string subOutcomeName = subOutcome["SubOutcome"].ToString();

//            AccordionPane pane = new AccordionPane();
//            pane.ID = "Pane_" + subOutcomeId;

//            string html = "<div class='overview-header'>" +
//                "<p><strong>PDP Goal:</strong> " + pdpGoal + "</p>" +
//                "<p><strong>Priority Focus:</strong> " + priorityFocus + "</p>" +
//                "<p><strong>Integration Programme:</strong> " + programme + "</p>" +
//                "<p><strong>Impact:</strong> " + impact + "</p>" +
//                "</div>";

//            html += "<h4>" + subOutcomeName + "</h4>";
//            pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));


//            html += "<table class='table table-bordered'><tr>" +
//                "<th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th>" +
//                "<th>PDP Fulfilment (Baseline 2025)</th><th>PDP Fulfilment (Target 2030)</th>" +
//                "<th>Implementation Institution</th><th>Dependency</th><th>Intervention</th>" +
//                "<th>Intervention Indicator</th><th>Baseline</th><th>2030 Term Target</th>" +
//                "<th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

//            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

//            foreach (DataRow iRow in relatedInterventions)
//            {
//                int interventionId = Convert.ToInt32(iRow["InterventionID"]);
//                string institution = iRow["ImplementationInstitution"].ToString();
//                string municipality = iRow["PrimaryMunicipality"].ToString();
//                string spatialRef = iRow["SpatialReference"].ToString();
//                string interventionName = iRow["InterventionName"].ToString();
//                string dependency = ""; // Add logic if available

//                DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
//                DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

//                foreach (DataRow ind in relatedIndicators)
//                {
//                    string indicatorName = ind["OutcomeIndicator"].ToString();
//                    string indicatorType = ind["IndicatorType"].ToString();
//                    string unit = ind["UnitOfMeasure"].ToString();
//                    string baseline = ind["BaselineValue"].ToString();
//                    string baselineYear = ind["BaselineYear"].ToString();
//                    string target2030 = ind["Target2030_TermTarget"].ToString();
//                    string targetYear = ind["TargetYear"].ToString();
//                    string targetValue = ind["TargetValue"].ToString();

//                    decimal annual = 0;
//                    decimal term = 0;
//                    string fyName = "";

//                    if (relatedBudgets.Length > 0)
//                    {
//                        DataRow bud = relatedBudgets[0];
//                        fyName = bud["FinancialYear"].ToString();
//                        annual = bud["AnnualBudget"] != DBNull.Value ? Convert.ToDecimal(bud["AnnualBudget"]) : 0;
//                        term = bud["TermBudget"] != DBNull.Value ? Convert.ToDecimal(bud["TermBudget"]) : 0;
//                    }

//                    html += "<tr><td>" + impact + "</td><td>" + indicatorName + "</td><td>" + indicatorType + "</td>" +
//                            "<td>" + baseline + " (" + baselineYear + ")</td><td>" + targetValue + " (" + targetYear + ")</td>" +
//                            "<td>" + institution + "</td><td>" + dependency + "</td><td>" + interventionName + "</td>" +
//                            "<td>" + indicatorName + "</td><td>" + baseline + "</td><td>" + target2030 + "</td>" +
//                            "<td>R" + annual.ToString("N2") + "</td><td>" + targetValue + "</td><td>" + spatialRef + "</td></tr>";
//                }
//            }

//            html += "</table>";
//            pane.ContentContainer.Controls.Add(new LiteralControl(html));
//            MyAccordion.Panes.Add(pane);
//        }
//    }

//    protected void gvInterventions_RowDataBound(object sender, GridViewRowEventArgs e)
//    {
//        if (e.Row.RowType == DataControlRowType.DataRow)
//        {
//            DataRowView interventionRow = (DataRowView)e.Row.DataItem;
//            int currentInterventionId = Convert.ToInt32(interventionRow["InterventionID"]);

//            GridView gvInterventionIndicators = (GridView)e.Row.FindControl("gvInterventionIndicators");
//            GridView gvInterventionBudgets = (GridView)e.Row.FindControl("gvInterventionBudgets");

//            if (planningDataSet != null && planningDataSet.Tables.Count >= 5)
//            {
//                DataTable indicatorsTable = planningDataSet.Tables[3];
//                DataTable budgetsTable = planningDataSet.Tables[4];

//                if (gvInterventionIndicators != null)
//                {
//                    DataView dvIndicators = new DataView(indicatorsTable);
//                    dvIndicators.RowFilter = "InterventionID =" + currentInterventionId;
//                    gvInterventionIndicators.DataSource = dvIndicators;
//                    gvInterventionIndicators.DataBind();
//                }

//                if (gvInterventionBudgets != null)
//                {
//                    DataView dvBudgets = new DataView(budgetsTable);
//                    dvBudgets.RowFilter = "InterventionID =" + currentInterventionId;
//                    gvInterventionBudgets.DataSource = dvBudgets;
//                    gvInterventionBudgets.DataBind();
//                }
//            }
//        }
//    }
//}



















































































































































//using AjaxControlToolkit;
//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class designPlanningOverview2 : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack)
//        {
//            Session["Cluster_ID"] = 401;
//            BindDropdowns();
//        }
//    }

//    private void BindDropdowns()
//    {
//        PlanningOverviewRepository repo = new PlanningOverviewRepository();
//        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
//        ddlWorkGroups.DataTextField = "WG_Name";
//        ddlWorkGroups.DataValueField = "WorkingGroupID";
//        ddlWorkGroups.DataBind();

//        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
//        ddlPMTDPPriorities.DataTextField = "PriorityName";
//        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
//        ddlPMTDPPriorities.DataBind();

//        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
//        ddlFinancialYears.DataTextField = "FY_Name";
//        ddlFinancialYears.DataValueField = "FY_ID";
//        ddlFinancialYears.DataBind();
//    }

//    protected void btnViewOverview_Click(object sender, EventArgs e)
//    {
//        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
//        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
//        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
//        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

//        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
//        DataSet ds = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

//        DataTable subOutcomes = ds.Tables["SubOutcomes"];
//        DataTable interventions = ds.Tables["Interventions"];

//        MyAccordion.Panes.Clear();

//        foreach (DataRow row in subOutcomes.Rows)
//        {
//            int subOutcomeId = Convert.ToInt32(row["SubOutcomeId"]);
//            string subOutcomeName = row["SubOutcome"].ToString();

//            AccordionPane pane = new AccordionPane();
//            pane.ID = "Pane_" + subOutcomeId;

//            // Simplified header rendering
//            pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

//            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);
//            string html = "<table><tr><th>Name</th><th>Institution</th><th>Municipality</th><th>Period</th></tr>";

//            foreach (DataRow iRow in relatedInterventions)
//            {
//                html += "<tr><td>" + iRow["InterventionName"] + "</td><td>" +
//                        iRow["ImplementationInstitution"] + "</td><td>" +
//                        iRow["PrimaryMunicipality"] + "</td><td>" +
//                        iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</td></tr>";
//            }

//            html += "</table>";
//            pane.ContentContainer.Controls.Add(new LiteralControl(html));
//            MyAccordion.Panes.Add(pane);
//        }
//    }
//}



























































// not finished yet


//using AjaxControlToolkit;
//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class designPlanningOverview2 : System.Web.UI.Page
//{
//    private DataSet planningDataSet;

//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack)
//        {
//            Session["Cluster_ID"] = 401;
//            BindDropdowns();
//        }
//    }

//    private void BindDropdowns()
//    {
//        PlanningOverviewRepository repo = new PlanningOverviewRepository();
//        ddlWorkGroups.DataSource = repo.GetAllWorkGroupsLookup();
//        ddlWorkGroups.DataTextField = "WG_Name";
//        ddlWorkGroups.DataValueField = "WorkingGroupID";
//        ddlWorkGroups.DataBind();

//        ddlPMTDPPriorities.DataSource = repo.GetAllPMTDPPrioritiesLookup();
//        ddlPMTDPPriorities.DataTextField = "PriorityName";
//        ddlPMTDPPriorities.DataValueField = "PMTDP_PriorityID";
//        ddlPMTDPPriorities.DataBind();

//        ddlFinancialYears.DataSource = repo.GetAllFinancialYearsLookup();
//        ddlFinancialYears.DataTextField = "FY_Name";
//        ddlFinancialYears.DataValueField = "FY_ID";
//        ddlFinancialYears.DataBind();
//    }

//    protected void btnViewOverview_Click(object sender, EventArgs e)
//    {
//        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
//        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
//        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
//        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

//        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
//        planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

//        DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
//        DataTable interventions = planningDataSet.Tables["Interventions"];
//        DataTable indicators = planningDataSet.Tables["Indicators"];
//        DataTable budgets = planningDataSet.Tables["Budgets"];

//        MyAccordion.Panes.Clear();

//        foreach (DataRow subOutcome in subOutcomes.Rows)
//        {
//            int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
//            string subOutcomeName = subOutcome["SubOutcome"].ToString();

//            AccordionPane pane = new AccordionPane();
//            pane.ID = "Pane_" + subOutcomeId;
//            pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

//            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

//            string html = "<table border='1'><tr><th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th><th>Implementation Institution</th><th>Intervention</th><th>Baseline</th><th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

//            string prevOutcome = "", prevIndicator = "", prevType = "", prevInstitution = "";
//            int outcomeSpan = 1, indicatorSpan = 1, typeSpan = 1, institutionSpan = 1;

//            foreach (DataRow iRow in relatedInterventions)
//            {
//                int interventionId = Convert.ToInt32(iRow["InterventionID"]);
//                string interventionName = iRow["InterventionName"].ToString();
//                string institution = iRow["ImplementationInstitution"].ToString();
//                string spatialRef = iRow["SpatialReference"].ToString();

//                DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
//                DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

//                foreach (DataRow ind in relatedIndicators)
//                {
//                    string outcome = subOutcomeName;
//                    string indicator = ind["OutcomeIndicator"].ToString();
//                    string type = ind["IndicatorType"].ToString();
//                    string baseline = ind["BaselineValue"].ToString();
//                    string target2030 = ind["Target2030_TermTarget"].ToString();
//                    string targetValue = ind["TargetValue"].ToString();

//                    decimal annual = 0;
//                    if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
//                        annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

//                    html += "<tr>";
//                    html += (outcome != prevOutcome ? "<td rowspan='1'>" + outcome + "</td>" : "<td></td>");
//                    html += (indicator != prevIndicator ? "<td rowspan='1'>" + indicator + "</td>" : "<td></td>");
//                    html += (type != prevType ? "<td rowspan='1'>" + type + "</td>" : "<td></td>");
//                    html += (institution != prevInstitution ? "<td rowspan='1'>" + institution + "</td>" : "<td></td>");
//                    html += "<td>" + interventionName + "</td><td>" + baseline + "</td><td>" + target2030 + "</td><td>R" + annual.ToString("N2") + "</td><td>" + targetValue + "</td><td>" + spatialRef + "</td>";
//                    html += "</tr>";

//                    prevOutcome = outcome;
//                    prevIndicator = indicator;
//                    prevType = type;
//                    prevInstitution = institution;
//                }
//            }

//            html += "</table>";
//            pane.ContentContainer.Controls.Add(new LiteralControl(html));
//            MyAccordion.Panes.Add(pane);
//        }
//    }
//}



