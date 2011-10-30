// ----------------------------------------------------------------------
// <copyright file="MetaContentTrash.aspx.cs" package="LEGOWEB">
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



public partial class LegoWebAdmin_MetaContentTrash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void linkRestoreButton_Click(object sender, EventArgs e)
    {
        try
        {
            this.MetaContentTrash1.restore_SelectedContents();
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
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        try
        {
            this.MetaContentTrash1.delete_SelectedContents();
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
    
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
