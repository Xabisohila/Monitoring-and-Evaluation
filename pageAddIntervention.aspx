<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageAddIntervention.aspx.cs" Inherits="pageAddIntervention" %>
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
    --pd:      #094db0;
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

/* Two-column grid for year fields */
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

/* ── Context tag ───────────────────────────────────────────── */
.ctx-tag {
    display: inline-block; padding: 3px 12px; border-radius: 20px;
    background: #e0ecff; color: #1e40af; font-size: 12px; font-weight: 700;
    letter-spacing: .3px; margin-left: 8px; vertical-align: middle;
}

/* ── PMTDP source toggle ───────────────────────────────────── */
.source-toggle {
    display: flex; border: 1.5px solid var(--border);
    border-radius: 8px; overflow: hidden; width: fit-content;
}
.source-opt {
    display: flex; align-items: center; gap: 7px;
    padding: 8px 20px; font-size: 13px; font-weight: 600;
    cursor: pointer; color: var(--muted); background: #f8fafc;
    transition: background .12s, color .12s; user-select: none;
}
.source-opt:first-child { border-right: 1.5px solid var(--border); }
.source-opt input[type=radio] { display: none; }
.source-opt.active { background: var(--primary); color: #fff; }

/* ── PMTDP picker ──────────────────────────────────────────── */
.pmtdp-tag {
    display: inline-flex; align-items: center; gap: 5px;
    margin-top: 8px; padding: 4px 12px; border-radius: 20px;
    font-size: 11px; font-weight: 700; letter-spacing: .3px;
    background: #dcfce7; color: #14532d;
}
.pmtdp-priority-label {
    display: block; margin-top: 6px; font-size: 12px; color: var(--muted);
}

/* ── Non-PMTDP notice ──────────────────────────────────────── */
.non-pmtdp-notice {
    background: #fffbeb; border: 1px solid #fcd34d;
    border-left: 4px solid #f59e0b;
    border-radius: 8px; padding: 12px 16px;
    font-size: 13px; color: #92400e; margin-bottom: 4px;
}

/* Read-only name field in PMTDP mode */
.name-readonly { background: #f1f5f9 !important; color: var(--muted) !important; cursor: default !important; }
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
        <h2>Add New Intervention</h2>
        <p>Complete the form below to add an intervention to the selected Programme of Action.</p>
    </div>

    <%-- Validation summary --%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        ShowErrors="True"
        HeaderText="Please correct the following errors:"
        CssClass="val-summary-box" />

    <%-- ── Form panel ─────────────────────────────────────── --%>
    <asp:Panel ID="pnlForm" runat="server">

        <%-- SECTION 1: Context --%>
        <div class="form-card">
            <div class="card-title">Programme Context</div>

            <div class="field">
                <label for="<%= ddlCluster.ClientID %>">Cluster</label>
                <asp:DropDownList ID="ddlCluster" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" />
                <asp:RequiredFieldValidator ID="rfvCluster" runat="server"
                    ControlToValidate="ddlCluster" InitialValue="0"
                    ErrorMessage="Cluster is required."
                    Display="Dynamic" CssClass="val-msg" />
            </div>

            <div class="field">
                <label for="<%= ddlPOA.ClientID %>">Programme of Action (POA)</label>
                <asp:DropDownList ID="ddlPOA" runat="server" />
                <asp:RequiredFieldValidator ID="rfvPOA" runat="server"
                    ControlToValidate="ddlPOA" InitialValue="0"
                    ErrorMessage="POA is required."
                    Display="Dynamic" CssClass="val-msg" />
            </div>
        </div>

        <%-- SECTION 2: Intervention details --%>
        <div class="form-card">
            <div class="card-title">Intervention Details</div>

            <%-- Source toggle --%>
            <div class="field">
                <label>Source</label>
                <div class="source-toggle">
                    <label class="source-opt active" id="optPMTDP" onclick="setSource('pmtdp')">
                        <input type="radio" name="sourceMode" value="pmtdp" checked />
                        &#10003;&nbsp;From PMTDP Plan
                    </label>
                    <label class="source-opt" id="optOther" onclick="setSource('other')">
                        <input type="radio" name="sourceMode" value="other" />
                        + Non-PMTDP Activity
                    </label>
                </div>
            </div>

            <%-- PMTDP picker (visible in PMTDP mode) --%>
            <div class="field" id="pmtdpPickerField">
                <label>Select PMTDP Intervention <span style="color:var(--danger)">*</span></label>
                <select id="ddlPmtdpPicker" onchange="onPmtdpPick(this)">
                    <option value="">-- Select from approved PMTDP --</option>
                </select>
                <span id="pmtdpTag" class="pmtdp-tag" style="display:none">
                    &#10003; PMTDP Aligned
                </span>
                <span id="pmtdpPriorityLabel" class="pmtdp-priority-label"></span>
            </div>

            <%-- Non-PMTDP notice (visible in Other mode) --%>
            <div id="nonPmtdpNotice" class="non-pmtdp-notice" style="display:none">
                &#9888; This intervention is not in the PMTDP plan. It will be flagged as a non-PMTDP activity.
            </div>

            <%-- Hidden: tracks which mode was active at submit --%>
            <asp:HiddenField ID="hfIsPmtdp" runat="server" Value="1" />

            <%-- Intervention Name --%>
            <div class="field">
                <label for="<%= txtInterventionName.ClientID %>">
                    Intervention Name
                    <span id="nameHint" style="font-weight:400;color:var(--muted);font-size:12px;margin-left:4px">(auto-filled from selection above)</span>
                </label>
                <asp:TextBox ID="txtInterventionName" runat="server" placeholder="Select a PMTDP intervention above..." />
                <asp:RequiredFieldValidator ID="rfvInterventionName" runat="server"
                    ControlToValidate="txtInterventionName"
                    ErrorMessage="Intervention Name is required. Select a PMTDP intervention or switch to Non-PMTDP mode."
                    Display="Dynamic" CssClass="val-msg" />
            </div>

            <div class="field">
                <label for="<%= txtInterventionDescription.ClientID %>">
                    Description <span class="opt">(optional)</span>
                </label>
                <asp:TextBox ID="txtInterventionDescription" runat="server" TextMode="MultiLine" />
            </div>
        </div>

        <%-- SECTION 3: Classification --%>
        <div class="form-card">
            <div class="card-title">Classification</div>

            <div class="field">
                <label for="<%= ddlLeadInstitution.ClientID %>">Lead Institution</label>
                <asp:DropDownList ID="ddlLeadInstitution" runat="server" />
                <asp:RequiredFieldValidator ID="rfvLeadInstitution" runat="server"
                    ControlToValidate="ddlLeadInstitution" InitialValue="0"
                    ErrorMessage="Lead Institution is required."
                    Display="Dynamic" CssClass="val-msg" />
            </div>

            <div class="field">
                <label for="<%= ddlWorkingGroup.ClientID %>">
                    Working Group <span class="opt">(optional)</span>
                </label>
                <asp:DropDownList ID="ddlWorkingGroup" runat="server" />
            </div>

            <div class="field">
                <label for="<%= ddlSubOutcome.ClientID %>">
                    Sub-Outcome <span class="opt">(optional)</span>
                </label>
                <asp:DropDownList ID="ddlSubOutcome" runat="server" />
            </div>
        </div>

        <%-- SECTION 4: Period --%>
        <div class="form-card">
            <div class="card-title">Period</div>
            <div class="two-col">
                <div class="field">
                    <label for="<%= txtStartYear.ClientID %>">Start Year</label>
                    <asp:TextBox ID="txtStartYear" runat="server" placeholder="e.g. 2025" />
                    <asp:RequiredFieldValidator ID="rfvStartYear" runat="server"
                        ControlToValidate="txtStartYear"
                        ErrorMessage="Start Year is required."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:RangeValidator ID="rvStartYear" runat="server"
                        ControlToValidate="txtStartYear" Type="Integer"
                        MinimumValue="1900" MaximumValue="2100"
                        ErrorMessage="Start Year must be a valid year (e.g. 2025)."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
                <div class="field">
                    <label for="<%= txtEndYear.ClientID %>">End Year</label>
                    <asp:TextBox ID="txtEndYear" runat="server" placeholder="e.g. 2030" />
                    <asp:RequiredFieldValidator ID="rfvEndYear" runat="server"
                        ControlToValidate="txtEndYear"
                        ErrorMessage="End Year is required."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:RangeValidator ID="rvEndYear" runat="server"
                        ControlToValidate="txtEndYear" Type="Integer"
                        MinimumValue="1900" MaximumValue="2100"
                        ErrorMessage="End Year must be a valid year (e.g. 2030)."
                        Display="Dynamic" CssClass="val-msg" />
                    <asp:CompareValidator ID="cvEndYear" runat="server"
                        ControlToValidate="txtEndYear" ControlToCompare="txtStartYear"
                        Operator="GreaterThanEqual" Type="Integer"
                        ErrorMessage="End Year must be on or after Start Year."
                        Display="Dynamic" CssClass="val-msg" />
                </div>
            </div>
        </div>

        <%-- SECTION 5: Location --%>
        <div class="form-card">
            <div class="card-title">Location <span style="font-weight:400;text-transform:none;letter-spacing:0;">(optional)</span></div>

            <div class="field">
                <label for="<%= ddlMunicipality.ClientID %>">
                    Primary Municipality <span class="opt">(optional)</span>
                </label>
                <asp:DropDownList ID="ddlMunicipality" runat="server" />
            </div>

            <div class="field">
                <label for="<%= txtSpatialReference.ClientID %>">
                    Spatial Reference <span class="opt">(optional)</span>
                </label>
                <asp:TextBox ID="txtSpatialReference" runat="server"
                    placeholder="e.g. coordinates, district, address" />
            </div>
        </div>

        <%-- DB error message (shown when save fails) --%>
        <asp:Label ID="lblDbError" runat="server" Visible="false"
            CssClass="val-summary-box" />

        <%-- Action buttons --%>
        <div class="btn-row">
            <asp:Button ID="btnSubmit" runat="server" Text="Add Intervention"
                OnClick="btnSubmit_Click" CssClass="btn-primary" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                OnClick="btnCancel_Click" CausesValidation="false" CssClass="btn-secondary" />
        </div>

    </asp:Panel><%-- /pnlForm --%>

    <%-- ── Success panel (shown after save) ───────────────── --%>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
        <div class="success-card">
            <div class="tick">&#10003;</div>
            <h3>Intervention Added Successfully</h3>
            <p>The intervention has been saved. What would you like to do next?</p>
            <div class="action-links">
                <asp:HyperLink ID="hlAddIndicator" runat="server"
                    CssClass="action-links link-primary">
                    + Add Indicator
                </asp:HyperLink>
                <asp:HyperLink ID="hlAddAnother" runat="server"
                    CssClass="action-links link-outline">
                    Add Another Intervention
                </asp:HyperLink>
                <asp:HyperLink ID="hlBackToPOA" runat="server"
                    CssClass="action-links link-outline">
                    Back to POA
                </asp:HyperLink>
            </div>
        </div>
    </asp:Panel>

</div>

<script>
// pmtdpInterventions is injected by the server as a JSON array.
// Each item: { name, priority, institution, spatial }
var pmtdpInterventions = pmtdpInterventions || [];

function buildPmtdpPicker() {
    var sel = document.getElementById('ddlPmtdpPicker');
    if (!sel) return;
    while (sel.options.length > 1) sel.remove(1);

    if (!pmtdpInterventions || pmtdpInterventions.length === 0) {
        sel.options[0].text = '-- No approved PMTDP interventions for this cluster --';
        return;
    }
    sel.options[0].text = '-- Select approved PMTDP intervention (' + pmtdpInterventions.length + ' available) --';

    var lastPriority = '';
    for (var i = 0; i < pmtdpInterventions.length; i++) {
        var item = pmtdpInterventions[i];
        var opt = document.createElement('option');
        opt.value = i;
        var label = item.name;
        if (item.priority !== lastPriority) label = '[' + item.priority + ']  ' + label;
        opt.text = label;
        sel.appendChild(opt);
        lastPriority = item.priority;
    }
}

function onPmtdpPick(sel) {
    var nameBox    = document.getElementById('<%= txtInterventionName.ClientID %>');
    var spatialBox = document.getElementById('<%= txtSpatialReference.ClientID %>');
    var tag        = document.getElementById('pmtdpTag');
    var priLabel   = document.getElementById('pmtdpPriorityLabel');
    var hf         = document.getElementById('<%= hfIsPmtdp.ClientID %>');

    if (!sel.value) {
        nameBox.value = '';
        nameBox.readOnly = true;
        nameBox.className = nameBox.className.replace(' name-readonly', '') + ' name-readonly';
        tag.style.display = 'none';
        priLabel.textContent = '';
        hf.value = '1';
        return;
    }

    var item = pmtdpInterventions[parseInt(sel.value)];
    nameBox.value = item.name;
    nameBox.readOnly = true;
    if (nameBox.className.indexOf('name-readonly') < 0)
        nameBox.className += ' name-readonly';

    if (item.spatial && spatialBox && !spatialBox.value)
        spatialBox.value = item.spatial;

    tag.style.display = 'inline-flex';
    priLabel.textContent = 'Priority: ' + item.priority
        + (item.institution ? '  |  Lead: ' + item.institution : '');
    hf.value = '1';
}

function setSource(mode) {
    var optPMTDP       = document.getElementById('optPMTDP');
    var optOther       = document.getElementById('optOther');
    var pickerField    = document.getElementById('pmtdpPickerField');
    var notice         = document.getElementById('nonPmtdpNotice');
    var nameHint       = document.getElementById('nameHint');
    var nameBox        = document.getElementById('<%= txtInterventionName.ClientID %>');
    var tag            = document.getElementById('pmtdpTag');
    var priLabel       = document.getElementById('pmtdpPriorityLabel');
    var hf             = document.getElementById('<%= hfIsPmtdp.ClientID %>');

    if (mode === 'pmtdp') {
        optPMTDP.classList.add('active');    optOther.classList.remove('active');
        pickerField.style.display = 'block'; notice.style.display = 'none';
        nameHint.style.display = 'inline';
        // Re-apply readonly state from picker selection
        var sel = document.getElementById('ddlPmtdpPicker');
        if (!sel.value) {
            nameBox.value = '';
            nameBox.readOnly = true;
            if (nameBox.className.indexOf('name-readonly') < 0)
                nameBox.className += ' name-readonly';
        }
        hf.value = '1';
    } else {
        optOther.classList.add('active');    optPMTDP.classList.remove('active');
        pickerField.style.display = 'none'; notice.style.display = 'block';
        nameHint.style.display = 'none';
        nameBox.readOnly = false;
        nameBox.className = nameBox.className.replace(' name-readonly', '');
        nameBox.value = '';
        nameBox.placeholder = 'Enter intervention name...';
        tag.style.display = 'none';
        priLabel.textContent = '';
        document.getElementById('ddlPmtdpPicker').value = '';
        hf.value = '0';
    }
}

document.addEventListener('DOMContentLoaded', function () {
    buildPmtdpPicker();
    // Start in PMTDP mode with name field locked until a selection is made
    var nameBox = document.getElementById('<%= txtInterventionName.ClientID %>');
    nameBox.readOnly = true;
    if (nameBox.className.indexOf('name-readonly') < 0)
        nameBox.className += ' name-readonly';
});
</script>

</asp:Content>
