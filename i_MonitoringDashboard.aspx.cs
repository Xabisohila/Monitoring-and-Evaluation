using MnE2.DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TextmagicRest.Model;

public partial class i_MonitoringDashboard : System.Web.UI.Page
{
    private readonly c_DepartmentDAL deptDAL = new c_DepartmentDAL();
    private readonly c_WorkingGroupDAL wgDAL = new c_WorkingGroupDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Any authenticated (or Planning/Cluster) */);
        if (!IsPostBack)
        {
            // Years (adjust or fetch from DB)
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025/26", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026/27", "2026"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2027/28", "2027"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2028/29", "2028"));

            // Quarters default Q1
            ddlQuarter.SelectedValue = "1";

            // Optional WG filter
            ddlWG.DataSource = wgDAL.GetAll();
            ddlWG.DataTextField = "WorkingGroupName";
            ddlWG.DataValueField = "WorkingGroupID";
            ddlWG.DataBind();
            ddlWG.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));

            // Department filter
            ddlDept.DataSource = deptDAL.GetAll();
            ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));

            BindAll();
        }
    }

    protected void FiltersChanged(object sender, EventArgs e)
    {
        BindAll();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindAll();
    }

    private int FY
    {
        get
        {
            return Convert.ToInt32(ddlFY.SelectedValue);
        }
    }

    private int Q
    {
        get
        {
            return Convert.ToInt32(ddlQuarter.SelectedValue);
        }
    }

    private int? WGFilter
    {
        get
        {
            return string.IsNullOrEmpty(ddlWG.SelectedValue) ? (int?)null : Convert.ToInt32(ddlWG.SelectedValue);
        }
    }

    private int? DeptFilter
    {
        get
        {
            return string.IsNullOrEmpty(ddlDept.SelectedValue) ? (int?)null : Convert.ToInt32(ddlDept.SelectedValue);
        }
    }

    private void BindAll()
    {
        BindHeadlineKPIs();
        BindWorkingGroupTiles();
        BindTopTables();
    }

    private void BindHeadlineKPIs()
    {
        // Base summary
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("i_sp_Dashboard_QuarterSummary", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", FY);
            cmd.Parameters.AddWithValue("@QuarterNumber", Q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    lblAchieved.Text = rdr["AchievedCount"].ToString();
                    lblNotAchieved.Text = rdr["NotAchievedCount"].ToString();
                    lblTotal.Text = rdr["TotalReported"].ToString();
                }
                else
                {
                    lblAchieved.Text = lblNotAchieved.Text = lblTotal.Text = "0";
                }
            }
        }

        // Non-compliance count
        int ncCount = 0;
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("i_sp_Report_NonCompliance", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", FY);
            cmd.Parameters.AddWithValue("@QuarterNumber", Q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read()) ncCount++;
            }
        }
        lblNCCount.Text = ncCount.ToString();

        // Awaiting counts (QA / Approval / Sign-off)
        int aQA = CountAwaiting("i_sp_Report_AwaitingQA");
        int aAp = CountAwaiting("i_sp_Report_AwaitingApproval");
        int aSo = CountAwaiting("sp_Report_AwaitingSignoff");
        // Display as combined or separate as needed
        // lblAwaitingTriple.Text = $"{aQA}/{aAp}/{aSo}";
        lblAwaitingTriple.Text = string.Format("{0}/{1}/{2}", aQA, aAp, aSo);
    }

    private int CountAwaiting(string proc)
    {
        int count = 0;
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand(proc, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", FY);
            cmd.Parameters.AddWithValue("@QuarterNumber", Q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read()) count++;
            }
        }
        return count;
    }

    private void BindWorkingGroupTiles()
    {

        try
        {
            // Uses IndicatorWorkingGroup mapping; optional Dept/WG scoping
            string sql = @"
SELECT
    wg.WorkingGroupID,
    wg.WorkingGroupName,
    SUM(CASE WHEN qr.Achieved = 1 THEN 1 ELSE 0 END) AS Achieved,
    COUNT(*) AS TotalReported
FROM i_WorkingGroups wg
JOIN i_IndicatorWorkingGroup iwg ON iwg.WorkingGroupID = wg.WorkingGroupID
JOIN i_Indicators i ON i.IndicatorID = iwg.IndicatorID
JOIN i_AnnualTargets atg ON atg.IndicatorID = i.IndicatorID AND atg.FinancialYear = @FY
JOIN i_QuarterlyTargets qt ON qt.AnnualTargetID = atg.AnnualTargetID AND qt.QuarterNumber = @Q
JOIN i_QuarterlyReports qr ON qr.QuarterlyTargetID = qt.QuarterlyTargetID AND qr.QuarterNumber = @Q
LEFT JOIN i_Users u ON u.UserID = qr.SubmittedByUserID
WHERE 1=1
";

            if (WGFilter.HasValue) sql += " AND wg.WorkingGroupID = @WGID";
            if (DeptFilter.HasValue) sql += " AND u.DepartmentID = @DeptID";
            sql += " GROUP BY wg.WorkingGroupID, wg.WorkingGroupName ORDER BY wg.WorkingGroupName;";

            var tiles = new List<dynamic>();
            using (var con = Database.GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FY", FY);
                cmd.Parameters.AddWithValue("@Q", Q);
                if (WGFilter.HasValue) cmd.Parameters.AddWithValue("@WGID", WGFilter.Value);
                if (DeptFilter.HasValue) cmd.Parameters.AddWithValue("@DeptID", DeptFilter.Value);

                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    int idx = 0;
                    while (rdr.Read())
                    {
                        int achieved = Convert.ToInt32(rdr["Achieved"]);
                        int total = Convert.ToInt32(rdr["TotalReported"]);
                        double pct = total > 0 ? (double)achieved / total : 0d;
                        tiles.Add(new
                        {
                            WorkingGroupName = rdr["WorkingGroupName"].ToString(),
                            Achieved = achieved,
                            TotalReported = total,
                            PercentAchieved = pct,
                            BarPercent = Math.Round(pct * 100, 0),
                            Css = CssFor(idx++)
                        });
                    }
                }
            }

            repWG.DataSource = tiles;
            repWG.DataBind();

            lblWGNote.Text = tiles.Count == 0
                ? "No WG data in scope (ensure Indicator↔WG mapping is populated, or adjust filters)."
                : "";
        }
        catch (Exception ex)
        {
            lblWGNote.Text = "Error loading WG data: " + ex.Message;
        }
    }

    private string CssFor(int idx)
    {
        string[] css = { "bg1", "bg2", "bg3", "bg4" };
        return css[idx % css.Length];
    }

    private void BindTopTables()
    {
        // Non-Compliance (Top 8)
        gvNC.DataSource = LoadTop("i_sp_Report_NonCompliance", 8);
        gvNC.DataBind();

        // Awaiting = union of three awaiting SProcs (Top 8 by submitted date)
        gvAwaiting.DataSource = LoadAwaitingTopCombined(8);
        gvAwaiting.DataBind();

        // Improvement Plan (Top 8)
        gvImprove.DataSource = LoadTop("i_sp_Report_ImprovementPlan", 8);
        gvImprove.DataBind();
    }

    private DataTable LoadTop(string proc, int top)
    {
        var dt = new DataTable();
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand(proc, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", FY);
            cmd.Parameters.AddWithValue("@QuarterNumber", Q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                dt.Load(rdr);
            }
        }
        // trim rows
        while (dt.Rows.Count > top) dt.Rows.RemoveAt(dt.Rows.Count - 1);
        return dt;
    }

    private DataTable LoadAwaitingTopCombined(int top)
    {
        // Pull each awaiting list and union in-memory with a tag column
        var combined = new DataTable();
        combined.Columns.Add("ReportID");
        combined.Columns.Add("IndicatorName");
        combined.Columns.Add("SubmittedDate");
        combined.Columns.Add("AwaitingStage");

        FillAwaiting("i_sp_Report_AwaitingQA", "QA", combined);
        FillAwaiting("i_sp_Report_AwaitingApproval", "Approval", combined);
        FillAwaiting("sp_Report_AwaitingSignoff", "Sign-off", combined);

        // sort by SubmittedDate desc if present
        try
        {
            combined.DefaultView.Sort = "SubmittedDate DESC";
            var topDt = combined.DefaultView.ToTable().Clone();
            int i = 0;
            foreach (DataRowView row in combined.DefaultView)
            {
                if (i++ >= top) break;
                topDt.ImportRow(row.Row);
            }
            return topDt;
        }
        catch { return combined; }
    }

    private void FillAwaiting(string proc, string tag, DataTable target)
    {
        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand(proc, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", FY);
            cmd.Parameters.AddWithValue("@QuarterNumber", Q);
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var r = target.NewRow();
                    r["ReportID"] = rdr["ReportID"];
                    r["IndicatorName"] = rdr["IndicatorName"];
                    r["SubmittedDate"] = rdr["SubmittedDate"];
                    r["AwaitingStage"] = tag;
                    target.Rows.Add(r);
                }
            }
        }
    }

    // --- Export current scope to CSV (simple starter; extend to include each table) ---
    protected void btnExportCsv_Click(object sender, EventArgs e)
    {
        // Example: export the combined Awaiting list in scope
        var dt = LoadAwaitingTopCombined(9999);

        Response.Clear();
        Response.ContentType = "text/csv";
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Monitoring_Awaiting_FY{0}_Q{1}.csv", FY, Q));

        // header
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (i > 0) Response.Write(",");
            Response.Write("\"" + dt.Columns[i].ColumnName.Replace("\"", "\"\"") + "\"");
        }
        Response.Write("\r\n");

        // rows
        foreach (DataRow r in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i > 0) Response.Write(",");
                var cell = r[i] != null ? r[i].ToString() : "";
                Response.Write("\"" + cell.Replace("\"", "\"\"") + "\"");
            }
            Response.Write("\r\n");
        }
        Response.End();
    }
}
