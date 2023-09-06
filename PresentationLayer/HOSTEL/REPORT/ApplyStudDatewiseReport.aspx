<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ApplyStudDatewiseReport.aspx.cs" Inherits="HOSTEL_REPORT_ApplyStudDatewiseReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="98%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">
                            HOSTEL REGISTRATION REPORT&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
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
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Select Criteria</legend>
                    <table width="100%" cellpadding="1" cellspacing="1" border="0">
                        <tr>
                            <td width="20%">
                                <span style="color: Red">*</span> Hostel Session :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" Width="200px" AppendDataBoundItems="True"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <span style="color: Red">* </span>From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromdate" runat="server" TabIndex="1" />
                                <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: hand" />
                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromdate" PopupButtonID="imgFromDate" />
                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromdate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                    OnInvalidCssClass="errordate" />
                                <ajaxToolKit:MaskedEditValidator ID="mevFromdate" runat="server" ControlExtender="meeFromDate"
                                    ControlToValidate="txtFromdate" IsValidEmpty="False" EmptyValueMessage="Please Select From Date"
                                    InvalidValueMessage="From date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                    Display="Dynamic" ValidationGroup="Submit" />
                          </td>
                          <tr>
                          <td>
                           
                                    <span style="color: Red">* </span> To Date
                              </td><td>
                                    <asp:TextBox ID="txtTodate" runat="server" TabIndex="1" />
                                    <asp:Image ID="imgTodate" runat="server" Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                                    <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodate"
                                        PopupButtonID="imgTodate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeTodate" runat="server" TargetControlID="txtTodate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                        OnInvalidCssClass="errordate" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevTodate" runat="server" ControlExtender="meeTodate"
                                        ControlToValidate="txtTodate" IsValidEmpty="False" EmptyValueMessage="Please Select To Date"
                                        InvalidValueMessage="To date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        Display="Dynamic" ValidationGroup="Submit" />
                                </td>
                        </tr>
                         <tr>
                            <td width="20%">
                             <span style="color: Red">&nbsp </span>Gender :
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlGender" runat="server" Width="200px" AppendDataBoundItems="True" TabIndex="1" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                             <span style="color: Red">&nbsp </span>Degree :
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlDegree" runat="server" Width="200px" AppendDataBoundItems="True" AutoPostBack="true"
                                    TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <tr>
                            <td width="20%">
                             <span style="color: Red">&nbsp </span>Branch :
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlBranch" runat="server" Width="200px" AppendDataBoundItems="True"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <tr>
                            <td width="20%">
                             <span style="color: Red">&nbsp </span>Semester :
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlSemester" runat="server" Width="200px" AppendDataBoundItems="True"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnReport" runat="server" Text="Student Apply List" ValidationGroup="Submit" OnClick="btnReport_Click"/>
                                <asp:Button ID="btnSummary" runat="server" Text="Summary Report" ValidationGroup="Submit" OnClick="btnSummary_Click"/>
                                <asp:Button ID="btnAppForm" runat="server" Text="Application Form" ValidationGroup="Submit" OnClick="btnAppForm_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                
                            </td>
                        </tr>
                        <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
