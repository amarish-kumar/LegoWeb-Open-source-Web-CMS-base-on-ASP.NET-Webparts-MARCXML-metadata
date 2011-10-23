using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWeb.DataProvider;

public partial class UserControls_MenuAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("menu_id") != null)
            {
                DataSet CatData = LegoWeb.BusLogic.Menus.get_MENU_BY_ID(int.Parse(CommonUtility.GetInitialValue("menu_id").ToString()));
                if (CatData.Tables[0].Rows.Count > 0)
                {
                    this.txtMenuID.Text = CatData.Tables[0].Rows[0]["MENU_ID"].ToString();
                    this.txtMenuID.Enabled = false;
                    this.txtMenuViTitle.Text = CatData.Tables[0].Rows[0]["MENU_VI_TITLE"].ToString();
                    this.txtMenuEnTitle.Text = CatData.Tables[0].Rows[0]["MENU_EN_TITLE"].ToString();
                    this.txtLinkUrl.Text = CatData.Tables[0].Rows[0]["MENU_LINK_URL"].ToString();
                    this.ImageMenuImageUrl.ImageUrl = CatData.Tables[0].Rows[0]["MENU_IMAGE_URL"].ToString();
                    this.HiddenMenuImageUrl.Value = CatData.Tables[0].Rows[0]["MENU_IMAGE_URL"].ToString();
                    this.radioIsPublic.Checked =(bool)CatData.Tables[0].Rows[0]["IS_PUBLIC"];
                    this.radioIsNotPublic.Checked = !(bool)CatData.Tables[0].Rows[0]["IS_PUBLIC"];
                    this.listBoxBrowserNavigation.SelectedValue = CatData.Tables[0].Rows[0]["BROWSER_NAVIGATE"].ToString();
                    int iMenuTypeId = int.Parse(CatData.Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
                    int iParentMenuId = int.Parse(CatData.Tables[0].Rows[0]["PARENT_MENU_ID"].ToString());
                    load_MenuTypes(iMenuTypeId);
                    load_ParentMenus(iMenuTypeId, iParentMenuId);
                }
            }
            else
            {
                int iMenuTypeId = int.Parse(CommonUtility.GetInitialValue("menu_type_id", "0").ToString());
                int iParentMenuId = int.Parse(CommonUtility.GetInitialValue("parent_menu_id", "0").ToString());
                load_MenuTypes(iMenuTypeId);
                load_ParentMenus(iMenuTypeId, iParentMenuId);
            }
        }
    }
    protected void load_MenuTypes(int iSelectedMenuTypeId)
    {
        DataTable secData = LegoWeb.BusLogic.MenuTypes.get_Search_Page(1, 100).Tables[0];
        this.dropMenuTypes.DataTextField = "MENU_TYPE_VI_TITLE";
        this.dropMenuTypes.DataValueField = "MENU_TYPE_ID";
        this.dropMenuTypes.DataSource = secData;
        this.dropMenuTypes.DataBind();
        if (iSelectedMenuTypeId > 0)
        {
            this.dropMenuTypes.SelectedValue = iSelectedMenuTypeId.ToString();
            this.dropMenuTypes.Enabled = false;            
        }
    }
    protected void dropMenuTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_ParentMenus(int.Parse(this.dropMenuTypes.SelectedValue.ToString()),0);
    }
    protected void load_ParentMenus(int iMenuTypeId,int iSelectedParentMenuId)
    {
        DataTable catData = LegoWeb.BusLogic.Menus.get_Search_Page(0, 0, 0, " - ", 1, 100).Tables[0];
        //để tránh việc chọn chính nó là cha của nó hoặc chọn con nó là cha của nó - chưa triệt để được chỉ xử lý được các trường hợp trực tiếp
        if (this.txtMenuID.Text != "")
        {
            for (int i = 0; i < catData.Rows.Count - 1; i++)
            {
                if (catData.Rows[i]["MENU_ID"].ToString() == this.txtMenuID.Text || catData.Rows[i]["PARENT_MENU_ID"].ToString() == this.txtMenuID.Text)
                {
                    catData.Rows.RemoveAt(i);
                    i--;
                }
            }
        }
        DataRow dr = catData.NewRow();
        dr["MENU_ID"] = "0";
        dr["MENU_VI_TITLE"] = "<< Mức gốc >>";
        catData.Rows.Add(dr);

        for(int i=0; i<catData.Rows.Count;i++)                
        {
            catData.Rows[i]["MENU_VI_TITLE"] = ((int)catData.Rows[i]["MENU_ID"]).ToString() + " " + catData.Rows[i]["MENU_VI_TITLE"].ToString();        
        }

        this.dropParentMenus.DataTextField = "MENU_VI_TITLE";
        this.dropParentMenus.DataValueField = "MENU_ID";
        this.dropParentMenus.DataSource = catData;
        this.dropParentMenus.DataBind();
        if (iSelectedParentMenuId > 0)
        {
            dropParentMenus.SelectedValue = iSelectedParentMenuId.ToString();
        }
        else
        {
            dropParentMenus.SelectedValue = "0";
        }
    }
    
    public void Save_MenuRecord()
    {
        if (CommonUtility.GetInitialValue("menu_id", null) == null)
        {
            if (LegoWeb.BusLogic.Menus.is_MenuItem_Exist(int.Parse(txtMenuID.Text)))
            {
                errorMessage.Text = "Mã Mục trình đơn đã tồn tại!.";
                txtMenuID.Focus();
                return;
            }
        }
        LegoWeb.BusLogic.Menus.addUpdate_MENU(int.Parse(txtMenuID.Text),int.Parse("0" + this.dropParentMenus.SelectedValue.ToString()), int.Parse("0" + this.dropMenuTypes.SelectedValue.ToString()), txtMenuViTitle.Text, txtMenuEnTitle.Text,txtLinkUrl.Text,HiddenMenuImageUrl.Value,int.Parse(this.listBoxBrowserNavigation.SelectedValue.ToString()),radioIsPublic.Checked);
        Response.Redirect("MenuManager.aspx?menu_type_id=" + dropMenuTypes.SelectedValue.ToString());
    }

    public void Cancel_Click()
    {
        Response.Redirect("MenuManager.aspx?menu_type_id=" + dropMenuTypes.SelectedValue.ToString());
    }
}
