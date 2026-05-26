using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pu_ImplementationInstitutionsSetup : System.Web.UI.Page
{
    private readonly cls_PU_ImplementationInstitutionSetup _repo = new cls_PU_ImplementationInstitutionSetup();
    private readonly cls_PU_ClusterSetup _clusterRepo = new cls_PU_ClusterSetup();

    // Plan (pseudocode):
    // - Keep existing GridView CRUD handlers.
    // - Footer add row uses: TextBox for InstitutionName, DropDownList for InstitutionType, DropDownList for Cluster.
    // - Bind InstitutionType dropdown with predefined options in RowDataBound (Footer).
    // - Bind Cluster dropdown in RowDataBound (Footer) as before.
    // - In RowCommand AddNew, read values from txtInstitutionNameAdd, ddlInstitutionTypeAdd, ddlClusterAdd.
    // - Validate required fields and save via _repo.Create(model), then rebind and show messages.

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        gvInstitutions.RowDataBound += gvInstitutions_RowDataBound;
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
        lblMsg.Text = string.Empty;

        gvInstitutions.DataSource = _repo.GetAll();
        gvInstitutions.DataBind();
    }

    protected void gvInstitutions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInstitutions.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvInstitutions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInstitutions.EditIndex = -1;
        BindGrid();
    }

    protected void gvInstitutions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(gvInstitutions.DataKeys[e.RowIndex].Value);

            var row = gvInstitutions.Rows[e.RowIndex];
            var txtNameEdit = (TextBox)row.FindControl("txtInstitutionNameEdit");
            var txtTypeEdit = (TextBox)row.FindControl("txtInstitutionTypeEdit");
            var ddlClusterEdit = (DropDownList)row.FindControl("ddlClusterEdit");

            if (txtNameEdit == null || ddlClusterEdit == null)
            {
                lblErr.Text = "Cannot find edit controls.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNameEdit.Text))
            {
                lblErr.Text = "Institution Name is required.";
                return;
            }

            if (string.IsNullOrEmpty(ddlClusterEdit.SelectedValue))
            {
                lblErr.Text = "Please select a cluster.";
                return;
            }

            var model = new ImplementationInstitution
            {
                InstitutionID = id,
                InstitutionName = txtNameEdit.Text.Trim(),
                InstitutionType = string.IsNullOrWhiteSpace(txtTypeEdit != null ? txtTypeEdit.Text : null)
                    ? null
                    : txtTypeEdit.Text.Trim(),
                ClusterID = Convert.ToInt32(ddlClusterEdit.SelectedValue)
            };

            bool ok = _repo.Update(model);
            if (!ok)
            {
                lblErr.Text = "Update failed (record may not exist).";
            }
            else
            {
                lblMsg.Text = "Saved.";
            }

            gvInstitutions.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error updating: " + ex.Message;
        }
    }

    protected void gvInstitutions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(gvInstitutions.DataKeys[e.RowIndex].Value);
            bool ok = _repo.Delete(id);
            if (!ok)
            {
                lblErr.Text = "Delete failed (record may not exist).";
            }
            else
            {
                lblMsg.Text = string.Format("Deleted institution ID {0}.", id);
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error deleting: " + ex.Message;
        }
    }

    protected void gvInstitutions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            var ddlClusterAdd = e.Row.FindControl("ddlClusterAdd") as DropDownList;
            if (ddlClusterAdd != null)
            {
                ddlClusterAdd.DataSource = _clusterRepo.GetAll();
                ddlClusterAdd.DataTextField = "ClusterName";
                ddlClusterAdd.DataValueField = "ClusterID";
                ddlClusterAdd.DataBind();
                ddlClusterAdd.Items.Insert(0, new ListItem("-- Select Cluster --", ""));
            }

            var ddlTypeAdd = e.Row.FindControl("ddlInstitutionTypeAdd") as DropDownList;
            if (ddlTypeAdd != null)
            {
                PopulateInstitutionType(ddlTypeAdd);
            }
            return;
        }

        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        // Attach delete confirm
        foreach (Control ctl in e.Row.Cells)
        {
            var deleteButton = ctl as LinkButton;
            if (deleteButton == null && ctl.HasControls())
            {
                deleteButton = FindDeleteButtonRecursive(ctl);
            }

            if (deleteButton != null && string.Equals(deleteButton.CommandName, "Delete", StringComparison.OrdinalIgnoreCase))
            {
                deleteButton.OnClientClick = "return confirm('Are you sure you want to delete this institution?');";
                break;
            }
        }

        // If row is in edit mode, populate cluster dropdown and attach update confirm
        if (e.Row.RowIndex == gvInstitutions.EditIndex)
        {
            // Populate Cluster dropdown
            var ddlClusterEdit = e.Row.FindControl("ddlClusterEdit") as DropDownList;
            if (ddlClusterEdit != null)
            {
                ddlClusterEdit.DataSource = _clusterRepo.GetAll();
                ddlClusterEdit.DataTextField = "ClusterName";
                ddlClusterEdit.DataValueField = "ClusterID";
                ddlClusterEdit.DataBind();

                var dataItem = e.Row.DataItem;
                if (dataItem != null)
                {
                    var currentClusterIdObj = DataBinder.Eval(dataItem, "ClusterID");
                    int currentClusterId;
                    if (currentClusterIdObj != null && int.TryParse(currentClusterIdObj.ToString(), out currentClusterId))
                    {
                        var it = ddlClusterEdit.Items.FindByValue(currentClusterId.ToString());
                        if (it != null)
                        {
                            ddlClusterEdit.ClearSelection();
                            it.Selected = true;
                        }
                        else
                        {
                            ddlClusterEdit.Items.Insert(0, new ListItem("-- Select Cluster --", ""));
                            ddlClusterEdit.SelectedIndex = 0;
                        }
                    }
                }
            }

            // Attach update confirm
            foreach (Control ctl in e.Row.Cells)
            {
                var cell = ctl as DataControlFieldCell;
                if (cell == null) continue;

                foreach (Control innerCtl in cell.Controls)
                {
                    var updateButton = innerCtl as LinkButton;
                    if (updateButton != null && updateButton.CommandName.Equals("Update", StringComparison.OrdinalIgnoreCase))
                    {
                        updateButton.OnClientClick = "return confirm('Are you sure you want to update this institution?');";
                        return;
                    }
                }
            }
        }
    }

    protected void gvInstitutions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.Equals(e.CommandName, "AddNew", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        try
        {
            var footer = gvInstitutions.FooterRow;
            if (footer == null)
            {
                lblErr.Text = "Footer row not available.";
                return;
            }

            var txtName = footer.FindControl("txtInstitutionNameAdd") as TextBox;
            var ddlType = footer.FindControl("ddlInstitutionTypeAdd") as DropDownList;
            var ddlCluster = footer.FindControl("ddlClusterAdd") as DropDownList;

            if (txtName == null || ddlType == null || ddlCluster == null)
            {
                lblErr.Text = "Cannot find add controls.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblErr.Text = "Institution Name is required.";
                return;
            }

            if (string.IsNullOrEmpty(ddlType.SelectedValue))
            {
                lblErr.Text = "Please select an institution type.";
                return;
            }

            if (string.IsNullOrEmpty(ddlCluster.SelectedValue))
            {
                lblErr.Text = "Please select a cluster.";
                return;
            }

            var model = new ImplementationInstitution
            {
                InstitutionName = txtName.Text.Trim(),
                InstitutionType = ddlType.SelectedValue,
                ClusterID = Convert.ToInt32(ddlCluster.SelectedValue)
            };

            int newId = _repo.Create(model);
            if (newId <= 0)
            {
                lblErr.Text = "Create failed.";
            }
            else
            {
                lblMsg.Text = "Added.";
            }

            gvInstitutions.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Error adding: " + ex.Message;
        }
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        BindGrid();
        lblMsg.Text = "Enter details in the footer row and click Add.";
    }

    private static LinkButton FindDeleteButtonRecursive(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            var lb = child as LinkButton;
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

    private static void PopulateInstitutionType(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("-- Select Type --", ""));
        ddl.Items.Add(new ListItem("Government", "Government"));
        ddl.Items.Add(new ListItem("NGO", "NGO"));
        ddl.Items.Add(new ListItem("Agency", "Agency"));
        ddl.Items.Add(new ListItem("Municipality", "Municipality"));
        ddl.Items.Add(new ListItem("SOE", "SOE"));
        ddl.Items.Add(new ListItem("Other", "Other"));
    }
}