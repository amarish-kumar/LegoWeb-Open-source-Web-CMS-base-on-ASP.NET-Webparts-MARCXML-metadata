<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DocumentDetails.aspx.cs" Inherits="DocumentDetails" Title="NORTH POWER SERVICE JOINT STOCK COMPANY" %>
<%@ Register src="Webparts/MenuBar.ascx" tagname="MenuBar" tagprefix="uc3" %>
<%@ Register src="Webparts/MenuTree.ascx" tagname="MenuTree" tagprefix="uc4" %>
<%@ Register src="Webparts/ContentBrowser.ascx" tagname="ContentBrowser" tagprefix="uc1" %>



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
              
                <uc4:MenuTree ID="MenuTree1" mnid="2000" runat="server" />
            
            </ZoneTemplate>
        </asp:WebPartZone>
       </div>
        <div class="ContentRight">
            <asp:WebPartZone ID="WebPartContentZone1" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                    
                <uc1:ContentBrowser ID="ContentBrowser1" runat="server" />
                    
            </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>



