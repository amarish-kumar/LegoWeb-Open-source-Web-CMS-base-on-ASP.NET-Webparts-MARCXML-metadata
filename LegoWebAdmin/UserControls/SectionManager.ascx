﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionManager.ascx.cs" Inherits="LgwUserControls_SectionManager" %>
<%@Register TagPrefix="CC" Namespace="LegoWebAdmin.Controls"%>
<table class="adminlist" cellspacing="1">   									
					<asp:repeater id="sectionManagerRepeater" runat="server" OnItemCommand="sectionManagerDataCommand" OnItemDataBound="sectionManagerItemDataBound">
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th width="3%" class="title">
							<asp:CheckBox ID="chkSelectAll" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" /></th>
							<th class="title"><%=Resources.strings.ID_Text %></th>							
							<th class="title"><%=Resources.strings.VietnameseTitle_Text %></th>
							<th class="title"><%=Resources.strings.EnglishTitle_Text %></th>
							<th class="title"><%=Resources.strings.Details_Text %></th>
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                            
                                <td align="center">                                
                                <%#(_sectionManagerData.PageNumber - 1) * _sectionManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtSectionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SECTION_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "SECTION_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="SectionAddUpdate.aspx?section_id=<%# DataBinder.Eval(Container.DataItem, "SECTION_ID") %>"><%# DataBinder.Eval(Container.DataItem, "SECTION_VI_TITLE")%></a>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "SECTION_EN_TITLE")%>                 
                                </td>
                                <td align="center">
                                <a href="CategoryManager.aspx?section_id=<%# DataBinder.Eval(Container.DataItem, "SECTION_ID") %>"><span class="icon-16-details"></span></a>                                
                                </td>
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#(_sectionManagerData.PageNumber - 1) * _sectionManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtSectionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SECTION_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "SECTION_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="SectionAddUpdate.aspx?section_id=<%# DataBinder.Eval(Container.DataItem, "SECTION_ID") %>"><%# DataBinder.Eval(Container.DataItem, "SECTION_VI_TITLE")%></a>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "SECTION_EN_TITLE")%>                 
                                </td>
                                <td align="center">
                                <a href="CategoryManager.aspx?section_id=<%# DataBinder.Eval(Container.DataItem, "SECTION_ID") %>"><span class="icon-16-details"></span></a>                                
                                </td>                                
                            </tr>                            
							</AlternatingItemTemplate>
							<FooterTemplate>
							</tbody>		
							<tfoot>					
							<tr>																					
							<td colspan="4" align="center">
							    <%=Resources.strings.Display_Text %>:
			                    <asp:DropDownList ID="dropRecordPerPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropRecordPerPage_SelectedIndexChanged">
							        <asp:ListItem Value="5" Text="5"></asp:ListItem>
							        <asp:ListItem Value="10" Text="10"></asp:ListItem>
							        <asp:ListItem Value="15" Text="15"></asp:ListItem>
							        <asp:ListItem Value="20" Text="20"></asp:ListItem>
							        <asp:ListItem Value="25" Text="25"></asp:ListItem>
							        <asp:ListItem Value="30" Text="30"></asp:ListItem>
							        <asp:ListItem Value="50" Text="50"></asp:ListItem>
							        <asp:ListItem Value="100" Text="100"></asp:ListItem>
							    </asp:DropDownList> 
							 </td>							
							 <td colspan="2" align="center">	
							 &nbsp;			
							<div runat="server" id="divNavigator" visible="<%#_sectionManagerData.PageCount>1%>" >									
									<CC:Navigator id="NavigatorNavigator" MaxPage="<%#_sectionManagerData.PageCount%>" PageNumber="<%#_sectionManagerData.PageNumber%>" runat="server">
 &nbsp;&nbsp;<CC:NavigatorItem type="FirstOn" runat="server" ID="Navigatoritem1">
											&nbsp;&nbsp;<asp:LinkButton id="NavigatorFirst" runat="server">
												<asp:image ID="Image1" runat="server" skinid="First"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="FirstOff" runat="server" ID="Navigatoritem2">
											&nbsp;&nbsp;<asp:image ID="Image2" runat="server" skinid="FirstOff"  /></CC:NavigatorItem>
 <CC:NavigatorItem type="PrevOn" runat="server" ID="Navigatoritem3">
											&nbsp;&nbsp;<asp:LinkButton id="NavigatorPrev" runat="server">
												<asp:image ID="Image3" runat="server" skinid="Prev"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="PrevOff" runat="server" ID="Navigatoritem4">
											&nbsp;&nbsp;<asp:image ID="Image4" runat="server" skinid="PrevOff"  /></CC:NavigatorItem>&nbsp; 
<CC:Pager id="NavigatorPager" Style="Centered" PagerSize="10" runat="server">
											<PageOnTemplate>
<asp:LinkButton runat="server" ID="Linkbutton1">
													<%#((PagerItem)Container).PageNumber.ToString()%>
												</asp:LinkButton>&nbsp;</PageOnTemplate>
											<PageOffTemplate><%#((PagerItem)Container).PageNumber.ToString()%>&nbsp;</PageOffTemplate>
										</CC:Pager>of&nbsp;<%#((Navigator)Container).MaxPage.ToString()%>&nbsp; 
<CC:NavigatorItem type="NextOn" runat="server" ID="Navigatoritem5">
											&nbsp;&nbsp;<asp:LinkButton id="NavigatorNext" runat="server">
												<asp:image ID="Image5" runat="server" skinid="Next"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="NextOff" runat="server" ID="Navigatoritem6">
											&nbsp;&nbsp;<asp:image ID="Image6" runat="server" skinid="NextOff"  /></CC:NavigatorItem>
 <CC:NavigatorItem type="LastOn" runat="server" ID="Navigatoritem7">
											&nbsp;&nbsp;<asp:LinkButton id="NavigatorLast" runat="server">
												<asp:image ID="Image7" runat="server" skinid="Last"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="LastOff" runat="server" ID="Navigatoritem8">
											&nbsp;&nbsp;<asp:image ID="Image8" runat="server" skinid="LastOff"  /></CC:NavigatorItem></CC:Navigator>
													
	                        </div>
	                         &nbsp;
							</td>													
							</tr>
							</tfoot>
							</FooterTemplate>

						</asp:repeater>
						
						</table>