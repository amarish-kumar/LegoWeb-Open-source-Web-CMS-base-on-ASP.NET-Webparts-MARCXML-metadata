// ----------------------------------------------------------------------
// <copyright file="MenuManager.ascx.cs" package="LEGOWEB">
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
using LegoWebAdmin.Controls;

public partial class LgwUserControls_MenuManager : System.Web.UI.UserControl
{
    protected MenuDataProvider _menuManagerData;
    protected int _menu_type_id=0;
    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "menuManager", typeof(SortFields), 1, 100);
                ViewState["menuManagerPageNumber"] = 1;
                ViewState["menuManagerPageSize"] =int.Parse(Session["PageSize"]!=null?Session["PageSize"].ToString():"50");                    
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
            dropDisplay.SelectedValue = Session["PageSize"] != null ? Session["PageSize"].ToString() : "50";
        }
    }
    private void menuManagerBind()
    {
        try
        {
            if (CommonUtility.GetInitialValue("menu_type_id", null) != null)
            {
                _menu_type_id = int.Parse(CommonUtility.GetInitialValue("menu_type_id", "0").ToString());
            }

            int outPageCount = 0;
            _menuManagerData.PageNumber = Convert.ToInt16(ViewState["menuManagerPageNumber"]);
            _menuManagerData.RecordsPerPage = (int)ViewState["menuManagerPageSize"];
            _menuManagerData.get_Search_Count(out outPageCount, 0, 0, _menu_type_id);
            ViewState["menuManagerPageCount"] = outPageCount;
            menuManagerRepeater.DataSource = _menuManagerData.get_Search_Current_Page(0, 0, _menu_type_id, "&nbsp;&nbsp;&nbsp;");
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
        if (CommonUtility.GetInitialValue("menu_type_id", null) != null)
        {
            _menu_type_id = int.Parse(CommonUtility.GetInitialValue("menu_type_id", "0").ToString());
        }
            _menuManagerData.PageNumber = Convert.ToInt16(ViewState["menuManagerPageNumber"]);
            _menuManagerData.RecordsPerPage = (int)ViewState["menuManagerPageSize"];
            _menuManagerData.PageCount = (int)ViewState["menuManagerPageCount"];
            menuManagerRepeater.DataSource = _menuManagerData.get_Search_Current_Page(0, 0, _menu_type_id, "&nbsp;&nbsp;&nbsp;");
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
                    LegoWebAdmin.BusLogic.Menus.moveUp_MENU(iMenuId);
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
                    LegoWebAdmin.BusLogic.Menus.moveDown_MENU(int.Parse(txtMenuId.Text));
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
                    LegoWebAdmin.BusLogic.Menus.remove_MENU(int.Parse(txtMenuId.Text));
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
                    LegoWebAdmin.BusLogic.Menus.publish_MENU(int.Parse(txtMenuId.Text),true);
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
                    LegoWebAdmin.BusLogic.Menus.publish_MENU(int.Parse(txtMenuId.Text), false);
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
        if (CommonUtility.GetInitialValue("menu_type_id", null) != null)
        {
            _menu_type_id = int.Parse(CommonUtility.GetInitialValue("menu_type_id", "0").ToString());
        }
        int parent_menu_id = 0;
        for (int i = 0; i < this.menuManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menuManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuId = (TextBox)menuManagerRepeater.Items[i].FindControl("txtMenuId");
                if (txtMenuId != null)
                {
                    parent_menu_id =int.Parse( txtMenuId.Text);
                }
            }
        }
        if (parent_menu_id != 0 && _menu_type_id == 0)
        {
            DataTable catData = LegoWebAdmin.BusLogic.Menus.get_MENU_BY_ID(parent_menu_id).Tables[0];
            _menu_type_id = int.Parse( catData.Rows[0]["MENU_TYPE_ID"].ToString());
        }
        Response.Redirect("MenuAddUpdate.aspx?menu_type_id=" + _menu_type_id.ToString() +"&parent_menu_id=" + parent_menu_id.ToString());
    }
    override protected void OnInit(EventArgs e)
    {
        _menuManagerData = new MenuDataProvider();
    }

    protected void dropMenuTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["menuManagerPageNumber"] = 1;
        menuManagerBind();
    }
}
