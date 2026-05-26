<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProject.aspx.cs" MasterPageFile="~/akshara.master" Inherits="Process_ViewProject" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="padding100" id="blog">
        <div class="container">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s">
                        <span><strong>Project</strong> View</span></h2>               
                </div>
            </div>
            <!-- /.row -->
            <asp:Label ID="lblError" runat="server" CssClass="NormalLabel" Visible="False"></asp:Label>
            

            <div class="row">
                <div class="col-md-6 animated wow fadeInRight" data-wow-delay="0.4s">
                     <h3> <strong><asp:Label ID="lblProjectName" runat="server" CssClass="NormalLabel"></asp:Label></strong></h3>
                   
                    <div class="entry-thumbnail pull-left">
                        &nbsp;<asp:Label ID="lblDuration" runat="server" Visible="False"></asp:Label><br />
                        <asp:Label ID="lblBackground" runat="server" Text="Project Background" CssClass="NormalLabel" Font-Bold="True"></asp:Label><br />
                        <asp:Label ID="txtBackground" runat="server" CssClass="BigTextBox" TextMode="MultiLine" ReadOnly="True"></asp:Label><br /><br />

                         <asp:Label ID="Label1" runat="server" Text="Project Type : " Font-Bold="True"></asp:Label><asp:Label ID="Label2" runat="server" Text=" Infrastructure Transfer - Capital "  ></asp:Label><br />
              
                            </div>
                    
                   
                </div>
                <div class="col-md-6">
                     
                     <asp:Chart ID="cProgress" runat="server" BackColor="LightCoral" BackGradientStyle="HorizontalCenter"   
                                    BorderlineWidth="0" Height="201px" Palette="None" PaletteCustomColors="Green"   
                                    Width="430px" BorderlineColor="64, 0, 64" >   
                                    <Titles>   
                                        <asp:Title ShadowOffset="10" Name="Items" Text="Project Progress" Font="Microsoft Sans Serif, 12pt, style=Bold" ShadowColor="Transparent" />   
                                    </Titles>   
                                    <Legends>   
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="IDMSGate"  
                                            LegendStyle="Row" HeaderSeparatorColor="Transparent" ItemColumnSeparatorColor="Transparent" />   
                                    </Legends>   
                                    <Series>   
                                        <asp:Series ChartType="Bar" Name="IDMSGate"  Url=""/>   
                                    </Series>   
                                    <ChartAreas>   
                                        <asp:ChartArea Name="Progress Bar" BorderWidth="0" BorderColor="Transparent" >   
                                            <AxisY LineColor="Transparent">
                                                <MajorGrid LineColor="Transparent" />
                                            </AxisY>
                                            <AxisX LineColor="Transparent" >
                                                <MajorGrid LineColor="Transparent" />
                                            </AxisX>
                                            <AxisX2 LineColor="Transparent">
                                            </AxisX2>
                                            <AxisY2 LineColor="Transparent">
                                            </AxisY2>
                                        </asp:ChartArea>
                                    </ChartAreas>   
                                </asp:Chart> 

                </div>

                <div class="col-md-6 animated wow fadeInRight" data-wow-delay="0.4s">
                    <div class="entry-thumbnail pull-left">
                                	<div id="wowslider-container1">
	                                <div class="ws_images"><ul>
		                                <li><img src="img/gallery/kirk1.jpg" alt="kirk1" title="kirk1" id="wows1_0"/></li>
		                                <li><img src="img/gallery/kirk2.jpg" alt="kirk2" title="kirk2" id="wows1_1"/></li>
		                                <li><img src="img/gallery/kirk3.jpg" alt="kirk3" title="kirk3" id="wows1_2"/></li>
		                                <li><img src="img/gallery/kirk4.jpg" alt="kirk4" title="kirk4" id="wows1_3"/></li>
		                                <li><img src="img/gallery/kirk5.jpg" alt="kirk5" title="kirk5" id="wows1_4"/></li>
		                                <li><img src="img/gallery/kirk6.jpg" alt="kirk6" title="kirk6" id="wows1_5"/></li>
		                                <li><img src="img/gallery/ray1.jpg" alt="ray1" title="ray1" id="wows1_6"/></li>
		                                <li><img src="img/gallery/ray2.jpg" alt="ray2" title="ray2" id="wows1_7"/></li>
		                                <li><img src="img/gallery/ray3.jpg" alt="ray3" title="ray3" id="wows1_8"/></li>
		                                <li><img src="img/gallery/ray4.jpg" alt="ray4" title="ray4" id="wows1_9"/></li>
		                                <li><img src="img/gallery/ray5.jpg" alt="full screen slider" title="Road" id="wows1_10"/></li>
		                                <li><img src="img/gallery/ray6.jpg" alt="ray6" title="ray6" id="wows1_11"/></li>
	                                </ul></div>
	                                <div class="ws_bullets">

	                                </div>
	                                <div class="ws_shadow"></div>
	                                </div>	 
                                    <script type="text/javascript" src="engine1/wowslider.js"></script>
	                                <script type="text/javascript" src="engine1/script.js"></script>     
                                 <br />                
                            </div> 
                </div>

                <div class="col-md-6">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="290px" ImageUrl="~/img/rayMap.jpg" Width="428px" /> 
                </div>
            </div>
  

            <br />
            <div class="row">  
              
                <div class="col-md-12 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
                   
                       <h4>Number of people employed</h4>
                     <asp:GridView ID="gvProjectEmployment" runat="server" AllowSorting="True" AllowPaging="true"
                     AutoGenerateColumns="False" WizardCustomPager="False" Width="600px" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                     <Columns>
                  
                     <asp:TemplateField HeaderText="Males"  >
                     <HeaderStyle HorizontalAlign="Center" Width="75px" BackColor ="Brown"/>
                     <ItemStyle HorizontalAlign="Center" Width="75px" Height="50px" />
                     <ItemTemplate>
                     <asp:Label ID="lblFemales" runat="server" Text= '<%# Bind("Males") %>' />
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Females"  >
                     <HeaderStyle HorizontalAlign="Center" Width="75px" BackColor ="Brown"/>
                     <ItemStyle HorizontalAlign="Center" Width="75px" Height="50px" />
                     <ItemTemplate>
                     <asp:Label ID="lblFemales" runat="server" Text= '<%# Bind("Females") %>' />
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Disabled" >
                     <HeaderStyle HorizontalAlign="Center" Width="75px" BackColor ="Brown"  />
                     <ItemStyle HorizontalAlign="Center" Width="75px" Height="50px" />
                     <ItemTemplate>
                     <asp:Label ID="lblDisabled" runat="server" Text='<%# Eval("Disabled") %>' />
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Students">
                     <HeaderStyle HorizontalAlign="Center" Width="75px" BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center" Width="75px" Height="50px" />
                     <ItemTemplate>
                         <asp:Label ID="lblStudents" runat="server" Text='<%# Eval("Students") %>' />
                          </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Total">
                     <HeaderStyle HorizontalAlign="Center" Width="75px" BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center" Width="75px" Height="50px" />
                     <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text= '<%# Bind("Total") %>' /> 
                     </ItemTemplate>
                     </asp:TemplateField>
                     </Columns>

                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />

                    </asp:GridView>
                   
                    
                </div>
                <!-- Box-4 -->
       
            </div>
        </div>
    </div>

  
</asp:Content>