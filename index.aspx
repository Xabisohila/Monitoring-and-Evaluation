<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="preview_dotnet_templates_akshara_multi_master_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
#home { height: 100vh; }

/* ── Transparent navbar (index.aspx only) ─────────────────── */
.navbar-adaptive {
    background: transparent !important;
    background-image: none !important;
    border-color: transparent !important;
    box-shadow: none !important;
    transition: background .35s ease, border-color .35s ease, box-shadow .35s ease;
}

/* Over a DARK section — white text */
.navbar-adaptive.nav-over-dark .navbar-brand-text strong { color: #fff !important; }
.navbar-adaptive.nav-over-dark .navbar-brand-text small  { color: rgba(255,255,255,.75) !important; }
.navbar-adaptive.nav-over-dark .navbar-nav > li > a      { color: rgba(255,255,255,.9) !important; }
.navbar-adaptive.nav-over-dark .navbar-nav > li > a:hover { color: #fff !important; }
.navbar-adaptive.nav-over-dark .navbar-toggle .icon-bar  { background: #fff !important; }

/* Over a LIGHT section — dark text */
.navbar-adaptive.nav-over-light {
    background: rgba(255,255,255,.97) !important;
    border-color: rgba(0,0,0,.08) !important;
    box-shadow: 0 2px 10px rgba(0,0,0,.08) !important;
}
.navbar-adaptive.nav-over-light .navbar-brand-text strong { color: #0C2D48 !important; }
.navbar-adaptive.nav-over-light .navbar-brand-text small  { color: #556070 !important; }
.navbar-adaptive.nav-over-light .navbar-nav > li > a      { color: #0C2D48 !important; }
.navbar-adaptive.nav-over-light .navbar-nav > li > a:hover { color: #12826A !important; }
.navbar-adaptive.nav-over-light .navbar-toggle .icon-bar  { background: #0C2D48 !important; }

/* Smooth text colour transitions */
.navbar-brand-text strong,
.navbar-brand-text small { transition: color .3s ease; }

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

<div id="home" data-navbar-bg="dark">
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

<script>
(function () {
    var navbar = document.querySelector('.navbar');
    if (!navbar) return;

    navbar.classList.add('navbar-adaptive');

    function applyTheme(theme) {
        if (theme === 'light') {
            navbar.classList.remove('nav-over-dark');
            navbar.classList.add('nav-over-light');
        } else {
            navbar.classList.remove('nav-over-light');
            navbar.classList.add('nav-over-dark');
        }
    }

    var sections = document.querySelectorAll('[data-navbar-bg]');

    if ('IntersectionObserver' in window) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    applyTheme(entry.target.getAttribute('data-navbar-bg') || 'dark');
                }
            });
        }, { rootMargin: '-58px 0px -85% 0px', threshold: 0 });
        sections.forEach(function (s) { observer.observe(s); });
    } else {
        function onScroll() {
            var scrollY = window.scrollY || window.pageYOffset;
            var current = 'dark';
            sections.forEach(function (s) {
                if (s.offsetTop <= scrollY + 60) {
                    current = s.getAttribute('data-navbar-bg') || 'dark';
                }
            });
            applyTheme(current);
        }
        window.addEventListener('scroll', onScroll, { passive: true });
        onScroll();
    }

    if (sections.length) applyTheme(sections[0].getAttribute('data-navbar-bg') || 'dark');
}());
</script>

</asp:Content>
