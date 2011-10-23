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


public partial class Webparts_IFrameBox:WebPartBase
{
    private string _ViTitle = "";
    private string _EnTitle = "";
    private string _SourceURL = "";

    public Webparts_IFrameBox()
    {
        this.Title = "IFRAME BOX";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "vi")
        {
            this.Title = _ViTitle;
            this.iFrameBoxTitle.Text = _ViTitle;
        }
        else
        {
            this.Title = _EnTitle;
            this.iFrameBoxTitle.Text = _EnTitle;
        }
        if (!String.IsNullOrEmpty(_SourceURL))
        {
            string ScriptKey = this.iframebox.ClientID + "Script";
            string EmbeddedScript = @"
                             <script type='text/javascript'>
             	             setTimeout(loadfunction,5000)
                             function loadfunction() 
                             {
             	                document.getElementById('iframeid').src = 'sourceURL';
                             }
                             </script>
                            ";
            EmbeddedScript = EmbeddedScript.Replace("iframeid", this.iframebox.ClientID);
            EmbeddedScript = EmbeddedScript.Replace("sourceURL", _SourceURL);

            if (!Page.ClientScript.IsClientScriptBlockRegistered(ScriptKey))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),ScriptKey,
                EmbeddedScript); 
        }
        else
        {
            this.iframebox.Attributes["src"] = "blank.html";
            this.iframebox.Visible = false;
        }
        }
    }

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// 
    /// </summary>
    public string Vi_Title
    {
        get
        {
            return _ViTitle;
        }
        set
        {
            _ViTitle = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// 
    /// </summary>
    public string En_Title
    {
        get
        {
            return _EnTitle;
        }
        set
        {
            _EnTitle = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// 
    /// </summary>
    public string Source_URL
    {
        get
        {
            return _SourceURL;
        }
        set
        {
            _SourceURL = value;
        }
    }


}
