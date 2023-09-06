<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_VoucherOnlinePayment.aspx.cs" Inherits="ACCOUNT_Acc_VoucherOnlinePayment" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
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
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VOUCHER ONLINE PAYMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>

                                <div class="panel panel-info">
                                    <div class="panel-heading">Voucher Details Selection</div>
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlVch" runat="server">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Voucher Type <span style="color: red">*</span>:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:DropDownList ID="ddlVoucherType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged" ><%--onchange="return CleraFields();"--%>
                                                      <%--  AutoPostBack="true" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged"--%>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="AV">ACCOUNT VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="BK">BULK VOUCHER</asp:ListItem>
                                                        <%-- <asp:ListItem Value="AD">ADVANCE VOUCHER</asp:ListItem>
                                                        <asp:ListItem Value="ST">STORE VOUCHER</asp:ListItem>--%>
                                                        <asp:ListItem Value="BL">BILL VOUCHER</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvType" runat="server"
                                                        ControlToValidate="ddlVoucherType" Display="None" ErrorMessage="Please Select Voucher Type" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="Voucher">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>From Date <span style="color: red"></span>:</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" />
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
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>To Date :</label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgTodate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtToDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgTodate" PopupPosition="BottomLeft" TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtToDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:MaskedEditExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>Bank Ledger : </label>
                                                </div>
                                                <div class="form-group col-md-6">

                                                    <asp:TextBox ID="txtBankLedger" runat="server" AutoPostBack="true" CssClass="form-control"
                                                        ToolTip="Please Enter Ledger Name"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="aceAcc" runat="server" TargetControlID="txtBankLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>

                                                </div>

                                            </div>



                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="form-group col-md-10">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnShow_Click"
                                                        ValidationGroup="Voucher" />
                                                    <asp:Button ID="btnMakePayment" runat="server"  Text="Make Payment" CssClass="btn btn-success" Visible="false" OnClick="btnMakePayment_Click" 
                                                        ValidationGroup="AccMoney" />
                                                    <asp:Button ID="btnUpdateTranStatus" runat="server" Text="Update Transaction Status" CssClass="btn btn-success" Visible="false" OnClick="btnUpdateTranStatus_Click" 
                                                        ValidationGroup="AccMoney" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                                    <asp:HyperLink ID="lnkDownload" Visible="false" runat="server"></asp:HyperLink>
                                                    <asp:ValidationSummary ID="vsAcc" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="AccMoney" DisplayMode="List" />

                                                    <asp:ValidationSummary ID="vsVoucher" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Voucher" DisplayMode="List" />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row" id="divVoucher" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Select Voucher :<span style="color: red">*</span></label>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:DropDownList ID="ddlVoucherNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlVoucherNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-12 table-responsive" id="divOther" runat="server" style="display: block">
                                                <asp:ListView ID="lvVoucher" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            NO RECORD FOUND                                                               
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <h4>Voucher Details</h4>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <%-- <th>Sr.No.</th>--%>
                                                                        <th>Select</th>
                                                                        <th>Voucher No.</th>
                                                                        <th>Payee Name</th>
                                                                        <th>Bank Name</th>
                                                                        <th>Bank Acc No.</th>
                                                                        <th>Amount</th>
                                                                        <th>Transaction Date</th>
                                                                        <%-- <th>Action</th>--%>
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
                                                            <%-- <td >
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>--%>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("VOUCHER_SQN") %>' /></td>
                                                            <td>
                                                                <%# Eval("STR_VOUCHER_NO")%>
                                                                <asp:HiddenField ID="hdnVchNo" runat="server" Value='<%# Eval("VOUCHER_NO")%>' />
                                                                <asp:HiddenField ID="hdnEmpDataId" runat="server" Value='<%# Eval("EMP_DATA_ID")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("PAYEE_NAME")%> 
                                                            </td>
                                                            <td>
                                                                <%# Eval("BANKNAME")%> 
                                                            </td>
                                                            <td>
                                                                <%# Eval("BANKACC_NO")%> 
                                                            </td>

                                                            <td>
                                                                <%# Eval("AMOUNT")%>                                                                  
                                                            </td>
                                                            <td>
                                                                <%# Eval("TRANSACTION_DATE")%> 
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                                <%--<div class="text-center">
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpVch" runat="server" PagedControlID="lvBVoucher" PageSize="50" Visible="false"
                                                            OnPreRender="dpVch_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" lang="javascript">
        function CleraFields() {
           
            $("#<%=divVoucher.ClientID%>").hide();
            $("#<%=divOther.ClientID%>").hide();
            $("#<%=btnMakePayment.ClientID%>").hide();
            $("#<%=btnMakePayment.ClientID%>").hide();
            $("#<%=btnUpdateTranStatus.ClientID%>").hide();          
            document.getElementById("<%=ddlVoucherNo.ClientID%>").value=0;
        }
    </script>

</asp:Content>
