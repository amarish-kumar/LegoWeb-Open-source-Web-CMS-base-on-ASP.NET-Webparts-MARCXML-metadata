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


public partial class Webparts_IFRAMEBOX:WebPartBase
{
    private string _box_css_name = "";
    private string _iframe_height = "";
    private string _iframe_width = "";
    private bool _iframe_scrolling = false;
    private string _iframe_source_url = "";

    public Webparts_IFRAMEBOX()
    {
        this.Title = "IFRAME BOX";
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

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("2.iframe height:")]
    [WebDescription("Set height value of iframe.")]
    /// <summary>
    /// Set height value of iframe or leave blank to keep default value
    /// </summary>
    public string p2_iframe_height
    {
        get
        {
            return _iframe_height;
        }
        set
        {
            _iframe_height = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.iframe width:")]
    [WebDescription("Set width value of iframe.")]
    /// <summary>
    /// Set width value of iframe or leave blank to keep default value
    /// </summary>
    public string p3_iframe_width
    {
        get
        {
            return _iframe_width;
        }
        set
        {
            _iframe_width = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("4.iframe scrolling:")]
    [WebDescription("Set iframe scrolling.")]
    /// <summary>
    /// Set iframe scrolling, default is no 
    /// </summary>
    public bool p4_iframe_scrolling
    {
        get
        {
            return _iframe_scrolling;
        }
        set
        {
            _iframe_scrolling = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("5.iframe source url:")]
    [WebDescription("Set iframe source url.")]
    /// <summary>
    /// Set iframe source url
    /// </summary>
    public string p5_iframe_source_url
    {
        get
        {
            return _iframe_source_url;
        }
        set
        {
            _iframe_source_url = value;
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

            if (!string.IsNullOrEmpty(_iframe_height))
            {
                iframebox.Attributes["height"] = _iframe_height;
            }
            if (!string.IsNullOrEmpty(_iframe_width))
            {
                iframebox.Attributes["width"] = _iframe_width;
            }

            iframebox.Attributes["scrolling"] = _iframe_scrolling == true ? "yes" : "no";

            if (!String.IsNullOrEmpty(_iframe_source_url))
            {
                string ScriptKey = this.iframebox.ClientID + "Script";
                String sFrameClientID = this.iframebox.ClientID;
                string EmbeddedScript = @"<script type='text/javascript'>setTimeout(loadfunction,5000); function loadfunction() { document.getElementById('{0}').src = '{1}';} </script>";
                EmbeddedScript = EmbeddedScript.Replace("{0}",sFrameClientID);
                EmbeddedScript = EmbeddedScript.Replace("{1}",_iframe_source_url);

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
}
