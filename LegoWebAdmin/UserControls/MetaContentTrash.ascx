<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetaContentTrash.ascx.cs" Inherits="LgwUserControls_MetaContentTrash" %>
<%@Register TagPrefix="CC" Namespace="LegoWebAdmin.Controls"%>

<table class="adminlist" cellspacing="1">   									
					<asp:repeater id="metaContentTrashrRepeater" runat="server" >
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th width="3%" class="title">
							<asp:CheckBox ID="chkSelectAll" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" /></th>
							<th class="title"><%=Resources.strings.ID_Text %></th>							
							<th class="title"><%=Resources.strings.Title_Text %></th>
							<th class="title"><%=Resources.strings.Language_Text %></th>
							<th class="title"><%=Resources.strings.Category_Text %></th>
							<th class="title"><%=Resources.strings.Modifier_Text %></th>
							<th class="title"><%=Resources.strings.ModifiedDate_Text %></th>	
							<th class="title"><%=Resources.strings.AccessRoles_Text %></th>	
							<th class="title"><%=Resources.strings.Hit_Text %></th>							
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
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtMetaContentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="MetaContentEditor.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                                                                              
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_VI_TITLE")%>                                
                                </td>                                                                                                                                                                     
                                <td align="left">                                
                                <%# String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "MODIFIED_USER").ToString()) ? DataBinder.Eval(Container.DataItem, "CREATED_USER") : DataBinder.Eval(Container.DataItem, "MODIFIED_USER")%>                                
                                </td>                                                                   
                                <td align="center">                                
                                <%# ((DateTime)DataBinder.Eval(Container.DataItem, "MODIFIED_DATE")).ToString("dd.MM.yy")%>                                
                                </td>   
                                <td align="left">                                
                                <%# int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 0 ? Resources.strings.Any_Text : (int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 1 ? Resources.strings.Registered_Text : DataBinder.Eval(Container.DataItem, "ACCESS_ROLES"))%>                                
                                </td>                                                                                                  
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
                                </td>                                                                  
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtMetaContentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="MetaContentEditor.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                                                                              
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "CATEGORY_VI_TITLE")%>                                
                                </td>                                                                                                                                                                     
                                <td align="left">                                
                                <%# String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "MODIFIED_USER").ToString()) ? DataBinder.Eval(Container.DataItem, "CREATED_USER") : DataBinder.Eval(Container.DataItem, "MODIFIED_USER")%>                                
                                </td>                                                                   
                                <td align="center">                                
                                <%# ((DateTime)DataBinder.Eval(Container.DataItem, "MODIFIED_DATE")).ToString("dd.MM.yy")%>                                
                                </td>   
                                <td align="left">                                
                                <%# int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 0 ? Resources.strings.Any_Text : (int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 1 ? Resources.strings.Registered_Text : DataBinder.Eval(Container.DataItem, "ACCESS_ROLES"))%>    
                                </td>                                                                                                  
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
                                </td>                                                                                                                                                                                                                               
                            </tr>                            
							</AlternatingItemTemplate>

						</asp:repeater>
						
						</table>
						
