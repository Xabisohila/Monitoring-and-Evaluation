//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class pageInterventionsDirectDetail : System.Web.UI.Page
//{
//    private DataSet currentInterventionDetailsDataSet;
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (!IsPostBack) // Only load data on the initial page load, not postbacks (like button clicks)
//        {
//            if (Request.QueryString["id"] != null) // Check if an "id" is present in the URL
//            {
//                int interventionId;
//                // Try to parse the ID from the query string
//                if (int.TryParse(Request.QueryString["id"], out interventionId))
//                {
//                    LoadInterventionDetails(interventionId);
//                }
//                else
//                {
//                    // Handle cases where ID is not a valid integer
//                    Response.Write("<p>Error: Invalid Intervention ID provided in the URL.</p>");
//                    // Or redirect to a list page: Response.Redirect("InterventionList.aspx");
//                }
//            }
//            else
//            {
//                // Handle cases where no ID is provided in the URL
//                Response.Write("<p>Error: No Intervention ID provided. Please use a URL like InterventionDetail.aspx?id=801</p>");
//                // Or redirect to a list page: Response.Redirect("InterventionList.aspx");
//            }

//            // Set the back link to the Planning Overview page, preserving filters if possible
//            string backUrl = "PlanningOverview.aspx";
//            if (Request.UrlReferrer != null) // Check if there's a referring page
//            {
//                // If the referrer was PlanningOverview, try to go back to it
//                if (Request.UrlReferrer.AbsolutePath.Contains("PlanningOverview.aspx"))
//                {
//                    backUrl = Request.UrlReferrer.ToString();
//                }
//            }
//            hlBackToOverview.NavigateUrl = backUrl;
//        }
//    }

//    private void LoadInterventionDetails(int interventionId)
//    {
//        InterventionDAL dal = new InterventionDAL();

//        currentInterventionDetailsDataSet = dal.GetInterventionDetails(interventionId);

//        if (currentInterventionDetailsDataSet != null && currentInterventionDetailsDataSet.Tables.Count >= 4) {
//            //DataSet ds = dal.GetInterventionDetails(interventionId); // Call the DAL method

//            DataTable interventionTable = currentInterventionDetailsDataSet.Tables[0];

//            if (interventionTable.Rows.Count > 0)
//            {
//                DataRow interventionRow = interventionTable.Rows[0];

//                lblInterventionName.Text = interventionRow["InterventionName"].ToString();
//                lblDescription.Text = interventionRow["InterventionDescription"] != DBNull.Value ? interventionRow["InterventionDescription"].ToString() : "N/A";
//                lblLeadInstitution.Text = interventionRow["LeadInstitution"].ToString();
//                lblWorkingGroup.Text = interventionRow["WorkingGroup"] != DBNull.Value ? interventionRow["WorkingGroup"].ToString() : "N/A";
//                lblMunicipality.Text = interventionRow["PrimaryMunicipality"] != DBNull.Value ? interventionRow["PrimaryMunicipality"].ToString() : "N/A";
//                lblSpatialReference.Text = interventionRow["SpatialReference"] != DBNull.Value ? interventionRow["SpatialReference"].ToString() : "N/A";
//                lblInterventionPeriod.Text = string.Format("{0} - {1}", interventionRow["InterventionStartYear"], interventionRow["InterventionEndYear"]);

//                // Populate HyperLinks for strategic alignment
//                hlPOA.Text = interventionRow["ParentPOA"].ToString();
//                hlPOA.NavigateUrl = string.Format("pagePOADetail.aspx?id={0}", interventionRow);

//                hlCluster.Text = interventionRow["ParentCluster"].ToString();
//                hlCluster.NavigateUrl = string.Format("ClusterDetail.aspx?id={0}", interventionRow);

//                hlPMTDP.Text = interventionRow["AlignedPMTDPPriority"].ToString();
//                hlPMTDP.NavigateUrl = string.Format("PMTDPDetail.aspx?id={0}", interventionRow);

//                hlPDP.Text = interventionRow["AlignedPDPID"].ToString();
//                hlPDP.NavigateUrl = string.Format("PDPDetail.aspx?id={0}", interventionRow);
//            }
//            else
//            {
//                // No intervention found with that ID
//                Response.Write("<p>No intervention details found for the provided ID.</p>");
//                return; // Exit if no main intervention data
//            }

//            // Result Set 2: Indicators and Targets (ds.Tables[1])
//            gvIndicators.DataSource = currentInterventionDetailsDataSet.Tables[1];
//            gvIndicators.DataBind();

//            // Result Set 3: Budgets (ds.Tables[2])
//            gvBudgets.DataSource = currentInterventionDetailsDataSet.Tables[2];
//            gvBudgets.DataBind();

//            // Result Set 4: Quarterly Reports (ds.Tables[3])
//            gvQuarterlyReports.DataSource = currentInterventionDetailsDataSet.Tables[3];
//            gvQuarterlyReports.DataBind();
//        }
//        else
//        {
//            // Something went wrong or not enough data tables returned
//            Response.Write("<p>Error: Failed to retrieve complete intervention data. Check stored procedure and database connection.</p>");
//        }
//    }











