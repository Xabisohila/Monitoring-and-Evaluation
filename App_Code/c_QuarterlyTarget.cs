using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_QuarterlyTarget
/// </summary>
public class c_QuarterlyTarget
{
    public int QuarterlyTargetID { get; set; }
    public int AnnualTargetID { get; set; }

    public int QuarterNumber { get; set; }   // 1–4
    public string TargetValue { get; set; }

    public string SpatialReference { get; set; }
}