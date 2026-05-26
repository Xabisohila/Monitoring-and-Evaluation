using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_SystemPeriodDAL
    {
        public int Upsert(c_SystemPeriod model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_SystemPeriod_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PeriodID", model.PeriodID == 0 ? (object)DBNull.Value : model.PeriodID);
                cmd.Parameters.AddWithValue("@FinancialYear", model.FinancialYear);
                cmd.Parameters.AddWithValue("@Quarter", model.Quarter);
                cmd.Parameters.AddWithValue("@OpenDate", model.OpenDate);
                cmd.Parameters.AddWithValue("@CloseDate", model.CloseDate);
                cmd.Parameters.AddWithValue("@IsOpen", model.IsOpen);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns PeriodID
            }
        }

        public c_SystemPeriod GetByID(int periodID)
        {
            c_SystemPeriod result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_SystemPeriod_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PeriodID", periodID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public c_SystemPeriod Get(int financialYear, int quarter)
        {
            c_SystemPeriod result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_SystemPeriod_Get", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", financialYear);
                cmd.Parameters.AddWithValue("@Quarter", quarter);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_SystemPeriod> GetAll()
        {
            var list = new List<c_SystemPeriod>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_SystemPeriod_List", con))
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

        public int Delete(int periodID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_SystemPeriod_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PeriodID", periodID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns PeriodID
            }
        }

        private static c_SystemPeriod Map(SqlDataReader dr)
        {
            return new c_SystemPeriod
            {
                PeriodID = Convert.ToInt32(dr["PeriodID"]),
                FinancialYear = Convert.ToInt32(dr["FinancialYear"]),
                Quarter = Convert.ToInt32(dr["Quarter"]),
                OpenDate = dr["OpenDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["OpenDate"]),
                CloseDate = dr["CloseDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CloseDate"]),
                IsOpen = dr["IsOpen"] != DBNull.Value && Convert.ToBoolean(dr["IsOpen"])
            };
        }
    }
}
