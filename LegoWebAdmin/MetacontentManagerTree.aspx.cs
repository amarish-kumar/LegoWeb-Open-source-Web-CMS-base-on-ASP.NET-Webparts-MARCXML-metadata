using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class MetacontentManagerTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        this.MetacontentManagerTree1.Publish_SelectedContents();
    }
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        this.MetacontentManagerTree1.UnPublish_SelectedContents();
    }

    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.MetacontentManagerTree1.Remove_SelectedContents();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.MetacontentManagerTree1.Edit_SelectedContent();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        Session["METADATA"] = null;
        this.MetacontentManagerTree1.AddNew_Content();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
