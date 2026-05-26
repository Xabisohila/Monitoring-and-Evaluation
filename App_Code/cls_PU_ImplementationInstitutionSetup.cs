using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for cls_PU_ImplementationInstitutionSetup
/// </summary>

    public class cls_PU_ImplementationInstitutionSetup
    {
        private readonly string _connString;

        public cls_PU_ImplementationInstitutionSetup()
        {
            _connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(_connString);
    }

    public List<ImplementationInstitution> GetAll()
        {
            var list = new List<ImplementationInstitution>();
            const string sql = @"
                SELECT i.InstitutionID, i.InstitutionName, i.InstitutionType, i.ClusterID,
                       c.ClusterName AS ClusterName
                FROM dbo.new_ImplementationInstitutions i
                LEFT JOIN dbo.new_Clusters c ON c.ClusterID = i.ClusterID
                ORDER BY i.InstitutionID;";

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new ImplementationInstitution
                        {
                            InstitutionID = rdr.GetInt32(0),
                            InstitutionName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            InstitutionType = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            ClusterID = rdr.GetInt32(3),
                            ClusterName = rdr.IsDBNull(4) ? null : rdr.GetString(4)
                        });
                    }
                }
            }
            return list;
        }

        public ImplementationInstitution GetById(int id)
        {
            const string sql = @"
                SELECT i.InstitutionID, i.InstitutionName, i.InstitutionType, i.ClusterID,
                       c.ClusterName AS ClusterName
                FROM dbo.new_ImplementationInstitutions i
                LEFT JOIN dbo.new_Clusters c ON c.ClusterID = i.ClusterID
                WHERE i.InstitutionID = @id;";

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new ImplementationInstitution
                        {
                            InstitutionID = rdr.GetInt32(0),
                            InstitutionName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            InstitutionType = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            ClusterID = rdr.GetInt32(3),
                            ClusterName = rdr.IsDBNull(4) ? null : rdr.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        public int Create(ImplementationInstitution i)
        {
            const string sql = @"
                INSERT INTO dbo.new_ImplementationInstitutions
                    (InstitutionName, InstitutionType, ClusterID)
                OUTPUT INSERTED.InstitutionID
                VALUES (@name, @type, @clusterId);";

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = (object)i.InstitutionName ?? DBNull.Value;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 100).Value = (object)i.InstitutionType ?? DBNull.Value;
                cmd.Parameters.Add("@clusterId", SqlDbType.Int).Value = i.ClusterID;

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public bool Update(ImplementationInstitution i)
        {
            const string sql = @"
                UPDATE dbo.new_ImplementationInstitutions
                SET InstitutionName = @name,
                    InstitutionType = @type,
                    ClusterID = @clusterId
                WHERE InstitutionID = @id;";

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = (object)i.InstitutionName ?? DBNull.Value;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 100).Value = (object)i.InstitutionType ?? DBNull.Value;
                cmd.Parameters.Add("@clusterId", SqlDbType.Int).Value = i.ClusterID;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = i.InstitutionID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            const string sql = @"DELETE FROM dbo.new_ImplementationInstitutions WHERE InstitutionID = @id;";
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

public class ImplementationInstitution
{
    public int InstitutionID { get; set; }
    public string InstitutionName { get; set; }
    public string InstitutionType { get; set; }
    public int ClusterID { get; set; }

    // Convenience for UI (join with clusters)
    public string ClusterName { get; set; }
}