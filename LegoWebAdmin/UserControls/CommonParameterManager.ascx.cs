// ----------------------------------------------------------------------
// <copyright file="CommonParameterManager.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using LegoWebAdmin.DataProvider;
using LegoWebAdmin.Controls;

public partial class LgwUserControls_CommonParameterManager : System.Web.UI.UserControl
{
    protected CommonParameterDataProvider _commonparameterManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                load_rdlParamType();
                btnFilter.Text = Resources.strings.btnFilter_Text;
                CommonUtility.InitializeGridParameters(ViewState, "commonparameterManager", typeof(SortFields), 1, 100);
                ViewState["commonparameterManagerPageNumber"] = 1;
                ViewState["commonparameterManagerPageSize"] = 10;
                commonparameterManagerBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void commonparameterManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _commonparameterManagerData.PageNumber = Convert.ToInt16(ViewState["commonparameterManagerPageNumber"]);
            _commonparameterManagerData.RecordsPerPage = (int)ViewState["commonparameterManagerPageSize"];
            _commonparameterManagerData.get_Search_Count(out outPageCount,int.Parse(rdlParamType.SelectedValue));
            ViewState["commonparameterManagerPageCount"] = outPageCount;
            commonparameterManagerRepeater.DataSource = _commonparameterManagerData.get_Search_Current_Page(int.Parse(rdlParamType.SelectedValue));
            commonparameterManagerRepeater.DataBind();
            if (commonparameterManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)commonparameterManagerRepeater.Controls[commonparameterManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["commonparameterManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void commonparameterManagerPageBind()
    {
            _commonparameterManagerData.PageNumber = Convert.ToInt16(ViewState["commonparameterManagerPageNumber"]);
            _commonparameterManagerData.RecordsPerPage = (int)ViewState["commonparameterManagerPageSize"];
            _commonparameterManagerData.PageCount = (int)ViewState["commonparameterManagerPageCount"];
            commonparameterManagerRepeater.DataSource = _commonparameterManagerData.get_Search_Current_Page(int.Parse(rdlParamType.SelectedValue));
            commonparameterManagerRepeater.DataBind();
            if (commonparameterManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)commonparameterManagerRepeater.Controls[commonparameterManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["commonparameterManagerPageSize"].ToString();
                }
            }

    }
    protected void commonparameterManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["commonparameterManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["commonparameterManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            commonparameterManagerPageBind();
    }
    protected void commonparameterManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.commonparameterManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)commonparameterManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)commonparameterManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }
    public void Remove_SelectedCommonParameters()
    {
        for (int i = 0; i < this.commonparameterManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)commonparameterManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCommonParameterName = (TextBox)commonparameterManagerRepeater.Items[i].FindControl("txtCommonParameterName");
                if (txtCommonParameterName != null)
                {
                    LegoWebAdmin.BusLogic.CommonParameters.remove_PARAMETER(txtCommonParameterName.Text);
                }
            }
        }
        commonparameterManagerPageBind();
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)commonparameterManagerRepeater.Controls[commonparameterManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["commonparameterManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            commonparameterManagerBind();
        }
    }
    public void Edit_SelectedCommonParameter()
    {
        for (int i = 0; i < this.commonparameterManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)commonparameterManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtCommonParameterName = (TextBox)commonparameterManagerRepeater.Items[i].FindControl("txtCommonParameterName");
                if (txtCommonParameterName != null)
                {
                    Response.Redirect("CommonParameterAddUpdate.aspx?parameter_name=" + txtCommonParameterName.Text);
                }
            }
        }

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ViewState["commonparameterManagerPageNumber"] = 1;
        commonparameterManagerBind();
    }

    override protected void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
        _commonparameterManagerData = new CommonParameterDataProvider();
        
    }
    protected void load_rdlParamType()
    {
        rdlParamType.Items.Clear();      
        ListItem item = new ListItem();
        item.Text = String.Format("<span style=\"width:50px\">{0}</span>",Resources.strings.All_Text);
        item.Value = "-1";
        item.Selected = true;
        rdlParamType.Items.Add(item);
        item = new ListItem();
        item.Text = String.Format("<span style=\"width:50px\">{0}</span>", Resources.strings.NotClassify_Text);
        item.Value = "0";
        rdlParamType.Items.Add(item);
        item = new ListItem();
        item.Text = String.Format("<span style=\"width:50px\">{0}</span>", Resources.strings.Registration_Text);
        item.Value = "1";
        rdlParamType.Items.Add(item);
        item = new ListItem();
        item.Text = String.Format("<span style=\"width:50px\">{0}</span>", Resources.strings.Proccess_Text);
        item.Value = "2";        
        rdlParamType.Items.Add(item);
        item = new ListItem();
        item.Text = String.Format("<span style=\"width:50px\">{0}</span>", Resources.strings.Dictionary_Text);
        item.Value = "3";
        rdlParamType.Items.Add(item);
    }
}
