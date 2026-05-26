<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="ClusterWG.aspx.cs" Inherits="ClusterWG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section id="services" class="padding50"> 
        <div class="container">
          <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">
                
                    <br /><br /><br /><br /><br /><br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> Cluster Working Groups </strong> </span></h2>
                     </div>
               
                </div>
               
            </div>
              <!-- ./Heading -->           
            <div class="row">  
                <!-- Box-1 -->  
                <div class="col-md-3 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
                    <a href="selection.aspx?&cluster=1" class="portfolio-link" >
                        
                        <img src="img/portfolio/item01.jpg" class="img-responsive" alt="">
                    </a>
                </div>
                <!-- Box-2 -->
                <div class="col-md-3 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
                    <a href="selection.aspx?&cluster=2" class="portfolio-link" >
                        
                        <img src="img/portfolio/item02.jpg" class="img-responsive" alt="">
                    </a>
                </div>
                <!-- Box-3 -->
                <div class="col-md-3 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
                    <a href="selection.aspx?&cluster=3" class="portfolio-link" >
                       
                        <img src="img/portfolio/item03.jpg" class="img-responsive" alt="">
                    </a>
                    
                </div>
                <!-- Box-4 -->
                <div class="col-md-3 col-sm-6 portfolio-item wow zoomIn animated" data-wow-duration="1500ms" data-wow-delay="100ms">
    <a href="selection.aspx?&cluster=4" class="portfolio-link" >
       
        <img src="img/portfolio/item04.jpg" class="img-responsive" alt="">
    </a>
    
</div>
       
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

