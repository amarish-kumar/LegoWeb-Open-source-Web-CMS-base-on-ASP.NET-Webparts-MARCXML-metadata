using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWeb.DataProvider;

public partial class UserControls_SectionAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (CommonUtility.GetInitialValue("section_id") != null)
            {
                DataSet SecData = LegoWeb.BusLogic.Sections.get_Section_By_ID(int.Parse(CommonUtility.GetInitialValue("section_id").ToString()));
                if (SecData.Tables[0].Rows.Count > 0)
                {
                    this.txtSectionID.Text = SecData.Tables[0].Rows[0]["SECTION_ID"].ToString();
                    this.txtSectionViTitle.Text = SecData.Tables[0].Rows[0]["SECTION_VI_TITLE"].ToString();
                    this.txtSectionEnTitle.Text = SecData.Tables[0].Rows[0]["SECTION_EN_TITLE"].ToString();
                }
            }


        }
    }

    public void Save_SectionRecord()
    {
        LegoWeb.BusLogic.Sections.add_Update(int.Parse(txtSectionID.Text), txtSectionViTitle.Text, txtSectionEnTitle.Text);
    }
}
