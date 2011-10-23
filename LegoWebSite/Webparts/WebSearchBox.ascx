<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebSearchBox.ascx.cs" Inherits="Webparts_WebSearchBox" %>


<div id="grey-header-title-box"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title"> <%=Resources.strings.WebSearch%></div>		    						
			<div class="m">			
			<div class="clearfix">

			        	<table width="100%">
			        	<tr>
						    <td align="right">
                                <asp:TextBox ID="txtKeywords" runat="server" Width="98%" 
                                BorderStyle="Solid" BorderColor="#9999FF" BorderWidth="1px" ></asp:TextBox>
                            </td>						
						</tr>
        				<tr>
						<td align="right">
						<asp:Button ID="btnSearch" runat="server" Text='<%$Resources:strings,Search%>' 
                                onclick="btnSearch_Click" BorderStyle="Outset" Font-Bold=true ForeColor="White" 
                    BorderColor="#CCFFFF" BackColor="#0066FF"/>
						</td>						
						</tr>
						</table>	


		    </div>		
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>



