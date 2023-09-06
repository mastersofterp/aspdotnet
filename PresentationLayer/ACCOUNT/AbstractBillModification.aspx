<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AbstractBillModification.aspx.cs" Inherits="ACCOUNT_AbstractBillModification"
    Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left:250px;
        }
    </style>
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
        </script>
    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    ABSTRACT BILL MODIFICATIONS
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
                <td>
                    <fieldset class="vista-grid" style="width: 75%; height:515px">
                        <legend class="titlebar">Abstract Bill Modification</legend>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
            <tr>
                                <td style="padding: 10px; width: 13%; text-align: left">
                                    From Date
                                </td>
                                <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                <td>
                                    <asp:TextBox ID="txtFrmDate" runat="server" Width="25%" Style="text-align: right" />
                                    &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                    </ajaxToolKit:MaskedEditExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp; Upto Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" Width="25%"/>
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
                <td style="padding: 10px; width: 13%; text-align: left">
                    Select Voucher
                </td>
                <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                <%-- <td style="width: 35%;">
                    <cc2:AutoSuggestBox ID="txtAcc" Width="100%" ToolTip="Please Enter Account Name"
                        DataType="All" ResourcesDir="AutoSuggestBox" runat="server" Font-Bold="True"></cc2:AutoSuggestBox>
                   </td>
               <td style="width:1%">
                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtAcc"
                        WatermarkText="Press space to get all ledgers." WatermarkCssClass="watermarked" />
               </td>--%>
                <td style="width: 35%;">
                    <asp:DropDownList ID="ddlVoucherType" runat="server" AppendDataBoundItems="True"
                        Width="69%">
                        <asp:ListItem Value="0" Text="--Please Select--"></asp:ListItem>
                        <asp:ListItem Value="PV" Text="PAYMENT VOUCHER"></asp:ListItem>
                        <asp:ListItem Value="APV" Text="ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                        <asp:ListItem Value="AAPV" Text="ADVANCE ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                        <asp:ListItem Value="AV" Text="ADVANCE VOUCHER"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDdlVoucher" runat="server" ControlToValidate="ddlVoucherType"
                        Display="None" ErrorMessage="Please Select Voucher Type" SetFocusOnError="True"
                        ValidationGroup="AccMoney" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 10%; text-align: center">
                    <asp:Button ID="btnGo" runat="server" Text="GO" Width="100%" ValidationGroup="AccMoney"
                        Height="29px" OnClick="btnGo_Click" />
                </td>
                <td style="width: 11%;">
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="93%" 
                        Height="29px" onclick="btnCancel_Click" />
                    
                </td>
                <td style="width: 26%;">
                    <asp:ValidationSummary ID="VS" runat="server" DisplayMode="List" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="AccMoney" />
                </td>
            </tr>
            <tr id="trGrid" runat="server" align="left">
                <td style="padding: 10px; text-align: right" colspan="6">
                    <asp:UpdatePanel ID="UPDLedger" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl" ScrollBars="Vertical" runat="server" Style="width: 90%; height: 350Px;
                                text-align: left" BorderColor="#0066FF">
                                <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand" >
                                    <HeaderTemplate>
                                        <table border="1" width="100%" style="height: 90%">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: ThreeDShadow; height: 2Px">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ABS_BILL_NO")%>'
                                                    CommandName="VoucherView" ImageUrl="~/images/edit.gif" ToolTip="Edit record" />
                                            </td>
                                            <td style="font-weight: bold" align="Center">
                                                Bill No. :-
                                                <%#Eval("ABS_BILL_NO")%>
                                            </td>
                                            <td style="font-weight: bold" align="Center">
                                                Head of Account :-
                                                <%#Eval("party_name")%>
                                            </td>
                                            <td style="font-weight: bold" align="Center">
                                                Gross Amount :-
                                                <%#Eval("GROSS_AMOUNT")%>
                                            </td>
                                            <td style="font-weight: bold" align="Center">
                                                Total Payble :<asp:Label ID="lblvchtype" runat="server" Text='<%#Eval("Tot_Payable")%>'> </asp:Label></td>
                                            <td rowspan="2" style="font-weight: bold;background-color:White;" valign="top" align="Center">
                                            <table>
                                            <tr style="border-bottom-style:solid">
                                            <td>
                                            Bill Print
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="text-align:center">
                                            <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                    CommandArgument='<%# Eval("ABS_BILL_NO")%>' CommandName="VoucherPrint" ImageUrl="~/images/print.gif" />
                                            </td>
                                            </tr>
                                            </table>
                                                <%--<hr /><br />
                                                <br />--%>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <%--ScrollBars="Auto"--%>
                                                <asp:Panel ID="pnl1" runat="server" Style="width: 100%; height: 100%" BorderColor="#0066FF">
                                                    <asp:ListView ID="lvGrp" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                                    <tr class="header">
                                                                        <th>
                                                                            Date
                                                                        </th>
                                                                        <th>
                                                                            Deduction Head
                                                                        </th>
                                                                        <th>
                                                                            Amount
                                                                        </th>
                                                                        <th>
                                                                            Transsaction Type
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td style="width: 10%">
                                                                    <%# Eval("TRansaction_Date")%>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <%# Eval("DeductHead")%>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <%# string.Format("{0:#,0.00}", Eval("AMOUNT"))%>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <%# Eval("TRAN_TYPE")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td style="width: 10%">
                                                                    <%# Eval("TRansaction_Date")%>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <%# Eval("DeductHead")%>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <%# string.Format("{0:#,0.00}", Eval("AMOUNT"))%>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <%# Eval("TRAN_TYPE")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            
                            
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </td>
            </tr>
            </table>
            </fieldset>
            </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
