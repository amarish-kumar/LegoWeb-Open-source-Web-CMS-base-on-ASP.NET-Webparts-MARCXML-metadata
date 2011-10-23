<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionAddUpdate.ascx.cs" Inherits="UserControls_SectionAddUpdate" %>
	<div class="col" style="width:600px">
		<fieldset class="adminform">
		<legend>Section Details</legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name">Mã:</label></td>
            <td>
                <asp:TextBox ID="txtSectionID" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SectionIDRequired" runat="server" ControlToValidate="txtSectionID" ErrorMessage="Bạn chưa nhập mã vùng thông tin!"
                     ToolTip="Chưa nhập Section ID." Display="Dynamic" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="SectionIDValidator" runat="server" ControlToValidate="txtSectionID"  ErrorMessage="Bạn chỉ được nhập vào một số nguyên" 
                    ToolTip="Bạn chỉ được nhập vào một số nguyên" ValidationExpression="\d*" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên tiếng Việt:</label></td>
            <td>
                <asp:TextBox ID="txtSectionViTitle" runat="server" Width="90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SectionViTitleRequired" runat="server" ControlToValidate="txtSectionViTitle" ErrorMessage="Bạn chưa nhập Vùng thông tin!"
                     ToolTip="Chưa nhập tên Vùng thông tin." Display="Dynamic" SetFocusOnError="true" ValidationGroup="SectionInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="key"><label for="name">Tên tiếng Anh:</label></td>
            <td>
                <asp:TextBox ID="txtSectionEnTitle" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
                
            </tbody>
            </table>
		</fieldset>
	</div>
<font color="aqua"><b><i><asp:Literal ID="errorMessage" runat="server"></asp:Literal></i></b></font>