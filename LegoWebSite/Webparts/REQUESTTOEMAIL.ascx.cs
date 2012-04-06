using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;

using System.Net.Mail;
using System.IO;
using System.Net.Configuration;

public partial class Webparts_REQUESTTOEMAIL : WebPartBase
{
    private string _box_css_name = null;//mean no box around

    public Webparts_REQUESTTOEMAIL()
    {
        this.Title = "SEND REQUEST TO EMAIL";
    }
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
    #endregion webparts properties
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _box_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(this.Title));
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
            btnSend.Text = Resources.strings.Send;
            btnCancel.Text = Resources.strings.Cancel;
            load_DestinationMailAddress();
        }
    }

    protected void load_DestinationMailAddress()
    {
        if (!IsPostBack)
        {
            this.drlistToEmailAddress.Items.Clear();

            //REQUEST_TO_ADDRESS store destination of request in format: email1,description1;email2,description2...
            string sToAddress = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("REQUEST_TO_ADDRESS");
            string[] ToAddList = sToAddress.Split(new char[] {';'});
            for(int i=0;i<ToAddList.Length;i++)
            {
                string[] ToAdd = ToAddList[i].Split(new char[] { ',' });
                if (ToAdd.Length == 2)
                {
                    ListItem item = new ListItem(ToAdd[1],ToAdd[0]);
                    drlistToEmailAddress.Items.Add(item);
                }
            }
        }
    }
    
    protected void cmdSend_Click(object sender, EventArgs e)
    {
        string sRequestTemplateFile = LegoWebSite.DataProvider.FileTemplateDataProvider.get_HtmlTemplateFile("RequestEmailTemplate");
        StreamReader sr = new System.IO.StreamReader(sRequestTemplateFile);
        string content = sr.ReadToEnd();
        sr.Close();
        content = content.Replace("[Sender]", this.txtSenderName.Text.Trim());
        content = content.Replace("[Phone]", this.txtSenderPhoneNumber.Text.Trim());
        content = content.Replace("[Email]", this.txtSenderEmail.Text.Trim());
        content = content.Replace("[Title]", this.txtRequestEmailSubject.Text.Trim());
        content = content.Replace("[Content]", this.txtRequestEmailBody.Text.Trim());
        content = content.Replace("[DateTime]", DateTime.Now.ToShortDateString());


        //Now we need to setup the mail message that will be sent
        //create the mailmessage object
        MailMessage message = new MailMessage();
        //set the bodyhtml to true so we can add html to our message
        message.IsBodyHtml = true;
        //set who the email is from, this should correspond to the email we setup
        //in the smtp settings earlier
        System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");


        message.From = new MailAddress(settings.Smtp.From);
        //send this message to the email destination
        message.To.Add(new MailAddress(drlistToEmailAddress.SelectedValue));
        //set the subject
        message.Subject = this.txtRequestEmailSubject.Text.Trim();
        //Set the body of our message
        message.Body = content;

        divSendRequest.Visible = false;
        divSendStatus.Visible = true;

        try
        {

            //Send the message
            SmtpClient client = new SmtpClient();
            client.Send(message);
            litSendRequestStatus.Text = String.Format("<h3>{0}</h3>",Resources.strings.YourRequestHasBeenSent);
        
        }
        catch (Exception ex)
        {
            litSendRequestStatus.Text = "Error:" + ex.Message;
        }
        
    }
   
      
}
