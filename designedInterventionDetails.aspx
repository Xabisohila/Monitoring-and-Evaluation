<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="designedInterventionDetails.aspx.cs" Inherits="designedInterventionDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .detail-container { max-width: 960px; margin: 0 auto; padding: 12px 20px; background: #fff; border: 1px solid #ddd; border-radius: 4px; }
        .detail-header { border-bottom: 1px solid #eee; margin-bottom: 12px; }
        .detail-header h2 { margin: 0; font-size: 22px; }
        .detail-grid { display: grid; grid-template-columns: 180px 1fr; row-gap: 6px; column-gap: 18px; }
        .detail-label { font-weight: 600; }
        .status-badge { display:inline-block; padding:2px 8px; border-radius:12px; background:#0078d4; color:#fff; font-size:12px; }
        .error { color:#b00020; margin-top:12px; }
        .empty { color:#666; font-style:italic; }
        .actions { margin-top:18px; }
        .actions a, .actions button { margin-right:8px; }
        .loading { font-style:italic; color:#555; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />

    <br /> <br /> <br /> <br /> <br />



    <asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>
            <div class="detail-container">
                <div class="detail-header">
                    <h2>
                        <asp:Label ID="lblTitle" runat="server" Text="Intervention Details"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="status-badge" Visible="false"></asp:Label>
                    </h2>
                    <asp:Label ID="lblId" runat="server" CssClass="small" />
                </div>

                <asp:Panel ID="pnlLoading" runat="server" Visible="false">
                    <div class="loading">Loading intervention...</div>
                </asp:Panel>

                <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                    <div class="detail-grid">
                        <div class="detail-label">Title</div>
                        <div><asp:Label ID="valTitle" runat="server" /></div>

                        <div class="detail-label">Description</div>
                        <div><asp:Literal ID="valDescription" runat="server" /></div>

                        <div class="detail-label">Start Date</div>
                        <div><asp:Label ID="valStartDate" runat="server" /></div>

                        <div class="detail-label">End Date</div>
                        <div><asp:Label ID="valEndDate" runat="server" /></div>

                        <div class="detail-label">Status</div>
                        <div><asp:Label ID="valStatus" runat="server" /></div>

                        <div class="detail-label">Last Updated</div>
                        <div><asp:Label ID="valLastUpdated" runat="server" /></div>
                    </div>

                    <div class="actions">
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn" OnClick="btnRefresh_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                    <div class="empty">No intervention found for the provided id.</div>
                </asp:Panel>

                <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script runat="server">
        
    </script>
</asp:Content>
