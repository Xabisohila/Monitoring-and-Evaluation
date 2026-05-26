<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designReporting.aspx.cs" Inherits="designReporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />


    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" />
    <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" />
    <asp:DropDownList ID="ddlPOA" runat="server" AutoPostBack="true" />
    <asp:DropDownList ID="ddlIntervention" runat="server" AutoPostBack="true" />
    <asp:TextBox ID="txtActualExpenditure" runat="server" />
    <asp:TextBox ID="txtPlannedExpenditure" runat="server" />
    <asp:TextBox ID="txtPerformanceActual" runat="server" />
    <asp:TextBox ID="txtPerformancePlanned" runat="server" />
    <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" />
    <%--<asp:Button ID="btnSubmitReport" runat="server" Text="Submit Report" OnClick="btnSubmitReport_Click" />--%>
    <asp:Button ID="Button1" runat="server" Text="Submit Report" />

    hello
</asp:Content>

