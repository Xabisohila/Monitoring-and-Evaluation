using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_POAList : Page
{
    private POADAL dal = new POADAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        if (!IsPostBack)
            BindGrid();
    }

    private void BindGrid()
    {
        DataTable dt = dal.GetAllPOAs();
        lblTotal.Text = dt.Rows.Count.ToString();
        gvPOAs.DataSource = dt;
        gvPOAs.DataBind();
    }
}
