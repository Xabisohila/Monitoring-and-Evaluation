using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;



public partial class editPlanningST : System.Web.UI.Page
{
    clsMnERecord oMnERecord = new clsMnERecord();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Fullname"] != null)
        {
            if (!Page.IsPostBack == true)
            {
                LoadPlanningData();
                lblFinancialYear.Text = (string)Session["FinancialYearText"];
            }
            else
            {

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
                lblFocusArea10.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[09][1].ToString();// text on the second column 1
                AccordionPane10.Visible = true;
                Session["FocusArea10"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[09][0].ToString();// number on the first column 0
                populateFocusArea10();
            }
            else if (i == 10)
            {
                //int check = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows.Count;
                //if (oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows.Count != 0) { 
                    //Session["SubOutcome4"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[3][0]; 
                    lblFocusArea11.Text = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[10][1].ToString();// text on the second column 1
                    AccordionPane11.Visible = true;
                    Session["FocusArea11"] = oMnERecord.Select_Suboutcome_byMTFS(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["StrategicPriority"])).Tables[0].Rows[10][0].ToString();// number on the first column 0
                    populateFocusArea11();
                //}
                
            }

        }
        

    }
    public void LoadPlanningData()
    {
        StratregicPriotiy();

        lblStrategicPriority.Text = (string)Session["StrategicPriorityName"];
        lblClusterWGName.Text = (string)Session["ClusterWGName"];
    }

    protected void populateFocusArea1()
    {
        try
        {
            gvIntervention1.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea1"]),  Convert.ToInt16(Session["StrategicPriority"]));
            gvIntervention1.AutoGenerateColumns = false;
            gvIntervention1.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea2()
    {
        try
        {
            gvFocusArea2.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea2"]), Convert.ToInt16(Session["StrategicPriority"]));
            gvFocusArea2.AutoGenerateColumns = false;
            gvFocusArea2.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea3()
    {
        try
        {
            gvFocusArea3.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea3"]), Convert.ToInt16(Session["StrategicPriority"]));
            gvFocusArea3.AutoGenerateColumns = false;
            gvFocusArea3.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    protected void populateFocusArea4()
    {
        try
        {
            gvFocusArea4.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea4"]),  Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea5.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea5"]), Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea6.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea6"]), Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea7.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea7"]),  Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea8.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea8"]),  Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea9.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea9"]), Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea10.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea10"]), Convert.ToInt16(Session["StrategicPriority"]));
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
            gvFocusArea11.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea11"]), Convert.ToInt16(Session["StrategicPriority"]));
            gvFocusArea11.AutoGenerateColumns = false;
            gvFocusArea11.DataBind();

        }
        catch (Exception ex)
        {

        }
    }

    protected void populateFocusArea12()
    {
        try
        {
            //gvFocusArea11.DataSource = oMnERecord.Select_PlanningData(Convert.ToInt16(Session["Cluster"]), 16, Convert.ToInt16(Session["StrategicPriority"]));
            //gvFocusArea11.AutoGenerateColumns = false;
            //gvFocusArea11.DataBind();

        }
        catch (Exception ex)
        {

        }
    }




    protected void gvIntervention1_DataBound(object sender, EventArgs e)
    {
        for (int i = gvIntervention1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvIntervention1.Rows[i];
            GridViewRow previousRow = gvIntervention1.Rows[i - 1];
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

    protected void gvFocusArea11_DataBound(object sender, EventArgs e)
    {
        for (int i = gvFocusArea11.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvFocusArea11.Rows[i];
            GridViewRow previousRow = gvFocusArea11.Rows[i - 1];
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
            for (int i = 0; i < gvIntervention1.Rows.Count; ++i)
            {
                Label Id = gvIntervention1.Rows[i].FindControl("lblKeyResult1Id") as Label;
                TextBox Baseline = gvIntervention1.Rows[i].FindControl("txtFA1Baseline") as TextBox;
                TextBox AnnualTarget = gvIntervention1.Rows[i].FindControl("txtFA1AnnualTarget") as TextBox;
                TextBox Q1Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q1Planning") as TextBox;
                TextBox Q2Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q2Planning") as TextBox;
                TextBox Q3Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q3Planning") as TextBox;
                TextBox Q4Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q4Planning") as TextBox;
                TextBox Total = gvIntervention1.Rows[i].FindControl("txtFA1Total") as TextBox;


                oMnERecord.KeyResultId = Convert.ToInt16(Id.Text);
                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.FinancialYear = Convert.ToInt16((string)Session["FinancialYear"]);
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
                Label Id = gvFocusArea2.Rows[i].FindControl("lblKeyResult2Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea3.Rows[i].FindControl("lblKeyResult3Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea4.Rows[i].FindControl("lblKeyResult4Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea5.Rows[i].FindControl("lblKeyResult5Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea6.Rows[i].FindControl("lblKeyResult6Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea7.Rows[i].FindControl("lblKeyResult7Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea8.Rows[i].FindControl("lblKeyResult8Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea9.Rows[i].FindControl("lblKeyResult9Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
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
                Label Id = gvFocusArea10.Rows[i].FindControl("lblKeyResult10Id") as Label;
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
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

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
                Label Id = gvFocusArea11.Rows[i].FindControl("lblKeyResult10Id") as Label;
                TextBox Baseline = gvFocusArea11.Rows[i].FindControl("txtFA11Baseline") as TextBox;
                TextBox AnnualTarget = gvFocusArea11.Rows[i].FindControl("txtFA11AnnualTarget") as TextBox;
                TextBox Q1Planning = gvFocusArea11.Rows[i].FindControl("txtFA11Q1Planning") as TextBox;
                TextBox Q2Planning = gvFocusArea11.Rows[i].FindControl("txtFA11Q2Planning") as TextBox;
                TextBox Q3Planning = gvFocusArea11.Rows[i].FindControl("txtFA11Q3Planning") as TextBox;
                TextBox Q4Planning = gvFocusArea11.Rows[i].FindControl("txtFA11Q4Planning") as TextBox;
                TextBox Total = gvFocusArea11.Rows[i].FindControl("txtFA11Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;
                oMnERecord.FinancialYear = Convert.ToInt16(Session["FinancialYear"]);
                oMnERecord.SubmitPlanningData(Convert.ToInt16(Id.Text));
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Submission Successful!')", true);

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }


    protected void lnkKeyIndicatorFA1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvIntervention1.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvIntervention1.Rows[i].FindControl("lblKeyResult1Id");
                    Label KeyResult = (Label)gvIntervention1.Rows[i].FindControl("lblKeyResult1");
                    LinkButton KeyIndicator = (LinkButton)gvIntervention1.Rows[i].FindControl("lnkKeyIndicatorFA1");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);
                    
                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
        

    }

    private void PopulateChart(int KeyIndicatorId, string KeyIndicator)
    {
        string[] x = new string[oMnERecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count];
        decimal[] y = new decimal[oMnERecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count];
        for (int i = 0; i < oMnERecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count; i++)
        {
            x[i] = oMnERecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows[i][0].ToString();
            y[i] = Convert.ToInt32(oMnERecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows[i][1]);
        }
        bcKeyIndicator.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, Name = KeyIndicator, BarColor = "#B85B3E" });
        bcKeyIndicator.CategoriesAxis = string.Join(",", x);
        bcKeyIndicator.ChartTitle = (string)Session["ChartTitle"];


    }
    protected void lnkKeyIndicatorFA2_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea2.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea2.Rows[i].FindControl("lblKeyResult2Id");
                    Label KeyResult = (Label)gvFocusArea2.Rows[i].FindControl("lblKeyResult2");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea2.Rows[i].FindControl("lnkKeyIndicatorFA2");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA3_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea3.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea3.Rows[i].FindControl("lblKeyResult3Id");
                    Label KeyResult = (Label)gvFocusArea3.Rows[i].FindControl("lblKeyResult3");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea3.Rows[i].FindControl("lnkKeyIndicatorFA3");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkKeyIndicatorFA4_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea4.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea4.Rows[i].FindControl("lblKeyResult4Id");
                    Label KeyResult = (Label)gvFocusArea4.Rows[i].FindControl("lblKeyResult4");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea4.Rows[i].FindControl("lnkKeyIndicatorFA4");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA5_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea5.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea5.Rows[i].FindControl("lblKeyResult5Id");
                    Label KeyResult = (Label)gvFocusArea5.Rows[i].FindControl("lblKeyResult5");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea5.Rows[i].FindControl("lnkKeyIndicatorFA5");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkKeyIndicatorFA6_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea6.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea6.Rows[i].FindControl("lblKeyResult6Id");
                    Label KeyResult = (Label)gvFocusArea6.Rows[i].FindControl("lblKeyResult6");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea6.Rows[i].FindControl("lnkKeyIndicatorFA6");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA7_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea7.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea7.Rows[i].FindControl("lblKeyResult7Id");
                    Label KeyResult = (Label)gvFocusArea7.Rows[i].FindControl("lblKeyResult7");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea7.Rows[i].FindControl("lnkKeyIndicatorFA7");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA8_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea8.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea8.Rows[i].FindControl("lblKeyResult8Id");
                    Label KeyResult = (Label)gvFocusArea8.Rows[i].FindControl("lblKeyResult8");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea8.Rows[i].FindControl("lnkKeyIndicatorFA8");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA9_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea9.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea9.Rows[i].FindControl("lblKeyResult9Id");
                    Label KeyResult = (Label)gvFocusArea9.Rows[i].FindControl("lblKeyResult9");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea9.Rows[i].FindControl("lnkKeyIndicatorFA9");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkKeyIndicatorFA10_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea10.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea10.Rows[i].FindControl("lblKeyResult10Id");
                    Label KeyResult = (Label)gvFocusArea10.Rows[i].FindControl("lblKeyResult10");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea10.Rows[i].FindControl("lnkKeyIndicatorFA10");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkKeyIndicatorFA11_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvFocusArea10.Rows)
            {
                if (rowItem.RowIndex == i)
                {

                    Label KeyResultId = (Label)gvFocusArea10.Rows[i].FindControl("lblKeyResult11Id");
                    Label KeyResult = (Label)gvFocusArea10.Rows[i].FindControl("lblKeyResult11");
                    LinkButton KeyIndicator = (LinkButton)gvFocusArea10.Rows[i].FindControl("lnkKeyIndicatorFA11");
                    Session["ChartTitle"] = KeyResult.Text;
                    PopulateChart(Convert.ToInt16(KeyResultId.Text), KeyIndicator.Text);

                    bcKeyIndicator.Visible = true;
                }
            }
            //PopulateJobs();

        }
        catch (Exception ex)
        {
        }
    }


 

    protected void txtFA1Q4Planning_TextChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvIntervention1.Rows.Count; ++i)
            {
                Label Id = gvIntervention1.Rows[i].FindControl("lblKeyResult1Id") as Label;
                TextBox Baseline = gvIntervention1.Rows[i].FindControl("txtFA1Baseline") as TextBox;
                TextBox AnnualTarget = gvIntervention1.Rows[i].FindControl("txtFA1AnnualTarget") as TextBox;
                TextBox Q1Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q1Planning") as TextBox;
                TextBox Q2Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q2Planning") as TextBox;
                TextBox Q3Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q3Planning") as TextBox;
                TextBox Q4Planning = gvIntervention1.Rows[i].FindControl("txtFA1Q4Planning") as TextBox;
                TextBox Total = gvIntervention1.Rows[i].FindControl("txtFA1Total") as TextBox;


                oMnERecord.AnnualTarget = AnnualTarget.Text;
                oMnERecord.Baseline = Baseline.Text;
                oMnERecord.Q1Planning = Q1Planning.Text;
                oMnERecord.Q2Planning = Q2Planning.Text;
                oMnERecord.Q3Planning = Q3Planning.Text;
                oMnERecord.Q4Planning = Q4Planning.Text;
                oMnERecord.Total = Total.Text;

                if ((Q1Planning.Text != "") && (Q2Planning.Text != "") && (Q3Planning.Text != "") && (Q4Planning.Text != "") && (AnnualTarget.Text != ""))
                {
                    Total.Text = Convert.ToString(Convert.ToInt16(Q1Planning.Text) + Convert.ToInt16(Q2Planning.Text) + Convert.ToInt16(Q3Planning.Text) + Convert.ToInt16(Q4Planning.Text));
                    if ((Convert.ToInt16(Total.Text ))> (Convert.ToInt16(AnnualTarget.Text))){
                        Total.BackColor = Color.Red;
                    }
                    else if((Convert.ToInt16(Total.Text)) < (Convert.ToInt16(AnnualTarget.Text))){
                        Total.BackColor = Color.Red;
                    }
                    else
                    {
                        Total.BackColor = Color.Transparent;
                    }
                }

            }

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void gvIntervention1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit") // Replace "Edit" with your actual CommandName
        {
            // Get the row index
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Access the row
            GridViewRow row = gvIntervention1.Rows[rowIndex];

            // Find the TextBox in the row
            TextBox txtBox = (TextBox)row.FindControl("txtKeyIndicatorFA1");
            TextBox txtBaseline_ = (TextBox)row.FindControl("txtFA1Baseline");

            TextBox txtAnnualTarget_ = (TextBox)row.FindControl("txtFA1AnnualTarget");

            TextBox txtQ1_ = (TextBox)row.FindControl("txtFA1Q1Planning");
            TextBox txtQ2_ = (TextBox)row.FindControl("txtFA1Q2Planning");
            TextBox txtQ3_ = (TextBox)row.FindControl("txtFA1Q3Planning");
            TextBox txtQ4_ = (TextBox)row.FindControl("txtFA1Q4Planning");

            TextBox txtTotal_ = (TextBox)row.FindControl("txtFA1Total");



            if (txtBox != null)
            {
                //string textValue = txtBox.Text; // Get the value from the TextBox
                // Perform your logic with the value


                txtEditName.Text = txtBox.Text;//textValue; // Set the value to the TextBox in the modal
                txtBaseline.Text = txtBaseline_.Text;
                txtAnnualTarget.Text = txtAnnualTarget_.Text;

                txtQ1.Text = Convert.ToString(txtQ1_.Text);
                txtQ2.Text = Convert.ToString(txtQ2_.Text);
                txtQ3.Text = Convert.ToString(txtQ3_.Text);
                txtQ4.Text = Convert.ToString(txtQ4_.Text);

                txt_Total.Text = Convert.ToString(txtTotal_.Text);



                // Trigger the modal to open using ScriptManager
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "$('#editModal').modal('show');", true);
            }
        }











        //if (e.CommandName == "Edit")
        //{
        //    // Get the row index from CommandArgument
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = gvIntervention1.Rows[rowIndex];

        //    // Retrieve the data from the row (example: ID and Name)
        //    string id = row.Cells[4].Text; // Assuming the first column is ID
        //    string name = row.Cells[5].Text; // Assuming the second column is Name

        //    // Populate the modal controls with the data
        //    hiddenEditID.Value = id; // Hidden field for ID
        //    txtEditName.Text = name; // TextBox for Name

        //    // Trigger the modal to open using ScriptManager
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "$('#editModal').modal('show');", true);
        //}








    }

    protected void gvIntervention1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
    }
}