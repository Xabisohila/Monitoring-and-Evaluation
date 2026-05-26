<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProvincialCalendar.aspx.cs"  MasterPageFile="~/akshara.master" Inherits="ProvincialCalendar" %>
<%@ Register    Assembly="AjaxControlToolkit"    Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register namespace="AjaxControlToolkit" tagprefix="AjaxControlToolkit" %>
<asp:Content ID="Calendar" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
        <section id="services" class="padding50"> 
        <div class="container">
          <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">
                
                    <br /><br /><br /><br /><br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> M&E Provincial Calendar </strong> </span></h2>
                        
                     </div>
               
                </div>
                </div>
    <asp:Label ID="Label4" runat="server" CssClass="NormalLabel" Text="From:"></asp:Label>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="NormalTextBox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="ImageButton1"/>
                <asp:ImageButton ID="ImageButton1" runat="Server" AlternateText="Click to show calendar" ImageUrl="~/img/Calendar_scheduleHS.png" />

    &nbsp;<asp:Label ID="Label5" runat="server" CssClass="NormalLabel" Text="To:"></asp:Label>&nbsp;<asp:TextBox ID="txtDateTo" runat="server" CssClass="NormalTextBox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="ImageButton2"/>
                <asp:ImageButton ID="ImageButton2" runat="Server" AlternateText="Click to show calendar" ImageUrl="~/img/Calendar_scheduleHS.png" />
    &nbsp;<asp:Button ID="btnCalendar" runat="server" CssClass="Button" OnClick="btnCalendar_Click" Text="Load Calendar Details" />
    <br />
    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:Calendar ID="Calendar1" runat="server" Height="723px" Width="1157px" OnDayRender=" Calendar1_DayRender" BackColor="#ffffff" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" ShowGridLines="True" ToolTip="IGR Calendar" >
        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
        <OtherMonthDayStyle ForeColor="#CC9966" />
        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
        <SelectorStyle BackColor="#FFCC66" />
        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC"  Height="1px"/>
        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
</asp:Calendar>
    </div>
            </section>
</asp:Content>
