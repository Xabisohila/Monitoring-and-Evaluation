using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cc_Evidence
/// </summary>
public class cc_Evidence
{
    public int EvidenceID { get; set; } 
    public int ReportID { get; set; } 
    public string FileName { get; set; } 
    public string FilePath { get; set; } 
    public DateTime UploadedDate { get; set; }
}