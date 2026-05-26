using Microsoft.Office.Interop.Excel;
using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_OutcomeAdmin : System.Web.UI.Page
{
    private readonly c_OutcomesDAL outDAL = new c_OutcomesDAL();
    private readonly c_PrioritiesDAL priDAL = new c_PrioritiesDAL();
    private readonly c_IntegrationProgrammesDAL progDAL = new c_IntegrationProgrammesDAL();

    private string CurrentSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "OutcomeName"; }
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
            BindDropdowns();
            BindGrid();
        }
    }

    private void BindDropdowns()
    {
        ddlPriority.DataSource = priDAL.GetAll();
        ddlPriority.DataTextField = "PriorityName";
        ddlPriority.DataValueField = "PriorityID";
        ddlPriority.DataBind();
        ddlPriority.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));

        ddlProgramme.DataSource = progDAL.GetAll();
        ddlProgramme.DataTextField = "ProgrammeName";
        ddlProgramme.DataValueField = "ProgrammeID";
        ddlProgramme.DataBind();
        ddlProgramme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
    }

    private void BindGrid()
    {
        var data = outDAL.GetAll();

        switch (CurrentSortExpression)
        {
            case "OutcomeID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.OutcomeID).ToList()
                    : data.OrderByDescending(x => x.OutcomeID).ToList();
                break;

            case "OutcomeName":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.OutcomeName).ToList()
                    : data.OrderByDescending(x => x.OutcomeName).ToList();
                break;

            case "PriorityID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.PriorityID).ToList()
                    : data.OrderByDescending(x => x.PriorityID).ToList();
                break;

            case "ProgrammeID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ProgrammeID).ToList()
                    : data.OrderByDescending(x => x.ProgrammeID).ToList();
                break;
        }

        gv.DataSource = data;
        gv.DataBind();
    }

    protected void gv_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            var hfP = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hfPriorityID");
            var hfR = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hfProgrammeID");
            var lblP = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblPriorityName");
            var lblR = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblProgrammeName");

            int id;
            if (hfP != null && lblP != null && int.TryParse(hfP.Value, out id))
            {
                var p = priDAL.GetByID(id);
                lblP.Text = p != null ? p.PriorityName : "";
            }
            if (hfR != null && lblR != null && int.TryParse(hfR.Value, out id))
            {
                var g = progDAL.GetByID(id);
                lblR.Text = g != null ? g.ProgrammeName : "";
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
        var model = new c_Outcome
        {
            OutcomeID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
            OutcomeName = txtName.Text,
            PriorityID = string.IsNullOrEmpty(ddlPriority.SelectedValue) ? (int?)null : Convert.ToInt32(ddlPriority.SelectedValue),
            ProgrammeID = string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? (int?)null : Convert.ToInt32(ddlProgramme.SelectedValue)
        };

        outDAL.Upsert(model);
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
            var m = outDAL.GetByID(id);
            if (m != null)
            {
                hfID.Value = m.OutcomeID.ToString();
                txtName.Text = m.OutcomeName;
                ddlPriority.SelectedValue = m.PriorityID.HasValue ? m.PriorityID.Value.ToString() : "";
                ddlProgramme.SelectedValue = m.ProgrammeID.HasValue ? m.ProgrammeID.Value.ToString() : "";
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            outDAL.Delete(id);
            BindGrid();
        }
    }

    private void ClearForm()
    {
        hfID.Value = "";
        txtName.Text = "";
        ddlPriority.SelectedIndex = 0;
        ddlProgramme.SelectedIndex = 0;
    }
}
