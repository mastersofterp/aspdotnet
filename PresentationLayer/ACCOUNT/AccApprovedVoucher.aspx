<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccApprovedVoucher.aspx.cs" Inherits="ACCOUNT_AccApprovedVoucher" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
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
                alert('Please Select Ledger.');
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
        function AskDelete() {
            debugger;
            if (confirm('Do You Want To Delete The Transaction ? ') == true) {

                document.getElementById('<%=hdnAskDelete.ClientID%>').value = "1";
                //alert(document.getElementById('ContentPlaceHolder1_hdnAskDelete').value);
                return true;
            }
            else {
                // document.getElementById('ContentPlaceHolder1_hdnAskDelete').value = 0;
                document.getElementById('<%=hdnAskDelete.ClientID%>').value = "0";
                //alert(document.getElementById('ContentPlaceHolder1_hdnAskDelete').value);
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
        //function validate() {
        //    debugger;
        //    var x = document.getElementById('<%= txtAcc.ClientID %>').value;
        //    var y = "Press space to get all ledgers.";
        //
        //    if (x == y) {
        //        alert("Please Select Ledgers ..!!!");
        //        return false;
        //    }
        //    return true;
        //}

        function validate() {
            debugger;
            var x = document.getElementById('<%= txtAcc.ClientID %>').value;

            if (x.trim() == '') {
                alert("Please Select Ledgers ..!!!");
                return false;
            }
            return true;
        }

    </script>
    
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%">
        <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server">
                                <div id="div2" runat="server"></div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">NEFT REPORT</h3>
                                </div>
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <div class="box-body">
                                   
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">NEFT Voucher</div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span><br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>From Date<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" Style="text-align: right"
                                                                    AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" />
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Upto Date<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" CssClass="form-control"
                                                                    AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" />
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
                                                                <input id="hdnAskDelete" runat="server" type="hidden" />
                                                            </div>
                                                        </div>
                                                    </div>
                                               <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Ledger Name<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                                AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                                ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtAcc" Display="None"
                                                                ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="lblCurBal" runat="server" BorderColor="White"
                                                                BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                                Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                            <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                                BorderStyle="None"
                                                                Style="background-color: Transparent;" ReadOnly="True"
                                                                Font-Size="XX-Small"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                       
                                                    
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                                            <asp:Button ID="btnGo" runat="server" Text="GO" ValidationGroup="AccMoney"
                                                                CssClass="btn btn-primary" OnClick="btnGo_Click" /><%--OnClientClick="return validate()"--%>
                                                            <asp:ValidationSummary ID="vsSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AccMoney" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="AccMoney"
                                                                OnClick="btnCancel_Click" />
                                                              
                                                          <asp:Button ID="btnPrintneft"  runat="server" Text="Print NEFT Rport" CssClass="btn btn-primary" ValidationGroup="AccMoney" OnClick="btnPrintneft_Click"
                                                                Visible="false" />
                                                            <%-- OnClick="btnPrintneft_Click"--%>
                                                            <%--<asp:Button ID="btnSubmit" runat="server" Text="" Visible="false" ValidationGroup="Submit"
                                                                CssClass="btn btn-primary" OnClick="btnSubmit_Click" />--%><%-- OnClientClick="return validate()"--%>
                                                            <br />
                                                             <br />
                                                             <br />
                                                            
                                                            <%--<div class="dropdownlist" id="drop_paymentmode" runat="server" visible="false">
                                                                  <asp:Label><b>Select Payment Mode :</b></asp:Label>
                                                   
                                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                                <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                                <asp:ListItem Value="R">RTGS</asp:ListItem>
                                                            </asp:DropDownList>
                                                            </div>--%>
                                                          
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <%--<div class="col-md-2">
                                                        </div>--%>

                                                        <div class="col-md-12" id="trGrid" runat="server">
                                                            <asp:Panel ID="pnl" ScrollBars="Vertical" runat="server" Style="text-align: left; Height: 350px; width: 100%"
                                                                BorderColor="#0066FF">
                                                                <asp:Repeater ID="RptData" runat="server"
                                                                    OnItemDataBound="RptData_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <table border="1" style="width: 100%">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr style="background-color: ActiveBorder">
                                                                            <td style="width: 4%">
                                                                                <%--<asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                    ImageUrl="~/images/edit.gif" ToolTip="Edit record" />--%>
                                                                                <%--delete voucher should be maintained--%>
                                                                                <%-- <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                    CommandName="VoucherDelete" ImageUrl="~/images/delete.gif" ToolTip="Delete record" OnClientClick="AskDelete()" />--%>
                                                                                <asp:HiddenField ID="hdnVoucherNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                                                <asp:CheckBox ID="chkVoucher" runat="server" ToolTip='<%# Eval("VOUCHER_SQN")%>' />
                                                                            </td>
                                                                            <td style="font-weight: bold; width: 12%" align="center">Voucher No.
                                                                                    <%#Eval("VOUCHER_NO")%>
                                                                            </td>
                                                                            <td style="font-weight: bold; width: 12%" align="center">Voucher Type :<asp:Label ID="lblvchtype" runat="server" Text='<%#Eval("Transaction_Type")%>'> </asp:Label></td>
                                                                          <%--  <td style="font-weight: bold; width: 9%" align="center">Voucher Print
                                                                                    <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                                                        CommandArgument='<%# Eval("VOUCHER_SQN")%>' CommandName="VoucherPrint" ImageUrl="~/images/print.gif" />
                                                                            </td>--%>
                                                                            <%-- <td style="font-weight: bold; width: 12%" align="center">Copy Voucher
                                                                                    <asp:ImageButton ID="btnCopy" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                        ImageUrl="~/IMAGES/copy2.png" Width="18px" Height="18px" ToolTip="Copy record"
                                                                                        CommandName="CopyVoucher" />
                                                                            </td>--%>
                                                                            <td style="font-weight: bold; width: 8%" align="center">Cheque No :
                                                                                      <asp:Label ID="lblChqNo" runat="server" Text='<%#Eval("CHQ_NO")%>'> </asp:Label></td>

                                                                            <%--<td id="tdPaymentMode" runat="server" style="font-weight: bold; width: 8%" align="center">Pay Mode :                                                                                      
                                                                                   <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true">
                                                                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                       <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                                                       <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                                                       <asp:ListItem Value="R">RTGS</asp:ListItem>
                                                                                   </asp:DropDownList>
                                                                            </td>--%>


                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6"><%--ScrollBars="Auto"--%>
                                                                                <asp:Panel ID="pnl1" runat="server" Style="width: 100%; height: 100%"
                                                                                    BorderColor="#0066FF">
                                                                                    <asp:ListView ID="lvGrp" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <div id="demo-grid">
                                                                                                <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                                                                    <tr class="bg-light-blue">
                                                                                                        <th>Date
                                                                                                        </th>
                                                                                                        <th>Particulars
                                                                                                        </th>
                                                                                                        <th align="right" style="text-align: right">Debit
                                                                                                        </th>
                                                                                                        <th align="right" style="text-align: right">Credit
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </table>
                                                                                            </div>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                                                <td style="width: 10%; font-weight: bold">
                                                                                                    <%# Eval("Date")%>
                                                                                                </td>
                                                                                                <td style="width: 30%; font-weight: bold">
                                                                                                    <%--<asp:HiddenField ID="hdnPartyNo" runat="server" Value='<%# Eval("PARTY_NO")%>' />--%>
                                                                                                    <%# Eval("Particulars")%>
                                                                                                    </td>
                                                                                                <td style="width: 15%; font-weight: bold; text-align: right;">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                                </td>
                                                                                                <td style="width: 15%; font-weight: bold; text-align: right;">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <AlternatingItemTemplate>
                                                                                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                                                <td style="width: 10%; font-weight: bold">
                                                                                                    <%# Eval("Date")%>
                                                                                                </td>
                                                                                                <td style="width: 30%; font-weight: bold" id="lblbankname" runat="server">
                                                                                                    <%# Eval("Particulars")%>
                                                                                                </td>
                                                                                                <td style="width: 15%; text-align: right; font-weight: bold">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                                </td>
                                                                                                <td style="width: 15%; text-align: right; font-weight: bold">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
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
                                                        </div>
                                                    </div>

                                                    <div id="Div3" class="row" runat="server" visible="false">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <span style="color: #FF0000">Op. Bal.:</span>
                                                            <asp:Label ID="lblOb" runat="server" Style="font-size: small; font-weight: 700;"></asp:Label>
                                                            &nbsp;&nbsp; <span style="color: #FF0000">Total Dr.:</span><asp:Label ID="lblDr"
                                                                Style="font-size: small; font-weight: 700;" runat="server"></asp:Label>
                                                            &nbsp;&nbsp; <span style="color: #FF0000">Total Cr.:</span><asp:Label ID="lblCr"
                                                                Style="font-size: medium; font-weight: 700;" runat="server"></asp:Label>
                                                            &nbsp;&nbsp; <span style="color: #FF0000">Close. Bal.:<asp:Label ID="lblclose" Style="font-size: medium; color: #000000; font-weight: 700;"
                                                                runat="server"></asp:Label>
                                                                <asp:Label ID="lblmode" Style="font-size: medium; color: #000000; font-weight: 700;"
                                                                    runat="server"></asp:Label>
                                                            </span>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

