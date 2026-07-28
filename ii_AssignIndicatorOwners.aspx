<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="ii_AssignIndicatorOwners.aspx.cs" Inherits="ii_AssignIndicatorOwners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
:root {
    --bg:#f7f8fb; --surface:#fff; --border:#dce3ec; --text:#1a2b4a; --muted:#64748b;
    --primary:#0b5ed7; --primary-dark:#094db0; --danger:#dc3545;
    --radius:12px; --shadow:0 2px 12px rgba(0,0,0,.07);
    --font:"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif;
}
body { background:var(--bg); font-family:var(--font); color:var(--text); }
.page-top { padding-top:90px; padding-bottom:60px; }
.page-header { margin-bottom:24px; }
.page-header h2 { margin:0 0 4px; font-size:24px; font-weight:800; color:var(--text); }
.page-header p  { margin:0; font-size:13px; color:var(--muted); }

.assign-grid { display:grid; grid-template-columns:1fr 1fr; gap:20px; margin-bottom:20px; }
@media(max-width:700px){ .assign-grid { grid-template-columns:1fr; } }

.pick-card {
    background:var(--surface); border:1px solid var(--border);
    border-radius:var(--radius); box-shadow:var(--shadow); overflow:hidden;
}
.pick-card-header {
    padding:12px 18px; background:#f8fafc; border-bottom:1px solid var(--border);
    display:flex; align-items:center; justify-content:space-between;
}
.pick-card-header h4 { margin:0; font-size:13px; font-weight:700; color:var(--text); }
.select-all-row {
    padding:8px 18px; border-bottom:1px solid #edf2f7;
    display:flex; align-items:center; gap:8px;
    font-size:12px; font-weight:600; color:var(--muted); background:#fafbfc;
}
.select-all-row input[type=checkbox] { margin:0; }
.cbl-wrap {
    max-height:280px; overflow-y:auto; padding:10px 18px;
}
.cbl-wrap table { border:none; box-shadow:none; background:transparent; }
.cbl-wrap table td { border:none; padding:4px 0; font-size:13px; }
.cbl-wrap input[type=checkbox] { margin-right:6px; }

.btn-assign {
    padding:11px 28px; font-size:14px; font-weight:700;
    border-radius:8px; border:none; background:var(--primary);
    color:#fff; cursor:pointer; transition:background .15s;
}
.btn-assign:hover { background:var(--primary-dark); }

