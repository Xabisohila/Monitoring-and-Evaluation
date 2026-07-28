using MnE2.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class WorkflowStage
{
    public int    Number          { get; set; }
    public string Name            { get; set; }
    public string Status          { get; set; } // "completed" | "active" | "locked"
    public string CountInfo       { get; set; }
    public string ActionUrl       { get; set; }
    public string ActionLabel     { get; set; }
    public string BlockedBy       { get; set; }
    public string WaitingFor      { get; set; }
    public string ResponsibleRole { get; set; }
}

public class WorkflowStatusDAL
{
    private struct RawCounts
    {
        public int UploadCount, TotalPriorities, AssignedPriorities;
        public int POAUploadCount, ApprovedPOACount, POACount;
        public int QTargetCount, OwnerCount;
    }

    private static RawCounts FetchCounts()
    {
        var c = new RawCounts();
        using (SqlConnection con = Database.GetConnection())
        using (SqlCommand cmd = new SqlCommand("sp_GetWorkflowStatus", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    c.UploadCount        = Convert.ToInt32(dr["UploadCount"]);
                    c.TotalPriorities    = Convert.ToInt32(dr["TotalPriorities"]);
                    c.AssignedPriorities = Convert.ToInt32(dr["AssignedPriorities"]);
                    c.POAUploadCount     = Convert.ToInt32(dr["POAUploadCount"]);
                    c.ApprovedPOACount   = Convert.ToInt32(dr["ApprovedPOACount"]);
                    c.POACount           = Convert.ToInt32(dr["POACount"]);
                    c.QTargetCount       = Convert.ToInt32(dr["QTargetCount"]);
                    c.OwnerCount         = Convert.ToInt32(dr["OwnerCount"]);
                }
            }
        }
        return c;
    }

    public List<WorkflowStage> GetStages()
    {
        RawCounts c = FetchCounts();

        int  unassigned = c.TotalPriorities - c.AssignedPriorities;
        bool isApproved = c.TotalPriorities > 0;
        bool allClusters = c.TotalPriorities > 0 && unassigned == 0;

        // Completion flags — one per stage in order
        bool[] done = new bool[]
        {
            c.UploadCount > 0,          // 0  Upload PMTDP
            isApproved,                 // 1  Approve PMTDP
            allClusters,                // 2  Assign Clusters
            c.POAUploadCount > 0,       // 3  Upload POA
            c.ApprovedPOACount > 0,     // 4  Approve POA
            c.QTargetCount > 0,         // 5  Set Quarterly Targets
            c.OwnerCount > 0            // 6  Assign Indicator Owners
        };

        var meta = new[]
        {
            new { Name = "Upload PMTDP",              Url = "i_PMTDPUpload.aspx",            Label = "Upload File",     Role = "Planning Unit" },
            new { Name = "Approve PMTDP",             Url = "j_PMTDPApprovalList.aspx",      Label = "Review Uploads",  Role = "Planning Unit (second user)" },
            new { Name = "Assign Clusters",           Url = "i_PMTDPPriorityList.aspx",      Label = "Assign Clusters", Role = "Planning Unit" },
            new { Name = "Upload POA",                Url = "i_POAUpload.aspx",              Label = "Upload POA",      Role = "Planning Unit" },
            new { Name = "Approve POA",               Url = "j_POAUploadApprovalList.aspx",  Label = "Review POAs",     Role = "Admin / OTP Monitoring" },
            new { Name = "Set Quarterly Targets",     Url = "i_QuarterlyTargets.aspx",       Label = "Set Targets",     Role = "Planning Unit" },
            new { Name = "Assign Indicator Owners",   Url = "ii_AssignIndicatorOwners.aspx", Label = "Assign Owners",   Role = "Planning Unit" }
        };

        var stages = new List<WorkflowStage>();

        for (int i = 0; i < meta.Length; i++)
        {
            string status, blockedBy = null, waitingFor = null, countInfo = null;

            if (done[i])
            {
                status = "completed";
                switch (i)
                {
                    case 2: countInfo = c.AssignedPriorities + " priorit" + (c.AssignedPriorities == 1 ? "y" : "ies") + " assigned"; break;
                    case 3: countInfo = c.POAUploadCount + " POA upload" + (c.POAUploadCount == 1 ? "" : "s") + " submitted"; break;
                    case 4: countInfo = c.ApprovedPOACount + " POA upload" + (c.ApprovedPOACount == 1 ? "" : "s") + " approved - " + c.POACount + " POA" + (c.POACount == 1 ? "" : "s") + " created"; break;
                    case 5: countInfo = c.QTargetCount + " quarterly target" + (c.QTargetCount == 1 ? "" : "s") + " set"; break;
                    case 6: countInfo = c.OwnerCount + " owner" + (c.OwnerCount == 1 ? "" : "s") + " assigned"; break;
                }
            }
            else
            {
                bool prevDone = (i == 0) || done[i - 1];
                if (prevDone)
                {
                    status = "active";
                    switch (i)
                    {
                        case 0: waitingFor = "Upload the PMTDP Excel file to begin the planning cycle."; break;
                        case 1: waitingFor = "A second Planning Unit user must approve the upload before data is applied."; break;
                        case 2: countInfo  = unassigned + " of " + c.TotalPriorities + " priorit" + (c.TotalPriorities == 1 ? "y" : "ies") + " still need a cluster."; break;
                        case 3: waitingFor = "Upload the Programme of Action Excel file for review."; break;
                        case 4: waitingFor = "An Admin or OTP Monitoring user must approve the POA upload to create live records."; break;
                        case 5: waitingFor = "Set quarterly targets for each indicator in the approved POA."; break;
                        case 6: waitingFor = "Assign a leading department (owner) to each indicator."; break;
                    }
                }
                else
                {
                    status = "locked";
                    for (int j = 0; j < i; j++)
                        if (!done[j]) { blockedBy = meta[j].Name; break; }
                }
            }

            stages.Add(new WorkflowStage
            {
                Number          = i + 1,
                Name            = meta[i].Name,
                Status          = status,
                CountInfo       = countInfo,
                ActionUrl       = meta[i].Url,
                ActionLabel     = meta[i].Label,
                BlockedBy       = blockedBy,
                WaitingFor      = waitingFor,
                ResponsibleRole = meta[i].Role
            });
        }

        return stages;
    }
}
