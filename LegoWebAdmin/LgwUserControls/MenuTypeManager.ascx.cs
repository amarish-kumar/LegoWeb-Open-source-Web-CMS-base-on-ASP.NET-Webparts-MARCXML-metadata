using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using LegoWeb.DataProvider;
using LegoWeb.Controls;

public partial class LgwUserControls_MenuTypeManager : System.Web.UI.UserControl
{
    protected MenuTypeDataProvider _menutypeManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "menutypeManager", typeof(SortFields), 1, 100);
                ViewState["menutypeManagerPageNumber"] = 1;
                ViewState["menutypeManagerPageSize"] = 10;
                menutypeManagerBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void menutypeManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _menutypeManagerData.PageNumber = Convert.ToInt16(ViewState["menutypeManagerPageNumber"]);
            _menutypeManagerData.RecordsPerPage = (int)ViewState["menutypeManagerPageSize"];
            _menutypeManagerData.get_Search_Count(out outPageCount);
            ViewState["menutypeManagerPageCount"] = outPageCount;
            menutypeManagerRepeater.DataSource = _menutypeManagerData.get_Search_Current_Page();
            menutypeManagerRepeater.DataBind();
            if (menutypeManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)menutypeManagerRepeater.Controls[menutypeManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["menutypeManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void menutypeManagerPageBind()
    {
            _menutypeManagerData.PageNumber = Convert.ToInt16(ViewState["menutypeManagerPageNumber"]);
            _menutypeManagerData.RecordsPerPage = (int)ViewState["menutypeManagerPageSize"];
            _menutypeManagerData.PageCount = (int)ViewState["menutypeManagerPageCount"];
            menutypeManagerRepeater.DataSource = _menutypeManagerData.get_Search_Current_Page();
            menutypeManagerRepeater.DataBind();
            if (menutypeManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)menutypeManagerRepeater.Controls[menutypeManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["menutypeManagerPageSize"].ToString();
                }
            }

    }
    protected void menutypeManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["menutypeManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["menutypeManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            menutypeManagerPageBind();
    }
    protected void menutypeManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.menutypeManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menutypeManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)menutypeManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }
    public void Remove_SelectedMenuTypes()
    {
        for (int i = 0; i < this.menutypeManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menutypeManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuTypeId = (TextBox)menutypeManagerRepeater.Items[i].FindControl("txtMenuTypeId");
                if (txtMenuTypeId != null)
                {
                    LegoWeb.BusLogic.MenuTypes.remove_MenuType(int.Parse(txtMenuTypeId.Text));
                }
            }
        }
        menutypeManagerPageBind();
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)menutypeManagerRepeater.Controls[menutypeManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["menutypeManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            menutypeManagerBind();
        }
    }
    public void Edit_SelectedMenuType()
    {
        for (int i = 0; i < this.menutypeManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)menutypeManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMenuTypeId = (TextBox)menutypeManagerRepeater.Items[i].FindControl("txtMenuTypeId");
                if (txtMenuTypeId != null)
                {
                    Response.Redirect("MenuTypeAddUpdate.aspx?menutype_id=" + txtMenuTypeId.Text);
                }
            }
        }

    }
    override protected void OnInit(EventArgs e)
    {
        _menutypeManagerData = new MenuTypeDataProvider();
    }
}
