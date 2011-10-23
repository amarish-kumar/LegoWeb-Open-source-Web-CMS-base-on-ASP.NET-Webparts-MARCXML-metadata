using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class Administrator_MetaContentManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                int section_id = CommonUtility.GetInitialValue("section_id", null)!=null?int.Parse(CommonUtility.GetInitialValue("section_id", null).ToString()):1;
                switch (section_id)
                {
                    case 1:
                        this.literalIconTitle.Text = @"<div class='header icon-48-article'>
                                                      Quản lý tin bài         
                                                     </div>";

                        break;
                    case 2:
                        this.literalIconTitle.Text = @"<div class='header icon-48-generic'>
                                                      Quản lý dữ liệu khác        
                                                     </div>";

                        break;
                }
        }
    }

    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        this.MetaContentManager1.Publish_SelectedContents();
    }
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        this.MetaContentManager1.UnPublish_SelectedContents();
    }
    
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.MetaContentManager1.Remove_SelectedContents();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.MetaContentManager1.Edit_SelectedContent();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        Session["METADATA"] = null;
        this.MetaContentManager1.AddNew_Content();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
