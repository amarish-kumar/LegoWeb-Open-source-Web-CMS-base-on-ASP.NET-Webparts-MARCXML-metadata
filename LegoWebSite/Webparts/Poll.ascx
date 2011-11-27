<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POLL.ascx.cs" Inherits="Webparts_POLL" %>

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
    
<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>		 
		 
            <div runat="server" id="divMessage" class="mInfo" visible="false" />
            <div id="divPoll" runat="server">
                <div class="poll-question">
					<div style="padding:3px">
						 <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
					</div>
                </div>
                <div id="divChoices" class="poll-choices" runat="server" visible="true">
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
                                    BorderStyle="Outset" Font-Bold=true ForeColor="White" 
                    BorderColor="#CCFFFF" BackColor="#0066FF"/>
                            </td>
                        </tr>            
                    </table>
                </div>
                <div id="divVoting" runat="server" visible="false" class="poll-choices">
                    <table width="100%" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                    <td align="center" valign="middle">
                        <img src="Captcha.aspx"/>
                    </td>
                    </tr>
                    <tr>
                    <td align="right">
                    <asp:TextBox ID="txtVoteConfirmNumber" runat="server" Width="100"></asp:TextBox>
                    <asp:Button ID="btnConfirmVote" Text="Xác Nhận" runat="server" OnClick="btnConfirmVote_OnClick" BorderStyle="Outset" Font-Bold=true ForeColor="White" 
                    BorderColor="#CCFFFF" BackColor="#0066FF"/>        
                    </td>
                    </tr>
                    </table>        
                </div>
                <div id="divResult" runat="server" visible="false" class="poll-choices">
                
                </div>        
            </div>

<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>




 