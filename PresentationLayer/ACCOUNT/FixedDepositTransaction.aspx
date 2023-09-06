<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="FixedDepositTransaction.aspx.cs" Inherits="ACCOUNT_FixedDepositTransaction" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#table2').DataTable({
                destroy: true,
            });
        });

        function bindRepeater() {
            $('#table2').DataTable({
                destroy: true,
            });
        }

        function RunThisAfterEachAsyncPostback() {
            bindRepeater();
        }

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }

    </script>

     <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <%-- <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>
    <div style="width: 100%">
        <%--<asp:UpdatePanel ID="UPDLedger" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">FIXED DEPOSIT ENTRY</h3>
                    </div>
                    <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                    <div class="box-body">

                        <asp:Panel ID="pnlList" runat="server">
                            <div class="form-group row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnAddNew" runat="server" ToolTip="Click to Add New FD" Text="Add New" CssClass="btn btn-primary" OnClick="btnAddNew_Click" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <asp:Repeater ID="rptFDRList" runat="server">
                                        <HeaderTemplate>
                                            <h4 class="box-title">FDR List</h4>
                                            <table id="table2" class="table table-bordered table-striped dt-responsive nowrap">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>EDIT
                                                        </th>
                                                        <th style="display: none">SRNO
                                                        </th>
                                                        <th>FDR NO
                                                        </th>
                                                        <th>INVESTED DATE
                                                        </th>
                                                        <th>MATURITY DATE
                                                        </th>
                                                        <th style="text-align: right">INVESTED AMT.
                                                        </th>
                                                        <th style="text-align: right">RATE
                                                        </th>
                                                        <th style="text-align: right">MATURITY AMT
                                                        </th>
                                                        <th style="text-align: center">ACTION
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnFDREdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("FDID") %>'
                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FDID") %>' OnClick="btnFDREdit_Click" TabIndex="1" />
                                                </td>
                                                <td style="display: none">
                                                    <asp:Label ID="lblFdSrNo" runat="server" Text='<%# Eval("FD_SRNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFDR_No" runat="server" Text='<%# Eval("FDR_NO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblInvest_Date" runat="server" Text='<%# Eval("INV_DATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMaturity_Date" runat="server" Text='<%# Eval("MAT_DATE")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <%# Eval("AMT_INVESTED") %>
                                                    <asp:HiddenField ID="hdnBilltype" runat="server" Value='<%# Eval("AMT_INVESTED") %>' />
                                                </td>
                                                <td style="text-align: right">
                                                    <%# Eval("ROI") %>
                                                    <asp:HiddenField ID="hdnFDSrNo" runat="server" Value='<%# Eval("FD_SRNO") %>' />
                                                    <asp:HiddenField ID="hdnIsclosed" runat="server" Value='<%# Eval("ISCLOSED") %>' />
                                                    <asp:HiddenField ID="hdnFDStatus" runat="server" Value='<%# Eval("FD_STATUS") %>' />
                                                </td>
                                                <td style="text-align: right">
                                                    <%# Eval("MATURITY_AMT") %>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnFDRClose" runat="server" ToolTip="Click to close FD" Text="Close" CommandArgument='<%# Eval("FDID") %>' CssClass="btn btn-primary" OnClick="btnFDRClose_Click" />
                                                    <asp:Button ID="btnFDRRenew" runat="server" Text="Renew" ToolTip="Click to renew FD" CommandArgument='<%# Eval("FDID") %>' CssClass="btn btn-primary" OnClick="btnFDRRenew_Click" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlDetails" runat="server">
                            <div class="form-group row">
                                <div class="col-md-12">
                                    Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span>
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Fixed Deposit Entry</div>
                                <div class="panel-body">
                                    <div class="col-md-12">

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Serial No.  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtserialNo" runat="server" CssClass="form-control" Font-Bold="true" ReadOnly="true" Style="color: blue; text-align: right"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtSerialNo" runat="server"
                                                    ControlToValidate="txtserialNo" Display="None"
                                                    ErrorMessage="Please Enter Serial No." SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2"></div>
                                            <div class="col-md-2">
                                                <label>Select Company<span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" TabIndex="1"
                                                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCompany" runat="server"
                                                    ControlToValidate="ddlCompany" Display="None"
                                                    ErrorMessage="Please Select Company" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Select Ledger  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtLedger" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                    AutoPostBack="false"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="0"
                                                    ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtLedger" runat="server"
                                                    ControlToValidate="txtLedger" Display="None"
                                                    ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Select Bank  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBankLedger" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Bank Name"
                                                    AutoPostBack="false"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autBankLedger" runat="server" TargetControlID="txtBankLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionInterval="0" CompletionSetCount="1"
                                                    ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtBankLedger" runat="server"
                                                    ControlToValidate="txtBankLedger" Display="None"
                                                    ErrorMessage="Please Select Bank Ledger" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Customer Id  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCustomerId" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtCustomerId" runat="server" ControlToValidate="txtCustomerId"
                                                    Display="None" ErrorMessage="Please Enter Customer Id" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <label>FDR No  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtFDRNo" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtFDRNo" runat="server"
                                                    ControlToValidate="txtFDRNo" Display="None"
                                                    ErrorMessage="Please Enter FDR Number" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Investment Date  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgInvestDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtInvestmentDate" Width="200px" runat="server" TabIndex="6" CssClass="form-control"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="imgInvestDate" TargetControlID="txtInvestmentDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtInvestmentDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtInvestmentDate" runat="server"
                                                        ControlToValidate="txtInvestmentDate" Display="None"
                                                        ErrorMessage="Please Enter Investment Date" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Maturity Date  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgMaturityDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtMaturityDate" runat="server" Width="200px" TabIndex="7" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="imgMaturityDate" TargetControlID="txtMaturityDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtMaturityDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtMaturityDate" runat="server" ControlToValidate="txtMaturityDate" Display="None"
                                                        ErrorMessage="Please Enter Maturity Date" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Amount Invested  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtInvestedAmt" runat="server" TabIndex="8" CssClass="form-control" MaxLength="13"
                                                    Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtInvestedAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:RequiredFieldValidator ID="rfvtxtInvestedAmt" runat="server"
                                                    ControlToValidate="txtInvestedAmt" Display="None"
                                                    ErrorMessage="Please Enter Invested Amount" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-1"></div>
                                            <div class="col-md-2">
                                                <label>Maturity Value(Amt.)  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtMaturityAmt" runat="server" TabIndex="9" CssClass="form-control" MaxLength="13"
                                                    Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMaturityAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:RequiredFieldValidator ID="rfvtxtMaturityAmt" runat="server"
                                                    ControlToValidate="txtMaturityAmt" Display="None"
                                                    ErrorMessage="Please Enter Maturity Amount" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Rate of Interest(ROI)<span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRateofInterest" runat="server" TabIndex="10" CssClass="form-control" MaxLength="5" Width="240px" Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRateofInterest"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:RequiredFieldValidator ID="rfvtxtRateofInterest" runat="server" ControlToValidate="txtRateofInterest" Display="None"
                                                    ErrorMessage="Please Enter Rate of Interest(ROI)" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <label>PAN Number  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPANNo" runat="server" TabIndex="11" CssClass="form-control" MaxLength="10" Width="240px" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtPANNo" runat="server" ControlToValidate="txtPANNo" Display="None"
                                                    ErrorMessage="Please Enter PAN Number" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Period From  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="PeriodFrom" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtPeriodFrom" runat="server" TabIndex="12" CssClass="form-control" Width="200px" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="PeriodFrom" TargetControlID="txtPeriodFrom">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtPeriodFrom" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPeriodFrom" Display="None"
                                                        ErrorMessage="Please Enter Period From Date" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Period To  <span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtPeriodTo" runat="server" TabIndex="13" Width="200px" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image1" TargetControlID="txtPeriodTo">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtPeriodTo" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPeriodTo" Display="None"
                                                        ErrorMessage="Please Enter Period To Date" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Interest To Be Accumulated<span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtInterestAccumlated" runat="server" TabIndex="10" CssClass="form-control" MaxLength="9" Width="240px" Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtInterestAccumlated"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInterestAccumlated" Display="None"
                                                    ErrorMessage="Please Enter Interest To Be Accumulated" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <label>FD Withdrawn Amount<span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtFDWithDrawAmt" runat="server" TabIndex="11" CssClass="form-control" MaxLength="10" Width="240px"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFDWithDrawAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFDWithDrawAmt" Display="None"
                                                    ErrorMessage="Please Enter FD Withdrawn Amount" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>FD Duration<span style="color: red">*</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtFDDuration" runat="server" TabIndex="10" CssClass="form-control" MaxLength="50" Width="240px" Style="text-align: right"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFDDuration" Display="None"
                                                    ErrorMessage="Please Enter FD Duration" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Register Book No.<span style="color: red"></span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRegisterBookNo" runat="server" TabIndex="11" CssClass="form-control" MaxLength="10" Width="240px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Scheme</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtScheme" runat="server" TabIndex="14" Width="240px" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2">
                                                <label>FD & Advice Attachment<span style="color: red"></span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="12" />
                                                <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg,.doc) upto 5MB" ForeColor="Red"></asp:Label>
                                            </div>

                                        </div>
                                        <%-- </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="flupld" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Reference<span style="color: red"></span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtReference" runat="server" TabIndex="10" CssClass="form-control" MaxLength="64" Width="240px" Style="text-align: right"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2">
                                                <label>Bank Address<span style="color: red"></span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBankAddress" runat="server" TabIndex="11" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Account Holder</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAccHolder" runat="server" TabIndex="15" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Address</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAddress" runat="server" TabIndex="16" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Nomination For</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNomination" runat="server" TabIndex="17" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Remark</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRemark" runat="server" TabIndex="18" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:UpdatePanel ID="dfffd" runat="server"> 
                            <ContentTemplate>

                                <asp:Panel ID="pnlOtherDetils" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Other Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="col-md-2">
                                                    <label>Email Id</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" TabIndex="19"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ErrorMessage="Invalid Email" ControlToValidate="txtEmailId"
                                                        SetFocusOnError="True" ValidationGroup="Email" Display="None"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Mobile No</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" TabIndex="20"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMobileNo"
                                                        FilterType="Custom, Numbers" ValidChars="" Enabled="True" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" TabIndex="21" Text="Add" ValidationGroup="Email"
                                                        OnClick="btnAdd_Click" />
                                                    <asp:ValidationSummary runat="server" ID="VSButtonAdd" ValidationGroup="Email" ShowSummary="false" DisplayMode="List" ShowMessageBox="true" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Repeater ID="rptOtherDetails" runat="server">
                                                        <HeaderTemplate>
                                                            <h4 class="box-title">List</h4>
                                                            <table id="table2" class="table table-bordered table-striped dt-responsive nowrap">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>ACTION
                                                                        </th>
                                                                        <th>EMAIL ID
                                                                        </th>
                                                                        <th>Mobile No
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SRNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="1" OnClick="btnEdit_Click" />
                                                                    <asp:ImageButton ID="btnDelete" runat="server" TabIndex="14" Text="Select" ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                        ImageUrl="~/IMAGES/delete.png" Width="15" Height="15" CommandArgument='<%# Eval("SRNO") %>' OnClick="btnDelete_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPayeeName" runat="server" Text='<%# Eval("MOBILENO") %>'></asp:Label>
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
                                    </div>
                                </asp:Panel>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                              <%--  <asp:AsyncPostBackTrigger ControlID="btnSubmit" />--%>
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="form-group row" id="dvButtons" runat="server">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Click to save FD" CssClass="btn btn-primary" TabIndex="22" ValidationGroup="AccMoney"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to cancel" CssClass="btn btn-warning" TabIndex="23" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Click to back" CssClass="btn btn-warning" TabIndex="24" OnClick="btnBack_Click" OnClientClick="bindRepeater();" />
                                <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    DisplayMode="List" ValidationGroup="AccMoney" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="btnAdd"/>   
                  <asp:PostBackTrigger  ControlID="btnSubmit"/>               
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
