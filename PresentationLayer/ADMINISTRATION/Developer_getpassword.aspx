<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Developer_getpassword.aspx.cs" Inherits="Developer_getpassword" Title=" " %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updPassword" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="70%">
            <tr>
                <td class="vista_page_title_bar" colspan="2" style="height:30px">
                   Developer Decrypt Password
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>  
                
            <%--PAGE HELP--%>
            <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
            <tr>
                 <td colspan="2">                           
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;"></div>
                
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton id="btnClose" runat="server" OnClientClick="return false;" Text="X" ToolTip="Close"
                                Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                               <span style="font-weight:bold;text-decoration: underline;">Page Help</span><br />
                               <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" /> Edit Record&nbsp;&nbsp;
                               <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" /> Delete Record
                            </p>
                            <p class="page_help_text"><asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                        </div>
                    </div>
                    
                    <script type="text/javascript" language="javascript">
                        // Move an element directly on top of another element (and optionally
                        // make it the same size)
                        function Cover(bottom, top, ignoreSize) {
                            var location = Sys.UI.DomElement.getLocation(bottom);
                            top.style.position = 'absolute';
                            top.style.top = location.y + 'px';
                            top.style.left = location.x + 'px';
                            if (!ignoreSize) {
                                top.style.height = bottom.offsetHeight + 'px';
                                top.style.width = bottom.offsetWidth + 'px';
                            }
                        }
                    </script>
                    
                    <ajaxToolkit:AnimationExtender id="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                        <Animations>
                            <OnClick>
                                <Sequence>
                                    <%-- Disable the button so it can't be clicked again --%>
                                    <EnableAction Enabled="false" />
                                    
                                    <%-- Position the wire frame on top of the button and show it --%>
                                    <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                    
                                    <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                                    <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                        <Move Horizontal="150" Vertical="-50" />
                                        <Resize Width="260" Height="280" />
                                        <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                    </Parallel>
                                    
                                    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                    <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                    <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                    <FadeIn AnimationTarget="info" Duration=".2"/>
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                    
                                    <%-- Flash the text/border red and fade in the "close" button --%>
                                    <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                        <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                    </Parallel>
                                    <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                        <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                        <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                    </Parallel>
                                </Sequence>
                            </OnClick>
                        </Animations>
                    </ajaxToolkit:AnimationExtender>
                    <ajaxToolkit:AnimationExtender id="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                            <OnClick>
                                <Sequence AnimationTarget="info">
                                    <%--  Shrink the info panel out of view --%>
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                        <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                        <FadeOut />
                                    </Parallel>
                                    
                                    <%--  Reset the sample so it can be played again --%>
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                    
                                    <%--  Enable the button so it can be played again --%>
                                    <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                </Sequence>
                            </OnClick>
                            <OnMouseOver>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                            </OnMouseOver>
                            <OnMouseOut>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                            </OnMouseOut>
                         </Animations>
                    </ajaxToolkit:AnimationExtender>
                </td>
            </tr>
            
            <tr>
               <td class="form_left_label">Username :</td>
               <td class="form_left_text">
                   <asp:TextBox ID="txtUserName" runat="server"
                   MaxLength="20" Width="150px" Wrap="False" ValidationGroup="getPassword" />  
                   <asp:RequiredFieldValidator ID="rfvUserName" runat="server" 
                        ControlToValidate="txtUserName" Display="None" ErrorMessage="User Name Required" 
                        ValidationGroup="getPassword"></asp:RequiredFieldValidator>          
               </td>
            </tr> 
            <tr id="trPwd" runat="server" visible="false">
               <td class="form_left_text">Password :</td>
               <td class="form_left_text">
                   <asp:Label ID="lblpassword" runat="server" Font-Bold="true" />                       
                   &nbsp;&nbsp;&nbsp;&nbsp;                       
               </td>
            </tr>
           
            <tr>
               <td class="form_left_label">&nbsp;</td>
               <td class="form_left_text">
                   <asp:Button ID="btnGetPassword" runat="server" Text="Get Password" 
                        OnClick="btnGetPassword_Click" ValidationGroup="getPassword" 
                       Width="100px" />&nbsp;&nbsp;
                  
                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        OnClick="btnCancel_Click" CausesValidation="False" Width="100px" />     
                   <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="getPassword" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
               </td>
             </tr>  
            <tr>
               <td class="form_left_label">&nbsp;</td>
               <td class="form_left_text">
                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />            
                    <asp:HiddenField ID="hfUano" runat="server" />
               </td>
             </tr>  
            <tr>
                <td class="form_left_label" colspan="2">
                 <asp:ListView ID="lvGetStud" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Student List</div>
                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                        <tr class="header">
                                        <th>
                                            Action
                                        </th>
                                            <th>
                                                Student Name
                                            </th>
                                            <th>
                                                Password
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" >
                                    <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("UA_NO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record"  TabIndex="6" />
                                    </td>
                                    <td>
                                        <%# Eval("UA_FULLNAME")%>
                                    </td>
                                    <td>
                                          <asp:Label ID="lblUserpass" runat="server" Text='<%# Eval("UA_PWD")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("UA_NO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record"  TabIndex="6" />
                                    </td>
                                    <td>
                                        <%# Eval("UA_FULLNAME")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserpass" runat="server" Text='<%# Eval("UA_PWD")%>'></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                </td>
            </tr>
           </table>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

