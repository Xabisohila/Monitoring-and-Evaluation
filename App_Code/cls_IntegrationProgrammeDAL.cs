//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for cls_IntegrationProgrammeDAL
///// </summary>
//public class cls_IntegrationProgrammeDAL
//{
//    public cls_IntegrationProgrammeDAL()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}





using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class IntegrationProgrammeDAL
{
    private string connectionString;

    public IntegrationProgrammeDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataTable GetAllIntegrationProgrammes()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM new_IntegrationProgrammes WHERE IsActive = 1", conn))
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataTable GetByCluster(int clusterId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM new_IntegrationProgrammes WHERE ClusterID = @ClusterID AND IsActive = 1", conn))
        {
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataTable GetByPDP(int pdpId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM new_IntegrationProgrammes WHERE PDP_ID = @PDP_ID AND IsActive = 1", conn))
        {
            cmd.Parameters.AddWithValue("@PDP_ID", pdpId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public void InsertIntegrationProgramme(string name, int clusterId, int pdpId, string description)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_IntegrationProgrammes (ProgrammeName, ClusterID, PDP_ID, Description) VALUES (@Name, @ClusterID, @PDP_ID, @Description)", conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);
            cmd.Parameters.AddWithValue("@PDP_ID", pdpId);
            cmd.Parameters.AddWithValue("@Description", description);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
