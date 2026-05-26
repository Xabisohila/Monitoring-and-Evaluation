using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class designedSubmitQuarterlyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdowns();
        }
    }



    private void BindDropdowns()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Bind Interventions
            SqlCommand cmdIntervention = new SqlCommand("SELECT InterventionID, InterventionName FROM new_Interventions WHERE IsApproved = 1 ORDER BY InterventionName", conn);
            SqlDataAdapter daIntervention = new SqlDataAdapter(cmdIntervention);
            DataTable dtIntervention = new DataTable();
            daIntervention.Fill(dtIntervention);

            ddlIntervention.DataSource = dtIntervention;
            ddlIntervention.DataTextField = "InterventionName";
            ddlIntervention.DataValueField = "InterventionID";
            ddlIntervention.DataBind();
            ddlIntervention.Items.Insert(0, new ListItem("Select Intervention", ""));

            // Bind Financial Years
            SqlCommand cmdFY = new SqlCommand("SELECT FY_ID, FY_Name FROM new_FinancialYears ORDER BY FY_Name DESC", conn);
            SqlDataAdapter daFY = new SqlDataAdapter(cmdFY);
            DataTable dtFY = new DataTable();
            daFY.Fill(dtFY);

            ddlFinancialYear.DataSource = dtFY;
            ddlFinancialYear.DataTextField = "FY_Name";
            ddlFinancialYear.DataValueField = "FY_ID";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.Items.Insert(0, new ListItem("Select Financial Year", ""));
        }
    }

    protected void btnSubmitReport_Click(object sender, EventArgs e)
    {
        try
        {

            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            int fyId = Convert.ToInt32(ddlFinancialYear.SelectedValue); 
            string quarter = ddlQuarter.SelectedValue;
            decimal actualExp = Convert.ToDecimal(txtActualExpenditure.Text);
            decimal plannedExp = Convert.ToDecimal(txtPlannedExpenditure.Text);
            decimal perfActual = Convert.ToDecimal(txtPerformanceActual.Text);
            decimal perfPlanned = Convert.ToDecimal(txtPerformancePlanned.Text);
            string deviation = txtDeviationExplanation.Text;

            //string uploadedBy = Session["Username"].ToString();
            string uploadedBy = Session["dbUserID"].ToString();
            
            string filePath = SaveFile(fileUpload);

            // Get SubOutcomeID for the selected intervention (nullable)
            int? subOutcomeId = GetSubOutcomeId(interventionId);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InterventionID", interventionId),
                new SqlParameter("@FY_ID", fyId),
                new SqlParameter("@Quarter", quarter),
                new SqlParameter("@ActualExpenditure", actualExp),
                new SqlParameter("@PlannedExpenditure", plannedExp),
                new SqlParameter("@PerformanceActualValue", perfActual),
                new SqlParameter("@PerformancePlannedValue", perfPlanned),
                new SqlParameter("@DeviationExplanation", deviation),
                new SqlParameter("@UploadedBy", uploadedBy),
                new SqlParameter("@UploadFilePath", filePath),
                // Add SubOutcomeID parameter; pass DBNull.Value when null
                new SqlParameter("@SubOutcomeID", subOutcomeId.HasValue ? (object)subOutcomeId.Value : DBNull.Value)
            };

            dbHelper db = new dbHelper();
            db.ExecuteStoredProcedure("new_SP_SubmitQuarterlyReport", parameters);

            lblMessage.Text = "Report submitted successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }

    private string SaveFile(FileUpload fileUpload)
    {
        string[] allowedExtensions = { ".pdf", ".docx", ".xlsx", ".jpg", ".png" };
        int maxFileSizeMB = 10;

        if (fileUpload.HasFile)
        {
            string extension = Path.GetExtension(fileUpload.FileName).ToLower();
            int fileSizeMB = fileUpload.PostedFile.ContentLength / (1024 * 1024);

            if (!allowedExtensions.Contains(extension))
                throw new Exception("Unsupported file type.");

            if (fileSizeMB > maxFileSizeMB)
                throw new Exception("File size exceeds 10MB limit.");

            string folderPath = Server.MapPath("~/uploads/interventions/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string uniqueName = Guid.NewGuid().ToString() + extension;
            string fullPath = Path.Combine(folderPath, uniqueName);
            fileUpload.SaveAs(fullPath);

            return "/uploads/interventions/" + uniqueName;
        }

        return string.Empty;
    }






































    
    private void LoadLatestReportSummary(int interventionId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"
            SELECT TOP 1 
                ActualExpenditure, PlannedExpenditure,
                PerformanceActualValue, PerformancePlannedValue,
                DeviationExplanation, ReportingDate,
                UploadedBy, UploadFilePath
            FROM new_QuarterlyReports
            WHERE InterventionID = @InterventionID
            ORDER BY ReportingDate DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                decimal actual = reader.IsDBNull(reader.GetOrdinal("ActualExpenditure")) ? 0 : Convert.ToDecimal(reader["ActualExpenditure"]);
                decimal planned = reader.IsDBNull(reader.GetOrdinal("PlannedExpenditure")) ? 0 : Convert.ToDecimal(reader["PlannedExpenditure"]);
                decimal perfActual = reader.IsDBNull(reader.GetOrdinal("PerformanceActualValue")) ? 0 : Convert.ToDecimal(reader["PerformanceActualValue"]);
                decimal perfPlanned = reader.IsDBNull(reader.GetOrdinal("PerformancePlannedValue")) ? 0 : Convert.ToDecimal(reader["PerformancePlannedValue"]);
                string deviation = reader.IsDBNull(reader.GetOrdinal("DeviationExplanation")) ? "No explanation provided." : reader["DeviationExplanation"].ToString();
                DateTime reportDate = reader.IsDBNull(reader.GetOrdinal("ReportingDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["ReportingDate"]);
                string uploadedBy = reader.IsDBNull(reader.GetOrdinal("UploadedBy")) ? "Unknown" : reader["UploadedBy"].ToString();
                string filePath = reader.IsDBNull(reader.GetOrdinal("UploadFilePath")) ? "#" : reader["UploadFilePath"].ToString();

                decimal remainingBudget = planned - actual;
                decimal deviationPercent = perfPlanned == 0 ? 0 : ((perfActual - perfPlanned) / perfPlanned) * 100;

                string statusText;
                string statusCssClass;
                string colorHex;

                if (Math.Abs(deviationPercent) <= 5)
                {
                    statusText = "- On Track";
                    statusCssClass = "status-ontrack";
                    colorHex = "#2e7d32"; // Green
                }
                else if (Math.Abs(deviationPercent) <= 15)
                {
                    statusText = "- Caution";
                    statusCssClass = "status-caution";
                    colorHex = "#ff9800"; // Orange
                }
                else
                {
                    statusText = "- Off Track";
                    statusCssClass = "status-offtrack";
                    colorHex = "#d32f2f"; // Red
                }

                lblRemainingBudget.Text = "R " + remainingBudget.ToString("N2");
                lblDeviationPercent.Text = deviationPercent.ToString("F2") + "% " + statusText;
                lblDeviationPercent.CssClass = statusCssClass;
                lblDeviationPercent.ForeColor = System.Drawing.ColorTranslator.FromHtml(colorHex);
                lblPerformance.Text = "Planned: " + perfPlanned.ToString("N2") + ", Actual: " + perfActual.ToString("N2");
                lblReportDate.Text = reportDate == DateTime.MinValue ? "N/A" : reportDate.ToString("dd MMM yyyy");
                lblDeviationExplanation.Text = deviation;
                lblUploadedBy.Text = uploadedBy;
                lnkDocument.NavigateUrl = filePath;

                pnlSummary.Visible = true;
            }
            else
            {
                pnlSummary.Visible = false;
            }
        }
    }

    protected void ddlIntervention_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(ddlIntervention.SelectedValue))
        {
            int interventionId = Convert.ToInt32(ddlIntervention.SelectedValue);
            AutoFillPlannedValues(interventionId);
            LoadLatestReportSummary(interventionId); // if you're also showing the summary panel
        }


    }



















    private void AutoFillPlannedValues(int interventionId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"
            SELECT TOP 1 PlannedExpenditure, PerformancePlannedValue
            FROM new_QuarterlyReports
            WHERE InterventionID = @InterventionID
            ORDER BY ReportingDate DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                decimal plannedExpenditure = reader.IsDBNull(reader.GetOrdinal("PlannedExpenditure")) ? 0 : Convert.ToDecimal(reader["PlannedExpenditure"]);
                decimal performancePlanned = reader.IsDBNull(reader.GetOrdinal("PerformancePlannedValue")) ? 0 : Convert.ToDecimal(reader["PerformancePlannedValue"]);

                txtPlannedExpenditure.Text = plannedExpenditure.ToString("N2");
                txtPerformancePlanned.Text = performancePlanned.ToString("N2");
            }
            else
            {
                txtPlannedExpenditure.Text = "0.00";
                txtPerformancePlanned.Text = "0.00";
            }
        }
    }

    private int? GetSubOutcomeId(int interventionId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 SubOutcomeID FROM new_Interventions WHERE InterventionID = @InterventionID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InterventionID", interventionId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return null;
                return Convert.ToInt32(result);
            }
        }
    }
}
