using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;

public partial class Webparts_USERLOGIN :WebPartBase
{
    private string _box_css_name = null;//mean no box around
    private string _registration_url = "UserRegistration.aspx";
    private string _updateprofile_url = "UserUpdateProfile.aspx";
    #region webparts properties
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("1.Box css class name:")]
    [WebDescription("Set css class name of container box.")]
    /// <summary>
    /// set box css name to set box container if contains -title- then title of box will auto set
    /// </summary>
    public string p1_box_css_name
    {
        get
        {
            return _box_css_name;
        }
        set
        {
            _box_css_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("2.New user registration url:")]
    [WebDescription("Set new user registration page")]
    /// <summary>
    /// Set new user registration page
    /// </summary>
    public string p2_registration_url
    {
        get
        {
            return _registration_url;
        }
        set
        {
            _registration_url = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Update user profile url:")]
    [WebDescription("Set update user profile page")]
    /// <summary>
    /// set update user profile url
    /// </summary>
    public string p3_updateprofile_url
    {
        get
        {
            return _updateprofile_url;
        }
        set
        {
            _updateprofile_url = value;
        }
    }

    #endregion webparts properties
   
    public Webparts_USERLOGIN()
    {
        this.Title = "USER LOGIN";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _box_css_name, this.Page.User.Identity.IsAuthenticated ? this.Page.User.Identity.Name : LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(this.Title));
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litBoxTop.Text = sBoxTop;
                    this.litBoxBottom.Text = sBoxBottom;
                }
                else
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _box_css_name);
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litBoxTop.Text = sBoxTop;
                    this.litBoxBottom.Text = sBoxBottom;
                }
            }

            if (this.Page.User.Identity.IsAuthenticated)
            {
                divLogin.Visible = false;
                load_UserInfo();
                divUserInfo.Visible = true;                
                divChangePassword.Visible = false;
                divPasswordRecovery.Visible = false;
            }
            else
            {
                divLogin.Visible = true;
                divUserInfo.Visible = false;
                divChangePassword.Visible = false;
                divPasswordRecovery.Visible = false;
            }
        }
    }

    public void OnLoginError(object sender, EventArgs e)
    {
        Login1.PasswordRecoveryText = Resources.strings.ForgotYourPassword;
    }

    public void OnLoggedIn(object sender, EventArgs e)
    {
        divLogin.Visible = false;
        load_UserInfo();
        divUserInfo.Visible = true;
        divChangePassword.Visible = false;
        divPasswordRecovery.Visible = false;
    }

    public void load_UserInfo()
    {
        LegoWebSiteForum.Buslogic.User myUser;
        if (Page.User.Identity.IsAuthenticated)
        {
           MembershipUser currentUser=Membership.GetUser(Page.User.Identity.Name);
           if (currentUser == null) return;

           int iUserId = LegoWebSiteForum.Buslogic.ForumUsers.GetUserIDFromEmail(currentUser.Email);
            if (iUserId > 0)
            {
                string sUserInfo = "";
                myUser = LegoWebSiteForum.Buslogic.ForumUsers.GetUser(iUserId);
                sUserInfo += "<table width='100%'>";
                if (!String.IsNullOrEmpty(myUser.Avatar))
                {
                    sUserInfo += String.Format("<tr><td align='center'><img src='{0}' style='max-width:120px;max-height:160px'/></td></tr>", myUser.Avatar);
                }
                sUserInfo += String.Format("<tr><td align='center'><b>{0}</b></td></tr><tr><td align='center'>Số bài:{1}</td></tr>", myUser.Alias, myUser.PostCount.ToString());
                sUserInfo += "</table>";
                literalUser.Text=sUserInfo;
            }
            else
            {
                literalUser.Text = String.Format("<span>{0}</span>", Resources.strings.UserHasNotRegistredAsAForumMember);
            }
        }
    
    }
    protected void linkUserUpdateProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect(_updateprofile_url);
    }
    protected void linkUserChangePassword_Click(object sender, EventArgs e)
    {
        divLogin.Visible = false;        
        divUserInfo.Visible = false;
        divChangePassword.Visible = true;
        divPasswordRecovery.Visible = false;
    }
    protected void ChangePassword1_CancelButtonClick(object sender, EventArgs e)
    {
        divLogin.Visible = false;
        divUserInfo.Visible = true;
        divChangePassword.Visible = false;
        divPasswordRecovery.Visible = false;
    }
    protected void ChangePassword1_ContinueButtonClick(object sender, EventArgs e)
    {
        divLogin.Visible = false;
        divUserInfo.Visible = true;
        divChangePassword.Visible = false;
        divPasswordRecovery.Visible = false;
    }
    protected void linkPasswordRecovery_Click(object sender, EventArgs e)
    {
        divLogin.Visible = false;
        divUserInfo.Visible = false;
        divChangePassword.Visible = false;
        divPasswordRecovery.Visible = true;
    }
    protected void linkUserRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect(_registration_url);
    }
}
