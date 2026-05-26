using MnE2.DAL;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class i_History : System.Web.UI.Page
{
    private readonly c_WorkflowHistoryDAL wf = new c_WorkflowHistoryDAL(); 
    protected void Page_Load(object sender, EventArgs e) 
    { 
        if (!IsPostBack) Bind(); 
    } 
    private void Bind() 
    { 
        int reportID = Convert.ToInt32(Request["rid"]); 
        gvHistory.DataSource = wf.ListByReport(reportID); 
        gvHistory.DataBind(); 
    } 
}