<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Acc_for_Grid.aspx.cs" Inherits="ACCOUNT_Acc_for_Grid" Title="Untitled Page"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowLedger() {

            var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=90%,height=100%';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;

        }

        function ShowledgerReport(ledger, party_no,fromDate,Todate) {
            debugger;
            var popUrl = 'Acc_ledgerReportGrid.aspx?ledger=' + ledger + '&party_no=' + party_no + '&fromDate=' + fromDate + '&Todate=' + Todate;
            var name = 'popUp';
            //            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
            //         'status=no,toolbar=no,titlebar=no,' +
            //         'left=50,top=35,width=900px,height=650px';
            var appearence = 'center:yes; dialogWidth:1150px; dialogHeight:900px; edge:raised; ' +
  'help:no; resizable:no; scroll:no; status:no;';
            //var openWindow = window.open(popUrl, name, appearence);
            var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }


        function CloseWindow() {
            window.close();
        }
    </script>

    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    TRIAL BALANCE REPORT
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
                <td style="padding: 10px; width: 20%; text-align: left">
                    Report Type
                </td>
                <td>
                    <asp:RadioButton runat="server" ID="rdbGeneral" Text="General Trial Balance" GroupName="ReportType"
                        Checked="true" AutoPostBack="True" OnCheckedChanged="rdbGeneral_CheckedChanged" />&nbsp;
                    <asp:RadioButton runat="server" ID="rdbGroup" Text="Group Trial Balance" GroupName="ReportType"
                        OnCheckedChanged="rdbGroup_CheckedChanged" AutoPostBack="True" />
                </td>
            </tr>
            <tr id="trFAGroup" runat="server" visible="false">
                <td style="padding: 10px; width: 20%; text-align: left">
                    Select Final Account Group
                </td>
                <td>
                    <asp:DropDownList ID="ddlFAGroup" runat="server" Width="65%" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="--Please Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFAGroup" runat="server" ErrorMessage="Please Select Final Account Group"
                        ControlToValidate="ddlFAGroup" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trDate" runat="server">
                <td style="padding: 10px; width: 13%; text-align: left">
                    From Date
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtFrmDate" runat="server" Width="12%" Style="text-align: right"
                        AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" />
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
                        AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" />
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
            <tr id="trReportType" runat="server">
                <td style="padding: 10px; width: 20%; text-align: left">
                    Select Report Type :
                </td>
                <td style="width: 100%;">
                    <asp:RadioButton ID="rdbDetail" runat="server" Text="Detailed Trial Balance" Checked="true"
                        AutoPostBack="true" OnCheckedChanged="rdbDetail_CheckedChanged" GroupName="Report" />&nbsp;
                    <asp:RadioButton ID="rdbShortTrailBalanec" runat="server" Text="Summary Trial Balance"
                        Checked="false" AutoPostBack="true" OnCheckedChanged="rdbShortTrailBalanec_CheckedChanged"
                        GroupName="Report" />&nbsp;
                    <asp:RadioButton ID="rdbConsolidateTrialBalance" runat="server" Text="Consolidate Trial Balance"
                        GroupName="Report" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    &nbsp;
                </td>
                <td colspan="5">
                    <asp:Button ID="btnShow" runat="server" Text="Show In Grid" Width="102px" OnClick="btnShow_Click" />
                    &nbsp;
                    <asp:Button ID="btnShowTrialBalance" runat="server" Text="Show Reports" OnClick="btnShowTrialBalance_Click"
                        Width="102px" ValidationGroup="Report" />
                    &nbsp;<%--<asp:Button ID="btnShowBalanceSheet" runat="server" 
                        Text="Balance Sheet" onclick="btnShowBalanceSheet_Click" Width="90px"   />--%>
                    &nbsp;<%--<asp:Button ID="btnPL" runat="server" 
                        Text="Income Expenditure"  Width="120px" onclick="btnPL_Click"   />--%>
                    &nbsp;<%--<asp:Button ID="btnRP" runat="server" 
                        Text="Receipt Payment"  Width="120px" onclick="btnRP_Click"    />--%>
                    &nbsp;<%--<asp:Button ID="btndb" runat="server" 
                        Text="Day Book"  Width="120px" onclick="btndb_Click"     />--%>
                </td>
            </tr>
            <tr runat="server" id="trLinkNewVoucher" visible="false" align="left">
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkNewVoucher" Text="Click Here To create New Voucher" runat="server"
                        PostBackUrl="~/ACCOUNT/AccountingVouchers.aspx?ISBACK=TRUE"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnl" ScrollBars="Vertical" runat="server" Style="width: 100%; height: auto;
                        text-align: left" BorderColor="#0066FF">
                        <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand">
                            <HeaderTemplate>
                                <table class="datatable" width="100%">
                                    <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                        <th style="width: 7%;">
                                            &nbsp;
                                        </th>
                                        <th style="width: 33%; text-align: center">
                                            Particulars
                                        </th>
                                        <th style="text-align: right; width: 16.6%;">
                                            Opening Balance
                                        </th>
                                        <th style="text-align: right; width: 13.4%;">
                                            Debit
                                        </th>
                                        <th style="text-align: right; width: 13.4%;">
                                            Credit
                                        </th>
                                        <th style="text-align: right">
                                            Closing Balance
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                                <table width="100%" style="height: 90%">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: ThreeDFace; height: 2Px">
                                    <td style="width: 7%">
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("MGRP_NO")%>'
                                            ImageUrl="~/images/action_down.gif" ToolTip="Expand Record" Visible="false" />
                                    </td>
                                    <td style="font-weight: bold; width: 33%" align="left">
                                        <%#Eval("PARTYNAME")%>
                                    </td>
                                    <td style="font-weight: bold;" align="right">
                                        <asp:Label ID="lblOpbal" runat="server" Text='<%#Eval("OP_BALANCE1")%>'> </asp:Label>
                                        &nbsp;<%#Eval("OpbalMode")%>
                                    </td>
                                    <td style="font-weight: bold" align="right">
                                        <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("DEBIT")%>'> </asp:Label>
                                    </td>
                                    <td style="font-weight: bold" align="right">
                                        <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("CREDIT")%>'> </asp:Label>
                                    </td>
                                    <td style="font-weight: bold" align="right">
                                        <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                        &nbsp;<%#Eval("clBalMode")%>
                                    </td>
                                </tr>
                                <tr id="trPanel" runat="server">
                                    <td colspan="7">
                                        <%--ScrollBars="Auto"--%>
                                        <asp:Panel ID="pnl1" runat="server" Style="width: 100%;" BorderColor="#0066FF">
                                            <asp:ListView ID="lvGrp" runat="server" OnItemCommand="lvGrp_OnItemCommand">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid" style="width: 99.5%">
                                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                            <%--<tr class="header">
                                                                <th>
                                                                    &nbsp;
                                                                </th>
                                                                <th>
                                                                    Particulars
                                                                </th>
                                                                <th align="center" style="text-align: right">
                                                                    Opening Balance
                                                                </th>
                                                                <th align="center" style="text-align: right">
                                                                    Debit
                                                                </th>
                                                                <th align="center" style="text-align: right">
                                                                    Credit
                                                                </th>
                                                                <th align="center" style="text-align: right">
                                                                    Closing Balance
                                                                </th>
                                                            </tr>--%>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trLedger" runat="server">
                                                        <td style="width: 7%">
                                                            <asp:ImageButton ID="lnkLedgerReport" runat="server" ToolTip='<%# Eval("Party_name")%>'
                                                                ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                Visible="false" />
                                                        </td>
                                                        <td id="trPartyName" runat="server" >
                                                            <asp:Label ID="lblParty" Font-Size="12" runat="server" Text=' <%#Eval("PARTYNAME")%>'></asp:Label>
                                                        </td>
                                                        <td style="width: 16.6%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("OP_BALANCE1"))%>&nbsp;
                                                            <%#Eval("OpbalMode")%>
                                                        </td>
                                                        <td style="width: 13.5%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                        </td>
                                                        <td style="width: 13.4%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("CL_BALANCE1"))%>&nbsp;
                                                            <%#Eval("clBalMode")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr id="trLedger" runat="server">
                                                        <td style="width: 7%">
                                                            <asp:ImageButton ID="lnkLedgerReport" runat="server" ToolTip='<%# Eval("Party_name")%>'
                                                                ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                Visible="false" />
                                                        </td>
                                                        <td id="trPartyName" runat="server">
                                                            <asp:Label ID="lblParty" Font-Size="12" runat="server" Text=' <%#Eval("PARTYNAME")%>'></asp:Label>
                                                        </td>
                                                        <td style="width: 16.6%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("OP_BALANCE1"))%>&nbsp;
                                                            <%#Eval("OpbalMode")%>
                                                        </td>
                                                        <td style="width: 13.5%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                        </td>
                                                        <td style="width: 13.4%; text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <%# string.Format("{0:#,0.00}", Eval("CL_BALANCE1"))%>&nbsp;
                                                            <%#Eval("clBalMode")%>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <%--<tr runat="server" visible="false">
                                    <td>
                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" TargetControlID="pnl1"
                                            ExpandControlID="btnEdit" CollapseControlID="btnEdit" CollapsedImage="~/images/action_up.gif"
                                            ExpandedImage="~/images/action_down.GIF" Collapsed="False">
                                        </ajaxToolKit:CollapsiblePanelExtender>
                                    </td>
                                </tr>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <table width="100%">
                                    <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                        <th style="width: 7%;">
                                            &nbsp;
                                        </th>
                                        <th style="width: 33%; text-align: left; font-weight: bold">
                                            Grand Total
                                        </th>
                                        <th style="text-align: right; width: 16.6%;">
                                            &nbsp;
                                        </th>
                                        <th style="text-align: right; width: 13.4%;">
                                            <asp:Label runat="server" ID="lblTotalDebit"></asp:Label>
                                        </th>
                                        <th style="text-align: right; width: 13.4%;">
                                            <asp:Label runat="server" ID="lblTotalCredit"></asp:Label>
                                        </th>
                                        <th style="text-align: right">
                                            &nbsp;
                                        </th>
                                    </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
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
                    &nbsp;
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
                    <asp:ValidationSummary ID="vsTrialBalanceReport" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="Report" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
