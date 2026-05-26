<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="ii_ApprovalInbox.aspx.cs" Inherits="ii_ApprovalInbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">





    <div class="card">
  <h2>Approval Inbox</h2>
  <div class="filter">
    <asp:DropDownList runat="server" ID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlQ" />
    <asp:Button runat="server" ID="btnFilter" Text="Filter" CssClass="btn" OnClick="btnFilter_Click" />
  </div>
  <asp:GridView runat="server" ID="gv" AutoGenerateColumns="false" CssClass="table" OnRowCommand="gv_RowCommand">
    <Columns>
      <asp:BoundField DataField="ReportID" HeaderText="ReportID" />
      <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" />
      <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted" DataFormatString="{0:yyyy-MM-dd}" />
      <asp:TemplateField HeaderText="Actions">
        <ItemTemplate>
          <asp:LinkButton runat="server" CommandName="view" CommandArgument='<%# Eval("ReportID") %>' Text="View" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
</div>






</asp:Content>

