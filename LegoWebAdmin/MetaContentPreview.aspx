<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MetaContentPreview.aspx.cs" Inherits="LegoWebAdmin_MetaContentPreview" Title="Content Preview" %>
<%@ Register src="~/UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="~/UserControls/MetaContentPreview.ascx" tagname="MetaContentPreview" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="AdminTools/JavaScripts/mootools.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/index.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/menu.js" type="text/javascript"></script>
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


<td class="button" id="toolbar-delete">
<asp:LinkButton ID="linkDeleteButton" class="toolbar" runat="server" 
        onclick="linkDeleteButton_Click" OnClientClick="return confirm('Are you sure to remove this item?')">
<span class="icon-32-trash" title="Delete">
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
 
<td class="button" id="toolbar-cancel">
<asp:LinkButton ID="linkCancelButton" class="toolbar" runat="server" 
        onclick="linkCancelButton_Click">
        <span class="icon-32-cancel" title="Cancel">
</span>
<%=Resources.strings.Cancel_Text %>
</asp:LinkButton>
</td>

<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.legoweb.org/help', 'Help', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Help">
</span>
<%=Resources.strings.Help_Text %>
</a>
</td>

</tr></table>
</div>
<div class="header icon-48-article">
<%=Resources.strings.ContentPreview_Text %>
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
	    <dl id="system-message">
        <dd class="message fade">
            <ul>
                <li><%=Resources.strings.ContentPreviewNotice_Text %> </li>
            </ul>
        </dd>
        </dl>

	  
		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">                                                                                                                                 
                            
                            <uc3:MetaContentPreview ID="MetaContentPreview1" runat="server" />
                                                                                   																									
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

