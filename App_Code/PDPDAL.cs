//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for PDPDAL
///// </summary>
//public class PDPDAL
//{
//    public PDPDAL()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}











using System.Configuration; // Required for ConfigurationManager
using System.Data;          // For DataTable, DataSet, CommandType
using System.Data.SqlClient; // For SqlConnection, SqlCommand, SqlDataAdapter, SqlParameter

public class PDPDAL
{
        private string connectionString;

        public PDPDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        

        // Helper method to execute a stored procedure that returns a single scalar value
        protected object ExecuteScalar(string spName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Helper method to execute a query (text or stored procedure) that returns a DataTable
        protected DataTable ExecuteDataTable(string queryOrSpName, CommandType commandType, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Helper method to execute a stored procedure that returns a DataSet (multiple DataTables)
        protected DataSet ExecuteDataSet(string spName, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// Retrieves all detailed hierarchical information for a single Provincial Development Plan (PDP).
        /// Returns a DataSet with multiple tables:
        ///  - PDP Info (main details)
        /// [1] - PMTDP Priorities under this PDP
        /// [2] - Programmes of Action (POAs) under these PMTDP Priorities
        /// [3] - Interventions under these POAs
        /// [4] - Intervention Indicators for these interventions
        /// [5] - Intervention Budgets for these interventions
        /// </summary>
        /// <param name="pdpId">The ID of the PDP to retrieve.</param>
        /// <returns>DataSet containing all linked hierarchical data for the PDP.</returns>
        public DataSet GetPDPDetails(int pdpId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PDP_ID", pdpId)
            };
            // SP_GetPDPDetails returns 6 result sets
            return ExecuteDataSet("new_SP_GetPDPDetails_2", parameters);
        }

















        //protected object ExecuteScalar(string spName, params SqlParameter[] parameters) // S_R55
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        using (SqlCommand cmd = new SqlCommand(spName, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure; // S_R88, S_R75
        //            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        //            conn.Open();
        //            return cmd.ExecuteScalar();
        //        }
        //    }

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

        //protected DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] parameters)
        //{
        //    DataSet ds = new DataSet();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        if (parameters != null && parameters.Length > 0)
        //        {
        //            cmd.Parameters.AddRange(parameters);
        //        }

        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            da.Fill(ds);
        //        }
        //    }

        //    return ds;
        //}

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
}