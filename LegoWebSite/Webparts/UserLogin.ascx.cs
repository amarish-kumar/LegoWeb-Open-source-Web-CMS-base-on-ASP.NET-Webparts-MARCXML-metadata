using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;

public partial class Webparts_UserLogin :WebPartBase
{
    private string _registration_url = "UserRegistration.aspx";
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Poll Id
    /// </summary>
    public string registration_url
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

    private string _updateprofile_url = "UserUpdateProfile.aspx";
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Poll Id
    /// </summary>
    public string updateprofile_url
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

    public Webparts_UserLogin()
    {
        this.Title = "USER LOGIN - ĐĂNG NHẬP";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        Login1.PasswordRecoveryText = "Quên mật khẩu?";
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
        //Forum.Buslogic.User myUser;
        //if (Page.User.Identity.IsAuthenticated)
        //{
        //   MembershipUser currentUser=Membership.GetUser(Page.User.Identity.Name);
        //   if (currentUser == null) return;

        //    int iUserId = Forum.Buslogic.ForumUsers.GetUserIDFromEmail(currentUser.Email);
        //    if (iUserId > 0)
        //    {
        //        string sUserInfo = "";
        //        myUser = Forum.Buslogic.ForumUsers.GetUser(iUserId);
        //        sUserInfo += "<table width='100%'>";
        //        if (!String.IsNullOrEmpty(myUser.Avatar))
        //        {
        //            sUserInfo += String.Format("<tr><td align='center'><img src='{0}' style='max-width:120px;max-height:160px'/></td></tr>", myUser.Avatar);
        //        }
        //        sUserInfo += String.Format("<tr><td align='center'><b>{0}</b></td></tr><tr><td align='center'>Số bài:{1}</td></tr>", myUser.Alias, myUser.PostCount.ToString());
        //        sUserInfo += "</table>";
        //        literalUser.Text=sUserInfo;
        //    }
        //    else
        //    {
        //        literalUser.Text = "<span>Người dùng không tồn tại trong diễn đàn</span>";
        //    }
        //}
    
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
