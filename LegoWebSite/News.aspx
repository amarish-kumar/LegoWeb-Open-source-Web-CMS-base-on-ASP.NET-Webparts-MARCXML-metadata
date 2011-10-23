<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" Title="NORTH POWER SERVICE JOINT STOCK COMPANY " %>
<%@ Register src="Webparts/MenuTree.ascx" tagname="MenuTree" tagprefix="uc1" %>
<%@ Register src="Webparts/TopReadBox.ascx" tagname="TopReadBox" tagprefix="uc2" %>
<%@ Register src="Webparts/ContentBrowser.ascx" tagname="ContentBrowser" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id ="Content_Body">
       <div class="MenuLeft">

        <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
            <PartStyle CssClass="Nopadding"></PartStyle>
            <CloseVerb Visible="false" />
            <MinimizeVerb Visible="false" />
            <ZoneTemplate> 
                
                <uc1:MenuTree ID="MenuTree1"  mnuid="1100" runat="server" />
                <uc2:TopReadBox ID="TopReadBox1" category_id="1100" runat="server" />
                
            </ZoneTemplate>
        </asp:WebPartZone>

       </div>
        <div class="ContentRight">
            <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%"  PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding">
                <PartStyle CssClass="nopadding"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                    <uc3:ContentBrowser ID="ContentBrowser1" runat="server" />
                    
                </ZoneTemplate>
            </asp:WebPartZone>
        </div>
    </div>
</asp:Content>


