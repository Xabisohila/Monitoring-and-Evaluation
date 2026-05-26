<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_MonitoringDashboard.aspx.cs" Inherits="i_MonitoringDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



    <style type="text/css">
        :root {
    --bg: #f6f7fb;
    --surface: #ffffff;
    --border: #d9dee5;
    --text: #1f2937;
    --muted: #64748b;
    --primary: #0b5ed7;
    --primary-dark: #094db0;
    --danger: #dc3545;
    --success: #16a34a;
    --warning: #f59e0b;
    --radius: 10px;
    --shadow: 0 2px 10px rgba(16, 24, 40, 0.08);
    --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
}

/* Page layout */
.page {
    max-width: 1200px;
    margin: 0 auto;
    padding: 24px;
    background: var(--bg);
    font-family: var(--font);
    color: var(--text);
}

/* Cards */
.card {
    background: var(--surface);
    border: 1px solid var(--border);
    border-radius: var(--radius);
    box-shadow: var(--shadow);
    padding: 20px;
    margin-bottom: 18px;
}

.card h2, .card h3, .card h4 {
    margin: 0 0 12px 0;
    font-weight: 700;
    letter-spacing: 0.2px;
}
.card h2 { font-size: 26px; }
.card h3 { font-size: 20px; }
.card h4 { font-size: 16px; }

/* Buttons + inputs group */
.btn-group {
    display: flex;
    flex-wrap: wrap;
    gap: 10px 12px;
    align-items: center;
}

.btn, .btn.btn-outline,
input[type="submit"], button {
    display: inline-block;
    padding: 9px 14px;
    font-size: 14px;
    font-weight: 600;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
    border: 1px solid var(--primary);
    background: var(--primary);
    color: #fff;
}
.btn:hover,
input[type="submit"]:hover,
button:hover { background: var(--primary-dark); border-color: var(--primary-dark); }
.btn:focus-visible,
input[type="submit"]:focus-visible,
button:focus-visible { outline: none; box-shadow: 0 0 0 3px rgba(11,94,215,.25); }

.btn.btn-outline {
    background: transparent;
    color: var(--primary);
    border-color: var(--primary);
}
.btn.btn-outline:hover {
    background: rgba(11,94,215,.08);
}

/* Labels and inputs */
label, asp\:label {
    font-size: 13px;
    color: var(--muted);
}
select, input[type="text"] {
    min-width: 160px;
    padding: 8px 10px;
    border: 1px solid var(--border);
    border-radius: 6px;
    background: var(--surface);
    color: var(--text);
    transition: border-color .15s ease, box-shadow .15s ease;
}
select:focus, input[type="text"]:focus {
    outline: none;
    border-color: var(--primary);
    box-shadow: 0 0 0 3px rgba(11,94,215,.15);
}

