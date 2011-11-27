<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenuBarActive.ascx.cs" Inherits="LgwUserControls_AdminMenuBarActive" %>

<div id="module-status">		    
            <span class="current-user"><%=this.Page.User.Identity.Name%></span>
            <span class="loggedin-users"><%=Membership.GetNumberOfUsersOnline()%></span>
            <span class="logout"><a href="default.aspx?task=logout"><%=Resources.strings.Logout_Text%></a>&nbsp;&nbsp;
                <asp:LinkButton id="btnSelectEnglish" runat="server" OnClick="en_Click">
		            <asp:image ID="EnglishFlag" runat="server" skinid="en"  />
		        </asp:LinkButton>
		        <asp:LinkButton id="btnSelectVietnamese" runat="server" OnClick="vi_Click">
		            <asp:image ID="VietnameseFlag" runat="server" skinid="vi"  />
		        </asp:LinkButton>
            </span>
</div>
<div id="module-menu">
<ul id="menu" >

<li class="node"><a><%=Resources.strings.Sytem_Text%></a>
<ul>
<li><a class="icon-16-cpanel" href="ControlPanel.aspx"><%=Resources.strings.ControlPanel_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-user" href="UserManager.aspx"><%=Resources.strings.Users_Text%></a></li>
<li><a class="icon-16-user" href="UserRoleManager.aspx"><%=Resources.strings.UserRoles_Text%></a></li>
<li><a class="icon-16-media" href="MediaManager.aspx"><%=Resources.strings.Files_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-config" href="CommonParameterManager.aspx"><%=Resources.strings.Settings_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-archive" href="MetaContentExport.aspx"><%=Resources.strings.MetadataExports_Text%></a></li>
<li><a class="icon-16-install" href="MetaContentImport.aspx"><%=Resources.strings.MetadataImports_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-logout" href="default.aspx?task=logout"><%=Resources.strings.Logout_Text%></a></li>
</ul>
</li>

<li class="node"><a><%=Resources.strings.Menus_Text%></a>
<ul>
<li><a class="icon-16-menumgr" href="MenuTypeManager.aspx"><%=Resources.strings.MenuTypes_Text%></a></li>
<li class="separator"><span></span></li>
<asp:Literal ID="menunames" runat="server"></asp:Literal>
</ul>
</li>
<li class="node"><a><%=Resources.strings.Contents_Text%></a>
<ul>
<li><a class="icon-16-article" href="MetaContentManager.aspx"><%=Resources.strings.ContentManager_Text%></a></li>
<li><a class="icon-16-trash" href="MetaContentTrash.aspx"><%=Resources.strings.TrashManager_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-section" href="SectionManager.aspx"><%=Resources.strings.Sections_Text%></a></li>
<li class="separator"><span></span></li>
<asp:Literal ID="sectionnames" runat="server"></asp:Literal>
</ul>
</li>

<li class="node"><a><%=Resources.strings.Modules_Text%></a>
<ul>
<li><a class="icon-16-forum" href="Forum/ForumManager.aspx"><%=Resources.strings.ForumManager_Text%></a></li>
</ul>
</li>

<li class="node"><a><%=Resources.strings.Help_Text%></a>
<ul>
<li><a class="icon-16-info" href="http://www.legoweb.org/"><%=Resources.strings.About_Text%></a></li>
<li><a class="icon-16-help" href="http://www.legoweb.org/help"><%=Resources.strings.HelpContent_Text%></a></li>
</ul>
</li>
</ul>
</div>