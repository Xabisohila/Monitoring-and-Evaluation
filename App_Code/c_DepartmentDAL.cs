using DocumentFormat.OpenXml.Bibliography;
using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_DepartmentDAL
    {
        public int Upsert(c_Department model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Department_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID == 0 ? (object)DBNull.Value : model.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName ?? (object)DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public c_Department GetByID(int departmentID)
        {
            c_Department result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Department_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Department
                        {
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            DepartmentName = dr["DepartmentName"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public c_Department GetByName(string departmentName)
        {
            c_Department result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Department_GetByName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName ?? (object)DBNull.Value);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Department
                        {
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            DepartmentName = dr["DepartmentName"].ToString()
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Department> GetAll()
        {
            var list = new List<c_Department>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Department_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Department
                        {
                            DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                            DepartmentName = dr["DepartmentName"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int departmentID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Department_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns DepartmentID
            }
        }
    }

}