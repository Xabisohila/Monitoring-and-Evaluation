using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MnE2.DAL;

public partial class i_Cluster : System.Web.UI.Page
{
    private readonly c_ClusterDAL dal = new c_ClusterDAL();

    private string CurrentSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "ClusterName"; }
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
            BindGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                var model = new c_Cluster
                {
                    ClusterID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
                    ClusterName = txtName.Text,
                    ClusterDescription = txtDescription.Text
                };

                dal.Upsert(model);
                ClearForm();
                BindGrid();
            }
            catch
            {
                // Handle error
                // Example: Show error message
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
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

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
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
                hfID.Value = m.ClusterID.ToString();
                txtName.Text = m.ClusterName;
                txtDescription.Text = m.ClusterDescription;
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            dal.Delete(id);
            ClearForm();
            BindGrid();
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        ClearForm();
        BindGrid();
    }

    private void BindGrid()
    {
        var data = dal.GetAll();

        switch (CurrentSortExpression)
        {
            case "ClusterID":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ClusterID).ToList()
                    : data.OrderByDescending(x => x.ClusterID).ToList();
                break;

            case "ClusterName":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ClusterName).ToList()
                    : data.OrderByDescending(x => x.ClusterName).ToList();
                break;

            case "ClusterDescription":
                data = CurrentSortDirection == SortDirection.Ascending
                    ? data.OrderBy(x => x.ClusterDescription).ToList()
                    : data.OrderByDescending(x => x.ClusterDescription).ToList();
                break;
        }

        gv.DataSource = data;
        gv.DataBind();
    }

    private void ClearForm()
    {
        hfID.Value = "";
        txtName.Text = "";
        txtDescription.Text = "";
    }

    private string GetSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["SortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;
        return sortDirection;
    }
}