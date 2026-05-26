using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POA : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!Page.IsPostBack)
            {

            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Session["testMode"] = rblMode.SelectedValue;
        Response.Redirect("testFilterSelection.aspx");
    }
}


