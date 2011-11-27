// ----------------------------------------------------------------------
// <copyright file="MetaContentManager.ascx.cs" package="LEGOWEB">
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

using LegoWebAdmin.DataProvider;
/// <summary>
    /// Webpart hiển thị chuyên mục nội dung theo hình cây
    /// </summary>
public partial class LgwUserControls_MetaContentManager : System.Web.UI.UserControl
{
    protected MetaContentDataProvider _metaContentManagerData;
    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {
                Session["CATEGORY_PATH_VALUE"] = null;
                load_dropSections();
                if (CommonUtility.GetInitialValue("section_id", null) != null)
                {
                    this.dropSections.SelectedValue = CommonUtility.GetInitialValue("section_id", null).ToString();                    
                }
                loadCategoryTree();

                CommonUtility.InitializeGridParameters(ViewState, "metaContentManager", typeof(SortFields), 1, 100);
                ViewState["metaContentManagerPageNumber"] = 1;
                ViewState["metaContentManagerPageSize"] = int.Parse(Session["PageSize"] != null ? Session["PageSize"].ToString() : "10");
                CategoryTree_SelectedNodeChanged(null, null);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["metaContentManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            Session["PageSize"] = dropDisplay.SelectedValue.ToString();
            metaContentManagerBind();
        }
    }
    private void metaContentManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _metaContentManagerData.PageNumber = Convert.ToInt16(ViewState["metaContentManagerPageNumber"]);
            _metaContentManagerData.RecordsPerPage = (int)ViewState["metaContentManagerPageSize"];
            int category_id = 0;
            if (CategoryTree.SelectedNode != null)
            {
                TreeNode node = CategoryTree.SelectedNode;
                category_id = int.Parse(node.Value);
            }

