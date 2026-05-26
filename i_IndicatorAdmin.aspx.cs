using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_IndicatorAdmin : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL indDAL = new c_IndicatorsDAL();
    private readonly c_OutcomesDAL outDAL = new c_OutcomesDAL();

    private string CurrentSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "IndicatorName"; }
        set { ViewState["SortExpression"] = value; }
    }

    private SortDirection CurrentSortDirection
    {
        get
        {
            return ViewState["SortDirection"] == null
                ? SortDirection.Ascending
                : (SortDirection)ViewState["SortDirection"];
        }
        set { ViewState["SortDirection"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Planning/Admin */);
        if (!IsPostBack)
        {
            BindOutcomes();
            BindGrid();
        }
    }

    private void BindOutcomes()
    {
        var outs = outDAL.GetAll();
        ddlOutcome.DataSource = outs;
        ddlOutcome.DataTextField = "OutcomeName";
        ddlOutcome.DataValueField = "OutcomeID";
        ddlOutcome.DataBind();
        ddlOutcome.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
    }

    private void BindGrid()
    {
        var data = indDAL.GetAll();

        switch (CurrentSortExpression)
        {
            case "IndicatorID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.IndicatorID).ToList()
                    : data.OrderByDescending(x => x.IndicatorID).ToList();
                break;

            case "IndicatorName":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.IndicatorName).ToList()
                    : data.OrderByDescending(x => x.IndicatorName).ToList();
                break;

            case "IndicatorType":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.IndicatorType).ToList()
                    : data.OrderByDescending(x => x.IndicatorType).ToList();
                break;

            case "OutcomeID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.OutcomeID).ToList()
                    : data.OrderByDescending(x => x.OutcomeID).ToList();
                break;

            case "IsCumulative":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.IsCumulative).ToList()
                    : data.OrderByDescending(x => x.IsCumulative).ToList();
                break;

            case "IsPercentage":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.IsPercentage).ToList()
                    : data.OrderByDescending(x => x.IsPercentage).ToList();
                break;
        }

        gv.DataSource = data;
        gv.DataBind();
    }

    protected void gv_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            var hf = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hfOutcomeID");
            var lbl = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblOutcomeName");
            int id;
            if (hf != null && lbl != null && int.TryParse(hf.Value, out id))
            {
                var o = outDAL.GetByID(id);
                lbl.Text = o != null ? o.OutcomeName : "";
            }
        }
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (string.Equals(CurrentSortExpression, e.SortExpression, StringComparison.OrdinalIgnoreCase))
        {
            CurrentSortDirection = CurrentSortDirection == SortDirection.Ascending
                ? SortDirection.Descending
                : SortDirection.Ascending;
        }
        else
        {
            CurrentSortExpression = e.SortExpression;
            CurrentSortDirection = SortDirection.Ascending;
        }

        BindGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal budget = 0;
        decimal.TryParse(txtBudget.Text, out budget);

        var model = new c_Indicator
        {
            IndicatorID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
            IndicatorName = txtName.Text,
            IndicatorType = txtType.Text,
            OutcomeID = Convert.ToInt32(ddlOutcome.SelectedValue),
            BaselineValue = txtBaseline.Text,
            TermTargetValue = txtTermTarget.Text,
            AnnualBudget = budget,
            ImplementingInstitution = txtImpl.Text,
            SupportingInstitutions = txtSupp.Text,
            CalculationType = txtCalc.Text,
            ReportingCycle = txtCycle.Text,
            IsCumulative = chkCumulative.Checked,
            IsPercentage = chkPercentage.Checked
        };

        indDAL.Upsert(model);
        ClearForm();
        BindGrid();
    }

    protected void gv_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int rowIndex;
        if (!int.TryParse(Convert.ToString(e.CommandArgument), out rowIndex))
        {
            return;
        }

        // Read the hidden key safely regardless of column visibility or sorting/paging
        int id = Convert.ToInt32(gv.DataKeys[rowIndex].Value);

        if (e.CommandName == "EditRow")
        {
            var m = indDAL.GetByID(id);
            if (m != null)
            {
                hfID.Value = m.IndicatorID.ToString();
                ddlOutcome.SelectedValue = m.OutcomeID.ToString();
                txtName.Text = m.IndicatorName;
                txtType.Text = m.IndicatorType;
                txtBaseline.Text = m.BaselineValue;
                txtTermTarget.Text = m.TermTargetValue;
                txtBudget.Text = m.AnnualBudget.ToString("0.##");
                txtImpl.Text = m.ImplementingInstitution;
                txtSupp.Text = m.SupportingInstitutions;
                txtCalc.Text = m.CalculationType;
                txtCycle.Text = m.ReportingCycle;
                chkCumulative.Checked = m.IsCumulative;
                chkPercentage.Checked = m.IsPercentage;
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            indDAL.Delete(id);
            BindGrid();
        }
    }

    private void ClearForm()
    {
        hfID.Value = "";
        ddlOutcome.SelectedIndex = 0;
        txtName.Text = txtType.Text = txtBaseline.Text = txtTermTarget.Text = "";
        txtBudget.Text = "";
        txtImpl.Text = txtSupp.Text = txtCalc.Text = txtCycle.Text = "";
        chkCumulative.Checked = chkPercentage.Checked = false;
    }
}
