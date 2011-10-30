<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonParameterAddUpdate.ascx.cs" Inherits="LgwUserControls_CommonParameterAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend><%=Resources.strings.SystemParameters_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.ParameterName_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterName" runat="server" MaxLength="50" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CommonParameterIDRequired" runat="server" ControlToValidate="txtCommonParameterName" ErrorMessage="Bạn chưa nhập Tên tham số!"
                     ToolTip="Chưa nhập Parameter Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="CommonParameterInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.Type_Text %>:</label></td>
            <td>
                <asp:DropDownList ID="dropPraramType" runat="server">

                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.VietnameseValue_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterViValue" runat="server" Width="90%" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.EnglishValue_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterEnValue" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Description_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtCommonParameterDescription" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
          
            </tbody>
            </table>
		</fieldset>
	</div>
