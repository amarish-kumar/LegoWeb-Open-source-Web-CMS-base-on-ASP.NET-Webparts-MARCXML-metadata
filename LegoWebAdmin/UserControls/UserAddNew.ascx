<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserAddNew.ascx.cs" Inherits="UserControls_UserAddNew" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>User Details</legend>
			<table class="admintable" cellspacing="1">
			<tbody>
    <tr>
    <td width="150px" class="key"><label for="name">Tên đăng nhập:</label></td>
    <td>
         <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="userNameRequired" runat="server" 
                                ControlToValidate="txtUserName" ErrorMessage="Chưa nhập tên đăng nhập." 
                                ToolTip="Chưa nhập tên đăng nhập." Display="Dynamic" SetFocusOnError="true"  ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
    </td>

    </tr>

    <tr>
    <td class="key"><label for="name">Email:</label></td>
    <td>
            <asp:TextBox ID="txtEMAIL" runat="server" MaxLength="50" Width="50%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="txtEMAILRequired" runat="server" ControlToValidate="txtEMAIL"
                ErrorMessage="Chưa nhập địa chỉ Email." ToolTip="Chưa nhập địa chỉ Email." Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="txtEMAILValidator" runat="server" ControlToValidate="txtEMAIL"
                ErrorMessage="Địa chỉ Email chưa chính xác (VD: name@gmail.com)" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                SetFocusOnError="True" ValidationGroup="UserInfo">*</asp:RegularExpressionValidator>

    </td>
    </tr>     
        <tr>
    <td class="key"><label for="name">Câu hỏi mật khẩu:</label></td>
    <td>
         <asp:TextBox ID="txtPasswordQuestion" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td class="key"><label for="name">Câu trả lời:</label></td>
    <td>
         <asp:TextBox ID="txtPasswordAnswer" runat="server"></asp:TextBox>
    </td>
    </tr>

    <tr>
        <td class="key"><label for="name">Vai trò:</label></td>
        <td>
        <table>
        <tr>
        <td>
        Đã gán
        </td>
        <td>
        </td>
        <td>
        Chưa gán
        </td>
        </tr>
        <tr>
        <td valign="top">
        <asp:ListBox ID="listUserRoles" runat="server" Width="200px" SelectionMode="Multiple"></asp:ListBox>        
        </td>
        <td valign="middle">
        <asp:LinkButton ID="linkButtonAssignRole" runat="server" 
                onclick="linkButtonAssignRole_Click"><=</asp:LinkButton><br />
        <asp:LinkButton ID="linkButtonRemoveRole" runat="server" 
                onclick="linkButtonRemoveRole_Click">=></asp:LinkButton>
        </td>
        <td valign="top">
                <asp:ListBox ID="listAvailableRoles" runat="server" Width="200px" SelectionMode="Multiple"></asp:ListBox>        
        </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
    <td class="key"><label for="name">Kích hoạt:</label></td>
    <td>
         <asp:CheckBox ID="checkActivate" Checked="true" runat="server" />
    </td>

    </tr>    
                            <tr>
                        <td class="key"><label for="name">Mật khẩu:</label></td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                ControlToValidate="txtPassword" ErrorMessage="Chưa nhập mật khẩu." 
                                ToolTip="Chưa nhập mật khẩu." Display="Dynamic" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionPassword" runat="server" 
                                ControlToValidate="txtPassword" Display="Dynamic" 
                                ErrorMessage="Mật khẩu phải có ít nhất 6 ký tự" 
                                ValidationExpression="(\w){6}(\w)*" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RegularExpressionValidator>
                        </td>

                    </tr>
                    <tr>
                        <td class="key"><label for="name">Nhập lại mật khẩu:</label></td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" 
                                Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                ControlToValidate="txtConfirmPassword" ErrorMessage="Chưa nhập lại mật khẩu." 
                                ToolTip="Chưa nhập lại mật khẩu." Display="Dynamic" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                                Display="Dynamic" ErrorMessage="Mật khẩu xác nhận chưa chính xác" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:CompareValidator>
                        </td>                            
                    </tr> 
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>
