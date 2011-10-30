<%@ Page Title="" Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="ErrorMessage.aspx.cs" Inherits="ErrorMessage" %>

<%@ Register src="LgwUserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="LgwUserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
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
				
                    <dl id='system-message'>
                    <dd class='error message fade'>
                        <ul>
                            <li> 
                            <asp:Literal ID="ltErrorMessage" runat="server"></asp:Literal>
                            </li>
                        </ul>
                    </dd>
                    </dl>

				<div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
  		</div>  		
	  
	  <div class="clr"></div>
	  		
    </div>
    </div>
		<div class="clr"></div>
	</div>
</asp:Content>

