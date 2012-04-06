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

/// <summary>
/// Webpart này dùng để hiển thị danh sách các nội dung mới nhất trong category_id hoặc section_id.
/// Cách trình diễn là danh mục n tiêu đề khi chuột di qua tiêu đề thì hiển thị popup tóm tắt.
/// Đặc biệt tự tìm kiếm trang đích phù hợp với mỗi loại nội dung theo hướng: content_id->category_id->menu_id->menu_link_url
/// </summary>

public partial class Webparts_CONTENTLISTTOPNEWS :WebPartBase
{
    //get top news by section id or category id not both
    private int _section_id = 0;
    private int _category_id = 0;
    private int _important_level = 0;
    private int _number_of_record = 5;
    private string _box_css_name = null;//mean no box around
    private string _template_name = "lgwdsp_titletipsummary";
    private string _default_post_page = "news.aspx";
    public Webparts_CONTENTLISTTOPNEWS()
    {
        this.Title = "CONTENT LIST TOP NEWS";
    }
    #region webpart properties
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
    [WebDisplayName("2.Section id:")]
    [WebDescription("Select the section to get contents of.")]
    /// <summary>
    /// section_id
    /// </summary>
    public int p2_section_id
    {
        get
        {
            return _section_id;
        }
        set
        {
            _section_id = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Category id:")]
    [WebDescription("Select the category to get contents of.")]
    /// <summary>
    /// category_id
    /// </summary>
    public int p3_category_id
    {
        get
        {
            return _category_id;
        }
        set
        {
            _category_id = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("4.Important level:")]
    [WebDescription("Set begining important level of contents.")]
    /// <summary>
    /// important_level
    /// </summary>
    public int p4_important_level
    {
        get
        {
            return _important_level;
        }
        set
        {
            _important_level = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("5.Number of records:")]
    [WebDescription("Set number of records to display")]
    /// <summary>
    /// number of record to display
    /// </summary>
    public int p5_number_of_record
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
    [WebDisplayName("6.Template name:")]
    [WebDescription("Select xslt template name")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p6_template_name
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
    [WebDisplayName("7.Default post page:")]
    [WebDescription("Set default post url for content link href")]
    /// <summary>
    /// post to page when click links
    /// </summary>
    public string p7_default_post_page
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
                    if (_section_id > 0)
                    {
                        DataTable secData = LegoWebSite.Buslgic.Sections.get_SECTION_BY_ID(_section_id).Tables[0];
                        if (secData.Rows.Count > 0)
                        {
                            this.Title = secData.Rows[0]["SECTION_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
                        }
                    }else
                    {
                        DataTable catData = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(_category_id).Tables[0];
                        if (catData.Rows.Count > 0)
                        {
                            this.Title = catData.Rows[0]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
                        }           
                    }                 
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _box_css_name,LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(this.Title));
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

            DataTable cntData =null;

            if(_section_id>0)
            {
               cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_BY_SECTION(_section_id, _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower(),_important_level);
            }else
            {
                cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_BY_CATEGORY(_category_id, _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower(),_important_level);
            }

            if (cntData.Rows.Count > 0)
            {
                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
                CRecords outRecs = new CRecords();
                UrlQuery myPost=new UrlQuery();
                string postURL = String.IsNullOrEmpty(_default_post_page) ? Request.Url.AbsolutePath : _default_post_page;
                CRecord myRec = new CRecord();
                for (int i = 0; i < cntData.Rows.Count; i++)
                {
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[i]["META_CONTENT_ID"], 0));
                    int iCatId = (int)cntData.Rows[i]["CATEGORY_ID"];

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
                    myPost= new UrlQuery(postURL);
                    myPost.Set("contentid", cntData.Rows[i]["META_CONTENT_ID"].ToString());
                    myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                    outRecs.Add(myRec);
                }
                this.litContent.Text = outRecs.XsltFile_Transform(sTemplateFileName);
            }
            else
            {
                this.litContent.Text = "<H3>" + Resources.strings.DataIsNotAvailable + "</H3>";
            }
        }
    }
}
