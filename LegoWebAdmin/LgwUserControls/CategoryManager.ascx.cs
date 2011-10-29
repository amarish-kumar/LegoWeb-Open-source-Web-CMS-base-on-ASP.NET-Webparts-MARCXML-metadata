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
using LegoWebAdmin.Controls;

public partial class LgwUserControls_CategoryManager : System.Web.UI.UserControl
{
    protected CategoryDataProvider _categoryManagerData;
    protected int _section_id = 0;
    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "categoryManager", typeof(SortFields), 1, 100);
                ViewState["categoryManagerPageNumber"] = 1;
                ViewState["categoryManagerPageSize"] = int.Parse(Session["PageSize"] != null ? Session["PageSize"].ToString() : "50");   
                categoryManagerBind();                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)categoryManagerRepeater.Controls[categoryManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["categoryManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            Session["PageSize"] = dropDisplay.SelectedValue.ToString();
            categoryManagerBind();
        }
    }

    protected void dropRecordPerPage_OnInit(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)categoryManagerRepeater.Controls[categoryManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            dropDisplay.SelectedValue = Session["PageSize"] != null ? Session["PageSize"].ToString() : "10";
        }
    }

    private void categoryManagerBind()
    {
        try
        {
            _section_id = int.Parse(CommonUtility.GetInitialValue("section_id", "0").ToString());
            int outPageCount = 0;
            _categoryManagerData.PageNumber = Convert.ToInt16(ViewState["categoryManagerPageNumber"]);
            _categoryManagerData.RecordsPerPage = (int)ViewState["categoryManagerPageSize"];
            _categoryManagerData.get_Search_Count(out outPageCount,0,0,_section_id);
            ViewState["categoryManagerPageCount"] = outPageCount;
            categoryManagerRepeater.DataSource = _categoryManagerData.get_Search_Current_Page(0, 0, _section_id, "&nbsp;&nbsp;&nbsp;");
            categoryManagerRepeater.DataBind();

            if (categoryManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)categoryManagerRepeater.Controls[categoryManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["categoryManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void categoryManagerPageBind()
    {
            _section_id = int.Parse(CommonUtility.GetInitialValue("section_id", "0").ToString());
            _categoryManagerData.PageNumber = Convert.ToInt16(ViewState["categoryManagerPageNumber"]);
            _categoryManagerData.RecordsPerPage = (int)ViewState["categoryManagerPageSize"];
            _categoryManagerData.PageCount = (int)ViewState["categoryManagerPageCount"];
            categoryManagerRepeater.DataSource = _categoryManagerData.get_Search_Current_Page(0, 0, _section_id, "&nbsp;&nbsp;&nbsp;");
            categoryManagerRepeater.DataBind();
            if (categoryManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)categoryManagerRepeater.Controls[categoryManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["categoryManagerPageSize"].ToString();
                }
            }
    }
    protected void categoryManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["categoryManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["categoryManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            categoryManagerPageBind();
    }
    protected void categoryManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)categoryManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }

    protected void linkOrderUp_OnClick(object sender, EventArgs e)
    {
        int iCategoryId = 0;
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    iCategoryId = int.Parse(txtCategoryId.Text);
                    LegoWebAdmin.BusLogic.Categories.moveUp_CATEGORY(iCategoryId);
                }
            }
        }
        categoryManagerPageBind();
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null && txtCategoryId.Text == iCategoryId.ToString())
                {
                    CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
                    cbRow.Checked = true;
                }            
        }
    }
    protected void linkOrderDown_OnClick(object sender, EventArgs e)
    {
        int iCategoryId = 0;
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.moveDown_CATEGORY(int.Parse(txtCategoryId.Text));
                }
            }
        }
        categoryManagerPageBind();
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
            if (txtCategoryId != null && txtCategoryId.Text == iCategoryId.ToString())
            {
                CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    public void Remove_SelectedCategories()
    {
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.remove_CATEGORY(int.Parse(txtCategoryId.Text));
                }
            }
        }
        categoryManagerPageBind();
    }
    public void Publish_SelectedCategories()
    {
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.publish_CATEGORY(int.Parse(txtCategoryId.Text),true);
                }
            }
        }
        categoryManagerPageBind();
    }
    public void UnPublish_SelectedCategories()
    {
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.publish_CATEGORY(int.Parse(txtCategoryId.Text), false);
                }
            }
        }
        categoryManagerPageBind();
    }

    public void Edit_SelectedCategory()
    {
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    Response.Redirect("CategoryAddUpdate.aspx?category_id=" + txtCategoryId.Text);
                }
            }
        }
    }
    public void AddNew_Category()
    {
        _section_id = int.Parse(CommonUtility.GetInitialValue("section_id", "0").ToString());
        int parent_category_id = 0;
        for (int i = 0; i < this.categoryManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)categoryManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCategoryId = (TextBox)categoryManagerRepeater.Items[i].FindControl("txtCategoryId");
                if (txtCategoryId != null)
                {
                    parent_category_id =int.Parse( txtCategoryId.Text);
                }
            }
        }
        if (parent_category_id >0 && _section_id==0)
        {
            DataTable catData = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(parent_category_id).Tables[0];
            _section_id =int.Parse( catData.Rows[0]["SECTION_ID"].ToString());
        }
        Response.Redirect("CategoryAddUpdate.aspx?section_id=" + _section_id.ToString() +"&parent_category_id=" + parent_category_id.ToString());
    }
    override protected void OnInit(EventArgs e)
    {
        _categoryManagerData = new CategoryDataProvider();
    }

}
