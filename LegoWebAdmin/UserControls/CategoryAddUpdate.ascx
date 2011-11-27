<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryAddUpdate.ascx.cs" Inherits="LgwUserControls_CategoryAddUpdate" %>

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
        document.getElementById('<%=ImageCategoryImageUrl.ClientID%>').src = fileUrl;
        document.getElementById('<%=HiddenCategoryImageUrl.ClientID%>').value = fileUrl;
    }
</script>

	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td style="width:150px" class="key"><label for="name"><%=Resources.strings.ID_Text %>:</label></td>
            <td style="width:450px">
                <asp:TextBox ID="txtCategoryID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CategoryIDRequired" runat="server" ControlToValidate="txtCategoryID" ErrorMessage="ID is required!"
                     ToolTip="ID is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="CategoryIDValidator" runat="server" ControlToValidate="txtCategoryID"  ErrorMessage="Only numbers are accepted!" 
                    ToolTip="Only numbers are accepted!" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Section_Text %>:</label></td>
            <td>
                <asp:DropDownList ID="dropSections" runat="server" OnSelectedIndexChanged="dropSections_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.ParentID_Text %>:</label></td>
            <td>
                <asp:DropDownList ID="dropParentCategories" runat="server"></asp:DropDownList>
            </td>
        </tr>
        
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.VietnameseTitle_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryViTitle" runat="server" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CategoryViTitleRequired" runat="server" ControlToValidate="txtCategoryViTitle" ErrorMessage="Title/name is required!"
                     ToolTip="Title/name is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.VietnameseValue_Text%>:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryEnTitle" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Alias_Text%>:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryAlias" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Template_Text %>:</label></td>
            <td>            
            <asp:DropDownList ID="dpTemplateNames" runat="server" 
                    oninit="dpTemplateNames_Init" ></asp:DropDownList>            
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.IsPublic_Text %>:</label></td>
            <td>
                <asp:RadioButton ID="radioIsPublic" GroupName="Public" runat="server" Text="Có" Checked="true"/> &nbsp; <asp:RadioButton ID="radioIsNotPublic" GroupName="Public" runat="server" Text="Không"/>
            </td>
        </tr>           
        <tr style="height:100px">
            <td class="key"><label for="name"><%=Resources.strings.ThumbImage_Text %>:</label></td>
            <td>
                <asp:Image id="ImageCategoryImageUrl" style="max-height:100px; max-width:150px" ImageUrl="" runat="server" />
                <asp:HiddenField ID="HiddenCategoryImageUrl" runat="server" Value=""/>
                <a href="javascript:BrowseServer();"><%=Resources.strings.Browse_Text %></a>
            </td>
        </tr>  
        
        <tr>            
            <td class="key">
                <label for="name"><%=Resources.strings.AdminRoles_Text %>:</label>
            </td>
            <td>
                <asp:DropDownList ID="dropAdminLevels" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="dropAdminLevels_SelectedIndexChanged">
                </asp:DropDownList><br />
                <asp:CheckBoxList ID="cblRoles" runat="server" oninit="cblRoles_Init" Visible="false">
                
                </asp:CheckBoxList>                                            
            </td>       
        </tr>   
        <tr>
             <td class="key">
                <label for="name"><%=Resources.strings.ApplyToChildren_Text %>:</label>
            </td>
            <td>
                    <asp:RadioButton ID="radioApplyChilrenNodes" GroupName="ApplyChilrenNodes" runat="server" Text="Yes"/> &nbsp; <asp:RadioButton ID="radioNotApplyChilrenNodes" GroupName="ApplyChilrenNodes" runat="server" Text="No" Checked="true"/>
            </td>                                           
        </tr>        
        
        <tr>
            <td>
            </td>
            <td class="key"><label for="name"><%=Resources.strings.LinkToMenu_Text %>:</label></td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Menu_Text%>:</label></td>
            <td>
                <asp:DropDownList ID="dropMenuTypes" runat="server" OnSelectedIndexChanged="dropMenuTypes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.MenuItem_Text%>:</label></td>
            <td>
                <asp:DropDownList ID="dropLinkMenus" runat="server"></asp:DropDownList>
            </td>
        </tr>
                              
        <tr>
            <td>
            </td>
            <td class="key"><label for="name"><%=Resources.strings.SEO_Text %>:</label></td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Title_Text%>:</label></td>
            <td>
                <asp:TextBox ID="txtSeoTitle" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Description_Text%>:</label></td>
            <td>
                <asp:TextBox ID="txtSeoDescription" runat="server" Width="95%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Keywords_Text%>:</label></td>
            <td>
               <asp:TextBox ID="txtSeoKeywords" runat="server" Width="95%" TextMode="MultiLine"></asp:TextBox> 
            </td>
        </tr>        
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>