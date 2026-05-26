<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="approvalIntervention.aspx.cs" Inherits="approvalIntervention" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    <%--<link href="css/PlanningPage.css" rel="stylesheet" />--%>
    <style type="text/css">
        .accordionContent {
            color: black;
        }
        .accordionHeader {
            background-color: #03AC13;
            color: white;
            font-weight: 600;

            padding: 1px 35px;
            /*padding: 14px 22px;*/


            font-size: 16px;
            border: 1px solid #dcdcdc;
            border-bottom: none;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .accordionHeader:hover {
            background-color: white;
            color: #03AC13;
            /*border: 3px solid;*/
            border: 1px solid;
        }
            .accordionHeader:active {
                background-color: white;
                color: #03AC13;
                /*border: 3px solid;*/
                border: 1px solid;
            }

        .accordionHeaderSelected {
            background-color: white;
            color: #03AC13;
            /*border: 3px solid;*/
            border: 1px solid;
        }
        .accordionContent {
            background-color: #fefefe;
            padding: 18px 22px;
            font-size: 15px;
            color: #333;
            border: 1px solid #dcdcdc;
            border-top: none;
        }
        .acordionLink {
            text-decoration: none;
            color: inherit;
            display: block;
            width: 100%;
        }


















           /*.container {*/
    /*max-width: 1400px;*/
    /*max-width:100%;*/
    /*margin: auto;
    padding: 25px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    width:auto;
}*/

h1, h2, h3, h4, h5 {
    /*color: #03AC13;*/ /* Green heading color */
    /*color: #ffffff;*/

    margin-top: 20px;
    margin-bottom: 10px;
}

.section-header {
    border-bottom: 2px solid #03AC13;
    padding-bottom: 5px;
    margin-bottom: 15px;
}

.data-label {
    font-weight: bold;
    color: #555;
    display: inline-block;
    width: 200px;
}

.data-value {
    display: inline-block;
    margin-left: 10px;
}

.section {
    margin-bottom: 30px;
    padding: 20px;
    background-color: #fcfcfc;
    border-radius: 6px;
    border: 1px solid #e0e0e0;
}

.table-container {
    overflow-x: auto;
    margin-top: 15px;
}

.data-grid {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 15px;
}

.data-grid th, .data-grid td {
    border: 1px solid #ddd;
    padding: 10px;
    text-align: left;
    vertical-align: top;
}

.data-grid th {
    background-color: #e9e9e9;
    font-weight: bold;
    color: #333;
}

.data-grid tr:nth-child(even) {
    background-color: #f9f9f9;
}

.nested-grid {
    margin-top: 10px;
    border: 1px solid #b2e5c2; /* light green border */
    background-color: #e6ffe6; /* soft green background */
}

.nested-grid th {
    background-color: #b3f0b3 !important; /* light green header */
}

.message {
    color: #888;
    text-align: center;
    margin-top: 50px;
    font-size: 1.1em;
}

.filter-panel {
    margin-bottom: 25px;
    padding: 15px;
    background-color: #f0fff0; /* light green background */
    border: 1px solid #a3e6a3; /* green border */
    border-radius: 6px;
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    align-items: center;
}

.filter-panel label {
    margin-right: 5px;
    font-weight: bold;
    color: #03AC13;
}

.filter-panel-width {
    margin-bottom: 25px;
    padding: 20px;
    /*background-color: #f0fff0;*/ /* light green background */
    /*border: 1px solid #a3e6a3;*/ /* green border */
    border-radius: 6px;
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    align-items: center;
}

.asp-dropdown {
    padding: 8px;
    border-radius: 4px;
    border: 1px solid #ccc;
    min-width: 200px;
}

.asp-button {
    padding: 10px 20px;
    background-color: #03AC13;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1em;
}

.asp-button:hover {
    background-color: #028a10;
}

.action-links {
    margin-top: 10px;
    text-align: right;
}

.action-links a {
    margin-left: 15px;
    color: #03AC13;
    text-decoration: none;
    font-weight: bold;
}

.action-links a:hover {
    text-decoration: underline;
}


.data-grid td {
    text-align: center;
    vertical-align: middle;
}

.fadeInUp .color1{
    color:black;
}


.merged-cell {
    border-bottom: none;
}



.my-button{
     padding:10px 20px;
}







.Header-1 {
    background-color: white;
    color: #03AC13;
    /*border: 3px solid;*/
    border: 1px solid;
}



































.formal-grid {
    width: 100%;
    border-collapse: collapse;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-size: 14px;
    color: #333;
    border: 1px solid #ccc;
}

.grid-header {
    background-color: #2c3e50;
    color: white;
    font-weight: bold;
    text-align: left;
    padding: 8px;
}

.grid-row {
    background-color: #f9f9f9;
    padding: 8px;
}

.grid-alt-row {
    background-color: #eef2f7;
    padding: 8px;
}

.nested-grid {
    width: 95%;
    margin: 5px auto;
    border: 1px solid #ddd;
    font-size: 13px;
}




























        .tab-container { display: flex; }
        .tab-button {
            padding: 10px;
            cursor: pointer;
            background: #eee;
            border: 1px solid #ccc;
            margin-right: 5px;
        }
        .tab-button.active { background: #ddd; }
        .tab-content { display: none; padding: 10px; border: 1px solid #ccc; }
        .tab-content.active { display: block; }
        table { width: 100%; border-collapse: collapse; }
        th, td { border: 1px solid #ccc; padding: 8px; }
    

    

    </style>


    <br /><br /><br /><br /><br /><br /><br /><br /><br />


    <%--<asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" 
        OnRowCommand="gvInterventions_RowCommand" 
        OnRowDataBound="gvInterventions_RowDataBound" 
        DataKeyNames="InterventionID">
        <Columns>
            <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false"/>
            <asp:BoundField DataField="InterventionName" HeaderText="Name" />
            <asp:TemplateField HeaderText="Indicators">
                <ItemTemplate>
                    <asp:GridView ID="gvIndicators" runat="server" AutoGenerateColumns="True" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Budgets">
                <ItemTemplate>
                    <asp:GridView ID="gvBudgets" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="FY_ID" HeaderText="FY" />
                            <asp:BoundField DataField="AnnualBudget" HeaderText="Annual Budget" />
                            <asp:BoundField DataField="TermBudget" HeaderText="Term Budget" />                 
                     </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
            <asp:ButtonField ButtonType="Button" CommandName="Decline" Text="Decline" />
        </Columns>
    </asp:GridView>--%>







    <div class="row section-title text-center">
        <br />
        <div class="row">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Interventions Approval</strong></span></h2>
        </div>
    </div>























    <div style="max-width: 700px; margin: auto;">

        <%--<asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False"
            OnRowCommand="gvInterventions_RowCommand"
            OnRowDataBound="gvInterventions_RowDataBound"
            DataKeyNames="InterventionID">
            <Columns>
                <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="InterventionName" HeaderText="Name" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="300px"/>
                <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="500px">
                    <ItemStyle/>
                    <ItemTemplate>
                        <div style="margin-bottom: 10px; ">
                            <strong>Indicators:</strong>
                            <asp:GridView ID="gvIndicators" runat="server" AutoGenerateColumns="True" CssClass="nested-grid" />
                        </div>
                        <div>
                            <strong>Budgets:</strong>
                            <asp:GridView ID="gvBudgets" runat="server" AutoGenerateColumns="False" CssClass="nested-grid">
                                <Columns>
                                    <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="FY_ID" HeaderText="FY" />
                                    <asp:BoundField DataField="AnnualBudget" HeaderText="Annual Budget" />
                                    <asp:BoundField DataField="TermBudget" HeaderText="Term Budget" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
                <asp:ButtonField ButtonType="Button" CommandName="Decline" Text="Decline" />
            </Columns>
        </asp:GridView>--%>

    </div>

    <br /><br />

    <div style="max-width: 1125px; margin: auto;">

        <div class="tab-container">
            <div id="btn-tab1" class="tab-button active" onclick="showTab('tab1')">New</div>
            <div id="btn-tab2" class="tab-button" onclick="showTab('tab2')">Approved</div>
            <div id="btn-tab3" class="tab-button" onclick="showTab('tab3')">Declined</div>
        </div>

        <div id="tab1" class="tab-content active">
            New Content
            <br /><br />
            <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
                hello
            </asp:GridView>--%>



<!--
Pseudocode / Plan:
1. Locate both GridView controls: gvInterventions and gvApprovedInterventions.
2. Replace the BoundField showing InterventionName with a TemplateField.
3. Inside TemplateField add an asp:HyperLink.
4. Set Text to Eval("InterventionName").
5. Set NavigateUrl to "pageInterventionsDirectDetail.aspx?id=" + InterventionID via Eval.
6. Keep existing header CSS classes and width.
7. Do not alter other columns or events.
-->

<!-- Updated gvInterventions -->
<asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False"
    OnRowCommand="gvInterventions_RowCommand"
    OnRowDataBound="gvInterventions_RowDataBound"
    DataKeyNames="InterventionID" Style="/*max-width: 700px*/ width:100%">
    <Columns>
        <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="300px">
            <ItemTemplate>
                <asp:HyperLink ID="lnkIntervention" runat="server"
                    Text='<%# Eval("InterventionName") %>'
                    NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="500px">
            <ItemTemplate>
                <div style="margin-bottom: 10px;">
                    <strong>Indicators:</strong>
                    <asp:GridView ID="gvIndicators" runat="server" AutoGenerateColumns="False" CssClass="nested-grid">
                        <Columns>
                            <asp:BoundField DataField="IndicatorID" HeaderText="IndicatorID" Visible="false" />
                            <asp:BoundField DataField="InterventionID" HeaderText="InterventionID" Visible="false" />
                            <asp:BoundField DataField="IndicatorName" HeaderText="Indicator Name" />
                            <asp:BoundField DataField="IndicatorType" HeaderText="Indicator Type" />
                            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" />
                            <asp:BoundField DataField="BaselineValue" HeaderText="Baseline Value" />
                            <asp:BoundField DataField="BaselineYear" HeaderText="Baseline Year" />
                            <asp:BoundField DataField="SubOutcomeID" HeaderText="SubOutcomeID" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <strong>Budgets:</strong>
                    <asp:GridView ID="gvBudgets" runat="server" AutoGenerateColumns="False" CssClass="nested-grid">
                        <Columns>
                            <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="FY_ID" HeaderText="FY" />
                            <asp:BoundField DataField="AnnualBudget" HeaderText="Annual Budget" />
                            <asp:BoundField DataField="TermBudget" HeaderText="Term Budget" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
        <asp:ButtonField ButtonType="Button" CommandName="Decline" Text="Decline" />
    </Columns>
</asp:GridView>



            <br /><br />






        </div>
        <div id="tab2" class="tab-content">
           <%-- <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true"></asp:GridView>--%>
            Approved Content


            <br /><br />
            
            <!-- Updated gvApprovedInterventions -->
<asp:GridView ID="gvApprovedInterventions" runat="server" AutoGenerateColumns="False"
    OnRowCommand="gvApprovedInterventions_RowCommand"
    OnRowDataBound="gvApprovedInterventions_RowDataBound"
    DataKeyNames="InterventionID" Style="/*max-width: 700px*/ width:100%">
    <Columns>
        <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="300px">
            <ItemTemplate>
                <asp:HyperLink ID="lnkInterventionApproved" runat="server"
                    Text='<%# Eval("InterventionName") %>'
                    NavigateUrl='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="Header-1" HeaderStyle-Width="500px">
            <ItemTemplate>
                <div style="margin-bottom: 10px;">
                    <strong>Indicators:</strong>
                    <asp:GridView ID="gvApprovedIndicators" runat="server" AutoGenerateColumns="False" CssClass="nested-grid">
                        <Columns>
                            <asp:BoundField DataField="IndicatorID" HeaderText="IndicatorID" Visible="false" />
                            <asp:BoundField DataField="InterventionID" HeaderText="InterventionID" Visible="false" />
                            <asp:BoundField DataField="IndicatorName" HeaderText="Indicator Name" />
                            <asp:BoundField DataField="IndicatorType" HeaderText="Indicator Type" />
                            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" />
                            <asp:BoundField DataField="BaselineValue" HeaderText="Baseline Value" />
                            <asp:BoundField DataField="BaselineYear" HeaderText="Baseline Year" />
                            <asp:BoundField DataField="SubOutcomeID" HeaderText="SubOutcomeID" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <strong>Budgets:</strong>
                    <asp:GridView ID="gvApprovedBudgets" runat="server" AutoGenerateColumns="False" CssClass="nested-grid">
                        <Columns>
                            <asp:BoundField DataField="InterventionID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="FY_ID" HeaderText="FY" />
                            <asp:BoundField DataField="AnnualBudget" HeaderText="Annual Budget" />
                            <asp:BoundField DataField="TermBudget" HeaderText="Term Budget" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
        <asp:ButtonField ButtonType="Button" CommandName="Decline" Text="Decline" />
    </Columns>
</asp:GridView>

            <br />














        </div>
        <div id="tab3" class="tab-content">
            Declined Content
        </div>
    </div>



    <br /><br />











    <script>
        function showTab(tabId) {
            document.querySelectorAll('.tab-content').forEach(el => el.classList.remove('active'));
            document.querySelectorAll('.tab-button').forEach(el => el.classList.remove('active'));
            document.getElementById(tabId).classList.add('active');
            document.getElementById('btn-' + tabId).classList.add('active');
        }
    </script>


</asp:Content>

