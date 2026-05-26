
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class cls_MunicipalityDAL
{
    private string connectionString;

    public cls_MunicipalityDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataTable GetAllDistricts()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT MunicipalityID, MunicipalityName FROM new_Municipalities ORDER BY MunicipalityName", conn))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public void InsertMunicipality(string name, int districtId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("INSERT INTO new_Municipalities (MunicipalityName, MunicipalityID) VALUES (@Name, @DistrictID)", conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@DistrictID", districtId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateMunicipality(int id, string name, int districtId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("UPDATE new_Municipalities SET MunicipalityName = @Name, DistrictID = @DistrictID WHERE MunicipalityID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@DistrictID", districtId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteMunicipality(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("DELETE FROM new_Municipalities WHERE MunicipalityID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public DataRow GetMunicipalityById(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT MunicipalityName, DistrictID FROM new_Municipalities WHERE MunicipalityID = @ID", conn))
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
