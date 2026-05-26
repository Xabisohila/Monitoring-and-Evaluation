using MnE2.DAL;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_SystemPeriodsAdmin : System.Web.UI.Page
{
    private readonly c_SystemPeriodDAL dal = new c_SystemPeriodDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Restrict as needed (Planning/Admin)
        //RequireRole(/* Planning/Admin role IDs */);
        if (!IsPostBack)
        {
            BindYears();
            BindGrid();
        }
    }

    private void BindYears()
    {
        // Populate a reasonable FY range (adjust to your needs)
        ddlFY.Items.Clear();
        int currentFY = DateTime.Now.Month >= 4 ? DateTime.Now.Year : DateTime.Now.Year - 1; // SA FY often Apr–Mar (tweak if needed)
        for (int y = currentFY - 1; y <= currentFY + 3; y++)
        {
            ddlFY.Items.Add(new System.Web.UI.WebControls.ListItem(y.ToString(), y.ToString()));
        }

        ddlFY.SelectedValue = currentFY.ToString();
        ddlQ.SelectedValue = "1";
    }

    private void BindGrid()
    {
        gv.DataSource = dal.GetAll();
        gv.DataBind();
    }

    protected void btnSuggest_Click(object sender, EventArgs e)
    {
        int fy = SafeInt(ddlFY.SelectedValue);
        int q = SafeInt(ddlQ.SelectedValue);
        Tuple<DateTime, DateTime> suggestion = SuggestOpenClose(fy, q);
        // For datetime-local inputs, use 'T' between date and time
        txtOpen.Text = suggestion.Item1.ToString("yyyy-MM-ddTHH:mm");
        txtClose.Text = suggestion.Item2.ToString("yyyy-MM-ddTHH:mm");
        lblMsg.Text = string.Empty;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        try
        {
            // Validate
            int fy;
            if (!int.TryParse(ddlFY.SelectedValue, out fy) || fy < 2000 || fy > 2100)
            {
                throw new ApplicationException("Please select a valid Financial Year (2000–2100).");
            }

            int q;
            if (!int.TryParse(ddlQ.SelectedValue, out q) || q < 1 || q > 4)
            {
                throw new ApplicationException("Quarter must be 1–4.");
            }

            DateTime open;
            if (!DateTime.TryParse(txtOpen.Text, out open))
            {
                throw new ApplicationException("Open Date must be in 'yyyy-MM-dd HH:mm' format.");
            }

            DateTime close;
            if (!DateTime.TryParse(txtClose.Text, out close))
            {
                throw new ApplicationException("Close Date must be in 'yyyy-MM-dd HH:mm' format.");
            }

            if (open >= close)
            {
                throw new ApplicationException("Close Date must be after Open Date.");
            }

            var model = new c_SystemPeriod
            {
                PeriodID = string.IsNullOrEmpty(hfID.Value) ? 0 : Convert.ToInt32(hfID.Value),
                FinancialYear = fy,
                Quarter = q,
                OpenDate = open,
                CloseDate = close,
                IsOpen = chkOpen.Checked
            };

            dal.Upsert(model);
            ClearForm();
            BindGrid();

            lblMsg.CssClass = "alert alert-success";
            lblMsg.Text = "Saved.";
        }
        catch (Exception ex)
        {
            lblMsg.CssClass = "alert alert-danger";
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
        lblMsg.Text = string.Empty;
    }

    private void ClearForm()
    {
        hfID.Value = string.Empty;
        // Reset to selected FY/Q, clear dates/open
        txtOpen.Text = string.Empty;
        txtClose.Text = string.Empty;
        chkOpen.Checked = false;
    }

    protected void gv_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        int periodID = Convert.ToInt32(gv.DataKeys[rowIndex].Value);

        if (e.CommandName == "EditRow")
        {
            var p = dal.GetByID(periodID);
            if (p != null)
            {
                hfID.Value = p.PeriodID.ToString();
                ddlFY.SelectedValue = p.FinancialYear.ToString();
                ddlQ.SelectedValue = p.Quarter.ToString();
                txtOpen.Text = p.OpenDate.ToString("yyyy-MM-dd HH:mm");
                txtClose.Text = p.CloseDate.ToString("yyyy-MM-dd HH:mm");
                chkOpen.Checked = p.IsOpen;
                lblMsg.Text = string.Empty;
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            dal.Delete(periodID);
            BindGrid();
            lblMsg.CssClass = "alert alert-success";
            lblMsg.Text = "Deleted.";
        }
        else if (e.CommandName == "CloseNow")
        {
            var p = dal.GetByID(periodID);
            if (p != null)
            {
                p.IsOpen = false;
                // If CloseDate is in future, bring it to now
                if (p.CloseDate > DateTime.Now)
                {
                    p.CloseDate = DateTime.Now;
                }
                dal.Upsert(p);
                BindGrid();
                lblMsg.CssClass = "alert alert-success";
                lblMsg.Text = "Period closed.";
            }
        }
    }

    /// <summary>
    /// Suggest open/close dates for FY/Q.
    /// Assumes FY spans Apr–Mar; quarter end months: Q1=Jun, Q2=Sep, Q3=Dec, Q4=Mar(next).
    /// Window: 1st to 20th of the month after the quarter ends (closing 23:59).
    /// </summary>
    private Tuple<DateTime, DateTime> SuggestOpenClose(int fy, int q)
    {
        // Compute quarter end month/year under an Apr–Mar FY
        int endYear = fy;
        int endMonth = 3; // default
        switch (q)
        {
            case 1:
                endMonth = 6;
                endYear = fy;
                break;   // Apr-Jun (FY)
            case 2:
                endMonth = 9;
                endYear = fy;
                break;   // Jul-Sep
            case 3:
                endMonth = 12;
                endYear = fy;
                break;  // Oct-Dec
            case 4:
                endMonth = 3;
                endYear = fy + 1;
                break; // Jan-Mar (next calendar year)
        }

        // Month after quarter end:
        int postMonth = endMonth == 12 ? 1 : endMonth + 1;
        int postYear = endMonth == 12 ? endYear + 1 : endYear;

        DateTime open = new DateTime(postYear, postMonth, 1, 0, 0, 0);
        DateTime close = new DateTime(postYear, postMonth, 20, 23, 59, 0);
        return new Tuple<DateTime, DateTime>(open, close);
    }

    private static int SafeInt(string s)
    {
        int n;
        return int.TryParse(s, out n) ? n : 0;
    }
}