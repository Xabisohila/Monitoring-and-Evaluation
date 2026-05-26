using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class ProvincialCalendar : System.Web.UI.Page
{
    DataSet dsSelDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Calendar1.VisibleDate = DateTime.Today;
            FillEventDataset();
            dsEvents = oCalendar.PopulateCalendar();

        }
        else
        {
            Calendar1.VisibleDate = DateTime.Today;
            FillEventDataset();
            dsEvents = oCalendar.PopulateCalendar();
        }
    }

    protected DataSet dsEvents;
    clsCalendar oCalendar = new clsCalendar();


    protected void FillEventDataset()
    {
        DateTime firstDate = new DateTime(Calendar1.VisibleDate.Year,
            Calendar1.VisibleDate.Month, 1);
        DateTime lastDate = GetFirstDayOfNextMonth();
        dsEvents = oCalendar.PopulateCalendar();
    }

    protected DateTime GetFirstDayOfNextMonth()
    {
        int monthNumber, yearNumber;
        if (Calendar1.VisibleDate.Month == 12)
        {
            monthNumber = 1;
            yearNumber = Calendar1.VisibleDate.Year + 1;
        }
        else
        {
            monthNumber = Calendar1.VisibleDate.Month + 1;
            yearNumber = Calendar1.VisibleDate.Year;
        }
        DateTime lastDate = new DateTime(yearNumber, monthNumber, 1);
        return lastDate;
    }


    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            //DataSet ds = new DataSet();

            string link = ("<a href= 'ViewCalendar.aspx?&date=");

            e.Cell.Text = e.Day.Date.Day.ToString();
            LiteralControl l = new LiteralControl();

            l.Text = e.Day.Date.Day.ToString() + " ";

            e.Cell.Controls.Add(l);
            // If the month is CurrentMonth   
            if (!e.Day.IsOtherMonth)
            {
                foreach (DataRow dr in dsEvents.Tables[0].Rows)
                {
                    if ((dr["Event_Date"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTimeOffset dtEvent = (DateTime)dr["Event_Date"];

                        string Event = (String)dr["Event"];
                        if (dtEvent.Equals(e.Day.Date))
                        {
                            e.Cell.BackColor = System.Drawing.Color.LightYellow;
                            LinkButton lb = new LinkButton();

                            lb.Text = link + (DateTime)dr["Event_Date"] + "'>" + "<br> <br>" + Event;

                            e.Cell.Controls.Add(lb);


                        }

                    }
                }

            }
            //If the month is not CurrentMonth then hide the Dates   
            else
            {
                e.Cell.Text = "";
            }
        }
        catch
        {
        }
    }
    protected void Calendar1_VisibleMonthChanged(object sender,
        MonthChangedEventArgs e)
    {
        FillEventDataset();
    }



    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        if ((txtDateFrom.Text != "") && (txtDateTo.Text != ""))
        {
            Session["DateFrom"] = txtDateFrom.Text;
            Session["DateTo"] = txtDateTo.Text;
            Response.Redirect("ViewCalendar.aspx");
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

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            System.DateTime activeDate = Calendar1.SelectedDate;
            //clsCalendar oCalendar = new clsCalendar();
            dsSelDate = oCalendar.PopulateCalendarActiveDate(activeDate);

            if (dsSelDate.Tables[0].Rows.Count == 0)
            {

            }
            else
            {

            }
        }
        catch
        {
        }
    }
}