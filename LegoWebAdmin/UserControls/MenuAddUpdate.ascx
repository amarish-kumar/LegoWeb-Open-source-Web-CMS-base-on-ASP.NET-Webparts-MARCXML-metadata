<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuAddUpdate.ascx.cs" Inherits="UserControls_MenuAddUpdate" %>
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
        document.getElementById('<%=ImageMenuImageUrl.ClientID%>').src = fileUrl;
        document.getElementById('<%=HiddenMenuImageUrl.ClientID%>').value = fileUrl;
    }
</script>

	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>Menu Details</legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name">Mã:</label></td>
            <td>
                <asp:TextBox ID="txtMenuID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuIDRequired" runat="server" ControlToValidate="txtMenuID" ErrorMessage="Bạn chưa nhập mã mục trình đơn!"
                     ToolTip="Chưa nhập Menu ID." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="MenuIDValidator" runat="server" ControlToValidate="txtMenuID"  ErrorMessage="Bạn chỉ được nhập vào một số nguyên" 
                    ToolTip="Bạn chỉ được nhập vào một số nguyên" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="MenuInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Trình đơn:</label></td>
            <td>
                <asp:DropDownList ID="dropMenuTypes" runat="server" OnSelectedIndexChanged="dropMenuTypes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Mã cha:</label></td>
            <td valign="middle">
                <asp:DropDownList ID="dropParentMenus" runat="server"></asp:DropDownList>
                <i>(tất cả các mục trình đơn trong các nhóm trình đơn)</i>
            </td>
        </tr>
        
        
        <tr>
            <td class="key"><label for="name">Tên tiếng Việt:</label></td>
            <td>
                <asp:TextBox ID="txtMenuViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuViTitleRequired" runat="server" ControlToValidate="txtMenuViTitle" ErrorMessage="Bạn chưa nhập Vùng thông tin!"
                     ToolTip="Chưa nhập tên Vùng thông tin." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên tiếng Anh:</label></td>
            <td>
                <asp:TextBox ID="txtMenuEnTitle" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="key"><label for="name">Link URL:</label></td>
            <td>            
                <asp:TextBox ID="txtLinkUrl" runat="server" Width="90%"></asp:TextBox>    
            </td>
        </tr>
        <tr style="height:100px">
            <td class="key"><label for="name">Ảnh đại diện:</label></td>
            <td>
                <asp:Image id="ImageMenuImageUrl" style="max-height:100px; max-width:150px" ImageUrl="" runat="server" />
                <asp:HiddenField ID="HiddenMenuImageUrl" runat="server" Value=""/>
                <a href="javascript:BrowseServer();">Chọn ảnh</a>
            </td>
        </tr> 
         <tr>
            <td class="key"><label for="name">Công bố:</label></td>
            <td>
                <asp:RadioButton ID="radioIsPublic" GroupName="Public" runat="server" Text="Có" Checked="true"/> &nbsp; <asp:RadioButton ID="radioIsNotPublic" GroupName="Public" runat="server" Text="Không"/>
            </td>
        </tr>           
        <tr>
            <td class="key"><label for="name">Cách mở Link:</label></td>
            <td>
               <asp:ListBox ID="listBoxBrowserNavigation" runat="server">
               <asp:ListItem Value="0" Text="Mở bình thường" Selected="True"></asp:ListItem>
               <asp:ListItem Value="1" Text="Mở trong cửa sổ mới"></asp:ListItem>
               <asp:ListItem Value="2" Text="Mở trong cửa sổ mới và không có thanh di chuyển"></asp:ListItem>
               </asp:ListBox>
            </td>
        </tr>           

            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>