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
public class clsMnERecord
{
    int mvarCluster;
    string mvarBaseline;
    string mvarAnnualTarget;
    string mvarQ1Planning;
    string mvarQ2Planning;
    string mvarQ3Planning;
    string mvarQ4Planning;
    string mvarTotal;
    string mvarQuarter;
    int mvarFinancialYear;
    int mvarKeyResultId;
    int mvarInterventionId;
    string mvarIntervention;
    int mvarSubOutcome;
    string mvarSubOutcomeInfo;
    int mvarStrategicPriority;
    string mvarKeyResults;
    string mvarKeyIndicator;
    string mvarResponsibleInstitution;

    int mvarKeyIndicatorId;
    string mvarFilePath;
    string mvarFileName;



    string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlConnection connection = null;

    public int ClusterName { get { return mvarCluster; } set { mvarCluster = value; } }
    public string Baseline { get { return mvarBaseline; } set { mvarBaseline = value; } }
    public string AnnualTarget { get { return mvarAnnualTarget; } set { mvarAnnualTarget = value; } }
    public string Q1Planning { get { return mvarQ1Planning; } set { mvarQ1Planning = value; } }
    public string Q2Planning { get { return mvarQ2Planning; } set { mvarQ2Planning = value; } }
    public string Q3Planning { get { return mvarQ3Planning; } set { mvarQ3Planning = value; } }
    public string Q4Planning { get { return mvarQ4Planning; } set { mvarQ4Planning = value; } }
    public string Total { get { return mvarTotal; } set { mvarTotal = value; } }
    public string Quarter { get { return mvarQuarter; } set { mvarQuarter = value; } }
    public int FinancialYear { get { return mvarFinancialYear; } set { mvarFinancialYear = value; } }
    public int KeyResultId { get { return mvarKeyResultId; } set { mvarKeyResultId = value; } }

    public int InterventionId { get { return mvarInterventionId; } set { mvarInterventionId = value; } }
    public int SubOutcome { get { return mvarSubOutcome; } set { mvarSubOutcome = value; } }
    public int StrategicPriority { get { return mvarStrategicPriority; } set { mvarStrategicPriority = value; } }
    public int Cluster { get { return mvarCluster; } set { mvarCluster = value; } }
    public string KeyResults { get { return mvarKeyResults; } set { mvarKeyResults = value; } }
    public string KeyIndicator { get { return mvarKeyIndicator; } set { mvarKeyIndicator = value; } }
    public string ResponsibleInstitution { get { return mvarResponsibleInstitution; } set { mvarResponsibleInstitution = value; } }
    public string Intervention { get { return mvarIntervention; } set { mvarIntervention = value; } }
    public string SuboutcomeInfo { get { return mvarSubOutcomeInfo; } set { mvarSubOutcomeInfo = value; } }

