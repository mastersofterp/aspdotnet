<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkVouchers.aspx.cs" Inherits="BulkVouchers" Title=""
    UICulture="auto" %>

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
    <%-- <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }

        #scr {
            overflow: auto;
        }
    </style>--%>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <%--<script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

    <script language="javascript" type="text/javascript">
        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, CAPTION, 'Create Sub Links');
            return true;
        }

    </script>

    <%--   <link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />

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
            //  var Amount = document.getElementById('<%%>').value;
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

            //  var Amount = parseFloat(document.getElementById().value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }




    </script>

    <script language="javascript" type="text/javascript">
        function copyamount() {

        }
        //Added by vijay andoju for checkingLedgers
        function CheckLedger() {

        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
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
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK VOUCHER CREATION</h3>
                        </div>

                        <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="panel panel-info">
                                    <%--       <div class="panel-heading">Create Bulk Vouchers</div>--%>
                                    <div class="panel-body">
                                        <div class="col-md-12">

                                            <div class="row">

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
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Against Account Entry</h5>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label><span style="color: red">*</span> Transaction Mode </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlTranType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                        meta:resourcekey="ddlTranTypeResource1" Enabled="false">
                                                        <asp:ListItem Value="P" meta:resourcekey="ListItemResource1" Text="Payment"></asp:ListItem>
                                                        <asp:ListItem Value="R" meta:resourcekey="ListItemResource2" Text="Receipt"></asp:ListItem>
                                                        <asp:ListItem Value="C" meta:resourcekey="ListItemResource3" Text="Contra"></asp:ListItem>
                                                        <asp:ListItem Value="J" meta:resourcekey="ListItemResource4" Text="Journal"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
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
                                                        <asp:TextBox ID="txtDate" runat="server"  CssClass="form-control"
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
                                            <br />
                                            <div class="row" id="Row2" runat="server">
                                                <div class="col-md-2">
                                                    <label><span style="color: red">*</span> Against Account  </label>
                                                </div>
                                                <div class="col-md-6">
                                                    <input id="hdnAgainstPartyId" runat="server" type="hidden" />
                                                    <asp:HiddenField ID="hdnOpartyManual" runat="server" Value="0" />
                                                    <%--<asp:TextBox ID="txtAgainstAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtAgainstAcc" runat="server" AutoPostBack="true" CssClass="form-control"
                                                        ToolTip="Please Enter Ledger Name"
                                                        OnTextChanged="txtAgainstAcc_TextChanged1"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtAgainstAcc"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtAgainstAcc" Display="None" ErrorMessage="Please Select Ledger for Against Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="lblCurbal1" runat="server" BorderColor="White" BorderStyle="None"
                                                        Font-Size="Small" Style="background-color: Transparent; text-align: right; padding-top: 7px"
                                                        TabIndex="-1" ForeColor="Black" Text="0.00" Font-Bold="true" Visible="false"></asp:Label>
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
                                                    <label><span style="color: red">*</span> Bill No. </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="lblCurBal" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                    <asp:TextBox ID="txtmd" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div id="trSponsor" runat="server" class="row">
                                                <br />
                                                <div class="col-md-2">
                                                    <label>Sponsor Project </label>
                                                    <%--<span style="color: red">*</span>--%>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlSponsor" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlSponsor_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvSponsorProject" runat="server"
                                                                ControlToValidate="ddlSponsor" Display="None" ErrorMessage="Please Select Sponsor Project" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney" InitialValue="0">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="col-md-2">
                                                    <label>Project Sub Head  </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlProjSubHead" runat="server" AppendDataBoundItems="true"
                                                        data-select2-enable="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProjSubHead_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    Remaining Amount:
                                                               <asp:Label ID="lblRemainAmt" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <%--<div class="col-md-3">
                                                            <asp:TextBox ID="TextBox1" runat="server" BorderColor="White"
                                                                BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                                Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox2" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                                BorderStyle="None"
                                                                Style="background-color: Transparent;" ReadOnly="True"
                                                                Font-Size="XX-Small"></asp:TextBox>
                                                        </div>--%>
                                            </div>
                                            <%-- <div class="row" runat="server" id="trSubHead">
                                                        <div class="col-md-2">
                                                            <label>Project Sub Head</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlProjSubHead" runat="server" AppendDataBoundItems="true"
                                                                Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlProjSubHead_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            Remaining amount:
                                                               <asp:Label ID="lblRemainAmt" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>--%>

                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Account Entry</h5>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row form-group">
                                                <div class="col-md-2">
                                                    <label><span style="color: red">*</span> Account  </label>
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
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCurBal" runat="server" CssClass="form-control" ToolTip="Current Balance" Visible="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">

                                                    <asp:Label ID="lblCurBal2" runat="server" TabIndex="-2" Font-Bold="true" Style="background-color: Transparent; text-align: left;"></asp:Label>
                                                    <asp:Label ID="lblCrDr2" runat="server" BorderColor="White" BorderStyle="None" Font-Size="XX-Small"
                                                        Height="16px" ReadOnly="True" Style="background-color: Transparent; text-align: left;"
                                                        TabIndex="-6" Width="20%" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblCur2" runat="server" BorderColor="White" BorderStyle="None" Font-Size="XX-Small"
                                                        ReadOnly="True" Style="background-color: Transparent" TabIndex="-2" Width="75px"
                                                        ForeColor="White" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <%--<br />--%>
                                            <div class="row form-group">
                                                <div class="col-md-2">
                                                    <%-- <label>Amount</label>--%>
                                                </div>
                                                <%-- <div class="col-md-3">--%>
                                                <%--<asp:TextBox ID="txtTranAmt" runat="server" Style="text-align: right" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                                                autocomplete="off" AutoPostBack="false" MaxLength="13" onkeyup="copyamount();" ></asp:TextBox>--%>
                                                <%-- <asp:TextBox ID="txtTranAmt" runat="server" Style="text-align: right" ToolTip="Please Enter Transaction Amount" CssClass="form-control"
                                                                autocomplete="off" AutoPostBack="false" MaxLength="13" onkeyup="copyamount();" OnTextChanged="txtTranAmt_TextChanged"></asp:TextBox>--%>
                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtTranAmt"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />--%>
                                                <%--</div>--%>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlcrdr" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlcrdr_SelectedIndexChanged">
                                                        <asp:ListItem>Dr</asp:ListItem>
                                                        <asp:ListItem>Cr</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div id="Div4" class="col-md-4" runat="server" visible="false">
                                                    <asp:TextBox ID="txtChqNo1" runat="server" ToolTip="Please Enter Account Name" CssClass="form-control"
                                                        Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtChequeDt1" runat="server" ToolTip="Please Enter Account Name" CssClass="form-control"
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
                                                    <%-----------------------Added by vijay andoju on 25-08-2020 for GST and IGST APPLICABLE--------------%>
                                                    <%-- <asp:CheckBox ID="chkGST" runat="server" Text="&nbsp;Is GST Applicable" AutoPostBack="true"
                                                                OnCheckedChanged="chkGst_CheckedChanged" />
                                                            <asp:CheckBox ID="chkIGST" runat="server" Text="&nbsp;Is IGST Applicable" AutoPostBack="true"
                                                                OnCheckedChanged="chkIGST_CheckedChanged" />--%>


                                                    <%--------------------------------------------------------------------------------------%>

                                                    <asp:CheckBox ID="chkTDSApplicable" runat="server" Text="&nbsp;Is TDS Applicable" AutoPostBack="true"
                                                        OnCheckedChanged="chkTDSApplicable_CheckedChanged" />
                                                </div>
                                            </div>

                                            <%---------------------------------------------------------------------------------------------------%>


                                            <div id="dvTDS" runat="server" visible="false" class="row form-group">

                                                <div class="col-sm-2">
                                                    <label><span style="color: red">*</span> TDS Account  </label>
                                                </div>
                                                <div class="col-md-3">

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
                                                <div class="col-sm-2">
                                                    <label><span style="color: red">*</span>  TDS Section </label>
                                                </div>
                                                <div class="col-md-3">

                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                        ControlToValidate="ddlSection" Display="None"
                                                        ErrorMessage="Please Select TDS Section" SetFocusOnError="true" InitialValue="0"
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
                                                    <asp:DropDownList ID="ddlCostCenter" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCostCenter" runat="server" ControlToValidate="ddlCostCenter"
                                                        Display="None" ErrorMessage="Please Select Cost Center" SetFocusOnError="True"
                                                        ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mt-3" runat="server" id="divDepartment" visible="True">
                                                <%--<br />--%>
                                                <div class="col-md-2">
                                                    <label>Department  </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddldepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldepartment"
                                                        Display="None" ErrorMessage="Please Select Department" SetFocusOnError="True"
                                                        ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                </div>
                                                <%-- <div class="col-md-1">
                                                            <asp:Label ID="lblDamoun" runat="server" Font-Bold="true" Text="" Font-Size="Small" BorderColor="White" BorderStyle="None"
                                                                Style="background-color: Transparent; text-align: right; padding-top: 7px"></asp:Label>
                                                        </div>--%>
                                                <div class="col-md-2">
                                                    <label>Budget Head</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlBudgetHead" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBudgetHead"
                                                        Display="None" ErrorMessage="Please Select Budget Head" SetFocusOnError="True"
                                                        ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblBudgetBal" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <br />


                                            <div class="row" runat="server" id="trBudgetHead" visible="false">
                                                <%-- <br />
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
                                                        </div>--%>
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
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="btnDelete_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="X" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                                        Visible="false" />
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTranAmt"
                                                                Display="None" ErrorMessage="Please Enter Transaction Amount" SetFocusOnError="True"
                                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cmpmoney" runat="server" ControlToValidate="txtTranAmt"
                                                                Display="None" ErrorMessage="Please Enter Numeric Values Only" SetFocusOnError="True"
                                                                Type="Double" ValidationGroup="AccMoney"></asp:CompareValidator>--%>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" id="rowgrid">
                                                <div class="col-12">
                                                    <input id="hdnAgParty" runat="server" type="hidden" />
                                                    <input id="hdnOparty" runat="server" type="hidden" />
                                                </div>
                                                <div class="col-12 mt-3">
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
                                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" Visible="false" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HtmlEncode="False" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                                        <ItemStyle Wrap="True" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" Visible="false" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Debit" HeaderText="Debit" Visible="false" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Credit" HeaderText="Credit" Visible="false" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
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
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="#000" />
                                                                <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="#000" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>

                                                        </div>
                                                    </asp:Panel>
                                                    <table class="FixHead FixHead-hover FixHead-striped" style="width: 100%" visible="false">
                                                        <tr id="Rowdrcr" runat="server" visible="false">
                                                            <td style="text-align: center; width: 1%; font-weight: bold">&nbsp;</td>
                                                            <td style="text-align: left; width: 10%; font-weight: bold">&nbsp;</td>
                                                            <td style="text-align: left; width: 15%; font-weight: bold"></td>
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
                                                            <td style="text-align: left; width: 15%; font-weight: bold; visibility: hidden"></td>
                                                            <td style="text-align: left; width: 15%; font-weight: bold">
                                                                <span visible="false" style="text-align: right;"><b></b></span>
                                                                <asp:Label ID="lblTotal" runat="server" Visible="false" BorderColor="#339933" Font-Bold="True"
                                                                    Style="text-align: right" Text="0.00 "></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </div>
                                            </div>
                                            <br />

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlupload" runat="server">
                                                        <div class="row" id="Div5" runat="server">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Upload Bills</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12" id="div6" style="display: block;">
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
                                                                            ToolTip="Click here to upload bills" TabIndex="24" ValidationGroup="upddocs" />

                                                                    </div>

                                                                </div>


                                                                <asp:Panel ID="pnlNewBills" runat="server" Visible="false">

                                                                    <asp:ListView ID="lvNewBills" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div id="lgv1">
                                                                                <div class="sub-heading">
                                                                                    <h5>Uploaded Bill List</h5>
                                                                                </div>
                                                                                <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Delete</th>
                                                                                            <th>Bill Name</th>
                                                                                            <th>File Name</th>
                                                                                            <th id="thdownload" runat="server" visible="false">Download</th>
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
                                                                                <td id="tddownload" runat="server" visible="false">
                                                                                    <asp:ImageButton ID="imgBill" runat="Server" ImageUrl="~/Images/action_down.png"
                                                                                        CommandArgument='<%#Eval("FILEPATH") %> ' ToolTip='<%#Eval("DISPLAYFILENAME") %> ' OnClick="imgBill_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </asp:Panel>

                                                            </div>
                                                        </div>

                                                    </asp:Panel>

                                                </ContentTemplate>

                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnAddBill" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <div class="row  mt-3">
                                                <div class="col-md-2">
                                                    <label>Tag User</label>
                                                </div>

                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlEmpType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" >Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Employee</asp:ListItem>
                                                        <asp:ListItem Value="2">Payee</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-2" id="divEmployee1" runat="server" visible="false">
                                                    <label><span style="color: red">*</span> Select Employee</label>
                                                </div>
                                                <div class="col-md-3" id="divEmployee2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>


                                            <div class="row mt-2">

                                                <div class="col-md-2" id="divPayeeNature1" runat="server" visible="false">
                                                    <label><span style="color: #FF0000; font-weight: bold">*</span>Payee Nature</label>
                                                </div>
                                                <div class="col-md-3" id="divPayeeNature2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlPayeeNature" ToolTip="Please Select Payee Nature" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true" runat="server"
                                                        OnSelectedIndexChanged="ddlPayeeNature_SelectedIndexChanged"
                                                        AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="ddlPayeeNature" Display="None"
                                                        ErrorMessage="Please Select Nature" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2" id="divPayee1" runat="server" visible="false">
                                                    <label><span style="color: #FF0000; font-weight: bold">*</span>Payee Name</label>
                                                </div>
                                                <div class="col-md-3" id="divPayee2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlName" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                                        ControlToValidate="ddlName" Display="None" InitialValue="0"
                                                        ErrorMessage="Please Select Name" SetFocusOnError="True"
                                                        ValidationGroup="Vendor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="lblAmount" runat="server" Font-Bold="true" Text="0.00" Visible="false"></asp:Label>
                                                </div>

                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-4">

                                                    <asp:Button ID="btnAddvendor" runat="server" CssClass="btn btn-primary" OnClick="btnAddvendor_Click" Text="Add"
                                                        ValidationGroup="Vendor" />
                                                    <asp:ValidationSummary ID="vsVendor" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Vendor" DisplayMode="List" />
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row" runat="server" id="Div9" hidden>

                                                <%--<div class="col-md-4">
                                                            <label>Name</label>
                                                            <asp:DropDownList ID="ddlName" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control">
                                                                <asp:ListItem Selected="True" Text="--Please Select--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>--%>

                                                <div class="col-md-3">
                                                    <label>Remark</label>
                                                    <asp:TextBox ID="txtRemark" runat="server" AutoPostBack="false" ToolTip="Please Enter Remark "
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Cheque No.</label>
                                                    <asp:TextBox ID="txtChqNo2" runat="server" AutoPostBack="false" ToolTip="Please Enter Account Name"
                                                        CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Date</label>
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


                                                        <input id="hdnVchId" runat="server" type="hidden" />

                                                    </div>
                                                </div>
                                                <%--<div class="col-md-1">
                                                            <br />
                                                            <asp:Button ID="btnAddvendor" runat="server" CssClass="btn btn-primary" OnClick="btnAddvendor_Click" Text="Add" />
                                                        </div>--%>
                                            </div>




                                            <div runat="server" id="Div7">
                                                <asp:UpdatePanel ID="SCPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdnFieldsCount" runat="server" />

                                                        <asp:HiddenField ID="hdnLedgerMode" runat="server" />
                                                        <asp:HiddenField ID="hdnLedgerMode1" runat="server" />
                                                        <asp:HiddenField ID="hdnLedgerMode2" runat="server" />

                                                        <asp:HiddenField ID="hdnlblLedgerAmount" runat="server" />
                                                        <asp:HiddenField ID="hdnlblLedgerAmount1" runat="server" />
                                                        <asp:HiddenField ID="hdnlblLedgerAmount2" runat="server" />

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvFields" runat="server" OnItemDataBound="lvFields_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                        <table  style="width: 100%" runat="server" id="lvfieldsTbl">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Action   </th>
                                                                                    <th>Name  </th>
                                                                                    <%--<th>Remark  </th>
                                                                                    <th>Cheque No</th>
                                                                                    <th>Cheque Date</th>--%>
                                                                                    <th id="thLedgerAmount" runat="server">
                                                                                        <asp:PlaceHolder runat="server" ID="lblLedgerAmount" />
                                                                                        <%-- <asp:TextBox runat="server" ID="txtlblLedgerAmount"></asp:TextBox>--%>
                                                                                        <%--<asp:Label ID ="lblLedgerAmount" runat="server" Text="Amount"></asp:Label> --%> </th>

                                                                                    <th id="thLedgerAmount1" runat="server">
                                                                                        <asp:PlaceHolder runat="server" ID="lblLedgerAmount1" />
                                                                                        <%--<asp:Label ID ="lblLedgerAmount1" runat="server" Text="Amount1"></asp:Label>  --%></th>

                                                                                    <th id="thLedgerAmount2" runat="server">
                                                                                        <asp:PlaceHolder runat="server" ID="lblLedgerAmount2" />
                                                                                        <%--<asp:Label ID ="lblLedgerAmount2" runat="server" Text="Amount2"></asp:Label> --%> </th>

                                                                                </tr>
                                                                            </thead>
                                                                            <%--</thead>--%>
                                                                            <tbody>
                                                                            </tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                                OnClick="btnEdit_Click" ImageUrl="~/Images/delete.png" ToolTip="Edit Record"
                                                                                OnClientClick="showConfirmDel1(this); return false;" />
                                                                            <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("SRNO") %>' />
                                                                        </td>
                                                                        <%--onblur="EditTotalAmount()"--%>
                                                                        <td>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("PARTYNAME") %>' />
                                                                            <asp:HiddenField ID="hdnFieldsCount" runat="server" Value='<%# Eval("EmpId") %>' />

                                                                        </td>
                                                                        <%--<td>
                                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>' />

                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lblChequeNo" runat="server" Text='<%# Eval("ChequeNo") %>' />

                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblChequeDate" runat="server" Text='<%# Eval("ChequeDate") %>' />

                                                                            </td>--%>
                                                                        <td>
                                                                            <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" Text='<%# (Eval("AMOUNT1") == null ?"0": Eval("AMOUNT1"))%>'
                                                                                onblur="CalculateTotalAmount();" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtamount1" runat="server" CssClass="form-control" Text='<%# (Eval("AMOUNT2") == null ? "0" : Eval("AMOUNT2"))%>'
                                                                                onblur="CalculateTotalAmount1();" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtamount2" runat="server" CssClass="form-control" Text='<%# (Eval("AMOUNT3")==null ? "0" : Eval("AMOUNT3"))%>'
                                                                                onblur="CalculateTotalAmount2();" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                                        </td>

                                                                    </tr>

                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                            <div id="OsCenterTable" runat="server" visible="false">
                                                                <table id="tblamt" class="table table-bordered table-hover">

                                                                    <td>
                                                                        <asp:Label ID="lbltotals" runat="server" Style="font-weight: bold;" Text="Total :"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lbltotalamount" runat="server" Style="font-weight: bold;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lbltotalamount1" runat="server" Style="font-weight: bold;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lbltotalamount2" runat="server" Style="font-weight: bold;" Enabled="false"></asp:TextBox>
                                                                    </td>

                                                                </table>
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </div>
                                            <div class="row" runat="server" id="divTotalAmt" visible="false">
                                                <div class="col-sm-6">
                                                </div>
                                                <div class="col-sm-3">
                                                    <label>Total Amount :</label>
                                                </div>
                                                <div class="col-md-3">

                                                    <asp:TextBox ID="txtCalculateAmt" runat="server" onkeypress="disablekeys();" onblur="sum();"
                                                        Style="font-weight: bold;"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnCalculateAmt" runat="server" Value="0" />

                                                </div>

                                            </div>
                                            <br />



                                            <div class="row" runat="server" id="Div8" visible="false">
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
                                            <div class="row" runat="server" id="divgstno" visible="false">
                                                <div class="col-sm-2">
                                                    <label>Gstin No:</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtGSTNNO" runat="server" placeholder="GSTIN NUMBER" CssClass="form-control" ToolTip="GSTIN NUMBER" MaxLength="15"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="txtgstinno" runat="server" TargetControlID="txtGSTNNO" FilterType="UppercaseLetters,Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                            </div>



                                            <div class="row">
                                                <div class="form-group col-lg-12 col-md-6 col-12" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Nature of Service</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNatureService" runat="server" ToolTip="Enter Nature of Service" TextMode="MultiLine"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-10 col-md-6 col-12" id="row3" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Narration</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" MaxLength="390"
                                                        ToolTip="Please Enter Narration" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>





                                            <br />
                                            <div class="row" id="trPaymentMode" runat="server" visible="true">
                                                <div class="col-sm-2">
                                                    <label><span style="color: red">*</span>Payment Mode  </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                                <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                                <asp:ListItem Value="R">RTGS</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvMode" runat="server"
                                                                ControlToValidate="ddlPaymentMode" Display="None" ErrorMessage="Please Select Payment Mode" 
                                                                SetFocusOnError="true" InitialValue="0"
                                                                ValidationGroup="AccMoney1">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <input id="hdnbal2" runat="server" type="hidden" />

                                    <br />
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-5">
                                            <input id="hdnIdEditParty" runat="server" type="hidden" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="AccMoney1"
                                                OnClick="btnSave_Click" OnClientClick="return ProjectValidation();" />
                                            <%-- OnClientClick="return AskSave();"  --%>
                                            <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnReset_Click" />
                                            <asp:Button ID="btnVerify" runat="server" Text="Verify" CssClass="btn btn-primary" OnClick="btnVerify_Click"
                                                Visible="false" />
                                            <asp:Button ID="btnReturn" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnReturn_Click"
                                                Visible="false" />
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
                                                    <div class="sub-heading">
                                                      
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" ValidationGroup="Validation"
                                                            CssClass="btn btn-info" OnClick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                                                        <asp:Button ID="btnBack" runat="server" Text="Close" ValidationGroup="Validation"
                                                            CssClass="btn btn-danger" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
                                                        <asp:Button ID="btnchequePrint" runat="server" CssClass="btn btn-primary" Text="Print Cheque" Visible="false" OnClick="btnchequePrint_Click" />
                                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                                        <asp:ListView ID="lvGrp" runat="server">
                                                            <LayoutTemplate>
                                                                <h4 id="demo-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Transaction</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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
                                                </div>
                                            </asp:Panel>
                                        </div>
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
                                                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                                        </div>
                                                        <div>
                                                            Selected Budget Head has Less than 0.00 or 0.00 amount left, Are you sure you want to Save data?
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="text-center">
                                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="btnOkDel_Click" />
                                                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" OnClick="btnNoDel_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <%-- <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mdlPopupDel1"
        runat="server" TargetControlID="div" PopupControlID="divdel" OkControlID="btnOkDel1"
        OnOkScript="okDel1Click();" CancelControlID="btnNoDel1" OnCancelScript="cancelDel1Click();"
        BackgroundCssClass="modalBackground" />--%>
                                    <%--<asp:Panel ID="divdel1" runat="server" Style="display: none" CssClass="modalPopup" Height="100px">--%>
                                    <%--<div class="box-footer">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-4">
                                <asp:Panel ID="divdel" runat="server" Style="display: none" CssClass="modalPopup" Height="150px">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/warning.gif" />
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel1" runat="server" Text="Yes" CssClass="btn-primary" />
                                                    <asp:Button ID="btnNoDel1" runat="server" Text="No" CssClass="btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            
               
           <%--    </asp:Panel>        --%>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        function disablekeys() {
            return false;
        }
    </script>


    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;
        //function showConfirmDel1(source) {
        //    this._source = source;
        //    this._popup = $find('mdlPopupDel1');

        //    //  find the confirm ModalPopup and show it    
        //    this._popup.show();
        //}
        //function okDel1Click() {
        //    //  find the confirm ModalPopup and hide it    
        //    this._popup.hide();
        //    //  use the cached button as the postback source
        //    __doPostBack(this._source.name, '');
        //}

        //function cancelDel1Click() {
        //    //  find the confirm ModalPopup and hide it 
        //    this._popup.hide();
        //    //  clear the event source
        //    this._source = null;
        //    this._popup = null;
        //}

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


    </script>

    <script language="javascript" type="text/javascript">
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function CalculateTotalAmount() {
            try {
              
                var totalAmt = 0.00;

                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl') != null)
                  
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl').getElementsByTagName('tr');
               // alert(dataRows);
                if (dataRows != null) {

                    //alert(dataRows);

                    for (i = 1; i < dataRows.length; i++) {

                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        //alert(dataCellCollection);

                        //var dataCell = dataCellCollection.item(5);
                        var dataCell = dataCellCollection.item(2);
                        // if( dataCellCollection.item(i)=='5')
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;


                        if (txtAmt == '')
                            txtAmt = 0;

                        if (document.getElementById('<%=hdnFieldsCount.ClientID%>').value > 0)
                            totalAmt += parseFloat(txtAmt);

                    }

                    //alert(totalAmt);
                    //alert(document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount'));

                    if (document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount').value = totalAmt;

                }
                sum();
            }
            catch (e) {
                alert("Error: " + e.description);
            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        function CalculateTotalAmount1() {
            try {
               
                var totalAmt1 = 0.00;

                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl').getElementsByTagName('tr');

                if (dataRows != null) {

                    //alert(dataRows);

                    for (i = 1; i < dataRows.length; i++) {

                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        //alert(dataCellCollection);

                        //var dataCell = dataCellCollection.item(6);
                        var dataCell = dataCellCollection.item(3);

                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;

                        if (txtAmt == '')
                            txtAmt = 0;

                        if (document.getElementById('<%=hdnFieldsCount.ClientID%>').value > 0)
                            totalAmt1 += parseFloat(txtAmt);

                    }

                    //alert(totalAmt);
                    //alert(document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount'));

                    if (document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount1') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount1').value = totalAmt1;

                }
                sum();
            }
            catch (e) {
                alert("Error: " + e.description);
            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        function CalculateTotalAmount2() {
            try {
               
                var totalAmt2 = 0.00;
               
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFields_lvfieldsTbl').getElementsByTagName('tr');
               
                if (dataRows != null) {

                    alert(dataRows);

                    for (i = 1; i < dataRows.length; i++) {

                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        //alert(dataCellCollection);

                        //var dataCell = dataCellCollection.item(7);
                        var dataCell = dataCellCollection.item(4);

                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;



                        if (txtAmt == '')
                            txtAmt = 0;

                        if (document.getElementById('<%=hdnFieldsCount.ClientID%>').value > 0)
                            totalAmt2 += parseFloat(txtAmt);

                    }

                    //alert(totalAmt);
                    //alert(document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount'));

                    if (document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount2') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount2').value = totalAmt2;

                }
                sum();
            }
            catch (e) {
                alert("Error: " + e.description);
            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        function sum() {
            try {
              
                var hdnlblLedgerAmount = document.getElementById('<%=hdnlblLedgerAmount.ClientID%>').value;
                var hdnlblLedgerAmount1 = document.getElementById('<%=hdnlblLedgerAmount1.ClientID%>').value;
                var hdnlblLedgerAmount2 = document.getElementById('<%=hdnlblLedgerAmount2.ClientID%>').value;

                //alert(hdnlblLedgerAmount);
                //alert(hdnlblLedgerAmount1);
                //alert(hdnlblLedgerAmount2);

                var txtFirstNumberValue = document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount').value;

                var totalAmt = 0;
                // debugger;
                if (txtFirstNumberValue > 0) {
                    totalAmt = parseInt(txtFirstNumberValue);
                }
                if (hdnlblLedgerAmount1 > 0) {

                    var txtSecondNumberValue = document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount1').value;

                    if (txtSecondNumberValue > 0) {

                        var Mode1 = document.getElementById('<%=hdnLedgerMode1.ClientID%>').value;

                        if (Mode1 == "Cr") {
                            totalAmt = parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue);
                        }
                        else if (Mode1 == "Dr") {
                            totalAmt = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue);
                        }
                    }
                }
                if (hdnlblLedgerAmount2 > 0) {

                    var txtThirdNumberValue = document.getElementById('ctl00_ContentPlaceHolder1_lbltotalamount2').value;

                    if (txtThirdNumberValue > 0) {

                        var Mode2 = document.getElementById('<%=hdnLedgerMode2.ClientID%>').value;

                        if (Mode2 == "Cr") {
                            totalAmt = totalAmt - parseInt(txtThirdNumberValue);
                        }
                        else if (Mode2 == "Dr") {
                            totalAmt = totalAmt + parseInt(txtThirdNumberValue);
                        }
                    }
                }
                document.getElementById('ctl00_ContentPlaceHolder1_txtCalculateAmt').value = totalAmt;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnCalculateAmt').value = totalAmt;

                //   var resultCr = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue);
                //   var resultDb = parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue);

                //   var result1Cr = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue) + parseInt(txtThiredNumberValue);
                //   var result1Db =parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue) - parseInt(txtThiredNumberValue);
                //if (Mode1 == "Cr")              
                //{
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtCalculateAmt').value = resultCr;
                //}
                //else if (Mode1 == "Dr") {
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtCalculateAmt').value = resultDb;
                //}
                //if (Mode2 == "Cr") {
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtCalculateAmt').value = result1Cr;
                //}
                //if (Mode2 == "Dr") {
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtCalculateAmt').value = result1Db;
                //}

            }

            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function EditTotalAmount() {
            try {
                CalculateTotalAmount();
                CalculateTotalAmount1();
                CalculateTotalAmount2();
            }
            catch (e) {
                alert("Error: " + e.description);
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
