<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_ReportDetails.aspx.cs" Inherits="i_ReportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        :root {
            --bg: #f7f8fb;
            --surface: #fff;
            --border: #d9dee5;
            --text: #1f2937;
            --muted: #64748b;
            --primary: #0b5ed7;
            --primary-dark: #094db0;
            --success: #28a745;
            --danger: #dc3545;
            --warning: #ffc107;
        }

        /* Page Container */
        .page {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        /* Card Styling */
        .card {
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: 8px;
            padding: 24px;
            margin-bottom: 24px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
            transition: box-shadow 0.3s ease;
        }

        .card:hover {
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
        }

        .card h3 {
            margin: 0 0 16px;
            font-size: 20px;
            font-weight: 700;
            color: var(--text);
            border-bottom: 2px solid var(--primary);
            padding-bottom: 8px;
        }

        /* Header Label */
        .lblHeader {
            font-size: 24px;
            font-weight: 700;
            color: var(--text);
            display: block;
            margin-bottom: 16px;
        }

        /* Button Group */
        .btn-group {
            display: flex;
            gap: 12px;
            flex-wrap: wrap;
        }

        .btn {
            display: inline-block;
            padding: 10px 16px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 8px;
            border: 1px solid var(--primary);
            background: var(--primary);
            color: #fff;
            text-decoration: none;
            cursor: pointer;
            transition: all 0.15s ease;
        }

        .btn:hover {
            background: var(--primary-dark);
            border-color: var(--primary-dark);
            color: #fff;
            text-decoration: none;
            transform: translateY(-1px);
            box-shadow: 0 4px 8px rgba(11, 94, 215, 0.2);
        }

        .btn-outline {
            background: var(--surface);
            color: var(--primary);
            border: 1px solid var(--primary);
        }

        .btn-outline:hover {
            background: var(--primary);
            color: #fff;
        }

        /* GridView Styling */
        .GridView {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
        }

        .GridView th,
        .GridView td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--border);
        }

        .GridView th {
            background: #f8f9fa;
            font-weight: 600;
            color: var(--text);
            width: 200px;
            vertical-align: top;
        }

        .GridView td {
            color: var(--text);
        }

        .GridView tr:last-child th,
        .GridView tr:last-child td {
            border-bottom: none;
        }

        .GridView tr:hover {
            background: #f8f9fa;
        }

        /* Badge Styling */
        .badge {
            display: inline-block;
            padding: 4px 10px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            margin-left: 8px;
        }

        .badge-success {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .badge-danger {
            background: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }

        .badge-warning {
            background: #fff3cd;
            color: #856404;
            border: 1px solid #ffeeba;
        }

        .badge-info {
            background: #d1ecf1;
            color: #0c5460;
            border: 1px solid #bee5eb;
        }

        /* Alert Styling */
        .alert {
            padding: 12px 16px;
            border-radius: 6px;
            font-size: 14px;
            margin: 8px 0;
        }

        .alert-success {
            background: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
            border-left: 4px solid var(--success);
        }

        .alert-info {
            background: #d1ecf1;
            color: #0c5460;
            border: 1px solid #bee5eb;
            border-left: 4px solid #17a2b8;
        }

        /* Evidence GridView */
        #ContentPlaceHolder1_gvEvidence {
            width: 100%;
            border-collapse: collapse;
        }

        #ContentPlaceHolder1_gvEvidence th {
            background: #f8f9fa;
            padding: 12px;
            text-align: left;
            font-weight: 600;
            border-bottom: 2px solid var(--border);
        }

        #ContentPlaceHolder1_gvEvidence td {
            padding: 12px;
            border-bottom: 1px solid var(--border);
        }

        #ContentPlaceHolder1_gvEvidence tr:hover {
            background: #f8f9fa;
        }

        #ContentPlaceHolder1_gvEvidence a {
            color: var(--primary);
            text-decoration: none;
            font-weight: 600;
            transition: color 0.15s ease;
        }

        #ContentPlaceHolder1_gvEvidence a:hover {
            color: var(--primary-dark);
            text-decoration: underline;
        }

        /* Empty State */
        .empty-state {
            text-align: center;
            padding: 32px;
            color: var(--muted);
            font-size: 14px;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .page {
                padding: 12px;
            }

            .card {
                padding: 16px;
            }

            .btn-group {
                flex-direction: column;
            }

            .btn {
                width: 100%;
                text-align: center;
            }

            .GridView th,
            .GridView td {
                padding: 8px;
                font-size: 13px;
            }

            .GridView th {
                width: 120px;
            }
        }

        @media (max-width: 480px) {
            .GridView {
                font-size: 12px;
            }

            .GridView th {
                width: 100px;
            }

            .card h3 {
                font-size: 18px;
            }
        }

        /* Additional Table Styling for Evidence */
        .GridView.table {
            margin-top: 8px;
        }

        /* Status indicators */
        .status-indicator {
            display: inline-flex;
            align-items: center;
            gap: 8px;
        }

        .status-dot {
            width: 8px;
            height: 8px;
            border-radius: 50%;
            display: inline-block;
        }

        .status-dot.success {
            background: var(--success);
        }

        .status-dot.danger {
            background: var(--danger);
        }

        .status-dot.warning {
            background: var(--warning);
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



        <br /><br /><br /><br />
<div class="container">
    <div class="section-title text-center">
        <br />
        <div>
            <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Report Details</strong></span></h2>
        </div>
    </div>
    <br /><br />


    

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportDetails.aspx.cs" Inherits="Reporting_ReportDetails" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Report Details</title>
    ~/Content/app.css
</head>
<body>
<form id="form1" runat="server">--%>
<div class="page">

    <div class="card">
        <%--<h2>Report Details</h2>--%>
        <asp:Label ID="lblHeader" runat="server" CssClass="lblHeader"></asp:Label>

        <div class="btn-group" style="margin-top:10px;">
            <asp:HyperLink runat="server" ID="lnkBackMySubs" CssClass="btn btn-outline" Text="← Back to My Submissions" NavigateUrl="~/i_MySubmissions.aspx" />
            <asp:HyperLink runat="server" ID="lnkBackMyTasks" CssClass="btn btn-outline" Text="← Back to My Tasks" NavigateUrl="~/i_MyTasks.aspx" />
            <asp:HyperLink runat="server" ID="lnkHistory" CssClass="btn btn" Text="View Workflow History" />
        </div>
    </div>

    <!-- Indicator + Target Summary -->
    <div class="card">
        <h3>Summary</h3>
        <table class="GridView">
            <tr><th>Indicator</th><td><asp:Label ID="lblIndicator" runat="server" /></td></tr>
            <tr><th>Financial Year</th><td><asp:Label ID="lblFY" runat="server" /></td></tr>
            <tr><th>Quarter</th><td><asp:Label ID="lblQuarter" runat="server" /></td></tr>
            <tr><th>Planned Target</th><td><asp:Label ID="lblPlanned" runat="server" /></td></tr>
            <tr><th>Actual Value</th><td><asp:Label ID="lblActual" runat="server" /></td></tr>
            <tr><th>Achieved?</th>
                <td>
                    <asp:Label ID="lblAchieved" runat="server" />
                    <asp:Label ID="lblAchievedBadge" runat="server" />
                </td>
            </tr>
            <tr><th>Submitted By</th><td><asp:Label ID="lblSubmitter" runat="server" /></td></tr>
            <tr><th>Submitted Date</th><td><asp:Label ID="lblSubmittedDate" runat="server" /></td></tr>
        </table>
    </div>

    <!-- Deviation / Remedial -->
    <div class="card">
        <h3>Deviation & Remedial Actions</h3>
        <asp:Panel ID="pnlDeviation" runat="server" Visible="false">
            <table class="GridView">
                <tr><th>Deviation Reason</th><td><asp:Label ID="lblDeviation" runat="server" /></td></tr>
                <tr><th>Remedial Actions</th><td><asp:Label ID="lblRemedial" runat="server" /></td></tr>
                <tr><th>Due Date</th><td><asp:Label ID="lblDueDate" runat="server" /></td></tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlNoDeviation" runat="server" Visible="false">
            <div class="alert alert-success">No deviation recorded — indicator achieved.</div>
        </asp:Panel>
    </div>

    <!-- POE Section -->
    <div class="card">
        <h3>Evidence Files (POE)</h3>
        <asp:GridView runat="server" ID="gvEvidence" AutoGenerateColumns="false" CssClass="GridView">
            <Columns>
                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                <asp:HyperLinkField HeaderText="Download"
                    Text="Download"
                    DataNavigateUrlFields="EvidenceID"
                    DataNavigateUrlFormatString="~/Reporting/EvidenceDownload.ashx?id={0}" />
                <asp:BoundField DataField="UploadedDate" HeaderText="Uploaded" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- Workflow Summary -->
    <div class="card">
        <h3>Latest Workflow Status</h3>
        <table class="GridView">
            <tr><th>Stage</th><td><asp:Label ID="lblLastStage" runat="server" /></td></tr>
            <tr><th>Status</th><td><asp:Label ID="lblLastStatus" runat="server" /></td></tr>
            <tr><th>Action By</th><td><asp:Label ID="lblLastActionBy" runat="server" /></td></tr>
            <tr><th>Action Date</th><td><asp:Label ID="lblLastActionDate" runat="server" /></td></tr>
            <tr><th>Comments</th><td><asp:Label ID="lblLastComments" runat="server" /></td></tr>
        </table>
    </div>

</div>




        </div>
<br /><br />






</asp:Content>

