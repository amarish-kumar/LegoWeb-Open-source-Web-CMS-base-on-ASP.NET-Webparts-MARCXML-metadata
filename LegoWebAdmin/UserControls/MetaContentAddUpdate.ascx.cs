using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using LegoWeb.DataProvider;
using MarcXmlParserEx;
using FredCK.FCKeditorV2;
using System.Web.Security;

public partial class UserControls_MetaContentAddUpdate : System.Web.UI.UserControl
{
    protected ContentEditorDataHelper _MetaContent = null;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonUtility.GetInitialValue("section_id", null) != null)
            {
                dropSections.SelectedValue = CommonUtility.GetInitialValue("section_id", null).ToString();
                if (dropSections.SelectedValue != null && dropSections.SelectedValue.ToString()!="")
                {
                    load_Categories(int.Parse(dropSections.SelectedValue.ToString()));
                }
                else
                {
                    load_Categories(0);
                }
            }
            if (CommonUtility.GetInitialValue("category_id", null) != null || CommonUtility.GetInitialValue("category_id", "").ToString()!=String.Empty)
            {
                int iCategoryId = int.Parse("0" + CommonUtility.GetInitialValue("category_id", "0").ToString());
                dropCategories.SelectedValue =iCategoryId.ToString() ;
            }
            if (CommonUtility.GetInitialValue("meta_content_id", null) != null)
            {
                //edit existing record
                Session["METADATA"] = null;
                edit_MetaContent(Int32.Parse(CommonUtility.GetInitialValue("meta_content_id", "0").ToString()));
            }
            else if (Session["METADATA"]!=null)
            {
                //continue update 
                update_MetaContent();
            }else
            { 
                //add new record base on template name
                Session["METADATA"] = null;
                create_NewMetaContent();
            }

        }
    }
    protected void update_MetaContent()
    {
        if (Session["METADATA"] == null) throw new Exception("Lỗi: Session['METADATA'] == null in update_MetaContent()");
        _MetaContent=new ContentEditorDataHelper();
        _MetaContent.load_Xml(Session["METADATA"].ToString());
        this.txtMetaContentID.Text = _MetaContent.MetaContentID.ToString();
        if (_MetaContent.MetaContentID > 0)
        {
            int iCategoryId = _MetaContent.CategoryID;
            int iSectionId = (int)LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(iCategoryId).Tables[0].Rows[0]["SECTION_ID"];
            load_Sections(iSectionId);
            load_Categories(iSectionId);
            dropCategories.SelectedValue = iCategoryId.ToString();
        }
        DataTable marcTable = _MetaContent.get_MarcDatafieldTable();
        CRecord labelRec=new CRecord();
        labelRec.load_File(FileTemplateDataProvider.get_LabelTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()))));
        _MetaContent.set_DataTableLabel(ref marcTable,labelRec);
        repeater_DataBind(marcTable);
        this.radioIsPublic.Checked = _MetaContent.IsPublic;
        this.radioIsNotPublic.Checked = !_MetaContent.IsPublic;
        this.labelEntryDate.Text = _MetaContent.EntryDate;
        this.labelCreator.Text = _MetaContent.Creator;
        this.labelModifyDate.Text = _MetaContent.ModifyDate;
        this.labelModifier.Text = _MetaContent.Modifier;
        this.dropLanguages.SelectedValue = _MetaContent.LangCode;
        this.dropAccessLevels.SelectedValue = _MetaContent.AccessLevel.ToString();        
        
        if (_MetaContent.AccessLevel >= 2)//special permission
        {
            cblRoles.Visible = true;
            //get roles in formation 
            string[] accessRoles = LegoWeb.BusLogic.MetaContents.get_ACCESS_ROLES(_MetaContent.MetaContentID);
            if (accessRoles != null && accessRoles.Length > 0)
            {
                for (int i = 0; i < accessRoles.Length; i++)
                {
                    for (int j = 0; j < cblRoles.Items.Count; j++)
                    {
                        if (cblRoles.Items[j].Value == accessRoles[i])
                        {
                            cblRoles.Items[j].Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            cblRoles.Visible = false;
        }

        load_listAddTag();
        this.txtAddValue.Attributes.Add("onClick", "changeInputID('" + this.txtAddValue.ClientID.ToString() + "'); return false;");

        Session["METADATA"] = _MetaContent.OuterXml;       
    }
    protected void edit_MetaContent(Int32 iMetaContentId)
    {
        string sXmlData = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_MARCXML(iMetaContentId,true);
        _MetaContent = new ContentEditorDataHelper();//chắc là lỗi do thiếu dòng này, kinh khủng thật
        _MetaContent.load_Xml(sXmlData);
        this.txtMetaContentID.Text = _MetaContent.MetaContentID.ToString();
        if (_MetaContent.MetaContentID>0)
        {
            Int16 iCategoryId = (Int16)Convert.ToDouble(_MetaContent.CategoryID);
            int iSectionId = (int)LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(iCategoryId).Tables[0].Rows[0]["SECTION_ID"];
            load_Sections(iSectionId);
            load_Categories(iSectionId);
            dropCategories.SelectedValue = iCategoryId.ToString();
        }
        string sTemplateName=LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()));
        string sLabelFile=FileTemplateDataProvider.get_LabelTemplateFile(sTemplateName);

        DataTable marcTable = _MetaContent.get_MarcDatafieldTable();
        CRecord labelRec = new CRecord();
        labelRec.load_File(sLabelFile);
        _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
        repeater_DataBind(marcTable);
        
        this.radioIsPublic.Checked = _MetaContent.IsPublic;
        this.radioIsNotPublic.Checked= !_MetaContent.IsPublic;
        this.labelEntryDate.Text = _MetaContent.EntryDate;
        this.labelCreator.Text = _MetaContent.Creator;
        this.labelModifyDate.Text = _MetaContent.ModifyDate;
        this.labelModifier.Text = _MetaContent.Modifier;
        this.dropLanguages.SelectedValue = _MetaContent.LangCode;
        this.dropAccessLevels.SelectedValue = _MetaContent.AccessLevel.ToString();
        
        if (_MetaContent.AccessLevel >= 2)//special permission
        {
            cblRoles.Visible = true;
            //get roles in formation 
            string[] accessRoles = LegoWeb.BusLogic.MetaContents.get_ACCESS_ROLES(iMetaContentId);
            if (accessRoles != null && accessRoles.Length > 0)
            {
                for (int i = 0; i < accessRoles.Length; i++)
                {
                    for (int j = 0; j < cblRoles.Items.Count; j++)
                    {
                        if (cblRoles.Items[j].Value == accessRoles[i])
                        {
                            cblRoles.Items[j].Selected = true;
                        }
                    }
                }
            }
        }else
        {
            cblRoles.Visible = false;
        }

        load_listAddTag();
        this.txtAddValue.Attributes.Add("onClick", "changeInputID('" + this.txtAddValue.ClientID.ToString() + "'); return false;");

        Session["METADATA"] = _MetaContent.OuterXml;
    }
    //load workform for new record
    protected void create_NewMetaContent()
    {
        _MetaContent = new ContentEditorDataHelper();
        _MetaContent.load_File(FileTemplateDataProvider.get_WorkformTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()))));
        _MetaContent.MetaContentID = 0;

        this.radioIsPublic.Checked = _MetaContent.IsPublic;
        this.radioIsNotPublic.Checked = !_MetaContent.IsPublic;
        this.labelEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        this.labelCreator.Text = this.Page.User.Identity.Name;
        this.labelModifyDate.Text = "";
        this.labelModifier.Text = "";
        this.dropLanguages.SelectedValue = _MetaContent.LangCode;
        this.dropAccessLevels.SelectedValue = _MetaContent.AccessLevel.ToString();
        
        DataTable marcTable = _MetaContent.get_MarcDatafieldTable();
        CRecord labelRec = new CRecord();
        labelRec.load_File(FileTemplateDataProvider.get_LabelTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()))));
        _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
        repeater_DataBind(marcTable);
                
        load_listAddTag();
        this.txtAddValue.Attributes.Add("onClick", "changeInputID('" + this.txtAddValue.ClientID.ToString() + "'); return false;");

        Session["METADATA"] = _MetaContent.OuterXml;
    }
    protected void dropSections_Init(object sender, EventArgs e)
    {
        load_Sections(0);
    }
    protected void load_Sections(int iSelectedSectionId)
    {
        DataTable secData = LegoWeb.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_VI_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();
        if (iSelectedSectionId > 0)
        {
            this.dropSections.SelectedValue = iSelectedSectionId.ToString();
            this.dropSections.Enabled = false;            
        }
    }
    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_Categories(int.Parse(this.dropSections.SelectedValue.ToString()));
    }
    protected void load_Categories(int iSectionId)
    {
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, iSectionId, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }
    protected void dropCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCategories.SelectedValue != null)
        {
            if (String.IsNullOrEmpty(txtMetaContentID.Text) || int.Parse(txtMetaContentID.Text) == 0)//new record so load new workform
            {
                create_NewMetaContent();
            }

            if (!is_UserCanUpdateContent(int.Parse(dropCategories.SelectedValue)))
            {
                this.ltErrorMessage.Text = "Bạn không có quyền cập nhật thông tin vào chuyên mục này!";
                return;
            }
            else
            {
                this.ltErrorMessage.Text = "";
                return;            
            }
        }
    }

    protected void load_listAddTag()
    {
        string sWorkformName = LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()));
        CRecord labelRec = new CRecord();
        string labelFileName = FileTemplateDataProvider.get_LabelTemplateFile(sWorkformName);
        labelRec.load_File(labelFileName);
        listAddTag.Items.Clear();
        for (int i = 0; i < labelRec.Datafields.Count; i++)
        {
            listAddTag.Items.Add(new ListItem(labelRec.Datafields.Datafield(i).Tag, labelRec.Datafields.Datafield(i).Tag));
        }
        listAddTag.SelectedIndex = 0;
        listAddTag_SelectedIndexChanged(null, null);
    }

    protected void repeater_DataBind(DataTable dataSource)
    {
        DataTable textTable = dataSource.Clone();
        DataTable ntextTable = dataSource.Clone();

        DataRow[] textRows = dataSource.Select("SUBFIELD_TYPE <>'NTEXT'");
        if (textRows != null)
        {
            for (int i = 0; i < textRows.Length; i++)
            {
                textTable.ImportRow(textRows[i]);
            }
        }

        DataRow[] ntextRows = dataSource.Select("SUBFIELD_TYPE = 'NTEXT'");
        if (ntextRows != null)
        {
            for (int i = 0; i < ntextRows.Length; i++)
            {
                ntextTable.ImportRow(ntextRows[i]);
            }
        }
        textTable.DefaultView.Sort = "TAG_INDEX, SUBFIELD_CODE ASC";
        ntextTable.DefaultView.Sort = "TAG_INDEX, SUBFIELD_CODE ASC";
        this.marcTextRepeater.DataSource = textTable;
        this.marcTextRepeater.DataBind();
        marcTextRepeater_hideDuplicatedTags();
        this.marcNTextRepeater.DataSource = ntextTable;
        this.marcNTextRepeater.DataBind();
        marcNTextRepeater_hideDuplicatedTags();
    }

    public void save_MetaContentRecord()
    {
        save_MetaContentData();

        if (Session["METADATA"] == null) throw new Exception("Lỗi: Session['METADATA'] == null in save_MetaContentRecord()");
        _MetaContent = new ContentEditorDataHelper();
        _MetaContent.load_Xml(Session["METADATA"].ToString());

        if (!is_UserCanUpdateContent(_MetaContent.CategoryID))
        {
            this.ltErrorMessage.Text = "Bạn không có quyền cập nhật thông tin vào chuyên mục này!";
            return;
        }

        int retID = LegoWeb.BusLogic.MetaContents.save_META_CONTENTS_XML(_MetaContent.OuterXml, this.Page.User.Identity.Name);        
        if (retID > 0 && _MetaContent.AccessLevel>=2)
        { 
            //save access roles
            string sAccessRoles = null;
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {                
                if (cblRoles.Items[i].Selected)
                { 
                    sAccessRoles+=((sAccessRoles!=null?",":"") + cblRoles.Items[i].Value);
                }
            }
            LegoWeb.BusLogic.MetaContents.set_ACCESS_ROLES(retID,sAccessRoles);
        }
        Session["METADATA"] = null;
        Response.Redirect("MetaContentManagerTree.aspx?section_id=" + dropSections.SelectedValue.ToString() + "&category_id=" + dropCategories.SelectedValue.ToString());
    }

    
    protected void save_MetaContentData()
    {
        try
        {
            if (Session["METADATA"] == null) throw new Exception("Lỗi: Session['METADATA'] == null in  save_MetaContentData()");
            _MetaContent = new ContentEditorDataHelper();
            _MetaContent.load_Xml(Session["METADATA"].ToString());

            if (!String.IsNullOrEmpty(this.dropCategories.SelectedValue))
            {
                _MetaContent.CategoryID = Convert.ToInt16(this.dropCategories.SelectedValue.ToString());
            }
            _MetaContent.IsPublic = this.radioIsPublic.Checked;
            _MetaContent.LangCode = this.dropLanguages.SelectedValue.ToString();
            _MetaContent.AccessLevel = int.Parse(this.dropAccessLevels.SelectedValue);

            DataTable marcTable = _MetaContent.create_MarcDataTable();

            for (int i = 0; i < marcTextRepeater.Items.Count; i++)
            {
                DataRow nRow = marcTable.NewRow();
                Label labelTag = (Label)marcTextRepeater.Items[i].FindControl("labelTag");
                if (labelTag != null)
                {
                    nRow["TAG"] = labelTag.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelTag==null");
                }

                Label labelTagIndex = (Label)marcTextRepeater.Items[i].FindControl("labelTagIndex");
                if (labelTagIndex != null)
                {
                    nRow["TAG_INDEX"] = int.Parse(labelTagIndex.Text);
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelTagIndex==null");
                }

                Label labelSubfieldID = (Label)marcTextRepeater.Items[i].FindControl("labelSubfieldID");
                if (labelSubfieldID != null && !String.IsNullOrEmpty(labelSubfieldID.Text))
                {
                    nRow["SUBFIELD_ID"] =Int32.Parse( labelSubfieldID.Text);
                }

                Label labelSubfieldCode = (Label)marcTextRepeater.Items[i].FindControl("labelSubfieldCode");
                if (labelSubfieldCode != null)
                {
                    nRow["SUBFIELD_CODE"] = labelSubfieldCode.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldCode==null");
                }

                Label labelSubfieldLabel = (Label)marcTextRepeater.Items[i].FindControl("labelSubfieldLabel");

                if (labelSubfieldLabel != null)
                {
                    nRow["SUBFIELD_LABEL"] = labelSubfieldLabel.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldLabel==null");
                }

                Label labelSubfieldType = (Label)marcTextRepeater.Items[i].FindControl("labelSubfieldType");
                if (labelSubfieldType != null)
                {
                    nRow["SUBFIELD_TYPE"] = labelSubfieldType.Text;
                    TextBox Subfield_Value = (TextBox)marcTextRepeater.Items[i].FindControl("Subfield_Value");
                    if (Subfield_Value != null)
                    {
                        nRow["SUBFIELD_VALUE"] = Subfield_Value.Text;
                    }
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldType==null");
                }

                marcTable.Rows.Add(nRow);
            }

            /*marcNTextRepeater*/
            #region marcNTextRepeater
            for (int i = 0; i < marcNTextRepeater.Items.Count; i++)
            {
                DataRow nRow = marcTable.NewRow();
                Label labelTag = (Label)marcNTextRepeater.Items[i].FindControl("labelTag");
                if (labelTag != null)
                {
                    nRow["TAG"] = labelTag.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelTag==null");
                }

                Label labelTagIndex = (Label)marcNTextRepeater.Items[i].FindControl("labelTagIndex");
                if (labelTagIndex != null)
                {
                    nRow["TAG_INDEX"] = int.Parse(labelTagIndex.Text);
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelTagIndex==null");
                }

                Label labelSubfieldID = (Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldID");
                if (labelSubfieldID != null && !String.IsNullOrEmpty(labelSubfieldID.Text))
                {
                    nRow["SUBFIELD_ID"] = labelSubfieldID.Text;
                }

                Label labelSubfieldCode = (Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldCode");
                if (labelSubfieldCode != null)
                {
                    nRow["SUBFIELD_CODE"] = labelSubfieldCode.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldCode==null");
                }

                Label labelSubfieldLabel = (Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldLabel");

                if (labelSubfieldLabel != null)
                {
                    nRow["SUBFIELD_LABEL"] = labelSubfieldLabel.Text;
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldLabel==null");
                }

                Label labelSubfieldType = (Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldType");
                if (labelSubfieldType != null)
                {
                    nRow["SUBFIELD_TYPE"] = labelSubfieldType.Text;
                    FCKeditor fckValue = (FCKeditor)marcNTextRepeater.Items[i].FindControl("NTEXT_Value");
                    if (fckValue != null)
                    {
                        nRow["SUBFIELD_VALUE"] = HttpUtility.HtmlDecode(fckValue.Value);
                    }
                }
                else
                {
                    throw new Exception("Lỗi save_MetaContentData labelSubfieldType==null");
                }

                marcTable.Rows.Add(nRow);
            }
            #endregion marcNTextRepeater
            _MetaContent.bind_TableDataToMarc(ref marcTable);
            Session["METADATA"] = _MetaContent.OuterXml;
        }
        catch (Exception ex)
        {
            throw new Exception("Lỗi lưu dữ liệu:" + ex.Message + "\n" + ex.InnerException);
        }
    }

    protected void cmdRemoveSelectedRow_Click(object sender, EventArgs e)
    {
        save_MetaContentData();

        if (Session["METADATA"] == null) throw new Exception("Lỗi: Session['METADATA'] == null in  cmdRemoveSelectedRow_Click()");
        _MetaContent = new ContentEditorDataHelper();
        _MetaContent.load_Xml(Session["METADATA"].ToString());

        DataTable marcTable = _MetaContent.get_MarcDatafieldTable();

        for (int i = 0; i < this.marcTextRepeater.Items.Count; i++)
        {
            CheckBox cb = ((CheckBox)marcTextRepeater.Items[i].FindControl("RowLevelCheckBox"));
            if (cb!=null && cb.Checked)
            {
                //remove by TAG_INDEX and SUBFIELD_CODE
                Label labelSubfieldID = ((Label)marcTextRepeater.Items[i].FindControl("labelSubfieldID"));

                if (labelSubfieldID != null && !String.IsNullOrEmpty(labelSubfieldID.Text))
                {
                    LegoWeb.BusLogic.MetaContents.remove_META_CONTENT_SUBFIELD(int.Parse(labelSubfieldID.Text));
                    DataRow[] remRows = marcTable.Select(" SUBFIELD_ID=" + labelSubfieldID.Text);
                    if (remRows != null) marcTable.Rows.Remove(remRows[0]);
                }
                else
                {
                    Label labelTagIndex = ((Label)marcTextRepeater.Items[i].FindControl("labelTagIndex"));
                    Label labelSubfieldCode = ((Label)marcTextRepeater.Items[i].FindControl("labelSubfieldCode"));

                    if (labelTagIndex != null && labelSubfieldCode != null)
                    {
                        DataRow[] remRows = marcTable.Select("TAG_INDEX=" + labelTagIndex.Text + " AND SUBFIELD_CODE='" + labelSubfieldCode.Text + "'");
                        if (remRows != null) marcTable.Rows.Remove(remRows[0]);                    
                    }

                }
            }
        }

        for (int i = 0; i < this.marcNTextRepeater.Items.Count; i++)
        {
            CheckBox cb = ((CheckBox)marcNTextRepeater.Items[i].FindControl("RowLevelCheckBox"));
            if (cb != null && cb.Checked)
            {
                //remove by TAG_INDEX and SUBFIELD_CODE
                Label labelSubfieldID = ((Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldID"));

                if (labelSubfieldID != null && !String.IsNullOrEmpty(labelSubfieldID.Text))
                {
                    LegoWeb.BusLogic.MetaContents.remove_META_CONTENT_SUBFIELD(int.Parse(labelSubfieldID.Text));
                    DataRow[] remRows = marcTable.Select(" SUBFIELD_ID=" + labelSubfieldID.Text);
                    if (remRows != null) marcTable.Rows.Remove(remRows[0]);
                }
                else
                {
                    Label labelTagIndex = ((Label)marcNTextRepeater.Items[i].FindControl("labelTagIndex"));
                    Label labelSubfieldCode = ((Label)marcNTextRepeater.Items[i].FindControl("labelSubfieldCode"));

                    if (labelTagIndex != null && labelSubfieldCode != null)
                    {
                        DataRow[] remRows = marcTable.Select("TAG_INDEX=" + labelTagIndex.Text + " AND SUBFIELD_CODE='" + labelSubfieldCode.Text + "'");
                        if (remRows != null) marcTable.Rows.Remove(remRows[0]);
                    }

                }
            }
        }
        CRecord labelRec = new CRecord();
        labelRec.load_File(FileTemplateDataProvider.get_LabelTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()))));
        _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
        repeater_DataBind(marcTable);
        _MetaContent.bind_TableDataToMarc(ref marcTable);

        Session["METADATA"] = _MetaContent.OuterXml;
    }

    protected void marcTextRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            //khong co cach nao dynamic add hoac remove control vi no khong giu duoc trang thai sau postback
            //thu nhieu lan khong duoc
            //mot control chi giu duoc trang thai neu no duoc them hoac bo trong Page_Init thoi nhe
            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                Label labelTag = (Label)e.Item.FindControl("labelTag");
                Label labelSubfieldCode = (Label)e.Item.FindControl("labelSubfieldCode");
                
                TextBox Subfield_Value = (TextBox)e.Item.FindControl("Subfield_Value");

                if (Subfield_Value!=null)
                {
                    switch(drv["SUBFIELD_TYPE"].ToString())
                    {
                        case "TEXT":
                            Subfield_Value.Style["width"] = "99%";
                            if (labelTag.Text == "245" && (labelSubfieldCode.Text == "a" || labelSubfieldCode.Text == "b"))
                            {
                                Subfield_Value.TextMode = TextBoxMode.MultiLine;
                            }
                            break;
                        case "NUMBER":
                            Subfield_Value.Style["width"] = "70px";
                            Subfield_Value.Style["text-align"] = "right";
                            break;
                        case "DATE":
                            Subfield_Value.Style["width"] = "70px";
                            Subfield_Value.Style["text-align"] = "right";
                            break;
                        default:
                            Subfield_Value.Style["width"] = "99%";
                            break;
                    
                    }
                    Subfield_Value.Attributes.Add("onClick", "changeInputID('" + Subfield_Value.ClientID.ToString() + "'); return false;");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void marcTextRepeater_hideDuplicatedTags()
    {
        string sOldTagIndex = "0";
        for (int i = 0; i < marcTextRepeater.Items.Count; i++)
        {
            Label labelTagIndex = (Label)marcTextRepeater.Items[i].FindControl("labelTagIndex");
            if (sOldTagIndex == labelTagIndex.Text)
            {
                Label labelTag = (Label)marcTextRepeater.Items[i].FindControl("labelTag");
                if (labelTag != null)
                {
                    labelTag.Visible = false;
                }
            }
            else
            {
                sOldTagIndex = labelTagIndex.Text;
            }
        }
    }
    protected void marcNTextRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            //khong co cach nao dynamic add hoac remove control vi no khong giu duoc trang thai sau postback
            //thu nhieu lan khong duoc
            //mot control chi giu duoc trang thai neu no duoc them hoac bo trong Page_Init thoi nhe
            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;

                FCKeditor NTEXT_Value = (FCKeditor)e.Item.FindControl("NTEXT_Value");
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["FCKEditorEnableCKFinder"].ToString()))
                {
                    CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
                    _FileBrowser.BasePath = "AdminTools/ckfinder/";
                    _FileBrowser.SetupFCKeditor(NTEXT_Value);
                }
                NTEXT_Value.ToolbarCanCollapse = true;
                NTEXT_Value.ToolbarStartExpanded = false;
                NTEXT_Value.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void marcNTextRepeater_hideDuplicatedTags()
    {
        string sOldTagIndex = "0";
        for (int i = 0; i < marcNTextRepeater.Items.Count; i++)
        {
            Label labelTagIndex = (Label)marcNTextRepeater.Items[i].FindControl("labelTagIndex");
            if (sOldTagIndex == labelTagIndex.Text)
            {
                Label labelTag = (Label)marcNTextRepeater.Items[i].FindControl("labelTag");
                if (labelTag != null)
                {
                    labelTag.Visible = false;
                }
            }
            else
            {
                sOldTagIndex = labelTagIndex.Text;
            }
        }
    }
    protected void cmdAddTagOrSubfield_Click(object sender, EventArgs e)
    {
        save_MetaContentData();//save data first

        if (Session["METADATA"] == null) throw new Exception("Lỗi: Session['METADATA'] == null in  cmdAddTagOrSubfield_Click()");
        _MetaContent = new ContentEditorDataHelper();
        _MetaContent.load_Xml(Session["METADATA"].ToString());

        CRecord labelRec = new CRecord();
        labelRec.load_File(FileTemplateDataProvider.get_LabelTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString()))));

        CDatafield Df = labelRec.Datafields.Datafield(listAddTag.SelectedValue.ToString());

        DataTable marcTable = _MetaContent.get_MarcDatafieldTable();
        int iTagIndex = 0;

        if (marcTable.Rows.Count > 0)
        {
            DataRow[] maxRows = marcTable.Select("TAG_INDEX=Max(TAG_INDEX)");
            iTagIndex = int.Parse("0" + maxRows[0]["TAG_INDEX"].ToString());
        }
        if (listAddSubfieldCode.SelectedValue == "")//add new tag
        {                    
            for (int i = 0; i < Df.Subfields.Count; i++)
            {
                DataRow nRow = marcTable.NewRow();
                nRow["TAG"] = Df.Tag;
                nRow["TAG_INDEX"] = iTagIndex + 1;
                nRow["SUBFIELD_CODE"] = Df.Subfields.Subfield(i).Code;
                nRow["SUBFIELD_TYPE"] = Df.Subfields.Subfield(i).Type;
                nRow["SUBFIELD_LABEL"] = Df.Subfields.Subfield(i).Value;
                nRow["SUBFIELD_VALUE"] = txtAddValue.Text;
                marcTable.Rows.Add(nRow);
            }
            marcTable.DefaultView.Sort = "TAG, TAG_INDEX ASC";

            _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
            repeater_DataBind(marcTable);
            _MetaContent.bind_TableDataToMarc(ref marcTable);
            Session["METADATA"] = _MetaContent.OuterXml;
            return;
        }
        else//add subfield to existing tag or new tag only one subfield
        {
            CSubfield Sf = Df.Subfields.Subfield(listAddSubfieldCode.SelectedValue.ToString());

            #region add to subfield to selected tag if have one
            for (int i = 0; i < this.marcTextRepeater.Items.Count; i++)
            {
                CheckBox cb = ((CheckBox)marcTextRepeater.Items[i].FindControl("RowLevelCheckBox"));
                if (cb.Checked)
                {
                    Label labelTag = (Label)marcTextRepeater.Items[i].FindControl("labelTag");
                    if(labelTag.Text == listAddTag.SelectedValue.ToString())
                    {
                        DataRow nRow = marcTable.NewRow();
                        nRow["TAG"] = labelTag.Text;
                        Label labelTagIndex = (Label)marcTextRepeater.Items[i].FindControl("labelTagIndex");
                        nRow["TAG_INDEX"] = int.Parse(labelTagIndex.Text);
                        nRow["SUBFIELD_CODE"] = Sf.Code;
                        nRow["SUBFIELD_TYPE"] = Sf.Type;
                        nRow["SUBFIELD_LABEL"] = Sf.Value;
                        nRow["SUBFIELD_VALUE"] = txtAddValue.Text;
                        marcTable.Rows.Add(nRow);
                        //sort go here
                        marcTable.DefaultView.Sort = "TAG, TAG_INDEX ASC";
                        _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
                        repeater_DataBind(marcTable);
                        _MetaContent.bind_TableDataToMarc(ref marcTable);
                        Session["METADATA"] = _MetaContent.OuterXml;
                        return;                    
                    }                                
                }
            }
            #endregion
            //if don't have match selected tag find existing tag
            for (int i = 0; i < marcTable.Rows.Count; i++)
            {
                if (marcTable.Rows[i]["TAG"].ToString() == listAddTag.SelectedValue.ToString())
                {
                    DataRow nRow = marcTable.NewRow();
                    nRow["TAG"] = marcTable.Rows[i]["TAG"];
                    nRow["TAG_INDEX"] = marcTable.Rows[i]["TAG_INDEX"];
                    nRow["SUBFIELD_CODE"] = Sf.Code;
                    nRow["SUBFIELD_TYPE"] = Sf.Type;
                    nRow["SUBFIELD_LABEL"] = Sf.Value;
                    nRow["SUBFIELD_VALUE"] = txtAddValue.Text;
                    marcTable.Rows.Add(nRow);
                    //sort go here
                    marcTable.DefaultView.Sort = "TAG, TAG_INDEX ASC";
                    _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
                    repeater_DataBind(marcTable);
                    _MetaContent.bind_TableDataToMarc(ref marcTable);
                    Session["METADATA"] = _MetaContent.OuterXml;
                    return;
                }
            }
            //don't have existing match tag create new tag with one subfield
            DataRow addRow = marcTable.NewRow();
            addRow["TAG"] = Df.Tag;
            addRow["TAG_INDEX"] = iTagIndex + 1;
            addRow["SUBFIELD_CODE"] = Sf.Code;
            addRow["SUBFIELD_TYPE"] = Sf.Type;
            addRow["SUBFIELD_LABEL"] = Sf.Value;
            addRow["SUBFIELD_VALUE"] = txtAddValue.Text;
            marcTable.Rows.Add(addRow);
            //sort go here
            marcTable.DefaultView.Sort = "TAG, TAG_INDEX ASC";
            _MetaContent.set_DataTableLabel(ref marcTable, labelRec);
            repeater_DataBind(marcTable);
            _MetaContent.bind_TableDataToMarc(ref marcTable);
            Session["METADATA"] = _MetaContent.OuterXml;
            return;
        }

    }

    protected override void OnInit(EventArgs e)
    {
        _MetaContent = new ContentEditorDataHelper();
        base.OnInit(e);
    }
    public void Cancel_Click()
    {
        Session["METADATA"] = null;
        Response.Redirect("MetaContentManagerTree.aspx?section_id=" + dropSections.SelectedValue.ToString() + "&category_id=" + dropCategories.SelectedValue.ToString());
    }
    protected void listAddTag_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        CRecord labelRec = new CRecord();
        string labelFileName = FileTemplateDataProvider.get_LabelTemplateFile(LegoWeb.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(int.Parse(dropCategories.SelectedValue.ToString())));
        labelRec.load_File(labelFileName);
        listAddSubfieldCode.Items.Clear();
        listAddSubfieldCode.Items.Add(new ListItem("All", ""));
        CDatafield Df = new CDatafield();
        if (labelRec.Datafields.get_Datafield(listAddTag.SelectedValue, ref Df))
        {
            for (int i = 0; i < Df.Subfields.Count; i++)
            {
                listAddSubfieldCode.Items.Add(new ListItem(Df.Subfields.Subfield(i).Value, Df.Subfields.Subfield(i).Code));
            }
        }
    }

    public void preview_MetaContent()
    {
        save_MetaContentData();//save data to  Session["METADATA"] first
        Response.Redirect("MetaContentPreview.aspx");
    }
    protected void linkTakeRelatedContent_Click(object sender, EventArgs e)
    {
        save_MetaContentData();
        Response.Redirect("LinkRelatedContent.aspx");
    }
    protected void cblRoles_Init(object sender, EventArgs e)
    {
        string[] availableRoles = Roles.GetAllRoles();
        for (int i = 0; i < availableRoles.Length; i++)
        {
            this.cblRoles.Items.Add(new ListItem(availableRoles[i],availableRoles[i]));
        }
    }
    protected void dropAccessLevels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(dropAccessLevels.SelectedValue) >= 2)
        {
            cblRoles.Visible = true;
        }
        else
        {
            cblRoles.Visible = false;
        }
    }

    /// <summary>
    /// Kiem tra xem current user co quyen cap nhat thong tin khong
    /// </summary>
    /// <param name="iCategoryId"></param>
    /// <returns></returns>
    public static bool is_UserCanUpdateContent(int iCATEGORY_ID)
    {
        bool isUserCan = true;
        DataTable catTable =LegoWeb.BusLogic.Categories.get_CATEGORY_BY_ID(iCATEGORY_ID).Tables[0];
        int iAdminLevel = int.Parse(catTable.Rows[0]["ADMIN_LEVEL"].ToString());
        if (iAdminLevel > 0)
        {
            string sAdminRoles = catTable.Rows[0]["ADMIN_ROLES"].ToString();
            if (!String.IsNullOrEmpty(sAdminRoles))
            {
                string[] allowRoles = sAdminRoles.Split(new char[] { ',',';' });
                isUserCan = false;//reset to false then check
                for (int i = 0; i < allowRoles.Length; i++)
                {
                    if (Roles.IsUserInRole(allowRoles[i]))
                    {
                        isUserCan = true;
                        break;
                    }
                }
            }
        }
        return isUserCan;
    }
}
