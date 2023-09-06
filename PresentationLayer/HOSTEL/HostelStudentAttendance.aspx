<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelStudentAttendance.aspx.cs" Inherits="ACADEMIC_HostelStudentAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px">
                        HOSTEL ATTENDANCE
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                            font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                    font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record&nbsp;&nbsp;
                                    <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                    Delete Record
                                </p>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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

                        <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
                        </ajaxToolKit:AnimationExtender>
                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
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
                        </ajaxToolKit:AnimationExtender>
                    </td>
                </tr>
            </table>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td>
                        <fieldset class="fieldset">
                            <legend class="legend">Student Detail </legend>
                            <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="width: 119px">
                                            Reg. No.:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" ToolTip="Please Enter Student Registration No. "
                                                Font-Bold="True"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                                                ErrorMessage="Please Enter User Id" SetFocusOnError="True" ValidationGroup="Search"
                                                Display="None"></asp:RequiredFieldValidator>
                                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="width: 119px">
                                            Regno :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblregno" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                      <td rowspan="6" style="width: 103px" valign="top">
                                            <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                        </td>
                                        <td rowspan="6" valign="top">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 119px">
                                            Name :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td style="width: 119px">
                                            Academic Year :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="display:none"  >
                                        <td  style=" display:"none" >
                                            Scheme :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 119px" >
                                            Semester :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblactivity" runat="server" Visible="false" ></asp:Label>
                                            <%--&nbsp;&nbsp; &nbsp;&nbsp;
                                    Section :&nbsp;<asp:Label ID="lblSection" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Hostel Name :
                                        </td>
                                        <td style="width: 353px">
                                            <asp:Label ID="lblhostel" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdhostteler" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 353px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <b>
                                                <asp:Label ID="lblMsg" runat="server" Visible="false"> 
                                    style="color:Red;">*</span></asp:Label>
                                            </b>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                              </fieldset>
                                <fieldset class="fieldset">
                            <asp:Panel ID="pnlatt" runat="server">
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="width: 210px">
                                       <span style="color:Red;">*</span>Are You Present in Hostel Today ?
                                        </td>
                                        <td style="width: 150px">
                                        <asp:RadioButtonList ID="Rdbattendance" AutoPostBack="true" runat="server" 
                                        RepeatDirection="Horizontal" onselectedindexchanged="Rdbattendance_SelectedIndexChanged" >
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                          </asp:RadioButtonList>
                                         </td>
                                         <td id="tdreason" visible="false" runat="server" style="width: 70px">
                                           Reason :
                                         </td>
                                          <td id="tdtxtreason">
                                          <asp:TextBox ID="txtreason" Visible="false" TextMode="MultiLine" Width="300px"  runat="server" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                    <td rowspan="3" align="center"   >
                                    </td>
                                    <td>
                                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" onclick="btnsubmit_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel"  Text="Cancel" runat="server" onclick="btnCancel_Click" />
                                    </td>
                                    </tr>
                                     </table>
                             </asp:Panel>
                             </fieldset> 
                             
                      
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
