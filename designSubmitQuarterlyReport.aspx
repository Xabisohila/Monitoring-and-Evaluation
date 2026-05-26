<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designSubmitQuarterlyReport.aspx.cs" Inherits="designSubmitQuarterlyReport" %>
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
    </style>








    <div class="section-title text-center">
        <br />
        <div>
            <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Submit Quarterly Report</strong></span></h2>
        </div>
    </div>




        <div style="max-width: 600px; margin: auto;">
            <%--<h2>Submit Quarterly Report</h2>--%>

            <div class="form-group">
                <label for="ddlIntervention">Intervention</label>
                <asp:DropDownList ID="ddlIntervention" runat="server" CssClass="form-control" />
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

            <div class="form-group">
                <label for="txtActualExpenditure">Actual Expenditure (R)</label>
                <asp:TextBox ID="txtActualExpenditure" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtPlannedExpenditure">Planned Expenditure (R)</label>
                <asp:TextBox ID="txtPlannedExpenditure" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtPerformanceActual">Performance Actual Value</label>
                <asp:TextBox ID="txtPerformanceActual" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtPerformancePlanned">Performance Planned Value</label>
                <asp:TextBox ID="txtPerformancePlanned" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtDeviationExplanation">Deviation Explanation</label>
                <asp:TextBox ID="txtDeviationExplanation" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="fileUpload">Upload Supporting Document</label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <asp:Button ID="btnSubmitReport" runat="server" Text="Submit Report" CssClass="btn btn-primary" OnClick="btnSubmitReport_Click" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            </div>
        </div>
 









</asp:Content>

