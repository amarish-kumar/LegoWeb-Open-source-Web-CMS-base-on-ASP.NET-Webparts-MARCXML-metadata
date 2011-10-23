<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebsiteInfo.ascx.cs" Inherits="Webparts_WebsiteInfo" %>
		<div id="blue-header-title-box"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title"> <%=Resources.strings.WebInfo%></div>		    						
			<div class="m">			
			<div class="clearfix">
			
			
        <div class="bonline"><span><%=Resources.strings.Today%> : <%=DateTime.Now.ToLongDateString()%></span></div>
         <div class="vertical"></div>
        <div class="bonline"><span><%=Resources.strings.OnlineUsers%> : <%=Application["OnlineUsers"].ToString()%></span></div>
         <div class="vertical"></div>
        <div class="bonline"> <span><%=Resources.strings.VisistedCount%> : <%=Application["Visited_Count"].ToString()%></span></div>
         <div class="vertical"></div>


		    </div>		
			        <div class="clr"></div>
			</div>
					
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
   		</div>

