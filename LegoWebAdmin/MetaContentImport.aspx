<%@ Page Title="Metadata imports" Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MetaContentImport.aspx.cs" Inherits="MetaContentImport" %>

<%@ Register src="~/UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
    	<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
    <%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="AdminTools/JavaScripts/mootools.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/index.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/menu.js" type="text/javascript"></script>
	<script type="text/javascript" src="AdminTools/ckfinder/ckfinder.js"></script>
	<script type="text/javascript">
	
    function BrowseServer()
    {
	    // You can use the "CKFinder" class to render CKFinder in a page:
		    var finder = new CKFinder() ;
	        finder.BasePath = 'AdminTools/ckfinder/' ;	// The path for the installation of CKFinder (default = "/ckfinder/").	
	        finder.SelectFunction = SetFileField ;
	        finder.Popup() ;	
    	
	    // It can also be done in a single line, calling the "static"
	    // Popup( basePath, width, height, selectFunction ) function:
	    // CKFinder.Popup( '../../', null, null, SetFileField ) ;
	    //
	    // The "Popup" function can also accept an object as the only argument.
	    // CKFinder.Popup( { BasePath : '../../', SelectFunction : SetFileField } ) ;
    }

    // This is a sample function which is called when a file is selected in CKFinder.
    function SetFileField(fileUrl) {

        var txtFile = '<%=txtFileName.ClientID%>';  
        document.getElementById(txtFile).value = fileUrl;
    }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="content-box">
	<div id="header-box">
        <%--menu bar go here--%>
		<uc1:AdminMenuBarActive ID="AdminMenuBarActive1" runat="server" />
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

<td class="button" id="toolbar-import">
<asp:LinkButton ID="linkImportContentButton" class="toolbar" runat="server" 
        onclick="linkImportContentButton_Click">
<span class="icon-32-save" title="Imports metadata">
</span>
<%=Resources.strings.Imports_Text %>
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
				<div class="header icon-48-install">
<%=Resources.strings.MetadataImports_Text %>
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
			
			    <fieldset class="adminform">
		         <legend><%=Resources.strings.Source_Text %></legend>											
                <table cellpadding="2" cellspacing="2" width="800px" border="0">
                <tbody>
                <tr>
                <td align="right" valign="middle" style="width:150px"><b><%=Resources.strings.MetadataFile_Text%>:</b></td>
                <td valign="middle" style="width:305px">
                <asp:TextBox ID="txtFileName" runat="server" Width="450px"></asp:TextBox>                     
                </td>
                <td valign="middle" align="left">
                    <asp:Button ID="btnBrowse" OnClientClick="BrowseServer();" Text="Browse" 
                        runat="server" onclick="btnBrowse_Click" />
                    <asp:Button ID="btnAnalyse" Text="Analyse" runat="server" 
                        onclick="btnAnalyse_Click" />
                </td>
                </tr>
                </tbody>
                </table>
                

                </fieldset>
                
                <div id="divDefaultCategory" runat="server" visible="false">
                <fieldset class="adminform">
		         <legend><%=Resources.strings.ImportParams_Text %>:</legend>		         
		        
		                <table cellpadding="2" cellspacing="2" width="700px" border="0">
		                <tbody >
		                <tr>		                
                        <td align="right" valign="middle" style="width:150px"><b><%=Resources.strings.DefaultSection_Text %>:</b></td>
                        <td align="left" valign="middle" style="width:200px"><asp:dropdownlist ID="dropSections" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="dropSections_SelectedIndexChanged"></asp:dropdownlist></td>
                        <td align="right" valign="middle" style="width:150px"><b> <%=Resources.strings.DefaultCategory_Text %>:</b></td>
                        <td align="left" valign="middle">
                            <asp:dropdownlist ID="dropCategories" runat="server" AutoPostBack="true"></asp:dropdownlist></td>
                        </tr>
                        <tr>
                        <td align="right" valign="middle" style="width:150px">
                        <b><%=Resources.strings.ImportTypes_Text %>:</b>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="radioImportTypes" runat="server" 
                                RepeatDirection="Horizontal">
		                    </asp:RadioButtonList>     
		                 </td>                      
                        </tr>
                        <tr>
                        <td align="right" valign="middle" style="width:150px">
                        <b></b>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="radioForceToDefaultCategory" runat="server" RepeatDirection="Horizontal">
		                    </asp:RadioButtonList>     
		                 </td>                      
                        </tr>
                        </tbody>
                        </table>
                 		        		       		         
		         </fieldset>
		         </div>
                
                       <table class="adminlist" cellspacing="1">  
                        <asp:repeater id="metaContentRepeater" runat="server">
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th class="title"><%=Resources.strings.ID_Text %></th>							
							<th class="title"><%=Resources.strings.Title_Text %></th>
							<th class="title"><%=Resources.strings.Language_Text %></th>
							<th class="title"><%=Resources.strings.Category_Text %></th>
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                            
                                <td align="center">                                
                                <%#Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                                                                               
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_ID")%>                                
                                </td>                                                                                                                                                                     
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                                                                               
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_ID")%>                                
                                </td>                                                                                                                                                                     
                            </tr>                            
							</AlternatingItemTemplate>
						</asp:repeater>      
					</table>                                                                        									
                                                							
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
