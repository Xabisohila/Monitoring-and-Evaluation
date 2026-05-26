<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Carousel -->
    
		
<header id="home">
		<!-- Background Image -->
		<div class="bg-img" style="background-image: url('./img/background.jpg');">
			 <div class="overlay"></div>
		</div>
		<!-- /Background Image -->

		<!-- home wrapper -->
		<div class="home-wrapper">
			<div class="container">
				<div class="row">

					<!-- home content -->
					<div class="col-md-10 col-md-offset-1">
						<div class="home-content"><br /><br />
                            <h2 id="headerText" class="home-text" runat="server"> MONITORING AND EVALUATION </h2>
                            <%--<a class="glyphicon glyphicon-apple " href="services.aspx">Services</a>--%>
                       
						</div>
					</div>
					<!-- /home content -->

				</div>
			</div>
		</div>
		<!-- /home wrapper -->

	</header>
         
</asp:Content>

