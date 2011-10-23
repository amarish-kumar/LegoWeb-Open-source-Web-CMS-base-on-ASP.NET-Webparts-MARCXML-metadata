using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;
using LegoWeb.DataProvider;

public partial class UserControls_CategoryAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("category_id") != null)
            {
                DataSet CatData = LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(int.Parse(CommonUtility.GetInitialValue("category_id").ToString()));
                if (CatData.Tables[0].Rows.Count > 0)
                {
                    this.txtCategoryID.Text = CatData.Tables[0].Rows[0]["CATEGORY_ID"].ToString();
                    this.txtCategoryID.Enabled = false;
                    this.txtCategoryViTitle.Text = CatData.Tables[0].Rows[0]["CATEGORY_VI_TITLE"].ToString();
                    this.txtCategoryEnTitle.Text = CatData.Tables[0].Rows[0]["CATEGORY_EN_TITLE"].ToString();
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
                        string[] accessRoles = LegoWeb.BusLogic.Categories.get_ADMIN_ROLES((int)CatData.Tables[0].Rows[0]["CATEGORY_ID"]);
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
                        iMenuTypeId = int.Parse(LegoWeb.BusLogic.Menus.get_MENU_BY_ID(iMenuId).Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
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
        DataTable secData = LegoWeb.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_VI_TITLE";
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
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, iSectionId, " - ", 1, 100).Tables[0];
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
        dr["CATEGORY_VI_TITLE"] = "<< Mức gốc >>";
        catData.Rows.Add(dr);
        for (int i = 0; i < catData.Rows.Count; i++)
        {
            catData.Rows[i]["CATEGORY_VI_TITLE"] = ((int)catData.Rows[i]["CATEGORY_ID"]).ToString() + " " + catData.Rows[i]["CATEGORY_VI_TITLE"].ToString();
        }
        this.dropParentCategories.DataTextField = "CATEGORY_VI_TITLE";
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
        if ((CommonUtility.GetInitialValue("category_id",null) == null) && LegoWeb.BusLogic.Categories.is_CATEGORY_ID_EXIST(int.Parse(txtCategoryID.Text)))
        {
            errorMessage.Text = "Mã bị trùng với Chuyên mục đã tồn tại!";
            return;
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

        LegoWeb.BusLogic.Categories.addUpdate_CATEGORY(int.Parse(txtCategoryID.Text),int.Parse("0" + this.dropParentCategories.SelectedValue.ToString()), int.Parse("0" + this.dropSections.SelectedValue.ToString()), txtCategoryViTitle.Text, txtCategoryEnTitle.Text, dpTemplateNames.SelectedValue.ToString(),HiddenCategoryImageUrl.Value,int.Parse("0" + dropLinkMenus.SelectedValue.ToString()),radioIsPublic.Checked, iAdminLevel,sAdminRoles);
        if (radioApplyChilrenNodes.Checked)//áp dụng cho node con
        {
            LegoWeb.BusLogic.Categories.apply_ADMIN_ROLES_TO_CHILREN(int.Parse(txtCategoryID.Text), iAdminLevel, sAdminRoles);    
        }
        Response.Redirect("CategoryManager.aspx?section_id=" + this.dropSections.SelectedValue.ToString());
    }
    public void Cancel_Click()
    {
        Response.Redirect("CategoryManager.aspx?section_id=" + this.dropSections.SelectedValue.ToString());
    }
    protected void load_MenuTypes(int iSelectedMenuTypeId)
    {
        DataTable mnutypeData = LegoWeb.BusLogic.MenuTypes.get_Search_Page(1, 100).Tables[0];
        DataRow row = mnutypeData.NewRow();
        row["MENU_TYPE_VI_TITLE"] = "<< Chọn trình đơn >>";
        row["MENU_TYPE_ID"] = 0;
        mnutypeData.Rows.Add(row);

        this.dropMenuTypes.DataTextField = "MENU_TYPE_VI_TITLE";
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
        DataTable mnuData = LegoWeb.BusLogic.Menus.get_Search_Page(0, 0, iMenuTypeId, " - ", 1, 100).Tables[0];

        DataRow dr = mnuData.NewRow();
        dr["MENU_ID"] = "0";
        dr["MENU_VI_TITLE"] = "<< Chọn mục trình đơn nếu có >>";
        mnuData.Rows.Add(dr);
        for (int i = 0; i < mnuData.Rows.Count; i++)
        {
            mnuData.Rows[i]["MENU_VI_TITLE"] = ((int)mnuData.Rows[i]["MENU_ID"]).ToString() + " " + mnuData.Rows[i]["MENU_VI_TITLE"].ToString();
        }
        this.dropLinkMenus.DataTextField = "MENU_VI_TITLE";
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
