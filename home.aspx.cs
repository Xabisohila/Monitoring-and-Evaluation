using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            Response.Redirect("index.aspx");
            //if (Session["UserType"].ToString() == "33")
            //{
            //    //headerText.InnerText = "MONITORING AND EVALUATION (Approver)";
            //}
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
}