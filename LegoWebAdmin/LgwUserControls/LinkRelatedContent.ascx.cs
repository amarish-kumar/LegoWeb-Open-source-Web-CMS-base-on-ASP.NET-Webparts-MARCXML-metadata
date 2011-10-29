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
using MarcXmlParserEx;

public partial class LgwUserControls_LinkRelatedContent : System.Web.UI.UserControl
{
    protected MetaContentDataProvider _metaContentManagerData;

    public enum SortFields { Default };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CommonUtility.InitializeGridParameters(ViewState, "metaContentManager", typeof(SortFields), 1, 100);
                ViewState["metaContentManagerPageNumber"] = 1;
                ViewState["metaContentManagerPageSize"] = 10;
                metaContentManagerBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void dropRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
        if (dropDisplay != null)
        {
            ViewState["metaContentManagerPageSize"] = int.Parse(dropDisplay.SelectedValue.ToString());
            metaContentManagerBind();
        }
    }
    private void metaContentManagerBind()
    {
        try
        {
            int outPageCount = 0;
            _metaContentManagerData.PageNumber = Convert.ToInt16(ViewState["metaContentManagerPageNumber"]);
            _metaContentManagerData.RecordsPerPage = (int)ViewState["metaContentManagerPageSize"];
            _metaContentManagerData.get_Admin_Search_Count(out outPageCount, int.Parse(this.dropSections.SelectedValue.ToString()), int.Parse(this.dropCategories.SelectedValue.ToString()));
            ViewState["metaContentManagerPageCount"] = outPageCount;
            metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue.ToString()), int.Parse(this.dropCategories.SelectedValue.ToString()));
            metaContentManagerRepeater.DataBind();

            if (metaContentManagerRepeater.Controls.Count > 1)
            {
                DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
                if (dropDisplay != null)
                {
                    dropDisplay.SelectedValue = ViewState["metaContentManagerPageSize"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void metaContentManagerPageBind()
    {
        _metaContentManagerData.PageNumber = Convert.ToInt16(ViewState["metaContentManagerPageNumber"]);
        _metaContentManagerData.RecordsPerPage = (int)ViewState["metaContentManagerPageSize"];
        _metaContentManagerData.PageCount = (int)ViewState["metaContentManagerPageCount"];
        metaContentManagerRepeater.DataSource = _metaContentManagerData.get_Admin_Search_Current_Page(int.Parse(this.dropSections.SelectedValue.ToString()),  int.Parse(this.dropCategories.SelectedValue.ToString()));
        metaContentManagerRepeater.DataBind();
        if (metaContentManagerRepeater.Controls.Count > 1)
        {
            DropDownList dropDisplay = ((DropDownList)metaContentManagerRepeater.Controls[metaContentManagerRepeater.Controls.Count - 1].Controls[0].FindControl("dropRecordPerPage"));
            if (dropDisplay != null)
            {
                dropDisplay.SelectedValue = ViewState["metaContentManagerPageSize"].ToString();
            }
        }
    }
    protected void metaContentManagerDataCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        bool BindAllowed = false;
        if (e.CommandName == "Sort")
        {
            ViewState["metaContentManagerPageNumber"] = 1;
            BindAllowed = true;
        }
        if (e.CommandName == "Navigate")
        {
            ViewState["metaContentManagerPageNumber"] = Int32.Parse(e.CommandArgument.ToString());
            BindAllowed = true;
        }
        if (BindAllowed)
            metaContentManagerPageBind();
    }
    protected void metaContentManagerItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }


    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)metaContentManagerRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }

    protected void linkOrderUp_OnClick(object sender, EventArgs e)
    {
        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    LegoWebAdmin.BusLogic.Categories.moveUp_CATEGORY(iMetaContentId);
                }
            }
        }
        metaContentManagerPageBind();
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
            if (txtMetaContentId != null && txtMetaContentId.Text == iMetaContentId.ToString())
            {
                CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    protected void linkOrderDown_OnClick(object sender, EventArgs e)
    {
        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.moveDown_CATEGORY(int.Parse(txtMetaContentId.Text));
                }
            }
        }
        metaContentManagerPageBind();
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
            if (txtMetaContentId != null && txtMetaContentId.Text == iMetaContentId.ToString())
            {
                CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
                cbRow.Checked = true;
            }
        }
    }
    public void Remove_SelectedMetaContents()
    {
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    LegoWebAdmin.BusLogic.MetaContents.movetrash_META_CONTENTS(int.Parse(txtMetaContentId.Text));
                }
            }
        }
        metaContentManagerPageBind();
    }
    public void Publish_SelectedCategories()
    {
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.publish_CATEGORY(int.Parse(txtMetaContentId.Text), true);
                }
            }
        }
        metaContentManagerPageBind();
    }
    public void UnPublish_SelectedCategories()
    {
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    LegoWebAdmin.BusLogic.Categories.publish_CATEGORY(int.Parse(txtMetaContentId.Text), false);
                }
            }
        }
        metaContentManagerPageBind();
    }

    public void Edit_SelectedMetaContent()
    {
        for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    Response.Redirect("MetaContentEditor.aspx?meta_content_id=" + txtMetaContentId.Text);
                }
            }
        }
    }
    public void AddNew_MetaContent()
    {
        string sSectionId = this.dropSections.SelectedValue.ToString();
        string sCategoryId = this.dropCategories.SelectedValue.ToString();
        Response.Redirect("MetaContentEditor.aspx?section_id=" + sSectionId + "&category_id=" + sCategoryId);
    }
    override protected void OnInit(EventArgs e)
    {
        _metaContentManagerData = new MetaContentDataProvider();
    }

    protected void dropSections_Init(object sender, EventArgs e)
    {
        DataTable secData = LegoWebAdmin.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        DataRow dr = secData.NewRow();
        dr["SECTION_ID"] = 0;
        dr["SECTION_VI_TITLE"] = "--Chọn vùng tin--";
        secData.Rows.Add(dr);
        this.dropSections.DataTextField = "SECTION_VI_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();
        this.dropSections.SelectedValue = "0";
    }

    protected void load_dropCategories(int iSectionId)
    {
        DataTable catData = LegoWebAdmin.BusLogic.Categories.get_Search_Page(0, 0, iSectionId, " - ", 1, 100).Tables[0];
        DataRow dr = catData.NewRow();
        dr["CATEGORY_ID"] = 0;
        dr["CATEGORY_VI_TITLE"] = "--Chọn chuyên mục--";
        catData.Rows.Add(dr);
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
        this.dropCategories.SelectedValue = "0";
    }

    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["metaContentManagerPageNumber"] = 1;
        load_dropCategories(int.Parse(this.dropSections.SelectedValue.ToString()));
        metaContentManagerBind();
    }

    protected void dropCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["metaContentManagerPageNumber"] = 1;
        metaContentManagerBind();
    }

    public void Take_LinkRelatedContents()
    {
        if (Session["METADATA"] == null) Response.Redirect("MetaContentEditor.aspx");
        LegoWebAdmin.DataProvider.ContentEditorDataHelper _MetaContentObject = new ContentEditorDataHelper();
        _MetaContentObject.load_Xml(Session["METADATA"].ToString());
        try
        {
            DataTable marcTable = _MetaContentObject.get_MarcDatafieldTable();
            CDatafield Df = new CDatafield();
            for (int i = 0; i < this.metaContentManagerRepeater.Items.Count; i++)
            {
                CheckBox cbRow = ((CheckBox)metaContentManagerRepeater.Items[i].FindControl("chkSelect"));
                if (cbRow.Checked == true)
                {
                    TextBox txtMetaContentId = (TextBox)metaContentManagerRepeater.Items[i].FindControl("txtMetaContentId");
                    if (txtMetaContentId != null)
                    {
                        int iTagIndex = marcTable.Rows.Count;
                        Int32 iMetaContentId = Int32.Parse(txtMetaContentId.Text);

                        Label labelMetaContentTitle = (Label)metaContentManagerRepeater.Items[i].FindControl("labelMetaContentTitle");
                        if (labelMetaContentTitle != null)
                        {
                            DataRow addRow = marcTable.NewRow();
                            addRow["TAG"] = 780;
                            addRow["TAG_INDEX"] = iTagIndex;
                            addRow["SUBFIELD_CODE"] = "t";
                            addRow["SUBFIELD_LABEL"] = " ";
                            addRow["SUBFIELD_TYPE"] = "TEXT";
                            addRow["SUBFIELD_VALUE"] = labelMetaContentTitle.Text;
                            marcTable.Rows.Add(addRow);

                            addRow = marcTable.NewRow();
                            addRow["TAG"] = 780;
                            addRow["TAG_INDEX"] = iTagIndex;
                            addRow["SUBFIELD_CODE"] = "w";
                            addRow["SUBFIELD_LABEL"] = " ";
                            addRow["SUBFIELD_TYPE"] = "NUMBER";
                            addRow["SUBFIELD_VALUE"] = iMetaContentId;
                            marcTable.Rows.Add(addRow);
                        }
                    }
                }
            }
            _MetaContentObject.bind_TableDataToMarc(ref marcTable);
            Session["METADATA"] = _MetaContentObject.OuterXml;
            Response.Redirect("MetaContentEditor.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void dropCategories_Init(object sender, EventArgs e)
    {
        load_dropCategories(0);
    }
}
