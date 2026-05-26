using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

// Strongly-typed model matching table columns and common display needs.
public class WorkingGroup
{
    public int WorkingGroupID { get; set; }
    public string WG_Name { get; set; }
    public string WG_Description { get; set; }
    public int LeadInstitutionID { get; set; }
    public int ClusterID { get; set; }
    public string ClusterName { get; set; }
    public string InstitutionName { get; set; }
}

/// <summary>
/// Data access for Working Groups (CRUD), aligned with cls_PU_ClusterSetup style.
/// </summary>
public class cls_PU_WGSetup
{
    private readonly string _connectionString;

    public cls_PU_WGSetup()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    // Auxiliary lookups (kept from existing code)
    public DataTable GetAllClusters()
    {
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand("SELECT ClusterID, ClusterName FROM new_Clusters ORDER BY ClusterName", conn))
        using (var da = new SqlDataAdapter(cmd))
        {
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public DataTable GetInstitutionsByCluster(int clusterId)
    {
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(@"SELECT InstitutionID, InstitutionName 
                                          FROM new_ImplementationInstitutions 
                                          WHERE ClusterID = @ClusterID 
                                          ORDER BY InstitutionName", conn))
        using (var da = new SqlDataAdapter(cmd))
        {
            cmd.Parameters.Add("@ClusterID", SqlDbType.Int).Value = clusterId;
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    // CRUD similar to cls_PU_ClusterSetup

    public List<WorkingGroup> GetAll()
    {
        const string sql = @"
            SELECT 
                wg.WorkingGroupID,
                wg.WG_Name,
                wg.WG_Description,
                wg.LeadInstitutionID,
                ii.InstitutionName,
                ii.ClusterID,
                c.ClusterName
            FROM dbo.new_WorkingGroups wg
            INNER JOIN dbo.new_ImplementationInstitutions ii ON wg.LeadInstitutionID = ii.InstitutionID
            INNER JOIN dbo.new_Clusters c ON ii.ClusterID = c.ClusterID
            ORDER BY c.ClusterName, ii.InstitutionName, wg.WG_Name";

        var list = new List<WorkingGroup>();
        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            conn.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    list.Add(new WorkingGroup
                    {
                        WorkingGroupID = rdr.GetInt32(0),
                        WG_Name = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                        WG_Description = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                        LeadInstitutionID = rdr.GetInt32(3),
                        InstitutionName = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                        ClusterID = rdr.GetInt32(5),
                        ClusterName = rdr.IsDBNull(6) ? null : rdr.GetString(6)
                    });
                }
            }
        }
        return list;
    }

    public WorkingGroup GetById(int id)
    {
        const string sql = @"
            SELECT 
                wg.WorkingGroupID,
                wg.WG_Name,
                wg.WG_Description,
                wg.LeadInstitutionID,
                ii.InstitutionName,
                ii.ClusterID,
                c.ClusterName
            FROM dbo.new_WorkingGroups wg
            INNER JOIN dbo.new_ImplementationInstitutions ii ON wg.LeadInstitutionID = ii.InstitutionID
            INNER JOIN dbo.new_Clusters c ON ii.ClusterID = c.ClusterID
            WHERE wg.WorkingGroupID = @id";

        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            conn.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    return new WorkingGroup
                    {
                        WorkingGroupID = rdr.GetInt32(0),
                        WG_Name = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                        WG_Description = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                        LeadInstitutionID = rdr.GetInt32(3),
                        InstitutionName = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                        ClusterID = rdr.GetInt32(5),
                        ClusterName = rdr.IsDBNull(6) ? null : rdr.GetString(6)
                    };
                }
            }
        }
        return null;
    }

    public int Create(WorkingGroup wg)
    {
        const string sql = @"
            INSERT INTO dbo.new_WorkingGroups (WG_Name, WG_Description, LeadInstitutionID)
            OUTPUT INSERTED.WorkingGroupID
            VALUES (@name, @desc, @leadInstitutionId)";

        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = (object)wg.WG_Name ?? DBNull.Value;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 1000).Value = (object)wg.WG_Description ?? DBNull.Value;
            cmd.Parameters.Add("@leadInstitutionId", SqlDbType.Int).Value = wg.LeadInstitutionID;

            conn.Open();
            var newId = (int)cmd.ExecuteScalar();
            return newId;
        }
    }

    public bool Update(WorkingGroup wg)
    {
        const string sql = @"
            UPDATE dbo.new_WorkingGroups
            SET WG_Name = @name,
                WG_Description = @desc,
                LeadInstitutionID = @leadInstitutionId
            WHERE WorkingGroupID = @id";

        using (var conn = GetConnection())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = (object)wg.WG_Name ?? DBNull.Value;
            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 1000).Value = (object)wg.WG_Description ?? DBNull.Value;
            cmd.Parameters.Add("@leadInstitutionId", SqlDbType.Int).Value = wg.LeadInstitutionID;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = wg.WorkingGroupID;

            conn.Open();
            var rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }

    public bool Delete(int id)
    {
        const string sql = @"DELETE FROM dbo.new_WorkingGroups WHERE WorkingGroupID = @id";
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
