using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWeb.DataProvider;

public partial class UserControls_CommonParameterAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("parameter_name") != null)
            {
                DataSet ParamData = LegoWeb.BusLogic.CommonParameters.get_LEGOWEB_COMMON_PARAMETER(CommonUtility.GetInitialValue("parameter_name").ToString());
                if (ParamData.Tables[0].Rows.Count > 0)
                {
                    this.txtCommonParameterName.Text = ParamData.Tables[0].Rows[0]["PARAMETER_NAME"].ToString();
                    this.dropPraramType.SelectedValue = ParamData.Tables[0].Rows[0]["PARAMETER_TYPE"].ToString();
                    this.txtCommonParameterViValue.Text = ParamData.Tables[0].Rows[0]["PARAMETER_VI_VALUE"].ToString();
                    this.txtCommonParameterEnValue.Text = ParamData.Tables[0].Rows[0]["PARAMETER_EN_VALUE"].ToString();
                    this.txtCommonParameterDescription.Text = ParamData.Tables[0].Rows[0]["PARAMETER_DESCRIPTION"].ToString();     
                }
            }

        }
    }

    public void Save_CommonParameterRecord()
    {
        LegoWeb.BusLogic.CommonParameters.addudp_LEGOWEB_COMMON_PARAMETER(txtCommonParameterName.Text,int.Parse(dropPraramType.SelectedValue), txtCommonParameterViValue.Text, txtCommonParameterEnValue.Text, txtCommonParameterDescription.Text);
    }
}
