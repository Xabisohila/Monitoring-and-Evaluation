using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_WorkflowHistory
/// </summary>
public class c_WorkflowHistory
{
    public int HistoryID { get; set; }

    public int ReportID { get; set; }
    public int StatusID { get; set; }

    public string Stage { get; set; }   // QA / Approval / Signoff

    public int ActionByUserID { get; set; }
    public DateTime ActionDate { get; set; }
    public string Comments { get; set; }
}