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

public partial class Webparts_CONTENTHORIZONTALCROLLER : WebPartBase
{  
    private int _category_id = 0;
    private int _number_of_record = 5;
    private string _template_name = "lgwdsp_horizontalscroller";
    private string _default_post_page = "news.aspx";
    private string _box_css_name = null;//mean no box around  
    private int _page_size = 2;
    private int _image_width = 150;
    private int _image_height = 100;
    public Webparts_CONTENTHORIZONTALCROLLER()
    {
        this.Title = "CONTENT HORIZONTAL SCROLLER AMAZON STYLE";
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
    [WebDisplayName("3.Number of records:")]
    [WebDescription("Set number of records to display")]
    /// <summary>
    /// number of record to display
    /// </summary>
    public int p3_number_of_record
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
    [WebDisplayName("4.Template name:")]
    [WebDescription("Select xslt template name")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p4_template_name
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
    [WebDisplayName("5.Default post page:")]
    [WebDescription("Set default post url for content link href")]
    /// <summary>
    /// post to page when click links
    /// </summary>
    public string p5_default_post_page
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
    [WebDisplayName("6.Page size:")]
    [WebDescription("Set number of images display in frame")]
    /// <summary>
    /// 
    /// </summary>
    public int p6_page_size
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
    [WebDisplayName("7.Image width:")]
    [WebDescription("Set width of thumb image")]
    /// <summary>
    /// 
    /// </summary>
    public int p7_image_width
    {
        get
        {
            return _image_width;
        }
        set
        {
            _image_width = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("8.Image height:")]
    [WebDescription("Set height of thumb image")]
    /// <summary>
    /// 
    /// </summary>
    public int p8_image_height
    {
        get
        {
            return _image_height;
        }
        set
        {
            _image_height = value;
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

            string slidescroll = @"<script language='javascript' type='text/javascript'>
            $(function() {
                $('.webwidget_scroller_amazon').webwidget_scroller_amazon({
                    scroller_title_show: 'enable',//enable  disable
                    scroller_time_interval: '4000',
                    scroller_window_background_color: 'none',
                    scroller_window_padding: '5',
                    scroller_border_size: '0',
                    scroller_border_color: '#CCC',
                    scroller_images_width: '"+ _image_width.ToString() + @"',
                    scroller_images_height: '"+ _image_height.ToString() + @"',
                    scroller_title_size: '12',
                    scroller_title_color: 'black',
                    scroller_show_count: '"+ _page_size.ToString() + @"',
                    directory: 'images'
                });
            });
            </script>";
            UrlQuery myPost = new UrlQuery();
            if (!String.IsNullOrEmpty(_default_post_page))
            {
                myPost = new UrlQuery(_default_post_page);
            }
            string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
            CRecords outRecs = new CRecords();
            CRecord myRec = new CRecord();            
            DataTable cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(_category_id, _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());
            for (int i = 0; i < cntData.Rows.Count; i++)
            {
                myRec = new CRecord();              
                myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[i]["META_CONTENT_ID"], 0));
                myPost.Set("contentid", cntData.Rows[i]["META_CONTENT_ID"].ToString());
                myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                outRecs.Add(myRec);
            }
            this.litContent.Text+="<div id='webwidget_scroller_amazon' class='webwidget_scroller_amazon'><div class='webwidget_scroller_simple2_mask'> <ul>";
            this.litContent.Text+= outRecs.XsltFile_Transform(sTemplateFileName);
            this.litContent.Text += "</ul></div><ul class='webwidget_scroller_simple2_nav'><li></li> <li></li></ul><div style='clear: both'></div></div>";
            Page.RegisterStartupScript("slidesroll", slidescroll);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude(
            GetType(),
            "webwidget_scroller_amazon",
            ResolveClientUrl("~/js/webwidget_scroller_amazon.js"));
        base.OnPreRender(e);
    }
}