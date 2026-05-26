using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_ProgrammeInstitutions : System.Web.UI.Page
{
    private readonly c_IntegrationProgrammesDAL progDAL = new c_IntegrationProgrammesDAL();
    private readonly c_InstitutionDAL instDAL = new c_InstitutionDAL();
    private readonly c_ProgrammeInstitutionDAL progInstDAL = new c_ProgrammeInstitutionDAL();

    private int ProgrammeID
    {
        get
        {
            int id;
            return int.TryParse(Request.QueryString["ProgrammeID"], out id) ? id : 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequireRole(/* Planning/Admin role IDs */);
        if (ProgrammeID == 0)
        {
            //Response.Redirect("i_ProgrammeAdmin.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadProgrammeDetails();
            BindInstitutions();
            BindGrid();
        }
    }

    private void LoadProgrammeDetails()
    {
        var prog = progDAL.GetByID(ProgrammeID);
        if (prog != null)
        {
            lblProgrammeName.Text = prog.ProgrammeName;
        }
        else
        {
            //Response.Redirect("i_ProgrammeAdmin.aspx");
        }
    }

    private void BindInstitutions()
    {
        var allInstitutions = instDAL.GetAll();
        var linkedInstitutions = progInstDAL.GetByProgrammeID(ProgrammeID);
        var linkedIds = linkedInstitutions.Select(x => x.InstitutionID).ToList();

        // Only show institutions that are not already linked to this programme
        var available = allInstitutions.Where(x => !linkedIds.Contains(x.InstitutionID)).ToList();

        ddlInstitution.DataSource = available;
        ddlInstitution.DataTextField = "InstitutionName";
        ddlInstitution.DataValueField = "InstitutionID";
        ddlInstitution.DataBind();
        ddlInstitution.Items.Insert(0, new ListItem("-- Select Institution --", ""));
    }

    private void BindGrid()
    {
        var data = progInstDAL.GetByProgrammeID(ProgrammeID);
        gvInstitutions.DataSource = data;
        gvInstitutions.DataBind();
    }

    protected void gvInstitutions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var hfInstID = (HiddenField)e.Row.FindControl("hfInstitutionID");
            var lblInstName = (Label)e.Row.FindControl("lblInstitutionName");
            int instId;
            if (hfInstID != null && lblInstName != null && int.TryParse(hfInstID.Value, out instId))
            {
                var inst = instDAL.GetByID(instId);
                lblInstName.Text = inst != null ? inst.InstitutionName : "";
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlInstitution.SelectedValue))
        {
            return;
        }

        var model = new c_ProgrammeInstitution
        {
            ProgrammeID = ProgrammeID,
            InstitutionID = Convert.ToInt32(ddlInstitution.SelectedValue)
        };

        progInstDAL.Insert(model);
        BindInstitutions();
        BindGrid();
    }

    protected void gvInstitutions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            progInstDAL.Delete(id);
            BindInstitutions();
            BindGrid();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("i_ProgrammeAdmin.aspx");
    }

}