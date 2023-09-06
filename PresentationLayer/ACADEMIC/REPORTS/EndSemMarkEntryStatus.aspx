<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EndSemMarkEntryStatus.aspx.cs" Inherits="ACADEMIC_REPORTS_EndSemMarkEntryStatus" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MARK ENTRY STATUS
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
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
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
            </td>
        </tr>
    </table>
         <br />
        <fieldset class="fieldset">
            <legend class="legend">Mark Entry Status</legend>
                 <table width="100%" cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td width="20%">
                            Session :
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" Width="320px" onselectedindexchanged="ddlSession_SelectedIndexChanged" 
                                AppendDataBoundItems="True" AutoPostBack="True" ></asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvSession" runat="server" InitialValue="0" SetFocusOnError="true"
                            ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session." ValidationGroup="Show">
                           </asp:RequiredFieldValidator> 
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            Degree :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" Width="320px" 
                                onselectedindexchanged="ddlDegree_SelectedIndexChanged" 
                                AppendDataBoundItems="True" AutoPostBack="True"></asp:DropDownList>
                           
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            Branch :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" Width="320px" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" >
                            Semester :
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="4" Width="320px" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                    <tr>
                        <td width="20%" >
                            Exam Type :</td>
                        <td >
                            <asp:DropDownList ID="ddlSubID" runat="server" TabIndex="5" Width="320px" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                <%--<asp:ListItem Value="S1"> INTERNAL EXAM </asp:ListItem>--%>
                                <asp:ListItem Value="S2"> MID SEM-TH </asp:ListItem>
                                <asp:ListItem Value="S4">END SEM-PR</asp:ListItem>
                                <asp:ListItem Value="EXTER">END SEM-TH</asp:ListItem>
                            </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvSubId" runat="server" InitialValue="0" SetFocusOnError="true"
                            ControlToValidate="ddlSubID" Display="None" 
                                ErrorMessage="Please Select Exam Type." ValidationGroup="Show">
                           </asp:RequiredFieldValidator> 
                        </td>
                    </tr>
                   
                   <tr>
                   <td></td>
                   <td></td>
                   </tr>
                   <tr>
                   <td></td>
                   <td></td>
                   </tr>
                    <tr>
                        <td width="20%">
                            <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />
                        </td>
                        <td>
                            <asp:Button ID="btnReport" runat="server" Text="Mark Entry Status_Detailed Report" TabIndex="5" 
                                ValidationGroup="Show" onclick="btnReport_Click" Width="210px" />
                            &nbsp;
                            <asp:Button ID="btnExcel" runat="server" Text="Status_Detailed Excel Report" 
                                onclick="btnExcel_Click" ValidationGroup="Show" Width="200px"  />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" 
                                onclick="btnCancel_Click" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnSummeryReport" runat="server" 
                                Text="Mark Entry Status_Summery Report" TabIndex="5" 
                                ValidationGroup="Show" Width="220px" onclick="btnSummeryReport_Click" />
                        &nbsp;
                            <asp:Button ID="btnSummeryExcel" runat="server" Text="Status_Summery Excel Report" 
                                 ValidationGroup="Show" Width="200px" onclick="btnSummeryExcel_Click"  />
                        </td>
                    </tr>
                 </table>
        </fieldset> 
        <div id="divMsg" runat="server"></div>
</asp:Content>

