<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedMonitoringOverview.aspx.cs" Inherits="designedMonitoringOverview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Monitoring Overview</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />






    
    <style type="text/css">


         .container1 {
    /*max-width: 1400px;*/
    max-width:100%;
    margin: auto;
    padding: 25px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    width:auto !important;
}

h1, h2, h3, h4, h5 {
    color: #03AC13; /* Green heading color */
    margin-top: 20px;
    margin-bottom: 10px;
}

.section-header {
    border-bottom: 2px solid #03AC13;
    padding-bottom: 5px;
    margin-bottom: 15px;
}

.data-label {
    font-weight: bold;
    color: #555;
    display: inline-block;
    width: 200px;
}

.data-value {
    display: inline-block;
    margin-left: 10px;
}

.section {
    margin-bottom: 30px;
    padding: 20px;
    background-color: #fcfcfc;
    border-radius: 6px;
    border: 1px solid #e0e0e0;
}

.table-container {
    overflow-x: auto;
    margin-top: 15px;
}

.data-grid {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 15px;
}

.data-grid th, .data-grid td {
    border: 1px solid #ddd;
    padding: 10px;
    text-align: left;
    vertical-align: top;
}

.data-grid th {
    background-color: #03ac13b5; /*#59c01f;*/ /*#e9e9e9;*/
    font-weight: bold;
    color: #333;
}

.data-grid tr:nth-child(even) {
    background-color: #f9f9f9;
}

.nested-grid {
    margin-top: 10px;
    border: 1px solid #b2e5c2; /* light green border */
    background-color: #e6ffe6; /* soft green background */
}

.nested-grid th {
    background-color: #b3f0b3 !important; /* light green header */
}

.message {
    color: #888;
    text-align: center;
    margin-top: 50px;
    font-size: 1.1em;
}

.filter-panel {
    margin-bottom: 25px;
    padding: 15px;
    background-color: #f0fff0; /* light green background */
    border: 1px solid #a3e6a3; /* green border */
    border-radius: 6px;
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    align-items: center;
}

.filter-panel label {
    margin-right: 5px;
    font-weight: bold;
    color: #03AC13;
}

.asp-dropdown {
    padding: 8px;
    border-radius: 4px;
    border: 1px solid #ccc;
    min-width: 200px;
}

.asp-button {
    padding: 10px 20px;
    background-color: #03AC13;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1em;
}

.asp-button:hover {
    background-color: #028a10;
}

.action-links {
    margin-top: 10px;
    text-align: right;
}

.action-links a {
    margin-left: 15px;
    color: #03AC13;
    text-decoration: none;
    font-weight: bold;
}

.action-links a:hover {
    text-decoration: underline;
}







.data-grid td {
    text-align: center;
    vertical-align: middle;
}


