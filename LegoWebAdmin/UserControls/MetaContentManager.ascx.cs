using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using LegoWeb.DataProvider;
using LegoWeb.Controls;

public partial class UserControls_MetaContentManager : System.Web.UI.UserControl
{
    protected MetaContentDataProvider _metaContentManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (CommonUtility.GetInitialValue("section_id", null) != null)
                {
                    this.dropSections.SelectedValue = CommonUtility.GetInitialValue("section_id", null).ToString();
                    load_dropCategories();
                }
                if (CommonUtility.GetInitialValue("category_id", null) != null)
                {
                    this.dropCategories.SelectedValue = CommonUtility.GetInitialValue("category_id", null).ToString();
                }
                CommonUtility.InitializeGridParameters(ViewState, "metaContentManager", typeof(SortFields), 1, 100);
                ViewState["metaContentManagerPageNumber"] = 1;
                ViewState["metaContentManagerPageSize"] = int.Parse(Session["PageSize"] != null ? Session["PageSize"].ToString() : "10"); 
                metaContentManagerBind();
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
            _metaContentManagerData.get_Admin_Search_Count(out outPageCount, int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), int.Parse(string.IsNullOrEmpty(this.dropCategories.SelectedValue)!=true?this.dropCategories.SelectedValue.ToString():"0"));
            ViewState["metaContentManagerPageCount"] = outPageCount;
            metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), int.Parse(string.IsNullOrEmpty(this.dropCategories.SelectedValue) != true ? this.dropCategories.SelectedValue.ToString() : "0"));
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
            metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue != null ? this.dropSections.SelectedValue.ToString() : "0"), int.Parse(string.IsNullOrEmpty(this.dropCategories.SelectedValue) != true ? this.dropCategories.SelectedValue.ToString() : "0"));
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
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    LegoWeb.BusLogic.MetaContents.moveUp_META_CONTENT(iMetaContentId);
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
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    LegoWeb.BusLogic.MetaContents.moveDown_META_CONTENT(int.Parse(txtMetaContentId.Text));
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
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    LegoWeb.BusLogic.MetaContents.remove_META_CONTENTS(int.Parse(txtMetaContentId.Text));
                }
            }
        }
        metaContentManagerPageBind();
    }
    public void Publish_SelectedContents()
    {
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    LegoWeb.BusLogic.MetaContents.publish_META_CONTENTS(int.Parse(txtMetaContentId.Text),true);
                }
            }
        }
        metaContentManagerPageBind();
    }
    public void UnPublish_SelectedContents()
    {
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    LegoWeb.BusLogic.MetaContents.publish_META_CONTENTS(int.Parse(txtMetaContentId.Text), false);
                }
            }
        }
        metaContentManagerPageBind();
    }

    public void Edit_SelectedContent()
    {
        ltErrorMessage.Text = "";
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
                    int iCategoryId = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
                        return;
                    }
                    Response.Redirect("MetaContentAddUpdate.aspx?meta_content_id=" + iMetaContentId.ToString());
                }
            }
        }
    }
    public void AddNew_Content()
    {
        string sSectionId = this.dropSections.SelectedValue.ToString();
        string sCategoryId = this.dropCategories.SelectedValue.ToString();
        if (!is_UserCanUpdateContent(int.Parse(sCategoryId)))
        {
            ltErrorMessage.Text = "Bạn không có quyền thay đổi nội dung chuyên mục này!";
            return;
        }
        Response.Redirect("MetaContentAddUpdate.aspx?section_id=" + sSectionId + "&category_id=" + sCategoryId);
    }
    override protected void OnInit(EventArgs e)
    {
        _metaContentManagerData = new MetaContentDataProvider();
    }

    protected void dropSections_Init(object sender, EventArgs e)
    {
        DataTable secData = LegoWeb.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];        
        this.dropSections.DataTextField = "SECTION_VI_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();

    }
    
    protected void load_dropCategories()
    {
        this.dropCategories.Items.Clear();
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue!=null?int.Parse(this.dropSections.SelectedValue.ToString()):0, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }
    
    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltErrorMessage.Text = "";
        ViewState["metaContentManagerPageNumber"] = 1;
        load_dropCategories();
        metaContentManagerBind();
    }

    protected void dropCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltErrorMessage.Text = "";
        ViewState["metaContentManagerPageNumber"] = 1;
        metaContentManagerBind();
    }
    protected void dropCategories_Init(object sender, EventArgs e)
    {
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue != null ? int.Parse(this.dropSections.SelectedValue.ToString()) : 0, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }

    /// <summary>
    /// Kiem tra xem current user co quyen cap nhat thong tin khong
    /// </summary>
    /// <param name="iCategoryId"></param>
    /// <returns></returns>
    public static bool is_UserCanUpdateContent(int iCATEGORY_ID)
    {
        bool isUserCan = true;
        DataTable catTable = LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(iCATEGORY_ID).Tables[0];
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
