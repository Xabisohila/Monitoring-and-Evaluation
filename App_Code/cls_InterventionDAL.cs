using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class cls_InterventionDAL
{
    private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public cls_InterventionDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetAllPOAs()
    {
        return ExecuteDataTable("SELECT POA_ID, POA_Name FROM new_ProgrammesOfAction ORDER BY POA_Name");
    }

    public DataTable GetAllInstitutions()
    {
        return ExecuteDataTable("SELECT InstitutionID, InstitutionName FROM new_ImplementationInstitutions ORDER BY InstitutionName");
    }

    public DataTable GetAllWorkingGroups()
    {
        return ExecuteDataTable("SELECT WorkingGroupID, WG_Name FROM new_WorkingGroups ORDER BY WG_Name");
    }

    public DataTable GetAllMunicipalities()
    {
        return ExecuteDataTable("SELECT MunicipalityID, MunicipalityName FROM new_Municipalities ORDER BY MunicipalityName");
    }

    public DataTable GetAllSubOutcomes()
    {
        return ExecuteDataTable("SELECT SubOutcomeID, SubOutcome FROM new_SubOutcomes ORDER BY SubOutcome");
    }

    public DataRow GetInterventionById(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM new_Interventions WHERE InterventionID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }
    }

    public bool SaveIntervention(int id, int poaId, int institutionId, int wgId, int municipalityId, int subOutcomeId, string name, string description, string startYear, string endYear)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            if (id == 0)
            {
                cmd.CommandText = @"INSERT INTO new_Interventions (POA_ID, InstitutionID, WorkingGroupID, MunicipalityID, SubOutcomeID, InterventionName, Description, StartYear, EndYear)
                                    VALUES (@POA_ID, @InstitutionID, @WorkingGroupID, @MunicipalityID, @SubOutcomeID, @Name, @Description, @StartYear, @EndYear)";
            }
            else
            {
                cmd.CommandText = @"UPDATE new_Interventions SET POA_ID=@POA_ID, InstitutionID=@InstitutionID, WorkingGroupID=@WorkingGroupID,
                                    MunicipalityID=@MunicipalityID, SubOutcomeID=@SubOutcomeID, InterventionName=@Name, Description=@Description,
                                    StartYear=@StartYear, EndYear=@EndYear WHERE InterventionID=@ID";
                cmd.Parameters.AddWithValue("@ID", id);
            }

            cmd.Parameters.AddWithValue("@POA_ID", poaId);
            cmd.Parameters.AddWithValue("@InstitutionID", institutionId);
            cmd.Parameters.AddWithValue("@WorkingGroupID", wgId);
            cmd.Parameters.AddWithValue("@MunicipalityID", municipalityId);
            cmd.Parameters.AddWithValue("@SubOutcomeID", subOutcomeId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@StartYear", startYear);
            cmd.Parameters.AddWithValue("@EndYear", endYear);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteIntervention(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("DELETE FROM new_Interventions WHERE InterventionID = @ID", conn))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    private DataTable ExecuteDataTable(string query)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
