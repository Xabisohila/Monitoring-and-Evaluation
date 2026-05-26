using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_ChangeRequestDAL
/// </summary>
public class cc_ChangeRequestDAL
{
    public int Upsert(cc_ChangeRequest model)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_ChangeRequest_Upsert", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ChangeRequestID", model.ChangeRequestID == 0 ? (object)DBNull.Value : model.ChangeRequestID);
            cmd.Parameters.AddWithValue("@ReportID", model.ReportID);
            cmd.Parameters.AddWithValue("@RequestedBy", model.RequestedBy);
            cmd.Parameters.AddWithValue("@RequestedTo", model.RequestedTo);
            cmd.Parameters.AddWithValue("@Reason", (object)model.Reason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Stage", model.Stage);
            cmd.Parameters.AddWithValue("@IsResolved", model.IsResolved);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public cc_ChangeRequest GetByID(int changeRequestID)
    {
        cc_ChangeRequest result = null;
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_ChangeRequest_GetByID", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ChangeRequestID", changeRequestID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    result = new cc_ChangeRequest
                    {
                        ChangeRequestID = Convert.ToInt32(dr["ChangeRequestID"]),
                        ReportID = Convert.ToInt32(dr["ReportID"]),
                        RequestedBy = Convert.ToInt32(dr["RequestedBy"]),
                        RequestedTo = Convert.ToInt32(dr["RequestedTo"]),
                        Reason = dr["Reason"] != null ? dr["Reason"].ToString() : null,
                        Stage = dr["Stage"] != null ? dr["Stage"].ToString() : null,
                        RequestDate = Convert.ToDateTime(dr["RequestDate"]),
                        IsResolved = Convert.ToBoolean(dr["IsResolved"])
                    };
                }
            }
        }
        return result;
    }

    public List<cc_ChangeRequest> ListAll()
    {
        var list = new List<cc_ChangeRequest>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_ChangeRequest_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_ChangeRequest
                    {
                        ChangeRequestID = Convert.ToInt32(dr["ChangeRequestID"]),
                        ReportID = Convert.ToInt32(dr["ReportID"]),
                        RequestedBy = Convert.ToInt32(dr["RequestedBy"]),
                        RequestedTo = Convert.ToInt32(dr["RequestedTo"]),
                        Reason = dr["Reason"] != null ? dr["Reason"].ToString() : null,
                        Stage = dr["Stage"] != null ? dr["Stage"].ToString() : null,
                        RequestDate = Convert.ToDateTime(dr["RequestDate"]),
                        IsResolved = Convert.ToBoolean(dr["IsResolved"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_ChangeRequest> ListByReport(int reportID)
    {
        var list = new List<cc_ChangeRequest>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_ChangeRequest_ListByReport", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReportID", reportID);
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_ChangeRequest
                    {
                        ChangeRequestID = Convert.ToInt32(dr["ChangeRequestID"]),
                        ReportID = Convert.ToInt32(dr["ReportID"]),
                        RequestedBy = Convert.ToInt32(dr["RequestedBy"]),
                        RequestedTo = Convert.ToInt32(dr["RequestedTo"]),
                        Reason = dr["Reason"] != null ? dr["Reason"].ToString() : null,
                        Stage = dr["Stage"] != null ? dr["Stage"].ToString() : null,
                        RequestDate = Convert.ToDateTime(dr["RequestDate"]),
                        IsResolved = Convert.ToBoolean(dr["IsResolved"])
                    });
                }
            }
        }
        return list;
    }

    public List<cc_ChangeRequest> ListUnresolved()
    {
        var list = new List<cc_ChangeRequest>();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("ii_sp_ChangeRequest_ListUnresolved", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new cc_ChangeRequest
                    {
                        ChangeRequestID = Convert.ToInt32(dr["ChangeRequestID"]),
                        ReportID = Convert.ToInt32(dr["ReportID"]),
                        RequestedBy = Convert.ToInt32(dr["RequestedBy"]),
                        RequestedTo = Convert.ToInt32(dr["RequestedTo"]),
                        Reason = dr["Reason"] != null ? dr["Reason"].ToString() : null,
                        Stage = dr["Stage"] != null ? dr["Stage"].ToString() : null,
                        RequestDate = Convert.ToDateTime(dr["RequestDate"]),
                        IsResolved = Convert.ToBoolean(dr["IsResolved"])
                    });
                }
            }
        }
        return list;
    }

    public int Delete(int changeRequestID)
    {
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_ChangeRequest_Delete", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ChangeRequestID", changeRequestID);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}

