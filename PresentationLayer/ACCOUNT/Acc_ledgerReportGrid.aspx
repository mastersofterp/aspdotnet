<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Acc_ledgerReportGrid.aspx.cs"
    Inherits="Acc_ledgerReportGrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Configuration For Voucher Print</title>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/tabs.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/blue/linkedin-blue.css" rel="stylesheet" type="text/css" />
    <link href="includes/modalbox.css" rel="stylesheet" type="text/css" />

    <script type="text/jscript" language="javascript">
    debugger
        function VoucherModification(param, ledger, party_no, fromDate, Todate) {
            location.href = 'AccountingVouchers.aspx?obj=config,' + param + '&ledger=' + ledger + '&party_no=' + party_no + '&fromDate=' + fromDate + '&Todate=' + Todate;
        }

        function CloseWindow() {
            window.close();
        }
        
    </script>

</head>
<body class="body">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="z-index: 1; position: fixed; top: 40%; left: 40%; text-align: center;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <%--<div style="width: 100%; background-color: #FF0040; padding-left: 5px; font-size: large">
                    Page is Redirecting. Please Wait...
                </div>--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="3">
                    Ledger Report
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
                                <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
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
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    <center>
                        <fieldset class="vista-grid" style="width: 90%;">
                                <legend class="titlebar">Ledger Report<br />
                            </legend><span style="font-weight: normal"></span><span style="color: red; font-weight: normal">
                            </span>
                           
                            <asp:UpdatePanel ID="UPDMainGroup" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnl" ScrollBars="Vertical" runat="server" Style="width: 100%; height: 90%;
                                        text-align: left" BorderColor="#0066FF">
                                        <table width="100%">
                                        <tr>
                                        <td colspan="2" align="right">
                                        <asp:Button ID="btnReport" runat="server" Text="Report" onclick="btnReport_Click" Visible="false"/>
                                        </td>
                                        </tr>
                                            <tr align="right">
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lnkNewVoucher" runat="server" Text="Click Here To Create New Voucher" Visible="false"
                                                        PostBackUrl="~/ACCOUNT/AccountingVouchers.aspx?ISBACK=FALSE&pageno=332"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr style="font-size: medium; font-weight: bold">
                                                <td align="left">
                                                    Ledger :
                                                    <asp:Label ID="lblLedger" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 35%">
                                                    From&nbsp;
                                                    <asp:Label ID="lblFrm" runat="server"></asp:Label>
                                                    &nbsp;To&nbsp;
                                                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="datatable" width="100%" style=" height: 2Px">
                                                    <tr class="header" style="font-weight: bold;background-color: ThreeDShadow;">
                                                        <td style="width: 11%">
                                                            Date
                                                        </td>
                                                        <td style="width: 30%">
                                                            Particulars
                                                        </td>
                                                        <td style="width: 15%">
                                                            Voucher Type
                                                        </td>
                                                        <td style="width: 14%">
                                                            Voucher No
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            Debit
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            Credit
                                                        </td>
                                                    </tr>
                                                    <tr class="header" style="font-weight: bold;background-color:ThreeDFace; height: 2Px">
                                                        <td style="width: 11%">
                                                            <asp:Label ID="lblopDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%">
                                                            OPENING BALANCE
                                                        </td>
                                                        <td style="width: 15%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 14%">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lblopDebit" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lblopCredit" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                                <table  width="100%" style="height: 100%">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background-color: ThreeDShadow; height: 2Px" runat="server" visible="false">
                                                    <td style="width: 7%">
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("VOUCHER_NO")%>'
                                                            ImageUrl="~/images/edit.gif" ToolTip="Edit record" Visible="false" />
                                                        delete voucher should be maintained &nbsp;<%--<asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%# Eval("Voucher_No")%>'
                                                            CommandName="VoucherDelete" ImageUrl="~/images/delete.gif" ToolTip="Delete record"
                                                            OnClientClick="AskDelete()" />--%>
                                                    </td>
                                                    <td style="font-weight: bold" align="Center">
                                                        Voucher Type :<asp:Label ID="lblvchtype" runat="server" Text='<%#Eval("Vch_Type")%>'
                                                            Visible="false"> </asp:Label></td>
                                                    <td style="font-weight: bold" align="Center">
                                                        Voucher No.
                                                        <%#Eval("VOUCHER_NO")%>
                                                    </td>
                                                    <td style="font-weight: bold" align="Center">
                                                        Voucher Print
                                                        <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                            CommandArgument='<%# Eval("Voucher_No")%>' CommandName="VoucherPrint" ImageUrl="~/images/print.gif" />
                                                        <asp:HiddenField ID="hdnTransferEntry" runat="server" Value='<%# Eval("TRANSFER_ENTRY")%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <%--ScrollBars="Auto"--%>
                                                        <asp:Panel ID="pnl1" runat="server" Style="width: 100%; height: 100%" BorderColor="#0066FF">
                                                            <asp:ListView ID="lvGrp" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <table class="datatable" width="100%">
                                                                            <tr class="header" runat="server" visible="false">
                                                                                <th>
                                                                                    Date
                                                                                </th>
                                                                                <th>
                                                                                    Particulars
                                                                                </th>
                                                                                <th>
                                                                                    Voucher Type
                                                                                </th>
                                                                                <th>
                                                                                    Voucher No
                                                                                </th>
                                                                                <th align="center" style="text-align: center">
                                                                                    Debit
                                                                                </th>
                                                                                <th align="center" style="text-align: center">
                                                                                    Credit
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td style="width: 10%">
                                                                            <asp:Label ID="TransactionDate" runat="server"></asp:Label>
                                                                             <asp:HiddenField ID="hdnTransferEntry" runat="server" Value='<%# Eval("TRANSFER_ENTRY")%>' />
                                                                        </td>
                                                                        <td style="width: 30%; cursor: pointer" onmouseout="this.style.backgroundColor='#F9F9F9'" onmouseover="this.style.backgroundColor='#81BEF7'">
                                                                            &nbsp;&nbsp; <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("party_name")%>' ></asp:Label>
                                                                        </td>
                                                                        <td style="width: 15%">
                                                                           &nbsp;&nbsp; <%#Eval("Vch_Type")%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">
                                                                            <%# Eval("Voucher_No")%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: right;">
                                                                            <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: right;">
                                                                            <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr  class="item" >
                                                                        <td style="width: 10%">
                                                                            <asp:Label ID="TransactionDate" runat="server"></asp:Label>
                                                                             <asp:HiddenField ID="hdnTransferEntry" runat="server" Value='<%# Eval("TRANSFER_ENTRY")%>' />
                                                                        </td>
                                                                        <td style="width: 30%; cursor: pointer" onmouseout="this.style.backgroundColor='#F9F9F9'" onmouseover="this.style.backgroundColor='#81BEF7'">
                                                                            &nbsp;&nbsp; <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("party_name")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="width: 15%">
                                                                            &nbsp;&nbsp; <%#Eval("Vch_Type")%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">
                                                                            <%# Eval("Voucher_No")%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: right;">
                                                                            <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                        </td>
                                                                        <td style="width: 15%; text-align: right;">
                                                                            <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table class="datatable" width="100%" style="background-color: ThreeDFace; height: 2Px">
                                                    <tr id="Tr1" class="header" style="font-weight: bold;">
                                                        <td style="width: 11%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 30%">
                                                            Current Total
                                                        </td>
                                                        <td style="width: 15%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 14%">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lbltotDebit" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lblTotCredit" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" class="header" style="font-weight: bold;">
                                                        <td style="width: 11%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 30%">
                                                            Closing Balance
                                                        </td>
                                                        <td style="width: 15%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 14%">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lblclDebit" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right" style="text-align: right; width: 15%">
                                                            <asp:Label ID="lblClCredit" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </center>
                </td>
            </tr>
            <asp:UpdatePanel ID="updDiv" runat="server"></asp:UpdatePanel>
        </table>
    </div>
    </form>
</body>
</html>
