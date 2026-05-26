using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_PriorityAdmin : System.Web.UI.Page
{
    private readonly c_PrioritiesDAL dal = new c_PrioritiesDAL();

    private string CurrentSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "PriorityName"; }
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
        if (!IsPostBack) Bind();
    }

    private void Bind()
    {
        var data = dal.GetAll();

        switch (CurrentSortExpression)
        {
            case "PriorityID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.PriorityID).ToList()
                    : data.OrderByDescending(x => x.PriorityID).ToList();
                break;

            case "PriorityName":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.PriorityName).ToList()
                    : data.OrderByDescending(x => x.PriorityName).ToList();
                break;

            case "Description":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.Description).ToList()
                    : data.OrderByDescending(x => x.Description).ToList();
                break;
        }

        gv.DataSource = data;
        gv.DataBind();
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

        Bind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var m = new c_Priority
        {
            PriorityID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
            PriorityName = txtName.Text,
            Description = txtDesc.Text
        };
        dal.Upsert(m);
        ClearForm();
        Bind();
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
                hfID.Value = m.PriorityID.ToString();
                txtName.Text = m.PriorityName;
                txtDesc.Text = m.Description;
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            dal.Delete(id);
            Bind();
        }
    }

    private void ClearForm()
    {
        hfID.Value = ""; txtName.Text = ""; txtDesc.Text = "";
    }
}
