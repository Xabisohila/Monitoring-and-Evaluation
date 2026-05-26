using System;
using System.Data;
using System.Data.SqlClient;

namespace MnE2.DAL
{
    public class d_PMTDPUploadDataDAL
    {
        public void InsertUploadData(int uploadId, DataRow r, string proposedAction)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_InsertUploadData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UploadRequestID", uploadId);
                cmd.Parameters.AddWithValue("@PriorityName",           Col(r, "PriorityName"));
                cmd.Parameters.AddWithValue("@ProgrammeName",          Col(r, "ProgrammeName"));
                cmd.Parameters.AddWithValue("@LeaderDeptName",         Col(r, "LeaderDeptName"));
                cmd.Parameters.AddWithValue("@OutcomeName",            Col(r, "OutcomeName"));
                cmd.Parameters.AddWithValue("@IndicatorName",          Col(r, "IndicatorName"));
                cmd.Parameters.AddWithValue("@IndicatorType",          Col(r, "IndicatorType"));
                cmd.Parameters.AddWithValue("@BaselineValue",          Col(r, "BaselineValue"));
                cmd.Parameters.AddWithValue("@TermTargetValue",        Col(r, "TermTargetValue"));
                cmd.Parameters.AddWithValue("@AnnualBudget",           Col(r, "AnnualBudget"));
                cmd.Parameters.AddWithValue("@ImplementingInstitution",Col(r, "ImplementingInstitution"));
                cmd.Parameters.AddWithValue("@SupportingInstitutions", Col(r, "SupportingInstitutions"));
                cmd.Parameters.AddWithValue("@IsCumulative",           YesNoToBit(r, "IsCumulative"));
                cmd.Parameters.AddWithValue("@IsPercentage",           YesNoToBit(r, "IsPercentage"));
                // Intervention-level fields (new)
                cmd.Parameters.AddWithValue("@InterventionName",       Col(r, "InterventionName"));
                cmd.Parameters.AddWithValue("@InterventionIndicator",  Col(r, "InterventionIndicator"));
                cmd.Parameters.AddWithValue("@Baseline2023_24",        Col(r, "Baseline2023_24"));
                cmd.Parameters.AddWithValue("@TermTarget2030",         Col(r, "TermTarget2030"));
                cmd.Parameters.AddWithValue("@TermBudget",             Col(r, "TermBudget"));
                cmd.Parameters.AddWithValue("@AnnualTarget",           Col(r, "AnnualTarget"));
                cmd.Parameters.AddWithValue("@SpatialReference",       Col(r, "SpatialReference"));
                cmd.Parameters.AddWithValue("@ProposedAction",         proposedAction);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Calls n_sp_PMTDP_ApplyApprovedRow for a single staging row.
        // The SP upserts into both i_* monitoring tables and new_PMTDP_Priorities.
        public void ApplyApprovedRow(int uploadDataID, int approvedByUserID)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_ApplyApprovedRow", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadDataID",     uploadDataID);
                cmd.Parameters.AddWithValue("@ApprovedByUserID", approvedByUserID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Returns the column value, or DBNull if the column is absent / empty.
        private static object Col(DataRow r, string name)
        {
            if (!r.Table.Columns.Contains(name)) return DBNull.Value;
            object v = r[name];
            return (v == null || v is DBNull || string.IsNullOrWhiteSpace(v.ToString()))
                ? (object)DBNull.Value : v;
        }

        // Converts "Yes"/"No"/"1"/"0"/"true"/"false" Excel cell values to bool.
        private static bool YesNoToBit(DataRow r, string name)
        {
            if (!r.Table.Columns.Contains(name)) return false;
            string s = (r[name] ?? "").ToString().Trim();
            return s.Equals("Yes",  StringComparison.OrdinalIgnoreCase)
                || s.Equals("true", StringComparison.OrdinalIgnoreCase)
                || s == "1";
        }

        public DataTable GetUploadData(int uploadId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("n_sp_PMTDP_GetUploadData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadRequestID", uploadId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}