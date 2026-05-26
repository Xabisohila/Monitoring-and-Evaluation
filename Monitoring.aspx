<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="Monitoring.aspx.cs" Inherits="MonitoringData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <section id="services" class="padding50"> 
        <div class="container">
          <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">
                
                    <br /><br /><br /><br /><br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> Monitoring </strong> </span></h2>
                        
                     </div>
               
                </div>
               <div class="row">
                   <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" Text="Strategic Priority : " /> <asp:Label ID="lblStrategicPriority" runat="server" /><br />
                        <asp:Label ID="Label2" runat="server"  Font-Bold="true" Text="Provincial Development Plan Goal : " Font-Size="Large" /><asp:Label ID="lblPDPGoal" runat="server" />  <br />
                    <asp:Label ID="Label3" runat="server"  Font-Bold="true" Text="Cluster Working Group : " Font-Size="Large" /><asp:Label ID="lblClusterWGName" runat="server" />  , <asp:Label ID="lblFinancialYear" runat="server" />  ,&nbsp; <asp:Label ID="lblQuarter" runat="server" />  <br /> <br />
                   <asp:Label ID="lblMessage" runat="server" Visible="false" />  <br />
               </div>
            </div>
              <!-- ./Heading -->           
            <div class="row">
                
                 
                 <br />
                <br />
                <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server" AutoSize="None" 
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" 
            HeaderCssClass="accordionHeader" 
            HeaderSelectedCssClass="accordionHeaderSelected" RequireOpenedPane="false" 
            SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250" Width="1160px" 
            >
           <panes>
               
               <AjaxControlToolkit:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="" HeaderCssClass="">
                   
                <header><a class="acordionLink" href="" ><asp:Label ID="lblFocusArea1"  runat="server" /></a> </header>
                <content> 
               
                 <asp:GridView ID="gvFocusArea1" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea1_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA1Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1Actual" runat="server"   Width="70px" OnTextChanged="txtFA1Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA1Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA1" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA1YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA1" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
        
                    <br /><br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea1" OnClick="btnSubmitFocusArea1_Click" Text="Submit" ForeColor="Black" />
               </content>
                    
            </AjaxControlToolkit:AccordionPane>
               
               <AjaxControlToolkit:AccordionPane ID="AccordionPane2" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea2"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea2" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea2_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA1Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Actual" runat="server"   Width="70px" OnTextChanged="txtFA2Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA1Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA2" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA2" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea2" OnClick="btnSubmitFocusArea2_Click" Text="Submit" ForeColor="Black" />
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane3" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea3"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea3" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea3_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="PMTDP OUTCOME"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Intervention" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA1Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Actual" runat="server"   Width="70px" OnTextChanged="txtFA3Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA3Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA3" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA3" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea3" OnClick="btnSubmitFocusArea3_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane4" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea4"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea4" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea4_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA4Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Actual" runat="server"   Width="70px" OnTextChanged="txtFA4Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA4Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA4" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA4" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea4" OnClick="btnSubmitFocusArea4_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane5" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea5"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea5" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea5_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA4Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Actual" runat="server"   Width="70px" OnTextChanged="txtFA5Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA5Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA5" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA5" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea5" OnClick="btnSubmitFocusArea5_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane6" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea6"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea6" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea6_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA6Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Actual" runat="server"   Width="70px" OnTextChanged="txtFA6Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA6Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA6" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA6" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea6" OnClick="btnSubmitFocusArea6_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane7" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea7"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea7" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea7_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA7Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Actual" runat="server"   Width="70px" OnTextChanged="txtFA7Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA7Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA7" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA7" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea7" OnClick="btnSubmitFocusArea7_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane8" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea8"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea8" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea8_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA8Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Actual" runat="server"   Width="70px" OnTextChanged="txtFA8Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA8Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA8" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA8" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea8" OnClick="btnSubmitFocusArea8_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane9" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea9"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea9" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea9_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA4Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Actual" runat="server"   Width="70px" OnTextChanged="txtFA9Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA9Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA9" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA9" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea9" OnClick="btnSubmitFocusArea9_Click" Text="Submit"  ForeColor="Black"/>
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane10" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea10"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea10" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea10_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA10Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10Actual" runat="server"   Width="70px" OnTextChanged="txtFA10Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA10Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA10" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA10YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA10" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea10" OnClick="btnSubmitFocusArea10_Click" Text="Submit" ForeColor="Black" />
               </content>
            </AjaxControlToolkit:AccordionPane> 

               <AjaxControlToolkit:AccordionPane ID="AccordionPane11" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""><asp:Label ID="lblFocusArea11"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea11" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea10_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                <asp:BoundField DataField="SubOutcome" HeaderText="Sub-Outcome"   ItemStyle-Width="200" Visible="false"/>
                                <asp:BoundField DataField="KeyResults" HeaderText="Key Results" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="KeyIndicator" HeaderText="Key Indicator"    ItemStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="KeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA11Baseline" runat="server" Text= '<%# Bind("Baseline") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11AnnualTarget" runat="server" Text= '<%# Bind("AnnualTarget") %>'  Width="70px" Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Planning" runat="server"
                                        Text= '<%# Bind("QPlanning") %>'  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Actual" runat="server"   Width="70px" OnTextChanged="txtFA11Actual_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Varience" runat="server"
                                         Width="70px" Enabled="false"></asp:TextBox>  <asp:Image ID="ImageFA11Status" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="irFA11" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11YTDTotal" runat="server"  Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11YrVar" runat="server"     Width="70px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Image ID="isFA11" runat="server"></asp:Image>
                                </ItemTemplate>
                                </asp:TemplateField>

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
                    <br /><br />
                    <asp:Button runat="server" ID="btnSubmitFocusArea11" OnClick="btnSubmitFocusArea11_Click" Text="Submit" ForeColor="Black" />
               </content>
            </AjaxControlToolkit:AccordionPane> 

            </panes>
        </AjaxControlToolkit:Accordion>
                            
       
            </div>
    </div><!--/.container-->
 </section>
    <!-- ======================================== Portfolio Modals ======================================== -->
    <!-- Use the modals below to showcase details about your portfolio projects! -->

    <!-- Download Section -->
    <section id="download" class="padding100">
        <div class="container">
            <div class="row">
                <div class="section-title col-sm-8 col-sm-offset-2 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2">
                    <h2 class="animated wow fadeInLeft" data-wow-delay="0.4s">
                        More Cluster Documents <span class="common"> to Download</span></h2>
                    
                </div>
            </div>
            <!-- ./end row -->
            <div class="row">
                <div class="col-sm-8 col-sm-offset-2 text-center">
                    <div class="download-wrap animated wow fadeInLeft" data-wow-delay="0.4s">
                        <a href="#" class="btn btn-download app"><strong>
                            Reporting Templates</strong> <span>Click to download</span> </a><a href="#" class="btn btn-download app"
                                data-wow-delay="0.2s"><strong>Cluster Presentations</strong> <span>
                                    Click to download</span> </a><a href="#" class="btn btn-download app"
                                        data-wow-delay="0.2s"><strong>Meeting Schedules</strong>
                                        <span>Click to download</span> </a>
                    </div>
                </div>
            </div>
            <!-- ./end row -->
        </div>
    </section>
    <!--End Download Section end-->
</asp:Content>

