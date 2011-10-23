<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsBoxOnePart.ascx.cs" Inherits="Webparts_NewsBoxOnePart" %>

		<div id="blue-title-box">
                
			<div class="t">				 
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title">
            <%=Title.ToUpper()%>		
            </div>
			<div class="m">			
			    <div class="clearfix">
                    <div id="divContentList" runat="server">
                    

                    
                    </div>			        			        
                </div>			
			        <div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
