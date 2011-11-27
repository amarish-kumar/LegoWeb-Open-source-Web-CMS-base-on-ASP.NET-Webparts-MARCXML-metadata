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

public partial class Webparts_LBNEWSPOPUP :WebPartBase
{
    //get top news by section id or category id not both
    private int _section_id = 0;
    private int _category_id = 0;
    private int _important_level = 0;
    private int _number_of_record = 5;
    private string _box_css_name = null;//mean no box around

    public Webparts_LBNEWSPOPUP()
    {
        this.Title = "NEWS WITH POPUP";
    }


    #region webpart properties
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// category
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
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// category
    /// </summary>
    public int category_id
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
    /// <summary>
    /// number of record to display
    /// </summary>
    public int number_of_record
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
    /// <summary>
    /// important level of meta content records
    /// </summary>
    public int important_level
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
    /// <summary>
    /// set box css name to format round box
    /// </summary>
    public string box_css_name
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

    #endregion 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
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

            if(section_id>0)
            {
               cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_BY_SECTION(_section_id, number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower(),_important_level);
            }else
            {
                cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_BY_CATEGORY(_category_id, number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower(),_important_level);
            }

            if (cntData.Rows.Count > 0)
            {
                CRecord myRec = new CRecord();
                string sContentHTML = "<div id='firstNew'>";
                for (int i = 0; i < cntData.Rows.Count; i++)
                {
                    myRec = new CRecord();
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[i]["META_CONTENT_ID"], 0));

                    int iCatId = int.Parse(myRec.Controlfields.Controlfield("002").Value.ToString());
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
                        if (MenuTable.Rows.Count > 0)
                        {
                            postURL = MenuTable.Rows[0]["MENU_LINK_URL"].ToString();
                        }
                        if (String.IsNullOrEmpty(postURL))
                        {
                            postURL = "contentbrowser.aspx";
                        }
                    }
                    UrlQuery postQuery = new UrlQuery(postURL);


                    string sNewsId = myRec.Controlfields.Controlfield("001").Value;
                    string sTitle = myRec.Datafields.Datafield("245").Subfields.Subfield("a").Value;
                    string sSummary = myRec.Datafields.Datafield("245").Subfields.Subfield("b").Value;

                    postQuery.Set("contentid", sNewsId);

                    string sTemp = "<div class=\"p1_tooltip\"><a onmouseover=\"ShowContent('id{0}'); return true;\"" +
                                 " onmouseout=\"HideContent('id{0}'); return true;\"" +
                                 " href=\"{1}\">{2}</a>" +
                                 " <div id=\"id{0}\" style=\"display:none; position:fixed; border-style: solid 1px blue; background-color: #c6e6f6; padding: 5px 0px 2px 5px;width:250px\"><b>{2}</b><br/>{3}</div></div>";
                    sContentHTML += String.Format(sTemp, sNewsId, postQuery.AbsoluteUri, sTitle, sSummary);


                }
                sContentHTML += "</div>";
                this.litContent.Text = sContentHTML;
            }
            else
            {
                this.litContent.Text = "<H3>Data is unavailable!</H3>";
            }
        }
    }
}
