<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Theme="legoweb" Title="KIPOS WEB ADMIN:Đăng nhập" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LEGOWEB ADMIN</title>
    <link href="App_Themes/kipos.ico" rel="shortcut icon" type="image/x-icon" />
     
<!--[if IE 7]>
<link href="App_Themes/ie7.css" rel="stylesheet" type="text/css" />
<![endif]-->
 
<!--[if lte IE 6]>
<link href="App_Themes/ie6.css" rel="stylesheet" type="text/css" />
<![endif]-->

</head>
<body id="minwidth-body">
    <form id="form1" runat="server"> 
    <div id="border-top" class="h_green">
		<div>
			<div>
				<span class="title">KIPOS</span>
			</div>
		</div>
	</div>
    
    	<div id="content-box">
		<div class="padding">
			<div id="element-box" class="login">
				<div class="t">
					<div class="t">
						<div class="t"></div>
					</div>
				</div>
				<div class="m">
 
					<h1>KIPOS Quản trị Web</h1>
					
							<div id="section-box">
			<div class="t">
				<div class="t">
					<div class="t"></div>
		 		</div>
	 		</div>
			<div class="m">
			
                <asp:Login ID="Login1" runat="server">
                    <LayoutTemplate>
                        <table border="0" cellpadding="2" cellspacing="2" 
                            style="border-collapse:collapse;">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td align="center" colspan="2">
                                                Đăng nhập</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                                    CssClass="modlgn_username">Người dùng:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="inputbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                    ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                                    CssClass="modlgn_passwd">Mật khẩu:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" CssClass="inputbox" 
                                                    TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                                    ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="RememberMe" runat="server" Text="Ghi nhớ tôi lần sau." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color:Red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Đăng nhập" 
                                                    ValidationGroup="Login1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
			
			
				<div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
		 			<div class="b"></div>
				</div>
			</div>
		</div>
		
					<p> Sử dụng người dùng và mật khẩu hợp lệ để truy cập vào chức năng Quản trị phía sau.</p>
					<p>
						<a href="Default.aspx">Trở về Trang chủ</a>
					</p>
					<div id="lock"></div>
					<div class="clr"></div>
				</div>
				<div class="b">
					<div class="b">
						<div class="b"></div>
					</div>
				</div>
			</div>
			<noscript>
				Warning! JavaScript must be enabled for proper operation of the Administrator back-end.			</noscript>
			<div class="clr"></div>
		</div>
	</div>
  
	<div id="border-bottom">
	<div>
	<div>
	</div>
	</div>
	</div>
	<div id="footer">
	    <p class="copyright">
		    <a href="http://www.legoweb.org" target="_blank"> Copyright &copy;HIENDAI SOFTWARE COMPANY 2004-2010</a>
	    </p>
    </div>  
    </form>
</body>
</html>
