<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonParameterAddUpdate.ascx.cs" Inherits="LgwUserControls_CommonParameterAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>Tham số hệ thống</legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name">Tên tham số:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterName" runat="server" MaxLength="50" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CommonParameterIDRequired" runat="server" ControlToValidate="txtCommonParameterName" ErrorMessage="Bạn chưa nhập Tên tham số!"
                     ToolTip="Chưa nhập Parameter Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="CommonParameterInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="150px" class="key"><label for="name">Phân loại:</label></td>
            <td>
                <asp:DropDownList ID="dropPraramType" runat="server">
                    <asp:ListItem Value="0" Text= "Không xác định"></asp:ListItem>
                    <asp:ListItem Value="1" Text= "Tham số đăng ký"></asp:ListItem>
                    <asp:ListItem Value="2" Text= "Tham số xử lý"></asp:ListItem>
                    <asp:ListItem Value="3" Text= "Tham số từ điển" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Giá trị tiếng Việt:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterViValue" runat="server" Width="90%" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Giá trị tiếng Anh:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterEnValue" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td class="key"><label for="name">Mô tả:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterDescription" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
          
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>