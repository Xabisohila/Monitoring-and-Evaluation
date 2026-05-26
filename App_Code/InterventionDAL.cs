using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//namespace YourProjectName.Data // Replace with your actual namespace
//{
public class InterventionDAL
{
    private string connectionString;

    public InterventionDAL()
    {
        // Replace "YourConnectionStringName" with the actual name in your Web.config or App.config
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //string conn1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    protected object ExecuteScalar(string spName, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
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

    public int CreateIntervention(
        string interventionName,
        string interventionDescription,
        int poaId,
        int leadInstitutionId,
        int? workingGroupId,
        int interventionStartYear,
        int interventionEndYear,
        int? municipalityId,
        string spatialReference, int subOutcomeId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@InterventionName", interventionName),
            new SqlParameter("@InterventionDescription", (object)interventionDescription ?? DBNull.Value),
            new SqlParameter("@POA_ID", poaId),
            new SqlParameter("@LeadInstitutionID", leadInstitutionId),
            new SqlParameter("@WorkingGroupID", (object)workingGroupId ?? DBNull.Value),
            new SqlParameter("@InterventionStartYear", interventionStartYear),
            new SqlParameter("@InterventionEndYear", interventionEndYear),
            new SqlParameter("@MunicipalityID", (object)municipalityId ?? DBNull.Value),
            new SqlParameter("@SpatialReference", (object)spatialReference ?? DBNull.Value),

            new SqlParameter("@SubOutcomeID", subOutcomeId)
        };

        object result = ExecuteScalar("new_SP_CreateIntervention", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }

    public int UpdateIntervention(
       int interventionId,
       string interventionName,
       string interventionDescription,
       int poaId,
       int leadInstitutionId,
       int? workingGroupId,
       int interventionStartYear,
       int interventionEndYear,
       int? municipalityId,
       string spatialReference)
    {
        SqlParameter[] parameters = new SqlParameter[]
         {
         new SqlParameter("@InterventionID", interventionId),
         new SqlParameter("@InterventionName", interventionName),
         new SqlParameter("@InterventionDescription", (object)interventionDescription?? DBNull.Value),
         new SqlParameter("@POA_ID", poaId),
         new SqlParameter("@LeadInstitutionID", leadInstitutionId),
         new SqlParameter("@WorkingGroupID", (object)workingGroupId?? DBNull.Value),
         new SqlParameter("@InterventionStartYear", interventionStartYear),
         new SqlParameter("@InterventionEndYear", interventionEndYear),
         new SqlParameter("@MunicipalityID", (object)municipalityId?? DBNull.Value),
         new SqlParameter("@SpatialReference", (object)spatialReference?? DBNull.Value)
         };

        // ExecuteScalar is used because SP_UpdateIntervention returns a single value (RowsAffected)
        object result = ExecuteScalar("new_SP_UpdateIntervention", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }



    /// <summary>
            /// Retrieves all detailed information for a single Intervention.
            /// Returns a DataSet with multiple tables:
            ///  - Intervention Info (main details)
            /// [1] - Intervention Indicators and Targets
            /// [2] - Intervention Budgets
            /// [3] - Quarterly Reports
            /// </summary>
            /// <param name="interventionId">The ID of the Intervention to retrieve.</param>
            /// <returns>DataSet containing all linked data for the Intervention.</returns>
    public DataSet GetInterventionDetails(int interventionId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@InterventionID", interventionId)
        };
        // SP_GetInterventionDetails returns multiple result sets (4 in total now)
        return ExecuteDataSet("new_SP_GetInterventionDetails_2", parameters);
    }

    public DataTable GetAllClustersLookup()
    {
        return ExecuteDataTable("SELECT ClusterID, ClusterName FROM new_Clusters ORDER BY ClusterName");
    }

    public DataTable GetAllPOAsLookup()
    {
        return ExecuteDataTable("SELECT POA_ID, POA_Name FROM new_ProgrammesOfAction ORDER BY POA_Name");
    }

    public DataTable GetAllLeadInstitutionsLookup()
    {
        return ExecuteDataTable("SELECT InstitutionID, InstitutionName FROM new_ImplementationInstitutions ORDER BY InstitutionName");
    }

    public DataTable GetAllWorkingGroupsLookup()
    {
        return ExecuteDataTable("SELECT WorkingGroupID, WG_Name FROM new_WorkingGroups ORDER BY WG_Name");
    }

    public DataTable GetAllMunicipalitiesLookup()
    {
        return ExecuteDataTable("SELECT MunicipalityID, MunicipalityName FROM new_Municipalities ORDER BY MunicipalityName");
    }


    // Additional
    public DataTable GetAllSubOutcomesLookup(int ClusterNumb)
    {
        return ExecuteDataTable("SELECT SubOutcomeID, SubOutcome FROM new_SubOutcomes Where  ClusterID = " +
            ClusterNumb + "ORDER BY SubOutcome");
    }









   /// <summary>
   /// Created 16 October 2025
   /// </summary>
   /// <param name="queryOrSpName"></param>
   /// <param name="commandType"></param>
   /// <param name="parameters"></param>
   /// <returns></returns>


    //private 
      public  DataTable ExecuteDataTable(string queryOrSpName, CommandType commandType, params SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }

    public DataTable GetPendingInterventions()
    {
        return ExecuteDataTable("SELECT * FROM new_Interventions WHERE IsApproved = 0", CommandType.Text);
    }

    public DataTable GetIndicatorsByIntervention(int interventionId)
    {
        SqlParameter param = new SqlParameter("@InterventionID", interventionId);
        return ExecuteDataTable("SELECT * FROM new_Intervention_Indicators WHERE InterventionID = @InterventionID", CommandType.Text, param);
    }

    public DataTable GetBudgetsByIntervention(int interventionId)
    {
        SqlParameter param = new SqlParameter("@InterventionID", interventionId);
        return ExecuteDataTable("SELECT * FROM new_Intervention_Budgets WHERE InterventionID = @InterventionID", CommandType.Text, param);
    }

    public void ApproveIntervention(int interventionId, int approverUserId, string approvalStatus, string approvalComment)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("new_SP_ApproveIntervention", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            cmd.Parameters.AddWithValue("@ApproverUserID", approverUserId);
            cmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
            cmd.Parameters.AddWithValue("@ApprovalComment", approvalComment ?? (object)DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }


    //----------------------------------------------------------------------------------

    public DataTable GetApprovedInterventions()
    {
        const string sql = "SELECT * FROM new_Interventions WHERE IsApproved = 1 AND ApprovalDate >= DATEADD(MONTH, -1, GETDATE())";
        return ExecuteDataTable(sql, CommandType.Text);
    }







































    //DataSet not DataTable format... if not working change back.
    //public DataSet GetInterventionDetails(int interventionId)
    //{
    //    SqlParameter[] parameters = new SqlParameter[]
    //    {
    //        new SqlParameter("@InterventionID", interventionId)
    //    };

    //    return ExecuteDataSet("new_SP_GetInterventionDetails_2", parameters);
    //}















    //... (existing using statements and namespace declaration)...

    //namespace YourProjectName.Data // IMPORTANT: Replace YourProjectName
    //{
    //public class InterventionDAL // : BaseDAL // Uncomment if you have BaseDAL
    //{
    //... (connectionString and ExecuteScalar/ExecuteDataTable helpers, as provided in previous fixes)...

    /// <summary>
            /// Retrieves all detailed information for a single Intervention.
            /// Returns a DataSet with multiple tables:
            ///  - Intervention Info (main details)
            /// [1] - Intervention Indicators and Targets
            /// [2] - Intervention Budgets
            /// [3] - Quarterly Reports
            /// </summary>
            /// <param name="interventionId">The ID of the Intervention to retrieve.</param>
            /// <returns>DataSet containing all linked data for the Intervention.</returns>
    //      public DataSet GetInterventionDetails(int interventionId)
    //      {
    //          SqlParameter parameters = new SqlParameter
    //{
    //  new SqlParameter("@InterventionID", interventionId)
    //};
    //          // SP_GetInterventionDetails returns multiple result sets (4 in total now)
    //          return ExecuteDataSet("SP_GetInterventionDetails", parameters);
    //      }








    //    public DataSet GetInterventionDetails(int interventionId)
    //    {
    //        SqlParameter[] parameters = new SqlParameter[]
    //        {
    //new SqlParameter("@InterventionID", interventionId)
    //        };

    //        // SP_GetInterventionDetails returns multiple result sets
    //        return ExecuteDataSet("SP_GetInterventionDetails", parameters);
    //    }


    //... (other methods like CreateIntervention, GetAllPOAsLookup, etc.)...



    //}










    /// <summary>
            /// Updates an existing Intervention record in the database.
            /// </summary>
            /// <returns>The number of rows affected (should be 1 for success), or 0 if update failed.</returns>


}


