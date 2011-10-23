using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class LegoWebAdmin_UserManager : System.Web.UI.Page
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

    protected void linkDeleteButton_Click(object sender, EventArgs e)
    {
        this.UserManager1.Remove_SelectedUsers();
    }
    protected void linkEditButton_Click(object sender, EventArgs e)
    {
        this.UserManager1.Edit_SelectedUser();
    }
    protected void linkNewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAddNew.aspx");
    }
}
