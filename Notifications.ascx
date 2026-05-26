<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Notifications.ascx.cs" Inherits="Notifications" %>

<div>
    <asp:Label runat="server" ID="lblBell" Text="🔔" />
    <asp:Label runat="server" ID="lblCount" CssClass="badge" />
    <asp:Repeater runat="server" ID="repNotes">
        <ItemTemplate>
            <div><%# Eval("SentDate", "{0:yyyy-MM-dd HH:mm}") %> - <%# Eval("Message") %></div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Button runat="server" ID="btnMarkAll" Text="Mark all read" OnClick="btnMarkAll_Click" />
</div>