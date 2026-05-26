<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="testMonitoring.aspx.cs" Inherits="POA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    


    <div class="padding100">
        <div class="col-md-12">
            <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Programme of Action</strong> </span></h2>
        </div>
    </div>

    <div class="nav">
        <a href="StrategicPriorities.aspx">Strategic Priorities</a><br />
        <a href="PDPGoals.aspx">PDP Goals</a><br />
        <a href="MTDPOutcomes.aspx">MTDP Outcomes</a><br />
        <a href="Interventions.aspx">Interventions</a><br />
        <a href="Indicators.aspx">Indicators</a><br />
        <a href="Clusters.aspx">Clusters</a><br />
        <a href="Locations.aspx">Locations</a><br />
        <a href="MonitoringFramework.aspx">Monitoring Framework</a><br />
        <a href="OutcomeIndicators.aspx">Outcome Indicators</a><br />
    </div>

    <br />
    <br />
    <br />


    <style type="text/css">

  /* Accordion Container */
#MyAccordion {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    width: 100%;
    max-width: 1160px;
    margin: 0 auto;
    border-radius: 8px;
    overflow: hidden;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

/* Accordion Header */
.accordionHeader {
    background-color: #f0f0f0;
    padding: 18px 24px;
    font-size: 17px;
    font-weight: 600;
    color: #333;
    border-bottom: 1px solid #ddd;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.accordionHeader:hover {
    background-color: #e2e2e2;
}

/* Selected Header */
.accordionHeaderSelected {
    background-color: #d0e6ff;
    color: #0056b3;
    border-left: 4px solid #0078d7;
}

/* Accordion Link */
.acordionLink {
    text-decoration: none;
    color: inherit;
    display: block;
    width: 100%;
}

/* Accordion Content */
.accordionContent {
    background-color: #ffffff;
    padding: 20px 24px;
    font-size: 15px;
    color: #444;
    border-bottom: 1px solid #ddd;
    animation: fadeIn 0.3s ease-in-out;
}

/* Fade-in animation */
@keyframes fadeIn {
    from { opacity: 0; transform: translateY(-5px); }
    to { opacity: 1; transform: translateY(0); }
}

/* Responsive tweaks */
@media (max-width: 768px) {
    .accordionHeader, .accordionContent {
        padding: 14px 18px;
        font-size: 15px;
    }
}

.acordionLink {
    display: flex;
    align-items: center;
    gap: 10px;
    color: inherit;
    text-decoration: none;
}

.accordion-icon {
    transition: transform 0.3s ease;
    font-size: 14px;
}

/* Rotate icon when selected */
.accordionHeaderSelected .accordion-icon {
    transform: rotate(90deg);
}






/* Smooth height transition */
/*.accordionContent {
    max-height: 0;
    overflow: hidden;
    transition: max-height 0.4s ease, padding 0.4s ease;*/
    /*padding: 0 24px;*/
/*}*/






/* When content is visible (you may need to toggle this class via JS or server-side logic) */
        .accordionContent.expanded {
            max-height: 500px;
        }/* Adjust based on expected content height */
    




    </style>


    <br />

    <div>
        <asp:Label ID="Label13" runat="server" Text="CLUSTER: " Font-Bold="true"></asp:Label>
        <asp:Label ID="lbl_cluster" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label5" runat="server" Text="PMTDP PRIORITY: " Font-Bold="true"></asp:Label>
        <asp:Label ID="lbl_Priority" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label6" runat="server" Text="PDPG: " Font-Bold="true"></asp:Label>
        <asp:Label ID="lbl_PDPG" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label7" runat="server" Text="WORKING GROUP: " Font-Bold="true"></asp:Label>
        <asp:Label ID="lbl_WorkingGroup" runat="server" Text=""></asp:Label><br />

        <asp:Label ID="Label8" runat="server" Text="YEAR: " Font-Bold="true"></asp:Label>
        <asp:Label ID="lbl_Year" runat="server" Text=""></asp:Label>
    </div>

    <br />




    <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server"
    AutoSize="None"
    ContentCssClass="accordionContent"
    FadeTransitions="false"
    FramesPerSecond="40"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    RequireOpenedPane="false"
    SelectedIndex="-1"
    SuppressHeaderPostbacks="true"
    TransitionDuration="250"
    Width="100%"
    padding-left="50px"
    padding-right="10px">
    
    <Panes>
        <AjaxControlToolkit:AccordionPane ID="AccordionPane3" runat="server">
            <Header>
                <a class="acordionLink" href="#">
                    <i class="fas fa-chevron-right accordion-icon"></i>
                    <asp:Label ID="Label4" runat="server" Text="Section Title" />
                </a>
            </Header>

            <Content>
                <asp:Label ID="Label1" runat="server" Text="This is the content of section 1." />





                <br />




                <h2>Monitoring Page</h2>
                <asp:Label ID="lblInfo" runat="server" />
                <asp:GridView ID="gvMonitoring" runat="server" AutoGenerateColumns="False" 
                    OnRowEditing="gvMonitoring_RowEditing" 
                    OnRowUpdating="gvMonitoring_RowUpdating" 
                    OnRowCancelingEdit="gvMonitoring_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Intervention">
                            <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Spend">
                            <ItemTemplate><%# Eval("ActualSpend") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtActualSpend" runat="server" Text='<%# Bind("ActualSpend") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deviation">
                            <ItemTemplate><%# Eval("Deviation") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDeviation" runat="server" Text='<%# Bind("Deviation") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>





                <br />


















            </Content>
        </AjaxControlToolkit:AccordionPane>

        <AjaxControlToolkit:AccordionPane ID="AccordionPane1" runat="server">
            <Header>
                <a class="acordionLink" href="#">
                    <i class="fas fa-chevron-right accordion-icon"></i>
                    <asp:Label ID="Label2" runat="server" Text="Section Title" />
                </a>
            </Header>

            <Content>
                <asp:Label ID="Label3" runat="server" Text="This is the content of section 2." />
            </Content>
        </AjaxControlToolkit:AccordionPane>
    </Panes>
    </AjaxControlToolkit:Accordion>




    <br />
    <br />
    <br />





</asp:Content>

