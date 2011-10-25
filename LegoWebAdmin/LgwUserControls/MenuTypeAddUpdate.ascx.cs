using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWeb.DataProvider;

public partial class LgwUserControls_MenuTypeAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("menutype_id") != null)
            {
                DataSet SecData = LegoWeb.BusLogic.MenuTypes.get_MenuType_By_ID(int.Parse(CommonUtility.GetInitialValue("menutype_id").ToString()));
                if (SecData.Tables[0].Rows.Count > 0)
                {
                    this.txtMenuTypeID.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_ID"].ToString();
                    this.txtMenuTypeViTitle.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_VI_TITLE"].ToString();
                    this.txtMenuTypeEnTitle.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_EN_TITLE"].ToString();
                    this.txtMenuTypeDescription.Text = SecData.Tables[0].Rows[0]["MENU_TYPE_DESCRIPTION"].ToString();     
                }
            }

        }
    }

    public bool Save_MenuTypeRecord()
    {
        if (CommonUtility.GetInitialValue("menutype_id", null) == null)
        {
            //verify duplicate if add new
            if (LegoWeb.BusLogic.MenuTypes.is_MenuType_Exist(int.Parse(txtMenuTypeID.Text)))
            {
                errorMessage.Text = "Mã trình đơn đã tồn tại!";
                txtMenuTypeID.Focus();
                return false;
            }
        }
        LegoWeb.BusLogic.MenuTypes.addUpdate_MenuType(int.Parse(txtMenuTypeID.Text), txtMenuTypeViTitle.Text,txtMenuTypeEnTitle.Text, txtMenuTypeDescription.Text);
        return true;
    }
}
