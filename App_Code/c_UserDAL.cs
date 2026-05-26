using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TextmagicRest.Model;

namespace MnE2.DAL
{
    public class c_UserDAL
    {
        public int Upsert(c_User model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", model.UserID == 0 ? (object)DBNull.Value : model.UserID);
                cmd.Parameters.AddWithValue("@Username", (object)model.Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PasswordHash", (object)model.PasswordHash ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FullName", (object)model.FullName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DepartmentID", (object)model.DepartmentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EntityID", (object)model.EntityID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleID", (object)model.RoleID ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns UserID
            }
        }

        public c_User GetByID(int userID)
        {
            c_User result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_User_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = MapUser(dr);
                    }
                }
            }
            return result;
        }

        public c_User GetByUsername(string username)
        {
            c_User result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_GetByUsername", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = MapUser(dr);
                    }
                }
            }
            return result;
        }

        public List<c_User> GetAll()
        {
            var list = new List<c_User>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_User_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapUser(dr));
                    }
                }
            }
            return list;
        }

        public List<c_User> ListByRole(int roleID)
        {
            var list = new List<c_User>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_ListByRole", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", roleID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapUser(dr));
                    }
                }
            }
            return list;
        }

        public List<c_User> ListByDepartment(int departmentID)
        {
            var list = new List<c_User>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_ListByDepartment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapUser(dr));
                    }
                }
            }
            return list;
        }

        public List<c_User> ListByEntity(int entityID)
        {
            var list = new List<c_User>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_ListByEntity", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityID", entityID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapUser(dr));
                    }
                }
            }
            return list;
        }

        public int Delete(int userID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_User_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns UserID
            }
        }

        private static c_User MapUser(SqlDataReader dr)
        {
            return new c_User
            {
                UserID = Convert.ToInt32(dr["UserID"]),
                Username = dr["Username"].ToString(),
                PasswordHash = dr["PasswordHash"].ToString(),
                FullName = dr["FullName"].ToString(),
                Email = dr["Email"].ToString(),
                DepartmentID = dr["DepartmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DepartmentID"]),
                EntityID = dr["EntityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["EntityID"]),
                RoleID = dr["RoleID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["RoleID"])
            };
        }

        // New Methods Below 

        // DAL/UserDAL.cs (add this method)
        public int AssignIndicatorOwner(int userID, int indicatorID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_User_AssignIndicatorOwner", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns 1 if success (per proc)
            }
        }
    }
}