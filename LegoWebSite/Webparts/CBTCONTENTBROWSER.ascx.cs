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
using MarcXmlParserEx;
using LegoWebSite.Buslgic;

/// <summary>
/// CONTENT BROWSER BOX: display content details and related contents list
/// auto dectect last last record of category if contentid not pass
/// </summary>
public partial class Webparts_CBTCONTENTBROWSER : WebPartBase
{
    private string _navibox_css_name = null;
    private string _navibox_title = null;
    private string _contentbox_css_name = null;
    private string _contentbox_title = null;
    private string _relatedbox_css_name = null;
    private string _relatedbox_title = null;
    private int _number_of_record = 10;
    private string _content_browser_template = "lgwdsp_thumbtitlesummary";
    private string _related_content_template = "lgwdsp_title";
    private string _default_post_page = null;

    public Webparts_CBTCONTENTBROWSER()
    {
        this.Title = "CONTENT BROWSER WITH CATEGORY NAVIGATOR AND RELATED CONTENT LIST";
    }
    #region webpart properties
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("1.Category navigator box css class name:")]
    [WebDescription("Set css class name of category navigator box")]
    /// <summary>
    /// Set css class name of category navigator box
    /// </summary>
    public string p01_navibox_css_name
    {
        get
        {
            return _navibox_css_name;
        }
        set
        {
            _navibox_css_name = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("2.Category navigator box title:")]
    [WebDescription("Set title of category navigator box")]
    /// <summary>
    /// Set title of category navigator box
    /// </summary>
    public string p02_navibox_title
    {
        get
        {
            return _navibox_title;
        }
        set
        {
            _navibox_title = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Content browser box css class name:")]
    [WebDescription("Set css class name of content browser box")]
    /// <summary>
    /// Set css class name of content browser box
    /// </summary>
    public string p03_contentbox_css_name
    {
        get
        {
            return _contentbox_css_name;
        }
        set
        {
            _contentbox_css_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("4.Content browser box title:")]
    [WebDescription("Set title of content browser box")]
    /// <summary>
    /// Set title of content browser box
    /// </summary>
    public string p04_contentbox_title
    {
        get
        {
            return _contentbox_title;
        }
        set
        {
            _contentbox_title = value;
        }
    }


    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("5.Related content list box css class name:")]
    [WebDescription("Set css class name of related content list box")]
    /// <summary>
    /// Set css class name of content browser box
    /// </summary>
    public string p05_relatedbox_css_name
    {
        get
        {
            return _relatedbox_css_name;
        }
        set
        {
            _relatedbox_css_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("6.Related content list box title:")]
    [WebDescription("Set title of related content list box")]
    /// <summary>
    /// Set title of related content list box
    /// </summary>
    public string p06_relatedbox_title
    {
        get
        {
            return _relatedbox_title;
        }
        set
        {
            _relatedbox_title = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("7.Number of records:")]
    [WebDescription("Set number of all record to display in content browser and related content list")]
    /// <summary>
    /// Set number of all record to display in content browser and related content list
    /// </summary>
    public int p07_number_of_record
    {
        get
        {
            return _number_of_record;
        }
        set
        {
            _number_of_record = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("8.Content browser list mode template:")]
    [WebDescription("Set the template to transform more than one records in content browser box")]
    /// <summary>
    /// Set xslt template to transform record in list mode - no meta_content_id is detected/specified
    /// </summary>
    public string p08_content_browser_template
    {
        get
        {
            return _content_browser_template;
        }
        set
        {
            _content_browser_template = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("9.Related content template:")]
    [WebDescription("Set the template to transform related contents")]
    /// <summary>
    /// Set xslt template to transform related contents 
    /// </summary>
    public string p09_related_content_template
    {
        get
        {
            return _related_content_template;
        }
        set
        {
            _related_content_template = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("10.Default post page:")]
    [WebDescription("Set default post url for content link href")]
    /// <summary>
    /// post to page when click links
    /// </summary>
    public string p10_default_post_page
    {
        get
        {
            return _default_post_page;
        }
        set
        {
            _default_post_page = value;
        }
    }

    #endregion webpart properties

    protected void Page_Load(object sender, EventArgs e)
    {
        int meta_content_id = 0;
        int category_id = 0;
        int menu_id=0;
        string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

        if (!IsPostBack)
        {

            //set round boxs

            if (!String.IsNullOrEmpty(_navibox_css_name))
            {
                if (_navibox_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _navibox_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(_navibox_title));
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litCatNaviBoxTop.Text = sBoxTop;
                    this.litCatNaviBoxBottom.Text = sBoxBottom;
                }
                else
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _navibox_css_name);
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litCatNaviBoxTop.Text = sBoxTop;
                    this.litCatNaviBoxBottom.Text = sBoxBottom;
                }
            }
            else
            {
                this.litCatNaviBoxTop.Text = "<div>";
                this.litCatNaviBoxBottom.Text = "</div>";
            }


            if (!String.IsNullOrEmpty(_contentbox_css_name))
            {
                if (_contentbox_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _contentbox_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(_contentbox_title));
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litContentBrowserBoxTop.Text = sBoxTop;
                    this.litContentBrowserBoxBottom.Text = sBoxBottom;
                }
                else
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _contentbox_css_name);
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litContentBrowserBoxTop.Text = sBoxTop;
                    this.litContentBrowserBoxBottom.Text = sBoxBottom;
                }
            }
            else
            {
                this.litContentBrowserBoxTop.Text = "<div>";
                this.litContentBrowserBoxBottom.Text = "</div>";            
            }


            if (!String.IsNullOrEmpty(_relatedbox_css_name))
            {
                if (_relatedbox_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _relatedbox_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(_relatedbox_title));
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litRelatedContentBoxTop.Text = sBoxTop;
                    this.litRelatedContentBoxBottom.Text = sBoxBottom;
                }
                else
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _relatedbox_css_name);
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litRelatedContentBoxTop.Text = sBoxTop;
                    this.litRelatedContentBoxBottom.Text = sBoxBottom;
                }
            }
            else
            {
                this.litRelatedContentBoxTop.Text = "<div>";
                this.litRelatedContentBoxBottom.Text = "</div>";            
            }

            //try to find meta_content_id in order of: meta_content_id; category_id->get top 1 meta_content_id of; menu_id->category_id->get top 1 meta_content_id of

            if (CommonUtility.GetInitialValue("contentid", null) != null)
            {
                meta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
            }
            
            if (CommonUtility.GetInitialValue("catid", null) != null)
            {
                category_id = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
            }
            
            if (CommonUtility.GetInitialValue("mnuid", null) != null)
            {
                menu_id = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
            }
            
            if (meta_content_id <= 0)
            {
                if (category_id > 0)
                {
                    if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
                    {
                        this.litContentBrowserContent.Text = "<H3>No data is available!</H3>";
                        return;
                    }
                    else
                    {
                        DataTable top1Data = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(category_id,1, sLangCode, null);
                        if (top1Data.Rows.Count > 0)
                        {
                            meta_content_id =(int)top1Data.Rows[0]["META_CONTENT_ID"];
                        }
                        else
                        {
                            this.litContentBrowserContent.Text = "<H3>No data is available!</H3>";
                            return;                        
                        }
                    }
                }
                else if (menu_id > 0)
                {
                    category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
                    if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
                    {
                        this.litContentBrowserContent.Text = "<H3>No data is available!</H3>";
                        return;
                    }
                    else
                    {
                        DataTable top1Data = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(category_id, 1, sLangCode, null);
                        if (top1Data.Rows.Count > 0)
                        {
                            meta_content_id = (int)top1Data.Rows[0]["META_CONTENT_ID"];
                        }
                        else
                        {
                            this.litContentBrowserContent.Text = "<H3>No data is available!</H3>";
                            return;
                        }
                    }
                }
                else
                {
                    this.litContentBrowserContent.Text = "<H3>No data is available!</H3>";
                    return;                    
                }                               
            }
            
            if (!LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(meta_content_id))
            {
                litContentBrowserContent.Text = "<H3>meta_content_id is not available!</H3>";
                return;
            }
            if (category_id == 0) category_id = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(meta_content_id);
            //Display Navigator Info
            this.litCatNaviContent.Text = LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, (String.IsNullOrEmpty(_default_post_page) == true ? Request.Url.AbsoluteUri:_default_post_page));                            
            #region verify access right
            //verify access right
            int iAccessLevel = LegoWebSite.Buslgic.MetaContents.get_ACCESS_LEVEL(meta_content_id);
            switch (iAccessLevel)
            { 
                case 1: //need logedin
                    if (!Page.User.Identity.IsAuthenticated)
                    {
                        this.litContentBrowserContent.Text = "<span><b>Only registered users can view details</b></span>";                            
                        return;
                    }
                break;
                case 2:
                if (!Page.User.Identity.IsAuthenticated)
                {
                    this.litContentBrowserContent.Text = "<span><b>Only registered users can view details</b></span>";
                    return;
                }
                else //verify user roles
                {
                    string[] sAllowAccessRoles = LegoWebSite.Buslgic.MetaContents.get_ACCESS_ROLES(meta_content_id);
                    string[] sUserRoles = Roles.GetRolesForUser(Page.User.Identity.Name);
                    bool bAllowAccess = false;
                    if (sUserRoles != null && sUserRoles.Length > 0 && sAllowAccessRoles!=null && sAllowAccessRoles.Length>0)
                    {
                        for (int x = 0; x < sUserRoles.Length; x++)
                        {
                            for (int y = 0; y < sAllowAccessRoles.Length; y++)
                            {
                                if (sUserRoles[x] == sAllowAccessRoles[y])
                                {
                                    bAllowAccess = true;
                                    break;
                                }
                            }
                            if (bAllowAccess) break;
                        }
                    }
                    if (!bAllowAccess)
                    {
                        this.litContentBrowserContent.Text = "<span><b>You are not authorized to view details</b></span>";
                        return;                        
                    }
                }
                break;
            }
            #endregion verify access right
            //auhorized 
            //Display Content Details
            CRecord myRec = new CRecord();
            myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, 1));
            string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(LegoWebSite.Buslgic.Categories.get_CATEGORY_TEMPLATE_NAME(category_id));
            string sContentHTML = myRec.XsltFile_Transform(sTemplateFileName);
            
            //find linked contents list
            CDatafields Dfs = myRec.Datafields;
            Dfs.Filter("780");
            int iLinkedCount = Dfs.Count;
            int[] linkedIDs = new int[iLinkedCount+1];
            for (int i = 0; i < iLinkedCount;i++ )
            {
                linkedIDs[i] = int.Parse(Dfs.Datafield(i).Subfields.Subfield("w").Value);
            }
            if (linkedIDs.Length > 0)//have linked contents
            {
                UrlQuery linkPost = new UrlQuery((String.IsNullOrEmpty(_default_post_page) == true ? Request.Url.AbsoluteUri : _default_post_page));
                linkPost.Remove("contentid");
                linkPost.Set("contentid"," ");//resever last param for link contentid
                sContentHTML = sContentHTML.Replace("{POST_URL}", linkPost.AbsoluteUri.Trim());
            }
            this.litContentBrowserContent.Text =sContentHTML ;
            //increase read count
            LegoWebSite.Buslgic.MetaContents.increase_READ_COUNT(meta_content_id);
            //exception ids
            linkedIDs[iLinkedCount] = meta_content_id;//last id is current meta_content_id

            DataTable relData = LegoWebSite.Buslgic.MetaContents.get_TOP_RELATED_CONTENTS(category_id, _number_of_record , sLangCode, linkedIDs);                
            if (relData.Rows.Count > 0)
            {                    
                for (int i = 0; i < relData.Rows.Count; i++)
                {
                    meta_content_id = (int)relData.Rows[i]["META_CONTENT_ID"];
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, 1));
                    sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_related_content_template);
                    UrlQuery postPage = new UrlQuery((String.IsNullOrEmpty(_default_post_page) == true ? Request.Url.AbsoluteUri : _default_post_page));
                    postPage.Remove("catid");
                    postPage.Remove("mnuid");
                    postPage.Set("contentid",meta_content_id.ToString());

                    this.litRelatedContentContent.Text += myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", postPage.AbsoluteUri);
                }
            }
            else
            {
                this.litRelatedContentContent.Text = "";
            }

        }        
    }
}
