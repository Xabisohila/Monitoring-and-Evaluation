using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




    public partial class Notifications : System.Web.UI.UserControl
    {
    private readonly c_NotificationsDAL dal = new c_NotificationsDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) Bind();
    }

    private int CurrentUserID
    {
        get { return (Page.Session["UserID"] == null) ? 0 : Convert.ToInt32(Page.Session["UserID"]); }
    }

    private void Bind()
    {
        if (CurrentUserID <= 0) return;
        lblCount.Text = dal.GetUnreadCount(CurrentUserID).ToString();
        repNotes.DataSource = dal.ListByUser(CurrentUserID, onlyUnread: false);
        repNotes.DataBind();
    }

    protected void btnMarkAll_Click(object sender, EventArgs e)
    {
        if (CurrentUserID <= 0) return;
        dal.MarkAllReadForUser(CurrentUserID);
        Bind();
    }
}
