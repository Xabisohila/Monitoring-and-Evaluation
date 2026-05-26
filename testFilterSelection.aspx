<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="testFilterSelection.aspx.cs" Inherits="POA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    


    <div class="padding100">
        <div class="col-md-12">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>testModeSelection</strong> </span></h2>
        </div>
    </div>



    <br />
    <br />
    <br />


    <h2>Filter Selection</h2>
    <asp:DropDownList ID="ddlStrategicPriority" runat="server" />
    <asp:DropDownList ID="ddlYear" runat="server">
        <asp:ListItem Text="2025" Value="2025" />
        <asp:ListItem Text="2026" Value="2026" />
    </asp:DropDownList>
    <asp:DropDownList ID="ddlWorkGroup" runat="server" />
    <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" />


    <br />
    <br />
    <br />





</asp:Content>

