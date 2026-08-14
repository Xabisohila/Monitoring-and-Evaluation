using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class j_PMTDPApprovalView : Page
{
    d_PMTDPUploadDAL     uploadDAL     = new d_PMTDPUploadDAL();
    d_PMTDPUploadDataDAL uploadDataDAL = new d_PMTDPUploadDataDAL();

    // ── Friendly column header names ────────────────────────────────────────
    private static readonly Dictionary<string, string> _displayNames =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "PriorityName",            "Priority Focus" },
        { "ProgrammeName",           "Integration Programme" },
        { "LeaderDeptName",          "Leading Department" },
        { "OutcomeName",             "Desired Outcome" },
        { "IndicatorName",           "Outcome Indicator" },
        { "IndicatorType",           "Indicator Type" },
        { "BaselineValue",           "Baseline Value" },
        { "TermTargetValue",         "Term Target Value" },
        { "AnnualBudget",            "Annual Budget" },
        { "ImplementingInstitution", "Implementing Institution" },
        { "SupportingInstitutions",  "Supporting Institutions" },
        { "IsCumulative",            "Is Cumulative" },
        { "IsPercentage",            "Is Percentage" },
        { "InterventionName",        "Intervention Name" },
        { "InterventionIndicator",   "Intervention Indicator" },
        { "Baseline2023_24",         "Baseline 2023/24" },
        { "TermTarget2030",          "Term Target 2030" },
        { "TermBudget",              "Term Budget" },
        { "AnnualTarget",            "Annual Target 2025/26" },
        { "SpatialReference",        "Spatial Reference" },
    };

    // Columns hidden from the reviewer (internal IDs + removed template fields)
    private static readonly HashSet<string> _hiddenCols = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "UploadDataID", "ProposedAction",
        "AnnualBudget", "SupportingInstitutions", "AnnualTarget"
    };

    // ── Properties ──────────────────────────────────────────────────────────
    private int UploadId
    {
        get
        {
            int id;
            return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
        }
    }

    private int CurrentUserId
    {
        get { return Session["UserID"] != null ? (int)Session["UserID"] : 0; }
    }

    private int SubmitterUserId
    {
        get { return ViewState["SubmitterUserId"] != null ? (int)ViewState["SubmitterUserId"] : 0; }
        set { ViewState["SubmitterUserId"] = value; }
    }

    // ── Page load ────────────────────────────────────────────────────────────
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        if (UploadId == 0)
        {
            Response.Redirect("j_PMTDPApprovalList.aspx");
            return;
        }

        try
        {
            UploadHeader header = uploadDAL.GetUploadHeader(UploadId);
            if (header != null)
            {
                SubmitterUserId = header.UploadedByUserID;

                // Populate metadata card
                litSubmitter.Text  = Server.HtmlEncode(header.SubmitterName);
                litUploadDate.Text = header.UploadDate != DateTime.MinValue
                    ? header.UploadDate.ToString("dd MMM yyyy HH:mm") : "—";
                litReviewer.Text   = !string.IsNullOrEmpty(header.ReviewerName)
                    ? Server.HtmlEncode(header.ReviewerName) : "-";
                string rawName = Path.GetFileName(header.FilePath);
                // Strip the leading ticks prefix added on save (e.g. "639208333267843517_")
                int sep = rawName.IndexOf('_');
                long dummy;
                string displayName = (sep > 0 && long.TryParse(rawName.Substring(0, sep), out dummy))
                    ? rawName.Substring(sep + 1)
                    : rawName;
                litFileName.Text = Server.HtmlEncode(displayName);

                string statusColor = header.Status == "Approved" ? "#065f46"
                                   : header.Status == "Rejected" ? "#991b1b" : "#92400e";
                string statusBg    = header.Status == "Approved" ? "#d1fae5"
                                   : header.Status == "Rejected" ? "#fee2e2" : "#fef3c7";
                lblStatus.Text = string.Format(
                    "<span style='display:inline-block;padding:3px 12px;border-radius:20px;font-size:12px;font-weight:700;background:{0};color:{1};'>{2}</span>",
                    statusBg, statusColor, Server.HtmlEncode(header.Status));

                if (!string.IsNullOrEmpty(header.ReviewComment))
                {
                    litReviewComment.Text = Server.HtmlEncode(header.ReviewComment);
                    pnlReviewNote.Visible = true;
                }

                pnlMeta.Visible = true;
            }

            DataTable dt = uploadDataDAL.GetUploadData(UploadId);

            if (dt == null || dt.Columns.Count == 0)
            {
                ShowMsg("No columns were returned from the database. " +
                        "Verify that dbo.i_PMTDP_UploadData exists and contains rows for this request ID.", "error");
                return;
            }

            // Remove internal columns the reviewer doesn't need to see
            foreach (string col in new List<string>(_hiddenCols))
                if (dt.Columns.Contains(col))
                    dt.Columns.Remove(col);

            gvData.DataSource = dt;
            gvData.DataBind();

            int rowCount = dt.Rows.Count;
            lblRowCount.Text = rowCount + " row" + (rowCount != 1 ? "s" : "");

            if (rowCount == 0)
            {
                var colNames = new System.Text.StringBuilder();
                foreach (DataColumn c in dt.Columns) { if (colNames.Length > 0) colNames.Append(", "); colNames.Append(c.ColumnName); }
                ShowMsg("No data rows found for Upload ID " + UploadId + ". " +
                        "Columns available: " + colNames, "info");
            }

            string status = header != null ? header.Status : "Pending";
            bool isPending = status == "Pending";

            if (!isPending)
            {
                // Already decided — hide the review form for everyone
                pnlReview.Visible = false;
            }
            else if (SubmitterUserId == CurrentUserId && SubmitterUserId != 0)
            {
                // Submitter viewing their own pending upload
                pnlOwnerBanner.Visible = true;
                pnlReview.Visible      = false;
            }
        }
        catch (Exception ex)
        {
            ShowMsg("Error loading upload data: " + ex.Message, "error");
        }
    }

    // ── Rename auto-generated column headers; convert BIT cells to Yes/No ──────
    protected void gvData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                string friendly;
                if (_displayNames.TryGetValue(cell.Text, out friendly))
                    cell.Text = friendly;
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // GridView renders BIT columns as disabled CheckBox controls.
            // Replace each checkbox with a readable "Yes" / "No" text label.
            foreach (TableCell cell in e.Row.Cells)
            {
                if (cell.Controls.Count == 1 && cell.Controls[0] is CheckBox)
                {
                    bool v = ((CheckBox)cell.Controls[0]).Checked;
                    cell.Controls.Clear();
                    cell.Text = v ? "Yes" : "No";
                }
            }
        }
    }

    // ── Approve ──────────────────────────────────────────────────────────────
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (CurrentUserId == 0) { ShowMsg("Session expired. Please log in again.", "error"); return; }

        if (SubmitterUserId == CurrentUserId)
        {
            ShowMsg("You cannot approve your own upload.", "error");
            return;
        }

        try
        {
            DataTable dt = uploadDataDAL.GetUploadData(UploadId);
            foreach (DataRow r in dt.Rows)
                uploadDataDAL.ApplyApprovedRow((int)r["UploadDataID"], CurrentUserId);

            uploadDAL.ReviewUpload(UploadId, CurrentUserId, "Approved", txtComment.Text);
            ShowMsg("Upload approved and applied successfully.", "success");
            pnlReview.Visible = false;
        }
        catch (Exception ex)
        {
            ShowMsg("Approval failed: " + ex.Message, "error");
        }
    }

    // ── Reject ───────────────────────────────────────────────────────────────
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (CurrentUserId == 0) { ShowMsg("Session expired. Please log in again.", "error"); return; }

        if (SubmitterUserId == CurrentUserId)
        {
            ShowMsg("You cannot reject your own upload.", "error");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtComment.Text) || txtComment.Text.Trim().Length < 10)
        {
            ShowMsg("A comment of at least 10 characters is required when rejecting.", "error");
            return;
        }

        try
        {
            uploadDAL.ReviewUpload(UploadId, CurrentUserId, "Rejected", txtComment.Text);

            if (SubmitterUserId > 0)
            {
                string notifMsg = "Your PMTDP upload (Ref #" + UploadId + ") has been rejected. Reason: " + txtComment.Text.Trim();
                new c_NotificationsDAL().Upsert(new c_Notification
                {
                    UserID  = SubmitterUserId,
                    Message = notifMsg,
                    IsRead  = false
                });
            }

            ShowMsg("Upload rejected.", "error");
            pnlReview.Visible = false;
        }
        catch (Exception ex)
        {
            ShowMsg("Rejection failed: " + ex.Message, "error");
        }
    }

    // ── Helper ───────────────────────────────────────────────────────────────
    private void ShowMsg(string text, string type)
    {
        lblMsg.Text = text;
        string css = "msg-bar visible ";
        css += type == "success" ? "msg-success" : type == "info" ? "msg-info" : "msg-error";
        lblMsg.CssClass = css;
    }
}
