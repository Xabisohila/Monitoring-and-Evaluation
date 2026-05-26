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
public class clsActivity
{
    int mvarResource1;
    string mvarActivity1;
    DateTime mvarStartDate1;
    DateTime mvarEndDate1;

    int mvarResource2;
    string mvarActivity2;
    DateTime mvarStartDate2;
    DateTime mvarEndDate2;

    int mvarResource3;
    string mvarActivity3;
    DateTime mvarStartDate3;
    DateTime mvarEndDate3;

    int mvarResource4;
    string mvarActivity4;
    DateTime mvarStartDate4;
    DateTime mvarEndDate4;

    int mvarResource5;
    string mvarActivity5;
    DateTime mvarStartDate5;
    DateTime mvarEndDate5;

    int mvarProjectId;
    DateTime mvarRegistrationDate;
    int mvarActivityId;
    float mvarActivityStatus;
    int mvarActivityProjectId;

    int mvarTaskId;
    float mvarTaskStatus;

    string mvarTask;

    string mvarTask1;

    string mvarActivityName;


    string conn = ConfigurationManager.ConnectionStrings["ProjectConnectionString"].ConnectionString;
    SqlConnection connection = null;

    public int Resource1 { get { return mvarResource1; } set { mvarResource1 = value; } }
    public string Activity1 { get { return mvarActivity1; } set { mvarActivity1 = value; } }
    public DateTime StartDate1 { get { return mvarStartDate1; } set { mvarStartDate1 = value; } }
    public DateTime EndDate1 { get { return mvarEndDate1; } set { mvarEndDate1 = value; } }

    public int Resource2 { get { return mvarResource2; } set { mvarResource2 = value; } }
    public string Activity2 { get { return mvarActivity2; } set { mvarActivity2 = value; } }
    public DateTime StartDate2 { get { return mvarStartDate2; } set { mvarStartDate2 = value; } }
    public DateTime EndDate2 { get { return mvarEndDate2; } set { mvarEndDate2 = value; } }

    public int Resource3 { get { return mvarResource3; } set { mvarResource3 = value; } }
    public string Activity3 { get { return mvarActivity3; } set { mvarActivity3 = value; } }
    public DateTime StartDate3 { get { return mvarStartDate3; } set { mvarStartDate3 = value; } }
    public DateTime EndDate3 { get { return mvarEndDate3; } set { mvarEndDate3 = value; } }

    public int Resource4 { get { return mvarResource4; } set { mvarResource4 = value; } }
    public string Activity4 { get { return mvarActivity4; } set { mvarActivity4 = value; } }
    public DateTime StartDate4 { get { return mvarStartDate4; } set { mvarStartDate4 = value; } }
    public DateTime EndDate4 { get { return mvarEndDate4; } set { mvarEndDate4 = value; } }

    public int Resource5 { get { return mvarResource5; } set { mvarResource5 = value; } }
    public string Activity5 { get { return mvarActivity5; } set { mvarActivity5 = value; } }
    public DateTime StartDate5 { get { return mvarStartDate5; } set { mvarStartDate5 = value; } }
    public DateTime EndDate5 { get { return mvarEndDate5; } set { mvarEndDate5 = value; } }
    public int ProjectId { get { return mvarProjectId; } set { mvarProjectId = value; } }

    public DateTime RegistrationDate { get { return mvarRegistrationDate; } set { mvarRegistrationDate = value; } }
    public int ActivityId { get { return mvarActivityId; } set { mvarActivityId = value; } }
    public float ActivityStatus { get { return mvarActivityStatus; } set { mvarActivityStatus = value; } }
    public int ActivityProjectId { get { return mvarActivityProjectId; } set { mvarActivityProjectId = value; } }

    public string Task { get { return mvarTask; } set { mvarTask = value; } }
    public int TaskId { get { return mvarTaskId; } set { mvarTaskId = value; } }
    public float TaskStatus { get { return mvarTaskStatus; } set { mvarTaskStatus = value; } }
    public string Task1 { get { return mvarTask1; } set { mvarTask1 = value; } }


    public string ActivityName { get { return mvarActivityName; } set { mvarActivityName = value; } }

    public void AddActivity1()
    { 
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddActivity1", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection

        cmd.Parameters.Add("@Resource1", SqlDbType.Int);
        cmd.Parameters.Add("@Activity1", SqlDbType.VarChar);
        cmd.Parameters.Add("@StartDate1", SqlDbType.DateTime);
        cmd.Parameters.Add("@EndDate1", SqlDbType.DateTime);

        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);

