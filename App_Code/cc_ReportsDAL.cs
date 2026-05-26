using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_ReportsDAL
/// </summary>
public class cc_ReportsDAL
{
    //public object ListAwaitingApproval(int fy, int q)
    //{
    //    throw new NotImplementedException();
    //}

   
    public List<cc_ReportListItem> ListAwaitingApproval(int fy, int q) 
    { 
        var list = new List<cc_ReportListItem>(); 
        using (var con = Database.GetConnection()) 
        using (var cmd = new SqlCommand("i_sp_Report_AwaitingApproval", con)) 
        { 
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@FinancialYear", fy); 
            cmd.Parameters.AddWithValue("@QuarterNumber", q); 
            con.Open(); using (var r = cmd.ExecuteReader()) 
            { 
                while (r.Read()) 
                { 
                    list.Add(new cc_ReportListItem
                    { 
                        ReportID = (int)r["ReportID"], 
                        IndicatorName = r["IndicatorName"].ToString(), 
                        SubmittedDate = (System.DateTime)r["SubmittedDate"] 
                    }); 
                } 
            } 
        } 
        return list; 
    }
    public cc_ReportViewModel GetReport(int reportId) 
    { 
        using (var con = Database.GetConnection()) 
        using (var cmd = new SqlCommand("sp_QuarterlyReport_GetByID", con)) 
        { 
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@ReportID", reportId); 
            con.Open(); using (var r = cmd.ExecuteReader()) 
            { 
                if (r.Read()) 
                { 
                    return new cc_ReportViewModel
                    { 
                        ReportID = (int)r["ReportID"], 
                        IndicatorName = r["IndicatorName"].ToString(), 
                        Planned = r["TargetValue"].ToString(), 
                        ActualValue = r["ActualValue"].ToString(), 
                        DeviationReason = r["DeviationReason"].ToString(), 
                        RemedialActions = r["RemedialActions"].ToString() 
                    }; 
                } 
            } 
        } 
        return null; 
    }
    public List<cc_Evidence> ListEvidence(int reportId) 
    { 
        var list = new List<cc_Evidence>(); 
        using (var con = Database.GetConnection()) 
        using (var cmd = new SqlCommand("sp_EvidenceFile_ListByReport", con)) 
        { 
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@ReportID", reportId); 
            con.Open(); using (var r = cmd.ExecuteReader()) 
            { 
                while (r.Read()) 
                { 
                    list.Add(new cc_Evidence
                    { 
                        EvidenceID = (int)r["EvidenceID"], 
                        ReportID = (int)r["ReportID"], FileName = r["FileName"].ToString(), 
                        FilePath = r["FilePath"].ToString(), UploadedDate = (System.DateTime)r["UploadedDate"] 
                    }); 
                } 
            } 
        } 
        return list; 
    }
    public List<cc_WorkflowItem> ListWorkflow(int reportId) 
    { 
        var list = new List<cc_WorkflowItem>(); 
        using (var con = Database.GetConnection()) 
        using (var cmd = new SqlCommand("sp_WorkflowHistory_ListByReport", con)) 
        { 
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@ReportID", reportId); 
            con.Open(); using (var r = cmd.ExecuteReader()) 
            { 
                while (r.Read()) 
                { 
                    list.Add(new cc_WorkflowItem
                    { 
                        HistoryID = (int)r["HistoryID"], 
                        ReportID = (int)r["ReportID"], 
                        StatusName = r["StatusName"].ToString(), 
                        Stage = r["Stage"].ToString(), ActionBy = r["ActionByName"].ToString(), 
                        ActionDate = (System.DateTime)r["ActionDate"], 
                        Comments = r["Comments"].ToString() 
                    }); 
                } 
            } 
        } 
        return list; 
    }
    
}





