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

public partial class i_ImprovementPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Planning / Cluster */);
        if (!IsPostBack)
        {
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026", "2026"));
            Bind();
        }
    }

    protected void FilterChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind()
    {
        if (string.IsNullOrEmpty(ddlFY.SelectedValue)) return;

        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("i_sp_Report_ImprovementPlan", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", Convert.ToInt32(ddlFY.SelectedValue));
            cmd.Parameters.AddWithValue("@QuarterNumber", Convert.ToInt32(ddlQuarter.SelectedValue));
            con.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                var dt = new DataTable();
                dt.Load(rdr);
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
    }
}
