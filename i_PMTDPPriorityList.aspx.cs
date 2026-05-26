using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_PMTDPPriorityList : Page
{
    private cls_PriorityDAL dal = new cls_PriorityDAL();

    // Cached once per request — avoids N separate DB calls in RowDataBound
    private DataTable _clusters;
    private DataTable Clusters
    {
        get { return _clusters ?? (_clusters = dal.GetAllClusters()); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        if (!IsPostBack)
            BindGrid();
    }

    private void BindGrid()
    {
        DataTable dt = dal.GetAllPrioritiesWithCluster();

        int unassigned = 0;
        foreach (DataRow r in dt.Rows)
            if (r["ClusterID"] == DBNull.Value) unassigned++;

        lblTotal.Text      = dt.Rows.Count.ToString();
        lblUnassigned.Text = unassigned.ToString();
        lblAssigned.Text   = (dt.Rows.Count - unassigned).ToString();

        gvPriorities.DataSource = dt;
        gvPriorities.DataBind();
    }

    protected void gvPriorities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        DataRowView row = (DataRowView)e.Row.DataItem;

        // ── Show/hide cluster status panels ──────────────────────────────
        bool hasCluster = row["ClusterID"] != DBNull.Value;

        var pnlUnassigned = (Panel)e.Row.FindControl("pnlUnassigned");
        var pnlAssigned   = (Panel)e.Row.FindControl("pnlAssigned");
        var lblClusterName = (Label)e.Row.FindControl("lblClusterName");
        var lnkCreatePOA  = (HyperLink)e.Row.FindControl("lnkCreatePOA");

        pnlUnassigned.Visible = !hasCluster;
        pnlAssigned.Visible   =  hasCluster;

        if (hasCluster)
            lblClusterName.Text = row["ClusterName"].ToString();

        lnkCreatePOA.Visible = hasCluster;

        // ── Populate cluster dropdown ─────────────────────────────────────
        var ddl = (DropDownList)e.Row.FindControl("ddlCluster");
        ddl.DataSource     = Clusters;
        ddl.DataTextField  = "ClusterName";
        ddl.DataValueField = "ClusterID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-- Select cluster --", "0"));

        if (hasCluster)
        {
            ListItem li = ddl.Items.FindByValue(row["ClusterID"].ToString());
            if (li != null) li.Selected = true;
        }
    }

    protected void gvPriorities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "AssignCluster") return;

        int priorityId = Convert.ToInt32(e.CommandArgument);

        GridViewRow row = ((Control)e.CommandSource).NamingContainer as GridViewRow;
        DropDownList ddl = row.FindControl("ddlCluster") as DropDownList;

        int clusterId = Convert.ToInt32(ddl.SelectedValue);
        if (clusterId == 0)
        {
            lblMsg.Text = "Please select a cluster before saving.";
            BindGrid();
            return;
        }

        bool ok = dal.AssignCluster(priorityId, clusterId);
        lblMsg.Text = ok ? "Cluster assigned successfully." : "Error assigning cluster — please try again.";

        BindGrid();
    }
}
