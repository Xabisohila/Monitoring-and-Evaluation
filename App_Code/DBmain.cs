using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DBmain
/// </summary>
public class DBmain
{
    public DBmain()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlConnection Open()
    {
        var cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        var cn = new SqlConnection(cs);
        cn.Open();
        return cn;
    }
}