﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRoleManager.ascx.cs" Inherits="LgwUserControls_UserRoleManager" %>
<table style="width:100%">
<tr>
<td style="width:50%; vertical-align:top" >
<%--location list--%>
    <fieldset class="adminform">
		<legend><%=Resources.strings.List_Text %></legend>
<table class="adminlist" cellspacing="1">   									
					<asp:repeater id="roleManagerRepeater" runat="server" OnItemCommand="repeater_OnItemCommand">
							<HeaderTemplate>
							<thead>
							<tr>
							<th class="title"><%=Resources.strings.Line_Text %></th>
							<th class="title"><%=Resources.strings.Role_Text %></th>		
							<th class="title"><%=Resources.strings.Command_Text %></th>						
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                            
                                <td align="center">                                
                                <%#Container.ItemIndex + 1%>
                                </td>                                                                
                                <td align="center">                                
                                  <%# Container.DataItem %>
                                </td>
                                <td align="center">                                
                                  <asp:LinkButton ID="linkEdit" Runat="server" CommandName="edit" CommandArgument='<%# Container.DataItem %>'><%=Resources.strings.Edit_Text %></asp:LinkButton>                               
                                </td>
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#Container.ItemIndex + 1%>
                                </td>                                                                
                                <td align="center">                                
                                  <%# Container.DataItem %>
                                </td>
                                <td align="center">                                
                                  <asp:LinkButton ID="linkEdit" Runat="server" CommandName="edit" CommandArgument='<%# Container.DataItem %>'><%=Resources.strings.Edit_Text %></asp:LinkButton>                               
                                </td>
                            </tr>                            
							</AlternatingItemTemplate>
							<FooterTemplate>
							<tr>
							<td colspan="3" valign="middle" align="right">
							    <asp:LinkButton ID="linkAddNew" runat="server" Text="<%$ Resources:Strings,Add.Text%>" OnClick="linkAddNew_OnClick"></asp:LinkButton>   
							</td>
							</tr>
							
							</FooterTemplate>

						</asp:repeater>
						
						</table>

</fieldset>
</td>
<td style="width:50%; vertical-align:top">
<div id="divAddUpdateUserRole" visible="false" runat="server">
<%--location add/update--%>
    <fieldset class="adminform">
		<legend><%=Resources.strings.Details_Text %></legend>
			<table class="admintable" cellspacing="1" width="100%">
			<tbody>
        <tr>
            <td width="150px" class="key"><label for="name"><%=Resources.strings.Role_Text %>:</label></td>
            <td>
                <asp:TextBox ID="txtUserRoleCode" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserRoleCodeRequired" runat="server" ControlToValidate="txtUserRoleCode" ErrorMessage="Role name is required!"
                     ToolTip="Role name is required!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="UserRoleInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>                                                           
        <tr>
            <td colspan="2">
                <asp:Literal ID="errorMessage" runat="server" />
            </td>
        </tr>
        <tr>        
            <td colspan="2" align="right">
                <asp:Button ID="btnDelete" 
                    OnClientClick="return confirm('Are you sure to remove item?')" 
                    runat="server" Text="<%$Resources:Strings,Delete.Text %>" onclick="btnDelete_Click" />&nbsp;       
                <asp:Button ID="btnOk" runat="server"  Text="<%$Resources:Strings,Save.Text %>" onclick="btnOk_Click" />&nbsp;       
                <asp:Button ID="btnCancel" runat="server"  Text="<%$Resources:Strings,Cancel.Text %>" 
                    onclick="btnCancel_Click" />&nbsp;       
            </td>
        </tr>
                                     
            </tbody>
            </table>
		</fieldset>
</div>
</td>
</tr>
</table>