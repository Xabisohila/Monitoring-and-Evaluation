using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class Cluster
{
    public int ClusterID { get; set; }
    public string ClusterName { get; set; }
    public string ClusterDescription { get; set; }
}

public class cls_PU_ClusterSetup
{
    public int ClusterID { get; set; }
    public string ClusterName { get; set; }
    public string ClusterDescription { get; set; }

    private readonly string _connectionString;

    public cls_PU_ClusterSetup()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public List<Cluster> GetAll()
    {
        var list = new List<Cluster>();
        const string sql = @"SELECT ClusterID, ClusterName, ClusterDescription
                                 FROM dbo.new_Clusters
                                 ORDER BY ClusterID";

        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            conn.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    list.Add(new Cluster
                    {
                        ClusterID = rdr.GetInt32(0),
                        ClusterName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                        ClusterDescription = rdr.IsDBNull(2) ? null : rdr.GetString(2)
                    });
                }
            }
        }
        return list;
    }

    public Cluster GetById(int id)
    {
        const string sql = @"SELECT ClusterID, ClusterName, ClusterDescription
                                 FROM dbo.new_Clusters
                                 WHERE ClusterID = @id";
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            conn.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    return new Cluster
                    {
                        ClusterID = rdr.GetInt32(0),
                        ClusterName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                        ClusterDescription = rdr.IsDBNull(2) ? null : rdr.GetString(2)
                    };
                }
            }
        }
        return null;
    }

    public int Create(Cluster c)
    {
        const string sql = @"INSERT INTO dbo.new_Clusters (ClusterName, ClusterDescription)
                                 OUTPUT INSERTED.ClusterID
                                 VALUES (@name, @desc)";
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = (object)c.ClusterName ?? DBNull.Value;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 500).Value = (object)c.ClusterDescription ?? DBNull.Value;

            conn.Open();
            var newId = (int)cmd.ExecuteScalar();
            return newId;
        }
    }

    public bool Update(Cluster c)
    {
        const string sql = @"UPDATE dbo.new_Clusters
                                 SET ClusterName = @name,
                                     ClusterDescription = @desc
                                 WHERE ClusterID = @id";
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = (object)c.ClusterName ?? DBNull.Value;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 500).Value = (object)c.ClusterDescription ?? DBNull.Value;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = c.ClusterID;

            conn.Open();
            var rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }

    public bool Delete(int id)
    {
        const string sql = @"DELETE FROM dbo.new_Clusters WHERE ClusterID = @id";
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            conn.Open();
            var rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }
}