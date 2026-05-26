using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for cls_QuarterlyReportDAL
/// </summary>
public class cls_QuarterlyReportDAL
{
    private string connectionString;
    public cls_QuarterlyReportDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    protected object ExecuteScalar(string spName, params SqlParameter[] parameters) // S_R55
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure; // S_R88, S_R75
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected object ExecuteScalar(string queryOrSpName, CommandType commandType, params SqlParameter[] parameters) // S_R75, S_R88
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] parameters)
    {
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }
        }

        return ds;
    }

    protected DataTable ExecuteDataTable(string queryOrSpName, params SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.Text; // Change to StoredProcedure if needed
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }


    /// <summary>
    /// Creates a new Quarterly Report record.
    /// </summary>
    /// <returns>The ID of the newly created Report, or 0 if creation failed.</returns>
    public int CreateQuarterlyReport(
        int interventionId,
        int fyId,
        string quarter,
        decimal actualExpenditure,
        decimal plannedExpenditure,
        decimal? performanceActualValue, // Nullable
        decimal? performancePlannedValue, // Nullable
        string deviationExplanation, // Nullable
        DateTime reportingDate)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@InterventionID", interventionId),
            new SqlParameter("@FY_ID", fyId),
            new SqlParameter("@Quarter", quarter),
            new SqlParameter("@ActualExpenditure", actualExpenditure),
            new SqlParameter("@PlannedExpenditure", plannedExpenditure),
            new SqlParameter("@PerformanceActualValue", (object)performanceActualValue?? DBNull.Value),
            new SqlParameter("@PerformancePlannedValue", (object)performancePlannedValue?? DBNull.Value),
            new SqlParameter("@DeviationExplanation", (object)deviationExplanation?? DBNull.Value),
            new SqlParameter("@ReportingDate", reportingDate)
        };

        object result = ExecuteScalar("new_SP_CreateQuarterlyReport", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }

    // --- Lookup Methods for Dropdowns ---
    public DataTable GetAllInterventionsLookup()
    {
        return ExecuteDataTable("SELECT InterventionID, InterventionName FROM new_Interventions ORDER BY InterventionName");
    }
    public DataTable GetAllFinancialYearsLookup()
    {
        return ExecuteDataTable("SELECT FY_ID, FY_Name FROM new_FinancialYears ORDER BY FY_Name DESC");
    }

    public DataTable GetAllQuartersLookup()
    {
        // Quarters are typically static, so we can generate them programmatically
        DataTable dt = new DataTable();
        dt.Columns.Add("QuarterValue", typeof(string));
        dt.Columns.Add("QuarterText", typeof(string));
        dt.Rows.Add("Q1", "Quarter 1 (Apr - Jun)");
        dt.Rows.Add("Q2", "Quarter 2 (July - Sep)"); // Assuming EC financial year starts July
        dt.Rows.Add("Q3", "Quarter 3 (Oct - Dec)");
        dt.Rows.Add("Q4", "Quarter 4 (Jan - Mar)");
        
        return dt;
    }

    public DataTable GetAllQuartersLookup1()
    {
        // Quarters are typically static, so we can generate them programmatically
        DataTable dt = new DataTable();
        dt.Columns.Add("QuarterValue", typeof(string));
        dt.Columns.Add("QuarterText", typeof(string));
        dt.Rows.Add("Q1", "Quarter 1 (Apr - Jun)");
        dt.Rows.Add("Q2", "Quarter 2 (July - Sep)"); // Assuming EC financial year starts July
        dt.Rows.Add("Q3", "Quarter 3 (Oct - Dec)");
        dt.Rows.Add("Q4", "Quarter 4 (Jan - Mar)");

        return dt;
    }

}




































namespace YourProjectName.Data // IMPORTANT: Replace YourProjectName
{
    public class QuarterlyReportDAL
    {
        



 

        

        

        
    }
}
