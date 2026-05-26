using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MnE2.DAL;

public partial class ii_AssignIndicatorOwners : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL _indicatorDal = new c_IndicatorsDAL();
    private readonly InterventionDAL _institutionDal = new InterventionDAL();
    private readonly cc_IndicatorOwnerDAL _ownerDal = new cc_IndicatorOwnerDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindIndicators();
            BindInstitutions();
            BindOwners();
        }
    }

    private void BindIndicators()
    {
        ddlIndicators.DataSource = _indicatorDal.GetAll();
        ddlIndicators.DataTextField = "IndicatorName";
        ddlIndicators.DataValueField = "IndicatorID";
        ddlIndicators.DataBind();
        ddlIndicators.Items.Insert(0, new ListItem("-- Select Indicator --", ""));
    }

    private void BindInstitutions()
    {
        DataTable dt = _institutionDal.GetAllLeadInstitutionsLookup();
        ddlInstitution.DataSource = dt;
        ddlInstitution.DataTextField = "InstitutionName";
        ddlInstitution.DataValueField = "InstitutionID";
        ddlInstitution.DataBind();
        ddlInstitution.Items.Insert(0, new ListItem("-- Select Department --", ""));
    }

    private void BindOwners()
    {
        if (string.IsNullOrEmpty(ddlIndicators.SelectedValue)) { gvOwners.DataSource = null; gvOwners.DataBind(); return; }
        int indicatorId = int.Parse(ddlIndicators.SelectedValue);
        gvOwners.DataSource = _ownerDal.ListByIndicator(indicatorId);
        gvOwners.DataBind();
    }

    protected void ddlIndicators_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOwners();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlIndicators.SelectedValue) || string.IsNullOrEmpty(ddlInstitution.SelectedValue))
            return;

        int indicatorId = int.Parse(ddlIndicators.SelectedValue);
        int institutionId = int.Parse(ddlInstitution.SelectedValue);

        _ownerDal.Upsert(new cc_IndicatorOwner { IndicatorID = indicatorId, InstitutionID = institutionId });

        BindOwners();
    }

    protected void gvOwners_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int ownerId = int.Parse((string)e.CommandArgument);
            _ownerDal.Delete(ownerId);
            BindOwners();
        }
    }

    protected void gvOwners_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected string GetIndicatorName(object indicatorIdObj)
    {
        if (indicatorIdObj == null) return string.Empty;
        int indicatorId = Convert.ToInt32(indicatorIdObj);
        ListItem item = ddlIndicators.Items.FindByValue(indicatorId.ToString());
        if (item != null) return item.Text;
        var indicator = _indicatorDal.GetByID(indicatorId);
        return indicator != null ? indicator.IndicatorName : "Unknown Indicator";
    }

    protected string GetInstitutionName(object institutionIdObj)
    {
        if (institutionIdObj == null) return string.Empty;
        int institutionId = Convert.ToInt32(institutionIdObj);
        ListItem item = ddlInstitution.Items.FindByValue(institutionId.ToString());
        if (item != null) return item.Text;
        DataTable dt = _institutionDal.GetAllLeadInstitutionsLookup();
        DataRow[] rows = dt.Select("InstitutionID = " + institutionId);
        return rows.Length > 0 ? rows[0]["InstitutionName"].ToString() : "Unknown Department";
    }
}
