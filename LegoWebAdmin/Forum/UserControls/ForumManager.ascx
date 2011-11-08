<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForumManager.ascx.cs" Inherits="LgwUserControls_ForumManager" %>
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
        document.getElementById('<%=ImageForumImageUrl.ClientID%>').src = fileUrl;
        document.getElementById('<%=HiddenForumImageUrl.ClientID%>').value = fileUrl;
    }
</script>

 <asp:Literal ID="litErrorSpaceHolder" runat="server"> </asp:Literal>

<table style="width:100%">
<tr>
<td style="width:60%; vertical-align:top" >
    <fieldset class="adminform">
		<legend><%=Resources.strings.Forums_Text%></legend>
<table class="adminlist" cellspacing="1">   									
					<asp:repeater id="forumManagerRepeater" runat="server" OnItemCommand="repeater_OnItemCommand">
							<HeaderTemplate>
							<thead>
							<tr>
							<th class="title"><%=Resources.strings.ID_Text %></th>
							<th class="title"><%=Resources.strings.Title_Text %></th>							
							<th class="title"><%=Resources.strings.Description_Text %></th>
							<th class="title"><%=Resources.strings.AdminRoles_Text %></th>
							<th class="title"><%=Resources.strings.OrderNumber_Text%></th>
							<th class="title"><%=Resources.strings.IsPublic_Text %></th>
							<th class="title"><%=Resources.strings.Command_Text %></th>
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                                                                                           
                                <td align="center">                                
                                <asp:Label ID="labelForumID" runat="server"><%# DataBinder.Eval(Container.DataItem, "ForumID")%></asp:Label>
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Title")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Description")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "AdminRoles")%>                                
                                </td> 
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "OrderNumber")%>                                
                                </td>                           
                                <td align="center">          
                                 <asp:Image ID="imgIsPublic" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsPublic")%>'/>  
                                 <asp:Image ID="imgIsNotPublic" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsPublic")%>'/>  
                                </td>                                      
                                <td align="center">                                
                                  <asp:LinkButton ID="linkEdit" Runat="server" CommandName="edit" CommandArgument='<%# Eval("ForumID") %>'><%=Resources.strings.btnEdit_Text %></asp:LinkButton>                               
                                </td>
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <asp:Label ID="labelForumID" runat="server"><%# DataBinder.Eval(Container.DataItem, "ForumID")%></asp:Label>
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Title")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Description")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "AdminRoles")%>                                
                                </td> 
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "OrderNumber")%>                                
                                </td>                           
                                <td align="center">          
                                 <asp:Image ID="imgIsPublic" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsPublic")%>'/>  
                                 <asp:Image ID="imgIsNotPublic" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsPublic")%>'/>  
                                </td>                                      
                                <td align="center">                                
                                  <asp:LinkButton ID="linkEdit" Runat="server" CommandName="edit" CommandArgument='<%# Eval("ForumID") %>'><%=Resources.strings.btnEdit_Text %></asp:LinkButton>                               
                                </td>
                            </tr>                            
							</AlternatingItemTemplate>
							<FooterTemplate>
							<tr>
							<td colspan="7" valign="middle" align="right">
							    <asp:LinkButton ID="linkAddNew" runat="server" OnClick="linkAddNew_OnClick"><%=Resources.strings.btnAdd_Text %></asp:LinkButton>   
							</td>
							</tr>
							
							</FooterTemplate>

						</asp:repeater>
						
						</table>

</fieldset>
</td>
<td style="width:50%; vertical-align:top">
<div id="divAddUpdateForumInfo" visible="false" runat="server">
<%--location add/update--%>
    <fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.ID_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtForumID" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Title_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is required!"
                     ToolTip="Title is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="ForumInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Description_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="90%" Rows="2"></asp:TextBox>
            </td>
        </tr>  
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.AdminRoles_Text %>:</label></td>
            <td>
                 <asp:CheckBoxList ID="cblRoles" runat="server" oninit="cblRoles_Init"></asp:CheckBoxList>   
            </td>
        </tr>                                                                                                                              
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.OrderNumber_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtOrderNumber" runat="server" MaxLength="10" style="text-align:right"></asp:TextBox>
            </td>
        </tr>          
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.ThumbImage_Text %>:</label></td>
            <td>
                <asp:Image id="ImageForumImageUrl" style="max-height:32px; max-width:32px" ImageUrl="" runat="server" />
                <asp:HiddenField ID="HiddenForumImageUrl" runat="server" Value=""/>
                <a href="javascript:BrowseServer();"><%=Resources.strings.btnBrowse_Text %></a>
            </td>
        </tr>  
        <tr>                                                                     
            <td class="key">
            <label for="name"><%=Resources.strings.IsPublic_Text %>:</label>
            </td>
            <td>
                <asp:RadioButton ID="radioIsPublic" GroupName="Public" runat="server" Text="Yes" Checked="true"/> &nbsp; <asp:RadioButton ID="radioIsNotPublic" GroupName="Public" runat="server" Text="No"/>                        
            </td> 
            
        </tr>      
          
        <tr>
            <td colspan="2">
                <asp:Literal ID="errorMessage" runat="server" />
            </td>
        </tr>
        <tr>        
            <td colspan="2" align="right">
                <asp:Button ID="btnDelete" 
                    OnClientClick="return confirm('Are you sure to remove this forum?')" 
                    runat="server" Text="Delete" onclick="btnDelete_Click" />&nbsp;       
                <asp:Button ID="btnOk" runat="server"  Text="Ok" onclick="btnOk_Click" />&nbsp;       
                <asp:Button ID="btnCancel" runat="server"  Text="Cancel" 
                    onclick="btnCancel_Click" />&nbsp;       
            </td>
        </tr>
                                     
            </tbody>
            </table>
		</fieldset>
</div>
</td>
</tr>
</table>
