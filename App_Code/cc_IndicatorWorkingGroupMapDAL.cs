using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_IndicatorWorkingGroupMapDAL
/// </summary>
public class cc_IndicatorWorkingGroupMapDAL
{
    public int Upsert(cc_IndicatorWorkingGroupMap model)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_IndicatorWorkingGroupMap_Upsert", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MapID", model.MapID == 0 ? (object)DBNull.Value : model.MapID);
            cmd.Parameters.AddWithValue("@IndicatorID", model.IndicatorID);
            cmd.Parameters.AddWithValue("@WorkingGroupID", model.WorkingGroupID);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public cc_IndicatorWorkingGroupMap GetByID(int mapID)
    {
        cc_IndicatorWorkingGroupMap result = null;
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorWorkingGroupMap_GetByID", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MapID", mapID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    result = new cc_IndicatorWorkingGroupMap
                    {
                        MapID = Convert.ToInt32(dr["MapID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"])
                    };
                }
            }
        }
        return result;
    }

    public List<cc_IndicatorWorkingGroupMap> ListAll()
    {
        var list = new List<cc_IndicatorWorkingGroupMap>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorWorkingGroupMap_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorWorkingGroupMap
                    {
                        MapID = Convert.ToInt32(dr["MapID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_IndicatorWorkingGroupMap> ListByIndicator(int indicatorID)
    {
        var list = new List<cc_IndicatorWorkingGroupMap>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_IndicatorWorkingGroupMap_ListByIndicator", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IndicatorID", indicatorID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorWorkingGroupMap
                    {
                        MapID = Convert.ToInt32(dr["MapID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_IndicatorWorkingGroupMap> ListByWorkingGroup(int workingGroupID)
    {
        var list = new List<cc_IndicatorWorkingGroupMap>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_IndicatorWorkingGroupMap_ListByWorkingGroup", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WorkingGroupID", workingGroupID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_IndicatorWorkingGroupMap
                    {
                        MapID = Convert.ToInt32(dr["MapID"]),
                        IndicatorID = Convert.ToInt32(dr["IndicatorID"]),
                        WorkingGroupID = Convert.ToInt32(dr["WorkingGroupID"])
                    });
                }
            }
        }
        return list;
    }

    public int Delete(int mapID)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_IndicatorWorkingGroupMap_Delete", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MapID", mapID);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()); // returns MapID
        }
    }
}

