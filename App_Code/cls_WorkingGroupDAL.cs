using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_WorkingGroupDAL
/// </summary>

public class cls_WorkingGroupDAL
{
    private string connectionString;

    public cls_WorkingGroupDAL()
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

    public DataTable GetInstitutionsByCluster(int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT InstitutionID, InstitutionName FROM new_ImplementationInstitutions WHERE ClusterID = @ClusterID ORDER BY InstitutionName", conn))
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

    public void InsertWorkingGroup(string name, string description, int leadInstitutionId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_WorkingGroups (WG_Name, WG_Description, LeadInstitutionID) VALUES (@Name, @Description, @LeadInstitutionID)", conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@LeadInstitutionID", leadInstitutionId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateWorkingGroup(int id, string name, string description, int leadInstitutionId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("UPDATE new_WorkingGroups SET WG_Name = @Name, WG_Description = @Description, LeadInstitutionID = @LeadInstitutionID WHERE WorkingGroupID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@LeadInstitutionID", leadInstitutionId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public DataRow GetWorkingGroupById(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT wg.WorkingGroupID, wg.WG_Name, wg.WG_Description, wg.LeadInstitutionID, ii.ClusterID FROM new_WorkingGroups wg JOIN new_ImplementationInstitutions ii ON wg.LeadInstitutionID = ii.InstitutionID WHERE wg.WorkingGroupID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
        }
    }
}
