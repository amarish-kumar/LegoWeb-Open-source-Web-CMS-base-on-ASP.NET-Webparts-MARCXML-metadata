// ----------------------------------------------------------------------
// <copyright file="MenuManager.aspx.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class LegoWebAdmin_MenuManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized update menu details!'");
            }
            if (CommonUtility.GetInitialValue("menu_type_id", null) != null)
            {
                DataTable MenuTypeTbl = LegoWebAdmin.BusLogic.MenuTypes.get_MenuType_By_ID(int.Parse(CommonUtility.GetInitialValue("menu_type_id", null).ToString())).Tables[0];
                if(MenuTypeTbl.Rows.Count>0)
                {
                    litMenuTypeName.Text = String.Format("[{0}] {1}", MenuTypeTbl.Rows[0]["MENU_TYPE_ID"].ToString(), MenuTypeTbl.Rows[0]["MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString());
                }else
                {
                    litMenuTypeName.Text="Error: No menu info";
                }
            }
        }
    }

    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Publish_SelectedMenus();
    }
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.UnPublish_SelectedMenus();
    }
    
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Remove_SelectedMenus();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Edit_SelectedMenu();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.AddNew_Menu();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
