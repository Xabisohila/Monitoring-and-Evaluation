using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addEditMunicipality : System.Web.UI.Page
{
    private cls_MunicipalityDAL dal = new cls_MunicipalityDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistricts();

            if (Request.QueryString["id"] != null)
            {
                int municipalityId = Convert.ToInt32(Request.QueryString["id"]);
                LoadMunicipality(municipalityId);
                btnDelete.Visible = true;
            }
        }
    }

    private void LoadDistricts()
    {
        DataTable dt = dal.GetAllDistricts();
        ddlDistrict.DataSource = dt;
        ddlDistrict.DataTextField = "DistrictName";
        ddlDistrict.DataValueField = "DistrictID";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
    }

    private void LoadMunicipality(int id)
    {
        DataRow dr = dal.GetMunicipalityById(id);
        if (dr != null)
        {
            txtMunicipalityName.Text = dr["MunicipalityName"].ToString();
            ddlDistrict.SelectedValue = dr["DistrictID"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string name = txtMunicipalityName.Text.Trim();
            int districtId = Convert.ToInt32(ddlDistrict.SelectedValue);

            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                dal.UpdateMunicipality(id, name, districtId);
                lblMessage.Text = "Municipality updated successfully.";
            }
            else
            {
                dal.InsertMunicipality(name, districtId);
                lblMessage.Text = "Municipality added successfully.";
                txtMunicipalityName.Text = "";
                ddlDistrict.SelectedIndex = 0;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            dal.DeleteMunicipality(id);
            lblMessage.Text = "Municipality deleted successfully.";
            txtMunicipalityName.Text = "";
            ddlDistrict.SelectedIndex = 0;
            btnDelete.Visible = false;
        }
    }
}
