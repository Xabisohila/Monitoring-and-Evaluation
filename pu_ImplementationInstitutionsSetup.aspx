<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pu_ImplementationInstitutionsSetup.aspx.cs" Inherits="pu_ImplementationInstitutionsSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    body{ font-family: Segoe UI, Arial, sans-serif; margin:20px; }
    h2,h3{ margin: 8px 0; }
    .msg { color:#0B6A0B; margin-left:10px; }
    .err { color:#B00020; margin-left:10px; }
    .btn { display:inline-block; padding:6px 12px; border:1px solid #D0D0D0; border-radius:4px; background:#F9FAFB; color:#1E1E1E; text-decoration:none; cursor:pointer; }
    .btn:hover { background:#F0F2F5; }
    .btn-primary { background:#1F4B99; border-color:#1F4B99; color:#fff; }
    .btn-primary:hover { background:#1B3F82; }
    .btn-danger { background:#C62828; border-color:#B71C1C; color:#fff; }
    .btn-danger:hover { background:#B71C1C; }
    .form-row{ margin:10px 0; }
    .formal-grid { width:100%; border-collapse:separate; border-spacing:0; font-family:"Segoe UI",Arial,sans-serif; font-size:14px; color:#1E1E1E; border:1px solid #D0D0D0; border-radius:6px; overflow:hidden; background:#fff; }
    .formal-grid-header th { background:#F3F3F3; color:#111; font-weight:600; padding:10px 14px; border-bottom:1px solid #D0D0D0; text-align:left; white-space:nowrap; }
    .formal-grid-row td { padding:10px 14px; border-bottom:1px solid #E7E7E7; background:#fff; }
    .formal-grid-alt td { background:#FAFAFA; padding:10px 14px; border-bottom:1px solid #E7E7E7; }
    .formal-grid tr:hover td { background:#F0F6FF; }
    .formal-grid-selected td { background:#D9E8FF !important; color:#003060 !important; }
    .formal-grid input[type="text"], .formal-grid select, .formal-grid textarea { width:95%; font-family:"Segoe UI",Arial; font-size:14px; padding:6px; border:1px solid #C8C8C8; border-radius:4px; outline:none; }
    .formal-grid input[type="text"]:focus, .formal-grid select:focus, .formal-grid textarea:focus { border-color:#4A90E2; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br /><br /><br />

    <div class="container">
        <div class="section-header">
            <div class="row section-title text-center">
                <br />
                <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Implementation Institutions Setup</strong></span></h2>
                </div>
            </div>
        </div>
        <br />
        <div style="margin: 10px 0;">
            <asp:Label ID="lblMsg" runat="server" CssClass="msg" />
            <asp:Label ID="lblErr" runat="server" CssClass="err" />
        </div>
        <br />
        <div class="grid">
            <div style="margin: 8px 0;">
                <asp:Button ID="btnShowAdd" runat="server" Text="➕ Add New Institution" CssClass="btn btn-primary" OnClick="btnShowAdd_Click" />
            </div>
            <br />
            <h3>All Institutions</h3>
            <asp:GridView ID="gvInstitutions" runat="server" AutoGenerateColumns="False"
                DataKeyNames="InstitutionID"
                OnRowEditing="gvInstitutions_RowEditing"
                OnRowCancelingEdit="gvInstitutions_RowCancelingEdit"
                OnRowUpdating="gvInstitutions_RowUpdating"
                OnRowDeleting="gvInstitutions_RowDeleting"
                OnRowDataBound="gvInstitutions_RowDataBound"
                OnRowCommand="gvInstitutions_RowCommand"
                CssClass="formal-grid"
                HeaderStyle-CssClass="formal-grid-header"
                RowStyle-CssClass="formal-grid-row"
                AlternatingRowStyle-CssClass="formal-grid-alt"
                SelectedRowStyle-CssClass="formal-grid-selected"
                GridLines="None"
                ShowFooter="True">

                <Columns>
                    <asp:BoundField DataField="InstitutionID" HeaderText="ID" ReadOnly="True" Visible="false" />

                    <asp:TemplateField HeaderText="Institution Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("InstitutionName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtInstitutionNameEdit" runat="server" Text='<%# Bind("InstitutionName") %>' MaxLength="200" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInstitutionNameAdd" runat="server" MaxLength="200" placeholder="Institution Name" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Institution Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("InstitutionType") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <!-- Retained as text for edit to avoid breaking existing UX -->
                            <asp:TextBox ID="txtInstitutionTypeEdit" runat="server" Text='<%# Bind("InstitutionType") %>' MaxLength="100" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlInstitutionTypeAdd" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cluster">
                        <ItemTemplate>
                            <asp:Label ID="lblCluster" runat="server" Text='<%# Eval("ClusterName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlClusterEdit" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlClusterAdd" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="210px">
                        
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server"
                                CssClass="btn" Text="✏️ Edit"
                                CommandName="Edit"
                                CausesValidation="false" UseSubmitBehavior="false" />
                            &nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server"
                                CssClass="btn btn-danger" Text="🗑️ Delete"
                                CommandName="Delete"
                                OnClientClick="return confirm('Delete this institution?');"
                                CausesValidation="false" UseSubmitBehavior="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server"
                                Text="Save" CssClass="btn btn-primary"
                                CommandName="Update" />
                            <asp:Button ID="btnCancel" runat="server"
                                Text="Cancel" CssClass="btn"
                                CommandName="Cancel" CausesValidation="false" UseSubmitBehavior="false" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-primary" Text="Add" CommandName="AddNew" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br /><br />
        </div>
    </div>
</asp:Content>