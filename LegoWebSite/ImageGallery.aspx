<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImageGallery.aspx.cs" Inherits="ImageGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="lgw-content">
        <div class="main-left-container">
         <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%"  PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
            <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
              
            
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="right-container">
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent">
            <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 


            </ZoneTemplate>
            </asp:WebPartZone>
        </div>
        
            <asp:WebPartZone ID="WebPartZone3" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    	

            <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>	
    </div>
</asp:Content>

