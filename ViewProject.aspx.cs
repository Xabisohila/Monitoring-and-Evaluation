using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Process_ViewProject : System.Web.UI.Page
{
    clsActivity oActivity = new clsActivity();
    clsManageAllProjects oProjectAttribute = new clsManageAllProjects();
    protected void Page_Load(object sender, EventArgs e)
    {

        string SessionProjectId = (string)Session["ProjectId"];
        int PostProjectId = Convert.ToInt16(SessionProjectId);


        oProjectAttribute.Display_B5Project_Attributes(PostProjectId);

        lblProjectName.Text = oProjectAttribute.ProjectName;
        txtBackground.Text = oProjectAttribute.Background;
        lblDuration.Text = oProjectAttribute.Duration;
        lblError.Visible = false;
        populateB5ProjectEmployment();
        Bindchart();
    }

      



    public void populateB5ProjectEmployment()
    {
        try
        {
            gvProjectEmployment.DataSource = oActivity.DisplayB5ProjectEmployment(Convert.ToInt16(Session["ProjectId"]));
            gvProjectEmployment.AutoGenerateColumns = false;
            gvProjectEmployment.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        
    }


    private void Bindchart()
    {

        DataSet ds = oProjectAttribute.Display_B5Project_Attributes(Convert.ToInt16((string)Session["ProjectId"]));
        DataTable ChartData = ds.Tables[0];

        //storing total rows count to loop on each Record   
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];

        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis   
            XPointMember[count] = ChartData.Rows[count]["Name"].ToString();
            //storing values for Y Axis   
            YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Status"]);

        }
        //binding chart control   
        cProgress.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line   
        cProgress.Series[0].BorderWidth = 10;
        //setting Chart type    
        cProgress.Series[0].ChartType = SeriesChartType.Bar;


        foreach (Series charts in cProgress.Series)
        {
            foreach (DataPoint point in charts.Points)
            {
                switch (point.AxisLabel)
                {
                    case "Q1": point.Color = Color.YellowGreen; break;
                    case "Q2": point.Color = Color.Yellow; break;
                    case "Q3": point.Color = Color.SpringGreen; break;
                }
                point.Label = string.Format("{0:0} ", point.YValues[0], point.AxisLabel);

            }
        }


        //Enabled 3D   
        //  Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;   


    }

    protected void gvMainActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProjectEmployment.PageIndex = e.NewPageIndex;
        populateB5ProjectEmployment();
    }
}