<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true"
    CodeFile="i_POAList.aspx.cs" Inherits="i_POAList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .poa-page { padding: 80px 0 60px; }

    /* header row */
    .page-header-row {
        display:flex; align-items:center; justify-content:space-between;
        flex-wrap:wrap; gap:12px; margin-bottom:24px;
    }
    .page-header-row h2 { margin:0; font-size:24px; font-weight:700; color:#1a2b4a; }
    .page-header-row p  { color:#64748b; font-size:13px; margin:4px 0 0; }

    /* stat pill */
    .stat-pill {
        background:#fff; border:1px solid #dce3ec; border-radius:10px;
        padding:14px 24px; min-width:150px; text-align:center;
        box-shadow:0 1px 6px rgba(0,0,0,.05);
    }
    .stat-pill .num { font-size:32px; font-weight:700; color:#1a2b4a; line-height:1; }
    .stat-pill .lbl { font-size:12px; color:#64748b; margin-top:5px; }

    /* table */
    .table-wrap {
        background:#fff; border:1px solid #dce3ec; border-radius:12px;
        box-shadow:0 2px 12px rgba(0,0,0,.06); overflow:hidden;
    }
    .poa-table { width:100%; border-collapse:collapse; }
    .poa-table th {
        padding:12px 16px; background:#f8fafc; font-size:11px; font-weight:700;
        text-transform:uppercase; letter-spacing:.4px; color:#64748b;
        border-bottom:2px solid #e2e8f0; text-align:left;
    }
    .poa-table td {
        padding:14px 16px; border-bottom:1px solid #f1f5f9;
        font-size:13px; color:#334155; vertical-align:middle;
    }
    .poa-table tr:last-child td { border-bottom:none; }
    .poa-table tr:hover td { background:#fafbfc; }

    /* name cell */
    .poa-name    { font-weight:600; color:#1a2b4a; font-size:14px; }
    .poa-outcome { font-size:11px; color:#94a3b8; margin-top:3px; max-width:280px; }

    /* cluster/priority pill */
    .tag-cluster  { display:inline-block; padding:3px 10px; border-radius:20px; font-size:11px; font-weight:600; background:#eff6ff; color:#1d4ed8; border:1px solid #bfdbfe; }
    .tag-priority { display:inline-block; padding:3px 10px; border-radius:20px; font-size:11px; font-weight:600; background:#f0fdf4; color:#166534; border:1px solid #bbf7d0; }

    /* period */
    .period-badge { font-size:12px; font-weight:600; color:#475569; }

    /* action buttons */
    .btn-view {
        display:inline-block; padding:6px 14px; font-size:12px; font-weight:600;
        border-radius:7px; border:1px solid #0b5ed7; color:#0b5ed7;
        background:#eff6ff; text-decoration:none; margin-right:6px;
    }
    .btn-view:hover { background:#0b5ed7; color:#fff; text-decoration:none; }
    .btn-edit {
        display:inline-block; padding:6px 14px; font-size:12px; font-weight:600;
        border-radius:7px; border:1px solid #dce3ec; color:#475569;
        background:#f8fafc; text-decoration:none;
    }
    .btn-edit:hover { background:#e2e8f0; color:#1a2b4a; text-decoration:none; }

    /* create button */
    .btn-create {
        display:inline-block; padding:9px 20px; font-size:13px; font-weight:600;
        border-radius:8px; background:#1d6f42; color:#fff; text-decoration:none;
    }
    .btn-create:hover { background:#155a34; color:#fff; text-decoration:none; }

    .empty-msg { padding:40px; text-align:center; color:#94a3b8; font-size:14px; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="poa-page">
<div class="container">

    <%-- Header --%>
    <div class="page-header-row">
        <div>
            <h2>Programmes of Action</h2>
            <p>All POAs created from PMTDP priorities. Click a row to view or edit.</p>
        </div>
        <div style="display:flex;align-items:center;gap:20px;flex-wrap:wrap;">
            <div class="stat-pill">
                <div class="num"><asp:Label ID="lblTotal" runat="server" Text="0" /></div>
                <div class="lbl">Total POAs</div>
            </div>
            <a href="pageAddPOA.aspx" class="btn-create">+ Create POA</a>
        </div>
    </div>

    <%-- Table --%>
    <div class="table-wrap">
    <asp:GridView ID="gvPOAs" runat="server"
        AutoGenerateColumns="false"
        CssClass="poa-table"
        GridLines="None"
        DataKeyNames="POA_ID"
        EmptyDataText="">
        <EmptyDataTemplate>
            <div class="empty-msg">No Programmes of Action have been created yet.<br />
                <a href="i_PMTDPPriorityList.aspx" style="color:#0b5ed7;">Assign clusters to priorities</a>
                first, then create a POA.
            </div>
        </EmptyDataTemplate>
        <Columns>

            <%-- 1. Cluster --%>
            <asp:TemplateField HeaderText="Cluster">
                <ItemTemplate>
                    <span class="tag-cluster"><%# Eval("ClusterName") ?? "—" %></span>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 2. Priority --%>
            <asp:TemplateField HeaderText="PMTDP Priority">
                <ItemTemplate>
                    <span class="tag-priority"><%# Eval("PriorityName") ?? "—" %></span>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 3. POA name + outcome --%>
            <asp:TemplateField HeaderText="Programme of Action">
                <ItemTemplate>
                    <div class="poa-name"><%# Eval("POA_Name") %></div>
                    <div class="poa-outcome"><%# Eval("DesiredOutcome") %></div>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 4. Period --%>
            <asp:TemplateField HeaderText="Period">
                <ItemTemplate>
                    <span class="period-badge"><%# Eval("POA_StartYear") %> &ndash; <%# Eval("POA_EndYear") %></span>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 5. Actions --%>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <a href='<%# "pagePOADetail.aspx?id=" + Eval("POA_ID") %>' class="btn-view">View</a>
                    <a href='<%# "pageEditPOA.aspx?id=" + Eval("POA_ID") %>' class="btn-edit">Edit</a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    </div>

</div>
</div>
</asp:Content>
