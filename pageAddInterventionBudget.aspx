<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageAddInterventionBudget.aspx.cs" Inherits="pageAddInterventionBudget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />




    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Add New Intervention Budget</title>
    <style type="text/css">
        /* Basic inline CSS for readability. Consider moving this to an external.css file. */
      
           body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; color: #333; }
   .container { max-width: 800px; margin: auto; padding: 25px; background-color: #fff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { color: #0056b3; margin-bottom: 20px; }
   .form-group { margin-bottom: 15px; }
   .form-group label { display: block; margin-bottom: 5px; font-weight: bold; color: #555; }
   .form-group input[type="text"],
   .form-group textarea,
   .form-group select {
            width: calc(100% - 22px); /* Account for padding and border */
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 1em;
        }
   .form-group textarea { resize: vertical; min-height: 80px; }
   .asp-button { padding: 10px 20px; background-color: #28a745; color: white; border: none; border-radius: 5px; cursor: pointer; font-size: 1em; margin-right: 10px; }
   .asp-button:hover { background-color: #218838; }
   .validation-message { color: red; font-size: 0.9em; margin-top: 5px; display: block; }
   .validation-summary { color: red; border: 1px solid red; padding: 10px; margin-bottom: 20px; background-color: #ffe6e6; border-radius: 5px; }
   .success-message { color: green; border: 1px solid green; padding: 10px; margin-bottom: 20px; background-color: #e6ffe6; border-radius: 5px; }
   .error-message { color: red; border: 1px solid red; padding: 10px; margin-bottom: 20px; background-color: #ffe6e6; border-radius: 5px; }
   .back-link { display: block; margin-top: 20px; text-align: center; }
   .back-link a { color: #007bff; text-decoration: none; font-weight: bold; }
   .back-link a:hover { text-decoration: underline; }


    </style>

        <div class="container">
            <h1>Add New Intervention Budget</h1>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowErrors="True" HeaderText="Please correct the following errors:" CssClass="validation-summary" />
            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>

            <div class="form-group">
                <label for="<%= ddlIntervention.ClientID %>">Intervention:</label>
                <asp:DropDownList ID="ddlIntervention" runat="server" CssClass="form-group select">
                    <asp:ListItem Text="-- Select Intervention --" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvIntervention" runat="server" ControlToValidate="ddlIntervention" InitialValue="0"
                    ErrorMessage="Intervention is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="<%= ddlFinancialYear.ClientID %>">Financial Year:</label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-group select">
                    <asp:ListItem Text="-- Select Financial Year --" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvFinancialYear" runat="server" ControlToValidate="ddlFinancialYear" InitialValue="0"
                    ErrorMessage="Financial Year is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="<%= txtAnnualBudget.ClientID %>">Annual Budget:</label>
                <asp:TextBox ID="txtAnnualBudget" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAnnualBudget" runat="server" ControlToValidate="txtAnnualBudget"
                    ErrorMessage="Annual Budget is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvAnnualBudget" runat="server" ControlToValidate="txtAnnualBudget" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Annual Budget must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
                <asp:RangeValidator ID="rvAnnualBudget" runat="server" ControlToValidate="txtAnnualBudget" Type="Double"
                    MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Budget must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>
            </div>

            <div class="form-group">
                <label for="<%= txtTermBudget.ClientID %>">Term Budget (Optional):</label>
                <asp:TextBox ID="txtTermBudget" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:CompareValidator ID="cvTermBudget" runat="server" ControlToValidate="txtTermBudget" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Term Budget must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
                <asp:RangeValidator ID="rvTermBudget" runat="server" ControlToValidate="txtTermBudget" Type="Double"
                    MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Term Budget must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>
            </div>

            <%--<asp:Button ID="btnSubmit" runat="server" Text="Add Budget" OnClick="btnSubmit_Click" CssClass="asp-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" CssClass="asp-button" />--%>

            <div class="back-link">
                <asp:HyperLink ID="hlBackToOverview" runat="server" Text="Back to Planning Overview"></asp:HyperLink>
            </div>
        </div>
















</asp:Content>