        cmd.Parameters["@Resource1"].Value = mvarResource1;
        cmd.Parameters["@Activity1"].Value = mvarActivity1;
        cmd.Parameters["@StartDate1"].Value = mvarStartDate1;
        cmd.Parameters["@EndDate1"].Value = mvarEndDate1;

        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;

        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
      
    }

    public void AddActivity2()
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddActivity2", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@Resource2", SqlDbType.Int);
        cmd.Parameters.Add("@Activity2", SqlDbType.VarChar);
        cmd.Parameters.Add("@StartDate2", SqlDbType.DateTime);
        cmd.Parameters.Add("@EndDate2", SqlDbType.DateTime);
        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);

        cmd.Parameters["@Resource2"].Value = mvarResource2;
        cmd.Parameters["@Activity2"].Value = mvarActivity2;
        cmd.Parameters["@StartDate2"].Value = mvarStartDate2;
        cmd.Parameters["@EndDate2"].Value = mvarEndDate2;
        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;
        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
       

    }

    public void AddActivity3()
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddActivity3", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@Resource3", SqlDbType.Int);
        cmd.Parameters.Add("@Activity3", SqlDbType.VarChar);
        cmd.Parameters.Add("@StartDate3", SqlDbType.DateTime);
        cmd.Parameters.Add("@EndDate3", SqlDbType.DateTime);
        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);

        cmd.Parameters["@Resource3"].Value = mvarResource3;
        cmd.Parameters["@Activity3"].Value = mvarActivity3;
        cmd.Parameters["@StartDate3"].Value = mvarStartDate3;
        cmd.Parameters["@EndDate3"].Value = mvarEndDate3;
        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;
        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
      
    }

    public void AddActivity4()
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddActivity4", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@Resource4", SqlDbType.Int);
        cmd.Parameters.Add("@Activity4", SqlDbType.VarChar);
        cmd.Parameters.Add("@StartDate4", SqlDbType.DateTime);
        cmd.Parameters.Add("@EndDate4", SqlDbType.DateTime);
        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);

        cmd.Parameters["@Resource4"].Value = mvarResource4;
        cmd.Parameters["@Activity4"].Value = mvarActivity4;
        cmd.Parameters["@StartDate4"].Value = mvarStartDate4;
        cmd.Parameters["@EndDate4"].Value = mvarEndDate4;
        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;
        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
        
    }

    public void AddActivity5()
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddActivity5", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@Resource5", SqlDbType.Int);
        cmd.Parameters.Add("@Activity5", SqlDbType.VarChar);
        cmd.Parameters.Add("@StartDate5", SqlDbType.DateTime);
        cmd.Parameters.Add("@EndDate5", SqlDbType.DateTime);
        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);


        cmd.Parameters["@Resource5"].Value = mvarResource5;
        cmd.Parameters["@Activity5"].Value = mvarActivity5;
        cmd.Parameters["@StartDate5"].Value = mvarStartDate5;
        cmd.Parameters["@EndDate5"].Value = mvarEndDate5;
        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;
        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
        
    }
    public DataSet Display_Project_Resources()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_SelectResource";
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

    public DataSet DisplayActivity(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
            cmd.Parameters["@ProjectId"].Value = ProjectId;

            cmd.CommandText = "sp_SelectActivity";
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
    
    public DataSet DisplayTask(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = ActivityId;

            cmd.CommandText = "sp_SelectTask";
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

    public DataSet UpdateActivityStatus(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = mvarActivityId;

            cmd.Parameters.Add("@ActivityStatus", SqlDbType.Float);
            cmd.Parameters["@ActivityStatus"].Value = mvarActivityStatus;

            cmd.CommandText = "sp_UpdateActivityStatus";
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

    public DataSet DisplayB5ProjectEmployment(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@B5ProjectId", SqlDbType.Int);
            cmd.Parameters["@B5ProjectId"].Value = ProjectId;

            cmd.CommandText = "sp_SelectB5Project_Employment";
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

    public DataSet UpdateTaskStatus(int TaskId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@TaskId", SqlDbType.Int);
            cmd.Parameters["@TaskId"].Value = mvarTaskId;

            cmd.Parameters.Add("@TaskStatus", SqlDbType.Float);
            cmd.Parameters["@TaskStatus"].Value = TaskStatus;

            cmd.CommandText = "sp_UpdateTaskStatus";
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

    public int DisplayTotalActivities(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
            cmd.Parameters["@ProjectId"].Value = ProjectId;

            cmd.CommandText = "sp_SelectTotalActivity";
            cmd.CommandType = CommandType.StoredProcedure;


            int dsData = new Int32();
            int totalColumns = Convert.ToInt32(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
               return  (int)cmd.ExecuteScalar();

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

    public int DisplayTotalTasks(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = mvarActivityId;

            cmd.CommandText = "sp_SelectTotalTasks";
            cmd.CommandType = CommandType.StoredProcedure;


            int dsData = new Int32();
            int totalColumns = Convert.ToInt32(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                return (int)cmd.ExecuteScalar();

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

    public DataSet UpdateTask(int TaskId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@TaskId", SqlDbType.Int);
            cmd.Parameters["@TaskId"].Value = mvarTaskId;

            cmd.Parameters.Add("@Task", SqlDbType.VarChar);
            cmd.Parameters["@Task"].Value = mvarTask;

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = mvarActivityId;

            cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
            cmd.Parameters["@ProjectId"].Value = mvarProjectId;

            cmd.CommandText = "sp_UpdateTask";
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

    public double GetActivityStatus(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
            cmd.Parameters["@ProjectId"].Value = ProjectId;

            cmd.CommandText = "sp_SelectActivityStatus";
            cmd.CommandType = CommandType.StoredProcedure;

            //double dsData = new Double();
            //double totalColumns = Convert.ToDouble(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;



            try
            {
                return (double)cmd.ExecuteScalar();

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

    public int GetTaskStatus(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = ActivityId;

            cmd.CommandText = "sp_SelectTaskStatus";
            cmd.CommandType = CommandType.StoredProcedure;

            //double dsData = new Double();
            //double totalColumns = Convert.ToDouble(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                return (int)cmd.ExecuteScalar();
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
            throw;
        }
    }

    public int DeleteTask(int TaskId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@TaskId", SqlDbType.Int);
            cmd.Parameters["@TaskId"].Value = TaskId;

            cmd.CommandText = "sp_DeleteTask";
            cmd.CommandType = CommandType.StoredProcedure;

            //double dsData = new Double();
            //double totalColumns = Convert.ToDouble(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                return (int)cmd.ExecuteScalar();
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
            throw;
        }
    }


    public int ActivityExist(int ProjectId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
            cmd.Parameters["@ProjectId"].Value = ProjectId;


            cmd.CommandText = "sp_IsActivityExist";
            cmd.CommandType = CommandType.StoredProcedure;

            int dsData = new Int32();
            int totalColumns = Convert.ToInt32(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
               return  (int)cmd.ExecuteScalar();

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


    public int TaskExist(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = ActivityId;


            cmd.CommandText = "sp_IsTaskExist";
            cmd.CommandType = CommandType.StoredProcedure;

            int dsData = new Int32();
            int totalColumns = Convert.ToInt32(dsData);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            try
            {
                return (int)cmd.ExecuteScalar();

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

    public DataSet OpenActivityStatus(int ActivityId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = mvarActivityId;


            cmd.CommandText = "sp_OpenActivityStatus";
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

    public DataSet OpenTaskStatus(int TaskId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.Parameters.Add("@TaskId", SqlDbType.Int);
            cmd.Parameters["@TaskId"].Value = TaskId;

            cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
            cmd.Parameters["@ActivityId"].Value = ActivityId;


            cmd.CommandText = "sp_OpenTaskStatus";
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


    public void AddTask()
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_AddTask", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
        cmd.Parameters.Add("@Task", SqlDbType.VarChar);
        cmd.Parameters.Add("@ProjectId", SqlDbType.Int);
        cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);


        cmd.Parameters["@ActivityId"].Value = mvarActivityId;
        cmd.Parameters["@Task"].Value = mvarTask;
        cmd.Parameters["@ProjectId"].Value = mvarProjectId;
        cmd.Parameters["@RegistrationDate"].Value = mvarRegistrationDate;
        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }


    }

    public void EditTask(int TaskId)
    {
        // Insert the Activity and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_editTask", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection
        cmd.Parameters.Add("@TaskId", SqlDbType.Int);
        cmd.Parameters.Add("@ActivityId", SqlDbType.Int);
        cmd.Parameters.Add("@Task", SqlDbType.VarChar);

        cmd.Parameters["@TaskId"].Value = TaskId;
        cmd.Parameters["@ActivityId"].Value = mvarActivityId;
        cmd.Parameters["@Task"].Value = Task;

        try
        {
            //conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }


    }

    public DataSet DisplayTask_Details(int TaskId)
    {
        // Insert the User and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_SelectTask_by_TaskId", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Command Parameters collection

        cmd.Parameters.Add("@TaskId", SqlDbType.Int);
        cmd.Parameters["@TaskId"].Value = TaskId;

           DataSet dsData = new DataSet("Projects");
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataReader dr = cmd.ExecuteReader();
            adapter.SelectCommand = cmd;

            try
            { 
                if (dr.Read())
            {
                ActivityName = dr.GetValue(0).ToString();
                Task = dr.GetValue(1).ToString();

            }
                dr.Close();
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
 }