<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_SystemPeriodsAdmin.aspx.cs" Inherits="i_SystemPeriodsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <style type="text/css">
        :root {
            --bg: #f7f8fb;
            --surface: #ffffff;
            --border: #d9dee5;
            --text: #1f2937;
            --muted: #64748b;
            --primary: #0b5ed7;
            --primary-dark: #094db0;
            --success: #16a34a;
            /*--warning: #f59e0b;
            --danger: #dc2626;*/
            --radius: 10px;
            --shadow: 0 2px 10px rgba(16, 24, 40, 0.08);
            --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        }

        .page {
            max-width: 1100px;
            margin: 0 auto;
            padding: 16px;
            font-family: var(--font);
            color: var(--text);
        }

        .card {
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: var(--radius);
            box-shadow: var(--shadow);
            padding: 18px;
            margin-bottom: 16px;
        }

        .card h3 {
            margin: 0 0 10px 0;
            font-size: 20px;
            font-weight: 700;
            letter-spacing: .2px;
        }

        .alert {
            padding: 10px 12px;
            border-radius: 8px;
            font-size: 13px;
            margin-bottom: 10px;
        }
        .alert-info {
            background: #eef6ff;
            border: 1px solid #cfe2ff;
            color: #084298;
        }

        .btn-group { display: flex; flex-wrap: wrap; gap: 10px; align-items: center; }
        .btn, .btn.btn-outline, .btn.btn-secondary,
        asp\:button, input[type="submit"], button {
            display: inline-block;
            padding: 9px 14px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
            border: 1px solid var(--primary);
            background: var(--primary);
            color: #fff;
        }
        .btn:hover { background: var(--primary-dark); border-color: var(--primary-dark); }
        .btn.btn-outline { background: transparent; color: var(--primary); }
        .btn.btn-outline:hover { background: rgba(11,94,215,.08); }
        .btn.btn-secondary { background: #6b7280; border-color: #6b7280; }
        .btn.btn-secondary:hover { background: #4b5563; border-color: #4b5563; }

        label, asp\:label { display: inline-block; font-size: 13px; color: var(--muted); }
        .input, input[type="text"], input[type="datetime-local"], select, asp\:dropdownlist, asp\:textbox {
            display: block;
            width: 100%;
            max-width: 360px;
            padding: 9px 12px;
            border: 1px solid var(--border);
            border-radius: 8px;
            background: var(--surface);
            color: var(--text);
            transition: border-color .15s ease, box-shadow .15s ease;
        }
        .input:focus, input[type="text"]:focus, input[type="datetime-local"]:focus, select:focus {
            outline: none; border-color: var(--primary); box-shadow: 0 0 0 3px rgba(11,94,215,.15);
        }

        .validation-summary-errors { color: #dc3545; font-size: 13px; margin-bottom: 8px; }

        /* Grid */
        .admin-grid { overflow: hidden; }
        table, asp\:gridview {
            width: 100%;
            border-collapse: collapse;
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: 10px;
            box-shadow: var(--shadow);
            font-family: var(--font);
        }
        table th, table td {
            padding: 10px 12px;
            font-size: 13px;
            color: var(--text);
            border-bottom: 1px solid var(--border);
            text-align: left;
            vertical-align: middle;
        }
        table th { background: #f1f5f9; font-weight: 600; }
        table tr:last-child td { border-bottom: none; }

        /* Grid Action Buttons */
        /*table td a, table td input[type="button"] {
            display: inline-block;
            padding: 6px 12px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 6px;
            cursor: pointer;
            text-decoration: none;
            transition: all .15s ease;
            border: 1px solid transparent;
            margin-right: 4px;
        }*/

        /* Edit Button - Primary Blue */
        /*table td a[href*="EditRow"], table td input[value="Edit"] {
            background: var(--primary);
            color: #fff;
            border-color: var(--primary);
        }
        table td a[href*="EditRow"]:hover, table td input[value="Edit"]:hover {
            background: var(--primary-dark);
            border-color: var(--primary-dark);
            box-shadow: 0 2px 6px rgba(11, 94, 215, 0.25);
        }*/

        /* Delete Button - Danger Red */
        /*table td a[href*="DeleteRow"], table td input[value="Delete"] {
            background: var(--danger);
            color: #fff;
            border-color: var(--danger);
        }
        table td a[href*="DeleteRow"]:hover, table td input[value="Delete"]:hover {
            background: #b91c1c;
            border-color: #b91c1c;
            box-shadow: 0 2px 6px rgba(220, 38, 38, 0.25);
        }*/

        /* Close Now Button - Warning Orange */
        /*table td a[href*="CloseNow"], table td input[value="Close Now"] {
            background: var(--warning);
            color: #fff;
            border-color: var(--warning);
        }
        table td a[href*="CloseNow"]:hover, table td input[value="Close Now"]:hover {
            background: #d97706;
            border-color: #d97706;
            box-shadow: 0 2px 6px rgba(245, 158, 11, 0.25);
        }*/

        /* Badges */
        .badge {
            display: inline-block;
            padding: 4px 8px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 600;
        }
        .badge-success { background: #dcfce7; color: #166534; border: 1px solid #bbf7d0; }
        .badge-muted { background: #f1f5f9; color: #334155; border: 1px solid #e2e8f0; }

        #lblMsg { display: block; margin-top: 8px; font-size: 13px; color: var(--muted); }

        @media (max-width: 640px) {
            .page { padding: 12px; }
            .btn-group { gap: 8px; }
            .input { max-width: 100%; }
            table th, table td { padding: 8px 10px; font-size: 12px; }
            /*table td a, table td input[type="button"] { 
                padding: 5px 10px; 
                font-size: 11px;
                margin-right: 3px;
            }*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <br /><br /><br /><br />
<div class="container">
    <div class="section-title text-center">
        <br />
        <div>
            <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Planning: System Periods (Open / Close Windows)</strong></span></h2>
        </div>
    </div>
    <%--<br /><br />--%>
    

    <div class="page">

        <!-- Form Card -->
        <div class="card">
            <%--<h2>System Periods (Open / Close Windows)</h2>--%>
            <div class="alert alert-info">
                Use <strong>Suggest Dates</strong> to auto-fill <em>Open = 1st</em> and <em>Close = 20th</em> of the month after the quarter ends.
            </div>

            <asp:ValidationSummary runat="server" ID="valSummary" CssClass="validation-summary-errors" />

            <asp:HiddenField runat="server" ID="hfID" />

            <div style="max-width:720px;">
                <label for="ddlFY">Financial Year</label>
                <asp:DropDownList runat="server" ID="ddlFY" CssClass="input">
                </asp:DropDownList>

                <label for="ddlQ" style="margin-top:.75rem;">Quarter</label>
                <asp:DropDownList runat="server" ID="ddlQ" CssClass="input">
                    <asp:ListItem Text="Q1" Value="1" />
                    <asp:ListItem Text="Q2" Value="2" />
                    <asp:ListItem Text="Q3" Value="3" />
                    <asp:ListItem Text="Q4" Value="4" />
                </asp:DropDownList>

                <label for="txtOpen" style="margin-top:.75rem;">Open Date (yyyy-MM-dd HH:mm)</label>
                <asp:TextBox runat="server" ID="txtOpen" CssClass="input" TextMode="DateTimeLocal" />

                <label for="txtClose" style="margin-top:.75rem;">Close Date (yyyy-MM-dd HH:mm)</label>
                <asp:TextBox runat="server" ID="txtClose" CssClass="input" TextMode="DateTimeLocal" />

                <div style="margin-top:.75rem;">
                    <asp:CheckBox runat="server" ID="chkOpen" Text="Is Open" />
                </div>

                <div class="btn-group" style="margin-top:1rem;">
                    <asp:Button runat="server" ID="btnSuggest" CssClass="btn btn-outline" Text="Suggest Dates" OnClick="btnSuggest_Click" />
                    <asp:Button runat="server" ID="btnSave" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                </div>

                <br />

                <asp:Label runat="server" ID="lblMsg" />
            </div>
        </div>

        <!-- Grid Card -->
        <div class="card admin-grid">
            <h3>Existing Periods</h3>
            <asp:GridView runat="server" ID="gv" AutoGenerateColumns="false" CssClass="GridView"
                DataKeyNames="PeriodID" OnRowCommand="gv_RowCommand">
                <Columns>
                    <asp:BoundField DataField="PeriodID" HeaderText="ID" />
                    <asp:BoundField DataField="FinancialYear" HeaderText="FY" />
                    <asp:BoundField DataField="Quarter" HeaderText="Q" />
                    <asp:BoundField DataField="OpenDate" HeaderText="Open" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    <asp:BoundField DataField="CloseDate" HeaderText="Close" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    <asp:TemplateField HeaderText="Open?">
                        <ItemTemplate>
                            <span class='<%# (Convert.ToBoolean(Eval("IsOpen")) ? "badge badge-success" : "badge badge-muted") %>'>
                                <%# Convert.ToBoolean(Eval("IsOpen")) ? "Yes" : "No" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="EditRow" Text="Edit" />
                    <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
                    <asp:ButtonField CommandName="CloseNow" Text="Close Now" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

    </div>
<br /><br />


</asp:Content>

