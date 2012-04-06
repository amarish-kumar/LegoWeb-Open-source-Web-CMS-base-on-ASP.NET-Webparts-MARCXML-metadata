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
/// Web content search results
/// </summary>
public partial class Webparts_WEBSEARCHRESULT : WebPartBase
{
    private string _box_css_name = null;//mean no box around
    private string _default_post_page = "WebSearch.aspx";
    private string _template_name = "lgwdsp_thumbtitlesummary";
    private int _section_id = 1;

    public Webparts_WEBSEARCHRESULT()
    {
        this.Title = "WEB SEARCH RESULTS";
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
    [WebDisplayName("2.Default post page:")]
    [WebDescription("Set default post url for post search query")]
    /// <summary>
    /// post to page when click links
    /// </summary>
    public string p2_default_post_page
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
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Template name:")]
    [WebDescription("Select xslt template name")]
    /// <summary>
    /// template for transforming record to html
    /// template_name: template file name to transform meta content record, if not set auto detect base on category template name meta_content_id->category_id->template_name
    /// </summary>
    public string p3_template_name
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
    #endregion

    protected LegoWebSite.DataProvider.ContentSearchDataProvider _contentSearchData;
    public enum SortFields { Default };

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

            CommonUtility.InitializeGridParameters(ViewState, "contentSearch", typeof(SortFields), 1, 100);
            ViewState["contentSearchPageNumber"] = 1;
            ViewState["contentSearchPageSize"] = 10;
            contentSearchBind();
        }
    }

    private void contentSearchBind()
    {

        string sSearchField = null;
        string sSearchValue = null;
        try
        {
            if (CommonUtility.GetInitialValue("s_searchfield", null) != null)
            {
                sSearchField = CommonUtility.GetInitialValue("s_searchfield", null).ToString();
            }
            if (CommonUtility.GetInitialValue("s_searchvalue", null) != null)
            {
                sSearchValue = CommonUtility.GetInitialValue("s_searchvalue", null).ToString();
            }

            int outPageCount = 0;

            _contentSearchData.PageNumber = Convert.ToInt16(ViewState["contentSearchPageNumber"]);
            _contentSearchData.RecordsPerPage = (int)ViewState["contentSearchPageSize"];
            _contentSearchData.get_User_Search_Count(out outPageCount, _section_id, sSearchField, sSearchValue);
            ViewState["contentSearchPageCount"] = outPageCount;

            labelMessages.Text = "Tìm thấy: " + _contentSearchData.RecordCount.ToString() + " kết quả";

            if (_contentSearchData.RecordCount >= 0)
            {
                DataTable Data = _contentSearchData.get_User_Search_Current_Page(_section_id, sSearchField, sSearchValue);

                // Create DataColumn objects of data types.
                DataColumn colString = new DataColumn("CONTENT_HTML");
                colString.DataType = System.Type.GetType("System.String");
                Data.Columns.Add(colString);
                string sContentXml = "";
                CRecord contentRec = new CRecord();
                Int16 iContentId = 0;
                string sTemplateName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    iContentId = Int16.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());
                    sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iContentId, 1);//no NTEXTS CONTENT_XML                  
                    contentRec.load_Xml(sContentXml);

                    int iCatId = int.Parse(contentRec.Controlfields.Controlfield("002").Value.ToString());

                    UrlQuery postURL = new UrlQuery(_default_post_page);

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
                            postURL = new UrlQuery(MenuTable.Rows[0]["MENU_LINK_URL"].ToString());                        
                        }
                        postURL.Set("contentid",iContentId.ToString());
                        Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", postURL.AbsoluteUri);
                        
                    }
                    else
                    {
                        postURL.Set("contentid", iContentId.ToString());
                        Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", postURL.AbsoluteUri);
                    }
                }
                contentSearchRepeater.DataSource = Data;
                contentSearchRepeater.DataBind();
            }
            else
            {
                labelMessages.Text = "Không tìm thấy kết quả nào cho từ khóa: <font face='arial' color='red'><b>" + sSearchValue + "</b></font>";
                contentSearchRepeater.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void contentSearchPageBind()
    {

        string sSearchField = null;
        string sSearchValue = null;


        if (CommonUtility.GetInitialValue("s_searchfield", null) != null)
        {
            sSearchField = CommonUtility.GetInitialValue("s_searchfield", null).ToString();
        }
        if (CommonUtility.GetInitialValue("s_searchvalue", null) != null)
        {
            sSearchValue = CommonUtility.GetInitialValue("s_searchvalue", null).ToString();
        }

        _contentSearchData.PageNumber = Convert.ToInt16(ViewState["contentSearchPageNumber"]);
        _contentSearchData.RecordsPerPage = (int)ViewState["contentSearchPageSize"];
        _contentSearchData.PageCount = (int)ViewState["contentSearchPageCount"];
        DataTable Data = _contentSearchData.get_User_Search_Current_Page(_section_id, sSearchField, sSearchValue);

        // Create DataColumn objects of data types.

        DataColumn colString = new DataColumn("CONTENT_HTML");
        colString.DataType = System.Type.GetType("System.String");
        Data.Columns.Add(colString);
        string sContentXml = "";
        CRecord contentRec = new CRecord();
        Int16 iContentId = 0;
        string sTemplateName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
        for (int i = 0; i < Data.Rows.Count; i++)
        {

            iContentId = Int16.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());
            sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iContentId, 1);//no NTEXTS CONTENT_XML                  
            contentRec.load_Xml(sContentXml);

            UrlQuery postURL = new UrlQuery(_default_post_page);

            int iCatId = int.Parse(contentRec.Controlfields.Controlfield("002").Value.ToString());

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
                    postURL = new UrlQuery(MenuTable.Rows[0]["MENU_LINK_URL"].ToString());
                }
                postURL.Set("contentid", iContentId.ToString());
                Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", postURL.AbsoluteUri);

            }
            else
            {
                postURL.Set("contentid", iContentId.ToString());
                Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", postURL.AbsoluteUri);
            }



        }
        contentSearchRepeater.DataSource = Data;
        contentSearchRepeater.DataBind();
    }
    protected void contentSearchDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["contentSearchPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["contentSearchPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            contentSearchPageBind();
    }
    override protected void OnInit(EventArgs e)
    {
        _contentSearchData = new LegoWebSite.DataProvider.ContentSearchDataProvider();

    }
}
