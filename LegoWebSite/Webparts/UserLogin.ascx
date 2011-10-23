<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLogin.ascx.cs" Inherits="Webparts_UserLogin" %>
		<div id="grey-header-title-box"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title"> <%=this.Page.User.Identity.IsAuthenticated?this.Page.User.Identity.Name:LegoWebSite.Buslgic.CommonParameters.asign_WEBPART_PERSONALIZE_PARAMETER(this.Title)%></div>		    						
			<div class="m">			
			<div class="clearfix">
			
		<!-- login form -->
		<div id="divLogin" runat="server" visible="false">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr bgcolor="white">
					<td valign="top">
					<asp:Login ID="Login1" runat="server" 
						UserNameLabelText="Mã:" 
						PasswordLabelText="M.khẩu:" OnLoginError="OnLoginError" OnLoggedIn="OnLoggedIn" 
						RememberMeText="Ghi nhớ" LoginButtonText="Login" TitleText="" 
						Width="100%" LabelStyle-Font-Bold="True" LabelStyle-VerticalAlign="Top">
					<CheckBoxStyle HorizontalAlign="Left" />
					<TextBoxStyle Width="110px" />
					<LabelStyle HorizontalAlign="Left" Width="70px" />
					</asp:Login>
					</td>
				</tr>
				<tr>
					<td valign="middle" style="border-top:1px solid #BCCBD8;background-color:#99FFFF;">
						<asp:LinkButton ID="linkUserRegistration" runat="server" OnClick="linkUserRegistration_Click" Font-Bold="False" Font-Size="Small" ForeColor="#003366">Đăng ký...</asp:LinkButton><br />
						<asp:LinkButton ID="linkPasswordRecovery" runat="server" OnClick="linkPasswordRecovery_Click" ForeColor="#003366" Font-Size="Small">Quên mật khẩu...</asp:LinkButton>
					</td>
				</tr>
			</table>
		</div>
		<div id="divUserInfo" runat="server" visible="false">
		<asp:Literal ID="literalUser" runat="server"></asp:Literal>
		<br />
		<asp:LinkButton ID="linkUserUpdateProfile" runat="server" OnClick="linkUserUpdateProfile_Click">Cập nhật hồ sơ</asp:LinkButton><br />
		<asp:LinkButton ID="LinkUserChangePassword" runat="server" OnClick="linkUserChangePassword_Click">Đổi mật khẩu</asp:LinkButton>
		</div>
		<div id="divChangePassword" runat="server">
			<asp:ChangePassword ID="ChangePassword1" runat="server" OnCancelButtonClick="ChangePassword1_CancelButtonClick" OnContinueButtonClick="ChangePassword1_ContinueButtonClick" SuccessText="Thay đổi mật khẩu thành công!" ContinueButtonText="Tiếp tục">
			<ChangePasswordTemplate>
				<table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
					<tr>
						<td>
							<table border="0" cellpadding="0" cellspacing="0">
							<tr>
							<td align="center">
							Thay đổi mật khẩu
							</td>
							</tr>
							<tr>
							<td align="left">
							<asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Mật khẩu:</asp:Label><br />
							<asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
							<asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$ChangePassword1">*</asp:RequiredFieldValidator>
							</td>
							</tr>
							<tr>
							<td align="left">
							<asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Mật khẩu mới:</asp:Label><br />
							<asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
							<asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ctl00$ChangePassword1">*</asp:RequiredFieldValidator>
							</td>
							</tr>
							<tr>
							<td align="left">
							<asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Khẳng định lại:</asp:Label><br />
							<asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
							<asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ctl00$ChangePassword1">*</asp:RequiredFieldValidator>
							</td>
							</tr>
							<tr>
							<td align="left">
							<asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ctl00$ChangePassword1"></asp:CompareValidator>
							</td>
							</tr>
							<tr>
							<td align="left" style="color: Red;">
							<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
							</td>
							</tr>
							<tr>
							<td align="left">
							<asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Thay đổi" ValidationGroup="ctl00$ChangePassword1" />
							<asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Bỏ qua" />
							</td>
							</tr>
							</table>
						</td>
					</tr>
				</table>
			</ChangePasswordTemplate>
			</asp:ChangePassword>
		</div>
		<div id="divPasswordRecovery" runat="server" visible="false">
			<asp:PasswordRecovery ID="PasswordRecovery1" runat="server"  SuccessText="Mật khẩu đã được gửi tới email của bạn">
			<UserNameTemplate>
				<table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
					<tr>
						<td>
							<table border="0" cellpadding="3" cellspacing="0">
								<tr>
									<td align="center" colspan="2">
									Quên mật khẩu?
									</td>
								</tr>
								<tr>
									<td align="center" colspan="2">
									Nhập vào mã người dùng để nhận lại mật khẩu!.
									</td>
								</tr>
								<tr>
									<td align="right">
									<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Mã:</asp:Label>
									</td>
									<td>
									<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
									<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="UserName is required." ToolTip="UserName is required." ValidationGroup="ctl00$PasswordRecovery1">*</asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td align="center" colspan="2" style="color: Red;">
									<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
									</td>
								</tr>
								<tr>
									<td align="right" colspan="2">
									<asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Chấp nhận" ValidationGroup="ctl00$PasswordRecovery1" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</UserNameTemplate>
			</asp:PasswordRecovery>
		</div>
		<!-- / login form -->

		    </div>		
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
