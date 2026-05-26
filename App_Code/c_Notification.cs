using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_Notification
/// </summary>
public class c_Notification
{
    public int NotificationID { get; set; }

    public int? ReportID { get; set; }
    public int? UserID { get; set; }

    public string Message { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsRead { get; set; }
}