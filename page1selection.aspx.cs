using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class page1selection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cluster = Request.QueryString["cluster"];
            Session["Cluster_ID"] = cluster;

            // Reset the Planning and Quarter Hide
            rbPlanning.Checked = true;
            divQuarter.Visible = false;
            Session["selectType"] = "Planning";

            PopulateDropdowns();
            var poas = GetPOAs();
            foreach (var poa in poas)
            {
                Panel panel = new Panel { CssClass = "poa-panel" };

                panel.Controls.Add(new Literal
                {
                    Text = string.Format(
                    "<h3>{0}</h3><p>{1}</p><p><strong>Outcome:</strong> {2}</p>",
                    poa.POA_Name,
                    poa.POA_Description,
                    poa.DesiredOutcome
                    )
                });

                POAContainer.Controls.Add(panel);
            }
        }
        else
        {

        }
    }

    private List<selectionPOA> GetPOAs()
    {
        List<selectionPOA> poaList = new List<selectionPOA>();
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("new_SP_GetPOA_BySelector", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@ClusterID", ddlCluster.SelectedValue);
            cmd.Parameters.AddWithValue("@ClusterID", Session["Cluster_ID"]);

            cmd.Parameters.AddWithValue("@Mode", ddlMode.SelectedValue);
            Session["ddlMode1"] = ddlMode.SelectedValue;

            cmd.Parameters.AddWithValue("@WorkingGroupID", ddlWorkingGroup.SelectedValue);
            Session["ddlWorkingGroup"] = ddlWorkingGroup.SelectedValue;

            cmd.Parameters.AddWithValue("@PMTDP_PriorityID", ddlPriority.SelectedValue);
            cmd.Parameters.AddWithValue("@FinancialYearID", ddlFinancialYear.SelectedValue);

            if (rbPlanning.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Quarter", ddlQuarter.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Quarter", DBNull.Value);
            }

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                selectionPOA poa = new selectionPOA
                {
                    POA_ID = Convert.ToInt32(reader["POA_ID"]),
                    POA_Name = reader["POA_Name"].ToString(),
                    POA_Description = reader["POA_Description"].ToString(),
                    POA_StartYear = Convert.ToInt32(reader["POA_StartYear"]),
                    POA_EndYear = Convert.ToInt32(reader["POA_EndYear"]),
                    DesiredOutcome = reader["DesiredOutcome"].ToString(),
                    ClusterName = reader["ClusterName"].ToString(),
                    PriorityName = reader["PriorityName"].ToString(),
                    PDP_Name = reader["PDP_Name"].ToString()
                };

                poaList.Add(poa);
            }

            reader.Close();
        }
        return poaList;
    }

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void PopulateDropdowns()
    {
        PlanningOverviewRepository repo = new PlanningOverviewRepository();
        cls_QuarterlyReportDAL dal = new cls_QuarterlyReportDAL();

        // Populate Work Groups
        DataTable dtWorkGroups = repo.GetAllWorkGroupsLookup();
        ddlWorkingGroup.DataSource = dtWorkGroups;
        ddlWorkingGroup.DataTextField = "WG_Name";
        ddlWorkingGroup.DataValueField = "WorkingGroupID";
        ddlWorkingGroup.DataBind();
        ddlWorkingGroup.Items.Insert(0, new ListItem("-- Select Work Group --", "0"));

        // Populate PMTDP Priorities
        DataTable dtPMTDPPriorities = repo.GetAllPMTDPPrioritiesLookup();
        ddlPriority.DataSource = dtPMTDPPriorities;
        ddlPriority.DataTextField = "PriorityName";
        ddlPriority.DataValueField = "PMTDP_PriorityID";
        ddlPriority.DataBind();
        ddlPriority.Items.Insert(0, new ListItem("-- Select Priority --", "0"));

        // Populate Financial Years
        DataTable dtFinancialYears = repo.GetAllFinancialYearsLookup();
        ddlFinancialYear.DataSource = dtFinancialYears;
        ddlFinancialYear.DataTextField = "FY_Name";
        ddlFinancialYear.DataValueField = "FY_ID";
        ddlFinancialYear.DataBind();
        ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Year --", "0"));

        // Populate Quarter Dropdown
        DataTable dtQuarters = dal.GetAllQuartersLookup1();
        ddlQuarter.DataSource = dtQuarters;
        ddlQuarter.DataTextField = "QuarterText";
        ddlQuarter.DataValueField = "QuarterValue";
        ddlQuarter.DataBind();
        ddlQuarter.Items.Insert(0, new ListItem("-- Select Quarter --", "0"));
    }

    protected void rbPlanning_CheckedChanged(object sender, EventArgs e)
    {
        if (rbPlanning.Checked == true)
        {
            divQuarter.Visible = false;
            Session["selectType"] = "Planning";
        }
        else if (rbMonitoring.Checked == true)
        {
            divQuarter.Visible = true;
            Session["selectType"] = "Monitoring";
        }
    }
    protected void rbMonitoring_CheckedChanged(object sender, EventArgs e)
    {
        if (rbMonitoring.Checked == true)
        {
            divQuarter.Visible = true;
            Session["selectType"] = "Monitoring";

        }
        else if (rbMonitoring.Checked == false)
        {
            divQuarter.Visible = false;
            //Session["selectType"] = null;
        }
        lblError.ForeColor = Color.Red;
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["Cluster_ID"] != null)
        {
            if (Session["selectType"] != null)
            {
                if (ddlWorkingGroup.SelectedItem.Value != "0")
                {
                    if (ddlPriority.SelectedItem.Value != "0")
                    {
                        if (ddlFinancialYear.SelectedItem.Value != "0")
                        {                           
                            Session["ddlWorkingGroupID"] = ddlWorkingGroup.SelectedItem.Value;
                            Session["PMTDP_PriorityID"] = ddlPriority.SelectedItem.Value;
                            Session["FY_ID"] = ddlFinancialYear.SelectedItem.Value;
                            string value = Session["selectType"].ToString();// ddlQuarter.SelectedItem.Value;

                            if(value == "Monitoring")
                            {
                                if (ddlQuarter.SelectedItem.Value != "0")
                                {
                                    Session["ddlQuarterID"] = ddlQuarter.SelectedItem.Value;
                                    Response.Redirect("pageMonitoringOverview.aspx");
                                }
                            }
                            else if(value == "Planning")
                            {
                                Session.Remove("ddlQuarterID");
                                Response.Redirect("pagePlanningOverview.aspx");
                            }
                            else
                            {
                                Response.Redirect("login.aspx");
                            }
                            //Response.Redirect("PagingPlanning.aspx");                          
                        }
                    }
                }
            }
        }  
        else
        {
            //Response.Redirect("login.aspx");
        }
    }
}



















