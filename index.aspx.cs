using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class preview_dotnet_templates_akshara_multi_master_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            Label1.Text = "Welcome, " + Session["Fullname"].ToString();
            if (Session["UserTypeD"].ToString() == "Administrator") // 32
            {
                // Additional logic can be placed here        
                Label2.Text = "Administrator";
            }
            else if (Session["UserTypeD"].ToString() == "Planning Unit") // 37
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "Planning Unit";
            }
            else if (Session["UserTypeD"].ToString() == "Department/Entity") // 38
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "Department/Entity";
            }
            else if (Session["UserTypeD"].ToString() == "WG Coordinator") // 39
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "WG Coordinator";
            }
            else if (Session["UserTypeD"].ToString() == "WG Convener") // 40
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "WG Convener";
            }
            else if (Session["UserTypeD"].ToString() == "OTP Monitoring") // 41
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "OTP Monitoring";
            }
            else if (Session["UserTypeD"].ToString() == "HOD") // 42
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "HOD";
            }
            else if (Session["UserTypeD"].ToString() == "Report Viewer") // 43
            {
                //Response.Redirect("admin/dashboard.aspx");
                Label2.Text = "Report Viewer";
            }
            else
            {
                // For unknown user types, redirect to login
                Response.Redirect("login.aspx");
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    public void MenuSettings()
    {
        // This method can be used to set menu visibility based on user type
        // For example:
        // if (Session["UserTypeD"].ToString() == "Administrator")
        // {
        //     adminMenu.Visible = true;
        //     planningUnitMenu.Visible = false;
        //     // Set visibility for other menus accordingly
        // }

        if(Session["UserTypeD"].ToString() == "Administrator")
        {
            // Show admin menu
            
        }
        else if(Session["UserTypeD"].ToString() == "Planning Unit")
        {
            // Show planning unit menu
        }
    }
}