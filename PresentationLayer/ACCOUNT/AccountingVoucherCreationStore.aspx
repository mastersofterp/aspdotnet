<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccountingVoucherCreationStore.aspx.cs" Inherits="ACCOUNT_AccountingVoucherCreationStore" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: black;
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
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup {
            background-color: #e5ecf9;
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
            text-align: center;
        }

        #scr {
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, CAPTION, 'Create Sub Links');
            return true;
        }

    </script>

    <link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //function Confirm() {
        //var TranVal = document.getElementById('<%= ddlTranType.ClientID%>');
        //var BudgetHead = document.getElementById('<%= ddlBudgetHead.ClientID%>');
        //if (TranVal == "P" || TranVal == "R") {
        // if (document.getElementById('<%= ddlBudgetHead.ClientID%>').visible == true && ($('#<%= ddlBudgetHead.ClientID%> option:selected').val() != "0")) {
        // if (document.getElementById('<%= lblBudgetBal.ClientID%>').value <= 0) {
        //var confirm_value = document.createElement("INPUT");
        //confirm_value.type = "hidden";
        //confirm_value.name = "confirm_value";
        //if (confirm("Selected Budget Head Has Less than 0.00 or 0.00 Amount left, Do you want to save data?") == true) {
        //    confirm_value.value = "Yes";
        //    return true;
        //} else {
        //    confirm_value.value = "No";
        //document.getElementById('<%= lblconfirm.ClientID%>').value = "NO";
        //        return false;
        //    }
        //    document.forms[0].appendChild(confirm_value);
        //}
        //}
        //}
        //}
    </script>

    <script language="javascript" type="text/javascript">
        function ShowLedger() {
            var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers&pageno=332';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbar=yes' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function ShowGroup() {
            var popUrl = 'maingroup.aspx?obj=' + 'AccountingVouchers&pageno=332';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=yes,,scrollbar=yes' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function ProjectSubHeadValidation() {

            var BudAmt = document.getElementById('ctl00_ContentPlaceHolder1_lblBudgetBal').innerHTML;
            var Amount = document.getElementById('<%= txtTranAmt.ClientID%>').value;
            if ((parseFloat(BudAmt) - parseFloat(Amount)) <= 0) {
                if ($('#<%= ddlBudgetHead.ClientID%> option:selected').val() != "0") {
                    if ((parseFloat(BudAmt) - parseFloat(Amount)) <= 0) {
                        //alert(actualPrice);
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        if (confirm("Selected Budget Head will have RS. " + (parseFloat(BudAmt) - parseFloat(Amount)) + " Amount left, Do you want to save data?") == true) {
                            confirm_value.value = "Yes";
                            // return true;
                        } else {
                            confirm_value.value = "No";
                            document.getElementById('<%= lblconfirm.ClientID%>').innerHTML = "NO";
                            return false;
                        }
                    }
                }
            }

            if ($('#<%= ddlSponsor.ClientID%> option:selected').val() != "0") {

                if ($('#<%= ddlProjSubHead.ClientID%> option:selected').val() == "0") {
                    alert('Please Select Project Sub Head.');

                    return false;
                }
            }
        }

        function ProjectValidation() {


            if ($('#<%= ddlTranType.ClientID%> option:selected').val() == "P") {
                if (parseFloat($('#<%= lblRemainAmt.ClientID%>').text()) == 0.00) {
                    alert("There is no balance for project. You can not proceed.");
                    return false;
                }

                if (parseFloat($('#<%= lblRemainAmt.ClientID%>').text()) < parseFloat($('#<%= lblTotal.ClientID%>').text())) {
                    alert("Total Amount is exceed from remaining amount.");
                    return false;
                }
            }

            //var Griddata = document.getElementById('ctl00_ContentPlaceHolder1_GridData').
            //var grd = $('#<%= GridData.ClientID %> tr').size();
            // alert($('#<%= GridData.ClientID %> tr').size());
            //var actualPrice = document.getElementById('ctl00_ContentPlaceHolder1_lblBudgetBal').innerHTML;
            //if ($('#<%= ddlBudgetHead.ClientID%> option:selected').val() != "0") {
            //if (parseFloat(actualPrice) <= 0) {
            //alert(actualPrice);
            // var confirm_value = document.createElement("INPUT");
            // confirm_value.type = "hidden";
            // confirm_value.name = "confirm_value";
            //if (confirm("Selected Budget Head Has RS. " + actualPrice + " Amount left, Do you want to save data?") == true) {
            //     confirm_value.value = "Yes";
            // return true;
            // } else {
            //    confirm_value.value = "No";
            //    document.getElementById('<%= lblconfirm.ClientID%>').innerHTML = "NO";
            //    return false;
            // }
            //}
            // }
            //document.forms[0].appendChild(confirm_value);


            //return Confirm();
            //alert('hii');
            return AskSave();

        }

        function ShowVoucherWindow(wattodo, rowcount) {
            if (rowcount == 0) {
                alert('No Record Present');
                return false;
            }

            if (wattodo == 'do') {

                // alert('hi');
                var vchno = document.getElementById('ctl00_ContentPlaceHolder1_hdnvch').value;
                var popUrl = 'ShowVoucherImage.aspx?id=' + 'AccountingVouchers' + ',' + 'no' + ',' + vchno;
                var name = 'popUp';
                var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                 'status=no,toolbar=no,titlebar=no,' +
                                 'left=50,top=35,width=900px,height=650px';
                var openWindow = window.open(popUrl, name, appearence);
                openWindow.focus();
            }
            else {
                var vchno = document.getElementById('ctl00_ContentPlaceHolder1_hdnvch').value;
                // alert('hi');
                var popUrl = 'ShowVoucherImage.aspx?id=' + 'AccountingVouchers' + ',' + vchno;
                var name = 'popUp';
                var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                 'status=no,toolbar=no,titlebar=no,' +
                                 'left=50,top=35,width=900px,height=650px';
                var openWindow = window.open(popUrl, name, appearence);

                openWindow.focus();
            }
            return false;
        }

        function AskSave() {
            if (confirm('Do You Want To Save The Transaction ? ') == true) {

                //alert('Yes');
                //document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                //alert(ContentPlaceHolder1_hdnAskSave);
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = "1";
                //alert(document.getElementById('ContentPlaceHolder1_hdnAskSave').value);

                return true;
            }
            else {
                //alert('NO');
                //document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                //document.getElementById('ctl00_ContentPlaceHolder1_btnSave').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = "0";
                //alert(document.getElementById('hdnAskSave').value);
                return false;
            }
        }

        function submitPopup() {

            __doPostBack('btnSubmit', 'abcd');
            return false;
        }


        function AskCheque() {
            if (confirm('Do You Want To Print Cheque ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnBack').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnBack').value = 0;
                return true;
            }
        }

        function ShowChequePrinting(VarDate, VarVchNo, VarParty, VarAmt, VarOparty, VarChequeNo) {
            var popUrl = 'ChequePrinting.aspx?obj=' + 'ChequePrinting,' + VarDate + ',' + VarVchNo + ',' + VarParty + ',' + VarAmt + ',' + VarOparty + ',' + VarChequeNo;
            var name = 'popUp';
            var appearence = 'center:yes; dialogWidth:650px; dialogHeight:350px; edge:raised; ' +
                             'help:no; resizable:no; scroll:no; status:no;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function ShowChequePrintingTran(VarDate, VarTranNo, VarParty, VarAmt, VarOparty, VarChqNo) {
            var popUrl = 'ChequePrintingTransaction.aspx?obj=' + 'ChequePrinting,' + VarDate + ',' + VarTranNo + ',' + VarParty + ',' + VarAmt + ',' + VarOparty + ',' + VarChqNo;
            var name = 'popUp';
            var appearence = 'center:yes; dialogWidth:850px; dialogHeight:450px; edge:raised; ' +
                             'help:no; resizable:no; scroll:no; status:no;dialogTop: 150px;  dialogLeft: 100px;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
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

        function CheckDuplicate(chqNo, CompCode) {
            var ChequeNo = chqNo.value;
            $.ajax({
                type: "POST",
                url: "AccountingVouchers.aspx/CheckDuplicateChequeNo",
                data: '{ChequeNo: "' + ChequeNo + '",compcode:"' + CompCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var IsDuplicate = response.d;
                    if (IsDuplicate == 'Available') {
                        alert('Cheque No :-' + ChequeNo + ' already used');
                        document.getElementById('<%=txtChqNo2.ClientID %>').value = '';
                        document.getElementById('<%=txtChqNo2.ClientID %>').focus();
                        return false;
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function CheckAmount(val) {

            var Amount = parseFloat(document.getElementById("<%=txtTranAmt.ClientID%>").value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }
        function calcgstamount() {
            var amount = parseFloat(document.getElementById("<%=txtCGSTAMOUNT1.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtCGSTPER.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtCGSTPER.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtCGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            document.getElementById('<%=txtTDSLedger.ClientID%>').focus();
        }
        function calsgstamount() {
            var amount = parseFloat(document.getElementById("<%=txtSGSTAMOUNT.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtSGTSPer.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtSGTSPer.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtSGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            document.getElementById("<%=txtCGST.ClientID%>").focus();
        }
        function calIgstamount() {
            var amount = parseFloat(document.getElementById("<%=txtIGSTAMOUNT.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtIGSTPER.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtIGSTPER.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtIGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            //document.getElementById('<%=btnAdd.ClientID%>').focus();
        }

        function calTdsamount() {
            var amount = parseFloat(document.getElementById("<%=txtTamount.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtTDSPer.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtTDSPer.ClientID%>").value = '';
            }
            if (per == "" || per == 0) {
                alert('Please Enter Percentage Value should be grater than 0');
                document.getElementById("<%=txtTDSPer.ClientID%>").value = '';
                return;

            } var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtTDSAmount.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            //document.getElementById('<%=txtTranAmt.ClientID%>').focus();
        }
    </script>

    <script language="javascript" type="text/javascript">
        function copyamount1() {
            var GST = document.getElementById('<%=chkGST.ClientID%>').checked;
            var IGST = document.getElementById('<%=chkIGST.ClientID%>').checked;
            var TDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;

            var Amount = parseFloat(document.getElementById('<%=txtTranAmt.ClientID%>').value);


            if (GST) {
                document.getElementById('<%=txtCGSTAMOUNT1.ClientID%>').value = document.getElementById('<%=txtSGSTAMOUNT.ClientID%>').value = Amount;
                calsgstamount();
                calcgstamount();
            }
            if (IGST) {

                document.getElementById('<%=txtIGSTAMOUNT.ClientID%>').value = Amount;
                calIgstamount();
            }
            if (TDS) {

                document.getElementById('<%=txtTamount.ClientID%>').value = Amount;
                calTdsamount();
            }
            document.getElementById('<%=txtTranAmt.ClientID%>').focus();
        }
        //Added by vijay andoju for checkingLedgers
        function CheckLedger() {

        }
    </script>


    <script language="javascript" type="text/javascript">
        function copyamount() {
            var BalanceAmount = document.getElementById('<%=lblBalAmount.ClientID%>').value;
            var Amount = document.getElementById('<%=txtTranAmt.ClientID%>').value;
            if (Number(Amount) > Number(BalanceAmount)) {
                alert('Amount Should be lessthan or equals to Balance Amount');
                document.getElementById('<%=txtTranAmt.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=lblBalAmount.ClientID%>').value = Number(BalanceAmount) - Number(Amount).toFixed(2);
            }

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
                                    <h3 class="box-title">PAYMENT VOUCHER CREATION FOR STORE</h3>
                                </div>
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <div class="box-body">
                                    <asp:Panel ID="pnlVPList" runat="server">
                                        <asp:ListView ID="lvVPEntry" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Vendor Payment Approved List</h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>VP Number</th>
                                                                <th>VP Date</th>
                                                                <th>Vendor Name</th>
                                                                <th>Payment Amount</th>
                                                                <th>Payment Type</th>
                                                               <%-- <th>Status</th>--%>
                                                                <th>Action</th>
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
                                                        <%# Eval("VP_NUMBER")%>                                                       
                                                    </td>
                                                    <td>
                                                        <%# Eval("VPDATE","{0:dd-MM-yyyy}")%>                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("PNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PAYMENT_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PAYMENT_TYPE")%>
                                                    </td>
                                                   <%-- <td>
                                                        <%# Eval("VP_STATUS")%>
                                                    </td>--%>
                                                    <td>
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Select"
                                                            CommandArgument='<%#Eval("VPID")%>' OnClick="btnSelect_Click"/>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlStoreVoucher" runat="server" Visible="false">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Create Account Vouchers For Store</div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span><br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:LinkButton ID="lnkGroup" runat="server" Font-Underline="True" ForeColor="Blue"
                                                                meta:resourcekey="lnkGroupResource1" Text="New Group" Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkLedger" runat="server" Font-Underline="True" ForeColor="Blue"
                                                                meta:resourcekey="lnkLedgerResource1" Text="New Ledger Head" Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkConfig0" runat="server" Font-Underline="True" ForeColor="Blue"
                                                                OnClick="lnkConfig_Click" meta:resourcekey="lnkConfig0Resource1" Text="Account Configuration"
                                                                Visible="false"></asp:LinkButton>
                                                            <asp:HiddenField ID="hdnvch" runat="server" />
                                                        </div>
                                                    </div>


                                                    <div class="row" id="Row1" runat="server">
                                                        <div class="col-md-3">
                                                            <%--<label style="color: red">Against Account Entry</label>--%>
                                                        </div>
                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>VP Number <span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-2">                                                          
                                                             <asp:Label ID="lblVPNumber" runat="server" Style="font-weight: bold" Text=""></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>VP Amount : </label>

                                                            <asp:Label ID="lblVPAmount" runat="server" Style="font-weight: bold" Text=""></asp:Label>
                                                        </div>
                                                       <div class="col-md-1">
                                                            <label>Bal. Amt: </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="lblBalAmount" runat="server" Enabled="false" Width="50%" Style="font-weight: bold" Text=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Transaction Mode<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblTransType" runat="server" Text="Payment" CssClass="form-control"></asp:Label>
                                                            <asp:DropDownList ID="ddlTranType" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Visible="false"
                                                                OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged" CssClass="form-control" TabIndex="1"
                                                                meta:resourcekey="ddlTranTypeResource1">
                                                                <asp:ListItem Value="P" Text="Payment" Selected="True"></asp:ListItem>
                                                                <%--  <asp:ListItem Value="R" meta:resourcekey="ListItemResource2" Text="Receipt"></asp:ListItem>
                                                                <asp:ListItem Value="C" meta:resourcekey="ListItemResource3" Text="Contra"></asp:ListItem>
                                                                <asp:ListItem Value="J" meta:resourcekey="ListItemResource4" Text="Journal"></asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2" style="width: 10%">
                                                            <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="form-control" Style="text-align: right; text-transform: uppercase;"
                                                                ToolTip="Please Enter Voucher No." meta:resourcekey="txtVoucherNoResource1"
                                                                OnTextChanged="txtVoucherNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" ControlToValidate="txtVoucherNo"
                                                                Display="None" ErrorMessage="Please Enter Voucher No" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney1"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-sm-1" style="text-align: right">
                                                            <b>
                                                                <asp:Label ID="lblDate" CssClass="control-label" runat="server" Text="Date:"></asp:Label></b>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <span id="lblSpan" runat="server" style="text-align: left; display: none"></span>
                                                                <asp:TextBox ID="txtDate" runat="server" Width="50%" Style="text-align: right" CssClass="form-control"
                                                                    AutoPostBack="true" OnTextChanged="txtDate_TextChanged" meta:resourcekey="txtDateResource1" />
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgCal" TargetControlID="txtDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtDate" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" id="Row2" runat="server">
                                                        <div class="col-md-2">
                                                            <label>Bank Ledger<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <input id="hdnAgainstPartyId" runat="server" type="hidden" />
                                                            <asp:HiddenField ID="hdnOpartyManual" runat="server" Value="0" />
                                                            <%--<asp:TextBox ID="txtAgainstAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtAgainstAcc" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                                OnTextChanged="txtAgainstAcc_TextChanged1"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtAgainstAcc"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                ControlToValidate="txtAgainstAcc" Display="None" ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblCurbal1" runat="server" BorderColor="White" BorderStyle="None"
                                                                Font-Size="Small" Style="background-color: Transparent; text-align: right; padding-top: 7px"
                                                                TabIndex="-1" ForeColor="Black" Text="0.00" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblCur1" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblCrDr1" runat="server" BorderColor="White" BorderStyle="None" Font-Size="Small"
                                                                ReadOnly="True" Style="background-color: Transparent; text-align: left;" TabIndex="-3"
                                                                Width="20%" Font-Bold="true"></asp:Label>
                                                            <input id="hdnCurBalAg" runat="server" type="hidden" />
                                                            <input id="hdnCurBal" runat="server" type="hidden" />
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div id="Div3" class="row" runat="server" visible="false">
                                                        <div class="col-md-2">
                                                            <label>Bill No.<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                                Width="90%" OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                            </asp:DropDownList>
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

                                                    <div id="trSponsor" runat="server" class="row">
                                                        <br />
                                                        <div class="col-md-2">
                                                            <label>Sponsor Project<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlSponsor" runat="server" AppendDataBoundItems="true" Width="90%"
                                                                OnSelectedIndexChanged="ddlSponsor_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TextBox1" runat="server" BorderColor="White"
                                                                BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                                Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox2" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                                BorderStyle="None"
                                                                Style="background-color: Transparent;" ReadOnly="True"
                                                                Font-Size="XX-Small"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <%--<label style="color: red">Account Entry</label>--%>
                                                        </div>
                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Vendor Ledger</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:LinkButton ID="lnkupload" runat="server" ToolTip="click to upload vouchers"
                                                                Font-Underline="True" ForeColor="Blue" Visible="false">Upload Vouchers</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkView" runat="server" ToolTip="Click To View Voucher"
                                                                Font-Underline="True" ForeColor="Blue" Visible="false">View Voucher</asp:LinkButton>
                                                            <asp:HiddenField ID="hdnPartyManual" runat="server" Value="0" />
                                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                                AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                ControlToValidate="txtAcc" Display="None"
                                                                ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblCurBal2" runat="server" TabIndex="-2" Font-Bold="true" Style="background-color: Transparent; text-align: left;"></asp:Label>
                                                            <asp:Label ID="lblCrDr2" runat="server" BorderColor="White" BorderStyle="None" Font-Size="XX-Small"
                                                                Height="16px" ReadOnly="True" Style="background-color: Transparent; text-align: left;"
                                                                TabIndex="-6" Width="20%" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblCur2" runat="server" BorderColor="White" BorderStyle="None" Font-Size="XX-Small"
                                                                ReadOnly="True" Style="background-color: Transparent" TabIndex="-2" Width="75px"
                                                                ForeColor="White" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Amount</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtTranAmt" runat="server" Style="text-align: right" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                                                autocomplete="off" AutoPostBack="false" MaxLength="13" onkeyup="copyamount();"></asp:TextBox>
                                                            <%-- <asp:TextBox ID="txtTranAmt" runat="server" Style="text-align: right" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                                                autocomplete="off" AutoPostBack="false" MaxLength="13" onkeyup="copyamount();" OnTextChanged="txtTranAmt_TextChanged"></asp:TextBox>--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtTranAmt"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                        </div>
                                                        <div class="col-md-2" style="width: 11%">
                                                            <asp:DropDownList ID="ddlcrdr" runat="server" CssClass="form-control"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlcrdr_SelectedIndexChanged">
                                                                <asp:ListItem>Dr</asp:ListItem>
                                                                <asp:ListItem>Cr</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="Div4" class="col-md-4" runat="server" visible="false">
                                                            <asp:TextBox ID="txtChqNo1" runat="server" ToolTip="Please Enter Account Name" Width="70%"
                                                                Visible="False"></asp:TextBox>
                                                            <asp:TextBox ID="txtChequeDt1" runat="server" ToolTip="Please Enter Account Name"
                                                                Width="70%" Visible="False"></asp:TextBox>&nbsp;<asp:Image ID="Image1" runat="server"
                                                                    ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtChequeDt1">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                OnInvalidCssClass="errordate" TargetControlID="txtChequeDt1" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                        <div id="divTax" runat="server" visible="false">
                                                            <%-----------------------Added by vijay andoju on 25-08-2020 for GST and IGST APPLICABLE--------------%>
                                                            <asp:CheckBox ID="chkGST" runat="server" Text="&nbsp;Is GST Applicable" AutoPostBack="true"
                                                                OnCheckedChanged="chkGst_CheckedChanged" />
                                                            <asp:CheckBox ID="chkIGST" runat="server" Text="&nbsp;Is IGST Applicable" AutoPostBack="true"
                                                                OnCheckedChanged="chkIGST_CheckedChanged" />


                                                            <%--------------------------------------------------------------------------------------%>

                                                            <asp:CheckBox ID="chkTDSApplicable" runat="server" Text="&nbsp;Is TDS Applicable" AutoPostBack="true"
                                                                OnCheckedChanged="chkTDSApplicable_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <br />


                                                    <%--Added by vijay andoju on 25-08-2020 for GST and IGST--%>
                                                    <div id="divgst" runat="server" visible="false" class="row">
                                                        <br />


                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>SGST Account :</label>
                                                            <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" ToolTip="Please EnterSGST Ledger" OnTextChanged="txtSGST_TextChanged" onblur="CheckLedger()"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtSGST"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                                                ControlToValidate="txtSGST" Display="None"
                                                                ErrorMessage="Please Enter SGST Account" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>SGST On Amount :</label>
                                                            <asp:TextBox ID="txtSGSTAMOUNT" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onkeyup="CheckAmount
                                                                (this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                ControlToValidate="txtSGSTAMOUNT" Display="None"
                                                                ErrorMessage="Please Enter SGST On Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSGSTAMOUNT" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div id="Div6" class="col-md-2" runat="server" visible="false">
                                                            <label>Section :</label>
                                                            <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>Per (%) :</label>
                                                            <asp:TextBox ID="txtSGTSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2" AutoPostBack="false"
                                                                ToolTip="Please Enter SGST Percentage" onblur="calsgstamount();"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtSGTSPer"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                                                ControlToValidate="txtSGTSPer" Display="None"
                                                                ErrorMessage="Please Enter SGST Percentage" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>SGST Amount :</label>
                                                            <asp:TextBox ID="txtSGSTAMT" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                                                ToolTip="Can be Edited"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtSGSTAMT"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                                                ControlToValidate="txtSGSTAMT" Display="None"
                                                                ErrorMessage="Please Enter SGST Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div id="divcgst" runat="server" visible="false" class="row">
                                                        <br />
                                                        <div class="col-md-2">
                                                            <label>&nbsp;</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>CGST Account :</label>
                                                            <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Ledger" OnTextChanged="txtCGST_TextChanged1"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtCGST"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                                ControlToValidate="txtCGST" Display="None"
                                                                ErrorMessage="Please Enter CGST Account" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>CGST On Amount :</label>
                                                            <asp:TextBox ID="txtCGSTAMOUNT1" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                ControlToValidate="txtCGSTAMOUNT1" Display="None"
                                                                ErrorMessage="Please Enter CGST On Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCGSTAMOUNT1" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div id="Div5" class="col-md-2" runat="server" visible="false">
                                                            <label>Section :</label>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>Per (%) :</label>
                                                            <asp:TextBox ID="txtCGSTPER" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                                                ToolTip="Please Enter CGST Percentage" AutoPostBack="false" onblur="calcgstamount();"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCGSTPER"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                                ControlToValidate="txtCGSTPER" Display="None"
                                                                ErrorMessage="Please Enter CGST Percentage" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>CGST Amount :</label>
                                                            <asp:TextBox ID="txtCGSTAMT" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                                                ToolTip="Can be Edited"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCGSTAMT"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                                                ControlToValidate="txtCGSTAMT" Display="None"
                                                                ErrorMessage="Please Enter CGST Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div id="divIgst" runat="server" visible="false" class="row">
                                                        <br />

                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>IGST Account :</label>
                                                            <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" ToolTip="Please Enter IGST Ledger" OnTextChanged="txtIGST_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtIGST"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                ControlToValidate="txtIGST" Display="None"
                                                                ErrorMessage="Please Enter IGST Account" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>ICGST On Amount :</label>
                                                            <asp:TextBox ID="txtIGSTAMOUNT" runat="server" CssClass="form-control" ToolTip="Please Enter IGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                ControlToValidate="txtIGSTAMOUNT" Display="None"
                                                                ErrorMessage="Please Enter IGST Account" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>

                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtIGSTAMOUNT" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div id="Div7" class="col-md-2" runat="server" visible="false">
                                                            <label>Section :</label>
                                                            <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>Per (%) :</label>
                                                            <asp:TextBox ID="txtIGSTPER" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                                                ToolTip="Please Enter IGST Percentage" AutoPostBack="false" onblur="calIgstamount();"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                ControlToValidate="txtIGSTPER" Display="None"
                                                                ErrorMessage="Please Enter IGST Percentage" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtIGSTPER"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>IGST Amount :</label>
                                                            <asp:TextBox ID="txtIGSTAMT" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                                                ToolTip="Can be Edited"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                ControlToValidate="txtIGSTAMT" Display="None"
                                                                ErrorMessage="Please Enter IGST Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtIGSTAMT"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                                        </div>
                                                    </div>
                                                    <%---------------------------------------------------------------------------------------------------%>


                                                    <div id="dvTDS" runat="server" visible="false" class="row">
                                                        <br />

                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>TDS Account :</label>
                                                            <asp:TextBox ID="txtTDSLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger" OnTextChanged="txtTDSLedger_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtTDSLedger"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                ControlToValidate="txtTDSLedger" Display="None"
                                                                ErrorMessage="Please Select TDS Account" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>TDS On Amount :</label>
                                                            <asp:TextBox ID="txtTamount" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Amount" Style="text-align: right" onkeypress="CheckAmount(this);" onblur="CalPerAmountforTDS(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTAmount" runat="server"
                                                                ControlToValidate="txtTamount" Display="None"
                                                                ErrorMessage="Please Enter TDS On Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeTamount" runat="server" TargetControlID="txtTamount" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>Section :</label>
                                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                ControlToValidate="ddlSection" Display="None"
                                                                ErrorMessage="Please Select TDS Section" SetFocusOnError="true" InitialValue="0"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>Per (%) :</label>
                                                            <asp:TextBox ID="txtTDSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                                                ToolTip="Please Enter TDS Percentage" AutoPostBack="false" onblur="calTdsamount()"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTDSPer"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                                ControlToValidate="txtTDSPer" Display="None"
                                                                ErrorMessage="Please Enter TDS Percentage1" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>TDS Amount :</label>
                                                            <asp:TextBox ID="txtTDSAmount" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                                                ToolTip="Can be Edited"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTDSAmount"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                                ControlToValidate="txtTDSAmount" Display="None"
                                                                ErrorMessage="Please Enter TDS Amount" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="row" id="Td24" runat="server">
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lblTotalDebit0" runat="server" Font-Bold="True"
                                                                Text="Difference :-"></asp:Label>
                                                            <asp:Label ID="lblTotalDiff" runat="server" Font-Bold="True"
                                                                Text="0.00 "></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row" runat="server" id="trSubHead">
                                                        <div class="col-md-2">
                                                            <label>Project Sub Head</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlProjSubHead" runat="server" AppendDataBoundItems="true"
                                                                Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlProjSubHead_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4">
                                                            Remaining amount:
                                                               <asp:Label ID="lblRemainAmt" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row" runat="server" id="row4">
                                                        <div class="col-md-2">
                                                            <label>Narration</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtPerNarration" runat="server" TextMode="MultiLine" ToolTip="Please Enter Narration"
                                                                CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row" runat="server" id="trCostCenter" visible="false">
                                                        <br />
                                                        <div class="col-md-2">
                                                            <label>Cost Center</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCostCenter" runat="server" ControlToValidate="ddlCostCenter"
                                                                Display="None" ErrorMessage="Please Select Cost Center" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="row" runat="server" id="divDepartment" visible="false">
                                                        <br />
                                                        <div class="col-md-2">
                                                            <label>Department</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddldepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldepartment"
                                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="lblDamoun" runat="server" Font-Bold="true" Text="" Font-Size="Small" BorderColor="White" BorderStyle="None"
                                                                Style="background-color: Transparent; text-align: right; padding-top: 7px"></asp:Label>
                                                        </div>
                                                    </div>


                                                    <div class="row" runat="server" id="trBudgetHead" visible="false">
                                                        <br />
                                                        <div class="col-md-2">
                                                            <label>Budget Head</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlBudgetHead" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged" CssClass="form-control">
                                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBudgetHead"
                                                                Display="None" ErrorMessage="Please Select Budget Head" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="lblBudgetBal" runat="server" Font-Bold="true" Text="" Font-Size="Small" BorderColor="White" BorderStyle="None"
                                                                Style="background-color: Transparent; text-align: right; padding-top: 7px"></asp:Label>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <br />
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="AccMoney"
                                                                OnClick="btnAdd_Click" OnClientClick=" if(Page_ClientValidate()) return ProjectSubHeadValidation(); return false;" />
                                                            <%----%>
                                                            <%--OnClientClick="return ProjectSubHeadValidation()"--%>
                                                            <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                DisplayMode="List" ValidationGroup="AccMoney" />
                                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="X" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                                                Visible="false" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTranAmt"
                                                                Display="None" ErrorMessage="Please Enter Transaction Amount" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cmpmoney" runat="server" ControlToValidate="txtTranAmt"
                                                                Display="None" ErrorMessage="Please Enter Numeric Values Only" SetFocusOnError="True"
                                                                Type="Double" ValidationGroup="AccMoney"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" id="rowgrid">
                                                        <div class="col-md-2">
                                                            <input id="hdnAgParty" runat="server" type="hidden" />
                                                            <input id="hdnOparty" runat="server" type="hidden" />
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:Panel ID="ScrollPanel" CssClass="scr" Height="200px" runat="server" ScrollBars="Both">
                                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" GridLines="Vertical"
                                                                    AutoGenerateColumns="False" OnSelectedIndexChanging="GridData_SelectedIndexChanging"
                                                                    CssClass="table table-striped table-bordered table-hover" BorderStyle="None" BorderWidth="1px"
                                                                    OnRowDataBound="GridData_RowDataBound" OnDataBound="GridData_DataBound">
                                                                    <Columns>
                                                                        <asp:CommandField ShowSelectButton="True" />
                                                                        <asp:BoundField DataField="Particulars" HeaderText="Particulars" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                                            <ItemStyle Wrap="False" Width="30%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Balance" HeaderText="Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Narration" HeaderText="Narration" HtmlEncode="False" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                                            <ItemStyle Wrap="True" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Debit" HeaderText="Debit" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Credit" HeaderText="Credit" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Mode" HeaderText="Mode" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnPartyNo" runat="server" Value='<%# Eval("Id") %>' />
                                                                                <asp:HiddenField ID="hdnChqNo" runat="server" Value='<%# Eval("ChqNo") %>' />
                                                                                <asp:HiddenField ID="hdnChqDate" runat="server" Value='<%# Eval("ChqDate") %>' />
                                                                                <asp:HiddenField ID="hdnOppParty" runat="server" Value='<%# Eval("OppParty") %>' />
                                                                                <asp:HiddenField ID="hdnCostCenterID" runat="server" Value='<%# Eval("CCID")%>' />
                                                                                <asp:HiddenField ID="hdnBudgetHeadId" runat="server" Value='<%# Eval("BudgetNo")%>' />
                                                                                <asp:HiddenField ID="hdnSubproject" runat="server" Value='<%# Eval("ProjectSubId")%>' />

                                                                                <asp:HiddenField ID="hdnsection" runat="server" Value='<%# Eval("TDSSection")%>' />
                                                                                <asp:HiddenField ID="hdnTDSamount" runat="server" Value='<%# Eval("TDSAMOUNT")%>' />
                                                                                <asp:HiddenField ID="hdnTDSPersentage" runat="server" Value='<%# Eval("TDSpercentage")%>' />
                                                                                <asp:HiddenField ID="hdnDepartmentId" runat="server" Value='<%# Eval("DepartmentId")%>' />
                                                                                <%--ADDED BY VIJAY ANDOJU ON 26082020--%>
                                                                                <asp:HiddenField ID="hdnIGSTPER" runat="server" Value='<%# Eval("IGSTper")%>' />
                                                                                <asp:HiddenField ID="hdnIGSTAMOUNT" runat="server" Value='<%# Eval("IGSTAmount")%>' />
                                                                                <asp:HiddenField ID="hdnIGSTONAMOUNT" runat="server" Value='<%# Eval("IGSTonAmount")%>' />
                                                                                <%--ADDED BY VIJAY ANDOJU ON 26082020--%>
                                                                                <asp:HiddenField ID="hdnCGSTPER" runat="server" Value='<%# Eval("CGSTper")%>' />
                                                                                <asp:HiddenField ID="hdnCGSTAMOUNT" runat="server" Value='<%# Eval("CGSTAmount")%>' />
                                                                                <asp:HiddenField ID="hdnCGSTONAMOUNT" runat="server" Value='<%# Eval("CGSTonAmount")%>' />
                                                                                <%--ADDED BY VIJAY ANDOJU ON 26082020--%>
                                                                                <asp:HiddenField ID="hdnSGSTPER" runat="server" Value='<%# Eval("SGSTper")%>' />
                                                                                <asp:HiddenField ID="hdnSGSTAMOUNT" runat="server" Value='<%# Eval("SGSTAmount")%>' />
                                                                                <asp:HiddenField ID="hdnSGSTONAMOUNT" runat="server" Value='<%# Eval("SGSTonAmount")%>' />

                                                                                <%---- Added by vijay andoju on 16092020--%>
                                                                                <asp:HiddenField ID="hdnDld" runat="server" Value='<%# Eval("Dld_id")%>' />


                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCC99" />
                                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="White" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <table class="FixHead FixHead-hover FixHead-striped" style="width: 100%">
                                                                <tr id="Rowdrcr" runat="server" visible="false">
                                                                    <td style="text-align: center; width: 1%; font-weight: bold">&nbsp;</td>
                                                                    <td style="text-align: left; width: 10%; font-weight: bold">&nbsp;</td>
                                                                    <td style="text-align: left; width: 15%; font-weight: bold">Total Amount</td>
                                                                    <td style="text-align: left; width: 15%; font-weight: bold">
                                                                        <asp:Label ID="lblTotalDebit" Style="text-align: right" runat="server" Text="0.00 Dr"
                                                                            BorderColor="#339933" Font-Bold="True"></asp:Label>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                                        <asp:Label ID="lblTotalCredit" Style="text-align: right" runat="server" Text="0.00 Cr"
                                                                            BorderColor="#339933" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="RowTot" runat="server">
                                                                    <td style="text-align: center; width: 1%; font-weight: bold">&nbsp;</td>
                                                                    <td style="text-align: left; width: 15%; font-weight: bold">&nbsp;</td>
                                                                    <td style="text-align: left; width: 15%; font-weight: bold; visibility: hidden">Total Amount</td>
                                                                    <td style="text-align: left; width: 15%; font-weight: bold">
                                                                        <span style="text-align: right;"><b>Total Amount :</b></span>
                                                                        <asp:Label ID="lblTotal" runat="server" BorderColor="#339933" Font-Bold="True"
                                                                            Style="text-align: right" Text="0.00 "></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div id="Div8" class="row" runat="server">
                                                        <div class="col-md-2">
                                                            <label>Party Name</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtPartyName" runat="server" TextMode="MultiLine" ToolTip="Please Enter Party Name"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label>PAN No</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtPanNo" runat="server" ToolTip="Please Enter PAN Number" MaxLength="10" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" id="divgstno" visible="true">
                                                        <div class="col-sm-2">
                                                            <label>Gstin No:</label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtGSTNNO" runat="server" placeholder="GSTIN NUMBER" CssClass="form-control" ToolTip="GSTIN NUMBER" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtgstinno" runat="server" TargetControlID="txtGSTNNO" FilterType="UppercaseLetters,Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label>Nature of Service</label>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtNatureService" runat="server" ToolTip="Enter Nature of Service" TextMode="MultiLine"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" id="row3" runat="server">
                                                        <div class="col-md-2">
                                                            <label>Narration</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" MaxLength="390"
                                                                ToolTip="Please Enter Narration" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" runat="server" id="Div9">
                                                        <div class="col-md-2">
                                                            <label>Cheque No./Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtChqNo2" runat="server" AutoPostBack="false" ToolTip="Please Enter Account Name"
                                                                MaxLength="6" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender TargetControlID="txtChqNo2" ID="ajxtklfiltertxtBox"
                                                                runat="server" ValidChars="0123456789">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtChequeDt2" runat="server" ToolTip="Please Enter Account Name"
                                                                    CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtChequeDt2">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtChequeDt2" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>

                                                                <asp:HiddenField runat="server" ID="hdnAskSave" />

                                                                <%--<input id="hdnAskSave" runat="server" type="hidden">--%>
                                                                <input id="hdnVchId" runat="server" type="hidden" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <input id="hdnIdEditParty" runat="server" type="hidden" />
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="AccMoney1"
                                                                OnClick="btnSave_Click" OnClientClick="return ProjectValidation();" />
                                                            <%-- OnClientClick="return AskSave();"  --%>
                                                            &nbsp;<asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnReset_Click" />
                                                            &nbsp;<asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBackToList_Click" />
                                                            <asp:Button ID="btnExporttoExcel" runat="server" Text="Export" OnClick="btnExporttoExcel_Click"
                                                                Visible="false" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="AccMoney1" DisplayMode="List" />
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                                DropShadow="True" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                                                Enabled="True">
                                                            </ajaxToolKit:ModalPopupExtender>
                                                            <asp:Panel ID="pnl" runat="server" Width="600px" BorderColor="#0066FF" BackColor="White" meta:resourcekey="pnlResource1">
                                                                <div class="panel panel-primary">
                                                                    <div class="panel-heading">
                                                                        Transaction
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                                        <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" ValidationGroup="Validation"
                                                                            CssClass="btn btn-info" OnClick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                                                                        &nbsp;<asp:Button ID="btnBack" runat="server" Text="Close" ValidationGroup="Validation"
                                                                            CssClass="btn btn-danger" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
                                                                        &nbsp;<asp:Button ID="btnchequePrint" runat="server" CssClass="btn btn-primary" Text="Print Cheque" OnClick="btnchequePrint_Click" />
                                                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                                                        <asp:ListView ID="lvGrp" runat="server">
                                                                            <LayoutTemplate>
                                                                                <h4 id="demo-grid">
                                                                                    <h4 class="box-title">Transaction
                                                                                    </h4>
                                                                                    <table class="table table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
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
                                                                                </div>
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
                                                                            <AlternatingItemTemplate>
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
                                                                            </AlternatingItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <input id="hdnbal2" runat="server" type="hidden" />
                </div>
                <div class="form-group row">
                    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                        runat="server" TargetControlID="div" PopupControlID="div"
                        BackgroundCssClass="modalBackground" />
                    <%--OkControlID="btnOkDel"
                        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"--%>
                    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup" Height="200px">
                        <div style="text-align: center">
                            <div class="col-md-12">
                                <div class="form-group row">
                                    <div class="text-center">
                                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                    </div>
                                    <div>
                                        Selected Budget Head has Less than 0.00 or 0.00 amount left, Are you sure you want to Save data?
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="text-center">
                                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-success" OnClick="btnOkDel_Click" />
                                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-danger" OnClick="btnNoDel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="txtAcc" />
            </Triggers>--%>
        </asp:UpdatePanel>
        <script type="text/javascript">
            //  keeps track of the delete button for the row
            //  that is going to be removed
            var _source;
            // keep track of the popup div
            var _popup;

            function showConfirmDel(source) {
                this._source = source;
                this._popup = $find('mdlPopupDel');

                //  find the confirm ModalPopup and show it    
                this._popup.show();
            }

            function okDelClick() {
                //  find the confirm ModalPopup and hide it    
                this._popup.hide();
                //  use the cached button as the postback source
                __doPostBack(this._source.name, '');
            }

            function cancelDelClick() {
                //  find the confirm ModalPopup and hide it 
                this._popup.hide();
                //  clear the event source
                this._source = null;
                this._popup = null;
            }
        </script>
        <script language="javascript" type="text/javascript">
            function OnSuccess(result) {
                document.getElementById('<%=lblSpan.ClientID %>').innerHTML = result;
                //document.getElementById("lblSpan").innerHTML=result;
            }

            function OnFailure(error) {
            }
            function CalPerAmountforTDS(val) {

                var AmtWithoutTDS = document.getElementById('<%= txtTamount.ClientID %>').value;
                var AmtWithTDS = document.getElementById('<%= txtTranAmt.ClientID %>').value;

                var ChTranAmount = Number(AmtWithTDS);
                var ChTdsAmount = Number(AmtWithoutTDS);

                if (ChTranAmount < ChTdsAmount) {
                    alert('TDS Amount Should be Less than Or Equals to Amount');
                    document.getElementById('<%= txtTamount.ClientID %>').value = '';
                    return;
                }


                var TDSper = document.getElementById('<%= txtTDSPer.ClientID %>');
                var NetTDSAmt = document.getElementById('<%= txtTDSAmount.ClientID %>');

                var Calc = 0, Netamt = 0;

                if (TDSper != "") {
                    Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
                    NetTDSAmt.value = Math.round(parseFloat(Calc));
                }
                else {

                }

            }
        </script>

        <div id="divMsg" runat="server">
        </div>
        <div id="dvConfirm" title="" runat="server" style="display: none">
            <asp:Label ID="lblconfirm" runat="server" Style="display: none"></asp:Label>
        </div>
    </div>
</asp:Content>

