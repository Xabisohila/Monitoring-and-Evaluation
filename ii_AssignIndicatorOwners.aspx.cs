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
        cblIndicators.DataSource = _indicatorDal.GetAll();
        cblIndicators.DataTextField = "IndicatorName";
        cblIndicators.DataValueField = "IndicatorID";
        cblIndicators.DataBind();
    }

    private void BindInstitutions()
    {
        DataTable dt = _institutionDal.GetAllLeadInstitutionsLookup();
        cblInstitution.DataSource = dt;
        cblInstitution.DataTextField = "InstitutionName";
        cblInstitution.DataValueField = "InstitutionID";
        cblInstitution.DataBind();
    }

    private void BindOwners()
    {
        var list = _ownerDal.ListAll();
        gvOwners.DataSource = list;
        gvOwners.DataBind();
        int count = list != null ? list.Count : 0;
        lblOwnerCount.Text    = count + " assignment" + (count != 1 ? "s" : "");
        lblOwnerCount.Visible = count > 0;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        var selectedIndicators  = new System.Collections.Generic.List<int>();
        var selectedInstitutions = new System.Collections.Generic.List<int>();

        foreach (ListItem item in cblIndicators.Items)
            if (item.Selected) selectedIndicators.Add(int.Parse(item.Value));

        foreach (ListItem item in cblInstitution.Items)
            if (item.Selected) selectedInstitutions.Add(int.Parse(item.Value));

        if (selectedIndicators.Count == 0 || selectedInstitutions.Count == 0)
        {
            ShowMsg("Please select at least one indicator and one department.", false);
            return;
        }

        int assigned = 0;
        foreach (int indId in selectedIndicators)
            foreach (int instId in selectedInstitutions)
            {
                _ownerDal.Upsert(new cc_IndicatorOwner { IndicatorID = indId, InstitutionID = instId });
                assigned++;
            }

        BindOwners();
        ShowMsg(assigned + " assignment" + (assigned != 1 ? "s" : "") + " saved successfully.", true);
    }

    protected void gvOwners_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveOwner")
        {
            int ownerId = int.Parse((string)e.CommandArgument);
            _ownerDal.Delete(ownerId);
            BindOwners();
        }
    }

    protected void gvOwners_RowDataBound(object sender, GridViewRowEventArgs e) { }

    protected string GetIndicatorName(object indicatorIdObj)
    {
        if (indicatorIdObj == null) return string.Empty;
        int indicatorId = Convert.ToInt32(indicatorIdObj);
        ListItem item = cblIndicators.Items.FindByValue(indicatorId.ToString());
        if (item != null) return item.Text;
        var indicator = _indicatorDal.GetByID(indicatorId);
        return indicator != null ? indicator.IndicatorName : "Unknown Indicator";
    }

    protected string GetInstitutionName(object institutionIdObj)
    {
        if (institutionIdObj == null) return string.Empty;
        int institutionId = Convert.ToInt32(institutionIdObj);
        ListItem item = cblInstitution.Items.FindByValue(institutionId.ToString());
        if (item != null) return item.Text;
        DataTable dt = _institutionDal.GetAllLeadInstitutionsLookup();
        DataRow[] rows = dt.Select("InstitutionID = " + institutionId);
        return rows.Length > 0 ? rows[0]["InstitutionName"].ToString() : "Unknown Department";
    }

    private void ShowMsg(string text, bool success)
    {
        lblMsg.Text     = text;
        lblMsg.CssClass = "msg-bar visible " + (success ? "msg-success" : "msg-error");
    }
}
