<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WebSearch.aspx.cs" Inherits="WebSearch" Title="Tìm kiếm nội dung website" %>

<%@ Register src="Webparts/WebSearchBox.ascx" tagname="WebSearchBox" tagprefix="uc1" %>
<%@ Register src="Webparts/WebSearchResult.ascx" tagname="WebSearchResult" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id="mainBgSeach">
        <div class="contentSeach">
            <div class="Search_BoxC">
                <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
                    <PartStyle CssClass="Nopadding"></PartStyle>
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate> 
                        
                        <uc1:WebSearchBox ID="WebSearchBox1" runat="server" />
                        
                    </ZoneTemplate>
                </asp:WebPartZone>
            </div>
             <div class="SeachResult">
                <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
                    <PartStyle CssClass="Nopadding"></PartStyle>
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate> 
                        
                        
                        <uc2:WebSearchResult ID="WebSearchResult1" runat="server" />
                        
                        
                    </ZoneTemplate>
                </asp:WebPartZone>
            </div>
            <%--<div id="colunmRightSearch">
                 <asp:WebPartZone ID="WebPartContentZone2" runat="server" Width="100%" PartStyle-CssClass="Nopadding">
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate>                        
                        
                    </ZoneTemplate>
                </asp:WebPartZone>
            </div>--%>
        </div>
    </div>
</asp:Content>

