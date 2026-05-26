using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_ReportViewModel
/// </summary>
public class cc_ReportViewModel
{
    public int ReportID { get; set; } 
    public string IndicatorName { get; set; } 
    public string Planned { get; set; } 
    public string ActualValue { get; set; } 
    public string DeviationReason { get; set; } 
    public string RemedialActions { get; set; }
}