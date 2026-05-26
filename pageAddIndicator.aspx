<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageAddIndicator.aspx.cs" Inherits="pageAddIndicator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
:root {
    --bg:      #f7f8fb;
    --surface: #ffffff;
    --border:  #dce3ec;
    --text:    #1a2b4a;
    --muted:   #64748b;
    --primary: #0b5ed7;
    --green:   #1d6f42;
    --danger:  #dc3545;
    --radius:  12px;
    --shadow:  0 2px 12px rgba(0,0,0,.07);
    --font:    "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}
body { background: var(--bg); font-family: var(--font); color: var(--text); }

/* ── Page header ───────────────────────────────────────────── */
.ai-header { padding: 40px 0 28px; border-bottom: 1px solid var(--border); margin-bottom: 32px; }
.ai-header .back-link {
    display: inline-flex; align-items: center; gap: 5px;
    font-size: 13px; font-weight: 600; color: var(--primary);
    text-decoration: none; margin-bottom: 12px;
}
.ai-header .back-link:hover { text-decoration: underline; }
.ai-header h2 { margin: 0 0 4px; font-size: 26px; font-weight: 800; color: var(--text); }
.ai-header p  { margin: 0; font-size: 13px; color: var(--muted); }

/* ── Form card ─────────────────────────────────────────────── */
.form-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    padding: 28px 32px; margin-bottom: 24px;
}
.form-card .card-title {
    font-size: 13px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .5px; color: var(--muted);
    padding-bottom: 12px; border-bottom: 1px solid #edf2f7;
    margin-bottom: 20px;
}

/* ── Form fields ───────────────────────────────────────────── */
.field   { margin-bottom: 18px; }
.field label {
    display: block; font-size: 13px; font-weight: 600; color: var(--text);
    margin-bottom: 6px;
}
.field label .opt {
    font-weight: 400; color: var(--muted); font-size: 12px; margin-left: 4px;
}
.field input[type="text"],
.field textarea,
.field select {
    width: 100%; padding: 9px 12px;
    font-size: 13px; font-family: var(--font); color: var(--text);
    border: 1px solid var(--border); border-radius: 8px;
    background: #f8fafc; outline: none;
    transition: border-color .15s, background .15s;
    box-sizing: border-box;
}
.field input[type="text"]:focus,
.field textarea:focus,
.field select:focus {
    border-color: var(--primary); background: #fff;
    box-shadow: 0 0 0 3px rgba(11,94,215,.1);
}
.field textarea { resize: vertical; min-height: 90px; }
.field select   { appearance: none; cursor: pointer;
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M6 8L0 0h12z'/%3E%3C/svg%3E");
    background-repeat: no-repeat; background-position: right 12px center;
    padding-right: 32px;
}

