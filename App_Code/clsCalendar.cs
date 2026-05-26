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
/// Summary description for clsCalendar
/// </summary>
public class clsCalendar
{
    DateTime mvarDateFrom;
    DateTime mvarDateTo;
    string mvarEvent;
    string mvarVenue;
    string mvarOffice;
    string mvarContactPerson;
    string mvarContactNumber;

    string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlConnection connection = null;

    public DateTime DateFrom { get { return mvarDateFrom; } set { mvarDateFrom = value; } }
    public DateTime DateTo { get { return mvarDateTo; } set { mvarDateTo = value; } }
    public string Event { get { return mvarEvent; } set { mvarEvent = value; } }
    public string Venue { get { return mvarVenue; } set { mvarVenue = value; } }
    public string Office { get { return mvarOffice; } set { mvarOffice = value; } }
    public string ContactPerson { get { return mvarContactPerson; } set { mvarContactPerson = value; } }
    public string ContactNumber { get { return mvarContactNumber; } set { mvarContactNumber = value; } }

    //public void Calendar()
    //{
    //    // Insert the User and details into db
    //    connection = new SqlConnection(conn);
    //    connection.Open();
    //    SqlCommand cmd = new SqlCommand("sp_InsertSchools", connection);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    // Add Parameters to Command Parameters collection
    //    cmd.Parameters.Add("@No", SqlDbType.Int);
    //    cmd.Parameters.Add("@Name", SqlDbType.VarChar);
    //    cmd.Parameters.Add("@Status", SqlDbType.VarChar);
    //    cmd.Parameters.Add("@Phase", SqlDbType.VarChar);
    //    cmd.Parameters.Add("@Sector", SqlDbType.VarChar);

    //    cmd.Parameters["@No"].Value = mvarNo;
    //    cmd.Parameters["@Name"].Value = mvarName;
    //    cmd.Parameters["@Status"].Value = mvarStatus;
    //    cmd.Parameters["@Phase"].Value = mvarPhase;
    //    cmd.Parameters["@Sector"].Value = mvarSector;

    //    try
    //    {
    //        //conn.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();
    //        while (reader.Read())
    //        {
    //            Console.WriteLine("{0} - {1:c}", reader.GetDateTime(0), reader.GetDecimal(2));
    //        }
    //        reader.Close();
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //}

    public DataSet PopulateCalendarSelectedDate(DateTime DateFrom, DateTime DateTo)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime);
            cmd.Parameters["@DateFrom"].Value = DateFrom;

            cmd.Parameters.Add("@DateTo", SqlDbType.DateTime);
            cmd.Parameters["@DateTo"].Value = DateTo;

            cmd.CommandText = "sp_Display_Calendar_Selected_Date";
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

    public DataSet PopulateCalendarActiveDate(DateTime ActiveDate)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            cmd.Parameters.Add("@ActiveDate", SqlDbType.DateTime);
            cmd.Parameters["@ActiveDate"].Value = ActiveDate;

            cmd.CommandText = "sp_Display_Calendar_Active_Date";
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

    public DataSet PopulateCalendar()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;


            cmd.CommandText = "sp_Display_Calendar";
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

    public DataSet PopulateCalendarEvent()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;


            cmd.CommandText = "sp_Display_CalendarEvents";
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

   

    public DataSet Delete_Calendar(int CalendarId)
    {
        // Insert the User and details into db
        connection = new SqlConnection(conn);
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_Delete_Calendar", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@CalendarId", SqlDbType.Int);
        cmd.Parameters["@CalendarId"].Value = CalendarId;

        DataSet dsData = new DataSet("Projects");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;

        try
        {
            adapter.Fill(dsData);
            return dsData;
        }
        finally
        {
            connection.Close();
        }
    }
}
