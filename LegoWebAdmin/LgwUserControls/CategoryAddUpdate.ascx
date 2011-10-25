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
		<legend>Chi tiết</legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name">Mã:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CategoryIDRequired" runat="server" ControlToValidate="txtCategoryID" ErrorMessage="Bạn chưa nhập mã vùng thông tin!"
                     ToolTip="Chưa nhập Category ID." Display="Dynamic" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="CategoryIDValidator" runat="server" ControlToValidate="txtCategoryID"  ErrorMessage="Bạn chỉ được nhập vào một số nguyên" 
                    ToolTip="Bạn chỉ được nhập vào một số nguyên" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Khu vực:</label></td>
            <td>
                <asp:DropDownList ID="dropSections" runat="server" OnSelectedIndexChanged="dropSections_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Mã cha:</label></td>
            <td>
                <asp:DropDownList ID="dropParentCategories" runat="server"></asp:DropDownList>
            </td>
        </tr>
        
        
        <tr>
            <td class="key"><label for="name">Tên tiếng Việt:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CategoryViTitleRequired" runat="server" ControlToValidate="txtCategoryViTitle" ErrorMessage="Bạn chưa nhập Vùng thông tin!"
                     ToolTip="Chưa nhập tên Vùng thông tin." Display="Dynamic" SetFocusOnError="true" ValidationGroup="CategoryInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên tiếng Anh:</label></td>
            <td>
                <asp:TextBox ID="txtCategoryEnTitle" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Khuôn mẫu:</label></td>
            <td>            
            <asp:DropDownList ID="dpTemplateNames" runat="server" 
                    oninit="dpTemplateNames_Init" ></asp:DropDownList>            
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Công bố:</label></td>
            <td>
                <asp:RadioButton ID="radioIsPublic" GroupName="Public" runat="server" Text="Có" Checked="true"/> &nbsp; <asp:RadioButton ID="radioIsNotPublic" GroupName="Public" runat="server" Text="Không"/>
            </td>
        </tr>           
        <tr style="height:100px">
            <td class="key"><label for="name">Ảnh đại diện:</label></td>
            <td>
                <asp:Image id="ImageCategoryImageUrl" style="max-height:100px; max-width:150px" ImageUrl="" runat="server" />
                <asp:HiddenField ID="HiddenCategoryImageUrl" runat="server" Value=""/>
                <a href="javascript:BrowseServer();">Chọn ảnh</a>
            </td>
        </tr>  
        
        <tr>            
            <td class="key">
                <label for="name">Nhóm quản trị:</label>
            </td>
            <td>
                <asp:DropDownList ID="dropAdminLevels" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="dropAdminLevels_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="Bất kỳ"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Chỉ định"></asp:ListItem>
                </asp:DropDownList><br />
                <asp:CheckBoxList ID="cblRoles" runat="server" oninit="cblRoles_Init" Visible="false">
                
                </asp:CheckBoxList>                                            
            </td>       
        </tr>   
        <tr>
             <td class="key">
                <label for="name">Áp dụng cho mức con:</label>
            </td>
            <td>
                    <asp:RadioButton ID="radioApplyChilrenNodes" GroupName="ApplyChilrenNodes" runat="server" Text="Có"/> &nbsp; <asp:RadioButton ID="radioNotApplyChilrenNodes" GroupName="ApplyChilrenNodes" runat="server" Text="Không" Checked="true"/>
            </td>                                           
        </tr>        
        
        <tr>
            <td>
            </td>
            <td class="key"><label for="name">Liên kết trình đơn:</label></td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Trình đơn:</label></td>
            <td>
                <asp:DropDownList ID="dropMenuTypes" runat="server" OnSelectedIndexChanged="dropMenuTypes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Mục trình đơn:</label></td>
            <td>
                <asp:DropDownList ID="dropLinkMenus" runat="server"></asp:DropDownList>
            </td>
        </tr>
                              
        
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>