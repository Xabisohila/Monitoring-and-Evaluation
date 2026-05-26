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



public partial class i_NonCompliance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Planning / Cluster roles */);
        if (!IsPostBack)
        {
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2025/2026", "2025"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2026/2027", "2026"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2027/2028", "2027"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2028/2029", "2028"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2029/2030", "2029"));
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem("2030/2031", "2030"));
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
        using (var cmd = new SqlCommand("i_sp_Report_NonCompliance", con))
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
