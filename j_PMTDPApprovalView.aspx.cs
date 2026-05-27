using MnE2.DAL;
using System;
using System.Data;
using System.Web.UI;

public partial class j_PMTDPApprovalView : Page
{
    d_PMTDPUploadDAL uploadDAL = new d_PMTDPUploadDAL();
    d_PMTDPUploadDataDAL uploadDataDAL = new d_PMTDPUploadDataDAL();

    private int UploadId
    {
        get { return Convert.ToInt32(Request.QueryString["id"]); }
    }

    private int CurrentUserId
    {
        get { return Session["UserID"] != null ? (int)Session["UserID"] : 0; }
    }

    // Persists across postbacks so button handlers don't need another DB call.
    private int SubmitterUserId
    {
        get { return ViewState["SubmitterUserId"] != null ? (int)ViewState["SubmitterUserId"] : 0; }
        set { ViewState["SubmitterUserId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UploadId == 0)
            {
                Response.Redirect("j_PMTDPApprovalList.aspx");
            }
            else
            {
                UploadHeader header = uploadDAL.GetUploadHeader(UploadId);
                if (header != null)
                    SubmitterUserId = header.UploadedByUserID;

                DataTable dt = uploadDataDAL.GetUploadData(UploadId);
                gvData.DataSource = dt;
                gvData.DataBind();
                lblRowCount.Text = dt.Rows.Count + " row" + (dt.Rows.Count != 1 ? "s" : "");

                if (SubmitterUserId == CurrentUserId && SubmitterUserId != 0)
                {
                    pnlOwnerBanner.Visible = true;
                    pnlReview.Visible      = false;
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (CurrentUserId == 0) { lblMsg.Text = "Session expired. Please log in again."; return; }

        if (SubmitterUserId == CurrentUserId)
        {
            lblMsg.Text = "You cannot approve your own upload.";
            return;
        }

        DataTable dt = uploadDataDAL.GetUploadData(UploadId);
        foreach (DataRow r in dt.Rows)
            uploadDataDAL.ApplyApprovedRow((int)r["UploadDataID"], CurrentUserId);

        uploadDAL.ReviewUpload(UploadId, CurrentUserId, "Approved", txtComment.Text);

        lblMsg.Text = "Upload approved and applied.";
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (CurrentUserId == 0) { lblMsg.Text = "Session expired. Please log in again."; return; }

        if (SubmitterUserId == CurrentUserId)
        {
            lblMsg.Text = "You cannot reject your own upload.";
            return;
        }

        if (string.IsNullOrWhiteSpace(txtComment.Text) || txtComment.Text.Trim().Length < 10)
        {
            lblMsg.Text = "A comment of at least 10 characters is required when rejecting.";
            return;
        }

        uploadDAL.ReviewUpload(UploadId, CurrentUserId, "Rejected", txtComment.Text);

        if (SubmitterUserId > 0)
        {
            string notifMsg = "Your PMTDP upload (Ref # " + UploadId + ") has been rejected by the Planning Unit.";
            if (!string.IsNullOrWhiteSpace(txtComment.Text))
                notifMsg += " Reason: " + txtComment.Text;

            new c_NotificationsDAL().Upsert(new c_Notification
            {
                UserID  = SubmitterUserId,
                Message = notifMsg,
                IsRead  = false
            });
        }

        lblMsg.Text = "Upload rejected.";
    }

}