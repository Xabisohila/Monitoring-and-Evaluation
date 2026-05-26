using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_NotificationsDAL
    {
        public int Upsert(c_Notification model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NotificationID", model.NotificationID == 0 ? (object)DBNull.Value : model.NotificationID);
                cmd.Parameters.AddWithValue("@ReportID", (object)model.ReportID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", (object)model.UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Message", (object)model.Message ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsRead", model.IsRead);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns NotificationID
            }
        }

        public c_Notification GetByID(int notificationID)
        {
            c_Notification result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationID", notificationID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_Notification> GetAll()
        {
            var list = new List<c_Notification>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_List", con))
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

        public List<c_Notification> ListByUser(int userID, bool onlyUnread = false)
        {
            var list = new List<c_Notification>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_ListByUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@OnlyUnread", onlyUnread ? 1 : 0);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public int MarkRead(int notificationID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_MarkRead", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationID", notificationID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns NotificationID
            }
        }

        public int MarkAllReadForUser(int userID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_MarkAllReadForUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns UserID
            }
        }

        public int GetUnreadCount(int userID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_UnreadCount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                object val = cmd.ExecuteScalar();
                return val == null || val == DBNull.Value ? 0 : Convert.ToInt32(val);
            }
        }

        public int Delete(int notificationID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_Notification_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationID", notificationID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns NotificationID
            }
        }

        private static c_Notification Map(SqlDataReader dr)
        {
            return new c_Notification
            {
                NotificationID = Convert.ToInt32(dr["NotificationID"]),
                ReportID = dr["ReportID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ReportID"]),
                UserID = dr["UserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UserID"]),
                Message = dr["Message"] == DBNull.Value ? null : dr["Message"].ToString(),
                SentDate = dr["SentDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["SentDate"]),
                IsRead = dr["IsRead"] != DBNull.Value && Convert.ToBoolean(dr["IsRead"])
            };
        }
    }
}