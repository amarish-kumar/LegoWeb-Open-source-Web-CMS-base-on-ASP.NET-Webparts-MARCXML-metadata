<%@ Page Language="C#" MasterPageFile="LegoWebAdmin.master" AutoEventWireup="true" CodeFile="MetaContentAddUpdate.aspx.cs" Inherits="Administrator_MetaContentAddUpdate" Title="KIPOSADMIN: Biên tập nội dung web" ValidateRequest="false" %>
<%@ Register src="UserControls/AdminMenuBarActive.ascx" tagname="AdminMenuBarActive" tagprefix="uc1" %>
<%@ Register src="UserControls/AdminMenuBarDeactive.ascx" tagname="AdminMenuBarDeactive" tagprefix="uc2" %>
<%@ Register src="UserControls/MetaContentAddUpdate.ascx" tagname="MetaContentAddUpdate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script language="javascript" type="text/javascript">
            var locale = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower()%>';
        </script>
       <script language="JavaScript" src='AdminTools/DatePicker.js' type="text/javascript" charset="utf-8"></script>
        <script language="JavaScript" type="text/javascript">
        
            //End Include Common JSFunctions

            //Date Picker Object Definitions @1-E6DE1018
            var myDatePicker = new Object();
            myDatePicker.format = "ShortDate";
            myDatePicker.style = "../App_Themes/Calendar/Style.css";
            myDatePicker.relativePathPart = "";
            myDatePicker.themeVersion = "3.0";

            //End Date Picker Object Definitions	


            //End CCS script
        </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="content-box">
	<div id="header-box">

        <%--menu bar go here--%>
		<uc2:AdminMenuBarDeactive ID="AdminMenuBarDeactive1" runat="server" />
		<div class="clr"></div>
	</div>
	

	   		<div class="clr"></div>
		
	<div class="border">
	  <div class="padding">		
	  
<div id="toolbar-box">
   			<div class="t">
				<div class="t">
					<div class="t"></div>
				</div>
			</div>
			<div class="m">
				<div class="toolbar" id="toolbar">
<table class="toolbar"><tr>

<td>
<asp:LinkButton ID="linkPreviewButton" class="toolbar" runat="server" 
        onclick="linkPreviewButton_Click">
<span class="icon-32-preview" title="Preview">
</span>
Xem thử
</asp:LinkButton>
</td>

<td class="button" id="toolbar-save">
<asp:LinkButton ID="linkSaveButton" class="toolbar" runat="server" 
        onclick="linkSaveButton_Click" ValidationGroup="MetaContentInfo" CausesValidation="true">
<span class="icon-32-save" title="Save">
</span>
Chấp nhận
</asp:LinkButton>
</td>
 

<td class="button" id="toolbar-cancel">
<asp:LinkButton ID="linkCancelButton" class="toolbar" runat="server" 
        onclick="linkCancelButton_Click" ValidationGroup="MetaContentInfo" CausesValidation="false">
        <span class="icon-32-cancel" title="Cancel">
</span>
Bỏ qua
</asp:LinkButton>
</td>
 
<td class="button" id="toolbar-help">
<a href="#" onclick="popupWindow('http://www.legoweb.org/help', 'Help', 640, 480, 1)" class="toolbar">
<span class="icon-32-help" title="Trợ giúp">
</span>
Trợ giúp
</a>
</td>
 
</tr></table>

</div>
				 <asp:Literal ID="literalIconTitle" runat="server"></asp:Literal>

				<div class="clr"></div>
			</div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
  		</div>  		
	  
	  <div class="clr"></div>
	  
		<div id="element-box">
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
			<div class="m">               

                            <uc3:MetaContentAddUpdate ID="MetaContentAddUpdate1" runat="server" />
									
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
		<div class="clr"></div>
	</div>
</asp:Content>

