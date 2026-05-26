//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for cls_PlanningOverviewRepository_3
///// </summary>
//public class cls_PlanningOverviewRepository_3
//{
//    public cls_PlanningOverviewRepository_3()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class cls_PlanningOverviewRepository_3
{
    private string connectionString;

    public cls_PlanningOverviewRepository_3()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    public DataSet GetPlanningOverviewBySubOutcome(int workGroupId, int pmtdpPriorityId, int financialYearId, int clusterId)
    {
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("new_SP_GetPlanningOverviewByFilters_5", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@WorkGroupID", workGroupId);
            cmd.Parameters.AddWithValue("@PMTDP_PriorityID", pmtdpPriorityId);
            cmd.Parameters.AddWithValue("@FinancialYearID", financialYearId);
            cmd.Parameters.AddWithValue("@ClusterID", clusterId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }

        // Optional: Name the tables for easier reference
        if (ds.Tables.Count > 0) ds.Tables[0].TableName = "PriorityHeader";
        if (ds.Tables.Count > 1) ds.Tables[1].TableName = "SubOutcomes";
        if (ds.Tables.Count > 2) ds.Tables[2].TableName = "Interventions";
        if (ds.Tables.Count > 3) ds.Tables[3].TableName = "Indicators";
        if (ds.Tables.Count > 4) ds.Tables[4].TableName = "Budgets";

        return ds;
    }
}
