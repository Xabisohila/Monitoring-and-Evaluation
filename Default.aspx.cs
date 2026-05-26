using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Fullname"] != null){

        }
        else
        {
            Response.Redirect("login.aspx");
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if ((username.Text == "55797741") && (password.Text == "@Pass789"))
        {
            Response.Redirect("home.aspx"); 
        }
        else
        {
            lblError.Text = "Username or password incorrect, please login again";
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;

        }
        
    }
}