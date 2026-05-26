using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for c_User
/// </summary>
public class c_User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public object DepartmentID { get; internal set; } // Optional, can be null
    public object EntityID { get; internal set; } // Optional, can be null
    public object RoleID { get; internal set; } // Optional, can be null
}