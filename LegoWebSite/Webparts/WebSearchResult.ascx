<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebSearchResult.ascx.cs" Inherits="Webparts_WebSearchResult" %>
<%@Register TagPrefix="CC" Namespace="LegoWebSite.Controls"%>
<div style=" color:#2D427B; border:1px solid #CDCDCD;">
    <div style="padding:10px;background-color:#FFFFFF; font-size:12px;margin:auto; ">
         <img src="images/search01.gif" style="vertical-align:middle;">
          <asp:Label ID="labelMessages" runat="server" Text=""></asp:Label>
    </div>
</div>
 <br />
<div  id="WebPartBox_OutBoundContainer">
    <div class="WebPartBox_SilverRoundHeader_Center">
        <div class="WebPartBox_SilverRoundHeader_Left">
            <div class="WebPartBox_SilverRoundHeader_Right"></div>
        </div>
    </div>
            <div class="clear box_top_search_center-all TabbedPanels"> 
                 <div>
                     <div>
                          <asp:repeater id="contentSearchRepeater" runat="server" OnItemCommand="contentSearchDataCommand">
                                <ItemTemplate>
                                     <div  class="News_BeginRow">
                                        <div class="News">
                                            <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>
                                        </div>
                                 </ItemTemplate>
                                 <AlternatingItemTemplate>
                                          <div class="News">                                                                                            
                                            <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>
                                         </div>  
                                      </div>       
                                       <div class="result_separator"></div>        
		                          </AlternatingItemTemplate>
                                   
                                    <FooterTemplate>
                                         <table class="PagingTable" border="0" id="paging">
                                            <tr>
                                                <td class="Normal" align="right">
                                                      <CC:Navigator id="NavigatorNavigator" MaxPage="<%#_contentSearchData.PageCount%>" PageNumber="<%#_contentSearchData.PageNumber%>" runat="server">
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
                                    </FooterTemplate>                                
                        </asp:repeater>  
                      </div>
                </div>
     </div>
    <div class="WebPartBox_SilverRoundBottom_Center">
        <div class="WebPartBox_SilverRoundBottom_Left">
            <div class="WebPartBox_SilverRoundBottom_Right"></div>
        </div>
    </div>
</div>