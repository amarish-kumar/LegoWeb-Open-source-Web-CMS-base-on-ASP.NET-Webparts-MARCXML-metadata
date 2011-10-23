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

public partial class Webparts_SendRequestEmail : WebPartBase
{
    public Webparts_SendRequestEmail()
    {
        this.Title = "Send Request Email";
    }

    public static String SendMailAddress
    {
        get
        {
            SmtpSection cfg = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            return cfg.Network.UserName;
        }
    }

    protected void load_ReceivedMailAddress()
    {
        if (!IsPostBack)
        {
            this.drlistContactService.Items.Clear();
            NameValueCollection ReceivedEmailAddress = System.Configuration.ConfigurationManager.GetSection("ReceivedEmailAddress") as NameValueCollection;
           
            for (int i = 0; i < ReceivedEmailAddress.Count; i++)
            {
                ListItem item = new ListItem(ReceivedEmailAddress[ReceivedEmailAddress.Keys[i]].ToString(), ReceivedEmailAddress.Keys[i].ToString());
                drlistContactService.Items.Add(item);
            }
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            load_ReceivedMailAddress();
        }
    }

    public string SendMail(string subject, string body, string to, bool isHtml, bool isSSL)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SendMailAddress, "EVNNPS");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isHtml;
                SmtpClient client = new SmtpClient();
                client.Send(mail);
            }
        }
        catch (SmtpException ex)
        {
            return ex.Message;
        }
        return "Thông tin của quý khách đã được gửi đi. Chúng tôi sẽ trả lời quý khách sớm nhất có thể. Cám ơn quý khách!";
   }
    
    protected void cmdSend_Click(object sender, EventArgs e)
    {
        StreamReader sr =  new System.IO.StreamReader(Request.MapPath(HttpContext.Current.Application["FCKeditor:UserFilesPath"].ToString() + "EmailTemplate.htm"));
        sr = File.OpenText(Server.MapPath(HttpContext.Current.Application["FCKeditor:UserFilesPath"].ToString() + "EmailTemplate.htm"));
        string content = sr.ReadToEnd();
        content = content.Replace("[Sender]", this.txtContactUserName.Text.Trim());
        content = content.Replace("[Phone]", this.txtContactUserPhone.Text.Trim());
        content = content.Replace("[Email]", this.txtContactUserEmail.Text.Trim());
        content = content.Replace("[Title]", this.txtContactTitle.Text.Trim());
        content = content.Replace("[Content]", this.txtContactEmailContent.Text.Trim());
        content = content.Replace("[DateTime]", DateTime.Now.ToShortDateString());
        try
        {
            WebMsgBox.Show((SendMail("Khách hàng liên hệ qua website", content, this.drlistContactService.SelectedItem.Value.ToString(), true, true)));
            this.txtContactUserName.Text = "";
            this.txtContactUserPhone.Text = "";
            this.txtContactUserEmail.Text = "";
            this.txtContactTitle.Text = "";
            this.txtContactEmailContent.Text = "";
        }
        catch (Exception ex)
        {
            this.SendMailMessage.Text = ex.Message;
        }
        
    }
   
      
}
