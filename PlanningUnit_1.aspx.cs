using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PlanningUnit_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadStats();
            //BindFrameworks();
            //BindPeriods();
        }
    }

    //private void LoadStats()
    //{
    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand(@"
    //        SELECT
    //          (SELECT COUNT(*) FROM dbo.Framework) AS Frameworks,
    //          (SELECT COUNT(*) FROM dbo.ReportingPeriod WHERE IsOpen=1) AS OpenPeriods,
    //          (SELECT COUNT(*) FROM dbo.PMTDP_Target) AS PMTDP,
    //          (SELECT COUNT(*) FROM dbo.POA_Target) AS POA;", cn))
    //    using (var rdr = cmd.ExecuteReader())
    //    {
    //        if (rdr.Read())
    //        {
    //            lblFrameworks.Text = rdr["Frameworks"].ToString();
    //            lblOpenPeriods.Text = rdr["OpenPeriods"].ToString();
    //            lblPMTDP.Text = rdr["PMTDP"].ToString();
    //            lblPOA.Text = rdr["POA"].ToString();
    //        }
    //    }
    //}

    //private void BindFrameworks()
    //{
    //    ddlFramework.Items.Clear();
    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand("SELECT FrameworkId, Name FROM dbo.Framework ORDER BY Name", cn))
    //    using (var rdr = cmd.ExecuteReader())
    //    {
    //        ddlFramework.DataSource = rdr;
    //        ddlFramework.DataTextField = "Name";
    //        ddlFramework.DataValueField = "FrameworkId";
    //        ddlFramework.DataBind();
    //    }
    //    ddlFramework.Items.Insert(0, new ListItem("-- Select Framework --", ""));
    //}

    //private void BindPeriods()
    //{
    //    int frameworkId;
    //    if (!int.TryParse(ddlFramework.SelectedValue, out frameworkId))
    //    {
    //        gvPeriods.DataSource = null;
    //        gvPeriods.DataBind();
    //        return;
    //    }

    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand("planning.ReportingPeriod_List", cn))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@FrameworkId", frameworkId);
    //        using (var da = new SqlDataAdapter(cmd))
    //        {
    //            var dt = new DataTable();
    //            da.Fill(dt);
    //            gvPeriods.DataSource = dt;
    //            gvPeriods.DataBind();
    //        }
    //    }
    //}

    protected void btnCreateFramework_Click(object sender, EventArgs e)
    {
    //    lblCreateFrameworkMsg.Text = "";
    //    int startYear, endYear;

    //    if (!int.TryParse(txtStartYear.Text.Trim(), out startYear) ||
    //        !int.TryParse(txtEndYear.Text.Trim(), out endYear))
    //    {
    //        lblCreateFrameworkMsg.CssClass = "error";
    //        lblCreateFrameworkMsg.Text = "Start/End year must be valid integers.";
    //        return;
    //    }

    //    if (endYear < startYear)
    //    {
    //        lblCreateFrameworkMsg.CssClass = "error";
    //        lblCreateFrameworkMsg.Text = "End year cannot be earlier than start year.";
    //        return;
    //    }

    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand("planning.Framework_Create", cn))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Name", txtFrameworkName.Text.Trim());
    //        cmd.Parameters.AddWithValue("@StartYear", startYear);
    //        cmd.Parameters.AddWithValue("@EndYear", endYear);
    //        cmd.Parameters.AddWithValue("@UserId", CurrentUserIdOr0());
    //        cmd.ExecuteNonQuery();
    //    }

    //    lblCreateFrameworkMsg.CssClass = "info";
    //    lblCreateFrameworkMsg.Text = "Framework created successfully.";
    //    txtFrameworkName.Text = txtStartYear.Text = txtEndYear.Text = "";

    //    // Refresh UI
    //    LoadStats();
    //    BindFrameworks();
    }

    protected void ddlFramework_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    BindPeriods();
    }

    protected void gvPeriods_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    //    if (e.CommandName != "Toggle") return;

    //    // Find row to read current IsOpen value
    //    var btn = (Control)e.CommandSource;
    //    var row = (GridViewRow)btn.NamingContainer;
    //    int rowIndex = row.RowIndex;

    //    int periodId = Convert.ToInt32(e.CommandArgument);
    //    int frameworkId = Convert.ToInt32(gvPeriods.DataKeys[rowIndex].Values["FrameworkId"]);
    //    var chk = row.Cells[4].Controls[0] as CheckBox;
    //    bool currentOpen = chk != null && chk.Checked;

    //    SetPeriodOpenClose(frameworkId, periodId, !currentOpen);
    //    BindPeriods();
    //    LoadStats();
    }

    private void SetPeriodOpenClose(int frameworkId, int periodId, bool isOpen)
    {
    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand("planning.ReportingPeriod_OpenClose", cn))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@FrameworkId", frameworkId);
    //        cmd.Parameters.AddWithValue("@PeriodId", periodId);
    //        cmd.Parameters.AddWithValue("@IsOpen", isOpen);
    //        cmd.Parameters.AddWithValue("@UserId", CurrentUserIdOr0());
    //        cmd.ExecuteNonQuery();
    //    }
    //    lblPageMsg.CssClass = "info";
    //    lblPageMsg.Text = isOpen ? "Reporting period opened." : "Reporting period closed.";
    }

    protected void btnAddPeriod_Click(object sender, EventArgs e)
    {
    //    lblAddPeriodMsg.Text = "";

    //    if (string.IsNullOrEmpty(ddlFramework.SelectedValue))
    //    {
    //        lblAddPeriodMsg.CssClass = "error";
    //        lblAddPeriodMsg.Text = "Select a framework first.";
    //        return;
    //    }

    //    int frameworkId = int.Parse(ddlFramework.SelectedValue);

    //    DateTime startDate, endDate;
    //    if (!DateTime.TryParseExact(txtStartDate.Text.Trim(), "yyyy-MM-dd",
    //        CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate)
    //        || !DateTime.TryParseExact(txtEndDate.Text.Trim(), "yyyy-MM-dd",
    //        CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
    //    {
    //        lblAddPeriodMsg.CssClass = "error";
    //        lblAddPeriodMsg.Text = "Dates must be in yyyy-MM-dd format.";
    //        return;
    //    }

    //    if (endDate < startDate)
    //    {
    //        lblAddPeriodMsg.CssClass = "error";
    //        lblAddPeriodMsg.Text = "End date cannot be earlier than start date.";
    //        return;
    //    }

    //    // Optional SP: planning.ReportingPeriod_Add
    //    using (var cn = Db.Open())
    //    using (var cmd = new SqlCommand("planning.ReportingPeriod_Add", cn))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@FrameworkId", frameworkId);
    //        cmd.Parameters.AddWithValue("@Name", txtPeriodName.Text.Trim());
    //        cmd.Parameters.AddWithValue("@StartDate", startDate);
    //        cmd.Parameters.AddWithValue("@EndDate", endDate);
    //        cmd.Parameters.AddWithValue("@UserId", CurrentUserIdOr0());
    //        cmd.ExecuteNonQuery();
    //    }

    //    lblAddPeriodMsg.CssClass = "info";
    //    lblAddPeriodMsg.Text = "Reporting period added.";
    //    txtPeriodName.Text = txtStartDate.Text = txtEndDate.Text = "";

    //    BindPeriods();
    //    LoadStats();
    }

}