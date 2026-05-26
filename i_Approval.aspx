<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_Approval.aspx.cs" Inherits="i_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <style type="text/css">
        
   /*:root {
    --color-bg: #f7f8fa;
    --color-surface: #ffffff;
    --color-border: #d9dee5;
    --color-primary: #0b5ed7;
    --color-primary-dark: #094db0;
    --color-text: #212529;
    --color-text-muted: #6c757d;
    --color-danger: #dc3545;
    --radius: 8px;
    --shadow: 0 2px 8px rgba(16, 24, 40, 0.08);
    --spacing: 12px;
    --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", "Liberation Sans", sans-serif;
}*/

/* Page container */
/*.container {
    max-width: 980px;
    margin: 0 auto;
    padding: 24px;
    background: var(--color-bg);
}*/

/* Card section */
/*.container > div > div.section-title {
    margin-bottom: 8px;
}

.section-title h2 {
    font-family: var(--font);
    font-size: 28px;
    font-weight: 700;
    color: var(--color-text);
    margin: 0;
    letter-spacing: 0.2px;
}

.background.double.animated.wow.fadeInUp.color1 {
    background: none;
}*/

/* Form card */
/*.container > div > div + br + .aspnet-form-wrapper,
.container > div > div + br + div {
    background: var(--color-surface);
    border: 1px solid var(--color-border);
    border-radius: var(--radius);
    box-shadow: var(--shadow);
    padding: 24px;
}*/

/* Labels */
/*asp\:label,
label[for] {
    display: inline-block;
    font-family: var(--font);
    font-size: 14px;
    color: var(--color-text);
    margin: 12px 0 6px;
}*/

/* Inputs */
/*input[type="text"],
select {
    display: block;
    width: 100%;
    max-width: 420px;
    padding: 10px 12px;
    border: 1px solid var(--color-border);
    border-radius: 6px;
    font-family: var(--font);
    font-size: 14px;
    color: var(--color-text);
    background: var(--color-surface);
    transition: border-color 0.15s ease, box-shadow 0.15s ease;
}

input[type="text"]:focus,
select:focus {
    outline: none;
    border-color: var(--color-primary);
    box-shadow: 0 0 0 3px rgba(11, 94, 215, 0.15);
}*/

/* Button */
/*asp\:button,
button,
input[type="submit"] {
    display: inline-block;
    margin-top: 16px;
    padding: 10px 16px;
    font-family: var(--font);
    font-size: 14px;
    font-weight: 600;
    color: #fff;
    background: var(--color-primary);
    border: 1px solid var(--color-primary);
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.15s ease, border-color 0.15s ease, box-shadow 0.15s ease;
}

asp\:button:hover,
button:hover,
input[type="submit"]:hover {
    background: var(--color-primary-dark);
    border-color: var(--color-primary-dark);
}

asp\:button:focus-visible,
button:focus-visible,
input[type="submit"]:focus-visible {
    outline: none;
    box-shadow: 0 0 0 3px rgba(11, 94, 215, 0.25);
}*/

/* Validation */
/*.text-danger,
.validation-summary-errors {
    color: var(--color-danger);
    font-size: 13px;
    margin-bottom: 8px;
}*/

/* GridView */
/*table,
asp\:gridview {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
    font-family: var(--font);
    background: var(--color-surface);
    border: 1px solid var(--color-border);
    border-radius: var(--radius);
    overflow: hidden;
    box-shadow: var(--shadow);
}

table th,
table td {
    padding: 10px 12px;
    font-size: 14px;
    color: var(--color-text);
    border-bottom: 1px solid var(--color-border);
    text-align: left;
    vertical-align: middle;
}

table th {
    background: #f0f2f5;
    font-weight: 600;
}

table tr:last-child td {
    border-bottom: none;
}*/

/* Grid buttons in cells */
/*table td .aspNetDisabled,
table td input[type="submit"],
table td button {
    padding: 6px 10px;
    font-size: 13px;
    border-radius: 6px;
}*/

/* Messages */
/*#lblMsg {
    display: block;
    margin-bottom: 8px;
    padding: 10px 12px;
    border-radius: 6px;
    font-size: 14px;
}*/

/* Spacing between fields */
/*asp\:label + select,
asp\:label + input[type="text"] {
    margin-top: 4px;
    margin-bottom: 8px;
}*/

/* Divider */
/*hr {
    border: none;
    border-top: 1px solid var(--color-border);
    margin: 20px 0;
}*/

