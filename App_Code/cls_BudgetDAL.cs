using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for cls_BudgetDAL
/// </summary>
public class cls_BudgetDAL
{
    private string connectionString;
    public cls_BudgetDAL()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    protected object ExecuteScalar(string spName, params SqlParameter[] parameters) // S_R55
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure; // S_R88, S_R75
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected object ExecuteScalar(string queryOrSpName, CommandType commandType, params SqlParameter[] parameters) // S_R75, S_R88
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    protected DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] parameters)
    {
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }
        }

        return ds;
    }

    protected DataTable ExecuteDataTable(string queryOrSpName, params SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(queryOrSpName, conn))
        {
            cmd.CommandType = CommandType.Text; // Change to StoredProcedure if needed
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }


    /// <summary>
    /// Creates a new Intervention Budget record.
    /// </summary>
    /// <returns>The ID of the newly created Budget, or 0 if creation failed.</returns>
    public int CreateInterventionBudget(
        int interventionId,
        int fyId,
        decimal annualBudget,
        decimal? termBudget) // Nullable for optional term budget
    {
        // Corrected: Use array initialization syntax for SqlParameter
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@InterventionID", interventionId),
            new SqlParameter("@FY_ID", fyId),
            new SqlParameter("@AnnualBudget", annualBudget),
            new SqlParameter("@TermBudget", (object)termBudget?? DBNull.Value) // Handle nullable to DBNull.Value
        };

        object result = ExecuteScalar("new_SP_CreateInterventionBudget", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }

    // --- Lookup Methods for Dropdowns ---
    public DataTable GetAllInterventionsLookup()
    {
        return ExecuteDataTable("SELECT InterventionID, InterventionName FROM new_Interventions ORDER BY InterventionName");
    }

    public DataTable GetAllFinancialYearsLookup()
    {
        return ExecuteDataTable("SELECT FY_ID, FY_Name FROM new_FinancialYears ORDER BY FY_Name DESC");
    }













    /// <summary>
    /// Updates an existing Intervention Budget record.
    /// </summary>
    /// <returns>The ID of the updated Budget, or 0 if update failed.</returns>
    public int UpdateInterventionBudget(
        int budgetId,
        int interventionId,
        int fyId,
        decimal annualBudget,
        decimal? termBudget) // Nullable for optional term budget
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@BudgetID", budgetId),
        new SqlParameter("@InterventionID", interventionId),
        new SqlParameter("@FY_ID", fyId),
        new SqlParameter("@AnnualBudget", annualBudget),
        new SqlParameter("@TermBudget", (object)termBudget ?? DBNull.Value)
        };

        object result = ExecuteScalar("new_SP_UpdateInterventionBudget", parameters);
        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
    }


    //public DataTable GetBudgetById1(int budgetId)
    //{
    //    string query = @"
    //    SELECT BudgetID, InterventionID, FY_ID, AnnualBudget, TermBudget
    //    FROM Intervention_Budgets
    //    WHERE BudgetID = @BudgetID";

    //    SqlParameter[] parameters = new SqlParameter[]
    //    {
    //    new SqlParameter("@BudgetID", budgetId)
    //    };

    //    return ExecuteDataTable(query, CommandType.Text, parameters);
    //}

    public DataSet GetBudgetById(int budgetId)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@BudgetID", budgetId)
        };
        // SP_GetInterventionDetails returns multiple result sets (4 in total now)
        return ExecuteDataSet("new_SP_GetBudgetById", parameters);
    }


}
