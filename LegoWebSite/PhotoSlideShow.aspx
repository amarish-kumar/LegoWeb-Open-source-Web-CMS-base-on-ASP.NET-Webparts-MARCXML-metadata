<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoSlideShow.aspx.cs" Inherits="PhotoSlideShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
    <div id="Content_Body">

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
           <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="0" PartStyle-CssClass="NoPadding"> 
                 <PartStyle CssClass="Nopadding"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		
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

