// ----------------------------------------------------------------------
// <copyright file="MetaContentExport.aspx.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
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

public partial class MetaContentExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnShowResults.Text = Resources.strings.ShowResults_Text;
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

            item = new ListItem();
            item.Text = String.Format("<span style='width:50px'>{0}</span>", Resources.strings.SystemTables_Text);
            item.Value = "2";
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
        CControlfield Cf = new CControlfield();
        CDatafield Df = new CDatafield();
        CSubfield Sf = new CSubfield();

        DataTable tbData=new DataTable();

        try
            {
                switch(radioFilterType.SelectedValue)
                {
                    case "0":
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

                             tbData= LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID).Tables[0];

                            for (int i = 0; i < tbData.Rows.Count; i++)
                            {
                                string sXmlContent = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), 1);
                                myRec.load_Xml(sXmlContent);
                                exRecs.Add(myRec);
                            }
                        break;
                    case "1":
                    
                            int iFromID = String.IsNullOrEmpty(txtFromId.Text) ? 0 : int.Parse(txtFromId.Text);
                            int iToID = String.IsNullOrEmpty(txtToId.Text) ? 0 : int.Parse(txtToId.Text);
                            tbData = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID).Tables[0];

                            for (int i = 0; i < tbData.Rows.Count; i++)
                            {
                                string sXmlContent = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_MARCXML(Int16.Parse(tbData.Rows[i]["META_CONTENT_ID"].ToString()), 1);
                                myRec.load_Xml(sXmlContent);
                                exRecs.Add(myRec);
                            }

                        break;
                    case "2":
                        //create each system table one MarcRecord
                        //leader 06 = s mean system table data record
                        //001 value is TABLE NAME

                        //LEGOWEB_COMMON_PARAMETERS
                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "LEGOWEB_COMMON_PARAMETERS";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aLEGOWEB_COMMON_PARAMETERS TABLE DATA";
                        myRec.Datafields.Add(Df);
                        
                        tbData = LegoWebAdmin.BusLogic.CommonParameters.get_LEGOWEB_COMMON_PARAMETERS().Tables[0];

                        foreach(DataRow row in tbData.Rows)
                        {
                            //a PARAMETER_NAME	nvarchar(50)	Unchecked
                            //b PARAMETER_TYPE	smallint	Checked
                            //c PARAMETER_VI_VALUE	nvarchar(255)	Checked
                            //d PARAMETER_EN_VALUE	nvarchar(255)	Checked
                            //e PARAMETER_DESCRIPTION	nvarchar(255)	Checked
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}$d{3}$e{4}", row["PARAMETER_NAME"].ToString(), row["PARAMETER_TYPE"].ToString(), row["PARAMETER_VI_VALUE"].ToString(), row["PARAMETER_EN_VALUE"].ToString(), row["PARAMETER_DESCRIPTION"].ToString());
                            myRec.Datafields.Add(Df);                            
                        }

                        exRecs.Add(myRec);

                        //LEGOWEB_SECTIONS
                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "LEGOWEB_SECTIONS";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aLEGOWEB_SECTIONS TABLE DATA";
                        myRec.Datafields.Add(Df);

                        tbData = LegoWebAdmin.BusLogic.Sections.get_LEGOWEB_SECTIONS().Tables[0];

                        foreach (DataRow row in tbData.Rows)
                        {
                            //a SECTION_ID	int	Unchecked
                            //b SECTION_VI_TITLE	nvarchar(250)	Unchecked
                            //c SECTION_EN_TITLE	nvarchar(250)	Checked
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}", row["SECTION_ID"].ToString(), row["SECTION_VI_TITLE"].ToString(), row["SECTION_EN_TITLE"].ToString());
                            myRec.Datafields.Add(Df);
                        }

                        exRecs.Add(myRec);

                        //LEGOWEB_CATEGORIES
                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "LEGOWEB_CATEGORIES";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aLEGOWEB_CATEGORIES TABLE DATA";
                        myRec.Datafields.Add(Df);

                        tbData = LegoWebAdmin.BusLogic.Categories.get_LEGOWEB_CATEGORIES().Tables[0];

                        foreach (DataRow row in tbData.Rows)
                        {
                            //a CATEGORY_ID	int	Unchecked
                            //b PARENT_CATEGORY_ID	int	Checked
                            //c SECTION_ID	int	Unchecked
                            //d CATEGORY_VI_TITLE	nvarchar(250)	Unchecked
                            //e CATEGORY_EN_TITLE	nvarchar(250)	Checked
                            //f CATEGORY_ALIAS	nvarchar(250)	Checked
                            //g CATEGORY_TEMPLATE_NAME	nvarchar(50)	Checked
                            //h CATEGORY_IMAGE_URL	nvarchar(250)	Checked
                            //i MENU_ID	int	Unchecked
                            //j IS_PUBLIC	bit	Checked
                            //k ADMIN_LEVEL	smallint	Checked
                            //l ADMIN_ROLES	nvarchar(250)	Checked
                            //m SEO_TITLE	nvarchar(100)	Checked
                            //n SEO_DESCRIPTION	nvarchar(255)	Checked
                            //o SEO_KEYWORDS	nvarchar(255)	Checked
                            //p ORDER_NUMBER smallint 
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}$d{3}$e{4}$f{5}$g{6}$h{7}$i{8}$j{9}$k{10}$l{11}$m{12}$n{13}$o{14}$p{15}", row["CATEGORY_ID"].ToString(), row["PARENT_CATEGORY_ID"].ToString(), row["SECTION_ID"].ToString(), row["CATEGORY_VI_TITLE"].ToString(), row["CATEGORY_EN_TITLE"].ToString(), row["CATEGORY_ALIAS"].ToString(), row["CATEGORY_TEMPLATE_NAME"].ToString(), row["CATEGORY_IMAGE_URL"].ToString(), row["MENU_ID"].ToString(), row["IS_PUBLIC"].ToString(), row["ADMIN_LEVEL"].ToString(), row["ADMIN_ROLES"].ToString(), row["SEO_TITLE"].ToString(), row["SEO_DESCRIPTION"].ToString(), row["SEO_KEYWORDS"].ToString(), row["ORDER_NUMBER"].ToString());
                            myRec.Datafields.Add(Df);
                        }

                        exRecs.Add(myRec);

                        //LEGOWEB_MENU_TYPES
                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "LEGOWEB_MENU_TYPES";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aLEGOWEB_MENU_TYPES TABLE DATA";
                        myRec.Datafields.Add(Df);

                        tbData = LegoWebAdmin.BusLogic.MenuTypes.get_LEGOWEB_MENU_TYPES().Tables[0];

                        foreach (DataRow row in tbData.Rows)
                        {
                            //a MENU_TYPE_ID	smallint	Unchecked
                            //b MENU_TYPE_VI_TITLE	nvarchar(50)	Unchecked
                            //c MENU_TYPE_EN_TITLE	nvarchar(50)	Unchecked
                            //d MENU_TYPE_DESCRIPTION	nvarchar(250)	Checked
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}$d{3}", row["MENU_TYPE_ID"].ToString(), row["MENU_TYPE_VI_TITLE"].ToString(), row["MENU_TYPE_EN_TITLE"].ToString(), row["MENU_TYPE_DESCRIPTION"].ToString());
                            myRec.Datafields.Add(Df);
                        }

                        exRecs.Add(myRec);


                        //LEGOWEB_MENUS
                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "LEGOWEB_MENUS";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aLEGOWEB_MENUS TABLE DATA";
                        myRec.Datafields.Add(Df);

                        tbData = LegoWebAdmin.BusLogic.Menus.get_LEGOWEB_MENUS().Tables[0];

                        foreach (DataRow row in tbData.Rows)
                        {
                            //a MENU_ID	int	Unchecked
                            //b PARENT_MENU_ID	int	Unchecked
                            //c MENU_TYPE_ID	int	Unchecked
                            //d MENU_VI_TITLE	nvarchar(50)	Checked
                            //e MENU_EN_TITLE	nvarchar(50)	Checked
                            //f MENU_IMAGE_URL	nvarchar(250)	Checked
                            //g MENU_LINK_URL	nvarchar(50)	Checked
                            //h BROWSER_NAVIGATE	tinyint	Unchecked
                            //i IS_PUBLIC	bit	Unchecked
                            //j ORDER_NUMBER smallint
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}$d{3}$e{4}$f{5}$g{6}$h{7}$i{8}$j{9}", row["MENU_ID"].ToString(), row["PARENT_MENU_ID"].ToString(), row["MENU_TYPE_ID"].ToString(), row["MENU_VI_TITLE"].ToString(), row["MENU_EN_TITLE"].ToString(), row["MENU_IMAGE_URL"].ToString(), row["MENU_LINK_URL"].ToString(), row["BROWSER_NAVIGATE"].ToString(), row["IS_PUBLIC"].ToString(), row["ORDER_NUMBER"].ToString());
                            myRec.Datafields.Add(Df);
                        }

                        exRecs.Add(myRec);

                        #region KIPOS TABLES

                        myRec = new CRecord();
                        myRec.set_LeaderValueByPos("s", 6, 6);
                        Cf = new CControlfield();
                        Cf.Tag = "001";
                        Cf.Value = "CAT_DMD_CATEGORY";
                        myRec.Controlfields.Add(Cf);

                        Df = new CDatafield();
                        Df.Tag = "245";
                        Df.SubfieldsText = "$aCAT_DMD_CATEGORY";
                        myRec.Datafields.Add(Df);

                        tbData = LegoWebAdmin.BusLogic.Menus.get_LEGOWEB_MENUS().Tables[0];

                        foreach (DataRow row in tbData.Rows)
                        {
                            //a MENU_ID	int	Unchecked
                            //b PARENT_MENU_ID	int	Unchecked
                            //c MENU_TYPE_ID	int	Unchecked
                            //d MENU_VI_TITLE	nvarchar(50)	Checked
                            //e MENU_EN_TITLE	nvarchar(50)	Checked
                            //f MENU_IMAGE_URL	nvarchar(250)	Checked
                            //g MENU_LINK_URL	nvarchar(50)	Checked
                            //h BROWSER_NAVIGATE	tinyint	Unchecked
                            //i IS_PUBLIC	bit	Unchecked
                            //j ORDER_NUMBER smallint
                            Df = new CDatafield();
                            Df.Tag = "650";
                            Df.SubfieldsText = String.Format("$a{0}$b{1}$c{2}$d{3}$e{4}$f{5}$g{6}$h{7}$i{8}$j{9}", row["MENU_ID"].ToString(), row["PARENT_MENU_ID"].ToString(), row["MENU_TYPE_ID"].ToString(), row["MENU_VI_TITLE"].ToString(), row["MENU_EN_TITLE"].ToString(), row["MENU_IMAGE_URL"].ToString(), row["MENU_LINK_URL"].ToString(), row["BROWSER_NAVIGATE"].ToString(), row["IS_PUBLIC"].ToString(), row["ORDER_NUMBER"].ToString());
                            myRec.Datafields.Add(Df);
                        }

                        exRecs.Add(myRec);







                        #endregion KIPOS TABLES

                        break;
                }

                if (exRecs.Count<=0)
                {
                    throw new Exception("No data to export!");
                }
                Response.ContentType = "APPLICATION/OCTET-STREAM";
                //set the filename
                Response.AddHeader("Content-Disposition", "attachment;filename=\"legowebContent.xml\"");
                String outStream = exRecs.OuterXml;
                Response.Write(outStream);
                Response.End();
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
    protected void linkCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ControlPanel.aspx");
    }

    protected void radioFilterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (radioFilterType.SelectedValue)
        { 
            case "0":
                divFilterByCat.Visible = true;
                divFilterById.Visible = false;
                btnShowResults.Visible = true;
                metaContentRepeater.Visible = true;
                break;
            case "1":
                divFilterByCat.Visible = false;
                divFilterById.Visible = true;
                btnShowResults.Visible = true;
                metaContentRepeater.Visible = true;
                break;
            case "2":
                divFilterByCat.Visible = false;
                divFilterById.Visible = false;
                btnShowResults.Visible = false;
                metaContentRepeater.Visible = false;                
                break;
        }
    }
    protected void btnShowResults_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable data=new DataTable();
            switch (radioFilterType.SelectedValue)
            {
                case "0":
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
                    data= LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_CATEGORY_ID(iCategoryID, iSectionID).Tables[0];
                    metaContentRepeater.DataSource = data;
                    metaContentRepeater.DataBind();
                    break;

                case "1":
                    int iFromID = String.IsNullOrEmpty(txtFromId.Text) ? 0 : int.Parse(txtFromId.Text);
                    int iToID = String.IsNullOrEmpty(txtToId.Text) ? 0 : int.Parse(txtToId.Text);
                    data = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_BY_ID(iFromID, iToID).Tables[0];
                    metaContentRepeater.DataSource = data;
                    metaContentRepeater.DataBind();
                break;

            }
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
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }

}
