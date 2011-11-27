// ----------------------------------------------------------------------
// <copyright file="CategoryManager.aspx.cs" package="LEGOWEB">
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



public partial class KiposWebAdmin_CategoryManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized to manage categories!'");
            }

            if (CommonUtility.GetInitialValue("section_id", null)!=null)
            {
                int section_id = int.Parse(CommonUtility.GetInitialValue("section_id", null).ToString());
                DataTable secTable = LegoWebAdmin.BusLogic.Sections.get_Section_By_ID(section_id).Tables[0];
                if (secTable.Rows.Count > 0)
                {
                    litSectionName.Text = String.Format("[{0}] {1}", secTable.Rows[0]["SECTION_ID"].ToString(), secTable.Rows[0]["SECTION_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString());
                }else
                {
                    litSectionName.Text="No section was selected!";
                }
            }
        }
    }

    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        
        try
        {
            this.CategoryManager1.Publish_SelectedCategories();
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
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        try
        {
            this.CategoryManager1.UnPublish_SelectedCategories();

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
            this.CategoryManager1.Remove_SelectedCategories();
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
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.CategoryManager1.Edit_SelectedCategory();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        this.CategoryManager1.AddNew_Category();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }

}
