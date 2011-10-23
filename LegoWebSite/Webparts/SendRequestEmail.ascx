<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendRequestEmail.ascx.cs" Inherits="Webparts_SendRequestEmail" %>
<div id="PanelBrowser">
    <div class="temp_5">
        <div class="title">
            <div class="fl"></div>
            <div class="fr"></div>
        </div>
        <div class="content">
            <div class="fc">
                <div id="TitleTopMenu" runat="server"> </div>
            </div>
             <div style="clear:both;"> </div>
            <div>
                 <table width="100%" cellpadding="1" cellspacing="2" border="0">
                    <tbody>
                        <tr>
                            <th colspan="3"></th>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%">
                                <span class="request_txt_title" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request_txt_title"><%=Resources.strings.UserName%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtContactUserName" CssClass="requesttxtclass" runat="server" 
                                    Width="300px"></asp:TextBox>                
                            </td>
                            <td align="left">
                            <asp:RequiredFieldValidator ID="ContactUserNameRequired" runat="server" ControlToValidate="txtContactUserName" ErrorMessage="Bạn chưa nhập họ tên!"
                                     ToolTip="Chưa nhập họ tên." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                            </td>
                        </tr>        
                        <tr>
                            <td align="right">                
                                <span class="request_txt_title"><%=Resources.strings.Phone%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtContactUserPhone" CssClass="requesttxtclass" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredPhoneNumber" runat="server" 
                                    ControlToValidate="txtContactUserPhone" Display="Dynamic" ErrorMessage="Bạn chưa nhập số điện thoại" 
                                    ValidationGroup="ContactInfo" SetFocusOnError="True">(*)</asp:RequiredFieldValidator>
                                &nbsp;</td>
                        </tr>        
                        <tr>
                            <td align="right">
                                <span class="request_txt_title" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request_txt_title"><%=Resources.strings.Email%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtContactUserEmail" CssClass="requesttxtclass" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td align="left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContactUserEmail" ErrorMessage="Bạn chưa nhập địa chỉ email!"
                                     ToolTip="Chưa nhập địa chỉ email." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                                 <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="txtContactUserEmail"  ErrorMessage="Địa chỉ email không hợp lệ" 
                                   ToolTip="Địa chỉ email không hợp lệ" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">                
                                <span class="request_txt_title"><%=Resources.strings.Title%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtContactTitle" CssClass="requesttxtclass" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredTitle" runat="server" 
                                    ControlToValidate="txtContactTitle" ErrorMessage="Bạn chưa nhập tiêu đề" 
                                    SetFocusOnError="True" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">               
                                <span class="request_txt_title"><%=Resources.strings.Service%></span>
                            </td>
                            <td>                
                               <asp:DropDownList ID="drlistContactService" 
                                    CssClass="requesttxtclass requestsentto" Width="300px" runat="server" 
                                    AppendDataBoundItems="False" EnableTheming="True" 
                                    ToolTip="Chọn email cần liên hệ"> </asp:DropDownList> 
                                    

                            </td>
                            <td>
                                <asp:RequiredFieldValidator id="RequiredFieldComboxEmail"  
                                      runat="server" ErrorMessage="Bạn chưa chọn địa chỉ email cần gửi" 
                                      ControlToValidate="drlistContactService" 
                                      InitialValue="none" Display="Dynamic" ValidationGroup="ContactInfo" 
                                    SetFocusOnError="True">(*)</asp:RequiredFieldValidator>
                           </td>
                        </tr>        
                        <tr>
                            <td align="right">
                                <span class="request_txt_title" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request_txt_title"><%=Resources.strings.Content%></span>
                            </td>            
                            <td class="style1">
                                <asp:TextBox ID="txtContactEmailContent" CssClass="requesttxtclass" 
                                    TextMode="MultiLine" Height="120" Width="600px" runat="server"></asp:TextBox>                
                            </td>
                            <td align="left">
                            <asp:RequiredFieldValidator ID="ContactEmailContentRequired" runat="server" ControlToValidate="txtContactEmailContent" ErrorMessage="Bạn chưa nhập nội dung!"
                                     ToolTip="Chưa nhập nội dung." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                            </td>
                        </tr>                                              
                        <tr>  
                            <td>                
                            </td>          
                            <td align="right" class="style1">
                                <asp:Button ID="cmdSend" CssClass="Request_Send" runat="server" Text="<%$Resources:strings,Send%>" OnClick="cmdSend_Click" ValidationGroup="ContactInfo"/>
                                <asp:Button ID="cmdCancel" CssClass="Request_Cencel" runat="server" 
                                    Text="<%$Resources:strings,Cancel%>" PostBackUrl="~/Default.aspx"/>
                            </td>                                    
                            <td>
                            </td>            
                        </tr> 
                        <tr>
                        <td colspan="3">           
                            <asp:Literal ID="SendMailMessage" runat="server"></asp:Literal>       
                        </td>
                        </tr>
                        
                    </tbody>
                </table>
            </div>
        </div>
        <div class="bottom">
            <div class="fl"></div>
            <div class="fr"></div>
        </div>
    </div>
</div>
