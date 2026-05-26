//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for designedPlanningOverviewModels
///// </summary>
//public class designedPlanningOverviewModels
//{
//    public designedPlanningOverviewModels()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}


using System;
using System.Collections.Generic;

public class Indicator
{
    public string OutcomeIndicator { get; set; }
    public string IndicatorType { get; set; }
    public string UnitOfMeasure { get; set; }
    public string BaselineValue { get; set; }
    public string BaselineYear { get; set; }
    public string TargetValue { get; set; }
    public string TargetYear { get; set; }
}

public class Intervention3
{
    public int InterventionID { get; set; }
    public string InterventionName { get; set; }
    public string ImplementationInstitution { get; set; }
    public string PrimaryMunicipality { get; set; }
    public string InterventionStartYear { get; set; }
    public string InterventionEndYear { get; set; }
    public List<Indicator> Indicators { get; set; }
}

public class SubOutcome
{
    public string SubOutcomeName { get; set; }
    public List<Intervention3> Interventions { get; set; }
    public string SubOutcome4 { get; set; }
}
