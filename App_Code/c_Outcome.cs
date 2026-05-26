using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_Outcome
/// </summary>
public class c_Outcome
{
    public int OutcomeID { get; set; }
    public string OutcomeName { get; set; }

    public int? PriorityID { get; set; }
    public int? ProgrammeID { get; set; }
}