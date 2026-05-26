using MnE2.DAL;
using System;

using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Targets_AnnualTargets : BasePage
{
    private readonly c_IndicatorsDAL indicators = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL annual = new c_AnnualTargetsDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        RequireRole(/* Admin, Planning, etc. role IDs */);
        if (!IsPostBack)
        {
            BindIndicators();
            BindGrid();
        }
    }

    private void BindIndicators()
    {
        ddlIndicator.DataSource = indicators.GetAll();
        ddlIndicator.DataBind();
        ddlIndicator.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
    }

    private void BindGrid()
    {
        gvAnnualTargets.DataSource = annual.GetAll();
        gvAnnualTargets.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        var model = new c_AnnualTarget
        {
            AnnualTargetID = string.IsNullOrWhiteSpace(hfAnnualTargetID.Value) ? 0 : Convert.ToInt32(hfAnnualTargetID.Value),
            IndicatorID = Convert.ToInt32(ddlIndicator.SelectedValue),
            FinancialYear = Convert.ToInt32(txtFY.Text),
            AnnualTargetValue = txtAnnualTarget.Text
        };

        annual.Upsert(model);
        ClearForm();
        BindGrid();
    }

    protected void gvAnnualTargets_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            var id = Convert.ToInt32(gvAnnualTargets.Rows[rowIndex].Cells[0].Text);
            var m = annual.GetByID(id);
            if (m != null)
            {
                hfAnnualTargetID.Value = m.AnnualTargetID.ToString();
                ddlIndicator.SelectedValue = m.IndicatorID.ToString();
                txtFY.Text = m.FinancialYear.ToString();
                txtAnnualTarget.Text = m.AnnualTargetValue;
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            var id = Convert.ToInt32(gvAnnualTargets.Rows[rowIndex].Cells[0].Text);
            annual.Delete(id);
            BindGrid();
        }
    }

    private void ClearForm()
    {
        hfAnnualTargetID.Value = "";
        ddlIndicator.SelectedIndex = 0;
        txtFY.Text = "";
        txtAnnualTarget.Text = "";
    }
}