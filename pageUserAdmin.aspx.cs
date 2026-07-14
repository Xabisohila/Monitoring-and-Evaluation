using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class pageUserAdmin : System.Web.UI.Page
{
    private readonly clsUser _user = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] == null || Convert.ToInt32(Session["UserType"]) != 32)
        {
            Response.Redirect("Index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadUsers();
            PopulateRoleDropdown();

            if (Request.QueryString["added"] == "1")
            {
                pnlSuccess.Visible  = true;
                litSuccessMsg.Text  = "User registered successfully.";
            }
        }
    }

    private void LoadUsers()
    {
        DataTable dt = _user.GetAllUsers();
        rptUsers.DataSource = dt;
        rptUsers.DataBind();
    }

    private void PopulateRoleDropdown()
    {
        DataSet ds = _user.PopulateOptions("UserType");
        ddlEditUserType.DataSource     = ds;
        ddlEditUserType.DataValueField = "OptionId";
        ddlEditUserType.DataTextField  = "Option";
        ddlEditUserType.DataBind();
        ddlEditUserType.Items.Insert(0, new ListItem("-- Select Role --", "0"));
    }

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        int userId = 0;
        if (!int.TryParse(hfEditUserId.Value, out userId) || userId <= 0)
        {
            ShowError("Invalid user selection. Please try again.");
            return;
        }

        int userType   = Convert.ToInt32(ddlEditUserType.SelectedValue);
        int activation = Convert.ToInt32(ddlEditActivation.SelectedValue);

        if (userType == 0)
        {
            ShowError("Please select a role before saving.");
            return;
        }

        try
        {
            _user.UpdateUserAccess(userId, userType, activation);
            LoadUsers();
            PopulateRoleDropdown();
            pnlSuccess.Visible = true;
            litSuccessMsg.Text = "User updated successfully.";
        }
        catch (Exception ex)
        {
            ShowError("Failed to update user: " + ex.Message);
        }
    }

    private void ShowError(string msg)
    {
        pnlError.Visible  = true;
        litErrorMsg.Text  = msg;
    }

    public string GetRoleCss(object userType)
    {
        switch (Convert.ToInt32(userType))
        {
            case 32: return "role-admin";
            case 37: return "role-planning";
            case 38: return "role-dept";
            case 39: return "role-wg";
            case 40: return "role-wg";
            case 41: return "role-otp";
            case 42: return "role-hod";
            case 43: return "role-viewer";
            default: return "role-other";
        }
    }
}
