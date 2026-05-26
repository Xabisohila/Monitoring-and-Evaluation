using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class BasePage : System.Web.UI.Page
{
    protected int CurrentUserID
    {
        get { return (Session["UserID"] == null) ? 0 : Convert.ToInt32(Session["UserID"]); }
    }
    protected int CurrentRoleID
    {
        get { return (Session["RoleID"] == null) ? 0 : Convert.ToInt32(Session["RoleID"]); }
    }
    protected string CurrentUserName
    {
        get { return (Session["FullName"] ?? "").ToString(); }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (CurrentUserID <= 0)
        {
            Response.Redirect("~/Login.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
        }
    }

    protected void RequireRole(params int[] allowedRoles)
    {
        if (allowedRoles == null || allowedRoles.Length == 0) return;
        foreach (var r in allowedRoles)
        {
            if (r == CurrentRoleID) return;
        }
        Response.Redirect("~/Unauthorized.aspx");
    }
}