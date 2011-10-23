<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Poll.ascx.cs" Inherits="Webparts_Poll" %>

    <script type="text/javascript">
        $(document).ready(function() 
        {       
            var imgPoll = new Image();
            imgPoll.src = 'images/red-bar.png';
            
            //show result
			$("div[id$=divResult] img").each(function()
            {
                var percentage = $(this).attr("val");
                $(this).css({width: "0%"}).animate({width: percentage}, 'slow'); 
            });
        });
    </script>
    
    <div><span class="WebPartHeader_IconTitle"><img src="images/icon-l.jpg" border="0" style="float:left;padding-right:3px"/><%=Title.ToUpper() %></span></div>
    <div class="WebPartHeader_Line"></div>
    <div class="WebPartBox_SteelBlueLine">
		 <div class="WebPartBox_SteelBlueLineWrap">
            <div runat="server" id="divMsg" class="mInfo" visible="false" />
            <div id="divPoll" runat="server">
                <div class="poll-question">
					<div style="padding:3px">
						 <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
					</div>
                </div>
                <div id="divQuestion" class="questionList" runat="server" visible="true">
                    <table width="100%" cellpadding="5" cellspacing="0" border="0">                
                        <tr>
                        <td>
                            <asp:RadioButtonList ID="radioListChoices" runat="server" Font-Names="Arial" 
                                Font-Size="9pt" ForeColor="#333333">            
                            </asp:RadioButtonList>
                        </td>
                        </tr>
                        <tr>
                            <td align="right">
                            <asp:Button ID="btnVote" Text="Bỏ phiếu" runat="server" OnClick="btnVote_OnClick" 
                                    Font-Bold="False" Font-Italic="False"/>
                            </td>
                        </tr>            
                    </table>
                </div>
                <div id="divVote" runat="server" visible="false" class="questionList">
                    <table width="100%" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                    <td colspan="2" valign="middle">
                        <img src="JpegImage.aspx"/>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <asp:TextBox ID="txtVoteConfirmNumber" runat="server" Width="100"></asp:TextBox>
                    </td>
                    <td>
                    <asp:Button ID="btnConfirmVote" Text="Xác Nhận" runat="server" OnClick="btnConfirmVote_OnClick"/>        
                    </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <asp:Label ID="labelErroMessage" runat="server" ForeColor="#CC3333"></asp:Label>
                    </td>
                    </tr>
                    </table>        
                </div>
                <div id="divResult" runat="server" visible="false" class="questionList">
                
                </div>        
            </div>
        </div>    
    </div>
    <div class="WebPartBox_SteelBlueFooter_Center">
        <div class="WebPartBox_SteelBlueFooter_Left"></div>
        <div class="WebPartBox_SteelBlueFooter_Right"></div>
    </div>


 