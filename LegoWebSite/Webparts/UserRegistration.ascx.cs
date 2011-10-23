using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.IO;
using System.Web.UI.WebControls.WebParts;

public partial class Webparts_UserRegistration :WebPartBase
{
    protected string _verifynewuser_url = "http://localhost/VerifyNewUser.aspx?mnuid=161&ID={0}";
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// khuon mau hien thi danh sach noi dung chinh khi co hon 1 noi dung tim thay
    /// </summary>
    public string verifynewuser_url
    {
        get
        {
            return _verifynewuser_url;
        }
        set
        {
            _verifynewuser_url = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            string sAgreementFile=LegoWebSite.DataProvider.FileTemplateDataProvider.get_HtmlTemplateFile("ForumUserAgreement");
            if (sAgreementFile != null)
            {
                StreamReader sr = new StreamReader(sAgreementFile);
                Literal literalAgreement = (Literal)CreateUserWizardStep1.ContentTemplateContainer.FindControl("literalUserAgreement");
                if (literalAgreement != null)
                {
                    literalAgreement.Text = sr.ReadToEnd();
                }
                sr.Close();
            }
        
        }
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        
        //Create a new user
        CreateUserWizard cuw = (CreateUserWizard)sender;
        MembershipUser newUser = Membership.GetUser(cuw.UserName);

        //update PATRON Email
        //KIPOSWEB.Buslogic.Circulation.update_CIRC_PATRON_EMAIL(cuw.UserName,cuw.Email);

        ////insert user to LEGOWEB_FORUM_USERS
        //TextBox Alias = (TextBox)CreateUserWizardStep1.ContentTemplateContainer.FindControl("Alias");
        //HiddenField AvatarURL = (HiddenField)CreateUserWizardStep1.ContentTemplateContainer.FindControl("AvatarURL");
        //// Create new user
        //Forum.Buslogic.User user = new Forum.Buslogic.User();
        //int iUserId = 0;
        ////check if email exsit     
        //iUserId = Forum.Buslogic.ForumUsers.GetUserIDFromEmail(cuw.Email);
        //if (iUserId > 0)
        //{
        //    //update
        //    user = Forum.Buslogic.ForumUsers.GetUser(iUserId);
        //    user.Alias = Alias.Text;
        //    if (!String.IsNullOrEmpty(AvatarURL.Value))
        //    {
        //        user.Avatar = AvatarURL.Value;
        //    }
        //    Forum.Buslogic.ForumUsers.UpdateUser(user);
        //}
        //else
        //{
        //    user.Alias = Alias.Text;
        //    user.Email = cuw.Email;
        //    iUserId = Forum.Buslogic.ForumUsers.AddUser(user);
        //    if (iUserId > 0)
        //    {
        //        if (!String.IsNullOrEmpty(AvatarURL.Value))
        //        {
        //            user.Avatar = AvatarURL.Value;
        //            Forum.Buslogic.ForumUsers.UpdateUser(user);
        //        }
        //    }               
        //}        

        //Set the user's id
        Guid newUserId = (Guid)newUser.ProviderUserKey; 
        //Now we need to create an url that will link to a VerifyNewUser page and
        //accept a query string that is the user's id 
        //setup the base of the url

        string verifynewuserUrl = "";
        if (String.IsNullOrEmpty(_verifynewuser_url))
        {
            string verifyUrl = "VerifyNewUser.aspx?ID=" + newUserId.ToString();
            //combine to make the final url
            verifynewuserUrl = CommonUtility.FullyQualifiedApplicationPath + verifyUrl;
        }
        else
        {
            verifynewuserUrl = string.Format(_verifynewuser_url, newUserId.ToString());
        }                
        
        //Now we need to setup the mail message that will be sent
        //create the mailmessage object
        MailMessage message = new MailMessage();
        //set the bodyhtml to true so we can add html to our message
        message.IsBodyHtml = true;
        //set who the email is from, this should correspond to the email we setup
        //in the smtp settings earlier
        System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup) config.GetSectionGroup("system.net/mailSettings");


