<%@ Page Title="Programme Institutions" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_ProgrammeInstitutions.aspx.cs" Inherits="i_ProgrammeInstitutions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        :root { --bg: #f7f8fb; --surface: #ffffff; --border: #d9dee5; --text: #1f2937; --muted: #64748b; --primary: #0b5ed7; --primary-dark: #094db0; --danger: #dc3545; --radius: 10px; --shadow: 0 2px 10px rgba(16, 24, 40, 0.08); --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif; }
        body { background: var(--bg); font-family: var(--font); color: var(--text); }
        .section-title h2 { margin: 0 0 12px 0; font-size: 26px; font-weight: 700; color: var(--text); letter-spacing: .2px; }
        .form-card { background: var(--surface); border: 1px solid var(--border); border-radius: var(--radius); box-shadow: var(--shadow); padding: 18px; margin-bottom: 16px; }
        asp\:label, label { display: inline-block; font-size: 13px; color: var(--muted); margin: 10px 0 6px; }
        asp\:textbox, input[type="text"], textarea, select, asp\:dropdownlist { display: block; width: 100%; max-width: 560px; padding: 9px 12px; border: 1px solid var(--border); border-radius: 8px; background: var(--surface); color: var(--text); transition: border-color .15s ease, box-shadow .15s ease; }
        asp\:textbox:focus, input[type="text"]:focus, textarea:focus, select:focus, asp\:dropdownlist:focus { outline: none; border-color: var(--primary); box-shadow: 0 0 0 3px rgba(11,94,215,.15); }
        small { color: var(--muted); }
        asp\:validationsummary, .validation-summary-errors { display: block; margin-bottom: 10px; color: var(--danger); font-size: 13px; }
        asp\:button, input[type="submit"], button { display: inline-block; margin-top: 12px; padding: 9px 14px; font-size: 14px; font-weight: 600; border-radius: 8px; border: 1px solid var(--primary); background: var(--primary); color: #fff; cursor: pointer; transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease; }
        asp\:button:hover, input[type="submit"]:hover, button:hover { background: var(--primary-dark); border-color: var(--primary-dark); }
        asp\:button:focus-visible, input[type="submit"]:focus-visible, button:focus-visible { outline: none; box-shadow: 0 0 0 3px rgba(11,94,215,.25); }
        .btn-default { background: #6c757d; border-color: #6c757d; }
        .btn-default:hover { background: #5c636a; border-color: #565e64; }
        hr { border: none; border-top: 1px solid var(--border); margin: 18px 0; }
        table, asp\:gridview { width: 100%; border-collapse: collapse; background: var(--surface); border: 1px solid var(--border); border-radius: 10px; overflow: hidden; box-shadow: var(--shadow); font-family: var(--font); }
        table th, table td { padding: 10px 12px; font-size: 13px; color: var(--text); border-bottom: 1px solid var(--border); text-align: left; vertical-align: top; }
        table th { background: #f1f5f9; font-weight: 600; }
        table tr:last-child td { border-bottom: none; }
        .programme-name { font-weight: 600; color: var(--text); font-size: 15px; margin-bottom: 20px; }
        @media (/*max-width: 640px*/) { .container { padding: 12px; } asp\:textbox, input[type="text"], textarea, select { /*max-width: 100%;*/ } table th, table td { padding: 8px 10px; font-size: 12px; } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Manage Supporting Institutions</strong></span></h2>
            </div>
        </div>
        <br /><br />

        <div class="programme-name">
            Programme: <asp:Label ID="lblProgrammeName" runat="server"></asp:Label>
        </div>

        <div>
            <asp:Label runat="server" Text="Institution:" AssociatedControlID="ddlInstitution" />
            <asp:DropDownList runat="server" ID="ddlInstitution" Width="400" />
        </div>

        <asp:Button runat="server" ID="btnAdd" Text="Add Institution" OnClick="btnAdd_Click" />
        <asp:Button runat="server" ID="btnBack" Text="Back to Programmes" OnClick="btnBack_Click" CssClass="btn-default" />
        <hr />

        <asp:GridView
            runat="server"
            ID="gvInstitutions"
            AutoGenerateColumns="false"
            DataKeyNames="ProgrammeInstitutionID"
            OnRowCommand="gvInstitutions_RowCommand"
            OnRowDataBound="gvInstitutions_RowDataBound"
            EmptyDataText="No supporting institutions assigned to this programme.">
            <Columns>
                <asp:BoundField DataField="ProgrammeInstitutionID" HeaderText="ID" Visible="false" />
                <asp:TemplateField HeaderText="Institution Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblInstitutionName" />
                        <asp:HiddenField runat="server" ID="hfInstitutionID" Value='<%# Eval("InstitutionID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" 
                            ID="btnDelete" 
                            CommandName="DeleteRow" 
                            CommandArgument='<%# Eval("ProgrammeInstitutionID") %>'
                            Text="Remove"
                            OnClientClick="return confirm('Are you sure you want to remove this institution from the programme?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br /><br />
</asp:Content>