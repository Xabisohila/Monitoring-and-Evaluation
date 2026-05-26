using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class B5 : System.Web.UI.Page
{
    clsManageAllProjects oViewProject = new clsManageAllProjects();
    protected void Page_Load(object sender, EventArgs e)
    {
        populateGrid();
        BindProjectCost();
        Bindchart();
        string Fullname = (string)Session["Fullname"];
        
    }

    public void populateGrid()
    {
        try
        {
            gvB5Project.DataSource = oViewProject.Display_B5Project_Details();
            gvB5Project.AutoGenerateColumns = false;
            gvB5Project.DataBind();

            //GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);

            foreach (GridViewRow row in gvB5Project.Rows)
            {
                int i = row.RowIndex;

                if (row.RowIndex == i)
                {
                    //Label LabelEndDate = gvB5Project.Rows[i].FindControl("lblEndDate") as Label;
                    //Label ProjectId = gvB5Project.Rows[i].FindControl("lblProjectId") as Label;
                    //Image igreen = gvB5Project.Rows[i].FindControl("sgreen") as Image;
                    //Image ired = gvB5Project.Rows[i].FindControl("sred") as Image;
                    //Image iyellow = gvB5Project.Rows[i].FindControl("syellow") as Image;
                    //DateTime EndDate = Convert.ToDateTime(LabelEndDate.Text);
                    //int processedProjectId = Convert.ToInt16(ProjectId.Text);
                    //clsActivity oActivity = new clsActivity();
                    ////LabelEndDate .BackColor = Color.Yellow;
                    //int ProjectExist = oActivity.ActivityExist(Convert.ToInt16(processedProjectId));

                    //var now = DateTime.Now;
                    //var expirationDate = EndDate;
                    //var FourteenDayBefore = expirationDate.AddDays(-14);

                    //if (ProjectExist == 0)
                    //{
                    //    igreen.Visible = false;
                    //    ired.Visible = true;
                    //    iyellow.Visible = false;
                    //    iyellow.ToolTip = "No activity";
                    //}

                    //else if (now > expirationDate && (oActivity.GetActivityStatus(processedProjectId)) != 100.00)
                    //{
                    //    ired.Visible = true;
                    //    igreen.Visible = false;
                    //    iyellow.Visible = false;
                    //    ired.ToolTip = "Finish Activities";
                    //}
                    //else if ((now > FourteenDayBefore && now < expirationDate) && (now > expirationDate && (oActivity.GetActivityStatus(processedProjectId)) != 100.00))
                    //{
                    //    igreen.Visible = false;
                    //    ired.Visible = false;
                    //    iyellow.Visible = true;
                    //    iyellow.ToolTip = "Check Due Date";
                    //}
                    //else if ((now > FourteenDayBefore && now < expirationDate) && (now > expirationDate && (oActivity.GetActivityStatus(processedProjectId)) == 100.00))
                    //{
                    //    igreen.Visible = true;
                    //    ired.Visible = false;
                    //    iyellow.Visible = false;
                    //    iyellow.ToolTip = "Project activities are all finished";
                    //}


                    //else
                    //{
                    //    igreen.Visible = true;
                    //    iyellow.Visible = false;
                    //    ired.Visible = false;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            //lblError.Text = "Some projects listed have no activities!";//ex.Message;
            //lblError.Visible = true;
            //lblError.ForeColor = System.Drawing.Color.Green;
        }
    }

    protected void lnkProjectName_OnClick(object sender, EventArgs e)
    {
        try
        {

            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvB5Project.Rows)
            {
                if (rowItem.RowIndex == i)
                {
                    Label LabelProjectId = gvB5Project.Rows[i].FindControl("lblProjectId") as Label;
                    //string ProjectId = Convert.ToString(LabelProjectId.Text);
                    Session["ProjectId"] = LabelProjectId.Text;
                    Response.Redirect("ViewProject.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }


    protected void gvProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvB5Project.PageIndex = e.NewPageIndex;
        populateGrid();
    }

    private void Bindchart()
    {

        DataSet ds = oViewProject.Display_Top3ProjectStage();

        DataTable ChartData = ds.Tables[0];

        //storing total rows count to loop on each Record   
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];

        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis   
            XPointMember[count] = ChartData.Rows[count]["name"].ToString();
            //storing values for Y Axis   
            YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["status"]);

        }
        //binding chart control   
        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line   
        Chart1.Series[0].BorderWidth = 10;
        //setting Chart type    
        Chart1.Series[0].ChartType = SeriesChartType.Doughnut;


        foreach (Series charts in Chart1.Series)
        {
            foreach (DataPoint point in charts.Points)
            {
                switch (point.AxisLabel)
                {
                    case "Q1": point.Color = Color.YellowGreen; break;
                    case "Q2": point.Color = Color.Yellow; break;
                    case "Q3": point.Color = Color.SpringGreen; break;
                }
                point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            }
        }


        //Enabled 3D   
        //  Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;   


    }

    private void BindProjectCost()
    {

        DataSet ds = oViewProject.Display_Top3ProjectCost();

        DataTable ChartData = ds.Tables[0];

        //storing total rows count to loop on each Record   
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];

        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis   
            XPointMember[count] = ChartData.Rows[count]["name"].ToString();
            //storing values for Y Axis   
            YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["cost"]);

        }
        //binding chart control   
        Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line   
        Chart2.Series[0].BorderWidth = 10;
        //setting Chart type    
        Chart2.Series[0].ChartType = SeriesChartType.Bar;


        foreach (Series charts in Chart1.Series)
        {
            foreach (DataPoint point in charts.Points)
            {
                switch (point.AxisLabel)
                {
                    case "Q1": point.Color = Color.YellowGreen; break;
                    case "Q2": point.Color = Color.Yellow; break;
                    case "Q3": point.Color = Color.SpringGreen; break;
                }
                point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            }
        }


        //Enabled 3D   
        //  Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;   


    }  
}