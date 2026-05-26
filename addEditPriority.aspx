<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addEditPriority.aspx.cs" Inherits="addEditPriority" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <br /><br /><br /><br />



    <div>
        <%--<h2>Add / Edit PMTDP Priority</h2>--%>

        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Add PMTDP Priority</strong></span></h2>
            </div>
        </div>

        <br />

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

        <div>
            <asp:Label ID="lblPDP" runat="server" Text="Select PDP:" />
            <asp:DropDownList ID="ddlPDP" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPDP" runat="server" ControlToValidate="ddlPDP"
                InitialValue="0" ErrorMessage="PDP is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblCluster" runat="server" Text="Select Cluster:" />
            <asp:DropDownList ID="ddlCluster" runat="server" />
            <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblPriorityName" runat="server" Text="Priority Name:" />
            <asp:TextBox ID="txtPriorityName" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPriorityName" runat="server" ControlToValidate="txtPriorityName"
                ErrorMessage="Priority name is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Columns="40" />
        </div>

        <div>
            <asp:Label ID="lblDesiredOutcome" runat="server" Text="Desired Outcome:" />
            <asp:TextBox ID="txtDesiredOutcome" runat="server" TextMode="MultiLine" Rows="3" Columns="40" />
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="false" />
    </div>



</asp:Content>

