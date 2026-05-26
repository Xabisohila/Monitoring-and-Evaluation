using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_ChangeRequest
/// </summary>
public class cc_ChangeRequest
{

    public int ChangeRequestID { get; set; }
    public int ReportID { get; set; }
    public int RequestedBy { get; set; }
    public int RequestedTo { get; set; }
    public string Reason { get; set; }
    public string Stage { get; set; } // QA, Approval, Signoff
    public DateTime RequestDate { get; set; }
    public bool IsResolved { get; set; }

    public cc_ChangeRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}