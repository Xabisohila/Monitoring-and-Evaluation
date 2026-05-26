<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="B5.aspx.cs" Inherits="B5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <div id="banner">
        <div class="container">
            <h1>
                100% Free Fully Responsive HTML5 Bootstrap Template.
                <br />
                Grab it now for free!
            </h1>
            <h3>
                This is a Fully Responsive and Modern App Landing Page Template.</h3>
        </div>
    </div>--%>
    <div class="padding100" id="blog">
        <div class="container">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s">
                        <span><strong>B5</strong> Projects</span></h2>                   
                </div>
            </div>
            <!-- /.row -->

            

            <div class="row">
                <div class="col-md-4 animated wow fadeInRight" data-wow-delay="0.4s">
                    <a href="#">
                        
                    </a>
                   
                    <div class="entry-thumbnail pull-left">
                                <img class="img-responsive" src="img/Premmier_sqr.jpg" alt="" width="250 px" height="250 px">                                
                            </div>
                     <h3>
                        <strong>Infrastructure Projects</strong></h3>
                    <h4>
                        Eastern Cape</h4>
                    <a class="btn btn-success" href="#">View Project <span class="glyphicon glyphicon-chevron-right">
                    </span></a>
                </div>
                <div class="col-md-8">
                     <div class="entry-thumbnail pull-left">
                                 <asp:Chart ID="Chart1" runat="server" BackColor="Yellow" BackGradientStyle="LeftRight"   
                                    BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Green"   
                                    Width="356px" BorderlineColor="64, 0, 64">   
                                    <Titles>   
                                        <asp:Title ShadowOffset="10" Name="Items" Text="Top 3 Project Stages" Font="Microsoft Sans Serif, 12pt, style=Bold" ShadowColor="White" />   
                                    </Titles>   
                                    <Legends>   
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"   
                                            LegendStyle="Row" />   
                                    </Legends>   
                                    <Series>   
                                        <asp:Series Name="Default" ChartType="Doughnut" />   
                                    </Series>   
                                    <ChartAreas>   
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />   
                                    </ChartAreas>   
                                </asp:Chart>       
                                                 
                            </div> 
                    
                    
                    <div class="entry-thumbnail pull-left">

                               <asp:Chart ID="Chart2" runat="server"   
                                    BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Maroon"   
                                    Width="375px" BorderlineColor="64, 0, 64">   
                                    <Titles>   
                                        <asp:Title ShadowOffset="10" Name="Items" Font="Microsoft Sans Serif, 12pt, style=Bold" ShadowColor="White" Text="Top 3 Project Cost" />   
                                    </Titles>   
                                    <Legends>   
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"   
                                            LegendStyle="Row" />   
                                    </Legends>   
                                    <Series>   
                                        <asp:Series Name="Default" ChartType="Bar" />   
                                    </Series>   
                                    <ChartAreas>   
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />   
                                    </ChartAreas>   
                                </asp:Chart>                                 
                            </div> 
                    
                    
                    
                    <%--<table style="width: 100%;">
                        <tr>
                            <td>Doughnut Here</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Doughnut Here</td>
                            <td>&Middle;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Doughnut Here</td>
                            <td>&nbsp;</td>
                            <td>&down;</td>
                        </tr>
                    </table>--%>
                </div>
            </div>
  

            <br />
            <div class="row">  
              
                <div class="col-md-12 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
                   
                       
                     <asp:GridView ID="gvB5Project" runat="server" AllowSorting="True" 
                     AutoGenerateColumns="False" WizardCustomPager="False"  BorderColor="#CC3300" CellPadding="4" OnPageIndexChanging = "gvProjects_PageIndexChanging"
                             AllowPaging="True" ForeColor="#333333" GridLines="None" PageSize="10">
                            <AlternatingRowStyle BackColor="White" Width="100%" />
                     <Columns>
                     <asp:TemplateField HeaderText="Project Name">
                     <HeaderStyle HorizontalAlign="Left" Width="250px"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="left" Width="250px" Height="50px"/>
                     <ItemTemplate>
                     <asp:Label  ID="lblProjectId" runat="server" Text= '<%# Bind("B5ProjectId") %>' visible="false" />
                         <asp:LinkButton Font-Bold="true" ID="lnkProjectName" runat="server" Text= '<%# Bind("Name") %>' OnClick="lnkProjectName_OnClick"/>
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="IDMS Gates"  >
                     <HeaderStyle HorizontalAlign="Center" BackColor ="Brown"/>
                     <ItemStyle HorizontalAlign="Center"  Width="100px" Height="50px" />
                     <ItemTemplate>
                     <asp:Label ID="Label2" runat="server" Text= '<%# Bind("Status") %>' />
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Municipal Region" >
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown"  />
                     <ItemStyle HorizontalAlign="Center"  Width="200px" Height="50px" />
                     <ItemTemplate>
                     <asp:Label ID="Label4" runat="server" Text= '<%# Bind("MunicipalRegion") %>' />
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date: Start">
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center" Width="100px"  Height="50px" />
                     <ItemTemplate>
                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate", "{0:d MMM yyyy}") %>' /> 
                     </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date: End">
                     <HeaderStyle HorizontalAlign="Center" BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center"  Width="100px" Height="50px" />
                     <ItemTemplate>
                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:d MMM yyyy}") %>' /> 
                     </ItemTemplate>
                     </asp:TemplateField>
                           <asp:TemplateField HeaderText="Total Cost">
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center"  Width="100px" Height="50px" />
                     <ItemTemplate>
                    R<asp:Label ID="lblBudget" runat="server" Text='<%# Eval("Cost") %>' /> 
                     </ItemTemplate>
                     </asp:TemplateField>
                           <asp:TemplateField HeaderText="Duration">
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center"  Width="80px" Height="50px" />
                     <ItemTemplate>
                    <asp:Label ID="lblSCMUpdate" runat="server" Text='<%# Eval("TotalYears") %>' /> year(s) , <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalMonths") %>' /> month(s)
                     </ItemTemplate>
                     </asp:TemplateField>
                          <asp:TemplateField HeaderText="Total Available">
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center" Width="100px"  Height="50px" />
                     <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalAvailable") %>' /> 
                          </ItemTemplate>
                     </asp:TemplateField>
                               <asp:TemplateField HeaderText="Current MTEF">
                     <HeaderStyle HorizontalAlign="Center"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="Center"  Width="100px" Height="50px" />
                     <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("CurrentMTEF") %>' /> 
                          </ItemTemplate>
                     </asp:TemplateField>
                     </Columns>

        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center"/>
        <PagerStyle ForeColor="#333333" HorizontalAlign="Center" BackColor="#FFCC66" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />

