<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContentBrowser.aspx.cs" Inherits="DocumentDetails" Title="NORTH POWER SERVICE JOINT STOCK COMPANY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
<div id ="lgw-content">
        <div class="main-left-container">
	       <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
                       PartStyle-BackColor="Transparent" BackColor="Transparent" PartChromeStyle-BackColor="Transparent">  
                       <PartStyle BackColor="Transparent" CssClass="PaddingAndTransparent"></PartStyle> 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>
       </div>
        <div class="right-container">
	       <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
                       PartStyle-BackColor="Transparent" BackColor="Transparent" PartChromeStyle-BackColor="Transparent">  
                       <PartStyle BackColor="Transparent" CssClass="PaddingAndTransparent"></PartStyle> 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>





