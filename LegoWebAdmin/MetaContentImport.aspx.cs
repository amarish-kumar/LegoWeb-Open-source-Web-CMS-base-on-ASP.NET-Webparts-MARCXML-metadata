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
        divDefaultCategory.Visible = false;
        DataTable contentTable = create_ContentTable();
        if (String.IsNullOrEmpty(txtFileName.Text))
        {
            litErrorMessage.Visible = true;
            litErrorMessage.Text = "You must select a metadata source file!";
            txtFileName.Focus();
            return;            
        }
        //chuyển đổi địa chỉ URL sang Physycal
        string sFileURL = txtFileName.Text;
        string sFileName= sFileURL.Replace(System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesVirtuaPath"],System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"]);
        sFileName = sFileName.Replace("/", "\\");
        if (!System.IO.File.Exists(sFileName))
        {
            litErrorMessage.Visible = true;
            litErrorMessage.Text = "source file does not exist!";
            return;
        }
        else
        {
            litErrorMessage.Visible = false;
            litErrorMessage.Text = "";
            divDefaultCategory.Visible = true;
        }

        CRecords myRecs = new CRecords();
        myRecs.load_File(sFileName);
        int iSkipCount = 0;
        for (int i = 0; i < myRecs.Count; i++)
        {
            CRecord myRec = new CRecord();
            myRec.load_Xml(myRecs.Record(i).OuterXml);
            Int32 iID = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("001").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("001").Value);
            Int32 iCatID = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("002").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("002").Value);            
            switch (radioImportTypes.SelectedValue)
            { 
                case "0"://append
                    myRec.Controlfields.Controlfield("001").Value = "0";
                    switch (radioForceToDefaultCategory.SelectedValue)
                    { 
                        case "0"://auto detech category id
                            if (!LegoWeb.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                            {
                                myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                            }                            
                            break;
                        case "1": //force to default category
                            myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                            break;
                    }
                    LegoWeb.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
                    break;
                case "1"://skip if ID exsist
                    if (!LegoWeb.BusLogic.MetaContents.is_META_CONTENTS_EXIST(iID))
                    {
                        switch (radioForceToDefaultCategory.SelectedValue)
                        {
                            case "0"://auto detech category id
                                if (!LegoWeb.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                                {
                                    myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                }
                                break;
                            case "1": //force to default category
                                myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                                break;
                        }
                        LegoWeb.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
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
                            if (!LegoWeb.BusLogic.Categories.is_CATEGORY_ID_EXIST(iCatID))
                            {
                                myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                            }
                            break;
                        case "1": //force to default category
                            myRec.Controlfields.Controlfield("002").Value = dropCategories.SelectedValue;
                            break;
                    }
                    LegoWeb.BusLogic.MetaContents.save_META_CONTENTS_XML(myRec.OuterXml, this.Page.User.Identity.Name);
                    break;
            }
        }
        litErrorMessage.Visible = true;
        litErrorMessage.Text = String.Format("Imports successfully {0} records, skips {1}!.",myRecs.Count.ToString(), iSkipCount.ToString());
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ControlPanel.aspx");
    }

    protected void btnAnalyse_Click(object sender, EventArgs e)
    {
        divDefaultCategory.Visible = false;
        DataTable contentTable = create_ContentTable();
        if (String.IsNullOrEmpty(txtFileName.Text))
        {
            litErrorMessage.Visible = true;
            litErrorMessage.Text = "You must select a metadata source file!";
            txtFileName.Focus();
            return;            
        }
        //chuyển đổi địa chỉ URL sang Physycal
        string sFileURL = txtFileName.Text;
        string sFileName= sFileURL.Replace(System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesVirtuaPath"],System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"]);
        sFileName = sFileName.Replace("/", "\\");
        if (!System.IO.File.Exists(sFileName))
        {
            litErrorMessage.Visible = true;
            litErrorMessage.Text = "Physical file does not exist!";
            return;
        }
        else
        {
            litErrorMessage.Visible = false;
            litErrorMessage.Text = "";
            divDefaultCategory.Visible = true;
        }

        CRecords myRecs = new CRecords();
        myRecs.load_File(sFileName);        
        for (int i = 0; i < myRecs.Count; i++)
        {
            DataRow row = contentTable.NewRow();
            row["META_CONTENT_ID"] = String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("001").Value) ? 0 : Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("001").Value);
            row["META_CONTENT_TITLE"] = myRecs.Record(i).Datafields.Datafield("245").Subfields.Subfield("a").Value;
            row["LANG_CODE"] = myRecs.Record(i).Controlfields.Controlfield("008").get_ValueByPos(35, 36);
            row["CATEGORY_ID"] =String.IsNullOrEmpty(myRecs.Record(i).Controlfields.Controlfield("002").Value)?0:Int32.Parse(myRecs.Record(i).Controlfields.Controlfield("002").Value);

            contentTable.Rows.Add(row);
        }

        metaContentRepeater.DataSource = contentTable;
        metaContentRepeater.DataBind();

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
        DataTable secData = LegoWeb.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();

    }

    protected void load_dropCategories()
    {
        this.dropCategories.Items.Clear();
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue != null ? int.Parse(this.dropSections.SelectedValue.ToString()) : 0, " - ", 1, 100).Tables[0];
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

    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
