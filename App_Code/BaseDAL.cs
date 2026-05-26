//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for BaseDAL
///// </summary>
//public class BaseDAL
//{
//    public BaseDAL()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}
















//using System.Configuration; // Required for ConfigurationManager
//using System.Data.SqlClient; // For SQL Server specific ADO.NET objects
//using System.Data; // For DataTable, DataSet, CommandType

//namespace MnE.DAL // Use your actual project namespace
//{
//    public abstract class BaseDAL
//    {
//        protected string connectionString;

//        public BaseDAL()
//        {
//            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
//        }

//        // Helper method to execute a stored procedure that returns a DataTable
//        protected DataTable ExecuteDataTable(string spName, params SqlParameter[] parameters)
//        {
//            DataTable dt = new DataTable();
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(spName, conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    if (parameters != null)
//                    {
//                        cmd.Parameters.AddRange(parameters);
//                    }
//                    conn.Open();
//                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//                    {
//                        da.Fill(dt);
//                    }
//                }
//            }
//            return dt;
//        }

//        // Helper method to execute a stored procedure that returns a DataSet (multiple DataTables)
//        protected DataSet ExecuteDataSet(string spName, params SqlParameter[] parameters)
//        {
//            DataSet ds = new DataSet();
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(spName, conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    if (parameters != null)
//                    {
//                        cmd.Parameters.AddRange(parameters);
//                    }
//                    conn.Open();
//                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//                    {
//                        da.Fill(ds);
//                    }
//                }
//            }
//            return ds;
//        }

//        // Helper method to execute a stored procedure for CUD operations (returns rows affected)
//        protected int ExecuteNonQuery(string spName, params SqlParameter[] parameters)
//        {
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(spName, conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    if (parameters != null)
//                    {
//                        cmd.Parameters.AddRange(parameters);
//                    }
//                    conn.Open();
//                    return cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        // Helper method to execute a stored procedure that returns a single scalar value (e.g., NewID)
//        protected object ExecuteScalar(string spName, params SqlParameter[] parameters)
//        {
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(spName, conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    if (parameters != null)
//                    {
//                        cmd.Parameters.AddRange(parameters);
//                    }
//                    conn.Open();
//                    return cmd.ExecuteScalar();
//                }
//            }
//        }
//    }
//}








using System.Configuration; // Required for ConfigurationManager
using System.Data.SqlClient; // For SQL Server specific ADO.NET objects
using System.Data; // For DataTable, DataSet, CommandType

namespace MnE.DAL // IMPORTANT: Replace YourProjectName
{
    public abstract class BaseDAL
    {
        protected string connectionString;

        public BaseDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
                    if (parameters != null)
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

        // We only need ExecuteDataSet for this step, but you can keep other helpers here from previous replies for future use.
        // For example:
        // protected int ExecuteNonQuery(string spName, params SqlParameter[] parameters) { ... }
        // protected object ExecuteScalar(string spName, params SqlParameter[] parameters) { ... }
        // protected DataTable ExecuteDataTable(string spName, params SqlParameter[] parameters) { ... }
    }
}