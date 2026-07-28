<%@ Page Title="POA Upload" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="i_POAUpload.aspx.cs" Inherits="i_POAUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
:root {
    --bg:      #f7f8fb;
    --surface: #ffffff;
    --border:  #dce3ec;
    --text:    #1a2b4a;
    --muted:   #64748b;
    --primary: #0C2D48;
    --accent:  #12826A;
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
.field input[type="file"]:hover { border-color: var(--accent); background: #f0faf7; }

.msg-info    { background:#eff6ff; color:#1e40af; border:1px solid #bfdbfe; border-left:4px solid #3b82f6; border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-success { background:#f0fdf4; color:#14532d; border:1px solid #86efac; border-left:4px solid var(--accent); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-error   { background:#fff1f2; color:#9f1239; border:1px solid #fecdd3; border-left:4px solid var(--danger); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }

.meta-grid { display:grid; grid-template-columns:1fr 1fr; gap:12px; }
.meta-item { background:#f8fafc; border:1px solid var(--border); border-radius:8px; padding:12px 14px; }
.meta-item .meta-label { font-size:11px; font-weight:700; text-transform:uppercase; letter-spacing:.5px; color:var(--muted); margin-bottom:4px; }
.meta-item .meta-value { font-size:13px; font-weight:600; color:var(--text); }

.preview-wrap { overflow-x: auto; margin-top: 8px; }
.preview-table { width: 100%; border-collapse: collapse; font-size: 12px; min-width: 900px; }
.preview-table th { background: var(--primary); color: #fff; padding: 8px 10px; text-align: left; white-space: nowrap; font-weight: 600; }
.preview-table td { padding: 7px 10px; border-bottom: 1px solid #edf2f7; vertical-align: top; }
.preview-table tr:hover td { background: #f8fafc; }
.preview-table td:empty::after { content: "—"; color: #cbd5e1; }

.badge-status { display:inline-block; padding:2px 8px; border-radius:10px; font-size:11px; font-weight:600; }
.badge-pending  { background:#fef3c7; color:#92400e; }
.badge-approved { background:#d1fae5; color:#065f46; }
.badge-rejected { background:#fee2e2; color:#991b1b; }

.row-count { display:inline-block; background:#e0f2fe; color:#0369a1; padding:2px 8px; border-radius:10px; font-size:12px; font-weight:700; margin-left:8px; }

.btn-primary   { padding:9px 24px; font-size:13px; font-weight:700; border:none; border-radius:8px; cursor:pointer; background:var(--accent); color:#fff; transition:background .15s; }
.btn-primary:hover { background:#0f6b57; }
.btn-secondary { padding:9px 20px; font-size:13px; font-weight:600; border-radius:8px; cursor:pointer; background:#fff; color:var(--text); border:1.5px solid var(--border); transition:background .15s; }
.btn-secondary:hover { background:#f1f5f9; }
.btn-row { display:flex; gap:10px; align-items:center; flex-wrap:wrap; }

.my-uploads-table th { background:#0C2D48; color:#fff; font-size:12px; font-weight:600; padding:8px 10px; }
.my-uploads-table td { font-size:12px; padding:7px 10px; vertical-align:middle; }

.step-indicator { display:flex; gap:0; margin-bottom:28px; }
.step { flex:1; text-align:center; padding:10px 4px; font-size:12px; font-weight:600;
        border-bottom:3px solid #e2e8f0; color:#94a3b8; }
.step.active { border-color:var(--accent); color:var(--accent); }
.step.done   { border-color:#64748b; color:#64748b; }

.btn-review {
    display:inline-block; padding:4px 12px; border-radius:6px;
    font-size:11px; font-weight:700; color:#fff; text-decoration:none;
    white-space:nowrap; transition:opacity .15s;
}
.btn-review:hover { opacity:.85; color:#fff; text-decoration:none; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="padding-top:80px; padding-bottom:60px; max-width:1100px;">

    <div class="pu-header">
        <h2><i class="glyphicon glyphicon-upload"></i> POA Upload</h2>
        <p>Upload your department's Programme of Action Excel file for review and approval.</p>
    </div>

    <%-- Alerts --%>
    <asp:Panel ID="pnlError"   runat="server" Visible="false"><div class="msg-error"><i class="glyphicon glyphicon-exclamation-sign"></i> <asp:Literal ID="litError" runat="server" /></div></asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false"><div class="msg-success"><i class="glyphicon glyphicon-ok-circle"></i> <asp:Literal ID="litSuccess" runat="server" /></div></asp:Panel>

    <%-- Resubmit notice (shown when ?resubmit= param is present) --%>
    <asp:Panel ID="pnlResubmitNotice" runat="server" Visible="false">
        <div style="background:#fefce8;border:1px solid #fde68a;border-left:4px solid #f59e0b;border-radius:8px;padding:12px 16px;margin-bottom:16px;font-size:13px;color:#713f12;">
            <i class="glyphicon glyphicon-refresh"></i>
            <strong>Resubmitting a rejected upload.</strong>
            Upload a corrected Excel file below. The previous data will be replaced and the request will return to <em>Pending</em> for re-review.
        </div>
    </asp:Panel>

    <%-- STEP 1: File upload --%>
    <asp:Panel ID="pnlUpload" runat="server">
        <div class="step-indicator">
            <div class="step active">1. Upload File</div>
            <div class="step">2. Preview</div>
            <div class="step">3. Submit for Review</div>
        </div>

        <div class="form-card">
            <div class="card-title">Select POA Excel File</div>
            <div class="msg-info">
                <strong>Before uploading:</strong> ensure your file follows the standard POA template format
                (4 metadata rows for Goal / Priority Focus / Integration Programme / Impact, then the column header row, then data rows).
                &nbsp;&nbsp;<a href="DownloadPOATemplate.aspx" style="font-weight:700; color:#0C2D48;">
                    <i class="glyphicon glyphicon-download-alt"></i> Download Template
                </a>
            </div>
            <div class="field" style="margin-top:16px;">
                <label>POA Excel File (.xlsx) <span style="color:var(--danger)">*</span></label>
                <asp:FileUpload ID="fuPOA" runat="server" accept=".xlsx,.xls" />
            </div>
            <div style="margin-top:16px;">
                <asp:Button ID="btnPreview" runat="server" Text="Upload &amp; Preview" CssClass="btn-primary" OnClick="btnPreview_Click" />
            </div>
        </div>
    </asp:Panel>

    <%-- STEP 2: Preview + Confirm --%>
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

        <%-- Data preview --%>
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
            <asp:Button ID="btnSubmit" runat="server" Text="Submit for Review" CssClass="btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel - Upload Different File" CssClass="btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
        </div>
    </asp:Panel>

    <%-- My Uploads --%>
    <div class="form-card" style="margin-top:36px;">
        <div class="card-title">My Previous Uploads</div>
        <asp:Panel ID="pnlNoUploads" runat="server">
            <p class="text-muted" style="font-size:13px;">You have not submitted any POA uploads yet.</p>
        </asp:Panel>
        <asp:Panel ID="pnlMyUploads" runat="server" Visible="false">
            <div class="preview-wrap">
                <table class="preview-table my-uploads-table">
                    <thead><tr>
                        <th>Submitted</th>
                        <th>Priority Focus</th>
                        <th>Integration Programme</th>
                        <th>Rows</th>
                        <th>Status</th>
                        <th>Reviewer Note</th>
                        <th></th>
                    </tr></thead>
                    <tbody>
                        <asp:Repeater ID="rptMyUploads" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("UploadDate", "{0:yyyy-MM-dd}") %></td>
                                    <td><%# Server.HtmlEncode(Eval("PriorityFocus").ToString()) %></td>
                                    <td><%# Server.HtmlEncode(Eval("IntegrationProgramme").ToString()) %></td>
                                    <td><%# Eval("DataRowCount") %></td>
                                    <td><span class="badge-status badge-<%# Eval("Status").ToString().ToLower() %>"><%# Eval("Status") %></span></td>
                                    <td><%# Server.HtmlEncode(Eval("ReviewComment").ToString()) %></td>
                                    <td>
                                        <%# (Eval("Status").ToString() == "Approved" && Eval("CreatedPOA_ID") != DBNull.Value)
                                            ? string.Format("<a href='pagePOADetail.aspx?id={0}' class='btn-review' style='background:#12826A;'>View POA &rarr;</a>", Eval("CreatedPOA_ID"))
                                            : Eval("Status").ToString() == "Rejected"
                                            ? string.Format("<a href='i_POAUpload.aspx?resubmit={0}' class='btn-review' style='background:#b45309;'>Resubmit</a>", Eval("UploadRequestID"))
                                            : "" %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>

</div>
</asp:Content>
