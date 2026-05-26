using System;
using System.Data;
using System.Data.SqlClient;

namespace MnE2.DAL
{
    public class d_PMTDPUploadDAL
    {
        public int CreateUploadRequest(int userId, string filePath)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_CreateUploadRequest", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadedByUserID", userId);
                cmd.Parameters.AddWithValue("@FilePath", filePath);

                SqlParameter outId = new SqlParameter("@UploadRequestID", SqlDbType.Int);
                outId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outId);

                con.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(outId.Value);
            }
        }

        public DataTable GetPendingUploads(int currentUserId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_GetPendingUploads", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CurrentUserID", currentUserId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public UploadHeader GetUploadHeader(int uploadId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_GetUploadHeader", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadRequestID", uploadId);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;

                    return new UploadHeader
                    {
                        UploadRequestID = uploadId,
                        UploadedByUserID = Convert.ToInt32(dr["UploadedByUserID"]),
                        Status = dr["Status"].ToString()
                    };
                }
            }
        }

        public DataTable GetMyUploads(int userId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_GetMyUploads", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void ReviewUpload(
            int uploadId,
            int reviewerUserId,
            string decision,
            string comment)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_ReviewUpload", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadRequestID", uploadId);
                cmd.Parameters.AddWithValue("@ReviewerUserID", reviewerUserId);
                cmd.Parameters.AddWithValue("@Decision", decision);
                cmd.Parameters.AddWithValue("@Comment", (object)comment ?? DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class UploadHeader
    {
        public int UploadRequestID { get; set; }
        public int UploadedByUserID { get; set; }
        public string Status { get; set; }
    }
}