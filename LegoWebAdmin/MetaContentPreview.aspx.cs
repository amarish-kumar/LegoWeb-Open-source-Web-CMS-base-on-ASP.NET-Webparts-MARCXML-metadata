// ----------------------------------------------------------------------
// <copyright file="MetaContentPreview.aspx.cs" package="LEGOWEB">
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



public partial class LegoWebAdmin_MetaContentPreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.MetaContentPreview1.Delete_PreviewRecord();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MetaContentEditor.aspx");
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Session["METADATA"] = null;
        Response.Redirect("MetaContentManager.aspx");
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
