using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for cls_QuarterlyReportDAL_2
/// </summary>

public class cls_QuarterlyReportDAL_2
{
    private string connectionString;

    public cls_QuarterlyReportDAL_2()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataTable GetAllInterventions()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT InterventionID, InterventionName FROM new_Interventions ORDER BY InterventionName", conn))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public DataTable GetLatestReportSummary(int interventionId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1
    (SELECT SUM(AnnualBudget) 
     FROM new_Intervention_Budgets 
     WHERE InterventionID = " + interventionId + " )  - SUM(ActualExpenditure) AS RemainingBudget, CASE " +
       " WHEN SUM(PerformancePlannedValue) > 0 THEN " +
           "  ROUND((SUM(PerformancePlannedValue) - SUM(PerformanceActualValue)) * 100.0 / SUM(PerformancePlannedValue), 2) " +
      "   ELSE 0 " +
   "  END AS DeviationPercentage, " +

    " SUM(PerformancePlannedValue) AS PlannedValue " +

" FROM new_QuarterlyReports " +
" WHERE InterventionID = " + interventionId 
        , conn))
        {
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public bool InsertQuarterlyReport(int interventionId, string financialYear, string quarter, decimal actualExpenditure, decimal performanceValue, string deviationExplanation, string documentPath)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO new_QuarterlyReports 
            (InterventionID, FinancialYear, Quarter, ActualExpenditure, PerformanceValue, DeviationExplanation, DocumentPath, CreatedDate)
            VALUES (@InterventionID, @FinancialYear, @Quarter, @ActualExpenditure, @PerformanceValue, @DeviationExplanation, @DocumentPath, GETDATE())
        ", conn))
        {
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
            cmd.Parameters.AddWithValue("@Quarter", quarter);
            cmd.Parameters.AddWithValue("@ActualExpenditure", actualExpenditure);
            cmd.Parameters.AddWithValue("@PerformanceValue", performanceValue);
            cmd.Parameters.AddWithValue("@DeviationExplanation", deviationExplanation);
            cmd.Parameters.AddWithValue("@DocumentPath", documentPath);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
