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
using LegoWebSite.Buslgic;

public partial class Webparts_MENUBAR:WebPartBase
{   
    public int _menu_type_id = 0;
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Content Category
    /// </summary>
    public int p1_menu_type_id
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

    public Webparts_MENUBAR()
    {
        this.Title = "MENUBAR";    
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        string sMenuHTML="";
        int iroot_menu_id = 0;
        int imenu_type_id = 0;
        int iactive_menu_id=0;
        int icategory_id=0;
        int imeta_content_id = 0;
        if (!IsPostBack)
        {
            imenu_type_id = _menu_type_id;

            iactive_menu_id= int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
            string skeywork = CommonUtility.GetInitialValue("SearchValue", "").ToString();

            if (iactive_menu_id == 0)
            { 
                //try to find active menu id from catid parameter
                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                     icategory_id= int.Parse(CommonUtility.GetInitialValue("catid", 0).ToString());
                     if (icategory_id > 0)
                    {
                        iactive_menu_id = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(icategory_id).Tables[0].Rows[0]["MENU_ID"].ToString());
                    }
                }
                else
                {
                    //try to find active Menu Id from contentid
                    if (CommonUtility.GetInitialValue("contentid", null) != null)
                    {
                        imeta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", 0).ToString());
                        if (imeta_content_id > 0)
                        {
                            icategory_id = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_CATEGORY_ID(imeta_content_id);
                            if (icategory_id > 0)
                            {
                                iactive_menu_id = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(icategory_id).Tables[0].Rows[0]["MENU_ID"].ToString());
                            }
                        }
                    }
                }
            }
            //still not found
            if (iactive_menu_id == 0 && Session["mnuid"] != null)
            {
                iactive_menu_id = (int)Session["mnuid"];
            }
            else
            {
                Session["mnuid"] = iactive_menu_id;
            }

            //try to discover root menu of current active menu then set active menu to root
            if (iactive_menu_id > 0)
            {
                DataTable mnuTable=LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iactive_menu_id).Tables[0];
                iroot_menu_id = (int)mnuTable.Rows[0]["PARENT_MENU_ID"];
                imenu_type_id=  (int)mnuTable.Rows[0]["MENU_TYPE_ID"];               
                while(iroot_menu_id>0)
                {
                    iactive_menu_id = iroot_menu_id;
                    iroot_menu_id = LegoWebSite.Buslgic.Menus.get_PARENT_MENU_ID(iactive_menu_id,imenu_type_id);
                }
            }
            //try to get Current Menu Type
            if (_menu_type_id == 0 && imenu_type_id==0)
            {
                if (iactive_menu_id> 0)
                {
                    imenu_type_id = int.Parse(LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iactive_menu_id).Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
                }
                else
                {
                    if (CommonUtility.GetInitialValue("mnutypeid", null) != null)
                    {
                        imenu_type_id = int.Parse(CommonUtility.GetInitialValue("mnutypeid", null).ToString());
                    }
                    else
                    {
                        imenu_type_id = LegoWebSite.Buslgic.MenuTypes.get_TOP_MENU_TYPE_ID();
                    }
                }
            }
            else if (_menu_type_id > 0)
            {
                imenu_type_id = _menu_type_id;
            }
            
            //Buil Menu Bar
            DataTable tbRootCate = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(0, imenu_type_id).Tables[0];
            if (tbRootCate.Rows.Count > 0)
            {
                      sMenuHTML += "<ul id='navigation'>";
                        for (int i = 0; i < tbRootCate.Rows.Count; i++)
                        {                            
                            int mnuLevel0Id = int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString());
                            string sMenuHref = tbRootCate.Rows[i]["MENU_LINK_URL"].ToString().ToLower();

                            if (iactive_menu_id == 0 && i==0 && skeywork=="")
                            {
                                sMenuHTML += "<li class='active'>";
                            }
                            else if (iactive_menu_id == mnuLevel0Id && i < (tbRootCate.Rows.Count-1))
                            {
                                sMenuHTML += "<li class='active'>";

                            }
                            else if (i == (tbRootCate.Rows.Count - 1))
                            {
                                if (iactive_menu_id == mnuLevel0Id)
                                {
                                    sMenuHTML += "<li class='active last'>";
                                }
                                else
                                {
                                    sMenuHTML += "<li class='last'>";
                                }
                            }
                            else
                            {
                                sMenuHTML += "<li>";
                            }
                            
                            if(int.Parse(tbRootCate.Rows[i]["BROWSER_NAVIGATE"].ToString())>0)
                            {
                                //open in new windows
                                sMenuHTML += "<a href=\"javascript:void(0)\" onclick=\"window.open('" + (sMenuHref.IndexOf("http://") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "')\">";

                            }else
                            {
                                sMenuHTML += "<a href='" + (sMenuHref.IndexOf("http://") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "'>";
                            }

                            sMenuHTML +="<span class='menu-left'></span>";
                            sMenuHTML +="<span class='menu-mid'>" + tbRootCate.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</span>";
                            sMenuHTML += "<span class='menu-right'></span>";
                            sMenuHTML +="</a>";


                            DataTable childR = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString()), imenu_type_id).Tables[0];
                            if (childR.Rows.Count > 0) //check data
                            {
                                sMenuHTML +=@"<div class='sub'><ul>";
                                

                                for (int j = 0; j < childR.Rows.Count; j++)
                                {
                                    sMenuHref = childR.Rows[j]["MENU_LINK_URL"].ToString();
                                    if (int.Parse(childR.Rows[j]["BROWSER_NAVIGATE"].ToString()) > 0)
                                    {
                                        sMenuHTML += "<li><a href=\"javascript:void(0)\" onclick=\"window.open('" + (sMenuHref.IndexOf("http://") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + "')\">" + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</a></li>";
                                    }
                                    else
                                    { 
                                        sMenuHTML += @"
                                        <li>
                                            <a  href='" + (sMenuHref.IndexOf("http://") >= 0 ? sMenuHref : ResolveUrl("~/" + sMenuHref)) + @"'>" + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + @"</a>
                                        </li>";
                                    }
                                }
                                sMenuHTML += @"</ul><div class='btm-bg'></div></div>";
                            }
                            sMenuHTML +="</li>";
                        }
                    sMenuHTML += "</ul>";
                }
                 this.mnTitle.InnerHtml = sMenuHTML;
            }
           
           
        }
     
    }
  