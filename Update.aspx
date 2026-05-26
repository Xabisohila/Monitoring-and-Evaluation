<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Update" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />


    <section id="services" class="padding50">
        <div class="container">
            <!-- Heading -->
            <div class="section-header">
                <div class="row section-title text-center">

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Update</strong></span></h2>

                    </div>

                </div>
                <div class="row">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" Text="PMTDP PRIORITY : " ToolTip="Strategic Priority " />
                    <asp:Label ID="lblStrategicPriority" runat="server" /><br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="PDPG : " Font-Size="Large" ToolTip="Provincial Development Plan Goal " /><asp:Label ID="lblPDPGoal" runat="server" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Cluster Working Group : " Font-Size="Large" /><asp:Label ID="lblClusterWGName" runat="server" />
                    ,
                    <asp:Label ID="lblFinancialYear" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Visible="false" />
                    <br />
                </div>
            </div>
            <!-- ./Heading -->
        </div>
        <div class="container">
            <div class="row">

                <div runat="server" Visible="false">

                <asp:Label ID="lbl_Intervention" runat="server" Text="Intervention: "></asp:Label> <asp:TextBox ID="txt_Intervention" runat="server"></asp:TextBox> <asp:Label ID="txt_Intervention1" runat="server" Text=""></asp:Label><br /><br />

                 <%--<asp:Label ID="Label18" runat="server" Text="Aobakwe Moeng" style="font-size:200px;"></asp:Label>--%><br />
                
                <asp:Label ID="Label4" runat="server" Text="Baseline: "></asp:Label><asp:Label ID="Label5" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label6" runat="server" Text="Annual Target: "></asp:Label><asp:Label ID="Label7" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label8" runat="server" Text="Q1: "></asp:Label><asp:Label ID="Label9" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label10" runat="server" Text="Q2: "></asp:Label><asp:Label ID="Label11" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label12" runat="server" Text="Q3: "></asp:Label><asp:Label ID="Label13" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label14" runat="server" Text="Q4: "></asp:Label><asp:Label ID="Label15" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="Label16" runat="server" Text="Annual Archive: "></asp:Label><asp:Label ID="Label17" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="lbl_FinancialYearId" runat="server" Text="Financial Year ID: "></asp:Label><asp:Label ID="txt_FinancialYearId" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="lbl_KeyResultId" runat="server" Text="Key Result Id: "></asp:Label><asp:Label ID="txt_KeyResultId" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="lbl_Total" runat="server" Text="Total: "></asp:Label><asp:Label ID="txt_Total" runat="server" Text=""></asp:Label><br /><br />

                <asp:Label ID="lbl_baseline" runat="server" Text="Label"></asp:Label>

                    </div>

                <br /><br />

                <div class="form-container">
                    <label for="txt_Baseline">Baseline:</label>
                    <asp:TextBox ID="txt_Baseline" runat="server"></asp:TextBox>

                    <label for="txt_AnnualTarget">Annual Target</label>
                    <asp:TextBox ID="txt_AnnualTarget" runat="server"></asp:TextBox>

                    <label for="txt_Q1">Quater 1</label>
                    <asp:TextBox ID="txt_Q1" runat="server" TextMode="Number"></asp:TextBox>

                    <label for="txt_Q2">Quater 2</label>
                    <asp:TextBox ID="txt_Q2" runat="server" TextMode="Number"></asp:TextBox>

                    <label for="txt_Q3">Quater 3</label>
                    <asp:TextBox ID="txt_Q3" runat="server" TextMode="Number"></asp:TextBox>

                    <label for="txt_Q4">Quater 4</label>
                    <asp:TextBox ID="txt_Q4" runat="server" TextMode="Number"></asp:TextBox>



                    <div style="margin-top: 10px" class="form-group">
                        <!-- Button -->
                        <div class="col-sm-12 controls">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>
                    </div>

                    <br />
                    <br />

                    <div runat="server" Visible="false">


                    <label for="txt_Resp_Institution">Responsible Institution</label>
                    <asp:TextBox ID="txt_Resp_Institution" runat="server" TextMode="Number"></asp:TextBox>

                    <label for="ddlOptions">Options</label>
                    <%--<asp:DropDownList ID="ddlOptions1" runat="server">
                        <asp:ListItem Text="Option 1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Option 2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Option 3" Value="3"></asp:ListItem>
                    </asp:DropDownList>--%>




                    <div style="margin-bottom: 25px" class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>
                        <asp:DropDownList runat="server" ID="ddlOptions" CssClass="form-control" style="height:40px;">
                            <asp:ListItem Text="Option 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Option 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Option 3" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

</div>




                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                </div>

            </div>
        </div>
        
    </section>







    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 30px;
            background-color: #f9f9f9;
        }
        .form-container {
            width: 400px;
            /*width: 100%;*/
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background-color: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form-container label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
            color: #333;
        }
        .form-container input[type="text"], 
        .form-container input[type="number"],
        .form-container select {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-container button {
            width: 100%;
            padding: 10px;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
        }
        .form-container button:hover {
            background-color: #0056b3;
        }
    </style>


    

</asp:Content>

