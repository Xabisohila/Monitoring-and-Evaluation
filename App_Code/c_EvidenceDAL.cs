using MnE2.DAL;
using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MnE2.DAL
{
    public class c_EvidenceDAL
    {
        public int Upsert(c_EvidenceFile model)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_EvidenceFile_Upsert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EvidenceID", model.EvidenceID == 0 ? (object)DBNull.Value : model.EvidenceID);
                cmd.Parameters.AddWithValue("@ReportID", model.ReportID);
                cmd.Parameters.AddWithValue("@FileName", (object)model.FileName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FilePath", (object)model.FilePath ?? DBNull.Value);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns EvidenceID
            }
        }

        public c_EvidenceFile GetByID(int evidenceID)
        {
            c_EvidenceFile result = null;

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("g_sp_EvidenceFile_GetByID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EvidenceID", evidenceID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        result = Map(dr);
                }
            }
            return result;
        }

        public List<c_EvidenceFile> GetAll()
        {
            var list = new List<c_EvidenceFile>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("l_sp_EvidenceFile_List", con))
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

        public List<c_EvidenceFile> ListByReport(int reportID)
        {
            var list = new List<c_EvidenceFile>();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_EvidenceFile_ListByReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportID", reportID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(Map(dr));
                }
            }
            return list;
        }

        public int Delete(int evidenceID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("i_sp_EvidenceFile_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EvidenceID", evidenceID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()); // returns EvidenceID
            }
        }

        private static c_EvidenceFile Map(SqlDataReader dr)
        {
            return new c_EvidenceFile
            {
                EvidenceID = Convert.ToInt32(dr["EvidenceID"]),
                ReportID = Convert.ToInt32(dr["ReportID"]),
                FileName = dr["FileName"] == DBNull.Value ? null : dr["FileName"].ToString(),
                FilePath = dr["FilePath"] == DBNull.Value ? null : dr["FilePath"].ToString(),
                UploadedDate = dr["UploadedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UploadedDate"])
            };
        }
    }
}