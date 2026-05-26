<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageInterventionsDirectDetail.aspx.cs" Inherits="pageInterventionsDirectDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
:root {
    --bg:      #f7f8fb;
    --surface: #ffffff;
    --border:  #dce3ec;
    --text:    #1a2b4a;
    --muted:   #64748b;
    --green:   #1d6f42;
    --blue:    #0b5ed7;
    --radius:  12px;
    --shadow:  0 2px 12px rgba(0,0,0,.07);
    --font:    "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }

/* ── Page header ───────────────────────────────────────────── */
.page-header { padding: 40px 0 24px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.page-header .back-link {
    display: inline-flex; align-items: center; gap: 6px;
    font-size: 13px; font-weight: 600; color: var(--blue);
    text-decoration: none; margin-bottom: 14px;
}
.page-header .back-link:hover { text-decoration: underline; }
.header-row { display: flex; justify-content: space-between; align-items: flex-start; gap: 12px; flex-wrap: wrap; }
.page-header h2 { margin: 0; font-size: 26px; font-weight: 800; color: var(--text); }
.period-badge {
    display: inline-block; padding: 3px 14px; border-radius: 20px;
    background: #e0f2fe; color: #0369a1; font-size: 12px; font-weight: 700;
    letter-spacing: .4px; margin-left: 10px; vertical-align: middle;
}
.header-actions { display: flex; gap: 8px; flex-wrap: wrap; align-items: center; flex-shrink: 0; }

/* ── Info grid ─────────────────────────────────────────────── */
.info-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
    gap: 16px; margin-bottom: 28px;
}
.info-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    padding: 18px 20px;
}
.info-card .lbl {
    font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .5px; color: var(--muted); margin-bottom: 10px;
}
.info-card .val { font-size: 14px; color: var(--text); line-height: 1.5; }
.info-card .val a { color: var(--blue); text-decoration: none; font-weight: 600; }
.info-card .val a:hover { text-decoration: underline; }
.meta-row { margin-bottom: 10px; }
.meta-row:last-child { margin-bottom: 0; }
.meta-key {
    font-size: 11px; color: var(--muted); font-weight: 600;
    text-transform: uppercase; letter-spacing: .4px; margin-bottom: 2px;
}
.meta-val { font-size: 13px; color: var(--text); }

/* ── Section heading ───────────────────────────────────────── */
.section-heading { display: flex; align-items: center; margin: 28px 0 14px; }
.section-heading h3 {
    margin: 0; font-size: 18px; font-weight: 700; color: var(--text);
    border-left: 4px solid var(--green); padding-left: 12px;
}