    public int KeyIndicatorId { get { return mvarKeyIndicatorId; } set { mvarKeyIndicatorId = value; } }
    public string FilePath { get { return mvarFilePath; } set { mvarFilePath = value; } }
    public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }


    public DataSet Select_Category()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_category";
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

    public DataSet SubmitPlanningData(int KeyResultId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_SubmitPlanningData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = KeyResultId;

            cmd.Parameters.Add("@Baseline", SqlDbType.VarChar);
            cmd.Parameters["@Baseline"].Value = mvarBaseline;

            cmd.Parameters.Add("@AnnualTarget", SqlDbType.VarChar);
            cmd.Parameters["@AnnualTarget"].Value = mvarAnnualTarget;

            cmd.Parameters.Add("@Q1Planning", SqlDbType.VarChar);
            cmd.Parameters["@Q1Planning"].Value = mvarQ1Planning;

            cmd.Parameters.Add("@Q2Planning", SqlDbType.VarChar);
            cmd.Parameters["@Q2Planning"].Value = mvarQ2Planning;

            cmd.Parameters.Add("@Q3Planning", SqlDbType.VarChar);
            cmd.Parameters["@Q3Planning"].Value = mvarQ3Planning;

            cmd.Parameters.Add("@Q4Planning", SqlDbType.VarChar);
            cmd.Parameters["@Q4Planning"].Value = mvarQ4Planning;

            cmd.Parameters.Add("@Total", SqlDbType.VarChar);
            cmd.Parameters["@Total"].Value = mvarTotal;

            cmd.Parameters.Add("@FinancialYear", SqlDbType.Int);
            cmd.Parameters["@FinancialYear"].Value = mvarFinancialYear;



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

    public DataSet SubmitInterventionData()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_InterventionData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InterventionId", SqlDbType.Int);
            cmd.Parameters["@InterventionId"].Value = mvarInterventionId;

            cmd.Parameters.Add("@SubOutcome", SqlDbType.Int);
            cmd.Parameters["@SubOutcome"].Value = mvarSubOutcome;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = mvarStrategicPriority;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = mvarCluster;

            cmd.Parameters.Add("@KeyResults", SqlDbType.VarChar);
            cmd.Parameters["@KeyResults"].Value = mvarKeyResults;

            cmd.Parameters.Add("@KeyIndicator", SqlDbType.VarChar);
            cmd.Parameters["@KeyIndicator"].Value = mvarKeyIndicator;

            cmd.Parameters.Add("@ResponsibleInstitution", SqlDbType.VarChar);
            cmd.Parameters["@ResponsibleInstitution"].Value = mvarResponsibleInstitution;




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

    public DataSet SubmitEvidence()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_SubmitEvidence";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = mvarKeyResultId;

            cmd.Parameters.Add("@FileName", SqlDbType.VarChar);
            cmd.Parameters["@FileName"].Value = mvarFileName;

            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar);
            cmd.Parameters["@FilePath"].Value =  mvarFilePath;

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

    public DataSet SelectEvidence(int KeyResultId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_Evidence";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = KeyResultId;

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
    public DataSet SelectPlanningScores(int KeyResultId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_PlanningScores";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = KeyResultId;

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


    public DataSet Select_FinancialYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_financialYear";
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

    public DataSet Select_Quarter()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_quarter";
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

    public DataSet Select_StrategicPriority(int Cluster)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_strategicPriority";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;


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

    public DataSet Select_WorkingGroup(int Cluster)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_WorkingGroup";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;


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


    public DataSet Select_StrategicPriority_Parent()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_strategicPriority_Parent";
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

    public DataSet Select_Intervention(int StrategicPriority)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_intervention";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Suboutcome", SqlDbType.Int);
            cmd.Parameters["@Suboutcome"].Value = StrategicPriority;

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

    public DataSet Select_Suboutcome(int Cluster)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_suboutcome";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

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

    public DataSet Select_Indicator(int Intervention)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_indicator";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Intervention", SqlDbType.Int);
            cmd.Parameters["@Intervention"].Value = Intervention;

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
    public DataSet Select_Suboutcome_byMTFS(int Cluster, int StrategicPriority)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_suboutcome_byMTSF";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;

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

    public DataSet Select_Cluster()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_cluster";
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


    public DataSet Select_PlanningData(int Cluster, int StrategicOutcome, int StrategicPriority)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_planningData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@StrategicOutcome", SqlDbType.Int);
            cmd.Parameters["@StrategicOutcome"].Value = StrategicOutcome;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;
            

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
    public DataSet Select_PlanningData_New()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_planningData_New";
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            //cmd.Parameters["@Cluster"].Value = Cluster;

            //cmd.Parameters.Add("@StrategicOutcome", SqlDbType.Int);
            //cmd.Parameters["@StrategicOutcome"].Value = StrategicOutcome;

            //cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            //cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;


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

    public DataSet Select_MonitoringData(int Cluster, int FocusArea, int StrategicPriority, int FinancialYear, string Quarter)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_monitoringData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@FocusArea", SqlDbType.Int);
            cmd.Parameters["@FocusArea"].Value = FocusArea;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;

            cmd.Parameters.Add("@FinancialYear", SqlDbType.Int);
            cmd.Parameters["@FinancialYear"].Value = FinancialYear;

            cmd.Parameters.Add("@Quarter", SqlDbType.VarChar);
            cmd.Parameters["@Quarter"].Value = Quarter;


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

    public DataSet Select_SomeMonitoringData(int Cluster, int FocusArea, int StrategicPriority)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_some_monitoringData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@FocusArea", SqlDbType.Int);
            cmd.Parameters["@FocusArea"].Value = FocusArea;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;




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

    public DataSet AddSubOutcome(int Cluster,  int StrategicPriority, string Suboutcome)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_submit_SubOutcome";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;

            cmd.Parameters.Add("@Suboutcome", SqlDbType.VarChar);
            cmd.Parameters["@Suboutcome"].Value = Suboutcome;

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

    public DataSet AddIntervention( int Suboutcome, string Intervention)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_submit_Intervention";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Suboutcome", SqlDbType.Int);
            cmd.Parameters["@Suboutcome"].Value = Suboutcome;

            cmd.Parameters.Add("@Intervention", SqlDbType.VarChar);
            cmd.Parameters["@Intervention"].Value = Intervention;



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

    //==================================================================================================


    public DataSet Get_PlanningData1(int Cluster, int StrategicOutcome, int StrategicPriority, int keyResultId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "select_planningData";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@StrategicOutcome", SqlDbType.Int);
            cmd.Parameters["@StrategicOutcome"].Value = StrategicOutcome;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = keyResultId;


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


    public DataSet Get_PlanningData(int Cluster, int StrategicOutcome, int StrategicPriority, int keyResultId)
    {
        try
        {

            connection = new SqlConnection(conn);
            connection.Open();
            SqlCommand cmd = new SqlCommand("get_planningData_keyResultId", connection);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@Cluster", SqlDbType.Int);
            cmd.Parameters["@Cluster"].Value = Cluster;

            cmd.Parameters.Add("@StrategicOutcome", SqlDbType.Int);
            cmd.Parameters["@StrategicOutcome"].Value = StrategicOutcome;

            cmd.Parameters.Add("@StrategicPriority", SqlDbType.Int);
            cmd.Parameters["@StrategicPriority"].Value = StrategicPriority;

            cmd.Parameters.Add("@KeyResultId", SqlDbType.Int);
            cmd.Parameters["@KeyResultId"].Value = keyResultId;

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                SuboutcomeInfo = dr.GetValue(0).ToString();
                KeyResultId = Convert.ToInt32(dr.GetValue(1).ToString());
                KeyIndicator = dr.GetValue(2).ToString();
                KeyResults = dr.GetValue(3).ToString();
                Intervention = dr.GetValue(4).ToString();
                Baseline = dr.GetValue(5).ToString();
                AnnualTarget = dr.GetValue(6).ToString();
                Q1Planning = dr.GetValue(7).ToString();
                Q2Planning = dr.GetValue(8).ToString();
                Q3Planning = dr.GetValue(9).ToString();
                Q4Planning = dr.GetValue(10).ToString();
                Total = dr.GetValue(11).ToString();
                ResponsibleInstitution = dr.GetValue(12).ToString();
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


    //==================================================================================================
}

