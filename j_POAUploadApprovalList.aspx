<%@ Page Title="POA Upload — Approval List" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="j_POAUploadApprovalList.aspx.cs" Inherits="j_POAUploadApprovalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
:root {
    --primary: #0C2D48; --accent: #12826A; --border: #dce3ec;
    --bg: #f7f8fb; --surface: #fff; --text: #1a2b4a; --muted: #64748b;
    --radius: 12px; --shadow: 0 2px 12px rgba(0,0,0,.07);
    --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }
.ap-header { padding: 36px 0 22px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.ap-header h2 { margin: 0 0 4px; font-size: 24px; font-weight: 800; color: var(--primary); }
.ap-header p  { margin: 0; font-size: 13px; color: var(--muted); }
.form-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    overflow: hidden; margin-bottom: 24px;
}
.card-header {
    padding: 14px 20px; background: #f8fafc;
    border-bottom: 1px solid var(--border);
    display: flex; align-items: center; justify-content: space-between;
}
.card-header h4 { margin: 0; font-size: 14px; font-weight: 700; color: var(--text); }
.badge-status { display:inline-block; padding:3px 10px; border-radius:20px; font-size:11px; font-weight:700; }
.badge-pending  { background:#fef3c7; color:#92400e; }
.badge-approved { background:#d1fae5; color:#065f46; }
.badge-rejected { background:#fee2e2; color:#991b1b; }
.btn-review {
    display:inline-block; padding:5px 14px; border-radius:6px;
    font-size:12px; font-weight:600; color:#fff; text-decoration:none;
    background:var(--primary); transition:opacity .15s; white-space:nowrap;
}
.btn-review:hover { opacity:.85; color:#fff; text-decoration:none; }
.btn-view {
    display:inline-block; padding:5px 14px; border-radius:6px;
    font-size:12px; font-weight:600; color:#fff; text-decoration:none;
    background:var(--accent); transition:opacity .15s; white-space:nowrap;
}
.btn-view:hover { opacity:.85; color:#fff; text-decoration:none; }
.btn-resubmit {
    display:inline-block; padding:5px 14px; border-radius:6px;
    font-size:12px; font-weight:600; color:#fff; text-decoration:none;
    background:#b45309; transition:opacity .15s; white-space:nowrap;
}
.btn-resubmit:hover { opacity:.85; color:#fff; text-decoration:none; }
.section-sep { margin: 0 0 20px; font-size: 15px; font-weight: 700; color: var(--primary);
               display: flex; align-items: center; gap: 10px; }
.section-sep::after { content:''; flex:1; height:1px; background:var(--border); }
.empty-state { padding: 28px; text-align: center; color: var(--muted); font-size: 13px; }
.table-scroll { overflow-x: auto; -webkit-overflow-scrolling: touch; }
.history-tbl { width: 100%; border-collapse: collapse; font-size: 13px; white-space: nowrap; }
.history-tbl th {
    padding: 10px 14px; background: #f1f5f9; font-weight: 600;
    color: #475569; text-align: left; border-bottom: 2px solid #dce3ec;
}
.history-tbl td {
    padding: 10px 14px; border-bottom: 1px solid #edf2f7;
    color: #334155; vertical-align: middle;
}
.history-tbl tr:last-child td { border-bottom: none; }
.history-tbl tr:hover td { background: #f8fafc; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="padding-top:80px; padding-bottom:60px; max-width:1100px;">

    <div class="ap-header">
        <h2><i class="glyphicon glyphicon-list-alt"></i> POA Upload - Approval List</h2>
        <p>Review submitted POA uploads. Pending uploads from other users require your approval.</p>
    </div>

    <%-- Pending Uploads (for Approvers) --%>
    <asp:Panel ID="pnlPendingSection" runat="server" Visible="false">
        <div class="section-sep">Awaiting Review</div>
        <div class="form-card">
            <div class="card-header"><h4>Pending Uploads</h4></div>
            <asp:Panel ID="pnlNoPending" runat="server" Visible="false">
                <div class="empty-state">No uploads are currently awaiting review.</div>
            </asp:Panel>
            <asp:Panel ID="pnlPending" runat="server" Visible="false">
                <div class="table-scroll" id="pendingTableWrap">
                    <asp:Repeater ID="rptPending" runat="server">
                        <HeaderTemplate>
                            <table class="history-tbl">
                            <tr>
                                <th>Submitted</th>
                                <th>Submitted By</th>
                                <th>Priority Focus</th>
                                <th>Integration Programme</th>
                                <th>Rows</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("UploadDate", "{0:dd MMM yyyy}") %></td>
                                <td><%# Server.HtmlEncode(Eval("UploadedBy").ToString()) %></td>
                                <td><%# Server.HtmlEncode(Eval("PriorityFocus").ToString()) %></td>
                                <td><%# Server.HtmlEncode(Eval("IntegrationProgramme").ToString()) %></td>
                                <td><%# Eval("DataRowCount") %></td>
                                <td><a href='j_POAUploadApprovalView.aspx?id=<%# Eval("UploadRequestID") %>' class="btn-review">Review</a></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </asp:Repeater>
                </div>
                <div id="pendingPager" style="display:none; padding:12px 20px; border-top:1px solid #edf2f7;
                     display:flex; align-items:center; justify-content:space-between; flex-wrap:wrap; gap:8px;">
                    <span id="pendingPageInfo" style="font-size:12px; color:#64748b;"></span>
                    <div style="display:flex; gap:6px;">
                        <button type="button" id="pendingPrev" onclick="pendingPage(-1)"
                            style="padding:5px 14px; font-size:12px; font-weight:600; border-radius:6px;
                                   border:1px solid #dce3ec; background:#fff; color:#1a2b4a; cursor:pointer;">&lsaquo; Prev</button>
                        <button type="button" id="pendingNext" onclick="pendingPage(1)"
                            style="padding:5px 14px; font-size:12px; font-weight:600; border-radius:6px;
                                   border:1px solid #dce3ec; background:#fff; color:#1a2b4a; cursor:pointer;">Next &rsaquo;</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>

    <%-- My Uploads --%>
    <div class="section-sep">My Submissions</div>
    <div class="form-card">
        <div class="card-header">
            <h4>Uploads I Submitted</h4>
            <a href="i_POAUpload.aspx" style="font-size:12px; font-weight:600; color:var(--accent);">+ Upload new POA</a>
        </div>
        <asp:Panel ID="pnlNoMine" runat="server">
            <div class="empty-state">You have not submitted any POA uploads yet. <a href="i_POAUpload.aspx">Upload now &rarr;</a></div>
        </asp:Panel>
        <asp:Panel ID="pnlMine" runat="server" Visible="false">
            <div class="table-scroll" id="mineTableWrap">
                <asp:Repeater ID="rptMine" runat="server">
                    <HeaderTemplate>
                        <table class="history-tbl">
                        <tr>
                            <th>Submitted</th>
                            <th>Priority Focus</th>
                            <th>Integration Programme</th>
                            <th>Rows</th>
                            <th>Status</th>
                            <th>Reviewer Note</th>
                            <th></th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("UploadDate", "{0:dd MMM yyyy}") %></td>
                            <td><%# Server.HtmlEncode(Eval("PriorityFocus").ToString()) %></td>
                            <td><%# Server.HtmlEncode(Eval("IntegrationProgramme").ToString()) %></td>
                            <td><%# Eval("DataRowCount") %></td>
                            <td><span class="badge-status badge-<%# Eval("Status").ToString().ToLower() %>"><%# Eval("Status") %></span></td>
                            <td style="color:#64748b; font-size:12px;"><%# Server.HtmlEncode(Eval("ReviewComment").ToString()) %></td>
                            <td>
                                <%# (Eval("Status").ToString() == "Approved" && Eval("CreatedPOA_ID") != DBNull.Value)
                                    ? string.Format("<a href='pagePOADetail.aspx?id={0}' class='btn-view'>View POA &rarr;</a>", Eval("CreatedPOA_ID"))
                                    : Eval("Status").ToString() == "Rejected"
                                    ? string.Format("<a href='i_POAUpload.aspx?resubmit={0}' class='btn-resubmit'>Resubmit</a>", Eval("UploadRequestID"))
                                    : "" %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </div>
            <div id="minePager" style="display:none; padding:12px 20px; border-top:1px solid #edf2f7;
                 display:flex; align-items:center; justify-content:space-between; flex-wrap:wrap; gap:8px;">
                <span id="minePageInfo" style="font-size:12px; color:#64748b;"></span>
                <div style="display:flex; gap:6px;">
                    <button type="button" id="minePrev" onclick="minePage(-1)"
                        style="padding:5px 14px; font-size:12px; font-weight:600; border-radius:6px;
                               border:1px solid #dce3ec; background:#fff; color:#1a2b4a; cursor:pointer;">&lsaquo; Prev</button>
                    <button type="button" id="mineNext" onclick="minePage(1)"
                        style="padding:5px 14px; font-size:12px; font-weight:600; border-radius:6px;
                               border:1px solid #dce3ec; background:#fff; color:#1a2b4a; cursor:pointer;">Next &rsaquo;</button>
                </div>
            </div>
        </asp:Panel>
    </div>

</div>

<script type="text/javascript">
function makePager(wrapId, pagerId, infoId, prevId, nextId, pageSize) {
    var wrap = document.getElementById(wrapId);
    if (!wrap) return null;
    var tbl = wrap.querySelector('.history-tbl');
    if (!tbl) return null;
    var rows = [];
    tbl.querySelectorAll('tr').forEach(function(r) {
        if (!r.querySelector('th')) rows.push(r);
    });
    if (!rows.length) return null;
    var totalPages = Math.ceil(rows.length / pageSize);
    var currentPage = 1;
    function render() {
        var start = (currentPage - 1) * pageSize;
        rows.forEach(function(r, i) {
            r.style.display = (i >= start && i < start + pageSize) ? '' : 'none';
        });
        document.getElementById(infoId).textContent =
            'Page ' + currentPage + ' of ' + totalPages + '  (' + rows.length + ' total)';
        document.getElementById(prevId).disabled = currentPage === 1;
        document.getElementById(nextId).disabled = currentPage === totalPages;
        document.getElementById(prevId).style.opacity = currentPage === 1 ? '0.4' : '1';
        document.getElementById(nextId).style.opacity = currentPage === totalPages ? '0.4' : '1';
    }
    if (rows.length > pageSize) document.getElementById(pagerId).style.display = 'flex';
    render();
    return function(dir) {
        currentPage = Math.min(totalPages, Math.max(1, currentPage + dir));
        render();
    };
}
(function () {
    var _pending = makePager('pendingTableWrap', 'pendingPager', 'pendingPageInfo', 'pendingPrev', 'pendingNext', 10);
    var _mine    = makePager('mineTableWrap',    'minePager',    'minePageInfo',    'minePrev',    'mineNext',    10);
    window.pendingPage = function(d) { if (_pending) _pending(d); };
    window.minePage    = function(d) { if (_mine)    _mine(d);    };
}());
</script>
</asp:Content>