//clsMnERecord oRecord = new clsMnERecord();
//    protected void Page_Load(object sender, EventArgs e)
//    {

//        if (Session["Fullname"] != null)
//        {
//            if (IsPostBack != true)
//            {
//                PopulateDropdowns();
//                rbPlanning.Checked = true;
//                divQuarter.Visible = false;
//                string cluster = Request.QueryString["cluster"];
//                Session["Cluster"] = cluster;

//                if (cluster == "1")
//                {
//                    Session["ClusterWGName"] = "Governance, State Capacity & Institutional Development";
//                    Session["ClusterWG_ID"] = 401;
//                }
//                else if (cluster == "2")
//                {
//                    Session["ClusterWGName"] = "Social Protection, Community And Human Development";
//                    Session["ClusterWG_ID"] = 402;
//                }
//                else if (cluster == "3")
//                {
//                    Session["ClusterWGName"] = "Economic Sector Investment, Employment & Infrastructure Development";
//                    Session["ClusterWG_ID"] = 403;

//                }
//                else if (cluster == "4")
//                {
//                    Session["ClusterWGName"] = "Justice, Crime Prevention and Security";
//                    Session["ClusterWG_ID"] = 404;

//                }
//                int urlCluster = Convert.ToInt16(cluster);

//                lblError.Visible = false;

//                LoadPOAs();
//            }
//            else
//            {
//                if(Session["ClusterWG_ID"] != null)
//                {
//                    if (ddlQuarter.SelectedItem.Value == "Q1")
//                    {
//                        Session["Planned"] = "Q1-Planned-T";
//                        Session["Actual"] = "Q1-Actual";
//                        Session["Var"] = "Q1-Var";
//                        Session["Total"] = "Q1-Total";
//                        Session["YrVar"] = "Q1:Yr-Var";

