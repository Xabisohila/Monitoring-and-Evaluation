<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="detailsPDP.aspx.cs" Inherits="detailsPDP" %>
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
.page-header h2 { margin: 0; font-size: 26px; font-weight: 800; color: var(--text); }
.period-badge {
    display: inline-block; padding: 3px 14px; border-radius: 20px;
    background: #e0f2fe; color: #0369a1; font-size: 12px; font-weight: 700;
    letter-spacing: .4px; margin-left: 10px; vertical-align: middle;
}

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
    letter-spacing: .5px; color: var(--muted); margin-bottom: 8px;
}
.info-card .val { font-size: 14px; color: var(--text); line-height: 1.5; }

/* ── Section heading ───────────────────────────────────────── */
.section-heading { margin: 28px 0 16px; }
.section-heading h3 {
    margin: 0; font-size: 18px; font-weight: 700; color: var(--text);
    border-left: 4px solid var(--green); padding-left: 12px;
}

/* ── Priority card ─────────────────────────────────────────── */
.priority-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    margin-bottom: 20px; overflow: hidden;
}
.priority-header {
    padding: 14px 20px; background: #1e3a5f;
    display: flex; align-items: center; gap: 10px;
}
.priority-header h4 { margin: 0; font-size: 15px; font-weight: 700; color: #fff; }
.priority-body { padding: 0 20px 20px; }
.priority-meta { padding: 14px 0 10px; border-bottom: 1px solid #edf2f7; margin-bottom: 16px; }
.priority-meta .meta-row { margin-bottom: 6px; }
.priority-meta .meta-key {
    font-size: 11px; color: var(--muted); font-weight: 600;
    text-transform: uppercase; letter-spacing: .4px;
}
.priority-meta .meta-val { font-size: 13px; color: var(--text); }

/* ── POA item ──────────────────────────────────────────────── */
.poa-item { margin-bottom: 20px; }
.poa-item:last-child { margin-bottom: 0; }
.poa-header {
    display: flex; align-items: center; gap: 12px;
    padding: 10px 0 8px; border-bottom: 1px solid #edf2f7;
    margin-bottom: 10px; flex-wrap: wrap;
}
.poa-header a {
    font-size: 14px; font-weight: 700; color: var(--blue);
    text-decoration: none;
}
.poa-header a:hover { text-decoration: underline; }
.poa-meta { font-size: 12px; color: var(--muted); }
.poa-sub-label {
    font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .5px; color: var(--muted);
    padding: 6px 0 6px; margin-bottom: 6px;
}

/* ── Nested table ──────────────────────────────────────────── */
.tbl-wrap { overflow-x: auto; }
.nested-tbl {
    width: 100%; border-collapse: collapse; font-size: 12px;
    border: 1px solid var(--border); border-radius: 8px; overflow: hidden;
}
.nested-tbl th {
    padding: 8px 12px; font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .4px; color: #fff; background: #334155;
    border-right: 1px solid rgba(255,255,255,.12); white-space: nowrap;
}
.nested-tbl th:last-child { border-right: none; }
.nested-tbl td {
    padding: 7px 12px; border-bottom: 1px solid #edf2f7;
    border-right: 1px solid #f1f5f9; color: #334155; white-space: nowrap;
}
.nested-tbl td.wrap-col { white-space: normal; min-width: 120px; max-width: 200px; word-break: break-word; }
.nested-tbl tr:last-child td { border-bottom: none; }
.nested-tbl tr:nth-child(even) td { background: #f8fafc; }
.nested-tbl .empty-row td {
    text-align: center; color: var(--muted); font-style: italic;
    padding: 12px; white-space: normal;
}
.nested-tbl a { color: var(--blue); text-decoration: none; font-weight: 600; }
.nested-tbl a:hover { text-decoration: underline; }

/* ── Empty notice ──────────────────────────────────────────── */
.empty-notice {
    background: #f8fafc; border: 1px dashed var(--border);
    border-radius: var(--radius); padding: 20px; text-align: center;
    color: var(--muted); font-size: 13px; margin-top: 10px;
}

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
            <asp:HyperLink ID="hlBackLink" runat="server" CssClass="back-link">&#8592; Back</asp:HyperLink>
            <h2>
                <asp:Label ID="lblPDPName" runat="server" />
                <span class="period-badge"><asp:Label ID="lblPDPPeriod" runat="server" /></span>
            </h2>
        </div>

        <%-- Info cards --%>
        <div class="info-grid">
            <div class="info-card">
                <div class="lbl">Vision</div>
                <div class="val"><asp:Label ID="lblPDPVision" runat="server" /></div>
            </div>
            <div class="info-card">
                <div class="lbl">Desired Outcome</div>
                <div class="val"><asp:Label ID="lblPDPDescription" runat="server" /></div>
            </div>
        </div>

        <%-- PMTDP Priorities --%>
        <div class="section-heading">
            <h3>PMTDP Priorities</h3>
        </div>

        <asp:Repeater ID="rptPMTDPPriorities" runat="server"
            OnItemDataBound="rptPMTDPPriorities_ItemDataBound">
            <ItemTemplate>
                <div class="priority-card">

                    <div class="priority-header">
                        <h4><asp:Label ID="lblPMTDPName" runat="server"
                                Text='<%# Eval("PriorityName") %>' /></h4>
                    </div>

                    <div class="priority-body">

                        <%-- Priority meta --%>
                        <div class="priority-meta">
                            <div class="meta-row">
                                <div class="meta-key">Description</div>
                                <div class="meta-val"><%# Eval("PriorityDescription") %></div>
                            </div>
                            <div class="meta-row" style="margin-top:6px;">
                                <div class="meta-key">Desired Outcome</div>
                                <div class="meta-val"><%# Eval("PriorityDesiredOutcome") %></div>
                            </div>
                        </div>

                        <%-- POAs --%>
                        <asp:Repeater ID="rptPOAs" runat="server"
                            OnItemDataBound="rptPOAs_ItemDataBound">
                            <ItemTemplate>
                                <div class="poa-item">

                                    <div class="poa-header">
                                        <asp:HyperLink ID="hlPOAName" runat="server"
                                            Text='<%# Eval("POA_Name") %>'
                                            NavigateUrl='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>' />
                                        <span class="poa-meta">
                                            <%# Eval("POA_StartYear") %> &ndash; <%# Eval("POA_EndYear") %>
                                            &nbsp;&bull;&nbsp; <%# Eval("ClusterName") %>
                                        </span>
                                    </div>

                                    <%-- Desired Outcome for POA --%>
                                    <div style="font-size:12px; color:var(--muted); margin-bottom:10px;">
                                        <%# Eval("POADesiredOutcome") %>
                                    </div>

                                    <%-- Interventions --%>
                                    <div class="poa-sub-label">Interventions</div>
                                    <div class="tbl-wrap">
                                        <asp:GridView ID="gvInterventions" runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass="nested-tbl"
                                            GridLines="None"
                                            EmptyDataText="No interventions.">
                                            <EmptyDataRowStyle CssClass="empty-row" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Intervention">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlInterventionName" runat="server"
                                                            Text='<%# Eval("InterventionName") %>'
                                                            NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ImplementationInstitution" HeaderText="Institution"  ItemStyle-CssClass="wrap-col" />
                                                <asp:BoundField DataField="WorkingGroup"              HeaderText="Work Group" />
                                                <asp:BoundField DataField="PrimaryMunicipality"       HeaderText="Municipality" />
                                                <asp:TemplateField HeaderText="Period">
                                                    <ItemTemplate>
                                                        <%# Eval("InterventionStartYear") %> &ndash; <%# Eval("InterventionEndYear") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div><%-- /priority-body --%>
                </div><%-- /priority-card --%>
            </ItemTemplate>
        </asp:Repeater>

    </asp:Panel><%-- /pnlContent --%>

</div>
</asp:Content>
