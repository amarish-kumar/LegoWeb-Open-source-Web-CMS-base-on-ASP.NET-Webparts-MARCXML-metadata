// ----------------------------------------------------------------------
// <copyright file="UserManager.aspx.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class LegoWebAdmin_UserManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized to access this page!'");
            }
        }
    }

    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.UserManager1.Remove_SelectedUsers();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.UserManager1.Edit_SelectedUser();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAddNew.aspx");
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