</style>

   <br /><br /><br />



    <div class="row section-title text-center">
        <br />
        <div class="row">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Monitoring</strong></span></h2>

        </div>
    </div>





       <%-- <div class="container1">
            <div class="filter-panel">
                <div>
                    <asp:Label ID="lblWorkGroup" runat="server" Text="Work Group:"></asp:Label>
                    <asp:DropDownList ID="ddlWorkGroups" runat="server" CssClass="asp-dropdown">
                        <asp:ListItem Text="-- Select Work Group --" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label ID="lblPMTDPPriority" runat="server" Text="PMTDP Priority:"></asp:Label>
                    <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" CssClass="asp-dropdown">
                        <asp:ListItem Text="-- Select Priority --" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label ID="lblFinancialYear" runat="server" Text="Financial Year:"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYears" runat="server" CssClass="asp-dropdown">
                        <asp:ListItem Text="-- Select Year --" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>


                <%--<div>
                    <asp:Label ID="lbl_Quarter" runat="server" Text="Quarter:"></asp:Label>
                    <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="asp-dropdown">
                        <asp:ListItem Text="-- Select Quarter --" Value="0" />
                        <asp:ListItem Text="Quarter1" Value="Q1" />
                        <asp:ListItem Text="Quarter2" Value="Q2" />
                        <asp:ListItem Text="Quarter3" Value="Q3" />
                        <asp:ListItem Text="Quarter4" Value="Q4" />
                    </asp:DropDownList>

                </div>-- %>

                <asp:Button ID="btnViewOverview" runat="server" Text="View Monitoring Overview" OnClick="btnViewOverview_Click" CssClass="asp-button" />
            </div>

            <asp:Panel ID="pnlOverview" runat="server" Visible="False">
                <h1 class="section-header">Monitoring Overview for: <asp:Label ID="lblSelectedPriorityName" runat="server"></asp:Label></h1>

                <div class="section">
                    <h2>Core Framework Details</h2>
                    <div><span class="data-label">Implementation Framework Name:</span> <span class="data-value"><asp:Label ID="lblFrameworkName" runat="server"></asp:Label></span></div>
                    <div><span class="data-label">PDP Goal:</span> <span class="data-value"><asp:Label ID="lblPDPGoal" runat="server"></asp:Label></span></div>
                    <div><span class="data-label">Priority Focus:</span> <span class="data-value"><asp:Label ID="lblPriorityFocus" runat="server"></asp:Label></span></div>
                    <div><span class="data-label">Overall Impact:</span> <span class="data-value"><asp:Label ID="lblOverallImpact" runat="server"></asp:Label></span></div>
                    <div><span class="data-label">Priority Description:</span> <span class="data-value"><asp:Label ID="lblPriorityDescription" runat="server"></asp:Label></span></div>
                </div>

                <div class="section">
                    <h2 class="section-header">Integration Programmes (Programmes of Action - POAs)</h2>
                    <asp:Repeater ID="rptPOAs" runat="server" OnItemDataBound="rptPOAs_ItemDataBound">
                        <ItemTemplate>
                            <div style="border: 1px solid #b3d9ff; padding: 15px; margin-bottom: 20px; background-color: #e6f7ff; border-radius: 5px;">
                                <h3>Programme Name: <asp:HyperLink ID="hlIntegrationProgrammeName" runat="server" Text='<%# Eval("IntegrationProgrammeName") %>' NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink></h3>
                                <div><span class="data-label">Desired Outcome:</span> <span class="data-value"><%# Eval("POADesiredOutcome") %></span></div>
                                <div><span class="data-label">Description:</span> <span class="data-value"><%# Eval("POA_Description") %></span></div>
                                <div><span class="data-label">Period:</span> <span class="data-value"><%# Eval("POA_StartYear") %> - <%# Eval("POA_EndYear") %></span></div>
                                <div><span class="data-label">Cluster:</span> <span class="data-value"><%# Eval("ClusterName") %></span></div>

                                <h4 style="margin-top: 15px; color: #007bff;">Interventions under this Programme</h4>
                                <div class="table-container">
                                    <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="data-grid" EmptyDataText="No interventions for this POA.">
                                        <Columns>


                                            <asp:TemplateField HeaderText="Intervention">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIntervention" runat="server" Text='<%# Eval("InterventionName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Institution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("ImplementationInstitution") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Municipality">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMunicipality" runat="server" Text='<%# Eval("PrimaryMunicipality") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Spatial Ref.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpatialRef" runat="server" Text='<%# Eval("SpatialReference") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="PrimaryMunicipality" HeaderText="Municipality" />
                                            <asp:BoundField DataField="SpatialReference" HeaderText="Spatial Ref." />-- %>
                                            <asp:BoundField DataField="InterventionStartYear" HeaderText="Start Year" />
                                            <asp:BoundField DataField="InterventionEndYear" HeaderText="End Year" />
                                            <asp:BoundField DataField="OverallStatus" HeaderText="Status" />
                                            <asp:BoundField DataField="LatestActualIndicatorValue" HeaderText="Actual Ind. Value" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="LatestPlannedIndicatorValue" HeaderText="Planned Ind. Value" DataFormatString="{0:N2}" />
                                            <asp:BoundField DataField="IndicatorDeviationPercentage" HeaderText="Ind. Dev. %" DataFormatString="{0:N2}%" />

                                            <%--<asp:BoundField DataField="LatestActualBudget" HeaderText="Actual Budget" DataFormatString="{0:C}" />-- %>
                                            <asp:TemplateField HeaderText="Actual Budget">
                                                <ItemTemplate>
                                                    <%# "R" + string.Format("{0:N2}", Eval("LatestActualBudget")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="LatestPlannedBudget" HeaderText="Planned Budget" DataFormatString="{0:C}" />- -%>
                                            <asp:TemplateField HeaderText="Planned Budget">
                                                <ItemTemplate>
                                                    <%# "R" + string.Format("{0:N2}", Eval("LatestPlannedBudget")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="BudgetDeviationPercentage" HeaderText="Budget Dev. %" DataFormatString="{0:N2}%" />
                                            <asp:BoundField DataField="Quarter" HeaderText="Latest Qtr" />
                                            <asp:BoundField DataField="ReportingDate" HeaderText="Report Date" DataFormatString="{0:d}" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlSubmitReport" runat="server" NavigateUrl='<%# "pageQuarterlyReportSubmission.aspx?interventionId=" + Eval("InterventionID") + "&fyId=" + ddlFinancialYears.SelectedValue %>'>Submit Report</asp:HyperLink>
                                                    <br />
                                                    <asp:HyperLink ID="hlViewReports" runat="server" NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>'>View All Reports</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ItemTemplate>
                        <%--<EmptyTemplate>
                            <p>No Programmes of Action found for this PMTDP Priority, Work Group, and Financial Year combination.</p>
                        </EmptyTemplate>-- %>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="Please select filters and click 'View Monitoring Overview' to see details."></asp:Label>
        </div>--%>


    <%----------------------------------------------------------------------------------------------------------------------------------%>




    <div class="container1">
        <div class="filter-panel">
            <div>
                <asp:Label ID="lblWorkGroup" runat="server" Text="Work Group:"></asp:Label>
                <asp:DropDownList ID="ddlWorkGroups" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Work Group --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div>
                <asp:Label ID="lblPMTDPPriority" runat="server" Text="PMTDP Priority:"></asp:Label>
                <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Priority --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div>
                <asp:Label ID="lblFinancialYear" runat="server" Text="Financial Year:"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYears" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Year --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div>
                <%--<label for="ddlQuarter">Quarter</label>--%>
                <asp:Label ID="lblQuarter" runat="server" Text="Quarter:"></asp:Label>
                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="Select Quarter" Value="" />
                    <asp:ListItem Text="Q1 - April to June" Value="Q1" />
                    <asp:ListItem Text="Q2 - July to September" Value="Q2" />
                    <asp:ListItem Text="Q3 - October to December" Value="Q3" />
                    <asp:ListItem Text="Q4 - January to March" Value="Q4" />
                </asp:DropDownList>
            </div>


            <asp:Button ID="btnViewOverview" runat="server" Text="View Monitoring Overview" OnClick="btnViewOverview_Click" CssClass="asp-button" />
        </div>


    
        <asp:Panel ID="pnlOverview" runat="server" Visible="False">
            <%--<h1 class="section-header">Monitoring Overview for:--%>
            <h1 class="section-header" style="text-align:center;">
                <asp:Label ID="lblSelectedPriorityName" runat="server" style="font-size:30px;" ></asp:Label></h1>

            <div class="section">
                <h3>Core Framework Details</h3>
                <div><span class="data-label">Implementation Framework Name:</span> <span class="data-value">
                    <asp:Label ID="lblFrameworkName" runat="server"></asp:Label></span></div>
                <div><span class="data-label">PDP Goal:</span> <span class="data-value">
                    <asp:Label ID="lblPDPGoal" runat="server"></asp:Label></span></div>
                <div><span class="data-label">Priority Focus:</span> <span class="data-value">
                    <asp:Label ID="lblPriorityFocus" runat="server"></asp:Label></span></div>
                <div><span class="data-label">Overall Impact:</span> <span class="data-value">
                    <asp:Label ID="lblOverallImpact" runat="server"></asp:Label></span></div>
                <div><span class="data-label">Priority Description:</span> <span class="data-value">
                    <asp:Label ID="lblPriorityDescription" runat="server"></asp:Label></span></div>
            </div>

