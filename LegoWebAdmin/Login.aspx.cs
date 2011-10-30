// ----------------------------------------------------------------------
// <copyright file="Login.aspx.cs" package="LEGOWEB">
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



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Login1.LoginButtonText = Resources.strings.Login_Text;
            this.Login1.RememberMeText = Resources.strings.RememberMeNextTime_Text;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "vi")
            {
                this.btnSelectEnglish.Visible = true;
                this.btnSelectVietnamese.Visible = false;
            }
            else
            {
                this.btnSelectEnglish.Visible = false;
                this.btnSelectVietnamese.Visible = true;
            }
        }
    }

    protected void en_Click(object sender, EventArgs e)
    {
        UrlQuery myURL = new UrlQuery(Request.Url.AbsoluteUri);
        myURL.Set("locale", "en-US");
        Response.Redirect(myURL.AbsoluteUri);
    }
    protected void vi_Click(object sender, EventArgs e)
    {
        UrlQuery myURL = new UrlQuery(Request.Url.AbsoluteUri);
        myURL.Set("locale", "vi-VN");
        Response.Redirect(myURL.AbsoluteUri);
    }

    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
