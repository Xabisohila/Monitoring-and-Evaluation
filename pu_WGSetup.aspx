<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pu_WGSetup.aspx.cs" Inherits="pu_WGSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .msg { color: #0B6A0B; margin-left: 10px; }
        .err { color: #B00020; margin-left: 10px; }
        .btn { display: inline-block; padding: 6px 12px; border: 1px solid #D0D0D0; border-radius: 4px; background: #F9FAFB; color: #1E1E1E; text-decoration: none; cursor: pointer; }
        .btn:hover { background: #F0F2F5; }
        .btn-primary { background: #1F4B99; border-color: #1F4B99; color: #fff; }
        .btn-primary:hover { background: #1B3F82; }
        .btn-danger { background: #C62828; border-color: #B71C1C; color: #fff; }
        .btn-danger:hover { background: #B71C1C; }

        .formal-grid { width: 100%; border-collapse: separate; border-spacing: 0; font-family: "Segoe UI", Arial, sans-serif; font-size: 14px; color: #1E1E1E; border: 1px solid #D0D0D0; border-radius: 6px; overflow: hidden; background: #ffffff; }
        .formal-grid-header th { background: #F3F3F3; color: #111; font-weight: 600; padding: 10px 14px; border-bottom: 1px solid #D0D0D0; text-align: left; white-space: nowrap; }
        .formal-grid-row td { padding: 10px 14px; border-bottom: 1px solid #E7E7E7; background: #ffffff; }
        .formal-grid-alt td { background: #FAFAFA; padding: 10px 14px; border-bottom: 1px solid #E7E7E7; }
        .formal-grid tr:hover td { background: #F0F6FF; }
        .formal-grid-selected td { background: #D9E8FF !important; color: #003060 !important; }
        .formal-grid input[type="text"],
        .formal-grid textarea,
        .formal-grid select { width: 95%; font-family: "Segoe UI", Arial; font-size: 14px; padding: 6px; border: 1px solid #C8C8C8; border-radius: 4px; outline: none; }
        .formal-grid input[type="text"]:focus,
        .formal-grid textarea:focus,
        .formal-grid select:focus { border-color: #4A90E2; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    <br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s">
                    <span><strong>Working Groups</strong></span>
                </h2>
            </div>
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="msg" />
        <asp:Label ID="lblError" runat="server" CssClass="err" />

        <br />


        <table>
            <tr>
                <td style="width: 150px; vertical-align: top;">
                    <asp:Label ID="lblCluster" runat="server" Text="Select Cluster:" />
                </td>
                <td style="width: 300px; vertical-align: top;">
                    <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" />
                    <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                        InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblLeadInstitution" runat="server" Text="Lead Institution:" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlLeadInstitution" runat="server">
                        <asp:ListItem Text="-- Select Institution --" Value="0" />
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvLeadInstitution" runat="server" ControlToValidate="ddlLeadInstitution"
                        InitialValue="0" ErrorMessage="Lead Institution is required." ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblWGName" runat="server" Text="Working Group Name:" />
                </td>
                <td>
                    <asp:TextBox ID="txtWGName" runat="server" MaxLength="200" />
                    <asp:RequiredFieldValidator ID="rfvWGName" runat="server" ControlToValidate="txtWGName"
                        ErrorMessage="Working Group name is required." ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWGDescription" runat="server" Text="Description:" />
                </td>
                <td>
                    <asp:TextBox ID="txtWGDescription" runat="server" TextMode="MultiLine" Rows="4" Columns="50" MaxLength="1000" Width="473px"/>
                </td>
            </tr>
        </table>

        <br />
        

       <%-- <div class="form-group">
            <asp:Label ID="lblCluster" runat="server" Text="Select Cluster:" />
            <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" />
        </div>--%>

        <%--<div class="form-group">
            <asp:Label ID="lblLeadInstitution" runat="server" Text="Lead Institution:" />
            <asp:DropDownList ID="ddlLeadInstitution" runat="server" />
            <asp:RequiredFieldValidator ID="rfvLeadInstitution" runat="server" ControlToValidate="ddlLeadInstitution"
                InitialValue="0" ErrorMessage="Lead Institution is required." ForeColor="Red" Display="Dynamic" />
        </div>--%>

        <%--<div class="form-group">
            <asp:Label ID="lblWGName" runat="server" Text="Working Group Name:" />
            <asp:TextBox ID="txtWGName" runat="server" MaxLength="200" />
            <asp:RequiredFieldValidator ID="rfvWGName" runat="server" ControlToValidate="txtWGName"
                ErrorMessage="Working Group name is required." ForeColor="Red" Display="Dynamic" />
        </div>--%>

        <%--<div class="form-group">
            <asp:Label ID="lblWGDescription" runat="server" Text="Description:" />
            <asp:TextBox ID="txtWGDescription" runat="server" TextMode="MultiLine" Rows="4" Columns="50" MaxLength="1000" />
        </div>--%>

        <asp:Button ID="btnSave" runat="server" Text="Add Working Group" OnClick="btnSave_Click" CssClass="btn btn-primary" />

        <br /><br />
        <h3>All Working Groups</h3>
        <asp:GridView ID="gvWorkingGroups" runat="server" AutoGenerateColumns="False"
            DataKeyNames="WorkingGroupID"
            OnRowEditing="gvWorkingGroups_RowEditing"
            OnRowCancelingEdit="gvWorkingGroups_RowCancelingEdit"
            OnRowUpdating="gvWorkingGroups_RowUpdating"
            OnRowDeleting="gvWorkingGroups_RowDeleting"
            CssClass="formal-grid"
            HeaderStyle-CssClass="formal-grid-header"
            RowStyle-CssClass="formal-grid-row"
            AlternatingRowStyle-CssClass="formal-grid-alt"
            SelectedRowStyle-CssClass="formal-grid-selected"
            GridLines="None">
            <Columns>
                <asp:BoundField DataField="WorkingGroupID" HeaderText="ID" ReadOnly="True" Visible="false" />

                <asp:TemplateField HeaderText="Cluster">
                    <ItemTemplate>
                        <asp:Label ID="lblClusterName" runat="server" Text='<%# Eval("ClusterName") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlClusterEdit" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Lead Institution">
                    <ItemTemplate>
                        <asp:Label ID="lblInstitutionName" runat="server" Text='<%# Eval("InstitutionName") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlInstitutionEdit" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="WG Name">
                    <ItemTemplate>
                        <asp:Label ID="lblWGNameItem" runat="server" Text='<%# Eval("WG_Name") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWGNameEdit" runat="server" Text='<%# Bind("WG_Name") %>' MaxLength="200" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblWGDescItem" runat="server" Text='<%# Eval("WG_Description") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWGDescEdit" runat="server" Text='<%# Bind("WG_Description") %>' TextMode="MultiLine" Rows="3" Columns="40" MaxLength="1000" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server"
                            CssClass="btn" Text="✏️ Edit"
                            CommandName="Edit"
                            CausesValidation="false" UseSubmitBehavior="false" />
                        &nbsp;
                        <asp:LinkButton ID="lnkDelete" runat="server"
                            CssClass="btn btn-danger" Text="🗑️ Delete"
                            CommandName="Delete"
                            OnClientClick="return confirm('Delete this working group?');"
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
    </div>

    <br /><br />
    <br />
</asp:Content>

