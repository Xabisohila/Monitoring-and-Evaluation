using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_AuditTrail
/// </summary>
public class c_AuditTrail
{
    public int AuditID { get; set; }

    public int? UserID { get; set; }
    public string ActionType { get; set; }
    public string TableName { get; set; }
    public int? RecordID { get; set; }

    public DateTime ActionDate { get; set; }
    public string IPAddress { get; set; }

    public string OldValue { get; set; }
    public string NewValue { get; set; }
}
