using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class j_PMTDPApprovalList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) { Response.Redirect("login.aspx"); return; }
        if (!IsPostBack) LoadPage();
    }

    private void LoadPage()
    {
        DataTable dt;
        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        using (var cmd = new SqlCommand("n_sp_PMTDP_GetAllUploadsForApprover", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var da = new SqlDataAdapter(cmd))
            {
                dt = new DataTable();
                da.Fill(dt);
            }
        }

        var dvPending = new DataView(dt) { RowFilter = "Status = 'Pending'" };
        var dvReviewed = new DataView(dt) { RowFilter = "Status <> 'Pending'" };

        if (dvPending.Count > 0)
        {
            rptPending.DataSource = dvPending;
            rptPending.DataBind();
            pnlNoPending.Visible = false;
            pnlPending.Visible   = true;
        }
        else
        {
            pnlNoPending.Visible = true;
            pnlPending.Visible   = false;
        }

        if (dvReviewed.Count > 0)
        {
            rptReviewed.DataSource = dvReviewed;
            rptReviewed.DataBind();
            pnlNoReviewed.Visible = false;
            pnlReviewed.Visible   = true;
        }
        else
        {
            pnlNoReviewed.Visible = true;
            pnlReviewed.Visible   = false;
        }
    }
}
