using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

public partial class upload : System.Web.UI.Page
{
    clsMnERecord oRecord = new clsMnERecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (IsPostBack != true)
            {



                populateCluster();

                lblError.Visible = false;


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
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlIndicator.SelectedIndex != 0)
            {
                //oRecord.KeyIndicatorId = Convert.ToInt32(ddlIndicator.SelectedValue);


                if (FileUpload1.HasFile == true)
                {
                    oRecord.KeyResultId = (Convert.ToInt32(ddlIndicator.SelectedValue));
                    oRecord.FilePath = "Evidence/" + FileUpload1.FileName;
                    oRecord.FileName = txtFileName.Text;
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Evidence/" + FileUpload1.FileName);
                    FileUpload1.SaveAs(path);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please load the right formart.')", true);
                }

                oRecord.SubmitEvidence();

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Information Submitted Successfully.')", true);
                PopulateChart(Convert.ToInt32(ddlIndicator.SelectedValue), ddlIndicator.SelectedItem.Text);
                ddlIntervention.SelectedIndex = 0;
                ddlSuboutcome.SelectedIndex = 0;
                ddlStrategicPriority.SelectedIndex = 0;
                ddlCluster.SelectedIndex = 0;
                txtFileName.Text = "";
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please select Indicator first')", true);
            }


        }
        catch (Exception ex){

        }


    }

    private void PopulateChart(int KeyIndicatorId, string KeyIndicator)
    {
        string[] x = new string[oRecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count];
        decimal[] y = new decimal[oRecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count];
        for (int i = 0; i < oRecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows.Count; i++)
        {
            x[i] = oRecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows[i][0].ToString();
            y[i] = Convert.ToInt32(oRecord.SelectPlanningScores(KeyIndicatorId).Tables[0].Rows[i][1]);
        }
        bcKeyIndicator.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, Name = KeyIndicator, BarColor = "#B85B3E" });
        bcKeyIndicator.CategoriesAxis = string.Join(",", x);
        bcKeyIndicator.ChartTitle = (string)Session["ChartTitle"];


    }

    public void populateIntervention()
    {
        ddlIntervention.DataSource = oRecord.Select_Intervention(Convert.ToInt16(ddlSuboutcome.SelectedValue));
        ddlIntervention.DataValueField = "InterventionId";
        ddlIntervention.DataTextField = "Intervention";
        ddlIntervention.DataBind();
        ddlIntervention.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Intervenstion", "0"));
    }

    public void populateSubOutcome()
    {
        ddlSuboutcome.DataSource = oRecord.Select_Suboutcome(Convert.ToInt16(ddlCluster.SelectedValue));
        ddlSuboutcome.DataValueField = "SuboutcomeId";
        ddlSuboutcome.DataTextField = "Suboutcome";
        ddlSuboutcome.DataBind();
        ddlSuboutcome.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select PMTDP OUTCOMES", "0"));
    }

    public void populateStrategicPriority()
    {
        ddlStrategicPriority.DataSource = oRecord.Select_StrategicPriority(Convert.ToInt16(ddlCluster.SelectedValue));
        ddlStrategicPriority.DataValueField = "StrategicPriorityId";
        ddlStrategicPriority.DataTextField = "StrategicPriorityName";
        ddlStrategicPriority.DataBind();
        ddlStrategicPriority.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select PMTDP PRIORITY", "0"));
    }

    public void populateCluster()
    {
        ddlCluster.DataSource = oRecord.Select_Cluster();
        ddlCluster.DataValueField = "ClusterId";
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Cluster", "0"));
    }


    public void populateIndicator()
    {
        ddlIndicator.DataSource = oRecord.Select_Indicator(Convert.ToInt16(ddlIntervention.SelectedValue));
        ddlIndicator.DataValueField = "KeyResultId";
        ddlIndicator.DataTextField = "KeyIndicator";
        ddlIndicator.DataBind();
        ddlIndicator.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Indicator", "0"));
    }

    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCluster.SelectedIndex != 0)
        {
            populateStrategicPriority();
            populateSubOutcome();
        }
    }


    protected void ddlSuboutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSuboutcome.SelectedIndex != 0)
        {
            populateIntervention();
        }
    }


    protected void ddlIntervention_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIntervention.SelectedIndex != 0)
                {
                    populateIndicator();

                }
        
    }

    protected void ddlIndicator_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateChart(Convert.ToInt32(ddlIndicator.SelectedValue), ddlIndicator.SelectedItem.Text);
        bcKeyIndicator.Visible = true;
        populateDownload();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow datarow = (GridViewRow)(((Control)sender).NamingContainer);
            int i = datarow.RowIndex;
            foreach (GridViewRow rowItem in gvDownload.Rows)
            {
                if (rowItem.RowIndex == i)
                {
                    string filename = rowItem.Cells[0].ToString();
                    if (filename != "")
                    {
                        string root = AppDomain.CurrentDomain.BaseDirectory;
                        string FilePath = ((Label)gvDownload.Rows[i].FindControl("lblFilePath")).Text;
                        string ServerPath = root + FilePath;

                        // --------------------------
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        string ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

                        System.IO.FileInfo file = new System.IO.FileInfo(ServerPath);
                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }
                    else
                    {
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            lblError.Text = "The document cannot be downloaded at this moment, please contact the administrator";
            lblError.Visible = true;
            lblError.ForeColor = System.Drawing.Color.Orange;
        }

    }

    public void populateDownload()
    {
        try
        {
            // convertSystemGeneratedFinancialYear();

            gvDownload.DataSource = oRecord.SelectEvidence(Convert.ToInt32(ddlIndicator.SelectedValue));
            gvDownload.AutoGenerateColumns = false;
            gvDownload.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
}