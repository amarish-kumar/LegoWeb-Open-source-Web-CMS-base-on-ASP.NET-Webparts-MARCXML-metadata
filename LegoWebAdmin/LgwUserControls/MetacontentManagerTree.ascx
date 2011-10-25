<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MetacontentManagerTree.ascx.cs" Inherits="LgwUserControls_MetacontentManagerTree" %>
<%@Register TagPrefix="CC" Namespace="LegoWeb.Controls"%>

<table width="100%" cellpadding="0" cellspacing="2">
    <tr>
        <td style="width:225px" valign="top">
        <fieldset class="adminform">
		<legend>Cấu trúc nội dung</legend>		
		<asp:dropdownlist ID="dropSections" runat="server" oninit="dropSections_Init" AutoPostBack="true" OnSelectedIndexChanged="dropSections_SelectedIndexChanged"></asp:dropdownlist>
		<br />
		
            <asp:TreeView 
                ID="CategoryTree" 
                runat="server" 
                ShowLines="True"
                DataSourceID="CategoryXmlData"
                Font-Names="Verdana;Tahoma"          
                Font-Size="Small"
                OnSelectedNodeChanged="CategoryTree_SelectedNodeChanged"
                EnableClientScript="true" 
                EnableViewState="true"
                >
               <DataBindings>                
                <asp:TreeNodeBinding 
                    DataMember="category" 
                    ValueField="CATEGORY_ID" 
                    TextField="CATEGORY_VI_TITLE" >
                </asp:TreeNodeBinding>                                                               
             </DataBindings>    
                <SelectedNodeStyle BackColor="#E0E0E0" BorderColor="Silver" />
              </asp:TreeView>
            <asp:XmlDataSource ID="CategoryXmlData" EnableCaching="false" XPath="/*/*" Runat="server"></asp:XmlDataSource>  
		
		</fieldset>
        </td>
        <td valign="top" style="width:100%">
        <span style="color:#CC9900; font-size:small; font-style:oblique">
        <asp:Literal ID="ltCategoryInfo" runat="server"></asp:Literal>
        </span>
        <span style="color:red; font-size:small; font-style:oblique">
        <asp:Literal ID="ltErrorMessage" runat="server"></asp:Literal>
        </span>        
        <br />
            <table class="adminlist" cellspacing="1">   			
				<asp:repeater id="metaContentManagerRepeater" runat="server" OnItemCommand="metaContentManagerDataCommand" OnItemDataBound="metaContentManagerItemDataBound">
							<HeaderTemplate>
							<thead>
							<tr>
							<th width="2%" class="title">#</th>
							<th width="3%" class="title">
							<asp:CheckBox ID="chkSelectAll" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" /></th>
							<th class="title">ID</th>							
							<th class="title">Tiêu đề</th>
							<th class="title">Ngôn ngữ</th>
							<th class="title">Công bố</th>
							<th class="title">							
							<asp:LinkButton ID="linkOrderUp" runat="server" OnClick="linkOrderUp_OnClick">
							<span class="icon-16-uparrow">
							</span>
							</asp:LinkButton>							
							<asp:LinkButton ID="linkOrderDown" runat="server" OnClick="linkOrderDown_OnClick">
							<span class="icon-16-downarrow">
							</span>
							</asp:LinkButton>							
							</th>
							<th class="title">Chuyên mục</th>
							<th class="title">Cập nhật</th>
							<th class="title">Ngày</th>	
							<th class="title">Xem chi tiết</th>	
							<th class="title">Lượt</th>							
							</tr>
							</thead>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
                            <tr class="row0">                            
                                <td align="center">                                
                                <%#(_metaContentManagerData.PageNumber - 1) * _metaContentManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtMetaContentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="MetaContentAddUpdate.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                 
                                <td align="center">          
                                 <asp:Image ID="imgIsPublic" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IS_PUBLIC")%>'/>  
                                 <asp:Image ID="imgIsNotPublic" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IS_PUBLIC")%>'/>  
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "ORDER_NUMBER")%>                                
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
                                <%# int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 0 ? "Bất kỳ" : (int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 1 ? "Đăng nhập" : DataBinder.Eval(Container.DataItem, "ACCESS_ROLES"))%>                                
                                </td>                                  
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
                                </td>                                                                  
                            </tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
							 <tr class="row1">
                                <td align="center">                                
                                <%#(_metaContentManagerData.PageNumber - 1) * _metaContentManagerData.RecordsPerPage + Container.ItemIndex + 1%>
                                </td>   
                                <td align="center">                                
                                <asp:CheckBox runat="server" title="Chọn" ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" /> 
                                <asp:TextBox ID="txtMetaContentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>' Visible="false" />                                  
                                </td>                                                               
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID")%>                             
                                </td>
                                <td align="left">                                
                                <a href="MetaContentAddUpdate.aspx?meta_content_id=<%# DataBinder.Eval(Container.DataItem, "META_CONTENT_ID") %>"><%# DataBinder.Eval(Container.DataItem, "META_CONTENT_TITLE")%></a>                                
                                </td>
                                <td align="center">                                
                                <%# DataBinder.Eval(Container.DataItem, "LANG_CODE")%>                                
                                </td>                                                                               
                                <td align="center">          
                                 <asp:Image ID="imgIsPublic" runat="server" SkinID="Tick" Visible='<%#(bool)DataBinder.Eval(Container.DataItem, "IS_PUBLIC")%>'/>  
                                 <asp:Image ID="imgIsNotPublic" runat="server" SkinID="Stop" Visible='<%#!(bool)DataBinder.Eval(Container.DataItem, "IS_PUBLIC")%>'/>  
                                </td>
                                <td align="left">                                
                                <%# DataBinder.Eval(Container.DataItem, "ORDER_NUMBER")%>                                
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
                                <%# int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 0 ? "Bất kỳ" : (int.Parse(DataBinder.Eval(Container.DataItem, "ACCESS_LEVEL").ToString()) == 1 ? "Đăng nhập" : DataBinder.Eval(Container.DataItem, "ACCESS_ROLES"))%>                                
                                </td>                                                                  
                                <td align="right">                                
                                <%# DataBinder.Eval(Container.DataItem, "READ_COUNT")%>                                
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
							<td colspan="9" align="center">			
							&nbsp;
							<div runat="server" id="divNavigator" visible="<%#_metaContentManagerData.PageCount>1%>" >									
									<CC:Navigator id="NavigatorNavigator" MaxPage="<%#_metaContentManagerData.PageCount%>" PageNumber="<%#_metaContentManagerData.PageNumber%>" runat="server">
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<CC:NavigatorItem type="FirstOn" runat="server" ID="Navigatoritem1">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorFirst" runat="server">
												<asp:image ID="Image1" runat="server" skinid="First"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="FirstOff" runat="server" ID="Navigatoritem2">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:image ID="Image2" runat="server" skinid="FirstOff"  /></CC:NavigatorItem>
 <CC:NavigatorItem type="PrevOn" runat="server" ID="Navigatoritem3">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorPrev" runat="server">
												<asp:image ID="Image3" runat="server" skinid="Prev"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="PrevOff" runat="server" ID="Navigatoritem4">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:image ID="Image4" runat="server" skinid="PrevOff"  /></CC:NavigatorItem>&nbsp; 
