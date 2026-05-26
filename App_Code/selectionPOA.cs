using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for selectionPOA
/// </summary>
public class selectionPOA
{
    public int POA_ID { get; set; }
    public string POA_Name { get; set; }
    public string POA_Description { get; set; }
    public int POA_StartYear { get; set; }
    public int POA_EndYear { get; set; }
    public string DesiredOutcome { get; set; }
    public string ClusterName { get; set; }
    public string PriorityName { get; set; }
    public string PDP_Name { get; set; }

    public List<selectionIntervention> Interventions { get; set; }

    public selectionPOA()
    {
        Interventions = new List<selectionIntervention>();
    }
}

public class selectionIntervention
{
    public int InterventionID { get; set; }
    public string InterventionName { get; set; }
    public string InterventionDescription { get; set; }
    public string LeadInstitution { get; set; }
    public string WorkingGroup { get; set; }
    public string Location { get; set; }
    public string SpatialReference { get; set; }
}

