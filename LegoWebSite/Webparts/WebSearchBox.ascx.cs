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


public partial class Webparts_WebSearchBox : WebPartBase
{
    private string _post_url = "WebSearch.aspx";
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Poll Id
    /// </summary>
    public string post_url
    {
        get
        {
            return _post_url;
        }
        set
        {
            _post_url = value;
        }
    }

    public Webparts_WebSearchBox()
    {
        this.Title = "WEB SEARCH BOX - TÌM NỘI DUNG WEBSITE";
    }
    protected void Page_Load(object sender, EventArgs e)
    {  
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("s_websearchtext", null) != null)
            {
                this.txtKeywords.Text = CommonUtility.GetInitialValue("s_websearchtext", null).ToString();
            }
            CommonUtility.setDefaultButton(this.Page, this.txtKeywords, this.btnSearch);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UrlQuery redirectUrl = new UrlQuery(_post_url == "" ? Request.Url.AbsoluteUri : _post_url);
        if (((Control)sender).ID == "btnSearch")
        {
            if (redirectUrl.QueryString != null)
            {
                //clean query string
                redirectUrl.Remove("s_websearchtext");
                redirectUrl.Remove("s_websearchfield");
                redirectUrl.Remove("s_websearchcategory");
                redirectUrl.Remove("s_websearchsection");
            }
        }
        if (!String.IsNullOrEmpty(this.txtKeywords.Text))
        {
            redirectUrl.Set("s_websearchtext", Server.UrlEncode(this.txtKeywords.Text));
        }
        Response.Redirect(redirectUrl.AbsoluteUri);
    }  
}
