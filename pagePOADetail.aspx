<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pagePOADetail.aspx.cs" Inherits="pagePOADetail" %>
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
    --danger:  #dc3545;
    --radius:  12px;
    --shadow:  0 2px 12px rgba(0,0,0,.07);
    --font:    "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }

/* ── Page header ───────────────────────────────────────────── */
.poa-header { padding: 40px 0 24px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.poa-header .back-link {
    display: inline-flex; align-items: center; gap: 6px;
    font-size: 13px; font-weight: 600; color: var(--blue);
    text-decoration: none; margin-bottom: 14px;
}
.poa-header .back-link:hover { text-decoration: underline; }
.poa-header-row { display: flex; justify-content: space-between; align-items: flex-start; gap: 12px; }
.poa-header h2 { margin: 0 0 0; font-size: 26px; font-weight: 800; color: var(--text); }
.poa-header .period-badge {
    display: inline-block; padding: 3px 14px; border-radius: 20px;
    background: #e0f2fe; color: #0369a1; font-size: 12px; font-weight: 700;
    letter-spacing: .4px; margin-left: 10px; vertical-align: middle;
}
.btn-edit-poa {
    display: inline-block; padding: 7px 16px; border-radius: 8px;
    font-size: 13px; font-weight: 600; text-decoration: none;
    background: #fff; color: var(--text); border: 1.5px solid var(--border);
    transition: background .15s; flex-shrink: 0; white-space: nowrap;
}
.btn-edit-poa:hover { background: #f1f5f9; border-color: #aab; text-decoration: none; color: var(--text); }

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
    letter-spacing: .5px; color: var(--muted); margin-bottom: 6px;
}
.info-card .val { font-size: 14px; color: var(--text); line-height: 1.5; }
.info-card .val a { color: var(--blue); text-decoration: none; font-weight: 600; }
.info-card .val a:hover { text-decoration: underline; }
.align-row { margin-bottom: 8px; }
.align-row:last-child { margin-bottom: 0; }
.align-lbl {
    font-size: 11px; color: var(--muted); font-weight: 600;
    text-transform: uppercase; letter-spacing: .4px;
}

/* ── Section heading ───────────────────────────────────────── */
.section-heading {
    display: flex; align-items: center; justify-content: space-between;
    margin: 32px 0 16px;
}
.section-heading h3 {
    margin: 0; font-size: 18px; font-weight: 700; color: var(--text);
    border-left: 4px solid var(--green); padding-left: 12px;
}
.btn-add {
    display: inline-block; padding: 8px 18px; font-size: 13px; font-weight: 600;
    border-radius: 8px; background: var(--green); color: #fff;
    text-decoration: none; transition: background .15s;
}
.btn-add:hover { background: #155a34; color: #fff; text-decoration: none; }

/* ── Intervention card ─────────────────────────────────────── */
.interv-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    margin-bottom: 20px; overflow: hidden;
}
.interv-header {
    padding: 14px 20px; background: #f1f5f9;
    border-bottom: 1px solid var(--border);
    display: flex; align-items: center; justify-content: space-between; gap: 12px;
    flex-wrap: wrap;
}
.interv-header h4 { margin: 0; font-size: 15px; font-weight: 700; color: var(--text); flex: 1; min-width: 0; }
.interv-header h4 a { color: var(--blue); text-decoration: none; }
.interv-header h4 a:hover { text-decoration: underline; }
.interv-header-right { display: flex; align-items: center; gap: 8px; flex-wrap: wrap; flex-shrink: 0; }
.period-tag { font-size: 11px; color: var(--muted); white-space: nowrap; }
.act-btn {
    display: inline-block; padding: 4px 10px; border-radius: 6px;
    font-size: 11px; font-weight: 600; text-decoration: none;
    background: #fff; color: var(--text); border: 1px solid var(--border);
    transition: background .15s; white-space: nowrap;
}
.act-btn:hover { background: #e2e8f0; color: var(--text); text-decoration: none; }
.act-btn-green { background: #f0fdf4; color: var(--green); border-color: #86efac; }
.act-btn-green:hover { background: #dcfce7; color: var(--green); }

.interv-body { padding: 16px 20px; }

/* ── Meta-grid ─────────────────────────────────────────────── */
.meta-grid {
    display: grid; grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
    gap: 8px 24px; margin-bottom: 16px;
}
.meta-item .key {
    font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .4px; color: var(--muted);
}
.meta-item .val { font-size: 13px; color: var(--text); word-break: break-word; }

/* ── Sub-section labels ────────────────────────────────────── */
.sub-heading {
    font-size: 12px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .5px; color: var(--muted);
    padding: 10px 0 6px; border-top: 1px solid #edf2f7; margin-top: 8px;
}

/* ── Nested tables ─────────────────────────────────────────── */
.tbl-wrap { overflow-x: auto; margin-bottom: 12px; }
.nested-tbl {
    width: 100%; border-collapse: collapse; font-size: 12px;
}
.nested-tbl th {
    padding: 8px 11px; font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .4px; color: #fff; background: #334155;
    border-right: 1px solid rgba(255,255,255,.12); white-space: nowrap;
}
.nested-tbl th:first-child { border-radius: 6px 0 0 0; }
.nested-tbl th:last-child  { border-radius: 0 6px 0 0; border-right: none; }
.nested-tbl td {
    padding: 7px 11px; border-bottom: 1px solid #edf2f7;
    border-right: 1px solid #f1f5f9; color: #334155;
    white-space: nowrap;
}
.nested-tbl td.wrap-col { white-space: normal; min-width: 120px; max-width: 220px; word-break: break-word; }
.nested-tbl tr:last-child td { border-bottom: none; }
.nested-tbl tr:nth-child(even) td { background: #f8fafc; }
.nested-tbl .empty-row td {
    text-align: center; color: var(--muted); font-style: italic; padding: 12px;
    white-space: normal;
}

/* ── Error banner ──────────────────────────────────────────── */
.error-banner {
    background: #fee2e2; border: 1px solid #fca5a5; border-left: 5px solid #ef4444;
    border-radius: var(--radius); padding: 16px 20px; margin: 30px 0;
    color: #7f1d1d; font-size: 14px;
}

/* ── No-interventions notice ───────────────────────────────── */
.empty-notice {
    background: #f8fafc; border: 1px dashed var(--border);
    border-radius: var(--radius); padding: 24px; text-align: center;
    color: var(--muted); font-size: 14px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

<div class="container" style="padding-bottom:60px; max-width:1100px; margin:0 auto;">

    <%-- Error panel (shown on failure) --%>
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <div class="error-banner">
            <strong>Error:</strong> <asp:Label ID="lblError" runat="server" />
        </div>
    </asp:Panel>

    <%-- Main content --%>
    <asp:Panel ID="pnlContent" runat="server">

        <%-- Page header --%>
        <div class="poa-header">
            <asp:HyperLink ID="hlBackToOverview" runat="server"
                CssClass="back-link">&#8592; Back to Planning Overview</asp:HyperLink>
            <div class="poa-header-row">
                <h2>
                    <asp:Label ID="lblPOAName" runat="server" />
                    <span class="period-badge"><asp:Label ID="lblPOAPeriod" runat="server" /></span>
                </h2>
                <asp:HyperLink ID="hlEditPOA" runat="server" CssClass="btn-edit-poa">
                    &#9998; Edit POA
                </asp:HyperLink>
            </div>
        </div>

        <%-- Info cards: Description | Desired Outcome | Strategic Alignment --%>
        <div class="info-grid">

            <div class="info-card">
                <div class="lbl">Description</div>
                <div class="val"><asp:Label ID="lblDescription" runat="server" /></div>
            </div>

            <div class="info-card">
                <div class="lbl">Desired Outcome</div>
                <div class="val"><asp:Label ID="lblDesiredOutcome" runat="server" /></div>
            </div>

            <div class="info-card">
                <div class="lbl">Strategic Alignment</div>
                <div class="val">
                    <div class="align-row">
                        <div class="align-lbl">PMTDP Priority</div>
                        <asp:Label ID="lblPMTDP" runat="server" />
                    </div>
                    <div class="align-row">
                        <div class="align-lbl">Cluster</div>
                        <asp:Label ID="lblCluster" runat="server" />
                    </div>
                    <div class="align-row">
                        <div class="align-lbl">Provincial Development Plan</div>
                        <asp:HyperLink ID="hlPDP" runat="server" />
                    </div>
                </div>
            </div>

        </div>

        <%-- Interventions section heading --%>
        <div class="section-heading">
            <h3>Interventions</h3>
            <asp:HyperLink ID="lnkAddIntervention" runat="server" CssClass="btn-add">
                + Add Intervention
            </asp:HyperLink>
        </div>

        <%-- No-interventions notice --%>
        <asp:Panel ID="pnlNoInterventions" runat="server" Visible="false">
            <div class="empty-notice">No interventions have been added to this Programme of Action yet.</div>
        </asp:Panel>

        <%-- Interventions repeater --%>
        <asp:Repeater ID="rptInterventions" runat="server"
            OnItemDataBound="rptInterventions_ItemDataBound">
            <ItemTemplate>
                <div class="interv-card">

                    <%-- Card header --%>
                    <div class="interv-header">
                        <h4>
                            <asp:HyperLink ID="hlInterventionName" runat="server"
                                Text='<%# Eval("InterventionName") %>'
                                NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>' />
                        </h4>
                        <div class="interv-header-right">
                            <span class="period-tag">
                                <%# Eval("InterventionStartYear") %> &ndash; <%# Eval("InterventionEndYear") %>
                            </span>
                            <a href='<%# "pageEditIntervention.aspx?id=" + Eval("InterventionID") %>' class="act-btn">Edit</a>
                            <a href='<%# "pageAddIndicator.aspx?interventionId=" + Eval("InterventionID") + "&poaId=" + CurrentPoaId %>' class="act-btn act-btn-green">+ Indicator</a>
                            <a href='<%# "pageAddBudget.aspx?interventionId=" + Eval("InterventionID") %>' class="act-btn act-btn-green">+ Budget</a>
                        </div>
                    </div>

                    <div class="interv-body">

                        <%-- Metadata grid --%>
                        <div class="meta-grid">
                            <div class="meta-item">
                                <div class="key">Description</div>
                                <div class="val"><%# SafeField(Container.DataItem, "InterventionDescription") %></div>
                            </div>
                            <div class="meta-item">
                                <div class="key">Lead Institution</div>
                                <div class="val"><%# SafeField(Container.DataItem, "ImplementationInstitution") %></div>
                            </div>
                            <div class="meta-item">
                                <div class="key">Working Group</div>
                                <div class="val"><%# SafeField(Container.DataItem, "WorkingGroup") %></div>
                            </div>
                            <div class="meta-item">
                                <div class="key">Sub-Outcome</div>
                                <div class="val"><%# SafeField(Container.DataItem, "SubOutcome") %></div>
                            </div>
                            <div class="meta-item">
                                <div class="key">Municipality</div>
                                <div class="val"><%# SafeField(Container.DataItem, "PrimaryMunicipality") %></div>
                            </div>
                            <div class="meta-item">
                                <div class="key">Spatial Reference</div>
                                <div class="val"><%# SafeField(Container.DataItem, "SpatialReference") %></div>
                            </div>
                        </div>

                        <%-- Indicators --%>
                        <div class="sub-heading">Indicators</div>
                        <div class="tbl-wrap">
                            <asp:GridView ID="gvInterventionIndicators" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="nested-tbl"
                                GridLines="None"
                                EmptyDataText="No indicators recorded.">
                                <EmptyDataRowStyle CssClass="empty-row" />
                                <Columns>
                                    <asp:BoundField DataField="IndicatorName"         HeaderText="Indicator"       ItemStyle-CssClass="wrap-col" />
                                    <asp:BoundField DataField="IndicatorType"         HeaderText="Type" />
                                    <asp:BoundField DataField="UnitOfMeasure"         HeaderText="Unit" />
                                    <asp:BoundField DataField="BaselineValue"         HeaderText="Baseline"        DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="BaselineYear"          HeaderText="Baseline Yr" />
                                    <asp:BoundField DataField="TargetValue"           HeaderText="Target"          DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="TargetYear"            HeaderText="Target Yr" />
                                    <asp:BoundField DataField="Target2030_TermTarget" HeaderText="2030 Term Target" ItemStyle-CssClass="wrap-col" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <%-- Budgets --%>
                        <div class="sub-heading">Budget</div>
                        <div class="tbl-wrap">
                            <asp:GridView ID="gvInterventionBudgets" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="nested-tbl"
                                GridLines="None"
                                EmptyDataText="No budget entries recorded.">
                                <EmptyDataRowStyle CssClass="empty-row" />
                                <Columns>
                                    <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                                    <asp:BoundField DataField="AnnualBudget"  HeaderText="Annual Budget"  DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="TermBudget"    HeaderText="Term Budget"    DataFormatString="{0:N2}" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div><%-- /interv-body --%>
                </div><%-- /interv-card --%>
            </ItemTemplate>
        </asp:Repeater>

    </asp:Panel><%-- /pnlContent --%>

</div>
</asp:Content>
