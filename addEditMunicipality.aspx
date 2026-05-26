<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addEditMunicipality.aspx.cs" Inherits="addEditMunicipality" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />




    <div>
        <h2>Municipality Management</h2>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

        <div>
            <asp:Label ID="lblDistrict" runat="server" Text="Select District:" />
            <asp:DropDownList ID="ddlDistrict" runat="server" />
            <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict"
                InitialValue="0" ErrorMessage="District is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div>
            <asp:Label ID="lblMunicipalityName" runat="server" Text="Municipality Name:" />
            <asp:TextBox ID="txtMunicipalityName" runat="server" />
            <asp:RequiredFieldValidator ID="rfvMunicipalityName" runat="server" ControlToValidate="txtMunicipalityName"
                ErrorMessage="Municipality name is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="false" />
    </div>




</asp:Content>

