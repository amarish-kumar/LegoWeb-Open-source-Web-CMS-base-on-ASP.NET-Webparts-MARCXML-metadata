<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuTypeAddUpdate.ascx.cs" Inherits="UserControls_MenuTypeAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>Thông tin trình đơn</legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name">Mã:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuTypeIDRequired" runat="server" ControlToValidate="txtMenuTypeID" ErrorMessage="Bạn chưa nhập mã vùng thông tin!"
                     ToolTip="Chưa nhập MenuType ID." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="MenuTypeIDValidator" runat="server" ControlToValidate="txtMenuTypeID"  ErrorMessage="Bạn chỉ được nhập vào một số nguyên" 
                    ToolTip="Bạn chỉ được nhập vào một số nguyên" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuTypeViTitleRequired" runat="server" ControlToValidate="txtMenuTypeViTitle" ErrorMessage="Bạn chưa nhập tên trình đơn!"
                     ToolTip="Chưa nhập tên trình đơn." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên (TA):</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeEnTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMenuTypeEnTitle" ErrorMessage="Bạn chưa nhập tên tiếng Anh trình đơn!"
                     ToolTip="Chưa nhập tên tiếng Anh trình đơn." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td class="key"><label for="name">Mô tả:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeDescription" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
          
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>