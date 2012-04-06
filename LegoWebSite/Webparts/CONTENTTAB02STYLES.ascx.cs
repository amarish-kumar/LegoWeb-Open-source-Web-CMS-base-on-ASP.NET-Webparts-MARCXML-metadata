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
/// Display multi category tabs, each tabs display 2 columns of content list 
/// </summary>
public partial class Webparts_CONTENTTAB02STYLES : WebPartBase
{
    private int _category_id = 0;
    private int _number_of_record = 5;
    private string _left_template_name = "lgwdsp_thumbtitlesummary";
    private string _right_template_name = "lgwdsp_titletipsummary";
    private string _default_post_page = "ContentBrowser.aspx";

    public Webparts_CONTENTTAB02STYLES()
    {
        this.Title = "TAB CONTENT LIST TWO STYLES";
    }

    #region webpart properties

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("1.Category id:")]
    [WebDescription("Select the category to get contents of.")]
    /// <summary>
    /// category_id
    /// </summary>
    public int p1_category_id
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
    [WebDisplayName("2.Number of records:")]
    [WebDescription("Set number of records to display")]
    /// <summary>
    /// number of record to display
    /// </summary>
    public int p2_number_of_record
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
    [WebDisplayName("3.Left template name:")]
    [WebDescription("Select a xslt template name for formating first record on the left")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p3_left_template_name
    {
        get
        {
            return _left_template_name;
        }
        set
        {
            _left_template_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("4.Right template name:")]
    [WebDescription("Select a xslt template name for formating other record on the right")]
    /// <summary>
    /// template for transforming record to html
    /// </summary>
    public string p4_right_template_name
    {
        get
        {
            return _right_template_name;
        }
        set
        {
            _right_template_name = value;
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

    protected override void OnPreRender(EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude(
                GetType(),
                "jqTabsExample",
                ResolveClientUrl("~/js/jqTabsExample.js"));

        Page.ClientScript.RegisterClientScriptInclude(
                GetType(),
                "jquery.tabs.pack",
                ResolveClientUrl("~/js/jquery.tabs.pack.js"));               
        base.OnPreRender(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_category_id == 0)
            {
                this.litTabControlTitle.Text = "Category id prarameter is not set!";
                return;
            }

            DataTable catData = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(_category_id).Tables[0];

            string TitleRoot = catData.Rows[0]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
            if (catData.Rows.Count > 0)
            {
                this.litTabControlTitle.Text = "<a href ='ContentNavigator.aspx?catid=" + int.Parse(catData.Rows[0]["CATEGORY_ID"].ToString()) + "'>" + TitleRoot + "</a>";
            }

            UrlQuery myPost = new UrlQuery();
            if (!String.IsNullOrEmpty(_default_post_page))
            {
                myPost = new UrlQuery(_default_post_page);
            }
            String sTabContents = "";

            DataTable tblTabCate = LegoWebSite.Buslgic.Categories.get_CATEGORY_CHILREN(_category_id).Tables[0];

            string stabs = "";
            stabs += "<div class='container'><ul>";
            for (int i = 0; i < tblTabCate.Rows.Count; i++)
            {

                stabs += "<li>";
                stabs += "<a  href='#" + tblTabCate.Rows[i]["CATEGORY_ID"].ToString() + "'><span>" + tblTabCate.Rows[i]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString() + "</span></a>";
                stabs += "</li>";

                sTabContents += "<div id='" + tblTabCate.Rows[i]["CATEGORY_ID"].ToString() + "'>";

                //get content by tab menu
                DataTable tabDataContent = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(int.Parse(tblTabCate.Rows[i]["CATEGORY_ID"].ToString()), _number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());
                CRecord myRec = new CRecord();
                if (tabDataContent.Rows.Count == 1)
                {
                    myRec = new CRecord();
                    string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_left_template_name);
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)tabDataContent.Rows[0]["META_CONTENT_ID"], 0));
                    myPost.Set("contentid", tabDataContent.Rows[0]["META_CONTENT_ID"].ToString());
                    myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                    sTabContents += "<div style='float:left;width:100%;'>" + myRec.XsltFile_Transform(sTemplateFileName) + "</div>";
                }
                else if (tabDataContent.Rows.Count > 1)
                {
                    myRec = new CRecord();

                    string sTempLeftFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_left_template_name);
                    string sTempRightFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_right_template_name);

                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)tabDataContent.Rows[0]["META_CONTENT_ID"], 0));
                    myPost.Set("contentid", tabDataContent.Rows[0]["META_CONTENT_ID"].ToString());
                    myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                    sTabContents += "<div style='float:left;width:50%;'>" + myRec.XsltFile_Transform(sTempLeftFileName) + "</div>";

                    sTabContents += "<div style='float:right;width:47%;'>";
                    CRecords outRecs = new CRecords();
                    for (int j = 1; j < tabDataContent.Rows.Count; j++)
                    {
                        myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)tabDataContent.Rows[j]["META_CONTENT_ID"], 0));
                        myPost.Set("contentid", tabDataContent.Rows[j]["META_CONTENT_ID"].ToString());
                        myRec.Controlfields.Controlfield("001").Value = myPost.AbsoluteUri;
                        outRecs.Add(myRec);
                    }
                    sTabContents += outRecs.XsltFile_Transform(sTempRightFileName);
                    sTabContents += "</div>";
                }
                sTabContents += "</div>";
            }
            stabs += "</ul>";
            stabs += sTabContents;
            stabs += "</div>";
            this.litTabs.Text = stabs;
        }
     }
}
