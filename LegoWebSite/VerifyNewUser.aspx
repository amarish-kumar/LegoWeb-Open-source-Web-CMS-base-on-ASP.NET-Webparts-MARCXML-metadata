<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VerifyNewUser.aspx.cs" Inherits="VerifyNewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">

    <div id ="lgw-content">
        <div class="left-container-225">
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
            <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 

            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="right-container-750">
            <asp:Label ID="label1" runat="server"></asp:Label>
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

