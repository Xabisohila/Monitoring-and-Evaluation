<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <div class="padding100">
        <div class="container">
             <div class="section-header">
                <div class="row section-title text-center">
                
                    <br />
                    <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> Provincial M&E </strong> </span></h2>
                        
                     </div>
               
                </div>
              
            </div>
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-6 ">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Monitoring and Evaluation  - Users</div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                        <div id="loginform" class="form-horizontal" role="form">
                            
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtPersalNumber" CssClass="form-control" placeholder="Persal Number"></asp:TextBox>
                            </div>
                          
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" placeholder="Lastname"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlTitle" CssClass="form-control" ></asp:DropDownList>
                            </div>


                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlUserType" CssClass="form-control" ></asp:DropDownList>
                            </div>

                              <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                            </div>
                             <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                            </div>
                           
                            
                        </div>
                    </div>
                </div>
            </div>

            <div id="loginbox2" style="margin-top: 50px;" class="mainbox col-md-6 ">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Monitoring and Evaluation  - Contact Details</div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert2" class="alert alert-danger col-sm-12">
                        </div>
                        <div id="loginform2" class="form-horizontal" role="form">


     <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" placeholder="Email Address"></asp:TextBox>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Cellphone or Landline"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            
                             <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:TextBox runat="server" ID="txtDesignation" CssClass="form-control" placeholder="Designation"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlActivation" CssClass="form-control" ></asp:DropDownList>
                            </div>

                           
                           
                        </div>
                    </div>
           
    </div>
    </div>

            <div style="margin-top: 2px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:Button ID="btnRegister" runat="server" Text="Register User" OnClick="btnRegister_Click" CssClass="btn btn-success" />
                                    <br /><br />
                                </div>
                            </div>

            
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
