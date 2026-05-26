
using System;
using System.Configuration; // Required for ConfigurationManager
using System.Data;          // For DataTable, DataSet, CommandType
using System.Data.SqlClient; // For SqlConnection, SqlCommand, SqlDataAdapter, SqlParameter


// This class will serve as your Data Access Layer for Programmes of Action (POAs).
public class POADAL
    {
        private string connectionString;

        public POADAL()
        {
        // Constructor: Initializes the connection string from Web.config
        // [1]1, S_R76, [13]
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

        // Helper method to execute a stored procedure that returns a single scalar value (e.g., NewID)
        // S_R77, S_R84
        //protected object ExecuteScalar(string spName, params SqlParameter parameters) // S_R55
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(spName, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure; // S_R88, S_R75
        //            if (parameters != null && parameters.Length > 0)
        //            {
        //                cmd.Parameters.AddRange(parameters);
        //            }
        //            conn.Open();
        //            return cmd.ExecuteScalar();
        //        }
        //    }
        //}


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


















    // Helper method to execute a query (text or stored procedure) that returns a DataTable
    // S_R17, S_R18, S_R19, S_R24, S_R26, S_R27, S_R28
    //protected DataTable ExecuteDataTable(string queryOrSpName, CommandType commandType, params SqlParameter parameters) // S_R75, S_R88
    //    {
    //        DataTable dt = new DataTable();
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
    //            {
    //                cmd.CommandType = commandType;
    //                if (parameters != null && parameters.Length > 0)
    //                {
    //                    cmd.Parameters.AddRange(parameters);
    //                }
    //                conn.Open();
    //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //                {
    //                    da.Fill(dt);
    //                }
    //            }
    //        }
    //        return dt;
    //    }


    









    /// <summary>
            /// Creates a new Programme of Action (POA) record in the database.
            /// </summary>
            /// <returns>The ID of the newly created POA, or 0 if creation failed.</returns>
    public int CreatePOA(
         string poaName,
         string poaDescription,
         int pmtdpPriorityId,
         int clusterId,
         int poaStartYear,
         int poaEndYear,
         string desiredOutcome)
        {
            // S_R48, S_R55
            SqlParameter[] parameters = new SqlParameter[]
             {
             new SqlParameter("@POA_Name", poaName),
             new SqlParameter("@POA_Description", (object)poaDescription?? DBNull.Value), // S_R82, S_R85
             new SqlParameter("@PMTDP_PriorityID", pmtdpPriorityId),
             new SqlParameter("@ClusterID", clusterId),
             new SqlParameter("@POA_StartYear", poaStartYear),
             new SqlParameter("@POA_EndYear", poaEndYear),
             new SqlParameter("@DesiredOutcome", (object)desiredOutcome?? DBNull.Value) // S_R82, S_R85
                        };

            // ExecuteScalar is used because SP_CreatePOA returns a single value (the new ID)
            object result = ExecuteScalar("new_SP_CreatePOA", parameters);

            // Convert the result to int, handling DBNull if necessary
            return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
        }

    // --- Lookup Methods for Dropdowns ---
    // These methods are crucial for populating the dropdowns on the AddPOA form.
    // S_R17, S_R18, S_R19, S_R22, S_R24, S_R26, S_R27, S_R28, S_R36, S_R40


    public DataTable GetAllPMTDPPrioritiesLookup()
        {
            return ExecuteDataTable("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities ORDER BY PriorityName");
        }

        public DataTable GetAllClustersLookup()
        {
        // OR return ExecuteDataTable
        //return ExecuteDataTable("SELECT ClusterID, ClusterName FROM new_Clusters ORDER BY ClusterName", CommandType.Text);
        return ExecuteDataTable("SELECT ClusterID, ClusterName FROM new_Clusters ORDER BY ClusterName");
    }

    public DataSet GetPOADetails(int poaId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@POA_ID", poaId)
        };
        // SP_GetPOADetails returns 4 result sets
        return ExecuteDataSet("new_SP_GetPOADetails_2", parameters);
    }








    // Edit POA start -------------------------------------------------------------------





    // Helper method to execute a stored procedure for CUD operations (returns rows affected, typically 1 for update)
    // This is a new helper method needed for Update/Delete operations if not already present.
    //protected int ExecuteNonQuery(string spName, params SqlParameter parameters)
    //{
    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(spName, conn))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            if (parameters != null && parameters.Length > 0)
    //            {
    //                cmd.Parameters.AddRange(parameters);
    //            }
    //            conn.Open();
    //            return cmd.ExecuteNonQuery(); // Returns number of rows affected
    //        }
    //    }
    //}


    /// <summary>
    /// Executes a stored procedure for CUD operations and returns the number of rows affected.
    /// </summary>
    /// <param name="spName">The name of the stored procedure.</param>
    /// <param name="parameters">Optional SQL parameters for the stored procedure.</param>
    /// <returns>Number of rows affected.</returns>
    protected int ExecuteNonQuery(string spName, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteNonQuery(); // Returns number of rows affected
        }
    }

    /// <summary>
    /// Updates an existing Programme of Action (POA) record in the database.
    /// </summary>
    /// <returns>The number of rows affected (should be 1 for success), or 0 if update failed.</returns>
    public int UpdatePOA(
    int poaId,
    string poaName,
    string poaDescription,
    int pmtdpPriorityId,
    int clusterId,
    int poaStartYear,
    int poaEndYear,
    string desiredOutcome)
    {
        SqlParameter[] parameters = new SqlParameter[]
         {
         new SqlParameter("@POA_ID", poaId),
         new SqlParameter("@POA_Name", poaName),
         new SqlParameter("@POA_Description", (object)poaDescription?? DBNull.Value),
         new SqlParameter("@PMTDP_PriorityID", pmtdpPriorityId),
         new SqlParameter("@ClusterID", clusterId),
         new SqlParameter("@POA_StartYear", poaStartYear),
         new SqlParameter("@POA_EndYear", poaEndYear),
         new SqlParameter("@DesiredOutcome", (object)desiredOutcome?? DBNull.Value)
         };

        // ExecuteScalar is used because SP_UpdatePOA returns a single value (RowsAffected)
        object result = ExecuteScalar("new_SP_UpdatePOA", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }




    /// <summary>
            /// Retrieves all detailed information for a single Programme of Action (POA).
            /// Returns a DataSet with multiple tables.
            /// </summary>
            /// <param name="poaId">The ID of the POA to retrieve.</param>
            /// <returns>DataSet containing all linked data for the POA.</returns>
    /// 
    //   public DataSet GetPOADetails(int poaId)
    //   {
    //       SqlParameter[] parameters = new SqlParameter[]
    //{
    //new SqlParameter("@POA_ID", poaId)
    //};
    //       // SP_GetPOADetails returns 4 result sets
    //       return ExecuteDataSet("SP_GetPOADetails", parameters);
    //   }

    // --- Lookup Methods for Dropdowns (ensure these are present) ---
    //public DataTable GetAllPMTDPPrioritiesLookup()
    //{
    //    return ExecuteDataTable("SELECT PMTDP_PriorityID, PriorityName FROM PMTDP_Priorities ORDER BY PriorityName", CommandType.Text);
    //}

    //public DataTable GetAllClustersLookup()
    //{
    //    return ExecuteDataTable("SELECT ClusterID, ClusterName FROM Clusters ORDER BY ClusterName", CommandType.Text);
    //}





    // Edit POA end ---------------------------------------------------------------------





    public DataTable GetAllPDPs()
    {
        return ExecuteDataTable(
            "SELECT PDP_ID, PDP_Name, PDP_StartYear, PDP_EndYear FROM new_ProvincialDevelopmentPlans ORDER BY PDP_StartYear DESC");
    }

    public DataTable GetPriorityContext(int priorityId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(
            "SELECT PDP_ID, ClusterID FROM new_PMTDP_Priorities WHERE PMTDP_PriorityID = @PriorityID", conn))
        {
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public DataTable GetAllPOAs()
    {
        string sql = @"
            SELECT p.POA_ID, p.POA_Name, p.POA_StartYear, p.POA_EndYear, p.DesiredOutcome,
                   pr.PriorityName, c.ClusterName
            FROM new_ProgrammesOfAction p
            LEFT JOIN new_PMTDP_Priorities pr ON pr.PMTDP_PriorityID = p.PMTDP_PriorityID
            LEFT JOIN new_Clusters c ON c.ClusterID = p.ClusterID
            ORDER BY c.ClusterName, pr.PriorityName, p.POA_Name";
        return ExecuteDataTable(sql);
    }

    public DataTable GetPrioritiesByCluster(int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities WHERE ClusterID = @ClusterID ORDER BY PriorityName", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public DataTable GetPrioritiesByClusterAndPDP(int clusterId, int pdpId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(
            "SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities WHERE ClusterID = @ClusterID AND PDP_ID = @PDP_ID ORDER BY PriorityName", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PDP_ID", pdpId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }






}


































