using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MnE2.DAL;

public partial class ii_ApprovalInbox : System.Web.UI.Page
{
    private cc_ReportsDAL _dal = new cc_ReportsDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFY.Items.Add("2025"); 
            ddlFY.Items.Add("2026");
            ddlQ.Items.Add("1"); 
            ddlQ.Items.Add("2"); 
            ddlQ.Items.Add("3"); 
            ddlQ.Items.Add("4");
            Bind(Get_dal());
        }
    }

    private cc_ReportsDAL Get_dal()
    {
        return _dal;
    }

    void Bind(cc_ReportsDAL _dal)
    {
        int fy = int.Parse(ddlFY.SelectedValue);
        int q = int.Parse(ddlQ.SelectedValue);
        gv.DataSource = _dal.ListAwaitingApproval(fy, q);
        gv.DataBind();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Bind(Get_dal());
    }

    protected void gv_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Response.Redirect("ReportView.aspx?id=" + e.CommandArgument);
        }
    }
}
