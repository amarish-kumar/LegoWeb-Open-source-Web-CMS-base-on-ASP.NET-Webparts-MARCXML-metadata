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
using LegoWebSite.DataProvider;
using LegoWebSite.Controls;
using MarcXmlParserEx;

/// <summary>
/// category content page navigator with auto focus page number with current contentid pass param
/// items are displayed float using div and css in xslt
/// </summary>
public partial class Webparts_CONTENTNAVIGATORFLOAT : WebPartBase
{
    protected LegoWebSite.DataProvider.ContentNavigatorDataProvider _contentNavigatorData;
    public enum SortFields { Default };

    private string _navibox_css_name = null;
    private string _navibox_title = null;
    private string _contentbox_css_name = null;
    private string _contentbox_title = null;
    private string _template_name = "lgwdsp_productfloat";
    private int _page_size = 10;
    private string _default_post_page = null;

    public Webparts_CONTENTNAVIGATORFLOAT()
    {
        this.Title = "CONTENT NAVIGATOR FLOAT ITEMS";
    }

    #region webparts properties
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
    [WebDisplayName("3.Content navigator box css class name:")]
    [WebDescription("Set css class name of content navigator box")]
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
    [WebDisplayName("4.Content navigator box title:")]
    [WebDescription("Set title of content navigator box")]
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
    [WebDisplayName("5.Template name:")]
    [WebDescription("Select xslt template name")]
    /// <summary>
    /// template for transforming record to html
    /// template_name: template file name to transform meta content record
    /// </summary>
    public string p05_template_name
    {
        get
        {
            return _template_name;
        }
        set
        {
            _template_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("6.Page size:")]
    [WebDescription("Set page size value: number of record per page")]
    /// <summary>
    ///  Set page size value: number of record per page   
    /// </summary>
    public int p06_page_size
    {
        get
        {
            return _page_size;
        }
        set
        {
            _page_size = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("7.Default post page:")]
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

    #endregion webparts properties

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                if (CommonUtility.GetInitialValue("contentid", null) != null)
                {
                    int meta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
                    if (meta_content_id > 0)
                    {
                        string spostURL = ResolveUrl("~/ContentBrowser.aspx");
                        UrlQuery myPost = new UrlQuery(spostURL);
                        myPost.Set("contentid", meta_content_id.ToString());
                        Response.Redirect(myPost.AbsoluteUri);
                    }
                }

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
                        this.litContentNavigatorBoxTop.Text = sBoxTop;
                        this.litContentNavigatorBoxBottom.Text = sBoxBottom;
                    }
                    else
                    {
                        string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _contentbox_css_name);
                        string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                        this.litContentNavigatorBoxTop.Text = sBoxTop;
                        this.litContentNavigatorBoxBottom.Text = sBoxBottom;
                    }
                }
                else
                {
                    this.litContentNavigatorBoxTop.Text = "<div>";
                    this.litContentNavigatorBoxBottom.Text = "</div>";
                }

                CommonUtility.InitializeGridParameters(ViewState, "contentNavigator", typeof(SortFields), 10, 100);
                ViewState["contentNavigatorPageNumber"] = 1;
                ViewState["contentNavigatorPageSize"] = _page_size;

                contentNavigatorBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void contentNavigatorBind()
    {
        int meta_content_id = 0;
        int category_id = 0;
        int menu_id = 0;
        string lang_code = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper();

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

        //try to get category_id from menu_id
        if (category_id == 0 && menu_id > 0)
        {
            category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
        }
        //try to get category_id from meta_content_id
        if (category_id == 0 && meta_content_id > 0)
        {
            category_id = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(meta_content_id);
        }

        if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
        {
            this.litCatNaviContent.Text = "<H3>Category does not exist!</H3>";
            return;
        }

        this.litCatNaviContent.Text = LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, Request.Url.AbsoluteUri);


        int outPageCount = 0;

        _contentNavigatorData.PageNumber = Convert.ToInt16(ViewState["contentNavigatorPageNumber"]);
        _contentNavigatorData.RecordsPerPage = (int)ViewState["contentNavigatorPageSize"];
        _contentNavigatorData.get_Result_Count(out outPageCount, category_id, lang_code);
        ViewState["contentNavigatorPageCount"] = outPageCount;

        DataTable Data = _contentNavigatorData.get_Current_Page(category_id, lang_code);

        if (_contentNavigatorData.RecordCount == 1 && Data.Rows.Count == 1)
        {
            UrlQuery postURL = new UrlQuery("ContentBrowser.aspx");
            postURL.Set("contentid", Data.Rows[0]["META_CONTENT_ID"].ToString());
            Response.Redirect(postURL.AbsoluteUri);
        }
        // Create DataColumn objects of data types.
        DataColumn colString = new DataColumn("CONTENT_HTML");
        colString.DataType = System.Type.GetType("System.String");
        Data.Columns.Add(colString);

        string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);

        for (int i = 0; i < Data.Rows.Count; i++)
        {
            string sContentXml = "";
            CRecord myRec = new CRecord();
            int icontentid = int.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());
            string postURL = "ContentBrowser.aspx";

            sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(icontentid, 0);//no NTEXTS CONTENT_XML                  
            myRec.load_Xml(sContentXml);

            if (!String.IsNullOrEmpty(_default_post_page))
            {
                postURL = _default_post_page;
            }
            else
            {
                int iCatId = int.Parse(myRec.Controlfields.Controlfield("002").Value.ToString());
                //try to findout related menuid to get postURL
                int iMnuId = 0;
                int iParentCatId = -1;
                while (iMnuId == 0 && iParentCatId != 0)
                {
                    DataTable CatTable = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCatId).Tables[0];
                    iParentCatId = int.Parse(CatTable.Rows[0]["PARENT_CATEGORY_ID"].ToString());
                    iCatId = iParentCatId;
                    iMnuId = int.Parse(CatTable.Rows[0]["MENU_ID"].ToString());
                }
                if (iMnuId > 0)
                {
                    DataTable MenuTable = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iMnuId).Tables[0];
                    if (MenuTable.Rows.Count > 0)
                    {
                        postURL = MenuTable.Rows[0]["MENU_LINK_URL"].ToString();
                    }
                }
            }
            UrlQuery postQuery = new UrlQuery(postURL);
            postQuery.Set("contentid", icontentid.ToString());
            myRec.Controlfields.Controlfield("001").Value = postQuery.AbsoluteUri;//change 001 value to post url                    
            Data.Rows[i]["CONTENT_HTML"] = myRec.XsltFile_Transform(sTemplateFileName);
        }
        contentNavigatorRepeater.DataSource = Data;
        contentNavigatorRepeater.DataBind();

        if (_contentNavigatorData.RecordCount >= 0)
        {
            Navigator pageNavigator = contentNavigatorRepeater.Controls[contentNavigatorRepeater.Controls.Count > 0 ? contentNavigatorRepeater.Controls.Count - 1 : 0].FindControl("NavigatorNavigator") as Navigator;
            if (pageNavigator != null) pageNavigator.Visible = true;
        }
        else
        {
            Navigator pageNavigator = contentNavigatorRepeater.Controls[contentNavigatorRepeater.Controls.Count > 0 ? contentNavigatorRepeater.Controls.Count - 1 : 0].FindControl("NavigatorNavigator") as Navigator;
            if (pageNavigator != null) pageNavigator.Visible = false;
        }

    }
    private void contentNavigatorPageBind()
    {
        int meta_content_id = 0;
        int category_id = 0;
        int menu_id = 0;
        string lang_code = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper();

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

        //try to get category_id from menu_id
        if (category_id == 0 && menu_id > 0)
        {
            category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
        }
        //try to get category_id from meta_content_id
        if (category_id == 0 && meta_content_id > 0)
        {
            category_id = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(meta_content_id);
        }

        if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
        {
            this.litCatNaviContent.Text = "<H3>Category does not exist!</H3>";
            return;
        }

        this.litCatNaviContent.Text = LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, Request.Url.AbsoluteUri);


        _contentNavigatorData.PageNumber = Convert.ToInt16(ViewState["contentNavigatorPageNumber"]);
        _contentNavigatorData.RecordsPerPage = (int)ViewState["contentNavigatorPageSize"];
        _contentNavigatorData.PageCount = (int)ViewState["contentNavigatorPageCount"];

        DataTable Data = _contentNavigatorData.get_Current_Page(category_id, lang_code);
        // Create DataColumn objects of data types.
        DataColumn colString = new DataColumn("CONTENT_HTML");
        colString.DataType = System.Type.GetType("System.String");
        Data.Columns.Add(colString);

        string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
        for (int i = 0; i < Data.Rows.Count; i++)
        {
            string sContentXml = "";
            CRecord myRec = new CRecord();
            int icontentid = int.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());
            string postURL = "ContentBrowser.aspx";

            sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(icontentid, 0);//no NTEXTS CONTENT_XML                  
            myRec.load_Xml(sContentXml);

            if (!String.IsNullOrEmpty(_default_post_page))
            {
                postURL = _default_post_page;
            }
            else
            {
                int iCatId = int.Parse(myRec.Controlfields.Controlfield("002").Value.ToString());
                //try to findout related menuid to get postURL
                int iMnuId = 0;
                int iParentCatId = -1;
                while (iMnuId == 0 && iParentCatId != 0)
                {
                    DataTable CatTable = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCatId).Tables[0];
                    iParentCatId = int.Parse(CatTable.Rows[0]["PARENT_CATEGORY_ID"].ToString());
                    iCatId = iParentCatId;
                    iMnuId = int.Parse(CatTable.Rows[0]["MENU_ID"].ToString());
                }
                if (iMnuId > 0)
                {
                    DataTable MenuTable = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iMnuId).Tables[0];
                    if (MenuTable.Rows.Count > 0)
                    {
                        postURL = MenuTable.Rows[0]["MENU_LINK_URL"].ToString();
                    }
                }
            }
            UrlQuery postQuery = new UrlQuery(postURL);
            postQuery.Set("contentid", icontentid.ToString());
            myRec.Controlfields.Controlfield("001").Value = postQuery.AbsoluteUri;
            Data.Rows[i]["CONTENT_HTML"] = myRec.XsltFile_Transform(sTemplateFileName);
        }
        contentNavigatorRepeater.DataSource = Data;
        contentNavigatorRepeater.DataBind();
    }
    protected void contentNavigatorDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["contentNavigatorPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["contentNavigatorPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            contentNavigatorPageBind();
    }
    override protected void OnInit(EventArgs e)
    {
        _contentNavigatorData = new ContentNavigatorDataProvider();
    }
}
