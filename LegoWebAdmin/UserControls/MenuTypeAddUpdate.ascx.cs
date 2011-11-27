// ----------------------------------------------------------------------
// <copyright file="MenuTypeAddUpdate.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWebAdmin.DataProvider;

public partial class LgwUserControls_MenuTypeAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("menu_type_id") != null)
            {
                DataSet SecData = LegoWebAdmin.BusLogic.MenuTypes.get_MenuType_By_ID(int.Parse(CommonUtility.GetInitialValue("menu_type_id").ToString()));
                if (SecData.Tables[0].Rows.Count > 0)
                {
                    this.txtMenuTypeID.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_ID"].ToString();
                    this.txtMenuTypeViTitle.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_VI_TITLE"].ToString();
                    this.txtMenuTypeEnTitle.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_EN_TITLE"].ToString();
                    this.txtMenuTypeDescription.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_DESCRIPTION"].ToString();     
                }
            }

        }
    }

    public bool Save_MenuTypeRecord()
    {
        if (CommonUtility.GetInitialValue("menu_type_id", null) == null)
        {
            //verify duplicate if add new
            if (LegoWebAdmin.BusLogic.MenuTypes.is_MenuType_Exist(int.Parse(txtMenuTypeID.Text)))
            {
                errorMessage.Text = "ID is existed!";
                txtMenuTypeID.Focus();
                return false;
            }
        }
        LegoWebAdmin.BusLogic.MenuTypes.addUpdate_MenuType(int.Parse(txtMenuTypeID.Text), txtMenuTypeViTitle.Text,txtMenuTypeEnTitle.Text, txtMenuTypeDescription.Text);
        return true;
    }
}
