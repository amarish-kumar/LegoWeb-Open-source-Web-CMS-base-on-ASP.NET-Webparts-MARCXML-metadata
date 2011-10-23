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


public partial class Webparts_DocumentBrowser :WebPartBase
{
	private string _template_name = "DocBrief";

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
	
    protected LegoWebSite.DataProvider.MetaContentDataProvider _DocumentBrowserData;
    public enum SortFields { Default };

    public Webparts_DocumentBrowser()
    {
        this.Title = "DOCUMENT BROWSER - DUYỆT TÀI LIỆU";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int meta_content_id = 0;
        int category_id = 0;
        int menu_id = 0;

        try
        {
            if (!IsPostBack)
            {

//                if (!Page.User.Identity.IsAuthenticated || !Roles.IsUserInRole(Page.User.Identity.Name, "NHANVIENNPS"))
//                {
//                    DocumentBrowser.InnerHtml = @"<span style='padding 10px'><H3>You are not authorized to access this content!.</H3></span><br/>
//                                                  <span style='padding 10px'><H3>Bạn không có quyền truy cập vào nội dung này, liên hệ với quản trị để biết thêm chi tiết!.</H3></span>";
//                    return;
//                }

                CommonUtility.InitializeGridParameters(ViewState, "DocumentBrowser", typeof(SortFields), 1, 100);
                ViewState["DocumentBrowserPageNumber"] = 1;
                ViewState["DocumentBrowserPageSize"] = 10;

                string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
                
                if (CommonUtility.GetInitialValue("contentid", null) != null)
                {
                    meta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
                    Response.Redirect("DocumentDetails.aspx?contentid=" + meta_content_id.ToString());
                }

                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                    category_id = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
                }
                else if (CommonUtility.GetInitialValue("mnuid", null) != null)
                {
                    menu_id = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
                    category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
                }
                //Display navigator
				this.TitleTopMenu.InnerHtml += "<img src='images/arr-dot.gif' border='0' style='float:left;padding-top:13px'/> <span class='textleft'>" + LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, Request.Url.AbsolutePath) + "</span>";

                if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
                {
                    this.DocumentBrowser.InnerHtml = "<H3>Xin lỗi, nội dung liên kết với trình đơn không tồn tại!</H3>";
                    return;
                }

                DocumentBrowserBind(category_id,sLangCode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DocumentBrowserBind(int iCategory_id,string sLang_Code)
    {
      
        try
        {
           
            int outPageCount = 0;

            _DocumentBrowserData.PageNumber = Convert.ToInt16(ViewState["DocumentBrowserPageNumber"]);
            _DocumentBrowserData.RecordsPerPage = (int)ViewState["DocumentBrowserPageSize"];
            _DocumentBrowserData.get_Document_Page_Count(out outPageCount, iCategory_id, sLang_Code);
            ViewState["DocumentBrowserPageCount"] = outPageCount;

            if (_DocumentBrowserData.RecordCount >= 0)
            {
                DataTable Data = _DocumentBrowserData.get_Page_Document_Current_Page(iCategory_id, sLang_Code);
                // Create DataColumn objects of data types.
                DataColumn colString = new DataColumn("CONTENT_HTML");
                colString.DataType = System.Type.GetType("System.String");
                Data.Columns.Add(colString);
              
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    string sContentXml = "";
                    CRecord myRec = new CRecord();
                    int metacontent_id = int.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());

                    sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(metacontent_id, false);//no NTEXTS CONTENT_XML                  
                    myRec.load_Xml(sContentXml);
                    string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(template_name);
                    Data.Rows[i]["CONTENT_HTML"] = myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", "DocumentDetails.aspx" + "?");
                }
                DocumentBrowserRepeater.DataSource = Data;
                DocumentBrowserRepeater.DataBind();
            }
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DocumentBrowserPageBind()
    {
        int category_id = 0;
        int menu_id = 0;
        string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
      
        try
        {
            if (CommonUtility.GetInitialValue("catid", null) != null)
            {
                category_id = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
            }
            else if (CommonUtility.GetInitialValue("mnuid", null) != null)
            {
                menu_id = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
                category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
            }
            if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
            {
                this.DocumentBrowser.InnerHtml = "<H3>Xin lỗi, nội dung liên kết với trình đơn không còn tồn tại!</H3>";
                return;
            }
            _DocumentBrowserData.PageNumber = Convert.ToInt16(ViewState["DocumentBrowserPageNumber"]);
            _DocumentBrowserData.RecordsPerPage = (int)ViewState["DocumentBrowserPageSize"];
            _DocumentBrowserData.PageCount = (int)ViewState["DocumentBrowserPageCount"];

            DataTable Data = _DocumentBrowserData.get_Page_Document_Current_Page(category_id,sLangCode);
            // Create DataColumn objects of data types.
            DataColumn colString = new DataColumn("CONTENT_HTML");
            colString.DataType = System.Type.GetType("System.String");
            Data.Columns.Add(colString);
           
            for (int i = 0; i < Data.Rows.Count; i++)
            {
                string sContentXml = "";
                CRecord myRec = new CRecord();
                int metacontent_id = int.Parse(Data.Rows[i]["META_CONTENT_ID"].ToString());
                sContentXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(metacontent_id, false);//no NTEXTS CONTENT_XML                  
                myRec.load_Xml(sContentXml);
                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(template_name);
                Data.Rows[i]["CONTENT_HTML"] = myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");
            }
            DocumentBrowserRepeater.DataSource = Data;
            DocumentBrowserRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DocumentBrowserDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["DocumentBrowserPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["DocumentBrowserPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
       if (BindAllowed)
            DocumentBrowserPageBind();
    }
    override protected void OnInit(EventArgs e)
    {
        _DocumentBrowserData = new MetaContentDataProvider();
    }
}
