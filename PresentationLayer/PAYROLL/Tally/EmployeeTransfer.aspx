<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeTransfer.aspx.cs" Inherits="PAYROLL_Tally_EmployeeTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Flash the text/border red and fade in the "close" button --%>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Page Title --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">PAYROLL TALLY TRANSFER
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
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
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>


            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblDetails" runat="server">
                <tr>
                    <td style="padding-left: 10px; padding-right: 10px; width: 60%">
                        <br />
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Selection Criteria</legend>
                            <span style="color: #FF0000; font-size: small">* Marked Is Mandatory !</span>
                            <table cellpadding="0" cellspacing="0" width="100%">

                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%">

                                            <tr>
                                                <td class="form_left_label" style="width: 20%;">
                                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                                    <asp:Label ID="lblCashBook" CssClass="control-label" runat="server" Text="Staff Type"></asp:Label>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:DropDownList ID="ddlstafftype" runat="server" Width="200px" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCashbook" runat="server" ControlToValidate="ddlstafftype"
                                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Submit"
                                                        SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="form_left_label" style="width: 20%;">
                                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                                    <asp:Label ID="lblPayMonth" CssClass="control-label" runat="server" Text="Pay Month"></asp:Label>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:DropDownList ID="ddlPayMonth" runat="server" Width="200px" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPayMonth"
                                                        Display="None" ErrorMessage="Please Select Pay Month" ValidationGroup="Submit"
                                                        SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="form_left_label" style="width: 20%;">
                                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                                    <asp:Label ID="lblServerName" CssClass="control-label" runat="server" Text="Pay Date"></asp:Label>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:TextBox ID="txtPayDate" runat="server" Width="100px" TabIndex="7" ToolTip="Please Enter Pay Date" />
                                                    <asp:Image ID="imgCalDateOfBirth" runat="server" src="../../images/calendar.png" Style="cursor: pointer"
                                                        TabIndex="8" Height="16px" />
                                                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtPayDate"
                                                        Display="None" ErrorMessage="Please Enter Pay Date" SetFocusOnError="True"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtPayDate" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtPayDate"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter Pay Date"
                                                        ControlExtender="meeDateOfBirth" ControlToValidate="txtPayDate" IsValidEmpty="False"
                                                        InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="Submit" SetFocusOnError="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 20%;"></td>
                                                <td class="form_left_label" style="width: 40%;"></td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 20%;"></td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:Button ID="btnShow" runat="server" TabIndex="5" Text="Show" ToolTip="Click to Save" ValidationGroup="Submit" class="btn btn-primary" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnShow_Click" Font-Bold="true" Height="25px" Width="80px" />
                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="btn btn-warning" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClientClick="Cancel()" OnClick="btnCancel_Click" Font-Bold="true" Height="25px" Width="80px" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" Font-Bold="true" Height="25px" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td id="Td1" style="padding-left: 10px; padding-right: 10px; width: 40%" valign="top" runat="server" visible="false">
                        <br />
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Amount Description</legend>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="form_left_label" style="width: 20%;">
                                                    <strong>CASH</strong>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:Label ID="lblCashAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 20%; font: bold">
                                                    <strong>DEMAND DRAFT</strong>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:Label ID="lblDDAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td class="form_left_label" style="width: 20%; font: bold">
                                                    <strong>CHEQUE</strong>
                                                </td>

                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:Label ID="lblChequeAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 20%; font: bold">
                                                    <strong>TOTAL AMOUNT</strong>
                                                </td>
                                                <td class="form_left_label" style="width: 40%;">
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 20%; padding-bottom: 30px"></td>

                                                <td class="form_left_label" style="width: 40%;"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="Table1" runat="server">
                <tr>
                    <td style="padding-left: 10px; padding-right: 10px; width: 60%">
                        <br />
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <asp:Panel ID="DivReceipt" runat="server" Visible="false">
                                            <table cellpadding="1" cellspacing="1" width="80%">
                                                <tr>
                                                    <td>
                                                        <fieldset class="vista-grid">
                                                            <legend class="titlebar">EMP INFO</legend>
                                                            <asp:GridView ID="grdEmpInfo" runat="server" HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnSubmit_Click" Font-Bold="true" Height="25px" Width="100px" />
                                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-primary" Text="Transfer" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnTransfer_Click" Font-Bold="true" Height="25px" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

