using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class cls_SubOutcomeDAL
{
    private string connectionString;

    public cls_SubOutcomeDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataTable GetAllClusters()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT ClusterID, ClusterName FROM new_Clusters ORDER BY ClusterName", conn))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public DataTable GetPrioritiesByCluster(int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT PMTDP_PriorityID, PriorityName FROM new_PMTDP_Priorities WHERE ClusterID = @ClusterID ORDER BY PriorityName", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public bool InsertSubOutcome(int clusterId, int priorityId, string subOutcome)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_SubOutcomes (ClusterID, PMTDP_PriorityID, SubOutcome) VALUES (@ClusterID, @PriorityID, @SubOutcome)", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            cmd.Parameters.AddWithValue("@SubOutcome", subOutcome);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }
}