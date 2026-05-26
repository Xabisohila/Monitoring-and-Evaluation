using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class pu_ClusterSetup : System.Web.UI.Page
{
    private readonly cls_PU_ClusterSetup _repo = new cls_PU_ClusterSetup();

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        gvClusters.RowDataBound += gvClusters_RowDataBound;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        lblErr.Text = string.Empty;
        gvClusters.DataSource = _repo.GetAll();
        gvClusters.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblErr.Text = "Cluster Name is required.";
                return;
            }

            var newCluster = new Cluster
            {
                ClusterName = txtName.Text.Trim(),
                ClusterDescription = string.IsNullOrWhiteSpace(txtDesc.Text) ? null : txtDesc.Text.Trim()
            };

            var newId = _repo.Create(newCluster);
            txtName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error creating cluster: " + ex.Message;
        }
    }

    protected void gvClusters_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        gvClusters.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvClusters_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        gvClusters.EditIndex = -1;
        BindGrid();
    }

    protected void gvClusters_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(gvClusters.DataKeys[e.RowIndex].Value);

            var row = gvClusters.Rows[e.RowIndex];
            var txtNameEdit = (System.Web.UI.WebControls.TextBox)row.FindControl("txtNameEdit");
            var txtDescEdit = (System.Web.UI.WebControls.TextBox)row.FindControl("txtDescEdit");

            if (txtNameEdit == null)
            {
                lblErr.Text = "Cannot find edit controls.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNameEdit.Text))
            {
                lblErr.Text = "Cluster Name is required.";
                return;
            }

            var c = new Cluster
            {
                ClusterID = id,
                ClusterName = txtNameEdit.Text.Trim(),
                ClusterDescription = txtDescEdit != null && !string.IsNullOrWhiteSpace(txtDescEdit.Text)
                    ? txtDescEdit.Text.Trim()
                    : null
            };

            bool ok = _repo.Update(c);
            if (!ok)
            {
                lblErr.Text = "Update failed (record may not exist).";
            }

            gvClusters.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error updating: " + ex.Message;
        }
    }

    protected void gvClusters_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(gvClusters.DataKeys[e.RowIndex].Value);
            bool ok = _repo.Delete(id);
            if (!ok)
            {
                lblErr.Text = "Delete failed (record may not exist).";
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error deleting: " + ex.Message;
        }
    }

    protected void gvClusters_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType != System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            return;
        }

        foreach (Control ctl in e.Row.Cells)
        {
            var deleteButton = ctl as System.Web.UI.WebControls.LinkButton;
            if (deleteButton == null && ctl.HasControls())
            {
                deleteButton = FindDeleteButtonRecursive(ctl);
            }

            if (deleteButton != null && string.Equals(deleteButton.CommandName, "Delete", StringComparison.OrdinalIgnoreCase))
            {
                deleteButton.OnClientClick = "return confirm('Are you sure you want to delete this cluster?');";
                break;
            }
        }

        if (e.Row.RowIndex == gvClusters.EditIndex)
        {
            foreach (Control ctl in e.Row.Cells)
            {
                DataControlFieldCell cell = ctl as DataControlFieldCell;
                if (cell != null)
                {
                    foreach (Control innerCtl in cell.Controls)
                    {
                        var updateButton = innerCtl as LinkButton;
                        if (updateButton != null && updateButton.CommandName.Equals("Update", StringComparison.OrdinalIgnoreCase))
                        {
                            updateButton.OnClientClick = "return confirm('Are you sure you want to update this cluster?');";
                            return;
                        }
                    }
                }
            }
        }
    }

    private static System.Web.UI.WebControls.LinkButton FindDeleteButtonRecursive(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            var lb = child as System.Web.UI.WebControls.LinkButton;
            if (lb != null && string.Equals(lb.CommandName, "Delete", StringComparison.OrdinalIgnoreCase))
            {
                return lb;
            }

            if (child.HasControls())
            {
                var found = FindDeleteButtonRecursive(child);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}

