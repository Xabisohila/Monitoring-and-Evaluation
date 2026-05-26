<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designDashboard.aspx.cs" Inherits="designDashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
<style type="text/css">
        .dashboard-header { font-size: 20px; font-weight: bold; margin-bottom: 10px; }
        .accordion-pane { margin-bottom: 20px; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; }




    body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f9f9f9;
        margin: 20px;
    }

    .dashboard-header {
        font-size: 24px;
        font-weight: bold;
        margin-bottom: 20px;
        color: #333;
    }

    form {
        max-width: 1200px;
        margin: auto;
        background-color: #fff;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    select, button {
        margin-right: 10px;
        padding: 8px;
        font-size: 14px;
    }

    button {
        background-color: #0078d7;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        button:hover {
            background-color: #005fa3;
        }

    .accordion-header {
        background-color: #0078d7;
        color: white;
        padding: 10px;
        font-weight: bold;
        border-radius: 4px 4px 0 0;
    }

    .accordion-content {
        background-color: #f1f1f1;
        padding: 15px;
        border-radius: 0 0 4px 4px;
        margin-bottom: 20px;
    }

    .accordion-header-selected {
        background-color: deeppink;
        color: white;
        padding: 10px;
        font-weight: bold;
        border-radius: 4px 4px 0 0;
    }

    table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 10px;
    }

    th, td {
        border: 1px solid #ccc;
        padding: 10px;
        text-align: left;
    }

    th {
        background-color: #e0e0e0;
        font-weight: bold;
    }

    h3, h4 {
        margin-top: 20px;
        color: #444;
    }


    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <br /><br /><br /><br /><br />


    
    <div class="dashboard-header">Planning Overview by SubOutcome</div>

    <asp:DropDownList ID="ddlWorkGroups" runat="server" />
    <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" />
    <asp:DropDownList ID="ddlFinancialYears" runat="server" />
    <asp:Button ID="btnViewOverview" runat="server" Text="View Overview"  OnClick="btnViewOverview_Click"/>
    

    <AjaxControlToolkit:accordion id="MyAccordion" runat="server" headercssclass="accordion-header" contentcssclass="accordion-content"  HeaderSelectedCssClass="accordion-header-selected"/>





</asp:Content>

