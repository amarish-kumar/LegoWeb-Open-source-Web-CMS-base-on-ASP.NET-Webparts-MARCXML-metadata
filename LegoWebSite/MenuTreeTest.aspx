<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MenuTreeTest.aspx.cs" Inherits="MenuTreeTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" Runat="Server">

		<div id="MenuTreeWrap"><%--grey-header-title-box--%>
			<div class="t">
		 		<div class="t">
					<div class="t"></div>
		 		</div>
			</div>
            <div class="title">Tiêu đề trình đơn</div>		    						
			<div class="m">		
            <div class="clearfix">
                <div id="MenuTreeItems">
                <div id="menuTree">
                            <div id="ShowMenuTree" runat="server">
                            <ul id='menuList'>
                                <li class='fly'>
                                     <a href='http://www.hiendai.com.vn'>
                                        <span class='text'>Công ty Hiện Đai</span>
                                     </a>
                                </li>
                                <li class='fly'>
                                     <a href='http://www.hiendai.com.vn'>
                                        <span class='text'>Củ chuối</span>
                                     </a>
                                     <ul>
                                          <li>
                                            <a href='http://www.vnexpress.net'>Báo Vnexpress</a>
                                          </li>
                                           <li>
                                            <a href='http://www.vietnamnet.vn'>Báo VietnamNet</a>
                                          </li>
                                     </ul>
                                </li>
                                <li class='fly'>
                                     <a href='http://www.hiendai.com.vn'>
                                        <span class='text'>Chấm muối</span>
                                     </a>
                                </li>                
                            </ul>
                            </div>
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

        <p></p>






</asp:Content>

