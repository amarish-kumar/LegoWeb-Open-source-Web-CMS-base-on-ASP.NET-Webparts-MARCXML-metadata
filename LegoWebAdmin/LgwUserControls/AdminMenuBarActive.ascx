<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenuBarActive.ascx.cs" Inherits="LgwUserControls_AdminMenuBarActive" %>

<div id="module-status">		    
            <span class="current-user"><%=this.Page.User.Identity.Name%></span>
            <span class="loggedin-users"><%=Membership.GetNumberOfUsersOnline()%></span>
            <span class="logout"><a href="default.aspx?task=logout"><%=Resources.strings.mnuiLogout_Text%></a>&nbsp;&nbsp;
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

<li class="node"><a><%=Resources.strings.mnuSytem_Text%></a>
<ul>
<li><a class="icon-16-cpanel" href="ControlPanel.aspx"><%=Resources.strings.mnuiControlPanel_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-user" href="UserManager.aspx"><%=Resources.strings.mnuiUsers_Text%></a></li>
<li><a class="icon-16-user" href="UserRoleManager.aspx"><%=Resources.strings.mnuiUserRoles_Text%></a></li>
<li><a class="icon-16-media" href="MediaManager.aspx"><%=Resources.strings.mnuiFiles_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-config" href="CommonParameterManager.aspx"><%=Resources.strings.mnuiSettings_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-archive" href="MetaContentExport.aspx"><%=Resources.strings.mnuiMetadataExports_Text%></a></li>
<li><a class="icon-16-install" href="MetaContentImport.aspx"><%=Resources.strings.mnuiMetadataImports_Text%></a></li>
<li class="separator"><span></span></li>
<li><a class="icon-16-logout" href="default.aspx?task=logout"><%=Resources.strings.mnuiLogout_Text%></a></li>
</ul>
</li>

<li class="node"><a><%=Resources.strings.mnuMenus_Text%></a>
<ul>
<li><a class="icon-16-menumgr" href="MenuTypeManager.aspx"><%=Resources.strings.mnuiMenuTypes_Text%></a></li>
<li class="separator"><span></span></li>
<asp:Literal ID="menunames" runat="server"></asp:Literal>
</ul>
</li>
<li class="node"><a><%=Resources.strings.mnuCategories_Text%></a>
<ul>
<li><a class="icon-16-section" href="SectionManager.aspx"><%=Resources.strings.mnuiSections_Text%></a></li>
<li class="separator"><span></span></li>
<asp:Literal ID="sectionnames" runat="server"></asp:Literal>
</ul>
</li>
<li class="node"><a><%=Resources.strings.mnuContents_Text%></a>
<ul>
<li><a class="icon-16-article" href="MetaContentManagerTree.aspx"><%=Resources.strings.mnuiContentManager_Text%></a></li>
<li class="separator"><span></span></li>
<asp:Literal ID="contentsections" runat="server"></asp:Literal>
</ul>
</li>
<li class="node"><a><%=Resources.strings.mnuTools_Text%></a>
<ul>
<li><a class="icon-16-module" href="ItemClassDefination.aspx">Phân loại đầu mục</a></li>
</ul>
</li>
<li class="node"><a><%=Resources.strings.mnuHelp_Text%></a>
<ul>
<li><a class="icon-16-info" href="">Giới thiệu</a></li>
<li><a class="icon-16-help" href="">Trợ giúp</a></li>
</ul>
</li>
</ul>
</div>