/* Responsive */
/*@media (max-width: 640px) {
    .container {
        padding: 16px;
    }

    input[type="text"],
    select {
        max-width: 100%;
    }

    table th,
    table td {
        padding: 8px 10px;
        font-size: 13px;
    }
}*/

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




    <br />
<br />
<br />
<br />

<div class="container">
    
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Approval (HOD/CEO)</strong></span></h2>
            </div>
        </div>

        <br />





    

<div class="page">

    <%--<pm:Breadcrumb runat="server" ID="Breadcrumb" />--%>

    <div class="card">
        <%--<h2>Approval Queue</h2>--%>
        <asp:Label runat="server" ID="lblMsg" />
    </div>

    <div class="card">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true"
            CssClass="GridView" DataKeyNames="ReportID"
            OnRowCommand="gv_RowCommand">
            <Columns>
                <asp:BoundField DataField="ReportID" HeaderText="ReportID" />
                <asp:BoundField DataField="IndicatorName" HeaderText="Indicator" />
                <asp:BoundField DataField="DepartmentName" HeaderText="Dept" />
                <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:ButtonField CommandName="Open" Text="Open" />
            </Columns>
        </asp:GridView>
    </div>

     <%--Expanded Indicator Report Details--%> 
    <asp:Panel runat="server" ID="pnlDetails" CssClass="card" Visible="true">

        <h3>Indicator Details</h3>
        <table class="GridView">
            <tr><th>Indicator</th><td><asp:Label ID="lblIndicator" runat="server" /></td></tr>
            <tr><th>Indicator Type</th><td><asp:Label ID="lblIndicatorType" runat="server" /></td></tr>
            <tr><th>Priority</th><td><asp:Label ID="lblPriority" runat="server" /></td></tr>
            <tr><th>Programme</th><td><asp:Label ID="lblProgramme" runat="server" /></td></tr>
            <tr><th>Outcome</th><td><asp:Label ID="lblOutcome" runat="server" /></td></tr>
            <tr><th>Baseline</th><td><asp:Label ID="lblBaseline" runat="server" /></td></tr>
            <tr><th>Term Target</th><td><asp:Label ID="lblTermTarget" runat="server" /></td></tr>
            <tr><th>Annual Target</th><td><asp:Label ID="lblAnnualTarget" runat="server" /></td></tr>
            <tr><th>Quarterly Target</th><td><asp:Label ID="lblQuarterlyTarget" runat="server" /></td></tr>
        </table>

        <h3>Report Submission</h3>
        <table class="GridView">
            <tr><th>Actual Value</th><td><asp:Label ID="lblActual" runat="server" /></td></tr>
            <tr><th>Achieved</th><td><asp:Label ID="lblAchieved" runat="server" /></td></tr>
            <tr><th>Submitted By</th><td><asp:Label ID="lblSubmitter" runat="server" /></td></tr>
            <tr><th>Submitted Date</th><td><asp:Label ID="lblSubmittedDate" runat="server" /></td></tr>
            <tr><th>Deviation Reason</th><td><asp:Label ID="lblDeviation" runat="server" /></td></tr>
            <tr><th>Remedial Actions</th><td><asp:Label ID="lblRemedial" runat="server" /></td></tr>
            <tr><th>Remedial Due Date</th><td><asp:Label ID="lblDueDate" runat="server" /></td></tr>
        </table>

        <h3>Evidence Files (POE)</h3>
        <asp:GridView runat="server" ID="gvEvidence" AutoGenerateColumns="true" CssClass="GridView">
            <Columns>
                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                <asp:HyperLinkField HeaderText="Download"
                    DataNavigateUrlFields="EvidenceID"
                    DataNavigateUrlFormatString="~/Reporting/EvidenceDownload.ashx?id={0}" 
                    Text="Download" />
                <asp:HyperLinkField HeaderText="Preview"
                    DataNavigateUrlFields="EvidenceID"
                    DataNavigateUrlFormatString="~/Reporting/EvidencePreview.aspx?id={0}"
                    Text="Preview" />
                <asp:BoundField DataField="UploadedDate" HeaderText="Uploaded" 
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
        </asp:GridView>

        <h3>Approval Actions</h3>
        <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" CssClass="input" />

        <div class="btn-group" style="margin-top:10px;">
            <asp:Button runat="server" ID="btnApprove" CssClass="btn btn-success"
                Text="Approve" OnClick="btnApprove_Click" />
            <asp:Button runat="server" ID="btnReject" CssClass="btn btn-danger"
                Text="Reject" OnClick="btnReject_Click" />
        </div>

    </asp:Panel>

</div>





    </div>


</asp:Content>

