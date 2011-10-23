<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaContentPreview.ascx.cs" Inherits="UserControls_MetaContentPreview" %>
<%@Register TagPrefix="CC" Namespace="LegoWeb.Controls"%>

<table width="100%" cellpadding="2" cellspacing="2">
<tbody>
<tr>
<td align="right" valign="middle"><b>Khuôn mẫu:</b></td>
<td align="left" valign="middle" style="width:120px">
<asp:DropDownList ID="dpTemplateNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpTemplateNames_SelectedIndexChanged"></asp:DropDownList>
</td> 
</tr>
</tbody>
</table>

<fieldset class="adminform">
<legend>Nội dung</legend>
<div style="clear:both"/>
<div id="divPreviewer" runat="server">



</div>
</fieldset>
				
