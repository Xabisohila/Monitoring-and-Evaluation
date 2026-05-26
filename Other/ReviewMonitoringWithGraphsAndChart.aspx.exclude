<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="ReviewMonitoring.aspx.cs" Inherits="ReviewMonitoring" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />







    <style type="text/css">
        .status-green { background-color: #d4edda; }
        .status-yellow { background-color: #fff3cd; }
        .status-red { background-color: #f8d7da; }
        .summary-box { padding: 10px; margin-bottom: 10px; border: 1px solid #ccc; }

        .filter-panel {
            margin-bottom: 25px;
            padding: 15px;
            background-color: #f0fff0; /* light green background */
            border: 1px solid #a3e6a3; /* green border */
            border-radius: 6px;
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            align-items: center;
        }

            .filter-panel label {
                margin-right: 5px;
                font-weight: bold;
                color: #03AC13;
            }

        .filter-panel-width {
            margin-bottom: 25px;
            padding: 20px;
            /*background-color: #f0fff0;*/ /* light green background */
            /*border: 1px solid #a3e6a3;*/ /* green border */
            border-radius: 6px;
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            align-items: center;
        }

        .asp-dropdown {
            padding: 8px;
            border-radius: 4px;
            border: 1px solid #ccc;
            min-width: 200px;
        }

        .asp-button {
            padding: 10px 20px;
            background-color: #03AC13;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 1em;
        }

            .asp-button:hover {
                background-color: #028a10;
            }
    </style>

    <br /><br /><br /><br />

    <%--<h2>Review & Monitoring Dashboard</h2>--%>

    <div class="section-title text-center">
        <br />
        <div>
            <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Review & Monitoring</strong></span></h2>
        </div>
    </div>

    <%--<div class="filter-panel-width">--%>
        <div class="filter-panel">
            
<!-- Add this DropDownList to ReviewMonitoring.aspx where other filters are located -->
<asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" CssClass="form-control" />
            <asp:DropDownList ID="ddlIntervention" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIntervention_SelectedIndexChanged" CssClass="asp-dropdown"/>
            <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" CssClass="asp-dropdown"/>
            <asp:DropDownList ID="ddlQuarter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQuarter_SelectedIndexChanged" CssClass="asp-dropdown">
                <asp:ListItem Text="All Quarters" Value="" />
                <asp:ListItem Text="Q1" Value="Q1" />
                <asp:ListItem Text="Q2" Value="Q2" />
                <asp:ListItem Text="Q3" Value="Q3" />
                <asp:ListItem Text="Q4" Value="Q4" />
            </asp:DropDownList>
        </div>
    <%--</div>--%>

    <br />

    <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="gvReports_RowDataBound">
        <Columns>
            <asp:BoundField DataField="InterventionName" HeaderText="Intervention" />
            <asp:BoundField DataField="Quarter" HeaderText="Quarter" />
            <asp:BoundField DataField="PlannedExpenditure" HeaderText="Planned (R)" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="ActualExpenditure" HeaderText="Actual (R)" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="PerformancePlannedValue" HeaderText="Target" />
            <asp:BoundField DataField="PerformanceActualValue" HeaderText="Achieved" />
            <asp:BoundField DataField="DeviationPercent" HeaderText="Deviation (%)" DataFormatString="{0:F2}" />
            <asp:HyperLinkField DataTextField="UploadFilePath" HeaderText="Document" DataNavigateUrlFields="UploadFilePath" Text="View" Target="_blank" />
        </Columns>
    </asp:GridView>

    <br />
    <asp:Label ID="lblSummary" runat="server" CssClass="summary-box" />

    <br /><br /><br />

    <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" CssClass="asp-button" OnClick="btnExportExcel_Click"  style="align-self:end"/>
    <asp:Button ID="btnExportPDF" runat="server" Text="Export to PDF" CssClass="asp-button" OnClick="btnExportPDF_Click" />

    <br /><br />

    <!-- Place somewhere inside Content2 (e.g., after filter-panel) -->
<asp:Chart ID="PerformanceChart" runat="server" Width="700px" Height="400px" Visible="true">
  <ChartAreas>
    <asp:ChartArea Name="ChartArea1" />
  </ChartAreas>
  <Series>
    <asp:Series Name="Series1" ChartType="Column" ChartArea="ChartArea1"
                XValueMember="X" YValueMembers="Y" />
  </Series>
  <Legends>
    <asp:Legend Name="Legend1" />
  </Legends>
</asp:Chart>

</asp:Content>

