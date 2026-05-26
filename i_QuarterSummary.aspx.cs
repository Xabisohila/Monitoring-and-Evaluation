using MnE2.DAL;

using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_QuarterSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // RequireRole(/* any authenticated */);
        if (!IsPostBack)
        {
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026", "2026"));
            BindSummary();
        }
    }

    protected void FilterChanged(object sender, EventArgs e)
    {
        BindSummary();
    }

    private void BindSummary()
    {
        if (string.IsNullOrEmpty(ddlFY.SelectedValue)) return;

        using (var con = Database.GetConnection())
        using (var cmd = new SqlCommand("i_sp_Dashboard_QuarterSummary", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FinancialYear", Convert.ToInt32(ddlFY.SelectedValue));
            cmd.Parameters.AddWithValue("@QuarterNumber", Convert.ToInt32(ddlQuarter.SelectedValue));
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
    }
}