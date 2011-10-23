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
using LegoWebSite.Buslgic;

public partial class Webparts_ContentBrowser : WebPartBase
{
    private int _number_of_record = 10;
    private string _default_post_page = null;
    private string _mainlist_template_name = "web_AnhNhandeTomtat";
    private string _footerlist_template_name = "web_Nhande";
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// khuon mau hien thi danh sach noi dung chinh khi co hon 1 noi dung tim thay
    /// </summary>
    public string Mainlist_Template_Name
    {
        get
        {
            return _mainlist_template_name;
        }
        set
        {
            _mainlist_template_name = value;
        }
    }
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// khuon mau hien thi danh sach noi dung lien quan duoi chan trang
    /// </summary>
    public string Footerlist_Template_Name
    {
        get
        {
            return _footerlist_template_name;
        }
        set
        {
            _footerlist_template_name = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set 
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
    public Webparts_ContentBrowser()
    {
        this.Title = "CONTENT BROWSER";    
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int meta_content_id = 0;
        int category_id = 0;
        int menu_id=0;
        
        if (!IsPostBack)
        {
            string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
            if (CommonUtility.GetInitialValue("contentid", null) != null)
            {
                meta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
            }
            if (meta_content_id > 0)
            {
                if (!LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(meta_content_id))
                {
                    divContentBrowser.InnerHtml = "<H3>Xin lỗi, nội dung không còn tồn tại!</H3>";
                    return;
                }
                category_id = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(meta_content_id);
                //Display Navigator Info
				this.divTopNavigator.InnerHtml += "<img src='images/arr-dot.gif' border='0' style='float:left;padding-top:13px'/> <span class='textleft'>" + LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, (_default_post_page == Request.Url.AbsolutePath ? "DocumentBrowser.aspx" : Request.Url.AbsolutePath)) + "</span>";

                
                #region verify access right
                //verify access right
                int iAccessLevel = LegoWebSite.Buslgic.MetaContents.get_ACCESS_LEVEL(meta_content_id);
                switch (iAccessLevel)
                { 
                    case 1: //need logedin
                        if (!Page.User.Identity.IsAuthenticated)
                        {
                            divContentBrowser.InnerHtml = "<span><b>Bạn cần đăng nhập để xem nội dung này! <br/> Only registered users can view details</b></span>";
                            return;
                        }
                    break;
                    case 2:
                    if (!Page.User.Identity.IsAuthenticated)
                    {
                        divContentBrowser.InnerHtml = "<span><b>Bạn cần đăng nhập để xem nội dung này!<br/> Only registered users can view details</b></span>";
                        return;
                    }
                    else //verify user roles
                    {
                        string[] sAllowAccessRoles = LegoWebSite.Buslgic.MetaContents.get_ACCESS_ROLES(meta_content_id);
                        string[] sUserRoles = Roles.GetRolesForUser(Page.User.Identity.Name);
                        bool bAllowAccess = false;
                        if (sUserRoles != null && sUserRoles.Length > 0 && sAllowAccessRoles!=null && sAllowAccessRoles.Length>0)
                        {
                            for (int x = 0; x < sUserRoles.Length; x++)
                            {
                                for (int y = 0; y < sAllowAccessRoles.Length; y++)
                                {
                                    if (sUserRoles[x] == sAllowAccessRoles[y])
                                    {
                                        bAllowAccess = true;
                                        break;
                                    }
                                }
                                if (bAllowAccess) break;
                            }
                        }
                        if (!bAllowAccess)
                        {
                            divContentBrowser.InnerHtml = "<span><b>Bạn không thuộc nhóm quyền xem nội dung này!<br/> You are not authorized to view details</b></span>";
                            return;                        
                        }
                    }
                    break;
                }
                #endregion verify access right

                //Display Content Details
                CRecord myRec = new CRecord();
                myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, true));
                string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(LegoWebSite.Buslgic.Categories.get_CATEGORY_TEMPLATE_NAME(category_id));
                divContentBrowser.InnerHtml = myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");               
                //increase read count
                LegoWebSite.Buslgic.MetaContents.increase_READ_COUNT(meta_content_id);
                DataTable relData = LegoWebSite.Buslgic.MetaContents.get_TOP_RELATED_CONTENTS(category_id, number_of_record , sLangCode, new int[] { meta_content_id });
                
                if (relData.Rows.Count > 0)
                {
                    this.divRelatedContents.Visible = true;
                    for (int i = 0; i < relData.Rows.Count; i++)
                    {
                        meta_content_id = (int)relData.Rows[i]["META_CONTENT_ID"];
                        myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, true));
                        sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_footerlist_template_name);
                        this.divRelatedContentDetails.InnerHtml += myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");
                    }
                }
                else
                {
                    this.divRelatedContents.Visible = false;
                }
            }
            else
            {
                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                    category_id = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
                }
                else if(CommonUtility.GetInitialValue("mnuid",null)!=null)
                {
                    menu_id = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
                    category_id = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menu_id);
                }
                if (!LegoWebSite.Buslgic.Categories.is_CATEGORY_EXIST(category_id))
                {
                    divContentBrowser.InnerHtml = "<H3>Xin lỗi, nội dung liên kết với trình đơn không còn tồn tại!</H3>";
                    return;
                }

                //Display navigator
				this.divTopNavigator.InnerHtml += "<img src='images/arr-dot.gif' border='0' style='float:left;padding-top:13px'/> <span class='textleft'>" + LegoWebSite.Buslgic.Categories.get_NavigatePath(category_id, Request.Url.AbsolutePath) + "</span>";
              
                DataTable cntData = LegoWebSite.Buslgic.MetaContents.get_TOP_NEWS_CONTENTS(category_id, number_of_record, sLangCode);

                if (cntData.Rows.Count > 0)
                {
                   //special category only have one content - > display content details if count=1
                    if (cntData.Rows.Count == 1)
                    {
                        meta_content_id = (int)cntData.Rows[0]["META_CONTENT_ID"];
                        #region verify access right
                        //verify access right
                        int iAccessLevel = LegoWebSite.Buslgic.MetaContents.get_ACCESS_LEVEL(meta_content_id);
                        switch (iAccessLevel)
                        {
                            case 1: //need logedin
                                if (!Page.User.Identity.IsAuthenticated)
                                {
                                    divContentBrowser.InnerHtml = "<span><b>Bạn cần đăng nhập để xem nội dung này! <br/> Only registered users can view details</b></span>";
                                    return;
                                }
                                break;
                            case 2:
                                if (!Page.User.Identity.IsAuthenticated)
                                {
                                    divContentBrowser.InnerHtml = "<span><b>Bạn cần đăng nhập để xem nội dung này!<br/> Only registered users can view details</b></span>";
                                    return;
                                }
                                else //verify user roles
                                {
                                    string[] sAllowAccessRoles = LegoWebSite.Buslgic.MetaContents.get_ACCESS_ROLES(meta_content_id);
                                    string[] sUserRoles = Roles.GetRolesForUser(Page.User.Identity.Name);
                                    bool bAllowAccess = false;
                                    if (sUserRoles != null && sUserRoles.Length > 0 && sAllowAccessRoles != null && sAllowAccessRoles.Length > 0)
                                    {
                                        for (int x = 0; x < sUserRoles.Length; x++)
                                        {
                                            for (int y = 0; y < sAllowAccessRoles.Length; y++)
                                            {
                                                if (sUserRoles[x] == sAllowAccessRoles[y])
                                                {
                                                    bAllowAccess = true;
                                                    break;
                                                }
                                            }
                                            if (bAllowAccess) break;
                                        }
                                    }
                                    if (!bAllowAccess)
                                    {
                                        divContentBrowser.InnerHtml = "<span><b>Bạn không thuộc nhóm quyền xem nội dung này!<br/> You are not authorized to view details</b></span>";
                                        return;
                                    }
                                }
                                break;
                        }
                        #endregion verify access right
                        CRecord myRec = new CRecord();
                        myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, true));
                        string sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(LegoWebSite.Buslgic.Categories.get_CATEGORY_TEMPLATE_NAME(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(meta_content_id)));
                        divContentBrowser.InnerHtml = myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");
                    }
                    else
                    {
                        //
                        for (int i = 0; i < cntData.Rows.Count; i++)
                        {
                            CRecord myRec = new CRecord();
                            meta_content_id = (int)cntData.Rows[i]["META_CONTENT_ID"];
                            myRec.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(meta_content_id, true));
                            string sTemplateFileName = "";
                            if (i < 5)
                            {
                                sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_mainlist_template_name);
                                this.divContentBrowser.InnerHtml += myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");
                            }
                            else
                            {
                                this.divRelatedContents.Visible = true;
                                sTemplateFileName = LegoWebSite.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(_footerlist_template_name);
                                this.divRelatedContentDetails.InnerHtml += myRec.XsltFile_Transform(sTemplateFileName).Replace("{POST_URL}", Request.Url.AbsolutePath + "?");
                            }
                        }
                    }
                }
                
            }
        }
    }

      [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Content Category
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
}
