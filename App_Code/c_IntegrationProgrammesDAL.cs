using MnE2.DAL;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_IntegrationProgrammesDAL
    {
        public int Upsert(c_IntegrationProgramme model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Programme_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeID", model.ProgrammeID == 0 ? (object)DBNull.Value : model.ProgrammeID);
                cmd.Parameters.AddWithValue("@ProgrammeName", (object)model.ProgrammeName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LeaderDeptID", (object)model.LeaderDeptID ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns ProgrammeID
            }
        }

        public c_IntegrationProgramme GetByID(int programmeID)
        {
            c_IntegrationProgramme result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Programme_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_IntegrationProgramme
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgrammeName = dr["ProgrammeName"].ToString(),
                            LeaderDeptID = dr["LeaderDeptID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LeaderDeptID"])
                        };
                    }
                }
            }
            return result;
        }

        public List<c_IntegrationProgramme> GetAll()
        {
            var list = new List<c_IntegrationProgramme>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Programme_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_IntegrationProgramme
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgrammeName = dr["ProgrammeName"].ToString(),
                            LeaderDeptID = dr["LeaderDeptID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LeaderDeptID"])
                        });
                    }
                }
            }
            return list;
        }

        public List<c_IntegrationProgramme> ListByLeaderDept(int leaderDeptID)
        {
            var list = new List<c_IntegrationProgramme>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lld_sp_Programme_ListByLeaderDept", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeaderDeptID", leaderDeptID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_IntegrationProgramme
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgrammeName = dr["ProgrammeName"].ToString(),
                            LeaderDeptID = dr["LeaderDeptID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LeaderDeptID"])
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int programmeID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Programme_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns ProgrammeID
            }
        }

        public c_IntegrationProgramme GetByNameAndDepartment(string programmeName, int? leaderDeptID)
        {
            c_IntegrationProgramme result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("gnd_sp_Programme_GetByNameAndDept", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeName", (object)programmeName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LeaderDeptID", (object)leaderDeptID ?? DBNull.Value);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_IntegrationProgramme
                        {
                            ProgrammeID = Convert.ToInt32(dr["ProgrammeID"]),
                            ProgrammeName = dr["ProgrammeName"].ToString(),
                            LeaderDeptID = dr["LeaderDeptID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LeaderDeptID"])
                        };
                    }
                }
            }
            return result;
        }
    }

}