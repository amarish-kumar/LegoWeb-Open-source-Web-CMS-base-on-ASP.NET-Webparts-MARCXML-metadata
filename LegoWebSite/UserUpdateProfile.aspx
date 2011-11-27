<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserUpdateProfile.aspx.cs" Inherits="UserUpdateProfile" %>

<%@ Register src="~/Webparts/MENULEFT.ascx" tagname="MENULEFT" tagprefix="uc1" %>
<%@ Register src="Webparts/UserUpdateProfile.ascx" tagname="UserUpdateProfile" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script src="../js/jquery-latest.js" type="text/javascript"></script> 
    <script src="../js/MenuTree.js" type="text/javascript"></script> 
    <script src="../js/jsPopup.js" type="text/javascript"></script>
    <script src="../js/searchEngine.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="lgw-content">
        <div class="left-column-240">
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                <uc1:MENULEFT ID="MENULEFT1" menu_type_id="2" runat="server" />
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="midle-right-column-740">
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    <uc2:UserUpdateProfile ID="UserUpdateProfile1" runat="server" />
               </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>