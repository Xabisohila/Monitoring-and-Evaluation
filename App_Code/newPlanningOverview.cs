using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace YourProjectName.Data // IMPORTANT: Replace YourProjectName
{
    public class newPlanningOverview
    {
        private string connectionString;
       // string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = null;
        public newPlanningOverview()
        {
            // Constructor: Initializes the connection string from Web.config
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Retrieves a comprehensive planning overview based on selected filters.
        /// Returns a DataSet with multiple tables:
        ///  - PMTDP Priority Header
        /// [1] - Programmes of Action (POAs)
        /// [2] - Interventions
        /// [3] - Intervention Indicators
        /// [4] - Intervention Budgets
        /// [5] - Work Groups Lookup (for dropdown)
        /// [6] - PMTDP Priorities Lookup (for dropdown)
        /// [7] - Financial Years Lookup (for dropdown)
        /// </summary>
        public DataSet GetPlanningOverview(int workGroupId, int pmtdpPriorityId, int financialYearId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("new1_SP_GetPlanningOverviewByFilters_2", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WorkGroupID", workGroupId);
                    cmd.Parameters.AddWithValue("@PMTDP_PriorityID", pmtdpPriorityId);
                    cmd.Parameters.AddWithValue("@FinancialYearID", financialYearId);

                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        // Helper methods to get lookup data for dropdowns (can also be extracted from the main SP's result sets)
        // These are provided separately for clarity if you prefer to load them independently,
        // but the main SP already returns these as result sets 5, 6, 7.
        // You can choose to use these separate methods or extract from the main DataSet.

        public DataTable GetAllWorkGroupsLookup()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT WorkingGroupID, WG_Name FROM new1_WorkingGroups ORDER BY WG_Name", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetAllPMTDPPrioritiesLookup()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT PMTDP_PriorityID, PriorityName FROM new1_PMTDP_Priorities ORDER BY PriorityName", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetAllFinancialYearsLookup()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FY_ID, FY_Name FROM new1_FinancialYears ORDER BY FY_Name DESC", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}