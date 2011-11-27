// ----------------------------------------------------------------------
// <copyright file="UserManager.ascx.cs" package="LEGOWEB">
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

public partial class LgwUserControls_UserManager : System.Web.UI.UserControl
{
    protected UserDataProvider _userManagerData;

    public enum SortFields { Default };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSearch.Text = Resources.strings.Search_Text;
            btnReset.Text = Resources.strings.Reset_Text;

            CommonUtility.InitializeGridParameters(ViewState, "userManager", typeof(SortFields), 1, 100);
            ViewState["userManagerPageNumber"] = 1;
            ViewState["userManagerPageSize"] = int.Parse(Session["PageSize"] != null ? Session["PageSize"].ToString() : "100");
            userManagerBind();
        }
    }
    protected void dropdownListRoles_Init(object sender, EventArgs e)
    {
        string[] listRoles = Roles.GetAllRoles();
        for (int i = 0; i < listRoles.Length; i++)
        {
            ListItem item = new ListItem(listRoles[i], listRoles[i]);
            this.dropdownListRoles.Items.Add(item);
        }
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)userManagerRepeater.Controls[userManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["userManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            Session["PageSize"] = dropDisplay.SelectedValue.ToString();
            userManagerBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        userManagerBind();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.RadioUser.Checked = false;
        this.RadioEmail.Checked = false;
        this.RadioRole.Checked = false;
        this.txtUserLogin.Text = "";
        this.txtEmail.Text = "";
        userManagerBind();
    }
    private void userManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _userManagerData.PageNumber = Convert.ToInt16(ViewState["userManagerPageNumber"]);
            _userManagerData.RecordsPerPage = (int)ViewState["userManagerPageSize"];
            if (txtUserLogin.Text!=String.Empty && RadioUser.Checked==true)
            {
                _userManagerData.findUsersByName(out outPageCount, txtUserLogin.Text);
            }
            else if (txtEmail.Text != String.Empty && RadioEmail.Checked == true)
            {
                _userManagerData.findUsersByEmail(out outPageCount, txtEmail.Text);
            }
            else if (dropdownListRoles.SelectedValue!=null && RadioRole.Checked==true)
            {
                _userManagerData.findUsersByRole(out outPageCount, dropdownListRoles.SelectedValue.ToString());
            }
            else
            {
                _userManagerData.findAllUsers(out outPageCount);
            }
            ViewState["userManagerPageCount"] = outPageCount;
            userManagerRepeater.DataSource = _userManagerData.Data;
            userManagerRepeater.DataBind();

            if (userManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)userManagerRepeater.Controls[userManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["userManagerPageSize"].ToString();
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void userManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        int FooterIndex = userManagerRepeater.Controls.Count - 1;
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["userManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["userManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            userManagerBind();
    }
    protected void userManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.userManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)userManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)userManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }
    public void Remove_SelectedUsers()
    {
        for (int i = 0; i < this.userManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)userManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtUserName=(TextBox)userManagerRepeater.Items[i].FindControl("txtUserName");
                if (txtUserName != null)
                {
                    MembershipUser user = Membership.GetUser(txtUserName.Text);
                    Membership.DeleteUser(user.UserName, true);
                }
            }
        }
        userManagerBind();
    }

    public void Edit_SelectedUser()
    {
        for (int i = 0; i < this.userManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)userManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtUserName = (TextBox)userManagerRepeater.Items[i].FindControl("txtUserName");
                if (txtUserName != null)
                {
                    Response.Redirect("UserUpdate.aspx?user="+txtUserName.Text);
                }
            }
        }
    
    }
    override protected void OnInit(EventArgs e)
    {
        _userManagerData = new UserDataProvider();        
    }
    
}
