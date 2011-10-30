// ----------------------------------------------------------------------
// <copyright file="CategoryAddUpdate.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
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
using System.Web.Security;
using LegoWebAdmin.DataProvider;

public partial class LgwUserControls_CategoryAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //load admin level
            ListItem item = new ListItem();
            item.Text = Resources.strings.Any_Text;
            item.Value = "0";
            item.Selected = true;
            dropAdminLevels.Items.Add(item);

            item = new ListItem();
            item.Text = Resources.strings.Specified_Text;
            item.Value = "1";
            dropAdminLevels.Items.Add(item);

            radioApplyChilrenNodes.Text = Resources.strings.Yes_Text;
            radioNotApplyChilrenNodes.Text = Resources.strings.No_Text;


            if (CommonUtility.GetInitialValue("category_id") != null)
            {
                DataSet CatData = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(int.Parse(CommonUtility.GetInitialValue("category_id").ToString()));
                if (CatData.Tables[0].Rows.Count > 0)
                {
                    this.txtCategoryID.Text = CatData.Tables[0].Rows[0]["CATEGORY_ID"].ToString();
                    this.txtCategoryID.Enabled = false;
                    this.txtCategoryViTitle.Text = CatData.Tables[0].Rows[0]["CATEGORY_VI_TITLE"].ToString();
                    this.txtCategoryEnTitle.Text = CatData.Tables[0].Rows[0]["CATEGORY_EN_TITLE"].ToString();
                    this.txtCategoryAlias.Text = CatData.Tables[0].Rows[0]["CATEGORY_ALIAS"].ToString();
                    this.dpTemplateNames.SelectedValue = CatData.Tables[0].Rows[0]["CATEGORY_TEMPLATE_NAME"].ToString();
                    this.radioIsPublic.Checked =(bool)CatData.Tables[0].Rows[0]["IS_PUBLIC"];
                    this.radioIsNotPublic.Checked = !(bool)CatData.Tables[0].Rows[0]["IS_PUBLIC"];

                    int iSectionId = int.Parse(CatData.Tables[0].Rows[0]["SECTION_ID"].ToString());
                    int iParentCategoryId = int.Parse(CatData.Tables[0].Rows[0]["PARENT_CATEGORY_ID"].ToString());
                    load_Sections(iSectionId);
                    load_ParentCategories(iSectionId, iParentCategoryId);
                    this.ImageCategoryImageUrl.ImageUrl = CatData.Tables[0].Rows[0]["CATEGORY_IMAGE_URL"].ToString();
                    this.HiddenCategoryImageUrl.Value = CatData.Tables[0].Rows[0]["CATEGORY_IMAGE_URL"].ToString();
                    this.dropAdminLevels.SelectedValue = CatData.Tables[0].Rows[0]["ADMIN_LEVEL"].ToString();

                    if (Convert.ToInt16(CatData.Tables[0].Rows[0]["ADMIN_LEVEL"].ToString()) >= 1)//special permission
                    {
                        cblRoles.Visible = true;
                        //get roles in formation 
                        string[] accessRoles = LegoWebAdmin.BusLogic.Categories.get_ADMIN_ROLES((int)CatData.Tables[0].Rows[0]["CATEGORY_ID"]);
                        if (accessRoles != null && accessRoles.Length > 0)
                        {
                            for (int i = 0; i < accessRoles.Length; i++)
                            {
                                for (int j = 0; j < cblRoles.Items.Count; j++)
                                {
                                    if (cblRoles.Items[j].Value == accessRoles[i])
                                    {
                                        cblRoles.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        cblRoles.Visible = false;
                    }
                    int iMenuId = int.Parse(CatData.Tables[0].Rows[0]["MENU_ID"].ToString());
                    int iMenuTypeId = 0;
                    if (iMenuId > 0)//get MenuTypeId of MenuId
                    {
                        iMenuTypeId = int.Parse(LegoWebAdmin.BusLogic.Menus.get_MENU_BY_ID(iMenuId).Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
                        load_MenuTypes(iMenuTypeId);
                        load_LinkMenus(iMenuTypeId, iMenuId);
                    }
                    else
                    {
                        load_MenuTypes(0);
                    }
                }
            }
            else
            {
                int iSectionId = int.Parse(CommonUtility.GetInitialValue("section_id", "0").ToString());
                int iParentCategoryId = int.Parse(CommonUtility.GetInitialValue("parent_category_id", "0").ToString());
                load_Sections(iSectionId);
                load_ParentCategories(iSectionId, iParentCategoryId);

                load_MenuTypes(0);
            }
        }
    }
    protected void load_Sections(int iSelectedSectionId)
    {
        DataTable secData = LegoWebAdmin.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() +"_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();
        if (iSelectedSectionId > 0)
        {
            this.dropSections.SelectedValue = iSelectedSectionId.ToString();
            this.dropSections.Enabled = false;            
        }
    }
    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_ParentCategories(int.Parse(this.dropSections.SelectedValue.ToString()),0);
    }
    protected void load_ParentCategories(int iSectionId,int iSelectedParentCategoryId)
    {
        DataTable catData = LegoWebAdmin.BusLogic.Categories.get_Search_Page(0, 0, iSectionId, " - ", 1, 100).Tables[0];
        //để tránh việc chọn chính nó là cha của nó hoặc chọn con nó là cha của nó - chưa triệt để được chỉ xử lý được các trường hợp trực tiếp
        if (this.txtCategoryID.Text != "")
        {
            for (int i = 0; i < catData.Rows.Count - 1; i++)
            {
                if (catData.Rows[i]["CATEGORY_ID"].ToString() == this.txtCategoryID.Text || catData.Rows[i]["PARENT_CATEGORY_ID"].ToString() == this.txtCategoryID.Text)
                {
                    catData.Rows.RemoveAt(i);
                    i--;
                }
            }
        }
        DataRow dr = catData.NewRow();
        dr["CATEGORY_ID"] = "0";
        dr["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"] =String.Format( "<< {0} >>",Resources.strings.RootLevel_Text);
        catData.Rows.Add(dr);
        for (int i = 0; i < catData.Rows.Count; i++)
        {
            catData.Rows[i]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"] = ((int)catData.Rows[i]["CATEGORY_ID"]).ToString() + " " + catData.Rows[i]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
        }
        this.dropParentCategories.DataTextField = "CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropParentCategories.DataValueField = "CATEGORY_ID";
        this.dropParentCategories.DataSource = catData;
        this.dropParentCategories.DataBind();
        if (iSelectedParentCategoryId > 0)
        {
            dropParentCategories.SelectedValue = iSelectedParentCategoryId.ToString();
        }
        else
        {
            dropParentCategories.SelectedValue = "0";
        }
    }

    public void Save_CategoryRecord()
    {
        //check if insert dublicated id
        if ((CommonUtility.GetInitialValue("category_id",null) == null) && LegoWebAdmin.BusLogic.Categories.is_CATEGORY_ID_EXIST(int.Parse(txtCategoryID.Text)))
        {
            throw new Exception("ID already exists!");
        }
        int iAdminLevel=int.Parse(dropAdminLevels.SelectedValue.ToString());
        string sAdminRoles = null;
        if (iAdminLevel>0)
        {
            //save admin roles            
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                if (cblRoles.Items[i].Selected)
                {
                    sAdminRoles += ((sAdminRoles != null ? "," : "") + cblRoles.Items[i].Value);
                }
            }
        }

        if (String.IsNullOrEmpty(txtCategoryAlias.Text))
        {
            txtCategoryAlias.Text = CommonUtility.convert_TitleToAlias(txtCategoryViTitle.Text);
        }
        LegoWebAdmin.BusLogic.Categories.addUpdate_CATEGORY(int.Parse(txtCategoryID.Text),int.Parse("0" + this.dropParentCategories.SelectedValue.ToString()), int.Parse("0" + this.dropSections.SelectedValue.ToString()), txtCategoryViTitle.Text, txtCategoryEnTitle.Text,txtCategoryAlias.Text, dpTemplateNames.SelectedValue.ToString(),HiddenCategoryImageUrl.Value,int.Parse("0" + dropLinkMenus.SelectedValue.ToString()),radioIsPublic.Checked, iAdminLevel,sAdminRoles);
        if (radioApplyChilrenNodes.Checked)//áp dụng cho node con
        {
            LegoWebAdmin.BusLogic.Categories.apply_ADMIN_ROLES_TO_CHILREN(int.Parse(txtCategoryID.Text), iAdminLevel, sAdminRoles);    
        }
        Response.Redirect("CategoryManager.aspx?section_id=" + this.dropSections.SelectedValue.ToString());
    }
    public void Cancel_Click()
    {
        Response.Redirect("CategoryManager.aspx?section_id=" + this.dropSections.SelectedValue.ToString());
    }
    protected void load_MenuTypes(int iSelectedMenuTypeId)
    {
        DataTable mnutypeData = LegoWebAdmin.BusLogic.MenuTypes.get_Search_Page(1, 100).Tables[0];
        DataRow row = mnutypeData.NewRow();
        row["MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"] =String.Format("<< {0} >>",Resources.strings.SelectMenu_Text);
        row["MENU_TYPE_ID"] = 0;
        mnutypeData.Rows.Add(row);

        this.dropMenuTypes.DataTextField = "MENU_TYPE_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropMenuTypes.DataValueField = "MENU_TYPE_ID";
        this.dropMenuTypes.DataSource = mnutypeData;
        this.dropMenuTypes.DataBind();
        this.dropMenuTypes.SelectedValue = "0";
        
        if (iSelectedMenuTypeId > 0)
        {
            this.dropMenuTypes.SelectedValue = iSelectedMenuTypeId.ToString();
        }
    }
    protected void cblRoles_Init(object sender, EventArgs e)
    {
        string[] availableRoles = Roles.GetAllRoles();
        for (int i = 0; i < availableRoles.Length; i++)
        {
            this.cblRoles.Items.Add(new ListItem(availableRoles[i], availableRoles[i]));
        }
    }

    protected void dropAdminLevels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(dropAdminLevels.SelectedValue) >= 1)
        {
            cblRoles.Visible = true;
            radioApplyChilrenNodes.Visible = true;
            radioNotApplyChilrenNodes.Visible = true;
        }
        else
        {
            cblRoles.Visible = false;
            radioApplyChilrenNodes.Visible = false;
            radioNotApplyChilrenNodes.Visible = false;
        }
    }

    protected void dropMenuTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_LinkMenus(int.Parse(this.dropMenuTypes.SelectedValue.ToString()), 0);
    }

    protected void load_LinkMenus(int iMenuTypeId, int iSelectedLinkMenuId)
    {
        DataTable mnuData = LegoWebAdmin.BusLogic.Menus.get_Search_Page(0, 0, iMenuTypeId, " - ", 1, 100).Tables[0];

        DataRow dr = mnuData.NewRow();
        dr["MENU_ID"] = "0";
        dr["MENU_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"] =String.Format("<< {0} >>",Resources.strings.SelectMenuItem_Text);
        mnuData.Rows.Add(dr);
        for (int i = 0; i < mnuData.Rows.Count; i++)
        {
            mnuData.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"] = ((int)mnuData.Rows[i]["MENU_ID"]).ToString() + " " + mnuData.Rows[i]["MENU_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
        }
        this.dropLinkMenus.DataTextField = "MENU_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropLinkMenus.DataValueField = "MENU_ID";
        this.dropLinkMenus.DataSource = mnuData;
        this.dropLinkMenus.DataBind();
        if (iSelectedLinkMenuId > 0)
        {
            dropLinkMenus.SelectedValue = iSelectedLinkMenuId.ToString();
        }
        else
        {
            dropLinkMenus.SelectedValue = "0";
        }
    }

    protected void dpTemplateNames_Init(object sender, EventArgs e)
    {
        this.dpTemplateNames.Items.Clear();
        string TemplatesDir = Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "/";
        DirectoryInfo di = new DirectoryInfo(TemplatesDir);
        FileInfo[] rgFiles = di.GetFiles("*.wfm");
        foreach (FileInfo fi in rgFiles)
        {
            this.dpTemplateNames.Items.Add(new ListItem(fi.Name.Substring(0, fi.Name.LastIndexOf(".")), fi.Name.Substring(0, fi.Name.LastIndexOf("."))));
        }
        this.dpTemplateNames.SelectedValue = "default";
    }
}
