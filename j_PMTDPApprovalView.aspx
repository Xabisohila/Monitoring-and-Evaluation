<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="j_PMTDPApprovalView.aspx.cs" Inherits="j_PMTDPApprovalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
:root {
    --bg:       #f7f8fb;
    --surface:  #ffffff;
    --border:   #dce3ec;
    --text:     #1a2b4a;
    --muted:    #64748b;
    --primary:  #0b5ed7;
    --pd:       #094db0;
    --success:  #1d6f42;
    --danger:   #dc3545;
    --radius:   12px;
    --shadow:   0 2px 12px rgba(0,0,0,.07);
    --font:     "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }

/* ── Page header ───────────────────────────────────────────── */
.rv-header { padding: 40px 0 20px; }
.rv-header h2 {
    margin: 0 0 4px; font-size: 26px; font-weight: 800; color: var(--text);
}
.rv-header .ref-tag {
    display: inline-block; padding: 2px 12px; border-radius: 20px;
    background: #e0ecff; color: #1e40af; font-size: 12px; font-weight: 700;
    letter-spacing: .4px; margin-left: 10px; vertical-align: middle;
}
.rv-header p { color: var(--muted); font-size: 13px; margin: 6px 0 0; }

/* ── Owner warning banner ──────────────────────────────────── */
.owner-banner {
    display: flex; align-items: flex-start; gap: 16px;
    background: #fffbeb; border: 1px solid #fcd34d;
    border-left: 5px solid #f59e0b;
    border-radius: var(--radius); padding: 18px 22px;
    margin-bottom: 24px;
}
.owner-banner .icon {
    flex-shrink: 0; width: 44px; height: 44px;
    background: #fef3c7; border-radius: 50%;
    display: flex; align-items: center; justify-content: center;
    font-size: 22px; color: #b45309;
}
.owner-banner .body strong {
    display: block; font-size: 15px; font-weight: 700; color: #92400e; margin-bottom: 4px;
}
.owner-banner .body p {
    margin: 0; font-size: 13px; color: #78350f; line-height: 1.6;
}

/* ── Data card ─────────────────────────────────────────────── */
.data-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    overflow: hidden; margin-bottom: 24px;
}
.data-card-header {
    padding: 14px 20px; background: #f1f5f9;
    border-bottom: 1px solid var(--border);
    display: flex; align-items: center; justify-content: space-between;
}
.data-card-header h4 { margin: 0; font-size: 15px; font-weight: 700; color: var(--text); }
.row-badge {
    background: var(--primary); color: #fff; font-size: 11px; font-weight: 700;
    padding: 3px 10px; border-radius: 20px;
}
.table-scroll { overflow-x: auto; -webkit-overflow-scrolling: touch; }

