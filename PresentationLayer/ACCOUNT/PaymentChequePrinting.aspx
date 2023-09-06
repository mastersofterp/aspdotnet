<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaymentChequePrinting.aspx.cs" Inherits="PaymentChequePrinting" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 220px;
        }
    </style>--%>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

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

            if (document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value == 'Press space to get all bank ledgers.' || document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value.trim() == '') {
                alert('Please Enter Ledger.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
                return false;
            }
            return true;
        }

        function ShowChequePrinting(VarDate, VarTranNo, VarParty, VarAmt, VarOparty, VarChqNo) {
            var popUrl = 'ChequePrintingTransaction.aspx?obj=' + 'ChequePrinting,' + VarDate + ',' + VarTranNo + ',' + VarParty + ',' + VarAmt + ',' + VarOparty + ',' + VarChqNo;
            var name = 'popUp';
            var appearence = 'center:yes; dialogWidth:1028px; dialogHeight:650px; edge:raised; ' +
                             'help:no; resizable:no; scroll:no; status:no;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
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
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CHEQUE PRINTING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            </div>
                            <asp:Panel ID="Panel1" runat="server">
                                <%-- <div class="col-md-8">--%>
                                <div class="col-12 mt-3">
                                    <%-- <div class="panel-heading">Print Cheque</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server"
                                                    AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" CssClass="form-control" />
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
                                                <sup>*</sup>
                                                <label>Upto Date</label>
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
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Ledger Name</label>
                                            </div>
                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetLegerName" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtAcc" Display="None"
                                                ErrorMessage="Please Enter Ledger Name" SetFocusOnError="true"
                                                ValidationGroup="AccMoney">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div id="Div4" class="col-12 btn-footer" runat="server">
                                    <asp:Button ID="btnGo" runat="server" Text="GO" ValidationGroup="AccMoney" CssClass="btn btn-primary"
                                        OnClick="btnGo_Click" OnClientClick="return CheckFields()" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="AccMoney"
                                        CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                </div>

                                <div class="col-12">
                                    <div class="table-responsive">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <asp:Repeater ID="RptData" runat="server">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Voucher No.
                                                                        <%#Eval("STR_VOUTCHER_NO")%>
                                                            </td>
                                                            <td>Voucher Type :<asp:Label ID="lblvchtype" runat="server" Text='<%#Eval("Transaction_Type")%>'> </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Panel ID="pnl1" runat="server">
                                                                    <div class="col-12">
                                                                        <asp:ListView ID="lvGrp" runat="server">
                                                                            <LayoutTemplate>
                                                                                <div id="demo-grid" class="vista-grid">
                                                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>Date</th>
                                                                                                <th>Particulars</th>
                                                                                                <th>Debit</th>
                                                                                                <th>Credit</th>
                                                                                                  <th runat="server" visible="false"></th>
                                                                                                    <th runat="server" visible="false"></th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>

                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="item">
                                                                                    <td>
                                                                                        <%# Eval("Date")%>
                                                                                        <asp:HiddenField ID="hdnTranDate" runat="server" Value='<%# Eval("Date")%>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkledger" runat="server" Text='<%# Eval("Particulars")%>'></asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                    </td>
                                                                                    <td runat="server" visible="false">
                                                                                        <asp:HiddenField ID="hdnparty" runat="server" Value='<%# Eval("PartyNo")%>' />
                                                                                    </td>
                                                                                    <td runat="server" visible="false">
                                                                                        <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VchNo")%>' />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <AlternatingItemTemplate>
                                                                                <tr class="altitem">
                                                                                    <td>
                                                                                        <%# Eval("Date")%>
                                                                                        <asp:HiddenField ID="hdnTranDate" runat="server" Value='<%# Eval("Date")%>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkledger" runat="server" Text='<%# Eval("Particulars")%>'></asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# string.Format("{0:#,0.00}", Eval("DEBIT"))%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# string.Format("{0:#,0.00}", Eval("CREDIT"))%>
                                                                                    </td>
                                                                                    <td runat="server" visible="false">
                                                                                        <asp:HiddenField ID="hdnparty" runat="server" Value='<%# Eval("PartyNo")%>' />
                                                                                    </td>
                                                                                    <td runat="server" visible="false">
                                                                                        <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VchNo")%>' />
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
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

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
