<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MENULEFT.ascx.cs" Inherits="Webparts_MENULEFT" %>


		<div id="MenuLeftWrap"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title"><asp:Literal ID="ltMenuTitle" runat="server"></asp:Literal></div>		    						
			<div class="m">		
            <div class="clearfix">
                <div id="MenuLeftItems">
                    <div id="menuitem">
                               <asp:Literal ID="ltMenuLeftItems" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
                               
            </div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>