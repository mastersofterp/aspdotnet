<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AccountMaster.aspx.cs" Inherits="AccountMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="Custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 260px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
  <%--   <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>

   <%--  --%>
<%--<script language="javascript" type="text/javascript" src="../IITMSTextBox>.js"></script>--%>

    <script language="javascript" type="text/javascript">
        function SetCurrentChequeNo(obj) {
            document.getElementById('ctl00_ctp_txtChqCurNo').value = obj.value;
            return false;

        }
        function CheckNumeric(obj) {
            var k = (window.event) ? event.keyCode : event.which;
            // alert(k);
            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46 || k == 13 || k == 110) {
                if (obj.value == '') {
                    alert('Field Cannot Be Blank');
                    obj.focus();
                    return false;
                }
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58 || k > 95 && k < 106) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ACCOUNT MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                   <%-- <div class="sub-heading">
                                        <h5>Add/Modify - Account Master</h5>
                                    </div>--%>
                                    <div class="row">
                                        <asp:HiddenField ID="hdnPartyNo" runat="server" />
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bank Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBank" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Please Enter Bank Name"
                                                OnTextChanged="txtBank_TextChanged"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtBank"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                ServiceMethod="GetBankDetails" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="rfvLedgerName" runat="server" ControlToValidate="txtBank"
                                                Display="None" ErrorMessage="Please Enter Bank Name" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <%--  <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtBank"
                                                        WatermarkText="Press space to get bank name." WatermarkCssClass="watermarked" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Account No.</label>
                                            </div>
                                            <asp:TextBox ID="txtAccountCode" runat="server" ToolTip="Please Enter Account No."
                                                CssClass="form-control" ValidationGroup="submit" TabIndex="2" MaxLength="15"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ft" runat="server"
                                                FilterMode="InvalidChars" InvalidChars=".!@#$%^&*=+';:/?'" TargetControlID="txtAccountCode">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAccountCode" runat="server" ErrorMessage="Please Enter Account No."
                                                ControlToValidate="txtAccountCode" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Account Name</label>
                                            </div>
                                            <asp:TextBox ID="txtAccountName" runat="server" ToolTip="Please Enter Account Name"
                                                CssClass="form-control" ValidationGroup="submit" TabIndex="3" MaxLength="70"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterMode="InvalidChars" InvalidChars=".!@#$%^&*=+';:/?'" TargetControlID="txtAccountName">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please Enter Account Name"
                                                ControlToValidate="txtAccountName" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowChkFrom" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Chq. From No.</label>
                                            </div>
                                            <asp:TextBox ID="txtChqFrmNo" runat="server" Style="text-align: right" TabIndex="4"
                                                ToolTip="Please Enter Cheque From No." CssClass="form-control" MaxLength="70"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvchqfrmno" runat="server" ErrorMessage="Please Enter Cheque From No."
                                                ControlToValidate="txtChqFrmNo" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowChkTo" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Chq. To No.</label>
                                            </div>
                                            <asp:TextBox ID="txtChqToNo" runat="server" Style="text-align: right" TabIndex="5"
                                                ToolTip="Please Enter Cheque To No." CssClass="form-control" MaxLength="70"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvchqtono" runat="server" ErrorMessage="Please Enter Cheque To No."
                                                ControlToValidate="txtChqToNo" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowChkCurrent" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Current Chq. No.</label>
                                            </div>
                                            <asp:TextBox ID="txtChqCurNo" runat="server" Style="text-align: right" TabIndex="6"
                                                ToolTip="Please Enter Current Cheque No." CssClass="form-control" MaxLength="9"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvchqcurno" runat="server" ErrorMessage="Please Enter Current Cheque No."
                                                ControlToValidate="txtChqCurNo" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowChkIssueDate" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Chq. Book Issue Dt.</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtChqDate" runat="server" ToolTip="Please Enter Cheque Issue Date"
                                                    CssClass="form-control" TabIndex="7" MaxLength="70" />
                                                <asp:RequiredFieldValidator ID="rfvchqissuedate" runat="server" ErrorMessage="Please Enter Cheque Issue Date"
                                                    ControlToValidate="txtChqDate" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtChqDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChqDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>End Status</label>
                                            </div>
                                            <asp:CheckBox ID="chkstatus" runat="server" Text="&nbsp;&nbsp;No" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                        CssClass="btn btn-primary" TabIndex="8" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="9" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                </div>
                                <div class="form-group col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Search</label>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True"
                                                Text="" ToolTip="Please Enter Group Name" CssClass="form-control" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>

                                        <div class="mt-2">
                                            <asp:ListBox ID="lstBankName" runat="server" AutoPostBack="True" Rows="20" CssClass="form-control"  style="height:300px!important;" OnSelectedIndexChanged="lstBankName_SelectedIndexChanged"></asp:ListBox>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function checkStatus(txt) {
            if (txt.value == 'c' || txt.value == 'C' || txt.value == 'D' || txt.value == 'd')
                txt.value = txt.value.toUpperCase();
            else {
                txt.value = '';
                alert('Please Enter Status as C for Credit & D for Debit');
                txt.focus = true;
            }
        }

    </script>
 <%--   <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        </script>--%>
</asp:Content>
