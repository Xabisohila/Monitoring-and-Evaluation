<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="pu_UploadPMTDP.aspx.cs" Inherits="pu_UploadPMTDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <br /><br /><br />

    <div class="container">
        <div class="section-header">
            <div class="row section-title text-center">
                <br />
                <div class="row">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s"><span><strong>Cluster Setup </strong></span></h2>
                </div>
            </div>
        </div>



        <br />
        <br />




        <h3>Upload PMTDP Targets (CSV)</h3>
        <asp:Panel runat="server" CssClass="card">
            <asp:Label Text="Framework:" runat="server" AssociatedControlID="ddlFramework" />
            <asp:DropDownList ID="ddlFramework" runat="server" Width="360" />
            <br />
            <br />
            <asp:FileUpload ID="fuCsv" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload & Validate" CssClass="btn" OnClick="btnUpload_Click" />
            <asp:Button ID="btnCommit" runat="server" Text="Commit" CssClass="btn" Enabled="false" OnClick="btnCommit_Click" />
            <asp:Label ID="lblMsg" runat="server" CssClass="info" />
        </asp:Panel>
        <asp:GridView ID="gvValidation" runat="server" AutoGenerateColumns="true" />


    </div>

</asp:Content>