            _metaContentManagerData.get_Admin_Search_Count(out outPageCount, int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), category_id);
            ViewState["metaContentManagerPageCount"] = outPageCount;
            metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), category_id);
            metaContentManagerRepeater.DataBind();

            if (metaContentManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["metaContentManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void metaContentManagerPageBind()
    {
        _metaContentManagerData.PageNumber = Convert.ToInt16(ViewState["metaContentManagerPageNumber"]);
        _metaContentManagerData.RecordsPerPage = (int)ViewState["metaContentManagerPageSize"];
        _metaContentManagerData.PageCount = (int)ViewState["metaContentManagerPageCount"];
        metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), int.Parse(CategoryTree.SelectedValue));
        metaContentManagerRepeater.DataBind();
        if (metaContentManagerRepeater.Controls.Count > 1)
        {
            DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
            if (dropDisplay != null)
            {
                dropDisplay.SelectedValue = ViewState["metaContentManagerPageSize"].ToString();
            }
        }
    }
    protected void metaContentManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["metaContentManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["metaContentManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            metaContentManagerPageBind();
    }
    protected void metaContentManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)metaContentManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }

    protected void linkOrderUp_OnClick(object sender, EventArgs e)
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception("You are not authorized to update this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.moveUp_META_CONTENT(iMetaContentId);
                }
            }
        }
        metaContentManagerPageBind();
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
            if (txtMetaContentId != null && txtMetaContentId.Text == iMetaContentId.ToString())
            {
                CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    protected void linkOrderDown_OnClick(object sender, EventArgs e)
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception("You are not authorized to update this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.moveDown_META_CONTENT(iMetaContentId);
                }
            }
        }
        metaContentManagerPageBind();
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
            if (txtMetaContentId != null && txtMetaContentId.Text == iMetaContentId.ToString())
            {
                CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    public void Remove_SelectedContents()
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        
                        throw new Exception( "You are not authorized to update this content!");
                        
                    }
                    LegoWebAdmin.BusLogic.MetaContents.movetrash_META_CONTENTS(iMetaContentId);

                }
            }
        }
        metaContentManagerPageBind();
    }
    public void Publish_SelectedContents()
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception("You are not authorized to update this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.publish_META_CONTENTS(iMetaContentId, true);
                }
            }
        }
        metaContentManagerPageBind();
    }
    public void UnPublish_SelectedContents()
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception( "You are not authorized to update this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.publish_META_CONTENTS(iMetaContentId, false);
                }
            }
        }
        metaContentManagerPageBind();
    }

    public void Edit_SelectedContent()
    {

        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception( "You are not authorized to update this content!");
                    }
                    Response.Redirect("MetaContentEditor.aspx?meta_content_id=" + iMetaContentId.ToString());
                }
            }
        }
    }
    public void AddNew_Content()
    {
        string sSectionId = this.dropSections.SelectedValue.ToString();
        string sCategoryId = CategoryTree.SelectedValue;
        if (!is_UserCanUpdateContent(int.Parse(sCategoryId)))
        {
            throw new Exception( "You are not authorized to update content in this category!");
        }
        Response.Redirect("MetaContentEditor.aspx?section_id=" + sSectionId + "&category_id=" + sCategoryId);
    }
    override protected void OnInit(EventArgs e)
    {
        _metaContentManagerData = new MetaContentDataProvider();
    }

    protected void load_dropSections()
    {
        DataTable secData = LegoWebAdmin.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();

    }
    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["metaContentManagerPageNumber"] = 1;
        loadCategoryTree();
        metaContentManagerBind();
    }
    protected void CategoryTree_SelectedNodeChanged(object sender, EventArgs e)
    {

        ViewState["metaContentManagerPageNumber"] = 1;
        string sID = this.CategoryTree.SelectedNode.Value;
        if (sID != string.Empty)
        {
            Session["CATEGORY_PATH_VALUE"] = this.CategoryTree.SelectedNode.ValuePath;
            Session["CATEGORY_PATH_TEXT"] = get_Current_Path_Text();
            DataSet CatData = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(int.Parse(sID));
            if (CatData.Tables[0].Rows.Count > 0)
            {
                int iAdminLevel = int.Parse(CatData.Tables[0].Rows[0]["ADMIN_LEVEL"].ToString());
                ltCategoryInfo.Text = String.Format(Resources.strings.CategoryInfo_Text, Session["CATEGORY_PATH_TEXT"].ToString(), iAdminLevel == 0 ? Resources.strings.Any_Text : CatData.Tables[0].Rows[0]["ADMIN_ROLES"].ToString());
            }
            else
            {
                ltCategoryInfo.Text = String.Format(Resources.strings.CategoryInfo_Text, Session["CATEGORY_PATH_TEXT"].ToString(),"");
            }
        }
        metaContentManagerBind();        
    }

    public void loadCategoryTree()
    {
        string sTreeXml = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_TREE_XML(0, int.Parse(dropSections.SelectedValue.ToString()));
        sTreeXml = sTreeXml.Replace("(0)", "");
        CategoryXmlData.Data = "<category><category CATEGORY_ID=\"0\" PARENT_CATEGORY_ID=\"0\" CATEGORY_VI_TITLE=\"Tất cả\" CATEGORY_EN_TITLE=\"All\" />" + sTreeXml + "</category>";
        this.CategoryTree.DataBindings[0].ValueField = "CATEGORY_ID";
        this.CategoryTree.DataBindings[0].TextField = "CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "_TITLE";
        this.CategoryTree.DataBind();

        if (CommonUtility.GetInitialValue("category_id", null) != null)
        {
            int icatid = int.Parse(CommonUtility.GetInitialValue("category_id", null).ToString());
            string sTreeValuePath = icatid.ToString();
            DataTable catTable = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(icatid).Tables[0];
            if (catTable.Rows.Count > 0)
            {
                icatid = int.Parse(catTable.Rows[0]["PARENT_CATEGORY_ID"].ToString());
                while (icatid > 0)
                {
                    sTreeValuePath = icatid.ToString() + "/" + sTreeValuePath;
                    catTable = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(icatid).Tables[0];
                    icatid = int.Parse(catTable.Rows[0]["PARENT_CATEGORY_ID"].ToString());
                }
                Session["CATEGORY_PATH_VALUE"] = sTreeValuePath;
            }
        }

       if (Session["CATEGORY_PATH_VALUE"] != null && Session["CATEGORY_PATH_VALUE"].ToString() != String.Empty)
        {
            string sID = Session["CATEGORY_PATH_VALUE"].ToString();
            string[] cIDs = sID.Split('/');
            TreeNode myNode = null;
            string findValue = "";
            if (cIDs.Length == 1)
            {
                myNode = this.CategoryTree.FindNode(sID);
                if (myNode != null)
                {
                    myNode.Select();
                }
            }
            else if (cIDs.Length > 1)
            {
                for (int i = 0; i <= cIDs.Length - 1; i++)
                {
                    findValue += ((i > 0 ? "/" : "") + cIDs[i]);
                    myNode = this.CategoryTree.FindNode(findValue);
                    if (myNode != null)
                    {
                        if (i < (cIDs.Length - 1))
                        {
                            myNode.Expand();
                        }
                        else
                        {
                            myNode.Select();
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            TreeNode myNode = null;
            myNode = this.CategoryTree.FindNode("0");
            if (myNode != null)
            {
                myNode.Select();
            }
        }
    }
    protected string get_Current_Path_Text()
    {
        string sPathText = "";
        TreeNode node = this.CategoryTree.SelectedNode;
        sPathText = node.Text;
        TreeNode pNode = node.Parent;
        while (pNode != null)
        {
            sPathText = pNode.Text + "/" + sPathText;
            node = pNode;
            pNode = node.Parent;
        }
        return sPathText;

    }

    /// <summary>
    /// Kiem tra xem current user co quyen cap nhat thong tin khong
    /// </summary>
    /// <param name="iCategoryId"></param>
    /// <returns></returns>
    public static bool is_UserCanUpdateContent(int iCATEGORY_ID)
    {
        bool isUserCan = true;
        DataTable catTable = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(iCATEGORY_ID).Tables[0];
        int iAdminLevel = int.Parse(catTable.Rows[0]["ADMIN_LEVEL"].ToString());
        if (iAdminLevel > 0)
        {
            string sAdminRoles = catTable.Rows[0]["ADMIN_ROLES"].ToString();
            if (!String.IsNullOrEmpty(sAdminRoles))
            {
                string[] allowRoles = sAdminRoles.Split(new char[] { ',', ';' });
                isUserCan = false;//reset to false then check
                for (int i = 0; i < allowRoles.Length; i++)
                {
                    if (Roles.IsUserInRole(allowRoles[i]))
                    {
                        isUserCan = true;
                        break;
                    }
                }
            }
        }
        return isUserCan;
    }
}
