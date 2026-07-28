<%@ Page Title="Register New User" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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

/* ── Page header ─────────────────────────────────────── */
.rg-header { padding: 36px 0 24px; border-bottom: 1px solid var(--border); margin-bottom: 28px; }
.rg-header .back-link {
    display: inline-flex; align-items: center; gap: 5px;
    font-size: 13px; font-weight: 600; color: var(--accent);
    text-decoration: none; margin-bottom: 10px;
}
.rg-header .back-link:hover { text-decoration: underline; }
.rg-header h2 { margin: 0 0 4px; font-size: 24px; font-weight: 800; color: var(--primary); }
.rg-header p  { margin: 0; font-size: 13px; color: var(--muted); }

/* ── Form cards ──────────────────────────────────────── */
.form-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    padding: 24px 28px; margin-bottom: 20px;
}
.form-card .card-title {
    font-size: 11px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .8px; color: var(--muted);
    padding-bottom: 12px; border-bottom: 1px solid #edf2f7;
    margin-bottom: 20px;
}

/* ── Fields ──────────────────────────────────────────── */
.field { margin-bottom: 16px; }
.field:last-child { margin-bottom: 0; }
.field label {
    display: block; font-size: 13px; font-weight: 600; color: var(--text);
    margin-bottom: 5px;
}
.field label .req { color: var(--danger); margin-left: 2px; }
.field input[type="text"],
.field input[type="password"],
.field select {
    width: 100%; padding: 9px 12px; font-size: 13px;
    font-family: var(--font); color: var(--text);
    border: 1px solid var(--border); border-radius: 8px;
    background: #f8fafc; outline: none;
    transition: border-color .15s, background .15s, box-shadow .15s;
    box-sizing: border-box;
}
.field input:focus, .field select:focus {
    border-color: var(--accent); background: #fff;
    box-shadow: 0 0 0 3px rgba(18,130,106,.12);
}
.field select {
    appearance: none; cursor: pointer;
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M6 8L0 0h12z'/%3E%3C/svg%3E");
    background-repeat: no-repeat; background-position: right 12px center;
    background-color: #f8fafc; padding-right: 32px;
}
.field select:focus { background-color: #fff; }

/* ── Password strength hint ──────────────────────────── */
.pwd-hint { font-size: 11px; color: var(--muted); margin-top: 4px; }

/* ── Grid helpers ────────────────────────────────────── */
.two-col   { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
.three-col { display: grid; grid-template-columns: 1fr 1fr 1fr; gap: 16px; }
@media (max-width: 640px) {
    .two-col, .three-col { grid-template-columns: 1fr; }
}

/* ── Action bar ──────────────────────────────────────── */
.action-bar {
    display: flex; gap: 10px; align-items: center;
    padding: 20px 28px; background: var(--surface);
    border: 1px solid var(--border); border-radius: var(--radius);
    box-shadow: var(--shadow);
}
.btn-save {
    padding: 10px 32px; font-size: 14px; font-weight: 700;
    border-radius: 8px; border: none; cursor: pointer;
    background: var(--accent); color: #fff;
    transition: background .15s, transform .1s;
}
.btn-save:hover  { background: #0f6b57; }
.btn-save:active { transform: scale(.98); }
.btn-cancel {
    padding: 10px 22px; font-size: 14px; font-weight: 600;
    border-radius: 8px; cursor: pointer;
    background: #fff; color: var(--text);
    border: 1.5px solid var(--border);
    text-decoration: none; display: inline-flex; align-items: center;
    transition: background .15s, border-color .15s;
}
.btn-cancel:hover { background: #f1f5f9; border-color: #aab; color: var(--text); }

/* ── Role hint cards ─────────────────────────────────── */
.role-hint {
    display: none; margin-top: 8px; padding: 10px 14px;
    border-radius: 8px; font-size: 12px; line-height: 1.5;
    border-left: 3px solid var(--accent); background: #f0faf7; color: #155a47;
}
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="padding-bottom: 60px; max-width: 860px;">

    <%-- Header --%>
    <div class="rg-header">
        <a href="pageUserAdmin.aspx" class="back-link">&#8592; Back to User Management</a>
        <h2>Register New User</h2>
        
    </div>

    <%-- SECTION 1: Identity --%>
    <div class="form-card">
        <div class="card-title">Identity</div>

        <div class="three-col">
            <div class="field">
                <label for="<%= ddlTitle.ClientID %>">Title</label>
                <asp:DropDownList ID="ddlTitle" runat="server"></asp:DropDownList>
            </div>
            <div class="field">
                <label for="<%= txtFirstname.ClientID %>">First Name <span class="req">*</span></label>
                <asp:TextBox ID="txtFirstname" runat="server" placeholder="e.g. Liyo de Liyo"></asp:TextBox>
            </div>
            <div class="field">
                <label for="<%= txtLastname.ClientID %>">Last Name <span class="req">*</span></label>
                <asp:TextBox ID="txtLastname" runat="server" placeholder="e.g. Jonasi"></asp:TextBox>
            </div>
        </div>

        <div class="two-col">
            <div class="field">
                <label for="<%= txtPersalNumber.ClientID %>">Persal Number <span class="req">*</span></label>
                <asp:TextBox ID="txtPersalNumber" runat="server" placeholder="Persal number"></asp:TextBox>
            </div>
            <div class="field">
                <label for="<%= txtDesignation.ClientID %>">Designation</label>
                <asp:TextBox ID="txtDesignation" runat="server" placeholder="e.g. Director"></asp:TextBox>
            </div>
        </div>
    </div>

    <%-- SECTION 2: Contact --%>
    <div class="form-card">
        <div class="card-title">Contact Details</div>

        <div class="two-col">
            <div class="field">
                <label for="<%= txtEmailAddress.ClientID %>">Email Address <span class="req">*</span></label>
                <asp:TextBox ID="txtEmailAddress" runat="server" placeholder="name@ecotp.gov.za"></asp:TextBox>
            </div>
            <div class="field">
                <label for="<%= txtPhone.ClientID %>">Phone Number</label>
                <asp:TextBox ID="txtPhone" runat="server" placeholder="e.g. 043 000 0000"></asp:TextBox>
            </div>
        </div>

        <div class="two-col">
            <div class="field">
                <label for="<%= ddlDepartment.ClientID %>">Department / Entity <span class="req">*</span></label>
                <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
            </div>
            <div class="field">
                <label for="<%= ddlDistrict.ClientID %>">District Municipality</label>
                <asp:DropDownList ID="ddlDistrict" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>

    <%-- SECTION 3: Access --%>
    <div class="form-card">
        <div class="card-title">System Access</div>

        <div class="two-col">
            <div class="field">
                <label for="<%= ddlUserType.ClientID %>">System Role <span class="req">*</span></label>
                <asp:DropDownList ID="ddlUserType" runat="server" onchange="showRoleHint(this.value)"></asp:DropDownList>
                <div class="role-hint" id="roleHint"></div>
            </div>
            <div class="field">
                <label for="<%= ddlActivation.ClientID %>">Account Status <span class="req">*</span></label>
                <asp:DropDownList ID="ddlActivation" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="two-col" style="margin-top:4px;">
            <div class="field">
                <label for="<%= txtPassword.ClientID %>">Password <span class="req">*</span></label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Set initial password"></asp:TextBox>
                <p class="pwd-hint">Share this password with the user securely after registration.</p>
            </div>
            <div class="field">
                <label for="<%= txtConfirmPassword.ClientID %>">Confirm Password <span class="req">*</span></label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Re-enter password"></asp:TextBox>
            </div>
        </div>
    </div>

    <%-- Action bar --%>
    <div class="action-bar">
        <asp:Button ID="btnRegister" runat="server" Text="Register User"
            CssClass="btn-save" OnClick="btnRegister_Click" OnClientClick="return validateForm();" />
        <a href="pageUserAdmin.aspx" class="btn-cancel">Cancel</a>
        <span id="spanValidationMsg" style="font-size:13px; color:#c0392b; display:none; margin-left:4px;"></span>
    </div>

</div>

<script>
var roleDescriptions = {
    "32": "Full system access. Can manage all users, reference data, and configurations.",
    "37": "Uploads and manages PMTDP plans, POAs, Interventions, Indicators and Targets.",
    "38": "Submits quarterly performance reports for their department or entity.",
    "39": "Reviews and quality-assures submitted reports within their Working Group.",
    "40": "Chairs the Working Group and monitors overall cluster performance.",
    "41": "Cross-cluster monitoring role for the Office of the Premier.",
    "42": "HOD or CEO — approves or rejects QA-passed quarterly reports.",
    "43": "Read-only access to dashboards and published reports."
};

function showRoleHint(val) {
    var hint = document.getElementById('roleHint');
    if (roleDescriptions[val]) {
        hint.textContent = roleDescriptions[val];
        hint.style.display = 'block';
    } else {
        hint.style.display = 'none';
    }
}

function validateForm() {
    var msg = document.getElementById('spanValidationMsg');
    msg.style.display = 'none';

    var first  = document.getElementById('<%= txtFirstname.ClientID %>').value.trim();
    var last   = document.getElementById('<%= txtLastname.ClientID %>').value.trim();
    var persal = document.getElementById('<%= txtPersalNumber.ClientID %>').value.trim();
    var email  = document.getElementById('<%= txtEmailAddress.ClientID %>').value.trim();
    var dept   = document.getElementById('<%= ddlDepartment.ClientID %>').value;
    var role   = document.getElementById('<%= ddlUserType.ClientID %>').value;
    var act    = document.getElementById('<%= ddlActivation.ClientID %>').value;
    var pwd    = document.getElementById('<%= txtPassword.ClientID %>').value;
    var cpwd   = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;

    if (!first)  { showMsg('First name is required.'); return false; }
    if (!last)   { showMsg('Last name is required.'); return false; }
    if (!persal) { showMsg('Persal number is required.'); return false; }
    if (!email)  { showMsg('Email address is required.'); return false; }
    if (dept === '0') { showMsg('Please select a department.'); return false; }
    if (role === '0') { showMsg('Please select a system role.'); return false; }
    if (act  === '0') { showMsg('Please select an account status.'); return false; }
    if (!pwd)         { showMsg('Password is required.'); return false; }
    if (pwd !== cpwd) { showMsg('Passwords do not match.'); return false; }

    return true;

    function showMsg(text) {
        msg.textContent = text;
        msg.style.display = 'inline';
    }
}
</script>
</asp:Content>
