<%@ Page Title="" Language="C#" MasterPageFile="~/Login.master"
    AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="preview_dotnet_templates_akshara_multi_master_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>
    /* ── Login page overrides ── */
    body { background: #f0f2f5; }

    .login-page-wrap {
        min-height: calc(100vh - 118px);
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 40px 15px;
    }

    .login-card {
        background: #fff;
        border-radius: 10px;
        box-shadow: 0 8px 32px rgba(0,0,0,0.12);
        width: 100%;
        max-width: 420px;
        overflow: hidden;
    }

    .login-card-header {
        background: linear-gradient(135deg, #038C13 0%, #026b0f 100%);
        padding: 32px 36px 28px;
        text-align: center;
    }

    .login-card-header img {
        height: 56px;
        margin-bottom: 14px;
        display: block;
        margin-left: auto;
        margin-right: auto;
    }

    .login-card-header h1 {
        color: #fff;
        font-size: 20px;
        font-weight: 700;
        margin: 0 0 4px;
        letter-spacing: 0.3px;
    }

    .login-card-header p {
        color: rgba(255,255,255,0.82);
        font-size: 13px;
        margin: 0;
    }

    .login-card-body {
        padding: 32px 36px 36px;
    }

    .login-error-bar {
        background: #fdf3f3;
        border: 1px solid #f5c6cb;
        border-left: 4px solid #dc3545;
        border-radius: 4px;
        color: #721c24;
        font-size: 13.5px;
        padding: 10px 14px;
        margin-bottom: 20px;
    }

    .login-field-label {
        font-size: 12.5px;
        font-weight: 600;
        color: #444;
        text-transform: uppercase;
        letter-spacing: 0.5px;
        margin-bottom: 6px;
        display: block;
    }

    .login-input-wrap {
        position: relative;
        margin-bottom: 20px;
    }

    .login-input-wrap .field-icon {
        position: absolute;
        left: 13px;
        top: 50%;
        transform: translateY(-50%);
        color: #aaa;
        font-size: 15px;
        pointer-events: none;
    }

    .login-input-wrap .form-control {
        border: 1.5px solid #dde2e8;
        border-radius: 6px;
        height: 44px;
        padding-left: 38px;
        font-size: 14.5px;
        color: #333;
        transition: border-color 0.2s, box-shadow 0.2s;
        box-shadow: none;
    }

    .login-input-wrap .form-control:focus {
        border-color: #038C13;
        box-shadow: 0 0 0 3px rgba(3,140,19,0.12);
        outline: none;
    }

    .login-input-wrap .form-control:focus + .field-icon,
    .login-input-wrap:focus-within .field-icon {
        color: #038C13;
    }

    .btn-login {
        width: 100%;
        height: 46px;
        background: #038C13;
        color: #fff;
        font-size: 15px;
        font-weight: 600;
        border: none;
        border-radius: 6px;
        letter-spacing: 0.4px;
        margin-top: 6px;
        transition: background 0.2s, transform 0.1s;
        cursor: pointer;
    }

    .btn-login:hover {
        background: #026b0f;
        color: #fff;
    }

    .btn-login:active {
        transform: scale(0.98);
    }

    .login-footer-note {
        text-align: center;
        margin-top: 20px;
        font-size: 12px;
        color: #999;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="login-page-wrap">
        <div class="login-card">

            <div class="login-card-header">
                <img src="img/OTPlogo.png" alt="Office of the Premier" />
                <h1>Provincial M&amp;E System</h1>
                <p>Monitoring &amp; Evaluation Planning Portal</p>
            </div>

            <div class="login-card-body">

                <asp:Label ID="lblError" runat="server" CssClass="login-error-bar"
                    Visible="false" />

                <label class="login-field-label" for="<%= txtPersalNumber.ClientID %>">Persal Number</label>
                <div class="login-input-wrap">
                    <asp:TextBox runat="server" ID="txtPersalNumber" CssClass="form-control"
                        placeholder="Enter your Persal number" autocomplete="username" />
                    <i class="glyphicon glyphicon-user field-icon"></i>
                </div>

                <label class="login-field-label" for="<%= txtPassword.ClientID %>">Password</label>
                <div class="login-input-wrap">
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"
                        placeholder="Enter your password" TextMode="Password" autocomplete="current-password" />
                    <i class="glyphicon glyphicon-lock field-icon"></i>
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Sign In"
                    OnClick="btnLogin_Click" CssClass="btn btn-login" UseSubmitBehavior="true" />

                <p class="login-footer-note">
                    Office of the Premier &mdash; EC
                </p>
            </div>

        </div>
    </div>

</asp:Content>
