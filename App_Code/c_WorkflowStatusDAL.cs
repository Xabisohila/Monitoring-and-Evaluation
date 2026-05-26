using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_WorkflowStatusDAL
    {
        public int Upsert(c_WorkflowStatus model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_WorkflowStatus_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatusID", model.StatusID == 0 ? (object)DBNull.Value : model.StatusID);
                cmd.Parameters.AddWithValue("@StatusName", (object)model.StatusName ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns StatusID
            }
        }

        public c_WorkflowStatus GetByID(int statusID)
        {
            c_WorkflowStatus result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_WorkflowStatus_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusID", statusID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_WorkflowStatus
                        {
                            StatusID = Convert.ToInt32(dr["StatusID"]),
                            StatusName = dr["StatusName"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public List<c_WorkflowStatus> GetAll()
        {
            var list = new List<c_WorkflowStatus>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_WorkflowStatus_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_WorkflowStatus
                        {
                            StatusID = Convert.ToInt32(dr["StatusID"]),
                            StatusName = dr["StatusName"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int statusID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_WorkflowStatus_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusID", statusID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns StatusID
            }
        }
    }
}