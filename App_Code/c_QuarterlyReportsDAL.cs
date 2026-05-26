using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_QuarterlyReportsDAL
    {
        public int Upsert(c_QuarterlyReport model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReportID", model.ReportID == 0 ? (object)DBNull.Value : model.ReportID);
                cmd.Parameters.AddWithValue("@QuarterlyTargetID", model.QuarterlyTargetID);
                cmd.Parameters.AddWithValue("@SubmittedByUserID", (object)model.SubmittedByUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@QuarterNumber", model.QuarterNumber);
                cmd.Parameters.AddWithValue("@ActualValue", (object)model.ActualValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Achieved", model.Achieved);
                cmd.Parameters.AddWithValue("@DeviationReason", (object)model.DeviationReason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RemedialActions", (object)model.RemedialActions ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OverAchieveReason", (object)model.OverAchieveReason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RemedialDueDate", (object)model.RemedialDueDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SpatialReference", (object)model.SpatialReference ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns ReportID
            }
        }

        public c_QuarterlyReport GetByID(int reportID)
        {
            c_QuarterlyReport result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_QuarterlyReport> GetAll()
        {
            var list = new List<c_QuarterlyReport>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_QuarterlyReport_List", con))
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

        public List<c_QuarterlyReport> ListByQuarterlyTarget(int quarterlyTargetID)
        {
            var list = new List<c_QuarterlyReport>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_ListByQuarterlyTarget", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuarterlyTargetID", quarterlyTargetID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public List<c_QuarterlyReport> ListByYearQuarter(int financialYear, int quarterNumber)
        {
            var list = new List<c_QuarterlyReport>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_ListByYearQuarter", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
                cmd.Parameters.AddWithValue("@QuarterNumber", quarterNumber);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public int Delete(int reportID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns ReportID
            }
        }

        //public List<c_QuarterlyReport> GetByTarget(int quarterlyTargetID)
        //{
        //    var list = new List<c_QuarterlyReport>();

        //    using (SqlConnection con = Database.GetConnection())
        //    using (SqlCommand cmd = new SqlCommand("i_sp_QuarterlyReport_GetByTarget", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@QuarterlyTargetID", quarterlyTargetID);

        //        con.Open();
        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //                list.Add(Map(dr));
        //        }
        //    }
        //    return list;
        //}

        private static c_QuarterlyReport Map(SqlDataReader dr)
        {
            return new c_QuarterlyReport
            {
                ReportID = Convert.ToInt32(dr["ReportID"]),
                QuarterlyTargetID = Convert.ToInt32(dr["QuarterlyTargetID"]),
                SubmittedByUserID = dr["SubmittedByUserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SubmittedByUserID"]),
                SubmittedDate = dr["SubmittedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["SubmittedDate"]),
                QuarterNumber = dr["QuarterNumber"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuarterNumber"]),
                ActualValue = dr["ActualValue"] == DBNull.Value ? null : dr["ActualValue"].ToString(),
                Achieved = dr["Achieved"] != DBNull.Value && Convert.ToBoolean(dr["Achieved"]),
                DeviationReason = dr["DeviationReason"] == DBNull.Value ? null : dr["DeviationReason"].ToString(),
                RemedialActions = dr["RemedialActions"] == DBNull.Value ? null : dr["RemedialActions"].ToString(),
                OverAchieveReason = dr["OverAchieveReason"] == DBNull.Value ? null : dr["OverAchieveReason"].ToString(),
                RemedialDueDate = dr["RemedialDueDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RemedialDueDate"]),
                SpatialReference = dr["SpatialReference"] == DBNull.Value ? null : dr["SpatialReference"].ToString()
            };
        }
    }
}