</asp:GridView>
                   
                    
                </div>
                <!-- Box-4 -->
       
            </div>
            <%--<div class="row">
                <div class="col-sm-6">
                    <div class="blog-post blog-large wow zoomIn" data-wow-duration="1500ms" data-wow-delay="100ms">
                        <article>
                            <header class="entry-header">
                                <div class="entry-thumbnail">
                                    <img class="img-responsive" src="img/item01.jpg" alt="">                                    
                                </div>
                                <div class="entry-date">25 June 2014</div>
                                <h3>We work in some of the world’s toughest situations where we offer bold</h3>
                            </header>

                            <div class="entry-content">
                                <P>We work in some of the world’s toughest situations where we offer bold, practical  and innovative solutions where there is real need. Our focus is on the next generation, current and future leaders and enhancing collaboration between faith communities. We work in some of the world’s toughest situations where we offer bold.</P>
                                <p>We work in some of the world’s toughest situations where we offer bold.
                                </p>
                                <a class="btn btn-danger" href="#">Read More</a>
                            </div>

                            <footer class="entry-meta">
                                <span class="entry-author"><i class="fa fa-pencil"></i> <a href="#">Victor</a></span>
                                <span class="entry-category"><i class="fa fa-folder-o"></i> <a href="#">Tutorial</a></span>
                                <span class="entry-comments"><i class="fa fa-comments-o"></i> <a href="#">15</a></span>
                            </footer>
                        </article>
                    </div>
                </div>
                <!--/.col-sm-6-->
                <div class="col-sm-6">
                    <div class="blog-post blog-media wow zoomIn" data-wow-duration="1500ms" data-wow-delay="100ms">
                        <article class="media clearfix">
                            <div class="entry-thumbnail pull-left">
                                <img class="img-responsive" src="img/item02.jpg" alt="">                                
                            </div>
                            <div class="media-body"> <!-- Content -->
                                <header class="entry-header">
                                    <div class="entry-date">01 May 2014</div>
                                    <h2 class="entry-title"><a href="#">BeReviews was a awesome envent in Hyd</a></h2>
                                </header>

                                <div class="entry-content">
                                    <P>We work in some of the world’s toughest situations where we offer bold.</P>
                                    <a class="btn btn-danger" href="#">Read More</a>
                                </div>

                                <footer class="entry-meta">
                                    <span class="entry-author"><i class="fa fa-pencil"></i> <a href="#">Campbell</a></span>
                                    <span class="entry-category"><i class="fa fa-folder-o"></i> <a href="#">Tutorial</a></span>
                                    <span class="entry-comments"><i class="fa fa-comments-o"></i> <a href="#">15</a></span>
                                </footer>
                            </div>
                            <!-- ./Content -->
                        </article>
                    </div>
                    <div class="blog-post blog-media wow zoomIn" data-wow-duration="1500ms" data-wow-delay="100ms">
                        <article class="media clearfix">
                            <div class="entry-thumbnail pull-left">
                                <img class="img-responsive" src="img/item03.jpg" alt="">
                                
                            </div>
                            <div class="media-body"> <!-- Content -->
                                <header class="entry-header">
                                    <div class="entry-date">01 May 2014</div>
                                    <h2 class="entry-title"><a href="#">BeReviews was a awesome envent in Hyd</a></h2>
                                </header>

                                <div class="entry-content">
                                    <P>We work in some of the world’s toughest situations where we offer bold.</P>
                                    <a class="btn btn-danger" href="#">Read More</a>
                                </div>

                                <footer class="entry-meta">
                                    <span class="entry-author"><i class="fa fa-pencil"></i> <a href="#">Campbell</a></span>
                                    <span class="entry-category"><i class="fa fa-folder-o"></i> <a href="#">Tutorial</a></span>
                                    <span class="entry-comments"><i class="fa fa-comments-o"></i> <a href="#">15</a></span>
                                </footer>
                            </div>
                            <!-- ./content -->
                        </article>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    <!-- /.container -->
    <%--<div class="modal fade" id="image-gallery" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="image-gallery-title">
                    </h4>
                </div>
                <div class="modal-body">
                    <img id="image-gallery-image" class="img-responsive" src="">
                </div>
                <div class="modal-footer">
                    <div class="col-md-2">
                        <button type="button" class="btn btn-primary" id="show-previous-image">
                            Previous</button>
                    </div>
                    <div class="col-md-8 text-justify" id="image-gallery-caption">
                        This text will be overwritten by jQuery
                    </div>
                    <div class="col-md-2">
                        <button type="button" id="show-next-image" class="btn btn-default">
                            Next</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>

