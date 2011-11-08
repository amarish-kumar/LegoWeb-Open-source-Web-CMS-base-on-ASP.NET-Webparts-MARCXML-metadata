<%@ Page Title="" Language="C#" MasterPageFile="~/LegoWebAdmin.master" AutoEventWireup="true" CodeFile="ForumManager.aspx.cs" Inherits="ForumManager" %>

<%@ Register src="UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="UserControls/ForumManager.ascx" tagname="ForumManager" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../AdminTools/JavaScripts/mootools.js" type="text/javascript"></script>
    <script src="../AdminTools/JavaScripts/index.js" type="text/javascript"></script>
    <script src="../AdminTools/JavaScripts/menu.js" type="text/javascript"></script>
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

<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.hiendai.com.vn/kipos', 'Trợ giúp', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Help">
</span>
<%=Resources.strings.btnHelp_Text %>
</a>
</td>

</tr></table>
</div>

                <div class='header icon-48-forum'>
                    <%=Resources.strings.ForumManager_Text %>
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
                            
                            <uc3:ForumManager ID="ForumManager1" runat="server" />
									
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

