<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChequePrinting.aspx.cs" Inherits="ChequePrinting" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="Custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">

        function OpenChequeModifyWindow() {
            var popUrl = 'ChequeEntryModifications.aspx?obj=' + 'ChequePrinting';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=50,width=600px,height=300px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;

        }


        function updateValues(popupValues) {
            alert('dasfdf');
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }



        function SetCurrentChequeNo(obj) {
            document.getElementById('ctl00_ContentPlaceHolder1_txtChqCurNo').value = obj.value;
            return false;

        }
        function CheckNumeric(obj) {


            var k = (window.event) ? event.keyCode : event.which;

            // alert(k);

            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46 || k == 13 || k == 110) {
                if (obj.value == '') {
                    alert('Field Cannot Be Blank');
                    obj.focus();
                    return false;
                }
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k > 45 && k < 58 || k > 95 && k < 106) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {

                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
    </script>

    <div style="width: 100%" onload="return SetAmountToWord();">
        <table cellpadding="0" cellspacing="0" width="98%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    CHEQUE PRINTING
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
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
                <td style="padding: 10px">
                    <%--<div id="divCompName" runat="server" class="account_compname">
                    </div>--%>
                    <fieldset  class="vista-grid" style="width:90%;">
                        <legend class="titlebar">Add/Modify - Cheque Printing</legend>
                        <asp:UpdatePanel ID="UPDLedger" runat="server">
                            <ContentTemplate>
                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="true" PopupControlID="pnl" OkControlID="btnno" TargetControlID="btnyes">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnl" runat="server" Visible="false">
                                    <asp:Label ID="lblprint" runat="server" Text="Did You Print This Cheque ?"></asp:Label>
                                    <asp:Button ID="btnyes" runat="server" Text="Yes" />
                                    <asp:Button ID="btnno" runat="server" Text="No" />
                                </asp:Panel>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td class="form_left_label" colspan="4">
                                            <asp:HiddenField ID="hdnPartyNo" runat="server" OnValueChanged="hdnPartyNo_ValueChanged" />
                                            <br />
                                            Note : <span style="color: #FF0000">* Marked is mandatory !</span><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Party Name:
                                        </td>
                                        <td class="form_left_text" style="width: 40%" colspan="3">
                                            <Custom:AutoSuggestBox ID="txtPartyName" runat="server" DataType="Payee" ResourcesDir="AutoSuggestBox"
                                                ToolTip="Please Enter Party Name" Width="100%" TabIndex="1">
                                            </Custom:AutoSuggestBox>
                                            <asp:RequiredFieldValidator ID="rfvLedgerName" runat="server" ControlToValidate="txtPartyName"
                                                Display="None" ErrorMessage="Please Enter Party Name" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtPartyName"
                                                WatermarkText="Press space to get all party names." WatermarkCssClass="watermarked" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <%--<span style="color: #FF0000; font-weight: bold">*</span>--%>Address:
                                        </td>
                                        <td class="form_left_text" style="width: 40%" colspan="3">
                                            <asp:TextBox ID="txtAddress" runat="server" ToolTip="Please Enter Address" Width="100%"
                                                ValidationGroup="submit" TabIndex="2" Style="text-align: left"></asp:TextBox>
                                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Address"
                                                ControlToValidate="txtAddress" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            &nbsp;Party Account No :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox ID="txtAccountCode" runat="server" ToolTip="Please Enter Party Account No."
                                                Width="19.5%" ValidationGroup="submit" TabIndex="3" Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAccountCode" runat="server" ValidChars="0123456789" TargetControlID="txtAccountCode"></ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Voucher No :
                                        </td>
                                        <td class="form_left_text" style="width: 8.7%;">
                                            <asp:TextBox ID="txtVrNo" runat="server" ToolTip="Please Enter Party Voucher No."
                                                Width="95%" TabIndex="4" Style="text-align: right"></asp:TextBox>
                                            
                                            
                                        </td>
                                        <td class="form_left_text" style="width: 8.7%;">
                                        &nbsp;&nbsp;&nbsp;Voucher Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtVrDate" runat="server" Style="text-align: right" ToolTip="Please Enter Voucher Date"
                                                Width="25%" TabIndex="5" />
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                TargetControlID="txtVrDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtVrDate">
                                            </ajaxToolKit:MaskedEditExtender></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            &nbsp; Bill No :
                                        </td>
                                        <td class="form_left_text" style="width: 8.7%;">
                                            <asp:TextBox ID="txtbillno" runat="server" ToolTip="Please Enter Bill No." Width="95%"
                                                TabIndex="6" Style="text-align: right"></asp:TextBox>
                                            
                                        </td>
                                        <td class="form_left_text">
                                            &nbsp;&nbsp;&nbsp;Bill Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtbilldate" runat="server" Style="text-align: right" ToolTip="Please Enter Bill Date"
                                                Width="25%" TabIndex="7" />
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft"
                                                TargetControlID="txtbilldate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtbilldate">
                                            </ajaxToolKit:MaskedEditExtender></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Amount :
                                        </td>
                                        <td class="form_left_text" style="width: 8.7%;">
                                            <asp:TextBox ID="txtAmount" runat="server" ToolTip="Please Enter Amount" Width="95%"
                                                TabIndex="8" Style="text-align: right"></asp:TextBox>
                                            
                                        </td>
                                        <td class="form_left_text">
                                            &nbsp;&nbsp;&nbsp;Department
                                            </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtdept" runat="server" Style="text-align: right" ToolTip="Please Enter Department"
                                                Width="40%" TabIndex="9" /></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>In Words:
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox ID="txtInWords" runat="server" ToolTip="Please Enter In Words Amount"
                                                Width="100%" TabIndex="10" Style="text-align: left"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                                                Display="None" ErrorMessage="Please Enter Amount" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Bank Name:
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <Custom:AutoSuggestBox ID="txtBank" runat="server" DataType="BankCheque" ResourcesDir="AutoSuggestBox"
                                                TabIndex="11" ToolTip="Please Enter Bank Name" Width="100%">
                                            </Custom:AutoSuggestBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBank"
                                                Display="None" ErrorMessage="Please Enter Bank Name" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>A/c Name:
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <Custom:AutoSuggestBox ID="txtBnkAccName" runat="server" DataType="BankCheque" ResourcesDir="AutoSuggestBox"
                                                TabIndex="12" ToolTip="Please Enter A/c Name" Width="100%">
                                            </Custom:AutoSuggestBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBnkAccName"
                                                Display="None" ErrorMessage="Please Enter A/c Name" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Acc. No :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox ID="txtAccNo" runat="server" ToolTip="Please Enter Account No." Width="40%"
                                                TabIndex="13" Style="text-align: right"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            <span style="color: #FF0000; font-weight: bold">*</span>Chq. No.:
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox ID="txtChqNo" runat="server" Style="text-align: right" TabIndex="14"
                                                ToolTip="Please Enter Account No." Width="20%" AutoPostBack="True" OnTextChanged="txtChqNo_TextChanged"
                                                MaxLength="6" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" TargetControlID="txtChqNo"
                                                FilterType="Custom, Numbers" />
                                            &nbsp;Chq. Date
                                            <asp:TextBox ID="txtChqDate" runat="server" Style="text-align: right" TabIndex="15"
                                                ToolTip="Please Enter Cheque Date" Width="20%" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image3" PopupPosition="BottomLeft"
                                                TargetControlID="txtChqDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChqDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 17%">
                                            &nbsp;
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAccNo"
                                                Display="None" ErrorMessage="Please Enter Account No." SetFocusOnError="True"
                                                ValidationGroup="submit" ></asp:RequiredFieldValidator>
                                            &nbsp;
                                        </td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtChqNo"
                                            Display="None" ErrorMessage="Please Enter Cheque No." SetFocusOnError="True"
                                            ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td> </td>
                                    </tr>
                                    <caption>
                                        </td> </td> </tr> &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtChqDate"
                                            Display="None" ErrorMessage="Please Enter Cheque Date" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                                &nbsp;Remark:&nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:TextBox ID="txtRemark" runat="server" Style="text-align: right" TabIndex="16"
                                                    TextMode="MultiLine" ToolTip="Please Enter Remark" Width="50%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                                <span style="color: #FF0000">*</span>Stamp&nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:CheckBox ID="chkStamp" runat="server" Text="Account Payee" />
                                                &nbsp;<asp:CheckBox ID="chkstatus" runat="server" Text="Cheque Cancel" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:Button ID="btnSubmit" runat="server" TabIndex="8" Text="Submit" ValidationGroup="submit"
                                                    Width="50px" OnClick="btnSubmit_Click" Style="height: 26px" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="9" Text="Cancel"
                                                    Width="50px" OnClick="btnCancel_Click" Style="height: 26px" />
                                                &nbsp;
                                                <asp:Button ID="btnGenerate" runat="server" CausesValidation="false" TabIndex="9"
                                                    Text="Generate" ValidationGroup="submit" Width="64px" OnClick="btnGenerate_Click"
                                                    Style="height: 26px" />
                                                &nbsp;<asp:Button ID="btnModify" runat="server" CausesValidation="false" TabIndex="9"
                                                    Text="Modify" Width="64px" OnClick="btnModify_Click" Style="height: 26px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 17%">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text" colspan="3">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function checkStatus(txt) {
            if (txt.value == 'c' || txt.value == 'C' || txt.value == 'D' || txt.value == 'd')
                txt.value = txt.value.toUpperCase();
            else {
                txt.value = '';
                alert('Please Enter Status as C for Credit & D for Debit');
                txt.focus = true;
            }
        }
    </script>

</asp:Content>
