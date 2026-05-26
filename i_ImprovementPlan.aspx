<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_ImprovementPlan.aspx.cs" Inherits="i_ImprovementPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">







    <h2>Improvement Plan (Under-Achieved)</h2>
    <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlFY" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged" />
    <asp:Label runat="server" Text="Quarter:" AssociatedControlID="ddlQuarter" />
    <asp:DropDownList runat="server" ID="ddlQuarter" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
        <asp:ListItem Text="Q1" Value="1" />
        <asp:ListItem Text="Q2" Value="2" />
        <asp:ListItem Text="Q3" Value="3" />
        <asp:ListItem Text="Q4" Value="4" />
    </asp:DropDownList>
    <asp:Button runat="server" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_Click" />
    <hr />
    <asp:GridView runat="server" ID="gv" AutoGenerateColumns="true" />





</asp:Content>

