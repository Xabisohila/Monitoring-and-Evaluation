using MnE2.DAL;
using System;
using System.Web.UI.WebControls;

public partial class i_QuarterlyTargets : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL     indicators = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL  annual     = new c_AnnualTargetsDAL();
    private readonly c_QuarterlyTargetsDAL qDAL     = new c_QuarterlyTargetsDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlIndicator.DataSource     = indicators.GetAll();
            ddlIndicator.DataTextField  = "IndicatorName";
            ddlIndicator.DataValueField = "IndicatorID";
            ddlIndicator.DataBind();
            ddlIndicator.Items.Insert(0, new ListItem("-- Select Indicator --", ""));

            ddlFY.Items.Add(new ListItem("2024/2025", "2024"));
            ddlFY.Items.Add(new ListItem("2025/2026", "2025"));
            ddlFY.Items.Add(new ListItem("2026/2027", "2026"));
            ddlFY.Items.Add(new ListItem("2027/2028", "2027"));
            ddlFY.Items.Add(new ListItem("2028/2029", "2028"));
            ddlFY.Items.Add(new ListItem("2029/2030", "2029"));
            ddlFY.Items.Add(new ListItem("2030/2031", "2030"));
            ddlFY.Items.Insert(0, new ListItem("-- Financial Year --", ""));
        }
    }

    protected void ddlIndicator_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAnnualTarget();
        BindGrid();
    }

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAnnualTarget();
        BindGrid();
    }

    private void LoadAnnualTarget()
    {
        hfAnnualTargetID.Value = "";
        if (string.IsNullOrEmpty(ddlIndicator.SelectedValue) ||
            string.IsNullOrEmpty(ddlFY.SelectedValue)) return;

        var at = annual.GetByIndicatorYear(
            Convert.ToInt32(ddlIndicator.SelectedValue),
            Convert.ToInt32(ddlFY.SelectedValue));

        if (at != null) hfAnnualTargetID.Value = at.AnnualTargetID.ToString();
    }

    private void BindGrid()
    {
        if (string.IsNullOrEmpty(hfAnnualTargetID.Value))
        {
            gvQuarterly.DataSource = null;
            gvQuarterly.DataBind();
            return;
        }
        gvQuarterly.DataSource = qDAL.ListByAnnual(Convert.ToInt32(hfAnnualTargetID.Value));
        gvQuarterly.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        if (string.IsNullOrEmpty(ddlIndicator.SelectedValue) ||
            string.IsNullOrEmpty(ddlFY.SelectedValue))
        {
            ShowError("Please select an Indicator and Financial Year before saving.");
            return;
        }

        if (string.IsNullOrEmpty(txtTarget.Text.Trim()))
        {
            ShowError("Please enter a Target Value.");
            return;
        }

        if (string.IsNullOrEmpty(hfAnnualTargetID.Value))
        {
            int newAtId = annual.Upsert(new c_AnnualTarget
            {
                AnnualTargetID    = 0,
                IndicatorID       = Convert.ToInt32(ddlIndicator.SelectedValue),
                FinancialYear     = Convert.ToInt32(ddlFY.SelectedValue),
                AnnualTargetValue = null
            });

            if (newAtId <= 0)
            {
                ShowError("Could not create the annual target record. Please try again.");
                return;
            }
            hfAnnualTargetID.Value = newAtId.ToString();
        }

        int annualTargetId;
        if (!int.TryParse(hfAnnualTargetID.Value, out annualTargetId) || annualTargetId <= 0)
        {
            ShowError("Invalid annual target — please re-select the Indicator and Financial Year.");
            return;
        }

        int quarterNumber;
        if (!int.TryParse(ddlQuarter.SelectedValue, out quarterNumber))
        {
            ShowError("Please select a Quarter.");
            return;
        }

        int existingId = 0;
        if (!string.IsNullOrEmpty(hfQuarterlyTargetID.Value))
            int.TryParse(hfQuarterlyTargetID.Value, out existingId);

        qDAL.Upsert(new c_QuarterlyTarget
        {
            QuarterlyTargetID = existingId,
            AnnualTargetID    = annualTargetId,
            QuarterNumber     = quarterNumber,
            TargetValue       = txtTarget.Text.Trim(),
            SpatialReference  = txtSpatial.Text.Trim()
        });

        ShowSuccess(existingId > 0 ? "Target updated successfully." : "Target saved successfully.");
        ClearForm();
        BindGrid();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
        lblStatus.Text = "";
    }

    protected void gvQuarterly_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        int id       = Convert.ToInt32(gvQuarterly.DataKeys[rowIndex].Value);

        if (e.CommandName == "EditRow")
        {
            var m = qDAL.GetByID(id);
            if (m != null)
            {
                hfQuarterlyTargetID.Value = m.QuarterlyTargetID.ToString();
                ddlQuarter.SelectedValue  = m.QuarterNumber.ToString();
                txtTarget.Text            = m.TargetValue;
                txtSpatial.Text           = m.SpatialReference;
                lblFormTitle.Text         = "Edit Quarterly Target";
                lblStatus.Text            = "";
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            qDAL.Delete(id);
            ClearForm();
            lblStatus.Text = "";
            BindGrid();
        }
    }

    private void ShowSuccess(string msg)
    {
        lblStatus.Text     = msg;
        lblStatus.CssClass = "msg-success";
    }

    private void ShowError(string msg)
    {
        lblStatus.Text     = msg;
        lblStatus.CssClass = "msg-error";
    }

    private void ClearForm()
    {
        hfQuarterlyTargetID.Value = "";
        ddlQuarter.SelectedIndex  = 0;
        txtTarget.Text            = "";
        txtSpatial.Text           = "";
        lblFormTitle.Text         = "Add Quarterly Target";
    }
}
