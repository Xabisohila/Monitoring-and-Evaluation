using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class preview_dotnet_templates_akshara_multi_master_akshara : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserTypeSettings();
        //SetCurrentPage();
        if (!IsPostBack) 
        {
            UserTypeSettings();
        }

    }


    private void UserTypeSettings()
    {
        if (Session["UserTypeD"] != null)
        {
            if(Session["UserTypeD"].ToString() == "Administrator")
            {
                ClusterA1_li.Visible = true;
                ddl_PlanningUnit.Visible = true;
                ddl_CapturerUnit.Visible = true;
                ddl_HOD_CEO.Visible = true;
                updates1.Visible = true;

                ddl_PU_Inbox.Visible = true;

                ClusterWG_0.Visible = false;

                pnlWorkflow.Visible = true;
            }
            else if (Session["UserTypeD"].ToString() == "Planning Unit")
            {
                //ClusterA1_li.Visible = false;
                //ClusterWG_1.Visible = false;
                //ClusterWG_0.Visible = false;
                //Reports_0.Visible = false;
                //Departments_0.Visible = false;
                //Calendar_0.Visible = false;
                //DD_PlanningUnit.Visible = false;
                ddl_PU_Upload.Visible = true;
                ddl_PU_Inbox.Visible = true;

                pnlWorkflow.Visible = true;
            }
            else if (Session["UserTypeD"].ToString() == "Department/Entity")
            {
                Dept_Entiry.Visible = true;
            }
            else if (Session["UserTypeD"].ToString() == "WG Coordinator")
            {
            }
            else if (Session["UserTypeD"].ToString() == "WG Convener")
            {
            }
            else if (Session["UserTypeD"].ToString() == "OTP Monitoring")
            {
            }
            else if (Session["UserTypeD"].ToString() == "HOD")
            {
            }
            else if (Session["UserTypeD"].ToString() == "Report Viewer")
            {
            }
            else
            {
            }
                
        }

    }





    //private void SetCurrentPage()
    //{
        //    var pageName = GetPageName();

    //    switch (pageName)
    //    {
    //        case "index.aspx":
    //            home.Attributes["class"] = "active";
    //            break;
    //        case "services.aspx":
    //            service.Attributes["class"] = "active";
    //            break;
    //        case "portfolio.aspx":
    //            portfolio.Attributes["class"] = "active";
    //            break;           
    //        case "blog.aspx":
    //            blog.Attributes["class"] = "active";
    //            break;
    //        case "contact.aspx":
    //            contact.Attributes["class"] = "active";
    //            break;
    //        //case "contact.htm":
    //        //    contact.Attributes["class"] = "active";
    //        //    break;
    //    }
    //}
    private string GetPageName()
    {
        return Request.Url.ToString().Split('/').Last();
    }
}
