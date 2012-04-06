<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WEBSEARCHRESULT.ascx.cs" Inherits="Webparts_WEBSEARCHRESULT" %>
<%@Register TagPrefix="CC" Namespace="LegoWebSite.Controls"%>
<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>

<span class="websearch_result"><asp:Label ID="labelMessages" runat="server" Text=""></asp:Label></span>          
    
<asp:repeater id="contentSearchRepeater" runat="server" OnItemCommand="contentSearchDataCommand">
    <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>
     </ItemTemplate>
     <AlternatingItemTemplate>                                                                                          
                <%# DataBinder.Eval(Container.DataItem, "CONTENT_HTML")%>    
      </AlternatingItemTemplate>
       
        <FooterTemplate>
             <table border="0" width="100%">
                <tr style="height:30px">
                    <td valign="middle" align="right">
                          <CC:Navigator id="NavigatorNavigator" MaxPage="<%#_contentSearchData.PageCount%>" PageNumber="<%#_contentSearchData.PageNumber%>" runat="server">
                            &nbsp;&nbsp;&nbsp;<CC:NavigatorItem type="FirstOn" runat="server" ID="Navigatoritem1">
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorFirst" runat="server">
                                                            <asp:image ID="Image1" runat="server" skinid="First" /></asp:LinkButton>
                                                    </CC:NavigatorItem>
                            <CC:NavigatorItem type="FirstOff" runat="server" ID="Navigatoritem2">
                                                        &nbsp;&nbsp;&nbsp;<asp:image ID="Image2" runat="server" skinid="FirstOff" /></CC:NavigatorItem>
                            <CC:NavigatorItem type="PrevOn" runat="server" ID="Navigatoritem3">
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorPrev" runat="server">
                                                            <asp:image ID="Image3" runat="server" skinid="Prev" /></asp:LinkButton>
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
                                                        &nbsp;&nbsp;&nbsp;<asp:image ID="Image6" runat="server" skinid="NextOff"  /></CC:NavigatorItem>
                            <CC:NavigatorItem type="LastOn" runat="server" ID="Navigatoritem7">
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton id="NavigatorLast" runat="server">
                                                            <asp:image ID="Image7" runat="server" skinid="Last"   /></asp:LinkButton>
                                                    </CC:NavigatorItem>
                            <CC:NavigatorItem type="LastOff" runat="server" ID="Navigatoritem8">
                                                        &nbsp;&nbsp;&nbsp;<asp:image ID="Image8" runat="server" skinid="LastOff"  /></CC:NavigatorItem></CC:Navigator>														
                    </td>
                </tr>
            </table> 
        </FooterTemplate>                                
</asp:repeater>  

<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>