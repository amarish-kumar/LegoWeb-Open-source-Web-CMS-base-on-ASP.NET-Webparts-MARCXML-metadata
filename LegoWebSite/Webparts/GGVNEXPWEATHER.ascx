<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GGVNEXPWEATHER.ascx.cs" Inherits="Webparts_GGVNEXPWEATHER" %>
<style type="text/css">

</style>
<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>

                                <img alt="" src="http://vnexpress.net/Images/search.gif" style="float:left"/>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                    <ContentTemplate>
                                        <asp:DropDownList ID="dropCities" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="dropCities_SelectedIndexChanged" Font-Names="Tahoma" 
                                            Font-Size="11px" Width="112px" Height="22px">
                                            <asp:ListItem Selected="True" Value="Hanoi.xml">H&#224; Nội</asp:ListItem>
                                            <asp:ListItem Value="Haiphong.xml">Hải Ph&#242;ng</asp:ListItem>
                                            <asp:ListItem Value="HCM.xml">TP HCM</asp:ListItem>
                                            <asp:ListItem Value="Danang.xml">Đ&#224; Nẵng</asp:ListItem>
                                            <asp:ListItem Value="Sonla.xml">Sơn La</asp:ListItem>
                                            <asp:ListItem Value="Viettri.xml">Việt Tr&#236;</asp:ListItem>
                                            <asp:ListItem Value="Vinh.xml">Vinh</asp:ListItem>
                                            <asp:ListItem Value="Nhatrang.xml">Nha Trang</asp:ListItem>
                                            <asp:ListItem Value="Pleicu.xml">Pleicu</asp:ListItem>
                                        </asp:DropDownList>
                           
                                        <p id="img-Do">
                                        <asp:Literal ID="litTemperature" runat="server"></asp:Literal>
                                        </p>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>

<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>