/* KPI row */
.kpi-row {
    display: grid;
    gap: 12px;
    grid-template-columns: repeat(5, 1fr);
}
.kpi-tile {
    position: relative;
    border-radius: 12px;
    padding: 14px;
    color: #fff;
    background: linear-gradient(135deg, #0b5ed7, #3b82f6);
    box-shadow: var(--shadow);
}
.kpi-tile .label {
    font-size: 12px;
    opacity: .9;
}
.kpi-tile .value {
    margin-top: 6px;
    font-size: 22px;
    font-weight: 700;
}
.kpi-tile .spark {
    position: absolute;
    right: 8px;
    bottom: 8px;
    width: 48px;
    height: 16px;
    opacity: .35;
    background: radial-gradient(circle at 10% 0%, rgba(255,255,255,.6), transparent 60%);
}

/* WG tiles */
.tile {
    border-radius: 10px;
    padding: 14px;
    color: #fff;
    box-shadow: var(--shadow);
}
.tile .label {
    font-size: 13px;
    opacity: .9;
}
.tile .kpi {
    margin-top: 6px;
    font-size: 22px;
    font-weight: 700;
}
.tile .progress {
    height: 8px;
    border-radius: 6px;
    background: rgba(255,255,255,.25);
    overflow: hidden;
}
.tile .progress span {
    display: block;
    height: 100%;
    background: rgba(255,255,255,.9);
    border-radius: 6px;
}

/* Alternating WG backgrounds via Css class */
.bg1 { background: linear-gradient(135deg, #2e7d32, #22c55e); }
.bg2 { background: linear-gradient(135deg, #7c2d12, #f97316); }
.bg3 { background: linear-gradient(135deg, #1d4ed8, #60a5fa); }
.bg4 { background: linear-gradient(135deg, #6b21a8, #a78bfa); }

/* Panels (three lists) */
.panel {
    border: 1px solid var(--border);
    border-radius: var(--radius);
    padding: 12px;
    background: var(--surface);
    box-shadow: var(--shadow);
}

/* GridView baseline */
table, asp\:gridview {
    width: 100%;
    border-collapse: collapse;
    background: var(--surface);
    border: 1px solid var(--border);
    border-radius: 8px;
    overflow: hidden;
    box-shadow: var(--shadow);
    font-family: var(--font);
}
table th, table td {
    padding: 10px 12px;
    font-size: 13px;
    color: var(--text);
    border-bottom: 1px solid var(--border);
    text-align: left;
}
table th {
    background: #f1f5f9;
    font-weight: 600;
}
table tr:last-child td { border-bottom: none; }

/* Notes */
.map-note {
    display: block;
    margin-top: 8px;
    font-size: 12px;
    color: var(--muted);
}

/* Utilities */
.fade-in { animation: fadeIn .25s ease; }
@keyframes fadeIn { from { opacity: 0; transform: translateY(4px); } to { opacity: 1; transform: none; } }

/* Responsive */
@media (max-width: 1024px) {
    .kpi-row { grid-template-columns: repeat(3, 1fr); }
}
@media (max-width: 640px) {
    .page { padding: 16px; }
    .kpi-row { grid-template-columns: repeat(2, 1fr); }
    .btn-group { gap: 8px; }
    select, input[type="text"] { min-width: 120px; width: 100%; }
}
    </style>









</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <br /><br /><br /><br>




<%--    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonitoringDashboard.aspx.cs" Inherits="Dashboard_MonitoringDashboard" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>PME Monitoring Dashboard</title>
    <!-- Uses your base + dashboard styles -->
    ~/Content/app.css
    ~/Content/dashboard.css
</head>
<body>
<form id="form1" runat="server">--%>
    <div class="page">
        <div class="card">
            <h2>Monitoring Dashboard</h2>
            <div class="btn-group" style="margin-bottom:12px;">
                <label for="ddlFY">Financial Year</label>
                <asp:DropDownList runat="server" ID="ddlFY" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged" />
                <label for="ddlQuarter" style="margin-left:12px;">Quarter</label>
                <asp:DropDownList runat="server" ID="ddlQuarter" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged">
                    <asp:ListItem Text="Q1" Value="1" />
                    <asp:ListItem Text="Q2" Value="2" />
                    <asp:ListItem Text="Q3" Value="3" />
                    <asp:ListItem Text="Q4" Value="4" />
                </asp:DropDownList>

                <!-- Optional cluster/WG/Dept filters (enable if you want to scope the tiles & lists) -->
                <asp:Label runat="server" Text="Working Group" AssociatedControlID="ddlWG" style="margin-left:12px;" />
                <asp:DropDownList runat="server" ID="ddlWG" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged" />

                <asp:Label runat="server" Text="Department" AssociatedControlID="ddlDept" style="margin-left:12px;" />
                <asp:DropDownList runat="server" ID="ddlDept" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged" />

                <asp:Button runat="server" ID="btnRefresh" CssClass="btn" Text="Refresh" OnClick="btnRefresh_Click" />
                <asp:Button runat="server" ID="btnExportCsv" CssClass="btn btn-outline" Text="Export CSV" OnClick="btnExportCsv_Click" />
            </div>

            <!-- KPI Tiles -->
            <div class="kpi-row">
                <div class="kpi-tile fade-in">
                    <div class="label">Achieved</div>
                    <div class="value"><asp:Label runat="server" ID="lblAchieved" /></div>
                    <div class="spark"></div>
                </div>
                <div class="kpi-tile fade-in" style="background:linear-gradient(135deg,#e11d48,#f97316);">
                    <div class="label">Not Achieved</div>
                    <div class="value"><asp:Label runat="server" ID="lblNotAchieved" /></div>
                    <div class="spark"></div>
                </div>
                <div class="kpi-tile fade-in" style="background:linear-gradient(135deg,#0f766e,#10b981);">
                    <div class="label">Total Reported</div>
                    <div class="value"><asp:Label runat="server" ID="lblTotal" /></div>
                    <div class="spark"></div>
                </div>
                <div class="kpi-tile fade-in" style="background:linear-gradient(135deg,#ca8a04,#facc15);">
                    <div class="label">Non‑Compliance</div>
                    <div class="value"><asp:Label runat="server" ID="lblNCCount" /></div>
                    <div class="spark"></div>
                </div>
                <div class="kpi-tile fade-in" style="background:linear-gradient(135deg,#334155,#64748b);">
                    <div class="label">Awaiting QA / Approval / Sign‑off</div>
                    <div class="value"><asp:Label runat="server" ID="lblAwaitingTriple" /></div>
                    <div class="spark"></div>
                </div>
            </div>
        </div>

        <!-- Working Group Performance (tiles with % achieved) -->
        <div class="card">
            <h3>Working Group Performance (FY/Q scope)</h3>
            <asp:Repeater runat="server" ID="repWG">
                <ItemTemplate>
                    <div class='tile <%# Eval("Css") %>'>
                        <div class="label"><%# Eval("WorkingGroupName") %></div>
                        <div class="kpi"><%# String.Format("{0:P0}", Eval("PercentAchieved")) %></div>
                        <div class="progress" style="margin-top:10px;">
                            <span style='width:<%# Eval("BarPercent", "{0}%") %>'></span>
                        </div>
                        <div style="margin-top:6px; font-size:12px;">
                            Achieved: <%# Eval("Achieved") %> / <%# Eval("TotalReported") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label runat="server" ID="lblWGNote" CssClass="map-note" />
        </div>

        <!-- Three quick tables -->
        <div class="card">
            <h3>Signals (Top Lists)</h3>
            <div class="btn-group" style="margin-bottom:10px">
                <asp:HyperLink runat="server" NavigateUrl="~/Reporting/NonCompliance.aspx" CssClass="btn btn-outline" Text="Open Non‑Compliance" />
                <asp:HyperLink runat="server" NavigateUrl="~/Workflow/AwaitingQA.aspx" CssClass="btn btn-outline" Text="Open Awaiting QA" />
                <asp:HyperLink runat="server" NavigateUrl="~/Workflow/AwaitingApproval.aspx" CssClass="btn btn-outline" Text="Open Awaiting Approval" />
                <asp:HyperLink runat="server" NavigateUrl="~/Workflow/AwaitingSignoff.aspx" CssClass="btn btn-outline" Text="Open Awaiting Sign‑off" />
                <asp:HyperLink runat="server" NavigateUrl="~/Reporting/ImprovementPlan.aspx" CssClass="btn btn-outline" Text="Open Improvement Plan" />
            </div>

            <div style="display:grid; gap:16px; grid-template-columns: 1fr 1fr 1fr;">
                <!-- Non-Compliance (Top 8) -->
                <div class="panel">
                    <h4>Non‑Compliance (Top)</h4>
                    <asp:GridView runat="server" ID="gvNC" AutoGenerateColumns="true" />
                </div>

                <!-- Awaiting combined (Top 8 by latest submit date) -->
                <div class="panel">
                    <h4>Awaiting (QA / Approval / Sign‑off)</h4>
                    <asp:GridView runat="server" ID="gvAwaiting" AutoGenerateColumns="true" />
                </div>

                <!-- Under-Achieved → Improvement plan (Top 8) -->
                <div class="panel">
                    <h4>Under‑Achieved (Improvement Plan)</h4>
                    <asp:GridView runat="server" ID="gvImprove" AutoGenerateColumns="true" />
                </div>
            </div>
        </div>
    </div>













</asp:Content>

