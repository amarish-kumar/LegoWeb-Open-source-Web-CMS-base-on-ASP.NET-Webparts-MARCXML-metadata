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

/// <summary>
/// Display menu left standard style with header and title is root menu title or menu type title.
/// If not set param auto dectect root menu by category id
/// Dynamic change MENU_LINK_URL depend on page context
/// </summary>

public partial class Webparts_MENUFLYOUT : WebPartBase
{
    private int _root_menu_id = 0;
    private int _menu_type_id = 0;
    private bool _show_menu_icon = false;

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("1.Root menu id:")]
    [WebDescription("Set root menu id.")]
    /// <summary>
    /// Set root menu id to display chilren menu items of
    /// </summary>
    public int p1_root_menu_id
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
    [WebDisplayName("2.Menu type id:")]
    [WebDescription("Set menu type id")]
    /// <summary>
    /// Set menu type id to display all menu items of
    /// </summary>
    public int p2_menu_type_id
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

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.show menu icon:")]
    [WebDescription("Set show menu icon option")]
    public bool p3_show_menu_icon
    {
        get
        {
            return _show_menu_icon;
        }
        set
        {
            _show_menu_icon = value;
        }
    }

    public Webparts_MENUFLYOUT()
    {
        this.Title = "MENU LEFT STANDARD STYLE";
    }
    protected override void OnPreRender(EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude(
                GetType(),
                "menuflyout",
                ResolveClientUrl("~/js/menuflyout.js"));
        base.OnPreRender(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sMenuHTML = "";
            int iactive_menu_id = 0;
            int iroot_menu_id = 0;
            int imenu_type_id = 0;


            iactive_menu_id = get_active_menu_id();

            if (_root_menu_id == 0 && _menu_type_id == 0)
            {
                //auto detect root for menu control: detect context value for _root_menu_id or _menu_type_id
                decide_menu_root(iactive_menu_id, out iroot_menu_id, out imenu_type_id);
            }
            else
            {
                iroot_menu_id = _root_menu_id;
                imenu_type_id = _menu_type_id;            
            }
            
            if (iroot_menu_id > 0)
            {
                DataTable tblMenus = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iroot_menu_id).Tables[0];
                if (tblMenus.Rows.Count > 0)
                {
                    this.ltMenuTitle.Text = tblMenus.Rows[0]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString().ToUpper();
                }
                else
                {
                    this.ltMenuTitle.Text = Resources.strings.DataIsNotAvailable;
                    return;
                }
            }
            else if(imenu_type_id>0)
            {
                DataTable tblMenuType = LegoWebSite.Buslgic.MenuTypes.get_MENU_TYPES_BY_ID(imenu_type_id).Tables[0];
                if (tblMenuType.Rows.Count > 0)
                {
                    this.ltMenuTitle.Text = tblMenuType.Rows[0]["MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString().ToUpper();
                }
                else
                {
                    this.ltMenuTitle.Text =Resources.strings.DataIsNotAvailable;
                    return;
                }            
            }

            int iparent_active_menu_id = 0;
            if (iactive_menu_id > 0)
            {
                DataTable mnuTab = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iactive_menu_id).Tables[0];
                if(mnuTab.Rows.Count>0)
                {
                   iparent_active_menu_id= int.Parse(mnuTab.Rows[0]["PARENT_MENU_ID"].ToString());
                }
            }
            //set tree menu


            DataTable tbRootMenuItems ;
                
            if(iroot_menu_id>0)
            {
                tbRootMenuItems = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(iroot_menu_id).Tables[0];
            }
            else
            {
                tbRootMenuItems = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(0,imenu_type_id).Tables[0];
            }

            if (tbRootMenuItems.Rows.Count > 0)
            {

                sMenuHTML += "<ul id='menuList'>";

                for (int i = 0; i < tbRootMenuItems.Rows.Count; i++)
                {
                    int mnuLevel0Id = int.Parse(tbRootMenuItems.Rows[i]["MENU_ID"].ToString());
                    string sMenuHref = tbRootMenuItems.Rows[i]["MENU_LINK_URL"].ToString().ToLower();
                    if ((mnuLevel0Id == iactive_menu_id) || (mnuLevel0Id == iparent_active_menu_id) && i <= (tbRootMenuItems.Rows.Count - 1))
                    {
                        sMenuHTML += "<li  class='active'>";
                    }
                    else
                    {
                        sMenuHTML += "<li class='fly'>";
                    }

                    if (int.Parse(tbRootMenuItems.Rows[i]["BROWSER_NAVIGATE"].ToString()) > 0)
                    {
                        //open in new windows
                        sMenuHTML += "<a href=\"javascript:void(0)\" onclick=\"window.open('" + (sMenuHref.IndexOf("http") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "')\">";

                    }
                    else
                    {
                        sMenuHTML += "<a href='" + (sMenuHref.IndexOf("http") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "'>";
                    }

                    sMenuHTML += (_show_menu_icon ? "<img alt='' src='" + tbRootMenuItems.Rows[i]["MENU_IMAGE_URL"] + "' />&nbsp;" : "") + "<span class='text'>" + tbRootMenuItems.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</span>";
                    sMenuHTML += "</a>";

                    DataTable childR = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(int.Parse(tbRootMenuItems.Rows[i]["MENU_ID"].ToString()),imenu_type_id).Tables[0];

                    if (childR.Rows.Count > 0) //check data
                    {

                        sMenuHTML += "<ul>";
                        for (int j = 0; j < childR.Rows.Count; j++)
                        {
                            sMenuHref=childR.Rows[j]["MENU_LINK_URL"].ToString().ToLower();
                            sMenuHTML += "<li>";                            
                            if (int.Parse(childR.Rows[j]["BROWSER_NAVIGATE"].ToString()) > 0)
                            {
                                //open in new windows
                                sMenuHTML += "<a href=\"javascript:void(0)\" onclick=\"window.open('" + (sMenuHref.IndexOf("http") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "')\">";

                            }
                            else
                            {
                                sMenuHTML += "<a href='" + (sMenuHref.IndexOf("http") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "'>";
                            }

                            sMenuHTML += (_show_menu_icon ? "<img alt='' src='" + tbRootMenuItems.Rows[i]["MENU_IMAGE_URL"] + "' />&nbsp;" : "") + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</a>";
                            sMenuHTML += "</li>";
                        }
                        sMenuHTML += " </ul>";
                    }
                    sMenuHTML += "</li>";
                }
                sMenuHTML += "</ul>";
            }
            this.ltMenuFlyOutItems.Text = sMenuHTML;
        }
    }

    protected int get_active_menu_id()
    {
        int iactive_menu_id = 0;
        if (CommonUtility.GetInitialValue("contentid", null) != null) //try to find active Menu Id from contentid
        {
            int iContentId = int.Parse(CommonUtility.GetInitialValue("contentid", 0).ToString());
            if (iContentId > 0)
            {
                int iCateroryId = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(iContentId);
                if (iCateroryId > 0)
                {
                    iactive_menu_id = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCateroryId).Tables[0].Rows[0]["MENU_ID"].ToString());
                }
            }
        }
        else if (CommonUtility.GetInitialValue("mnuid", null) != null)
        {
            iactive_menu_id = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());

        }
        else //try to find Active Menu Id from catid parameter
        {
            if (CommonUtility.GetInitialValue("catid", null) != null)
            {
                int iCateroryId = int.Parse(CommonUtility.GetInitialValue("catid", 0).ToString());
                iactive_menu_id = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCateroryId).Tables[0].Rows[0]["MENU_ID"].ToString());
            }
        }
        return iactive_menu_id;
    }
    /// <summary>
    /// try to discover root level of menu control
    /// if current active menu is child menu so parent menu is root
    /// if current active menu is root level so menu type is root of menu control
    /// </summary>
    /// <param name="iactive_menu_id"></param>
    /// <param name="iroot_menu_id"></param>
    /// <param name="imenu_type_id"></param>
    protected void decide_menu_root(int iactive_menu_id, out int iroot_menu_id, out int imenu_type_id)
    {
        iroot_menu_id=0;
        imenu_type_id=0;
        if (iactive_menu_id > 0)
        {
            DataTable mnuTable = LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iactive_menu_id).Tables[0];
            iroot_menu_id = LegoWebSite.Buslgic.Menus.get_PARENT_MENU_ID(iactive_menu_id, (int)mnuTable.Rows[0]["MENU_TYPE_ID"]);
            if (iroot_menu_id <= 0) imenu_type_id = (int)mnuTable.Rows[0]["MENU_TYPE_ID"];
        }
        else
        {
            if (CommonUtility.GetInitialValue("catid", null) != null)
            {
                int iCateroryId = int.Parse(CommonUtility.GetInitialValue("catid", 0).ToString());
                DataTable catTable = LegoWebSite.Buslgic.Categories.get_CATEGORY_CHILREN(iCateroryId).Tables[0];
                for (int i = 0; i < catTable.Rows.Count; i++)
                { 
                    DataTable mnuTable=LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID((int)catTable.Rows[i]["MENU_ID"]).Tables[0];
                    if (mnuTable.Rows.Count > 0)
                    {
                        imenu_type_id = (int)mnuTable.Rows[0]["MENU_TYPE_ID"];
                        break;
                    }
                }
            }
        
        }
    }
}

