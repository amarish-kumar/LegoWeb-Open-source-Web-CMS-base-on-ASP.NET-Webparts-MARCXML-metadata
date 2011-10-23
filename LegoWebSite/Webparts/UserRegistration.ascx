<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRegistration.ascx.cs"
    Inherits="Webparts_UserRegistration" %>

		<div id="grey-header-title-box"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title"> <%=Resources.strings.UserRegistration%></div>		    						
			<div class="m">			
			<div class="clearfix">
			
			
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
        DisableCreatedUser="True" LoginCreatedUser="False" 
        oncreateduser="CreateUserWizard1_CreatedUser" 
        oncreatinguser="CreateUserWizard1_CreatingUser" 
CreateUserButtonText="Ghi danh">
        <CreateUserButtonStyle BorderColor="#C4C4C4" BorderStyle="Solid" Font-Bold="True" ForeColor="#424242" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>  
                <table cellpadding="2" cellspacing="3" style="width:100%; border-bottom:1px solid #cccccc;">    
                <tr>
                <td align="right" valign="middle">
                    <b><%=Resources.strings.PatronBarcode%>:</b>
                </td>
                <td valign="middle">
			            <asp:TextBox ID="UserName" runat="server" CssClass="textbox"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserName"
							ErrorMessage="UserName is required." ToolTip="UserName is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                </td>
                </tr>
                                           
                <tr>
                <td align="right" valign="middle">
                    <b><%=Resources.strings.Alias%>:</b>
                </td>
                <td valign="middle">
			            <asp:TextBox ID="Alias" runat="server" CssClass="textbox"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Alias"
							ErrorMessage="Alias is required." ToolTip="Alias is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                </td>
                </tr>
                <tr>
                <td align="right" valign="middle">
                    <b><%=Resources.strings.Email%>:</b>
                </td>
                <td valign="middle">
				    <asp:TextBox ID="Email" runat="server" CssClass="textbox"></asp:TextBox>
					<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="Email"
						ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>                                               
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
						ErrorMessage="Email address is not valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />                                                   
                </td>
                </tr>
                <tr>
                <td align="right" valign="middle">
                    <b><%=Resources.strings.Password%>:</b>
                </td>
                <td valign="middle">
					<asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
					<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
						ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                </td>
                </tr>
                <tr>
                <td align="right" valign="middle">
                    <b><%=Resources.strings.RetypePassword%>:</b>
                </td>
                <td valign="middle">
					<asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"  CssClass="textbox"></asp:TextBox>
					<asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
						ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
						ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>		
					<asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
						ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
						ValidationGroup="CreateUserWizard1"></asp:CompareValidator>																		
                </td>
                </tr>
                <tr>
                    <td align="right" valign="middle">
                    <b><%=Resources.strings.Avatar%>:</b>
                    </td>                                
                    <td valign="middle">
					<asp:HiddenField ID="AvatarURL" runat="server" />
					<asp:FileUpload ID="fileUploadAvatar" runat="server"/>
                    </td>
                </tr>
                <tr>
                <td>
                
                </td>
                <td>
                <span style="color:red">                
                <asp:Literal ID="CustomErrorMessage" runat="server" EnableViewState="False"></asp:Literal><br />
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                </span>
                </td>
                </tr>
                <tr>
                <td colspan="2" class="top-register">
                    <%=Resources.strings.RegistrationAgreement%>
                </td>
                
                </tr>
                <tr>
                <td colspan=2 style="padding-left:50px">
                	<asp:Literal ID="literalUserAgreement" runat="server">
							Điều kiện tham gia.
					</asp:Literal>
                </td>                                
                </tr>
                <tr>
                <tr>
                <td colspan="2">&nbsp;</td>
                </tr>
                <td colspan="2" style="padding-left:55px">
                <b><asp:CheckBox ID="chkUserAgreement" runat="server" Text='<%$Resources:strings,IAgree%>'/></b>
                </td>
                </tr>
                </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <%=Resources.strings.RegistrationCompleteNotify%>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
            
		    </div>		
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
