<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designPlanningOverview2.aspx.cs" Inherits="designPlanningOverview2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
<style>
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
    style = padding:10px 20px;
}


    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <br /><br /><br /><br /><br /><br />

    <%--<h2 class="section-header">Planning Overview by SubOutcome</h2>--%>

    <div class="row section-title text-center">
        <br />
        <div class="row">
            <h2 class="background double animated wow fadeInUp color1" style="color:#000000;" data-wow-delay="0.2s"><span><strong>Planning Overview by SubOutcome</strong></span></h2>
        </div>
    </div>

 <br /><br />



    <%--<asp:DropDownList ID="ddlWorkGroups" runat="server" />
    <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" />
    <asp:DropDownList ID="ddlFinancialYears" runat="server" />
    <asp:Button ID="btnViewOverview" runat="server" Text="View Overview" OnClick="btnViewOverview_Click" />

    <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server"
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
        Width="1160px">
    </AjaxControlToolkit:Accordion>--%>








   




    <%--<asp:DropDownList ID="ddlWorkGroups" runat="server" />
    <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" />
    <asp:DropDownList ID="ddlFinancialYears" runat="server" />--%>









    <div class="filter-panel">
    <label for="ddlWorkGroups">Work Group:</label>
    <asp:DropDownList ID="ddlWorkGroups" runat="server" CssClass="asp-dropdown" />

    <label for="ddlPMTDPPriorities">PMTDP Priority:</label>
    <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" CssClass="asp-dropdown" />

    <label for="ddlFinancialYears">Financial Year:</label>
    <asp:DropDownList ID="ddlFinancialYears" runat="server" CssClass="asp-dropdown" />

    <asp:Button ID="btnViewOverview" runat="server" Text="Overview" CssClass="asp-button" OnClick="btnViewOverview_Click" />
</div>

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





































    <%-- correct --%>


<%--    <div class="filter-panel" runat="server" visible="true">
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
    <asp:Button ID="btnViewOverview" runat="server" Text="View Planning Overview" OnClick="btnViewOverview_Click" CssClass="asp-button" />
</div>--%>

























    <%--<asp:Button ID="btnViewOverview" runat="server" Text="View Overview" OnClick="btnViewOverview_Click" />--%>

   <%-- <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" FadeTransitions="true" FramesPerSecond="40" TransitionDuration="250" RequireOpenedPane="false" />--%>


    <br /><br />

    

</asp:Content>

