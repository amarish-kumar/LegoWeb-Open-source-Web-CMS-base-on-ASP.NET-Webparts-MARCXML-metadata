<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GGVNEXPSECURITIES.ascx.cs" Inherits="Webparts_GGVNEXPSECURITIES" %>
                <script type="text/javascript">
                    function thaydoi(t, s, u, v) {
                        if (t.className == "") {
                            document.getElementById(s).className = "";
                            t.className = "gach-chan";
                            document.getElementById(u).style.display = "block";
                            document.getElementById(v).style.display = "none";
                        }
                    }
                    function loadStockIframes()
                    {
                        var t = setTimeout("loadStockData()", 1000);
                    }
                    function loadStockData() {
                        var IframeHCMStock = document.getElementById("IframeHCMStock");
                        IframeHCMStock.src = "http://vnexpress.net/User/ck/hcms/HCMStockSmall.asp";

                        var IframeHNStock = document.getElementById("IframeHNStock");
                        IframeHNStock.src = "http://vnexpress.net/User/ck/hns/HNStockSmall.asp";
                    }
                </script>
                <style type="text/css">                
                .HeaderStock 
                {
                	color: #FFFFFF;
                	font-size: 11px;
                	height: 21px;
                	text-align: center;
                }
                .HeaderStock1 
                {
                	color: #FFFFFF;
                	font-family: Arial;
                	font-size: 11px;
                	text-align: center;
                }
                #chHOSE 
                {
                	color: #313494;
                	float: left;
                	font-weight: bold;
                	padding: 3px 0 5px;
                	text-align: center;
                	width: 50%;
                }
                #chHASTC 
                {
                	color: #313494;
                	float: right;
                	font-weight: bold;
                	padding: 3px 0 5px;
                	text-align: center;
                	width: 50%;
                }
                .gach-chan 
                {
                	background-color: #8CBACE;
                	text-decoration: underline;
                }

                </style>

<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>

                <div class="side-box">
                <div style="clear:both"><div class="" onclick="thaydoi(this,'chHASTC','chisovn','chisoha')" style="cursor:pointer;/cursor:hand;font-size: 13px;" id="chHOSE">HOSE</div><div onclick="thaydoi(this,'chHOSE','chisoha','chisovn')" style="cursor:pointer;/cursor:hand;font-size: 13px;" id="chHASTC" class="gach-chan">HASTC</div></div>
                   <div id="chisovn" style="clear:both; display: none;">
                   <table cellpadding="0" cellspacing="0" style="background-color:#CE2429;  border-collapse:collapse; " width="100%">
                   <tr>
                   <td class="HeaderStock" style="height:31px; border-right:solid 1px white">
                   Mã CK
                   </td>
                   <td class="HeaderStock" style="height:31px; border-right:solid 1px white">
                   TC
                   </td>
                   <td style="height:31px; border-right:solid 1px white">
                   <table cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                   <td colspan="2" class="HeaderStock" style="border-bottom:solid 1px white">Khớp lệnh</td>
                   </tr>
                   <tr>
                   <td class="HeaderStock1" style="border-right:solid 1px white; background-color:#AF637D">
                   Giá
                   </td>
                   <td class="HeaderStock1" style="background-color:#AF637D">
                   KL
                   </td>
                   </tr>
                   </table>
                   </td>
                    <td class="HeaderStock">
                    +/-
                    </td>
                   </tr>
                   </table>

                    <div style="width:100%;height:309px;overflow:auto;">
                        <iframe id="IframeHCMStock" name="ifrmChungKhoan" height="305" width="100%"   src="" frameborder="0" scrolling="no"></iframe>
                    </div>
                   </div> 
                   <div style="clear:both;display: block; " id="chisoha" class="cl">
                                      <table cellpadding="0" cellspacing="0" style="background-color:#CE2429;  border-collapse:collapse; " width="100%">
                   <tr>
                   <td class="HeaderStock" style="height:31px; border-right:solid 1px white">
                   Mã CK
                   </td>
                   <td class="HeaderStock" style="height:31px; border-right:solid 1px white">
                   TC
                   </td>
                   <td style="height:31px; border-right:solid 1px white">
                   <table cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                   <td colspan="2" class="HeaderStock" style="border-bottom:solid 1px white">Khớp lệnh</td>
                   </tr>
                   <tr>
                   <td class="HeaderStock1" style="border-right:solid 1px white; background-color:#AF637D">
                   Giá
                   </td>
                   <td class="HeaderStock1" style="background-color:#AF637D">
                   KL
                   </td>
                   </tr>
                   </table>
                   </td>
                    <td class="HeaderStock">
                    +/-
                    </td>
                   </tr>
                   </table>
                        <div style="width:100%;height:309px;overflow:auto;">
                            <iframe id="IframeHNStock" name="ifrmChungKhoan" height="305" width="100%"   src="" frameborder="0" scrolling="no"></iframe>

                        </div>
                   </div> 
            </div>
            
<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>

                                