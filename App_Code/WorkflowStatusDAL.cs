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
    public string BlockedBy       { get; set; } // name of the blocking stage
    public string WaitingFor      { get; set; } // human-readable reason
    public string ResponsibleRole { get; set; }
}

public class WorkflowStatusDAL
{
    private struct RawCounts
    {
        public int UploadCount, TotalPriorities, AssignedPriorities;
        public int POACount, InterventionCount, IndicatorCount;
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
                    c.POACount           = Convert.ToInt32(dr["POACount"]);
                    c.InterventionCount  = Convert.ToInt32(dr["InterventionCount"]);
                    c.IndicatorCount     = Convert.ToInt32(dr["IndicatorCount"]);
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

        int unassigned  = c.TotalPriorities - c.AssignedPriorities;
        bool isApproved = c.TotalPriorities > 0; // data applied via ApplyApprovedRow = approved
        bool allClusters = c.TotalPriorities > 0 && unassigned == 0;

        // Completion flags in stage order
        bool[] done = new bool[]
        {
            c.UploadCount > 0,          // 0 Upload
            isApproved,                 // 1 Approved
            allClusters,                // 2 Clusters
            c.POACount > 0,             // 3 POAs
            c.InterventionCount > 0,    // 4 Interventions
            c.IndicatorCount > 0,       // 5 Indicators
            c.QTargetCount > 0,         // 6 Quarterly targets
            c.OwnerCount > 0            // 7 Owners
        };

        // Stage metadata
        var meta = new[]
        {
            new { Name = "Upload PMTDP",          Url = "i_PMTDPUpload.aspx",           Label = "Upload File",       Role = "Planning Unit" },
            new { Name = "Approve PMTDP",          Url = "j_PMTDPApprovalList.aspx",     Label = "Review Uploads",    Role = "Planning Unit (second user)" },
            new { Name = "Assign Clusters",        Url = "i_PMTDPPriorityList.aspx",     Label = "Assign Clusters",   Role = "Planning Unit" },
            new { Name = "Create POAs",            Url = "pageAddPOA.aspx",              Label = "Create POA",        Role = "Planning Unit" },
            new { Name = "Add Interventions",      Url = "pageAddIntervention.aspx",     Label = "Add Intervention",  Role = "Planning Unit" },
            new { Name = "Define Indicators",      Url = "pageAddIndicator.aspx",        Label = "Add Indicator",     Role = "Planning Unit" },
            new { Name = "Set Quarterly Targets",  Url = "i_QuarterlyTargets.aspx",      Label = "Set Targets",       Role = "Planning Unit" },
            new { Name = "Assign Owners",          Url = "ii_AssignIndicatorOwners.aspx",Label = "Assign Owners",     Role = "Planning Unit" }
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
                    case 3: countInfo = c.POACount + " POA" + (c.POACount == 1 ? "" : "s") + " created"; break;
                    case 4: countInfo = c.InterventionCount + " intervention" + (c.InterventionCount == 1 ? "" : "s"); break;
                    case 5: countInfo = c.IndicatorCount + " indicator" + (c.IndicatorCount == 1 ? "" : "s"); break;
                    case 6: countInfo = c.QTargetCount + " quarterly target" + (c.QTargetCount == 1 ? "" : "s"); break;
                    case 7: countInfo = c.OwnerCount + " owner" + (c.OwnerCount == 1 ? "" : "s") + " assigned"; break;
                }
            }
            else
            {
                // Active = previous stage done; locked = some earlier stage not done
                bool prevDone = (i == 0) || done[i - 1];
                if (prevDone)
                {
                    status = "active";
                    switch (i)
                    {
                        case 0: waitingFor = "Upload the PMTDP Excel file to begin."; break;
                        case 1: waitingFor = "A second Planning Unit user must approve the upload before data is applied."; break;
                        case 2: countInfo  = unassigned + " of " + c.TotalPriorities + " priorit" + (c.TotalPriorities == 1 ? "y" : "ies") + " still need a cluster."; break;
                        case 3: waitingFor = "Create the first Programme of Action (POA) for an assigned cluster."; break;
                        case 4: waitingFor = "Add at least one intervention to a POA."; break;
                        case 5: waitingFor = "Define indicators for your interventions."; break;
                        case 6: waitingFor = "Set quarterly targets for each indicator."; break;
                        case 7: waitingFor = "Assign a leading department (owner) to each indicator."; break;
                    }
                }
                else
                {
                    status = "locked";
                    // Point to the first incomplete preceding stage
                    for (int j = 0; j < i; j++)
                    {
                        if (!done[j]) { blockedBy = meta[j].Name; break; }
                    }
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
