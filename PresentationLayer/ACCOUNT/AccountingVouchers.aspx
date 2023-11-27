<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AccountingVouchers.aspx.cs" Inherits="AccountingVouchers" Title=""
    UICulture="auto" %>

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

        #ctl00_ContentPlaceHolder1_pnl {
            background: #fff;
            box-shadow: 0px 0px 10px #adacac;
            padding: 15px 0px;
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
    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

    <script language="javascript" type="text/javascript">
        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, CAPTION, 'Create Sub Links');
            return true;
        }

    </script>

    <%--<link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>

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
                if ($('#<%= ddlSponsor.ClientID%> option:selected').val() != "0") {
                    if (parseFloat($('#<%= lblRemainAmt.ClientID%>').text()) == 0.00) {

                        alert("There is no balance for project. You can not proceed.");
                        return false;
                    }
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
            debugger;
            alert("tanu")
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
            if (Gvienamount > Amount) {
                alert("Amount Should be less than or equal to Amount");
                val.value = Amount;
            }

        }
        function calcgstamount() {
            var amount = parseFloat(document.getElementById("<%=txtCgstOnAmount.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtCgstPer.ClientID%>").value).toFixed(2);
            if (per != 'NaN') {
                if (per > 99) {
                    alert('The Percentage Should be Less than 100');
                    document.getElementById("<%=txtCgstPer.ClientID%>").value = 0;
                }
                else if (per == "" || per == 0) {
                    alert('Please Enter Percentage Value should be grater than 0');
                    document.getElementById("<%=txtCgstPer.ClientID%>").value = 0;
                    return;

                }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtCGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
        }
    }
    function calsgstamount() {
        var amount = parseFloat(document.getElementById("<%=txtSgstOnAmount.ClientID%>").value).toFixed(2);
        var per = parseFloat(document.getElementById("<%=txtSGTSPer.ClientID%>").value).toFixed(2);
        if (per != 'NaN') {
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtSGTSPer.ClientID%>").value = 0;
            }
            else if (per == "" || per == 0) {
                alert('Please Enter Percentage Value should be grater than 0');
                document.getElementById("<%=txtSGTSPer.ClientID%>").value = 0;
                return;

            }
        var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
        document.getElementById("<%=txtSGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
    }
}
function calIgstamount() {
    var amount = parseFloat(document.getElementById("<%=txtIgstOnAmount.ClientID%>").value).toFixed(2);
    var per = parseFloat(document.getElementById("<%=txtIGSTPER.ClientID%>").value).toFixed(2);
    if (per != 'NaN') {
        if (per > 99) {
            alert('The Percentage Should be Less than 100');
            document.getElementById("<%=txtIGSTPER.ClientID%>").value = 0;
        }
        else if (per == "" || per == 0) {
            alert('Please Enter Percentage Value should be grater than 0');
            document.getElementById("<%=txtIGSTPER.ClientID%>").value = 0;
            return;

        }
    var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
    document.getElementById("<%=txtIGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
}
}

function calTdsamount() {
    var amount = parseFloat(document.getElementById("<%=txtTdsOnAmount.ClientID%>").value).toFixed(2);
    var per = parseFloat(document.getElementById("<%=txtTDSPer.ClientID%>").value).toFixed(2);
    if (per != 'NaN') {
        if (per > 99) {
            alert('The Percentage Should be Less than 100');
            document.getElementById("<%=txtTDSPer.ClientID%>").value = 0;
        }
        else if (per == "" || per == 0) {
            alert('Please Enter Percentage Value should be grater than 0');
            document.getElementById("<%=txtTDSPer.ClientID%>").value = 0;
            return;

        } var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
    var total = parseFloat(amountper * per).toFixed(2);
    total = Math.round(total);
    document.getElementById("<%=txtTDSAmount.ClientID%>").value = total;
  }
}
function calTdsOnCgstAmount() {
    var amount = parseFloat(document.getElementById("<%=txtTdsCgstOnAmt.ClientID%>").value).toFixed(2);
    var per = parseFloat(document.getElementById("<%=txtTdsOnCgstPer.ClientID%>").value).toFixed(2);
    if (per != 'NaN') {
        if (per > 99) {
            alert('The Percentage Should be Less than 100');
            document.getElementById("<%=txtTdsOnCgstPer.ClientID%>").value = 0;
        }
        else if (per == "" || per == 0) {
            alert('Please Enter Percentage Value should be grater than 0');
            document.getElementById("<%=txtTdsOnCgstPer.ClientID%>").value = 0;
            return;

        } var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
    document.getElementById("<%=txtTdsOnCgstAmt.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
    }
}
function calTdsOnSgstAmount() {
    var amount = parseFloat(document.getElementById("<%=txtTdsSgstOnAmt.ClientID%>").value).toFixed(2);
    var per = parseFloat(document.getElementById("<%=txtTdsOnSgstPer.ClientID%>").value).toFixed(2);
    if (per != 'NaN') {
        if (per > 99) {
            alert('The Percentage Should be Less than 100');
            document.getElementById("<%=txtTdsOnSgstPer.ClientID%>").value = 0;
        }
        else if (per == "" || per == 0) {
            alert('Please Enter Percentage Value should be grater than 0');
            document.getElementById("<%=txtTdsOnSgstPer.ClientID%>").value = 0;
            return;

        } var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
    document.getElementById("<%=txtTdsOnSgstAmt.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
    }
}
function calTdsOnIGstAmount() {
    var amount = parseFloat(document.getElementById("<%=txtTdsIgstOnAmt.ClientID%>").value).toFixed(2);
    var per = parseFloat(document.getElementById("<%=txtTdsOnIgstPer.ClientID%>").value).toFixed(2);
    if (per != 'NaN') {
        if (per > 99) {
            alert('The Percentage Should be Less than 100');
            document.getElementById("<%=txtTdsOnIgstPer.ClientID%>").value = 0;
        }
        else if (per == "" || per == 0) {
            alert('Please Enter Percentage Value should be grater than 0');
            document.getElementById("<%=txtTdsOnIgstPer.ClientID%>").value = 0;
            return;

        } var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
    document.getElementById("<%=txtTdsOnIgstAmt.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
    }
}
    </script>

    <script language="javascript" type="text/javascript">
        function copyamount() {
            var GST = document.getElementById('<%=chkGST.ClientID%>').checked;
            var IGST = document.getElementById('<%=chkIGST.ClientID%>').checked;
            var TDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            var TDSonGST = document.getElementById('<%=chkTdsOnGST.ClientID%>').checked;
            var TDSonIGST = document.getElementById('<%=chkTdsOnIGST.ClientID%>').checked;

            var Amount = parseFloat(document.getElementById('<%=txtTranAmt.ClientID%>').value);


            if (GST) {
                document.getElementById('<%=txtCgstOnAmount.ClientID%>').value = document.getElementById('<%=txtSgstOnAmount.ClientID%>').value = Amount;
                calsgstamount();
                calcgstamount();
            }
            if (IGST) {
                document.getElementById('<%=txtIgstOnAmount.ClientID%>').value = Amount;
                calIgstamount();
            }
            if (TDS) {
                document.getElementById('<%=txtTdsOnAmount.ClientID%>').value = Amount;
                calTdsamount();
            }
            if (TDSonGST) {
                document.getElementById('<%=txtTdsCgstOnAmt.ClientID%>').value = Amount;
                document.getElementById('<%=txtTdsSgstOnAmt.ClientID%>').value = Amount;
                calTdsOnCgstAmount();
                calTdsOnSgstAmount();
            }
            if (TDSonIGST) {
                document.getElementById('<%=txtTdsIgstOnAmt.ClientID%>').value = Amount;
                calTdsOnIGstAmount();
            }
        }
        //Added by vijay andoju for checkingLedgers
        function CheckLedger() {

        }
    </script>

    <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>

    <%--  <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>--%>

    <asp:Panel ID="UPDLedger" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div10" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">ACCOUNTING VOUCHER CREATION</h3>
                    </div>
                    <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                    <div class="box-body">
                        <asp:Panel ID="Panel1" runat="server">
                            <%--  <div class="panel-heading">Create Account Vouchers</div>--%>
                            <div class="col-12">
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
                                    <div class=" col-12 sub-heading">
                                        <h5>Against Account Entry</h5>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <label><span style="color: red">*</span> Transaction Mode  </label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlTranType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            meta:resourcekey="ddlTranTypeResource1">
                                            <asp:ListItem Value="P" meta:resourcekey="ListItemResource1" Text="Payment"></asp:ListItem>
                                            <asp:ListItem Value="R" meta:resourcekey="ListItemResource2" Text="Receipt"></asp:ListItem>
                                            <asp:ListItem Value="C" meta:resourcekey="ListItemResource3" Text="Contra"></asp:ListItem>
                                            <asp:ListItem Value="J" meta:resourcekey="ListItemResource4" Text="Journal"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2" style="width: 10%">
                                        <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="form-control"
                                            ToolTip="Please Enter Voucher No." meta:resourcekey="txtVoucherNoResource1"
                                            OnTextChanged="txtVoucherNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" ControlToValidate="txtVoucherNo"
                                            Display="None" ErrorMessage="Please Enter Voucher No" SetFocusOnError="True"
                                            ValidationGroup="AccMoney1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1" style="text-align: right">
                                        <b>
                                            <asp:Label ID="lblDate" CssClass="control-label" runat="server" Text="Date"></asp:Label></b>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image3">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <span id="lblSpan" runat="server" style="text-align: left; display: none"></span>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnTextChanged="txtDate_TextChanged" meta:resourcekey="txtDateResource1" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="Image3" TargetControlID="txtDate">
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
                                <%--  <br />     --%>

                                <div class="row" runat="server" visible="false">
                                    <div class="col-md-2">
                                        <label><span style="color: red">*</span>  Bill No. </label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged">
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


                                <br />
                                <div id="trSponsor" runat="server" class="row">
                                    <%-- <br />--%>
                                    <div class="col-md-2">
                                        <label>Sponsor Project<span style="color: red"></span>  </label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlSponsor" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSponsor_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <%--  <div class="row" runat="server" id="trSubHead">--%>
                                    <div class="col-md-2">
                                        <label>Project Sub Head</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlProjSubHead" runat="server" AppendDataBoundItems="true"
                                            data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProjSubHead_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        Remaining amount:
                                          <asp:Label ID="lblRemainAmt" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <%--  </div>--%>
                                </div>
                                <br />
                                <div id="divDepartment" class="row md-12" runat="server">

                                    <div class="col-md-2">
                                        <label>Department</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddldepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldepartment"
                                            Display="None" ErrorMessage="Please Select Department" SetFocusOnError="True"
                                            ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <br />

                                <div class="row md-6" runat="server" id="divDeptBudget" visible="false">

                                    <div class="col-md-2">
                                        <label>Budget Head</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlBudgetHead" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
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
                                    <%--  <div class="col-md-2">
                                                            <asp:Label ID="lblDamoun" runat="server" Font-Bold="true" Text="" Font-Size="Small" BorderColor="White" BorderStyle="None"
                                                                Style="background-color: Transparent; text-align: right; padding-top: 7px"></asp:Label>
                                                        </div>--%>
                                </div>

                                <div class="row mt-3" id="Row2" runat="server">
                                    <div class="col-md-2">
                                        <label><span style="color: red">*</span> Against Account  </label>
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
                                            ControlToValidate="txtAgainstAcc" Display="None" ErrorMessage="Please Select Against Account" SetFocusOnError="true"
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
                                <div class="row mt-3 ">
                                    <div class=" col-12 sub-heading">
                                        <h5>Account Entry</h5>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <label><span style="color: red">*</span> Account</label>
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
                                            ErrorMessage="Please Select Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblCurBal2" runat="server" TabIndex="-2" Font-Bold="true" Style="background-color: Transparent; text-align: left;"></asp:Label>
                                        <asp:Label ID="lblCrDr2" runat="server" BorderColor="Black" BorderStyle="None"
                                            Height="16px" ReadOnly="True" Style="background-color: Transparent; text-align: left;"
                                            TabIndex="-6" Width="20%" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblCur2" runat="server" BorderColor="white" BorderStyle="None"
                                            ReadOnly="True" Style="background-color: Transparent" TabIndex="-2" Width="75px"
                                            ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>

                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <label><span style="color: red">*</span>Amount</label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtTranAmt" runat="server" Width="170%" Font-Size="Small" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                            autocomplete="off" AutoPostBack="false" MaxLength="12" onkeyup="copyamount();"></asp:TextBox>
                                        <%-- <asp:TextBox ID="txtTranAmt" runat="server" Style="text-align: right" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                                                autocomplete="off" AutoPostBack="false" MaxLength="13" onkeyup="copyamount();" OnTextChanged="txtTranAmt_TextChanged"></asp:TextBox>--%>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtTranAmt"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                    </div>
                                    <div class="col-md-1">
                                        <asp:DropDownList ID="ddlcrdr" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlcrdr_SelectedIndexChanged">
                                            <asp:ListItem>Dr</asp:ListItem>
                                            <asp:ListItem>Cr</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>trSposor
                                    <div id="Div5" class="col-md-1" runat="server" visible="false">
                                        <asp:TextBox ID="txtChqNo1" runat="server" ToolTip="Please Enter Account Name" Width="70%"
                                            Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtChequeDt1" runat="server" ToolTip="Please Enter Account Name"
                                            Visible="False"></asp:TextBox>&nbsp;<asp:Image ID="Image1" runat="server"
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



                                    <div>
                                        <asp:CheckBox ID="chkTDSApplicable" runat="server" Text="TDS (Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkTDSApplicable_CheckedChanged" />
                                        <asp:CheckBox ID="chkGST" runat="server" Text="GST (Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkGst_CheckedChanged" />
                                        <asp:CheckBox ID="chkTdsOnGST" runat="server" Text="TDS ON GST (Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkTdsOnGST_CheckedChanged" />
                                        <asp:CheckBox ID="chkIGST" runat="server" Text="IGST (Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkIGST_CheckedChanged" />
                                        <asp:CheckBox ID="chkTdsOnIGST" runat="server" Text="TDS ON IGST (Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkTdsOnIGST_CheckedChanged" />
                                        <asp:CheckBox ID="chkSecurity" runat="server" Text="SECURITY(Y/N)" AutoPostBack="true"
                                            OnCheckedChanged="chkSecurity_CheckedChanged" />

                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Tag User</label>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlEmpType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Employee</asp:ListItem>
                                            <asp:ListItem Value="2">Payee</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2" id="divEmployee1" runat="server" visible="false">
                                        <label><span style="color: red">*</span> Select Employee</label>
                                    </div>
                                    <div class="col-md-3" id="divEmployee2" runat="server" visible="false">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2" id="divPayeeNature1" runat="server" visible="false">
                                        <label><span style="color: red">*</span> Select Payee Nature</label>
                                    </div>
                                    <div class="col-md-3" id="divPayeeNature2" runat="server" visible="false">
                                        <asp:DropDownList ID="ddlPayeeNature" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPayeeNature_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2" id="divPayee1" runat="server" visible="false">
                                        <label>Select Payee Name<span style="color: red">*</span></label>
                                    </div>
                                    <div class="col-md-3" id="divPayee2" runat="server" visible="false">
                                        <asp:DropDownList ID="ddlPayee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPayee_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <br />
                                <div class="row" id="divPrevAdv1" runat="server" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label>Previous Advance</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlPrevAdv" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPrevAdv_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="LblBalanceAmount" runat="server" TabIndex="-2" Font-Bold="true" Style="background-color: Transparent; text-align: left;" Visible="false"></asp:Label>

                                    </div>
                                </div>

                                <%--Added by vijay andoju on 25-08-2020 for GST and IGST--%>
                                <div id="divgst" runat="server" visible="false" class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>SGST Account </label>
                                        </div>
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>SGST On Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtSgstOnAmount" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calsgstamount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtSgstOnAmount" Display="None"
                                            ErrorMessage="Please Enter SGST On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSgstOnAmount" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div6" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSgstSection" runat="server" AppendDataBoundItems="true" AutoPostBack="false" CssClass="form-control"
                                            onblur="CalPerAmountforTDS(this);">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Per (%)</label>
                                        </div>
                                        <asp:TextBox ID="txtSGTSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2" AutoPostBack="false"
                                            ToolTip="Please Enter SGST Percentage" onblur="calsgstamount();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtSGTSPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="txtSGTSPer" Display="None"
                                            ErrorMessage="Please Enter Per(%) For SGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>SGST Amount </label>
                                        </div>
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>CGST Account</label>
                                        </div>
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>CGST On Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtCgstOnAmount" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calcgstamount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtCgstOnAmount" Display="None"
                                            ErrorMessage="Please Enter CGST On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCgstOnAmount" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCgstSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            onblur="CalPerAmountforTDS(this);">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Per (%)</label>
                                        </div>
                                        <asp:TextBox ID="txtCgstPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            ToolTip="Please Enter CGST Percentage" AutoPostBack="false" onblur="calcgstamount();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCgstPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                            ControlToValidate="txtCgstPer" Display="None"
                                            ErrorMessage="Please Enter Per(%) For CGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>CGST Amount</label>
                                        </div>
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


                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-3">
                                        <label>IGST Account </label>
                                        <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" ToolTip="Please Enter IGST Ledger"></asp:TextBox>
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
                                        <label>IGST On Amount </label>
                                        <asp:TextBox ID="txtIgstOnAmount" runat="server" CssClass="form-control" ToolTip="Please Enter IGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calIgstamount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtIgstOnAmount" Display="None"
                                            ErrorMessage="Please Enter IGST Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtIgstOnAmount" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div id="Div7" class="col-md-2" runat="server" visible="false">
                                        <label>Section</label>
                                        <asp:DropDownList ID="ddlIgstSection" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            onblur="CalPerAmountforTDS(this);">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <label><span style="color: red">*</span> Per (%)</label>
                                        <asp:TextBox ID="txtIGSTPER" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            ToolTip="Please Enter IGST Percentage" AutoPostBack="false" onblur="calIgstamount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="txtIGSTPER" Display="None"
                                            ErrorMessage="Please Enter Per(%) For IGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtIGSTPER"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                    </div>
                                    <div class="col-md-2">
                                        <label>IGST Amount </label>
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS Account</label>
                                        </div>
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS On Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtTdsOnAmount" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Amount" onkeyup="CheckAmount(this);" onblur="calTdsamount();"></asp:TextBox><%--onblur="CalPerAmountforTDS(this)"--%>
                                        <asp:RequiredFieldValidator ID="rfvTAmount" runat="server"
                                            ControlToValidate="txtTdsOnAmount" Display="None"
                                            ErrorMessage="Please Enter TDS On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeTamount" runat="server" TargetControlID="txtTdsOnAmount" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTdsSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlTdsSection_SelectedIndexChanged">
                                            <%--onblur="CalPerAmountforTDS(this);"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="ddlTdsSection" Display="None"
                                            ErrorMessage="Please Select Section For TDS" SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Per (%)</label>
                                        </div>
                                        <asp:TextBox ID="txtTDSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            ToolTip="Please Enter TDS Percentage" AutoPostBack="false" onblur="calTdsamount()"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTDSPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="txtTDSPer" Display="None"
                                            ErrorMessage="Please Enter Per(%) For TDS" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS Amount</label>
                                        </div>
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


                                <%--Added by Gopal Anthati on 31-08-2021--%>
                                <div id="divTdsOnCgst" runat="server" visible="false" class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS On CGST Account </label>
                                        </div>
                                        <asp:TextBox ID="txtTdsOnCgstAcc" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger" OnTextChanged="txtTdsOnCgstAcc_TextChanged"></asp:TextBox>
                                        <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtTdsOnCgstAcc"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                            ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                        </ajaxToolKit:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                            ControlToValidate="txtTdsOnCgstAcc" Display="None"
                                            ErrorMessage="Please Select TDS On CGST Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS CGST On Amount </label>
                                        </div>
                                        <asp:TextBox ID="txtTdsCgstOnAmt" runat="server" CssClass="form-control" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calTdsOnCgstAmount();"></asp:TextBox><%--onblur="CalPerAmountforTDS(this)"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="txtTdsCgstOnAmt" Display="None"
                                            ErrorMessage="Please Enter TDS CGST On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtTdsCgstOnAmt" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section </label>
                                        </div>
                                        <asp:DropDownList ID="ddlTdsOnCgstSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlTdsOnCgstSection_SelectedIndexChanged">
                                            <%--onblur="CalPerAmountforTDS(this);"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                            ControlToValidate="ddlTdsOnCgstSection" Display="None"
                                            ErrorMessage="Please Select Section For TDS On CGST" SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Per (%) </label>
                                        </div>
                                        <asp:TextBox ID="txtTdsOnCgstPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            AutoPostBack="false" onblur="calTdsOnCgstAmount()"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTdsOnCgstPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server"
                                            ControlToValidate="txtTdsOnCgstPer" Display="None"
                                            ErrorMessage="Please Enter Per(%) For TDS On CGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>TDS On CGST Amount </label>
                                        </div>
                                        <asp:TextBox ID="txtTdsOnCgstAmt" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                            ToolTip="Can be Edited"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTdsOnCgstAmt"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                                            ControlToValidate="txtTdsOnCgstAmt" Display="None"
                                            ErrorMessage="Please Enter TDS On CGST Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div id="divTdsOnSgst" runat="server" visible="false" class="row mb-3">

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>TDS On SGST Account </label>
                                        <asp:TextBox ID="txtTdsOnSgstAcc" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger" OnTextChanged="txtTdsOnCgstAcc_TextChanged"></asp:TextBox>
                                        <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" TargetControlID="txtTdsOnSgstAcc"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                            ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                        </ajaxToolKit:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server"
                                            ControlToValidate="txtTdsOnSgstAcc" Display="None"
                                            ErrorMessage="Please Select TDS On SGST Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>TDS SGST On Amount</label>
                                        <asp:TextBox ID="txtTdsSgstOnAmt" runat="server" CssClass="form-control" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calTdsOnSgstAmount();"></asp:TextBox><%--onblur="CalPerAmountforTDS(this)"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server"
                                            ControlToValidate="txtTdsSgstOnAmt" Display="None"
                                            ErrorMessage="Please Enter TDS SGST On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtTdsSgstOnAmt" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>Section </label>
                                        <asp:DropDownList ID="ddlTdsOnSgstSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlTdsOnSgstSection_SelectedIndexChanged">
                                            <%--onblur="CalPerAmountforTDS(this);"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server"
                                            ControlToValidate="ddlTdsOnSgstSection" Display="None"
                                            ErrorMessage="Please Select Section For TDS On SGST" SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-md-6 col-12">
                                        <label><span style="color: red">*</span> Per (%) </label>
                                        <asp:TextBox ID="txtTdsOnSgstPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            AutoPostBack="false" onblur="calTdsOnSgstAmount()"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txtTdsOnSgstPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server"
                                            ControlToValidate="txtTdsOnSgstPer" Display="None"
                                            ErrorMessage="Please Enter Per (%) For TDS On SGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2 col-md-6 col-12">
                                        <label>TDS On CGST Amount</label>
                                        <asp:TextBox ID="txtTdsOnSgstAmt" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                            ToolTip="Can be Edited"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txtTdsOnSgstAmt"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server"
                                            ControlToValidate="txtTdsOnSgstAmt" Display="None"
                                            ErrorMessage="Please Enter TDS On SGST Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div id="divTdsOnIgst" runat="server" visible="false" class="row">

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>TDS On IGST Account </label>
                                        <asp:TextBox ID="txtTdsOnIgstAcc" runat="server" CssClass="form-control" OnTextChanged="txtTdsOnIgstAcc_TextChanged"></asp:TextBox>
                                        <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtTdsOnIgstAcc"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                            ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                        </ajaxToolKit:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server"
                                            ControlToValidate="txtTdsOnIgstAcc" Display="None"
                                            ErrorMessage="Please Select TDS On Gst Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>TDS IGST On Amount</label>
                                        <asp:TextBox ID="txtTdsIgstOnAmt" runat="server" CssClass="form-control" Style="text-align: right" onkeyup="CheckAmount(this);" onblur="calTdsOnIGstAmount();"></asp:TextBox><%--onblur="CalPerAmountforTDS(this)"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                                            ControlToValidate="txtTdsIgstOnAmt" Display="None"
                                            ErrorMessage="Please Enter TDS IGST On Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtTdsIgstOnAmt" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>Section </label>
                                        <asp:DropDownList ID="ddlTdsOnIgstSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlTdsOnIgstSection_SelectedIndexChanged">
                                            <%--onblur="CalPerAmountforTDS(this);"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                                            ControlToValidate="ddlTdsSection" Display="None"
                                            ErrorMessage="Please Select Section For Tds On Gst" SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-1 col-md-6 col-12">
                                        <label><span style="color: red">*</span> Per (%)</label>
                                        <asp:TextBox ID="txtTdsOnIgstPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="2"
                                            AutoPostBack="false" onblur="calTdsOnIGstAmount()"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtTdsOnIgstPer"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                                            ControlToValidate="txtTdsOnIgstPer" Display="None"
                                            ErrorMessage="Please Enter Per (%) For Tds On IGST" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2 col-md-6 col-12">
                                        <label>TDS On IGST Amount</label>
                                        <asp:TextBox ID="txtTdsOnIgstAmt" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right"
                                            ToolTip="Can be Edited"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtTdsOnIgstAmt"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server"
                                            ControlToValidate="txtTdsOnIgstAmt" Display="None"
                                            ErrorMessage="Please Enter TDS On IGST Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div id="divSecurity" runat="server" visible="false" class="row">

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>Security Account </label>
                                        <asp:TextBox ID="txtSecurityAcc" runat="server" CssClass="form-control" OnTextChanged="txtSecurityAcc_TextChanged"></asp:TextBox>
                                        <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtSecurityAcc"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                            ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                        </ajaxToolKit:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server"
                                            ControlToValidate="txtSecurityAcc" Display="None"
                                            ErrorMessage="Please Select Security Account" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <label>Security Amount</label>
                                        <asp:TextBox ID="txtSecurityAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server"
                                            ControlToValidate="txtSecurityAmt" Display="None"
                                            ErrorMessage="Please Enter Security Amount" SetFocusOnError="true"
                                            ValidationGroup="AccMoney">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%---------------------------------------------------------------------------------------------------%>
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



                                <div class="row" runat="server" id="row4">
                                    <div class="col-md-2">
                                        <label>Narration</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPerNarration" runat="server" TextMode="MultiLine" ToolTip="Please Enter Narration"
                                            CssClass="form-control" MaxLength="60"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-3" runat="server" id="trCostCenter" visible="false">

                                    <div class="col-md-2">
                                        <label>Cost Center</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCostCenter" runat="server" ControlToValidate="ddlCostCenter"
                                            Display="None" ErrorMessage="Please Select Cost Center" SetFocusOnError="True"
                                            ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <br />

                                <%--  start multipal cost center --%>
                                <div class="col-md-12" runat="server" id="trMultiCostCenter" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-3 col-12" id="div67" runat="server">
                                            <label>Cost Center</label>
                                            <asp:DropDownList ID="ddlmuliicostcenter" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-3 col-12" id="div68" runat="server">
                                            <label>Amount</label>
                                            <asp:TextBox ID="txtccAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-3 col-12" id="div71" runat="server">

                                            <asp:Button ID="btnaddmulticostcenter" runat="server" Text="Add" CssClass="btn btn-primary"
                                                ValidationGroup="multicc" ToolTip="" TabIndex="31" OnClick="btnaddmulticostcenter_Click" />

                                        </div>

                                        <div class="form-group col-md-12">
                                            <asp:Panel runat="server" ID="pnlMulticostC" ScrollBars="Auto">
                                                <asp:ListView ID="lvMultiCC" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <table id="divMultcc" runat="server" class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Remove</th>
                                                                        <th>Particulars
                                                                        </th>
                                                                        <th>Cost Center
                                                                        </th>
                                                                        <th id="lblapisc" runat="server">Amount
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
                                                                <asp:ImageButton ID="btneditMultCC" runat="server" CommandArgument='<%# Eval("CC_ID") %>'
                                                                    OnClick="btneditMultCC_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                <asp:HiddenField ID="hdnsnid" runat="server" Value='<%# Eval("CC_ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCcpartyname" runat="server" Text='<%# Eval("PartyName") %>' />
                                                                <asp:HiddenField ID="hdMulticcPartyId" runat="server" Value='<%# Eval("PartyID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblnameofCC" runat="server" Text='<%# Eval("CostCenter") %>' />
                                                                <asp:HiddenField ID="hdMulticcID" runat="server" Value='<%# Eval("CostCenterID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>' />
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                                <%--  end multipal cost center --%>



                                <div class="row mt-3">

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="AccMoney"
                                            OnClick="btnAdd_Click" OnClientClick=" if(Page_ClientValidate()) return ProjectSubHeadValidation(); return false;" />
                                        <%----%>
                                        <%--OnClientClick="return ProjectSubHeadValidation()"--%>
                                        <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                            DisplayMode="List" ValidationGroup="AccMoney" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?')" />
                                        <asp:Button ID="btnCancel" runat="server" Text="X" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                            Visible="false" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTranAmt"
                                            Display="None" ErrorMessage="Please Enter Amount" SetFocusOnError="True"
                                            ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpmoney" runat="server" ControlToValidate="txtTranAmt"
                                            Display="None" ErrorMessage="Please Enter Numeric Values Only" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="AccMoney"></asp:CompareValidator>
                                    </div>
                                    <div class="col-md-2">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />
                                    </div>
                                </div>
                                <br />


                                <div class="row" runat="server" id="rowgrid" visible="false">
                                    <div class="col-12">
                                        <asp:Panel ID="ScrollPanel" CssClass="scr" runat="server">

                                            <div class="table table-responsive">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" GridLines="Vertical"
                                                    AutoGenerateColumns="False" OnSelectedIndexChanging="GridData_SelectedIndexChanging"
                                                    CssClass="table table-striped table-bordered nowrap" BorderStyle="None" BorderWidth="1px"
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
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TagEmployee" HeaderText="Emp./Payee Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IsEmployee" HeaderText="IsEmployee" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemStyle CssClass="hidden" />
                                                            <HeaderStyle CssClass="hidden" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TagEmpIdno" HeaderText="TagEmpIdno" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemStyle CssClass="hidden" />
                                                            <HeaderStyle CssClass="hidden" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PrevAdvanceId" HeaderText="PrevAdvanceId" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemStyle CssClass="hidden" />
                                                            <HeaderStyle CssClass="hidden" />
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

                                                                <%--ADDED BY Gopal Anthati ON 02092021--%>
                                                                <asp:HiddenField ID="hdnTdsOnCgstSection" runat="server" Value='<%# Eval("TdsOnCgstSection")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnCgstPer" runat="server" Value='<%# Eval("TdsOnCgstPer")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnCgstAmt" runat="server" Value='<%# Eval("TdsOnCgstAmt")%>' />
                                                                <asp:HiddenField ID="hdnTdsCgstonAmt" runat="server" Value='<%# Eval("TdsCgstonAmt")%>' />
                                                                <%--ADDED BY Gopal Anthati ON 02092021--%>
                                                                <asp:HiddenField ID="hdnTdsOnSgstSection" runat="server" Value='<%# Eval("TdsOnSgstSection")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnSgstPer" runat="server" Value='<%# Eval("TdsOnSgstPer")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnSgstAmt" runat="server" Value='<%# Eval("TdsOnSgstAmt")%>' />
                                                                <asp:HiddenField ID="hdnTdsSgstonAmt" runat="server" Value='<%# Eval("TdsSgstonAmt")%>' />
                                                                <%--ADDED BY Gopal Anthati ON 02092021--%>
                                                                <asp:HiddenField ID="hdnTdsOnIgstSection" runat="server" Value='<%# Eval("TdsOnIgstSection")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnIgstPer" runat="server" Value='<%# Eval("TdsOnIgstPer")%>' />
                                                                <asp:HiddenField ID="hdnTdsOnIgstAmt" runat="server" Value='<%# Eval("TdsOnIgstAmt")%>' />
                                                                <asp:HiddenField ID="hdnTdsIgstonAmt" runat="server" Value='<%# Eval("TdsIgstonAmt")%>' />

                                                                <asp:HiddenField ID="hdnSecurityAmt" runat="server" Value='<%# Eval("SecurityAmt")%>' />

                                                                <asp:HiddenField ID="hdnIsEmployee" runat="server" Value='<%# Eval("IsEmployee") %>' />
                                                                <asp:HiddenField ID="hdnParticulars" runat="server" Value='<%# Eval("Particulars") %>' />
                                                                <asp:HiddenField ID="hdnBalance" runat="server" Value='<%# Eval("Balance") %>' />
                                                                <asp:HiddenField ID="hdnAmount" runat="server" Value='<%# Eval("Amount") %>' />
                                                                <asp:HiddenField ID="hdnMode" runat="server" Value='<%# Eval("Mode") %>' />
                                                                <asp:HiddenField ID="hdnTagEmpIdno" runat="server" Value='<%# Eval("TagEmpIdno") %>' />
                                                                <asp:HiddenField ID="hdnPrevAdvId" runat="server" Value='<%# Eval("PrevAdvanceId") %>' />

                                                                <%---- Added by vijay andoju on 16092020--%>
                                                                <asp:HiddenField ID="hdnDld" runat="server" Value='<%# Eval("Dld_id")%>' />


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle ForeColor="#000" />
                                                    <HeaderStyle BackColor="#3C8DBC" ForeColor="#000" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </div>

                                        </asp:Panel>

                                        <table class="FixHead FixHead-hover FixHead-striped" style="width: 100%">
                                            <tr id="Rowdrcr" runat="server" visible="false">
                                                <td style="text-align: center; width: 1%; font-weight: bold">&nbsp;</td>
                                                <td style="text-align: left; width: 10%; font-weight: bold">&nbsp;</td>
                                                <td style="text-align: left; width: 9%; font-weight: bold">Total Amount</td>
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
                                                <td style="text-align: left; width: 9%; font-weight: bold; visibility: hidden">Total Amount</td>
                                                <td style="text-align: left; width: 15%; font-weight: bold">
                                                    <span style="text-align: right;"><b>Total Amount :</b></span>
                                                    <asp:Label ID="lblTotal" runat="server" BorderColor="#339933" Font-Bold="True"
                                                        Style="text-align: right" Text="0.00 "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>


                                <div id="Div3" class="row mt-2" runat="server">
                                    <div class="col-md-2">
                                        <label>Party Name</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPartyName" runat="server" TextMode="MultiLine" ToolTip="Please Enter Party Name"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>PAN No</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPanNo" runat="server" ToolTip="Please Enter PAN Number" MaxLength="10" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <div class="row" runat="server" id="divgstno" visible="true">
                                    <div class="col-sm-2">
                                        <label>Gstin No</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtGSTNNO" runat="server" placeholder="GSTIN NUMBER" CssClass="form-control" ToolTip="GSTIN NUMBER" MaxLength="15"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtgstinno" runat="server" TargetControlID="txtGSTNNO" FilterType="UppercaseLetters,Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="col-sm-2">
                                        <label><span style="color: red">*</span> Pay Mode</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <%-- started --%>
                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>
                                                    <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label></label>
                                            </div>

                                        </div>
                                     <%-- ended --%>


                                <br />
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Nature of Service</label>
                                        </div>
                                        <asp:TextBox ID="txtNatureService" runat="server" ToolTip="Enter Nature of Service" TextMode="MultiLine"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12" id="row3" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Narration</label>
                                        </div>
                                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" MaxLength="1000"
                                            ToolTip="Please Enter Narration" CssClass="form-control"></asp:TextBox>
                                    </div>


                                </div>
                                <br />


                                <br />
                                <div class="row" runat="server" id="Div4">
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
                                                Format="dd/MM/yyyy" PopupButtonID="Image4" TargetControlID="txtChequeDt2">
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



                                  <div class="row" runat="server" id="DivInvoicedateno" visible="false">
                                    <div class="col-md-2">
                                        <label>Invoice No./Invoice date</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtinvoiceNo" runat="server" AutoPostBack="false" ToolTip="Please Enter Invoice No."
                                            MaxLength="20" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtinvoicedate" runat="server" ToolTip="Please Enter Invoice Date"
                                                CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtinvoicedate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                OnInvalidCssClass="errordate" TargetControlID="txtinvoicedate" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True">
                                            </ajaxToolKit:MaskedEditExtender>

                                            <asp:HiddenField runat="server" ID="HiddenField1" />

                                            <%--<input id="hdnAskSave" runat="server" type="hidden">--%>
                                            <input id="Hidden1" runat="server" type="hidden" />

                                        </div>
                                    </div>
                                </div>


                                <div class="row mb-3 mt-3" id="Div8" runat="server">
                                    <div class="col-12">
                                        <asp:Panel ID="pnlupload" runat="server">
                                            <div class="sub-heading">
                                                <h5>Upload Bills</h5>
                                            </div>
                                            <div id="div9" style="display: block;">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>     
                                                            <label>Bill Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtBillName" runat="server" ToolTip="Enter Bill Name" CssClass="form-control"
                                                            MaxLength="100" TabIndex="24" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Multiple bill can be attach</label>
                                                        </div>
                                                        <asp:FileUpload ID="FileUploadBill" runat="server" class="multi" TabIndex="24" ToolTip="Click here to browse bills" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddBill" runat="server" Text="Add Bill" OnClick="btnAddBill_Click" CssClass="btn btn-primary"
                                                            ToolTip="Click here to upload bills" TabIndex="24" />

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlNewBills" runat="server" Visible="false">

                                                            <asp:ListView ID="lvNewBills" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1"> 
                                                                        <div class="sub-heading">
                                                                            <h5>Uploaded Bill List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                                            <%--nowrap display--%>
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Delete</th>
                                                                                    <th>Bill Name</th>
                                                                                    <th>File Name</th>
                                                                                    <th>Download</th>
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
                                                                            <asp:ImageButton ID="btnDeleteBill" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                                                AlternateText='<%#Eval("FILEPATH") %> ' ToolTip='<%#Eval("DISPLAYFILENAME") %>' OnClick="btnDeleteBill_Click"
                                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this Bill?')" />
                                                                        </td>
                                                                        <td><%#Eval("DOCUMENTNAME") %></td>
                                                                        <td>
                                                                            <%#Eval("DISPLAYFILENAME") %>                                                                    
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgBill" runat="Server" ImageUrl="~/Images/action_down.png"
                                                                                CommandArgument='<%#Eval("FILEPATH") %> ' ToolTip='<%#Eval("DISPLAYFILENAME") %> ' OnClick="imgBill_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <input id="hdnIdEditParty" runat="server" type="hidden" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="AccMoney1"
                                    OnClick="btnSave_Click" OnClientClick="return ProjectValidation();" />
                                <%-- OnClientClick="return AskSave();"  --%>
                                <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnReset_Click" />
                                <asp:Button ID="btnExporttoExcel" runat="server" Text="Export" OnClick="btnExporttoExcel_Click"
                                    Visible="false" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="AccMoney1" DisplayMode="List" />
                            </div>
                            <div class="col-12">

                                <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />

                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="pnl" TargetControlID="btnForPopUpModel2"
                                    Enabled="True">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnl" runat="server" meta:resourcekey="pnlResource1">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Transaction</h5>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" ValidationGroup="Validation"
                                            CssClass="btn btn-info" OnClick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                                        <asp:Button ID="btnBack" runat="server" Text="Close" ValidationGroup="Validation"
                                            CssClass="btn btn-primary" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
                                        <asp:Button ID="btnchequePrint" runat="server" CssClass="btn btn-primary" Text="Print Cheque" Visible="false" OnClick="btnchequePrint_Click" />
                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvGrp" runat="server">
                                            <LayoutTemplate>
                                                <h4 id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Transaction</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
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


                                </asp:Panel>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <input id="hdnbal2" runat="server" type="hidden" />

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
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                            </div>
                            <div>
                                Selected Budget Head has Less than 0.00 or 0.00 amount left, Are you sure you want to Save data?
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="btnOkDel_Click" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" OnClick="btnNoDel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </asp:Panel>
    <%--  </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddBill" />
            </Triggers>
        </asp:UpdatePanel>--%>
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

            var AmtWithoutTDS = document.getElementById('<%= txtTdsOnAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtTranAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTdsOnAmount.ClientID %>').value = '';
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

    <script language="javascript" type="text/javascript">
        function OnPrevAdv(result) {
            debugger;
            if (result == 1) {
                alert("Can't be Tag This Voucher for Advance which was already completed");
                document.getElementById('ctl00_ContentPlaceHolder1_ddlPrevAdv').value = "0";
                return;
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
    <div id="dvConfirm" title="" runat="server" style="display: none">
        <asp:Label ID="lblconfirm" runat="server" Style="display: none"></asp:Label>
    </div>

</asp:Content>
