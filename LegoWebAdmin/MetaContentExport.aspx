<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MetaContentExport.aspx.cs" Inherits="MetaContentExport" Title="Metadata Exports" %>

<%@ Register src="~/UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="~/UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="AdminTools/JavaScripts/mootools.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/index.js" type="text/javascript"></script>
    <script src="AdminTools/JavaScripts/menu.js" type="text/javascript"></script>
    <script type = "text/javascript" language = "javascript"> 

    function ValidateNumerTextBox(i) 
    {
        if(i.value.length>0) 
        {
        i.value = i.value.replace(/[^\d]+/g, ''); 
        }
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

<td class="button" id="toolbar-export">
<asp:LinkButton ID="linkExportButton" class="toolbar" runat="server" 
        onclick="linkExportButton_Click">
<span class="icon-32-export" title="Export metadata">
</span>
<%=Resources.strings.Exports_Text %>
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
<span class="icon-32-help" title="Trợ giúp">
</span>
<%=Resources.strings.Help_Text %>
</a>
</td>

</tr></table>
</div>
				<div class="header icon-48-archive">
<%=Resources.strings.MetadataExports_Text %>
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
		                <legend><%=Resources.strings.FilterCriteria_Text%>:</legend>		                
		                <table cellpadding="2" cellspacing="2" width="600px" border="0">
                        <tbody>
                        <tr>
                        <td align="right" valign="middle" style="width:100px"><b> <%=Resources.strings.FilterType_Text%>:</b></td>
                        <td>
		                    <asp:RadioButtonList ID="radioFilterType" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="radioFilterType_SelectedIndexChanged">
		                    </asp:RadioButtonList>                        
                        </td>
		                </tr>
		                </tbody>
		                </table>
		                <div id="divFilterByCat" runat="server" visible="true">
		                <table cellpadding="2" cellspacing="2" width="600px" border="0">
		                <tbody >
		                <tr>		                
                        <td align="right" valign="middle" style="width:100px"><b><%=Resources.strings.Section_Text %>:</b></td>
                        <td align="left" valign="middle" style="width:200px"><asp:dropdownlist ID="dropSections" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="dropSections_SelectedIndexChanged"></asp:dropdownlist></td>
                        <td align="right" valign="middle" style="width:100px"><b> <%=Resources.strings.Category_Text %>:</b></td>
                        <td align="left" valign="middle">
                            <asp:dropdownlist ID="dropCategories" runat="server" AutoPostBack="true" ></asp:dropdownlist></td>
                        </tr>
                        </tbody>
                        </table>
                        </div>

   		                <div id="divFilterById" runat="server" visible="false">
		                <table cellpadding="2" cellspacing="2" width="600px" border="0">
		                <tbody>
		                <tr>		                
                        <td align="right" valign="middle" style="width:100px"><b><%=Resources.strings.FromID_Text %>:</b></td>
                        <td align="left" valign="middle" style="width:150px">
                        <asp:TextBox ID="txtFromId" runat="server" style="text-align:right;" ></asp:TextBox>
                        </td>
                        <td align="right" valign="middle" style="width:100px"><b><%=Resources.strings.ToID_Text %>:</b></td>
                        <td align="left" valign="middle">
                        <asp:TextBox ID="txtToId" runat="server" style="text-align:right;" ></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromId"
                        ErrorMessage="Only number accepted" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>                        
                        </td>
                        <td colspan="2">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtToId"
                        ErrorMessage="Only number accepted" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>                        
                        </td>
                        </tr>
                        </tbody>
                        </table>
                        </div>

		                <table cellpadding="2" cellspacing="2" width="600px" border="0">
		                <tbody>
		                <tr>		                
                        <td  colspan="4" align="right" valign="middle"><asp:Button ID="btnShowResults" 
                                runat="server" Text="Show results" onclick="btnShowResults_Click" /></td>
                        </tr>
                        </tbody>
                        </table>                        
                    </fieldset>                                                            
                    
                    <table class="adminlist" cellspacing="1">  
                        <asp:repeater id="metaContentRepeater" runat="server">
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th class="title">ID</th>							
							<th class="title"><%=Resources.strings.Title_Text %></th>
							<th class="title"><%=Resources.strings.Language_Text %></th>
							<th class="title"><%=Resources.strings.IsPublic_Text %></th>
							<th class="title"><%=Resources.strings.Category_Text %></th>
							<th class="title"><%=Resources.strings.UserName_Text %></th>
							<th class="title"><%=Resources.strings.Update_Text%></th>	
							<th class="title"><%=Resources.strings.Hit_Text %></th>							
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
                                <a href="MetaContentEditor.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                 
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "ORDER_NUMBER")%>                                
                                </td>                                                                
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_VI_TITLE")%>                                
                                </td>                                                                                                                                                                     
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "MODIFIED_USER")%>                                
                                </td>                                                                   
                                <td align="center">                                
                                <%# ((DateTime)DataBinder.Eval(Container.DataItem, "MODIFIED_DATE")).ToString("dd.MM.yy")%>                                
                                </td>   
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
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
                                <a href="MetaContentEditor.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                 
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "ORDER_NUMBER")%>                                
                                </td>                                                                
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_VI_TITLE")%>                                
                                </td>                                                                                                                                                                     
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "MODIFIED_USER")%>                                
                                </td>                                                                   
                                <td align="center">                                
                                <%# ((DateTime)DataBinder.Eval(Container.DataItem, "MODIFIED_DATE")).ToString("dd.MM.yy")%>                                
                                </td>   
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
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
