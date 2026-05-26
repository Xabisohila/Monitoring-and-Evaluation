<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCalendar.aspx.cs"  MasterPageFile="~/akshara.master" Inherits="ViewCalendar" %>
<%@ Register    Assembly="AjaxControlToolkit"    Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Calendar" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
            <section id="services" class="padding50"> 
        <div class="container">
          <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">
                
                    <br /><br /><br /><br /><br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> M&E Calendar </strong> </span></h2>
                        
                     </div>
               
                </div>
                </div>
    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
    
      <table style="width: 28%;">
          <tr>
              <td style="width: 153px">&nbsp;</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
          <tr>
              <td style="width: 153px">
        <asp:Label ID="Label4" runat="server" CssClass="NormalLabel" Text="From:"></asp:Label>
                </td>
              <td>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="LongTextBox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="ImageButton1"/>
                </td>
              <td>
                <asp:ImageButton ID="ImageButton1" runat="Server" AlternateText="Click to show calendar" ImageUrl="~/img/Calendar_scheduleHS.png" />

              </td>
          </tr>
          <tr>
              <td style="width: 153px"><asp:Label ID="Label5" runat="server" CssClass="NormalLabel" Text="To:"></asp:Label></td>
              <td>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="LongTextBox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="ImageButton2"/>
                </td>
              <td>
                <asp:ImageButton ID="ImageButton2" runat="Server" AlternateText="Click to show calendar" ImageUrl="~/img/Calendar_scheduleHS.png" />
              </td>
          </tr>
          <tr>
              <td style="width: 153px">&nbsp;</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
          </tr>
          <tr>
              <td style="width: 153px">&nbsp;</td>
              <td><asp:Button ID="btnCalendar" runat="server" CssClass="Button" OnClick="btnCalendar_Click" Text="View Details" />

              </td>
              <td>&nbsp;</td>
          </tr>
      </table>
      <br />

      <br />

    <asp:GridView ID="gvCalendar" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Outset" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="gvCalendar_PageIndexChanging" Width="1157px" WizardCustomPager="False" style="margin-right: 0px" ForeColor="Black" GridLines="Horizontal">
        <Columns>
             <asp:TemplateField HeaderText="Date">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="Left" Width="75px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="75px" />
                <ItemTemplate>
                    <asp:Label ID="lblVenue" runat="server" Text='<%# Bind("Event_Date", "{0:d MMM yyyy}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="Left" Width="15px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="15px" />
                <ItemTemplate>
                    <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Time") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="Left" Width="205px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="205px" />
                <ItemTemplate>
                    <asp:Label ID="lblEventId" runat="server" Text='<%# Bind("Id") %>' visible="false"/>
                    <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("Event") %>' />
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Venue">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="left" Width="100px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblVenue" runat="server" Text='<%# Eval("Location") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Person">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="left" Width="50px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="50px" />
                <ItemTemplate>
                    <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("Contact_Person") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact number">
                <HeaderStyle BackColor="#F8C471  " HorizontalAlign="left" Width="50px" />
                <ItemStyle Height="50px" HorizontalAlign="left" Width="50px" />
                <ItemTemplate>
                    <asp:Label ID="lblContactNumber" runat="server" Text='<%# Eval("Contact_Number") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="Black" />
        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="White" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
            <br /><br />
</div>
</section>

</asp:Content>
