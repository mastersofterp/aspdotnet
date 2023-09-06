<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccVoucherVerifyApprove.aspx.cs" Inherits="ACCOUNT_AccVoucherVerifyApprove" %>


<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>--%>
    <%--<script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>
    <%-- <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>--%>
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
            //if (document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value == '') {
            //    alert('Please Select Ledger.');
            //    document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
            //    return false;
            //}
        }
        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, 'Tool Tip', 'CreateSubLinks');
            return true;
        }
    </script>

    <%-- <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>
    <%-- <script language="javascript" type="text/javascript">
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

    </script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VOUCHER VERIFY/APPROVE</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="Panel2" runat="server">
                        <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>

                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"
                                                AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Upto Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control"
                                                AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" />
                                            <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft"
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
                                    <div class="form-group col-lg-6 col-md-6 col-12" id="divLedger" runat="server" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Ledger Name</label>
                                                </div>
                                                <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                    AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:TextBox ID="lblCurBal" runat="server" BorderColor="White"
                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                    Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                    BorderStyle="None"
                                                    Style="background-color: Transparent;" ReadOnly="True"
                                                    Font-Size="XX-Small"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                <asp:Button ID="btnGo" runat="server" Text="GO" ValidationGroup="AccMoney"
                                    CssClass="btn btn-primary" OnClick="btnGo_Click" /><%--OnClientClick="return validate()"--%>
                                <asp:ValidationSummary ID="vsSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AccMoney" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="AccMoney"
                                    OnClick="btnCancel_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="" Visible="false" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" OnClick="btnSubmit_Click" /><%-- OnClientClick="return validate()"--%>
                            </div>

                            <%--<div class="col-md-2">
                                                        </div>--%>
                            <div class="col-12" id="trGrid" runat="server">
                                <div class="table-responsive">
                                    <asp:Panel ID="pnl" runat="server">
                                        <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                            <asp:Repeater ID="RptData" runat="server" OnItemCommand="RptData_ItemCommand"
                                                OnItemDataBound="RptData_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%--<asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                    ImageUrl="~/images/edit.gif" ToolTip="Edit record" />--%>
                                                            <%--delete voucher should be maintained--%>
                                                            <%-- <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                    CommandName="VoucherDelete" ImageUrl="~/images/delete.gif" ToolTip="Delete record" OnClientClick="AskDelete()" />--%>
                                                            <asp:HiddenField ID="hdnVoucherNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                            <asp:HiddenField ID="hdnVoucherSQNNo" runat="server" Value='<%# Eval("VOUCHER_SQN")%>' />
                                                            <asp:CheckBox ID="chkVoucher" runat="server" ToolTip='<%# Eval("VOUCHER_SQN")%>' />
                                                        </td>
                                                        <td>Details
                                                                                    <asp:ImageButton ID="btnDetails" ToolTip="Click For Voucher Details. " runat="server"
                                                                                        CommandArgument='<%# Eval("VOUCHER_SQN")%>' CommandName="VoucherDetails"
                                                                                        ImageUrl="~/Images/i.gif" />
                                                            <asp:HiddenField ID="hdnPartyNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                        </td>
                                                        <td>Voucher No.
                                                                                    <%#Eval("VOUCHER_NO")%>
                                                        </td>
                                                        <td>Created By:
                                                                             <asp:Label ID="lblcreatedby" Style="font-weight: bold; width: 10%" align="center" runat="server"> </asp:Label>
                                                        </td>
                                                        <td>Voucher Type :<asp:Label ID="lblvchtype" runat="server" Text='<%#Eval("Transaction_Type")%>'> </asp:Label></td>
                                                        <td>Voucher Print
                                                                                    <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                                                        CommandArgument='<%# Eval("VOUCHER_SQN")%>' CommandName="VoucherPrint" ImageUrl="~/Images/print.png" />
                                                        </td>
                                                        <%-- <td style="font-weight: bold; width: 12%" align="center">Copy Voucher
                                                                                    <asp:ImageButton ID="btnCopy" runat="server" CommandArgument='<%# Eval("VOUCHER_SQN")%>'
                                                                                        ImageUrl="~/IMAGES/copy2.png" Width="18px" Height="18px" ToolTip="Copy record"
                                                                                        CommandName="CopyVoucher" />
                                                                            </td>--%>
                                                        <td>View Bill
                                                                                   <asp:ImageButton ID="btnViewBill" ToolTip="Click For Voucher Printing. " runat="server"
                                                                                       CommandArgument='<%# Eval("VOUCHER_SQN")%>' CommandName="ViewBill" ImageUrl="~/Images/view2.png" />
                                                        </td>

                                                        <td>Cheque No :
                                                                                      <asp:Label ID="lblChqNo" runat="server" Text='<%#Eval("CHQ_NO")%>'> </asp:Label></td>

                                                        <td id="tdPaymentMode" runat="server" style="font-weight: bold; width: 8%" align="center">Pay Mode : 
                                                        <asp:Label ID="lblPayMode" runat="server" Text='<%#Eval("PAYMENT_MODE")%>'> </asp:Label>

                                                        </td>
                                                        <asp:DropDownList ID="ddlPaymentMode" Visible="false" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                            <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                            <asp:ListItem Value="R">RTGS</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </td>
                                                                          

                                                    </tr>
                                                    <tr>
                                                        <td colspan="9"><%--ScrollBars="Auto"--%>
                                                            <asp:Panel ID="pnl1" runat="server">
                                                                <asp:ListView ID="lvGrp" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Date
                                                                                        </th>
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
                                                                                <%# Eval("Date")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Particulars")%></td>

                                                                            <td>
                                                                                <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                            </td>
                                                                            <td>
                                                                                <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr class="altitem">
                                                                            <td>
                                                                                <%# Eval("Date")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Particulars")%>
                                                                            </td>
                                                                            <%--<td style="width: 15%; text-align: right; font-weight: bold">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                                </td>
                                                                                                <td style="width: 15%; text-align: right; font-weight: bold">
                                                                                                    <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                                </td>--%>
                                                                            <td>
                                                                                <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                            </td>
                                                                            <td>
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
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>


                            <div id="Div3" class="col-12" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Op. Bal. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblOb" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Dr. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDr" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Cr. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCr" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Close. Bal. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblclose" runat="server" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblmode" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>

                        </asp:Panel>


                        <div class="col-12" id="Divpnlupload" runat="server" visible="false">
                            <asp:Panel ID="pnlupload" runat="server">
                                <%-- <div class="sub-heading">
                                                Uploaded Bills                            
                                            </div>--%>
                                <div class="form-group" id="div7" style="display: block;">
                                    <asp:Panel ID="pnlNewBills" runat="server">
                                        <div class="col-12 ">
                                            <asp:ListView ID="lvNewBills" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Uploaded Bill List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
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
                                        </div>

                                    </asp:Panel>

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnback_Click" />

                                </div>

                            </asp:Panel>
                        </div>

                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

