using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class designSubmitQuarterlyReport : System.Web.UI.Page
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
            SqlCommand cmdIntervention = new SqlCommand("SELECT InterventionID, InterventionName FROM new_Interventions ORDER BY InterventionName", conn);
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
            string uploadedBy = Session["Username"] != null ? Session["Username"].ToString() : string.Empty;
            string filePath = SaveFile(fileUpload);

            // Get SubOutcomeID based on selected InterventionID
            int subOutcomeID = GetSubOutcomeId(interventionId);

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
                new SqlParameter("@SubOutcomeID", subOutcomeID)
            };

            dbHelper db = new dbHelper();
            db.ExecuteStoredProcedure("sp_SubmitQuarterlyReport", parameters);
            //db.ExecuteStoredProcedure("new_SP_SubmitQuarterlyReport", parameters);

            lblMessage.Text = "Report submitted successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }

    /// <summary>
    /// Retrieves the SubOutcomeID for the given InterventionID.
    /// Returns 0 if not found or if value is NULL.
    /// </summary>
    private int GetSubOutcomeId(int interventionId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 SubOutcomeID FROM new_Interventions WHERE InterventionID = @InterventionID", conn))
        {
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            conn.Open();
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                int parsed;
                if (int.TryParse(result.ToString(), out parsed))
                    return parsed;
            }

            return 0;
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
}
