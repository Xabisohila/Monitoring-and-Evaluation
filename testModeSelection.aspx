<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="testModeSelection.aspx.cs" Inherits="POA" %>
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


    <h2>Select Mode</h2>
    <asp:RadioButtonList ID="rblMode" runat="server">
        <asp:ListItem Text="Planning" Value="Planning" />
        <asp:ListItem Text="Monitoring" Value="Monitoring" />
    </asp:RadioButtonList>
    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />


    <br />
    <br />
    <br />





</asp:Content>

