using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class j_POAUploadApprovalView : Page
{
    private readonly d_POAUploadDAL      _dal      = new d_POAUploadDAL();
    private readonly d_POAUploadApplyDAL _applyDal = new d_POAUploadApplyDAL();

    private int RequestId
    {
        get
        {
            int id;
            return int.TryParse(Request.QueryString["id"], out id) ? id : 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] == null) { Response.Redirect("login.aspx"); return; }
        int role = Convert.ToInt32(Session["UserType"]);
        if (role != 32 && role != 37 && role != 41) { Response.Redirect("Index.aspx"); return; }

        if (RequestId == 0) { Response.Redirect("j_POAUploadApprovalList.aspx"); return; }

        if (!IsPostBack)
        {
            LoadPage(role);
        }
    }

    private void LoadPage(int role)
    {
        DataRow header = _dal.GetUploadHeader(RequestId);
        if (header == null) { Response.Redirect("j_POAUploadApprovalList.aspx"); return; }

        // Populate header metadata labels
        litUploadedBy.Text   = Server.HtmlEncode(header["UploadedBy"].ToString());
        litUploadDate.Text   = Convert.ToDateTime(header["UploadDate"]).ToString("yyyy-MM-dd HH:mm");
        litPriorityFocus.Text = Server.HtmlEncode(header["PriorityFocus"].ToString());
        litProgramme.Text    = Server.HtmlEncode(header["IntegrationProgramme"].ToString());
        litGoal.Text         = Server.HtmlEncode(header["GoalName"].ToString());
        litImpact.Text       = Server.HtmlEncode(header["ImpactStatement"].ToString());

        // Pre-populate POA name from integration programme
        txtPOAName.Text = header["IntegrationProgramme"].ToString().Trim();

        // Build data preview table
        DataTable data = _dal.GetUploadData(RequestId);
        BuildPreviewTable(data);

        // Check status
        string status = header["Status"].ToString();
        bool isPending = status == "Pending";
        bool canApprove = (role == 32 || role == 41);

        if (!isPending)
        {
            pnlAlreadyReviewed.Visible = true;
            litCurrentStatus.Text      = status;
        }

        // Show approval form only to approvers on pending uploads
        pnlApprovalForm.Visible = (isPending && canApprove);

        if (pnlApprovalForm.Visible)
        {
            // Verify the submitter is not the current user (extra guard)
            int submitterId = Convert.ToInt32(header["UploadedByUserID"]);
            int currentUser = Convert.ToInt32(Session["UserID"]);
            if (submitterId == currentUser)
            {
                pnlApprovalForm.Visible  = false;
                pnlOwnerBanner.Visible   = true;
            }
            else
            {
                PopulatePriorityDropdown();
                ddlStartYear.SelectedValue = "2025";
                ddlEndYear.SelectedValue   = "2030";
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int priorityId = Convert.ToInt32(ddlPriority.SelectedValue);
        string poaName = txtPOAName.Text.Trim();
        int startYear  = Convert.ToInt32(ddlStartYear.SelectedValue);
        int endYear    = Convert.ToInt32(ddlEndYear.SelectedValue);
        string comment = txtComment.Text.Trim();
        int reviewerId = Convert.ToInt32(Session["UserID"]);

        if (priorityId == 0 || string.IsNullOrEmpty(poaName) || startYear == 0 || endYear == 0)
        {
            ShowError("Please complete all required mapping fields before approving.");
            return;
        }

        // Get cluster from selected priority
        DataTable priorities = _dal.GetPrioritiesForMapping();
        int clusterId = 0;
        foreach (DataRow r in priorities.Rows)
        {
            if (Convert.ToInt32(r["PMTDP_PriorityID"]) == priorityId)
            {
                clusterId = Convert.ToInt32(r["ClusterID"]);
                break;
            }
        }
        if (clusterId == 0)
        {
            ShowError("The selected PMTDP Priority does not have a Cluster assigned. Please configure the Priority first.");
            return;
        }

        try
        {
            int newPoaId = _applyDal.ApplyUpload(RequestId, priorityId, clusterId, poaName,
                startYear, endYear, reviewerId, comment);

            pnlApprovalForm.Visible = false;
            ShowSuccess(string.Format(
                "POA upload approved and records created successfully. " +
                "<a href=\"pagePOADetail.aspx?id={0}\" style=\"font-weight:700;\">View the new POA &rarr;</a>",
                newPoaId));
        }
        catch (Exception ex)
        {
            ShowError("Failed to apply the upload: " + ex.Message +
                (ex.InnerException != null ? " — " + ex.InnerException.Message : ""));
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        string comment = txtComment.Text.Trim();
        int reviewerId = Convert.ToInt32(Session["UserID"]);

        if (string.IsNullOrEmpty(comment))
        {
            ShowError("A rejection comment is required.");
            return;
        }

        try
        {
            _dal.Reject(RequestId, reviewerId, comment);
            pnlApprovalForm.Visible = false;
            ShowSuccess("Upload rejected. The submitter will see your comment.");
        }
        catch (Exception ex)
        {
            ShowError("Failed to reject: " + ex.Message);
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private void PopulatePriorityDropdown()
    {
        DataTable dt = _dal.GetPrioritiesForMapping();

        var jsonSb = new StringBuilder("{");
        bool first = true;
        foreach (DataRow row in dt.Rows)
        {
            string pId   = row["PMTDP_PriorityID"].ToString();
            string cId   = row["ClusterID"].ToString();
            string cName = row["ClusterName"].ToString().Replace("\"", "'");
            if (!first) jsonSb.Append(",");
            jsonSb.AppendFormat("\"{0}\":{{\"clusterId\":{1},\"clusterName\":\"{2}\"}}", pId, cId, cName);
            first = false;

            ddlPriority.Items.Add(new ListItem(
                string.Format("{0} ({1})", row["PriorityName"], row["ClusterName"]),
                pId));
        }
        jsonSb.Append("}");
        hfPriorityData.Value = jsonSb.ToString();
    }

    private void BuildPreviewTable(DataTable data)
    {
        string[] cols   = { "InterventionName","InterventionIndicator","IndicatorType",
                            "ImplementingInstitution","Baseline2023_24","Target2026_27",
                            "TermTarget2025_2030","AnnualBudget","SpatialReference" };
        string[] labels = { "Intervention","Indicator","Type",
                            "Institution","Baseline 23/24","Target 26/27",
                            "Term Target","Annual Budget","Spatial" };

        var sb = new StringBuilder();
        sb.Append("<table class='preview-table'><thead><tr>");
        for (int i = 0; i < labels.Length; i++)
            sb.AppendFormat("<th>{0}</th>", labels[i]);
        sb.Append("</tr></thead><tbody>");

        foreach (DataRow row in data.Rows)
        {
            sb.Append("<tr>");
            foreach (string col in cols)
            {
                string val = data.Columns.Contains(col)
                    ? HttpUtility.HtmlEncode((row[col] ?? "").ToString())
                    : "";
                sb.AppendFormat("<td>{0}</td>", val);
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");
        phDataTable.Controls.Add(new LiteralControl(sb.ToString()));
    }

    private void ShowError(string msg)
    {
        litError.Text      = msg;
        pnlError.Visible   = true;
        pnlSuccess.Visible = false;
    }

    private void ShowSuccess(string msg)
    {
        litSuccess.Text    = msg;
        pnlSuccess.Visible = true;
        pnlError.Visible   = false;
    }
}
