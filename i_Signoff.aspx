<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_Signoff.aspx.cs" Inherits="i_Signoff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">









 
    <h2>Sign-off (Working Group Convenor)</h2>
    <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlFY" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged" />
    <asp:Label runat="server" Text="Quarter:" AssociatedControlID="ddlQuarter" />
    <asp:DropDownList runat="server" ID="ddlQuarter" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
        <asp:ListItem Text="Q1" Value="1" />
        <asp:ListItem Text="Q2" Value="2" />
        <asp:ListItem Text="Q3" Value="3" />
        <asp:ListItem Text="Q4" Value="4" />
    </asp:DropDownList>
    <hr />
    <asp:GridView runat="server" ID="gvAwaitingSignoff" AutoGenerateColumns="false" OnRowCommand="gvAwaitingSignoff_RowCommand">
        <Columns>
            <asp:BoundField DataField="ReportID" HeaderText="ReportID" />
            <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" />
            <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:ButtonField CommandName="Open" Text="Open" />
        </Columns>
    </asp:GridView>
    <hr />
    <asp:Panel runat="server" ID="pnlDetail" Visible="false">
        <h3>Sign-off Report <asp:Label runat="server" ID="lblReportID"></asp:Label></h3>
        <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Rows="3" Width="400" />
        <br />
        <asp:Button runat="server" ID="btnSignoff" Text="Sign-off" OnClick="btnSignoff_Click" />
        <asp:Button runat="server" ID="btnNotApproved" Text="Not Approved" OnClick="btnNotApproved_Click" />
        <asp:HiddenField runat="server" ID="hfReportID" />
    </asp:Panel>
















</asp:Content>

