<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addEditIntervention.aspx.cs" Inherits="addEditIntervention" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />




    <h2>Intervention Management</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <div>
        <label>POA:</label>
        <asp:DropDownList ID="ddlPOA" runat="server" />
    </div>
    <div>
        <label>Lead Institution:</label>
        <asp:DropDownList ID="ddlInstitution" runat="server" />
    </div>
    <div>
        <label>Working Group:</label>
        <asp:DropDownList ID="ddlWorkingGroup" runat="server" />
    </div>
    <div>
        <label>Municipality:</label>
        <asp:DropDownList ID="ddlMunicipality" runat="server" />
    </div>
    <div>
        <label>SubOutcome:</label>
        <asp:DropDownList ID="ddlSubOutcome" runat="server" />
    </div>
    <div>
        <label>Intervention Name:</label>
        <asp:TextBox ID="txtInterventionName" runat="server" />
    </div>
    <div>
        <label>Description:</label>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" Columns="50" />
    </div>
    <div>
        <label>Start Year:</label>
        <asp:TextBox ID="txtStartYear" runat="server" />
    </div>
    <div>
        <label>End Year:</label>
        <asp:TextBox ID="txtEndYear" runat="server" />
    </div>

    <h3>Indicators</h3>
    <asp:TextBox ID="txtIndicator" runat="server" />
    <asp:Button ID="btnAddIndicator" runat="server" Text="Add Indicator" OnClick="btnAddIndicator_Click" />

    <h3>Budgets</h3>
    <asp:TextBox ID="txtBudgetYear" runat="server" />
    <asp:TextBox ID="txtBudgetAmount" runat="server" />
    <asp:Button ID="btnAddBudget" runat="server" Text="Add Budget" OnClick="btnAddBudget_Click" />

    <h3>Documents</h3>
    <asp:FileUpload ID="fileDocument" runat="server" />
    <asp:TextBox ID="txtDocumentDescription" runat="server" />
    <asp:Button ID="btnUploadDocument" runat="server" Text="Upload Document" OnClick="btnUploadDocument_Click" />

    <asp:Button ID="btnSave" runat="server" Text="Save Intervention" OnClick="btnSave_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete Intervention" OnClick="btnDelete_Click" Visible="false" />








</asp:Content>

