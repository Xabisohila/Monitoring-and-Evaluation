using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_QuarterlyReport
/// </summary>
public class c_QuarterlyReport
{
    public int ReportID { get; set; }
    public int QuarterlyTargetID { get; set; }
    public int? SubmittedByUserID { get; set; }
    public DateTime SubmittedDate { get; set; }

    public int QuarterNumber { get; set; }
    public string ActualValue { get; set; }
    public bool Achieved { get; set; }

    public string DeviationReason { get; set; }
    public string RemedialActions { get; set; }
    public string OverAchieveReason { get; set; }
    public DateTime? RemedialDueDate { get; set; }

    public string SpatialReference { get; set; }
}
