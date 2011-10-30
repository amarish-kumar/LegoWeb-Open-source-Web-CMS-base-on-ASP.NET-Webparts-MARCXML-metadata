<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaContentEditor.ascx.cs" Inherits="LgwUserControls_MetaContentEditor" %>
	<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
    <%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>

	<script type="text/javascript" src="AdminTools/ckfinder/ckfinder.js"></script>
	<script type="text/javascript">
	var inputId="";
	function On_Init()
	{
        inputId='<%=txtAddValue.ClientID%>';
    }
    function changeInputID(newId)
    {
        inputId=newId;
    }
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
	    document.getElementById(inputId).value = fileUrl ;
    }
</script>
		
		
		<asp:Literal ID="litErrorSpaceHolder" runat="server"> </asp:Literal>

		<table cellspacing="0" cellpadding="0" border="0" width="100%">  		<tr>
		<td valign="top">		 
        	<table class="adminform" cellspacing="1" width="100%" style="table-layout:fixed">  				
					<asp:repeater id="marcTextRepeater" runat="server" 
                            onitemdatabound="marcTextRepeater_ItemDataBound">
							<HeaderTemplate>
							<tr>
							    <th style="width:25px"><%=Resources.strings.Tag_Text%></th>
							    <th style="width:120px"><%=Resources.strings.Label_Text%></th>
							    <th><%=Resources.strings.Content_Text%></th>
							    <th style="width:35px;" align="center"><%=Resources.strings.Select_Text%></th>
							</tr>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">
                                <td>
                                <asp:Label ID="labelTag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAG") %>'></asp:Label>
                                <asp:Label ID="labelTagIndex" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "TAG_INDEX") %>'></asp:Label>
                                </td>                                                         
                                <td>    
                                <asp:Label ID="labelSubfieldID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_ID") %>'></asp:Label>                                                                                                                                  
                                <asp:Label ID="labelSubfieldCode" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_CODE") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldLabel" runat="server"  Font-Bold="true" ForeColor="CornflowerBlue" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_LABEL") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_TYPE") %>'></asp:Label>                                                                     
                                </td>
                                <td align="left" >                                                                                                    
                                    <asp:TextBox ID="Subfield_Value" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_VALUE")%>'></asp:TextBox>                                   
                                </td>
                                <td align="right">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="RowLevelCheckBox" />    
                                </td>
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">                           

                                <td>
                                <asp:Label ID="labelTag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAG") %>'></asp:Label>
                                <asp:Label ID="labelTagIndex" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "TAG_INDEX") %>'></asp:Label>
                                </td>                                                         
                                <td>    
                                <asp:Label ID="labelSubfieldID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_ID") %>'></asp:Label>                                                                                                                                  
                                <asp:Label ID="labelSubfieldCode" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_CODE") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldLabel" Font-Bold="true" ForeColor="CornflowerBlue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_LABEL") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_TYPE") %>'></asp:Label>                                                                     
                                </td>
                                <td align="left" >     

                                   <asp:TextBox ID="Subfield_Value" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_VALUE")%>'></asp:TextBox>                
                                
                                </td>
                                <td align="right">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="RowLevelCheckBox" />    
                                </td>

                            </tr>
							</AlternatingItemTemplate>
							<FooterTemplate>																								
							</FooterTemplate>
						</asp:repeater>
						
