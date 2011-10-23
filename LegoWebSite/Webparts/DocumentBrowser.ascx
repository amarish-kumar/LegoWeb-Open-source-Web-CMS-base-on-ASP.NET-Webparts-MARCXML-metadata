<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocumentBrowser.ascx.cs" Inherits="Webparts_DocumentBrowser" %>
<%@Register TagPrefix="CC" Namespace="LegoWebSite.Controls"%>
<div id="PanelBrowser">
       <div class="title_C">
			 <div class="titleContent_C">
				<div class="titleContent_L">
						<div id="TitleTopMenu"  runat="server" style="padding-left: 10px"></div>
						 <div class="titleContent_R"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <br />
        <div id="showContentDoc" class="Content_C" runat="server">
            <div class="WebPartBox_SilverRoundHeader_Center">
                <div class="WebPartBox_SilverRoundHeader_Left">
                    <div class="WebPartBox_SilverRoundHeader_Right"></div>
                </div>
            </div>
            <div>
                    <div class="clear box_top_search_center-all TabbedPanels"> 
                         <div>
                             <div id="DocumentBrowser" runat="server">
                                 <table cellpadding="0" cellspacing="0" width="100%"  >		
                                  <asp:repeater id="DocumentBrowserRepeater" runat="server" OnItemCommand="DocumentBrowserDataCommand">
                                         <ItemTemplate> 
			                                <tr  class="result_separator">
                                                <td align="center" width="5%" >
                                                    <font size="1"><%#(_DocumentBrowserData.PageNumber - 1) * _DocumentBrowserData.RecordsPerPage + Container.ItemIndex + 1%>.</font>
                                                </td>
                                                <td align="left" valign="top" width="95%" style="padding:5px;">
                                                       <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>
                                                </td> 
                                             </tr>                              
			                                </ItemTemplate>
			                                <AlternatingItemTemplate>
			                                <tr style="background-color:#faf6f6"  class="result_separator">
                                                <td align="center" width="5%" >
                                                    <font size="1"><%#(_DocumentBrowserData.PageNumber - 1) * _DocumentBrowserData.RecordsPerPage + Container.ItemIndex + 1%>.</font>
                                                </td>
                                                <td align="left" valign="top" width="95%" style="padding:5px;">                                                                                            
                                                    <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>
                                                </td>
                                            </tr>                           
			                            </AlternatingItemTemplate>		
                                    <FooterTemplate>
                                            <tr>
                                                <td colspan="2" height="10px"></td>
                                            </tr>
                                            <tr>
                                                <td class="Normal" colspan="2">
                                                    <table class="PagingTable"  border="0" id="paging">
                                                        <tr>
                                                            <td class="Normal" align="right">
                                                               
                                                                  <CC:Navigator id="NavigatorNavigator" MaxPage="<%#_DocumentBrowserData.PageCount%>" PageNumber="<%#_DocumentBrowserData.PageNumber%>" runat="server">
                                                                    &nbsp;&nbsp;&nbsp;<CC:NavigatorItem type="FirstOn" runat="server" ID="Navigatoritem1">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorFirst" runat="server">
		                                                                                            <asp:image ID="Image1" runat="server" skinid="First" ImageUrl="~/images/arr_left.gif"   /></asp:LinkButton>
                                                                                            </CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="FirstOff" runat="server" ID="Navigatoritem2">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:image ID="Image2" runat="server" skinid="FirstOff"  ImageUrl="~/images/arr_left.gif"/></CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="PrevOn" runat="server" ID="Navigatoritem3">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorPrev" runat="server">
		                                                                                            <asp:image ID="Image3" runat="server" skinid="Prev" ImageUrl="~/images/arr_left.gif"  /></asp:LinkButton>
                                                                                            </CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="PrevOff" runat="server" ID="Navigatoritem4">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:image ID="Image4" runat="server" skinid="PrevOff"  /></CC:NavigatorItem>&nbsp; 
                                                                    <CC:Pager id="NavigatorPager" PagerSize="10" runat="server">
	                                                                                            <PageOnTemplate>
                                                                    <asp:LinkButton runat="server" ID="Linkbutton1">
			                                                                                            <%#((PagerItem)Container).PageNumber.ToString()%>
		                                                             </asp:LinkButton>&nbsp;</PageOnTemplate>
	                                                                                            <PageOffTemplate><%#((PagerItem)Container).PageNumber.ToString()%>&nbsp;</PageOffTemplate>
                                                                                            </CC:Pager>of&nbsp;<%#((Navigator)Container).MaxPage.ToString()%>&nbsp; 
                                                                    <CC:NavigatorItem type="NextOn" runat="server" ID="Navigatoritem5">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorNext" runat="server">
		                                                                                            <asp:image ID="Image5" runat="server" skinid="Next" /></asp:LinkButton>
                                                                                            </CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="NextOff" runat="server" ID="Navigatoritem6">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:image ID="Image6" runat="server" skinid="NextOff" ImageUrl="~/images/arr_right.gif" /></CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="LastOn" runat="server" ID="Navigatoritem7">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorLast" runat="server">
		                                                                                            <asp:image ID="Image7" runat="server" skinid="Last" ImageUrl="~/images/arr_right.gif"  /></asp:LinkButton>
                                                                                            </CC:NavigatorItem>
                                                                    <CC:NavigatorItem type="LastOff" runat="server" ID="Navigatoritem8">
	                                                                                            &nbsp;&nbsp;&nbsp;<asp:image ID="Image8" runat="server" skinid="LastOff"  ImageUrl="~/images/arr_right.gif" /></CC:NavigatorItem></CC:Navigator>														
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                          </tr>     
                                    </FooterTemplate>                                
                                </asp:repeater>  
                                </table>
                              </div>
                        </div>
                    </div>
              </div>
            <div class="WebPartBox_SilverRoundBottom_Center">
                <div class="WebPartBox_SilverRoundBottom_Left">
                    <div class="WebPartBox_SilverRoundBottom_Right"></div>
                </div>
            </div>
        </div>
</div>
