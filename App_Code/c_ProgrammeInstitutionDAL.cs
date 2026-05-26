using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for c_ProgrammeInstitutionDAL
/// </summary>
public class c_ProgrammeInstitutionDAL
{
   
    private readonly string connectionString;

    public c_ProgrammeInstitutionDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public List<c_ProgrammeInstitution> GetByProgrammeID(int programmeID)
    {
        var list = new List<c_ProgrammeInstitution>();
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand("SELECT ProgrammeInstitutionID, ProgrammeID, InstitutionID, Role FROM i_ProgrammeInstitutions WHERE ProgrammeID = @ProgrammeID", conn))
            {
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new c_ProgrammeInstitution
                        {
                            ProgrammeInstitutionID = reader.GetInt32(0),
                            ProgrammeID = reader.GetInt32(1),
                            InstitutionID = reader.GetInt32(2),
                            Role = reader.IsDBNull(3) ? null : reader.GetString(3)
                        });
                    }
                }
            }
        }
        return list;
    }

    public void Insert(c_ProgrammeInstitution model)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand("INSERT INTO i_ProgrammeInstitutions (ProgrammeID, InstitutionID, Role) VALUES (@ProgrammeID, @InstitutionID, @Role)", conn))
            {
                cmd.Parameters.AddWithValue("@ProgrammeID", model.ProgrammeID);
                cmd.Parameters.AddWithValue("@InstitutionID", model.InstitutionID);
                cmd.Parameters.AddWithValue("@Role", (object)model.Role ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int programmeInstitutionID)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand("DELETE FROM i_ProgrammeInstitutions WHERE ProgrammeInstitutionID = @ProgrammeInstitutionID", conn))
            {
                cmd.Parameters.AddWithValue("@ProgrammeInstitutionID", programmeInstitutionID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
