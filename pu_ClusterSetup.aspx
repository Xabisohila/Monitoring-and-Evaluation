<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pu_ClusterSetup.aspx.cs" Inherits="pu_ClusterSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%-- PSEUDOCODE:
        - Align GridView look and feel with pu_ImplementationInstitutionsSetup.aspx
        - Add shared button styles: .btn, .btn-primary, .btn-danger
        - Ensure .formal-grid, header, row, alt-row, hover match
        - Replace CommandField with an Actions TemplateField using styled LinkButtons (Edit/Delete)
        - Provide EditItemTemplate with styled Update/Cancel buttons
        - Keep existing RowEditing, RowUpdating, RowDeleting, RowCancelingEdit handlers
        - Do not change data bindings; keep Name/Description templates as-is
    --%>
    <style type="text/css">
        body { font-family: Segoe UI, Arial, sans-serif; margin: 20px; }
        h2, h3 { margin: 8px 0; }

        .msg { color: #0B6A0B; margin-left: 10px; }
        .err { color: #B00020; margin-left: 10px; }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            border: 1px solid #D0D0D0;
            border-radius: 4px;
            background: #F9FAFB;
            color: #1E1E1E;
            text-decoration: none;
            cursor: pointer;
        }
        .btn:hover { background: #F0F2F5; }
        .btn-primary { background: #1F4B99; border-color: #1F4B99; color: #fff; }
        .btn-primary:hover { background: #1B3F82; }
        .btn-danger { background: #C62828; border-color: #B71C1C; color: #fff; }
        .btn-danger:hover { background: #B71C1C; }

        .form-row { margin: 10px 0; }
        .label { display: inline-block; width: 160px; }
        .grid { margin-top: 20px; }

        /* Main table */
        .formal-grid {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            font-family: "Segoe UI", Arial, sans-serif;
            font-size: 14px;
            color: #1E1E1E;
            border: 1px solid #D0D0D0;
            border-radius: 6px;
            overflow: hidden;
            background: #ffffff;
        }

        /* Header */
        .formal-grid-header th {
            background: #F3F3F3;
            color: #111;
            font-weight: 600;
            padding: 10px 14px;
            border-bottom: 1px solid #D0D0D0;
            text-align: left;
            white-space: nowrap;
        }

        /* Regular rows */
        .formal-grid-row td {
            padding: 10px 14px;
            border-bottom: 1px solid #E7E7E7;
            background: #ffffff;
        }

        /* Alternating rows */
        .formal-grid-alt td {
            background: #FAFAFA;
            padding: 10px 14px;
            border-bottom: 1px solid #E7E7E7;
        }

        /* Hover effect */
        .formal-grid tr:hover td {
            background: #F0F6FF;
        }

        /* Selected row */
        .formal-grid-selected td {
            background: #D9E8FF !important;
            color: #003060 !important;
        }

        /* Edit mode: style input fields */
        .formal-grid input[type="text"],
        .formal-grid textarea {
            width: 95%;
            font-family: "Segoe UI", Arial;
            font-size: 14px;
            padding: 6px;
            border: 1px solid #C8C8C8;
            border-radius: 4px;
            outline: none;
        }

        .formal-grid input[type="text"]:focus,
        .formal-grid textarea:focus {
            border-color: #4A90E2;
        }

        /* Command buttons inside grid */
        .formal-grid a,
        .formal-grid input[type="submit"] {
            color: #1A4E8A;
            font-weight: 600;
            text-decoration: none;
            margin-right: 6px;
        }

        .formal-grid a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />

    <div class="container">
        <div class="section-header">
            <div class="row section-title text-center">
                <br />
                <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Cluster Setup </strong></span></h2>
                </div>
            </div>
        </div>

        <asp:Label ID="lblErr" runat="server" CssClass="err" />
        <asp:Panel runat="server" CssClass="card">
            <div class="form-row">
                <asp:TextBox ID="txtName" runat="server" MaxLength="100" placeholder="Cluster Name" />
                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                    ControlToValidate="txtName" ErrorMessage="Required"
                    ForeColor="Red" Display="Dynamic" ValidationGroup="Val1" />
            </div>
            <div class="form-row">
                <asp:TextBox ID="txtDesc" runat="server" MaxLength="500" TextMode="MultiLine" Rows="3" Columns="50" placeholder="Cluster Description" />
            </div>
        </asp:Panel>

        <br />

        <asp:Button ID="Button1" runat="server" Text="Add Cluster" OnClick="btnAdd_Click" CssClass="btn" />
        <asp:Label ID="Label1" runat="server" CssClass="msg" />

        <br /><br />

        <div class="grid">
            <h3>All Clusters</h3>
            <asp:GridView ID="gvClusters" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ClusterID"
                OnRowEditing="gvClusters_RowEditing"
                OnRowCancelingEdit="gvClusters_RowCancelingEdit"
                OnRowUpdating="gvClusters_RowUpdating"
                OnRowDeleting="gvClusters_RowDeleting"
                CssClass="formal-grid"
                HeaderStyle-CssClass="formal-grid-header"
                RowStyle-CssClass="formal-grid-row"
                AlternatingRowStyle-CssClass="formal-grid-alt"
                SelectedRowStyle-CssClass="formal-grid-selected"
                GridLines="None">

                <Columns>
                    <asp:BoundField DataField="ClusterID" HeaderText="ID" ReadOnly="True" Visible="false" />

                    <asp:TemplateField HeaderText="Cluster Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ClusterName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Bind("ClusterName") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cluster Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("ClusterDescription") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescEdit" runat="server" Text='<%# Bind("ClusterDescription") %>'
                                TextMode="MultiLine" Rows="3" Columns="40" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <%-- Actions column styled like pu_ImplementationInstitutionsSetup.aspx --%>
                    <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="220px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server"
                                CssClass="btn" Text="✏️ Edit"
                                CommandName="Edit"
                                CausesValidation="false" UseSubmitBehavior="false" />
                            &nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server"
                                CssClass="btn btn-danger" Text="🗑️ Delete"
                                CommandName="Delete"
                                OnClientClick="return confirm('Delete this cluster?');"
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
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br /><br />
        </div>
    </div>
</asp:Content>

