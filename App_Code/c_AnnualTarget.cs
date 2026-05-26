using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_AnnualTarget
/// </summary>
public class c_AnnualTarget
{
    public int AnnualTargetID { get; set; }
    public int IndicatorID { get; set; }
    public int FinancialYear { get; set; }
    public string AnnualTargetValue { get; set; }
}