<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script src="js/jquery-latest.js" type="text/javascript"></script> 
    <script src="js/MenuTree.js" type="text/javascript"></script> 
    <script src="js/jsPopup.js" type="text/javascript"></script>
    <script src="js/searchEngine.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id="lgw-content">
    <%--change to 4 columns redesign by NTU--%>
	<table cellpadding="0" cellpadding="0" border="0" width="100%" style="table-layout:fixed">
	<tbody>
	<tr>
	<td width="200px" valign="top">
           <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                 <PartStyle CssClass="Nopadding"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>	
	</td>
	<td width="100%" valign="top">
	<table cellpadding="0" cellspacing="0" border="0" width="100%" style="table-layout:fixed">
	<tbody>
	<tr>
	<td colspan="2" valign="top" align="center">
	<%--home flash go here--%>
                <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                     <PartStyle CssClass="Nopadding"></PartStyle>
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate> 
                        
                        
                    </ZoneTemplate>
                </asp:WebPartZone>		
	</td>
	</tr>
	<tr>
	<td valign="top" width="50%">
	<%--midleft col go here--%>
	            <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                <PartStyle CssClass="Nopadding"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                   
                </ZoneTemplate>
            </asp:WebPartZone>
	</td>
	<td valign="top" width="50%">
	<%--middleright col go here--%>
	                <asp:WebPartZone ID="WebPartZone5" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                    <PartStyle CssClass="Nopadding"></PartStyle>
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate>                        

                    </ZoneTemplate>
                </asp:WebPartZone>
	</td>
	</tr>
	</tbody>
	</table>			
	</td>
	<td valign="top" width="230px">
	<%--right side column go here--%>
	            <asp:WebPartZone ID="WebPartZone3" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                    <PartStyle CssClass="Nopadding"></PartStyle>
                    <CloseVerb Visible="false" />
                    <MinimizeVerb Visible="false" />
                    <ZoneTemplate>   

                    </ZoneTemplate>
                </asp:WebPartZone>
	</td>
	</tr>	
	</tbody>	
	</table>	                      
	
	</div>
</asp:Content>

