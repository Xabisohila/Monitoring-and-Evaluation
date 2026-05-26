//using AjaxControlToolkit;
//using iTextSharp.tool.xml.html;
//using Microsoft.Office.Interop.Excel;
//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;

using AjaxControlToolkit;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using iTextSharp.tool.xml.html;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class designedPlanningOverview : System.Web.UI.Page
{
    private DataSet planningDataSet;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!IsPostBack)
            {
                //Session["Cluster_ID"] = 401;

                if (Session["Cluster_ID"] != null)
                {
                    BindDropdowns();
                    selectedValues();
                }
                else
                {
                    Response.Redirect("home.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }   
    }

    //protected void onStart()
    //{
    //    int selectedClusterId = Convert.ToInt32((string)Session["Cluster_ID"]);
    //    string selectedType = Session["selectType"].ToString();//"Planning";
    //    int selectedWorkGroupId = Convert.ToInt32((string)Session["ddlWorkingGroupID"]);
    //    int selectedPMTDPPriorityId = Convert.ToInt32((string)Session["PMTDP_PriorityID"]);
    //    int selectedFinancialYearId = Convert.ToInt32((string)Session["FY_ID"]);

    //    // Validate selections
    //    if (selectedWorkGroupId == 0 || selectedPMTDPPriorityId == 0 || selectedFinancialYearId == 0)
    //    {
    //        //lblMessage.Text = "Please make a selection for Work Group, PMTDP Priority, and Financial Year.";
    //        //lblMessage.Visible = true;
    //        //pnlOverview.Visible = false;
    //        //return;
    //    }

    //    //LoadPlanningOverview(selectedWorkGroupId, selectedPMTDPPriorityId, selectedFinancialYearId, selectedClusterId);

    //}

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
            btnViewOverview1_Click(this, EventArgs.Empty);

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

        MyAccordion.Panes.Clear();

        foreach (DataRow subOutcome in subOutcomes.Rows)
        {
            int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
            string subOutcomeName = subOutcome["SubOutcome"].ToString();

            AccordionPane pane = new AccordionPane();
            pane.ID = "Pane_" + subOutcomeId;
            pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

            if (relatedInterventions.Length > 0)
            {
                string html = "<table class='table table-bordered'>" +
                              "<tr style='background-color:#80bf64; color: white;'>" +
                              "<th>Intervention Name</th>" +
                              "<th>Institution</th>" +
                              "<th>Municipality</th>" +
                              "<th>Period</th>" +
                              "<th>Action</th>" +
                              "</tr>";

                // Manual grouping by Institution and Municipality
                Dictionary<string, List<DataRow>> grouped = new Dictionary<string, List<DataRow>>();

                foreach (DataRow row in relatedInterventions)
                {
                    string institution = row["ImplementationInstitution"].ToString();
                    string municipality = row["PrimaryMunicipality"].ToString();
                    string key = institution + "|" + municipality;

                    if (!grouped.ContainsKey(key))
                    {
                        grouped[key] = new List<DataRow>();
                    }

                    grouped[key].Add(row);
                }

                foreach (var group in grouped)
                {
                    List<DataRow> rows = group.Value;
                    int rowspan = rows.Count;
                    bool firstRow = true;

                    foreach (DataRow iRow in rows)
                    {
                        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
                        string interventionName = iRow["InterventionName"].ToString();
                        string institution = iRow["ImplementationInstitution"].ToString();
                        string municipality = iRow["PrimaryMunicipality"].ToString();
                        string period = iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"];

                        html += "<tr>";

                        html += 
                            //"<td>pageInterventionsDirectDetail.aspx?id=<span>" + interventionName + "</span></a></td>" +
                            "<td><a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + 
                            "&cls=" + clusterId +
                            "'an style='text-decoration:underline; font-weight:bold;'><span>" + interventionName + "</span></a></td>";

                        if (firstRow)
                        {
                            html += "<td rowspan='" + rowspan + "' style='text-align:center;vertical-align: middle;'>" + institution + "</td>";
                            html += "<td rowspan='" + rowspan + "' style='text-align:center;vertical-align: middle;'>" + municipality + "</td>";
                            firstRow = false;
                        }

                        html += "<td>" + period + "</td>" +
                                "<td><button onclick='location.href='designedSubmitQuarterlyReport.aspx?id =" + interventionId + "'>Submit</button></td>" +
                                "</tr>";
                    }
                }

                html += "</table>";


                pane.ContentContainer.Controls.Add(new LiteralControl(html));
            }

            MyAccordion.Panes.Add(pane);
        }
    }
    protected void btnViewOverview123456789_Click(object sender, EventArgs e)
    {
        int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        DataTable interventions = planningDataSet.Tables["Interventions"];

        MyAccordion.Panes.Clear();

        foreach (DataRow subOutcome in subOutcomes.Rows)
        {
            int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
            string subOutcomeName = subOutcome["SubOutcome"].ToString();

            AccordionPane pane = new AccordionPane();
            pane.ID = "Pane_" + subOutcomeId;
            pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

            if (relatedInterventions.Length > 0)
            {
                string html = "<table class='table table-bordered'>" +
                              "<tr style='background-color:#80bf64; color: white;'>" +
                              "<th>Intervention Name</th>" +
                              "<th>Institution</th>" +
                              "<th>Municipality</th>" +
                              "<th>Period</th>" +
                              "<th>Action</th>" +
                              "</tr>";

                foreach (DataRow iRow in relatedInterventions)
                {
                    int interventionId = Convert.ToInt32(iRow["InterventionID"]);
                    string interventionName = iRow["InterventionName"].ToString();
                    string institution = iRow["ImplementationInstitution"].ToString();
                    string municipality = iRow["PrimaryMunicipality"].ToString();
                    string period = iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"];

                    html += "<tr>" +
                            "<td><a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "'an style='text-decoration:underline; font-weight:bold;'><span>" + interventionName + "</span></a></td>" +
                            "<td>" + institution + "</td>" +
                            "<td>" + municipality + "</td>" +
                            "<td>" + period + "</td>" +
                            "<td><button onclick='location.href='designedSubmitQuarterlyReport.aspx?id =" + interventionId + "</td></ tr > ";

                            
                }

                html += "</table>";
                pane.ContentContainer.Controls.Add(new LiteralControl(html));
            }

            MyAccordion.Panes.Add(pane);
        }
    }

















    protected void btn_AddIntervention_Click(object sender, EventArgs e)
    {

        Response.Redirect("pageAddIntervention.aspx?&cls=" + Session["Cluster_ID"].ToString());
    }

























































































    protected void btnViewOverview1_Click_Other(object sender, EventArgs e)
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
                //string html = "<div class='intervention-block'>" +

                //-------------------------------------------------------------------
                //"<a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "' >" +
                //"<p style='font-size:25px;text-decoration:underline;'> " + iRow["InterventionName"] + "</p></a>" +
                //    "<p><strong>Institution:</strong> " + iRow["ImplementationInstitution"] + "</p>" +
                //    "<p><strong>Municipality:</strong> " + iRow["PrimaryMunicipality"] + "</p>" +
                //    "<p><strong>Period:</strong> " + iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</p>" +
                //    "<h6>Indicators</h6>" +
                //    "<table class='table table-bordered'>" +
                //"<tr style='background-color:#80bf64; color: white;'><th>Name</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th></tr>";

                //-------------------------------------------------------------------

                string html = "<div class='intervention-block'>" +
    "<table class='table table-bordered'>" +
    "<tr><th colspan='2'><a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "'25px;text-decoration:underline;'>" + iRow["InterventionName"] + "</span></a></th></tr>" +
    "<tr><td><strong>Institution:</strong></td><td>" + iRow["ImplementationInstitution"] + "</td></tr>" +
    "<tr><td><strong>Municipality:</strong></td><td>" + iRow["PrimaryMunicipality"] + "</td></tr>" +
    "<tr><td><strong>Period:</strong></td><td>" + iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</td></tr>" +
    "</table>";

                //-------------------------------------------------------------------













            html +=    "<h6>Indicators</h6>" +
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


                html += "</table>";


                html +=

                    "<div style='margin-top:10px;'>" +
"<asp:button  class='my-button' onclick=" + "location.href='designedSubmitQuarterlyReport.aspx?id=" + interventionId + "' " +
"style='padding:10px 20px; font-size:16px; background-color:#b3777b; color:white;'>Submit New Report</asp:button></div>"










+ "</div><hr/>";









                pane.ContentContainer.Controls.Add(new LiteralControl(html));
            }

            MyAccordion.Panes.Add(pane);
        }
        //btnViewOverview1_Click(sender, e);
    }


































































































































    // this 1 works perfectly, the above is just for aditional features

    protected void btnViewOverview1_Working_Main_Click(object sender, EventArgs e)
    {
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
//            pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

//            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

//            foreach (DataRow iRow in relatedInterventions)
//            {
//                int interventionId = Convert.ToInt32(iRow["InterventionID"]);
//                //string html = "<div class='intervention-block'>" +


//                //"<a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "' >" +
//                //"<p style='font-size:25px;text-decoration:underline;'> " + iRow["InterventionName"] + "</p></a>" +
//                //    "<p><strong>Institution:</strong> " + iRow["ImplementationInstitution"] + "</p>" +
//                //    "<p><strong>Municipality:</strong> " + iRow["PrimaryMunicipality"] + "</p>" +
//                //    "<p><strong>Period:</strong> " + iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</p>" +
//                //    "<h6>Indicators</h6>" +
//                //    "<table class='table table-bordered'>" +
//                //"<tr style='background-color:#80bf64; color: white;'><th>Name</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th></tr>";

//                string html = "<div class='intervention-block'>" +
//    "<table class='table table-bordered'>" +
//    "<tr><th colspan='2'><a href='pageInterventionsDirectDetail.aspx?id=" + interventionId + "'25px;text-decoration:underline;'>" + iRow["InterventionName"] + "</span></a></th></tr>" +
//    "<tr><td><strong>Institution:</strong></td><td>" + iRow["ImplementationInstitution"] + "</td></tr>" +
//    "<tr><td><strong>Municipality:</strong></td><td>" + iRow["PrimaryMunicipality"] + "</td></tr>" +
//    "<tr><td><strong>Period:</strong></td><td>" + iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"] + "</td></tr>" +
//    "</table>";

//                html += "<h6>Indicators</h6>" +
//                    "<table class='table table-bordered'>" +
//                "<tr style='background-color:#80bf64; color: white;'><th>Name</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th></tr>";








//                DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
//                foreach (DataRow ind in relatedIndicators)
//                {
//                    html += "<tr><td>" + ind["OutcomeIndicator"] + "</td><td>" + ind["IndicatorType"] + "</td><td>" +
//                            ind["UnitOfMeasure"] + "</td><td>" + ind["BaselineValue"] + " (" + ind["BaselineYear"] + ")</td><td>" +
//                            ind["TargetValue"] + " (" + ind["TargetYear"] + ")</td></tr>";
//                }

//                html += "</table><h6>Budgets</h6><table class='table table-bordered'>" +
//                        "<tr style='background-color:#80bf64; color: white;'><th>Year</th><th>Annual Budget</th><th>Term Budget</th></tr>";







//                DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);
//                foreach (DataRow bud in relatedBudgets)
//                {
//                    decimal annual = bud["AnnualBudget"] != DBNull.Value ? Convert.ToDecimal(bud["AnnualBudget"]) : 0;
//                    decimal term = bud["TermBudget"] != DBNull.Value ? Convert.ToDecimal(bud["TermBudget"]) : 0;

//                    html += "<tr><td>" + bud["FinancialYear"] + "</td><td>R" + annual.ToString("N2") + "</td><td>R" + term.ToString("N2") + "</td></tr>";



//                }


//                html += "</table>";


//                html +=

//                    "<div style='margin-top:10px;'>" +
//"<asp:button  class='my-button' onclick=" + "location.href='designedSubmitQuarterlyReport.aspx?id=" + interventionId + "' " +
//"style='padding:10px 20px; font-size:16px; background-color:#b3777b; color:white;'>Submit New Report</asp:button></div>"










//+ "</div><hr/>";









//                pane.ContentContainer.Controls.Add(new LiteralControl(html));
//            }

//            MyAccordion.Panes.Add(pane);
//        }
//        //btnViewOverview1_Click(sender, e);
    }









































































    protected void btnViewOverview111_Click(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];

        //List<SubOutcome> subOutcomeList = new List<SubOutcome>();

        //foreach (DataRow subOutcomeRow in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcomeRow["SubOutcomeId"]);
        //    string subOutcomeName = subOutcomeRow["SubOutcome"].ToString();

        //    List<Intervention3> interventionList = new List<Intervention3>();
        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"].ToString();
        //        string municipality = iRow["PrimaryMunicipality"].ToString();
        //        string startYear = iRow["InterventionStartYear"].ToString();
        //        string endYear = iRow["InterventionEndYear"].ToString();

        //        List<Indicator> indicatorList = new List<Indicator>();
        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);

        //        foreach (DataRow ind in relatedIndicators)
        //        {
        //            indicatorList.Add(new Indicator
        //            {
        //                OutcomeIndicator = ind["OutcomeIndicator"].ToString(),
        //                IndicatorType = ind["IndicatorType"].ToString(),
        //                UnitOfMeasure = ind["UnitOfMeasure"].ToString(),
        //                BaselineValue = ind["BaselineValue"].ToString(),
        //                BaselineYear = ind["BaselineYear"].ToString(),
        //                TargetValue = ind["TargetValue"].ToString(),
        //                TargetYear = ind["TargetYear"].ToString()
        //            });
        //        }

        //        interventionList.Add(new Intervention3
        //        {
        //            InterventionID = interventionId,
        //            InterventionName = interventionName,
        //            ImplementationInstitution = institution,
        //            PrimaryMunicipality = municipality,
        //            InterventionStartYear = startYear,
        //            InterventionEndYear = endYear,
        //            Indicators = indicatorList
        //        });
        //    }

        //    subOutcomeList.Add(new SubOutcome
        //    {
        //        SubOutcome4 = subOutcomeName,
        //        Interventions = interventionList
        //    });
        //}

        //rptSubOutcomes.DataSource = subOutcomeList;
        //rptSubOutcomes.DataBind();
    }






















    protected void btnViewOverview11_Click(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];

        //MyAccordion.Panes.Clear();

        //foreach (DataRow subOutcome in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //    string subOutcomeName = subOutcome["SubOutcome"].ToString();

        //    AccordionPane pane = new AccordionPane();
        //    pane.ID = "Pane_" + subOutcomeId;
        //    pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    string html = "<table class='table table-bordered'>" +
        //                  "<thead><tr style='background-color:#80bf64; color: white;'>" +
        //                  "<th>Intervention</th><th>Indicator</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th><th>Institution</th><th>Municipality</th><th>Period</th></tr></thead><tbody>";

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"].ToString();
        //        string municipality = iRow["PrimaryMunicipality"].ToString();
        //        string period = iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"];

        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
        //        int rowspan = relatedIndicators.Length > 0 ? relatedIndicators.Length : 1;

        //        for (int i = 0; i < rowspan; i++)
        //        {
        //            html += "<tr>";

        //            if (i == 0)
        //            {
        //                html += "<td rowspan='" + rowspan + "' style='text-align:center; vertical-align:middle;'>" +
        //                        "<a href='pageInterventionsDirectDetail" +
        //                        ".aspx?id=" + interventionId +
        //                        "<span style=" + "'" + "font-size:20px; text-decoration:underline;'>" +
        //                        HttpUtility.HtmlEncode(interventionName) +
        //                        "</span></a></td>";


        //                //html += "<td rowspan='" + rowspan + "' class='intervention-cell'>" +
        //                //        "pageInterventionsDirectDetail.aspx?id=" + interventionId +
        //                //        "<span class='intervention-link'>" +
        //                //        HttpUtility.HtmlEncode(interventionName) +
        //                //        "</span></a></td>";








        //            }

        //            if (relatedIndicators.Length > 0)
        //            {
        //                DataRow ind = relatedIndicators[i];
        //                string indicatorName = ind["OutcomeIndicator"].ToString();
        //                string type = ind["IndicatorType"].ToString();
        //                string unit = ind["UnitOfMeasure"].ToString();
        //                string baseline = ind["BaselineValue"] + " (" + ind["BaselineYear"] + ")";
        //                string target = ind["TargetValue"] + " (" + ind["TargetYear"] + ")";

        //                html += "<td>" + (string.IsNullOrWhiteSpace(indicatorName) ? "&nbsp;" : HttpUtility.HtmlEncode(indicatorName)) + "</td>" +
        //                        "<td>" + (string.IsNullOrWhiteSpace(type) ? "&nbsp;" : HttpUtility.HtmlEncode(type)) + "</td>" +
        //                        "<td>" + (string.IsNullOrWhiteSpace(unit) ? "&nbsp;" : HttpUtility.HtmlEncode(unit)) + "</td>" +
        //                        "<td>" + (string.IsNullOrWhiteSpace(baseline) ? "&nbsp;" : baseline) + "</td>" +
        //                        "<td>" + (string.IsNullOrWhiteSpace(target) ? "&nbsp;" : target) + "</td>";
        //            }
        //            else
        //            {
        //                html += "<td colspan='5'>&nbsp;</td>";
        //            }

        //            if (i == 0)
        //            {
        //                html += "<td rowspan='" + rowspan + "' style='text-align:center; vertical-align:middle;'>" +
        //                        (string.IsNullOrWhiteSpace(institution) ? "&nbsp;" : HttpUtility.HtmlEncode(institution)) +
        //                        "</td>";

        //                html += "<td rowspan='" + rowspan + "' style='text-align:center; vertical-align:middle;'>" +
        //                        (string.IsNullOrWhiteSpace(municipality) ? "&nbsp;" : HttpUtility.HtmlEncode(municipality)) +
        //                        "</td>";


                        
        //            }


        //            //html += "<td>" + (string.IsNullOrWhiteSpace(institution) ? "&nbsp;" : HttpUtility.HtmlEncode(institution)) + "</td>" +
        //            //        "<td>" + (string.IsNullOrWhiteSpace(municipality) ? "&nbsp;" : HttpUtility.HtmlEncode(municipality)) + "</td>" +
        //            //        "<td>" + (string.IsNullOrWhiteSpace(period) ? "&nbsp;" : period) + "</td>";

        //            html += //"<td>" + (string.IsNullOrWhiteSpace(institution) ? "&nbsp;" : HttpUtility.HtmlEncode(institution)) + "</td>" +
        //                    //"<td>" + (string.IsNullOrWhiteSpace(municipality) ? "&nbsp;" : HttpUtility.HtmlEncode(municipality)) + "</td>" +
        //                    "<td>" + (string.IsNullOrWhiteSpace(period) ? "" : period) + "</td>";

        //            html += "</tr>";
        //        }
        //    }

        //    html += "</tbody></table>";
        //    pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //    MyAccordion.Panes.Add(pane);
        //}
    }




    protected void btnViewOverviewEish_Click(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];
        //DataTable budgets = planningDataSet.Tables["Budgets"];

        //MyAccordion.Panes.Clear();

        //foreach (DataRow subOutcome in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //    string subOutcomeName = subOutcome["SubOutcome"].ToString();

        //    AccordionPane pane = new AccordionPane();
        //    pane.ID = "Pane_" + subOutcomeId;
        //    pane.HeaderContainer.Controls.Add(new LiteralControl("<h4>" + subOutcomeName + "</h4>"));

        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    string html = "<table class='table table-bordered'>" +
        //                  "<thead><tr style='background-color:#80bf64; color: white;'>" +
        //                  "<th>Intervention</th><th>Indicator</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th><th>Institution</th><th>Municipality</th><th>Period</th></tr></thead><tbody>";

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"].ToString();
        //        string municipality = iRow["PrimaryMunicipality"].ToString();
        //        string period = iRow["InterventionStartYear"] + " - " + iRow["InterventionEndYear"];

        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);

        //        if (relatedIndicators.Length == 0)
        //        {
        //            html += "<tr>" +
        //                    "<td>pageInterventionsDirectDetail.aspx?id=" + 
        //                    "<p style='font-size:25px;text-decoration:underline;'>" + interventionName + "</p></a></td>" +
        //                    "<td colspan='4'>No indicators found</td>" +
        //                    "<td>" + institution + "</td>" +
        //                    "<td>" + municipality + "</td>" +
        //                    "<td>" + period + "</td>" +
        //                    "</tr>";
        //        }
        //        else
        //        {
        //            foreach (DataRow ind in relatedIndicators)
        //            {
        //                int indicatorId = Convert.ToInt32(ind["IndicatorID"]);
        //                string indicatorName = "<ageIndicatorDetail.aspx?id=" +
        //                                       "<p style='font-size:18px;text-decoration:underline;'>" + ind["OutcomeIndicator"] + "</p></a>";
        //                string type = ind["IndicatorType"].ToString();
        //                string unit = ind["UnitOfMeasure"].ToString();
        //                string baseline = ind["BaselineValue"] + " (" + ind["BaselineYear"] + ")";
        //                string target = ind["TargetValue"] + " (" + ind["TargetYear"] + ")";

        //                html += "<tr>" +
        //                        "<td><a href='pageInterventionsDirectDetail.aspx"+ 
        //                        "<p style = 'font-size:25px;text-decoration:underline;' > " + interventionName + " </ p ></ a ></ td > " +
        //                        "<td>" + indicatorName + "</td>" +
        //                    "<td>" + type + "</td>" +
        //                    "<td>" + unit + "</td>" +
        //                    "<td>" + baseline + "</td>" +
        //                    "<td>" + target + "</td>" +
        //                    "<td>" + institution + "</td>" +
        //                    "<td>" + municipality + "</td>" +
        //                    "<td>" + period + "</td>" +
        //                    "</tr>";
        //            }
        //        }
        //    }

        //    html += "</tbody></table>";
        //    pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //    MyAccordion.Panes.Add(pane);
        //}
    }





    // another 1 below


        //protected void btnViewOverview1_Click(object sender, EventArgs e)
        //{
        //    int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //    int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //    int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //    int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //    cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //    planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //    DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //    DataTable interventions = planningDataSet.Tables["Interventions"];
        //    DataTable indicators = planningDataSet.Tables["Indicators"];
        //    DataTable budgets = planningDataSet.Tables["Budgets"];

        //    MyAccordion.Panes.Clear();

        //    foreach (DataRow subOutcome in subOutcomes.Rows)
        //    {
        //        int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //        string subOutcomeName = subOutcome["SubOutcome"].ToString();

        //        AccordionPane pane = new AccordionPane();
        //        pane.ID = "Pane_" + subOutcomeId;
        //        pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

        //        DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //        string html = "<table border='1'><tr style='background-color:#80bf64; color: white;'><th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th><th>Implementation Institution</th><th>Intervention</th><th>Baseline</th><th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

        //        string prevOutcome = "", prevIndicator = "", prevType = "", prevInstitution = "";
        //        int outcomeSpan = 1, indicatorSpan = 1, typeSpan = 1, institutionSpan = 1;

        //        foreach (DataRow iRow in relatedInterventions)
        //        {
        //            int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //            string interventionName = iRow["InterventionName"].ToString();
        //            string institution = iRow["ImplementationInstitution"].ToString();
        //            string spatialRef = iRow["SpatialReference"].ToString();

        //            DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
        //            DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

        //            foreach (DataRow ind in relatedIndicators)
        //            {
        //                string outcome = subOutcomeName;
        //                string indicator = ind["OutcomeIndicator"].ToString();
        //                string type = ind["IndicatorType"].ToString();
        //                string baseline = ind["BaselineValue"].ToString();
        //                string target2030 = ind["Target2030_TermTarget"].ToString();
        //                string targetValue = ind["TargetValue"].ToString();

        //                decimal annual = 0;
        //                if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
        //                    annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

        //                html += "<tr>";
        //                html += (outcome != prevOutcome ? "<td rowspan='1'>" + outcome + "</td>" : "<td></td>");
        //                html += (indicator != prevIndicator ? "<td rowspan='1'>" + indicator + "</td>" : "<td></td>");
        //                html += (type != prevType ? "<td rowspan='1'>" + type + "</td>" : "<td></td>");
        //                html += (institution != prevInstitution ? "<td rowspan='1'>" + institution + "</td>" : "<td></td>");
        //                html += "<td>" + interventionName + "</td><td>" + baseline + "</td><td>" + target2030 + "</td><td>R" + annual.ToString("N2") + "</td><td>" + targetValue + "</td><td>" + spatialRef + "</td>";
        //                html += "</tr>";

        //                prevOutcome = outcome;
        //                prevIndicator = indicator;
        //                prevType = type;
        //                prevInstitution = institution;
        //            }
        //        }

        //        html += "</table>";
        //        pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //        MyAccordion.Panes.Add(pane);
        //    }
        //}




    protected void btnViewOverview3_Click(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];
        //DataTable budgets = planningDataSet.Tables["Budgets"];

        //MyAccordion.Panes.Clear();

        //foreach (DataRow subOutcome in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //    string subOutcomeName = subOutcome["SubOutcome"] == DBNull.Value ? "" : subOutcome["SubOutcome"].ToString();

        //    AccordionPane pane = new AccordionPane();
        //    pane.ID = "Pane_" + subOutcomeId;
        //    pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    string html = "<style>" +
        //                  "table { width: 100%; border-collapse: collapse; }" +
        //                  "th, td { border: 1px solid #ccc; padding: 8px; text-align: left; vertical-align: top; }" +
        //                  "th { background-color: #80bf64; color: white; }" +
        //                  "tr:nth-child(even) { background-color: #f9f9f9; }" +
        //                  "td[rowspan] { background-color: #e8f5e9; text-align: center; vertical-align: middle; }" +
        //                  "</style>";

        //    html += "<table><tr>" +
        //            "<th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th>" +
        //            "<th>Implementation Institution</th><th>Intervention</th><th>Baseline</th>" +
        //            "<th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"] == DBNull.Value ? "" : iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"] == DBNull.Value ? "" : iRow["ImplementationInstitution"].ToString();
        //        string spatialRef = iRow["SpatialReference"] == DBNull.Value ? "" : iRow["SpatialReference"].ToString();

        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
        //        DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

        //        foreach (DataRow ind in relatedIndicators)
        //        {
        //            string indicator = ind["OutcomeIndicator"] == DBNull.Value ? "" : ind["OutcomeIndicator"].ToString();
        //            string type = ind["IndicatorType"] == DBNull.Value ? "" : ind["IndicatorType"].ToString();
        //            string baseline = ind["BaselineValue"] == DBNull.Value ? "" : ind["BaselineValue"].ToString();
        //            string target2030 = ind["Target2030_TermTarget"] == DBNull.Value ? "" : ind["Target2030_TermTarget"].ToString();
        //            string targetValue = ind["TargetValue"] == DBNull.Value ? "" : ind["TargetValue"].ToString();

        //            decimal annual = 0;
        //            if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
        //                annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

        //            Dictionary<string, object> row = new Dictionary<string, object>();
        //            row["Outcome"] = subOutcomeName;
        //            row["Indicator"] = indicator;
        //            row["Type"] = type;
        //            row["Institution"] = institution;
        //            row["Intervention"] = interventionName;
        //            row["Baseline"] = baseline;
        //            row["Target2030"] = target2030;
        //            row["Annual"] = annual;
        //            row["TargetValue"] = targetValue;
        //            row["SpatialRef"] = spatialRef;

        //            rows.Add(row);
        //        }
        //    }

        //    // Count rowspans for Outcome and Institution
        //    int outcomeRowspan = rows.Count;
        //    Dictionary<string, int> institutionCounts = new Dictionary<string, int>();
        //    foreach (Dictionary<string, object> row in rows)
        //    {
        //        string institution = row["Institution"].ToString();
        //        if (!institutionCounts.ContainsKey(institution)) institutionCounts[institution] = 0;
        //        institutionCounts[institution]++;
        //    }

        //    HashSet<string> renderedInstitutions = new HashSet<string>();
        //    bool outcomeRendered = false;

        //    foreach (Dictionary<string, object> row in rows)
        //    {
        //        string outcome = row["Outcome"].ToString();
        //        string indicator = row["Indicator"].ToString();
        //        string type = row["Type"].ToString();
        //        string institution = row["Institution"].ToString();
        //        string intervention = row["Intervention"].ToString();
        //        string baseline = row["Baseline"].ToString();
        //        string target2030 = row["Target2030"].ToString();
        //        string targetValue = row["TargetValue"].ToString();
        //        string spatialRef = row["SpatialRef"].ToString();
        //        decimal annual = Convert.ToDecimal(row["Annual"]);

        //        html += "<tr>";

        //        // Merge Desired Outcome once
        //        if (!outcomeRendered)
        //        {
        //            html += "<td rowspan='" + outcomeRowspan + "'>" + outcome + "</td>";
        //            outcomeRendered = true;
        //        }

        //        // Render other columns
        //        html += "<td>" + indicator + "</td>";
        //        html += "<td>" + type + "</td>";

        //        // Merge Implementation Institution
        //        if (!renderedInstitutions.Contains(institution))
        //        {
        //            html += "<td rowspan='" + institutionCounts[institution] + "'>" + institution + "</td>";
        //            renderedInstitutions.Add(institution);
        //        }

        //        html += "<td>" + intervention + "</td>";
        //        html += "<td>" + baseline + "</td>";
        //        html += "<td>" + target2030 + "</td>";
        //        html += "<td>R" + annual.ToString("N2") + "</td>";
        //        html += "<td>" + targetValue + "</td>";
        //        html += "<td>" + spatialRef + "</td>";

        //        html += "</tr>";
        //    }

        //    html += "</table>";
        //    pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //    MyAccordion.Panes.Add(pane);
        //}
    }


    protected void btnViewOverview1_Click_worksForOneColumn(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];
        //DataTable budgets = planningDataSet.Tables["Budgets"];

        //MyAccordion.Panes.Clear();

        //foreach (DataRow subOutcome in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //    string subOutcomeName = subOutcome["SubOutcome"] == DBNull.Value ? "" : subOutcome["SubOutcome"].ToString();

        //    AccordionPane pane = new AccordionPane();
        //    pane.ID = "Pane_" + subOutcomeId;
        //    pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    string html = "<style>" +
        //                  "table { width: 100%; border-collapse: collapse; }" +
        //                  "th, td { border: 1px solid #ccc; padding: 8px; text-align: left; vertical-align: top; }" +
        //                  "th { background-color: #80bf64; color: white; }" +
        //                  "tr:nth-child(even) { background-color: #f9f9f9; }" +
        //                  "td[rowspan] { background-color: #e8f5e9; text-align: center; vertical-align: middle; }" +
        //                  "</style>";

        //    html += "<table><tr>" +
        //            "<th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th>" +
        //            "<th>Implementation Institution</th><th>Intervention</th><th>Baseline</th>" +
        //            "<th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"] == DBNull.Value ? "" : iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"] == DBNull.Value ? "" : iRow["ImplementationInstitution"].ToString();
        //        string spatialRef = iRow["SpatialReference"] == DBNull.Value ? "" : iRow["SpatialReference"].ToString();

        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
        //        DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

        //        foreach (DataRow ind in relatedIndicators)
        //        {
        //            string indicator = ind["OutcomeIndicator"] == DBNull.Value ? "" : ind["OutcomeIndicator"].ToString();
        //            string type = ind["IndicatorType"] == DBNull.Value ? "" : ind["IndicatorType"].ToString();
        //            string baseline = ind["BaselineValue"] == DBNull.Value ? "" : ind["BaselineValue"].ToString();
        //            string target2030 = ind["Target2030_TermTarget"] == DBNull.Value ? "" : ind["Target2030_TermTarget"].ToString();
        //            string targetValue = ind["TargetValue"] == DBNull.Value ? "" : ind["TargetValue"].ToString();

        //            decimal annual = 0;
        //            if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
        //                annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

        //            Dictionary<string, object> row = new Dictionary<string, object>();
        //            row["Outcome"] = subOutcomeName;
        //            row["Indicator"] = indicator;
        //            row["Type"] = type;
        //            row["Institution"] = institution;
        //            row["Intervention"] = interventionName;
        //            row["Baseline"] = baseline;
        //            row["Target2030"] = target2030;
        //            row["Annual"] = annual;
        //            row["TargetValue"] = targetValue;
        //            row["SpatialRef"] = spatialRef;

        //            rows.Add(row);
        //        }
        //    }

        //    // Count how many rows to merge for Desired Outcome
        //    int outcomeRowspan = rows.Count;
        //    bool outcomeRendered = false;

        //    foreach (Dictionary<string, object> row in rows)
        //    {
        //        string outcome = row["Outcome"].ToString();
        //        string indicator = row["Indicator"].ToString();
        //        string type = row["Type"].ToString();
        //        string institution = row["Institution"].ToString();
        //        string intervention = row["Intervention"].ToString();
        //        string baseline = row["Baseline"].ToString();
        //        string target2030 = row["Target2030"].ToString();
        //        string targetValue = row["TargetValue"].ToString();
        //        string spatialRef = row["SpatialRef"].ToString();
        //        decimal annual = Convert.ToDecimal(row["Annual"]);

        //        html += "<tr>";

        //        // Merge only Desired Outcome
        //        if (!outcomeRendered)
        //        {
        //            html += "<td rowspan='" + outcomeRowspan + "'>" + outcome + "</td>";
        //            outcomeRendered = true;
        //        }

        //        // Render all other columns normally
        //        html += "<td>" + indicator + "</td>";
        //        html += "<td>" + type + "</td>";
        //        html += "<td>" + institution + "</td>";
        //        html += "<td>" + intervention + "</td>";
        //        html += "<td>" + baseline + "</td>";
        //        html += "<td>" + target2030 + "</td>";
        //        html += "<td>R" + annual.ToString("N2") + "</td>";
        //        html += "<td>" + targetValue + "</td>";
        //        html += "<td>" + spatialRef + "</td>";

        //        html += "</tr>";
        //    }

        //    html += "</table>";
        //    pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //    MyAccordion.Panes.Add(pane);
        //}
    }


    protected void btnViewOverview1_Click_worksforAllColumns(object sender, EventArgs e)
    {
        //int wgId = Convert.ToInt32(ddlWorkGroups.SelectedValue);
        //int priorityId = Convert.ToInt32(ddlPMTDPPriorities.SelectedValue);
        //int fyId = Convert.ToInt32(ddlFinancialYears.SelectedValue);
        //int clusterId = Convert.ToInt32(Session["Cluster_ID"]);

        //cls_PlanningOverviewRepository_3 repo = new cls_PlanningOverviewRepository_3();
        //planningDataSet = repo.GetPlanningOverviewBySubOutcome(wgId, priorityId, fyId, clusterId);

        //DataTable subOutcomes = planningDataSet.Tables["SubOutcomes"];
        //DataTable interventions = planningDataSet.Tables["Interventions"];
        //DataTable indicators = planningDataSet.Tables["Indicators"];
        //DataTable budgets = planningDataSet.Tables["Budgets"];

        //MyAccordion.Panes.Clear();

        //foreach (DataRow subOutcome in subOutcomes.Rows)
        //{
        //    int subOutcomeId = Convert.ToInt32(subOutcome["SubOutcomeId"]);
        //    string subOutcomeName = subOutcome["SubOutcome"] == DBNull.Value ? "" : subOutcome["SubOutcome"].ToString();

        //    AccordionPane pane = new AccordionPane();
        //    pane.ID = "Pane_" + subOutcomeId;
        //    pane.HeaderContainer.Controls.Add(new LiteralControl(subOutcomeName));

        //    DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);

        //    string html = "<style>" +
        //                  "table { width: 100%; border-collapse: collapse; }" +
        //                  "th, td { border: 1px solid #ccc; padding: 8px; text-align: left; vertical-align: top; }" +
        //                  "th { background-color: #80bf64; color: white; }" +
        //                  "tr:nth-child(even) { background-color: #f9f9f9; }" +
        //                  "td[rowspan] { background-color: #e8f5e9; text-align: center; vertical-align: middle; }" +
        //                  "</style>";

        //    html += "<table><tr>" +
        //            "<th>Desired Outcome</th><th>Outcome Indicator</th><th>Indicator Type</th>" +
        //            "<th>Implementation Institution</th><th>Intervention</th><th>Baseline</th>" +
        //            "<th>2030 Term Target</th><th>MTEF Budget</th><th>2025/2026 Target</th><th>Spatial Reference</th></tr>";

        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //    foreach (DataRow iRow in relatedInterventions)
        //    {
        //        int interventionId = Convert.ToInt32(iRow["InterventionID"]);
        //        string interventionName = iRow["InterventionName"] == DBNull.Value ? "" : iRow["InterventionName"].ToString();
        //        string institution = iRow["ImplementationInstitution"] == DBNull.Value ? "" : iRow["ImplementationInstitution"].ToString();
        //        string spatialRef = iRow["SpatialReference"] == DBNull.Value ? "" : iRow["SpatialReference"].ToString();

        //        DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
        //        DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);

        //        foreach (DataRow ind in relatedIndicators)
        //        {
        //            string indicator = ind["OutcomeIndicator"] == DBNull.Value ? "" : ind["OutcomeIndicator"].ToString();
        //            string type = ind["IndicatorType"] == DBNull.Value ? "" : ind["IndicatorType"].ToString();
        //            string baseline = ind["BaselineValue"] == DBNull.Value ? "" : ind["BaselineValue"].ToString();
        //            string target2030 = ind["Target2030_TermTarget"] == DBNull.Value ? "" : ind["Target2030_TermTarget"].ToString();
        //            string targetValue = ind["TargetValue"] == DBNull.Value ? "" : ind["TargetValue"].ToString();

        //            decimal annual = 0;
        //            if (relatedBudgets.Length > 0 && relatedBudgets[0]["AnnualBudget"] != DBNull.Value)
        //                annual = Convert.ToDecimal(relatedBudgets[0]["AnnualBudget"]);

        //            Dictionary<string, object> row = new Dictionary<string, object>();
        //            row["Outcome"] = subOutcomeName;
        //            row["Indicator"] = indicator;
        //            row["Type"] = type;
        //            row["Institution"] = institution;
        //            row["Intervention"] = interventionName;
        //            row["Baseline"] = baseline;
        //            row["Target2030"] = target2030;
        //            row["Annual"] = annual;
        //            row["TargetValue"] = targetValue;
        //            row["SpatialRef"] = spatialRef;

        //            rows.Add(row);
        //        }
        //    }

        //    // Count occurrences
        //    Dictionary<string, int> outcomeCounts = new Dictionary<string, int>();
        //    Dictionary<string, int> indicatorCounts = new Dictionary<string, int>();
        //    Dictionary<string, int> typeCounts = new Dictionary<string, int>();
        //    Dictionary<string, int> institutionCounts = new Dictionary<string, int>();

        //    foreach (Dictionary<string, object> row in rows)
        //    {
        //        string outcome = row["Outcome"].ToString();
        //        string indicator = row["Indicator"].ToString();
        //        string type = row["Type"].ToString();
        //        string institution = row["Institution"].ToString();

        //        if (!outcomeCounts.ContainsKey(outcome)) outcomeCounts[outcome] = 0;
        //        outcomeCounts[outcome]++;

        //        if (!indicatorCounts.ContainsKey(indicator)) indicatorCounts[indicator] = 0;
        //        indicatorCounts[indicator]++;

        //        if (!typeCounts.ContainsKey(type)) typeCounts[type] = 0;
        //        typeCounts[type]++;

        //        if (!institutionCounts.ContainsKey(institution)) institutionCounts[institution] = 0;
        //        institutionCounts[institution]++;
        //    }

        //    HashSet<string> renderedOutcomes = new HashSet<string>();
        //    HashSet<string> renderedIndicators = new HashSet<string>();
        //    HashSet<string> renderedTypes = new HashSet<string>();
        //    HashSet<string> renderedInstitutions = new HashSet<string>();

        //    foreach (Dictionary<string, object> row in rows)
        //    {
        //        string outcome = row["Outcome"].ToString();
        //        string indicator = row["Indicator"].ToString();
        //        string type = row["Type"].ToString();
        //        string institution = row["Institution"].ToString();
        //        string intervention = row["Intervention"].ToString();
        //        string baseline = row["Baseline"].ToString();
        //        string target2030 = row["Target2030"].ToString();
        //        string targetValue = row["TargetValue"].ToString();
        //        string spatialRef = row["SpatialRef"].ToString();
        //        decimal annual = Convert.ToDecimal(row["Annual"]);

        //        html += "<tr>";

        //        // Render merged cells only once
        //        if (!renderedOutcomes.Contains(outcome))
        //        {
        //            html += "<td rowspan='" + outcomeCounts[outcome] + "'>" + outcome + "</td>";
        //            renderedOutcomes.Add(outcome);
        //        }

        //        if (!renderedIndicators.Contains(indicator))
        //        {
        //            html += "<td rowspan='" + indicatorCounts[indicator] + "'>" + indicator + "</td>";
        //            renderedIndicators.Add(indicator);
        //        }

        //        if (!renderedTypes.Contains(type))
        //        {
        //            html += "<td rowspan='" + typeCounts[type] + "'>" + type + "</td>";
        //            renderedTypes.Add(type);
        //        }

        //        if (!renderedInstitutions.Contains(institution))
        //        {
        //            html += "<td rowspan='" + institutionCounts[institution] + "'>" + institution + "</td>";
        //            renderedInstitutions.Add(institution);
        //        }

        //        // Always render remaining cells
        //        html += "<td>" + intervention + "</td>";
        //        html += "<td>" + baseline + "</td>";
        //        html += "<td>" + target2030 + "</td>";
        //        html += "<td>R" + annual.ToString("N2") + "</td>";
        //        html += "<td>" + targetValue + "</td>";
        //        html += "<td>" + spatialRef + "</td>";

        //        html += "</tr>";
        //    }

        //    html += "</table>";
        //    pane.ContentContainer.Controls.Add(new LiteralControl(html));
        //    MyAccordion.Panes.Add(pane);
        //}
    }




    
}


