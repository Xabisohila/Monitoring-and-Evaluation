using MnE2.DAL;
using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_WorkingGroupDAL
    {
        public int Upsert(c_WorkingGroup model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_WorkingGroup_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkingGroupID", model.WorkingGroupID == 0 ? (object)DBNull.Value : model.WorkingGroupID);
                cmd.Parameters.AddWithValue("@WorkingGroupName", model.WorkingGroupName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ClusterID", model.ClusterID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public c_WorkingGroup GetByID(int workingGroupID)
        {
            c_WorkingGroup result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_WorkingGroup_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkingGroupID", workingGroupID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_WorkingGroup
                        {
                            WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"]),
                            WorkingGroupName = dr["WorkingGroupName"].ToString(),
                            ClusterID = Convert.ToInt32(dr["ClusterID"])
                        };
                    }
                }
            }
            return result;
        }

        public List<c_WorkingGroup> GetAll()
        {
            var list = new List<c_WorkingGroup>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_WorkingGroup_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_WorkingGroup
                        {
                            WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"]),
                            WorkingGroupName = dr["WorkingGroupName"].ToString(),
                            ClusterID = Convert.ToInt32(dr["ClusterID"])
                        });
                    }
                }
            }
            return list;
        }

        public List<c_WorkingGroup> ListByCluster(int clusterID)
        {
            var list = new List<c_WorkingGroup>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lc_sp_WorkingGroup_ListByCluster", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClusterID", clusterID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_WorkingGroup
                        {
                            WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"]),
                            WorkingGroupName = dr["WorkingGroupName"].ToString(),
                            ClusterID = Convert.ToInt32(dr["ClusterID"])
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int workingGroupID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_WorkingGroup_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WorkingGroupID", workingGroupID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns WorkingGroupID
            }
        }
    }
}