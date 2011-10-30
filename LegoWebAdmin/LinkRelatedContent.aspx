<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="LinkRelatedContent.aspx.cs" Inherits="LegoWebAdmin_LinkRelatedContent" Title="Link related contents" %>
<%@ Register src="LgwUserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="LgwUserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="LgwUserControls/LinkRelatedContents.ascx" tagname="LinkRelatedContents" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="content-box">
	<div id="header-box">
        <%--menu bar go here--%>
		<uc2:AdminMenuBarDeactive ID="AdminMenuBarDeactive1" runat="server" />
		<div class="clr"></div>
	</div>
	

	   		<div class="clr"></div>
		
	<div class="border">
	  <div class="padding">		
	  
<div id="toolbar-box">
   			<div class="t">
				<div class="t">
					<div class="t"></div>
				</div>
			</div>
			<div class="m">
				<div class="toolbar" id="toolbar">
<table class="toolbar"><tr>

<td class="button" id="toolbar-apply">
<asp:LinkButton ID="linkTakeRelatedContent" class="toolbar" runat="server" 
        onclick="linkTakeRelatedContent_Click">
<span class="icon-32-apply" title="Link">
</span>
<%=Resources.strings.btnLink_Text %>
</asp:LinkButton>
</td>
 
<td class="button" id="toolbar-cancel">
<asp:LinkButton ID="linkCancelButton" class="toolbar" runat="server" 
        onclick="linkCancelButton_Click">
        <span class="icon-32-cancel" title="Cancel">
</span>
<%=Resources.strings.btnCancel_Text %>
</asp:LinkButton>
</td>

<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.legoweb.org/help', 'Help', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Help">
</span>
<%=Resources.strings.btnHelp_Text %>
</a>
</td>

</tr></table>
</div>
	
	<div class="header icon-48-article">
        <%=Resources.strings.SelectRelatedContents_Text %>
    </div>

				<div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
  		</div>  		
	  
	  <div class="clr"></div>
	      <asp:Literal ID="litErrorSpaceHolder" runat="server"> </asp:Literal>
		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">
                                                                         
                            <uc3:LinkRelatedContents ID="LinkRelatedContents1" runat="server" />
                            									
			        <div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
    </div>
    </div>
		<div class="clr"></div>
	</div>

</asp:Content>

