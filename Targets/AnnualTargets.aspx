<%--<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="AnnualTargets.aspx.cs" Inherits="Targets_AnnualTargets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnualTargets.aspx.cs" Inherits="Targets_AnnualTargets" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Annual Targets</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <h2>Annual Targets</h2>
        <asp:ValidationSummary runat="server" ID="valSummary" CssClass="text-danger" />
        <asp:Label runat="server" Text="Indicator:" AssociatedControlID="ddlIndicator" />
        <asp:DropDownList runat="server" ID="ddlIndicator" DataTextField="IndicatorName" DataValueField="IndicatorID" />
        <asp:RequiredFieldValidator ControlToValidate="ddlIndicator" InitialValue="" ErrorMessage="Select an Indicator" runat="server" />

        <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="txtFY" />
        <asp:TextBox runat="server" ID="txtFY" />
        <asp:RequiredFieldValidator ControlToValidate="txtFY" ErrorMessage="FY required" runat="server" />

        <asp:Label runat="server" Text="Annual Target Value:" AssociatedControlID="txtAnnualTarget" />
        <asp:TextBox runat="server" ID="txtAnnualTarget" />

        <asp:HiddenField runat="server" ID="hfAnnualTargetID" />
        <asp:Button runat="server" ID="btnSave" Text="Save / Update" OnClick="btnSave_Click" />
        <hr />
        <asp:GridView runat="server" ID="gvAnnualTargets" AutoGenerateColumns="false" OnRowCommand="gvAnnualTargets_RowCommand">
            <Columns>
                <asp:BoundField DataField="AnnualTargetID" HeaderText="ID" />
                <asp:BoundField DataField="IndicatorID" HeaderText="IndicatorID" />
                <asp:BoundField DataField="FinancialYear" HeaderText="Year" />
                <asp:BoundField DataField="AnnualTargetValue" HeaderText="Target" />
                <asp:ButtonField CommandName="EditRow" Text="Edit" />
                <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
            </Columns>
        </asp:GridView>
    </div>
</form>
</body>
</html>