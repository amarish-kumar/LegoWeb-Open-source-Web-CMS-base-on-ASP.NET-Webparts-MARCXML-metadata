<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentBrowser.ascx.cs" Inherits="Webparts_ContentBrowser" %>
<div id="PanelBrowser">

		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m" style="padding: 0px 0px 0px 10px;">			
				
					<div id="divTopNavigator"  runat="server" class="clearfix"></div>				
	
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>

        <br />
        
		<div id="blue-element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m" style="padding: 10px 5px 10px 5px;">			

			
                        <div id="divContentBrowser" runat="server" class="clearfix">
                            
                        </div>       
                          
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>
   		
   		   <br />
   		   
        <div id="divRelatedContents" runat="server" visible="false">
                <div id="grey-header-title-box">
                    <div class="t">
	                    <div class="t">
		                    <div class="t"></div>
	                    </div>
                    </div>
                    <div class="title"><%=Resources.strings.OtherItems %> </div>
                    <div class="m" style="padding:10px 5px 10px 5px;">			


                            <div id="divRelatedContentDetails" runat="server"  class="clearfix"> 
                            
                            </div>

                            <div class="clr"></div>
                    </div>
        					
                    <div class="b">
	                    <div class="b">
		                    <div class="b"></div>
	                    </div>
                    </div>
                </div>
        </div>
</div>
