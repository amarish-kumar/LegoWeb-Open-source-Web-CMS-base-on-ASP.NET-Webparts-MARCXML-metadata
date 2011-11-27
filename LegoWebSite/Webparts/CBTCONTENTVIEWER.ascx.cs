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
/// Display one meta content record using template transformation.
/// use for: flash/movies/picture
/// parameters:
/// meta_content_id: id of metat content record, if set display specified record 
/// category_id: if meta_content_id not set, auto detech 1 last update meta content record in category_id
/// template_name: template file name to transform meta content record, if not set auto detect base on category template name
/// </summary>

public partial class Webparts_CBTCONTENTVIEWER: WebPartBase
{
    private int _category_id=0;
    private int _meta_content_id=0;
    private string _template_name=null;//mean auto detech
    private string _box_css_name = null;//mean no box around

    public Webparts_CBTCONTENTVIEWER()
    {
        this.Title = "CONTENT VIEWER BOX USING TEMPLATE";
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
    [WebDisplayName("2.Meta content id:")]
    [WebDescription("Set meta content id to display, if not select top one of category set below.")]
    /// <summary>
    /// meta_content_id: id of metat content record, if set display specified record
    /// </summary>
    public int p2_meta_content_id
    {
        get
        {
            return _meta_content_id;
        }
        set
        {
            _meta_content_id = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Category id:")]
    [WebDescription("Select the category to get top 1 content record of.")]
    /// <summary>
    /// category_id: if meta_content_id not set, auto detech 1 last update meta content record in category_id
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
    [WebDisplayName("4.Template name:")]
    [WebDescription("Select xslt template name")]
    /// <summary>
    /// template for transforming record to html
    /// template_name: template file name to transform meta content record, if not set auto detect base on category template name meta_content_id->category_id->template_name
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
    
    #endregion webparts properties

    protected void Page_Load(object sender, EventArgs e)
    {
        int metacontentid = 0;

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

            metacontentid= discover_content_id();            
            if (metacontentid==0 ||(metacontentid > 0 && !LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(metacontentid)))
            {
                this.litContent.Text = "<H3>No suitable data!</H3>";
                return;
            }            
            CRecord myRec = new CRecord();
            string sMetaXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(metacontentid, 1);
            myRec.load_Xml(sMetaXml);
            string sTemplateFileName;
            if (!string.IsNullOrEmpty(_template_name))
            {
                sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
            }
            else
            {
                sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(LegoWebSite.Buslgic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(myRec.Controlfields.Controlfield("002").Value)));       
            }
            string sOutHTML=myRec.XsltFile_Transform(sTemplateFileName);
            this.litContent.Text = sOutHTML;
        }
    }

    private int discover_content_id()
    {
        int contentid = 0;
        int categoryid = 0;
        int menuid = 0;
        if (_meta_content_id == 0)
        {
            if (_category_id == 0)
            {
                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                    categoryid = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
                }
                else if (CommonUtility.GetInitialValue("mnuid", null) != null)
                {                    
                    menuid = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
                    categoryid = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menuid);
                }
            }
            else
            {
                categoryid = _category_id;
            }
            
            //try to discover contentid            
            if (categoryid > 0)
            {
                DataTable top1Data = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(categoryid, 1, System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower(), null);
                if (top1Data.Rows.Count > 0)
                {
                    contentid = (int)top1Data.Rows[0]["META_CONTENT_ID"];
                }            
            }
        }
        else
        {
            contentid = _meta_content_id;
        }
        return contentid;
    }

}
