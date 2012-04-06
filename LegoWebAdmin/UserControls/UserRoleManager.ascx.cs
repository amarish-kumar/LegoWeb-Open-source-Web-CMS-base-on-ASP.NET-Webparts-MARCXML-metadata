// ----------------------------------------------------------------------
// <copyright file="UserRoleManager.ascx.cs" package="LEGOWEB">
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


public partial class LgwUserControls_UserRoleManager : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnOk.Text = Resources.strings.Ok_Text;
            btnCancel.Text = Resources.strings.Cancel_Text;
            btnDelete.Text = Resources.strings.Delete_Text;

            roleManagerBind();
        }
    }
    protected void roleManagerBind()
    {
        string[] sRoles = Roles.GetAllRoles();
        roleManagerRepeater.DataSource = sRoles;
        roleManagerRepeater.DataBind();
    }

    protected void linkAddNew_OnClick(object sender, EventArgs e)
    {
        divAddUpdateUserRole.Visible = true;
        errorMessage.Text = "";
        txtUserRoleCode.Text = "";
        txtUserRoleCode.Enabled = true;
        btnDelete.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divAddUpdateUserRole.Visible = false;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Roles.RoleExists(txtUserRoleCode.Text))
            {
                Roles.CreateRole(txtUserRoleCode.Text);
            }
            divAddUpdateUserRole.Visible = false;
            roleManagerBind();
        }
        catch (Exception ex)
        {
            errorMessage.Text ="Lỗi:" + ex.Message + " " + ex.InnerException;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
        Roles.DeleteRole(txtUserRoleCode.Text);
            divAddUpdateUserRole.Visible = false;
            roleManagerBind();
                }
        catch (Exception ex)
        {
            errorMessage.Text ="Lỗi:" + ex.Message + " " + ex.InnerException;
        }
    }

     protected void repeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
     {
         if (e.CommandName == "edit")
         {
             divAddUpdateUserRole.Visible = true;
             errorMessage.Text = "";
             txtUserRoleCode.Text = e.CommandArgument.ToString();
             txtUserRoleCode.Enabled = false;
             btnDelete.Visible = true;

         }
     }
}
