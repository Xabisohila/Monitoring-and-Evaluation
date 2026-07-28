using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public class d_POAUploadApplyDAL
{
    private readonly string _conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    private readonly d_POAUploadDAL _dal = new d_POAUploadDAL();

    // Entry point: create POA + interventions + indicators, then mark upload approved.
    // Returns the new POA_ID.
    public int ApplyUpload(int uploadRequestId, int pmtdpPriorityId, int clusterId,
        string poaName, int startYear, int endYear, int reviewerUserId, string reviewComment)
    {
        DataTable rows = _dal.GetUploadData(uploadRequestId);
        int poaId;

        using (var cn = new SqlConnection(_conn))
        {
            cn.Open();
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // Take DesiredOutcome from the first non-empty data row
                    string desiredOutcome = "";
                    foreach (DataRow dr0 in rows.Rows)
                    {
                        string v = dr0["DesiredOutcome"].ToString().Trim();
                        if (!string.IsNullOrEmpty(v)) { desiredOutcome = v; break; }
                    }

                    poaId = CreatePOA(cn, tx, pmtdpPriorityId, clusterId, poaName, startYear, endYear, desiredOutcome);

                    // Resolve FY for the budget (financial year matching startYear+1, e.g. 2025 → 2026/2027)
                    int fyId = ResolveFYId(cn, tx, startYear + 1);

                    // Track interventions we've already created (key = InterventionName)
                    var interventionMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    foreach (DataRow row in rows.Rows)
                    {
                        string interventionName = row["InterventionName"].ToString().Trim();
                        if (string.IsNullOrEmpty(interventionName)) continue;

                        if (!interventionMap.ContainsKey(interventionName))
                        {
                            string instName  = row["ImplementingInstitution"].ToString().Trim();
                            int instId       = ResolveInstitutionId(cn, tx, instName);
                            string spatial   = row["SpatialReference"].ToString().Trim();

                            int interventionId = CreateIntervention(cn, tx, interventionName, poaId, instId, startYear, endYear, spatial, reviewerUserId);
                            interventionMap[interventionName] = interventionId;

                            // Write budget for this intervention (first row's value)
                            decimal budget = ParseDecimal(row["AnnualBudget"].ToString());
                            if (budget > 0 && fyId > 0)
                                CreateBudget(cn, tx, interventionId, fyId, budget);
                        }

                        string indicatorName = row["InterventionIndicator"].ToString().Trim();
                        if (string.IsNullOrEmpty(indicatorName)) continue;

                        string indType    = row["IndicatorType"].ToString().Trim();
                        if (string.IsNullOrEmpty(indType)) indType = "Output";

                        decimal baseline  = ParseDecimal(row["Baseline2023_24"].ToString());
                        string termTarget = row["TermTarget2025_2030"].ToString().Trim();
                        decimal target    = ParseDecimal(row["Target2026_27"].ToString());

                        int indicatorId = CreateIndicator(cn, tx,
                            interventionMap[interventionName], indicatorName, indType, baseline, 2023, termTarget);

                        if (target > 0 || !string.IsNullOrEmpty(termTarget))
                            CreateIndicatorTarget(cn, tx, indicatorId, 2026, target, termTarget);
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        _dal.MarkApproved(uploadRequestId, reviewerUserId, reviewComment, poaId);
        return poaId;
    }

    // ── Private helpers ───────────────────────────────────────────────────

    private int CreatePOA(SqlConnection cn, SqlTransaction tx, int priorityId, int clusterId,
        string name, int startYear, int endYear, string desiredOutcome = "")
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO new_ProgrammesOfAction
                    (POA_Name, PMTDP_PriorityID, ClusterID, POA_StartYear, POA_EndYear, DesiredOutcome)
                VALUES
                    (@Name, @PriorityID, @ClusterID, @Start, @End, @DesiredOutcome);
                SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Name",           name);
            cmd.Parameters.AddWithValue("@PriorityID",     priorityId);
            cmd.Parameters.AddWithValue("@ClusterID",      clusterId);
            cmd.Parameters.AddWithValue("@Start",          startYear);
            cmd.Parameters.AddWithValue("@End",            endYear);
            cmd.Parameters.AddWithValue("@DesiredOutcome", string.IsNullOrEmpty(desiredOutcome) ? (object)DBNull.Value : desiredOutcome);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    private int ResolveInstitutionId(SqlConnection cn, SqlTransaction tx, string name)
    {
        if (string.IsNullOrEmpty(name)) return GetDefaultInstitutionId(cn, tx);

        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT TOP 1 InstitutionID FROM new_ImplementationInstitutions WHERE InstitutionName LIKE '%' + @Name + '%'";
            cmd.Parameters.AddWithValue("@Name", name);
            var result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);
        }
        return GetDefaultInstitutionId(cn, tx);
    }

    private int GetDefaultInstitutionId(SqlConnection cn, SqlTransaction tx)
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT TOP 1 InstitutionID FROM new_ImplementationInstitutions ORDER BY InstitutionID";
            var result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 201;
        }
    }

    private int ResolveFYId(SqlConnection cn, SqlTransaction tx, int year)
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT TOP 1 FY_ID FROM new_FinancialYears WHERE FY_Name LIKE @Pattern ORDER BY FY_Name";
            cmd.Parameters.AddWithValue("@Pattern", year + "%");
            var result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
    }

    private void CreateBudget(SqlConnection cn, SqlTransaction tx, int interventionId, int fyId, decimal annualBudget)
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO new_Intervention_Budgets
                    (InterventionID, FY_ID, AnnualBudget)
                VALUES
                    (@InterventionID, @FY_ID, @AnnualBudget)";
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            cmd.Parameters.AddWithValue("@FY_ID",          fyId);
            cmd.Parameters.AddWithValue("@AnnualBudget",   annualBudget);
            cmd.ExecuteNonQuery();
        }
    }

    private int CreateIntervention(SqlConnection cn, SqlTransaction tx, string name, int poaId,
        int institutionId, int startYear, int endYear, string spatial, int approverUserId)
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO new_Interventions
                    (InterventionName, POA_ID, LeadInstitutionID, InterventionStartYear,
                     InterventionEndYear, SpatialReference, IsApproved, ApprovedByUserID,
                     ApprovalDate, PMTDP_Source)
                VALUES
                    (@Name, @POA_ID, @InstID, @Start, @End,
                     @Spatial, 1, @Approver, GETDATE(), 0);
                SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Name",     name);
            cmd.Parameters.AddWithValue("@POA_ID",   poaId);
            cmd.Parameters.AddWithValue("@InstID",   institutionId);
            cmd.Parameters.AddWithValue("@Start",    startYear);
            cmd.Parameters.AddWithValue("@End",      endYear);
            cmd.Parameters.AddWithValue("@Spatial",  string.IsNullOrEmpty(spatial) ? (object)DBNull.Value : spatial);
            cmd.Parameters.AddWithValue("@Approver", approverUserId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    private int CreateIndicator(SqlConnection cn, SqlTransaction tx, int interventionId,
        string name, string type, decimal baseline, int baselineYear, string termTarget)
    {
        // Insert into new_Intervention_Indicators — used by pagePOADetail
        int newIndId;
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO new_Intervention_Indicators
                    (InterventionID, IndicatorName, IndicatorType, BaselineValue,
                     BaselineYear, TermTargetValue, IsCumulative, IsPercentage)
                VALUES
                    (@InterventionID, @Name, @Type, @Baseline, @BaselineYear,
                     @TermTarget, 0, 0);
                SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@InterventionID", interventionId);
            cmd.Parameters.AddWithValue("@Name",         name);
            cmd.Parameters.AddWithValue("@Type",         type);
            cmd.Parameters.AddWithValue("@Baseline",     baseline);
            cmd.Parameters.AddWithValue("@BaselineYear", baselineYear);
            cmd.Parameters.AddWithValue("@TermTarget",   string.IsNullOrEmpty(termTarget) ? (object)DBNull.Value : termTarget);
            newIndId = Convert.ToInt32(cmd.ExecuteScalar());
        }

        // Also register in i_Indicators so it appears in the quarterly targets dropdown.
        // OutcomeID left NULL — uploaded indicators are not tied to a legacy outcome.
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO i_Indicators
                    (IndicatorName, IndicatorType, BaselineValue, TermTargetValue, IsCumulative, IsPercentage)
                VALUES
                    (@Name, @Type, @Baseline, @TermTarget, 0, 0)";
            cmd.Parameters.AddWithValue("@Name",      name);
            cmd.Parameters.AddWithValue("@Type",      string.IsNullOrEmpty(type) ? (object)DBNull.Value : type);
            cmd.Parameters.AddWithValue("@Baseline",  baseline.ToString());
            cmd.Parameters.AddWithValue("@TermTarget", string.IsNullOrEmpty(termTarget) ? (object)DBNull.Value : termTarget);
            cmd.ExecuteNonQuery();
        }

        return newIndId;
    }

    private void CreateIndicatorTarget(SqlConnection cn, SqlTransaction tx, int indicatorId,
        int targetYear, decimal targetValue, string termTarget)
    {
        using (var cmd = cn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO new_Indicator_Targets
                    (IndicatorID, TargetYear, TargetValue, Target2030_TermTarget)
                VALUES
                    (@IndID, @Year, @Value, @Term)";
            cmd.Parameters.AddWithValue("@IndID", indicatorId);
            cmd.Parameters.AddWithValue("@Year",  targetYear);
            cmd.Parameters.AddWithValue("@Value", targetValue);
            cmd.Parameters.AddWithValue("@Term",  string.IsNullOrEmpty(termTarget) ? (object)DBNull.Value : termTarget);
            cmd.ExecuteNonQuery();
        }
    }

    private static decimal ParseDecimal(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        // Strip currency symbols, commas, spaces; keep digits and decimal point
        string clean = Regex.Replace(s, @"[^\d\.\-]", "");
        decimal result;
        return decimal.TryParse(clean, out result) ? result : 0;
    }
}
