<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="ReportSummary.aspx.cs" Inherits="ReportSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <div class="padding100">
        <div class="container">
     <div class="col-md-12">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong> Reports Summary</strong> </span></h2>
      </div>


            <div id="loginbox" style="margin-top: 50px;" class="col-md-4">
                 <asp:Label ID="lblError" runat="server" Text="" visible="false"  />
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                           Report Evidence with Interventions and Indicators </div>
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
                                <asp:DropDownList runat="server" ID="ddlStrategicPriority" CssClass="form-control"  ></asp:DropDownList>
                            </div>

                            

                            <div style="margin-bottom: 25px" class="input-group"  runat="server" >
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlSuboutcome" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSuboutcome_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div style="margin-bottom: 25px" class="input-group"  runat="server" >
                                <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                                <asp:DropDownList runat="server" ID="ddlIntervention" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIntervention_SelectedIndexChanged" ></asp:DropDownList>
                            </div>

                            
                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:DropDownList runat="server" ID="ddlIndicator" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIndicator_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"/>
                                </div>
                            </div>

                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:TextBox runat="server" ID="txtFileName" Visible="false" placeholder ="File Name (100 characters (max)" TextMode="MultiLine" CssClass="form-control" MaxLength="100"/>
                                </div>
                            </div>

                           <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" Visible="false" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <div  class="col-md-8">
                <AjaxControlToolkit:BarChart ID="bcKeyIndicator" runat="server" ChartHeight="300" ChartWidth="720" Visible="false"
                            
                            ChartType="Column" ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9"
                            ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" Width="100%" Height="321px">
                        </AjaxControlToolkit:BarChart>    
                
                <br />
                <asp:GridView ID="gvDownload" runat="server" AllowSorting="True" 
                     AutoGenerateColumns="False" WizardCustomPager="False"  BorderColor="#CC3300" CellPadding="4" 
                             AllowPaging="True" ForeColor="#333333" GridLines="None" PageSize="10" Width="443px">
                            <AlternatingRowStyle BackColor="White" Width="100%" />
                     <Columns>
                     <asp:TemplateField HeaderText="Download Evidence">
                     <HeaderStyle HorizontalAlign="Left" Width="250px"  BackColor ="Brown" />
                     <ItemStyle HorizontalAlign="left" Width="250px" Height="50px"/>
                     <ItemTemplate>
                     <asp:Label  ID="lblFilePath" runat="server" Text= '<%# Bind("FilePath") %>' visible="false" />
                     <asp:Label  ID="lblEvidenceId" runat="server" Text= '<%# Bind("EvidenceId") %>' visible="false" />
                         <asp:LinkButton Font-Bold="true" ID="lnkDownload" runat="server" Text= '<%# Bind("FileName") %>' OnClick="lnkDownload_Click"/>
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
            <div class="row">
             </div>
           
    </div>
    </div>
</asp:Content>
