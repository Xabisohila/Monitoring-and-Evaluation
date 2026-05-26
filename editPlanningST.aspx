<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="editPlanningST.aspx.cs" Inherits="editPlanningST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <%--<link rel="stylesheet" href="h ttps://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
<script src="h ttps://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
<script src="h ttps://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
<script src="h ttps://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>--%>

    <section id="services" class="padding50"> 
        <div class="container">
          <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">
                
                    <br /><br /><br /><br /><br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> Planning </strong> </span></h2>
                        
                     </div>
               
                </div>
               <div class="row">
                   <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" Text="PMTDP PRIORITY : " ToolTip="Strategic Priority " /> <asp:Label ID="lblStrategicPriority" runat="server" /><br />
                        <asp:Label ID="Label2" runat="server"  Font-Bold="true" Text="PDPG : " Font-Size="Large"  ToolTip="Provincial Development Plan Goal " /><asp:Label ID="lblPDPGoal" runat="server" />  <br />
                    <asp:Label ID="Label3" runat="server"  Font-Bold="true" Text="Cluster Working Group : " Font-Size="Large" /><asp:Label ID="lblClusterWGName" runat="server" />  , <asp:Label ID="lblFinancialYear" runat="server" />  <br /> <br />
                   <asp:Label ID="lblMessage" runat="server" Visible="false" />  <br />
               </div>
            </div>
              <!-- ./Heading -->  
            <AjaxControlToolkit:BarChart ID="bcKeyIndicator" runat="server" ChartHeight="300" ChartWidth="1000" Visible="false"
                            
                            ChartType="Column" ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9"
                            ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" Width="1060px" Height="321px">
                        </AjaxControlToolkit:BarChart>       
            <div class="row">
                
                 
                <AjaxControlToolkit:Accordion ID="MyAccordion" runat="server" AutoSize="None" 
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" 
            HeaderCssClass="accordionHeader" 
            HeaderSelectedCssClass="accordionHeaderSelected" RequireOpenedPane="false" 
            SelectedIndex="-1" SuppressHeaderPostbacks="true" TransitionDuration="250" Width="1160px" 
            >
           <panes>
               
               <AjaxControlToolkit:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="" HeaderCssClass="">
                   
                <header><a class="acordionLink" href="" style="color:white"><asp:Label ID="lblFocusArea1"  runat="server" /></a> </header>
                <content> 
               
                 <asp:GridView ID="gvIntervention1" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" OnRowCommand="gvIntervention1_RowCommand"
                      OnDataBound="gvIntervention1_DataBound" HorizontalAlign="Left" OnRowEditing="gvIntervention1_RowEditing">
                            <Columns>
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA1" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA1_Click"/>
                                    
                                    <asp:Label ID="txtKeyIndicatorFA1" runat="server" Visible="false"
                                        Text='<%# Bind("KeyIndicator") %>' BorderStyle="None" Width="40px"></asp:Label>

                                    <%--<asp:TextBox ID="TextBox1" runat="server" Visible="false"
                                        Text='<%# Bind("KeyIndicator") %>' BorderStyle="None" Width="40px"></asp:TextBox>--%>

		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult1Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult1" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    
                                    
                                    <asp:Label ID="txtFA1Baseline" runat="server"
                                        Text='<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:Label>

                                    <%--<asp:TextBox ID="TextBox1" runat="server"
                                        Text='<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>--%>

                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>

                                    <asp:Label ID="txtFA1AnnualTarget" runat="server"
                                        Text='<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:Label>

                                    <%--<asp:TextBox ID="TextBox1" runat="server"
                                        Text='<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>--%>



                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:Label ID="txtFA1Q1Planning" runat="server"
                                            Text='<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:Label>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"
                                            Text='<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>--%>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:Label ID="txtFA1Q2Planning" runat="server"
                                            Text='<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:Label>

                                        <%--<asp:TextBox ID="TextBox2" runat="server"
                                            Text='<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>--%>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:Label ID="txtFA1Q3Planning" runat="server"
                                            Text='<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:Label>

                                        <%--<asp:TextBox ID="TextBox3" runat="server"
                                            Text='<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>--%>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:Label ID="txtFA1Q4Planning" runat="server"
                                            Text='<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px" OnTextChanged="txtFA1Q4Planning_TextChanged" AutoPostBack="true"></asp:Label>


                                        <%--<asp:TextBox ID="TextBox4" runat="server"
                                            Text='<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px" OnTextChanged="txtFA1Q4Planning_TextChanged" AutoPostBack="true"></asp:TextBox>--%>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Annual Archived" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:Label ID="txtFA1Total" runat="server"
                                            Text='<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:Label>

                                        <%--<asp:TextBox ID="TextBox5" runat="server"
                                            Text='<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>--%>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10px">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="btn btn-primary" OnClick="btnEdit_Click">Edit</asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                    <asp:Button runat="server" ID="btnSubmitFocusArea1" OnClick="btnSubmitFocusArea1_Click" Text="Submit" ForeColor="Black" />



                    <br />



                    <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true" style="color:#000;">
                        <div class="modal-dialog" role="document" >
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="editModalLabel" style="color:#000;">Edit</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    


                                    <div class="container">
                                        <div class="row">
                                            <div  style="width:45%;">
                                                <!-- Example TextBox for Editing -->
                                                <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <!-- Example Hidden Field for ID -->
                                                <asp:HiddenField ID="hiddenEditID" runat="server" />

                                                <%-- Other --%>
                                                <asp:TextBox ID="txtBaseline" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="txtAnnualTarget" runat="server" CssClass="form-control"></asp:TextBox><br />

                                                <asp:TextBox ID="lbl_Q1" runat="server" CssClass="text-center" Text="Quater 1"></asp:TextBox><br />
                                                <asp:TextBox ID="txtQ1" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                <br />

                                                <asp:TextBox ID="lbl_Q2" runat="server" CssClass="text-center" Text="Quater 2"></asp:TextBox><br />
                                                <asp:TextBox ID="txtQ2" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                <br />

                                                <asp:TextBox ID="lbl_Q3" runat="server" CssClass="text-center" Text="Quater 3"></asp:TextBox><br />
                                                <asp:TextBox ID="txtQ3" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                <br />

                                                <asp:TextBox ID="lbl_Q4" runat="server" CssClass="text-center" Text="Quater 4"></asp:TextBox><br />
                                                <asp:TextBox ID="txtQ4" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                <br />

                                                <asp:TextBox ID="lbl_Total" runat="server" CssClass="text-center" Text="Annual Archived" Enabled="false"></asp:TextBox><br />
                                                <asp:TextBox ID="txt_Total" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                <br />
                                            </div>
                                            <div  style="width:45%;">
                                                2 of 2
                                            </div>
                                        </div>
                                    </div>





                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
               </content>
                    
            </AjaxControlToolkit:AccordionPane>
               
               <AjaxControlToolkit:AccordionPane ID="AccordionPane2" runat="server" ContentCssClass="" HeaderCssClass="">
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea2"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea2" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea2_DataBound" HorizontalAlign="Left">
                            <Columns>
                            
                                        <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                
                                        <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA2" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA2_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult2Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult2" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA2Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Archived" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea3"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea3" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea3_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA3" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA3_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult3Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult3" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA3Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA3Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea4"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea4" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea4_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA4" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA4_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult4Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult4" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA4Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA4Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea5"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea5" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea5_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA5" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA5_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult5Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult5" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA5Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA5Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea6"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea6" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea6_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA6" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA6_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult6Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult6" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA6Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA6Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea7"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea7" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea7_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA7" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA7_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult7Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult7" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA7Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA7Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea8"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea8" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea8_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA8" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA8_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult8Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult8" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA8Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA8Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea9"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea9" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea9_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                               
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA9" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA9_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult9Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult9" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA9Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA9Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea10"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea10" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea10_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA10" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA10_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult10Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult10" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA2Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA2Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ResponsibleInstitution" HeaderText="Responsible Institution" ItemStyle-Width="250" />
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
                <header><a  class="acordionLink" href=""  style="color:white"><asp:Label ID="lblFocusArea11"  runat="server" /></a>  </header>
                <content>    
                  <asp:GridView ID="gvFocusArea11" runat="server" AutoGenerateColumns="False" BackColor="White"  BorderStyle="None" BorderWidth="1px" CellPadding="3" OnDataBound="gvFocusArea11_DataBound" HorizontalAlign="Left">
                            <Columns>
                    
                                
                                <asp:BoundField DataField="Intervention" HeaderText="Intervention" ItemStyle-Width="250" />
                                <asp:TemplateField HeaderText="Indicator" ItemStyle-Width="250">
                                <ItemTemplate>
			                        <asp:LinkButton ID="lnkKeyIndicatorFA11" runat="server" Text= '<%# Bind("KeyIndicator") %>' ForeColor="Blue" OnClick="lnkKeyIndicatorFA11_Click"/>
		                        </ItemTemplate>
		                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baseline" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblKeyResult11Id" Visible="false" Text= '<%# Bind("KeyResultId") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblKeyResult11" Visible="false" Text= '<%# Bind("Intervention") %>'></asp:Label>
                                    <asp:TextBox ID="txtFA11Baseline" runat="server"
                                        Text= '<%# Bind("Baseline") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Target" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11AnnualTarget" runat="server"
                                        Text= '<%# Bind("AnnualTarget") %>' BorderStyle="None" Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q1" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Q1Planning" runat="server"
                                        Text= '<%# Bind("Q1Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q2" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Q2Planning" runat="server"
                                        Text= '<%# Bind("Q2Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q3" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Q3Planning" runat="server"
                                        Text= '<%# Bind("Q3Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q4" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Q4Planning" runat="server"
                                        Text= '<%# Bind("Q4Planning") %>' BorderStyle="None" Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Achieved" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFA11Total" runat="server"
                                        Text= '<%# Bind("TotalValue") %>' BorderStyle="None" Width="70px"></asp:TextBox>
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
                    <asp:Button runat="server" ID="Button1" OnClick="btnSubmitFocusArea10_Click" Text="Submit" ForeColor="Black" />
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

