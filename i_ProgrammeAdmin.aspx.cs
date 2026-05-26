using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_ProgrammeAdmin : System.Web.UI.Page
{
    private readonly c_IntegrationProgrammesDAL dal = new c_IntegrationProgrammesDAL();
    private readonly c_DepartmentDAL deptDAL = new c_DepartmentDAL();

    private string CurrentSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "ProgrammeName"; }
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
        //RequireRole(/* Planning/Admin role IDs */);
        if (!IsPostBack)
        {
            BindDepartments();
            BindGrid();
        }
    }

    private void BindDepartments()
    {
        var depts = deptDAL.GetAll();
        ddlLeaderDept.DataSource = depts;
        ddlLeaderDept.DataTextField = "DepartmentName";
        ddlLeaderDept.DataValueField = "DepartmentID";
        ddlLeaderDept.DataBind();
        ddlLeaderDept.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- None --", ""));
    }

    private void BindGrid()
    {
        var data = dal.GetAll();

        switch (CurrentSortExpression)
        {
            case "ProgrammeID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ProgrammeID).ToList()
                    : data.OrderByDescending(x => x.ProgrammeID).ToList();
                break;

            case "ProgrammeName":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ProgrammeName).ToList()
                    : data.OrderByDescending(x => x.ProgrammeName).ToList();
                break;

            case "LeaderDeptID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.LeaderDeptID).ToList()
                    : data.OrderByDescending(x => x.LeaderDeptID).ToList();
                break;
        }

        gv.DataSource = data;
        gv.DataBind();
    }

    protected void gv_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            var hf = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hfDeptID");
            var lbl = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblDeptName");
            int id;
            if (hf != null && lbl != null && int.TryParse(hf.Value, out id))
            {
                var dept = deptDAL.GetByID(id);
                lbl.Text = dept != null ? dept.DepartmentName : "";
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
        int? leaderDeptId = string.IsNullOrEmpty(ddlLeaderDept.SelectedValue)
            ? (int?)null
            : Convert.ToInt32(ddlLeaderDept.SelectedValue);

        var model = new c_IntegrationProgramme
        {
            ProgrammeID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
            ProgrammeName = txtName.Text,
            LeaderDeptID = leaderDeptId
        };

        dal.Upsert(model);
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
            var m = dal.GetByID(id);
            if (m != null)
            {
                hfID.Value = m.ProgrammeID.ToString();
                txtName.Text = m.ProgrammeName;
                ddlLeaderDept.SelectedValue = m.LeaderDeptID.HasValue ? m.LeaderDeptID.Value.ToString() : "";
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            dal.Delete(id);
            ClearForm();
            BindGrid();
        }
    }
    
    private void ClearForm()
    {
        hfID.Value = "";
        txtName.Text = "";
        ddlLeaderDept.SelectedIndex = 0;
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        ClearForm(); // Add this line to clear the hidden field
        BindGrid();
    }

}
