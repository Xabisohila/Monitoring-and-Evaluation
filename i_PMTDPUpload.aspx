<%@ Page Title="PMTDP Upload" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_PMTDPUpload.aspx.cs" Inherits="i_PMTDPUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
:root {
    --bg:      #f7f8fb;
    --surface: #ffffff;
    --border:  #dce3ec;
    --text:    #1a2b4a;
    --muted:   #64748b;
    --primary: #0C2D48;
    --accent:  #1d6f42;
    --danger:  #c0392b;
    --radius:  12px;
    --shadow:  0 2px 12px rgba(0,0,0,.07);
    --font:    "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }

.pu-header { padding: 36px 0 22px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.pu-header h2 { margin: 0 0 4px; font-size: 24px; font-weight: 800; color: var(--primary); }
.pu-header p  { margin: 0; font-size: 13px; color: var(--muted); }

.form-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    padding: 24px 28px; margin-bottom: 20px;
}
.form-card .card-title {
    font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .8px; color: var(--muted);
    padding-bottom: 12px; border-bottom: 1px solid #edf2f7; margin-bottom: 20px;
}

.field { margin-bottom: 16px; }
.field label { display: block; font-size: 13px; font-weight: 600; color: var(--text); margin-bottom: 5px; }
.field input[type="file"] {
    display: block; width: 100%; padding: 9px 12px; font-size: 13px;
    border: 1px dashed #94a3b8; border-radius: 8px; background: #f8fafc;
    cursor: pointer; box-sizing: border-box;
}
.field input[type="file"]:hover { border-color: var(--accent); }

.msg-info    { background:#eff6ff; color:#1e40af; border:1px solid #bfdbfe; border-left:4px solid #3b82f6; border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-success { background:#f0fdf4; color:#14532d; border:1px solid #86efac; border-left:4px solid var(--accent); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-error   { background:#fff1f2; color:#9f1239; border:1px solid #fecdd3; border-left:4px solid var(--danger); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }

/* Step indicator */
.step-indicator { display:flex; gap:0; margin-bottom:28px; }
.step { flex:1; text-align:center; padding:10px 4px; font-size:12px; font-weight:600;
        border-bottom:3px solid #e2e8f0; color:#94a3b8; }
.step.active { border-color:var(--accent); color:var(--accent); }
.step.done   { border-color:#64748b; color:#64748b; }

/* Meta info grid */
.meta-grid { display:grid; grid-template-columns:1fr 1fr; gap:12px; }
@media(max-width:600px) { .meta-grid { grid-template-columns:1fr; } }
.meta-item { background:#f8fafc; border:1px solid var(--border); border-radius:8px; padding:12px 14px; }
.meta-item .meta-label { font-size:11px; font-weight:700; text-transform:uppercase; letter-spacing:.5px; color:var(--muted); margin-bottom:4px; }
.meta-item .meta-value { font-size:13px; font-weight:600; color:var(--text); }

/* Preview table */
.preview-wrap { overflow-x:auto; margin-top:8px; }
.preview-table { width:100%; border-collapse:collapse; font-size:12px; min-width:900px; }
.preview-table th { background:var(--primary); color:#fff; padding:8px 10px; text-align:left; white-space:nowrap; font-weight:600; }
.preview-table td { padding:7px 10px; border-bottom:1px solid #edf2f7; vertical-align:top; }
.preview-table tr:hover td { background:#f8fafc; }
.preview-table td:empty::after { content:"—"; color:#cbd5e1; }
.row-count { display:inline-block; background:#e0f2fe; color:#0369a1; padding:2px 8px; border-radius:10px; font-size:12px; font-weight:700; margin-left:8px; }

/* Buttons */
.btn-primary   { padding:9px 24px; font-size:13px; font-weight:700; border:none; border-radius:8px; cursor:pointer; background:var(--accent); color:#fff; transition:background .15s; }
.btn-primary:hover { background:#155a34; }
.btn-secondary { padding:9px 20px; font-size:13px; font-weight:600; border-radius:8px; cursor:pointer; background:#fff; color:var(--text); border:1.5px solid var(--border); }
.btn-secondary:hover { background:#f1f5f9; }
.btn-row { display:flex; gap:10px; align-items:center; flex-wrap:wrap; }

/* History */
.badge-status { display:inline-block; padding:2px 8px; border-radius:10px; font-size:11px; font-weight:600; }
.badge-pending  { background:#fef3c7; color:#92400e; }
.badge-approved { background:#d1fae5; color:#065f46; }
.badge-rejected { background:#fee2e2; color:#991b1b; }
.my-uploads-table th { background:#0C2D48; color:#fff; font-size:12px; font-weight:600; padding:8px 10px; }
.my-uploads-table td { font-size:12px; padding:7px 10px; vertical-align:middle; border-bottom:1px solid #edf2f7; }
.my-uploads-table tr:hover td { background:#f8fafc; }
.btn-view-pmtdp {
    display:inline-block; padding:4px 12px; border-radius:6px;
    font-size:11px; font-weight:700; color:#fff; background:var(--accent);
    text-decoration:none; white-space:nowrap;
}
.btn-view-pmtdp:hover { opacity:.85; color:#fff; text-decoration:none; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container" style="padding-top:80px; padding-bottom:60px; max-width:1100px;">

    <div class="pu-header">
        <h2><i class="glyphicon glyphicon-upload"></i> PMTDP Upload &mdash; 5-Year Structure</h2>
        <p>Upload the PMTDP Excel file. A second Planning Unit user must approve before data is applied.</p>
    </div>

    <%-- Alerts --%>
    <asp:Panel ID="pnlError"   runat="server" Visible="false">
        <div class="msg-error"><i class="glyphicon glyphicon-exclamation-sign"></i> <asp:Literal ID="litError" runat="server" /></div>
    </asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
        <div class="msg-success"><i class="glyphicon glyphicon-ok-circle"></i> <asp:Literal ID="litSuccess" runat="server" /></div>
    </asp:Panel>

    <%-- ── STEP 1: File upload ── --%>
    <asp:Panel ID="pnlUpload" runat="server">
        <div class="step-indicator">
            <div class="step active">1. Upload File</div>
            <div class="step">2. Preview</div>
            <div class="step">3. Submit for Review</div>
        </div>

        <div class="form-card">
            <div class="card-title">Select PMTDP Excel File</div>
            <div class="msg-info">
                <strong>Before uploading:</strong> ensure your file follows the PMTDP template &mdash;
                fill in the 4 header rows (Goal / Priority / Programme / Impact), then add one intervention-indicator per row.
                &nbsp;&nbsp;<a href="Templates/PMTDP_Upload_Template.xlsx" style="font-weight:700;color:#0C2D48;">
                    <i class="glyphicon glyphicon-download-alt"></i> Download Template
                </a>
            </div>
            <div class="field" style="margin-top:16px;">
                <label>PMTDP Excel File (.xlsx) <span style="color:var(--danger)">*</span></label>
                <asp:FileUpload ID="fuPMTDP" runat="server" accept=".xlsx" />
            </div>
            <div style="margin-top:16px;">
                <asp:Button ID="btnUpload" runat="server" Text="Upload &amp; Preview"
                    CssClass="btn-primary" OnClick="btnUpload_Click" />
            </div>
        </div>
    </asp:Panel>

    <%-- ── STEP 2: Preview + Confirm ── --%>
    <asp:Panel ID="pnlPreview" runat="server" Visible="false">
        <div class="step-indicator">
            <div class="step done">1. Upload File</div>
            <div class="step active">2. Preview</div>
            <div class="step">3. Submit for Review</div>
        </div>

        <%-- Header metadata --%>
        <div class="form-card">
            <div class="card-title">Extracted Header Information</div>
            <div class="meta-grid">
                <div class="meta-item">
                    <div class="meta-label">Development Plan Goal</div>
                    <div class="meta-value"><asp:Literal ID="litGoal" runat="server" /></div>
                </div>
                <div class="meta-item">
                    <div class="meta-label">Priority Focus</div>
                    <div class="meta-value"><asp:Literal ID="litPriority" runat="server" /></div>
                </div>
                <div class="meta-item">
                    <div class="meta-label">Integration Programme</div>
                    <div class="meta-value"><asp:Literal ID="litProgramme" runat="server" /></div>
                </div>
                <div class="meta-item">
                    <div class="meta-label">Impact Statement</div>
                    <div class="meta-value"><asp:Literal ID="litImpact" runat="server" /></div>
                </div>
            </div>
        </div>

        <%-- Data preview table --%>
        <div class="form-card">
            <div class="card-title">
                Data Preview
                <asp:Literal ID="litRowCount" runat="server" />
            </div>
            <div class="preview-wrap">
                <asp:PlaceHolder ID="phPreviewTable" runat="server" />
            </div>
        </div>

        <div class="btn-row">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit for Review"
                CssClass="btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel &mdash; Upload Different File"
                CssClass="btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
        </div>
    </asp:Panel>

    <%-- ── My Submissions History ── --%>
    <div class="form-card" style="margin-top:36px;">
        <div class="card-title">My PMTDP Submissions</div>
        <asp:Panel ID="pnlHistoryEmpty" runat="server" Visible="false">
            <p style="color:var(--muted);font-size:13px;margin:0;">You have not submitted any PMTDP uploads yet.</p>
        </asp:Panel>
        <asp:Panel ID="pnlHistoryGrid" runat="server" Visible="false">
            <div class="preview-wrap">
                <asp:Repeater ID="rptHistory" runat="server">
                    <HeaderTemplate>
                        <table class="preview-table my-uploads-table">
                        <thead><tr>
                            <th>Ref #</th>
                            <th>Submitted</th>
                            <th>Status</th>
                            <th>Reviewer Comment</th>
                            <th></th>
                        </tr></thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("UploadRequestID") %></td>
                            <td><%# Eval("SubmittedDate") %></td>
                            <td><span class="badge-status badge-<%# Eval("Status").ToString().ToLower() %>"><%# Eval("Status") %></span></td>
                            <td><%# Eval("ReviewComment") %></td>
                            <td><%# Eval("Status").ToString() == "Approved"
                                ? string.Format("<a href='j_PMTDPApprovalView.aspx?id={0}' class='btn-view-pmtdp'>View PMTDP &rarr;</a>", Eval("UploadRequestID"))
                                : "" %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody></table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>

</div>

<%-- ── Submission success modal ── --%>
<div class="modal fade" id="modalSubmitSuccess" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document" style="max-width:480px;">
        <div class="modal-content" style="border-radius:14px;border:none;box-shadow:0 8px 40px rgba(0,0,0,.18);">
            <div class="modal-body" style="padding:40px 36px 28px;text-align:center;">
                <div style="width:72px;height:72px;background:#d1fae5;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 20px;font-size:36px;color:#059669;">&#10003;</div>
                <h4 style="margin:0 0 8px;font-size:20px;font-weight:700;color:#1a2b4a;">Submitted for Approval</h4>
                <p style="color:#64748b;font-size:14px;margin:0 0 6px;">Your PMTDP upload has been queued for review.</p>
                <p style="color:#64748b;font-size:13px;margin:0;">A second Planning Unit user must approve the upload before the data is applied. You will <strong>not</strong> be able to approve your own submission.</p>
            </div>
            <div class="modal-footer" style="border:none;padding:0 36px 32px;justify-content:center;gap:12px;">
                <button type="button" class="btn-primary" style="min-width:130px;"
                    data-dismiss="modal" onclick="window.location.href='i_PMTDPUpload.aspx';">Upload Another</button>
                <button type="button" class="btn-secondary" style="min-width:100px;" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<%-- ── Error modal ── --%>
<div class="modal fade" id="modalError" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document" style="max-width:440px;">
        <div class="modal-content" style="border-radius:14px;border:none;box-shadow:0 8px 40px rgba(0,0,0,.18);">
            <div class="modal-body" style="padding:40px 36px 28px;text-align:center;">
                <div style="width:72px;height:72px;background:#fff1f2;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 20px;font-size:36px;color:#be123c;">&#9888;</div>
                <h4 style="margin:0 0 10px;font-size:18px;font-weight:700;color:#1a2b4a;">Action Required</h4>
                <p id="modalErrorMsg" style="color:#64748b;font-size:14px;margin:0;line-height:1.6;"></p>
            </div>
            <div class="modal-footer" style="border:none;padding:0 36px 32px;justify-content:center;">
                <button type="button" class="btn-primary" style="min-width:100px;" data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>
