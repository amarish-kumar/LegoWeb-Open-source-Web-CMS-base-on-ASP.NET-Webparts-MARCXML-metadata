<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">
<%--EVNNPS layout--%>
<%--   <div id="lgw-content">    
    <table cellpadding="0" cellpadding="0" border="0" width="100%" style="table-layout:fixed">
	<tbody>
	<tr>
	<td colspan="2"  style="width:665px" valign="top">
	<table cellpadding="0" cellpadding="0" border="0" width="100%" style="table-layout:fixed">
	<tbody>
	<tr>
	    <td colspan="2" valign="top">
	       <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    
	    </td>	    
	</tr>	
	<tr>
	    <td style="width:60%" valign="top" >
	       <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    	   	   	   	    
	    </td>	    
	    <td style="width:40%" valign="top">
	       <asp:WebPartZone ID="WebPartZone3" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
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
	<td valign="top">
	       <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"> 
                 <PartStyle CssClass="PaddingAndTransparent"></PartStyle>
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    	
	</td>	
	</tr>
	</tbody>
	</table>                    	
	</div>--%>

<%--VITECO layout--%>
<%--    <div id="lgw-content">    
    <div id="tripleleftcontainer">
	       <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2"> 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    		
	</div>
	<div id="triplemiddlecontainer">
	       <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="2">                 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    		
    </div>
    <div id="triplerightcontainer">
	       <asp:WebPartZone ID="WebPartZone3" runat="server" Width="100%" PartChromeType="none" Padding="2">                 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    	
    </div>
	<div>
		       <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%" PartChromeType="none" Padding="2">                 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>	
	</div>                  	
	</div>
--%>


<%--HIENDAI layout --%>
<div id="lgw-content">    
 
 <div class="main-left-container">
 <div class="full-container">
	       <asp:WebPartZone ID="WebPartZone1" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
                       PartStyle-BackColor="Transparent" BackColor="Transparent" PartChromeStyle-BackColor="Transparent">  
                       <PartStyle BackColor="Transparent" CssClass="PaddingAndTransparent"></PartStyle> 

                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    
</div>
<div class="main-container-left">
	       <asp:WebPartZone ID="WebPartZone2" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
                       PartStyle-BackColor="Transparent" BackColor="Transparent" PartChromeStyle-BackColor="Transparent">  
                       <PartStyle BackColor="Transparent" CssClass="PaddingAndTransparent"></PartStyle> 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>		    	   	    	   	   	   	    
</div>
<div class="main-container-right">
	       <asp:WebPartZone ID="WebPartZone3" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
                       PartStyle-BackColor="Transparent" BackColor="Transparent" PartChromeStyle-BackColor="Transparent">  
                       <PartStyle BackColor="Transparent" CssClass="PaddingAndTransparent"></PartStyle> 
                <CloseVerb Visible="false" />
                <MinimizeVerb Visible="false" />
                <ZoneTemplate> 
                    
                </ZoneTemplate>
            </asp:WebPartZone>
</div>
 </div>		    	   	    	    	    	   	    
<div class="right-container">
	       <asp:WebPartZone ID="WebPartZone4" runat="server" Width="100%" PartChromeType="none" Padding="2" PartStyle-CssClass="PaddingAndTransparent"  
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

