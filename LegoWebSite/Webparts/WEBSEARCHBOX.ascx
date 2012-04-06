<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WEBSEARCHBOX.ascx.cs" Inherits="Webparts_WEBSEARCHBOX" %>

<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tbody>
<tr>
<td valign="middle" align="right">
<input id="txtKeywords" class="websearch_textbox" onkeypress="return trapEnterKey(this.value,event);" onblur="if (this.value=='') this.value='Nhập vào từ khóa tìm kiếm';" onfocus="if (this.value=='Nhập vào từ khóa tìm kiếm') this.value='';" value=""/>       
</td>
<td valign="middle" align="left" style="width:26px">
<a href="javascript:searchEngine();" style="width: 25px; height: 25px;" class="websearch_button"></a>
</td>
</tr>
</tbody>
</table>       
<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>
