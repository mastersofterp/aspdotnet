<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeesRegisterReport.aspx.cs" Inherits="ACCOUNT_FeesRegisterReport" Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    FEES REGISTER
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
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
                                <%--  Enable the button so it can be played again --%>
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

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
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
                <td style="padding: 10px" colspan="6">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 45%">
                    <asp:RadioButton ID="rdoGenralFees" runat="server" Text="General Fees" GroupName="FeeType"
                        AutoPostBack="true" OnCheckedChanged="rdoGenralFees_CheckedChanged" Checked="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdoMiscFees" runat="server" Text="Miscellaneous Fees" GroupName="FeeType"
                        AutoPostBack="true" OnCheckedChanged="rdoMiscFees_CheckedChanged" />
                </td>
                <td style="width: 45%">
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    From Date
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtFrmDate" runat="server" Width="12%" Style="text-align: right"
                        AutoPostBack="True"/>
                    &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                    </ajaxToolKit:MaskedEditExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp; Upto Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" Width="12%"
                        AutoPostBack="True"/>
                    &nbsp;<asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                        TargetControlID="txtUptoDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                        MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                    </ajaxToolKit:MaskedEditExtender>
                    <input id="hdnBal" runat="server" type="hidden" />
                    <input id="hdnMode" runat="server" type="hidden" />
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr id="row18" runat="server">
                <td class="form_left_label" style="width: 10%; height: 19px;">
                    Degree
                </td>
                <td class="form_left_text" style="height: 19px; width: 45%;">
                    <asp:DropDownList ID="ddlDegree" runat="server" Width="150px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 45%;">
                    &nbsp;
                </td>
            </tr>
            <tr id="row4" runat="server">
                <td class="form_left_label" style="width: 10%; height: 19px;">
                    Receipt Type&nbsp;
                </td>
                <td class="form_left_text" colspan="2" style="width: 45%">
                    <asp:DropDownList ID="ddlRecept" runat="server" Width="150px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlRecept_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    &nbsp;
                </td>
                <td colspan="5">
                    <asp:Button ID="btnRP" runat="server" Text="Show Fees Register" Width="176px" OnClick="btnRP_Click" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 40%;">
                    &nbsp;
                </td>
                <td style="width: 10%; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td style="width: 10%;">
                    &nbsp;
                </td>
                <td style="width: 300px;">
                    <asp:TextBox ID="lblCurBal" runat="server" Height="23px" Width="80px" BorderColor="White"
                        BorderStyle="None" Style="background-color: Transparent; margin-left: 6px;" ReadOnly="True"
                        Font-Size="XX-Small"></asp:TextBox>
                    <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="12px" BorderColor="White"
                        BorderStyle="None" Style="background-color: Transparent; margin-left: 6px;" ReadOnly="True"
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td style="padding: 10px; text-align: right" colspan="6">
                    <asp:UpdatePanel ID="UPDLedger" runat="server">
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="Left" style="font-size: medium">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
