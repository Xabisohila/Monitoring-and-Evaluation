<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addQuarterlyReport.aspx.cs" Inherits="addQuarterlyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />





    <h2>Quarterly Report Submission</h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <div>
        <asp:Label ID="lblIntervention" runat="server" Text="Select Intervention:" />
        <asp:DropDownList ID="ddlIntervention" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIntervention_SelectedIndexChanged" />
        <asp:RequiredFieldValidator ID="rfvIntervention" runat="server" ControlToValidate="ddlIntervention"
            InitialValue="0" ErrorMessage="Intervention is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <asp:Panel ID="pnlSummary" runat="server" Visible="false">
        <h3>Latest Report Summary</h3>
        <asp:Label ID="lblRemainingBudget" runat="server" Text="Remaining Budget: " />
        <br />
        <asp:Label ID="lblDeviation" runat="server" Text="Deviation (%): " />
        <br />
        <asp:Label ID="lblPlannedValue" runat="server" Text="Planned Value: " />
        <br />
        <asp:Label ID="lblCurrentStatus" runat="server" Text="Current Status: " />
    </asp:Panel>

    <h3>New Quarterly Report</h3>
    <div>
        <asp:Label ID="lblFinancialYear" runat="server" Text="Financial Year:" />
        <asp:TextBox ID="txtFinancialYear" runat="server" />
        <asp:RequiredFieldValidator ID="rfvFinancialYear" runat="server" ControlToValidate="txtFinancialYear"
            ErrorMessage="Financial Year is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <div>
        <asp:Label ID="lblQuarter" runat="server" Text="Quarter:" />
        <asp:DropDownList ID="ddlQuarter" runat="server">
            <asp:ListItem Text="-- Select Quarter --" Value="0" />
            <asp:ListItem Text="Q1" Value="Q1" />
            <asp:ListItem Text="Q2" Value="Q2" />
            <asp:ListItem Text="Q3" Value="Q3" />
            <asp:ListItem Text="Q4" Value="Q4" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvQuarter" runat="server" ControlToValidate="ddlQuarter"
            InitialValue="0" ErrorMessage="Quarter is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <div>
        <asp:Label ID="lblActualExpenditure" runat="server" Text="Actual Expenditure:" />
        <asp:TextBox ID="txtActualExpenditure" runat="server" />
        <asp:RequiredFieldValidator ID="rfvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure"
            ErrorMessage="Actual Expenditure is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <div>
        <asp:Label ID="lblPerformanceValue" runat="server" Text="Performance Value:" />
        <asp:TextBox ID="txtPerformanceValue" runat="server" />
        <asp:RequiredFieldValidator ID="rfvPerformanceValue" runat="server" ControlToValidate="txtPerformanceValue"
            ErrorMessage="Performance Value is required." ForeColor="Red" Display="Dynamic" />
    </div>

    <div>
        <asp:Label ID="lblDeviationExplanation" runat="server" Text="Deviation Explanation:" />
        <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" Rows="4" Columns="50" />
    </div>

    <div>
        <asp:Label ID="lblDocument" runat="server" Text="Upload Supporting Document:" />
        <asp:FileUpload ID="fuDocument" runat="server" />
    </div>

    <asp:Button ID="btnSave" runat="server" Text="Save Report" OnClick="btnSave_Click" />




</asp:Content>

