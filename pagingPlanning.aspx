<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pagingPlanning.aspx.cs" Inherits="pagingPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    
<style type="text/css">
      /*  .poa-panel {
            border: 1px solid #ccc;
            padding: 15px;
            margin-bottom: 20px;
            background-color: #f9f9f9;
        }
        .intervention {
            margin-left: 20px;
            padding: 10px;
            background-color: #eef;
        }

        .accordionContent {
            padding: 10px;
            background-color: #f9f9f9;
        }
        .accordionHeader {
            font-weight: bold;
            background-color: #e0e0e0;
            padding: 10px;
        }
        .accordionHeaderSelected {
            background-color: #d0d0d0;
        }*/
    </style>

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

    <div style="width:500px; height:500px;">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><br />
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label><br />
    </div>

    



    <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server"
AutoSize="None"
ContentCssClass="accordionContent"
FadeTransitions="false"
FramesPerSecond="40"
HeaderCssClass="accordionHeader"
HeaderSelectedCssClass="accordionHeaderSelected"
RequireOpenedPane="false"
SelectedIndex="-1"
SuppressHeaderPostbacks="true"
TransitionDuration="250"
Width="100%"
padding-left="50px"
padding-right="10px">

<Panes>
    <AjaxControlToolkit:AccordionPane ID="AccordionPane3" runat="server">
        <Header>
            <a class="acordionLink" href="#">
                <i class="fas fa-chevron-right accordion-icon"></i>
                <asp:Label ID="Label6" runat="server" Text="Section Title" />
            </a>
        </Header>

        <Content>
		
		    </Content>
</AjaxControlToolkit:AccordionPane>

<AjaxControlToolkit:AccordionPane ID="AccordionPane1" runat="server">
    <Header>
        <a class="acordionLink" href="#">
            <i class="fas fa-chevron-right accordion-icon"></i>
            <asp:Label ID="Label7" runat="server" Text="Section Title" />
        </a>
    </Header>
		<Content>
		
		    </Content>
</AjaxControlToolkit:AccordionPane>
</Panes>
</AjaxControlToolkit:Accordion>






    










    

   





















</asp:Content>