<CC:Pager id="NavigatorPager" Style="Centered" PagerSize="10" runat="server">
											<PageOnTemplate>
<asp:LinkButton runat="server" ID="Linkbutton1">
													<%#((PagerItem)Container).PageNumber.ToString()%>
												</asp:LinkButton>&nbsp;</PageOnTemplate>
											<PageOffTemplate><%#((PagerItem)Container).PageNumber.ToString()%>&nbsp;</PageOffTemplate>
										</CC:Pager>of&nbsp;<%#((Navigator)Container).MaxPage.ToString()%>&nbsp; 
<CC:NavigatorItem type="NextOn" runat="server" ID="Navigatoritem5">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorNext" runat="server">
												<asp:image ID="Image5" runat="server" skinid="Next"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="NextOff" runat="server" ID="Navigatoritem6">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:image ID="Image6" runat="server" skinid="NextOff"  /></CC:NavigatorItem>
 <CC:NavigatorItem type="LastOn" runat="server" ID="Navigatoritem7">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorLast" runat="server">
												<asp:image ID="Image7" runat="server" skinid="Last"  /></asp:LinkButton>
										</CC:NavigatorItem>
 <CC:NavigatorItem type="LastOff" runat="server" ID="Navigatoritem8">
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:image ID="Image8" runat="server" skinid="LastOff"  /></CC:NavigatorItem></CC:Navigator>
													
	                        </div>
	                        &nbsp;
							</td>													
							</tr>
							</tfoot>
							</FooterTemplate>

						</asp:repeater>
			</table>
        </td>
    </tr>
</table>


						
