using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_EntityDAL
    {
        public int Upsert(c_Entity model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Entity_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EntityID", model.EntityID == 0 ? (object)DBNull.Value : model.EntityID);
                cmd.Parameters.AddWithValue("@EntityName", (object)model.EntityName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DepartmentID", (object)model.DepartmentID ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns new/updated EntityID
            }
        }

        public c_Entity GetByID(int entityID)
        {
            c_Entity result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Entity_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityID", entityID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Entity
                        {
                            EntityID = Convert.ToInt32(dr["EntityID"]),
                            EntityName = dr["EntityName"].ToString(),
                            DepartmentID = dr["DepartmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DepartmentID"])
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Entity> GetAll()
        {
            var list = new List<c_Entity>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Entity_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Entity
                        {
                            EntityID = Convert.ToInt32(dr["EntityID"]),
                            EntityName = dr["EntityName"].ToString(),
                            DepartmentID = dr["DepartmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DepartmentID"])
                        });
                    }
                }
            }
            return list;
        }

        public List<c_Entity> ListByDepartment(int departmentID)
        {
            var list = new List<c_Entity>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Entity_ListByDepartment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Entity
                        {
                            EntityID = Convert.ToInt32(dr["EntityID"]),
                            EntityName = dr["EntityName"].ToString(),
                            DepartmentID = dr["DepartmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DepartmentID"])
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int entityID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Entity_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityID", entityID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns EntityID
            }
        }
    }
}