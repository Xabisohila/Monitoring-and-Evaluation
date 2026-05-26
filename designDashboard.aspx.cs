using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class designDashboard : System.Web.UI.Page
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
            pane.HeaderContainer.Controls.Add(new LiteralControl("<h3>" + subOutcomeName + "</h3>"));

            DataRow[] relatedInterventions = interventions.Select("SubOutcomeID = " + subOutcomeId);
            foreach (DataRow iRow in relatedInterventions)
            {
                int interventionId = Convert.ToInt32(iRow["InterventionID"]);

                string html = string.Format(@"
                <div><strong>{0}</strong></div>
                <div>Institution: {1}</div>
                <div>Municipality: {2}</div>
                <div>Period: {3} - {4}</div>
                <h4>Indicators</h4>
                <table>
                    <tr><th>Name</th><th>Type</th><th>Unit</th><th>Baseline</th><th>Target</th></tr>",
                    iRow["InterventionName"],
                    iRow["ImplementationInstitution"],
                    iRow["PrimaryMunicipality"],
                    iRow["InterventionStartYear"],
                    iRow["InterventionEndYear"]
                );

                DataRow[] relatedIndicators = indicators.Select("InterventionID = " + interventionId);
                foreach (DataRow ind in relatedIndicators)
                {
                    html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3} ({4})</td><td>{5} ({6})</td></tr>",
                        ind["OutcomeIndicator"],
                        ind["IndicatorType"],
                        ind["UnitOfMeasure"],
                        ind["BaselineValue"],
                        ind["BaselineYear"],
                        ind["TargetValue"],
                        ind["TargetYear"]
                    );
                }

                html += @"</table><h4>Budgets</h4><table><tr><th>Year</th><th>Annual Budget</th><th>Term Budget</th></tr>";

                DataRow[] relatedBudgets = budgets.Select("InterventionID = " + interventionId);
                foreach (DataRow bud in relatedBudgets)
                {
                    decimal annual = bud["AnnualBudget"] != DBNull.Value ? Convert.ToDecimal(bud["AnnualBudget"]) : 0;
                    decimal term = bud["TermBudget"] != DBNull.Value ? Convert.ToDecimal(bud["TermBudget"]) : 0;

                    html += string.Format("<tr><td>{0}</td><td>R{1:N2}</td><td>R{2:N2}</td></tr>",
                        bud["FinancialYear"],
                        annual,
                        term
                    );
                }

                html += "</table>";
                pane.ContentContainer.Controls.Add(new LiteralControl(html));

            }
            MyAccordion.Panes.Add(pane);
        }
    }
}