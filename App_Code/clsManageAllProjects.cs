using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// This class add all user data from the textbox to the database through a Stored Procedure
/// </summary>
public class clsManageAllProjects
{
    int mvarProjectId;
    string mvarProjectName;
    string mvarProjectSponsor;
    DateTime mvarStartDate;
    DateTime mvarEndDate;
    string mvarMainActivities;
    string mvarRisk;
    string mvarUnit;
    string mvarProjectManager;
    DateTime mvarRegistrationDate;
    string mvarEmailAddress;
    string mvarBackground;
    string mvarMunicipalRegion;
    string mvarDuration;
    decimal mvarCost;
    decimal mvarTotalAvailable;
    string mvarCurrentMTEF;
    string mvarDescription;
    string mvarIDMSGate;
    string mvarProjectStatus;

    public int ProjectId { get { return mvarProjectId; } set { mvarProjectId = value; } }
    public string ProjectName { get { return mvarProjectName; } set { mvarProjectName = value; } }
    public string ProjectSponsor { get { return mvarProjectSponsor; } set { mvarProjectSponsor = value; } }
    public DateTime StartDate { get { return mvarStartDate; } set { mvarStartDate = value; } }
    public DateTime EndDate { get { return mvarEndDate; } set { mvarEndDate = value; } }
    public string MainActivities { get { return mvarMainActivities; } set { mvarMainActivities = value; } }
    public string Risk { get { return mvarRisk; } set { mvarRisk = value; } }
    public string Unit { get { return mvarUnit; } set { mvarUnit = value; } }
    public string ProjectManager { get { return mvarProjectManager; } set { mvarProjectManager = value; } }
    public DateTime RegistrationDate { get { return mvarRegistrationDate; } set { mvarRegistrationDate = value; } }
    public string EmailAddress { get { return mvarEmailAddress; } set { mvarEmailAddress = value; } }
    public string Background { get { return mvarBackground; } set { mvarBackground = value; } }
    public string MunicipalRegion { get { return mvarMunicipalRegion; } set { mvarMunicipalRegion = value; } }
    public string Duration { get { return mvarDuration; } set { mvarDuration = value; } }
    public decimal Cost { get { return mvarCost; } set { mvarCost = value; } }
    public decimal TotalAvailable { get { return mvarTotalAvailable; } set { mvarTotalAvailable = value; } }
    public string CurrentMTEF { get { return mvarCurrentMTEF; } set { mvarCurrentMTEF = value; } }
    public string Description { get { return mvarDescription; } set { mvarDescription = value; } }
    public string IDMSGate { get { return mvarIDMSGate; } set { mvarIDMSGate = value; } }
    public string ProjectStatus { get { return mvarProjectStatus; } set { mvarProjectStatus = value; } }

    SqlConnection connection = null;
    string ProjectConn = ConfigurationManager.ConnectionStrings["ProjectConnectionString"].ConnectionString;

    public DataSet Display_B5Project_Details()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(ProjectConn);
            cmd.Connection = connection;

            cmd.CommandText = "sp_SelectB5ProjectAttributes";
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                adapter.Fill(dsData);
                return dsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        catch (Exception ex)
        {
            //LogError(ex);
            throw;
        }
    }

    public DataSet Display_Top3ProjectStage()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(ProjectConn);
            cmd.Connection = connection;

            cmd.CommandText = "sp_SelectTop3Project";
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                adapter.Fill(dsData);
                return dsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        catch (Exception ex)
        {
            //LogError(ex);
            throw;
        }
    }

    public DataSet Display_Top2ProjectStage()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(ProjectConn);
            cmd.Connection = connection;

            cmd.CommandText = "sp_SelectTop2Project";
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                adapter.Fill(dsData);
                return dsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        catch (Exception ex)
        {
            //LogError(ex);
            throw;
        }
    }

    public DataSet Display_Top3ProjectCost()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(ProjectConn);
            cmd.Connection = connection;

            cmd.CommandText = "sp_SelectTop3ProjectCost";
            cmd.CommandType = CommandType.StoredProcedure;


            DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                adapter.Fill(dsData);
                return dsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        catch (Exception ex)
        {
            //LogError(ex);
            throw;
        }
    }

    public DataSet Display_B5Project_Attributes(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(ProjectConn);
            cmd.Connection = connection;

            connection.Open();
            cmd.Parameters.Add("@B5ProjectId", SqlDbType.Int);
            cmd.Parameters["@B5ProjectId"].Value = ProjectId;

            cmd.CommandText = "sp_SelectB5Project_by_ProjectId";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Background = dr.GetValue(1).ToString();
                ProjectName = dr.GetValue(2).ToString();
                IDMSGate = dr.GetValue(3).ToString();
                MunicipalRegion = dr.GetValue(4).ToString();
                StartDate = Convert.ToDateTime(dr.GetValue(5).ToString());
                EndDate = Convert.ToDateTime(dr.GetValue(6).ToString());

                Duration = dr.GetValue(7).ToString() + " year(s)" + dr.GetValue(8).ToString() + " month(s)";
                Cost = Convert.ToDecimal(dr.GetValue(9).ToString());
                TotalAvailable = Convert.ToDecimal(dr.GetValue(10).ToString());
                CurrentMTEF = dr.GetValue(11).ToString();
                Description = dr.GetValue(12).ToString();
            }

            dr.Close();
            DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                adapter.Fill(dsData);
                return dsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        catch (Exception ex)
        {
            //LogError(ex);
            throw;
        }
    }
}

