<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CONTENTTAB02STYLES.ascx.cs" Inherits="Webparts_CONTENTTAB02STYLES" %>

<div id="tabs_container"> 
    <div class="block_title">
		 <span class="left"></span>
		 <span class="center">
			<h1>
			<asp:Literal ID="litTabControlTitle" runat="server"></asp:Literal>			
			</h1>
		</span>
		 <span class="right"></span>
	</div>
	<asp:Literal ID="litTabs" runat="server">
	
	</asp:Literal>
	<div class="icon-vertical-line"></div> 
</div>
