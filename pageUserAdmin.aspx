<%@ Page Title="User Management" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="pageUserAdmin.aspx.cs" Inherits="pageUserAdmin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<style>
    .ua-header     { display:flex; align-items:center; justify-content:space-between; margin-bottom:18px; }
    .ua-header h3  { margin:0; color:#0C2D48; font-weight:700; }
    .ua-filter     { display:flex; gap:10px; margin-bottom:16px; }
    .ua-filter input, .ua-filter select { max-width:260px; }
    .ua-table th   { background:#0C2D48; color:#fff; border-color:#0C2D48 !important; font-weight:600; font-size:13px; }
    .ua-table td   { vertical-align:middle; font-size:13px; }
    .role-badge    { display:inline-block; padding:2px 8px; border-radius:10px; font-size:11px; font-weight:600; letter-spacing:.3px; }
    .role-admin    { background:#c0392b; color:#fff; }
    .role-planning { background:#1a5276; color:#fff; }
    .role-dept     { background:#117a65; color:#fff; }
    .role-wg       { background:#7d6608; color:#fff; }
    .role-hod      { background:#6c3483; color:#fff; }
    .role-otp      { background:#1a5276; color:#fff; }
    .role-viewer   { background:#616a6b; color:#fff; }
    .role-other    { background:#717d7e; color:#fff; }
    .status-active   { background:#1e8449; color:#fff; padding:2px 8px; border-radius:10px; font-size:11px; font-weight:600; }
    .status-inactive { background:#922b21; color:#fff; padding:2px 8px; border-radius:10px; font-size:11px; font-weight:600; }
    .modal-header-dark { background:#0C2D48; color:#fff; border-radius:5px 5px 0 0; }
    .modal-header-dark .close { color:#fff; opacity:.8; }
    .msg-success { background:#d5f5e3; color:#1e8449; border:1px solid #a9dfbf; padding:10px 16px; border-radius:4px; margin-bottom:16px; }
    .msg-error   { background:#fadbd8; color:#922b21; border:1px solid #f1948a; padding:10px 16px; border-radius:4px; margin-bottom:16px; }
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" style="padding-top:80px; padding-bottom:40px;">

    <div class="ua-header">
        <h3><i class="glyphicon glyphicon-users"></i> User Management</h3>
        <a href="register.aspx" class="btn btn-success">
            <i class="glyphicon glyphicon-plus"></i> Add New User
        </a>
    </div>

    <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
        <div class="msg-success"><i class="glyphicon glyphicon-ok-circle"></i> <asp:Literal ID="litSuccessMsg" runat="server" /></div>
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <div class="msg-error"><i class="glyphicon glyphicon-exclamation-sign"></i> <asp:Literal ID="litErrorMsg" runat="server" /></div>
    </asp:Panel>

    <%-- Filters --%>
    <div class="ua-filter">
        <input type="text" id="txtSearch" class="form-control" placeholder="Search name or Persal number" onkeyup="filterTable()" />
        <select id="selRole" class="form-control" onchange="filterTable()">
            <option value="">All Roles</option>
        </select>
        <select id="selStatus" class="form-control" onchange="filterTable()" style="max-width:140px;">
            <option value="">All Statuses</option>
            <option value="active">Active</option>
            <option value="inactive">Inactive</option>
        </select>
    </div>

    <%-- User table --%>
    <div class="table-responsive">
        <table class="table table-bordered table-hover ua-table" id="tblUsers">
            <thead>
                <tr>
                    <th>Persal</th>
                    <th>Full Name</th>
                    <th>Role</th>
                    <th>Department</th>
                    <th>Status</th>
                    <th style="width:70px;text-align:center;">Edit</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptUsers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("PersalNumber") %></td>
                            <td><%# Server.HtmlEncode(Eval("FullName").ToString()) %></td>
                            <td><span class='role-badge <%# GetRoleCss(Eval("UserType")) %>'><%# Server.HtmlEncode(Eval("UserTypeName").ToString()) %></span></td>
                            <td><%# Server.HtmlEncode(Eval("DepartmentName").ToString()) %></td>
                            <td>
                                <span class='<%# Convert.ToInt32(Eval("Activation")) == 20 ? "status-active" : "status-inactive" %>'>
                                    <%# Convert.ToInt32(Eval("Activation")) == 20 ? "Active" : "Inactive" %>
                                </span>
                            </td>
                            <td style="text-align:center;">
                                <button type="button" class="btn btn-xs btn-default"
                                    data-userid="<%# Eval("apl_user_id") %>"
                                    data-usertype="<%# Eval("UserType") %>"
                                    data-activation="<%# Eval("Activation") %>"
                                    data-name="<%# Server.HtmlEncode(Eval("FullName").ToString()) %>"
                                    onclick="openEditModal(this)">
                                    <i class="glyphicon glyphicon-pencil"></i>
                                </button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <p class="text-muted small" id="lblCount"></p>

    <%-- Hidden fields for edit postback --%>
    <asp:HiddenField ID="hfEditUserId"     runat="server" />
    <asp:HiddenField ID="hfEditUserType"   runat="server" />
    <asp:HiddenField ID="hfEditActivation" runat="server" />

    <%-- Edit modal --%>
    <div class="modal fade" id="modalEditUser" tabindex="-1" role="dialog" aria-labelledby="editModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header modal-header-dark">
                    <button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>
                    <h4 class="modal-title" id="editModalLabel">Edit User: <span id="spanEditName"></span></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Role</label>
                        <asp:DropDownList ID="ddlEditUserType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Account Status</label>
                        <asp:DropDownList ID="ddlEditActivation" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Active"   Value="20" />
                            <asp:ListItem Text="Inactive" Value="0"  />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btnSaveEdit" runat="server" Text="Save Changes"
                        CssClass="btn btn-primary" OnClick="btnSaveEdit_Click" />
                </div>
            </div>
        </div>
    </div>

</div>

<script>
    function openEditModal(btn) {
        var uid   = btn.getAttribute('data-userid');
        var utype = btn.getAttribute('data-usertype');
        var uact  = btn.getAttribute('data-activation');
        var name  = btn.getAttribute('data-name');

        document.getElementById('<%= hfEditUserId.ClientID %>').value     = uid;
        document.getElementById('<%= hfEditUserType.ClientID %>').value   = utype;
        document.getElementById('<%= hfEditActivation.ClientID %>').value = uact;
        document.getElementById('spanEditName').textContent = name;

        var ddlType = document.getElementById('<%= ddlEditUserType.ClientID %>');
        for (var i = 0; i < ddlType.options.length; i++) {
            ddlType.options[i].selected = (ddlType.options[i].value === utype);
        }
        var ddlAct = document.getElementById('<%= ddlEditActivation.ClientID %>');
        for (var i = 0; i < ddlAct.options.length; i++) {
            ddlAct.options[i].selected = (ddlAct.options[i].value === uact);
        }

        $('#modalEditUser').modal('show');
    }

    function filterTable() {
        var q      = document.getElementById('txtSearch').value.toLowerCase();
        var role   = document.getElementById('selRole').value.toLowerCase();
        var status = document.getElementById('selStatus').value.toLowerCase();
        var rows   = document.querySelectorAll('#tblUsers tbody tr');
        var visible = 0;
        rows.forEach(function(row) {
            var persal = row.cells[0].textContent.toLowerCase();
            var name   = row.cells[1].textContent.toLowerCase();
            var rowRole   = row.cells[2].textContent.trim().toLowerCase();
            var rowStatus = row.cells[4].textContent.trim().toLowerCase();
            var ok = (!q    || name.indexOf(q) > -1 || persal.indexOf(q) > -1)
                  && (!role   || rowRole === role)
                  && (!status || rowStatus === status);
            row.style.display = ok ? '' : 'none';
            if (ok) visible++;
        });
        document.getElementById('lblCount').textContent = visible + ' user(s) shown';
    }

    window.addEventListener('DOMContentLoaded', function () {
        // Populate role filter from table data
        var roles = {};
        document.querySelectorAll('#tblUsers tbody tr td:nth-child(3)').forEach(function(cell) {
            var r = cell.textContent.trim();
            if (r) roles[r] = true;
        });
        var sel = document.getElementById('selRole');
        Object.keys(roles).sort().forEach(function(r) {
            var opt = document.createElement('option');
            opt.value = r.toLowerCase(); opt.textContent = r;
            sel.appendChild(opt);
        });

        // Initial count
        var total = document.querySelectorAll('#tblUsers tbody tr').length;
        document.getElementById('lblCount').textContent = total + ' user(s)';
    });
</script>
</asp:Content>
