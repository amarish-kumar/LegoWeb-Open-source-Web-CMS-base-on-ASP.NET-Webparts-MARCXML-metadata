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

public partial class Webparts_MenuTree : WebPartBase
{
    private int _root_menu_id = 0;
    private int _menu_type_id = 0;

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Root Menu Id
    /// </summary>
    public int root_menu_id
    {
        get
        {
            return _root_menu_id;
        }
        set
        {
            _root_menu_id = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Root Menu Id
    /// </summary>
    public int menu_type_id
    {
        get
        {
            return _menu_type_id;
        }
        set
        {
            _menu_type_id = value;
        }
    }

    public Webparts_MenuTree()
    {
        this.Title = "MENUTREE - TRÌNH ĐƠN HÌNH CÂY";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sTempMenu = "";

            if (_root_menu_id == 0 && _menu_type_id==0)
            {
                this.ltMenuTitle.Text = "Chưa thiết lập tham số";
                return;
            }
            //set title menu
            if (_root_menu_id > 0)
            {
                DataTable tblMenus = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(_root_menu_id).Tables[0];
                if (tblMenus.Rows.Count > 0)
                {
                    this.ltMenuTitle.Text = tblMenus.Rows[0]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString();
                }
                else
                {
                    this.ltMenuTitle.Text = "Không có dữ liệu";
                    return;
                }
            }
            else if(_menu_type_id>0)
            {
                DataTable tblMenuType = LegoWebSite.Buslgic.MenuTypes.get_MENU_TYPES_BY_ID(_menu_type_id).Tables[0];
                if (tblMenuType.Rows.Count > 0)
                {
                    this.ltMenuTitle.Text = tblMenuType.Rows[0]["MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString();
                }
                else
                {
                    this.ltMenuTitle.Text = "Không có dữ liệu";
                    return;
                }            
            }

            //try to discovery Active Menu Id

            int iActiveMenuId = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());

            if (iActiveMenuId == 0)
            {
                //try to find active Menu Id from contentid
                if (CommonUtility.GetInitialValue("contentid", null) != null)
                {
                    int iContentId = int.Parse(CommonUtility.GetInitialValue("contentid", 0).ToString());
                    if (iContentId > 0)
                    {
                        int iCateroryId = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(iContentId);
                        if (iCateroryId > 0)
                        {
                            iActiveMenuId = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCateroryId).Tables[0].Rows[0]["MENU_ID"].ToString());
                        }
                    }
                }
                else //try to find Active Menu Id from catid parameter
                {
                    if (CommonUtility.GetInitialValue("catid", null) != null)
                    {
                        int iCateroryId = int.Parse(CommonUtility.GetInitialValue("catid", 0).ToString());
                        iActiveMenuId = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCateroryId).Tables[0].Rows[0]["MENU_ID"].ToString());
                    }
                }
            }
            int iParentActiveMenuId = 0;
            if(iActiveMenuId > 0)
            {
                DataTable mnuTab=LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iActiveMenuId).Tables[0];
                if(mnuTab.Rows.Count>0)
                {
                   iParentActiveMenuId= int.Parse(mnuTab.Rows[0]["PARENT_MENU_ID"].ToString());
                }
            }
            //set tree menu


            DataTable tbRootCate ;
                
            if(_root_menu_id>0)
            {
                tbRootCate = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(_root_menu_id).Tables[0];
            }
            else
            {
                tbRootCate = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(0,_menu_type_id).Tables[0];
            }

            if (tbRootCate.Rows.Count > 0)
            {

                sTempMenu += "<ul id='menuList'>";

                for (int i = 0; i < tbRootCate.Rows.Count; i++)
                {
                    int mnuLevel0Id = int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString());

                    if ((mnuLevel0Id == iActiveMenuId) || (mnuLevel0Id == iParentActiveMenuId) && i <= (tbRootCate.Rows.Count - 1))
                    {
                        sTempMenu += "<li  class='active'>";
                    }
                    else
                    {
                        sTempMenu += "<li class='fly'>";
                    }
                    sTempMenu += "<a href='" + tbRootCate.Rows[i]["MENU_LINK_URL"] + "'>";
                    sTempMenu += "<span class='text'>" + tbRootCate.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</span>";
                    sTempMenu += "</a>";

                    DataTable childR = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString()),_menu_type_id).Tables[0];

                    if (childR.Rows.Count > 0) //check data
                    {
                        sTempMenu += "<ul>";
                        for (int j = 0; j < childR.Rows.Count; j++)
                        {
                            sTempMenu += "<li>";
                            sTempMenu += "<a href='" + childR.Rows[j]["MENU_LINK_URL"].ToString() + "'>" + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</a>";
                            sTempMenu += "</li>";
                        }
                        sTempMenu += " </ul>";
                    }
                    sTempMenu += "</li>";
                }
                sTempMenu += "</ul>";
            }
            this.ltMenuTreeItems.Text = sTempMenu;
        }
    }


}