        message.From = new MailAddress(settings.Smtp.From);
        //send this message to the email of the user who just created an account
        message.To.Add(new MailAddress(cuw.Email));
        //set the subject
        message.Subject = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("REGISTRATION_EMAIL_SUBJECT"); 
        //next we need to build the message body, I'm going to use the stringbuilder
        //to simplify this a bit.
        StringBuilder sb = new StringBuilder();
        //add the 'Welcome username, ' line
        sb.Append(String.Format(LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("REGISTRATION_EMAIL_BODY"), cuw.UserName, verifynewuserUrl)); 
        //Set the body of our message
        message.Body = sb.ToString(); 
        //Send the message
        SmtpClient client = new SmtpClient();
        client.Send(message); 
        
    }

    protected void  CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {
       
        
        CheckBox chkAgreement = (CheckBox)CreateUserWizardStep1.ContentTemplateContainer.FindControl("chkUserAgreement");
        if (chkAgreement != null && chkAgreement.Checked == false)
        {
            e.Cancel = true;
            return;
        }

        TextBox UserName = (TextBox)CreateUserWizardStep1.ContentTemplateContainer.FindControl("UserName");
        TextBox Email = (TextBox)CreateUserWizardStep1.ContentTemplateContainer.FindControl("Email");        
        FileUpload fileUploadAvatar = (FileUpload)CreateUserWizardStep1.ContentTemplateContainer.FindControl("fileUploadAvatar");
        Literal CustomErrorMessage = (Literal)CreateUserWizardStep1.ContentTemplateContainer.FindControl("CustomErrorMessage");
        HiddenField AvatarURL = (HiddenField)CreateUserWizardStep1.ContentTemplateContainer.FindControl("AvatarURL");
        CustomErrorMessage.Visible = false;

        string sUserName = UserName.Text;

        //if (!KIPOSWEB.Buslogic.Circulation.is_CIRC_PATRON_EXIST(sUserName))
        //{
        //    CustomErrorMessage.Visible = true;
        //    CustomErrorMessage.Text = "Không tồn tại mã độc giả này/ Patron Barcode does not exist!";
        //    e.Cancel = true;
        //    return;
        //}
        
        AvatarURL.Value = null;
        if (fileUploadAvatar != null && fileUploadAvatar.HasFile)
        {
            try
            {
                if (fileUploadAvatar.PostedFile.ContentType == "image/pjpeg" || fileUploadAvatar.PostedFile.ContentType == "image/jpeg" || fileUploadAvatar.PostedFile.ContentType == "image/jpg" || fileUploadAvatar.PostedFile.ContentType == "image/gif" || fileUploadAvatar.PostedFile.ContentType == "image/bmp" || fileUploadAvatar.PostedFile.ContentType == "image/png")
                {
                    if (fileUploadAvatar.PostedFile.ContentLength < 302400)
                    {
                        string filename = Path.GetFileName(fileUploadAvatar.FileName);
                        //change avatar file name to email name
                        filename = filename.Substring(filename.LastIndexOf("."));
                        filename = Email.Text.Replace(".", "") + filename;
                        filename = filename.Replace("@", "");

                        fileUploadAvatar.SaveAs(Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "Image/Avatars/" + filename);
                        AvatarURL.Value = Application["FCKeditor:UserFilesVirtuaPath"].ToString() + "Image/Avatars/" + filename;
                    }
                    else
                    {
                        CustomErrorMessage.Text = "Upload status: The file has to be less than 300 kb!";
                        CustomErrorMessage.Visible = true;
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    CustomErrorMessage.Text = "Upload status: file type is not accepted!";
                    CustomErrorMessage.Visible = true;
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                CustomErrorMessage.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                CustomErrorMessage.Visible = true;
                e.Cancel = true;
                return;
            }
        }


 
    }
}
