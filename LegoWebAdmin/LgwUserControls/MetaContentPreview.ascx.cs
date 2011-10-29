using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.IO;
using LegoWebAdmin.DataProvider;
using MarcXmlParserEx;
using LegoWebAdmin.Controls;

public partial class LgwUserControls_MetaContentPreview : System.Web.UI.UserControl
{
    ContentEditorDataHelper _MetaContentObject;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                load_TemplateNames();
                if (CommonUtility.GetInitialValue("meta_content_id", null) != null)
                {
                    int iMetaContentId = Int32.Parse(CommonUtility.GetInitialValue("meta_content_id", null).ToString());
                    _MetaContentObject = new ContentEditorDataHelper();
                    string sXmlData =LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_MARCXML(iMetaContentId,1);
                    _MetaContentObject.load_Xml(sXmlData);
                    dpTemplateNames.SelectedValue = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(_MetaContentObject.CategoryID);
                    Session["METADATA"] = _MetaContentObject.OuterXml;
                    preview_MetaContent();
                }
                else if (Session["METADATA"] != null)
                {
                    _MetaContentObject = new ContentEditorDataHelper();
                    _MetaContentObject.load_Xml(Session["METADATA"].ToString());
                    dpTemplateNames.SelectedValue = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_TEMPLATE_NAME(_MetaContentObject.CategoryID);
                    preview_MetaContent();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void preview_MetaContent()
    {        
        if (Session["METADATA"] == null) throw new Exception("Session['METADATA']==null in preview_MetaContent()");
        _MetaContentObject = new ContentEditorDataHelper();
        _MetaContentObject.load_Xml(Session["METADATA"].ToString());

        string sTemplateName = LegoWebAdmin.DataProvider.FileTemplateDataProvider.get_XsltTemplateFile(dpTemplateNames.SelectedValue);
        this.divPreviewer.InnerHtml=_MetaContentObject.XsltFile_Transform(sTemplateName);
    }
    public void Delete_PreviewRecord()
    {
        if (Session["METADATA"] == null) throw new Exception("Session['METADATA']==null in preview_MetaContent()");
        _MetaContentObject = new ContentEditorDataHelper();
        _MetaContentObject.load_Xml(Session["METADATA"].ToString());

        if (_MetaContentObject.MetaContentID > 0)
        {
            LegoWebAdmin.BusLogic.MetaContents.movetrash_META_CONTENTS(_MetaContentObject.MetaContentID);
        }
        Session["METADATA"] = null;
        Response.Redirect("MetaContentManager.aspx");
    }
    protected void dpTemplateNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        preview_MetaContent();
    }
    private void load_TemplateNames()
    {
        this.dpTemplateNames.Items.Clear();
        string TemplatesDir = Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "/";
        DirectoryInfo di = new DirectoryInfo(TemplatesDir);
        FileInfo[] rgFiles = di.GetFiles("*.xsl");
        foreach (FileInfo fi in rgFiles)
        {
            this.dpTemplateNames.Items.Add(new ListItem(fi.Name.Substring(0, fi.Name.LastIndexOf(".")), fi.Name.Substring(0, fi.Name.LastIndexOf("."))));
        }
        this.dpTemplateNames.SelectedValue = "default";
    }
}
