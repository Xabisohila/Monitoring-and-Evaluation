using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pu_WGSetup : System.Web.UI.Page
{
    private readonly cls_PU_WGSetup _repo = new cls_PU_WGSetup();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadClusters();
            BindGrid();
        }
    }

    private void LoadClusters()
    {
        var dt = _repo.GetAllClusters();
        ddlCluster.DataSource = dt;
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataValueField = "ClusterID";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new ListItem("-- Select Cluster --", "0"));

        // For edit dropdown population later, store all clusters in ViewState
        ViewState["AllClusters"] = dt;
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int clusterId = int.Parse(ddlCluster.SelectedValue);
        LoadLeadInstitutions(clusterId);
    }

    private void LoadLeadInstitutions(int clusterId)
    {
        var dt = _repo.GetInstitutionsByCluster(clusterId);
        ddlLeadInstitution.DataSource = dt;
        ddlLeadInstitution.DataTextField = "InstitutionName";
        ddlLeadInstitution.DataValueField = "InstitutionID";
        ddlLeadInstitution.DataBind();
        ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Institution --", "0"));
    }

    private void BindGrid()
    {
        lblError.Text = string.Empty;
        gvWorkingGroups.DataSource = _repo.GetAll();
        gvWorkingGroups.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCluster.SelectedValue == "0")
            {
                lblError.Text = "Cluster is required.";
                return;
            }
            if (ddlLeadInstitution.SelectedValue == "0")
            {
                lblError.Text = "Lead Institution is required.";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWGName.Text))
            {
                lblError.Text = "Working Group name is required.";
                return;
            }

            var wg = new WorkingGroup
            {
                WG_Name = txtWGName.Text.Trim(),
                WG_Description = string.IsNullOrWhiteSpace(txtWGDescription.Text) ? null : txtWGDescription.Text.Trim(),
                LeadInstitutionID = int.Parse(ddlLeadInstitution.SelectedValue)
            };

            var newId = _repo.Create(wg);

            // Reset form
            txtWGName.Text = string.Empty;
            txtWGDescription.Text = string.Empty;
            ddlCluster.SelectedIndex = 0;
            ddlLeadInstitution.Items.Clear();
            ddlLeadInstitution.Items.Insert(0, new ListItem("-- Select Institution --", "0"));

            lblMessage.Text = "Working Group added successfully.";
            BindGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error creating working group: " + ex.Message;
        }
    }

    protected void gvWorkingGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvWorkingGroups.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvWorkingGroups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvWorkingGroups.EditIndex = -1;
        BindGrid();
    }

    protected void gvWorkingGroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        // Confirm delete/update like Cluster page
        foreach (Control ctl in e.Row.Cells)
        {
            var lb = ctl as LinkButton;
            if (lb == null && ctl.HasControls())
            {
                lb = FindButtonRecursive<LinkButton>(ctl, "Delete");
            }
            if (lb != null && string.Equals(lb.CommandName, "Delete", StringComparison.OrdinalIgnoreCase))
            {
                lb.OnClientClick = "return confirm('Are you sure you want to delete this working group?');";
                break;
            }
        }

        if (e.Row.RowIndex == gvWorkingGroups.EditIndex)
        {
            foreach (Control ctl in e.Row.Cells)
            {
                var cell = ctl as DataControlFieldCell;
                if (cell == null) continue;

                foreach (Control inner in cell.Controls)
                {
                    var updateButton = inner as LinkButton;
                    if (updateButton != null && updateButton.CommandName.Equals("Update", StringComparison.OrdinalIgnoreCase))
                    {
                        updateButton.OnClientClick = "return confirm('Are you sure you want to update this working group?');";
                        // no return; continue to populate edit dropdowns
                    }
                }
            }

            // Populate Cluster and Institution edit dropdowns
            var row = e.Row;
            var ddlClusterEdit = (DropDownList)row.FindControl("ddlClusterEdit");
            var ddlInstitutionEdit = (DropDownList)row.FindControl("ddlInstitutionEdit");

            var dataItem = (WorkingGroup)row.DataItem;
            if (ddlClusterEdit != null)
            {
                var clusters = (DataTable)ViewState["AllClusters"];
                ddlClusterEdit.DataSource = clusters;
                ddlClusterEdit.DataTextField = "ClusterName";
                ddlClusterEdit.DataValueField = "ClusterID";
                ddlClusterEdit.DataBind();
                ddlClusterEdit.SelectedValue = dataItem.ClusterID.ToString();

                // Institutions depend on selected cluster
                var inst = _repo.GetInstitutionsByCluster(dataItem.ClusterID);
                ddlInstitutionEdit.DataSource = inst;
                ddlInstitutionEdit.DataTextField = "InstitutionName";
                ddlInstitutionEdit.DataValueField = "InstitutionID";
                ddlInstitutionEdit.DataBind();
                ddlInstitutionEdit.SelectedValue = dataItem.LeadInstitutionID.ToString();
            }
        }
    }

    protected void gvWorkingGroups_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(gvWorkingGroups.DataKeys[e.RowIndex].Value);
            var row = gvWorkingGroups.Rows[e.RowIndex];

            var txtNameEdit = (TextBox)row.FindControl("txtWGNameEdit");
            var txtDescEdit = (TextBox)row.FindControl("txtWGDescEdit");
            var ddlClusterEdit = (DropDownList)row.FindControl("ddlClusterEdit");
            var ddlInstitutionEdit = (DropDownList)row.FindControl("ddlInstitutionEdit");

            if (txtNameEdit == null || ddlClusterEdit == null || ddlInstitutionEdit == null)
            {
                lblError.Text = "Cannot find edit controls.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNameEdit.Text))
            {
                lblError.Text = "Working Group name is required.";
                return;
            }

            int clusterId = int.Parse(ddlClusterEdit.SelectedValue);
            int leadInstitutionId = int.Parse(ddlInstitutionEdit.SelectedValue);

            var wg = new WorkingGroup
            {
                WorkingGroupID = id,
                WG_Name = txtNameEdit.Text.Trim(),
                WG_Description = txtDescEdit != null && !string.IsNullOrWhiteSpace(txtDescEdit.Text) ? txtDescEdit.Text.Trim() : null,
                LeadInstitutionID = leadInstitutionId
            };

            bool ok = _repo.Update(wg);
            if (!ok)
            {
                lblError.Text = "Update failed (record may not exist).";
            }

            gvWorkingGroups.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error updating: " + ex.Message;
        }
    }

    protected void gvWorkingGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
            {
                int id = Convert.ToInt32(gvWorkingGroups.DataKeys[e.RowIndex].Value);
                bool ok = _repo.Delete(id);
                if (!ok)
                {
                    lblError.Text = "Delete failed (record may not exist).";
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error deleting: " + ex.Message;
            }
    }

    private static T FindButtonRecursive<T>(Control parent, string commandName) where T : class, IButtonControl
    {
        foreach (Control child in parent.Controls)
        {
            var btn = child as T;
            if (btn != null && string.Equals(btn.CommandName, commandName, StringComparison.OrdinalIgnoreCase))
            {
                return btn;
            }

            if (child.HasControls())
            {
                var found = FindButtonRecursive<T>(child, commandName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return default(T);
    }
}
