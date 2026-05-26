<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="PlanningUnit_1.aspx.cs" Inherits="PlanningUnit_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- Plan:
    - Define base styles for body and headings.
    - Style `.card` panels with padding, border, shadow.
    - Style `.grid` for GridView table appearance.
    - Style `.btn` for primary and default button states, with hover/focus.
    - Style `.error` for validation error messages.
    - Style `.info` for informational inline labels.
    - Style lists inside cards for spacing.
    -->
    <style type="text/css">
        body {
            font-family: Segoe UI, Tahoma, Arial, sans-serif;
            font-size: 14px;
            color: #1f2937;
            background-color: #f7fafc;
            margin: 0;
            padding: 0;
        }

        h3 {
            margin: 16px 0;
            font-weight: 600;
            color: #111827;
        }

        h4 {
            margin: 0 0 12px 0;
            font-weight: 600;
            color: #111827;
        }

        .card {
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 6px;
            padding: 14px 16px;
            margin: 12px 0;
            box-shadow: 0 1px 2px rgba(0,0,0,0.04);
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 8px;
        }

        .grid th,
        .grid td {
            border: 1px solid #e5e7eb;
            padding: 8px 10px;
            text-align: left;
        }

        .grid th {
            background-color: #f3f4f6;
            font-weight: 600;
        }

        .grid tr:nth-child(even) {
            background-color: #fafafa;
        }

        .grid tr:hover {
            background-color: #f9fafb;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            border: 1px solid #2563eb;
            background-color: #2563eb;
            color: #ffffff;
            border-radius: 4px;
            cursor: pointer;
            font-size: 13px;
            line-height: 1.4;
            text-decoration: none;
        }

        .btn:hover {
            background-color: #1d4ed8;
            border-color: #1d4ed8;
        }

        .btn:disabled {
            background-color: #9ca3af;
            border-color: #9ca3af;
            cursor: not-allowed;
        }

        .error {
            color: #b91c1c;
            font-size: 12px;
            margin-left: 8px;
            display: inline-block;
        }

        .info {
            color: #374151;
            font-size: 12px;
            margin-left: 8px;
            display: inline-block;
        }

        .card ul {
            list-style: disc;
            padding-left: 20px;
            margin: 6px 0 0 0;
        }

        .card li {
            margin: 2px 0;
        }

        /* Inputs */
        input[type="text"] {
            padding: 6px 8px;
            border: 1px solid #d1d5db;
            border-radius: 4px;
            outline: none;
        }

        input[type="text"]:focus {
            border-color: #2563eb;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.15);
        }

        hr {
            border: 0;
            border-top: 1px solid #e5e7eb;
            margin: 16px 0;
        }

        /* Align labels and inputs neatly */
        label {
            display: inline-block;
            min-width: 160px;
            margin-right: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">





<br /><br /><br /><br />


    <div class="container">

    <h3>Planning – Frameworks & Reporting Periods</h3>

    <!-- Quick stats -->
    <asp:Panel ID="pnlStats" runat="server" CssClass="card">
        <h4>Quick Stats</h4>
        <ul>
            <li>Frameworks defined: <asp:Label ID="lblFrameworks" runat="server" Text="0" /></li>
            <li>Open reporting periods: <asp:Label ID="lblOpenPeriods" runat="server" Text="0" /></li>
            <li>PMTDP targets loaded: <asp:Label ID="lblPMTDP" runat="server" Text="0" /></li>
            <li>POA targets loaded: <asp:Label ID="lblPOA" runat="server" Text="0" /></li>
        </ul>
    </asp:Panel>

    <hr />

    <!-- Create Framework -->
    <asp:Panel ID="pnlCreateFramework" runat="server" CssClass="card">
        <h4>Create Framework</h4>

        <div>
            <asp:Label AssociatedControlID="txtFrameworkName" runat="server" Text="Name:" />
            <asp:TextBox ID="txtFrameworkName" runat="server" Width="300" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFrameworkName"
                ErrorMessage="Framework name is required." Display="Dynamic" CssClass="error" />
        </div>

        <div style="margin-top:6px;">
            <asp:Label AssociatedControlID="txtStartYear" runat="server" Text="Start Year:" />
            <asp:TextBox ID="txtStartYear" runat="server" Width="80" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartYear"
                ErrorMessage="Start year is required." Display="Dynamic" CssClass="error" />
            <asp:RangeValidator runat="server" ControlToValidate="txtStartYear" MinimumValue="2000"
                MaximumValue="2100" Type="Integer" ErrorMessage="Start year is invalid." CssClass="error" />
        </div>
        q
        <div style="margin-top:6px;">
            <asp:Label AssociatedCqontrolID="txtEndYear" runat="server" Text="End Year:" />
            <asp:TextBox ID="txtEndYear" runat="server" Width="80" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndYear"
                ErrorMessage="End year is required." Display="Dynamic" CssClass="error" />
            <asp:RangeValidator runat="server" ControlToValidate="txtEndYear" MinimumValue="2000"
                MaximumValue="2100" Type="Integer" ErrorMessage="End year is invalid." CssClass="error" />
        </div>

        <div style="margin-top:10px;">
            <asp:Button ID="btnCreateFramework" runat="server" Text="Create Framework"
                OnClick="btnCreateFramework_Click" CssClass="btn" />
            <asp:Label ID="lblCreateFrameworkMsg" runat="server" CssClass="info" />
        </div>
    </asp:Panel>

    <hr />

    <!-- Select Framework -->
    <asp:Panel ID="pnlSelectFramework" runat="server" CssClass="card">
        <h4>Manage Reporting Periods</h4>
        <asp:DropDownList ID="ddlFramework" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlFramework_SelectedIndexChanged" Width="380" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFramework" InitialValue=""
            ErrorMessage="Select a framework." CssClass="error" />
    </asp:Panel>

    <!-- Periods Grid -->
    <asp:GridView ID="gvPeriods" runat="server" AutoGenerateColumns="false"
        DataKeyNames="PeriodId,FrameworkId" CssClass="grid" OnRowCommand="gvPeriods_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="Period ID" DataField="PeriodId" />
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Start" DataField="StartDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="End" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:CheckBoxField HeaderText="Is Open" DataField="IsOpen" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnToggle" runat="server"
                        Text='<%# (bool)Eval("IsOpen") ? "Close" : "Open" %>'
                        CommandName="Toggle" CommandArgument='<%# Eval("PeriodId") %>' CssClass="btn" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Add Period (optional) -->
    <asp:Panel ID="pnlAddPeriod" runat="server" CssClass="card">
        <h4>Add Reporting Period (Optional)</h4>
        <div>
            <asp:Label AssociatedControlID="txtPeriodName" runat="server" Text="Name:" />
            <asp:TextBox ID="txtPeriodName" runat="server" Width="240" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPeriodName"
                ErrorMessage="Period name is required." CssClass="error" />
        </div>
        <div style="margin-top:6px;">
            <asp:Label AssociatedControlID="txtStartDate" runat="server" Text="Start Date (yyyy-MM-dd):" />
            <asp:TextBox ID="txtStartDate" runat="server" Width="140" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate"
                ErrorMessage="Start date is required." CssClass="error" />
        </div>
        <div style="margin-top:6px;">
            <asp:Label AssociatedControlID="txtEndDate" runat="server" Text="End Date (yyyy-MM-dd):" />
            <asp:TextBox ID="txtEndDate" runat="server" Width="140" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate"
                ErrorMessage="End date is required." CssClass="error" />
        </div>
        <div style="margin-top:10px;">
            <asp:Button ID="btnAddPeriod" runat="server" Text="Add Period" OnClick="btnAddPeriod_Click" CssClass="btn" />
            <asp:Label ID="lblAddPeriodMsg" runat="server" CssClass="info" />
        </div>
    </asp:Panel>

    <!-- Messages -->
    <asp:Label ID="lblPageMsg" runat="server" CssClass="info" />

    <hr />

    <!-- Quick Actions -->
    <asp:Panel ID="pnlQuickActions" runat="server" CssClass="card">
        <h4>Quick Actions</h4>
        <ul>
            <li>~/Planning/UploadPMTDP.aspxUpload PMTDP Targets</a></li>
            <li>~/Planning/UploadPOA.aspxUpload POA Targets</a></li>
            <li>~/Planning/Reports.aspxPlanning Reports</a></li>
        </ul>
    </asp:Panel>












        </div>

</asp:Content>

