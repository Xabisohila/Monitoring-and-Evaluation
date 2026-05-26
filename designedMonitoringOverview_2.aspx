<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedMonitoringOverview_2.aspx.cs" Inherits="designedMonitoringOverview_2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <%--<title>Monitoring Overview</title>
    <link href="h ttps://cdn.jsdelivr.net/npm/bootstrap@5.3.0otstrap.min.css
    h ttps://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />











        <div class="container mt-4">
            <h2>Monitoring Overview</h2>

            <!-- Filter Panel -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlCluster" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlWorkGroup" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-select" />
                </div>
            </div>

            <div class="mb-3">
                <asp:Button ID="btnViewMonitoring" runat="server" Text="View Monitoring" CssClass="btn btn-primary" OnClick="btnViewMonitoring_Click" />
                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" CssClass="btn btn-success ms-2" OnClick="btnExportExcel_Click" />

                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false" />

            <!-- SubOutcome Accordion -->
            <div class="accordion" id="accordionSubOutcomes">
                <asp:Repeater ID="rptSubOutcomes" runat="server" OnItemDataBound="rptSubOutcomes_ItemDataBound">
                    <ItemTemplate>
                        <div class="accordion-item">
                            <h2 class="accordion-header" id='<%# "heading" + Eval("SubOutcomeId") %>'>
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target='<%# "#collapse" + Eval("SubOutcomeId") %>'
                                        aria-expanded="false" aria-controls='<%# "collapse" + Eval("SubOutcomeId") %>'>
                                    <%# Eval("SubOutcome") %>
                                </button>
                            </h2>
                            <div id='<%# "collapse" + Eval("SubOutcomeId") %>' class="accordion-collapse collapse"
                                 aria-labelledby='<%# "heading" + Eval("SubOutcomeId") %>' data-bs-parent="#accordionSubOutcomes">
                                <div class="accordion-body">
                                    <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Intervention Title">
                                                <ItemTemplate>
                                                    <a>
                                                        <%# Eval("InterventionName") %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InterventionDescription" HeaderText="Description" />
                                            <asp:BoundField DataField="LeadInstitution" HeaderText="Institution" />
                                            <asp:BoundField DataField="Municipality" HeaderText="Municipality" />
                                            <asp:BoundField DataField="StartYear" HeaderText="Start Year" />
                                            <asp:BoundField DataField="EndYear" HeaderText="End Year" />
                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="gvIndicators" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-striped mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                            <asp:BoundField DataField="Baseline" HeaderText="Baseline" />
                                            <asp:BoundField DataField="BaselineYear" HeaderText="Year" />
                                            <asp:BoundField DataField="Target2025" HeaderText="Target 2025" />
                                            <asp:BoundField DataField="Target2030" HeaderText="Target 2030" />
                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="gvBudgets" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="FY" HeaderText="Financial Year" />
                                            <asp:BoundField DataField="AnnualBudget" HeaderText="Annual Budget" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="TermBudget" HeaderText="Term Budget" DataFormatString="{0:C}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>















</asp:Content>

