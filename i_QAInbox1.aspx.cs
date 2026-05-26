using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_QAInbox1 : System.Web.UI.Page
{
    private readonly c_WorkflowHistoryDAL wf = new c_WorkflowHistoryDAL();
    private readonly c_QuarterlyReportsDAL reports = new c_QuarterlyReportsDAL();
    private readonly c_QuarterlyTargetsDAL qTargets = new c_QuarterlyTargetsDAL();

    public int CurrentUserID { get; private set; }// You can set this from session or auth context

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* WGCoordinator, OTPValidator role IDs */);
        if (!IsPostBack)
        {
            CurrentUserID = 1; // Set this to the actual logged-in user ID for testing
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026", "2026"));
            BindGrid();
        }
    }

    protected void FilterChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        if (string.IsNullOrEmpty(ddlFY.SelectedValue)) return;

        // Use the IR2 proc: sp_Report_AwaitingQA
        using (var con = MnE2.DAL.Database.GetConnection())
        using (var cmd = new SqlCommand("i_sp_Report_AwaitingQA", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", Convert.ToInt32(ddlFY.SelectedValue));
            cmd.Parameters.AddWithValue("@QuarterNumber", Convert.ToInt32(ddlQuarter.SelectedValue));
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                var dt = new DataTable();
                dt.Load(rdr);
                gvAwaitingQA.DataSource = dt;
                gvAwaitingQA.DataBind();
            }
        }
    }

    protected void gvAwaitingQA_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int reportID = Convert.ToInt32(gvAwaitingQA.Rows[rowIndex].Cells[0].Text);

            var r = reports.GetByID(reportID);
            if (r != null)
            {
                var qt = qTargets.GetByID(r.QuarterlyTargetID);
                lblReportID.Text = r.ReportID.ToString();
                hfReportID.Value = r.ReportID.ToString();
                lblPlanned.Text = "Planned: " + (qt == null ? "-" : qt.TargetValue);
                lblActual.Text = "Actual: " + r.ActualValue + (r.Achieved ? " (Achieved)" : " (Not achieved)");
                pnlDetail.Visible = true;
            }
        }
    }

    protected void btnQAPass_Click(object sender, EventArgs e)
    {
        ProcessQA(/* QA_Pass StatusID */ 3);
    }
    protected void btnQAFail_Click(object sender, EventArgs e)
    {
        ProcessQA(/* QA_Fail StatusID */ 4);
    }

    private void ProcessQA(int statusID)
    {
        if (string.IsNullOrEmpty(hfReportID.Value)) return;
        wf.SetQA(Convert.ToInt32(hfReportID.Value), statusID, CurrentUserID, txtQAComments.Text);
        pnlDetail.Visible = false;
        BindGrid();
    }
}