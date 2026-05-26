<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_QuarterlyTargetsUpload.aspx.cs" Inherits="i_QuarterlyTargetsUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

  
    .box { border:1px solid #ddd; padding:12px; margin:10px 0; }
    .links a { margin-right: 12px; }
    .ok { color:#0a6; } .err { color:#b00; } .warn { color:#a60; }

        :root {
            --bg: #f7f8fb;
            --surface: #ffffff;
            --border: #d9dee5;
            --text: #1f2937;
            --muted: #64748b;
            --primary: #0b5ed7;
            --primary-dark: #094db0;
            --success: #007a33;
            --danger: #b00020;
            --warning: #e39f00;
            --radius: 10px;
            --shadow: 0 2px 10px rgba(16, 24, 40, 0.08);
            --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        }

        body { background: var(--bg); font-family: var(--font); color: var(--text); }

        h2, h4 { margin: 0 0 12px 0; font-weight: 700; color: var(--text); letter-spacing: .2px; }
        h2 { font-size: 26px; }
        h4 { font-size: 18px; }

        /* Status classes */
        .ok { color: var(--success); }
        .err { color: var(--danger); }
        .warn { color: var(--warning); }

        /* Box container */
        .box {
            border: 1px solid var(--border);
            border-radius: var(--radius);
            padding: 16px;
            margin: 16px 0;
            background: var(--surface);
            box-shadow: var(--shadow);
        }

        /* Links section */
        .links a {
            display: inline-block;
            margin-right: 16px;
            margin-bottom: 8px;
            color: var(--primary);
            text-decoration: none;
            font-weight: 500;
            transition: color .15s ease;
        }
        .links a:hover { color: var(--primary-dark); text-decoration: underline; }

        /* Labels and inputs */
        asp\:label, label {
            display: inline-block;
            font-size: 13px;
            font-weight: 500;
            color: var(--muted);
            margin: 8px 0 4px;
        }

        select, asp\:dropdownlist,
        asp\:fileupload, input[type="file"] {
            display: inline-block;
            min-width: 220px;
            padding: 8px 12px;
            border: 1px solid var(--border);
            border-radius: 8px;
            background: var(--surface);
            color: var(--text);
            font-size: 14px;
            transition: border-color .15s ease, box-shadow .15s ease;
        }
        select:focus, asp\:dropdownlist:focus,
        input[type="file"]:focus {
            outline: none;
            border-color: var(--primary);
            box-shadow: 0 0 0 3px rgba(11,94,215,.15);
        }

        /* Buttons */
        asp\:button, input[type="submit"], button {
            display: inline-block;
            margin: 10px 8px 10px 0;
            padding: 9px 16px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 8px;
            border: 1px solid var(--primary);
            background: var(--primary);
            color: #fff;
            cursor: pointer;
            transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
        }
        asp\:button:hover, input[type="submit"]:hover, button:hover {
            background: var(--primary-dark);
            border-color: var(--primary-dark);
        }
        asp\:button:focus-visible, input[type="submit"]:focus-visible, button:focus-visible {
            outline: none;
            box-shadow: 0 0 0 3px rgba(11,94,215,.25);
        }

        /* Table styling for filters */
        .box table {
            width: 100%;
            border-collapse: collapse;
            margin: 10px 0;
        }
        .box table td {
            padding: 8px 12px;
            vertical-align: middle;
        }
        .box table td:first-child {
            width: 150px;
            font-weight: 500;
            color: var(--text);
        }

        /* Small text */
        small {
            display: block;
            margin-top: 8px;
            font-size: 12px;
            color: var(--muted);
            font-style: italic;
        }

        /* GridView preview */
        table, asp\:gridview {
            width: 100%;
            border-collapse: collapse;
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: var(--radius);
            overflow: hidden;
            box-shadow: var(--shadow);
            font-family: var(--font);
            margin: 16px 0;
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
            color: var(--text);
        }
        table tr:last-child td { border-bottom: none; }
        table tr:hover { background: #f8fafc; }

        /* Status label */
        #lblStatus {
            display: block;
            margin-top: 12px;
            padding: 10px 12px;
            font-size: 13px;
            border-radius: 8px;
            font-weight: 500;
        }

        /* Paragraph styling */
        .box p {
            margin: 0 0 12px 0;
            font-size: 14px;
            color: var(--text);
        }

        hr {
            border: none;
            border-top: 1px solid var(--border);
            margin: 20px 0;
        }

        /* Responsive */
        @media (max-width: 768px) {
            .box table td:first-child {
                width: 120px;
            }
            select, input[type="file"] {
                width: 100%;
                min-width: unset;
            }
            .links a {
                display: block;
                margin-right: 0;
            }
        }

        @media (max-width: 640px) {
            .box table {
                font-size: 12px;
            }
            .box table td {
                padding: 6px 8px;
            }
            table th, table td {
                padding: 8px 10px;
                font-size: 12px;
            }
            asp\:button, input[type="submit"], button {
                width: 100%;
                margin: 8px 0;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Quarterly Targets – Bulk Upload</strong></span></h2>
            </div>
        </div>
        <br /><br />

        <!-- Filters panel (above file upload) -->
      <%--  <div class="box" >
            <h4>Scope</h4>
            <table>
                <tr>
                    <td><asp:Label runat="server" Text="Department:" AssociatedControlID="ddlDepartment" /></td>
                    <td><asp:DropDownList runat="server" ID="ddlDepartment" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select Department --" Value="" />
                    </asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Label runat="server" Text="Entity:" AssociatedControlID="ddlEntity" /></td>
                    <td><asp:DropDownList runat="server" ID="ddlEntity" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select Entity --" Value="" />
                    </asp:DropDownList></td>
                </tr>
            </table>
            <small>Choose <b>either</b> a Department <b>or</b> an Entity for this upload.</small>
        </div>--%>


    <%--<div class="box links">
        <asp:HyperLink runat="server" ID="lnkPMTDP" Text="Download PMTDP Template" />
        <asp:HyperLink runat="server" ID="lnkPOA" Text="Download POA Template" />
        <asp:HyperLink runat="server" ID="lnkQuarterly" Text="Download Quarterly Target Template" />
    </div>--%>

    <div class="box">
        <p>Select your completed <b>Quarterly Targets</b> Excel file (.xlsx):</p>
        <asp:FileUpload runat="server" ID="fuExcel" />
        <asp:Button runat="server" ID="btnUploadPreview" Text="Upload &amp; Preview" OnClick="btnUploadPreview_Click" />
        <asp:Button runat="server" ID="btnConfirm" Text="Confirm Upload" OnClick="btnConfirm_Click" Visible="false" />
        <asp:Button runat="server" ID="btnErrorsCsv" Text="Download Errors (CSV)" OnClick="btnErrorsCsv_Click" Visible="false" />
        <br />
        <asp:Label runat="server" ID="lblStatus" />
    </div>

    <asp:GridView runat="server" ID="gvPreview" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Row" DataField="RowNo" />
            <asp:BoundField HeaderText="IndicatorName" DataField="IndicatorName" />
            <asp:BoundField HeaderText="FY" DataField="FinancialYear" />
            <asp:BoundField HeaderText="Quarter" DataField="Quarter" />
            <asp:BoundField HeaderText="Target" DataField="TargetValue" />
            <asp:BoundField HeaderText="Status" DataField="Status" />
            <asp:BoundField HeaderText="Message" DataField="Message" />
        </Columns>
    </asp:GridView>

    <asp:HiddenField runat="server" ID="hfPreviewKey" />




    </div>
    <br /><br />

</asp:Content>

