<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master"
    AutoEventWireup="true" CodeFile="page1selection.aspx.cs" Inherits="page1selection" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        input[type=checkbox], input[type=radio] {
            margin: 0px 8px 0px 0px;
            /*margin-top: 1px\9;*/
            line-height: normal;
        }
    </style>


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
                                
                                <%--<asp:RadioButton ID="rbPlanning1" runat="server" GroupName="MorE" Text="Planning" OnCheckedChanged="rbPlanning_CheckedChanged"  AutoPostBack="true" style="padding-right: 45px;"/>
                                <asp:RadioButton ID="rbMonitoring1" runat="server" GroupName="MorE" Text="Monitoring" OnCheckedChanged="rbMonitoring_CheckedChanged"  AutoPostBack="true"/>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlWorkingGroup1" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlStrategicPriority1" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList runat="server" ID="ddlFinancialYear1" CssClass="form-control" ></asp:DropDownList>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group" id="divQuarter" runat="server" visible="false">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList runat="server" ID="ddlQuarter1" CssClass="form-control" ></asp:DropDownList>
                            </div>--%>




                                <asp:RadioButton ID="rbPlanning" runat="server" GroupName="MorE" Text="Planning" OnCheckedChanged="rbPlanning_CheckedChanged" AutoPostBack="true" style="padding-right: 45px;" />
                                <asp:RadioButton ID="rbMonitoring" runat="server" GroupName="MorE" Text="Monitoring" OnCheckedChanged="rbMonitoring_CheckedChanged" AutoPostBack="true" />
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                               <asp:DropDownList ID="ddlWorkingGroup" runat="server" CssClass="form-control" />
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control"/>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server"  CssClass="form-control"/>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group" id="divQuarter" runat="server" visible="false">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList ID="ddlQuarter" runat="server" Visible="true" CssClass="form-control"/>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group" id="div1" runat="server" visible="false">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                <asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" />
                            </div>














                            <%--<asp:DropDownList ID="ddlCluster" runat="server" CssClass="form-control"/>--%>
                                
                                
                                
                                
                                

                                <asp:PlaceHolder ID="POAContainer" runat="server" />














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
