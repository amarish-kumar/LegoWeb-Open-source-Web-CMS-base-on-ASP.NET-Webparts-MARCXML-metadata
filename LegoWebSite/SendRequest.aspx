<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendRequest.aspx.cs" Inherits="SendRequest" %>
<%@ Register src="Webparts/MENULEFT.ascx" tagname="MENULEFT" tagprefix="uc1" %>
<%@ Register src="Webparts/Contact_Mail.ascx" tagname="Contact_Mail" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script src="js/jquery-latest.js" type="text/javascript"></script> 
    <script src="js/MenuTree.js" type="text/javascript"></script> 
    <script src="js/jsPopup.js" type="text/javascript"></script>
    <script src="js/searchEngine.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="lgw-content">
        <div class="left-column-240">
         <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                <uc1:MENULEFT ID="MENULEFT1" mnid="1400" runat="server" />
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="midle-right-column-740" id="ShowRequest"  runat="server">
            <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
               
                <uc2:Contact_Mail ID="Contact_Mail1" runat="server" />
               
            </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>

