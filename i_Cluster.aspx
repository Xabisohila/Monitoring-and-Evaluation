<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_Cluster.aspx.cs" Inherits="i_Cluster" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
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
        hr { border: none; border-top: 1px solid var(--border); margin: 18px 0; }
        table, asp\:gridview { width: 100%; border-collapse: collapse; background: var(--surface); border: 1px solid var(--border); border-radius: 10px; overflow: hidden; box-shadow: var(--shadow); font-family: var(--font); }
        table th, table td { padding: 10px 12px; font-size: 13px; color: var(--text); border-bottom: 1px solid var(--border); text-align: left; vertical-align: top; }
        table th { background: #f1f5f9; font-weight: 600; }
        table tr:last-child td { border-bottom: none; }
        @media (/*max-width: 640px*/) { .container { padding: 12px; } asp\:textbox, input[type="text"], textarea, select { /*max-width: 100%;*/ } table th, table td { padding: 8px 10px; font-size: 12px; } }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
    <div class="container">
        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Provincial Clusters</strong></span></h2>
            </div>
        </div>
        <br /><br />

        <!-- Search Box -->
        <div class="search-box">
            <input type="text" id="searchInput" placeholder="Search cluster..." onkeyup="filterTable()" style="width:400px"/>
        </div>

        <asp:ValidationSummary runat="server" ID="valSummary" ForeColor="Red"/>
        <asp:HiddenField runat="server" ID="hfID" />

        <div>
            <asp:Label runat="server" Text="Cluster Name:" AssociatedControlID="txtName" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="Cluster name is required" ForeColor="Red"/>
            <asp:TextBox runat="server" ID="txtName" Width="400" MaxLength="100" /> 
        </div>

        <div>
            <asp:Label runat="server" Text="Cluster Description:" AssociatedControlID="txtDescription" />
            <asp:TextBox runat="server" ID="txtDescription" Width="400" MaxLength="500" TextMode="MultiLine" Rows="3" /> 
        </div>

        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
        <hr />

        <asp:GridView
            runat="server"
            ID="gv"
            AutoGenerateColumns="false"
            DataKeyNames="ClusterID"
            AllowSorting="true"
            OnSorting="gv_Sorting"
            OnRowCommand="gv_RowCommand"
            AllowPaging="true"
            PageSize="10"
            OnPageIndexChanging="gv_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ClusterID" HeaderText="ID" Visible="false" SortExpression="ClusterID" />
                <asp:BoundField DataField="ClusterName" HeaderText="Cluster Name" SortExpression="ClusterName" />
                <asp:BoundField DataField="ClusterDescription" HeaderText="Description" SortExpression="ClusterDescription" />
                <asp:ButtonField CommandName="EditRow" Text="Edit" />
                <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
            </Columns>
        </asp:GridView>
    </div>
    <br /><br />

    <script type="text/javascript">
        function filterTable() {
            var input = document.getElementById('searchInput');
            var filter = input.value.toLowerCase();
            var table = document.getElementById('<%= gv.ClientID %>');
            var tr = table.getElementsByTagName('tr');

            for (var i = 1; i < tr.length; i++) {
                var td = tr[i].getElementsByTagName('td');
                var found = false;

                for (var j = 0; j < td.length - 1; j++) {
                    if (td[j]) {
                        var txtValue = td[j].textContent || td[j].innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            found = true;
                            break;
                        }
                    }
                }

                tr[i].style.display = found ? '' : 'none';
            }
        }
    </script>
</asp:Content>





































