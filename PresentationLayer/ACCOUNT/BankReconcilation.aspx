<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BankReconcilation.aspx.cs" Inherits="BankReconcilation" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: #ccc;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 500px;
            height: 500px;
            overflow-y: auto;
        }

        .ledgermodalBackground {
            background-color: #fff;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup {
            background-color: #fff;
            border-width: 3px;
            border-style: double;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 80%;
            height: 600px;
        }
    </style>
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 250px;
        }

        /*.modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }*/
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <%--   <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

 <%--   <script language="javascript" type="text/javascript">
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

            //            if (document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value == 'Press space to get all bank ledgers.') {
            //                alert('Please Enter Ledger.');
            //                document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
            //                return false;

        }

        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, 'Tool Tip', 'CreateSubLinks');
            return true;
        }

        function btnEdit(crl) {

            //alert("sdasdasda");
            var st = crl.id.split("_RptData_ctl");
            var i = st[1].split("_btnEdit");

            var index = i[0];
            //alert(index);

            //  var rate = document.getElementById('ctl00_ContentPlaceHolder1_RptData_ctl' + index + '_btnEdit').value;
            document.getElementById('ctl00_ContentPlaceHolder1_RptData_ctl' + index + '_txtbankdate').enable = true;
        }
    </script>--%>

    <%--   <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

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
        function validate() {
            debugger;
            var x = document.getElementById('<%= txtAcc.ClientID %>').value;
            var y = "Press space to get all bank ledgers.";

            if (x == y) {
                alert("Please Select Ledger...!!!");
                return false;
            }
            return true;
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBankRecon"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updBankRecon" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK RECONCILIATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                            </div>
                            <asp:Panel ID="pnl1" runat="server">
                                <div class="col-12">
                                    <%-- <div class="sub-heading">
                                        <h5>Bank Reconciliation Report</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-8 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>The period of From Date & UpTo Date Should not be more than one month.</span> </p>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-12 col-12"></div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Report Type</label>
                                            </div>
                                            <asp:RadioButton ID="rdbWithNarration" runat="server" Text="With Narration" GroupName="Report" />&nbsp;
                                               <asp:RadioButton ID="rdbWithoutNarration" runat="server" Text="Without Narration"
                                                   GroupName="Report" Checked="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                                <span style="color: #FF0000">*</span>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"
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
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Upto Date</label>
                                                <span style="color: #FF0000">*</span>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control"
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
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Reconciled Statement"
                                            CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReconVoucher" runat="server" OnClick="btnReconVoucher_Click"
                                            Visible="false" Text="UnReconcile Stat." CssClass="btn btn-primary" />
                                        <asp:Button ID="btnAllVoucher" runat="server" Text="All Vouchers" CssClass="btn btn-primary" OnClick="btnAllVoucher_Click"
                                            Visible="false" />
                                    </div>

                                    <div class="row mt-2">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Ledger Name</label>
                                                <span style="color: #FF0000">*</span>
                                            </div>
                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                AutoPostBack="true" OnTextChanged="txtAcc_TextChanged1"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                ServiceMethod="GetReconciliationLedger" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtAcc" Display="None"
                                                ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                ValidationGroup="AccMoney">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Curr. Bal</label>
                                            </div>
                                            <asp:TextBox ID="lblCurBal" runat="server" CssClass="form-control" BorderColor="White"
                                                BorderStyle="None" Style="background-color: Transparent; margin-left: 6px" ReadOnly="True"
                                                Font-Size="Medium"></asp:TextBox>
                                            <asp:TextBox ID="txtmd" runat="server" CssClass="form-control" BorderColor="White"
                                                BorderStyle="None" Style="background-color: Transparent; margin-left: 6px;" ReadOnly="True"
                                                Font-Size="Medium" Visible="false"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="btn btn-primary" ValidationGroup="AccMoney"
                                            OnClick="btnGo_Click" OnClientClick="return CheckFields()" CausesValidation="true" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="AccMoney"
                                            OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <div class="col-12 mt-3" id="RptHeading" runat="server" visible="false">
                                    <div class="table-table-responsive">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <td>Voucher No.
                                                    </td>
                                                    <td>Trans. Date
                                                    </td>
                                                    <td>Voucher Type
                                                    </td>
                                                    <td>Cheque No.
                                                    </td>
                                                    <td>Cheque Date
                                                    </td>
                                                    <td>Cheque Amount
                                                    </td>
                                                    <td>Bank Date
                                                    </td>
                                                </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-12 mt-3" runat="server">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lstData" runat="server" OnItemDataBound="lstData_ItemDataBound" OnItemCommand="lstData_ItemCommand">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>DATE
                                                            </th>
                                                            <th>VCH. NO.
                                                            </th>
                                                            <th>Particulars
                                                            </th>
                                                            <th>VCH. TYPE
                                                            </th>
                                                            <th>CHQ. NO.
                                                            </th>
                                                            <th>CHQ. DATE
                                                            </th>
                                                            <th>CHQ. AMT.
                                                            </th>
                                                             <th>TRANS. TYPE
                                                            </th>
                                                            <th>BANK DATE
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("TRANSACTION_DATE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("VOUCHERNO") %>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkbtnPartyName" CommandArgument='<%# Eval("VOUCHER_SQN") %>' runat="server"
                                                            Text='<%# Eval("PARTICULARS") %>' CommandName="View"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSACTIONTYPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CHECK_NO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CHEQUEDATE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CHEQUEAMOUNT") %>
                                                    </td>
                                                     <%--<td>
                                                        <%# Eval("Ledger") %>
                                                    </td>--%>
                                                    <td>
                                                        <%--<div class="input-group date">
                                                            <div class="input-group-addon" id="imgCal10">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>--%>
                                                            <asp:TextBox ID="txtbankdate" runat="server" Enabled="true" Text='<%# Eval("RECONCILEDATE")%>'
                                                                CssClass="form-control"></asp:TextBox>
                                                           <%-- <div class="input-group-addon">
                                                                <asp:ImageButton ID="btnEdit" runat="server" ToolTip="Click For Edit Bank Date" CommandArgument='<%# Eval("Voucher_No")%>'
                                                                    ImageUrl="~/Images/edit.png" CommandName="BankDateEdit" />
                                                            </div>--%>
                                                        
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="txtbankdate" PopupPosition="BottomLeft" TargetControlID="txtbankdate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtbankdate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:HiddenField ID="voucherSqn" runat="server" Value='<%#Eval("VOUCHER_SQN")%>' />
                                                        <asp:HiddenField ID="HDNCHQNO" runat="server" Value='<%#Eval("CHECK_NO")%>' />
                                                        <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VOUCHERNO")%>' />
                                                        <asp:HiddenField ID="hdnVchType" runat="server" Value='<%# Eval("TRANSACTIONTYPE")%>' />
                                                        <asp:HiddenField ID="hdnTranDate" runat="server" Value='<%# Eval("TRANSACTION_DATE")%>' />
                                                            <%--</div>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="form-group col-md-12" runat="server" id="trGrid" align="left" style="display: none">
                                    <asp:UpdatePanel ID="UPDLedger" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnl" runat="server">

                                                <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table border="1" class="table-bordered" width="100%" style="height: 90%">
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="font-weight: bold; width: 13%" align="Center">
                                                                <asp:Label ID="lblHeadVoucherNo" runat="server" Text='<%#Eval("STR_VOUCHER_NO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblHeadTransDate" runat="server" Text='<%#Eval("Transaction_Date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblHeadVoucherType" runat="server" Text='<%#Eval("Transaction_Type")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblHeadChequeNo" runat="server" Text='<%#Eval("CHQ_NO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblHeadChequeDate" runat="server" Text='<%#Eval("CHQ_DATE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblChequeAmount" runat="server" Text='<%#Eval("CHEQ_AMOUNT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="imgCal10" runat="server" class="fa fa-calendar text-blue"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbankdate" runat="server" Text='<%# Eval("RECON_DATE")%>'
                                                                        Style="text-align: right;" CssClass="form-control"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                        Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ToolTip="Click For Edit Bank Date" CommandArgument='<%# Eval("Voucher_No")%>'
                                                                    ImageUrl="~/Images/edit.png" CommandName="BankDateEdit" />
                                                                <asp:HiddenField ID="voucherSqn" runat="server" Value='<%#Eval("voucher_sqn")%>' />
                                                                <asp:HiddenField ID="HDNCHQNO" runat="server" Value='<%#Eval("CHQ_NO")%>' />

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="8">
                                                                <asp:Panel ID="pnl1" runat="server">
                                                                    <asp:ListView ID="lvGrp" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div id="demo-grid" class="vista-grid">
                                                                                <%--<div id="Div1" class="titlebar" runat="server">
                                                                    Ledger Vouchers
                                                                </div>--%>
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>
                                                                                                <%--Date--%>
                                                                                            </th>
                                                                                            <th>Particulars
                                                                                            </th>
                                                                                            <%--  <th>
                                                                            Bank Date
                                                                        </th>--%>
                                                                                            <th>Debit
                                                                                            </th>
                                                                                            <th align="right" style="text-align: right">Credit
                                                                                            </th>
                                                                                            <%--<th></th>
                                                                                                    <th></th>--%>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>

                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%-- <%# Eval("Date")%>--%>
                                                                                    <asp:HiddenField ID="hdnTranDate" runat="server" Value='<%# Eval("Date")%>' />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("Particulars")%>
                                                                                </td>
                                                                                <%--<td style="width:15%">
                                                                <asp:TextBox ID="txtbankdate" Width="80px" runat="server" text='<%# Eval("ReconcileDate")%>'  Style="text-align: right"></asp:TextBox>
                                                                    <asp:Image ID="imgCal10" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                        Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                                                                </td>--%>
                                                                                <td>
                                                                                    <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                    <asp:HiddenField ID="hdnparty" runat="server" Value='<%# Eval("PartyNo")%>' />
                                                                                    <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VchNo")%>' />
                                                                                    <asp:HiddenField ID="hdnVchType" runat="server" Value='<%# Eval("VchType")%>' />
                                                                                </td>
                                                                                <%--<td style="width: 1%;">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 1%;">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 1%;">
                                                                                                
                                                                                            </td>--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <AlternatingItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%--<%# Eval("Date")%>--%>
                                                                                    <asp:HiddenField ID="hdnTranDate" runat="server" Value='<%# Eval("Date")%>' />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("Particulars")%>
                                                                                </td>
                                                                                <%--<td style="width:15%">
                                                                   <asp:TextBox ID="txtbankdate" Width="80px" runat="server"  text='<%# Eval("ReconcileDate")%>' Style="text-align: right"></asp:TextBox>
                                                                    <asp:Image ID="imgCal10" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                        Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtbankdate">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                </td>--%>
                                                                                <td>
                                                                                    <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                    <asp:HiddenField ID="hdnparty" runat="server" Value='<%# Eval("PartyNo")%>' />
                                                                                    <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VchNo")%>' />
                                                                                    <asp:HiddenField ID="hdnVchType" runat="server" Value='<%# Eval("VchType")%>' />
                                                                                </td>
                                                                                <%--<td style="width: 1%;">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 1%;">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 1%;">
                                                                                                
                                                                                            </td>--%>
                                                                            </tr>
                                                                        </AlternatingItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <div class="text-center" id="trButton" runat="server" visible="false">
                                        <asp:Button ID="btnReconcile" runat="server" Text="Reconcile" OnClick="btnReconcile_Click"
                                            CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                                <div class="form-group col-md-12" runat="server" id="trBalances" visible="false" style="display: none">
                                    <div class="col-md-2">
                                        <label><span style="color: #FF0000">Op. Bal.:</span></label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblOb" runat="server" Style="font-size: small; font-weight: 700;"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label><span style="color: #FF0000">Total Dr.:</span></label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblDr" Style="font-size: small; font-weight: 700;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label><span style="color: #FF0000">Total Cr.:</span></label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblCr" Style="font-size: medium; font-weight: 700;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label><span style="color: #FF0000">Close. Bal.:</span></label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblclose" Style="font-size: medium; color: #000000; font-weight: 700;" runat="server"></asp:Label>
                                        <asp:Label ID="lblmode" Style="font-size: medium; color: #000000; font-weight: 700;" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>

                            </asp:Panel>

                            <div>
                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                     CancelControlID="btnClose" PopupControlID="Panel2" TargetControlID="btnForPopUpModel2" 
                                    Enabled="True">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" meta:resourcekey="pnlResource1">
                                    <%--<div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Transaction</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="Validation" CssClass="btn btn-warning" meta:resourcekey="btnBackResource1" />
                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvGrp1" runat="server">
                                            <LayoutTemplate>

                                                <div class="sub-heading">
                                                    <h5>Transaction</h5>
                                                </div>
                                                <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Particulars
                                                            </th>
                                                            <th>Debit
                                                            </th>
                                                            <th>Credit
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("LEDGER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEBIT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREDIT") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%--<AlternatingItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("LEDGER")%>
                                    </td>
                                    <td>
                                        <%# Eval("DEBIT")%>
                                    </td>
                                    <td>
                                        <%# Eval("CREDIT")%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>--%>
                                        </asp:ListView>

                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
