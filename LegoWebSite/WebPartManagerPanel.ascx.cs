using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebPartManagerPanel : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		// Based on WebPartManager's capabilities, show / hide options for 
		// switching display modes of the page.
		//
        if (Page.User.Identity.IsAuthenticated && Roles.IsUserInRole(Page.User.Identity.Name, "WEBMASTERS"))
        {
            this.divWPManagerPanel.Visible = true;
            _browseViewLabel.Visible =
                WebPartManagerMain.SupportedDisplayModes.Contains(WebPartManager.BrowseDisplayMode);
            _catalogViewLabel.Visible =
                WebPartManagerMain.SupportedDisplayModes.Contains(WebPartManager.CatalogDisplayMode);
            _connectViewLabel.Visible =
                WebPartManagerMain.SupportedDisplayModes.Contains(WebPartManager.ConnectDisplayMode);
            _designViewLabel.Visible =
                WebPartManagerMain.SupportedDisplayModes.Contains(WebPartManager.DesignDisplayMode);
            _editViewLabel.Visible =
                WebPartManagerMain.SupportedDisplayModes.Contains(WebPartManager.EditDisplayMode);
            _personalizationModeToggleLabel.Visible =
                WebPartManagerMain.Personalization.CanEnterSharedScope;
            //_resetPersonlizationState.Visible = WebPartManagerMain.Personalization.HasPersonalizationState;
        }
        else
        {
            this.divWPManagerPanel.Visible = false;
        }
	}
	protected void cmdBrowseView_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.DisplayMode = WebPartManager.BrowseDisplayMode;

	}
	protected void cmdDesignView_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.DisplayMode = WebPartManager.DesignDisplayMode;

	}

	protected void cmdEditView_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.DisplayMode = WebPartManager.EditDisplayMode;

	}
	protected void cmdConnectView_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.DisplayMode = WebPartManager.ConnectDisplayMode;

	}

	protected void cmdCatalogView_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.DisplayMode = WebPartManager.CatalogDisplayMode;

	}
	protected void cmdPersonalizationModeToggle_Click(object sender, EventArgs e)
	{
		WebPartManagerMain.Personalization.ToggleScope();
	}
    protected void cmdResetPersonalizationState_Click(object sender, EventArgs e)
    {
        WebPartManagerMain.Personalization.ResetPersonalizationState();
    }
}