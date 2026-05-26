using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

public partial class WorkflowStatusPanel : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<WorkflowStage> stages;
        try
        {
            stages = new WorkflowStatusDAL().GetStages();
        }
        catch
        {
            // SP not yet deployed — render nothing so the page still works
            return;
        }

        RenderPanel(stages);
    }

    private void RenderPanel(List<WorkflowStage> stages)
    {
        WorkflowStage active = null;
        bool allDone = true;

        foreach (var s in stages)
        {
            if (s.Status != "completed") allDone = false;
            if (s.Status == "active" && active == null) active = s;
        }

        // ── Toggle pill label + dot class (set via JS on page, driven by a hidden span)
        // We inject a small startup script instead of manipulating server controls
        string dotClass  = allDone ? "all-done" : (active != null ? "active" : "completed");
        string pillLabel = allDone
            ? "All steps complete &#10003;"
            : (active != null ? "Next: " + active.Name : "Workflow");

        Page.ClientScript.RegisterStartupScript(GetType(), "wfPill",
            string.Format(
                "document.getElementById('wfStatusDot').className='wf-dot {0}';" +
                "document.getElementById('wfStatusLabel').innerHTML='{1}';",
                dotClass,
                pillLabel.Replace("'", "&#39;")),
            true);

        // ── Next-action card
        var nc = new StringBuilder();
        if (allDone)
        {
            nc.Append("<div class=\"wf-next-card all-done\">");
            nc.Append("<div class=\"wf-nc-eyebrow\">All Stages Complete</div>");
            nc.Append("<div class=\"wf-nc-title\">Planning workflow is fully complete &#10003;</div>");
            nc.Append("</div>");
        }
        else if (active != null)
        {
            nc.Append("<div class=\"wf-next-card\">");
            nc.Append("<div class=\"wf-nc-eyebrow\">Next Required Step</div>");
            nc.AppendFormat("<div class=\"wf-nc-title\">{0}</div>", Encode(active.Name));

            if (!string.IsNullOrEmpty(active.CountInfo))
                nc.AppendFormat("<div class=\"wf-nc-detail\">{0}</div>", Encode(active.CountInfo));
            if (!string.IsNullOrEmpty(active.WaitingFor))
                nc.AppendFormat("<div class=\"wf-nc-detail\">{0}</div>", Encode(active.WaitingFor));

            nc.AppendFormat("<div class=\"wf-nc-role\">Responsible: <span>{0}</span></div>", Encode(active.ResponsibleRole));
            nc.AppendFormat("<a class=\"wf-nc-go\" href=\"{0}\">{1} &#8594;</a>", active.ActionUrl, Encode(active.ActionLabel));
            nc.Append("</div>");
        }
        litNextCard.Text = nc.ToString();

        // ── Blocked card (show when a stage is locked, explaining the blocker)
        WorkflowStage firstLocked = null;
        foreach (var s in stages)
        {
            if (s.Status == "locked") { firstLocked = s; break; }
        }

        if (firstLocked != null && !string.IsNullOrEmpty(firstLocked.BlockedBy))
        {
            var bc = new StringBuilder();
            bc.Append("<div class=\"wf-blocked-card\">");
            bc.Append("<div class=\"wf-bc-label\">&#9888; Waiting For</div>");
            bc.AppendFormat("<strong>{0}</strong> must be completed before <strong>{1}</strong> is available.",
                Encode(firstLocked.BlockedBy), Encode(firstLocked.Name));
            bc.Append("</div>");
            litBlockedCard.Text = bc.ToString();
        }

        // ── Stage list rows
        var sb = new StringBuilder();
        foreach (var s in stages)
        {
            string iconHtml;
            switch (s.Status)
            {
                case "completed": iconHtml = "&#10003;"; break;
                case "locked":    iconHtml = "&#128274;"; break;
                default:          iconHtml = s.Number.ToString(); break;
            }

            sb.AppendFormat("<div class=\"wf-stage-row\">");

            // Icon
            sb.AppendFormat("<div class=\"wf-sr-icon {0}\">{1}</div>", s.Status, iconHtml);

            // Body
            sb.Append("<div class=\"wf-sr-body\">");
            sb.AppendFormat("<div class=\"wf-sr-name {0}\">{1}</div>", s.Status, Encode(s.Name));

            if (!string.IsNullOrEmpty(s.CountInfo))
                sb.AppendFormat("<div class=\"wf-sr-count\">{0}</div>", Encode(s.CountInfo));
            else if (s.Status == "locked" && !string.IsNullOrEmpty(s.BlockedBy))
                sb.AppendFormat("<div class=\"wf-sr-count\">Blocked by: {0}</div>", Encode(s.BlockedBy));

            sb.Append("</div>");

            // Action link (only for active stages)
            if (s.Status == "active")
                sb.AppendFormat("<a class=\"wf-sr-action\" href=\"{0}\">{1}</a>", s.ActionUrl, Encode(s.ActionLabel));

            sb.Append("</div>");
        }
        litStages.Text = sb.ToString();
    }

    private static string Encode(string s)
    {
        return System.Web.HttpUtility.HtmlEncode(s ?? string.Empty);
    }
}