//}








using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class pageInterventionsDirectDetail : System.Web.UI.Page
{
    private DataSet currentInterventionDetailsDataSet;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // Only load data on the initial page load, not postbacks (like button clicks)
        {
            if (Request.QueryString["id"] != null) // Check if an "id" is present in the URL
            {
                int interventionId;
                // Try to parse the ID from the query string
                if (int.TryParse(Request.QueryString["id"], out interventionId))
                {
                    LoadInterventionDetails(interventionId);
                }
                else
                {
                    // Handle cases where ID is not a valid integer
                    Response.Write("<p>Error: Invalid Intervention ID provided in the URL.</p>");
                    // Or redirect to a list page: Response.Redirect("InterventionList.aspx");
                }
            }
            else
            {
                // Handle cases where no ID is provided in the URL
                Response.Write("<p>Error: No Intervention ID provided. Please use a URL like InterventionDetail.aspx?id=801</p>");
                // Or redirect to a list page: Response.Redirect("InterventionList.aspx");
            }
            // Set the back link to the Planning Overview page, preserving filters if possible
            string backUrl = "PlanningOverview.aspx";
            if (Request.UrlReferrer != null) // Check if there's a referring page
            {
                // If the referrer was PlanningOverview, try to go back to it
                if (Request.UrlReferrer.AbsolutePath.Contains("PlanningOverview.aspx"))
                {
                    backUrl = Request.UrlReferrer.ToString();
                }
            }
            hlBackToOverview.NavigateUrl = backUrl;
        }
    }
    private void LoadInterventionDetails(int interventionId)
    {
        InterventionDAL dal = new InterventionDAL();
        currentInterventionDetailsDataSet = dal.GetInterventionDetails(interventionId);
        if (currentInterventionDetailsDataSet != null && currentInterventionDetailsDataSet.Tables.Count >= 4)
        {
            //DataSet ds = dal.GetInterventionDetails(interventionId); // Call the DAL method
            DataTable interventionTable = currentInterventionDetailsDataSet.Tables[0];
            if (interventionTable.Rows.Count > 0)
            {
                DataRow interventionRow = interventionTable.Rows[0];
                lblInterventionName.Text = interventionRow["InterventionName"].ToString();
                lblDescription.Text = interventionRow["InterventionDescription"] != DBNull.Value ? interventionRow["InterventionDescription"].ToString() : "N/A";
                lblLeadInstitution.Text = interventionRow["LeadInstitution"].ToString();
                lblWorkingGroup.Text = interventionRow["WorkingGroup"] != DBNull.Value ? interventionRow["WorkingGroup"].ToString() : "N/A";
                lblMunicipality.Text = interventionRow["PrimaryMunicipality"] != DBNull.Value ? interventionRow["PrimaryMunicipality"].ToString() : "N/A";
                lblSpatialReference.Text = interventionRow["SpatialReference"] != DBNull.Value ? interventionRow["SpatialReference"].ToString() : "N/A";
                lblInterventionPeriod.Text = string.Format("{0} - {1}", interventionRow["InterventionStartYear"], interventionRow["InterventionEndYear"]);
                // Populate HyperLinks for strategic alignment
                hlPOA.Text = interventionRow["ParentPOA"].ToString();
                hlPOA.NavigateUrl = string.Format("pagePOADetail.aspx?id={0}", interventionRow);
                hlCluster.Text = interventionRow["ParentCluster"].ToString();
                hlCluster.NavigateUrl = string.Format("ClusterDetail.aspx?id={0}", interventionRow);
                hlPMTDP.Text = interventionRow["AlignedPMTDPPriority"].ToString();
                hlPMTDP.NavigateUrl = string.Format("PMTDPDetail.aspx?id={0}", interventionRow);
                hlPDP.Text = interventionRow["AlignedPDPID"].ToString();
                hlPDP.Text = interventionRow["AlignedPDP"].ToString();
                //hlPDP.NavigateUrl = string.Format("PDPDetail.aspx?id={0}", interventionRow);
                hlPDP.NavigateUrl = string.Format("detailsPDP.aspx?id={0}", interventionRow);
            }
            else
            {
                // No intervention found with that ID
                Response.Write("<p>No intervention details found for the provided ID.</p>");
                return; // Exit if no main intervention data
            }
            // Result Set 2: Indicators and Targets (ds.Tables[1])
            gvIndicators.DataSource = currentInterventionDetailsDataSet.Tables[1];
            gvIndicators.DataBind();
            // Result Set 3: Budgets (ds.Tables[2])
            gvBudgets.DataSource = currentInterventionDetailsDataSet.Tables[2];
            gvBudgets.DataBind();
            // Result Set 4: Quarterly Reports (ds.Tables[3])
            gvQuarterlyReports.DataSource = currentInterventionDetailsDataSet.Tables[3];
            gvQuarterlyReports.DataBind();
        }
        else
        {
            // Something went wrong or not enough data tables returned
            Response.Write("<p>Error: Failed to retrieve complete intervention data. Check stored procedure and database connection.</p>");
        }
    }
}