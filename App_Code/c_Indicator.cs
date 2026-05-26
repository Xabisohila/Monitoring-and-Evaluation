using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_Indicator
/// </summary>
public class c_Indicator
{
    public int IndicatorID { get; set; }
    public string IndicatorName { get; set; }
    public string IndicatorType { get; set; }

    public int OutcomeID { get; set; }

    public string BaselineValue { get; set; }
    public string TermTargetValue { get; set; }
    public decimal AnnualBudget { get; set; }

    public string ImplementingInstitution { get; set; }
    public string SupportingInstitutions { get; set; }

    public string CalculationType { get; set; }
    public string ReportingCycle { get; set; }

    public bool IsCumulative { get; set; }
    public bool IsPercentage { get; set; }


    // Models/Indicator.cs  (add a nullable WorkingGroupID for Upsert convenience)
    public int? WorkingGroupID { get; set; } // optional WG to attach on upsert
}