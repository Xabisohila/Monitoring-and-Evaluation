<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_PMTDPUpload.aspx.cs" Inherits="i_PMTDPUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .pmtdp-page { padding: 100px 0 60px; }

    /* Upload card */
    .upload-card {
        background: #fff;
        border: 1px solid #dce3ec;
        border-radius: 12px;
        padding: 28px 32px;
        box-shadow: 0 2px 12px rgba(0,0,0,.06);
        margin-bottom: 28px;
    }
    .upload-card h3 { margin: 0 0 4px; font-size: 20px; font-weight: 700; color: #1a2b4a; }
    .upload-card p.sub { color: #64748b; font-size: 13px; margin-bottom: 20px; }

    .upload-row { display: flex; align-items: center; flex-wrap: wrap; gap: 12px; }

    .file-wrapper {
        flex: 1; min-width: 220px;
        border: 2px dashed #c5d0de;
        border-radius: 8px;
        padding: 10px 14px;
        background: #f8fafc;
        font-size: 13px; color: #475569;
    }
    .file-wrapper input[type="file"] { width: 100%; }

    .btn-upload {
        padding: 10px 22px; font-size: 14px; font-weight: 600;
        border-radius: 8px; border: none;
        background: #1d6f42; color: #fff; cursor: pointer; white-space: nowrap;
    }
    .btn-upload:hover { background: #155a34; }

    .btn-submit-approval {
        padding: 10px 22px; font-size: 14px; font-weight: 600;
        border-radius: 8px; border: none;
        background: #0b5ed7; color: #fff; cursor: pointer;
    }
    .btn-submit-approval:hover { background: #094db0; }

    .msg-bar {
        display: none; margin-top: 14px; padding: 10px 14px;
        border-radius: 8px; font-size: 13px;
        background: #f0f9ff; border: 1px solid #bae6fd; color: #0369a1;
    }
    .msg-bar.visible { display: block; }
    .msg-bar.error   { background: #fff1f2; border-color: #fecdd3; color: #be123c; }

    /* Preview card */
    .preview-card {
        background: #fff; border: 1px solid #dce3ec; border-radius: 12px;
        box-shadow: 0 2px 12px rgba(0,0,0,.06); overflow: hidden; margin-bottom: 20px;
    }
    .preview-header {
        padding: 16px 24px; border-bottom: 1px solid #e8edf3;
        display: flex; align-items: center; justify-content: space-between;
        background: #f8fafc;
    }
    .preview-header h4 { margin: 0; font-size: 15px; font-weight: 700; color: #1a2b4a; }
    .badge-count {
        background: #1d6f42; color: #fff; font-size: 12px; font-weight: 600;
        padding: 3px 10px; border-radius: 20px;
    }

    .table-scroll { overflow-x: auto; -webkit-overflow-scrolling: touch; }

    /* Preview table */
    .pmtdp-tbl { width: 100%; border-collapse: collapse; font-size: 12px; white-space: nowrap; }
    .pmtdp-tbl th {
        padding: 9px 12px; font-weight: 600; font-size: 11px;
        text-transform: uppercase; letter-spacing: .4px;
        border-right: 1px solid rgba(255,255,255,.2);
        border-bottom: 2px solid rgba(0,0,0,.1);
        color: #fff;
    }
    .pmtdp-tbl td {
        padding: 8px 12px; border-bottom: 1px solid #edf2f7;
        border-right: 1px solid #edf2f7; color: #334155;
        max-width: 220px; overflow: hidden; text-overflow: ellipsis; vertical-align: top;
    }
    .pmtdp-tbl tr:hover td { background: #f0f9ff !important; }
    .pmtdp-tbl tr:last-child td { border-bottom: none; }

    /* Column group header colours */
    .col-priority { background-color: #1e6b9e; }
    .col-outcome  { background-color: #1d6f42; }
    .col-inst     { background-color: #92400e; }
    .col-interv   { background-color: #5b21b6; }

    /* Column group cell tints */
    .col-priority-td { background-color: #eff8ff; }
    .col-outcome-td  { background-color: #f0fdf4; }
    .col-inst-td     { background-color: #fffbeb; }
    .col-interv-td   { background-color: #faf5ff; }

    /* Alternating row — slightly deeper tints */
    .pmtdp-tbl tr.alt td.col-priority-td { background-color: #e0f0ff; }
    .pmtdp-tbl tr.alt td.col-outcome-td  { background-color: #e0f7e8; }
    .pmtdp-tbl tr.alt td.col-inst-td     { background-color: #fef3d0; }
    .pmtdp-tbl tr.alt td.col-interv-td   { background-color: #ede8ff; }
    .pmtdp-tbl tr.alt td                 { background-color: #f8fafc; }

    /* My submissions history */
    .history-card {
        background: #fff; border: 1px solid #dce3ec; border-radius: 12px;
        box-shadow: 0 2px 12px rgba(0,0,0,.06); overflow: hidden; margin-top: 32px;
    }
    .history-header {
        padding: 16px 24px; background: #f8fafc;
        border-bottom: 1px solid #e8edf3;
        font-size: 15px; font-weight: 700; color: #1a2b4a;
    }
    .history-tbl { width: 100%; border-collapse: collapse; font-size: 13px; }
    .history-tbl th {
        padding: 10px 14px; background: #f1f5f9; font-weight: 600;
        color: #475569; text-align: left; border-bottom: 2px solid #dce3ec;
    }
    .history-tbl td {
        padding: 10px 14px; border-bottom: 1px solid #edf2f7; color: #334155;
        vertical-align: middle;
    }
    .history-tbl tr:last-child td { border-bottom: none; }
    .history-tbl tr:hover td { background: #f8fafc; }
    .status-badge {
        display: inline-block; padding: 3px 10px; border-radius: 20px;
        font-size: 11px; font-weight: 700; text-transform: uppercase; letter-spacing: .4px;
    }
    .badge-pending  { background: #fef3c7; color: #92400e; }
    .badge-approved { background: #d1fae5; color: #065f46; }
    .badge-rejected { background: #fee2e2; color: #991b1b; }
    .history-empty  { padding: 24px; text-align: center; color: #94a3b8; font-size: 13px; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="pmtdp-page">
<div class="container">

    <%-- Page title --%>
    <div style="margin-bottom:24px;padding-top:24px;">
        <h2 style="margin:0;font-size:24px;font-weight:700;color:#1a2b4a;">
            PMTDP Upload &mdash; 5-Year Structure
        </h2>
        <p style="color:#64748b;font-size:13px;margin:4px 0 0;">
            Upload the PMTDP Excel file. A second Planning Unit user must approve before data is applied.
        </p>
    </div>

    <%-- Upload card --%>
    <div class="upload-card">
        <h3>Select File</h3>
        <p class="sub">Accepted: .xlsx &nbsp;&nbsp;|&nbsp;&nbsp; Sheet must be named <strong>PMTDP</strong></p>
        <div class="upload-row">
            <div class="file-wrapper">
                <asp:FileUpload ID="fuPMTDP" runat="server" />
            </div>
            <asp:Button ID="btnUpload" runat="server" Text="Upload &amp; Preview"
                OnClick="btnUpload_Click" CssClass="btn-upload" />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit for Approval"
                OnClick="btnSubmit_Click" Visible="false" CssClass="btn-submit-approval" />
        </div>
        <asp:Label ID="lblMsg" runat="server" CssClass="msg-bar" />
    </div>

    <%-- Preview card — hidden until a file is successfully loaded --%>
    <asp:Panel ID="previewCard" runat="server" Visible="false" CssClass="preview-card">
        <div class="preview-header">
            <h4>Data Preview</h4>
            <asp:Label ID="lblRowCount" runat="server" CssClass="badge-count" />
        </div>

        <%-- Colour legend --%>
        <div style="padding:10px 24px;background:#f8fafc;border-bottom:1px solid #e8edf3;font-size:12px;display:flex;gap:20px;flex-wrap:wrap;">
            <span><span style="display:inline-block;width:12px;height:12px;background:#1e6b9e;border-radius:3px;margin-right:5px;vertical-align:middle;"></span>Priority &amp; Programme</span>
            <span><span style="display:inline-block;width:12px;height:12px;background:#1d6f42;border-radius:3px;margin-right:5px;vertical-align:middle;"></span>Outcome &amp; Indicator</span>
            <span><span style="display:inline-block;width:12px;height:12px;background:#92400e;border-radius:3px;margin-right:5px;vertical-align:middle;"></span>Institutions &amp; Flags</span>
            <span><span style="display:inline-block;width:12px;height:12px;background:#5b21b6;border-radius:3px;margin-right:5px;vertical-align:middle;"></span>Intervention Level</span>
        </div>

        <div class="table-scroll">
            <asp:GridView ID="gvPreview" runat="server"
                AutoGenerateColumns="true"
                CssClass="pmtdp-tbl"
                GridLines="None"
                OnRowCreated="gvPreview_RowCreated" />
        </div>
    </asp:Panel>

    <%-- ── My Submissions History ── --%>
    <div class="history-card">
        <div class="history-header">My PMTDP Submissions</div>
        <asp:Panel ID="pnlHistoryEmpty" runat="server" Visible="false">
            <div class="history-empty">You have not submitted any PMTDP uploads yet.</div>
        </asp:Panel>
        <asp:Panel ID="pnlHistoryGrid" runat="server" Visible="false">
            <asp:Repeater ID="rptHistory" runat="server">
                <HeaderTemplate>
                    <table class="history-tbl">
                    <tr>
                        <th>Ref #</th>
                        <th>Submitted</th>
                        <th>Status</th>
                        <th>Reviewer Comment</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("UploadRequestID") %></td>
                        <td><%# Eval("SubmittedDate") %></td>
                        <td><%# (string)Eval("StatusBadge") %></td>
                        <td><%# Eval("ReviewComment") %></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background:#f8fafc;">
                        <td><%# Eval("UploadRequestID") %></td>
                        <td><%# Eval("SubmittedDate") %></td>
                        <td><%# (string)Eval("StatusBadge") %></td>
                        <td><%# Eval("ReviewComment") %></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>

</div>
</div>

<%-- ── Validation / error modal ── --%>
<div class="modal fade" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalErrorLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document" style="max-width:440px;">
        <div class="modal-content" style="border-radius:14px;border:none;box-shadow:0 8px 40px rgba(0,0,0,.18);">
            <div class="modal-body" style="padding:40px 36px 28px;text-align:center;">
                <div style="width:72px;height:72px;background:#fff1f2;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 20px;font-size:36px;color:#be123c;">
                    &#9888;
                </div>
                <h4 style="margin:0 0 10px;font-size:18px;font-weight:700;color:#1a2b4a;">Action Required</h4>
                <p id="modalErrorMsg" style="color:#64748b;font-size:14px;margin:0;line-height:1.6;"></p>
            </div>
            <div class="modal-footer" style="border:none;padding:0 36px 32px;justify-content:center;">
                <button type="button" class="btn-submit-approval" style="min-width:100px;" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

<%-- ── Submission success modal ── --%>
<div class="modal fade" id="modalSubmitSuccess" tabindex="-1" role="dialog" aria-labelledby="modalSubmitLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document" style="max-width:480px;">
        <div class="modal-content" style="border-radius:14px;border:none;box-shadow:0 8px 40px rgba(0,0,0,.18);">

            <div class="modal-body" style="padding:40px 36px 28px;text-align:center;">
                <%-- Green circle tick icon --%>
                <div style="width:72px;height:72px;background:#d1fae5;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 20px;font-size:36px;color:#059669;">
                    &#10003;
                </div>
                <h4 style="margin:0 0 8px;font-size:20px;font-weight:700;color:#1a2b4a;">Submitted for Approval</h4>
                <p style="color:#64748b;font-size:14px;margin:0 0 6px;">
                    Your PMTDP upload has been queued for review.
                </p>
                <p style="color:#64748b;font-size:13px;margin:0;">
                    A second Planning Unit user must approve the upload before the data is applied to the system.
                    You will <strong>not</strong> be able to approve your own submission.
                </p>
            </div>

            <div class="modal-footer" style="border:none;padding:0 36px 32px;justify-content:center;gap:12px;">
                <button type="button" class="btn-upload" style="min-width:130px;"
                    data-dismiss="modal" onclick="window.location.href='i_PMTDPUpload.aspx';">
                    Upload Another
                </button>
                <button type="button" style="min-width:130px;padding:10px 22px;font-size:14px;font-weight:600;border-radius:8px;border:1px solid #dce3ec;background:#fff;color:#1a2b4a;cursor:pointer;"
                    data-dismiss="modal">
                    Close
                </button>
            </div>

        </div>
    </div>
</div>

<script type="text/javascript">
(function () {
    // Show message label with correct style
    var lbl = document.getElementById('<%= lblMsg.ClientID %>');
    if (lbl && lbl.innerText.trim() !== '') {
        lbl.style.display = 'block';
        var txt = lbl.innerText.toLowerCase();
        if (txt.indexOf('failed') > -1 || txt.indexOf('invalid') > -1 || txt.indexOf('please') > -1)
            lbl.classList.add('error');
    }

    // Map display header labels to colour group
    var groups = {
        'Priority Focus':'priority', 'Integration Programme':'priority', 'Leading Department':'priority',
        'Desired Outcome':'outcome', 'Outcome Indicator':'outcome', 'Indicator Type':'outcome',
        'Baseline Value':'outcome', 'Term Target Value':'outcome', 'Annual Budget':'outcome',
        'Implementing Institution':'inst', 'Supporting Institutions':'inst',
        'Is Cumulative':'inst', 'Is Percentage':'inst',
        'Intervention Name':'interv', 'Intervention Indicator':'interv',
        'Baseline 2023/24':'interv', 'Term Target 2030':'interv',
        'Term Budget':'interv', 'Annual Target 2025/26':'interv', 'Spatial Reference':'interv'
    };

    var tbl = document.querySelector('.pmtdp-tbl');
    if (tbl) {
        var headers = tbl.querySelectorAll('th');
        var colGroups = [];
        headers.forEach(function (th) {
            var g = groups[th.innerText.trim()] || '';
            colGroups.push(g);
            if (g) th.classList.add('col-' + g);
        });

        var dataRows = tbl.querySelectorAll('tr');
        var idx = 0;
        dataRows.forEach(function (row) {
            if (row.querySelector('th')) return;
            if (idx % 2 === 1) row.classList.add('alt');
            idx++;
            row.querySelectorAll('td').forEach(function (td, i) {
                if (colGroups[i]) td.classList.add('col-' + colGroups[i] + '-td');
            });
        });
    }
}());
</script>
</asp:Content>
