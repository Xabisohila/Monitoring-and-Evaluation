using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_EvidenceFile
/// </summary>
public class c_EvidenceFile
{
    public int EvidenceID { get; set; }
    public int ReportID { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }

    public DateTime UploadedDate { get; set; }
}