using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class ViewCalendar : System.Web.UI.Page
{
    clsCalendar oCalendar = new clsCalendar();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string DateFrom = (string)Session["DateFrom"];
            string DateTo = (string)Session["DateTo"];
            string urlDate = Request.QueryString["date"];

            txtDateFrom.Text = DateFrom;
            txtDateTo.Text = DateTo;

            if ((txtDateFrom.Text != "") && (txtDateTo.Text != ""))
            {
                populateGrid();
            }
            else if (urlDate != "")
            {
                txtDateFrom.Text = urlDate;
                txtDateTo.Text = urlDate;
                populateGrid();
            }
            else if (txtDateFrom.Text == "")
            {
                lblError.Text = "Please select Date From ";
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;
            }
            else if (txtDateTo.Text == "")
            {
                lblError.Text = "Please select Date To ";
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;
            }

        }
        else
        {
            if ((txtDateFrom.Text != "") && (txtDateTo.Text != ""))
            {
                populateGrid();
            }
            else if (txtDateFrom.Text == "")
            {
                lblError.Text = "Please select Date From ";
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;
            }
            else if (txtDateTo.Text == "")
            {
                lblError.Text = "Please select Date To ";
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;
            }
        }
    }

    public void populateGrid()
    {
        try
        {
            gvCalendar.DataSource = oCalendar.PopulateCalendarSelectedDate(Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text));
            gvCalendar.AutoGenerateColumns = false;
            gvCalendar.DataBind();

            //Session.Remove("DateFrom");
            //Session.Remove("DateTo");

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;//"Sorry, there is no event on this range!";
            lblError.Visible = true;
            lblError.ForeColor = System.Drawing.Color.Green;
        }
    }
    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        populateGrid();
    }

    protected void gvCalendar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCalendar.PageIndex = e.NewPageIndex;
        populateGrid();
    }
}