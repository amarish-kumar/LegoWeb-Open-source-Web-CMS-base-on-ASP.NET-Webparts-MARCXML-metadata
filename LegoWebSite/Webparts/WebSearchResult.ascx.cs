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

public partial class Webparts_WebSearchResult: WebPartBase
{
    private string _template_name = "AnhNhandeTomtatTimkiem";
    private int _section_id = 1;
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Content Category
    /// </summary>
    public string template_name
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
    /// <summary>
    /// Set Content Category
    /// </summary>
    public int section_id
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


    protected LegoWebSite.DataProvider.MetaContentDataProvider _contentSearchData;
    public enum SortFields { Default };

    public Webparts_WebSearchResult()
    {
        this.Title = "WEB SEARCH RESULT - KẾT QUẢ TÌM NỘI DUNG WEB";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CommonUtility.InitializeGridParameters(ViewState, "contentSearch", typeof(SortFields), 1, 100);
                ViewState["contentSearchPageNumber"] = 1;
                ViewState["contentSearchPageSize"] = 10;
                contentSearchBind();
            }
         
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void contentSearchBind()
    {
      
        string sSearchField = null;
        string sSearchValue = null;
        try
        {
            if (CommonUtility.GetInitialValue("SearchField", null) != null)
            {
                sSearchField = CommonUtility.GetInitialValue("SearchField", null).ToString();
            }
            if (CommonUtility.GetInitialValue("SearchValue", null) != null)
            {
                sSearchValue = CommonUtility.GetInitialValue("SearchValue", null).ToString();
            }

            int outPageCount = 0;

            _contentSearchData.PageNumber = Convert.ToInt16(ViewState["contentSearchPageNumber"]);
            _contentSearchData.RecordsPerPage = (int)ViewState["contentSearchPageSize"];
            _contentSearchData.get_User_Search_Count(out outPageCount, _section_id,sSearchField, sSearchValue);
            ViewState["contentSearchPageCount"] = outPageCount;

            labelMessages.Text = "<span>Tìm thấy: " + _contentSearchData.RecordCount.ToString() + " kết quả.</span>";

            if (_contentSearchData.RecordCount>= 0)
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
                    sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iContentId, false);//no NTEXTS CONTENT_XML                  
                    contentRec.load_Xml(sContentXml);

                    int iCatId = int.Parse(contentRec.Controlfields.Controlfield("002").Value.ToString());
                    string postURL= "";

                    //try to findout related menuid to get postURL
                    int iMnuId = 0;
                    int iParentCatId=-1;
                    while (iMnuId == 0 && iParentCatId != 0)
                    {
                        DataTable CatTable=LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCatId).Tables[0];
                        iParentCatId= int.Parse(CatTable.Rows[0]["PARENT_CATEGORY_ID"].ToString());
                        iCatId = iParentCatId;
                        iMnuId = int.Parse(CatTable.Rows[0]["MENU_ID"].ToString());
                    }                                                           
                    if (iMnuId > 0)
                    {
                        DataTable MenuTable = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iMnuId).Tables[0];
                        postURL = MenuTable.Rows[0]["MENU_LINK_URL"].ToString();
                        if (!String.IsNullOrEmpty(postURL))
                        {
                            if (postURL.IndexOf("?") > 0)
                            {
                                if (!postURL.EndsWith("?") && !postURL.EndsWith("&")) postURL += "&";
                            }
                            else
                            {
                                postURL += "?";
                            }
                            Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}",postURL );
                        }
                    }
                    else
                    {
                        Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}","News.aspx?");                    
                    }
                }
                contentSearchRepeater.DataSource = Data;
                contentSearchRepeater.DataBind();
            }
            else
            {
                labelMessages.Text = "<span>Không tìm thấy kết quả nào cho từ khóa: <font face='arial' color='red'><b>" + sSearchValue + "</b></font></span>";
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

      
        if (CommonUtility.GetInitialValue("SearchField", null) != null)
        {
            sSearchField = CommonUtility.GetInitialValue("SearchField", null).ToString();
        }
        if (CommonUtility.GetInitialValue("SearchValue", null) != null)
        {
            sSearchValue = CommonUtility.GetInitialValue("SearchValue", null).ToString();
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
            sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iContentId,false);//no NTEXTS CONTENT_XML                  
            contentRec.load_Xml(sContentXml);

            int iCatId = int.Parse(contentRec.Controlfields.Controlfield("002").Value.ToString());
            string postURL = "";

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
                postURL = MenuTable.Rows[0]["MENU_LINK_URL"].ToString();
                if (!String.IsNullOrEmpty(postURL))
                {
                    if (postURL.IndexOf("?") > 0)
                    {
                        if (!postURL.EndsWith("?") && !postURL.EndsWith("&")) postURL += "&";
                    }
                    else
                    {
                        postURL += "?";
                    }
                    Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", postURL);
                }
            }
            else
            {
                Data.Rows[i]["CONTENT_HTML"] = contentRec.XsltFile_Transform(sTemplateName).Replace("{POST_URL}", "News.aspx?");
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
        _contentSearchData = new  MetaContentDataProvider();
      
    }
}
