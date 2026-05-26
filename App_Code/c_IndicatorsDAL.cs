using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_IndicatorsDAL
    {
        // DAL/IndicatorDAL.cs (modify Upsert signature & call)
        public int Upsert(c_Indicator model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Indicator_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", model.IndicatorID == 0 ? (object)DBNull.Value : model.IndicatorID);
                cmd.Parameters.AddWithValue("@IndicatorName", model.IndicatorName);
                cmd.Parameters.AddWithValue("@IndicatorType", (object)model.IndicatorType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OutcomeID", model.OutcomeID);
                cmd.Parameters.AddWithValue("@BaselineValue", (object)model.BaselineValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TermTargetValue", (object)model.TermTargetValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AnnualBudget", model.AnnualBudget);
                cmd.Parameters.AddWithValue("@ImplementingInstitution", (object)model.ImplementingInstitution ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SupportingInstitutions", (object)model.SupportingInstitutions ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CalculationType", (object)model.CalculationType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReportingCycle", (object)model.ReportingCycle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsCumulative", model.IsCumulative);
                cmd.Parameters.AddWithValue("@IsPercentage", model.IsPercentage);

                // NEW optional WG mapping (null allowed)
                cmd.Parameters.AddWithValue("@WorkingGroupID", (object)model.WorkingGroupID ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public c_Indicator GetByID(int indicatorID)
        {
            c_Indicator result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Indicator_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = Map(dr);
                    }
                }
            }
            return result;
        }

        public List<c_Indicator> GetAll()
        {
            var list = new List<c_Indicator>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_Indicator_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public List<c_Indicator> ListByOutcome(int outcomeID)
        {
            var list = new List<c_Indicator>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lo_sp_Indicator_ListByOutcome", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OutcomeID", outcomeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public int Delete(int indicatorID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Indicator_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns IndicatorID
            }
        }

        public c_Indicator GetByNameAndOutcome(string indicatorName, int outcomeID)
        {
            c_Indicator result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_Indicator_GetByNameAndOutcome", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorName", (object)indicatorName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OutcomeID", outcomeID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = Map(dr);
                    }
                }
            }
            return result;
        }

        // DAL/IndicatorDAL.cs (helper)
        public int? GetIndicatorIdByNameOutcome(string indicatorName, string outcomeName)
        {
            using (var con = Database.GetConnection())
            using (var cmd = new SqlCommand("gg_sp_Indicator_GetByNameOutcome", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorName", indicatorName);
                cmd.Parameters.AddWithValue("@OutcomeName", outcomeName);
                con.Open();
                var obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value) return null;
                return Convert.ToInt32(obj);
            }
        }

        private static c_Indicator Map(SqlDataReader dr)
        {
            return new c_Indicator
            {
                IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                IndicatorName = dr["IndicatorName"].ToString(),
                IndicatorType = dr["IndicatorType"].ToString(),
                OutcomeID = Convert.ToInt32(dr["OutcomeID"]),
                BaselineValue = dr["BaselineValue"] == DBNull.Value ? null : dr["BaselineValue"].ToString(),
                TermTargetValue = dr["TermTargetValue"] == DBNull.Value ? null : dr["TermTargetValue"].ToString(),
                AnnualBudget = dr["AnnualBudget"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["AnnualBudget"]),
                ImplementingInstitution = dr["ImplementingInstitution"] == DBNull.Value ? null : dr["ImplementingInstitution"].ToString(),
                SupportingInstitutions = dr["SupportingInstitutions"] == DBNull.Value ? null : dr["SupportingInstitutions"].ToString(),
                CalculationType = dr["CalculationType"] == DBNull.Value ? null : dr["CalculationType"].ToString(),
                ReportingCycle = dr["ReportingCycle"] == DBNull.Value ? null : dr["ReportingCycle"].ToString(),
                IsCumulative = dr["IsCumulative"] != DBNull.Value && Convert.ToBoolean(dr["IsCumulative"]),
                IsPercentage = dr["IsPercentage"] != DBNull.Value && Convert.ToBoolean(dr["IsPercentage"])
            };
        }
    }
}