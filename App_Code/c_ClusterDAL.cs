using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace MnE2.DAL
{
    public class c_ClusterDAL
    {
        public int Upsert(c_Cluster model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Cluster_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClusterID", model.ClusterID == 0 ? (object)DBNull.Value : model.ClusterID);
                cmd.Parameters.AddWithValue("@ClusterName", model.ClusterName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ClusterDescription", model.ClusterDescription ?? (object)DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public c_Cluster GetByID(int clusterID)
        {
            c_Cluster result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Cluster_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClusterID", clusterID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Cluster
                        {
                            ClusterID = Convert.ToInt32(dr["ClusterID"]),
                            ClusterName = dr["ClusterName"].ToString(),
                            ClusterDescription = dr["ClusterDescription"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Cluster> GetAll()
        {
            var list = new List<c_Cluster>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Cluster_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Cluster
                        {
                            ClusterID = Convert.ToInt32(dr["ClusterID"]),
                            ClusterName = dr["ClusterName"].ToString(),
                            ClusterDescription = dr["ClusterDescription"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int clusterID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Cluster_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClusterID", clusterID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns ClusterID
            }
        }
    }
}