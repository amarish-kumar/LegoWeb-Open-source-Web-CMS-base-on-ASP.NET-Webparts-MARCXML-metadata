<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GGWEBSITEINFO.ascx.cs" Inherits="Webparts_GGWEBSITEINFO" %>
<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>
			
			
        <div class="webinfo"><span><%=Resources.strings.Today%>: <%=DateTime.Now.ToLongDateString()%></span></div>
        <div class="icon-vertical-line"></div>
        <div class="webinfo"><span><%=Resources.strings.OnlineUsers%> : <%=Application["OnlineUsers"].ToString()%></span></div>
        <div class="icon-vertical-line"></div>
        <div class="webinfo"> <span><%=Resources.strings.MonthVisitedCount%> : <%=Application[String.Format("VISITED_IN_{0}", DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00"))].ToString()%></span></div>
        <div class="icon-vertical-line"></div>
        <div class="webinfo"> <span><%=Resources.strings.TotalVisitedCount%> : <%=Application["TOTAL_VISITED_COUNT"].ToString()%></span></div>
        <div class="icon-vertical-line"></div>

        
<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>
