using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    /// <summary>
    /// DAL for the AuditTrail table.
    /// - Insert-only for writes (no Update/Delete to preserve audit history).
    /// - Read helpers: GetByID, GetAll, ListByRecord.
    /// </summary>
    public class c_AuditTrailDAL
    {
        /// <summary>
        /// Insert a new audit entry (append-only).
        /// Uses: sp_AuditTrail_Insert
        /// Returns: new AuditID
        /// </summary>
        public int Insert(c_AuditTrail model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_AuditTrail_Insert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (object)model.UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ActionType", (object)model.ActionType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TableName", (object)model.TableName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RecordID", (object)model.RecordID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IPAddress", (object)model.IPAddress ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OldValue", (object)model.OldValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NewValue", (object)model.NewValue ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns AuditID
            }
        }

        /// <summary>
        /// Get a single audit entry by its ID.
        /// Uses: sp_AuditTrail_GetByID
        /// </summary>
        public c_AuditTrail GetByID(int auditID)
        {
            c_AuditTrail result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_AuditTrail_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AuditID", auditID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        /// <summary>
        /// List all audit entries (most recent first).
        /// Uses: sp_AuditTrail_List
        /// </summary>
        public List<c_AuditTrail> GetAll()
        {
            var list = new List<c_AuditTrail>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_AuditTrail_List", con))
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

        /// <summary>
        /// List audit entries for a specific table/record key.
        /// Uses: sp_AuditTrail_ListByRecord
        /// </summary>
        public List<c_AuditTrail> ListByRecord(string tableName, int recordID)
        {
            var list = new List<c_AuditTrail>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_AuditTrail_ListByRecord", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TableName", tableName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RecordID", recordID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        // ---- Private mapper ----
        private static c_AuditTrail Map(SqlDataReader dr)
        {
            return new c_AuditTrail
            {
                AuditID = Convert.ToInt32(dr["AuditID"]),
                UserID = dr["UserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UserID"]),
                ActionType = dr["ActionType"] == DBNull.Value ? null : dr["ActionType"].ToString(),
                TableName = dr["TableName"] == DBNull.Value ? null : dr["TableName"].ToString(),
                RecordID = dr["RecordID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["RecordID"]),
                ActionDate = dr["ActionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ActionDate"]),
                IPAddress = dr["IPAddress"] == DBNull.Value ? null : dr["IPAddress"].ToString(),
                OldValue = dr["OldValue"] == DBNull.Value ? null : dr["OldValue"].ToString(),
                NewValue = dr["NewValue"] == DBNull.Value ? null : dr["NewValue"].ToString()
            };
        }
    }
}