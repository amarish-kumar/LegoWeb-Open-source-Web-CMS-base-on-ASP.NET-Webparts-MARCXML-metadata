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

public partial class UserControls_MenuManager : System.Web.UI.UserControl
{
    protected MenuDataProvider _menuManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "menuManager", typeof(SortFields), 1, 100);
                ViewState["menuManagerPageNumber"] = 1;
                ViewState["menuManagerPageSize"] =int.Parse(Session["PageSize"]!=null?Session["PageSize"].ToString():"10");                    
                menuManagerBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)menuManagerRepeater.Controls[menuManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["menuManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            Session["PageSize"] = dropDisplay.SelectedValue.ToString();
            menuManagerBind();
        }
    }
    protected void dropRecordPerPage_OnInit(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)menuManagerRepeater.Controls[menuManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            dropDisplay.SelectedValue = Session["PageSize"] != null ? Session["PageSize"].ToString() : "10";
        }
    }
    private void menuManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _menuManagerData.PageNumber = Convert.ToInt16(ViewState["menuManagerPageNumber"]);
            _menuManagerData.RecordsPerPage = (int)ViewState["menuManagerPageSize"];
            _menuManagerData.get_Search_Count(out outPageCount,0,0,int.Parse(this.dropMenuTypes.SelectedValue.ToString()));
            ViewState["menuManagerPageCount"] = outPageCount;
            menuManagerRepeater.DataSource = _menuManagerData.get_Search_Current_Page(0, 0, int.Parse(this.dropMenuTypes.SelectedValue.ToString()), "&nbsp;&nbsp;&nbsp;");
            menuManagerRepeater.DataBind();

            if (menuManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)menuManagerRepeater.Controls[menuManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["menuManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void menuManagerPageBind()
    {
            _menuManagerData.PageNumber = Convert.ToInt16(ViewState["menuManagerPageNumber"]);
            _menuManagerData.RecordsPerPage = (int)ViewState["menuManagerPageSize"];
            _menuManagerData.PageCount = (int)ViewState["menuManagerPageCount"];
            menuManagerRepeater.DataSource = _menuManagerData.get_Search_Current_Page(0, 0, int.Parse(this.dropMenuTypes.SelectedValue.ToString()), "&nbsp;&nbsp;&nbsp;");
            menuManagerRepeater.DataBind();
            if (menuManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)menuManagerRepeater.Controls[menuManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["menuManagerPageSize"].ToString();
                }
            }
    }
    protected void menuManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["menuManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["menuManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            menuManagerPageBind();
    }
    protected void menuManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)menuManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }

    protected void linkOrderUp_OnClick(object sender, EventArgs e)
    {
        int iMenuId = 0;
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    iMenuId = int.Parse(txtMenuId.Text);
                    LegoWeb.BusLogic.Menus.moveUp_MENU(iMenuId);
                }
            }
        }
        menuManagerPageBind();
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null && txtMenuId.Text == iMenuId.ToString())
                {
                    CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
                    cbRow.Checked = true;
                }            
        }
    }
    protected void linkOrderDown_OnClick(object sender, EventArgs e)
    {
        int iMenuId = 0;
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    LegoWeb.BusLogic.Menus.moveDown_MENU(int.Parse(txtMenuId.Text));
                }
            }
        }
        menuManagerPageBind();
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
            if (txtMenuId != null && txtMenuId.Text == iMenuId.ToString())
            {
                CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    public void Remove_SelectedMenus()
    {
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    LegoWeb.BusLogic.Menus.remove_MENU(int.Parse(txtMenuId.Text));
                }
            }
        }
        menuManagerPageBind();
    }
    public void Publish_SelectedMenus()
    {
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    LegoWeb.BusLogic.Menus.publish_MENU(int.Parse(txtMenuId.Text),true);
                }
            }
        }
        menuManagerPageBind();
    }
    public void UnPublish_SelectedMenus()
    {
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    LegoWeb.BusLogic.Menus.publish_MENU(int.Parse(txtMenuId.Text), false);
                }
            }
        }
        menuManagerPageBind();
    }

    public void Edit_SelectedMenu()
    {
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    Response.Redirect("MenuAddUpdate.aspx?menu_id=" + txtMenuId.Text);
                }
            }
        }
    }
    public void AddNew_Menu()
    {
        string sMenuTypeId = this.dropMenuTypes.SelectedValue.ToString();
        string sParentMenuId = "0";
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    sParentMenuId = txtMenuId.Text;
                }
            }
        }
        if (sParentMenuId != "0" && sMenuTypeId == "0")
        {
            DataTable catData = LegoWeb.BusLogic.Menus.get_MENU_BY_ID(int.Parse(sParentMenuId)).Tables[0];
            sMenuTypeId = catData.Rows[0]["MENU_TYPE_ID"].ToString();
        }
        Response.Redirect("MenuAddUpdate.aspx?menu_type_id=" + sMenuTypeId +"&parent_menu_id=" + sParentMenuId);
    }
    override protected void OnInit(EventArgs e)
    {
        _menuManagerData = new MenuDataProvider();
    }

    protected void dropMenuTypes_Init(object sender, EventArgs e)
    {
        DataTable secData = LegoWeb.BusLogic.MenuTypes.get_Search_Page(1, 100).Tables[0];
        this.dropMenuTypes.DataTextField = "MENU_TYPE_VI_TITLE";
        this.dropMenuTypes.DataValueField = "MENU_TYPE_ID";
        this.dropMenuTypes.DataSource = secData;
        this.dropMenuTypes.DataBind();
        if (CommonUtility.GetInitialValue("menu_type_id", null) != null)
        {
            this.dropMenuTypes.SelectedValue = CommonUtility.GetInitialValue("menu_type_id", null).ToString();
        }
    }
    protected void dropMenuTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["menuManagerPageNumber"] = 1;
        menuManagerBind();
    }
}
