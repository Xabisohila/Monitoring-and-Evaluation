<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedAddSubOutcome.aspx.cs" Inherits="designedAddSubOutcome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <br /><br /><br /><br />



    <div>
        <%--<h2>Add New SubOutcome</h2>--%>

        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Add New SubOutcome</strong></span></h2>
            </div>
        </div>

        <br />




        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />


        <div>
            <asp:Label ID="lblCluster" runat="server" Text="Select Cluster:" />
            <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblPriority" runat="server" Text="Select Priority:" />
            <asp:DropDownList ID="ddlPriority" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ControlToValidate="ddlPriority"
                InitialValue="0" ErrorMessage="Priority is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblSubOutcome" runat="server" Text="SubOutcome Name:" />
            <asp:TextBox ID="txtSubOutcome" runat="server" />
            <asp:RequiredFieldValidator ID="rfvSubOutcome" runat="server" ControlToValidate="txtSubOutcome"
                ErrorMessage="SubOutcome name is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>



    












</asp:Content>