//                    }
//                    else if (ddlQuarter.SelectedItem.Text == "Q2")
//                    {
//                        Session["Planned"] = "Q2 Planned-T";
//                        Session["Actual"] = "Q2-Actual";
//                        Session["Var"] = "Q2-Var";
//                        Session["Total"] = "Q2-Total";
//                        Session["YrVar"] = "Q2:Yr-Var";
//                    }
//                    else if (ddlQuarter.SelectedItem.Text == "Q3")
//                    {
//                        Session["Planned"] = "Q3 Planned-T";
//                        Session["Actual"] = "Q3-Actual";
//                        Session["Var"] = "Q3-Var";
//                        Session["Total"] = "Q3-Total";
//                        Session["YrVar"] = "Q3:Yr-Var";
//                    }
//                    else if (ddlQuarter.SelectedItem.Text == "Q4")
//                    {
//                        Session["Planned"] = "Q4 Planned-T";
//                        Session["Actual"] = "Q4-Actual";
//                        Session["Var"] = "Q4-Var";
//                        Session["Total"] = "Q4-Total";
//                        Session["YrVar"] = "Q4:Yr-Var";
//                    }
//                }
//                else
//                {
//                    Response.Redirect("login.aspx");
//                }
//            }
//        }
//        else
//        {
//            Response.Redirect("login.aspx");
//        }

//    }



//    protected void rbPlanning_CheckedChanged(object sender, EventArgs e)
//    {
//        if (rbPlanning.Checked == true)
//        {
//            divQuarter.Visible = false;
//        }
//        else if (rbMonitoring.Checked == true)
//        {
//            divQuarter.Visible = true;
//        }
//    }
//    protected void rbMonitoring_CheckedChanged(object sender, EventArgs e)
//    {
//        if (rbMonitoring.Checked == true)
//        {
//            divQuarter.Visible = true;
//        }
//        else if (rbMonitoring.Checked == false)
//        {
//            divQuarter.Visible = false;
//        }
//        //else if ((rbMonitoring.Checked == true) && (ddlQuarter.SelectedIndex == 0))
//        //{
//        //    lblError.Text = "Please select Quarter";
//        //    lblError.Visible = true;
//        //    lblError.ForeColor = Color.Red;
//        //}
//    }

//    //------------------------------------------------------------------------------------------------------------



//    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        ddlQuarter.Visible = ddlMode.SelectedValue == "Monitoring";
//    }


//    private void LoadPOAs()
//    {
//        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

//        using (SqlConnection conn = new SqlConnection(connStr))
//        using (SqlCommand cmd = new SqlCommand("new_SP_GetPOA_BySelector", conn))
//        {
//            cmd.CommandType = CommandType.StoredProcedure;

//            // Replace with actual values from dropdowns
//            //cmd.Parameters.AddWithValue("@ClusterID", ddlCluster.SelectedValue);
//            //cmd.Parameters.AddWithValue("@Mode", ddlMode.SelectedValue);
//            //cmd.Parameters.AddWithValue("@WorkingGroupID", ddlWorkingGroup.SelectedValue);
//            //cmd.Parameters.AddWithValue("@PMTDP_PriorityID", ddlPriority.SelectedValue);
//            //cmd.Parameters.AddWithValue("@FinancialYearID", ddlFinancialYear.SelectedValue);

//            cmd.Parameters.AddWithValue("@ClusterID", Session["ClusterWG_ID"]);
//            cmd.Parameters.AddWithValue("@Mode", ddlMode.SelectedValue);
//            cmd.Parameters.AddWithValue("@WorkingGroupID", ddlWorkingGroup.SelectedValue);
//            cmd.Parameters.AddWithValue("@PMTDP_PriorityID", ddlPriority.SelectedValue);
//            cmd.Parameters.AddWithValue("@FinancialYearID", ddlFinancialYear.SelectedValue);

//            if (ddlMode.SelectedValue == "Monitoring")
//                cmd.Parameters.AddWithValue("@Quarter", ddlQuarter.SelectedValue);
//            else
//                cmd.Parameters.AddWithValue("@Quarter", DBNull.Value);

//            conn.Open();
//            SqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                // Create a panel for each POA
//                Panel poaPanel = new Panel();
//                poaPanel.CssClass = "poa-panel";

//                Label lblTitle = new Label();
//                lblTitle.Text = "";//$"<h3>{reader["POA_Name"]}</h3><p>{reader["POA_Description"]}</p>";
//                poaPanel.Controls.Add(lblTitle);

//                // Add more controls for other fields as needed
//                // You can also store POA_ID and use it to fetch nested data like interventions

//                POAContainer.Controls.Add(poaPanel);
//            }

//            reader.Close();
//        }
//    }

//}