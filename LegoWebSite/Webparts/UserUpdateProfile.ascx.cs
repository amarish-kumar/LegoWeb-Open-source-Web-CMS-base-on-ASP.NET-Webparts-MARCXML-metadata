using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;

public partial class Webparts_UserUpdateProfile:WebPartBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                load_UserProfile();
            }
            else
            {
                labelEmail.Text = "Bạn chưa đăng nhập!";
                btnOk.Enabled = false;
            }        
        }
    }
    void load_UserProfile()
    {
        MembershipUser member = Membership.GetUser(Page.User.Identity.Name);
        LegoWebSiteForum.Buslogic.User user = new LegoWebSiteForum.Buslogic.User();
        int iUserId = LegoWebSiteForum.Buslogic.ForumUsers.GetUserIDFromEmail(member.Email);
        if (iUserId > 0)
        {
            user = LegoWebSiteForum.Buslogic.ForumUsers.GetUser(iUserId);
            this.labelEmail.Text = user.Email;
            this.txtAlias.Text = user.Alias;
            this.ImageAvatar.ImageUrl = user.Avatar;            
        }
        else
        {
            this.labelEmail.Text = member.Email;
            this.txtAlias.Text = "";
            this.ImageAvatar.ImageUrl = "";
        }    
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        //check Avartar
        if (fileUploadAvatar != null && fileUploadAvatar.HasFile)
        {
            try
            {
                if (fileUploadAvatar.PostedFile.ContentLength>0)
                {
					// the regex for an image
					Regex imageFilenameRegex = new Regex(@"(.*?)\.(jpg|JPG|jpeg|JPEG|png|PNG|gif|GIF|bmp|BMP)$");
					if(imageFilenameRegex.IsMatch(fileUploadAvatar.PostedFile.FileName))
					{
						if (fileUploadAvatar.PostedFile.ContentLength < 102400)
						{
							string filename = Path.GetFileName(fileUploadAvatar.FileName);
							//change avatar file name to email name
							filename = filename.Substring(filename.LastIndexOf("."));
							filename = this.labelEmail.Text.Replace(".", "") + filename;
							filename = filename.Replace("@", "");

							fileUploadAvatar.SaveAs(Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "Image/Avatars/" + filename);
							this.ImageAvatar.ImageUrl=Application["FCKeditor:UserFilesVirtuaPath"].ToString() + "Image/Avatars/" + filename;
						}
						else
						{
							CustomErrorMessage.Text = "Upload status: The file has to be less than 100 kb!";
							CustomErrorMessage.Visible = true;
							return;
						}
                    }
                }
                else
                {
                    CustomErrorMessage.Text = "Upload status: Only JPEG files are accepted!";
                    CustomErrorMessage.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                CustomErrorMessage.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                CustomErrorMessage.Visible = true;
                return;
            }
        }
        //check Alias
        if (string.IsNullOrEmpty(txtAlias.Text))
        {
            CustomErrorMessage.Text = "Ký danh không cho phép bỏ trống";
            CustomErrorMessage.Visible = true;
            return;
        }

        // Create new user
        LegoWebSiteForum.Buslogic.User user = new LegoWebSiteForum.Buslogic.User();
        int iUserId = 0;
        //check if user exsit     
        iUserId = LegoWebSiteForum.Buslogic.ForumUsers.GetUserIDFromEmail(labelEmail.Text);
        if (iUserId > 0)
        {
            //update
            user = LegoWebSiteForum.Buslogic.ForumUsers.GetUser(iUserId);
            user.Alias = txtAlias.Text;
            if (!String.IsNullOrEmpty(ImageAvatar.ImageUrl))
            {
                user.Avatar = ImageAvatar.ImageUrl;
            }
            LegoWebSiteForum.Buslogic.ForumUsers.UpdateUser(user);
        }
        else
        {
            user.Alias = txtAlias.Text;
            user.Email = labelEmail.Text;
            iUserId = LegoWebSiteForum.Buslogic.ForumUsers.AddUser(user);
            if (iUserId > 0)
            {
                if (!String.IsNullOrEmpty(ImageAvatar.ImageUrl))
                {
                    user.Avatar = ImageAvatar.ImageUrl;
                    LegoWebSiteForum.Buslogic.ForumUsers.UpdateUser(user);
                }
            }
        }
        UrlQuery redirectURL = new UrlQuery("ForumThreadBrowser.aspx?" + Request.QueryString);
        Response.Redirect(redirectURL.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UrlQuery redirectURL = new UrlQuery("ForumThreadBrowser.aspx?" + Request.QueryString);
        Response.Redirect(redirectURL.AbsoluteUri);
    }
}
