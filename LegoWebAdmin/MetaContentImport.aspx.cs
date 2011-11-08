// ----------------------------------------------------------------------
// <copyright file="MetaContentImport.aspx.cs" package="LEGOWEB">
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
using MarcXmlParserEx;
using System.Web.Security;

public partial class MetaContentImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnBrowse.Text = Resources.strings.btnBrowse_Text;
            btnAnalyse.Text = Resources.strings.btnAnalyse_Text;
            ListItem item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.Append_Text);
            item.Value = "0";
            item.Selected = true;
            radioImportTypes.Items.Add(item);
            item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.SkipIfDuplicatedID_Text);
            item.Value = "1";
            radioImportTypes.Items.Add(item);
            item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.OverWriteIfDuplicatedID_Text);
            item.Value = "2";
            radioImportTypes.Items.Add(item);
            //load force to default category

            item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.AutoDetectCategory_Text);
            item.Value = "0";
            item.Selected = true;
            radioForceToDefaultCategory.Items.Add(item);

            item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.ForceToDefaultCategory_Text);
            item.Value = "1";
            radioForceToDefaultCategory.Items.Add(item);



            load_dropSections();
            load_dropCategories();

            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized to import metadata!'");
            }
        }
    }
    protected void linkImportContentButton_Click(object sender, EventArgs e)
    {
       litErrorSpaceHolder.Text = "";
        //try
        //{
        divDefaultCategory.Visible = false;
        DataTable contentTable = create_ContentTable();
        if (String.IsNullOrEmpty(txtFileName.Text))
        {
            throw new Exception("You must select a source file!");
        }
        //chuyển đổi địa chỉ URL sang Physycal
        string sFileURL = txtFileName.Text;
        string sFileName= sFileURL.Replace(System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesVirtuaPath"],System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"]);
        sFileName = sFileName.Replace("/", "\\");
        if (!System.IO.File.Exists(sFileName))
        {
            throw new Exception("Source file does not exists!");
        }
        else
        {
            divDefaultCategory.Visible = true;
        }

        CRecords myRecs = new CRecords();
        myRecs.load_File(sFileName);
        int iSkipCount = 0;
        for (int i = 0; i < myRecs.Count; i++)
        {
            CRecord myRec = new CRecord();
            myRec.load_Xml(myRecs.Record(i).OuterXml);

            if (myRec.get_LeaderValueByPos(6, 6) == "s")//system tables data
            {
                import_SystemTableData(myRec);
            }
            else
            {
                Int32 iID = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("001").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("001").Value);
                Int32 iCatID = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("002").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("002").Value);
                switch (radioImportTypes.SelectedValue)
                {
                    case "0"://append
                        myRec.Controlfields.Controlfield("001").Value = "0";
                        switch (radioForceToDefaultCategory.SelectedValue)
                        {
                            case "0"://auto detech category id
                                if (!LegoWebAdmin.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                                {
                                    myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                }
                                break;
                            case "1": //force to default category
                                myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                break;
                        }
                        LegoWebAdmin.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
                        break;
                    case "1"://skip if ID exsist
                        if (!LegoWebAdmin.BusLogic.MetaContents.is_META_CONTENTS_EXIST(iID))
                        {
                            switch (radioForceToDefaultCategory.SelectedValue)
                            {
                                case "0"://auto detech category id
                                    if (!LegoWebAdmin.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                                    {
                                        myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                    }
                                    break;
                                case "1": //force to default category
                                    myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                    break;
                            }
                            LegoWebAdmin.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
                        }
                        else
                        {
                            iSkipCount++;
                        }
                        break;
                    case "2":
                        switch (radioForceToDefaultCategory.SelectedValue)
                        {
                            case "0"://auto detech category id
                                if (!LegoWebAdmin.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                                {
                                    myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                }
                                break;
                            case "1": //force to default category
                                myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                break;
                        }
                        LegoWebAdmin.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
                        break;
                }
            }
        }
        
        litErrorSpaceHolder.Text = String.Format("Imports successfully {0} records, skips {1}!.", myRecs.Count.ToString(), iSkipCount.ToString());
//        }
//        catch (Exception ex)
//        {
//            String errorFomat = @"<dl id='system-message'>
//                                            <dd class='error message fade'>
//	                                            <ul>
//		                                            <li>{0}</li>
//	                                            </ul>
//                                            </dd>
//                                            </dl>";
//            litErrorSpaceHolder.Text = String.Format(errorFomat, ex.Message);

//        }
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ControlPanel.aspx");
    }

    protected void btnAnalyse_Click(object sender, EventArgs e)
    {
        litErrorSpaceHolder.Text = "";
        try
        {
        divDefaultCategory.Visible = false;
        DataTable contentTable = create_ContentTable();
        if (String.IsNullOrEmpty(txtFileName.Text))
        {
            throw new Exception("You must select a source file!");
        }
        //chuyển đổi địa chỉ URL sang Physycal
        string sFileURL = txtFileName.Text;
        string sFileName= sFileURL.Replace(System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesVirtuaPath"],System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"]);
        sFileName = sFileName.Replace("/", "\\");
        if (!System.IO.File.Exists(sFileName))
        {
            throw new Exception("File doest not exists!");
        }
        else
        {

            divDefaultCategory.Visible = true;
        }

        CRecords myRecs = new CRecords();
        myRecs.load_File(sFileName);        
        for (int i = 0; i < myRecs.Count; i++)
        {
            DataRow row = contentTable.NewRow();
            if (myRecs.Record(i).get_LeaderValueByPos(6, 6) == "s")//system tables data
            {
                row["META_CONTENT_ID"] = 0;
                row["META_CONTENT_TITLE"] = myRecs.Record(i).Datafields.Datafield("245").Subfields.Subfield("a").Value;
                row["LANG_CODE"] = "";
                row["CATEGORY_ID"] = 0;
            }
            else
            {
                row["META_CONTENT_ID"] = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("001").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("001").Value);
                row["META_CONTENT_TITLE"] = myRecs.Record(i).Datafields.Datafield("245").Subfields.Subfield("a").Value;
                row["LANG_CODE"] = myRecs.Record(i).Controlfields.Controlfield("008").get_ValueByPos(35, 36);
                row["CATEGORY_ID"] = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("002").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("002").Value);
            }
            contentTable.Rows.Add(row);
        }

        metaContentRepeater.DataSource = contentTable;
        metaContentRepeater.DataBind();
        }
        catch (Exception ex)
        {
            String errorFomat = @"<dl id='system-message'>
                                            <dd class='error message fade'>
	                                            <ul>
		                                            <li>{0}</li>
	                                            </ul>
                                            </dd>
                                            </dl>";
            litErrorSpaceHolder.Text = String.Format(errorFomat, ex.Message);

        }


    }
    protected void btnBrowse_Click(object sender, EventArgs e)
    {

    }

    private DataTable create_ContentTable()
    {
        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.Int32");
        myDataColumn.ColumnName = "META_CONTENT_ID";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "META_CONTENT_TITLE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "LANG_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.Int32");
        myDataColumn.ColumnName = "CATEGORY_ID";
        myDataTable.Columns.Add(myDataColumn);        

        return myDataTable;
    }


    protected void load_dropSections()
    {
        DataTable secData = LegoWebAdmin.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();

    }

    protected void load_dropCategories()
    {
        this.dropCategories.Items.Clear();
        DataTable catData = LegoWebAdmin.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue != null ? int.Parse(this.dropSections.SelectedValue.ToString()) : 0, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() +  "_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }

    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["metaContentManagerPageNumber"] = 1;
        load_dropCategories();
    }

    protected void import_SystemTableData(CRecord tblRec)
    {
        CDatafields Dfs;
        CDatafield Df=new CDatafield();
        switch (tblRec.Controlfields.Controlfield("001").Value)
        { 
            case "LEGOWEB_COMMON_PARAMETERS":
                //Df.SubfieldsText = String.Format("$a{0} $b{1} $c{2} $d{3} $e{4}", row["PARAMETER_NAME"].ToString(), row["PARAMETER_TYPE"].ToString(), row["PARAMETER_VI_VALUE"].ToString(), row["PARAMETER_EN_VALUE"].ToString(), row["PARAMETER_DESCRIPTION"].ToString());
                Dfs=tblRec.Datafields;
                Dfs.Filter("650");
                for (int i = 0; i < Dfs.Count; i++)
                { 
                    Df=Dfs.Datafield(i);
                    LegoWebAdmin.BusLogic.CommonParameters.addudp_LEGOWEB_COMMON_PARAMETER(Df.Subfields.Subfield("a").Value, int.Parse(Df.Subfields.Subfield("b").Value), Df.Subfields.Subfield("c").Value, Df.Subfields.Subfield("d").Value, Df.Subfields.Subfield("e").Value);
                }
            break;
            case "LEGOWEB_SECTIONS":
            //Df.SubfieldsText = String.Format("$a{0} $b{1} $c{2}", row["SECTION_ID"].ToString(), row["SECTION_VI_TITLE"].ToString(), row["SECTION_EN_TITLE"].ToString());
            Dfs = tblRec.Datafields;
            Dfs.Filter("650");
            for (int i = 0; i < Dfs.Count; i++)
            {
                Df = Dfs.Datafield(i);
                LegoWebAdmin.BusLogic.Sections.add_Update(int.Parse(Df.Subfields.Subfield("a").Value), Df.Subfields.Subfield("b").Value, Df.Subfields.Subfield("c").Value);
            }
            break;

            case "LEGOWEB_CATEGORIES":
            //Df.SubfieldsText = String.Format("$a{0} $b{1} $c{2} $d{3} $e{4} $f{5} $g{6} $h{7} $i{8} $j{9} $k{10} $l{11} $m{12} $n{13} $o{14} $p{15}", row["CATEGORY_ID"].ToString(), row["PARENT_CATEGORY_ID"].ToString(), row["SECTION_ID"].ToString(), row["CATEGORY_VI_TITLE"].ToString(), row["CATEGORY_EN_TITLE"].ToString(), row["CATEGORY_ALIAS"].ToString(), row["CATEGORY_TEMPLATE_NAME"].ToString(), row["CATEGORY_IMAGE_URL"].ToString(), row["MENU_ID"].ToString(), row["IS_PUBLIC"].ToString(), row["ADMIN_LEVEL"].ToString(), row["ADMIN_ROLES"].ToString(), row["SEO_TITLE"].ToString(), row["SEO_DESCRIPTION"].ToString(), row["SEO_KEYWORDS"].ToString(),row["ORDER_NUMBER"].ToString());

            Dfs = tblRec.Datafields;
            Dfs.Filter("650");
            for (int i = 0; i < Dfs.Count; i++)
            {
                Df = Dfs.Datafield(i);
                LegoWebAdmin.BusLogic.Categories.addUpdate_CATEGORY(int.Parse(Df.Subfields.Subfield("a").Value), int.Parse(Df.Subfields.Subfield("b").Value), int.Parse(Df.Subfields.Subfield("c").Value), Df.Subfields.Subfield("d").Value, Df.Subfields.Subfield("e").Value, Df.Subfields.Subfield("f").Value, Df.Subfields.Subfield("g").Value, Df.Subfields.Subfield("h").Value, int.Parse(Df.Subfields.Subfield("i").Value), Convert.ToBoolean(Df.Subfields.Subfield("j").Value), int.Parse(Df.Subfields.Subfield("k").Value),Df.Subfields.Subfield("l").Value, Df.Subfields.Subfield("m").Value, Df.Subfields.Subfield("n").Value,Df.Subfields.Subfield("o").Value);
                if (!String.IsNullOrEmpty(Df.Subfields.Subfield("p").Value))
                {
                    LegoWebAdmin.BusLogic.Categories.update_CATEGORY_ORDER(int.Parse(Df.Subfields.Subfield("a").Value), int.Parse(Df.Subfields.Subfield("p").Value));
                }
            }
            break;

            case "LEGOWEB_MENU_TYPES":
            //Df.SubfieldsText = String.Format("$a{0} $b{1} $c{2} $d{3}", row["MENU_TYPE_ID"].ToString(), row["MENU_TYPE_VI_TITLE"].ToString(), row["MENU_TYPE_EN_TITLE"].ToString(), row["MENU_TYPE_DESCRIPTION"].ToString());
            Dfs = tblRec.Datafields;
            Dfs.Filter("650");
            for (int i = 0; i < Dfs.Count; i++)
            {
                Df = Dfs.Datafield(i);
                LegoWebAdmin.BusLogic.MenuTypes.addUpdate_MenuType(int.Parse(Df.Subfields.Subfield("a").Value), Df.Subfields.Subfield("b").Value, Df.Subfields.Subfield("c").Value, Df.Subfields.Subfield("d").Value);
            }
            break;
            

            case "LEGOWEB_MENUS":
            //Df.SubfieldsText = String.Format("$a{0} $b{1} $c{2} $d{3} $e{4} $f{5} $g{6} $h{7} $i{8} $j{9}", row["MENU_ID"].ToString(), row["PARENT_MENU_ID"].ToString(), row["MENU_TYPE_ID"].ToString(), row["MENU_VI_TITLE"].ToString(), row["MENU_EN_TITLE"].ToString(), row["MENU_IMAGE_URL"].ToString(), row["MENU_LINK_URL"].ToString(), row["BROWSER_NAVIGATE"].ToString(), row["IS_PUBLIC"].ToString(),row["ORDER_NUMBER"].ToString());
            Dfs = tblRec.Datafields;
            Dfs.Filter("650");
            for (int i = 0; i < Dfs.Count; i++)
            {
                Df = Dfs.Datafield(i);
                LegoWebAdmin.BusLogic.Menus.addUpdate_MENU(int.Parse(Df.Subfields.Subfield("a").Value), int.Parse(Df.Subfields.Subfield("b").Value), int.Parse(Df.Subfields.Subfield("c").Value), Df.Subfields.Subfield("d").Value, Df.Subfields.Subfield("e").Value, Df.Subfields.Subfield("f").Value, Df.Subfields.Subfield("g").Value, int.Parse(Df.Subfields.Subfield("h").Value), Convert.ToBoolean(Df.Subfields.Subfield("i").Value));
                if (!String.IsNullOrEmpty(Df.Subfields.Subfield("j").Value))
                {
                    LegoWebAdmin.BusLogic.Menus.update_MENU_ORDER(int.Parse(Df.Subfields.Subfield("a").Value), int.Parse(Df.Subfields.Subfield("j").Value));
                }
            }
            break;

        }    
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
