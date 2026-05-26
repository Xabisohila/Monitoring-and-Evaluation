using MnE2.DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class i_Signoff : System.Web.UI.Page
{
    private readonly c_WorkflowHistoryDAL wf = new c_WorkflowHistoryDAL();

    public int CurrentUserID { get; private set; }// You can set this from session or auth context

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* WGConvenor role ID */);
        if (!IsPostBack)
        {
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

        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("sp_Report_AwaitingSignoff", con))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", Convert.ToInt32(ddlFY.SelectedValue));
            cmd.Parameters.AddWithValue("@QuarterNumber", Convert.ToInt32(ddlQuarter.SelectedValue));
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                var dt = new System.Data.DataTable();
                dt.Load(rdr);
                gvAwaitingSignoff.DataSource = dt;
                gvAwaitingSignoff.DataBind();
            }
        }
    }

    protected void gvAwaitingSignoff_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int reportID = Convert.ToInt32(gvAwaitingSignoff.Rows[rowIndex].Cells[0].Text);
            hfReportID.Value = reportID.ToString();
            lblReportID.Text = reportID.ToString();
            pnlDetail.Visible = true;
        }
    }

    protected void btnSignoff_Click(object sender, EventArgs e)
    {
        DoSignoff(/* Signoff_SignedOff StatusID */ 7);
    }
    protected void btnNotApproved_Click(object sender, EventArgs e)
    {
        DoSignoff(/* Signoff_NotApproved StatusID */ 8);
    }

    private void DoSignoff(int statusID)
    {
        if (string.IsNullOrEmpty(hfReportID.Value)) return;
        wf.SetSignoff(Convert.ToInt32(hfReportID.Value), statusID, CurrentUserID, txtComments.Text);
        pnlDetail.Visible = false;
        BindGrid();
    }
}