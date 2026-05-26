using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration; // Required for ConfigurationManager
using System.Data;          // For DataTable, DataSet, CommandType
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for cls_IndicatorDAL
/// </summary>
public class cls_IndicatorDAL
{
    private string connectionString;
    public cls_IndicatorDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    protected object ExecuteScalar(string spName, params SqlParameter[] parameters) // S_R55
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure; // S_R88, S_R75
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected object ExecuteScalar(string queryOrSpName, CommandType commandType, params SqlParameter[] parameters) // S_R75, S_R88
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] parameters)
    {
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }
        }

        return ds;
    }

    protected DataTable ExecuteDataTable(string queryOrSpName, params SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.Text; // Change to StoredProcedure if needed
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }


    /// <summary>
            /// Creates a new Intervention Indicator and optionally its associated Target.
            /// </summary>
            /// <returns>The ID of the newly created Indicator, or 0 if creation failed.</returns>
    public int CreateInterventionIndicator(
        int interventionId,
        string indicatorName,
        string indicatorType,
        string unitOfMeasure,
        decimal baselineValue,
        int baselineYear,
        decimal? targetValue, // Nullable for optional target
        int? targetYear,       // Nullable for optional target
        string target2030TermTarget) // Nullable for optional target
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@InterventionID", interventionId),
            new SqlParameter("@IndicatorName", indicatorName),
            new SqlParameter("@IndicatorType", indicatorType),
            new SqlParameter("@UnitOfMeasure", (object)unitOfMeasure?? DBNull.Value),
            new SqlParameter("@BaselineValue", baselineValue),
            new SqlParameter("@BaselineYear", baselineYear),
            new SqlParameter("@TargetValue", (object)targetValue?? DBNull.Value),
            new SqlParameter("@TargetYear", (object)targetYear?? DBNull.Value),
            new SqlParameter("@Target2030_TermTarget", (object)target2030TermTarget?? DBNull.Value)
        };

        object result = ExecuteScalar("new_SP_CreateInterventionIndicator", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }

    // --- Lookup Methods for Dropdowns ---
    // These are needed to populate dropdowns on the AddIndicator form.
    public DataTable GetAllInterventionsLookup()
    {
        // Assuming you want to list all interventions by name and ID
        return ExecuteDataTable("SELECT InterventionID, InterventionName FROM new_Interventions ORDER BY InterventionName");
    }

    public DataTable GetAllIndicatorTypesLookup()
    {
        // Read the CHECK constraint definition so the dropdown always matches exactly
        // what the database permits — no hardcoded list that can get out of sync.
        const string sql = @"
            SELECT cc.definition
            FROM   sys.check_constraints cc
            JOIN   sys.columns           c  ON cc.parent_object_id = c.object_id
                                           AND cc.parent_column_id = c.column_id
            WHERE  cc.parent_object_id = OBJECT_ID('dbo.new_Intervention_Indicators')
              AND  c.name = 'IndicatorType'";

        DataTable result = new DataTable();
        result.Columns.Add("IndicatorType", typeof(string));

        try
        {
            DataTable raw = ExecuteDataTable(sql);
            if (raw.Rows.Count > 0)
            {
                string definition = raw.Rows[0]["definition"].ToString();
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Match m in Regex.Matches(definition, @"'([^']+)'"))
                {
                    string val = m.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(val) && seen.Add(val))
                        result.Rows.Add(val);
                }
            }
        }
        catch { }

        // Fallback if constraint unreadable or table not yet seeded
        if (result.Rows.Count == 0)
        {
            foreach (string t in new[] { "Activity", "Impact", "Input", "Outcome", "Output", "Process" })
                result.Rows.Add(t);
        }

        return result;
    }


    /// <summary>
            /// Updates an existing Intervention Indicator and optionally its associated Target.
            /// </summary>
            /// <returns>The number of rows affected (should be 1 for success), or 0 if update failed.</returns>
    public int UpdateInterventionIndicator(
        int indicatorId,
        int interventionId,
        string indicatorName,
        string indicatorType,
        string unitOfMeasure,
        decimal baselineValue,
        int baselineYear,
        decimal? targetValue,
        int? targetYear,
        string target2030TermTarget)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@IndicatorID", indicatorId),
            new SqlParameter("@InterventionID", interventionId),
            new SqlParameter("@IndicatorName", indicatorName),
            new SqlParameter("@IndicatorType", indicatorType),
            new SqlParameter("@UnitOfMeasure", (object)unitOfMeasure?? DBNull.Value),
            new SqlParameter("@BaselineValue", baselineValue),
            new SqlParameter("@BaselineYear", baselineYear),
            new SqlParameter("@TargetValue", (object)targetValue?? DBNull.Value),
            new SqlParameter("@TargetYear", (object)targetYear?? DBNull.Value),
            new SqlParameter("@Target2030_TermTarget", (object)target2030TermTarget?? DBNull.Value)
        };

        object result = ExecuteScalar("new_SP_UpdateInterventionIndicator", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }


    /// <summary>
    /// Retrieves all detailed information for a single Intervention Indicator.
    /// Returns a DataSet with two tables:
    ///  - Indicator Info (main details)
    /// [1] - Indicator Target (optional)
    /// </summary>
    /// <param name="indicatorId">The ID of the Indicator to retrieve.</param>
    /// <returns>DataSet containing all linked data for the Indicator.</returns>
    public DataSet GetIndicatorDetails(int indicatorId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@IndicatorID", indicatorId)
        };
        // SP_GetIndicatorDetails returns 2 result sets
        return ExecuteDataSet("new_SP_GetIndicatorDetails", parameters);
    }

}
