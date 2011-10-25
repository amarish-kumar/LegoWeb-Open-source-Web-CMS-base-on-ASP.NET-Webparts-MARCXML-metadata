using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LgwUserControls_AdminMenuBarDeactive : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
}
