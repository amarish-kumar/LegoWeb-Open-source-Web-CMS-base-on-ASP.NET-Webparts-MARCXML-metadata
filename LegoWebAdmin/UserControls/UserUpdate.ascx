<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserUpdate.ascx.cs" Inherits="UserControls_UserUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>User Details</legend>
	<table class="admintable" cellspacing="1">
	<tbody>
    <tr>
    <td width="150px" class="key"><label for="name">Tên đăng nhập:</label></td>
    <td>
        <asp:TextBox ID="txtUserName" runat="server" Enabled="false"></asp:TextBox>
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
         <asp:CheckBox ID="checkActivate" Checked="false" runat="server" />
    </td>

    </tr>    

    <tr>
    <td class="key"><label for="name">Khóa:</label></td>
    <td>
         <asp:CheckBox ID="checkLock" Checked="false" runat="server"/>
    </td>
    </tr>     
</tbody>
</table>
</fieldset>
</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>
