using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class cls_PriorityDAL
{
    private string connectionString;

    public cls_PriorityDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataTable GetAllPDPs()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT PDP_ID, PDP_Name FROM new_ProvincialDevelopmentPlans ORDER BY PDP_Name", conn))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
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

    public bool InsertPriority(int pdpId, int clusterId, string name, string description, string outcome)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_PMTDP_Priorities (PDP_ID, ClusterID, PriorityName, PriorityDescription, DesiredOutcome) VALUES (@PDP_ID, @ClusterID, @PriorityName, @PriorityDescription, @DesiredOutcome)", conn))
        {
            cmd.Parameters.AddWithValue("@PDP_ID", pdpId);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PriorityName", name);
            cmd.Parameters.AddWithValue("@PriorityDescription", description);
            cmd.Parameters.AddWithValue("@DesiredOutcome", outcome);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool UpdatePriority(int priorityId, int pdpId, int clusterId, string name, string description, string outcome)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("UPDATE new_PMTDP_Priorities SET PDP_ID = @PDP_ID, ClusterID = @ClusterID, PriorityName = @PriorityName, PriorityDescription = @PriorityDescription, DesiredOutcome = @DesiredOutcome WHERE PMTDP_PriorityID = @PriorityID", conn))
        {
            cmd.Parameters.AddWithValue("@PDP_ID", pdpId);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PriorityName", name);
            cmd.Parameters.AddWithValue("@PriorityDescription", description);
            cmd.Parameters.AddWithValue("@DesiredOutcome", outcome);
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool DeletePriority(int priorityId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("DELETE FROM new_PMTDP_Priorities WHERE PMTDP_PriorityID = @PriorityID", conn))
        {
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public DataTable GetPriorityById(int priorityId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM new_PMTDP_Priorities WHERE PMTDP_PriorityID = @PriorityID", conn))
        {
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    // Returns all PMTDP Priorities with their cluster name and PDP name for the list/assignment page.
    public DataTable GetAllPrioritiesWithCluster()
    {
        string sql = @"
            SELECT
                p.PMTDP_PriorityID,
                p.PriorityName,
                p.PriorityDescription,
                p.DesiredOutcome,
                p.ClusterID,
                c.ClusterName,
                p.PDP_ID,
                d.PDP_Name
            FROM new_PMTDP_Priorities p
            LEFT JOIN new_Clusters                  c ON c.ClusterID = p.ClusterID
            LEFT JOIN new_ProvincialDevelopmentPlans d ON d.PDP_ID   = p.PDP_ID
            ORDER BY c.ClusterName, p.PriorityName";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    // Updates only the ClusterID for a priority — used by the inline assignment page.
    public bool AssignCluster(int priorityId, int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(
            "UPDATE new_PMTDP_Priorities SET ClusterID = @ClusterID WHERE PMTDP_PriorityID = @PriorityID", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PriorityID", priorityId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

