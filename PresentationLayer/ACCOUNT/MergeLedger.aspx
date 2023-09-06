<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MergeLedger.aspx.cs" Inherits="Account_MergeLedger" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
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
                        <div id="div2" runat="server">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">ACCOUNTING LEDGER MERGING</h3>
                            </div>
                            <div class="box-body">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                      <%--  <div class="sub-heading">
                                            <h5>Merge Ledger</h5>
                                        </div>--%>

                                        <div class="row">
                                            <div class="form-group col-lg-10 col-md-12 col-12">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note </h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>MERGING ACCOUNT FROM ONE TO ANOTHER WILL PERMANENTLY REMOVE THE TRANSACTIONS. PLEASE BE CONFIRM ! </span></p>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select The Ledger To Be Merged</label>
                                                </div>
                                                <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtAcc" Display="None"
                                                    ErrorMessage="Please Select Ledger To Merge" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select The Ledger To Merge Into</label>
                                                </div>
                                                <asp:TextBox ID="txtAcc1" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtAcc1"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtAcc1" Display="None"
                                                    ErrorMessage="Please Select Ledger To be Merge" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" >

                                                <asp:CheckBox ID="chkDeleteLedger" runat="server" Visible="false"
                                                    Text="Delete The Account To Merge" AutoPostBack="True"
                                                    onclick=" return CheckResponse(this);" Enabled="False"
                                                    OnCheckedChanged="chkDeleteLedger_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                            ValidationGroup="AccMoney" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnMerge" runat="server" Enabled="False" OnClick="btnMerge_Click"
                                            OnClientClick="return ConfirmLedgerMerge();" Text="Merge Ledger" CssClass="btn btn-primary"  />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            CssClass="btn btn-warning" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="AccMoney" Height="20px" />
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <input id="hdnBal" runat="server" type="hidden" />
                                                    <input id="hdnMode" runat="server" type="hidden" />
                                                </div>
                                                <asp:TextBox ID="lblCurBal" runat="server" Height="23px" Width="80px" BorderColor="White"
                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 6px;" ReadOnly="True"
                                                    Font-Size="XX-Small"></asp:TextBox>
                                                <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="12px" BorderColor="White"
                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 6px;" ReadOnly="True"
                                                    Font-Size="XX-Small"></asp:TextBox>
                                                <asp:Label ID="lblmode" Style="font-size: medium; color: #000000; font-weight: 700;"
                                                    runat="server"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 mb-4">
                                        <div class="row">
                                              <div class="col-lg-6 col-md-6 col-12">
                                                <asp:Panel ID="pnlLedgerToMerge" runat="server" Visible="False">
                                                    <asp:ListView ID="lvLedgerToMerge" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Transaction(Ledger To Merge)</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap " style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>TRANS DATE
                                                                            </th>
                                                                            <th>TRANS TYPE
                                                                            </th>
                                                                            <th>AMOUNT
                                                                            </th>
                                                                            <th>VOUCHER NO
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
                                                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TRANSACTION_DATE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTransactionType" runat="server" Text='<%# Eval("TRANSACTION_TYPE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVoucherno" runat="server" Text='<%# Eval("VOUCHER_NO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <asp:Panel ID="pnlLedgerToBeMerge" runat="server" Visible="False">
                                                    <asp:ListView ID="lvLedgerToBeMerge" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Transaction(Ledger To Be Merge Into)</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap " style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>TRANS DATE
                                                                            </th>
                                                                            <th>TRANS TYPE
                                                                            </th>
                                                                            <th>AMOUNT
                                                                            </th>
                                                                            <th>VOUCHER NO
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
                                                                    <asp:Label ID="lblTransactionDate1" runat="server" Text='<%# Eval("TRANSACTION_DATE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTransactionType1" runat="server" Text='<%# Eval("TRANSACTION_TYPE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAmount1" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVoucherNo1" runat="server" Text='<%# Eval("VOUCHER_NO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                           
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
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
    <style type="text/css">
        .style3 {
            width: 3%;
        }

        .style4 {
            width: 44px;
        }

        .style5 {
            width: 407px;
        }

        .EmptyDataCSS {
            background-color: Red;
            color: White;
            font-size: 24px;
            font-weight: bolder;
            text-align: center;
            height: 10px;
        }

        .TableCSS {
            background-color: Gray;
            width: 725px;
        }
    </style>
    <%--<script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

   <%-- <script type="text/javascript" src="../AutoSuggestBox.js"></script>--%>
    <script language="javascript" type="text/javascript">
        function ConfirmLedgerMerge() {
            if (confirm('Are you sure to merge ledger?')) {
                if (confirm('Ledger may contains transaction.Ledger to merge will be deleted permanatly still you want to merge ?')) {
                    if (confirm('Ledger is about to transfer..!')) {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        function EnableDisable() {
            alert('Enterd');
            var txtLedgerToMerge = document.getElementById('txtAcc');
            var txtLedgerToBeMerge = document.getElementById('txtAcc1');
            var btnmerge = document.getElementById('btnMerge');
            alert('Enterd');
            if (txtLedgerToMerge.value != "" && txtLedgerToBeMerge != "") {
                btnmerge.style.disabled = false;
                alert('Enabled');
            }
            else {
                btnmerge.style.disabled = true;
                alert('Disabled');
            }
        }
        function CheckResponse(chk) {
            if (chk.checked == true) {
                if (confirm("Ledger will be deleted completely and will not be available for further reference. Do you still want to Delete?") == true) {
                }
                else {
                    var chkid = document.getElementById('ctl00_ContentPlaceHolder1_chkDeleteLedger');
                    chkid.checked = false;
                }
                return;
            }
        }
    </script>
</asp:Content>

