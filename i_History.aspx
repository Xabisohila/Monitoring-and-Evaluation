<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_History.aspx.cs" Inherits="i_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">







            <h2>Workflow History</h2>
            <asp:GridView runat="server" ID="gvHistory" AutoGenerateColumns="true">
                <Columns>
                    <asp:BoundField DataField="Stage" HeaderText="Stage" />
                    <asp:BoundField DataField="StatusID" HeaderText="StatusID" />
                    <asp:BoundField DataField="ActionByUserID" HeaderText="UserID" />
                    <asp:BoundField DataField="ActionDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    <asp:BoundField DataField="Comments" HeaderText="Comments" />
                </Columns>
            </asp:GridView>
    
    





</asp:Content>

