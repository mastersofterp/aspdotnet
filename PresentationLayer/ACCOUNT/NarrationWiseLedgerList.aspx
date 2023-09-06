<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="NarrationWiseLedgerList.aspx.cs" Inherits="ACCOUNT_NarrationWiseLedgerList"
    Title=" " %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <style type="text/css">
        .textsize
        {
            resize: none;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckFields() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').value == '') {
                alert('Please Enter From Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').value == '') {
                alert('Please Enter Upto Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value == '') {
                alert('Please Enter Ledger.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
                return false;
            }
        }
        function popUpToolTip(CAPTION) {

            var strText = CAPTION;
            overlib(strText, 'Tool Tip', 'CreateSubLinks');
            return true;
        }

        function ShowledgerReport(ledger, party_no, fromDate, Todate) {
            debugger;

            var popUrl = 'Acc_ledgerReportGrid.aspx?ledger=' + ledger + '&party_no=' + party_no + '&fromDate=' + fromDate + '&Todate=' + Todate;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=1,' +
                     'status=no,toolbar=no,titlebar=no,' +
                     'left=50,top=35,width=900px,height=650px,scrollbars=1';
            //          var appearence = 'center:yes; dialogWidth:1150px; dialogHeight:900px; edge:raised; ' +
            //'help:no; resizable:no; scrollbars=1; status:no;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowLedger() {
            var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function AskSave() {
            if (confirm('Do You Want To Save The Transaction ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                return false;
            }
        }

        function CheckTranChar(obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Either C OR D');
                obj.focus();
            }
            return false;
        }

        function ShowHelp() {
            var popUrl = 'PopUp.aspx?fn=' + 'LedgerHelp';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=100,top=50,width=600px,height=300px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function SetFoc(obj) {
            obj.style.backgroundColor = SetTextBackColor();  // This function is created at Master page , register by javascript.
            var objRange = obj.createTextRange();
            objRange.moveStart("character", 0);
            objRange.moveEnd("character", obj.value.length);
            objRange.select();
            obj.focus();
        }

        function updateValues(popupValues) {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }
    
    
    </script>

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            DataTable();
        }
        function DataTable() {
            dt(document).ready(function() {
                dt(".DataGrid").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "bSort": false
                });
            });

            dt(document).ready(function() {
                dt("th.icon span").remove();
                dt("th.icon").css({ cursor: 'arrow' });
                dt("th.icon").unbind('click');
            });
        }

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <div style="z-index: 1; position: absolute;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 1024px; padding-left: 5px; background-color: Black; height: 950px;
                    padding: 250px 50px 50px 450px; opacity: 0.4; filter: alpha(opacity=40);">
                    <div style="">
                        <img src="../IMAGES/anim_loading_75x75.gif" />
                        Please Wait..
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div style="width: 100%; height: 591px;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="vista_page_title_bar" style="height: 30px" colspan="5">
                            NARRATION WISE LEDGER REPORT
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                                border: solid 1px #D0D0D0;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
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
                                        <%--  Shrink the info panel out of view --%>
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
                        <td style="padding: 10px" colspan="5">
                            <div id="divCompName" runat="server" class="account_compname">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset class="vista-grid" style="width: 75%;">
                                <legend class="titlebar">Narration Wise Ledger Report</legend>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="3" style="padding-left: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td>
                                            <tr>
                                                <td style="padding: 10px; width: 12%; text-align: left">
                                                    From Date
                                                </td>
                                                <td style="width: 1%">
                                                    <b>:</b>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtFrmDate" runat="server" Width="12%" Style="text-align: right"
                                                        AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" TabIndex="4" />
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
                                                        AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" TabIndex="5" />
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
                                            <tr>
                                                <td style="padding: 10px; width: 12%; text-align: left">
                                                    Enter Narration
                                                </td>
                                                <td style="width: 1%">
                                                    <b>:</b>
                                                </td>
                                                <td style="width: 40%;">
                                                    <asp:TextBox ID="txtAcc" Width="100%" runat="server" placeholder="Select Narration"
                                                        AutoPostBack="true" onkeydown="return (event.keyCode!=13);" ToolTip="Enter Narration"
                                                        TabIndex="1"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AuttxtItemName" runat="server" TargetControlID="txtAcc"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                        ServiceMethod="GetItems" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtAcc" runat="server" ControlToValidate="txtAcc"
                                                        ErrorMessage="Please Enter Narration Name" Display="None" ValidationGroup="LedReport"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px; width: 12%; text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 1%">
                                                </td>
                                                <td style="width: 50%;" colspan="3">
                                                    <asp:Button ID="bntShowGrid" runat="server" Text="Show in Grid" OnClick="btnShowGrid_Click"
                                                        Width="110px" Height="25px" />
                                                    &nbsp;
                                                    <asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" Width="120px"
                                                        OnClick="btnExportExcel_Click" Height="25px" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="110px" Height="25px"
                                                        OnClick="btnCancel_Click" TabIndex="2" />
                                                    <asp:ValidationSummary ID="vsLedger" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="LedReport" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="text-align: right" colspan="5">
                                                    <asp:Panel ID="GridPanel" runat="server" Width="100%">
                                                        <asp:GridView runat="server" ID="GridExcel" Caption="Narration Wise Ledger Report"
                                                            CaptionAlign="Top" CellPadding="4" ForeColor="#333333" GridLines="Both">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <%--   <sortedascendingcellstyle backcolor="#F5F7FB" />
                                                            <sortedascendingheaderstyle backcolor="#6D95E1" />
                                                            <sorteddescendingcellstyle backcolor="#E9EBEF" />
                                                            <sorteddescendingheaderstyle backcolor="#4870BE" />--%>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="padding: 10px; text-align: right" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <td style="padding: 10px" colspan="5">
                                <div id="div1" runat="server" class="account_compname">
                                </div>
                                <center id="grid" runat="server" visible="false">
                                    <fieldset class="vista-grid" style="width: 250%;">
                                        <legend class="titlebar">Narration Wise Ledger Report<br />
                                        </legend><span style="font-weight: normal"></span><span style="color: red; font-weight: normal">
                                        </span>
                                        <asp:Panel ID="pnl" runat="server" Style="width: 100%; text-align: left" BorderColor="#0066FF">
                                            <div class="DocumentList">
                                                <asp:Repeater ID="RptData" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="datatable" width="100%">
                                                            <tr class="header" style="font-weight: bold; background-color: ThreeDShadow;">
                                                                <td style="width: 10%; text-align: center">
                                                                    DATE
                                                                </td>
                                                                <td style="width: 10%; text-align: center">
                                                                    VCH NO
                                                                </td>
                                                                <td style="width: 40%; text-align: center">
                                                                    PARTY NAME
                                                                </td>
                                                                <td style="text-align: center; width: 15%">
                                                                    VCH TYPE
                                                                </td>
                                                                <td style="text-align: center; width: 10%">
                                                                    TRAN
                                                                </td>
                                                                <td style="text-align: center; width: 15%">
                                                                    AMOUNT
                                                                </td>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                        <table width="100%">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 10%">
                                                                <%# Eval("TRANSACTION_DATE")%>
                                                            </td>
                                                            <td style="text-align: center; font-weight: bold; width: 10%">
                                                                <%#Eval("VOUCHER_NO")%>
                                                            </td>
                                                            <td style="text-align: left; font-weight: bold; width: 40%">
                                                                <%#Eval("PARTY_NAME")%>
                                                            </td>
                                                            <td style="text-align: center; font-weight: bold; width: 15%">
                                                                <%#Eval("TRANSACTION_TYPE")%>
                                                            </td>
                                                            <td style="text-align: center; font-weight: bold; width: 10%">
                                                                <%#Eval("TRAN")%>
                                                            </td>
                                                            <td style="text-align: center; font-weight: bold; width: 15%">
                                                                <%#Eval("AMOUNT")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </asp:Panel>
                                    </fieldset>
                                </center>
                            </td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
