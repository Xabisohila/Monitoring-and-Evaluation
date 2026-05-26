<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="new_PU_View.aspx.cs" Inherits="new_PU_View" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
    body {
        font-family: 'Segoe UI';
        background: #f4f6f9;
        margin: 0;
    }

    .container {
        /*width: 95%;*/
        margin: auto;
        /*padding: 20px;*/
    }

    .title {
        font-size: 26px;
        font-weight: bold;
        color: #2c3e50;
        margin-bottom: 20px;
    }

    .filters {
        background: white;
        padding: 15px;
        box-shadow: 0px 2px 6px rgba(0,0,0,0.1);
        border-radius: 6px;
        margin-bottom: 20px;
    }

    .filters label {
        font-weight: 600;
        margin-right: 10px;
    }

    select {
        padding: 6px;
        margin-right: 20px;
        border-radius: 4px;
        border: 1px solid #ccc;
    }

    .accordionHeader {
        background: #0078d4;
        color: white;
        padding: 10px;
        font-weight: bold;
        cursor: pointer;
    }

    .accordionContent {
        background: #ffffff;
        padding: 10px;
        border: 1px solid #ddd;
    }

    .grid {
        width: 100%;
        border-collapse: collapse;
    }

    .grid th {
        background: #2c3e50;
        color: white;
        padding: 8px;
    }

    .grid td {
        padding: 8px;
        border-bottom: 1px solid #ddd;
    }

    .grid tr:hover {
        background: #f1f1f1;
    }

    .linkBtn {
        color: #0078d4;
        text-decoration: underline;
        cursor: pointer;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br /><br /><br /><br />

<asp:ScriptManager ID="ScriptManager1" runat="server" />

<div class="container">

    <div class="title">Sector Performance Dashboard</div>

    <!-- FILTERS -->
    <div class="filters">
        <label>Sector:</label>
        <asp:DropDownList ID="ddlSector" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" />

        <label>Working Group:</label>
        <asp:DropDownList ID="ddlWG" runat="server" />

        <label>Priority:</label>
        <asp:DropDownList ID="ddlPriority" runat="server" />
    </div>

    <!-- ACCORDION -->
    <ajaxToolkit:Accordion ID="Accordion1" runat="server"
        HeaderCssClass="accordionHeader"
        ContentCssClass="accordionContent"
        FadeTransitions="true"
        SelectedIndex="0">

        <Panes>

            <ajaxToolkit:AccordionPane ID="pane1" runat="server">
                <Header>
                    <asp:Label ID="lblFocusArea1" runat="server" />
                </Header>

                <Content>
                    <asp:GridView ID="gvIntervention1" runat="server"
                        CssClass="grid"
                        AutoGenerateColumns="false"
                        OnRowCommand="gv_RowCommand">

                        <Columns>

                            <asp:BoundField DataField="InterventionName" HeaderText="Intervention" />

                            <asp:TemplateField HeaderText="Indicator">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIndicator"
                                        runat="server"
                                        CssClass="linkBtn"
                                        Text='<%# Eval("KeyIndicator") %>'
                                        CommandName="ViewIndicator"
                                        CommandArgument='<%# Eval("KeyIndicator") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </Content>
            </ajaxToolkit:AccordionPane>

        </Panes>

    </ajaxToolkit:Accordion>

</div>

</asp:Content>









