using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class LgwUserControls_ForumManager  : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.radioIsPublic.Text = Resources.strings.Yes_Text;
        this.radioIsNotPublic.Text = Resources.strings.No_Text;
        this.btnOk.Text = Resources.strings.Ok_Text;
        this.btnCancel.Text = Resources.strings.Cancel_Text;
        this.btnDelete.Text = Resources.strings.Delete_Text;

        if (!IsPostBack)
        {
            forumManagerBind();
        }

    }
    protected void forumManagerBind()
    {
        DataTable fData = LegoWebForum.BusLogic.Forums.get_LEGOWEB_FORUMS(-1);
        forumManagerRepeater.DataSource = fData;
        forumManagerRepeater.DataBind();
    }

    protected void linkAddNew_OnClick(object sender, EventArgs e)
    {
        litErrorSpaceHolder.Text = "";
        divAddUpdateForumInfo.Visible = true;
        txtForumID.Text = "";
        txtForumID.Enabled = true;
        txtTitle.Text = "";
        txtDescription.Text = "";
        cblRoles_Init(null, null);
        txtOrderNumber.Text = "";
        ImageForumImageUrl.ImageUrl ="";
        HiddenForumImageUrl.Value ="";
        btnDelete.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        litErrorSpaceHolder.Text = "";
        divAddUpdateForumInfo.Visible = false;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {

            string sadminRoles = null;
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {                
                if (cblRoles.Items[i].Selected)
                { 
                    sadminRoles+=((sadminRoles!=null?",":"") + cblRoles.Items[i].Value);
                }
            }
            if (String.IsNullOrEmpty(txtForumID.Text) || txtForumID.Text == "0")
            {
                LegoWebForum.BusLogic.Forums.add_LEGOWEB_FORUMS(txtTitle.Text, txtDescription.Text, sadminRoles, radioIsPublic.Checked == true ? true : false, int.Parse("0" + txtOrderNumber.Text), HiddenForumImageUrl.Value);
            }
            else
            {
                LegoWebForum.BusLogic.Forums.update_LEGOWEB_FORUMS(int.Parse(txtForumID.Text), txtTitle.Text, txtDescription.Text, sadminRoles, radioIsPublic.Checked == true ? true : false, int.Parse("0" + txtOrderNumber.Text), HiddenForumImageUrl.Value);
            }
            divAddUpdateForumInfo.Visible = false;
            forumManagerBind();

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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LegoWebForum.BusLogic.Forums.delete_LEGOWEB_FORUMS(int.Parse(txtForumID.Text));
            divAddUpdateForumInfo.Visible = false;
            forumManagerBind();
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

    protected void repeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable fData = LegoWebForum.BusLogic.Forums.get_LEGOWEB_FORUMS(int.Parse(e.CommandArgument.ToString()));
            if (fData.Rows.Count > 0)
            {
                litErrorSpaceHolder.Text = "";
                divAddUpdateForumInfo.Visible = true;
                txtForumID.Text = fData.Rows[0]["ForumID"].ToString();
                txtTitle.Text = fData.Rows[0]["Title"].ToString();
                txtDescription.Text = fData.Rows[0]["Description"].ToString();
                string[] adminRoles = fData.Rows[0]["AdminRoles"].ToString().Split(new char[] { ',', ';' });
                if (adminRoles != null && adminRoles.Length > 0)
                {
                    for (int i = 0; i < adminRoles.Length; i++)
                    {
                        for (int j = 0; j < cblRoles.Items.Count; j++)
                        {
                            if (cblRoles.Items[j].Value == adminRoles[i])
                            {
                                cblRoles.Items[j].Selected = true;
                            }
                        }
                    }
                }
                radioIsPublic.Checked = (bool)fData.Rows[0]["IsPublic"];
                radioIsNotPublic.Checked = !(bool)fData.Rows[0]["IsPublic"];
                ImageForumImageUrl.ImageUrl = fData.Rows[0]["ImageURL"].ToString();
                HiddenForumImageUrl.Value = fData.Rows[0]["ImageURL"].ToString();
                btnDelete.Visible = true;
            }
        }
    }
    protected void cblRoles_Init(object sender, EventArgs e)
    {
        this.cblRoles.Items.Clear();
        string[] availableRoles = Roles.GetAllRoles();
        for (int i = 0; i < availableRoles.Length; i++)
        {
            this.cblRoles.Items.Add(new ListItem(availableRoles[i], availableRoles[i]));
        }
    }

}
