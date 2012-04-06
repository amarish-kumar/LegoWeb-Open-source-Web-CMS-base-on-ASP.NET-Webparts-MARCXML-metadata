<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" %>
<%@ Register src="~/Webparts/MENUFLYOUT.ascx" tagname="MENUFLYOUT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="lgw-content">
        <div class="left-container-225" >
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
            <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                <uc1:MENUFLYOUT ID="MENUFLYOUT1" menu_type_id="2" runat="server" />
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="right-container-750">
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent">
            <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
               </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>