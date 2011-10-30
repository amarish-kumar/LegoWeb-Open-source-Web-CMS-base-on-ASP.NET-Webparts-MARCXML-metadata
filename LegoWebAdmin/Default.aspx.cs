// ----------------------------------------------------------------------
// <copyright file="Default.aspx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
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



public partial class LegoWebAdmin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("task", null) != null)
            {
                if (CommonUtility.GetInitialValue("task", "").ToString().ToLower() == "logout")
                {
                    FormsAuthentication.SignOut();
                    Roles.DeleteCookie();
                    Session.Clear();
                    Response.Redirect("~/");
                }
            }
            Response.Redirect("ControlPanel.aspx");
        }
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
