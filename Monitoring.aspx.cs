using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

public partial class MonitoringData : System.Web.UI.Page
{
    clsMnERecord oMnERecord = new clsMnERecord();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!Page.IsPostBack == true)
            {
                LoadMonitoringData();
                lblFinancialYear.Text = (string)Session["FinancialYearText"];
                lblQuarter.Text = (string)Session["Quarter"];
            }
            else
            {
                //LoadMonitoringData();
                lblFinancialYear.Text = (string)Session["FinancialYearText"];
                lblQuarter.Text = (string)Session["Quarter"];
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        

        
    }

    public void StratregicPriotiy()
    {

        AccordionPane1.Visible = false;
        AccordionPane2.Visible = false;
        AccordionPane3.Visible = false;
        AccordionPane4.Visible = false;
        AccordionPane5.Visible = false;
        AccordionPane6.Visible = false;
        AccordionPane7.Visible = false;
        AccordionPane8.Visible = false;
        AccordionPane9.Visible = false;
        AccordionPane10.Visible = false;
        AccordionPane11.Visible = false;
        int TotalRows = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows.Count;
        //foreach (DataColumn dc in oMnERecord.Select_Suboutcome(Convert.ToInt16(Session["Cluster"])).Tables[0].Rows.Count > 0)
        //{
        for (int i = 0; i < TotalRows; ++i)
        {
            if (i == 0)
            {
                //Session["SubOutcome1"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[0][0];
                lblFocusArea1.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[0][1].ToString();
                AccordionPane1.Visible = true;
                Session["FocusArea1"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[0][0].ToString();
                populateFocusArea1();
            }
            else if (i == 1)
            {
                //Session["SubOutcome2"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[1][0];
                lblFocusArea2.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[1][1].ToString();
                AccordionPane2.Visible = true;
                Session["FocusArea2"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[1][0].ToString();
                populateFocusArea2();
            }
            else if (i == 2)
            {
                //Session["SubOutcome3"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[2][0]; // number on the first column 0
                lblFocusArea3.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[2][1].ToString();
                AccordionPane3.Visible = true;
                Session["FocusArea3"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[2][0].ToString();
                populateFocusArea3();
            }
            else if (i == 3)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea4.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][1].ToString();// text on the second column 1
                AccordionPane4.Visible = true;
                Session["FocusArea4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0].ToString();// number on the first column 0
                populateFocusArea4();
            }

            else if (i == 4)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea5.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[4][1].ToString();// text on the second column 1
                AccordionPane5.Visible = true;
                Session["FocusArea5"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[4][0].ToString();// number on the first column 0
                populateFocusArea5();
            }
            else if (i == 5)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea6.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[5][1].ToString();// text on the second column 1
                AccordionPane6.Visible = true;
                Session["FocusArea6"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[5][0].ToString();// number on the first column 0
                populateFocusArea6();
            }
            else if (i == 6)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea7.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[6][1].ToString();// text on the second column 1
                AccordionPane7.Visible = true;
                Session["FocusArea7"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[6][0].ToString();// number on the first column 0
                populateFocusArea7();
            }
            else if (i == 7)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea8.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[7][1].ToString();// text on the second column 1
                AccordionPane8.Visible = true;
                Session["FocusArea8"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[7][0].ToString();// number on the first column 0
                populateFocusArea8();
            }
            else if (i == 8)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea8.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[8][1].ToString();// text on the second column 1
                AccordionPane8.Visible = true;
                Session["FocusArea8"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[8][0].ToString();// number on the first column 0
                populateFocusArea8();
            }

            else if (i == 9)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea10.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[10][1].ToString();// text on the second column 1
                AccordionPane10.Visible = true;
                Session["FocusArea10"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[10][0].ToString();// number on the first column 0
                populateFocusArea10();
            }
            else if (i == 10)
            {
                //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                lblFocusArea11.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[11][1].ToString();// text on the second column 1
                AccordionPane11.Visible = true;
                Session["FocusArea11"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[11][0].ToString();// number on the first column 0
                populateFocusArea11();
            }

        }


    }
    public void LoadMonitoringData()
    {
        StratregicPriotiy();
    

        lblStrategicPriority.Text = (string)Session["StrategicPriorityName"];
        lblClusterWGName.Text = (string)Session["ClusterWGName"];
    }

    protected void populateFocusArea1()
    {
        try
        {
            if (oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea1"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]).Tables[0].Rows.Count > 0)
            {
                gvFocusArea1.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea1.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea1.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea1.Columns[9].HeaderText = (string)Session["Total"];
                gvFocusArea1.Columns[10].HeaderText = (string)Session["YrVar"];
                gvFocusArea1.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea1"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
                gvFocusArea1.AutoGenerateColumns = false;
                gvFocusArea1.DataBind();
            }
            else
            {
                gvFocusArea1.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea1.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea1.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea1.Columns[9].HeaderText = (string)Session["Total"];
                gvFocusArea1.Columns[10].HeaderText = (string)Session["YrVar"];
                gvFocusArea1.DataSource = oMnERecord.Select_SomeMonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea1"]), Convert.ToInt16(Session["StrategicPriority"]));
                gvFocusArea1.AutoGenerateColumns = false;
                gvFocusArea1.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea2()
    {
        try
        {
            if (oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea2"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]).Tables[0].Rows.Count > 0)
            {

                gvFocusArea2.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea2.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea2.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea2.Columns[8].HeaderText = (string)Session["Total"];
                gvFocusArea2.Columns[9].HeaderText = (string)Session["YrVar"];
                gvFocusArea2.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea2"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
                gvFocusArea2.AutoGenerateColumns = false;
                gvFocusArea2.DataBind();
            }
            else
            {
                gvFocusArea2.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea2.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea2.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea2.Columns[8].HeaderText = (string)Session["Total"];
                gvFocusArea2.Columns[9].HeaderText = (string)Session["YrVar"];
                gvFocusArea2.DataSource = oMnERecord.Select_SomeMonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea2"]), Convert.ToInt16(Session["StrategicPriority"]));
                gvFocusArea2.AutoGenerateColumns = false;
                gvFocusArea2.DataBind();
            }

            

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea3()
    {
        try
        {
            if (oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea3"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]).Tables[0].Rows.Count > 0)
            {
                gvFocusArea3.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea3.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea3.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea3.Columns[8].HeaderText = (string)Session["Total"];
                gvFocusArea3.Columns[9].HeaderText = (string)Session["YrVar"];
                gvFocusArea3.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea3"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
                gvFocusArea3.AutoGenerateColumns = false;
                gvFocusArea3.DataBind();
            }
            else
            {
                gvFocusArea3.Columns[5].HeaderText = (string)Session["Planned"];
                gvFocusArea3.Columns[6].HeaderText = (string)Session["Actual"];
                gvFocusArea3.Columns[7].HeaderText = (string)Session["Var"];
                gvFocusArea3.Columns[8].HeaderText = (string)Session["Total"];
                gvFocusArea3.Columns[9].HeaderText = (string)Session["YrVar"];
                gvFocusArea3.DataSource = oMnERecord.Select_SomeMonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea3"]), Convert.ToInt16(Session["StrategicPriority"]));
                gvFocusArea3.AutoGenerateColumns = false;
                gvFocusArea3.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea4()
    {
        try
        {
            gvFocusArea4.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea4.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea4.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea4.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea4.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea4.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea4"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea4.AutoGenerateColumns = false;
            gvFocusArea4.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea5()
    {
        try
        {
            gvFocusArea5.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea5.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea5.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea5.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea5.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea5.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea5"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea5.AutoGenerateColumns = false;
            gvFocusArea5.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea6()
    {
        try
        {
            gvFocusArea6.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea6.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea6.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea6.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea6.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea6.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea6"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea6.AutoGenerateColumns = false;
            gvFocusArea6.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea7()
    {
        try
        {
            gvFocusArea7.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea7.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea7.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea7.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea7.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea7.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea7"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea7.AutoGenerateColumns = false;
            gvFocusArea7.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea8()
    {
        try
        {
            gvFocusArea8.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea8.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea8.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea8.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea8.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea8.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea9"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea8.AutoGenerateColumns = false;
            gvFocusArea8.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea9()
    {
        try
        {
            gvFocusArea9.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea9.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea9.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea9.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea9.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea9.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea9"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea9.AutoGenerateColumns = false;
            gvFocusArea9.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea10()
    {
        try
        {
            gvFocusArea10.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea10.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea10.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea10.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea10.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea10.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea10"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea10.AutoGenerateColumns = false;
            gvFocusArea10.DataBind();

        }
        catch (Exception ex)
        {

        }
    }

    protected void populateFocusArea11()
    {
        try
        {
            gvFocusArea11.Columns[5].HeaderText = (string)Session["Planned"];
            gvFocusArea11.Columns[6].HeaderText = (string)Session["Actual"];
            gvFocusArea11.Columns[7].HeaderText = (string)Session["Var"];
            gvFocusArea11.Columns[8].HeaderText = (string)Session["Total"];
            gvFocusArea11.Columns[9].HeaderText = (string)Session["YrVar"];
            gvFocusArea11.DataSource = oMnERecord.Select_MonitoringData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea10"]), Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["FinancialYear"]), (string)Session["Quarter"]);
            gvFocusArea11.AutoGenerateColumns = false;
            gvFocusArea11.DataBind();

        }
        catch (Exception ex)
        {

        }
    }


    protected void gvFocusArea1_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea1.Rows[i];
            GridViewRow previousRow = gvFocusArea1.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))

                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
   }
    protected void gvFocusArea2_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea2.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea2.Rows[i];
            GridViewRow previousRow = gvFocusArea2.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea3_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea3.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea3.Rows[i];
            GridViewRow previousRow = gvFocusArea3.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea4_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea4.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea4.Rows[i];
            GridViewRow previousRow = gvFocusArea4.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea5_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea5.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea5.Rows[i];
            GridViewRow previousRow = gvFocusArea5.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea6_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea6.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea6.Rows[i];
            GridViewRow previousRow = gvFocusArea6.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea7_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea7.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea7.Rows[i];
            GridViewRow previousRow = gvFocusArea7.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea8_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea8.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea8.Rows[i];
            GridViewRow previousRow = gvFocusArea8.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea9_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea9.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea9.Rows[i];
            GridViewRow previousRow = gvFocusArea9.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }
    protected void gvFocusArea10_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea10.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea10.Rows[i];
            GridViewRow previousRow = gvFocusArea10.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    if ((row.Cells[j].Text == previousRow.Cells[j].Text) && (row.Cells[j].Text.Trim() != "" && previousRow.Cells[j].Text.Trim() != ""))
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
            }
        }

    }

    protected void btnSubmitFocusArea1_Click(object sender, EventArgs e)
    {
       
        try
        {
            for (int i = 0; i < gvFocusArea1.Rows.Count; ++i)
            {
                Label Id = gvFocusArea1.Rows[i].FindControl("KeyResult1Id") as Label;
                TextBox Baseline = gvFocusArea1.Rows[i].FindControl("txtFA1Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea1.Rows[i].FindControl("txtFA1AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea1.Rows[i].FindControl("txtFA1Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea1.Rows[i].FindControl("txtFA1Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea1.Rows[i].FindControl("txtFA1Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea1.Rows[i].FindControl("txtFA1Q4Planning") as TextBox;
                TextBox Total = gvFocusArea1.Rows[i].FindControl("txtFA1Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
               oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
               ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);
                
            }
            
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void btnSubmitFocusArea2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea2.Rows.Count; ++i)
            {
                Label Id = gvFocusArea2.Rows[i].FindControl("KeyResult2Id") as Label;
                TextBox Baseline = gvFocusArea2.Rows[i].FindControl("txtFA2Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea2.Rows[i].FindControl("txtFA2AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea2.Rows[i].FindControl("txtFA2Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea2.Rows[i].FindControl("txtFA2Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea2.Rows[i].FindControl("txtFA2Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea2.Rows[i].FindControl("txtFA2Q4Planning") as TextBox;
                TextBox Total = gvFocusArea2.Rows[i].FindControl("txtFA2Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea3_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea3.Rows.Count; ++i)
            {
                Label Id = gvFocusArea3.Rows[i].FindControl("KeyResult3Id") as Label;
                TextBox Baseline = gvFocusArea3.Rows[i].FindControl("txtFA3Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea3.Rows[i].FindControl("txtFA3AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea3.Rows[i].FindControl("txtFA3Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea3.Rows[i].FindControl("txtFA3Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea3.Rows[i].FindControl("txtFA3Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea3.Rows[i].FindControl("txtFA3Q4Planning") as TextBox;
                TextBox Total = gvFocusArea3.Rows[i].FindControl("txtFA3Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea4_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea4.Rows.Count; ++i)
            {
                Label Id = gvFocusArea4.Rows[i].FindControl("KeyResult4Id") as Label;
                TextBox Baseline = gvFocusArea4.Rows[i].FindControl("txtFA4Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea4.Rows[i].FindControl("txtFA4AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea4.Rows[i].FindControl("txtFA4Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea4.Rows[i].FindControl("txtFA4Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea4.Rows[i].FindControl("txtFA4Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea4.Rows[i].FindControl("txtFA4Q4Planning") as TextBox;
                TextBox Total = gvFocusArea4.Rows[i].FindControl("txtFA4Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea5_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea5.Rows.Count; ++i)
            {
                Label Id = gvFocusArea5.Rows[i].FindControl("KeyResult5Id") as Label;
                TextBox Baseline = gvFocusArea5.Rows[i].FindControl("txtFA5Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea5.Rows[i].FindControl("txtFA5AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea5.Rows[i].FindControl("txtFA5Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea5.Rows[i].FindControl("txtFA5Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea5.Rows[i].FindControl("txtFA5Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea5.Rows[i].FindControl("txtFA5Q4Planning") as TextBox;
                TextBox Total = gvFocusArea5.Rows[i].FindControl("txtFA5Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea6_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea6.Rows.Count; ++i)
            {
                Label Id = gvFocusArea6.Rows[i].FindControl("KeyResult6Id") as Label;
                TextBox Baseline = gvFocusArea6.Rows[i].FindControl("txtFA6Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea6.Rows[i].FindControl("txtFA6AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea6.Rows[i].FindControl("txtFA6Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea6.Rows[i].FindControl("txtFA6Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea6.Rows[i].FindControl("txtFA6Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea6.Rows[i].FindControl("txtFA6Q4Planning") as TextBox;
                TextBox Total = gvFocusArea6.Rows[i].FindControl("txtFA6Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea7_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea7.Rows.Count; ++i)
            {
                Label Id = gvFocusArea7.Rows[i].FindControl("KeyResult7Id") as Label;
                TextBox Baseline = gvFocusArea7.Rows[i].FindControl("txtFA7Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea7.Rows[i].FindControl("txtFA7AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea7.Rows[i].FindControl("txtFA7Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea7.Rows[i].FindControl("txtFA7Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea7.Rows[i].FindControl("txtFA7Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea7.Rows[i].FindControl("txtFA7Q4Planning") as TextBox;
                TextBox Total = gvFocusArea7.Rows[i].FindControl("txtFA7Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea8_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea8.Rows.Count; ++i)
            {
                Label Id = gvFocusArea8.Rows[i].FindControl("KeyResult8Id") as Label;
                TextBox Baseline = gvFocusArea8.Rows[i].FindControl("txtFA8Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea8.Rows[i].FindControl("txtFA8AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea8.Rows[i].FindControl("txtFA8Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea8.Rows[i].FindControl("txtFA8Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea8.Rows[i].FindControl("txtFA8Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea8.Rows[i].FindControl("txtFA8Q4Planning") as TextBox;
                TextBox Total = gvFocusArea8.Rows[i].FindControl("txtFA8Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea9_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea9.Rows.Count; ++i)
            {
                Label Id = gvFocusArea9.Rows[i].FindControl("KeyResult9Id") as Label;
                TextBox Baseline = gvFocusArea9.Rows[i].FindControl("txtFA9Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea9.Rows[i].FindControl("txtFA9AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea9.Rows[i].FindControl("txtFA9Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea9.Rows[i].FindControl("txtFA9Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea9.Rows[i].FindControl("txtFA9Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea9.Rows[i].FindControl("txtFA9Q4Planning") as TextBox;
                TextBox Total = gvFocusArea9.Rows[i].FindControl("txtFA9Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
    protected void btnSubmitFocusArea10_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea10.Rows.Count; ++i)
            {
                Label Id = gvFocusArea10.Rows[i].FindControl("KeyResult10Id") as Label;
                TextBox Baseline = gvFocusArea10.Rows[i].FindControl("txtFA10Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea10.Rows[i].FindControl("txtFA10AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea10.Rows[i].FindControl("txtFA10Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea10.Rows[i].FindControl("txtFA10Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea10.Rows[i].FindControl("txtFA10Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea10.Rows[i].FindControl("txtFA10Q4Planning") as TextBox;
                TextBox Total = gvFocusArea10.Rows[i].FindControl("txtFA10Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA1Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea1.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea1.Rows[i].FindControl("txtFA1Actual") as TextBox;
                TextBox Planning = gvFocusArea1.Rows[i].FindControl("txtFA1Planning") as TextBox;
                TextBox Varience = gvFocusArea1.Rows[i].FindControl("txtFA1Varience") as TextBox;
                TextBox Annual = gvFocusArea1.Rows[i].FindControl("txtFA1AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea1.Rows[i].FindControl("txtFA1YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea1.Rows[i].FindControl("txtFA1YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea1.Rows[i].FindControl("irFA1") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea1.Rows[i].FindControl("isFA1") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please calculate from the beginning !')", true);
        }
    }
    
    protected void txtFA2Actual_TextChanged(object sender, EventArgs e)
    {
     try
            {
                for (int i = 0; i < gvFocusArea2.Rows.Count; ++i)
                {
                    TextBox Actual = gvFocusArea2.Rows[i].FindControl("txtFA2Actual") as TextBox;
                    TextBox Planning = gvFocusArea2.Rows[i].FindControl("txtFA2Planning") as TextBox;
                    TextBox Varience = gvFocusArea2.Rows[i].FindControl("txtFA2Varience") as TextBox;
                    TextBox Annual = gvFocusArea2.Rows[i].FindControl("txtFA2AnnualTarget") as TextBox;
                    TextBox YTDTotal = gvFocusArea2.Rows[i].FindControl("txtFA2YTDTotal") as TextBox;
                    TextBox YrVar = gvFocusArea2.Rows[i].FindControl("txtFA2YrVar") as TextBox;
                    System.Web.UI.WebControls.Image RoundImage = gvFocusArea2.Rows[i].FindControl("irFA2") as System.Web.UI.WebControls.Image;
                    System.Web.UI.WebControls.Image SquareImage = gvFocusArea2.Rows[i].FindControl("isFA2") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

            }

            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
            }
    }

    protected void txtFA3Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea3.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea3.Rows[i].FindControl("txtFA3Actual") as TextBox;
                TextBox Planning = gvFocusArea3.Rows[i].FindControl("txtFA3Planning") as TextBox;
                TextBox Varience = gvFocusArea3.Rows[i].FindControl("txtFA3Varience") as TextBox;
                TextBox Annual = gvFocusArea3.Rows[i].FindControl("txtFA3AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea3.Rows[i].FindControl("txtFA3YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea3.Rows[i].FindControl("txtFA3YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea3.Rows[i].FindControl("irFA3") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea3.Rows[i].FindControl("isFA3") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA4Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea4.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea4.Rows[i].FindControl("txtFA4Actual") as TextBox;
                TextBox Planning = gvFocusArea4.Rows[i].FindControl("txtFA4Planning") as TextBox;
                TextBox Varience = gvFocusArea4.Rows[i].FindControl("txtFA4Varience") as TextBox;
                TextBox Annual = gvFocusArea4.Rows[i].FindControl("txtFA4AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea4.Rows[i].FindControl("txtFA4YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea4.Rows[i].FindControl("txtFA4YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea4.Rows[i].FindControl("irFA4") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea4.Rows[i].FindControl("isFA4") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA5Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea5.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea5.Rows[i].FindControl("txtFA5Actual") as TextBox;
                TextBox Planning = gvFocusArea5.Rows[i].FindControl("txtFA5Planning") as TextBox;
                TextBox Varience = gvFocusArea5.Rows[i].FindControl("txtFA5Varience") as TextBox;
                TextBox Annual = gvFocusArea5.Rows[i].FindControl("txtFA5AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea5.Rows[i].FindControl("txtFA5YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea5.Rows[i].FindControl("txtFA5YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea5.Rows[i].FindControl("irFA5") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea5.Rows[i].FindControl("isFA5") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA6Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea6.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea6.Rows[i].FindControl("txtFA6Actual") as TextBox;
                TextBox Planning = gvFocusArea6.Rows[i].FindControl("txtFA6Planning") as TextBox;
                TextBox Varience = gvFocusArea6.Rows[i].FindControl("txtFA6Varience") as TextBox;
                TextBox Annual = gvFocusArea6.Rows[i].FindControl("txtFA6AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea6.Rows[i].FindControl("txtFA6YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea6.Rows[i].FindControl("txtFA6YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea6.Rows[i].FindControl("irFA6") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea6.Rows[i].FindControl("isFA6") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA7Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea7.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea7.Rows[i].FindControl("txtFA7Actual") as TextBox;
                TextBox Planning = gvFocusArea7.Rows[i].FindControl("txtFA7Planning") as TextBox;
                TextBox Varience = gvFocusArea7.Rows[i].FindControl("txtFA7Varience") as TextBox;
                TextBox Annual = gvFocusArea7.Rows[i].FindControl("txtFA7AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea7.Rows[i].FindControl("txtFA7YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea7.Rows[i].FindControl("txtFA7YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea7.Rows[i].FindControl("irFA7") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea7.Rows[i].FindControl("isFA7") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA8Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea8.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea8.Rows[i].FindControl("txtFA8Actual") as TextBox;
                TextBox Planning = gvFocusArea8.Rows[i].FindControl("txtFA8Planning") as TextBox;
                TextBox Varience = gvFocusArea8.Rows[i].FindControl("txtFA8Varience") as TextBox;
                TextBox Annual = gvFocusArea8.Rows[i].FindControl("txtFA8AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea8.Rows[i].FindControl("txtFA8YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea8.Rows[i].FindControl("txtFA8YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea8.Rows[i].FindControl("irFA8") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea8.Rows[i].FindControl("isFA8") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA9Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea9.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea9.Rows[i].FindControl("txtFA8Actual") as TextBox;
                TextBox Planning = gvFocusArea9.Rows[i].FindControl("txtFA8Planning") as TextBox;
                TextBox Varience = gvFocusArea9.Rows[i].FindControl("txtFA8Varience") as TextBox;
                TextBox Annual = gvFocusArea9.Rows[i].FindControl("txtFA8AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea9.Rows[i].FindControl("txtFA8YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea9.Rows[i].FindControl("txtFA8YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea9.Rows[i].FindControl("irFA9") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea9.Rows[i].FindControl("isFA9") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA10Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea10.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea10.Rows[i].FindControl("txtFA10Actual") as TextBox;
                TextBox Planning = gvFocusArea10.Rows[i].FindControl("txtFA10Planning") as TextBox;
                TextBox Varience = gvFocusArea10.Rows[i].FindControl("txtFA10Varience") as TextBox;
                TextBox Annual = gvFocusArea10.Rows[i].FindControl("txtFA10AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea10.Rows[i].FindControl("txtFA10YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea10.Rows[i].FindControl("txtFA10YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea10.Rows[i].FindControl("irFA10") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea10.Rows[i].FindControl("isFA10") as System.Web.UI.WebControls.Image;

                int ActualNo = Convert.ToInt16(Actual.Text);
                int PlanningNo = Convert.ToInt16(Planning.Text);
                int AnnualNo = Convert.ToInt16(Annual.Text);
                Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                YTDTotal.Text = Convert.ToString(ActualNo);
                YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                if (ActualNo >= (PlanningNo * 80 / 100))
                {
                    RoundImage.ImageUrl = "img/rgreen.png";
                }
                if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                {
                    RoundImage.ImageUrl = "img/ryellow.png";
                }
                if (ActualNo <= (PlanningNo * 50 / 100))
                {
                    RoundImage.ImageUrl = "img/rred.png";
                    SquareImage.ImageUrl = "img/sred.png";
                }

                if (ActualNo >= (PlanningNo * 95 / 100))
                {
                    SquareImage.ImageUrl = "img/sgreen.png";
                }
                if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                {
                    SquareImage.ImageUrl = "img/syellow.png";
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void txtFA11Actual_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea11.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea11.Rows[i].FindControl("txtFA10Actual") as TextBox;
                TextBox Planning = gvFocusArea11.Rows[i].FindControl("txtFA10Planning") as TextBox;
                TextBox Varience = gvFocusArea11.Rows[i].FindControl("txtFA10Varience") as TextBox;
                TextBox Annual = gvFocusArea11.Rows[i].FindControl("txtFA10AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea11.Rows[i].FindControl("txtFA10YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea11.Rows[i].FindControl("txtFA10YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea11.Rows[i].FindControl("irFA10") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea11.Rows[i].FindControl("isFA10") as System.Web.UI.WebControls.Image;

                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void btnSubmitFocusArea11_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvFocusArea11.Rows.Count; ++i)
            {
                TextBox Actual = gvFocusArea11.Rows[i].FindControl("txtFA10Actual") as TextBox;
                TextBox Planning = gvFocusArea11.Rows[i].FindControl("txtFA10Planning") as TextBox;
                TextBox Varience = gvFocusArea11.Rows[i].FindControl("txtFA10Varience") as TextBox;
                TextBox Annual = gvFocusArea11.Rows[i].FindControl("txtFA10AnnualTarget") as TextBox;
                TextBox YTDTotal = gvFocusArea11.Rows[i].FindControl("txtFA10YTDTotal") as TextBox;
                TextBox YrVar = gvFocusArea11.Rows[i].FindControl("txtFA10YrVar") as TextBox;
                System.Web.UI.WebControls.Image RoundImage = gvFocusArea11.Rows[i].FindControl("irFA10") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image SquareImage = gvFocusArea11.Rows[i].FindControl("isFA10") as System.Web.UI.WebControls.Image;
                
                if (Actual.Text != "")
                {
                    int ActualNo = Convert.ToInt16(Actual.Text);
                    int PlanningNo = Convert.ToInt16(Planning.Text);
                    int AnnualNo = Convert.ToInt16(Annual.Text);
                    Varience.Text = Convert.ToString(ActualNo - PlanningNo);
                    YTDTotal.Text = Convert.ToString(ActualNo);
                    YrVar.Text = Convert.ToString(AnnualNo - ActualNo);

                    if (ActualNo >= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/rgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 80 / 100))
                    {
                        RoundImage.ImageUrl = "img/ryellow.png";
                    }
                    if (ActualNo <= (PlanningNo * 50 / 100))
                    {
                        RoundImage.ImageUrl = "img/rred.png";
                        SquareImage.ImageUrl = "img/sred.png";
                    }

                    if (ActualNo >= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/sgreen.png";
                    }
                    if (ActualNo > (PlanningNo * 50 / 100) && ActualNo <= (PlanningNo * 95 / 100))
                    {
                        SquareImage.ImageUrl = "img/syellow.png";
                    }
                }
                
                
                //oMnERecord.Total = Total.Text;
                //oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }
}