<%--            <div class="section">
                <h3 class="section-header" style="text-align:center;">Integration Programmes (Programmes of Action - POAs)</h3>
                <asp:Repeater ID="rptPOAs" runat="server" OnItemDataBound="rptPOAs_ItemDataBound">
                    <ItemTemplate>
                        <div style="border: 1px solid #b3d9ff; padding: 15px; margin-bottom: 20px; background-color: #e6f7ff; border-radius: 5px;">
                            <h3>Programme Name: 
                            <asp:HyperLink ID="hlIntegrationProgrammeName" runat="server"
                                Text='<%# Eval("IntegrationProgrammeName") %>'
                                NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink>
                            </h3>
                            <div><span class="data-label">Desired Outcome:</span> <span class="data-value"><%# Eval("POADesiredOutcome") %></span></div>
                            <div><span class="data-label">Description:</span> <span class="data-value"><%# Eval("POA_Description") %></span></div>
                            <div><span class="data-label">Period:</span> <span class="data-value"><%# Eval("POA_StartYear") %> - <%# Eval("POA_EndYear") %></span></div>
                            <div><span class="data-label">Cluster:</span> <span class="data-value"><%# Eval("ClusterName") %></span></div>

                            <h4 style="margin-top: 15px; color: #007bff;">Interventions under this Programme</h4>
                            <div class="table-container">
                                <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="data-grid" EmptyDataText="No interventions for this POA.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Intervention">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIntervention" runat="server" Text='<%# Eval("InterventionName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Institution">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("ImplementationInstitution") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMunicipality" runat="server" Text='<%# Eval("PrimaryMunicipality") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spatial Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpatialRef" runat="server" Text='<%# Eval("SpatialReference") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="InterventionStartYear" HeaderText="Start Year" />
                                        <asp:BoundField DataField="InterventionEndYear" HeaderText="End Year" />
                                        <asp:BoundField DataField="OverallStatus" HeaderText="Status" />
                                        <asp:BoundField DataField="LatestActualIndicatorValue" HeaderText="Actual Ind. Value" DataFormatString="{0:N2}" />
                                        <asp:BoundField DataField="LatestPlannedIndicatorValue" HeaderText="Planned Ind. Value" DataFormatString="{0:N2}" />
                                        <asp:BoundField DataField="IndicatorDeviationPercentage" HeaderText="Ind. Dev. %" DataFormatString="{0:N2}%" />

                                        <asp:TemplateField HeaderText="Actual Budget">
                                            <ItemTemplate>
                                                <%# "R" + string.Format("{0:N2}", Eval("LatestActualBudget")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Planned Budget">
                                            <ItemTemplate>
                                                <%# "R" + string.Format("{0:N2}", Eval("LatestPlannedBudget")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="BudgetDeviationPercentage" HeaderText="Budget Dev. %" DataFormatString="{0:N2}%" />
                                        <asp:BoundField DataField="Quarter" HeaderText="Latest Qtr" />
                                        <asp:BoundField DataField="ReportingDate" HeaderText="Report Date" DataFormatString="{0:d}" />

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlSubmitReport" runat="server"
                                                    NavigateUrl='<%# "pageQuarterlyReportSubmission.aspx?interventionId=" + Eval("InterventionID") + "&fyId=" + ddlFinancialYears.SelectedValue %>'>
                                                Submit Report
                                                </asp:HyperLink>
                                                <br />
                                                <asp:HyperLink ID="hlViewReports" runat="server"
                                                    NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>'>
                                                View All Reports
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>















