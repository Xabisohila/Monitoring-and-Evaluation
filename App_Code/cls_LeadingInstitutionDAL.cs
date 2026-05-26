using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class cls_LeadingInstitutionDAL
{
    private string connectionString;

    public cls_LeadingInstitutionDAL()
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

    public bool InsertInstitution(string name, string type, int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_ImplementationInstitutions (InstitutionName, InstitutionType, ClusterID) VALUES (@Name, @Type, @ClusterID)", conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool UpdateInstitution(int id, string name, string type, int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("UPDATE new_ImplementationInstitutions SET InstitutionName = @Name, InstitutionType = @Type, ClusterID = @ClusterID WHERE InstitutionID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteInstitution(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("DELETE FROM new_ImplementationInstitutions WHERE InstitutionID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public DataRow GetInstitutionById(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT InstitutionID, InstitutionName, InstitutionType, ClusterID FROM new_ImplementationInstitutions WHERE InstitutionID = @ID", conn))
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
