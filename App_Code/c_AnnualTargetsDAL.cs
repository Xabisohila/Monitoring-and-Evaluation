using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_AnnualTargetsDAL
    {
        public int Upsert(c_AnnualTarget model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_AnnualTarget_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AnnualTargetID", model.AnnualTargetID == 0 ? (object)DBNull.Value : model.AnnualTargetID);
                cmd.Parameters.AddWithValue("@IndicatorID", model.IndicatorID);
                cmd.Parameters.AddWithValue("@FinancialYear", model.FinancialYear);
                cmd.Parameters.AddWithValue("@AnnualTargetValue", (object)model.AnnualTargetValue ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns AnnualTargetID
            }
        }

        // DAL/AnnualTargetsDAL.cs
        public int UpsertIfProvided(int indicatorId, int financialYear, string annualValueOrNull)
        {
            using (var con = Database.GetConnection())
            using (var cmd = new SqlCommand("iii_sp_AnnualTarget_UpsertIfProvided", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorId);
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
                cmd.Parameters.AddWithValue("@AnnualTargetValue",
                    string.IsNullOrWhiteSpace(annualValueOrNull) ? (object)DBNull.Value : annualValueOrNull);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // -1 when skipped
            }
        }

        public c_AnnualTarget GetByID(int annualTargetID)
        {
            c_AnnualTarget result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_AnnualTarget_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AnnualTargetID", annualTargetID);

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

        public List<c_AnnualTarget> GetAll()
        {
            var list = new List<c_AnnualTarget>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_AnnualTarget_List", con))
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

        public List<c_AnnualTarget> ListByIndicator(int indicatorID)
        {
            var list = new List<c_AnnualTarget>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_AnnualTarget_ListByIndicator", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public c_AnnualTarget GetByIndicatorYear(int indicatorID, int financialYear)
        {
            c_AnnualTarget result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_AnnualTarget_GetByIndicatorYear", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public int Delete(int annualTargetID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_AnnualTarget_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AnnualTargetID", annualTargetID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns AnnualTargetID
            }
        }

        private static c_AnnualTarget Map(SqlDataReader dr)
        {
            return new c_AnnualTarget
            {
                AnnualTargetID = Convert.ToInt32(dr["AnnualTargetID"]),
                IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                FinancialYear = Convert.ToInt32(dr["FinancialYear"]),
                AnnualTargetValue = dr["AnnualTargetValue"] == DBNull.Value ? null : dr["AnnualTargetValue"].ToString()
            };
        }
    }
}