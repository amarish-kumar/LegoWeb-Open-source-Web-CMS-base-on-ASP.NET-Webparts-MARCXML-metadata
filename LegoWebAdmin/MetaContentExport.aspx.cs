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
            if (!Roles.IsUserInRole("ADMINISTRATORS"))
            {
                Response.Redirect("ErrorMessage.aspx?ErrorMessage='Bạn không có quyền truy cập vào tính năng này!'");
            }
        }
    }
    protected void dropSections_Init(object sender, EventArgs e)
    {
        DataTable secData = LegoWeb.BusLogic.Sections.get_Search_Page(1, 100).Tables[0];
        this.dropSections.DataTextField = "SECTION_VI_TITLE";
        this.dropSections.DataValueField = "SECTION_ID";
        this.dropSections.DataSource = secData;
        this.dropSections.DataBind();

    }

    protected void load_dropCategories()
    {
        this.dropCategories.Items.Clear();
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue != null ? int.Parse(this.dropSections.SelectedValue.ToString()) : 0, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
        this.dropCategories.DataValueField = "CATEGORY_ID";
        this.dropCategories.DataSource = catData;
        this.dropCategories.DataBind();
    }

    protected void dropCategories_Init(object sender, EventArgs e)
    {
        DataTable catData = LegoWeb.BusLogic.Categories.get_Search_Page(0, 0, this.dropSections.SelectedValue != null ? int.Parse(this.dropSections.SelectedValue.ToString()) : 0, " - ", 1, 100).Tables[0];
        this.dropCategories.DataTextField = "CATEGORY_VI_TITLE";
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
            DataTable tbData = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID).Tables[0];

            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                string sXmlContent = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), true);
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

            DataTable tbData = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID).Tables[0];

            for (int i = 0; i < tbData.Rows.Count; i++)
            {
                string sXmlContent = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), true);
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
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (radioFilterType.SelectedValue == "1")
        {
            int iFromID = String.IsNullOrEmpty(txtFromId.Text) ? 0 : int.Parse(txtFromId.Text);
            int iToID = String.IsNullOrEmpty(txtToId.Text) ? 0 : int.Parse(txtToId.Text);
            DataSet data = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID);
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
            DataSet data = LegoWeb.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID);
            metaContentRepeater.DataSource = data;
            metaContentRepeater.DataBind();
        }
    }


}
