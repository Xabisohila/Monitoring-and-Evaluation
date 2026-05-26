using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_PrioritiesDAL
    {
        public int Upsert(c_Priority model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Priority_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PriorityID", model.PriorityID == 0 ? (object)DBNull.Value : model.PriorityID);
                cmd.Parameters.AddWithValue("@PriorityName", (object)model.PriorityName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object)model.Description ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns PriorityID
            }
        }

        public c_Priority GetByID(int priorityID)
        {
            c_Priority result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Priority_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PriorityID", priorityID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Priority
                        {
                            PriorityID = Convert.ToInt32(dr["PriorityID"]),
                            PriorityName = dr["PriorityName"].ToString(),
                            Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Priority> GetAll()
        {
            var list = new List<c_Priority>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Priority_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Priority
                        {
                            PriorityID = Convert.ToInt32(dr["PriorityID"]),
                            PriorityName = dr["PriorityName"].ToString(),
                            Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int priorityID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Priority_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PriorityID", priorityID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns PriorityID
            }
        }

        public c_Priority GetByName(string priorityName)
        {
            c_Priority result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Priority_GetByName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PriorityName", (object)priorityName ?? DBNull.Value);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Priority
                        {
                            PriorityID = Convert.ToInt32(dr["PriorityID"]),
                            PriorityName = dr["PriorityName"].ToString(),
                            Description = dr["Description"] == DBNull.Value ? null : dr["Description"].ToString()
                        };
                    }
                }
            }
            return result;
        }
    }

}