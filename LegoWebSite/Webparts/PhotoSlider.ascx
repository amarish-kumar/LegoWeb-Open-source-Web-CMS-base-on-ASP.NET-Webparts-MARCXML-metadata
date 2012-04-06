<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PHOTOSLIDER.ascx.cs" Inherits="Webparts_PHOTOSLIDER" %>
<style type='text/css'> 
body{
	font-family:arial
}
 
.clear {
	clear:both
}
 
#gallery {
	position:relative;
	height:360px;
	z-index:1;
}
	#gallery a {
		float:left;
		position:absolute;
	}
	
	#gallery a img {
		border:none;
	}
	
	#gallery a.show {
		z-index:2;
	}
 
	#gallery .caption {
		z-index:200; 
		background-color:#000; 
		color:#ffffff; 
		height:65px; 
		width:100%; 
		position:absolute;
		bottom:0;
	}
 
	#gallery .caption .content {
		margin:5px
	}
	
	#gallery .caption .content h3 {
		margin:0;
		padding:0;
		color:#1DCCEF;
	}
	
 
</style>

<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>
<asp:Literal ID="litContent" runat="server"></asp:Literal>
<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>

