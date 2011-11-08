<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenuBarDeactive.ascx.cs" Inherits="LgwUserControls_AdminMenuBarDeactive" %>
		<div id="module-status">	
            <span class="current-user"><%=this.Page.User.Identity.Name%></span>
            <span class="loggedin-users"><%=Membership.GetNumberOfUsersOnline()%></span>
            <span class="logout"><a href="../default.aspx?task=logout"><%=Resources.strings.mnuiLogout_Text%></a>&nbsp;&nbsp;
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
            <li class="disabled"><a><%=Resources.strings.mnuSytem_Text%></a>
            </li>
            <li class="disabled"><a><%=Resources.strings.mnuMenus_Text%></a>
            </li>   
            <li class="disabled"><a><%=Resources.strings.mnuContents_Text%></a>
            </li>                                                               
            <li class="disabled"><a><%=Resources.strings.mnuModules_Text%></a>
            </li>                                                               
            <li class="disabled"><a><%=Resources.strings.mnuHelp_Text%></a>
            </li>                                             
            </ul>
		</div>