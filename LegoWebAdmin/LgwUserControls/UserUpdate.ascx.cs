// ----------------------------------------------------------------------
// <copyright file="UserUpdate.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
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

public partial class LgwUserControls_UserUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sUserName = null;
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("User", null) != null)
            {
                sUserName = CommonUtility.GetInitialValue("User", "").ToString();
            }
            else
            {
                return;
            }
            MembershipUser user = Membership.GetUser(sUserName);
            this.txtUserName.Text = user.UserName;
            this.txtEMAIL.Text = user.Email;
            this.txtPasswordQuestion.Text = user.PasswordQuestion;
            this.txtPasswordQuestion.Enabled = false;
            this.checkActivate.Checked = user.IsApproved;
            if (user.IsLockedOut)
            {
                checkLock.Enabled = true;
                checkLock.Checked = true;
            }
            else
            {
                checkLock.Enabled = false;
                checkLock.Checked = false;
            }
            string[] availableRoles = Roles.GetAllRoles();
            for (int i = 0; i < availableRoles.Length; i++)
            {
                if (Roles.IsUserInRole(sUserName, availableRoles[i]))
                {
                    this.listUserRoles.Items.Add(availableRoles[i]);
                }
                else
                {
                    this.listAvailableRoles.Items.Add(availableRoles[i]);
                }
            }

        }
    }
    protected void linkButtonAssignRole_Click(object sender, EventArgs e)
    {
        if (this.listAvailableRoles.SelectedItem != null)
        {
            this.listUserRoles.Items.Add(this.listAvailableRoles.SelectedItem);
            this.listAvailableRoles.Items.Remove(this.listAvailableRoles.SelectedItem);
        }
    }
    protected void linkButtonRemoveRole_Click(object sender, EventArgs e)
    {
        if (this.listUserRoles.SelectedItem != null)
        {
            this.listAvailableRoles.Items.Add(this.listUserRoles.SelectedItem);
            this.listUserRoles.Items.Remove(this.listUserRoles.SelectedItem);
        }
    }

    public void Save_User()
    {
        MembershipUser user = Membership.GetUser(txtUserName.Text);
        foreach (ListItem item in this.listUserRoles.Items)
        {
            if (!Roles.IsUserInRole(user.UserName, item.Value))
            {
                Roles.AddUserToRole(user.UserName, item.Value);
            }
        }
        foreach (ListItem item in this.listAvailableRoles.Items)
        {
            if (Roles.IsUserInRole(user.UserName, item.Value))
            {
                Roles.RemoveUserFromRole(user.UserName, item.Value);
            }
        }

        if (user.IsLockedOut && checkLock.Checked == false)
        {
            user.UnlockUser();
        }

        user.Email = this.txtEMAIL.Text;
        user.IsApproved =this.checkActivate.Checked;
        Membership.UpdateUser(user);
        
    }
}