/* ── GridView table ────────────────────────────────────────── */
.rv-tbl { width: 100%; border-collapse: collapse; font-size: 12px; white-space: nowrap; }
.rv-tbl th {
    padding: 9px 12px; font-weight: 600; font-size: 11px;
    text-transform: uppercase; letter-spacing: .4px; color: #fff;
    border-right: 1px solid rgba(255,255,255,.15);
    border-bottom: 2px solid rgba(0,0,0,.1);
    background: #334155;
}
.rv-tbl td {
    padding: 8px 12px; border-bottom: 1px solid #edf2f7;
    border-right: 1px solid #f1f5f9; color: #334155;
    max-width: 240px; overflow: hidden; text-overflow: ellipsis; vertical-align: top;
}
.rv-tbl tr:hover td { background: #f0f7ff !important; }
.rv-tbl tr:last-child td { border-bottom: none; }
.rv-tbl tr:nth-child(even) td { background: #f8fafc; }

/* ── Review action card ────────────────────────────────────── */
.review-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    padding: 24px 28px; margin-bottom: 24px;
}
.review-card h4 { margin: 0 0 6px; font-size: 16px; font-weight: 700; color: var(--text); }
.review-card p  { margin: 0 0 16px; font-size: 13px; color: var(--muted); }

.review-card textarea {
    width: 100%; max-width: 560px; padding: 10px 14px;
    font-size: 13px; font-family: var(--font);
    border: 1px solid var(--border); border-radius: 8px;
    background: #f8fafc; color: var(--text);
    resize: vertical; outline: none;
    transition: border-color .15s;
    box-sizing: border-box;
}
.review-card textarea:focus { border-color: var(--primary); background: #fff; }

.btn-row { display: flex; gap: 10px; margin-top: 16px; flex-wrap: wrap; }

.btn-approve {
    padding: 10px 26px; font-size: 14px; font-weight: 600;
    border-radius: 8px; border: none; cursor: pointer;
    background: var(--success); color: #fff;
    transition: background .15s;
}
.btn-approve:hover { background: #155a34; }

.btn-reject {
    padding: 10px 26px; font-size: 14px; font-weight: 600;
    border-radius: 8px; cursor: pointer;
    background: #fff; color: var(--danger);
    border: 1.5px solid var(--danger);
    transition: background .15s, color .15s;
}
.btn-reject:hover { background: var(--danger); color: #fff; }

/* ── Result message bar ────────────────────────────────────── */
.msg-bar {
    display: none; padding: 12px 16px; border-radius: 8px;
    font-size: 13px; font-weight: 500; margin-top: 12px;
}
.msg-bar.visible { display: block; }
.msg-success { background: #d1fae5; border: 1px solid #6ee7b7; color: #065f46; }
.msg-error   { background: #fee2e2; border: 1px solid #fca5a5; color: #991b1b; }
.msg-info    { background: #e0f2fe; border: 1px solid #7dd3fc; color: #075985; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container">

    <%-- Page header --%>
    <div class="rv-header">
        <h2>
            Review PMTDP Upload
            <span class="ref-tag">Ref&nbsp;#&nbsp;<%= Request.QueryString["id"] %></span>
        </h2>
        <p>Review the uploaded data below before approving or rejecting.</p>
    </div>

    <%-- Owner warning banner (shown when submitter views their own upload) --%>
    <asp:Panel ID="pnlOwnerBanner" runat="server" Visible="false">
        <div class="owner-banner">
            <div class="icon">&#9888;</div>
            <div class="body">
                <strong>Your Submission</strong>
                <p>You submitted this upload and cannot approve or reject it.
                   A second Planning Unit user must review it.</p>
            </div>
        </div>
    </asp:Panel>

    <%-- Data preview card --%>
    <div class="data-card">
        <div class="data-card-header">
            <h4>Upload Data</h4>
            <asp:Label ID="lblRowCount" runat="server" CssClass="row-badge" />
        </div>
        <div class="table-scroll">
            <asp:GridView ID="gvData" runat="server"
                AutoGenerateColumns="true"
                CssClass="rv-tbl"
                GridLines="None" />
        </div>
    </div>

    <%-- Review action card (hidden for the submitter) --%>
    <asp:Panel ID="pnlReview" runat="server">
        <div class="review-card">
            <h4>Decision</h4>
            <p>Optionally add a comment before approving or rejecting.</p>
            <asp:TextBox ID="txtComment" runat="server"
                TextMode="MultiLine" Rows="3"
                placeholder="Add a comment or reason (optional)..." />
            <div class="btn-row">
                <asp:Button ID="btnApprove" runat="server"
                    Text="Approve" OnClick="btnApprove_Click"
                    CssClass="btn-approve" />
                <asp:Button ID="btnReject" runat="server"
                    Text="Reject" OnClick="btnReject_Click"
                    CssClass="btn-reject" />
            </div>
        </div>
    </asp:Panel>

    <%-- Action result message --%>
    <asp:Label ID="lblMsg" runat="server" CssClass="msg-bar" />

</div>

<script>
(function () {
    var lbl = document.getElementById('<%= lblMsg.ClientID %>');
    if (!lbl || !lbl.innerText.trim()) return;

    lbl.style.display = 'block';
    var t = lbl.innerText.toLowerCase();
    if (t.indexOf('approved') > -1)        lbl.className += ' msg-success';
    else if (t.indexOf('rejected') > -1 || t.indexOf('cannot') > -1 || t.indexOf('expired') > -1)
                                           lbl.className += ' msg-error';
    else                                   lbl.className += ' msg-info';
})();
</script>
</asp:Content>
