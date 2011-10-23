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
                    Response.Redirect("ErrorMessage.aspx?ErrorMessage='Bạn không có quyền truy cập vào tính năng này!'");
                }

            if (CommonUtility.GetInitialValue("section_id", null)!=null)
            {
                int section_id = int.Parse(CommonUtility.GetInitialValue("section_id", null).ToString());
                switch (section_id)
                { 
                    case 1:
                        this.literalIconTitle.Text=@"<div class='header icon-48-content'>
                                                      Chuyên mục tin bài         
                                                     </div>";

                         break;
                    case 2:
                         this.literalIconTitle.Text = @"<div class='header icon-48-module'>
                                                      Chuyên mục dữ liêu khác        
                                                     </div>";

                         break;
                    case 3:
                         this.literalIconTitle.Text = @"<div class='header icon-48-archive'>
                                                      Các loại thư mục         
                                                     </div>";

                         break;
                }

            }
        }
    }

    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        this.CategoryManager1.Publish_SelectedCategories();
    }
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        this.CategoryManager1.UnPublish_SelectedCategories();
    }
    
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.CategoryManager1.Remove_SelectedCategories();
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
