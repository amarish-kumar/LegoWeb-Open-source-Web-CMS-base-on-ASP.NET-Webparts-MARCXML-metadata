// ----------------------------------------------------------------------
// <copyright file="UserAddNew.ascx.cs" package="LEGOWEB">
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



public partial class LgwUserControls_UserAddNew : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Text = "";
            this.txtPassword.Text = "";
            this.txtConfirmPassword.Text = "";
            this.txtEMAIL.Text = "";
            this.listUserRoles.Items.Clear();
            this.listAvailableRoles.Items.Clear();
            string[] availableRoles = Roles.GetAllRoles();
            for (int i = 0; i < availableRoles.Length; i++)
            {
                this.listAvailableRoles.Items.Add(availableRoles[i]);
            }
        }
    }
    public void Save_UserRecord()
    {
        MembershipCreateStatus kq;
        Membership.CreateUser(this.txtUserName.Text, txtPassword.Text, this.txtEMAIL.Text, txtPasswordQuestion.Text != "" ? txtPasswordQuestion.Text : "0", txtPasswordAnswer.Text != "" ? txtPasswordAnswer.Text : "0", checkActivate.Checked, out kq);

        if (kq == MembershipCreateStatus.Success)
        {
            foreach (ListItem item in listUserRoles.Items)
            {
                Roles.AddUserToRole(this.txtUserName.Text, item.Value);
            }            
        }
        else if (kq == MembershipCreateStatus.DuplicateUserName)
        {
            throw new Exception("User name already exists!");
        }
        else
        {
            throw new Exception("Unknown error occurred in creating new user account!");
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
}
