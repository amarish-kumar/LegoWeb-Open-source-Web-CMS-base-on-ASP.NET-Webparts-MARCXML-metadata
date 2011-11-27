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

public partial class Webparts_LBTTOP2TEMPLATE : WebPartBase
{
    public enum OrderBy
    {
        Date = 0,
        OrderNumber = 1
    }
    private string _box_css_name = null;//mean no box around
    private int _category_id = 0;
    private int _number_of_record = 5;
    private OrderBy _order_by = OrderBy.Date;
    private string _top_template_name = "lgwdsp_thumbtitlesummary";
    private string _bottom_template_name = "lgwdsp_title";
    private string _default_post_page = "news.aspx";
   

    public Webparts_LBTTOP2TEMPLATE()
    {
        this.Title = "TOP CONTENTS LIST TWO STYLES";
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
    [WebDisplayName("2.Category id:")]
    [WebDescription("Select the category to get contents of.")]
    /// <summary>
    /// category_id
    /// </summary>
    public int p2_category_id
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
    [WebDisplayName("3.Order by:")]
    [WebDescription("Set order by clause to get top records.")]
    /// <summary>
    /// category
    /// </summary>
    public OrderBy p3_order_by
    {
        get
        {
            return _order_by;
        }
        set
        {
            _order_by = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("4.Number of records:")]
    [WebDescription("Set number of records to display")]
    /// <summary>
    /// number of record to display
    /// </summary>
    public int p4_number_of_record
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
    [WebDisplayName("5.Top template name:")]
    [WebDescription("Select a xslt template name for formating first record")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p5_top_template_name
    {
        get
        {
            return _top_template_name;
        }
        set
        {
            _top_template_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("6.Bottom template name:")]
    [WebDescription("Select a xslt template name for formating first record")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p6_bottom_template_name
    {
        get
        {
            return _bottom_template_name;
        }
        set
        {
            _bottom_template_name = value;
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
            DataTable catData = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(_category_id).Tables[0];
            if (catData.Rows.Count > 0)
            {
                this.Title = catData.Rows[0]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
            }
            else
            {
                this.litContent.Text = "<H3>category_id is not vailable!</H3>";
                return;
            }

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

            DataTable cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(_category_id, _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower(), _order_by == OrderBy.OrderNumber ? "ORDER BY ORDER_NUMBER ASC" : null);
            string outHTML = "";
            if (cntData.Rows.Count > 0)
            {
                CRecord myRec = new CRecord();
                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_top_template_name);
                myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[0]["META_CONTENT_ID"], 0));
                outHTML = myRec.XsltFile_Transform(sTemplateFileName);
                UrlQuery myPost = new UrlQuery(String.IsNullOrEmpty(_default_post_page) == true ? Request.Url.AbsolutePath : _default_post_page);
                myPost.Set("contentid", cntData.Rows[0]["META_CONTENT_ID"].ToString());
                this.litContent.Text += outHTML.Replace("{POST_URL}", myPost.AbsoluteUri);
            }
            else
            {
                this.litContent.Text ="<H3>Data is not available!</H3>";
            }
            if (cntData.Rows.Count > 1)
            {                
                CRecord myRec = new CRecord();
                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_bottom_template_name);
                for (int i = 1; i < cntData.Rows.Count; i++)
                {
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[i]["META_CONTENT_ID"], 0));
                    outHTML = myRec.XsltFile_Transform(sTemplateFileName);
                    UrlQuery myPost = new UrlQuery(String.IsNullOrEmpty(_default_post_page) == true ? Request.Url.AbsolutePath : _default_post_page);
                    myPost.Set("contentid", cntData.Rows[i]["META_CONTENT_ID"].ToString());
                    this.litContent.Text += outHTML.Replace("{POST_URL}", myPost.AbsoluteUri);
                }
            }
        }
     }
}
