using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class UpgradeDatabase : System.Web.UI.Page
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

    protected void btnRun_Click(object sender, EventArgs e)
    {
        errorMessage.Text = "";
        try
        {
            LegoWeb.BusLogic.UpgradeDatabase.run_SQLScript(txtSqlScripts.Text);
        }
        catch (Exception ex)
        {
            errorMessage.Text ="Lỗi thực thi:" + ex.Message + " " + ex.InnerException;
        }
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
