<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pageInterventionDetails.aspx.cs" Inherits="pageInterventionDetails" %>
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
    background-color: white;/* #d0e6ff;*/
    color: #03ac13;/* #0056b3;*/
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
    


























         
body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f4f6f9;
        margin: 20px;
    }

    h2 {
        color: #333;
        margin-top: 40px;
    }

    .grid-container {
        margin-bottom: 40px;
        background-color: #fff;
        border: 1px solid #ddd;
        border-radius: 6px;
        box-shadow: 0 2px 5px rgba(0,0,0,0.05);
        padding: 20px;
    }

    .gridview {
        width: 100%;
        border-collapse: collapse;
    }

    .gridview th {
        /*background-color: #0078D7;*/
        background-color: #03AC13;
        color: white;
        padding: 10px;
        text-align: left;
    }

    .gridview td {
        padding: 10px;
        border-bottom: 1px solid #eee;
    }

    .gridview tr:nth-child(even) {
        background-color: #f9f9f9;
    }

    .gridview tr:hover {
        background-color: #f1f1f1;
    }

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




                <%--<h2>Planning Page</h2>
                <asp:Label ID="lblInfo" runat="server" />
                <asp:GridView ID="gvPlanning" runat="server" AutoGenerateColumns="False" 
                    OnRowEditing="gvPlanning_RowEditing" 
                    OnRowUpdating="gvPlanning_RowUpdating" 
                    OnRowCancelingEdit="gvPlanning_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Intervention">
                            <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Budget">
                            <ItemTemplate><%# Eval("Budget") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBudget" runat="server" Text='<%# Bind("Budget") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>--%>





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



                <asp:GridView ID="gvIntervention1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" OnDataBound="gvIntervention1_DataBound" HorizontalAlign="Left">
                    <Columns>

                        <asp:TemplateField HeaderText="InterventionName" ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFA1AnnualTarget" runat="server"
                                    Text='<%# Bind("InterventionName") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

               
               <%--<asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
               <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
               <ItemTemplate>
                   <asp:LinkButton ID="lnkKeyIndicatorFA1" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA1_Click"/>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
               <ItemTemplate>
                   <asp:Label runat="server" ID="lblKeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                   <asp:Label runat="server" ID="lblKeyResult1" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                   <asp:TextBox ID="txtFA1Baseline" runat="server"
                       Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1AnnualTarget" runat="server"
                       Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1Q1Planning" runat="server"
                       Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1Q2Planning" runat="server"
                       Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1Q3Planning" runat="server"
                       Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1Q4Planning" runat="server"
                       Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px" OnTextChanged="txtFA1Q4Planning_TextChanged" AutoPostBack="true"></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Annual Archived" ItemStyle-Width="10px">
               <ItemTemplate>
                   <asp:TextBox ID="txtFA1Total" runat="server"
                       Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px" ></asp:TextBox>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />--%>
             </Columns>
           <FooterStyle BackColor="White" ForeColor="#000066" />
           <HeaderStyle BackColor="#980BF64" Font-Bold="True" ForeColor="#000000" />
           <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
           <RowStyle ForeColor="#000000" />
           <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
           <SortedAscendingCellStyle BackColor="#F1F1F1" />
           <SortedAscendingHeaderStyle BackColor="#007DBB" />
           <SortedDescendingCellStyle BackColor="#CAC9C9" />
           <SortedDescendingHeaderStyle BackColor="#00547E" />
       </asp:GridView>


                <br />


<h2>Intervention Details</h2>
<div class="grid-container">
    <asp:GridView ID="GridViewIntervention" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate><%# Eval("InterventionName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate><%# Eval("InterventionDescription") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lead Institution">
                <ItemTemplate><%# Eval("LeadInstitution") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<h2>Indicators</h2>
<div class="grid-container">
    <asp:GridView ID="GridViewIndicators" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:TemplateField HeaderText="Indicator">
                <ItemTemplate><%# Eval("IndicatorName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Baseline">
                <ItemTemplate><%# Eval("BaselineValue") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Target Year">
                <ItemTemplate><%# Eval("TargetYear") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<h2>Budgets</h2>
<div class="grid-container">
    <asp:GridView ID="GridViewBudgets" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate><%# Eval("FinancialYear") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Annual Budget">
                <ItemTemplate><%# Eval("AnnualBudget", "{0:C}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Term Budget">
                <ItemTemplate><%# Eval("TermBudget", "{0:C}") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<h2>Quarterly Reports</h2>
<div class="grid-container">
    <asp:GridView ID="GridViewReports" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate><%# Eval("FinancialYear") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quarter">
                <ItemTemplate><%# Eval("Quarter") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actual">
                <ItemTemplate><%# Eval("ActualExpenditure", "{0:C}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Planned">
                <ItemTemplate><%# Eval("PlannedExpenditure", "{0:C}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deviation">
                <ItemTemplate><%# Eval("BudgetDeviation", "{0:C}") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>































            </Content>
        </AjaxControlToolkit:AccordionPane>







































        <AjaxControlToolkit:AccordionPane ID="AccordionPane2" runat="server">
    <Header>
        <a class="acordionLink" href="#">
            <i class="fas fa-chevron-right accordion-icon"></i>
            <asp:Label ID="Label9" runat="server" Text="Section Title" />
        </a>
    </Header>

    <Content>
        <asp:Label ID="Label10" runat="server" Text="This is the content of section 2." />


        <asp:GridView ID="gvInterventions" runat="server" AutoGenerateColumns="False" CssClass="table">
    <Columns>
        <asp:BoundField DataField="InterventionName" HeaderText="Intervention" />
        <asp:BoundField DataField="LeadInstitution" HeaderText="Lead By" />
        <asp:HyperLinkField DataTextField="InterventionName" DataNavigateUrlFields="InterventionID" DataNavigateUrlFormatString="InterventionDetail.aspx?id={0}" HeaderText="Details" Text="View" />
    </Columns>
</asp:GridView>





    </Content>
</AjaxControlToolkit:AccordionPane>
    </Panes>
    </AjaxControlToolkit:Accordion>




    <br />
    <br />
    <br />





</asp:Content>

