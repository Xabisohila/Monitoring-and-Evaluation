<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_MySubmissions.aspx.cs" Inherits="i_MySubmissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        :root {
            --bg: #f7f8fb;
            --surface: #ffffff;
            --border: #e5e7eb;
            --text: #1f2937;
            --text-light: #6b7280;
            --muted: #64748b;
            --primary: #0b5ed7;
            --primary-dark: #094db0;
            --primary-light: #e7f1ff;
            --success: #10b981;
            --success-light: #d1fae5;
            --danger: #ef4444;
            --danger-light: #fee2e2;
            --warning: #f59e0b;
            --warning-light: #fef3c7;
            --info: #6366f1;
            --secondary: #6c757d;
            --radius: 12px;
            --radius-sm: 8px;
            --shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px -1px rgba(0, 0, 0, 0.1);
            --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -4px rgba(0, 0, 0, 0.1);
            --font: "Segoe UI", -apple-system, BlinkMacSystemFont, Roboto, "Helvetica Neue", Arial, sans-serif;
        }

        body {
            background: var(--bg);
            font-family: var(--font);
            color: var(--text);
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        .section-title {
            margin-bottom: 32px;
        }

        .section-title h2 {
            font-size: 32px;
            font-weight: 700;
            color: var(--text);
            margin: 0;
            letter-spacing: -0.5px;
        }

        /* Status badges */
        .status-box {
            display: inline-block;
            padding: 6px 12px;
            border-radius: var(--radius-sm);
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .status-pass {
            background-color: var(--success-light);
            color: var(--success);
        }

        .status-fail {
            background-color: var(--danger-light);
            color: var(--danger);
        }

        .status-pending {
            background-color: var(--warning-light);
            color: var(--warning);
        }

        /* GridView Styles */
        .table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: var(--radius);
            overflow: hidden;
            box-shadow: var(--shadow-lg);
            font-family: var(--font);
        }

        .table th {
            background: linear-gradient(180deg, #f9fafb 0%, #f3f4f6 100%);
            padding: 16px 20px;
            font-size: 13px;
            font-weight: 700;
            color: var(--text);
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid var(--border);
            text-align: left;
        }

        .table td {
            padding: 16px 20px;
            font-size: 14px;
            color: var(--text);
            border-bottom: 1px solid #f3f4f6;
            vertical-align: middle;
        }

        .table tr:last-child td {
            border-bottom: none;
        }

        .table tr:hover td {
            background-color: #fafbfc;
            transition: background-color 0.2s ease;
        }

        /* Button Styles */
        .table input[type="button"],
        .table input[type="submit"] {
            padding: 8px 16px;
            font-size: 13px;
            font-weight: 600;
            border-radius: var(--radius-sm);
            border: 1px solid var(--primary);
            background: var(--primary);
            color: #ffffff;
            cursor: pointer;
            transition: all 0.2s ease;
            margin-right: 8px;
        }

        .table input[type="button"]:hover,
        .table input[type="submit"]:hover {
            background: var(--primary-dark);
            border-color: var(--primary-dark);
            transform: translateY(-1px);
            box-shadow: 0 4px 6px -1px rgba(11, 94, 215, 0.2);
        }

        .table input[type="button"]:active,
        .table input[type="submit"]:active {
            transform: translateY(0);
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .container {
                padding: 12px;
            }

            .section-title h2 {
                font-size: 24px;
            }

            .table {
                font-size: 12px;
            }

            .table th,
            .table td {
                padding: 12px 10px;
            }

            .table input[type="button"],
            .table input[type="submit"] {
                padding: 6px 12px;
                font-size: 12px;
            }
        }

        /* Empty state */
        .table tr td[colspan] {
            text-align: center;
            padding: 40px 20px;
            color: var(--text-light);
            font-size: 15px;
        }

        /* Pager styles (if pagination is added) */
        .table .GridPager {
            background: #fafbfc;
            padding: 16px;
            border-top: 1px solid var(--border);
        }

        .table .GridPager td {
            text-align: center;
        }

        .table .GridPager a {
            display: inline-block;
            padding: 6px 12px;
            margin: 0 4px;
            border-radius: var(--radius-sm);
            color: var(--primary);
            text-decoration: none;
            transition: all 0.2s ease;
        }

        .table .GridPager a:hover {
            background: var(--primary-light);
        }

        .table .GridPager span {
            display: inline-block;
            padding: 6px 12px;
            margin: 0 4px;
            border-radius: var(--radius-sm);
            background: var(--primary);
            color: white;
            font-weight: 600;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>My Submissions</strong></span></h2>
            </div>
        </div>
        <br /><br />

        <asp:GridView runat="server" ID="gvSubmissions" AutoGenerateColumns="false" OnRowCommand="gvSubmissions_RowCommand" CssClass="table">
            <Columns>
                <asp:BoundField DataField="ReportID" HeaderText="Report ID" />
                <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" />
                <asp:BoundField DataField="FinancialYear" HeaderText="Year" />
                <asp:BoundField DataField="QuarterNumber" HeaderText="Quarter" />
                <asp:BoundField DataField="ActualValue" HeaderText="Actual" />
                <asp:BoundField DataField="AchievedFlag" HeaderText="Achieved" />
                <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><%# Eval("StatusHtml") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="POE">
                    <ItemTemplate><%# Eval("POEHtml") %></ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField CommandName="View" Text="View Details" />
                <asp:ButtonField CommandName="Workflow" Text="Workflow History" />
            </Columns>
        </asp:GridView>

    </div>
    <br /><br />

</asp:Content>

