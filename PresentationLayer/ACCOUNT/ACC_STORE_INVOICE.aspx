<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ACC_STORE_INVOICE.aspx.cs" Inherits="ACCOUNT_ACC_STORE_INVOICE" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>


    <style>
        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>
    <script lang="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript" lang="javascript">
        function clientShowing(source, args) {
            debugger;
            source._popupBehavior._element.style.zIndex = 10000;
        }
    </script>

    <script type="text/javascript" lang="javascript">
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

    </script>
    <script type="text/javascript" lang="javascript">
        function TaxAmountCal() {
            var GST = document.getElementById('<%=chkGST.ClientID%>').checked;


            document.getElementById("<%=txtItemAmount.ClientID%>").value;
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartyAmount').value;
            if (GST == false) {
                document.getElementById("<%=txtItemAmount.ClientID%>").value = document.getElementById("<%=hdnTotalAmount.ClientID%>").value

            }

        }
        function TaxAmountCalIGST() {
            var IGST = document.getElementById('<%=chkIGST.ClientID%>').checked;
            document.getElementById("<%=txtItemAmount.ClientID%>").value;
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartyAmount').value;
            if (IGST == false) {
                document.getElementById("<%=txtItemAmount.ClientID%>").value = document.getElementById("<%=hdnTotalAmount.ClientID%>").value

            }

        }
        function TaxAmountCalTDS() {
            var TDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            if (TDS == false) {
                document.getElementById("<%=txtPartyAmount.ClientID%>").value = document.getElementById("<%=hdnVendorAmount.ClientID%>").value
            }
        }

    </script>



    <script type="text/javascript" lang="javascript">
        function GetVendorName(Val) {

            var string = "" + Val.id + "";
            myarray = string.split("_");
            var index = myarray[3];
            var Value1 = 'ctl00_ContentPlaceHolder1_GridInvoice_' + index + '_lblVendorName';

            var Namestring = document.getElementById(Value1).innerText;
            //alert(Namestring);

            //alert('te');
        }
    </script>
    <script type="text/javascript" lang="javascript">
        function heckTax(Val) {
            debugger;
            var Result = true;
            var RowCount = document.getElementById("<%=hdnTaxRowCount.ClientID %>").value;
            var GST = document.getElementById('<%=chkGST.ClientID%>').checked;
            var IGST = document.getElementById('<%=chkIGST.ClientID%>').checked;
            var TDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            for (i = 0; i < RowCount; i++) {
                var str = document.getElementById('ctl00_ContentPlaceHolder1_lvTaxList_ctrl' + i + '_lblTaxHead').innerText;
                if (GST) {

                    Result = str.includes('SGST');
                    Result = str.includes('CGST');
                }
                if (IGST) {

                    Result = str.includes('IGST');
                }
                if (TDS) {

                    Result = str.includes('TDS');
                }


            }

            return Result;
        }
    </script>
    <script type="text/javascript" lang="javascript">
        function calcgstamount() {
            var amount = parseFloat(document.getElementById("<%=hdnTotalAmount.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtCGSTPER.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtCGSTPER.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtCGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat(parseFloat(amount) - parseFloat(document.getElementById("<%=txtCGSTAMT.ClientID%>").value) - parseFloat(document.getElementById("<%=txtSGSTAMT.ClientID%>").value)).toFixed(2);


        }
        function calsgstamount() {
            var amount = parseFloat(document.getElementById("<%=hdnTotalAmount.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtSGTSPer.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtSGTSPer.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtSGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat(parseFloat(amount) - parseFloat(document.getElementById("<%=txtSGSTAMT.ClientID%>").value)).toFixed(2);

        }
        function calIgstamount() {
            debugger;
            var amount = parseFloat(document.getElementById("<%=hdnTotalAmount.ClientID%>").value).toFixed(2);
            var per = parseFloat(document.getElementById("<%=txtIGSTPER.ClientID%>").value).toFixed(2);
            if (per > 99) {
                alert('The Percentage Should be Less than 100');
                document.getElementById("<%=txtIGSTPER.ClientID%>").value = '';
            }
            var amountper = parseFloat(parseFloat(amount) / 100).toFixed(2);
            document.getElementById("<%=txtIGSTAMT.ClientID%>").value = parseFloat(amountper * per).toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat(parseFloat(amount) - parseFloat(document.getElementById("<%=txtIGSTAMT.ClientID%>").value)).toFixed(2);
        }

        function calTdsamount() {
            var amount = parseFloat(document.getElementById("<%=hdnVendorAmount.ClientID%>").value).toFixed(2);
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
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartyAmount').value = parseFloat(parseFloat(parseFloat(amount) - document.getElementById("<%=txtTDSAmount.ClientID%>").value)).toFixed(2);

        }
    </script>

    <script language="javascript" type="text/javascript">
        function copyamount() {
            var GST = document.getElementById('<%=chkGST.ClientID%>').checked;
            var IGST = document.getElementById('<%=chkIGST.ClientID%>').checked;
            var TDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;

            var Amount = parseFloat(document.getElementById('<%=txtTamount.ClientID%>').value);


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
            //document.getElementById('<%=txtTamount.ClientID%>').focus();
        }
        //Added by vijay andoju for checkingLedgers

    </script>
    <script language="javascript" type="text/javascript">
        function TaxAmount() {
          
            var RowCount = document.getElementById('<%=hdnTaxRowCount.ClientID%>').value;
            var TotalAmount = document.getElementById('<%=hdnVendorAmount.ClientID%>').value;
            var DrAmount = 0, CrAmount = 0;
            var DrCr;
            for (i = 0; i < RowCount; i++) {
                DrCr = document.getElementById('ctl00_ContentPlaceHolder1_lvTaxList_ctrl' + i + '_lblMode').innerText;
                if (DrCr == 'Dr') {
                    DrAmount = parseFloat(DrAmount) + parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvTaxList_ctrl' + i + '_txtAmount').value);
                }
                if (DrCr == 'Cr') {
                    CrAmount = parseFloat(CrAmount) + parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvTaxList_ctrl' + i + '_txtAmount').value);
                }
            }
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartyAmount').value = parseFloat(parseFloat(TotalAmount) - parseFloat(CrAmount));
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat(parseFloat(TotalAmount) - parseFloat(DrAmount));
        }
    </script>
    <script lang="ja" type="text/javascript">
        function CopyPartyName() {
            document.getElementById('<%=txtPartyName.ClientID%>').value= document.getElementById('<%=txtCashBankLedger.ClientID%>').value.split('*')[0];
        }
        function CheckAmount() {
            var amount = parseFloat(document.getElementById("<%=hdnTotalAmount.ClientID%>").value).toFixed(2);
          
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat( parseFloat(parseFloat(amount) - parseFloat( parseFloat(document.getElementById("<%=txtSGSTAMT.ClientID%>").value)).toFixed(2)- parseFloat(document.getElementById("<%=txtCGSTAMT.ClientID%>").value)).toFixed(2));

        }
        function CheckIgstAmount() {
            var amount = parseFloat(document.getElementById("<%=hdnTotalAmount.ClientID%>").value).toFixed(2);
          
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemAmount').value = parseFloat(parseFloat(amount) - parseFloat(document.getElementById("<%=txtIGSTAMT.ClientID%>").value)).toFixed(2);
        
        }
        function CheckTdsAmount() {
            var amount = parseFloat(document.getElementById("<%=hdnVendorAmount.ClientID%>").value).toFixed(2);
          
            document.getElementById('ctl00_ContentPlaceHolder1_txtPartyAmount').value = parseFloat(parseFloat(parseFloat(amount) - document.getElementById("<%=txtTDSAmount.ClientID%>").value)).toFixed(2);

        }
    </script>



    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upStoreInvoice"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="upStoreInvoice" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STORE INVOICE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" class="account_compname" style="font-size: x-large; text-align: center">
                                </div>
                                <div>Note:<span style="color: red">* Marked is mandatory !</span></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Store Invoice</div>
                                        <div class="panel-body">
                                            <div class="form-group col-sm-12" runat="server" visible="false">
                                                <div class="col-sm-2">
                                                    <label>Invoice No<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvInvoice" runat="server" ControlToValidate="ddlInvoice" InitialValue="0" ErrorMessage="Please Select Invoice" ValidationGroup="Invoice" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" ValidationGroup="Invoice" />
                                                    <asp:ValidationSummary ID="vsInvoice" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Invoice" />

                                                </div>
                                            </div>
                                            <div class="form-group col-sm-12 ">



                                                <asp:Repeater ID="GridInvoice" runat="server" OnItemCommand="GridInvoice_ItemCommand">
                                                    <HeaderTemplate>
                                                        <h4 class="box-title">Invoice Details</h4>
                                                        <table id="table2" class="table table-bordered table-striped dt-responsive nowrap">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Action
                                                                    </th>
                                                                    <th>Invoice No
                                                                    </th>
                                                                    <th>Vendor Name
                                                                    </th>
                                                                    <th>Purchase Order 
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%--       <asp:ImageButton ID="btnSelect" runat="server" 
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" CommandName='<%# Eval("INVTRNO") %>' OnClick="btnSelect_Click" TabIndex="9" />--%>

                                                                <asp:LinkButton ID="lnkselect" runat="server" Text="Select" OnClick="lnkselect_Click" OnClientClick="GetVendorName(this)" ToolTip='<%# Eval("Amount","{0:0.00}")%>' CommandName='<%# Eval("INVTRNO") %>' CommandArgument='<%#Eval("BHALNO")%>'></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblInvoice" runat="server" Text='<%# Eval("INVNO")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdndept" runat="server"  Value='<%# Eval("SDNO") %>'   />
                                                                <asp:HiddenField ID="hdnPno" runat="server" Value='<%# Eval("PNO") %>' />                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VENDOR NAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPurchaseOrder" runat="server" Text='<%# Eval("REFNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0.00}")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody></table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlVoucher" runat="server" Visible="false">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Store Invoice Voucher</div>
                                        <div class="panel-body">
                                            <div class="form-group row ">
                                                <div class="col-sm-2">
                                                    <label>Transaction Mode <span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblPaymentType" runat="server" Text="Journal" CssClass="form-control"></asp:Label>
                                                    <asp:DropDownList ID="ddlPaymenttype" runat="server" Enabled="false" CssClass="form-control" Visible="false">
                                                        <asp:ListItem Value="J">Journal</asp:ListItem>
                                                        <asp:ListItem Value="Dr">Payment</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-7">
                                                    <label>Vendor Name :</label><asp:Label ID="lblPname" runat="server" Style="font-weight: bold" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hdnPname" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-2">
                                                    <label>Vendor Ledger<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtCashBankLedger" runat="server" CssClass="form-control" AutoPostBack="false" onblur="CopyPartyName()" OnTextChanged="txtCashBankLedger_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtCashBankLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <%--<asp:RegularExpressionValidator ID="revvendor" runat="server" ControlToValidate="txtCashBankLedger" Display="None"  ErrorMessage="Please Enter Vendor Ledger" ValidationGroup="AccMoney"></asp:RegularExpressionValidator>--%>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtPartyAmount" runat="server" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnTotalAmount" runat="server" />
                                                    <asp:HiddenField ID="hdnVendorAmount" runat="server" />

                                                </div>
                                                <div class="col-sm-2">
                                                    <label style="font-weight: bold">Cr</label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-2">
                                                    <label>Account Ledger<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtAccountLedger" runat="server" CssClass="form-control" AutoPostBac="true" OnTextChanged="txtAccountLedger_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtAccountLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAccountLedger" Display="None" ErrorMessage="Please Enter Account Ledger" ValidationGroup="AccMoney"></asp:RegularExpressionValidator>--%>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtItemAmount" runat="server" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label style="font-weight: bold">Dr</label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-2">
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:CheckBox ID="chkGST" runat="server" Text="&nbsp;Is GST Applicable" AutoPostBack="true"
                                                        OnCheckedChanged="chkGST_CheckedChanged" onclick="TaxAmountCal()" />
                                                    <asp:CheckBox ID="chkIGST" runat="server" Text="&nbsp;&nbsp;Is IGST Applicable" AutoPostBack="true"
                                                        OnCheckedChanged="chkIGST_CheckedChanged" onclick="TaxAmountCalIGST()" />
                                                    <asp:CheckBox ID="chkTDSApplicable" runat="server" Text="&nbsp;&nbsp;Is TDS Applicable" AutoPostBack="true"
                                                        OnCheckedChanged="chkTDSApplicable_CheckedChanged" onclick="TaxAmountCalTDS()" />
                                                    <asp:HiddenField ID="hdnTaxRowCount" runat="server" />
                                                </div>
                                            </div>
                                            <div id="divgst" runat="server" visible="false" class="row">
                                                <br />


                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-3">
                                                    <label>SGST Account :</label>
                                                    <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" ToolTip="Please EnterSGST Ledger" OnTextChanged="txtSGST_TextChanged" onblur="CheckLedger()"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtSGST"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                                        ControlToValidate="txtSGST" Display="None"
                                                        ErrorMessage="Please Enter SGST Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2" runat="server" visible="false">
                                                    <label>SGST On Amount :</label>
                                                    <asp:TextBox ID="txtSGSTAMOUNT" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onblur="CheckAmount(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="txtSGSTAMOUNT" Display="None"
                                                        ErrorMessage="Please Enter SGST On Amount" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSGSTAMOUNT" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div id="Div6" class="col-md-2" runat="server" visible="false">
                                                    <label>Section :</label>
                                                    <asp:DropDownList ID="ddlSection1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlSection1_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <label>Per (%) :</label>
                                                    <asp:TextBox ID="txtSGTSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="5" AutoPostBack="false"
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
                                                  <div class="col-md-1">
                                                    <%--<label>Mode</label>--%>
                                                    <asp:Label ID="Label2" runat="server" Text="Dr" Style="font-weight:bold"></asp:Label>
                                                </div>
                                            </div>

                                            <div id="divcgst" runat="server" visible="false" class="row">
                                                <br />
                                                <div class="col-md-2">
                                                    <label>&nbsp;</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>CGST Account :</label>
                                                    <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Ledger" OnTextChanged="txtCGST_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtCGST"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                        ControlToValidate="txtCGST" Display="None"
                                                        ErrorMessage="Please Enter CGST Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-md-2" runat="server" visible="false">
                                                    <label>CGST On Amount :</label>
                                                    <asp:TextBox ID="txtCGSTAMOUNT1" runat="server" CssClass="form-control" ToolTip="Please Enter CGST Amount" Style="text-align: right" onkeyup="CheckAmount(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="txtCGSTAMOUNT1" Display="None"
                                                        ErrorMessage="Please Enter CGST On Amount" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCGSTAMOUNT1" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div id="Div3" class="col-md-2" runat="server" visible="false">
                                                    <label>Section :</label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" onblur="CalPerAmountforTDS(this);">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <label>Per (%) :</label>
                                                    <asp:TextBox ID="txtCGSTPER" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="5"
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
                                                  <div class="col-md-1">
                                                    <%--<label>Mode</label>--%>
                                                    <asp:Label ID="Label1" runat="server" Text="Dr" Style="font-weight:bold"></asp:Label>
                                                </div>
                                            </div>

                                            <div id="divIgst" runat="server" visible="false" class="row">
                                                <br />

                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-3">
                                                    <label>IGST Account :</label>
                                                    <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" ToolTip="Please Enter IGST Ledger" OnTextChanged="txtIGST_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" TargetControlID="txtIGST"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                        ControlToValidate="txtIGST" Display="None"
                                                        ErrorMessage="Please Enter IGST Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2" runat="server" visible="false">
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
                                                    <asp:TextBox ID="txtIGSTPER" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="5"
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
                                                    <asp:TextBox ID="txtIGSTAMT" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right" onblur="CheckIgstAmount()"
                                                        ToolTip="Can be Edited"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                        ControlToValidate="txtIGSTAMT" Display="None"
                                                        ErrorMessage="Please Enter IGST Amount" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtIGSTAMT"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                   
                                                </div>
                                                <div class="col-md-1">
                                                    <%--<label>Mode</label>--%>
                                                    <asp:Label ID="lblIgstMode" runat="server" Text="Dr" Style="font-weight:bold"></asp:Label>
                                                </div>
                                                   
                                            
                                            </div>

                                            <div id="dvTDS" runat="server" visible="false" class="row">
                                                <br />

                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-3">
                                                    <label>TDS Account :</label>
                                                    <asp:TextBox ID="txtTDSLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger" OnTextChanged="txtTDSLedger_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" TargetControlID="txtTDSLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                        ControlToValidate="txtTDSLedger" Display="None"
                                                        ErrorMessage="Please Select TDS Account" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2" runat="server" visible="false">
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
                                                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                        ControlToValidate="ddlSection" Display="None"
                                                        ErrorMessage="Please Select TDS Section" SetFocusOnError="true" InitialValue="0"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-1">
                                                    <label>Per (%) :</label>
                                                    <asp:TextBox ID="txtTDSPer" runat="server" CssClass="form-control" Style="text-align: right" MaxLength="5"
                                                        ToolTip="Please Enter TDS Percentage" AutoPostBack="false" onblur="calTdsamount()"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTDSPer"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                        ControlToValidate="txtTDSPer" Display="None"
                                                        ErrorMessage="Please Enter TDS Percentage1" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>TDS Amount :</label>
                                                    <asp:TextBox ID="txtTDSAmount" runat="server" MaxLength="7" CssClass="form-control" Style="text-align: right" onblur="CheckTdsAmount();"
                                                        ToolTip="Can be Edited"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtTDSAmount"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                        ControlToValidate="txtTDSAmount" Display="None"
                                                        ErrorMessage="Please Enter TDS Amount" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                  <div class="col-md-1">
                                                    <%--<label>Mode</label>--%>
                                                    <asp:Label ID="Label3" runat="server" Text="Cr" Style="font-weight:bold"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-2"></div>
                                                <div class="col-sm-9">
                                                    <asp:ListView ID="lvTaxList" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <h4>Tax Details</h4>
                                                            </div>
                                                            <table class="table table-bordered table-hover" style="margin-bottom: 0%">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>S.No</th>
                                                                        <th>Tax Head</th>
                                                                        <th>Percentage</th>
                                                                        <th>Ledger Head</th>
                                                                        <th>Amount</th>
                                                                        <th>Mode</th>

                                                                    </tr>
                                                                </thead>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Container.DisplayIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblTaxHead" runat="server" Text='<%# Eval("FNAME")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnFno" runat="server" Value='<%# Eval("FNO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblper" runat="server" Text='<%# Eval("PERCENTAGE")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtTaxLedger" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtTaxLedger"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                                    </ajaxToolKit:AutoCompleteExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("AMT")%>' MaxLength="15" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtAmount"></ajaxToolKit:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblMode" runat="server" Text='<%# Eval("MODE")%>' Style="font-weight: bold"></asp:Label>
                                                                    <asp:HiddenField ID="hdnMode" runat="server" Value='<%# Eval("MODE")%>' />
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="form-group col-sm-9 "></div>
                                                    <div class="form-group col-sm-3 ">
                                                        <label>Total Amount: </label>
                                                        <asp:Label ID="lblBillAmount" runat="server" Text="" Style="font-weight: bold; text-align: right"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-12" runat="server" id="divadditional" visible="false">
                                                    <div class="form-group col-sm-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">
                                                                Extra Charges OR Discount.
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="form-group row">

                                                                    <div class="form-group col-md-3">
                                                                        <label>Additional Charges OR Discount.</label>
                                                                    </div>
                                                                    <div class="form-group col-md-1">
                                                                        <label>If Discount</label>
                                                                    </div>
                                                                    <div class="form-group col-md-2">
                                                                        <label>Percentage(%).</label>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Select Ledger</label>
                                                                    </div>
                                                                    <div class="form-group col-md-2">
                                                                        <label>Amount.</label>
                                                                    </div>

                                                                    <div class=" col-md-12" runat="server" id="divAd1" visible="false">
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtEcharge" CssClass="form-control" TabIndex="23" runat="server" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:CheckBox ID="chkDiscount" runat="server" Checked="false" TabIndex="24" Enabled="false" />
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEPer" runat="server" CssClass="form-control" TabIndex="25" Enabled="false">0</asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEPer" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEPer" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtE1Ledger" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtE1Ledger"
                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                                            </ajaxToolKit:AutoCompleteExtender>

                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEAmt" runat="server" CssClass="form-control" TabIndex="26" Style="text-align: right" Enabled="false">0</asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEAmt" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEAmt" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:Label ID="lblMode" runat="server" Style="font-weight: bold" Text=" "></asp:Label>
                                                                        </div>


                                                                    </div>
                                                                    <div class=" col-md-12" runat="server" id="divAd2" visible="false">
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtEcharge1" CssClass="form-control" runat="server" TabIndex="27" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:CheckBox ID="chkDiscount1" runat="server" Checked="false" Enabled="false" />
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEPer1" CssClass="form-control" runat="server" TabIndex="28" Enabled="false">0</asp:TextBox>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEPer1" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEPer1" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtE2Ledger" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtE2Ledger"
                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                                            </ajaxToolKit:AutoCompleteExtender>
                                                                        </div>

                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEAmt1" CssClass="form-control" runat="server" TabIndex="29" Style="text-align: right" Enabled="false">0</asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEAmt1" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEAmt1" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:Label ID="lblMode1" runat="server" Style="font-weight: bold" Text=" "></asp:Label>

                                                                        </div>

                                                                    </div>
                                                                    <div class=" col-md-12" runat="server" id="divAd3" visible="false">
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtEcharge2" runat="server" CssClass="form-control" TabIndex="30" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:CheckBox ID="chkDiscount2" runat="server" Checked="false" TabIndex="31" Enabled="false" />
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEPer2" runat="server" CssClass="form-control" TabIndex="32" Enabled="false">0</asp:TextBox>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEPer2" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEPer2" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtE3Ledger" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtE3Ledger"
                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                                            </ajaxToolKit:AutoCompleteExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEAmt2" runat="server" CssClass="form-control" TabIndex="33" Style="text-align: right" Enabled="false">0</asp:TextBox>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEAmt2" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEAmt2" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:Label ID="lblMode2" runat="server" Style="font-weight: bold" Text=" "></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                    <div class=" col-md-12" runat="server" id="divAd4" visible="false">

                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtEcharge3" runat="server" CssClass="form-control" TabIndex="34" Enabled="false"></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:CheckBox ID="chkDiscount3" runat="server" Checked="false" TabIndex="35" Enabled="false" />
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEPer3" runat="server" CssClass="form-control" TabIndex="36" Enabled="false">0</asp:TextBox>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEPer3" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEPer3" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-3">
                                                                            <asp:TextBox ID="txtE4Ledger" runat="server" CssClass="form-control"></asp:TextBox>

                                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtE4Ledger"
                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                                            </ajaxToolKit:AutoCompleteExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-2">
                                                                            <asp:TextBox ID="txtEAmt3" runat="server" CssClass="form-control" TabIndex="37" Style="text-align: right" Enabled="false">0</asp:TextBox>

                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEAmt3" runat="server"
                                                                                FilterType="Custom,Numbers" TargetControlID="txtEAmt3" ValidChars=".">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-md-1">
                                                                            <asp:Label ID="lblMode3" runat="server" Style="font-weight: bold" Text=" "></asp:Label>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class=" col-sm-6">
                                                        <div class="col-sm-4">
                                                            <label>Party Name :</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPartyName" runat="server" TextMode="MultiLine" ToolTip="Please Enter Party Name" autocomplete="off"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <div class="col-md-3">
                                                            <label>Pan No :</label>
                                                        </div>
                                                        <div class="form-group col-md-7">
                                                            <asp:TextBox ID="txtPanNo" runat="server" ToolTip="Please Enter PAN Number" autocomplete="off" MaxLength="10" CssClass="form-control">
                                                            </asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-3">
                                                            <label>Gstin No:</label>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtGSTNNO" runat="server" placeholder="GSTIN NUMBER" autocomplete="off" CssClass="form-control" ToolTip="GSTIN NUMBER" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGSTNNO" FilterType="UppercaseLetters,Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>




                                                <div class="form-group row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2">
                                                            <label>Nature of Service :</label>
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox ID="txtNatureService" runat="server" ToolTip="Enter Nature of Service" TextMode="MultiLine"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row" id="row3" runat="server">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2">
                                                            <label>Narration :</label>
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" MaxLength="390"
                                                                ToolTip="Please Enter Narration" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>Cheque No./Date :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtChqNo2" runat="server" AutoPostBack="false" ToolTip="Please Enter Account Name" autocomplete="off"
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
                                                </div>

                                                <div class="form-group row text-center">
                                                    <div class="col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnsave_Click" ValidationGroup="AccMoney" />
                                                            <asp:Button ID="btnback" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnback_Click" />
                                                            <asp:ValidationSummary ID="vs" ValidationGroup="AccMoney" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Div2">
                                            <div class="col-md-2">
                                                <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                            </div>

                                            <div class="col-md-5">
                                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                    DropShadow="True" PopupControlID="Panel1" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                                    Enabled="True">
                                                </ajaxToolKit:ModalPopupExtender>
                                                <asp:Panel ID="Panel1" runat="server" Width="600px" BorderColor="#0066FF" BackColor="White" meta:resourcekey="pnlResource1">
                                                    <div class="panel panel-primary">
                                                        <div class="panel-heading">
                                                            Transaction
                                                        </div>
                                                        <div class="panel-body">
                                                            <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                            <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" ValidationGroup="Validation"
                                                                CssClass="btn btn-info" OnClick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                                                            &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="Validation"
                                                                CssClass="btn btn-danger" OnClick="btnClose_Click" meta:resourcekey="btnBackResource1" />
                                                            &nbsp;<asp:Button ID="btnchequePrint" runat="server" CssClass="btn btn-primary" Text="Print Cheque" Style="display: none" OnClick="btnchequePrint_Click" />
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
                                </asp:Panel>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

