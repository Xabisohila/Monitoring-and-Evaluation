<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageQuarterlyReportSubmission.aspx.cs" Inherits="pageQuarterlyReportSubmission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />



    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Quarterly Report Submission</title>
    <style type="text/css">
        /* Basic inline CSS for readability. Consider moving this to an external.css file. */
     /*body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; color: #333; }*/
   .container { max-width: 800px; margin: auto; padding: 25px; background-color: #fff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { color: #0056b3; margin-bottom: 20px; }
   .form-group { margin-bottom: 15px; }
   .form-group label { display: block; margin-bottom: 5px; font-weight: bold; color: #555; }
   .form-group input[type="text"],
   .form-group input[type="date"],
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

    <br /><br /><br />

   <%--     <div class="container">
            <h1>Quarterly Report Submission</h1>

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
                <label for="<%= ddlQuarter.ClientID %>">Quarter:</label>
                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-group select">
                    <asp:ListItem Text="-- Select Quarter --" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvQuarter" runat="server" ControlToValidate="ddlQuarter" InitialValue="0"
                    ErrorMessage="Quarter is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="<%= txtReportingDate.ClientID %>">Reporting Date:</label>
                <asp:TextBox ID="txtReportingDate" runat="server" TextMode="Date" CssClass="form-group input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvReportingDate" runat="server" ControlToValidate="txtReportingDate"
                    ErrorMessage="Reporting Date is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvReportingDate" runat="server" ControlToValidate="txtReportingDate" Operator="DataTypeCheck" Type="Date"
                    ErrorMessage="Reporting Date must be a valid date." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
            </div>

            <hr style="margin: 30px 0; border-top: 1px dashed #ccc;" />
            <h2>Actuals and Deviations</h2>

            <div class="form-group">
                <label for="<%= txtActualExpenditure.ClientID %>">Actual Expenditure:</label>
                <asp:TextBox ID="txtActualExpenditure" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure"
                    ErrorMessage="Actual Expenditure is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Actual Expenditure must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
                <%--<asp:RangeValidator ID="rvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure" Type="Double"
                    MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Expenditure must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>- -%>
            </div>

            <div class="form-group">
                <label for="<%= txtPlannedExpenditure.ClientID %>">Planned Expenditure:</label>
                <asp:TextBox ID="txtPlannedExpenditure" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure"
                    ErrorMessage="Planned Expenditure is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Planned Expenditure must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
               <%-- <asp:RangeValidator ID="rvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure" Type="Double"
                    MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Planned Expenditure must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>- -%>
            </div>

            <div class="form-group">
                <label for="<%= txtPerformanceActualValue.ClientID %>">Actual Indicator Value (Optional):</label>
                <asp:TextBox ID="txtPerformanceActualValue" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:CompareValidator ID="cvPerformanceActualValue" runat="server" ControlToValidate="txtPerformanceActualValue" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Actual Indicator Value must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
            </div>

            <div class="form-group">
                <label for="<%= txtPerformancePlannedValue.ClientID %>">Planned Indicator Value (Optional):</label>
                <asp:TextBox ID="txtPerformancePlannedValue" runat="server" CssClass="form-group input"></asp:TextBox>
                <asp:CompareValidator ID="cvPerformancePlannedValue" runat="server" ControlToValidate="txtPerformancePlannedValue" Operator="DataTypeCheck" Type="Double"
                    ErrorMessage="Planned Indicator Value must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
            </div>

            <div class="form-group">
                <label for="<%= txtDeviationExplanation.ClientID %>">Deviation Explanation (Optional):</label>
                <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" CssClass="form-group textarea"></asp:TextBox>
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Submit Report" OnClick="btnSubmit_Click" CssClass="asp-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" CssClass="asp-button" />

            <div class="back-link">
                <asp:HyperLink ID="hlBackToOverview" runat="server" Text="Back to Monitoring Overview"></asp:HyperLink>
            </div>
        </div>--%>











    <div class="container">
    <h1>Quarterly Report Submission</h1>

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
        <label for="<%= ddlQuarter.ClientID %>">Quarter:</label>
        <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-group select">
            <asp:ListItem Text="-- Select Quarter --" Value="0"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvQuarter" runat="server" ControlToValidate="ddlQuarter" InitialValue="0"
            ErrorMessage="Quarter is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
        <label for="<%= txtReportingDate.ClientID %>">Reporting Date:</label>
        <asp:TextBox ID="txtReportingDate" runat="server" TextMode="Date" CssClass="form-group input"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvReportingDate" runat="server" ControlToValidate="txtReportingDate"
            ErrorMessage="Reporting Date is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvReportingDate" runat="server" ControlToValidate="txtReportingDate" Operator="DataTypeCheck" Type="Date"
            ErrorMessage="Reporting Date must be a valid date." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
    </div>

    <hr style="margin: 30px 0; border-top: 1px dashed #ccc;" />
    <h2>Actuals and Deviations</h2>

    <div class="form-group">
        <label for="<%= txtActualExpenditure.ClientID %>">Actual Expenditure:</label>
        <asp:TextBox ID="txtActualExpenditure" runat="server" CssClass="form-group input"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure"
            ErrorMessage="Actual Expenditure is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure" Operator="DataTypeCheck" Type="Double"
            ErrorMessage="Actual Expenditure must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
        <%--<asp:RangeValidator ID="rvActualExpenditure" runat="server" ControlToValidate="txtActualExpenditure" Type="Double"
            MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Expenditure must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>--%>
    </div>

    <div class="form-group">
        <label for="<%= txtPlannedExpenditure.ClientID %>">Planned Expenditure:</label>
        <asp:TextBox ID="txtPlannedExpenditure" runat="server" CssClass="form-group input"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure"
            ErrorMessage="Planned Expenditure is required." Display="Dynamic" CssClass="validation-message"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure" Operator="DataTypeCheck" Type="Double"
            ErrorMessage="Planned Expenditure must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
        <%-- <asp:RangeValidator ID="rvPlannedExpenditure" runat="server" ControlToValidate="txtPlannedExpenditure" Type="Double"
            MinimumValue="0.00" MaximumValue="999999999999.99" ErrorMessage="Planned Expenditure must be a positive number." Display="Dynamic" CssClass="validation-message"></asp:RangeValidator>--%>
    </div>

    <div class="form-group">
        <label for="<%= txtPerformanceActualValue.ClientID %>">Actual Indicator Value (Optional):</label>
        <asp:TextBox ID="txtPerformanceActualValue" runat="server" CssClass="form-group input"></asp:TextBox>
        <asp:CompareValidator ID="cvPerformanceActualValue" runat="server" ControlToValidate="txtPerformanceActualValue" Operator="DataTypeCheck" Type="Double"
            ErrorMessage="Actual Indicator Value must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
    </div>

    <div class="form-group">
        <label for="<%= txtPerformancePlannedValue.ClientID %>">Planned Indicator Value (Optional):</label>
        <asp:TextBox ID="txtPerformancePlannedValue" runat="server" CssClass="form-group input"></asp:TextBox>
        <asp:CompareValidator ID="cvPerformancePlannedValue" runat="server" ControlToValidate="txtPerformancePlannedValue" Operator="DataTypeCheck" Type="Double"
            ErrorMessage="Planned Indicator Value must be a number." Display="Dynamic" CssClass="validation-message"></asp:CompareValidator>
    </div>

    <div class="form-group">
        <label for="<%= txtDeviationExplanation.ClientID %>">Deviation Explanation (Optional):</label>
        <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" CssClass="form-group textarea"></asp:TextBox>
    </div>

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Report" OnClick="btnSubmit_Click" CssClass="asp-button" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" CssClass="asp-button" />

    <div class="back-link">
        <asp:HyperLink ID="hlBackToOverview" runat="server" Text="Back to Monitoring Overview"></asp:HyperLink>
    </div>
</div>


</asp:Content>

