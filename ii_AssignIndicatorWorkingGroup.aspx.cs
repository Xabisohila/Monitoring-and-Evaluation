using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ii_AssignIndicatorWorkingGroup : System.Web.UI.Page
{
    private readonly c_WorkingGroupDAL _wgDal = new c_WorkingGroupDAL();
    private readonly c_IndicatorsDAL _indicatorDal = new c_IndicatorsDAL();
    private readonly cc_IndicatorWorkingGroupMapDAL _mapDal = new cc_IndicatorWorkingGroupMapDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWorkingGroups();
            BindIndicators();
            BindMappings();
        }
    }

    private void BindWorkingGroups()
    {
        ddlWorkingGroups.DataSource = _wgDal.GetAll();
        ddlWorkingGroups.DataTextField = "WorkingGroupName";
        ddlWorkingGroups.DataValueField = "WorkingGroupID";
        ddlWorkingGroups.DataBind();
    }

    private void BindIndicators()
    {
        ddlIndicators.DataSource = _indicatorDal.GetAll();
        ddlIndicators.DataTextField = "IndicatorName";
        ddlIndicators.DataValueField = "IndicatorID";
        ddlIndicators.DataBind();
    }

    private void BindMappings()
    {
        if (ddlWorkingGroups.Items.Count == 0) return;
        int wgId = int.Parse(ddlWorkingGroups.SelectedValue);
        gvMappings.DataSource = _mapDal.ListByWorkingGroup(wgId);
        gvMappings.DataBind();
    }

    protected void ddlWorkingGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMappings();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        int wgId = int.Parse(ddlWorkingGroups.SelectedValue);
        int indicatorId = int.Parse(ddlIndicators.SelectedValue);
        _mapDal.Upsert(new cc_IndicatorWorkingGroupMap { IndicatorID = indicatorId, WorkingGroupID = wgId });
        BindMappings();
    }

    protected void gvMappings_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            int mapId = int.Parse((string)e.CommandArgument);
            _mapDal.Delete(mapId);
            BindMappings();
        }
    }

    protected void gvMappings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // This event handler is available if you need to do additional row customization
    }

    protected string GetIndicatorName(object indicatorIdObj)
    {
        if (indicatorIdObj == null) return string.Empty;

        int indicatorId = Convert.ToInt32(indicatorIdObj);

        // Find the indicator name from the dropdown list (already loaded)
        var item = ddlIndicators.Items.FindByValue(indicatorId.ToString());
        if (item != null)
        {
            return item.Text;
        }

        // If not found in dropdown, fetch from database
        var indicator = _indicatorDal.GetByID(indicatorId);
        return indicator != null ? indicator.IndicatorName : "Unknown Indicator";
    }

    protected string GetWorkingGroupName(object workingGroupIdObj)
    {
        if (workingGroupIdObj == null) return string.Empty;

        int workingGroupId = Convert.ToInt32(workingGroupIdObj);

        // Find the working group name from the dropdown list (already loaded)
        var item = ddlWorkingGroups.Items.FindByValue(workingGroupId.ToString());
        if (item != null)
        {
            return item.Text;
        }

        // If not found in dropdown, fetch from database
        var workingGroup = _wgDal.GetByID(workingGroupId);
        return workingGroup != null ? workingGroup.WorkingGroupName : "Unknown Working Group";
    }
}