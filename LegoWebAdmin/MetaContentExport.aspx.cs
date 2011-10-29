using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MarcXmlParserEx;
using System.Web.Security;

public partial class MetaContentExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnShowResults.Text = Resources.strings.btnShowResults_Text;
            //load filter type radio buttons
            ListItem item=new ListItem();
            item.Text=String.Format("<span style='width:50px'>{0}</span>",Resources.strings.FilterByCategory_Text);
            item.Value="0";
            item.Selected=true;
            radioFilterType.Items.Add(item);
            item=new ListItem();
            item.Text=String.Format("<span style='width:50px'>{0}</span>",Resources.strings.FilterByID_Text);
            item.Value="1";
            radioFilterType.Items.Add(item);

            load_dropSections();
            load_dropCategories();

            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='You are not authorized to export metadata!'");
            }
        }
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
        this.dropCategories.DataTextField = "CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }

    protected void dropSections_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["metaContentManagerPageNumber"] = 1;
        load_dropCategories();
    }
    protected void linkExportButton_Click(object sender, EventArgs e)
    {
        CRecords exRecs = new CRecords();
        CRecord myRec = new CRecord();
        if (radioFilterType.SelectedValue == "1")
        {
            int iFromID = String.IsNullOrEmpty(txtFromId.Text) ? 0 : int.Parse(txtFromId.Text);
            int iToID = String.IsNullOrEmpty(txtToId.Text) ? 0 : int.Parse(txtToId.Text);
            DataTable tbData = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID).Tables[0];

            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                string sXmlContent = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), 1);
                myRec.load_Xml(sXmlContent);
                exRecs.Add(myRec);
            }
        }
        else
        {
            int iSectionID = 0;
            int iCategoryID = 0;
            if (dropSections.SelectedValue != null)
            {
                iSectionID = int.Parse(dropSections.SelectedValue.ToString());
            }
            if (dropCategories.SelectedValue != null)
            {
                iCategoryID = int.Parse(dropCategories.SelectedValue.ToString());
            }

            DataTable tbData = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID).Tables[0];

            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                string sXmlContent = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), 1);
                myRec.load_Xml(sXmlContent);
                exRecs.Add(myRec);
            }
        
        }

        Response.ContentType = "APPLICATION/OCTET-STREAM";
        //set the filename
        Response.AddHeader("Content-Disposition", "attachment;filename=\"legowebContent.xml\"");
        String outStream = exRecs.OuterXml;
        Response.Write(outStream);
        Response.End();
    }
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ControlPanel.aspx");
    }

    protected void radioFilterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radioFilterType.SelectedValue == "0")
        {
            divFilterByCat.Visible = true;
            divFilterById.Visible = false;
        }
        else
        {
            divFilterByCat.Visible = false;
            divFilterById.Visible = true;       
        }

    }
    protected void btnShowResults_Click(object sender, EventArgs e)
    {
        if (radioFilterType.SelectedValue == "1")
        {
            int iFromID = String.IsNullOrEmpty(txtFromId.Text) ? 0 : int.Parse(txtFromId.Text);
            int iToID = String.IsNullOrEmpty(txtToId.Text) ? 0 : int.Parse(txtToId.Text);
            DataSet data = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID);
            metaContentRepeater.DataSource = data;
            metaContentRepeater.DataBind();
        }
        else
        {
            int iSectionID = 0;
            int iCategoryID = 0;
            if (dropSections.SelectedValue != null)
            {
                iSectionID = int.Parse(dropSections.SelectedValue.ToString());
            }
            if (dropCategories.SelectedValue != null)
            {
                iCategoryID = int.Parse(dropCategories.SelectedValue.ToString());
            }
            DataSet data = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID);
            metaContentRepeater.DataSource = data;
            metaContentRepeater.DataBind();
        }
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }

}
