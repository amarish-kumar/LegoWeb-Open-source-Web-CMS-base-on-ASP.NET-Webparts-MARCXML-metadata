<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenuBarDeactive.ascx.cs" Inherits="LgwUserControls_AdminMenuBarDeactive" %>
		<div id="module-status">	
            <span class="current-user"><%=this.Page.User.Identity.Name%></span>
            <span class="loggedin-users"><%=Membership.GetNumberOfUsersOnline()%></span>
            <span class="logout"><a href="../default.aspx?task=logout"><%=Resources.strings.Logout_Text%></a>&nbsp;&nbsp;
                <asp:LinkButton id="btnSelectEnglish" runat="server" OnClick="en_Click">
		            <asp:image ID="EnglishFlag" runat="server" skinid="en"  />
		        </asp:LinkButton>
		        <asp:LinkButton id="btnSelectVietnamese" runat="server" OnClick="vi_Click">
		            <asp:image ID="VietnameseFlag" runat="server" skinid="vi"  />
		        </asp:LinkButton>
            </span>
        </div>
		<div id="module-menu">
			<ul id="menu" class="disabled">
            <li class="disabled"><a><%=Resources.strings.Sytem_Text%></a>
            </li>
            <li class="disabled"><a><%=Resources.strings.Menus_Text%></a>
            </li>   
            <li class="disabled"><a><%=Resources.strings.Contents_Text%></a>
            </li>                                                               
            <li class="disabled"><a><%=Resources.strings.Modules_Text%></a>
            </li>                                                                                                                                                                
            <li class="node"><a><%=Resources.strings.Help_Text%></a>
            <ul>
            <li><a class="icon-16-info" href="http://www.legoweb.org/"><%=Resources.strings.About_Text%></a></li>
            <li><a class="icon-16-help" href="http://www.legoweb.org/help"><%=Resources.strings.HelpContent_Text%></a></li>
            </ul>
            </li>                                           
            </ul>
		</div>