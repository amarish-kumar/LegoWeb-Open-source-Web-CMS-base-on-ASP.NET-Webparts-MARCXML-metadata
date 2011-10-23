using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class LegoWebAdmin_MenuManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='Bạn không có quyền truy cập vào tính năng này!'");
            }
        }
    }

    protected void linkPublishButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Publish_SelectedMenus();
    }
    protected void linkUnPublishButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.UnPublish_SelectedMenus();
    }
    
    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Remove_SelectedMenus();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.Edit_SelectedMenu();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        this.MenuManager1.AddNew_Menu();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
