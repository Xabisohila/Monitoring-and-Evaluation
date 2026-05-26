using System;
using System.Configuration;
using System.Data.SqlClient;

public class dbHelper
{
    private readonly string connectionString;

    public dbHelper()
    {
        try
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        catch (Exception ex)
        {
            // Log error (placeholder)
            throw new Exception("Error retrieving connection string: " + ex.Message);
        }
    }

    /// <summary>
    /// Returns a new SqlConnection object.
    /// </summary>
    public SqlConnection GetConnection()
    {
        try
        {
            return new SqlConnection(connectionString);
        }
        catch (Exception ex)
        {
            // Log error (placeholder)
            throw new Exception("Error creating SQL connection: " + ex.Message);
        }
    }

    /// <summary>
    /// Executes a non-query stored procedure with parameters.
    /// </summary>
    public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log error (placeholder)
                    throw new Exception("Error executing stored procedure: " + ex.Message);
                }
            }
        }
    }
}
