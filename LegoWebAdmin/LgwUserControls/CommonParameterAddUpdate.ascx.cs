// ----------------------------------------------------------------------
// <copyright file="CommonParameterAddUpdate.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LegoWebAdmin.DataProvider;

public partial class LgwUserControls_CommonParameterAddUpdate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ListItem item = new ListItem();
            item.Value="0";
            item.Text=Resources.strings.NotSpecified_Text;
            dropPraramType.Items.Add(item);

            item = new ListItem();
            item.Value="1";
            item.Text=Resources.strings.Registration_Text;
            dropPraramType.Items.Add(item);

            item = new ListItem();
            item.Value="2";
            item.Text=Resources.strings.Proccess_Text;
            dropPraramType.Items.Add(item);

            item = new ListItem();
            item.Value="3";
            item.Selected=true;
            item.Text=Resources.strings.Dictionary_Text;
            dropPraramType.Items.Add(item);

            if (CommonUtility.GetInitialValue("parameter_name") != null)
            {
                DataSet ParamData = LegoWebAdmin.BusLogic.CommonParameters.get_LEGOWEB_COMMON_PARAMETER(CommonUtility.GetInitialValue("parameter_name").ToString());
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

            LegoWebAdmin.BusLogic.CommonParameters.addudp_LEGOWEB_COMMON_PARAMETER(txtCommonParameterName.Text, int.Parse(dropPraramType.SelectedValue), txtCommonParameterViValue.Text, txtCommonParameterEnValue.Text, txtCommonParameterDescription.Text);
        
    }
}
