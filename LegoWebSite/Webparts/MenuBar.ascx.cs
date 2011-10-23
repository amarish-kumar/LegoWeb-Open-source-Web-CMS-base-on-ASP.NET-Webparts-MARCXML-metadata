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

public partial class Webparts_MenuBar:WebPartBase
{
    public string sTempMenu;
    public int _menu_type_id = 0;
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Content Category
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

    public Webparts_MenuBar()
    {
        this.Title = "MENUBAR";    
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int iActiveMenuId = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
            string keywork = CommonUtility.GetInitialValue("SearchValue", "").ToString();


            if (iActiveMenuId == 0)
            { 
                //try to find Active Menu Id from catid parameter
                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                    int iCateroryId = int.Parse(CommonUtility.GetInitialValue("catid", 0).ToString());
                    if (iCateroryId > 0)
                    {
                        iActiveMenuId = int.Parse(LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCateroryId).Tables[0].Rows[0]["MENU_ID"].ToString());
                    }
                }
                else
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
                }
                //still not found
                if (iActiveMenuId == 0 && Session["mnuid"]!=null)
                {
                    iActiveMenuId = (int)Session["mnuid"];
                }
            }

            Session["mnuid"] = iActiveMenuId;

            //try to get Root Menu Id of this ActiveMenuId
            if (iActiveMenuId > 0)
            {
                int iRootMenuId = LegoWebSite.Buslgic.Menus.get_PARENT_MENU_ID(iActiveMenuId);
                while(iRootMenuId>0)
                {
                    iActiveMenuId = iRootMenuId;
                    iRootMenuId = LegoWebSite.Buslgic.Menus.get_PARENT_MENU_ID(iActiveMenuId);
                }
            }
            //try to get Current Menu Type
            if (_menu_type_id == 0)
            {
                if (iActiveMenuId > 0)
                {
                    _menu_type_id = int.Parse(LegoWebSite.Buslgic.Menus.get_MENUS_BY_MENU_ID(iActiveMenuId).Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
                }
                else
                {
                    if (CommonUtility.GetInitialValue("mnutypeid", null) != null)
                    {
                        _menu_type_id = int.Parse(CommonUtility.GetInitialValue("mnutypeid", null).ToString());
                    }
                    else
                    {
                        _menu_type_id = LegoWebSite.Buslgic.MenuTypes.get_TOP_MENU_TYPE_ID();
                    }
                }
            }            
            
            //Buil Menu Bar
            DataTable tbRootCate = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(0, _menu_type_id).Tables[0];
            if (tbRootCate.Rows.Count > 0)
            {
                      sTempMenu += "<ul id='navigation'>";
                        for (int i = 0; i < tbRootCate.Rows.Count; i++)
                        {
                            int mnuLevel0Id = int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString());

                            if (iActiveMenuId == 0 && i==0 && keywork=="")
                            {
                                sTempMenu += "<li class='active'>";
                            }
                            else if (iActiveMenuId == mnuLevel0Id && i < (tbRootCate.Rows.Count-1))
                            {
                                sTempMenu += "<li class='active'>";

                            }
                            else if (i == (tbRootCate.Rows.Count - 1))
                            {
                                if (iActiveMenuId == mnuLevel0Id)
                                {
                                    sTempMenu += "<li class='active last'>";
                                }
                                else
                                {
                                    sTempMenu += "<li class='last'>";
                                }
                            }
                            else
                            {
                                sTempMenu += "<li>";
                            }
                            
                            if(int.Parse(tbRootCate.Rows[i]["BROWSER_NAVIGATE"].ToString())>0)
                            {
                                //open in new windows
                                sTempMenu += "<a href=\"javascript:void(0)\" onclick=\"window.open('" + tbRootCate.Rows[i]["MENU_LINK_URL"] + "')\">";

                            }else
                            {
                                sTempMenu += "<a href='" + tbRootCate.Rows[i]["MENU_LINK_URL"] + "'>";
                            }

                            sTempMenu +="<span class='menu-left'></span>";
                            sTempMenu +="<span class='menu-mid'>" + tbRootCate.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</span>";
                            sTempMenu += "<span class='menu-right'></span>";
                            sTempMenu +="</a>";


                            DataTable childR = LegoWebSite.Buslgic.Menus.get_MENUS_BY_PARENT_ID(int.Parse(tbRootCate.Rows[i]["MENU_ID"].ToString()), _menu_type_id).Tables[0];
                            if (childR.Rows.Count > 0) //check data
                            {
                                sTempMenu +=@"<div class='sub'><ul>";

                                for (int j = 0; j < childR.Rows.Count; j++)
                                {
                                    if (int.Parse(childR.Rows[j]["BROWSER_NAVIGATE"].ToString()) > 0)
                                    {
                                        sTempMenu += "<li><a href=\"javascript:void(0)\" onclick=\"window.open('" + childR.Rows[j]["MENU_LINK_URL"] + "')\">" + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + "</a></li>";
                                    }
                                    else
                                    { 
                                        sTempMenu += @"
                                        <li>
                                            <a  href='" + childR.Rows[j]["MENU_LINK_URL"] + @"'>" + childR.Rows[j]["MENU_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE"].ToString() + @"</a>
                                        </li>";
                                    }
                                }
                                sTempMenu += @"</ul><div class='btm-bg'></div></div>";
                            }
                            sTempMenu +="</li>";
                        }
                    sTempMenu += "</ul>";
                }
                 this.mnTitle.InnerHtml = sTempMenu;
            }
           
           
        }
     
    }
  