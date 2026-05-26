using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class i_AnnualTargets : System.Web.UI.Page
{
    private readonly c_IndicatorsDAL indicators = new c_IndicatorsDAL();
    private readonly c_AnnualTargetsDAL annual = new c_AnnualTargetsDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //RequireRole(/* Admin, Planning, etc. role IDs */);
            if (!IsPostBack)
            {
                BindIndicators();
                BindFinancialYears();
                BindGrid();
            }
        }
        catch 
        { 
                //Response.Redirect("AccessDenied.aspx");
        }
        
    }

    private void BindIndicators()
    {
        try
        {
            ddlIndicator.DataSource = indicators.GetAll();
            ddlIndicator.DataBind();
            ddlIndicator.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
        }
        catch
        {
            lblMsg.Text = "Error loading indicators.";
            lblMsg.Visible = true;
            btnSave.Enabled = false;
            return;
        }
    }

    private void BindFinancialYears()
    {
        try
        {
            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 5;
            int endYear = currentYear + 6;

            ddlFY.Items.Clear();
            ddlFY.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));

            for (int year = startYear; year <= endYear; year++)
            {
                ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem(year.ToString(), year.ToString()));
            }
        }
        catch
        {
            lblMsg.Text = "Error loading financial years.";
            lblMsg.Visible = true;
            btnSave.Enabled = false;
            return;
        }
    }

    private void BindGrid()
    {
        try
        {
            var annualTargets = annual.GetAll();
            var indicatorsList = indicators.GetAll();
            
            // Create a DataTable to join indicator names
            DataTable dt = new DataTable();
            dt.Columns.Add("AnnualTargetID", typeof(int));
            dt.Columns.Add("IndicatorID", typeof(int));
            dt.Columns.Add("IndicatorName", typeof(string));
            dt.Columns.Add("FinancialYear", typeof(int));
            dt.Columns.Add("AnnualTargetValue", typeof(string));

            foreach (var target in annualTargets)
            {
                var indicator = indicatorsList.FirstOrDefault(i => i.IndicatorID == target.IndicatorID);
                dt.Rows.Add(
                    target.AnnualTargetID,
                    target.IndicatorID,
                    indicator != null ? indicator.IndicatorName : "Unknown",
                    target.FinancialYear,
                    target.AnnualTargetValue
                );
            }

            gvAnnualTargets.DataSource = dt;
            gvAnnualTargets.DataKeyNames = new string[] { "AnnualTargetID" };
            gvAnnualTargets.DataBind();
        }
        catch
        {
            //lblMsg.Text = "Error loading annual targets.";
            //gvAnnualTargets.DataSource = null;
            //gvAnnualTargets.DataBind();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid) return;

            var model = new c_AnnualTarget
            {
                AnnualTargetID = string.IsNullOrWhiteSpace(hfAnnualTargetID.Value) ? 0 : Convert.ToInt32(hfAnnualTargetID.Value),
                IndicatorID = Convert.ToInt32(ddlIndicator.SelectedValue),
                FinancialYear = Convert.ToInt32(ddlFY.SelectedValue),
                AnnualTargetValue = txtAnnualTarget.Text
            };

            annual.Upsert(model);
            ClearForm();
            BindGrid();
        }
        catch
        {
            lblMsg.Text = "Error saving annual target.";
            lblMsg.Visible = true; 
            lblMsg.ForeColor = System.Drawing.Color.Red;
            //btnSave.Enabled = false; return;
        }

    }

    protected void gvAnnualTargets_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            
            if (e.CommandName == "EditRow")
            {
                // Use DataKeys instead of reading from cell text
                var id = Convert.ToInt32(gvAnnualTargets.DataKeys[rowIndex].Value);
                var m = annual.GetByID(id);
                if (m != null)
                {
                    hfAnnualTargetID.Value = m.AnnualTargetID.ToString();
                    ddlIndicator.SelectedValue = m.IndicatorID.ToString();
                    ddlFY.SelectedValue = m.FinancialYear.ToString();
                    txtAnnualTarget.Text = m.AnnualTargetValue;
                    
                    lblMsg.Text = "Record loaded for editing.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Record not found.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                // Use DataKeys instead of reading from cell text
                var id = Convert.ToInt32(gvAnnualTargets.DataKeys[rowIndex].Value);
                annual.Delete(id);
                BindGrid();
                ClearForm();
                
                lblMsg.Text = "Record deleted successfully.";
                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error processing request: " + ex.Message;
            lblMsg.Visible = true;
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void ClearForm()
    {
        try
        {
            hfAnnualTargetID.Value = "";
            ddlIndicator.SelectedIndex = 0;
            ddlFY.SelectedIndex = 0;
            txtAnnualTarget.Text = "";
        }
        catch
        {
            //lblMsg.Text = "Error clearing form.";
        }
    }
}
