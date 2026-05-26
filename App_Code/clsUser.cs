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
public class clsUser
{
    string mvarPersalNumber;
    string mvarPassword;
    string mvarFirstname;
    string mvarLastname;
    int mvarTitle;
    string mvarEmailAddress;
    string mvarPhone;
    int mvarDepartment;
    int mvarDistrict;
    string mvarDesignation;
    int mvarActivation;
    int mvarUserType;
    string mvarFullname;
    string mvarAmmendedOn;

    int mvarUserID;




    string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlConnection connection = null;

    public string PersalNumber { get { return mvarPersalNumber; } set { mvarPersalNumber = value; } }
    public string Password { get { return mvarPassword; } set { mvarPassword = value; } }
    public string Firstname { get { return mvarFirstname; } set { mvarFirstname = value; } }
    public string Lastname { get { return mvarLastname; } set { mvarLastname = value; } }
    public int Title { get { return mvarTitle; } set { mvarTitle = value; } }
    public string EmailAddress { get { return mvarEmailAddress; } set { mvarEmailAddress = value; } }
    public string Phone { get { return mvarPhone; } set { mvarPhone = value; } }
    public int Department { get { return mvarDepartment; } set { mvarDepartment = value; } }
    public int District { get { return mvarDistrict; } set { mvarDistrict = value; } }
    public string Designation { get { return mvarDesignation; } set { mvarDesignation = value; } }
    public int Activation { get { return mvarActivation; } set { mvarActivation = value; } }
    public int UserType { get { return mvarUserType; } set { mvarUserType = value; } }
    public string Fullname { get { return mvarFullname; } set { mvarFullname = value; } }
    public string AmmendedOn { get { return mvarAmmendedOn; } set { mvarAmmendedOn = value; } }



    public int UserID { get { return mvarUserID; } set { mvarUserID = value; } }




    public DataSet RegisterUser()
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            connection.Open();

            cmd.CommandText = "sp_RegisterUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PersalNumber", SqlDbType.VarChar);
            cmd.Parameters["@PersalNumber"].Value = mvarPersalNumber;

            cmd.Parameters.Add("@Password", SqlDbType.VarChar);
            cmd.Parameters["@Password"].Value = mvarPassword;

            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar);
            cmd.Parameters["@FirstName"].Value = mvarFirstname;

            cmd.Parameters.Add("@LastName", SqlDbType.VarChar);
            cmd.Parameters["@LastName"].Value = mvarLastname;

            cmd.Parameters.Add("@Title", SqlDbType.Int);
            cmd.Parameters["@Title"].Value = mvarTitle;

            cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
            cmd.Parameters["@EmailAddress"].Value = mvarEmailAddress;

            cmd.Parameters.Add("@TelephoneNumber", SqlDbType.VarChar);
            cmd.Parameters["@TelephoneNumber"].Value = mvarPhone;

            cmd.Parameters.Add("@Department", SqlDbType.Int);
            cmd.Parameters["@Department"].Value = mvarDepartment;

            cmd.Parameters.Add("@District", SqlDbType.Int);
            cmd.Parameters["@District"].Value = mvarDistrict;

            cmd.Parameters.Add("@UserType", SqlDbType.Int);
            cmd.Parameters["@UserType"].Value = mvarUserType;

            cmd.Parameters.Add("@Designation", SqlDbType.VarChar);
            cmd.Parameters["@Designation"].Value = mvarDesignation;

            cmd.Parameters.Add("@Activation", SqlDbType.Int);
            cmd.Parameters["@Activation"].Value = mvarActivation;

            cmd.Parameters.Add("@RegistrationDate", SqlDbType.DateTime);
            cmd.Parameters["@RegistrationDate"].Value = DateTime.Now;



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

    public DataSet PopulateOptions(string OptionType)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            connection = new SqlConnection(conn);
            cmd.Connection = connection;

            cmd.Parameters.Add("@OptionType", SqlDbType.VarChar);
            cmd.Parameters["@OptionType"].Value = OptionType;

            cmd.CommandText = "Select_Options";
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

    public DataSet LoginDetails(int postPersalNumber)
    {
        try
        {
            // Insert the User and details into db
            connection = new SqlConnection(conn);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_LoginDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add Parameters to Command Parameters collection

            cmd.Parameters.Add("@PersalNumber", SqlDbType.Int);
            cmd.Parameters["@PersalNumber"].Value = postPersalNumber;


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                PersalNumber = dr.GetValue(0).ToString();
                Password = dr.GetValue(1).ToString();
                Activation = Convert.ToInt32(dr.GetValue(2).ToString());
                UserType = Convert.ToInt32(dr.GetValue(3).ToString());
                Fullname = dr.GetValue(4).ToString();
                EmailAddress = dr.GetValue(5).ToString();
                Department = Convert.ToInt16(dr.GetValue(6).ToString());
                AmmendedOn = dr.GetValue(7).ToString();

                UserID = Convert.ToInt32(dr.GetValue(8).ToString());
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

