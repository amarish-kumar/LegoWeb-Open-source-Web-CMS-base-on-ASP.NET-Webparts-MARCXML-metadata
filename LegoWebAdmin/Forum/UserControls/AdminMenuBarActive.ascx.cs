// ----------------------------------------------------------------------
// <copyright file="AdminMenuBarActive.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LgwUserControls_AdminMenuBarActive : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "vi")
        {
            this.btnSelectEnglish.Visible = true;
            this.btnSelectVietnamese.Visible = false;
        }
        else
        {
            this.btnSelectEnglish.Visible = false;
            this.btnSelectVietnamese.Visible = true;
        }

        //load list of menu types
        DataTable mnuData = LegoWebAdmin.BusLogic.MenuTypes.get_Search_Page(1, 100).Tables[0];
        string sMenus = "";
        for (int i = 0; i < mnuData.Rows.Count; i++)
        {
            sMenus += String.Format("<li><a class=\"icon-16-menu\" href=\"../MenuManager.aspx?menu_type_id={0}\">{1}</a></li>", mnuData.Rows[i]["MENU_TYPE_ID"].ToString(), mnuData.Rows[i]["MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString());
        }
        this.menunames.Text = sMenus;

        //load list of sections
        //load list of sections in contents manager
        DataTable secData = LegoWebAdmin.BusLogic.Sections.get_Search_Page(1, 10).Tables[0];
        string sSections = "";
        for (int i = 0; i < secData.Rows.Count; i++)
        {
            sSections += String.Format("<li><a class=\"icon-16-category\" href=\"../CategoryManager.aspx?section_id={0}\">{1}</a></li>", secData.Rows[i]["SECTION_ID"].ToString(), secData.Rows[i]["SECTION_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString());
        }
        this.sectionnames.Text = sSections;
    }

    protected void en_Click(object sender, EventArgs e)
    {
        UrlQuery myURL = new UrlQuery(Request.Url.AbsoluteUri);
        myURL.Set("locale","en-US");
        Response.Redirect(myURL.AbsoluteUri);
    }
    protected void vi_Click(object sender, EventArgs e)
    {
        UrlQuery myURL = new UrlQuery(Request.Url.AbsoluteUri);
        myURL.Set("locale", "vi-VN");
        Response.Redirect(myURL.AbsoluteUri);
    }

}