<!--repeater for NTEXT SubFields -->						
						
		              <asp:repeater id="marcNTextRepeater" runat="server" onitemdatabound="marcNTextRepeater_ItemDataBound">
							<ItemTemplate>
                            <tr class="row0">
                                <td>
                                <asp:Label ID="labelTag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAG") %>'></asp:Label>
                                <asp:Label ID="labelTagIndex" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "TAG_INDEX") %>'></asp:Label>
                                </td>                                                         
                                <td>    
                                <asp:Label ID="labelSubfieldID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_ID") %>'></asp:Label>                                                                                                                                  
                                <asp:Label ID="labelSubfieldCode" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_CODE") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldLabel" runat="server"  Font-Bold="true" ForeColor="CornflowerBlue" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_LABEL") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_TYPE") %>'></asp:Label>                                                                     
                                </td>
                                <td align="left" >                                                                                                    
                                    <FCKeditorV2:FCKeditor ID="NTEXT_Value" runat="server" BasePath="AdminTools/fckeditor/" ToolbarCanCollapse="true" ToolbarSet="Default" Value='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_VALUE")%>' Height="400" Width="100%">
                                    </FCKeditorV2:FCKeditor>
                                </td>
                                <td align="right">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="RowLevelCheckBox" />    
                                </td>
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">                           

                                <td>
                                <asp:Label ID="labelTag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAG") %>'></asp:Label>
                                <asp:Label ID="labelTagIndex" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "TAG_INDEX") %>'></asp:Label>
                                </td>                                                         
                                <td>    
                                <asp:Label ID="labelSubfieldID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_ID") %>'></asp:Label>                                                                                                                                  
                                <asp:Label ID="labelSubfieldCode" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_CODE") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldLabel" Font-Bold="true" ForeColor="CornflowerBlue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_LABEL") %>'></asp:Label>    
                                <asp:Label ID="labelSubfieldType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_TYPE") %>'></asp:Label>                                                                     
                                </td>
                                <td align="left" >     

                                    <FCKeditorV2:FCKeditor ID="NTEXT_Value" runat="server" BasePath="AdminTools/fckeditor/" ToolbarCanCollapse="true" ToolbarSet="Default" Value='<%# DataBinder.Eval(Container.DataItem, "SUBFIELD_VALUE")%>' Height="400" Width="100%">
                                    </FCKeditorV2:FCKeditor>                                
                                                                
                                </td>
                                <td align="right">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="RowLevelCheckBox" />    
                                </td>

                            </tr>
							</AlternatingItemTemplate>
							<FooterTemplate>																								
							</FooterTemplate>
						</asp:repeater>

						
						
						
				<tr style="background-color: #ecf7e4">							
				<td colspan="4">				
                   <table cellpadding="2" cellspacing="2" width="100%" border="0">
				    <tr>                                               
                       <td>
                         <div style="float:right">
                               <asp:DropDownList ID="listAddTag" runat="server" onselectedindexchanged="listAddTag_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;
                               <asp:DropDownList ID="listAddSubfieldCode" runat="server"></asp:DropDownList> &nbsp;    
                               <asp:TextBox ID="txtAddValue" runat="server" Width="250px"></asp:TextBox>
                         </div>
                       </td>
                   </tr>                               
                   
                    <tr>
				    <td>
				    <div style="float:right">
				    <table class="commandbar" border="0">
				    <tr>
                        <td class="button">
                            <asp:LinkButton ID="linkTakeRelatedContent" class="toolbar" runat="server" ToolTip="Link related contents" onclick="linkTakeRelatedContent_Click">
                            <span class="icon-16-info" title="Remove selected rows">
                            </span>          
                            <%=Resources.strings.btnTakeRelatedContents_Text%>
                            </asp:LinkButton>
                        </td>
                        <td class="button">
                            <a href="#" onclick="javascript:showDatePicker('myDatePicker','aspnetForm',inputId);" class="toolbar">
                            <span class="icon-16-stats" title="Date picker">
                            </span>
                            <%=Resources.strings.btnPickDate_Text%>
                            </a>
                        </td>
                        <td class="button">
                            <a href="#" onclick="BrowseServer();" class="toolbar">
                            <span class="icon-16-media" title="File browse">
                            </span>
                            <%=Resources.strings.btnBrowseFile_Text%>
                            </a>
                        </td>
                        <td class="button">
                            <asp:LinkButton ID="linkRemoveSelectedRow" class="toolbar" runat="server" ToolTip="Xóa dòng chọn" onclick="cmdRemoveSelectedRow_Click">
                            <span class="icon-16-logout" title="Remove selected rows">
                            </span>          
                            <%=Resources.strings.btnRemoveLine_Text%>
                            </asp:LinkButton>
                        </td>                        
                        <td class="button">
                            <asp:LinkButton ID="linkAddTagOrSubfield" class="toolbar" runat="server" ToolTip="Thêm dòng" onclick="cmdAddTagOrSubfield_Click">
                            <span class="icon-16-checkin" title="Add rows">
                            </span>          
                            <%=Resources.strings.btnAddLine_Text%>
                            </asp:LinkButton>
                        </td>
				    </tr>
				    </table>				
				    </div>			             
				    </td>
				    </tr>                     
                   </table>                     							
                 </td>
                </tr>
           </table>
        </td>
        <td valign="top" width="270px">
        <fieldset class="adminform">
        <legend><%=Resources.strings.ControlFields_Text %></legend>
 	        <table class="admintable" cellspacing="1" width="100%" style="table-layout:fixed">
                <tbody style="padding-left:10px">
                    <tr>
                        <td class="key" style="width:80px">
                        <label for="name"><%=Resources.strings.ID_Text %>:</label>
                        </td>
                        <td>
                        <asp:textbox ID="txtMetaContentID" runat="server" Enabled="false"></asp:textbox>                        
                        </td>            
                    </tr>
                    <tr>                                     
                       <td class="key">
                        <label for="name"><%=Resources.strings.Alias_Text %>:</label>
                        </td>
                        <td>
                        <asp:TextBox ID="txtMetaContentAlias" runat="server" Text="" Width="98%"></asp:TextBox> 
                        </td>
                    </tr>                                
                     <tr>
                        <td class="key">
                            <label for="name"><%=Resources.strings.Section_Text %>:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropSections" runat="server" oninit="dropSections_Init" 
                                onselectedindexchanged="dropSections_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>         
                    </tr>
                    <tr>                                     
                        <td class="key">
                            <label for="name"><%=Resources.strings.Category_Text %>:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropCategories" runat="server" AutoPostBack="true" onselectedindexchanged="dropCategories_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td> 
                    </tr>   
                    <tr>                                     
                        <td class="key">
                            <label for="name"><%=Resources.strings.Language_Text %>:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropLanguages" runat="server">
                            </asp:DropDownList>
                        </td> 
                    </tr>                      
                                                                             
                    <tr>                                                                     
                        <td colspan="2" align="right">
                                <asp:RadioButtonList ID="radioRecordStatus" runat="server" RepeatDirection="Horizontal">
                                
                                </asp:RadioButtonList>
                       </td>                         
                    </tr>
                    <tr>                                     
                        
                        <td class="key">
                            <label for="name"><%=Resources.strings.AccessLevel_Text %>:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropAccessLevels" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="dropAccessLevels_SelectedIndexChanged">

                            </asp:DropDownList><br />
                            <asp:CheckBoxList ID="cblRoles" runat="server" oninit="cblRoles_Init" Visible="false">
                            
                            </asp:CheckBoxList>                            
                        </td>                                                  
                    </tr>     
                    <tr>                                     
                        <td class="key">
                            <label for="name"><%=Resources.strings.EntryDate_Text %>:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelEntryDate" runat="server"></asp:Label>           
                        </td> 
                    </tr>                 
                    <tr>                                     
                       <td class="key">
                            <label for="name"><%=Resources.strings.Creator_Text %>:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelCreator" runat="server"></asp:Label>           
                        </td> 
                    </tr>                                   
                    <tr>                                     
                        <td class="key">
                            <label for="name"><%=Resources.strings.ModifiedDate_Text %>:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelModifyDate" runat="server"></asp:Label>           
                        </td> 
                    </tr>                 
                    <tr>                                     
                       <td class="key">
                        <label for="name"><%=Resources.strings.Modifier_Text %>:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelModifier" runat="server"></asp:Label>           
                        </td> 
                    </tr>                                              
            </tbody>
            </table>
        </fieldset>
        </td>
        </tr>
        </table>
        <script type="text/javascript">
		    On_Init();
		</script>