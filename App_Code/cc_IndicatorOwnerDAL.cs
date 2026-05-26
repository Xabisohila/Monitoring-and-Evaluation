using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_IndicatorOwnerDAL
/// </summary>
public class cc_IndicatorOwnerDAL
{
    public int Upsert(cc_IndicatorOwner model)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorOwner_Upsert", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OwnerID", model.OwnerID == 0 ? (object)DBNull.Value : model.OwnerID);
            cmd.Parameters.AddWithValue("@IndicatorID", model.IndicatorID);
            cmd.Parameters.AddWithValue("@InstitutionID", model.InstitutionID);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public cc_IndicatorOwner GetByID(int ownerID)
    {
        cc_IndicatorOwner result = null;
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorOwner_GetByID", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OwnerID", ownerID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    result = new cc_IndicatorOwner
                    {
                        OwnerID = Convert.ToInt32(dr["OwnerID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        InstitutionID = Convert.ToInt32(dr["InstitutionID"])
                    };
                }
            }
        }
        return result;
    }

    public List<cc_IndicatorOwner> ListAll()
    {
        var list = new List<cc_IndicatorOwner>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorOwner_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorOwner
                    {
                        OwnerID = Convert.ToInt32(dr["OwnerID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        InstitutionID = Convert.ToInt32(dr["InstitutionID"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_IndicatorOwner> ListByIndicator(int indicatorID)
    {
        var list = new List<cc_IndicatorOwner>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_IndicatorOwner_ListByIndicator", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorOwner
                    {
                        OwnerID = Convert.ToInt32(dr["OwnerID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        InstitutionID = Convert.ToInt32(dr["InstitutionID"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_IndicatorOwner> ListByInstitution(int institutionID)
    {
        var list = new List<cc_IndicatorOwner>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorOwner_ListByInstitution", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstitutionID", institutionID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorOwner
                    {
                        OwnerID = Convert.ToInt32(dr["OwnerID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        InstitutionID = Convert.ToInt32(dr["InstitutionID"])
                    });
                }
            }
        }
        return list;
    }

    public int Delete(int ownerID)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorOwner_Delete", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OwnerID", ownerID);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
