<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_Workflow_MyTasks.aspx.cs" Inherits="i_Workflow_MyTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <h2>My Tasks</h2>

    <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlFY" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged" />
    <asp:Label runat="server" Text="Quarter:" AssociatedControlID="ddlQuarter" />
    <asp:DropDownList runat="server" ID="ddlQuarter" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged">
        <asp:ListItem Text="Q1" Value="1" />
        <asp:ListItem Text="Q2" Value="2" />
        <asp:ListItem Text="Q3" Value="3" />
        <asp:ListItem Text="Q4" Value="4" />
    </asp:DropDownList>
    <asp:Button runat="server" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_Click" />
    <hr />

    <!-- QA tasks (visible to QA roles only) -->
    <asp:Panel runat="server" ID="pnlQA" Visible="false">
        <h3>Awaiting QA</h3>
        <asp:GridView runat="server" ID="gvQA" AutoGenerateColumns="true" OnRowCommand="gvQA_RowCommand">
            <Columns>
                <asp:ButtonField CommandName="Open" Text="Open QA" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <!-- Approval tasks (visible to Approval roles only) -->
    <asp:Panel runat="server" ID="pnlApproval" Visible="false">
        <h3>Awaiting Approval</h3>
        <asp:GridView runat="server" ID="gvApproval" AutoGenerateColumns="true" OnRowCommand="gvApproval_RowCommand">
            <Columns>
                <asp:ButtonField CommandName="Open" Text="Open Approval" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <!-- Sign-off tasks (visible to Convenor roles only) -->
    <asp:Panel runat="server" ID="pnlSignoff" Visible="false">
        <h3>Awaiting Sign-off</h3>
        <asp:GridView runat="server" ID="gvSignoff" AutoGenerateColumns="true" OnRowCommand="gvSignoff_RowCommand">
            <Columns>
                <asp:ButtonField CommandName="Open" Text="Open Sign-off" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <!-- Corrections required (visible to submitters) -->
    <asp:Panel runat="server" ID="pnlCorrections" Visible="true">
        <h3>Corrections Required (Failed QA)</h3>
        <asp:GridView ID="gvCorrections" runat="server" DataKeyNames="ReportID" OnRowCommand="gvCorrections_RowCommand" AutoGenerateColumns="true">
            <Columns>
                <asp:ButtonField CommandName="Open" Text="Fix & Resubmit" />
            </Columns>
        </asp:GridView>
    </asp:Panel>



</asp:Content>

