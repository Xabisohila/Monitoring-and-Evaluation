using System;
using System.Web.UI;

public partial class preview_dotnet_templates_akshara_multi_master_index : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        string fullname  = Session["Fullname"].ToString();
        string userType  = Session["UserTypeD"] != null ? Session["UserTypeD"].ToString() : "";

        Label1.Text = "Welcome, <strong>" + Server.HtmlEncode(fullname) + "</strong>";
        Label2.Text = userType;

        switch (userType)
        {
            case "Administrator":
                hlDashboard.NavigateUrl = "home.aspx";
                break;
            case "Planning Unit":
                hlDashboard.NavigateUrl = "pagePlanningOverview.aspx";
                break;
            case "Department/Entity":
                hlDashboard.NavigateUrl = "i_MyTasks.aspx";
                break;
            case "WG Coordinator":
            case "WG Convener":
                hlDashboard.NavigateUrl = "i_MyTasks.aspx";
                break;
            case "OTP Monitoring":
                hlDashboard.NavigateUrl = "i_MonitoringDashboard.aspx";
                break;
            case "HOD":
                hlDashboard.NavigateUrl = "ii_ApprovalInbox.aspx";
                break;
            case "Report Viewer":
                hlDashboard.NavigateUrl = "ReportSummary.aspx";
                break;
            default:
                Response.Redirect("login.aspx");
                return;
        }
    }
}
