<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserManager.ascx.cs" Inherits="UserControls_UserManager" %>
<%@Register TagPrefix="CC" Namespace="LegoWeb.Controls"%>
<table width="100%" cellpadding="2" cellspacing="2">
<tbody>
<tr>
<td align="right" valign="middle" style="width:200px"><font color="gray"><i>Lọc tài khoản(% thay cho ký tự bất kỳ)</i></font></td>
<td align="right" valign="middle" style="width:80px"><b> Tên truy cập:</b></td>
<td align="left" valign="middle" style="width:20px"><asp:RadioButton ID="RadioUser" GroupName="FindCheck" runat="server" /></td>
<td align="left" valign="middle" style="width:100px"><asp:TextBox ID="txtUserLogin" runat="server"></asp:TextBox></td>
<td align="right" valign="middle" style="width:60px"><b> Email:</b></td>
<td align="left" valign="middle" style="width:20px"><asp:RadioButton ID="RadioEmail" GroupName="FindCheck" runat="server" /></td>
<td align="left" valign="middle" style="width:100px"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
<td align="right" valign="middle" style="width:60px"><b> Vai trò:</b></td>
<td align="left" valign="middle" style="width:20px"><asp:RadioButton ID="RadioRole" GroupName="FindCheck" runat="server" /></td>
<td align="left" valign="middle" style="width:100px"><asp:dropdownlist ID="dropdownListRoles" runat="server" oninit="dropdownListRoles_Init"></asp:dropdownlist></td>
<td align="right" valign="middle">
   <asp:Button ID="cmdClear" class="searchbutton" runat="server" Text="Bỏ lọc" onclick="cmdClear_Click" />  <asp:Button ID="cmdSearch" class="searchbutton" runat="server" Text="Tìm" onclick="cmdSearch_Click" />
</td>
</tr>
</tbody>
</table>
					
	<table class="adminlist" cellspacing="1">   									
					<asp:repeater id="userManagerRepeater" runat="server" OnItemCommand="userManagerDataCommand" OnItemDataBound="userManagerItemDataBound">
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th width="3%" class="title">
							<asp:CheckBox ID="chkSelectAll" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" />
							</th>
							<th class="title">Tên</th>							
							<th class="title">Email</th>
							<th class="title">Ghi chú</th>
							<th class="title">Duyệt</th>
							<th class="title">Kích hoạt</th>
							<th class="title">Ngày tạo</th>
							<th class="title">Hoạt động cuối</th>
							<th class="title">Đang Online</th>
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                            
                                <td align="center">                                
                                <%#(_userManagerData.PageNumber - 1) * _userManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="left">                                
                                <a href="UserUpdate.aspx?User=<%# DataBinder.Eval(Container.DataItem, "UserName") %>"><%# DataBinder.Eval(Container.DataItem, "UserName")%></a>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Email")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Comment")%>                                
                                </td>
                                <td align="center">          
                                 <asp:Image ID="imgApproved" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsApproved")%>'/>  
                                 <asp:Image ID="imgNotApproved" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsApproved")%>'/>  
                                </td>
                                <td align="center">    
                                <asp:Image ID="imgLockedOut" runat="server" SkinID="Stop" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsLockedOut")%>'/>                                                                                          
                                <asp:Image ID="imgNotLockedOut" runat="server" SkinID="Tick" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsLockedOut")%>'/>                                                                                          
                                </td>         
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "CreationDate")%>                                
                                </td>     
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LastActivityDate")%>                                
                                </td>     
                                <td align="center">                                    
                                <asp:Image ID="imgIsOnline" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsOnline")%>'/>                                                                                          
                                </td>                                                                                                                    
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#(_userManagerData.PageNumber - 1) * _userManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />                                                                                                
                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' Visible="false" />
                                </td> 
                                <td align="left">                                
                                <a href="UserUpdate.aspx?User=<%# DataBinder.Eval(Container.DataItem, "UserName") %>"><%# DataBinder.Eval(Container.DataItem, "UserName")%></a>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Email")%>                                
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "Comment")%>                                
                                </td>
                                <td align="center">          
                                 <asp:Image ID="imgApproved" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsApproved")%>'/>  
                                 <asp:Image ID="imgNotApproved" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsApproved")%>'/>  
                                </td>
                                <td align="center">    
                                <asp:Image ID="imgLockedOut" runat="server" SkinID="Stop" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsLockedOut")%>'/>                                                                                          
                                <asp:Image ID="imgNotLockedOut" runat="server" SkinID="Tick" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IsLockedOut")%>'/>                                                                                          
                                </td>              
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "CreationDate")%>                                
                                </td>     
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LastActivityDate")%>                                
                                </td>    
                                <td align="center">                                    
                                <asp:Image ID="imgIsOnline" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IsOnline")%>'/>                                                                                          
                                </td>                                                                                   
                            </tr>                            
							</AlternatingItemTemplate>
							<FooterTemplate>
							</tbody>		
							<tfoot>					
							<tr>		
							<td colspan="3" align="center">
							    Trình bày:
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
							<td colspan="7" align="center">		
							&nbsp;	
							<div runat="server" id="divNavigator" visible="<%#_userManagerData.PageCount>1%>" >									
									<CC:Navigator id="NavigatorNavigator" MaxPage="<%#_userManagerData.PageCount%>" PageNumber="<%#_userManagerData.PageNumber%>" runat="server">
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


 