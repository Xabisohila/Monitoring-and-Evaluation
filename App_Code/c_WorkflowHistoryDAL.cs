using MnE2.DAL;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_WorkflowHistoryDAL
    {
        public int Upsert(c_WorkflowHistory model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_WorkflowHistory_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HistoryID", model.HistoryID == 0 ? (object)DBNull.Value : model.HistoryID);
                cmd.Parameters.AddWithValue("@ReportID", model.ReportID);
                cmd.Parameters.AddWithValue("@StatusID", model.StatusID);
                cmd.Parameters.AddWithValue("@Stage", (object)model.Stage ?? DBNull.Value);          // "QA" | "Approval" | "Signoff"
                cmd.Parameters.AddWithValue("@ActionByUserID", model.ActionByUserID);
                cmd.Parameters.AddWithValue("@Comments", (object)model.Comments ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns HistoryID
            }
        }

        public c_WorkflowHistory GetByID(int historyID)
        {
            c_WorkflowHistory result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_WorkflowHistory_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HistoryID", historyID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_WorkflowHistory> GetAll()
        {
            var list = new List<c_WorkflowHistory>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_WorkflowHistory_List", con))
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

        public List<c_WorkflowHistory> ListByReport(int reportID)
        {
            var list = new List<c_WorkflowHistory>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("lr_sp_WorkflowHistory_ListByReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // This SP returns extra joined columns (StatusName, ActionByName).
                        list.Add(new c_WorkflowHistory
                        {
                            HistoryID = Convert.ToInt32(dr["HistoryID"]),
                            ReportID = Convert.ToInt32(dr["ReportID"]),
                            StatusID = Convert.ToInt32(dr["StatusID"]),
                            Stage = dr["Stage"].ToString(),
                            ActionByUserID = Convert.ToInt32(dr["ActionByUserID"]),
                            ActionDate = dr["ActionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ActionDate"]),
                            Comments = dr["Comments"] == DBNull.Value ? null : dr["Comments"].ToString()
                            // If you want to expose StatusName/ActionByName, add those to your model or a view-model.
                        });
                    }
                }
            }
            return list;
        }

        // --- Stage helper methods (atomic) ---

        public int SetQA(int reportID, int statusID, int actionByUserID, string comments = null)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Workflow_QA_Set", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);
                cmd.Parameters.AddWithValue("@StatusID", statusID);
                cmd.Parameters.AddWithValue("@ActionByUserID", 33);// actionByUserID);
                cmd.Parameters.AddWithValue("@Comments", (object)comments ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns HistoryID
            }
        }

        public int SetApproval(int reportID, int statusID, int actionByUserID, string comments = null)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Workflow_Approval_Set", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);
                cmd.Parameters.AddWithValue("@StatusID", statusID);
                cmd.Parameters.AddWithValue("@ActionByUserID", actionByUserID);
                cmd.Parameters.AddWithValue("@Comments", (object)comments ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns HistoryID
            }
        }

        public int SetSignoff(int reportID, int statusID, int actionByUserID, string comments = null)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_Workflow_Signoff_Set", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);
                cmd.Parameters.AddWithValue("@StatusID", statusID);
                cmd.Parameters.AddWithValue("@ActionByUserID", actionByUserID);
                cmd.Parameters.AddWithValue("@Comments", (object)comments ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns HistoryID
            }
        }

        private static c_WorkflowHistory Map(SqlDataReader dr)
        {
            return new c_WorkflowHistory
            {
                HistoryID = Convert.ToInt32(dr["HistoryID"]),
                ReportID = Convert.ToInt32(dr["ReportID"]),
                StatusID = Convert.ToInt32(dr["StatusID"]),
                Stage = dr["Stage"].ToString(),
                ActionByUserID = Convert.ToInt32(dr["ActionByUserID"]),
                ActionDate = dr["ActionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ActionDate"]),
                Comments = dr["Comments"] == DBNull.Value ? null : dr["Comments"].ToString()
            };
        }
    }
}