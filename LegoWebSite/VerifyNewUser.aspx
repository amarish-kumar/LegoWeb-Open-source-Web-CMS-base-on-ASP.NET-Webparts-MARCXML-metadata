<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VerifyNewUser.aspx.cs" Inherits="VerifyNewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">

    <div id ="lgw-content">
        <div class="left-column-240">
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 

            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="midle-right-column-740">
            <asp:Label ID="label1" runat="server"></asp:Label>
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
                <ZoneTemplate> 

               </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>

