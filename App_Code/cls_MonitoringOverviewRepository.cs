using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_MonitoringOverviewRepository
/// </summary>
public class cls_MonitoringOverviewRepository
{
    private string connectionString;
    private string previousInstitution = string.Empty;

    public cls_MonitoringOverviewRepository()
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
    /// Retrieves a comprehensive monitoring overview based on selected filters.
    /// Returns a DataSet with multiple tables:
    ///  - PMTDP Priority Header
    /// [1] - Programmes of Action (POAs)
    /// [2] - Interventions (with latest actuals and deviations)
    /// [3] - Intervention Indicators (planned vs actual, deviations)
    /// [4] - Intervention Budgets (planned vs actual, deviations)
    /// [5] - Work Groups Lookup
    /// [6] - PMTDP Priorities Lookup
    /// [7] - Financial Years Lookup
    /// </summary>
    public DataSet GetMonitoringOverview(int workGroupId, int pmtdpPriorityId, int financialYearId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@WorkGroupID", workGroupId),
            new SqlParameter("@PMTDP_PriorityID", pmtdpPriorityId),
            new SqlParameter("@FinancialYearID", financialYearId)
        };
        return ExecuteDataSet("new_SP_GetMonitoringOverviewByFilters", parameters);
    }


    // --- Lookup Methods for Dropdowns (can also be extracted from the main SP's result sets) ---
    // These are provided separately for clarity if you prefer to load them independently.
    // The main SP already returns these as result sets 5, 6, 7.

    public DataTable GetAllWorkGroupsLookup()
    {
        return ExecuteDataTable("SELECT WorkingGroupID, WG_Name FROM new_WorkingGroups ORDER BY WG_Name");
    }
    public DataTable GetAllPMTDPPrioritiesLookup()
    {
        return ExecuteDataTable("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities ORDER BY PriorityName");
    }

    public DataTable GetAllFinancialYearsLookup()
    {
        return ExecuteDataTable("SELECT FY_ID, FY_Name FROM new_FinancialYears ORDER BY FY_Name DESC");
    }

    public DataTable GetAllClustersLookup()
    {
        return ExecuteDataTable("SELECT ClusterID, ClusterName, ClusterDescription FROM new_Clusters ORDER BY ClusterID ASC");
    }


    public DataSet GetMonitoringOverview_2(int clusterId, int workGroupId, int priorityId, int financialYearId)
    {
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("new_SP_GetMonitoringOverview_2", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@WorkGroupID", workGroupId);
            cmd.Parameters.AddWithValue("@PMTDP_PriorityID", priorityId);
            cmd.Parameters.AddWithValue("@FinancialYearID", financialYearId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
        }

        return ds;
    }
}

























namespace YourProjectName.Data // IMPORTANT: Replace YourProjectName
{
    public class MonitoringOverviewRepository
    {
       





        

        

        
    }
}