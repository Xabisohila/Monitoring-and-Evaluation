using MnE2.DAL;
using System;
using System.Web.UI;

public partial class j_PMTDPApprovalList : Page
{
    d_PMTDPUploadDAL uploadDAL = new d_PMTDPUploadDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvUploads.DataSource = uploadDAL.GetPendingUploads((int)Session["UserID"]);
            gvUploads.DataBind();
        }
    }
}