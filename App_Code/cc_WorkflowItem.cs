using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_WorkflowItem
/// </summary>
public class cc_WorkflowItem
{
    public int HistoryID { get; set; } 
    public int ReportID { get; set; } 
    public string StatusName { get; set; } 
    public string Stage { get; set; } 
    public string ActionBy { get; set; } 
    public DateTime ActionDate { get; set; } 
    public string Comments { get; set; }
}