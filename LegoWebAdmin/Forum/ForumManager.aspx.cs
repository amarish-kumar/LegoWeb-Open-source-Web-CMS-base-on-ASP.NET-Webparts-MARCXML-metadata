using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class ForumManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("~/ErrorMessage.aspx?ErrorMessage='You are not authorized to manage forums!'");
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
