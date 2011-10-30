<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaContentPreview.ascx.cs" Inherits="LgwUserControls_MetaContentPreview" %>
<%@Register TagPrefix="CC" Namespace="LegoWebAdmin.Controls"%>


<table width="100%" cellpadding="2" cellspacing="2">
<tbody>
<tr>
<td align="right" valign="middle"><b><%=Resources.strings.Template_Text %>:</b></td>
<td align="left" valign="middle" style="width:120px">
<asp:DropDownList ID="dpTemplateNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpTemplateNames_SelectedIndexChanged"></asp:DropDownList>
</td> 
</tr>
</tbody>
</table>


		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">                                                                                                                                 
                            
                    <asp:Literal ID="litContentViewer" runat="server"></asp:Literal>
                                                                                   																									
			        <div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>



				