/* Two-column grid */
.two-col { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
@media (max-width: 560px) { .two-col { grid-template-columns: 1fr; } }

/* ── Validation messages ───────────────────────────────────── */
.val-msg {
    display: block; font-size: 12px; color: var(--danger);
    margin-top: 4px;
}
.val-summary-box {
    background: #fff1f2; border: 1px solid #fecdd3; border-left: 4px solid var(--danger);
    border-radius: var(--radius); padding: 14px 18px; margin-bottom: 24px;
    font-size: 13px; color: #9f1239;
}
.val-summary-box ul { margin: 6px 0 0 18px; padding: 0; }

/* ── Buttons ───────────────────────────────────────────────── */
.btn-row { display: flex; gap: 10px; margin-top: 8px; flex-wrap: wrap; align-items: center; }
.btn-primary {
    padding: 10px 28px; font-size: 14px; font-weight: 600;
    border-radius: 8px; border: none; cursor: pointer;
    background: var(--green); color: #fff;
    transition: background .15s;
}
.btn-primary:hover { background: #155a34; }
.btn-secondary {
    padding: 10px 22px; font-size: 14px; font-weight: 600;
    border-radius: 8px; cursor: pointer;
    background: #fff; color: var(--text);
    border: 1.5px solid var(--border);
    transition: background .15s, border-color .15s;
}
.btn-secondary:hover { background: #f1f5f9; border-color: #aab; }

/* ── Success panel ─────────────────────────────────────────── */
.success-card {
    background: #f0fdf4; border: 1px solid #86efac; border-left: 5px solid var(--green);
    border-radius: var(--radius); padding: 24px 28px;
}
.success-card .tick {
    width: 52px; height: 52px; background: #d1fae5; border-radius: 50%;
    display: flex; align-items: center; justify-content: center;
    font-size: 26px; color: var(--green); margin-bottom: 14px;
}
.success-card h3 { margin: 0 0 6px; font-size: 18px; font-weight: 700; color: #14532d; }
.success-card p  { margin: 0 0 18px; font-size: 13px; color: #166534; }
.success-card .action-links { display: flex; gap: 10px; flex-wrap: wrap; }
.success-card .action-links a {
    display: inline-block; padding: 9px 20px; border-radius: 8px;
    font-size: 13px; font-weight: 600; text-decoration: none;
}
.link-primary { background: var(--green); color: #fff; }
.link-primary:hover { background: #155a34; color: #fff; }
.link-outline { border: 1.5px solid var(--border); background: #fff; color: var(--text); }
.link-outline:hover { background: #f1f5f9; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

<div class="container" style="padding-bottom:60px; max-width:820px;">

    <%-- Page header --%>
    <div class="ai-header">
        <asp:HyperLink ID="hlBackToOverview" runat="server" CssClass="back-link">
            &#8592; Back
        </asp:HyperLink>
        <h2>Add Indicator</h2>
        <p>Complete the form below to add an indicator to the selected Intervention.</p>
    </div>

    <%-- Validation summary --%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        ShowErrors="True"
        HeaderText="Please correct the following errors:"
        CssClass="val-summary-box" />

    <%-- ── Form panel ─────────────────────────────────────── --%>
    <asp:Panel ID="pnlForm" runat="server">

        <%-- SECTION 1: Intervention --%>
        <div class="form-card">
            <div class="card-title">Intervention</div>

            <div class="field">
                <label for="<%= ddlIntervention.ClientID %>">Intervention</label>
                <asp:DropDownList ID="ddlIntervention" runat="server" />
                <asp:RequiredFieldValidator ID="rfvIntervention" runat="server"
                    ControlToValidate="ddlIntervention" InitialValue="0"
                    ErrorMessage="Intervention is required."
                    Display="Dynamic" CssClass="val-msg" />
            </div>
        </div>

        <%-- SECTION 2: Indicator Details --%>
        <div class="form-card">
            <div class="card-title">Indicator Details</div>

            <div class="field">
                <label for="<%= txtIndicatorName.ClientID %>">Indicator Name</label>
                <asp:TextBox ID="txtIndicatorName" runat="server" />
                <asp:RequiredFieldValidator ID="rfvIndicatorName" runat="server"
                    ControlToValidate="txtIndicatorName"
                    ErrorMessage="Indicator Name is required."
                    Display="Dynamic" CssClass="val-msg" />
            </div>

            <div class="two-col">
                <div class="field">
                    <label for="<%= ddlIndicatorType.ClientID %>">Indicator Type</label>
                    <asp:DropDownList ID="ddlIndicatorType" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvIndicatorType" runat="server"
                        ControlToValidate="ddlIndicatorType" InitialValue="0"
                        ErrorMessage="Indicator Type is required."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
                <div class="field">
                    <label for="<%= txtUnitOfMeasure.ClientID %>">
                        Unit of Measure <span class="opt">(optional)</span>
                    </label>
                    <asp:TextBox ID="txtUnitOfMeasure" runat="server"
                        placeholder="e.g. %, number, rand" />
                </div>
            </div>
        </div>

        <%-- SECTION 3: Baseline --%>
        <div class="form-card">
            <div class="card-title">Baseline</div>

            <div class="two-col">
                <div class="field">
                    <label for="<%= txtBaselineValue.ClientID %>">Baseline Value</label>
                    <asp:TextBox ID="txtBaselineValue" runat="server" placeholder="e.g. 42.5" />
                    <asp:RequiredFieldValidator ID="rfvBaselineValue" runat="server"
                        ControlToValidate="txtBaselineValue"
                        ErrorMessage="Baseline Value is required."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:CompareValidator ID="cvBaselineValue" runat="server"
                        ControlToValidate="txtBaselineValue"
                        Operator="DataTypeCheck" Type="Double"
                        ErrorMessage="Baseline Value must be a number."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
                <div class="field">
                    <label for="<%= txtBaselineYear.ClientID %>">Baseline Year</label>
                    <asp:TextBox ID="txtBaselineYear" runat="server" placeholder="e.g. 2024" />
                    <asp:RequiredFieldValidator ID="rfvBaselineYear" runat="server"
                        ControlToValidate="txtBaselineYear"
                        ErrorMessage="Baseline Year is required."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:RangeValidator ID="rvBaselineYear" runat="server"
                        ControlToValidate="txtBaselineYear" Type="Integer"
                        MinimumValue="1900" MaximumValue="2100"
                        ErrorMessage="Baseline Year must be a valid year."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
            </div>
        </div>

        <%-- SECTION 4: Target Information --%>
        <div class="form-card">
            <div class="card-title">Target Information <span style="font-weight:400;text-transform:none;letter-spacing:0;">(optional)</span></div>

            <div class="two-col">
                <div class="field">
                    <label for="<%= txtTargetValue.ClientID %>">
                        Target Value <span class="opt">(optional)</span>
                    </label>
                    <asp:TextBox ID="txtTargetValue" runat="server" placeholder="e.g. 80.0" />
                    <asp:CompareValidator ID="cvTargetValue" runat="server"
                        ControlToValidate="txtTargetValue"
                        Operator="DataTypeCheck" Type="Double"
                        ErrorMessage="Target Value must be a number."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
                <div class="field">
                    <label for="<%= txtTargetYear.ClientID %>">
                        Target Year <span class="opt">(optional)</span>
                    </label>
                    <asp:TextBox ID="txtTargetYear" runat="server" placeholder="e.g. 2030" />
                    <asp:RangeValidator ID="rvTargetYear" runat="server"
                        ControlToValidate="txtTargetYear" Type="Integer"
                        MinimumValue="1900" MaximumValue="2100"
                        ErrorMessage="Target Year must be a valid year."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:CompareValidator ID="cvTargetYearCompare" runat="server"
                        ControlToValidate="txtTargetYear" ControlToCompare="txtBaselineYear"
                        Operator="GreaterThanEqual" Type="Integer"
                        ErrorMessage="Target Year must be on or after Baseline Year."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
            </div>

            <div class="field">
                <label for="<%= txtTarget2030TermTarget.ClientID %>">
                    2030 Term Target <span class="opt">(optional)</span>
                </label>
                <asp:TextBox ID="txtTarget2030TermTarget" runat="server" TextMode="MultiLine"
                    placeholder="Describe the 2030 term target..." />
            </div>
        </div>

        <%-- DB error message --%>
        <asp:Label ID="lblDbError" runat="server" Visible="false" CssClass="val-summary-box" />

        <%-- Action buttons --%>
        <div class="btn-row">
            <asp:Button ID="btnSubmit" runat="server" Text="Add Indicator"
                OnClick="btnSubmit_Click" CssClass="btn-primary"
                OnClientClick="return showConfirm('<%= btnSubmit.ClientID %>');" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                OnClick="btnCancel_Click" CausesValidation="false" CssClass="btn-secondary" />
        </div>

    </asp:Panel>

    <%-- ── Success panel (shown after save) ───────────────── --%>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
        <div class="success-card">
            <div class="tick">&#10003;</div>
            <h3>Indicator Added Successfully</h3>
            <p>The indicator has been saved. What would you like to do next?</p>
            <div class="action-links">
                <asp:HyperLink ID="hlAddAnother" runat="server"
                    CssClass="action-links link-primary">
                    + Add Another Indicator
                </asp:HyperLink>
                <asp:HyperLink ID="hlViewIntervention" runat="server"
                    CssClass="action-links link-outline">
                    View Intervention
                </asp:HyperLink>
                <asp:HyperLink ID="hlBackToOverview2" runat="server"
                    CssClass="action-links link-outline">
                    Back to Overview
                </asp:HyperLink>
            </div>
        </div>
    </asp:Panel>

</div>

<%-- Confirmation modal --%>
<div id="confirmModal" style="display:none;position:fixed;inset:0;background:rgba(0,0,0,.5);z-index:9999;align-items:center;justify-content:center;">
    <div style="background:#fff;border-radius:12px;padding:32px 28px;max-width:380px;width:90%;box-shadow:0 20px 60px rgba(0,0,0,.25);text-align:center;font-family:'Segoe UI',Roboto,Arial,sans-serif;">
        <div style="width:52px;height:52px;background:#fef9c3;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 16px;font-size:26px;color:#d97706;">&#9998;</div>
        <h3 style="margin:0 0 8px;font-size:18px;font-weight:700;color:#1a2b4a;">Confirm Save</h3>
        <p style="margin:0 0 24px;font-size:13px;color:#64748b;">Are you sure you want to save this record?</p>
        <div style="display:flex;gap:10px;justify-content:center;">
            <button type="button" onclick="cancelConfirm()" style="padding:9px 22px;border-radius:8px;border:1.5px solid #dce3ec;background:#fff;cursor:pointer;font-size:13px;font-weight:600;color:#1a2b4a;">Cancel</button>
            <button type="button" onclick="doConfirm()" style="padding:9px 22px;border-radius:8px;border:none;background:#1d6f42;color:#fff;cursor:pointer;font-size:13px;font-weight:600;">Yes, Save</button>
        </div>
    </div>
</div>
<script type="text/javascript">
    var _confirmed = false, _btnId = '';
    function showConfirm(id) {
        if (_confirmed) { _confirmed = false; return true; }
        _btnId = id;
        document.getElementById('confirmModal').style.display = 'flex';
        return false;
    }
    function doConfirm() {
        document.getElementById('confirmModal').style.display = 'none';
        _confirmed = true;
        document.getElementById(_btnId).click();
    }
    function cancelConfirm() {
        document.getElementById('confirmModal').style.display = 'none';
    }
</script>
</asp:Content>