.msg-bar { display:none; margin-bottom:16px; padding:11px 16px; border-radius:8px; font-size:13px; }
.msg-bar.visible { display:block; }
.msg-success { background:#d1fae5; border:1px solid #6ee7b7; color:#065f46; }
.msg-error   { background:#fee2e2; border:1px solid #fca5a5; color:#991b1b; }

.owners-card {
    background:var(--surface); border:1px solid var(--border);
    border-radius:var(--radius); box-shadow:var(--shadow); overflow:hidden; margin-top:8px;
}
.owners-card-header {
    padding:13px 20px; background:#f8fafc; border-bottom:1px solid var(--border);
    font-size:14px; font-weight:700; color:var(--text);
}
.owners-tbl { width:100%; border-collapse:collapse; font-size:13px; }
.owners-tbl th {
    padding:10px 14px; background:#f1f5f9; font-weight:600;
    color:#475569; text-align:left; border-bottom:2px solid var(--border);
}
.owners-tbl td { padding:10px 14px; border-bottom:1px solid #edf2f7; vertical-align:middle; }
.owners-tbl tr:last-child td { border-bottom:none; }
.owners-tbl tr:hover td { background:#f8fafc; }
.btn-remove {
    font-size:12px; font-weight:600; color:var(--danger);
    background:none; border:none; cursor:pointer; padding:0; margin:0;
    text-decoration:underline;
}
.badge-count {
    background:var(--primary); color:#fff; font-size:11px; font-weight:700;
    padding:2px 10px; border-radius:20px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container page-top">

    <div class="page-header">
        <h2>Assign Indicator Owners</h2>
        <p>Select one or more indicators and one or more leading departments, then click Assign.</p>
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="msg-bar" />

    <div class="assign-grid">

        <%-- Indicators column --%>
        <div class="pick-card">
            <div class="pick-card-header">
                <h4>Indicators</h4>
                <span id="indCount" class="badge-count">0 selected</span>
            </div>
            <div class="select-all-row">
                <input type="checkbox" id="chkAllInd" onclick="selectAll('cblIndicators', this)" />
                <label for="chkAllInd" style="margin:0; cursor:pointer;">Select All</label>
            </div>
            <div class="cbl-wrap">
                <asp:CheckBoxList ID="cblIndicators" runat="server"
                    DataTextField="IndicatorName" DataValueField="IndicatorID"
                    onclick="updateCount('cblIndicators','indCount')" />
            </div>
        </div>

        <%-- Departments column --%>
        <div class="pick-card">
            <div class="pick-card-header">
                <h4>Leading Departments</h4>
                <span id="deptCount" class="badge-count">0 selected</span>
            </div>
            <div class="select-all-row">
                <input type="checkbox" id="chkAllDept" onclick="selectAll('cblInstitution', this)" />
                <label for="chkAllDept" style="margin:0; cursor:pointer;">Select All</label>
            </div>
            <div class="cbl-wrap">
                <asp:CheckBoxList ID="cblInstitution" runat="server"
                    DataTextField="InstitutionName" DataValueField="InstitutionID"
                    onclick="updateCount('cblInstitution','deptCount')" />
            </div>
        </div>

    </div>

    <asp:Button runat="server" ID="btnAssign" Text="Assign Selected"
        CssClass="btn-assign" OnClick="btnAssign_Click" />

    <%-- Current assignments table --%>
    <div class="owners-card" style="margin-top:28px;">
        <div class="owners-card-header">Current Assignments
            <asp:Label ID="lblOwnerCount" runat="server" CssClass="badge-count" style="margin-left:10px;" />
        </div>
        <asp:GridView
            runat="server"
            ID="gvOwners"
            AutoGenerateColumns="false"
            DataKeyNames="OwnerID"
            OnRowCommand="gvOwners_RowCommand"
            OnRowDataBound="gvOwners_RowDataBound"
            CssClass="owners-tbl"
            GridLines="None"
            EmptyDataText="No owners assigned yet."
            EmptyDataRowStyle-CssClass="owners-tbl"
            UseAccessibleHeader="true">
            <Columns>
                <asp:BoundField DataField="OwnerID"       Visible="false" />
                <asp:BoundField DataField="InstitutionID" Visible="false" />
                <asp:BoundField DataField="IndicatorID"   Visible="false" />
                <asp:TemplateField HeaderText="Indicator">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblIndicatorName"
                            Text='<%# GetIndicatorName(Eval("IndicatorID")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Leading Department">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblInstitutionName"
                            Text='<%# GetInstitutionName(Eval("InstitutionID")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="RemoveOwner"
                            CommandArgument='<%# Eval("OwnerID") %>'
                            Text="Remove" CssClass="btn-remove"
                            OnClientClick="return confirm('Remove this assignment?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</div>

<script>
function selectAll(listId, chk) {
    var wrap = document.getElementById('<%= cblIndicators.ClientID %>');
    // resolve actual rendered ID for either list
    var el = document.getElementById(listId) ||
             document.querySelector('[id$="' + listId + '"]') ||
             document.getElementById('<%= cblIndicators.ClientID %>');
    // find the matching list by server ID suffix
    var allLists = document.querySelectorAll('table[id$="' + listId + '"] input[type=checkbox], ' +
                   '[id*="' + listId + '"] input[type=checkbox]');
    allLists.forEach(function(cb) { cb.checked = chk.checked; });
    // update counter badge
    var badge = listId === '<%= cblIndicators.ClientID %>' || listId === 'cblIndicators'
        ? 'indCount' : 'deptCount';
    updateCount(listId, badge);
}

function updateCount(listId, badgeId) {
    var checks = document.querySelectorAll('[id*="' + listId + '"] input[type=checkbox]');
    var n = 0;
    checks.forEach(function(cb) { if (cb.checked) n++; });
    var badge = document.getElementById(badgeId);
    if (badge) badge.textContent = n + ' selected';
}

// wire up live counts on load
window.addEventListener('DOMContentLoaded', function() {
    updateCount('<%= cblIndicators.ClientID %>', 'indCount');
    updateCount('<%= cblInstitution.ClientID %>', 'deptCount');

    // attach onchange to each checkbox for live count
    document.querySelectorAll('[id*="<%= cblIndicators.ClientID %>"] input[type=checkbox]')
        .forEach(function(cb){ cb.addEventListener('change', function(){ updateCount('<%= cblIndicators.ClientID %>','indCount'); }); });
    document.querySelectorAll('[id*="<%= cblInstitution.ClientID %>"] input[type=checkbox]')
        .forEach(function(cb){ cb.addEventListener('change', function(){ updateCount('<%= cblInstitution.ClientID %>','deptCount'); }); });
});
</script>
</asp:Content>

