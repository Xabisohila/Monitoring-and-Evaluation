using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_OutcomesDAL
    {
        public int Upsert(c_Outcome model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Outcome_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OutcomeID", model.OutcomeID == 0 ? (object)DBNull.Value : model.OutcomeID);
                cmd.Parameters.AddWithValue("@OutcomeName", (object)model.OutcomeName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PriorityID", (object)model.PriorityID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProgrammeID", (object)model.ProgrammeID ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns OutcomeID
            }
        }

        public c_Outcome GetByID(int outcomeID)
        {
            c_Outcome result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Outcome_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OutcomeID", outcomeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Outcome
                        {
                            OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                            OutcomeName = dr["OutcomeName"].ToString(),
                            PriorityID = dr["PriorityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["PriorityID"]),
                            ProgrammeID = dr["ProgrammeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProgrammeID"])
                        };
                    }
                }
            }
            return result;
        }

        public List<c_Outcome> GetAll()
        {
            var list = new List<c_Outcome>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Outcome_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Outcome
                        {
                            OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                            OutcomeName = dr["OutcomeName"].ToString(),
                            PriorityID = dr["PriorityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["PriorityID"]),
                            ProgrammeID = dr["ProgrammeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProgrammeID"])
                        });
                    }
                }
            }
            return list;
        }

        public List<c_Outcome> ListByPriority(int priorityID)
        {
            var list = new List<c_Outcome>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lp_sp_Outcome_ListByPriority", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PriorityID", priorityID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Outcome
                        {
                            OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                            OutcomeName = dr["OutcomeName"].ToString(),
                            PriorityID = dr["PriorityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["PriorityID"]),
                            ProgrammeID = dr["ProgrammeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProgrammeID"])
                        });
                    }
                }
            }
            return list;
        }

        public List<c_Outcome> ListByProgramme(int programmeID)
        {
            var list = new List<c_Outcome>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lp_sp_Outcome_ListByProgramme", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new c_Outcome
                        {
                            OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                            OutcomeName = dr["OutcomeName"].ToString(),
                            PriorityID = dr["PriorityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["PriorityID"]),
                            ProgrammeID = dr["ProgrammeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProgrammeID"])
                        });
                    }
                }
            }
            return list;
        }

        public int Delete(int outcomeID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Outcome_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OutcomeID", outcomeID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns OutcomeID
            }
        }

        public c_Outcome GetByNamePriorityAndProgramme(string outcomeName, int priorityID, int programmeID)
        {
            c_Outcome result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Outcome_GetByNamePriorityAndProgramme", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OutcomeName", (object)outcomeName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PriorityID", priorityID);
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new c_Outcome
                        {
                            OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                            OutcomeName = dr["OutcomeName"].ToString(),
                            PriorityID = dr["PriorityID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["PriorityID"]),
                            ProgrammeID = dr["ProgrammeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ProgrammeID"])
                        };
                    }
                }
            }
            return result;
        }
    }
}