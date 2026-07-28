<%@ Page Title="POA Upload — Review" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="j_POAUploadApprovalView.aspx.cs" Inherits="j_POAUploadApprovalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
:root {
    --primary: #0C2D48; --accent: #12826A; --danger: #c0392b;
    --border: #dce3ec; --bg: #f7f8fb; --surface: #fff;
    --text: #1a2b4a; --muted: #64748b;
    --radius: 12px; --shadow: 0 2px 12px rgba(0,0,0,.07);
    --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }
.rv-header { padding: 36px 0 22px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.rv-header .back-link { display:inline-flex; align-items:center; gap:5px; font-size:13px; font-weight:600; color:var(--accent); text-decoration:none; margin-bottom:10px; }
.rv-header .back-link:hover { text-decoration:underline; }
.rv-header h2 { margin:0 0 4px; font-size:22px; font-weight:800; color:var(--primary); }
.rv-header p  { margin:0; font-size:13px; color:var(--muted); }
.form-card {
    background:var(--surface); border:1px solid var(--border);
    border-radius:var(--radius); box-shadow:var(--shadow);
    padding:22px 26px; margin-bottom:20px;
}
.form-card .card-title {
    font-size:11px; font-weight:700; text-transform:uppercase;
    letter-spacing:.8px; color:var(--muted);
    padding-bottom:10px; border-bottom:1px solid #edf2f7; margin-bottom:16px;
}
.meta-grid { display:grid; grid-template-columns:1fr 1fr; gap:12px; }
.meta-item { background:#f8fafc; border:1px solid var(--border); border-radius:8px; padding:10px 14px; }
.meta-item .meta-label { font-size:11px; font-weight:700; text-transform:uppercase; letter-spacing:.5px; color:var(--muted); margin-bottom:3px; }
.meta-item .meta-value { font-size:13px; font-weight:600; color:var(--text); }

.field { margin-bottom:16px; }
.field:last-child { margin-bottom:0; }
.field label { display:block; font-size:13px; font-weight:600; color:var(--text); margin-bottom:5px; }
.field label .req { color:var(--danger); margin-left:2px; }
.field input[type="text"], .field select, .field textarea {
    width:100%; padding:9px 12px; font-size:13px;
    border:1px solid var(--border); border-radius:8px;
    background:#f8fafc; color:var(--text); outline:none; box-sizing:border-box;
    transition:border-color .15s, box-shadow .15s;
}
.field input[type="text"]:focus, .field select:focus, .field textarea:focus {
    border-color:var(--accent); background:#fff;
    box-shadow:0 0 0 3px rgba(18,130,106,.12);
}
.field select { appearance:none; cursor:pointer;
    background-image:url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M6 8L0 0h12z'/%3E%3C/svg%3E");
    background-repeat:no-repeat; background-position:right 12px center; padding-right:32px; }
.two-col { display:grid; grid-template-columns:1fr 1fr; gap:16px; }
@media (max-width:640px) { .two-col, .meta-grid { grid-template-columns:1fr; } }

.msg-success { background:#f0fdf4; color:#14532d; border:1px solid #86efac; border-left:4px solid var(--accent); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-error   { background:#fff1f2; color:#9f1239; border:1px solid #fecdd3; border-left:4px solid var(--danger); border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.msg-warn    { background:#fefce8; color:#713f12; border:1px solid #fde68a; border-left:4px solid #f59e0b; border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }

.preview-wrap { overflow-x:auto; }
.preview-table { width:100%; border-collapse:collapse; font-size:12px; min-width:800px; }
.preview-table th { background:var(--primary); color:#fff; padding:8px 10px; text-align:left; white-space:nowrap; font-weight:600; }
.preview-table td { padding:7px 10px; border-bottom:1px solid #edf2f7; }
.preview-table tr:nth-child(even) td { background:#f8fafc; }

.btn-row { display:flex; gap:10px; align-items:center; flex-wrap:wrap; margin-top:8px; }
.btn-approve { padding:10px 26px; font-size:14px; font-weight:700; border:none; border-radius:8px; cursor:pointer; background:var(--accent); color:#fff; transition:background .15s; }
.btn-approve:hover { background:#0f6b57; }
.btn-reject  { padding:10px 22px; font-size:14px; font-weight:700; border:none; border-radius:8px; cursor:pointer; background:#fff; color:var(--danger); border:1.5px solid var(--danger); transition:background .15s; }
.btn-reject:hover { background:#fff1f2; }

.cluster-hint { display:none; font-size:12px; color:var(--muted); margin-top:4px; background:#f8fafc; border-radius:6px; padding:6px 10px; border:1px solid var(--border); }
.info-banner { background:#eff6ff; color:#1e40af; border:1px solid #bfdbfe; border-left:4px solid #3b82f6; border-radius:8px; padding:12px 16px; margin-bottom:16px; font-size:13px; }
.owner-banner {
    display:flex; align-items:flex-start; gap:16px;
    background:#fffbeb; border:1px solid #fcd34d;
    border-left:5px solid #f59e0b;
    border-radius:var(--radius); padding:18px 22px; margin-bottom:24px;
}
.owner-banner .icon {
    flex-shrink:0; width:44px; height:44px;
    background:#fef3c7; border-radius:50%;
    display:flex; align-items:center; justify-content:center;
    font-size:22px; color:#b45309;
}
.owner-banner .body strong { display:block; font-size:15px; font-weight:700; color:#92400e; margin-bottom:4px; }
.owner-banner .body p { margin:0; font-size:13px; color:#78350f; line-height:1.6; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="padding-top:80px; padding-bottom:60px; max-width:1100px;">

    <div class="rv-header">
        <a href="j_POAUploadApprovalList.aspx" class="back-link">&#8592; Back to Approval List</a>
        <h2>Review POA Upload</h2>
        <p>Verify the data below, map it to an existing PMTDP Priority, then approve or reject.</p>
    </div>

    <asp:Panel ID="pnlAlreadyReviewed" runat="server" Visible="false">
        <div class="msg-warn"><i class="glyphicon glyphicon-info-sign"></i> This upload has already been reviewed and is currently <strong><asp:Literal ID="litCurrentStatus" runat="server" /></strong>. No further changes can be made.</div>
    </asp:Panel>

    <asp:Panel ID="pnlError"   runat="server" Visible="false"><div class="msg-error"><i class="glyphicon glyphicon-exclamation-sign"></i> <asp:Literal ID="litError" runat="server" /></div></asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false"><div class="msg-success"><i class="glyphicon glyphicon-ok-circle"></i> <asp:Literal ID="litSuccess" runat="server" Mode="PassThrough" /></div></asp:Panel>

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

    <%-- Upload Header info --%>
    <div class="form-card">
        <div class="card-title">Upload Details</div>
        <div class="meta-grid">
            <div class="meta-item">
                <div class="meta-label">Submitted By</div>
                <div class="meta-value"><asp:Literal ID="litUploadedBy" runat="server" /></div>
            </div>
            <div class="meta-item">
                <div class="meta-label">Submitted On</div>
                <div class="meta-value"><asp:Literal ID="litUploadDate" runat="server" /></div>
            </div>
            <div class="meta-item">
                <div class="meta-label">Priority Focus (from Excel)</div>
                <div class="meta-value"><asp:Literal ID="litPriorityFocus" runat="server" /></div>
            </div>
            <div class="meta-item">
                <div class="meta-label">Integration Programme</div>
                <div class="meta-value"><asp:Literal ID="litProgramme" runat="server" /></div>
            </div>
            <div class="meta-item">
                <div class="meta-label">Goal</div>
                <div class="meta-value"><asp:Literal ID="litGoal" runat="server" /></div>
            </div>
            <div class="meta-item">
                <div class="meta-label">Impact Statement</div>
                <div class="meta-value"><asp:Literal ID="litImpact" runat="server" /></div>
            </div>
        </div>
    </div>

    <%-- Data preview --%>
    <div class="form-card">
        <div class="card-title">Data Preview</div>
        <div class="preview-wrap">
            <asp:PlaceHolder ID="phDataTable" runat="server" />
        </div>
    </div>

    <%-- Approval form (only for approvers on Pending uploads) --%>
    <asp:Panel ID="pnlApprovalForm" runat="server" Visible="false">
        <div class="form-card">
            <div class="card-title">Mapping &amp; Approval</div>

            <div class="info-banner">
                <strong>Approve &amp; Create Records:</strong> selecting a PMTDP Priority and POA dates below
                will create the POA when you click
                &ldquo;Approve &amp; Apply.&rdquo;
            </div>

            <div class="field">
                <label for="<%= ddlPriority.ClientID %>">PMTDP Priority (link to existing) <span class="req">*</span></label>
                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control"
                    onchange="updateClusterHint(this)">
                    <asp:ListItem Text="-- Select PMTDP Priority --" Value="0" />
                </asp:DropDownList>
                <div class="cluster-hint" id="clusterHint">Cluster: <span id="clusterName"></span></div>
            </div>

            <div class="field">
                <label for="<%= txtPOAName.ClientID %>">POA Name (for the new record) <span class="req">*</span></label>
                <asp:TextBox ID="txtPOAName" runat="server" placeholder="e.g. Inclusive Economic Growth POA 2025–2030" />
            </div>

            <div class="two-col">
                <div class="field">
                    <label for="<%= ddlStartYear.ClientID %>">POA Start Year <span class="req">*</span></label>
                    <asp:DropDownList ID="ddlStartYear" runat="server">
                        <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="2025" Value="2025" />
                        <asp:ListItem Text="2026" Value="2026" />
                        <asp:ListItem Text="2027" Value="2027" />
                        <asp:ListItem Text="2028" Value="2028" />
                        <asp:ListItem Text="2029" Value="2029" />
                        <asp:ListItem Text="2030" Value="2030" />
                    </asp:DropDownList>
                </div>
                <div class="field">
                    <label for="<%= ddlEndYear.ClientID %>">POA End Year <span class="req">*</span></label>
                    <asp:DropDownList ID="ddlEndYear" runat="server">
                        <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="2025" Value="2025" />
                        <asp:ListItem Text="2026" Value="2026" />
                        <asp:ListItem Text="2027" Value="2027" />
                        <asp:ListItem Text="2028" Value="2028" />
                        <asp:ListItem Text="2029" Value="2029" />
                        <asp:ListItem Text="2030" Value="2030" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="field">
                <label for="<%= txtComment.ClientID %>">Reviewer Comment <span style="font-weight:400; color:var(--muted); font-size:12px;">(optional for approval, required for rejection)</span></label>
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" placeholder="Add any notes for the submitter..." />
            </div>

            <div class="btn-row">
                <asp:Button ID="btnApprove" runat="server" Text="Approve &amp; Apply" CssClass="btn-approve"
                    OnClick="btnApprove_Click" OnClientClick="return validateApproval();" />
                <asp:Button ID="btnReject"  runat="server" Text="Reject"              CssClass="btn-reject"
                    OnClick="btnReject_Click" OnClientClick="return validateRejection();" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <%-- Hidden field stores cluster data for JS --%>
    <asp:HiddenField ID="hfPriorityData" runat="server" />

</div>

<script>
var priorityData = {};
(function() {
    var raw = document.getElementById('<%= hfPriorityData.ClientID %>').value;
    if (raw) {
        try { priorityData = JSON.parse(raw); } catch(e) {}
    }
})();

function updateClusterHint(sel) {
    var hint = document.getElementById('clusterHint');
    var span = document.getElementById('clusterName');
    var d = priorityData[sel.value];
    if (d) {
        span.textContent = d.clusterName;
        hint.style.display = 'block';
    } else {
        hint.style.display = 'none';
    }
}

function validateApproval() {
    var pri  = document.getElementById('<%= ddlPriority.ClientID %>').value;
    var name = document.getElementById('<%= txtPOAName.ClientID %>').value.trim();
    var sy   = document.getElementById('<%= ddlStartYear.ClientID %>').value;
    var ey   = document.getElementById('<%= ddlEndYear.ClientID %>').value;
    if (pri === '0')  { alert('Please select a PMTDP Priority.'); return false; }
    if (!name)        { alert('Please enter a POA Name.'); return false; }
    if (sy === '0')   { alert('Please select a Start Year.'); return false; }
    if (ey === '0')   { alert('Please select an End Year.'); return false; }
    if (parseInt(ey) < parseInt(sy)) { alert('End Year must be greater than or equal to Start Year.'); return false; }
    return confirm('Approve this upload and create POA records in the live database?');
}

function validateRejection() {
    var comment = document.getElementById('<%= txtComment.ClientID %>').value.trim();
    if (!comment) { alert('Please provide a reason for rejection in the Comment field.'); return false; }
    return confirm('Reject this upload? The submitter will see your comment.');
}
</script>
</asp:Content>
