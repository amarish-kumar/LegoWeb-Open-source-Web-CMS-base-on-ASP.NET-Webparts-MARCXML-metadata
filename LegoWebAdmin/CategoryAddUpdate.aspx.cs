// ----------------------------------------------------------------------
// <copyright file="CategoryAddUpdate.aspx.cs" package="LEGOWEB">
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



public partial class KiposWebAdmin_CategoryAddUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            litCategoryAddUpdate.Text = Resources.strings.AddUpdateCategory_Text;
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized to update category!'");
            }
        }

    }

    protected void linkSaveButton_Click(object sender, EventArgs e)
    {
        
        try
        {
            this.CategoryAddUpdate1.Save_CategoryRecord();
        }
        catch (Exception ex)
        {
            String errorFomat = @"<dl id='system-message'>
                                            <dd class='error message fade'>
	                                            <ul>
		                                            <li>{0}</li>
	                                            </ul>
                                            </dd>
                                            </dl>";
            litErrorSpaceHolder.Text = String.Format(errorFomat, ex.Message);
        }
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        this.CategoryAddUpdate1.Cancel_Click();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
