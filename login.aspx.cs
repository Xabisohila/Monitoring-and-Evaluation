using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading;

public partial class preview_dotnet_templates_akshara_multi_master_login : System.Web.UI.Page
{
    clsUser oUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private void Reset()
    {
        txtPersalNumber.Text = string.Empty;
        txtPassword.Text = string.Empty;
        lblError.Visible = false;
        Session.Clear();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPersalNumber.Text == string.Empty & txtPassword.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your username and password')", true); 
            }
            else if (txtPersalNumber.Text == string.Empty)
            {
                
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your username')", true);
            }
            else if (txtPassword.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your password')", true);
               

            }
            else
            {

                string PersalNumber = txtPersalNumber.Text;
                oUser.LoginDetails(Convert.ToInt32(PersalNumber));

                string Password = txtPassword.Text;
                string dbPersalNumber = oUser.PersalNumber;
                int dbUserType = oUser.UserType;
                string dbPassword = oUser.Password;
                int Activation = oUser.Activation;
                int dbDepartmentId = oUser.Department;

                int dbUserID = oUser.UserID;
                

                Session["Fullname"] = oUser.Fullname; // Name and Surname
                Session["EmailAddress"] = oUser.EmailAddress; // Work email address
                Session["EmployerDepartment"] = oUser.Department;// Department ID
                Session["UserType"] = Convert.ToString(dbUserType); // UserType Number in Options

                Session["dbUserID"] = dbUserID;// Loggedin UserID


                //New session variables for user type and department
                //Session["UserTypeD"] = oUser.UserTypeD;
                

                Session["UserID"] = dbUserID;
                Session["RoleID"] = dbUserType;
                Session["FullName"] = oUser.Fullname;
                Session["Department"] = oUser.Department;
                Session["Email"] = oUser.EmailAddress;
                Session["Activation"] = oUser.Activation;
                Session["UserPersalNumber"] = oUser.PersalNumber;




                // Basic credential check
                if (Password != dbPassword || PersalNumber != dbPersalNumber)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Persal Number or Password entered is incorrect')", true);
                    return;
                }

                // Activation check applies to all user types
                if (Activation != 20)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('This profile is not activated, please contact your administrator')", true);
                    return;
                }

                // Route by UserType
                switch (dbUserType)
                {
                    case 32: // OTP Capturer / Administrator (as per existing comment)
                        Session["UserTypeD"] = "Administrator";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;

                    // Add other known user types here. For now, set a generic role name and route to Index.
                    // Replace "UserType_<id>" string with concrete role names when available.
                    
                    case 37:// OTP Capturer / Administrator (as per existing comment)
                        Session["UserTypeD"] = "Planning Unit";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 38:
                        Session["UserTypeD"] = "Department/Entity";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 39:
                        Session["UserTypeD"] = "WG Coordinator";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 40:
                        Session["UserTypeD"] = "WG Convener";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 41:
                        Session["UserTypeD"] = "OTP Monitoring";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 42:
                        Session["UserTypeD"] = "HOD";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    case 43:
                        Session["UserTypeD"] = "Report Viewer";
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    default:
                        // Unknown user type fallback
                        Session["UserTypeD"] = "UserType_" + dbUserType;
                        Response.Redirect("Index.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                }
            }

        }
        //catch (ThreadAbortException)
        //{
        //    // Don't handle ThreadAbortException - it's expected during redirects
        //    throw;
        //}
        catch (Exception ex)
        {

            lblError.Text = ex.Message;
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
        }

        
    }
}