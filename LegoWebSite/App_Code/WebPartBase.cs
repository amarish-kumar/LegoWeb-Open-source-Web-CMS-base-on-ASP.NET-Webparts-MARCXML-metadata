using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for WebPartBase
/// </summary>
public class WebPartBase:UserControl,IWebPart
{
    
    public WebPartBase()
    { 
    
    }
    protected String _title  = " ";
    public String Title
    {
        get
        {
            return _title;
        }
        
        set
        {
            _title = value;
        }

    }
        protected String _subTitle = "";
    public String Subtitle
    {
        get
        {
            return _subTitle;
        }
    }

    protected String _catalogIconImageUrl= "";

    public String CatalogIconImageUrl
    {
        get
        {
           return _catalogIconImageUrl;
        }
        
        set
        {
            _catalogIconImageUrl = value;
        }
    }

    protected String _description = "";

    public String Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    protected String _titleUrl ="";// "http://www.hiendai.com.vn";
    public String TitleUrl
    {
        get
        {
            return _titleUrl;
        }
        set
        {
            _titleUrl = value;
        }
    }

    protected String _titleIconImageUrl="";
    public String TitleIconImageUrl
    {
        get
        {
            return _titleIconImageUrl;
        }
        set
        {
            _titleIconImageUrl = value;
        }
    }

}
