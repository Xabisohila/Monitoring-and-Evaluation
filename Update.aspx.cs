using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update : System.Web.UI.Page
{
    clsMnERecord oMnERecord = new clsMnERecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Fullname"] != null)
        {
            if (!Page.IsPostBack)
            {
                
                if (Session["baseline"] != null)
                {
                    lbl_baseline.Text = Session["baseline"].ToString();
                    populateDetails();
                }
                else
                {

                }
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

    public void populateDetails()
    {
        lblFinancialYear.Text = (string)Session["FinancialYearText"];
        lblStrategicPriority.Text = (string)Session["StrategicPriorityName"];
        lblClusterWGName.Text = (string)Session["ClusterWGName"];

        oMnERecord.Get_PlanningData(Convert.ToInt16(Session["Cluster"]), Convert.ToInt16(Session["FocusArea1"]), 
            Convert.ToInt16(Session["StrategicPriority"]), Convert.ToInt16(Session["keyResultId"]));

        txt_Intervention.Text = oMnERecord.Intervention;
        txt_Baseline.Text = oMnERecord.Baseline;
        txt_AnnualTarget.Text = oMnERecord.AnnualTarget;
        txt_Q1.Text = oMnERecord.Q1Planning;
        txt_Q2.Text = oMnERecord.Q2Planning;
        txt_Q3.Text = oMnERecord.Q3Planning;
        txt_Q4.Text = oMnERecord.Q4Planning;

        txt_FinancialYearId.Text = Session["FinancialYear"].ToString();
        txt_KeyResultId.Text = Session["keyResultId"].ToString();
        txt_Total.Text = oMnERecord.Total;



    }

    public void Update_One()
    {
        try
        {
            oMnERecord.KeyResultId = Convert.ToInt32(Session["keyResultId"]);
            oMnERecord.AnnualTarget = txt_AnnualTarget.Text;
            oMnERecord.Baseline = txt_Baseline.Text;
            oMnERecord.Q1Planning = txt_Q1.Text;
            oMnERecord.Q2Planning = txt_Q2.Text;
            oMnERecord.Q3Planning = txt_Q3.Text;
            oMnERecord.Q4Planning = txt_Q4.Text;
            oMnERecord.Total = txt_Total.Text;
            oMnERecord.FinancialYear = Convert.ToInt16((string)Session["FinancialYear"]);

            oMnERecord.SubmitPlanningData(Convert.ToInt16(oMnERecord.KeyResultId));
            
            string script = "alert('Submission Update Successful!'); window.location.href = 'lastPlanningST.aspx';";
            ScriptManager.RegisterStartupScript(this, GetType(), "PopupRedirectScript", script, true);

        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('File is not yet available!')", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Update_One();
    }
}