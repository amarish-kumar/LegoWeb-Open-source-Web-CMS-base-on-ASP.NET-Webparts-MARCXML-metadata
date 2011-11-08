<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="SectionAddUpdate.aspx.cs" Inherits="Administrator_SectionAddUpdate" Title="Add/update section" %>
<%@ Register src="~/UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="~/UserControls/SectionAddUpdate.ascx" tagname="SectionAddUpdate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div id="header-box">
        <%--menu bar go here--%>
		<uc2:AdminMenuBarDeactive ID="AdminMenuBarDeactive1" runat="server" />
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
<td class="button" id="toolbar-save">
<asp:LinkButton ID="linkSaveButton" class="toolbar" runat="server" 
        onclick="linkSaveButton_Click" ValidationGroup="SectionInfo" CausesValidation="true">
<span class="icon-32-save" title="Save">
</span>
<%=Resources.strings.btnSave_Text %>
</asp:LinkButton>
</td>
 

<td class="button" id="toolbar-cancel">
<asp:LinkButton ID="linkCancelButton" class="toolbar" runat="server" 
        onclick="linkCancelButton_Click" ValidationGroup="SectionInfo" CausesValidation="false">
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
<div class="header icon-48-sections">
<%=Resources.strings.AddUpdateSection_Text %>
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
                

                            <uc3:SectionAddUpdate ID="SectionAddUpdate1" runat="server" />

									
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

