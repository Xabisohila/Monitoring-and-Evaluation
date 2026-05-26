<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true"
    CodeFile="i_PMTDPPriorityList.aspx.cs" Inherits="i_PMTDPPriorityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .pu-page { padding: 80px 0 60px; }

    /* stat pills */
    .stat-row  { display:flex; gap:16px; flex-wrap:wrap; margin-bottom:28px; }
    .stat-pill {
        background:#fff; border:1px solid #dce3ec; border-radius:10px;
        padding:16px 24px; min-width:150px; flex:1;
        box-shadow:0 1px 6px rgba(0,0,0,.05); text-align:center;
    }
    .stat-pill .num { font-size:32px; font-weight:700; color:#1a2b4a; line-height:1; }
    .stat-pill .lbl { font-size:12px; color:#64748b; margin-top:5px; }
    .stat-pill.warn .num { color:#b45309; }
    .stat-pill.ok   .num { color:#059669; }

    /* filter tabs */
    .filter-tabs { display:flex; gap:8px; margin-bottom:16px; }
    .tab-btn {
        padding:7px 18px; border-radius:20px; font-size:13px; font-weight:600;
        border:1px solid #dce3ec; background:#fff; color:#475569; cursor:pointer;
    }
    .tab-btn.active { background:#1a2b4a; color:#fff; border-color:#1a2b4a; }

    /* table wrapper */
    .table-wrap {
        background:#fff; border:1px solid #dce3ec; border-radius:12px;
        box-shadow:0 2px 12px rgba(0,0,0,.06);
    }
    .pri-table { width:100%; border-collapse:collapse; }
    .pri-table th {
        padding:12px 16px; background:#f8fafc; font-size:11px; font-weight:700;
        text-transform:uppercase; letter-spacing:.4px; color:#64748b;
        border-bottom:2px solid #e2e8f0; text-align:left;
    }
    .pri-table th:first-child { border-radius:12px 0 0 0; }
    .pri-table th:last-child  { border-radius:0 12px 0 0; }
    .pri-table td {
        padding:14px 16px; border-bottom:1px solid #f1f5f9;
        font-size:13px; color:#334155; vertical-align:middle;
    }
    .pri-table tr:last-child td { border-bottom:none; }
    .pri-table tr:hover td { background:#fafbfc; }

    /* priority cell */
    .pri-name  { font-weight:600; color:#1a2b4a; font-size:14px; margin-bottom:3px; }
    .pri-pdp   { font-size:11px; color:#94a3b8; }
    .pri-outcome {
        font-size:12px; color:#64748b; margin-top:5px;
        max-width:300px; line-height:1.4;
    }

    /* badges */
    .badge-no  {
        display:inline-block; padding:4px 12px; border-radius:20px;
        font-size:11px; font-weight:600;
        background:#fef3c7; color:#92400e; border:1px solid #fde68a;
    }
    .badge-yes {
        display:inline-block; padding:4px 12px; border-radius:20px;
        font-size:11px; font-weight:600;
        background:#d1fae5; color:#065f46; border:1px solid #a7f3d0;
    }

    /* cluster dropdown */
    .ddl-cluster {
        padding:7px 10px; border:1px solid #cbd5e1; border-radius:7px;
        font-size:13px; color:#334155; background:#fff; width:100%; max-width:220px;
    }
    .ddl-cluster:focus { outline:none; border-color:#0b5ed7; }

    /* buttons */
    .btn-save {
        padding:7px 16px; font-size:13px; font-weight:600; border-radius:7px;
        border:none; background:#1d6f42; color:#fff; cursor:pointer;
    }
    .btn-save:hover { background:#155a34; }
    .btn-poa {
        display:inline-block; padding:7px 14px; font-size:12px; font-weight:600;
        border-radius:7px; border:1px solid #0b5ed7; color:#0b5ed7;
        background:#eff6ff; text-decoration:none;
    }
    .btn-poa:hover { background:#0b5ed7; color:#fff; text-decoration:none; }
    .lnk-edit { font-size:12px; color:#94a3b8; margin-left:10px; text-decoration:none; }
    .lnk-edit:hover { color:#475569; }

    /* toast */
    #toast {
        display:none; position:fixed; bottom:28px; right:28px;
        background:#1a2b4a; color:#fff; padding:14px 22px; border-radius:10px;
        font-size:14px; font-weight:600; box-shadow:0 4px 20px rgba(0,0,0,.2); z-index:9999;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="pu-page">
<div class="container">

    <%-- Title --%>
    <div style="margin-bottom:24px;">
        <h2 style="margin:0;font-size:24px;font-weight:700;color:#1a2b4a;">PMTDP Priorities &mdash; Cluster Assignment</h2>
        <p style="color:#64748b;font-size:13px;margin:4px 0 0;">
            Assign each priority to a cluster before creating a POA.
        </p>
    </div>

    <%-- Stats --%>
    <div class="stat-row">
        <div class="stat-pill">
            <div class="num"><asp:Label ID="lblTotal"      runat="server" Text="0" /></div>
            <div class="lbl">Total Priorities</div>
        </div>
        <div class="stat-pill warn">
            <div class="num"><asp:Label ID="lblUnassigned" runat="server" Text="0" /></div>
            <div class="lbl">Awaiting Cluster</div>
        </div>
        <div class="stat-pill ok">
            <div class="num"><asp:Label ID="lblAssigned"   runat="server" Text="0" /></div>
            <div class="lbl">Cluster Assigned</div>
        </div>
    </div>

    <%-- Filter tabs --%>
    <div class="filter-tabs">
        <button class="tab-btn active" onclick="filterRows('all');return false;">All</button>
        <button class="tab-btn"        onclick="filterRows('pending');return false;">Awaiting Assignment</button>
        <button class="tab-btn"        onclick="filterRows('done');return false;">Assigned</button>
    </div>

    <%-- Hidden message used to fire toast --%>
    <asp:Label ID="lblMsg" runat="server" style="display:none;" />

    <%-- Priority table --%>
    <div class="table-wrap">
    <asp:GridView ID="gvPriorities" runat="server"
        AutoGenerateColumns="false"
        CssClass="pri-table"
        GridLines="None"
        DataKeyNames="PMTDP_PriorityID"
        OnRowDataBound="gvPriorities_RowDataBound"
        OnRowCommand="gvPriorities_RowCommand">
        <Columns>

            <%-- 1. Priority info --%>
            <asp:TemplateField HeaderText="Priority">
                <ItemTemplate>
                    <div class="pri-name"><%# Eval("PriorityName") %></div>
                    <div class="pri-pdp"><%# Eval("PDP_Name") %></div>
                    <div class="pri-outcome"><%# Eval("DesiredOutcome") %></div>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 2. Current cluster status --%>
            <asp:TemplateField HeaderText="Current Cluster">
                <ItemTemplate>
                    <asp:Panel ID="pnlUnassigned" runat="server">
                        <span class="badge-no">Not assigned</span>
                    </asp:Panel>
                    <asp:Panel ID="pnlAssigned" runat="server">
                        <span class="badge-yes">&#10003; <asp:Label ID="lblClusterName" runat="server" /></span>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 3. Inline cluster assignment --%>
            <asp:TemplateField HeaderText="Assign Cluster">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlCluster" runat="server" CssClass="ddl-cluster" />
                    <asp:LinkButton ID="btnAssign" runat="server"
                        CommandName="AssignCluster"
                        CommandArgument='<%# Eval("PMTDP_PriorityID") %>'
                        CssClass="btn-save"
                        style="display:inline-block;margin-top:6px;">Save</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- 4. Actions --%>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkCreatePOA" runat="server"
                        NavigateUrl='<%# "pageAddPOA.aspx?priorityId=" + Eval("PMTDP_PriorityID") %>'
                        CssClass="btn-poa">+ Create POA</asp:HyperLink>
                    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="lnk-edit"
                        NavigateUrl='<%# "addEditPriority.aspx?id=" + Eval("PMTDP_PriorityID") %>'>
                        Edit
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    </div>

</div>
</div>

<div id="toast"></div>

<script type="text/javascript">
    function filterRows(mode) {
        document.querySelectorAll('.tab-btn').forEach(function(b) { b.classList.remove('active'); });
        event.target.classList.add('active');
        document.querySelectorAll('.pri-table tbody tr').forEach(function(row) {
            var assigned = row.querySelector('.badge-yes') !== null;
            row.style.display =
                mode === 'all'     ? '' :
                mode === 'pending' ? (assigned ? 'none' : '') :
                                     (assigned ? '' : 'none');
        });
    }

    (function() {
        var msg = document.getElementById('<%= lblMsg.ClientID %>');
        if (msg && msg.innerText.trim() !== '') {
            var t = document.getElementById('toast');
            t.innerText = msg.innerText;
            t.style.display = 'block';
            setTimeout(function() { t.style.display = 'none'; }, 3500);
        }
    }());
</script>
</asp:Content>
