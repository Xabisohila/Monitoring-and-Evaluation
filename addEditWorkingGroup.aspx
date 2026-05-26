<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="addEditWorkingGroup.aspx.cs" Inherits="addEditWorkingGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

        <style type="text/css">
    /* Basic inline CSS for readability. Consider moving this to an external.css file. */
    /*body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; color: #333; }*/
   /*.container { max-width: 800px; margin: auto; padding: 25px; background-color: #fff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }*/
    h1 { color: #0056b3; margin-bottom: 20px; }
   .form-group { margin-bottom: 15px; }
   .form-group label { display: block; margin-bottom: 5px; font-weight: bold; color: #555; }
   .form-group input[type="text"],
   .form-group textarea,
   .form-group select {
        width: calc(100% - 22px); /* Account for padding and border */
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 1em;
    }
   .form-group textarea { resize: vertical; min-height: 80px; }
   .asp-button { padding: 10px 20px; background-color: #28a745; color: white; border: none; border-radius: 5px; cursor: pointer; font-size: 1em; margin-right: 10px; }
   .asp-button:hover { background-color: #218838; }
   .validation-message { color: red; font-size: 0.9em; margin-top: 5px; display: block; }
   .validation-summary { color: red; border: 1px solid red; padding: 10px; margin-bottom: 20px; background-color: #ffe6e6; border-radius: 5px; }
   .success-message { color: green; border: 1px solid green; padding: 10px; margin-bottom: 20px; background-color: #e6ffe6; border-radius: 5px; }
   .back-link { display: block; margin-top: 20px; text-align: center; }
   .back-link a { color: #007bff; text-decoration: none; font-weight: bold; }
   .back-link a:hover { text-decoration: underline; }
</style>


    <br /><br /><br /><br />
    <div class="container">

    <div>
        <%--<h2>Add/Edit Working Group</h2>--%>

        <div class="section-title text-center">
            <br />
            <div>
                <h2 class="background double animated wow fadeInUp color1" style="color: #000000;" data-wow-delay="0.2s"><span><strong>Add Working Group</strong></span></h2>
            </div>
        </div>

        <br />








        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

        <div class="form-group">
            <asp:Label ID="lblCluster" runat="server" Text="Select Cluster:" />
            <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfvCluster" runat="server" ControlToValidate="ddlCluster"
                InitialValue="0" ErrorMessage="Cluster is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblLeadInstitution" runat="server" Text="Lead Institution:" />
            <asp:DropDownList ID="ddlLeadInstitution" runat="server" />
            <asp:RequiredFieldValidator ID="rfvLeadInstitution" runat="server" ControlToValidate="ddlLeadInstitution"
                InitialValue="0" ErrorMessage="Lead Institution is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblWGName" runat="server" Text="Working Group Name:" />
            <asp:TextBox ID="txtWGName" runat="server" />
            <asp:RequiredFieldValidator ID="rfvWGName" runat="server" ControlToValidate="txtWGName"
                ErrorMessage="Working Group name is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblWGDescription" runat="server" Text="Description:" />
            <asp:TextBox ID="txtWGDescription" runat="server" TextMode="MultiLine" Rows="4" Columns="50" />
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="asp-button" />
    </div>


    <br />
        </div>







</asp:Content>

