<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PayeeMaster.aspx.cs" Inherits="PayeeMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="Custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 250px;
        }
    </style>
    <%--  <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>
     <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function SetCurrentChequeNo(obj) {
            document.getElementById('ctl00_ContentPlaceHolder1_txtChqCurNo').value = obj.value;
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
   <%-- <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>

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
                            <h3 class="box-title">PAYEE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 mb-3">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                   <%-- <div class="sub-heading">
                                        <h5>Add/Modify - Payee Master</h5>
                                    </div>--%>
                                    <div class="row">
                                        <asp:HiddenField ID="hdnPartyNo" runat="server" />
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Payee Name </label>
                                            </div>
                                            <asp:TextBox ID="txtPayee" runat="server" TabIndex="1" placeholder="Please Enter Payee Name" ToolTip="Please Enter Payee Name" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtPayee" Display="None" ErrorMessage="Please Enter Payee Name" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Pan No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPanNo" runat="server" Style="text-transform: uppercase" MaxLength="10" TabIndex="2" placeholder="Enter Pan Card No." ToolTip="Please Enter Pan No" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Payee Nature </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPayeeNature" ToolTip="Please Select Payee Nature" ValidationGroup="submit" TabIndex="3" CssClass="form-control" data-select2-enable="true" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlPayeeNature_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                ControlToValidate="ddlPayeeNature" Display="None" InitialValue="0" ErrorMessage="Please Select Nature" SetFocusOnError="True" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Address</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" TabIndex="4" placeholder="Please Enter Address" ToolTip="Please Enter Address" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBank" ToolTip="Please Select Bank" ValidationGroup="submit" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="ddlBank" Display="None" ErrorMessage="Please Select Bank." SetFocusOnError="True" InitialValue="0"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bank A/C No.</label>
                                            </div>
                                            <asp:TextBox ID="txtAccountCode" runat="server" TabIndex="6" placeholder="Enter Bank Account No" ToolTip="Please Enter Bank Account No." ValidationGroup="submit"
                                                MaxLength="20" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter A/C No" Display="None"
                                                ControlToValidate="txtAccountCode" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>IFSC Code</label>
                                            </div>
                                            <asp:TextBox ID="txtIfsc" runat="server" TabIndex="7" placeholder="Enter IFSC Code" ToolTip="Please Enter IFSC." ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter IFSC Code" Display="None" ControlToValidate="txtIfsc" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:TextBox ID="txtBranch" runat="server" TabIndex="8" placeholder="Please Enter Branch" ToolTip="Please Enter Branch." ValidationGroup="submit"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Branch" Display="None" ControlToValidate="txtBranch" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Contact Number </label>
                                            </div>
                                            <asp:TextBox ID="txtContactNo" runat="server" TabIndex="7" placeholder="Enter Contact Number" ToolTip="Please Enter Contact Number." ValidationGroup="submit" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfContactNo" runat="server" ErrorMessage="Please Enter Contact Number" Display="None" ControlToValidate="txtContactNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" ValidationGroup="submit" ControlToValidate="txtContactNo" ErrorMessage="Please Enter Valid Contact Number"
                                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Email Id </label>
                                            </div>
                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="8" placeholder="Please Enter Email Id" ToolTip="Please Enter Email Id." ValidationGroup="submit"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ErrorMessage="Please Enter Email Id" Display="None" ControlToValidate="txtEmail" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True"
                                                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                ValidationGroup="submit"></asp:RegularExpressionValidator>

                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Ledger Name </label>
                                            </div>
                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Please Enter Ledger Name" Enabled="false"
                                                AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                            <asp:HiddenField ID="hdnOpartyManual" runat="server" Value="0" />
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Ledger Name" ControlToValidate="txtAcc" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                                ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:Label ID="lblCurBal" runat="server"></asp:Label>
                                            <asp:Label ID="txtmd" runat="server"></asp:Label>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Cancelled</label>
                                            </div>
                                            <asp:CheckBox ID="chkstatus" runat="server" TabIndex="10" Text="&nbsp;&nbsp;No" />

                                        </div>
                                    </div>
                                </div>
                                <div class=" col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                        CssClass="btn btn-primary" TabIndex="11" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel Report" CssClass="btn btn-info" TabIndex="13" OnClick="btnExcel_Click" />
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            CssClass="btn btn-warning" TabIndex="12" OnClick="btnCancel_Click" />
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                </div>
                                <div class="col-12 text-center">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                                <div class="col-12 mb-4">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Search</label>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True"
                                              Text="" ToolTip="Please Enter Group Name" TabIndex="14" OnTextChanged="txtSearch_TextChanged"
                                                CssClass="form-control"></asp:TextBox>
                                        <div class="mt-2">
                                            <asp:ListBox ID="lstBankName" runat="server" AutoPostBack="True" TabIndex="15"
                                                Rows="20" CssClass="form-control"
                                                OnSelectedIndexChanged="lstBankName_SelectedIndexChanged" style="height:300px!important;"></asp:ListBox>
                                        </div>
                                    </div>

                                </div>



                            </asp:Panel>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
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
  <%--  <script type="text/javascript">
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
