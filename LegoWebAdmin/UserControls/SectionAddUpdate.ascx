<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionAddUpdate.ascx.cs" Inherits="LgwUserControls_SectionAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.ID_Text%>:</label></td>
            <td>
                <asp:TextBox ID="txtSectionID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SectionIDRequired" runat="server" ControlToValidate="txtSectionID" ErrorMessage="ID is required!"
                     ToolTip="ID is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="SectionIDValidator" runat="server" ControlToValidate="txtSectionID"  ErrorMessage="Only numbers are accepted!" 
                    ToolTip="Only numbers are accepted!" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.VietnameseTitle_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtSectionViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SectionViTitleRequired" runat="server" ControlToValidate="txtSectionViTitle" ErrorMessage="Title/name is required!"
                     ToolTip="Title/name is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name"><%=Resources.strings.EnglishTitle_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtSectionEnTitle" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
                
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>