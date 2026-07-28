using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class d_POAUploadDAL
{
    private readonly string _conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    public int CreateRequest(int userId, string filePath, string goalName, string priorityFocus, string integrationProgramme, string impactStatement)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_CreateRequest", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadedByUserID", userId);
            cmd.Parameters.AddWithValue("@FilePath", filePath ?? "");
            cmd.Parameters.AddWithValue("@GoalName", (object)goalName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PriorityFocus", (object)priorityFocus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IntegrationProgramme", (object)integrationProgramme ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ImpactStatement", (object)impactStatement ?? DBNull.Value);
            var outId = new SqlParameter("@UploadRequestID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outId);
            cn.Open();
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(outId.Value);
        }
    }

    public void InsertRow(int requestId, string desiredOutcome, string outcomeIndicator, string indicatorType,
        string baselinePDP, string targetPDP2030, string implementingInstitution, string numberIE,
        string interventionName, string interventionIdText, string interventionIndicator,
        string baseline2023_24, string termTarget, string target2026_27, string annualBudget, string spatialRef)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_InsertRow", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID", requestId);
            cmd.Parameters.AddWithValue("@DesiredOutcome", (object)desiredOutcome ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OutcomeIndicator", (object)outcomeIndicator ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IndicatorType", (object)indicatorType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BaselinePDP", (object)baselinePDP ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TargetPDP2030", (object)targetPDP2030 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ImplementingInstitution", (object)implementingInstitution ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NumberIE", (object)numberIE ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InterventionName", (object)interventionName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InterventionID_Text", (object)interventionIdText ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InterventionIndicator", (object)interventionIndicator ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Baseline2023_24", (object)baseline2023_24 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TermTarget2025_2030", (object)termTarget ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Target2026_27", (object)target2026_27 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AnnualBudget", (object)annualBudget ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SpatialReference", (object)spatialRef ?? DBNull.Value);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public DataTable GetMyUploads(int userId)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetMyUploads", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UserID", userId);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataTable GetPendingUploads(int currentUserId)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetPendingUploads", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CurrentUserID", currentUserId);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataTable GetAllUploads()
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetAllUploads", cn) { CommandType = CommandType.StoredProcedure })
        {
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataRow GetUploadHeader(int requestId)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetUploadHeader", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID", requestId);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }

    public DataTable GetUploadData(int requestId)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetUploadData", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID", requestId);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public void Reject(int requestId, int reviewerUserId, string comment)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_Reject", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID", requestId);
            cmd.Parameters.AddWithValue("@ReviewerUserID", reviewerUserId);
            cmd.Parameters.AddWithValue("@ReviewComment", (object)comment ?? DBNull.Value);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void MarkApproved(int requestId, int reviewerUserId, string comment, int createdPoaId = 0)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_MarkApproved", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID", requestId);
            cmd.Parameters.AddWithValue("@ReviewerUserID", reviewerUserId);
            cmd.Parameters.AddWithValue("@ReviewComment", (object)comment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedPOA_ID", createdPoaId > 0 ? (object)createdPoaId : DBNull.Value);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void ClearAndReset(int requestId, int userId, string filePath,
        string goalName, string priorityFocus, string integrationProgramme, string impactStatement)
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_ClearAndReset", cn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@UploadRequestID",      requestId);
            cmd.Parameters.AddWithValue("@ResubmittedByUserID",  userId);
            cmd.Parameters.AddWithValue("@FilePath",             filePath ?? "");
            cmd.Parameters.AddWithValue("@GoalName",             (object)goalName             ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PriorityFocus",        (object)priorityFocus        ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IntegrationProgramme", (object)integrationProgramme ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ImpactStatement",      (object)impactStatement      ?? DBNull.Value);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public DataTable GetPrioritiesForMapping()
    {
        using (var cn = new SqlConnection(_conn))
        using (var cmd = new SqlCommand("sp_POAUpload_GetPrioritiesForMapping", cn) { CommandType = CommandType.StoredProcedure })
        {
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
