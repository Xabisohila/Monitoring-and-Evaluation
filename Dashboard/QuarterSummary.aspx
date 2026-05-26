<%--<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="QuarterSummary.aspx.cs" Inherits="Dashboard_QuarterSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
--%>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuarterSummary.aspx.cs" Inherits="Dashboard_QuarterSummary" %>
<!DOCTYPE html>
<html>
<head runat="server"><title>Quarter Summary</title></head>
<body>
<form id="form1" runat="server">
    <h2>Quarter Summary</h2>
    <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlFY" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged" />
    <asp:Label runat="server" Text="Quarter:" AssociatedControlID="ddlQuarter" />
    <asp:DropDownList runat="server" ID="ddlQuarter" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
        <asp:ListItem Text="Q1" Value="1" />
        <asp:ListItem Text="Q2" Value="2" />
        <asp:ListItem Text="Q3" Value="3" />
        <asp:ListItem Text="Q4" Value="4" />
    </asp:DropDownList>
    <hr />
    <div>
        <strong>Achieved:</strong> <asp:Label runat="server" ID="lblAchieved" />
        &nbsp;|&nbsp;
        <strong>Not Achieved:</strong> <asp:Label runat="server" ID="lblNotAchieved" />
        &nbsp;|&nbsp;
        <strong>Total Reported:</strong> <asp:Label runat="server" ID="lblTotal" />
    </div>
</form>
</body>
</html>