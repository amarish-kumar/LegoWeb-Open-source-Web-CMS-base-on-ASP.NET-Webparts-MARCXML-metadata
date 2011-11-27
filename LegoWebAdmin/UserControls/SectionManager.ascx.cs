// ----------------------------------------------------------------------
// <copyright file="SectionManager.ascx.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
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

public partial class LgwUserControls_SectionManager : System.Web.UI.UserControl
{
    protected SectionDataProvider _sectionManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "sectionManager", typeof(SortFields), 1, 100);
                ViewState["sectionManagerPageNumber"] = 1;
                ViewState["sectionManagerPageSize"] = 10;
                sectionManagerBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void sectionManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _sectionManagerData.PageNumber = Convert.ToInt16(ViewState["sectionManagerPageNumber"]);
            _sectionManagerData.RecordsPerPage = (int)ViewState["sectionManagerPageSize"];
            _sectionManagerData.get_Search_Count(out outPageCount);
            ViewState["sectionManagerPageCount"] = outPageCount;
            sectionManagerRepeater.DataSource = _sectionManagerData.get_Search_Current_Page();
            sectionManagerRepeater.DataBind();
            if (sectionManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)sectionManagerRepeater.Controls[sectionManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["sectionManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void sectionManagerPageBind()
    {
            _sectionManagerData.PageNumber = Convert.ToInt16(ViewState["sectionManagerPageNumber"]);
            _sectionManagerData.RecordsPerPage = (int)ViewState["sectionManagerPageSize"];
            _sectionManagerData.PageCount = (int)ViewState["sectionManagerPageCount"];
            sectionManagerRepeater.DataSource = _sectionManagerData.get_Search_Current_Page();
            sectionManagerRepeater.DataBind();
            if (sectionManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)sectionManagerRepeater.Controls[sectionManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["sectionManagerPageSize"].ToString();
                }
            }

    }
    protected void sectionManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["sectionManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["sectionManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            sectionManagerPageBind();
    }
    protected void sectionManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.sectionManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)sectionManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }

    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)sectionManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }
    public void Remove_SelectedSections()
    {
        for (int i = 0; i < this.sectionManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)sectionManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtSectionId = (TextBox)sectionManagerRepeater.Items[i].FindControl("txtSectionId");
                if (txtSectionId != null)
                {
                    LegoWebAdmin.BusLogic.Sections.remove_Section(int.Parse(txtSectionId.Text));
                }
            }
        }
        sectionManagerPageBind();
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)sectionManagerRepeater.Controls[sectionManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["sectionManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            sectionManagerBind();
        }
    }
    public void Edit_SelectedSection()
    {
        for (int i = 0; i < this.sectionManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)sectionManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtSectionId = (TextBox)sectionManagerRepeater.Items[i].FindControl("txtSectionId");
                if (txtSectionId != null)
                {
                    Response.Redirect("SectionAddUpdate.aspx?section_id=" + txtSectionId.Text);
                }
            }
        }

    }
    override protected void OnInit(EventArgs e)
    {
        _sectionManagerData = new SectionDataProvider();
    }
}
