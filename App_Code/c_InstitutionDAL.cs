using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// Summary description for c_InstitutionDAL
/// </summary>
public class c_InstitutionDAL
{

    private readonly string connectionString;

    public c_InstitutionDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public List<c_Institution> GetAll()
    {
        var list = new List<c_Institution>();
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand("SELECT InstitutionID, InstitutionName, InstitutionCode, IsActive FROM i_Institutions WHERE IsActive = 1", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new c_Institution
                        {
                            InstitutionID = reader.GetInt32(0),
                            InstitutionName = reader.GetString(1),
                            InstitutionCode = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        });
                    }
                }
            }
        }
        return list;
    }

    public c_Institution GetByID(int institutionID)
    {
        c_Institution inst = null;
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand("SELECT InstitutionID, InstitutionName, InstitutionCode, IsActive FROM i_Institutions WHERE InstitutionID = @InstitutionID", conn))
            {
                cmd.Parameters.AddWithValue("@InstitutionID", institutionID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inst = new c_Institution
                        {
                            InstitutionID = reader.GetInt32(0),
                            InstitutionName = reader.GetString(1),
                            InstitutionCode = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        };
                    }
                }
            }
        }
        return inst;
    }

    public List<c_Institution> GetByProgrammeID(int programmeID)
    {
        var list = new List<c_Institution>();
        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = new SqlCommand(@"
                SELECT i.InstitutionID, i.InstitutionName, i.InstitutionCode, i.IsActive 
                FROM i_Institutions i
                INNER JOIN i_ProgrammeInstitutions pi ON i.InstitutionID = pi.InstitutionID
                WHERE pi.ProgrammeID = @ProgrammeID AND i.IsActive = 1", conn))
            {
                cmd.Parameters.AddWithValue("@ProgrammeID", programmeID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new c_Institution
                        {
                            InstitutionID = reader.GetInt32(0),
                            InstitutionName = reader.GetString(1),
                            InstitutionCode = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        });
                    }
                }
            }
        }
        return list;
    }
}


