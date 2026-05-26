using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_QuarterlyTargetsDAL
    {
        public int Upsert(c_QuarterlyTarget model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyTarget_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QuarterlyTargetID", model.QuarterlyTargetID == 0 ? (object)DBNull.Value : model.QuarterlyTargetID);
                cmd.Parameters.AddWithValue("@AnnualTargetID", model.AnnualTargetID);
                cmd.Parameters.AddWithValue("@QuarterNumber", model.QuarterNumber);
                cmd.Parameters.AddWithValue("@TargetValue", (object)model.TargetValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SpatialReference", (object)model.SpatialReference ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns QuarterlyTargetID
            }
        }

        public int UpsertDept(int indicatorId, int financialYear, int quarterNumber, string targetValue, string spatialReference)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("iii_sp_QuarterlyTarget_UpsertDept", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IndicatorID", indicatorId);
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
                cmd.Parameters.AddWithValue("@QuarterNumber", quarterNumber);
                cmd.Parameters.AddWithValue("@TargetValue", (object)targetValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SpatialReference", (object)spatialReference ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns QuarterlyTargetID
            }
        }

        public c_QuarterlyTarget GetByID(int quarterlyTargetID)
        {
            c_QuarterlyTarget result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_QuarterlyTarget_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuarterlyTargetID", quarterlyTargetID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_QuarterlyTarget> GetAll()
        {
            var list = new List<c_QuarterlyTarget>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_QuarterlyTarget_List", con))
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

        public List<c_QuarterlyTarget> ListByAnnual(int annualTargetID)
        {
            var list = new List<c_QuarterlyTarget>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("la_sp_QuarterlyTarget_ListByAnnual", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AnnualTargetID", annualTargetID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public c_QuarterlyTarget GetByIndicatorYearQuarter(int indicatorID, int financialYear, int quarterNumber)
        {
            c_QuarterlyTarget result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_QuarterlyTarget_GetByIndicatorYearQuarter", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
                cmd.Parameters.AddWithValue("@QuarterNumber", quarterNumber);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public int Delete(int quarterlyTargetID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyTarget_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuarterlyTargetID", quarterlyTargetID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns QuarterlyTargetID
            }
        }

        private static c_QuarterlyTarget Map(SqlDataReader dr)
        {
            return new c_QuarterlyTarget
            {
                QuarterlyTargetID = Convert.ToInt32(dr["QuarterlyTargetID"]),
                AnnualTargetID = Convert.ToInt32(dr["AnnualTargetID"]),
                QuarterNumber = Convert.ToInt32(dr["QuarterNumber"]),
                TargetValue = dr["TargetValue"] == DBNull.Value ? null : dr["TargetValue"].ToString(),
                SpatialReference = dr["SpatialReference"] == DBNull.Value ? null : dr["SpatialReference"].ToString()
            };
        }
    }
}