<%--            <div class="section">
    <h3 class="section-header" style="text-align:center;">Integration Programmes (Programmes of Action - POAs)</h3>
    <asp:Repeater ID="rptPOAs" runat="server" OnItemDataBound="rptPOAs_ItemDataBound">
        <ItemTemplate>
            <div style="border: 1px solid #b3d9ff; padding: 15px; margin-bottom: 20px; background-color: #f0fff0; border-radius: 5px;">
                <h3>Programme Name: 
                    <asp:HyperLink ID="hlIntegrationProgrammeName" runat="server"
                        Text='<%# Eval("IntegrationProgrammeName") %>'
                        NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink>
                </h3>
                <div><span class="data-label">Desired Outcome:</span> <span class="data-value"><%# Eval("POADesiredOutcome") %></span></div>
                <div><span class="data-label">Description:</span> <span class="data-value"><%# Eval("POA_Description") %></span></div>
                <div><span class="data-label">Period:</span> <span class="data-value"><%# Eval("POA_StartYear") %> - <%# Eval("POA_EndYear") %></span></div>
                <div><span class="data-label">Cluster:</span> <span class="data-value"><%# Eval("ClusterName") %></span></div>

                <h4 style="margin-top: 15px; color: #007bff;">Interventions under this Programme</h4>
                <div class="table-container">
                    <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="data-grid" EmptyDataText="No interventions for this POA.">
                        <Columns>
                            <asp:TemplateField HeaderText="Intervention">
                                <ItemTemplate>
                                    <asp:Label ID="lblIntervention" runat="server" Text='<%# Eval("InterventionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Institution">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("ImplementationInstitution") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Municipality">
                                <ItemTemplate>
                                    <asp:Label ID="lblMunicipality" runat="server" Text='<%# Eval("PrimaryMunicipality") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spatial Ref.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpatialRef" runat="server" Text='<%# Eval("SpatialReference") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="InterventionStartYear" HeaderText="Start Year" />
                            <asp:BoundField DataField="InterventionEndYear" HeaderText="End Year" />
                            <asp:BoundField DataField="OverallStatus" HeaderText="Status" />
                            <asp:BoundField DataField="LatestActualIndicatorValue" HeaderText="Actual Ind. Value" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="LatestPlannedIndicatorValue" HeaderText="Planned Ind. Value" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="IndicatorDeviationPercentage" HeaderText="Ind. Dev. %" DataFormatString="{0:N2}%" />
                           
                            <%-- <!-- Combined Budget Column --> -- %>
                            
                            <asp:TemplateField HeaderText="Budget (Actual / Planned)">
                                <ItemTemplate>
                                    <%# "R" + string.Format("{0:N2}", Eval("LatestActualBudget")) + " / R" + string.Format("{0:N2}", Eval("LatestPlannedBudget")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="BudgetDeviationPercentage" HeaderText="Budget Dev. %" DataFormatString="{0:N2}%" />
                            <asp:BoundField DataField="Quarter" HeaderText="Latest Qtr" />
                            <asp:BoundField DataField="ReportingDate" HeaderText="Report Date" DataFormatString="{0:d}" />

                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlSubmitReport" runat="server"
                                        NavigateUrl='<%# "designedSubmitQuarterlyReport.aspx?interventionId=" + Eval("InterventionID") + "&fyId=" + ddlFinancialYears.SelectedValue %>'>
                                        Submit Report
                                    </asp:HyperLink>
                                    <br />
                                    <asp:HyperLink ID="hlViewReports" runat="server"
                                        NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>'>
                                        View All Reports
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>--%>




