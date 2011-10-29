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

public partial class LgwUserControls_MetaContentTrash : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                metaContentTrashBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void metaContentTrashBind()
    {
        try
        {
            metaContentTrashrRepeater.DataSource = LegoWebAdmin.BusLogic.MetaContents.get_Trash_META_CONTENTS();
            metaContentTrashrRepeater.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        for (int i = 0; i < this.metaContentTrashrRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentTrashrRepeater.Items[i].FindControl("chkSelect"));
            cbRow.Checked = cb.Checked;
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbHeader = ((CheckBox)metaContentTrashrRepeater.Controls[0].FindControl("chkSelectAll"));
        if (cbHeader != null)
        {
            cbHeader.Checked = false;
        }
    }
    public void delete_SelectedContents()
    {        
        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentTrashrRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentTrashrRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentTrashrRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                        throw new Exception("You are not authorized to delete this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.delete_META_CONTENTS(int.Parse(txtMetaContentId.Text));
                }
            }
        }
        metaContentTrashBind();
    }
    public void restore_SelectedContents()
    {       
        int iMetaContentId = 0;
        for (int i = 0; i < this.metaContentTrashrRepeater.Items.Count; i++)
        {
            CheckBox cbRow = ((CheckBox)metaContentTrashrRepeater.Items[i].FindControl("chkSelect"));
            if (cbRow.Checked == true)
            {
                TextBox txtMetaContentId = (TextBox)metaContentTrashrRepeater.Items[i].FindControl("txtMetaContentId");
                if (txtMetaContentId != null)
                {
                    iMetaContentId = int.Parse(txtMetaContentId.Text);
                    int iCategoryId = LegoWebAdmin.BusLogic.MetaContents.get_META_CONTENT_CATEGORY_ID(iMetaContentId);
                    if (!is_UserCanUpdateContent(iCategoryId))
                    {
                         throw new Exception("You are not authorized to restore this content!");
                    }
                    LegoWebAdmin.BusLogic.MetaContents.restore_META_CONTENTS(int.Parse(txtMetaContentId.Text));
                }
            }
        }
        metaContentTrashBind();
    }

    public static bool is_UserCanUpdateContent(int iCATEGORY_ID)
    {
        bool isUserCan = true;
        DataTable catTable = LegoWebAdmin.BusLogic.Categories.get_CATEGORY_BY_ID(iCATEGORY_ID).Tables[0];
        int iAdminLevel = int.Parse(catTable.Rows[0]["ADMIN_LEVEL"].ToString());
        if (iAdminLevel > 0)
        {
            string sAdminRoles = catTable.Rows[0]["ADMIN_ROLES"].ToString();
            if (!String.IsNullOrEmpty(sAdminRoles))
            {
                string[] allowRoles = sAdminRoles.Split(new char[] { ',', ';' });
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
