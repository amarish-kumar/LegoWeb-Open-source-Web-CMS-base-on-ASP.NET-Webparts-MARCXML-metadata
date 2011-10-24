<%@ Page Title="" Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MetaContentImport.aspx.cs" Inherits="MetaContentImport" %>

<%@ Register src="UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
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
    function SetFileField(fileUrl) 
    {
        document.getElementById('<%=txtFileName.ClientID%>').value = fileUrl;
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
<span class="icon-32-save" title="Nhập khẩu dữ liệu">
</span>
Nhập khẩu
</asp:LinkButton>
</td>
 
<td class="button" id="toolbar-cancel">
<asp:LinkButton ID="linkCancelButton" class="toolbar" runat="server" 
        onclick="linkCancelButton_Click">
        <span class="icon-32-cancel" title="Cancel">
</span>
Bỏ qua
</asp:LinkButton>
</td>

<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.legoweb.org/help', 'Help', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Trợ giúp">
</span>
Trợ giúp
</a>
</td>

</tr></table>
</div>
				<div class="header icon-48-install">
Nhập khẩu dữ liệu web
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
			
			    <fieldset class="adminform">
		         <legend>Nhập khẩu dữ liệu web:</legend>											
                <table cellpadding="2" cellspacing="2" width="650px" border="0">
                <tbody>
                <tr>
                <td align="right" valign="middle" style="width:100px"><b>Tệp tin(*.xml):</b></td>
                <td valign="middle" style="width:305px">
                <asp:TextBox ID="txtFileName" runat="server" Width="350px"></asp:TextBox>                     
                </td>
                <td valign="middle" align="left">
                    <asp:Button ID="btnFileBrowse" OnClientClick="BrowseServer();" Text="Chọn tệp" 
                        runat="server" onclick="btnFileBrowse_Click" />
                    <asp:Button ID="btnAnalyzeData" Text="Phân tích" runat="server" 
                        onclick="btnAnalyzeData_Click" />
                </td>
                </tr>
                <tr>
                <td>
                </td>
                <td>
                    <asp:Literal ID="litErrorMessage" runat="server" Visible="false"></asp:Literal>
                </td>
                </tr>
                </tbody>
                </table>
                

                </fieldset>
                
                <div id="divDefaultCategory" runat="server" visible="false">
                <fieldset class="adminform">
		         <legend>Tham số nhập khẩu:</legend>		         
		        
		                <table cellpadding="2" cellspacing="2" width="700px" border="0">
		                <tbody >
		                <tr>		                
                        <td align="right" valign="middle" style="width:150px"><b> Vùng tin mặc định:</b></td>
                        <td align="left" valign="middle" style="width:200px"><asp:dropdownlist ID="dropSections" runat="server" oninit="dropSections_Init" AutoPostBack="true" OnSelectedIndexChanged="dropSections_SelectedIndexChanged"></asp:dropdownlist></td>
                        <td align="right" valign="middle" style="width:150px"><b> Chuyên mục mặc định:</b></td>
                        <td align="left" valign="middle">
                            <asp:dropdownlist ID="dropCategories" runat="server" AutoPostBack="true" oninit="dropCategories_Init"></asp:dropdownlist></td>
                        </tr>
                        <tr>
                        <td align="right" valign="middle" style="width:150px">
                        <b>Kiểu nhập khẩu:</b>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="radioImportOptions" runat="server" 
                                RepeatDirection="Horizontal">
		                    <asp:ListItem Value="0" Text="Thêm mới (mã số mới)" Selected="True"></asp:ListItem>
		                    <asp:ListItem Value="1" Text="Bỏ qua nếu trùng mã số" Selected="False"></asp:ListItem>
		                    <asp:ListItem Value="2" Text="Ghi đè nếu trùng mã số" Selected="False"></asp:ListItem>
		                    </asp:RadioButtonList>     
		                 </td>                      
                        </tr>
                        <tr>
                        <td align="right" valign="middle" style="width:150px">
                        <b></b>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="radioForceToDefaultCategory" runat="server" RepeatDirection="Horizontal">
		                    <asp:ListItem Value="0" Text="Tự động vào Chuyên mục " Selected="True"></asp:ListItem>
		                    <asp:ListItem Value="1" Text="Bắt buộc vào Chuyên mục mặc định" Selected="False"></asp:ListItem>
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
							<th class="title">ID</th>							
							<th class="title">Tiêu đề</th>
							<th class="title">Ngôn ngữ</th>
							<th class="title">Chuyên mục</th>
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
