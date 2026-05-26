<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addEditLeadingInstitution.aspx.cs" Inherits="addEditLeadingInstitution" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%-- 
        PSEUDOCODE / DETAILED PLAN:

        1. Add page-level CSS to create a clean, card-like panel centered on the page.
           - Define .form-container, .panel, .panel-header, .panel-body, .form-row, .label, .input, .actions, .btn, .btn-primary, .btn-danger, .message.
           - Use simple responsive rules (max-width, padding, and flex layout for label/control pairs).

        2. Keep existing server controls but add CssClass attributes to match CSS.
           - Labels get .label class.
           - TextBox and DropDownList get .input class.
           - Buttons get .btn and contextual classes.
           - ValidationSummary and individual validators styled consistently.

        3. Add a ValidationSummary at top so model errors show cleanly.
           - Red colored message area for lblMessage.

        4. Wrap content inside a styled panel: header (title) + body (form rows) + footer (action buttons).
           - Use semantic markup (divs) compatible with ASP.NET WebForms.

        5. Maintain existing validators and IDs so code-behind bindings continue to work.

        6. Keep delete button visible attribute controlled by server-side code (Visible="false" by default).

    --%>

    <style type="text/css">
        .form-container {
            max-width: 780px;
            margin: 24px auto;
            font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
        }

        .panel {
            background: #ffffff;
            border: 1px solid #e1e4e8;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgba(34,34,34,0.06);
            overflow: hidden;
        }

        .panel-header {
            background: linear-gradient(#f6f8fa, #eef2f6);
            padding: 16px 20px;
            border-bottom: 1px solid #e1e4e8;
        }

        .panel-header h2 {
            margin: 0;
            font-size: 20px;
            color: #222;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .panel-body {
            padding: 18px 20px;
        }

        .message {
            color: #c53030;
            margin-bottom: 12px;
            display: block;
            font-weight: 600;
        }

        .validation-summary {
            border-left: 4px solid #f5c6cb;
            background: #fff5f6;
            color: #8b1f2d;
            padding: 10px 12px;
            margin-bottom: 12px;
            border-radius: 3px;
        }

        .form-row {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 12px;
            flex-wrap: wrap;
        }

        .form-row .label {
            width: 220px;
            min-width: 140px;
            color: #333;
            font-weight: 600;
        }

        .form-row .input {
            flex: 1;
            min-width: 220px;
            padding: 8px 10px;
            border: 1px solid #cfd8e3;
            border-radius: 4px;
            font-size: 14px;
            box-sizing: border-box;
            background: #fff;
        }

        .asp-validator {
            margin-left: 8px;
            color: #d9534f;
            font-size: 13px;
        }

        .actions {
            padding: 14px 20px;
            border-top: 1px solid #f1f4f7;
            display: flex;
            gap: 8px;
            justify-content: flex-end;
            background: #fbfcfd;
        }

        .btn {
            display: inline-block;
            padding: 8px 14px;
            font-size: 14px;
            border-radius: 4px;
            border: 1px solid transparent;
            cursor: pointer;
        }

        .btn-primary {
            background-color: #0078d4;
            color: #fff;
            border-color: #0078d4;
        }

        .btn-primary:hover { background-color: #0066b8; }

        .btn-danger {
            background-color: #d9534f;
            color: #fff;
            border-color: #d43f3a;
        }

        .btn-secondary {
            background: #f3f4f6;
            color: #222;
            border-color: #d1d5db;
        }

        @media (max-width: 640px) {
            .form-row { flex-direction: column; align-items: stretch; }
            .form-row .label { width: auto; min-width: 0; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <br /><br /><br /><br /><br /><br /><br />
    <div class="form-container">
        <div class="panel">
            <div class="panel-header">
                <h2>
                    <asp:Label ID="lblTitle" runat="server" Text="Add Leading Institution" />
                </h2>
            </div>

            <div class="panel-body">
                <asp:Label ID="lblMessage" runat="server" CssClass="message" />

                <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="validation-summary" HeaderText="Please fix the following:" ShowSummary="true" DisplayMode="BulletList" />

                <div class="form-row">
                    <asp:Label ID="lblInstitutionName" runat="server" AssociatedControlID="txtInstitutionName" CssClass="label" Text="Institution Name:" />
                    <asp:TextBox ID="txtInstitutionName" runat="server" CssClass="input" />
                    <asp:RequiredFieldValidator ID="rfvInstitutionName" runat="server" ControlToValidate="txtInstitutionName"
                        ErrorMessage="Institution Name is required." ForeColor="Red" Display="Dynamic" CssClass="asp-validator" />
                </div>

                <div class="form-row">
                    <asp:Label ID="lblInstitutionType" runat="server" AssociatedControlID="ddlInstitutionType" CssClass="label" Text="Institution Type:" />
                    <asp:DropDownList ID="ddlInstitutionType" runat="server" CssClass="input" />
                    <asp:RequiredFieldValidator ID="rfvInstitutionType" runat="server" ControlToValidate="ddlInstitutionType"
                        InitialValue="0" ErrorMessage="Institution Type is required." ForeColor="Red" Display="Dynamic" CssClass="asp-validator" />
                </div>

                <div class="form-row">
                    <asp:Label ID="lblCluster" runat="server" AssociatedControlID="ddlCluster" CssClass="label" Text="Select Cluster:" />
                    <asp:DropDownList ID="ddlCluster" runat="server" CssClass="input" />
                    <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                        InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" CssClass="asp-validator" />
                </div>
            </div>

            <div class="actions">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="false" CssClass="btn btn-danger" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="history.back(); return false;" CssClass="btn btn-secondary" />
            </div>
        </div>
    </div>
</asp:Content>
