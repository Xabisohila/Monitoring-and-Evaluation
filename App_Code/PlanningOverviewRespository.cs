using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Intervention
/// </summary>
public class PlanningOverviewRepository
{
    int mvarInterventionID;


    string conn1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlConnection connection = null;

    public int InterventionID { get { return mvarInterventionID; } set { mvarInterventionID = value; } }

    public PlanningOverviewRepository()
    {

    }



    public DataSet GetInterventionDetails(int interventionID)
    {


        try
        {
            using (SqlConnection connection = new SqlConnection(conn1))
            using (SqlCommand cmd = new SqlCommand("new_SP_GetInterventionDetails_2", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InterventionID", SqlDbType.Int).Value = interventionID;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dsData = new DataSet("InterventionDetails");

                adapter.Fill(dsData);

                // Optional: Name the tables
                if (dsData.Tables.Count > 0) dsData.Tables[0].TableName = "Intervention";
                if (dsData.Tables.Count > 1) dsData.Tables[1].TableName = "Indicators";
                if (dsData.Tables.Count > 2) dsData.Tables[2].TableName = "Budgets";
                if (dsData.Tables.Count > 3) dsData.Tables[3].TableName = "Reports";

                return dsData;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database error: " + ex.Message);
        }
    }

    


    
        

        /// <summary>
        /// Retrieves a comprehensive planning overview based on selected filters.
        /// Returns a DataSet with multiple tables:
        ///  - PMTDP Priority Header
        /// [1] - Programmes of Action (POAs)
        /// [2] - Interventions
        /// [3] - Intervention Indicators
        /// [4] - Intervention Budgets
        /// [5] - Work Groups Lookup
        /// [6] - PMTDP Priorities Lookup
        /// [7] - Financial Years Lookup
        /// </summary>
        public DataSet GetPlanningOverview(int workGroupId, int pmtdpPriorityId, int financialYearId, int clusterId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(conn1))
            {
            using (SqlCommand cmd = new SqlCommand("new_SP_GetPlanningOverviewByFilters_3", conn))
            //using (SqlCommand cmd = new SqlCommand("new_SP_GetPlanningOverviewByFilters_5", conn))
            {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WorkGroupID", workGroupId);
                    cmd.Parameters.AddWithValue("@PMTDP_PriorityID", pmtdpPriorityId);
                    cmd.Parameters.AddWithValue("@FinancialYearID", financialYearId);

                    cmd.Parameters.AddWithValue("@ClusterID", clusterId); // New parameter

                conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        // Helper methods to get lookup data for dropdowns (can also be part of the main SP's result sets)
        // These are provided separately for clarity if you prefer to load them independently.
        // However, the main SP already returns these as result sets 6, 7, 8.
        // You can choose to use these separate methods or extract from the main DataSet.

        public DataTable GetAllWorkGroupsLookup()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conn1))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT WorkingGroupID, WG_Name FROM new_WorkingGroups ORDER BY WG_Name", conn))
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

    //new_WorkingGroupLookup for Specific Cluster with institutions

    public DataTable GetAllWorkGroupsLookup_2(int ClusterID)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(conn1))
        {
            using (SqlCommand cmd = new 
                SqlCommand("SELECT WG.WorkingGroupID, WG.WG_Name, WG.WG_Description FROM new_WorkingGroups WG " +
                    "JOIN new_ImplementationInstitutions II ON WG.LeadInstitutionID = II.InstitutionID " +
                    "WHERE II.ClusterID = " + ClusterID  +
                    " ORDER BY WG_Name;", conn))
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
            using (SqlConnection conn = new SqlConnection(conn1))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities ORDER BY PriorityName", conn))
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



    public DataTable GetAllPMTDPPrioritiesLookup_via_Cluster(int ClusterID)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(conn1))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities WHERE ClusterID = " + ClusterID + " ORDER BY PriorityName", conn))
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
            using (SqlConnection conn = new SqlConnection(conn1))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FY_ID, FY_Name FROM new_FinancialYears ORDER BY FY_Name DESC", conn))
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

