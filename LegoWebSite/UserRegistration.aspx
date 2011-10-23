<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" %>
<%@ Register src="Webparts/MenuTree.ascx" tagname="MenuTree" tagprefix="uc1" %>
<%@ Register src="Webparts/UserRegistration.ascx" tagname="UserRegistration" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="Content_Body">
        <div class="MenuLeft">
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                <uc1:MenuTree ID="MenuTree1" menu_type_id="2" runat="server" />
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="ContentRight">
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    <uc2:UserRegistration ID="UserRegistration1" runat="server" />
               </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>