<div class="section">
    <h3 class="section-header" style="text-align:center;">Integration Programmes (Programmes of Action - POAs)</h3>
    <asp:Repeater ID="rptPOAs" runat="server" OnItemDataBound="rptPOAs_ItemDataBound">
        <ItemTemplate>
            <div style="border: 1px solid #b3d9ff; padding: 15px; margin-bottom: 20px; background-color: #f0fff0; border-radius: 5px;">
                <h3>Programme Name: 
                    <asp:HyperLink ID="hlIntegrationProgrammeName" runat="server"
                        Text='<%# Eval("IntegrationProgrammeName") %>'
                        NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink>
                </h3>
                <div><span class="data-label">Desired Outcome:</span> <span class="data-value"><%# Eval("POADesiredOutcome") %></span></div>
                <div><span class="data-label">Description:</span> <span class="data-value"><%# Eval("POA_Description") %></span></div>
                <div><span class="data-label">Period:</span> <span class="data-value"><%# Eval("POA_StartYear") %> - <%# Eval("POA_EndYear") %></span></div>
                <div><span class="data-label">Cluster:</span> <span class="data-value"><%# Eval("ClusterName") %></span></div>

                <h4 style="margin-top: 15px; color: #007bff;">Interventions under this Programme</h4>
                <div class="table-container">


    <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="data-grid" EmptyDataText="No interventions for this POA.">
    <Columns>
        <asp:TemplateField HeaderText="Intervention">
            <ItemTemplate>
                <asp:Label ID="lblIntervention" runat="server" Text='<%# Eval("InterventionName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Institution">
            <ItemTemplate>
                <asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("ImplementationInstitution") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Municipality">
            <ItemTemplate>
                <asp:Label ID="lblMunicipality" runat="server" Text='<%# Eval("PrimaryMunicipality") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <%--<asp:TemplateField HeaderText="Spatial Ref.">
            <ItemTemplate>
                <asp:Label ID="lblSpatialRef" runat="server" Text='<%# Eval("SpatialReference") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

        <%-- <!-- Combined Period Column --> --%>
        
        <asp:TemplateField HeaderText="Period">
            <ItemTemplate>
                <%# Eval("InterventionStartYear") + " - " + Eval("InterventionEndYear") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="OverallStatus" HeaderText="Status" />
        <asp:BoundField DataField="LatestActualIndicatorValue" HeaderText="Actual Ind. Value" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="LatestPlannedIndicatorValue" HeaderText="Planned Ind. Value" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="IndicatorDeviationPercentage" HeaderText="Ind. Dev. %" DataFormatString="{0:N2}%" />

        <%-- <!-- Combined Budget Column --> --%>
        
        <asp:TemplateField HeaderText="Budget (Actual / Planned)">
            <ItemTemplate>
                <%# "R" + string.Format("{0:N2}", Eval("LatestActualBudget")) + " / R" + string.Format("{0:N2}", Eval("LatestPlannedBudget")) %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="BudgetDeviationPercentage" HeaderText="Budget Dev. %" DataFormatString="{0:N2}%" />
        <asp:BoundField DataField="Quarter" HeaderText="Latest Qtr" />
        <asp:BoundField DataField="ReportingDate" HeaderText="Report Date" DataFormatString="{0:d}" />



        <asp:TemplateField HeaderText="Spatial Ref.">
    <ItemTemplate>
        <asp:Label ID="lblSpatialRef" runat="server" Text='<%# Eval("SpatialReference") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>



        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hlSubmitReport" runat="server"
                    NavigateUrl='<%# "designedSubmitQuarterlyReport.aspx?interventionId=" + Eval("InterventionID") + "&fyId=" + ddlFinancialYears.SelectedValue %>'>
                    Submit Report
                </asp:HyperLink>
                <br />
                <asp:HyperLink ID="hlViewReports" runat="server"
                    NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>'>
                    View All Reports
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



                    
            </div>
        </ItemTemplate>
    </asp:Repeater>




        </asp:Panel>

        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="Please select filters and click 'View Monitoring Overview' to see details."></asp:Label>
    </div>





    <%-----------------------------------------------------------------------------------------------------------------------------------------------%>










































































































