<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaContentAddUpdate.ascx.cs" Inherits="LgwUserControls_MetaContentAddUpdate" %>
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
	
		<table cellspacing="0" cellpadding="0" border="0" width="100%">  		<tr>
		<td valign="top">		    
        	<table class="adminform" cellspacing="1" width="100%" style="table-layout:fixed">  				
					<asp:repeater id="marcTextRepeater" runat="server" 
                            onitemdatabound="marcTextRepeater_ItemDataBound">
							<HeaderTemplate>
							<tr>
							    <th style="width:25px">Mã</th>
							    <th style="width:120px">Nhãn</th>
							    <th>Nội dung</th>
							    <th style="width:30px">Chọn</th>
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
                            <asp:LinkButton ID="linkTakeRelatedContent" class="toolbar" runat="server" ToolTip="Liên kết nội dung" onclick="linkTakeRelatedContent_Click">
                            <span class="icon-16-info" title="Remove selected rows">
                            </span>          
                            Liên kết                      
                            </asp:LinkButton>
                        </td>
                        <td class="button">
                            <a href="#" onclick="javascript:showDatePicker('myDatePicker','aspnetForm',inputId);" class="toolbar">
                            <span class="icon-16-stats" title="Date picker">
                            </span>
                            Chọn ngày
                            </a>
                        </td>
                        <td class="button">
                            <a href="#" onclick="BrowseServer();" class="toolbar">
                            <span class="icon-16-media" title="File browse">
                            </span>
                            Chọn tệp
                            </a>
                        </td>
                        <td class="button">
                            <asp:LinkButton ID="linkRemoveSelectedRow" class="toolbar" runat="server" ToolTip="Xóa dòng chọn" onclick="cmdRemoveSelectedRow_Click">
                            <span class="icon-16-logout" title="Remove selected rows">
                            </span>          
                            Xóa dòng                      
                            </asp:LinkButton>
                        </td>                        
                        <td class="button">
                            <asp:LinkButton ID="linkAddTagOrSubfield" class="toolbar" runat="server" ToolTip="Thêm dòng" onclick="cmdAddTagOrSubfield_Click">
                            <span class="icon-16-checkin" title="Add rows">
                            </span>          
                            Thêm dòng                      
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
        <legend>Thông tin điều khiển</legend>
 	        <table class="admintable" cellspacing="1" width="100%" style="table-layout:fixed">
                <tbody style="padding-left:10px">
                    <tr>
                        <td class="key" style="width:80px">
                        <label for="name">Mã số:</label>
                        </td>
                        <td>
                        <asp:textbox ID="txtMetaContentID" runat="server" Enabled="false"></asp:textbox>                        
                        </td>            
                    </tr>
                    <tr>                                     
                        <td>
                        </td>
                        <td>
                        </td>
                     </tr>
                     <tr>
                        <td class="key">
                            <label for="name">Vùng:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropSections" runat="server" oninit="dropSections_Init" 
                                onselectedindexchanged="dropSections_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>         
                    </tr>
                    <tr>                                     
                        <td class="key">
                            <label for="name">Chuyên mục:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropCategories" runat="server" AutoPostBack="true" onselectedindexchanged="dropCategories_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td> 
                    </tr>   
                    <tr>                                     
                        <td class="key">
                            <label for="name">Ngôn ngữ:</label>
                        </td>
                        <td>                        
                            <asp:DropDownList ID="dropLanguages" runat="server">
                            <asp:ListItem Value="vi" Text="Tiếng Việt"></asp:ListItem>
                            <asp:ListItem Value="en" Text="Tiếng Anh"></asp:ListItem>
                            </asp:DropDownList>
                        </td> 
                    </tr>                      
                                                                             
                    <tr>                                                                     
                        <td class="key">
                        <label for="name">Công bố:</label>
                        </td>
                        <td>
                            <asp:RadioButton ID="radioIsPublic" GroupName="Public" runat="server" Text="Có" Checked="true"/> &nbsp; <asp:RadioButton ID="radioIsNotPublic" GroupName="Public" runat="server" Text="Không"/>                        
                        </td> 
                        
                    </tr>
                    <tr>                                     
                        
                        <td class="key">
                            <label for="name">Mức truy cập:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropAccessLevels" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="dropAccessLevels_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Công khai"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Đăng nhập"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Đặc biệt"></asp:ListItem>
                            </asp:DropDownList><br />
                            <asp:CheckBoxList ID="cblRoles" runat="server" oninit="cblRoles_Init" Visible="false">
                            
                            </asp:CheckBoxList>                            
                        </td>                                                  
                    </tr>     
                    <tr>                                     
                        <td class="key">
                            <label for="name">Ngày nhập:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelEntryDate" runat="server"></asp:Label>           
                        </td> 
                    </tr>                 
                    <tr>                                     
                       <td class="key">
                            <label for="name">Người nhập:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelCreator" runat="server"></asp:Label>           
                        </td> 
                    </tr>                                   
                    <tr>                                     
                        <td class="key">
                            <label for="name">Ngày sửa:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelModifyDate" runat="server"></asp:Label>           
                        </td> 
                    </tr>                 
                    <tr>                                     
                       <td class="key">
                        <label for="name">Người sửa:</label>
                        </td>
                        <td>             
                        <asp:Label ID="labelModifier" runat="server"></asp:Label>           
                        </td> 
                    </tr>      
                    <tr>
                    <td colspan="2">
            		    <span style="color:Red; font-style:oblique; font-size:small">                             <asp:Literal ID="ltErrorMessage" runat="server"></asp:Literal>                         </span>
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