<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DeleteStorePayment.aspx.cs" Inherits="ACCOUNT_DeleteStorePayment" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <h3 class="box-title">DELETE STORE PAYMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" class="account_compname" style="font-size: x-large; text-align: center">
                                </div>
                                <div>Note:<span style="color: red">* Marked is mandatory !</span></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Delete Store Payment</div>
                                        <div class="panel-body">
                                            <div id="Div2" class="form-group col-sm-12" runat="server">
                                                <div class="col-md-3">
                                                    <label>Vendor Name<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Vendor Name"></asp:TextBox><%--AutoPostBack="true" OnTextChanged="txtAcc_TextChanged" --%>
                                                    <ajaxToolKit:AutoCompleteExtender ID="autVendor" runat="server" TargetControlID="txtAcc"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                        ServiceMethod="GetMergeVendor" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtAcc" Display="None"
                                                                ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-sm-4">                                                   
                                                    <asp:Button ID="btnGo" runat="server" Text="GO" ValidationGroup="AccMoney"
                                                        CssClass="btn btn-primary" OnClick="btnGo_Click" OnClientClick="return validate()" />
                                                    <asp:ValidationSummary ID="vsSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AccMoney" />
                                                    <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="AccMoney"
                                                                OnClick="btnCancel_Click" />--%>
                                                        </div>
                                                </div>
                                                <div class="form-group col-sm-12 ">

                                                    <asp:Repeater ID="GridInvoice" runat="server">
                                                        <%--<EmptyDataTemplate>
                                                                <br />
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblErr" runat="server" Text="No Record Found"></asp:Label>
                                                                </p>
                                                            </EmptyDataTemplate>--%>
                                                        <HeaderTemplate>
                                                            <h4 class="box-title">Payment Details</h4>
                                                            <table id="table2" class="table table-bordered table-striped dt-responsive nowrap">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Invoice No
                                                                        </th>
                                                                        <th>Vendor Name
                                                                        </th>
                                                                        <%-- <th>Purchase Order 
                                                                    </th>--%>
                                                                        <th>Amount
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>                                                                   
                                                                    <%--<asp:LinkButton ID="lnkselect" runat="server" Text="Delete" 
                                                                        ToolTip='<%# Eval("COMPANY_CODE")%>' CommandName='<%# Eval("ACC_STRINV_PAYMENT_TRNO") %>'
                                                                        CommandArgument='<%#Eval("VOUCHER_NO")%>'
                                                                       OnClick="lnkselect_Click"></asp:LinkButton>--%> <%--OnClientClick="showConfirmDel(this); return false;" --%>
                                                                    <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName='<%# Eval("ACC_STRINV_PAYMENT_TRNO") %>' 
                                                                        CommandArgument='<%#Eval("VOUCHER_SQN")%>' ToolTip='<%# Eval("COMPANY_CODE")%>' CssClass="btn btn-info btn-xs"
                                                                        OnClick="btndelete_Click" OnClientClick="showConfirmDel(this); return false;"/>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblInvoice" runat="server" Text='<%# Eval("INVOICE_NO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VENDOR NAME")%>'></asp:Label>
                                                                </td>
                                                                <%--<td>
                                                                <asp:Label ID="lblPurchaseOrder" runat="server" Text='<%# Eval("REFNO")%>'></asp:Label>
                                                            </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT","{0:0.00}")%>'></asp:Label>
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
                            </div>
                        </div>

                        <div class="box-footer">
                            <div class="col-md-12">
                                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
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

            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
        </ContentTemplate>
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

    <script type="text/javascript">      
        function validate() {
            debugger;
            var x = document.getElementById('<%= txtAcc.ClientID %>').value;

            if (x.trim() == '') {
                alert("Please Select Vendors ..!!!");
                return false;
            }
            return true;
        }

    </script>

</asp:Content>

