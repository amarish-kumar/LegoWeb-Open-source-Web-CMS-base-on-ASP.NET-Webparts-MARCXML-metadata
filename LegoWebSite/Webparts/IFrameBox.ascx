<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IFrameBox.ascx.cs" Inherits="Webparts_IFrameBox" %>

<div class="WebPartBox_AquaRoundHeader">
    <div class="ttright">
        <p class="ttmid">
            <span class="textleft"><asp:Label ID="iFrameBoxTitle" runat="server"></asp:Label></span>
        </p>
    </div>
</div>

<div class="WebPartBox_AquaRoundBody">
         <div class ="divStock" runat="server">
            <iframe id="iframebox" runat="server" scrolling="no" height="98" frameborder="0" src="blank.html" marginheight="0" marginwidth="0" style="width:100%; height:100%;"> </iframe>                   
        </div>
</div>



