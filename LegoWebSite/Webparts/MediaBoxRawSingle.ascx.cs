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
/// Webpart này trình bày 1 nội dung siêu dữ liệu theo 1 template không có tác động đặc biệt khác.
/// Chủ yếu trình bày một hộp nội dung flash/movies/picture
/// parameters:
/// meta_content_id: là id của nội dung siêu dữ liệu cần trình diễn
/// category_id: nếu meta_content_id không được thiết lập chương trình tự tìm lấy 1 nội dung mới nhất thuộc category_id được thiết lập
/// template_name: là khuôn mẫu trình diễn cho nội dung siêu dữ liệu/ nếu không được thiết lập thì tự động tìm từ bản ghi nội dung
/// </summary>


public partial class Webparts_MediaBoxRawSingle: WebPartBase
{
    private int _category_id=0;
    private int _meta_content_id=0;
    private string _template_name=null;
    public Webparts_MediaBoxRawSingle()
    {
        this.Title = "MEDIA BOX RAW SINGLE - HỘP TRÌNH DIỄN ĐA PHƯƠNG TIỆN 1 NỘI DUNG";    
    }       
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(_meta_content_id==0)
            {
                //try to get last metacontent if category exist
                if(_category_id>0)
                {
                    DataTable contentTable=LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_BY_CATEGORY(_category_id, 1, System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());
                    if (contentTable.Rows.Count > 0)
                    {
                        _meta_content_id = (int)contentTable.Rows[0]["META_CONTENT_ID"];
                    }
                    else
                    {
                        divMediaBoxRawSingle.InnerHtml = "<H3>Không thấy dữ liệu!</H3>";
                        return;
                    }
                }else
                {
                    divMediaBoxRawSingle.InnerHtml = "<H3>Tham số chưa được thiết lập!</H3>";
                    return;
                }
            
            }
            if (_meta_content_id > 0 && !LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(_meta_content_id))
            {
                divMediaBoxRawSingle.InnerHtml = "<H3>Dữ liệu không tồn tại</H3>";
                return;
            }            

            CRecord myRec = new CRecord();
            string sMetaXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, true);
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
			this.divMediaBoxRawSingle.InnerHtml = myRec.XsltFile_Transform(sTemplateFileName);
        }
    }
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// nếu tham số này được thiết lập mà meta_content_id không được lập thì chương trình tự tìm 1 nội dung mới cập nhật nhất ở chuyên mục này
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
    /// thiết lập tham số này xác định nội dung cố định cần trình diễn
    /// </summary>
    public int meta_content_id
    {
        get {
            return _meta_content_id;
        }
        set 
        {
            _meta_content_id = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// thiết lập tham số này xác định mẫu trình diễn bắt buộc, nếu không tự tìm mẫu meta_content_id->category_id->template_name
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
}