/* ── Tables ────────────────────────────────────────────────── */
.tbl-wrap { overflow-x: auto; margin-bottom: 24px; }
.data-tbl {
    width: 100%; border-collapse: collapse; font-size: 13px;
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); overflow: hidden;
}
.data-tbl th {
    padding: 10px 14px; font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .4px; color: #fff; background: #334155;
    border-right: 1px solid rgba(255,255,255,.12); white-space: nowrap;
}
.data-tbl th:last-child { border-right: none; }
.data-tbl td {
    padding: 9px 14px; border-bottom: 1px solid #edf2f7;
    border-right: 1px solid #f1f5f9; color: #334155;
    white-space: nowrap;
}
.data-tbl td.wrap-col { white-space: normal; min-width: 140px; max-width: 240px; word-break: break-word; }
.data-tbl tr:last-child td { border-bottom: none; }
.data-tbl tr:nth-child(even) td { background: #f8fafc; }
.data-tbl .empty-row td {
    text-align: center; color: var(--muted); font-style: italic;
    padding: 16px; white-space: normal;
}

/* ── Buttons ───────────────────────────────────────────────── */
.btn-edit {
    display: inline-block; padding: 7px 16px; border-radius: 8px;
    font-size: 13px; font-weight: 600; text-decoration: none;
    background: #fff; color: var(--text); border: 1.5px solid var(--border);
    transition: background .15s; white-space: nowrap;
}
.btn-edit:hover { background: #f1f5f9; border-color: #aab; text-decoration: none; color: var(--text); }
.btn-add {
    display: inline-block; padding: 7px 16px; font-size: 13px; font-weight: 600;
    border-radius: 8px; background: var(--green); color: #fff;
    text-decoration: none; transition: background .15s; white-space: nowrap;
}
.btn-add:hover { background: #155a34; color: #fff; text-decoration: none; }

/* ── Error banner ──────────────────────────────────────────── */
.error-banner {
    background: #fee2e2; border: 1px solid #fca5a5; border-left: 5px solid #ef4444;
    border-radius: var(--radius); padding: 16px 20px; margin: 30px 0;
    color: #7f1d1d; font-size: 14px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

<div class="container" style="padding-bottom:60px; max-width:1100px; margin:0 auto;">

    <%-- Error panel --%>
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <div class="error-banner">
            <strong>Error:</strong> <asp:Label ID="lblError" runat="server" />
        </div>
    </asp:Panel>

    <%-- Main content --%>
    <asp:Panel ID="pnlContent" runat="server">

        <%-- Page header --%>
        <div class="page-header">
            <asp:HyperLink ID="hlBackToOverview" runat="server" CssClass="back-link">&#8592; Back</asp:HyperLink>
            <div class="header-row">
                <h2>
                    <asp:Label ID="lblInterventionName" runat="server" />
                    <span class="period-badge"><asp:Label ID="lblInterventionPeriod" runat="server" /></span>
                </h2>
                <div class="header-actions">
                    <asp:HyperLink ID="hlEditIntervention" runat="server" CssClass="btn-edit">&#9998; Edit</asp:HyperLink>
                    <asp:HyperLink ID="hlAddIndicator" runat="server" CssClass="btn-add">+ Indicator</asp:HyperLink>
                    <asp:HyperLink ID="hlAddBudget" runat="server" CssClass="btn-add">+ Budget</asp:HyperLink>
                </div>
            </div>
        </div>

        <%-- Info cards --%>
        <div class="info-grid">

            <div class="info-card">
                <div class="lbl">Description</div>
                <div class="val"><asp:Label ID="lblDescription" runat="server" /></div>
            </div>

            <div class="info-card">
                <div class="lbl">Details</div>
                <div class="val">
                    <div class="meta-row">
                        <div class="meta-key">Lead Institution</div>
                        <div class="meta-val"><asp:Label ID="lblLeadInstitution" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">Working Group</div>
                        <div class="meta-val"><asp:Label ID="lblWorkingGroup" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">Municipality</div>
                        <div class="meta-val"><asp:Label ID="lblMunicipality" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">Spatial Reference</div>
                        <div class="meta-val"><asp:Label ID="lblSpatialReference" runat="server" /></div>
                    </div>
                </div>
            </div>

            <div class="info-card">
                <div class="lbl">Strategic Alignment</div>
                <div class="val">
                    <div class="meta-row">
                        <div class="meta-key">Parent POA</div>
                        <div class="meta-val"><asp:HyperLink ID="hlPOA" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">Cluster</div>
                        <div class="meta-val"><asp:Label ID="lblCluster" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">PMTDP Priority</div>
                        <div class="meta-val"><asp:Label ID="lblPMTDP" runat="server" /></div>
                    </div>
                    <div class="meta-row">
                        <div class="meta-key">Aligned PDP</div>
                        <div class="meta-val"><asp:HyperLink ID="hlPDP" runat="server" /></div>
                    </div>
                </div>
            </div>

        </div>

        <%-- Indicators & Targets --%>
        <div class="section-heading">
            <h3>Indicators &amp; Targets</h3>
        </div>
        <div class="tbl-wrap">
            <asp:GridView ID="gvIndicators" runat="server" AutoGenerateColumns="false"
                CssClass="data-tbl" GridLines="None" EmptyDataText="No indicators recorded.">
                <EmptyDataRowStyle CssClass="empty-row" />
                <Columns>
                    <asp:BoundField DataField="IndicatorName"         HeaderText="Indicator"        ItemStyle-CssClass="wrap-col" />
                    <asp:BoundField DataField="IndicatorType"         HeaderText="Type" />
                    <asp:BoundField DataField="UnitOfMeasure"         HeaderText="Unit" />
                    <asp:BoundField DataField="BaselineValue"         HeaderText="Baseline"         DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="BaselineYear"          HeaderText="Baseline Yr" />
                    <asp:BoundField DataField="TargetValue"           HeaderText="Target"           DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="TargetYear"            HeaderText="Target Yr" />
                    <asp:BoundField DataField="Target2030_TermTarget" HeaderText="2030 Term Target" ItemStyle-CssClass="wrap-col" />
                </Columns>
            </asp:GridView>
        </div>

        <%-- Budget Allocation --%>
        <div class="section-heading">
            <h3>Budget Allocation</h3>
        </div>
        <div class="tbl-wrap">
            <asp:GridView ID="gvBudgets" runat="server" AutoGenerateColumns="false"
                CssClass="data-tbl" GridLines="None" EmptyDataText="No budget entries recorded.">
                <EmptyDataRowStyle CssClass="empty-row" />
                <Columns>
                    <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                    <asp:BoundField DataField="AnnualBudget"  HeaderText="Annual Budget"  DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="TermBudget"    HeaderText="Term Budget"    DataFormatString="{0:N2}" />
                </Columns>
            </asp:GridView>
        </div>

        <%-- Quarterly Reports --%>
        <div class="section-heading">
            <h3>Quarterly Reports</h3>
        </div>
        <div class="tbl-wrap">
            <asp:GridView ID="gvQuarterlyReports" runat="server" AutoGenerateColumns="false"
                CssClass="data-tbl" GridLines="None" EmptyDataText="No quarterly reports.">
                <EmptyDataRowStyle CssClass="empty-row" />
                <Columns>
                    <asp:BoundField DataField="FinancialYear"      HeaderText="FY" />
                    <asp:BoundField DataField="Quarter"            HeaderText="Quarter" />
                    <asp:BoundField DataField="ActualExpenditure"  HeaderText="Actual Spend"  DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="PlannedExpenditure" HeaderText="Planned Spend" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="BudgetDeviation"    HeaderText="Deviation"     DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="ReportingDate"      HeaderText="Report Date"   DataFormatString="{0:d}" />
                </Columns>
            </asp:GridView>
        </div>

    </asp:Panel><%-- /pnlContent --%>

</div>
</asp:Content>
