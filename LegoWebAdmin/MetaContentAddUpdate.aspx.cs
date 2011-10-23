using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class Administrator_MetaContentAddUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int section_id = CommonUtility.GetInitialValue("section_id", null) != null ? int.Parse(CommonUtility.GetInitialValue("section_id", null).ToString()) : 0;
            switch (section_id)
            {
                case 1:
                    this.literalIconTitle.Text = @"<div class='header icon-48-article'>
                                                      Tin bài:[Thêm/Sửa]         
                                                     </div>";
                    break;
                case 2:
                    this.literalIconTitle.Text = @"<div class='header icon-48-generic'>
                                                      Dữ liệu khác:[Thêm/Sửa]
                                                     </div>";
                    break;
                default:
                    this.literalIconTitle.Text = @"<div class='header icon-48-article-add'>
                                                      Dữ liệu web:[Thêm/Sửa]
                                                     </div>";
                    break;
            }        
        }
    }

    protected void linkSaveButton_Click(object sender, EventArgs e)
    {
        this.MetaContentAddUpdate1.save_MetaContentRecord();
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        this.MetaContentAddUpdate1.Cancel_Click();
    }

    protected void linkPreviewButton_Click(object sender, EventArgs e)
    {
        this.MetaContentAddUpdate1.preview_MetaContent();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
