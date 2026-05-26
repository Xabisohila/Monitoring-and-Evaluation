using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_SystemPeriod
/// </summary>
public class c_SystemPeriod
{
    public int PeriodID { get; set; }

    public int FinancialYear { get; set; }
    public int Quarter { get; set; }

    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public bool IsOpen { get; set; }
}