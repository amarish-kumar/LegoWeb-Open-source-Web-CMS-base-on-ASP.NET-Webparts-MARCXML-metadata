using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class LegoWebAdmin_LinkRelatedContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void linkTakeRelatedContent_Click(object sender, EventArgs e)
    {
        this.LinkRelatedContent1.Take_LinkRelatedContents();
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MetaContentAddUpdate.aspx");
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
