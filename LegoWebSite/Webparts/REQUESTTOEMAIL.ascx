<%@ Control Language="C#" AutoEventWireup="true" CodeFile="REQUESTTOEMAIL.ascx.cs" Inherits="Webparts_REQUESTTOEMAIL" %>
<asp:Literal ID="litBoxTop" runat="server"></asp:Literal>

<div id="divSendRequest" runat="server" visible="true">
                 <table width="100%" cellpadding="1" cellspacing="2" border="0">
                    <tbody>
                        <tr>
                            <th colspan="3"></th>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%">
                                <span class="request-label" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request-label"><%=Resources.strings.RequestSenderName%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtSenderName" CssClass="request-text-box" runat="server" 
                                    Width="300px"></asp:TextBox>                
                            </td>
                            <td align="left">
                            <asp:RequiredFieldValidator ID="SenderNameRequired" runat="server" ControlToValidate="txtSenderName" ErrorMessage="Sender name is required!"
                                     ToolTip="Sender name is required." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                            </td>
                        </tr>        
                        <tr>
                            <td align="right">                
                                <span class="request-label"><%=Resources.strings.Phone%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtSenderPhoneNumber" CssClass="request-text-box" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredSenderPhoneNumber" runat="server" 
                                    ControlToValidate="txtSenderPhoneNumber" Display="Dynamic" ErrorMessage="Your phone number is required" 
                                    ValidationGroup="ContactInfo" SetFocusOnError="True">(*)</asp:RequiredFieldValidator>
                                &nbsp;</td>
                        </tr>        
                        <tr>
                            <td align="right">
                                <span class="request-label" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request-label"><%=Resources.strings.Email%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtSenderEmail" CssClass="request-text-box" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td align="left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSenderEmail" ErrorMessage="Sender email is required!"
                                     ToolTip="Sender email is required." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                                 <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="txtSenderEmail"  ErrorMessage="Invalid email address!" 
                                   ToolTip="Invalid email address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">                
                                <span class="request-label"><%=Resources.strings.Title%></span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtRequestEmailSubject" CssClass="request-text-box" Width="300px" 
                                    runat="server"></asp:TextBox>                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredTitle" runat="server" 
                                    ControlToValidate="txtRequestEmailSubject" ErrorMessage="Request title is required!" 
                                    SetFocusOnError="True" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">               
                                <span class="request-label"><%=Resources.strings.SendTo%></span>
                            </td>
                            <td>                
                               <asp:DropDownList ID="drlistToEmailAddress" 
                                    CssClass="request-text-box requestsentto" Width="300px" runat="server" 
                                    AppendDataBoundItems="False" EnableTheming="True" 
                                    ToolTip="Select destination address"> </asp:DropDownList> 
                                    

                            </td>
                            <td>
                                <asp:RequiredFieldValidator id="RequiredFieldComboxEmail"  
                                      runat="server" ErrorMessage="You must select destination address" 
                                      ControlToValidate="drlistToEmailAddress" 
                                      InitialValue="none" Display="Dynamic" ValidationGroup="ContactInfo" 
                                    SetFocusOnError="True">(*)</asp:RequiredFieldValidator>
                           </td>
                        </tr>        
                        <tr>
                            <td align="right">
                                <span class="request-label" style="color: red;font-weight:normal;">(*)</span>
                                <span class="request-label"><%=Resources.strings.Content%></span>
                            </td>            
                            <td class="style1">
                                <asp:TextBox ID="txtRequestEmailBody" CssClass="request-text-box" 
                                    TextMode="MultiLine" Height="120" Width="600px" runat="server"></asp:TextBox>                
                            </td>
                            <td align="left">
                            <asp:RequiredFieldValidator ID="RequestEmailBodyRequired" runat="server" ControlToValidate="txtRequestEmailBody" ErrorMessage="Request content is required!"
                                     ToolTip="Request content is required." Display="Dynamic" SetFocusOnError="true" ValidationGroup="ContactInfo">(*)</asp:RequiredFieldValidator>                
                            </td>
                        </tr>                                              
                        <tr>  
                            <td>                
                            </td>          
                            <td align="right" class="style1">
                                <asp:Button ID="btnSend" CssClass="request-button" runat="server" Text="Send" 
                                    OnClick="cmdSend_Click" ValidationGroup="ContactInfo"/>
                                <asp:Button ID="btnCancel" CssClass="request-button" runat="server" 
                                    Text="Cancel" PostBackUrl="~/Default.aspx"/>
                            </td>                                    
                            <td>
                            </td>            
                        </tr>                         
                    </tbody>
                </table>
</div>
<div id="divSendStatus" runat="server" visible="false">
    <asp:Literal ID="litSendRequestStatus" runat="server"></asp:Literal>
</div>
<asp:Literal ID="litBoxBottom" runat="server"></asp:Literal>

