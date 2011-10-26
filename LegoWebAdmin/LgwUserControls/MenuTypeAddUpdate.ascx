<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuTypeAddUpdate.ascx.cs" Inherits="LgwUserControls_MenuTypeAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.ID_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuTypeIDRequired" runat="server" ControlToValidate="txtMenuTypeID" ErrorMessage="ID is required!"
                     ToolTip="ID is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="MenuTypeIDValidator" runat="server" ControlToValidate="txtMenuTypeID"  ErrorMessage="Only number is accepted!" 
                    ToolTip="Only number is accepted!" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.VietnameseTitle_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MenuTypeViTitleRequired" runat="server" ControlToValidate="txtMenuTypeViTitle" ErrorMessage="Menu name/title is required!"
                     ToolTip="Menu name/title is required!." Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.EnglishTitle_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeEnTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMenuTypeEnTitle" ErrorMessage="Menu name/title is required!"
                     ToolTip="Menu name/title is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="MenuTypeInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.Description_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtMenuTypeDescription" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
          
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>