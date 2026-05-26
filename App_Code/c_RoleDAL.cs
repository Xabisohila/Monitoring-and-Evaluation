using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_RoleDAL
    {
        public int Upsert(c_Role model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Role_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RoleID", model.RoleID == 0 ? (object)DBNull.Value : model.RoleID);
                cmd.Parameters.AddWithValue("@RoleName", (object)model.RoleName ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns RoleID
            }
        }

        public c_Role GetByID(int roleID)
        {
            c_Role result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Role_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", roleID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Role
                        {
                            RoleID = Convert.ToInt32(dr["RoleID"]),
                            RoleName = dr["RoleName"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Role> GetAll()
        {
            var list = new List<c_Role>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Role_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Role
                        {
                            RoleID = Convert.ToInt32(dr["RoleID"]),
                            RoleName = dr["RoleName"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int roleID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Role_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", roleID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns RoleID
            }
        }
    }
}