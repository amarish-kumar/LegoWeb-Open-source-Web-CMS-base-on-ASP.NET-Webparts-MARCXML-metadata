using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class KiposWebAdmin_CategoryAddUpdate : System.Web.UI.Page
{
    int section_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='Bạn không có quyền truy cập vào tính năng này!'");
            }

            if (CommonUtility.GetInitialValue("section_id", null) != null)
            {
                section_id = int.Parse(CommonUtility.GetInitialValue("section_id", null).ToString());
            }
            else
            {
                if (CommonUtility.GetInitialValue("category_id") != null)
                {
                    DataSet CatData = LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(int.Parse(CommonUtility.GetInitialValue("category_id").ToString()));
                    section_id = int.Parse(CatData.Tables[0].Rows[0]["SECTION_ID"].ToString());
                }
            }
            switch (section_id)
            {
                case 1:
                    this.labelSectionTitle.Text = "Chuyên mục tin bài";
                    break;
                case 2:
                    this.labelSectionTitle.Text = "Chuyên mục dữ liệu khác";
                    break;
                case 3:
                    this.labelSectionTitle.Text = "Các bộ sưu tập của Thư viện";
                    break;
            }
        }

    }

    protected void linkSaveButton_Click(object sender, EventArgs e)
    {
        this.CategoryAddUpdate1.Save_CategoryRecord();
        
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        this.CategoryAddUpdate1.Cancel_Click();
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
