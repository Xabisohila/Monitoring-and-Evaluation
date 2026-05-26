<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_POAUpload.aspx.cs" Inherits="i_POAUpload" %>

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
            --danger: #dc3545;
            --radius: 10px;
            --shadow: 0 2px 10px rgba(16, 24, 40, 0.08);
            --font: "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        }

        body { background: var(--bg); font-family: var(--font); color: var(--text); }

        h2 { margin: 0 0 12px 0; font-size: 26px; font-weight: 700; color: var(--text); letter-spacing: .2px; }

        /* Labels and inputs */
        asp\:label, label { display: inline-block; font-size: 13px; color: var(--muted); margin: 8px 0 4px; }
        select, asp\:dropdownlist,
        asp\:fileupload, input[type="file"] {
            display: inline-block;
            min-width: 180px;
            padding: 8px 10px;
            border: 1px solid var(--border);
            border-radius: 8px;
            background: var(--surface);
            color: var(--text);
            transition: border-color .15s ease, box-shadow .15s ease;
        }
        select:focus, asp\:dropdownlist:focus,
        input[type="file"]:focus {
            outline: none; border-color: var(--primary); box-shadow: 0 0 0 3px rgba(11,94,215,.15);
        }

        /* Buttons */
        asp\:button, input[type="submit"], button {
            display: inline-block;
            margin-top: 10px;
            padding: 9px 14px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 8px;
            border: 1px solid var(--primary);
            background: var(--primary);
            color: #fff;
            cursor: pointer;
            transition: background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
        }
        asp\:button:hover, input[type="submit"]:hover, button:hover { background: var(--primary-dark); border-color: var(--primary-dark); }
        asp\:button:focus-visible, input[type="submit"]:focus-visible, button:focus-visible { outline: none; box-shadow: 0 0 0 3px rgba(11,94,215,.25); }

        hr { border: none; border-top: 1px solid var(--border); margin: 16px 0; }

        /* GridView preview */
        table, asp\:gridview {
            width: 100%;
            border-collapse: collapse;
            background: var(--surface);
            border: 1px solid var(--border);
            border-radius: 10px;
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
        table th { background: #f1f5f9; font-weight: 600; }
        table tr:last-child td { border-bottom: none; }

        /* Messages */
        #lblMsg { display: block; margin-top: 10px; font-size: 13px; color: var(--muted); }

        /* Responsive */
        @media (max-width: 640px) {
            select, input[type="file"] { width: 100%; }
            table th, table td { padding: 8px 10px; font-size: 12px; }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">





        <br /><br /><br /><br />
<div class="container">
    <div class="section-title text-center">
        <br />
        <div>
            <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>POA Upload</strong></span></h2>
        </div>
    </div>
    <br /><br />





    <%--<h2>POA Upload (Annual & Quarterly Targets)</h2>--%>
    <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
    <asp:DropDownList runat="server" ID="ddlFY" />
    <br /><br />
    <asp:FileUpload runat="server" ID="fuPOA" />
    <asp:Button runat="server" ID="btnUpload" Text="Upload & Preview" OnClick="btnUpload_Click" />
    <hr />
    <asp:GridView runat="server" ID="gvPreview" AutoGenerateColumns="true" />
    <br />
    <asp:Button runat="server" ID="btnCommit" Text="Commit and Save" OnClick="btnCommit_Click" Visible="false" />
    <asp:Label runat="server" ID="lblMsg" />


    <br />

    <asp:Button runat="server" ID="btnDownloadPOA" Text="Download POA Template" OnClick="btnDownloadPOA_Click" />



        </div>
<br /><br />









</asp:Content>

