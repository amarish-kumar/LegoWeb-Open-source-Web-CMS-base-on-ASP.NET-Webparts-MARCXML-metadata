<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="UpgradeDatabase.aspx.cs" Inherits="UpgradeDatabase" Title="KIPOSADMIN: Nâng cấp CSDL" %>
<%@ Register src="UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


	<div id="header-box">
        <!--menu bar go here -->
        <uc1:AdminMenuBarActive ID="AdminMenuBarActive1" runat="server" />
		<div class="clr"></div>
	</div>
	

	   		<div class="clr"></div>
<div id="content-box">		
	<div class="border">
	  <div class="padding">		
	  
<div id="toolbar-box">
   			<div class="t">
				<div class="t">
					<div class="t"></div>
				</div>
			</div>
			<div class="m">
				<div class="toolbar" id="toolbar">

</div>
<div class="header icon-48-info">
Nâng cấp CSDL
</div>

				<div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
  		</div>  		
	  
	  <div class="clr"></div>
	  
		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">
                                             
                                             <center>
            <table width="540px" border="0" cellpadding="2" cellspacing="2">
            <tr>
                <td style="width:460px">
                <asp:TextBox ID="txtSqlScripts" runat="server" TextMode="MultiLine" Rows="10" Width="100%">
                </asp:TextBox>
                </td>
                <td valign="middle">
                <asp:Button ID="btnRun" 
                        OnClientClick="return confirm('Chạy kịch bản SQL có thể làm hỏng CSDL của bạn, bạn có chắc chắn muốn chạy?')" 
                        Text="Run" runat="server" onclick="btnRun_Click"/>
                </td>
            </tr>
            <tr>
            <td colspan="2">
            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
            </td>
            </tr>
            </table>
									</center>
			        <div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
    </div>
    </div>
		<div class="clr"></div>
	</div>
</asp:Content>

