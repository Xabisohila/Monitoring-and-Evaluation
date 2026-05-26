<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="selection.aspx.cs" Inherits="Selection" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="padding100">
        <div class="container">
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
                 <asp:Label ID="lblError" runat="server" Text="" visible="false"  />
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Select Strategic Priority and Period </div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                        <div id="loginform" class="form-horizontal" role="form">
                            <div style="margin-bottom: 25px" class="input-group">
                                
                                <asp:RadioButton ID="rbPlanning" runat="server" GroupName="MorE" Text="Planning" OnCheckedChanged="rbPlanning_CheckedChanged"  AutoPostBack="true"/>
                                <asp:RadioButton ID="rbMonitoring" runat="server" GroupName="MorE" Text="Monitoring" OnCheckedChanged="rbMonitoring_CheckedChanged"  AutoPostBack="true"/>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlWorkingGroup" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlStrategicPriority" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList runat="server" ID="ddlFinancialYear" CssClass="form-control" ></asp:DropDownList>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group" id="divQuarter" runat="server" visible="false">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList runat="server" ID="ddlQuarter" CssClass="form-control" ></asp:DropDownList>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
    </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
