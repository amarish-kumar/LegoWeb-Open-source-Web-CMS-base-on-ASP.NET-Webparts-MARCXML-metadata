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

public partial class Webparts_TopReadBox : WebPartBase
{

    private int _category_id = 0;
    private int _number_of_record = 5;
    private string _template_name = "DocNhieuNhat";    
    private string _default_post_page = "default.aspx";


    public Webparts_TopReadBox()
    {
        this.Title = "TOP READ BOX";
    }


    #region webpart properties

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
    /// template for display bottom records
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
    /// post to page when click links
    /// </summary>
    public string default_post_page
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
            this.Title = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("DOC_NHIEU_NHAT");
            DataTable cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_READ_CONTENTS(category_id, number_of_record, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());
            if (cntData.Rows.Count > 0)
            {
                CRecord myRec = new CRecord();
                CSubfield Sf = new CSubfield();

                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_template_name);
                for (int i = 0; i < cntData.Rows.Count; i++)
                {
                    myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML((int)cntData.Rows[i]["META_CONTENT_ID"], false));
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
                    this.divTopRead.InnerHtml += myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", (default_post_page == "" ? Request.Url.AbsolutePath : default_post_page) + "?");
                }
            }

        }
    }
}
