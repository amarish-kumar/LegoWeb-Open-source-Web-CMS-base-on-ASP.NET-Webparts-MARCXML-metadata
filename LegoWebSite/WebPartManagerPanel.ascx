<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebPartManagerPanel.ascx.cs" Inherits="WebPartManagerPanel" %>
<asp:WebPartManager ID="WebPartManagerMain" runat="server">
</asp:WebPartManager>

<div id="divWPManagerPanel" runat="server">
<table class="Toolbar" border="0" cellpadding="0" cellspacing="5" width="100%">	
	<tr>
		<td align="right">
		  <asp:Label runat="server" ID="_browseViewLabel">
			<asp:LinkButton ID="cmdBrowseView" runat="server" OnClick="cmdBrowseView_Click" CssClass="Menu" Font-Size="7pt" ForeColor="White" ><%=Resources.strings.BrowseView%></asp:LinkButton>&nbsp;|&nbsp;
		  </asp:Label>	
		  <asp:Label runat="server" ID="_designViewLabel">
			<asp:LinkButton ID="cmdDesignView" runat="server" OnClick="cmdDesignView_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.DesignView%></asp:LinkButton>&nbsp;|&nbsp;
      </asp:Label>
		  <asp:Label runat="server" ID="_editViewLabel">
			<asp:LinkButton ID="cmdEditView" runat="server" OnClick="cmdEditView_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.EditView%></asp:LinkButton>&nbsp;|&nbsp;
      </asp:Label>
		  <asp:Label runat="server" ID="_catalogViewLabel">
			<asp:LinkButton ID="cmdCatalogView" runat="server" OnClick="cmdCatalogView_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.CatalogView%></asp:LinkButton>&nbsp;|&nbsp;
      </asp:Label>
	  <asp:Label runat="server" ID="_connectViewLabel">
			<asp:LinkButton ID="cmdConnectView" runat="server" OnClick="cmdConnectView_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.ConnectView%></asp:LinkButton>&nbsp;|&nbsp;
      </asp:Label>
	  <asp:Label runat="server" ID="_personalizationModeToggleLabel">
			<asp:LinkButton ID="cmdPersonalizationModeToggle" runat="server" OnClick="cmdPersonalizationModeToggle_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.PersonalizationModeToggle%></asp:LinkButton>&nbsp;&nbsp;
      </asp:Label>
<%--	  <asp:Label runat="server" ID="_resetPersonlizationState">
			<asp:LinkButton ID="cmdResetPersonalizationState" runat="server" OnClick="cmdResetPersonalizationState_Click" Font-Size="7pt" ForeColor="White"><%=Resources.strings.ResetPersonalizationState%></asp:LinkButton>&nbsp;
      </asp:Label>            
      qua nguy hiem neu de nut nay
--%>		
    </td>
  </tr>
  <tr>
		<td align="right" >	
            <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="<%$Resources:strings,Login%>" 
                LogoutText="<%$Resources:strings,LogOut%>" />&nbsp;&nbsp;
            <%=Resources.strings.Mode%>="<b><%= WebPartManagerMain.DisplayMode.Name%></b>"&nbsp;&nbsp;
			<%=Resources.strings.Scope%>="<b><%= WebPartManagerMain.Personalization.Scope.ToString()%></b>"
			
	  </td>
  </tr>

</table>
</div>