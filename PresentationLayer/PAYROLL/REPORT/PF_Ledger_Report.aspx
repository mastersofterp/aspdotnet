﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Ledger_Report.aspx.cs" Inherits="PAYROLL_REPORTS_PF_Ledger_Report"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                PF PERSONAL LEDGER REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
            <td style="padding-left: 10px">
                <br />
                <div style="text-align: left; width: 95%;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Employee/Staff for PF Personal Ledger Report</legend>
                        <br>
                        <table cellpadding="0" cellspacing="0" style="padding-left: 200px">
                            <tr>
                                <td class="form_left_text">
                                    <asp:RadioButton ID="radEmployee" Font-Bold="true" Text="Selected Employee" runat="server"
                                        GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />
                                </td>
                                <td class="form_left_text">
                                    <asp:RadioButton ID="radStaff" Font-Bold="true" Text="Selected Staff" runat="server"
                                        GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellpadding="0" cellspacing="0" id="tblEmployee" runat="server">
                            <tr runat="server" id="trEmployee">
                                <td class="form_left_label">
                                    Employee :
                                </td>
                                <td class="form_left_text" colspan="2">
                                    <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true"
                                        Width="300px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="trEligibleFor">
                                <td class="form_left_label">
                                    Eligible For :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trStaff">
                                <td class="form_left_label">
                                    Staff :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" Width="300px"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                        Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Fin.Year Start Date :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" OnTextChanged="txtFromDate_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                    &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                    <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                        PopupPosition="BottomLeft">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                        ValidationGroup="PF" SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                        ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                        InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                        Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                    Fin.Year End Date :
                                    <asp:TextBox ID="txtToDate" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                    &nbsp;<%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                    </ajaxToolKit:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <br />
                                    <asp:Button ID="butReport" Text="Show Report" runat="server" Width="120px" ValidationGroup="PF"
                                        OnClick="butReport_Click" />
                                    <asp:Button ID="butCancel" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
