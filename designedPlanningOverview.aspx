<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedPlanningOverview.aspx.cs" Inherits="designedPlanningOverview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .accordionContent {
            color: black;
        }
        .accordionHeader {
            background-color: #03AC13;
            color: white;
            font-weight: 600;

            padding: 1px 35px;
            /*padding: 14px 22px;*/


            font-size: 16px;
            border: 1px solid #dcdcdc;
            border-bottom: none;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .accordionHeader:hover {
            background-color: white;
            color: #03AC13;
            /*border: 3px solid;*/
            border: 1px solid;
        }
            .accordionHeader:active {
                background-color: white;
                color: #03AC13;
                /*border: 3px solid;*/
                border: 1px solid;
            }

        .accordionHeaderSelected {
            background-color: white;
            color: #03AC13;
            /*border: 3px solid;*/
            border: 1px solid;
        }
        .accordionContent {
            background-color: #fefefe;
            padding: 18px 22px;
            font-size: 15px;
            color: #333;
            border: 1px solid #dcdcdc;
            border-top: none;
        }
        .acordionLink {
            text-decoration: none;
            color: inherit;
            display: block;
            width: 100%;
        }


















           .container {
    /*max-width: 1400px;*/
    /*max-width:100%;*/
    margin: auto;
    padding: 25px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    width:auto;
}

h1, h2, h3, h4, h5 {
    /*color: #03AC13;*/ /* Green heading color */
    /*color: #ffffff;*/

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
    background-color: #e9e9e9;
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

.filter-panel-width {
    margin-bottom: 25px;
    padding: 20px;
    /*background-color: #f0fff0;*/ /* light green background */
    /*border: 1px solid #a3e6a3;*/ /* green border */
    border-radius: 6px;
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    align-items: center;
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

.fadeInUp .color1{
    color:black;
}


.merged-cell {
    border-bottom: none;
}



.my-button{
     padding:10px 20px;
}


    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <br /><br /><br /><br /><br /><br />

    <%--<h2 class="section-header">Planning Overview by SubOutcome</h2>--%>

    <div class="section-title text-center">
        <br />
        <div >
            <h2 class="background double animated wow fadeInUp color1" style="color:#000000;" data-wow-delay="0.2s"><span><strong>Planning Overview by SubOutcome</strong></span></h2>
        </div>
    </div>

 <br /><br />


    <div class="filter-panel-width">
    <div class="filter-panel">
        <label for="ddlWorkGroups">Work Group:</label>
        <asp:DropDownList ID="ddlWorkGroups" runat="server" CssClass="asp-dropdown" />

        <label for="ddlPMTDPPriorities">PMTDP Priority:</label>
        <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" CssClass="asp-dropdown" />

        <label for="ddlFinancialYears">Financial Year:</label>
        <asp:DropDownList ID="ddlFinancialYears" runat="server" CssClass="asp-dropdown" />

        <asp:Button ID="btnViewOverview" runat="server" Text="Overview" CssClass="asp-button" OnClick="btnViewOverview1_Click" />
    </div>
    




<%--    <asp:Repeater ID="rptSubOutcomes" runat="server">
    <ItemTemplate>
        <h4><%# Eval("SubOutcomeName") %></h4>
        <table class="table table-bordered">
            <thead>
                <tr class="table-header">
                    <th>Intervention</th>
                    <th>Indicator</th>
                    <th>Type</th>
                    <th>Unit</th>
                    <th>Baseline</th>
                    <th>Target</th>
                    <th>Institution</th>
                    <th>Municipality</th>
                    <th>Period</th>
                </tr>
            </thead>
            <asp:Repeater ID="rptInterventions" runat="server" DataSource='<%# Eval("Interventions") %>'>
                <ItemTemplate>
                    <tr>
                        <td rowspan='<%# ((List<Indicator>)Eval("Indicators")).Count == 0 ? 1 : ((List<Indicator>)Eval("Indicators")).Count %>'>
                            <a>
                                <span href="#" class="intervention-link"><%# Eval("InterventionName") %></span>
                            </a>
                        </td>
                        <asp:Repeater ID="rptIndicators" runat="server" DataSource='<%# Eval("Indicators") %>'>
                            <ItemTemplate>
                                <td><%# Eval("OutcomeIndicator") %></td>
                                <td><%# Eval("IndicatorType") %></td>
                                <td><%# Eval("UnitOfMeasure") %></td>
                                <td><%# Eval("BaselineValue") %> (<%# Eval("BaselineYear") %>)</td>
                                <td><%# Eval("TargetValue") %> (<%# Eval("TargetYear") %>)</td>
                                <td><%# Container.DataItem is Intervention3 ? ((Intervention3)Container.DataItem).ImplementationInstitution : "" %></td>
                                <td><%# Container.DataItem is Intervention3 ? ((Intervention3)Container.DataItem).PrimaryMunicipality : "" %></td>
                                <td><%# Container.DataItem is Intervention3 ? ((Intervention3)Container.DataItem).InterventionStartYear + " - " + ((Intervention3)Container.DataItem).InterventionEndYear : "" %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </ItemTemplate>
</asp:Repeater>--%>







        <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server"
            HeaderCssClass="accordionHeader"
            ContentCssClass="accordionContent"
            FadeTransitions="true"
            FramesPerSecond="40"
            TransitionDuration="250"
            RequireOpenedPane="false"
            SuppressHeaderPostbacks="true"
            SelectedIndex="-1"
            Width="100%">
        </AjaxControlToolkit:Accordion>

        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="Please select filters and click 'View Planning Overview' to see details." Visible="false"></asp:Label>

        <br />

        <div style="align-content:end; margin-left: auto;">
            <div class="filter-panel">

                <asp:Button ID="btn_AddIntervention" runat="server" Text="Add New Intervention" CssClass="asp-button" OnClick="btn_AddIntervention_Click"/>
                <asp:Button ID="btn_Excel" runat="server" Text="Export to Excel" CssClass="asp-button" OnClick="btnViewOverview1_Click"/>
                <asp:Button ID="btn_Print" runat="server" Text="Print PDF" CssClass="asp-button" OnClick="btnViewOverview1_Click"/>
            </div>
        </div>




    </div>
    <br />
    <br />

    

</asp:Content>

