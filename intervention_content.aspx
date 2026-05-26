<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="intervention_content.aspx.cs" Inherits="intervention_content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="padding100">
        <div class="container">
            <div class="col-md-8">
            <div id="loginbox" style="margin-top: 50px;" class="col-md-12">
                 <asp:Label ID="lblError" runat="server" Text="" visible="false"  />
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Add Interventions and Indicators </div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                        <div id="loginform" class="form-horizontal" role="form">
                            <div style="margin-bottom: 25px" class="input-group">
                                
                            </div>

                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlCluster" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" ></asp:DropDownList>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlStrategicPriority" CssClass="form-control" ></asp:DropDownList>
                               
                            </div>

                            <div style="margin-bottom: 25px" class="input-group">
                               
                               <asp:LinkButton ID="lbAddStrategicPriority" runat="server" OnClick="lbAddStrategicPriority_Click" >Add Suboutcome </asp:LinkButton><br />
                               
                            </div>

                            <div style="margin-bottom: 25px" class="input-group"  runat="server" >
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlSuboutcome" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSuboutcome_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                              <div style="margin-bottom: 25px" class="input-group">
                               
                                <asp:LinkButton ID="lbAddIntervention" runat="server" OnClick="lbAddIntervention_Click" >Add Intervention</asp:LinkButton>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group"  runat="server" >
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlIntervention" CssClass="form-control" ></asp:DropDownList>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:TextBox runat="server" ID="txtKeyResultNo" placeholder ="Indicator No." CssClass="form-control" Visible="False" />
                                </div>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:TextBox runat="server" ID="txtKeyIndicator" placeholder ="Key Indicator" CssClass="form-control"/>
                                </div>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:TextBox runat="server" ID="txtResponsibleInstitution" placeholder ="Responsible Institution" CssClass="form-control"/>
                                </div>
                            </div>

                           <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

                
           </div>
            <div  class="col-md-2">
                 <br /> <br /> <br />
                <asp:Label ID="lblAdd" runat="server" visible="false" Font-Bold="true" Font-Underline="true"/> 
                
                <br />
                
             <asp:TextBox runat="server" ID="txtAdd" placeholder ="Additional Information" Visible="false" CssClass="form-control" Height="200px" Width="200px" TextMode="MultiLine"/>
                <br />
                <asp:Button ID="btnSubmitAdd" runat="server" Text="Submit" OnClick="btnSubmitAdd_Click" CssClass="btn btn-success" Visible="false" />
            <div class="row">
             </div>
           
    </div>
    </div>


        
    </div>
</asp:Content>
