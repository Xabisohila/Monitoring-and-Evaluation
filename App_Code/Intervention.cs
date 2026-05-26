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
public class Intervention
{
    //int mvarInterventionID;
    

    //string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //SqlConnection connection = null;

    //public int InterventionID { get { return mvarInterventionID; } set { mvarInterventionID = value; } }

    //public Intervention()
    //{
        
    //}
    //public DataSet GetInterventionDetails(int interventionID)
    //{
    //    try
    //    {
    //        using (SqlConnection connection = new SqlConnection(conn))
    //        using (SqlCommand cmd = new SqlCommand("new_SP_GetInterventionDetails", connection))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.Add("@InterventionID", SqlDbType.Int).Value = interventionID;

    //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //            DataSet dsData = new DataSet("InterventionDetails");

    //            adapter.Fill(dsData);

    //            // Optional: Name the tables
    //            if (dsData.Tables.Count > 0) dsData.Tables[0].TableName = "Intervention";
    //            if (dsData.Tables.Count > 1) dsData.Tables[1].TableName = "Indicators";
    //            if (dsData.Tables.Count > 2) dsData.Tables[2].TableName = "Budgets";
    //            if (dsData.Tables.Count > 3) dsData.Tables[3].TableName = "Reports";

    //            return dsData;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Database error: " + ex.Message);
    //    }
    //}





























































































    //public int CreateIntervention(
    //        string interventionName,
    //        string interventionDescription,
    //        int poaId,
    //        int leadInstitutionId,
    //        int? workingGroupId, // Nullable int for optional FK
    //        int interventionStartYear,
    //        int interventionEndYear,
    //        int? municipalityId, // Nullable int for optional FK
    //        string spatialReference)
    //{
    //    SqlParameter parameters = new SqlParameter
    //        {
    //            new SqlParameter("@InterventionName", interventionName),
    //            new SqlParameter("@InterventionDescription", (object)interventionDescription?? DBNull.Value), // Handle NULL
    //            new SqlParameter("@POA_ID", poaId),
    //            new SqlParameter("@LeadInstitutionID", leadInstitutionId),
    //            new SqlParameter("@WorkingGroupID", (object)workingGroupId?? DBNull.Value), // Handle NULL
    //            new SqlParameter("@InterventionStartYear", interventionStartYear),
    //            new SqlParameter("@InterventionEndYear", interventionEndYear),
    //            new SqlParameter("@MunicipalityID", (object)municipalityId?? DBNull.Value), // Handle NULL
    //            new SqlParameter("@SpatialReference", (object)spatialReference?? DBNull.Value) // Handle NULL
    //        };

    //    // ExecuteScalar is used because SP_CreateIntervention returns a single value (the new ID)
    //    object result = ExecuteScalar("SP_CreateIntervention", parameters);

    //    // Convert the result to int, handling DBNull if necessary
    //    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    //}



























//    using System.Configuration; // Required for ConfigurationManager
//using System.Data;          // For DataTable, DataSet
//using System.Data.SqlClient; // For SqlConnection, SqlCommand, SqlDataAdapter, SqlParameter

//namespace YourProjectName.Data // IMPORTANT: Replace YourProjectName
//{
//    // Assuming BaseDAL exists and provides ExecuteScalar, ExecuteDataTable, etc.
//    // If not, you'll need to add the connectionString and basic ADO.NET boilerplate here.
//    public class InterventionDAL // : BaseDAL // Uncomment if you have BaseDAL
//    {
//        // If you don't have BaseDAL, uncomment and use this:
//        private string connectionString;
//        public InterventionDAL()
//        {
//            connectionString = ConfigurationManager.ConnectionStrings.ConnectionString;
//        }
//        protected object ExecuteScalar(string spName, params SqlParameter parameters)
//        {
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(spName, conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    if (parameters != null) cmd.Parameters.AddRange(parameters);
//                    conn.Open();
//                    return cmd.ExecuteScalar();
//                }
//            }
//        }
//        protected DataTable ExecuteDataTable(string queryOrSpName, params SqlParameter parameters)
//        {
//            DataTable dt = new DataTable();
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
//                {
//                    cmd.CommandType = CommandType.Text; // Default to Text, change if SP
//                    if (parameters != null) cmd.Parameters.AddRange(parameters);
//                    conn.Open();
//                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//                    {
//                        da.Fill(dt);
//                    }
//                }
//            }
//            return dt;
//        }
//        // End of BaseDAL-like methods if not inheriting

//        /// <summary>
//        /// Creates a new Intervention record in the database.
//        /// </summary>
//        /// <returns>The ID of the newly created Intervention, or 0 if creation failed.</returns>
//        public int CreateIntervention(
//            string interventionName,
//            string interventionDescription,
//            int poaId,
//            int leadInstitutionId,
//            int? workingGroupId, // Nullable int for optional FK
//            int interventionStartYear,
//            int interventionEndYear,
//            int? municipalityId, // Nullable int for optional FK
//            string spatialReference)
//        {
//            SqlParameter parameters = new SqlParameter
//            {
//                new SqlParameter("@InterventionName", interventionName),
//                new SqlParameter("@InterventionDescription", (object)interventionDescription?? DBNull.Value), // Handle NULL
//                new SqlParameter("@POA_ID", poaId),
//                new SqlParameter("@LeadInstitutionID", leadInstitutionId),
//                new SqlParameter("@WorkingGroupID", (object)workingGroupId?? DBNull.Value), // Handle NULL
//                new SqlParameter("@InterventionStartYear", interventionStartYear),
//                new SqlParameter("@InterventionEndYear", interventionEndYear),
//                new SqlParameter("@MunicipalityID", (object)municipalityId?? DBNull.Value), // Handle NULL
//                new SqlParameter("@SpatialReference", (object)spatialReference?? DBNull.Value) // Handle NULL
//            };

//            // ExecuteScalar is used because SP_CreateIntervention returns a single value (the new ID)
//            object result = ExecuteScalar("SP_CreateIntervention", parameters);

//            // Convert the result to int, handling DBNull if necessary
//            return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
//        }

//        // --- Lookup Methods for Dropdowns ---
//        // These methods are crucial for populating the dropdowns on the AddIntervention form.
//        // You might already have these in PlanningOverviewRepository or another DAL.
//        // If so, you can reuse them or copy them here for simplicity.

//        public DataTable GetAllPOAsLookup()
//        {
//            // Assuming POA_ID and POA_Name are sufficient for the dropdown
//            return ExecuteDataTable("SELECT POA_ID, POA_Name FROM ProgrammesOfAction ORDER BY POA_Name");
//        }

//        public DataTable GetAllLeadInstitutionsLookup()
//        {
//            // Assuming InstitutionID and InstitutionName are sufficient
//            return ExecuteDataTable("SELECT InstitutionID, InstitutionName FROM ImplementationInstitutions ORDER BY InstitutionName");
//        }

//        public DataTable GetAllWorkingGroupsLookup()
//        {
//            // Assuming WorkingGroupID and WG_Name are sufficient
//            return ExecuteDataTable("SELECT WorkingGroupID, WG_Name FROM WorkingGroups ORDER BY WG_Name");
//        }

//        public DataTable GetAllMunicipalitiesLookup()
//        {
//            // Assuming MunicipalityID and MunicipalityName are sufficient
//            return ExecuteDataTable("SELECT MunicipalityID, MunicipalityName FROM Municipalities ORDER BY MunicipalityName");
//        }
//    }
//}

















}