using System;
using System.Web.UI;

public partial class j_POAUploadApprovalList : Page
{
    private readonly d_POAUploadDAL _dal = new d_POAUploadDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] == null) { Response.Redirect("login.aspx"); return; }
        int role = Convert.ToInt32(Session["UserType"]);
        if (role != 32 && role != 37 && role != 41) { Response.Redirect("Index.aspx"); return; }

        if (!IsPostBack)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            bool canApprove = (role == 32 || role == 41);

            // Pending uploads section (for approvers only)
            if (canApprove)
            {
                pnlPendingSection.Visible = true;
                var pending = _dal.GetPendingUploads(userId);
                if (pending.Rows.Count == 0)
                {
                    pnlNoPending.Visible = true;
                    pnlPending.Visible   = false;
                }
                else
                {
                    pnlNoPending.Visible = false;
                    pnlPending.Visible   = true;
                    rptPending.DataSource = pending;
                    rptPending.DataBind();
                }
            }

            // My uploads
            var mine = _dal.GetMyUploads(userId);
            if (mine.Rows.Count == 0)
            {
                pnlNoMine.Visible = true;
                pnlMine.Visible   = false;
            }
            else
            {
                pnlNoMine.Visible = false;
                pnlMine.Visible   = true;
                rptMine.DataSource = mine;
                rptMine.DataBind();
            }
        }
    }
}
