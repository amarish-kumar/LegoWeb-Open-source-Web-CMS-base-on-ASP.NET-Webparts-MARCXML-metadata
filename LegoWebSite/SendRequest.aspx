<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendRequest.aspx.cs" Inherits="SendRequest" %>
<%@ Register src="Webparts/MenuTree.ascx" tagname="MenuTree" tagprefix="uc1" %>
<%@ Register src="Webparts/SendRequestEmail.ascx" tagname="SendRequestEmail" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="Content_Body">
        <div class="MenuLeft">
         <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                <uc1:MenuTree ID="MenuTree1" mnid="1400" runat="server" />
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="ContentRight" id="ShowRequest"  runat="server">
            <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
               
                <uc2:SendRequestEmail ID="SendRequestEmail1" runat="server" />
               
            </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>

