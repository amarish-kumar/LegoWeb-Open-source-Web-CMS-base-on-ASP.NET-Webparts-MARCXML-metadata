<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserAddNew.ascx.cs" Inherits="LgwUserControls_UserAddNew" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text%></legend>
			<table class="admintable" cellspacing="1">
			<tbody>
    <tr>
    <td width="150px" class="key"><label for="name"><%=Resources.strings.UserName_Text%>:</label></td>
    <td>
         <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="userNameRequired" runat="server" 
                                ControlToValidate="txtUserName" ErrorMessage="Username is required!" 
                                ToolTip="Username is required!" Display="Dynamic" SetFocusOnError="true"  ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
    </td>

    </tr>

    <tr>
    <td class="key"><label for="name"><%=Resources.strings.Email_Text %>:</label></td>
    <td>
            <asp:TextBox ID="txtEMAIL" runat="server" MaxLength="50" Width="50%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="txtEMAILRequired" runat="server" ControlToValidate="txtEMAIL"
                ErrorMessage="Email is required!" ToolTip="Email is required!" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="txtEMAILValidator" runat="server" ControlToValidate="txtEMAIL"
                ErrorMessage="Email address is not correct (VD: name@gmail.com)" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                SetFocusOnError="True" ValidationGroup="UserInfo">*</asp:RegularExpressionValidator>

    </td>
    </tr>     
        <tr>
    <td class="key"><label for="name"><%=Resources.strings.PasswordQuestion_Text%>:</label></td>
    <td>
         <asp:TextBox ID="txtPasswordQuestion" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td class="key"><label for="name"><%=Resources.strings.Answer_Text%>:</label></td>
    <td>
         <asp:TextBox ID="txtPasswordAnswer" runat="server"></asp:TextBox>
    </td>
    </tr>

    <tr>
        <td class="key"><label for="name"><%=Resources.strings.Role_Text%>:</label></td>
        <td>
        <table>
        <tr>
        <td>
        <%=Resources.strings.Assigned_Text%>
        </td>
        <td>
        </td>
        <td>
        <%=Resources.strings.Available_Text%>
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
    <td class="key"><label for="name"><%=Resources.strings.IsActived_Text%>:</label></td>
    <td>
         <asp:CheckBox ID="checkActivate" Checked="true" runat="server" />
    </td>

    </tr>    
                            <tr>
                        <td class="key"><label for="name"><%=Resources.strings.Password_Text%>:</label></td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                ControlToValidate="txtPassword" ErrorMessage="Password is required!" 
                                ToolTip="Password is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionPassword" runat="server" 
                                ControlToValidate="txtPassword" Display="Static" 
                                ErrorMessage="Password must has at least 6 characters" 
                                ValidationExpression="^.*\w{6,}.*$" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RegularExpressionValidator>
                        </td>

                    </tr>
                    <tr>
                        <td class="key"><label for="name"><%=Resources.strings.ConfirmPassword_Text%>:</label></td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" 
                                Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm password is required!" 
                                ToolTip="Confirm password is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                                Display="Dynamic" ErrorMessage="Password and confirm password not match!" SetFocusOnError="true" ValidationGroup="UserInfo">*</asp:CompareValidator>
                        </td>                            
                    </tr> 
            </tbody>
            </table>
		</fieldset>
	</div>
