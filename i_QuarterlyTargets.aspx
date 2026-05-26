<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_QuarterlyTargets.aspx.cs" Inherits="i_QuarterlyTargets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
.field { margin-bottom: 18px; }
.field label {
    display: block; font-size: 13px; font-weight: 600; color: var(--text);
    margin-bottom: 6px;
}
.field label .opt { font-weight: 400; color: var(--muted); font-size: 12px; margin-left: 4px; }
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
.field select {
    appearance: none; cursor: pointer;
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='8' viewBox='0 0 12 8'%3E%3Cpath fill='%2364748b' d='M6 8L0 0h12z'/%3E%3C/svg%3E");
    background-repeat: no-repeat; background-position: right 12px center;
    padding-right: 32px;
}

/* Two-column grid */
.two-col { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
@media (max-width: 600px) { .two-col { grid-template-columns: 1fr; } }

/* ── Buttons ───────────────────────────────────────────────── */
.btn-row { display: flex; gap: 10px; margin-top: 8px; flex-wrap: wrap; align-items: center; }
.btn-primary {
    padding: 10px 28px; font-size: 14px; font-weight: 600;
    border-radius: 8px; border: none; cursor: pointer;
    background: var(--green); color: #fff; transition: background .15s;
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

/* ── Status messages ───────────────────────────────────────── */
.msg-success {
    display: block; margin-top: 12px; padding: 10px 14px;
    border-radius: 8px; background: #d1fae5; border: 1px solid #a7f3d0;
    color: #065f46; font-size: 13px; font-weight: 600;
}
.msg-error {
    display: block; margin-top: 12px; padding: 10px 14px;
    border-radius: 8px; background: #fff1f2; border: 1px solid #fecdd3;
    color: #be123c; font-size: 13px; font-weight: 600;
}

/* ── Section heading ───────────────────────────────────────── */
.section-heading { margin: 8px 0 16px; }
.section-heading h3 {
    margin: 0; font-size: 18px; font-weight: 700; color: var(--text);
    border-left: 4px solid var(--green); padding-left: 12px;
}

/* ── Data table card ───────────────────────────────────────── */
.tbl-card {
    background: var(--surface); border: 1px solid var(--border);
    border-radius: var(--radius); box-shadow: var(--shadow);
    overflow: hidden; margin-bottom: 24px;
}
.tbl-wrap { overflow-x: auto; }
.data-tbl { width: 100%; border-collapse: collapse; font-size: 13px; }
.data-tbl th {
    padding: 10px 14px; font-size: 11px; font-weight: 700;
    text-transform: uppercase; letter-spacing: .4px;
    color: #fff; background: #334155;
    border-right: 1px solid rgba(255,255,255,.1);
}
.data-tbl th:last-child { border-right: none; }
.data-tbl td {
    padding: 9px 14px; border-bottom: 1px solid #edf2f7;
    border-right: 1px solid #f1f5f9; color: #334155;
    vertical-align: middle;
}
.data-tbl tr:last-child td { border-bottom: none; }
.data-tbl tr:nth-child(even) td { background: #f8fafc; }
.data-tbl tr:hover td { background: #f0f7ff !important; }
.data-tbl .act-edit   { color: var(--primary); font-size: 12px; font-weight: 600; text-decoration: none; }
.data-tbl .act-delete { color: var(--danger);  font-size: 12px; font-weight: 600; text-decoration: none; }
.data-tbl .act-edit:hover   { text-decoration: underline; }
.data-tbl .act-delete:hover { text-decoration: underline; }
.q-badge {
    display: inline-block; padding: 2px 10px; border-radius: 20px;
    background: #e0f2fe; color: #0369a1; font-size: 12px; font-weight: 700;
}

/* ── Empty notice ──────────────────────────────────────────── */
.empty-notice {
    padding: 24px; text-align: center;
    color: var(--muted); font-size: 14px; font-style: italic;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div class="container" style="padding-bottom: 60px; max-width: 900px;">

    <%-- Page header --%>
    <div class="ai-header">
        <h2>Quarterly Targets</h2>
        <p>Set and manage quarterly targets per indicator and financial year.</p>
    </div>

    <%-- SECTION 1: Filter – Indicator & Financial Year --%>
    <div class="form-card">
        <div class="card-title">Indicator &amp; Financial Year</div>
        <div class="two-col">
            <div class="field">
                <label for="<%= ddlIndicator.ClientID %>">Indicator</label>
                <asp:DropDownList ID="ddlIndicator" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlIndicator_SelectedIndexChanged" />
            </div>
            <div class="field">
                <label for="<%= ddlFY.ClientID %>">Financial Year</label>
                <asp:DropDownList ID="ddlFY" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFY_SelectedIndexChanged" />
            </div>
        </div>
    </div>

    <%-- SECTION 2: Add / Edit form --%>
    <div class="form-card">
        <div class="card-title">
            <asp:Label ID="lblFormTitle" runat="server" Text="Add Quarterly Target" />
        </div>

        <div class="two-col">
            <div class="field">
                <label for="<%= ddlQuarter.ClientID %>">Quarter</label>
                <asp:DropDownList ID="ddlQuarter" runat="server">
                    <asp:ListItem Text="Quarter 1" Value="1" />
                    <asp:ListItem Text="Quarter 2" Value="2" />
                    <asp:ListItem Text="Quarter 3" Value="3" />
                    <asp:ListItem Text="Quarter 4" Value="4" />
                </asp:DropDownList>
            </div>
            <div class="field">
                <label for="<%= txtTarget.ClientID %>">Target Value</label>
                <asp:TextBox ID="txtTarget" runat="server" placeholder="e.g. 250" />
            </div>
        </div>

        <div class="field">
            <label for="<%= txtSpatial.ClientID %>">
                Spatial Reference <span class="opt">(optional)</span>
            </label>
            <asp:TextBox ID="txtSpatial" runat="server"
                placeholder="e.g. district, municipality, coordinates" />
        </div>

        <asp:HiddenField ID="hfAnnualTargetID"   runat="server" />
        <asp:HiddenField ID="hfQuarterlyTargetID" runat="server" />

        <asp:Label ID="lblStatus" runat="server" />

        <div class="btn-row">
            <asp:Button ID="btnSave" runat="server" Text="Save / Update"
                OnClick="btnSave_Click" CssClass="btn-primary"
                OnClientClick="return showConfirm('<%= btnSave.UniqueID %>');" />
            <asp:Button ID="btnClear" runat="server" Text="Clear"
                OnClick="btnClear_Click" CausesValidation="false" CssClass="btn-secondary" />
        </div>
    </div>

    <%-- Grid section --%>
    <div class="section-heading">
        <h3>Saved Targets</h3>
    </div>

    <div class="tbl-card">
        <div class="tbl-wrap">
            <asp:GridView ID="gvQuarterly" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="QuarterlyTargetID"
                OnRowCommand="gvQuarterly_RowCommand"
                CssClass="data-tbl"
                GridLines="None"
                EmptyDataText="No targets recorded for the selected indicator and financial year.">
                <EmptyDataRowStyle CssClass="empty-notice" />
                <Columns>
                    <asp:TemplateField HeaderText="Quarter">
                        <ItemTemplate>
                            <span class="q-badge">Q<%# Eval("QuarterNumber") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TargetValue"      HeaderText="Target Value" />
                    <asp:BoundField DataField="SpatialReference" HeaderText="Spatial Ref" />
                    <asp:ButtonField CommandName="EditRow"   Text="Edit"   ButtonType="Link" ControlStyle-CssClass="act-edit" />
                    <asp:ButtonField CommandName="DeleteRow" Text="Delete" ButtonType="Link" ControlStyle-CssClass="act-delete" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>

<%-- Confirmation modal --%>
<div id="confirmModal" style="display:none;position:fixed;inset:0;background:rgba(0,0,0,.5);z-index:9999;align-items:center;justify-content:center;">
    <div style="background:#fff;border-radius:12px;padding:32px 28px;max-width:380px;width:90%;box-shadow:0 20px 60px rgba(0,0,0,.25);text-align:center;font-family:'Segoe UI',Roboto,Arial,sans-serif;">
        <div style="width:52px;height:52px;background:#fef9c3;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 16px;font-size:26px;color:#d97706;">&#9998;</div>
        <h3 style="margin:0 0 8px;font-size:18px;font-weight:700;color:#1a2b4a;">Confirm Save</h3>
        <p style="margin:0 0 24px;font-size:13px;color:#64748b;">Are you sure you want to save / update this target?</p>
        <div style="display:flex;gap:10px;justify-content:center;">
            <button type="button" onclick="cancelConfirm()" style="padding:9px 22px;border-radius:8px;border:1.5px solid #dce3ec;background:#fff;cursor:pointer;font-size:13px;font-weight:600;color:#1a2b4a;">Cancel</button>
            <button type="button" onclick="doConfirm()" style="padding:9px 22px;border-radius:8px;border:none;background:#1d6f42;color:#fff;cursor:pointer;font-size:13px;font-weight:600;">Yes, Save</button>
        </div>
    </div>
</div>
<script type="text/javascript">
    var _uniqueId = '';
    function showConfirm(uniqueId) {
        _uniqueId = uniqueId;
        document.getElementById('confirmModal').style.display = 'flex';
        return false;
    }
    function doConfirm() {
        document.getElementById('confirmModal').style.display = 'none';
        __doPostBack(_uniqueId, '');
    }
    function cancelConfirm() {
        document.getElementById('confirmModal').style.display = 'none';
    }
</script>
</asp:Content>
