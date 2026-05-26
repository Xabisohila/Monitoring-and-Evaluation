using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    clsUser oUser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!Page.IsPostBack)
            {
                populateDistrictMunicipality();
                populateDepartment();
                populateTitle();
                populateActive();
                populateUserType();
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

    public void populateDistrictMunicipality()
    {
        ddlDistrict.DataSource = oUser.PopulateOptions("District");
        ddlDistrict.DataValueField = "OptionId";
        ddlDistrict.DataTextField = "Option";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("--Please Select District--", "0"));
    }

    public void populateDepartment()
    {
        ddlDepartment.DataSource = oUser.PopulateOptions("Department");
        ddlDepartment.DataValueField = "OptionId";
        ddlDepartment.DataTextField = "Option";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("--Please Select Department--", "0"));
    }

    public void populateTitle()
    {
        ddlTitle.DataSource = oUser.PopulateOptions("Title");
        ddlTitle.DataValueField = "OptionId";
        ddlTitle.DataTextField = "Option";
        ddlTitle.DataBind();
        ddlTitle.Items.Insert(0, new ListItem("--Please Select Title--", "0"));
    }

    public void populateActive()
    {
        ddlActivation.DataSource = oUser.PopulateOptions("Active");
        ddlActivation.DataValueField = "OptionId";
        ddlActivation.DataTextField = "Option";
        ddlActivation.DataBind();
        ddlActivation.Items.Insert(0, new ListItem("--Please Select Status--", "0"));
    }

    public void populateUserType()
    {
        ddlUserType.DataSource = oUser.PopulateOptions("UserType");
        ddlUserType.DataValueField = "OptionId";
        ddlUserType.DataTextField = "Option";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, new ListItem("--Please Select User Type--", "0"));
    }


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        oUser.PersalNumber = txtPersalNumber.Text;
        oUser.Password = txtPassword.Text;
        oUser.Firstname = txtFirstname.Text;
        oUser.Lastname = txtLastname.Text;
        oUser.Title = Convert.ToInt16(ddlTitle.SelectedValue);
        oUser.EmailAddress = txtEmailAddress.Text;
        oUser.Phone = txtPhone.Text;
        oUser.Department = Convert.ToInt16(ddlDepartment.SelectedValue);
        oUser.District = Convert.ToInt16(ddlDistrict.SelectedValue);
        oUser.Designation = txtDesignation.Text;
        oUser.UserType = Convert.ToInt16(ddlUserType.SelectedValue);
        oUser.Activation = Convert.ToInt16(ddlActivation.SelectedValue);
        oUser.RegisterUser();
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Registration Successful!')", true);
        Clear();
    }

    public void Clear()
    {
        txtPersalNumber.Text = "";
        txtPassword.Text = "";
        txtFirstname.Text = "";
        txtLastname.Text = "";
        ddlTitle.SelectedIndex = 0;
        txtEmailAddress.Text = "";
        txtPhone.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlDistrict.SelectedIndex = 0;
        txtDesignation.Text = "";
        ddlActivation.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
    }
}