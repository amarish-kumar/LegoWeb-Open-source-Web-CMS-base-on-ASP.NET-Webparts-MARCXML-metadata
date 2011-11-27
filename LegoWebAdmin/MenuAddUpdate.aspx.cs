// ----------------------------------------------------------------------
// <copyright file="MenuAddUpdate.aspx.cs" package="LEGOWEB">
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



public partial class LegoWebAdmin_MenuAddUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='Bạn không có quyền truy cập vào tính năng này!'");
            }
        }
    }

    protected void linkSaveButton_Click(object sender, EventArgs e)
    {
        this.MenuAddUpdate1.Save_MenuRecord();
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        this.MenuAddUpdate1.Cancel_Click();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
