<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="Other.aspx.cs" Inherits="Other" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



    <meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Planning Overview by Filters</title>
<style type="text/css">
    /* Basic inline CSS for readability. Consider moving this to an external.css file. */
    /*body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; color: #333; }*/
   /*.container { max-width: 1400px; margin: auto; padding: 25px;*/ /*background-color: #fff;*/ /*border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
    h1, h2, h3, h4, h5 { color: #0056b3; margin-top: 20px; margin-bottom: 10px; }
   .section-header { border-bottom: 2px solid #0056b3; padding-bottom: 5px; margin-bottom: 15px; }
   .data-label { font-weight: bold; color: #555; display: inline-block; width: 200px; }
   .data-value { display: inline-block; margin-left: 10px; }
   .section { margin-bottom: 30px; padding: 20px; background-color: #fcfcfc; border-radius: 6px; border: 1px solid #e0e0e0; }
   .table-container { overflow-x: auto; margin-top: 15px; }
   .data-grid { width: 100%; border-collapse: collapse; margin-bottom: 15px; }
   .data-grid th,.data-grid td { border: 1px solid #ddd; padding: 10px; text-align: left; vertical-align: top; }
   .data-grid th { background-color: #e9e9e9; font-weight: bold; color: #333; }
   .data-grid tr:nth-child(even) { background-color: #f9f9f9; }
   .nested-grid { margin-top: 10px; border: 1px solid #cceeff; background-color: #e6f7ff; }
   .nested-grid th { background-color: #b3e0ff!important; }
   .message { color: #888; text-align: center; margin-top: 50px; font-size: 1.1em; }
   .filter-panel { margin-bottom: 25px; padding: 15px; background-color: #f0f8ff; border: 1px solid #aaddff; border-radius: 6px; display: flex; flex-wrap: wrap; gap: 20px; align-items: center; }
   .filter-panel label { margin-right: 5px; font-weight: bold; color: #0056b3; }
   .asp-dropdown { padding: 8px; border-radius: 4px; border: 1px solid #ccc; min-width: 200px; }
   .asp-button { padding: 10px 20px; background-color: #007bff; color: white; border: none; border-radius: 5px; cursor: pointer; font-size: 1em; }
   .asp-button:hover { background-color: #0056b3; }
   .action-links { margin-top: 10px; text-align: right; }
   .action-links a { margin-left: 15px; color: #007bff; text-decoration: none; font-weight: bold; }
   .action-links a:hover { text-decoration: underline; }*/

   .container {
    /*max-width: 1400px;*/
    max-width:100%;
    margin: auto;
    padding: 25px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    width:auto;
}

h1, h2, h3, h4, h5 {
    color: #03AC13; /* Green heading color */
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

</style>






</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css
    https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js

    <br /><br /><br /><br /><br /><br />




    <div class="row section-title text-center">


    <br />
    <div class="row">
        <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Planning</strong></span></h2>

    </div>

</div>




    <div class="container">

        <div class="filter-panel" runat="server" visible="true">
            <div>
                <asp:Label ID="lblWorkGroup" runat="server" Text="Work Group:"></asp:Label>
                <asp:DropDownList ID="ddlWorkGroups" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Work Group --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="lblPMTDPPriority" runat="server" Text="PMTDP Priority:"></asp:Label>
                <asp:DropDownList ID="ddlPMTDPPriorities" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Priority --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="lblFinancialYear" runat="server" Text="Financial Year:"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYears" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Text="-- Select Year --" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <%--<asp:Button ID="btnViewOverview" runat="server" Text="View Planning Overview" OnClick="btnViewOverview1_Click" CssClass="asp-button" />--%>
        </div>

<%--       <asp:Repeater ID="rptSubOutcomes" runat="server" OnItemDataBound="rptSubOutcomes_ItemDataBound">
    <ItemTemplate>
        <div class="accordion-item">
            <h2 class="accordion-header" id='<%# "heading" + Eval("SubOutcomeId") %>'>
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                        data-bs-target='<%# "#collapse" + Eval("SubOutcomeId") %>'
                        aria-expanded="false" aria-controls='<%# "collapse" + Eval("SubOutcomeId") %>'>
                    <%# Eval("SubOutcome") %>
                </button>
            </h2>
            <div id='<%# "collapse" + Eval("SubOutcomeId") %>' class="accordion-collapse collapse"
                 aria-labelledby='<%# "heading" + Eval("SubOutcomeId") %>' data-bs-parent="#accordionSubOutcomes">
                <div class="accordion-body">
                    <asp:GridView ID="gvInterventionsBySubOutcome" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Intervention Name">
                                <ItemTemplate>
                                    <a href='<%# "pageInterventionsDirectDetail.aspx?id=" + Eval("InterventionID") %>'>
                                        <%# Eval("InterventionName") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="InterventionDescription" HeaderText="Description" />
                            <asp:BoundField DataField="ImplementationInstitution" HeaderText="Institution" />
                            <asp:BoundField DataField="PrimaryMunicipality" HeaderText="Municipality" />
                            <asp:BoundField DataField="SpatialReference" HeaderText="Spatial Ref." />
                            <asp:BoundField DataField="InterventionStartYear" HeaderText="Start Year" />
                            <asp:BoundField DataField="InterventionEndYear" HeaderText="End Year" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>

            <!-- Existing POA and Intervention Layout -->
            <%--<asp:Repeater ID="rptPOAs" runat="server" OnItemDataBound="rptPOAs_ItemDataBound">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-header">
                            <strong>POA:</strong> <%--<%# Eval("ProgrammeOfAction") %>- -%>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="InterventionName" HeaderText="Intervention Name" />
                                    <asp:BoundField DataField="InterventionDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="ImplementationInstitution" HeaderText="Institution" />
                                    <asp:BoundField DataField="PrimaryMunicipality" HeaderText="Municipality" />
                                    <asp:BoundField DataField="SpatialReference" HeaderText="Spatial Ref." />
                                    <asp:BoundField DataField="InterventionStartYear" HeaderText="Start Year" />
                                    <asp:BoundField DataField="InterventionEndYear" HeaderText="End Year" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>--%>
        </asp:Panel>
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="Please select filters and click 'View Planning Overview' to see details."></asp:Label>
    </div>
</asp:Content>

