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

public partial class Webparts_CONTENTLISTTOPHIT:WebPartBase
{
    private string _box_css_name = null;//mean no box around
    private int _category_id = 0;
    private int _number_of_record = 5;
    private string _template_name = "lgwdsp_tophit";    
    private string _default_post_page = "contentbrowser.aspx";

    public Webparts_CONTENTLISTTOPHIT()
    {
        this.Title = "CONTENT LIST TOP HIT";
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
    [WebDescription("Set category id to gets the most read contents of.")]
    /// <summary>
    /// Set category id to gets the most read contents of
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
    [WebDescription("Set number of records to display in list")]
    /// <summary>
    /// Set number of all record to display in content browser and related content list
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
    [WebDescription("Set the template to transform records to display in list")]
    /// <summary>
    /// Set xslt template to transform record in list
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
    #endregion 
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
            DataTable cntData = LegoWebSite.Buslgic.MetaContents.get_MOST_READ_CONTENTS(_category_id, _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());            
            if (cntData.Rows.Count > 0)
            {
                CRecord myRec = new CRecord();
                CSubfield Sf = new CSubfield();

                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
                UrlQuery myPost = new UrlQuery();
                if (!String.IsNullOrEmpty(_default_post_page))
                {
                    myPost = new UrlQuery(_default_post_page);
                }
                CRecords outRecs = new CRecords();

                for (int i = 0; i < cntData.Rows.Count; i++)
                {
                    int meta_content_id=(int)cntData.Rows[i]["META_CONTENT_ID"];
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, 0));
                    if (myRec.Datafields.Datafield("245").Subfields.get_Subfield("n", ref Sf))
                    {
                        Sf.Value = cntData.Rows[i]["READ_COUNT"].ToString();
                    }
                    else
                    {
                        Sf.ReConstruct();
                        Sf.Code = "n";
                        Sf.Value = cntData.Rows[i]["READ_COUNT"].ToString();
                        myRec.Datafields.Datafield("245").Subfields.Add(Sf);
                    }
                    myPost.Set("contentid",cntData.Rows[i]["META_CONTENT_ID"].ToString());
                    myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                    outRecs.Add(myRec);
                }
                this.litContent.Text = outRecs.XsltFile_Transform(sTemplateFileName);
            }
        }
    }
}
