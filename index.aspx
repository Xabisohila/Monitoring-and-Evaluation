<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="preview_dotnet_templates_akshara_multi_master_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Carousel -->

    <div id="home">
		<!-- Background Image -->
		<div class="bg-img" style="background-image: url('./img/background.jpg');">
			 <div class="overlay">
            <div class="home-wrapper">
                <h2 class="home-text">MONITORING AND EVALUATION</h2>
                <p class="home-subtext" id="userText" runat="server">Provincial Monitoring and Evaluation System</p> <%-- Performance --%>
        <asp:Label ID="Label1" CssClass="home-text" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="Label2" CssClass="home-text" runat="server" Text=""></asp:Label>
             </div>
                 </div>
		</div>
  </div>

 

   
</asp:Content>

