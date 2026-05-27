<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="preview_dotnet_templates_akshara_multi_master_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
#home { height: 100vh; }

/* Subtitle line above the main heading */
.home-subtext {
    color: rgba(255,255,255,.7);
    font-size: 13px;
    font-weight: 600;
    letter-spacing: 3px;
    text-transform: uppercase;
    margin: 0 0 14px;
}

/* Thin divider between heading block and welcome block */
.hero-divider {
    width: 56px;
    height: 3px;
    background: rgba(255,255,255,.35);
    border-radius: 2px;
    margin: 22px auto 26px;
}

/* "Welcome, Full Name" line */
.hero-welcome {
    display: block;
    color: #fff;
    font-size: 22px;
    font-weight: 300;
    margin-bottom: 12px;
}
.hero-welcome strong { font-weight: 700; }

/* Role badge pill */
.hero-role {
    display: inline-block;
    padding: 5px 18px;
    border-radius: 20px;
    background: rgba(255,255,255,.15);
    border: 1px solid rgba(255,255,255,.3);
    color: rgba(255,255,255,.9);
    font-size: 12px;
    font-weight: 700;
    letter-spacing: .8px;
    text-transform: uppercase;
    margin-bottom: 32px;
}

/* CTA button */
.hero-cta {
    display: inline-block;
    padding: 11px 30px;
    background: #0b5ed7;
    color: #fff;
    font-size: 14px;
    font-weight: 700;
    border-radius: 8px;
    text-decoration: none;
    letter-spacing: .3px;
    transition: background .2s;
}
.hero-cta:hover,
.hero-cta:focus { background: #094db0; color: #fff; text-decoration: none; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="home">
    <div class="bg-img" style="background-image: url('./img/background.jpg');">
        <div class="overlay">
            <div class="home-wrapper">
                <p class="home-subtext">Provincial Monitoring and Evaluation System</p>
                <h2 class="home-text">MONITORING AND EVALUATION</h2>
                <div class="hero-divider"></div>
                <asp:Label ID="Label1" CssClass="hero-welcome" runat="server" Text="" />
                <asp:Label ID="Label2" CssClass="hero-role"    runat="server" Text="" />
                <br />
                <asp:HyperLink ID="hlDashboard" runat="server" CssClass="hero-cta">
                    Go to Dashboard &#8594;
                </asp:HyperLink>
            </div>
        </div>
    </div>
</div>

</asp:Content>
