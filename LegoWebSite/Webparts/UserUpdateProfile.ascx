<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserUpdateProfile.ascx.cs" Inherits="Webparts_UserUpdateProfile" %>

<div id="grey-header-title-box"><%--grey-header-title-box--%>
	<div class="t">
 		<div class="t">
			<div class="t"></div>
 		</div>
	</div>
    <div class="title"> HỒ SƠ THÀNH VIÊN DIỄN ĐÀN</div>		    						
	<div class="m">			
	<div class="clearfix">
       
       
             <table width="100%">
             <tr>
             <td valign="top">
                         <table width="100%">
            <tr>
            <td align="right">
                Email:
            </td>
            <td align="left">
                <asp:Label ID="labelEmail" runat="server"></asp:Label>
            </td>            
            </tr>
            <tr>
            <td align="right">
                Ký danh:
            </td>
            <td align="left">
                <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
            </td>            
            </tr>
            <tr>
            <td align="right">
                Ảnh đại diện:
            </td>
            <td align="left">
                <asp:FileUpload ID="fileUploadAvatar" runat="server" />
            </td>            
            </tr>
              </table>              
             </td>

             <td valign="top">
             <asp:Image ID="ImageAvatar" ImageUrl="" runat="server"  style="max-height:200px; max-width:200px"/>           
             </td>                          
             </tr>
             <tr>
             <td colspan="2">
             <asp:Label ID="CustomErrorMessage" runat="server" Visible="false"></asp:Label>
             </td>
             </tr>             
             <tr>             
             <td colspan="2" align="right">
             <asp:Button ID="btnOk" runat="server" Text="Chấp nhận" onclick="btnOk_Click" />
             <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" onclick="btnCancel_Click" />
             </td>
             </tr>
             </table>                


    </div>		
	        <div class="clr"></div>
	</div>
			
	<div class="b">
		<div class="b">
			<div class="b"></div>
		</div>
	</div>
</div>
