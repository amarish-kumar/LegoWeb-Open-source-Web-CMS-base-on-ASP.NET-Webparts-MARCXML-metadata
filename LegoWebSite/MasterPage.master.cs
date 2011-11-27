using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)        
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "vi")
            {
                this.changeLanguage_link.Text = "English";
            }
            else
            {
                this.changeLanguage_link.Text = "Tiếng Việt";
            }
            LoginStatus1.LoginText = Resources.strings.Login;
            LoginStatus1.LogoutText = Resources.strings.Logout;
            loginPopupLink.Text = Resources.strings.Login;
            Login1.LoginButtonText = Resources.strings.Login;
            Login1.UserNameLabelText = Resources.strings.UserName;
            Login1.PasswordLabelText = Resources.strings.Password;
            Login1.RememberMeText = Resources.strings.RememberMe;

            HtmlMeta metaDesc = new HtmlMeta();
            metaDesc.Name = "description";
            metaDesc.Content = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("META_DESCRIPTION");
            Page.Header.Controls.Add(metaDesc);

            HtmlMeta metaKey = new HtmlMeta();
            metaKey.Name = "keywords";
            metaKey.Content = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("META_KEYWORDS");
            Page.Header.Controls.Add(metaKey);

        }
        if (this.Page.User.Identity.IsAuthenticated)
        {
            this.LoginStatus1.Visible = true;
            this.loginPopupLink.Visible = false;            
        }
        else
        {
            this.LoginStatus1.Visible = false;
            this.loginPopupLink.Visible = true;
        }
    }

    protected void changeLanguage_link_Click(object sender, EventArgs e)
    {
        String sCurrentURL = Request.Url.PathAndQuery;
        if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "vi")
        {
            sCurrentURL = sCurrentURL.IndexOf("locale") > 0 ? sCurrentURL.Remove(sCurrentURL.IndexOf("locale") - 1) : sCurrentURL;
            Response.Redirect(sCurrentURL.IndexOf("?") > 0 ? sCurrentURL + "&locale=en-US" : sCurrentURL + "?locale=en-US");
        }
        else
        {
            sCurrentURL = sCurrentURL.IndexOf("locale") > 0 ? sCurrentURL.Remove(sCurrentURL.IndexOf("locale") - 1) : sCurrentURL;
            Response.Redirect(sCurrentURL.IndexOf("?") > 0 ? sCurrentURL + "&locale=vi-VN" : sCurrentURL + "?locale=vi-VN");
        }
    }
}
