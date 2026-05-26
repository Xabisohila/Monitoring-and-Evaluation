using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using YourProjectName.Data;

public partial class addQuarterlyReport : System.Web.UI.Page
{
    cls_QuarterlyReportDAL_2 dal = new cls_QuarterlyReportDAL_2();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInterventions();
        }
    }

    private void LoadInterventions()
    {
        DataTable dt = dal.GetAllInterventions();
        ddlIntervention.DataSource = dt;
        ddlIntervention.DataTextField = "InterventionName";
        ddlIntervention.DataValueField = "InterventionID";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new ListItem("-- Select Intervention --", "0"));
    }

    protected void ddlIntervention_SelectedIndexChanged(object sender, EventArgs e)
    {
        int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
        if (interventionId > 0)
        {
            DataTable summary = dal.GetLatestReportSummary(interventionId);
            if (summary.Rows.Count > 0)
            {
                DataRow row = summary.Rows[0];
                lblRemainingBudget.Text = "Remaining Budget: " + row["RemainingBudget"].ToString();
                lblDeviation.Text = "Deviation (%): " + row["DeviationPercentage"].ToString();
                lblPlannedValue.Text = "Planned Value: " + row["PlannedValue"].ToString();
                //lblCurrentStatus.Text = "Current Status: " + row["CurrentStatus"].ToString();
                pnlSummary.Visible = true;
            }
            else
            {
                pnlSummary.Visible = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            string financialYear = txtFinancialYear.Text.Trim();
            string quarter = ddlQuarter.SelectedValue;
            decimal actualExpenditure = Convert.ToDecimal(txtActualExpenditure.Text.Trim());
            decimal performanceValue = Convert.ToDecimal(txtPerformanceValue.Text.Trim());
            string deviationExplanation = txtDeviationExplanation.Text.Trim();
            string documentPath = "";

            if (fuDocument.HasFile)
            {
                string fileName = System.IO.Path.GetFileName(fuDocument.FileName);
                string savePath = Server.MapPath("~/UploadedDocuments/") + fileName;
                fuDocument.SaveAs(savePath);
                documentPath = "UploadedDocuments/" + fileName;
            }

            bool success = dal.InsertQuarterlyReport(interventionId, financialYear, quarter, actualExpenditure, performanceValue, deviationExplanation, documentPath);

            lblMessage.Text = success ? "Report saved successfully." : "Error saving report.";
            lblMessage.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }
}
