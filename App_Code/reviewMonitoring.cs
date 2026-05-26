using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

public class reviewMonitoring
{
    // NEW: properties required by GridView Eval() expressions and code-behind
    public string InterventionID { get; set; }
    public string ClusterID { get; set; }

    // Existing properties used in UI and code-behind
    public string InterventionName { get; set; }
    public string Quarter { get; set; }
    public decimal PlannedExpenditure { get; set; }
    public decimal ActualExpenditure { get; set; }
    public decimal PerformancePlannedValue { get; set; }
    public decimal PerformanceActualValue { get; set; }
    public decimal DeviationPercent { get; set; }
    public string UploadFilePath { get; set; }

    // Added clusterId parameter. This method joins to new_SubOutcomes to filter by ClusterID.
    public static List<reviewMonitoring> GetReports(string interventionId, string fyId, string quarter, string clusterId)
    {
        List<reviewMonitoring> list = new List<reviewMonitoring>();
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string query = @"
                SELECT r.*, i.InterventionName, so.ClusterID
                FROM new_QuarterlyReports r
                JOIN new_Interventions i ON r.InterventionID = i.InterventionID
                LEFT JOIN new_SubOutcomes so ON r.SubOutcomeID = so.SubOutcomeID
                WHERE (@InterventionID = '' OR r.InterventionID = @InterventionID)
                  AND (@FY_ID = '' OR r.FY_ID = @FY_ID)
                  AND (@Quarter = '' OR r.Quarter = @Quarter)
                  AND (@ClusterID = '' OR so.ClusterID = @ClusterID)
                  AND i.IsApproved = 1
                ORDER BY r.ReportingDate DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@InterventionID", interventionId ?? "");
            cmd.Parameters.AddWithValue("@FY_ID", fyId ?? "");
            cmd.Parameters.AddWithValue("@Quarter", quarter ?? "");
            cmd.Parameters.AddWithValue("@ClusterID", clusterId ?? "");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    decimal planned = reader.IsDBNull(reader.GetOrdinal("PerformancePlannedValue")) ? 0 : Convert.ToDecimal(reader["PerformancePlannedValue"]);
                    decimal actual = reader.IsDBNull(reader.GetOrdinal("PerformanceActualValue")) ? 0 : Convert.ToDecimal(reader["PerformanceActualValue"]);
                    decimal deviation = planned == 0 ? 0 : ((actual - planned) / planned) * 100;

                    decimal plannedExpenditure = reader.IsDBNull(reader.GetOrdinal("PlannedExpenditure")) ? 0 : Convert.ToDecimal(reader["PlannedExpenditure"]);
                    decimal actualExpenditure = reader.IsDBNull(reader.GetOrdinal("ActualExpenditure")) ? 0 : Convert.ToDecimal(reader["ActualExpenditure"]);
                    string uploadPath = reader.IsDBNull(reader.GetOrdinal("UploadFilePath")) ? string.Empty : reader["UploadFilePath"].ToString();

                    list.Add(new reviewMonitoring
                    {
                        InterventionID = reader["InterventionID"].ToString(),
                        InterventionName = reader["InterventionName"].ToString(),
                        Quarter = reader["Quarter"].ToString(),
                        PlannedExpenditure = plannedExpenditure,
                        ActualExpenditure = actualExpenditure,
                        PerformancePlannedValue = planned,
                        PerformanceActualValue = actual,
                        DeviationPercent = deviation,
                        UploadFilePath = uploadPath,
                        ClusterID = reader["ClusterID"].ToString()
                    });
                }
            }
        }

        return list;
    }
}