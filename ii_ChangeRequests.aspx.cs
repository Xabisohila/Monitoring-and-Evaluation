using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ii_ChangeRequests : System.Web.UI.Page
{
    private readonly cc_ChangeRequestDAL _crDal = new cc_ChangeRequestDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        if (chkOnlyUnresolved.Checked)
            gvCR.DataSource = _crDal.ListUnresolved();
        else
            gvCR.DataSource = _crDal.ListAll();

        gvCR.DataBind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void chkOnlyUnresolved_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvCR_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MarkResolved")
        {
            int crid = int.Parse((string)e.CommandArgument);

            // mark resolved using Upsert (fetch + set IsResolved = true)
            var model = _crDal.GetByID(crid);
            if (model != null)
            {
                model.IsResolved = true;
                _crDal.Upsert(model);
                BindData();
            }
        }
    }
}