<%--    <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server"
    AutoSize="None"
    ContentCssClass="accordionContent"
    FadeTransitions="false"
    FramesPerSecond="40"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    RequireOpenedPane="false"
    SelectedIndex="-1"
    SuppressHeaderPostbacks="true"
    TransitionDuration="250"
    Width="100%"
    padding-left="50px"
    padding-right="10px">

    <Panes>
        <AjaxControlToolkit:AccordionPane ID="AccordionPane3" runat="server">
            <Header>
                <a class="acordionLink" href="#">
                    <i class="fas fa-chevron-right accordion-icon"></i>
                    <asp:Label ID="Label4" runat="server" Text="Section Title" />
                    <h3>Programme Name: <asp:HyperLink ID="hlIntegrationProgrammeName1" runat="server" Text='<%# Eval("IntegrationProgrammeName") %>' NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink></h3>
                </a>
            </Header>
            <Content>
                <h3>Programme Name: <asp:HyperLink ID="hlIntegrationProgrammeName2" runat="server" Text='<%# Eval("IntegrationProgrammeName") %>' NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>'></asp:HyperLink></h3>
            </Content>
        </AjaxControlToolkit:AccordionPane>

        <AjaxControlToolkit:AccordionPane ID="AccordionPane1" runat="server">
            <Header>
                <a class="acordionLink" href="#">
                    <i class="fas fa-chevron-right accordion-icon"></i>
                    <asp:Label ID="Label2" runat="server" Text="Section Title" />
                </a>
            </Header>
            <Content>

            </Content>
        </AjaxControlToolkit:AccordionPane>
    </Panes>
</AjaxControlToolkit:Accordion>--%>


</asp:Content>

