﻿<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MenuManager.aspx.cs" Inherits="LegoWebAdmin_MenuManager" Title="Menu details manager" %>
<%@ Register src="~/UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="~/UserControls/MenuManager.ascx" tagname="MenuManager" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="AdminTools/JavaScripts/mootools.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/index.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/menu.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div id="header-box">
        <!--menu bar go here -->
        <uc1:AdminMenuBarActive ID="AdminMenuBarActive1" runat="server" />
		<div class="clr"></div>
	</div>
	

	   		<div class="clr"></div>
	   		
<div id="content-box">		
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

<td class="button" id="toolbar-publish">
<asp:LinkButton ID="linkPublishButton" class="toolbar" runat="server" 
        onclick="linkPublishButton_Click">
<span class="icon-32-publish" title="Publish">
</span>
<%=Resources.strings.Publish_Text %>
</asp:LinkButton>
</td>
 
<td class="button" id="toolbar-unpublish">
<asp:LinkButton ID="linkUnPublishButton" class="toolbar" runat="server" 
        onclick="linkUnPublishButton_Click">
<span class="icon-32-unpublish" title="Unpublish">
</span>
<%=Resources.strings.UnPublish_Text %>
</asp:LinkButton>
</td>


<td class="button" id="toolbar-delete">
<asp:LinkButton ID="linkDeleteButton" class="toolbar" runat="server" 
        onclick="linkDeleteButton_Click" OnClientClick="return confirm('Bạn thực sự muốn xóa các tài khoản được chọn?')">
<span class="icon-32-delete" title="Delete">
</span>
<%=Resources.strings.Delete_Text %>
</asp:LinkButton>
</td>

<td class="button" id="toolbar-edit">
<asp:LinkButton ID="linkEditButton" class="toolbar" runat="server" 
        onclick="linkEditButton_Click">
<span class="icon-32-edit" title="Edit">
</span>
<%=Resources.strings.Edit_Text %>
</asp:LinkButton>
</td>

<td class="button" id="toolbar-new">
<asp:LinkButton ID="linkNewButton" class="toolbar" runat="server" 
        onclick="linkNewButton_Click">
<span class="icon-32-new" title="New">
</span>
<%=Resources.strings.Add_Text %>
</asp:LinkButton>
</td>

<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.legoweb.org/help', 'Help', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Trợ giúp">
</span>
<%=Resources.strings.Help_Text %>
</a>
</td>

</tr></table>
</div>
<div class="header icon-48-menu">
<asp:Literal ID="litMenuTypeName" runat="server"></asp:Literal>
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
	  
		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">                                             
                            
                            <uc3:MenuManager ID="MenuManager1" runat="server" />
									
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

