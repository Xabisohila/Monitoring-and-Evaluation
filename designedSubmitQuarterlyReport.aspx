<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedSubmitQuarterlyReport.aspx.cs" Inherits="designedSubmitQuarterlyReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <br /><br /><br /><br /><br />



    <style type="text/css">
        .form-group { margin-bottom: 15px; }
        label { display: block; font-weight: bold; }
        .form-control { width: 100%; padding: 8px; }
        .btn { padding: 10px 20px; }

        .asp-button { padding: 10px 20px; background-color: #28a745; color: white; border: none; border-radius: 5px; cursor: pointer; font-size: 1em; margin-right: 10px; }
.asp-button:hover { background-color: #218838; }
    </style>





    <div class="row section-title text-center">
        <br />
        <div class="row">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Submit Quarterly Report</strong></span></h2>
        </div>
    </div>



    <div style="max-width: 600px; margin: auto;">

        <asp:Panel ID="pnlSummary" runat="server" Visible="false" CssClass="summary-panel" style="padding:15px; border:1px solid #ccc; margin-top:20px;">
    <h3>Latest Report Summary</h3>
    <p><strong>Remaining Budget:</strong> <asp:Label ID="lblRemainingBudget" runat="server" /></p>
    <p><strong>Deviation %:</strong> <asp:Label ID="lblDeviationPercent" runat="server" /></p>
    <p><strong>Performance:</strong> <asp:Label ID="lblPerformance" runat="server" /></p>
    <p><strong>Reporting Date:</strong> <asp:Label ID="lblReportDate" runat="server" /></p>
    <p><strong>Deviation Explanation:</strong> <asp:Label ID="lblDeviationExplanation" runat="server" /></p>
    <p><strong>Uploaded By:</strong> <asp:Label ID="lblUploadedBy" runat="server" /></p>
    <p><strong>Supporting Document:</strong> <asp:HyperLink ID="lnkDocument" runat="server" Text="View Document" Target="_blank" /></p>
</asp:Panel>



    <br />
        </div>









        <div style="max-width: 600px; margin: auto;">
            <%--<h2>Submit Quarterly Report</h2>--%>

            <div class="form-group">
                <label for="ddlIntervention">Intervention</label>
                <asp:DropDownList ID="ddlIntervention" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIntervention_SelectedIndexChanged"/>
            </div>

            <div class="form-group">
                <label for="ddlFinancialYear">Financial Year</label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="ddlQuarter">Quarter</label>
                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Quarter" Value="" />
                    <asp:ListItem Text="Q1 - April to June" Value="Q1" />
                    <asp:ListItem Text="Q2 - July to September" Value="Q2" />
                    <asp:ListItem Text="Q3 - October to December" Value="Q3" />
                    <asp:ListItem Text="Q4 - January to March" Value="Q4" />
                </asp:DropDownList>
            </div>

            <%-- Swaped these --%>

            <div class="form-group">
                <label for="txtPlannedExpenditure">Planned Expenditure (R)</label>
                <asp:TextBox ID="txtPlannedExpenditure" runat="server" CssClass="form-control" style="max-width: 230px;"/>
            </div>

            <div class="form-group">
                <label for="txtActualExpenditure">Actual Expenditure (R)</label>
                <asp:TextBox ID="txtActualExpenditure" runat="server" CssClass="form-control" style="max-width: 230px;"/>
            </div>

            <%-- Swaped these --%>

            <%--------------------------------------------------------------------------------------------------%>

            <%-- Swaped these --%>

            <div class="form-group">
                <%--<label for="txtPerformancePlanned">Performance Planned Value</label>--%>
                <label for="txtPerformancePlanned">Planned Target</label>
                <asp:TextBox ID="txtPerformancePlanned" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <%--<label for="txtPerformanceActual">Performance Actual Value</label>--%>
                <label for="txtPerformanceActual">Actual Achieved</label>
                <asp:TextBox ID="txtPerformanceActual" runat="server" CssClass="form-control" />
            </div>

            <%-- Swaped these --%>

            

            

            <div class="form-group">
                <%--<label for="txtDeviationExplanation">Deviation Explanation</label>--%>
                <label for="txtDeviationExplanation">Reason for Deviation</label>
                <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="fileUpload">Upload Supporting Document</label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" AllowMultiple="false"/>
            </div>

            <br />

            <div class="form-group">
                <asp:Button ID="btnSubmitReport" runat="server" Text="Submit Report" CssClass="asp-button" OnClick="btnSubmitReport_Click"/>
            </div>

            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            </div>
        </div>
 























    



</asp:Content>

