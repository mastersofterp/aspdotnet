<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ledgerhead.aspx.cs" Inherits="Account_ledgerhead" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="Custom" Namespace="ASB" Assembly="AutoSuggestBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }

        .CenterPB {
            position: fixed;
            left: 50%;
            top: 50%;
            margin-top: -20px; /* make this half your image/element height */
            margin-left: -20px; /* make this half your image/element width */
            width: auto;
            height: auto;
        }
    </style>
    <%--  <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>
    <script language="javascript" type="text/javascript">
        function ShowLedger() {

            var popUrl = 'AccountMaster.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;

        }

        function ShowGroup() {

            var popUrl = 'maingroup.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
        function ShowRecPayGroup() {

            var popUrl = 'rpgroup.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
        function ShowMainGroup() {

            var popUrl = 'maingroup.aspx?pageno=1912';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
        function TransactionCheck() {
            if (confirm("Transaction is available in this ledger, Still you want to update?") == true)
                return true;
            else
                return false;
        }
    </script>

    <%--  <script type="text/javascript">   // Added By Akshay Dixit on 10-05-2022
          function SHOPOPUP(vall) {
              //debugger;
              //var myArr = new Array();
              //myString = "" + vall.id + "";
              //myArr = myString.split("_");
              //var index = myArr[0] + '_' + myArr[1] + '_' + myArr[2] + '_' + myArr[3] + '_' + 'hdBillNo';
              //var Id = document.getElementById(index).value;
              //var Id = document.getElementById(index).innerText;
              var popUrl = 'maingroup.aspx?';
              var name = 'popUp';
              var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                    'status=no,toolbar=no,titlebar=no,' +
                                    'left=50,top=35,width=900px,height=650px';
              var openWindow = window.open(popUrl, name, appearence);
              //var openWindow = window.showModalDialog(popUrl, name, appearence);
              openWindow.focus();
              return false;
          }
       </script>    --%>

    <%--   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("#lvCompany [id*=chkall]").click(function () {
                if ($(this).is(":checked")) {
                    $("#lvCompany [id*=chk]").attr("checked", "checked");
                } else {
                    $("#lvCompany [id*=chk]").removeAttr("checked");
                }
            });
            $("#lvCompany [id*=chk]").click(function () {
                if ($("#lvCompany [id*=chk]").length == $("#lvCompany [id*=chk]:checked").length) {
                    $("#lvCompany [id*=chkall]").attr("checked", "checked");
                } else {
                    $("#lvCompany [id*=chkall]").removeAttr("checked");
                }
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function CloseMe() {
            window.close();
            return false;
        }

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }


        function CheckNumeric(obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46) {
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
            var appearence = 'dependent=yes,menubar=no,resizable=no,' + 'status=no,toolbar=no,titlebar=no,' + 'left=100,top=50,width=600px,height=300px';
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
    <%-- <div style="z-index: 1; position: fixed; left: 600px;" class="CenterPB">
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
    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>

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
                            <h3 class="box-title">LEDGER HEAD</h3>
                        </div>
                        <div class="box-body">
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <%-- <div class="sub-heading">
                                            <h5>Create Ledgers</h5>
                                        </div>--%>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:HiddenField ID="hdnPartyNo" runat="server" OnValueChanged="hdnPartyNo_ValueChanged" />
                                            <label><span style="color: red">*</span> Ledger Name  </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtLedgerName" runat="server" TabIndex="1" TextMode="MultiLine"
                                                ToolTip="Please Enter Ledger Name" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLedgerName" runat="server" ControlToValidate="txtLedgerName"
                                                Display="None" ErrorMessage="Please Enter Ledger Name" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                            <label><span style="color: red">*</span> Account Code  </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtAccountCode" runat="server" AutoPostBack="True" Enabled="true"
                                                OnTextChanged="txtAccountCode_TextChanged" TabIndex="2" ToolTip="Please Enter Account Code" CssClass="form-control"
                                                ValidationGroup="submit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAccountCode" runat="server" ControlToValidate="txtAccountCode"
                                                Display="None" ErrorMessage="Please Enter Account Code" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Address : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtAddress" runat="server" TabIndex="3" ToolTip="Please Enter Address"
                                                ValidationGroup="submit" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Nature of Work :</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtNatureWork" runat="server" TabIndex="4" ToolTip="Please Enter Nature of Work"
                                                ValidationGroup="submit" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="Div5" class="row" runat="server">
                                        <div class="col-md-2">
                                            <label>TIN No. : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtTinNo" runat="server" TabIndex="3" ToolTip="Please Enter TIN NO."
                                                ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>PAN No. : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtPanNo" runat="server" TabIndex="3" ToolTip="Please Enter PAN NO."
                                                ValidationGroup="submit" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="Div6" class="row" runat="server">
                                            <%--Started  Pawan Nikhare : 27/10/2023--%>
                                         <div class="col-md-2">
                                            <label>GST No. : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtGSTtNo" runat="server" MaxLength="15"
                                                TabIndex="4" ToolTip="Please Enter GST No." CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                TargetControlID="txtGSTtNo" ValidChars="0123456789">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                         <%--Ended  Pawan Nikhare : 27/10/2023--%>


                                        <div class="col-md-2">
                                            <label>Contact No. : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtContactNo" runat="server" MaxLength="15"
                                                TabIndex="4" ToolTip="Please Enter Contact No." CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeContactNo" runat="server" FilterMode="ValidChars"
                                                TargetControlID="txtContactNo" ValidChars="0123456789">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                       
                                    </div>
                                    <br />
                                    <div id="Div1" class="row" runat="server">
                                        
                                        <div class="col-md-2">
                                            <label>Opening Balance  </label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtOpenBalance" runat="server" onblur="EnableDisableStatus(this);"
                                                TabIndex="5" ToolTip="Please Enter Opening Balance" MaxLength="15"
                                                ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbOpenBal" runat="server" FilterMode="ValidChars"
                                                TargetControlID="txtOpenBalance" ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:DropDownList ID="ddlDrCr" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Selected="True" Value="D">Dr</asp:ListItem>
                                                <asp:ListItem Value="C">Cr</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlDrCr"
                                                Display="None" ErrorMessage="Please Select Status." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="Div7" class="row" runat="server">
                                        <div class="col-md-2">
                                            <label><span style="color: red">*</span> Final Account Group  </label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlFAGroup" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlFAGroup_SelectedIndexChanged" TabIndex="7" ValidationGroup="submit"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFAGroup" runat="server" ControlToValidate="ddlFAGroup"
                                                Display="None" ErrorMessage="Please Select Final Account Group" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnMainGrp" runat="server" OnClick="btnMainGrp_Click" TabIndex="13"
                                                Text="Open" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            <%--OnClientClick="SHOPOPUP(this);"--%>
                                        </div>
                                        <div id="trbnkacc" class="row" runat="server">
                                            <div class="col-md-3">
                                                <label>Bank Account Name : </label>
                                            </div>
                                            <div class="col-md-8">

                                                <asp:TextBox ID="txtBankAc" runat="server" CssClass="form-control" AutoPostBack="true" ToolTip="Please Enter Bank Name" OnTextChanged="txtBankAc_TextChanged"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtBankAc"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>

                                                <%--  <Custom:AutoSuggestBox ID="txtBankAc" runat="server" DataType="BankAccount" ResourcesDir="AutoSuggestBox"
                                                                    TabIndex="8" ToolTip="Press space to get bank accounts, if not available please open an bank account and attached it."
                                                                    CssClass="form-control"></Custom:AutoSuggestBox>
                                                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtBankAc"
                                                                    WatermarkCssClass="watermarked" WatermarkText="Press space to get bank accounts." />--%>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lnkBtnBankAcc" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnBankAcc_Click">Open</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <%-- <div id="Div8" class="row" runat="server">
                                                    </div>
                                                    <br />
                                                    <div id="Div9" class="row" runat="server">
                                                    </div>--%>
                                    <div id="trRPGroup" class="row" runat="server">
                                        <div class="col-6">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><span style="color: red">*</span> Receipt Group  </label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="ddlRPGroup" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="9" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRPGroup" runat="server" ControlToValidate="ddlRPGroup"
                                                        Display="None" ErrorMessage="Please Select Receipt Group" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnRecPayGrp" runat="server" OnClick="btnRecPayGrp_Click" TabIndex="13"
                                                        Text="Open" ValidationGroup="submit" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                        <div id="trPGroup" class="col-6" runat="server">

                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><span style="color: red">*</span>  Payment Group </label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="ddlPGroup" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="9" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPGroup"
                                                        Display="None" ErrorMessage="Please Select Payment Group" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnRecPayGrp1" runat="server" OnClick="btnRecPayGrp_Click" TabIndex="13"
                                                        Text="Open" ValidationGroup="submit" CssClass="btn btn-primary" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <br />


                                    <div id="trSalePurchase" runat="server" visible="false">
                                        <div class="col-md-2">
                                            <label>Sale/Purchase Group : </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlSalePurchase" runat="server" AppendDataBoundItems="true"
                                                Height="19px" TabIndex="9" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="S">Sale</asp:ListItem>
                                                <asp:ListItem Value="P">Purchase</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="Div14" class="row" runat="server">

                                        <div class="col-md-4">
                                            <asp:CheckBox ID="chkCostCenter" runat="server" Checked="false" Text="Apply Cost Center" /><br />
                                            <asp:CheckBox ID="chkDefault" runat="server" Checked="false" Text="Set Default Ledger" />
                                            <asp:CheckBox ID="chkBudgetHead" runat="server" Checked="False" Text="Is Budget Head applicable" />
                                        </div>
                                    </div>
                                    <br />

                                    <div id="divpnl" class="row" runat="server" visible="false">
                                        <div class="col-12">
                                            <asp:Panel ID="pnlCompany" runat="server" ScrollBars="Vertical">
                                                <asp:ListView ID="lvCompany" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading">
                                                                <h5>COMPANY NAME LIST</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>

                                                                        <th>
                                                                            <asp:CheckBox ID="chkall" OnCheckedChanged="chkall_OnCheckedChanged" runat="server"
                                                                                AutoPostBack="true" />
                                                                        </th>
                                                                        <th>Company Name
                                                                        </th>
                                                                        <th>Opening Balance
                                                                        </th>
                                                                        <th>Debit/Credit
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
                                                                <asp:CheckBox ID="chk" runat="server" ToolTip=' <%# Eval("COMPANY_Code")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCompanyName" Text='<%# Eval("COMPANY_NAME")%>' runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOpeningBal" runat="server" CssClass="form-control" ToolTip="Enter Opening Balance"
                                                                    Text="0"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbOpeningBal" runat="server" FilterMode="ValidChars"
                                                                    TargetControlID="txtOpeningBal" ValidChars="0123456789.">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="grdddlDrCr" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="D" Text="Debit"></asp:ListItem>
                                                                    <asp:ListItem Value="C" Text="Credit"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="13"
                                                Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" />

                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                                TabIndex="14" Text="Cancel" CssClass="btn btn-warning" />

                                            <asp:Button ID="btnPanReport" runat="server" CausesValidation="false" OnClick="btnPanReport_Click"
                                                TabIndex="14" Text="PAN/TIN No Report" CssClass="btn btn-info" Visible="false" />

                                            <asp:Button ID="btnBack" runat="server" CausesValidation="false" OnClick="btnBack_Click"
                                                Text="Close" Visible="False" CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Search Criteria.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"
                                                        Text="" ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>

                                                    <div class="mt-2">
                                                        <asp:ListBox ID="lstLedgerName" runat="server" AutoPostBack="True" CssClass="form-control "
                                                            OnSelectedIndexChanged="lstLedgerName_SelectedIndexChanged" Rows="20" Style="height: 300px!important"></asp:ListBox>
                                                        <ajaxToolKit:ListSearchExtender ID="lstLedgerName_ListSearchExtender" runat="server"
                                                            Enabled="True" TargetControlID="lstLedgerName">
                                                        </ajaxToolKit:ListSearchExtender>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>



                                    <div class="row mb-4">
                                        <div class=" col-12">
                                            <div class="sub-heading">
                                                <h5>Total Opening Balances</h5>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Total Debit :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTotDr" Text="0.00" Font-Bold="true" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Total Credit :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTotCr" Text="0.00" Font-Bold="true" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Difference :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDiff" Text="0.00" Font-Bold="true" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>


                                    <div class="col-md-4" id="tdFAgroup" runat="server" style="display: none">
                                        <asp:Panel ID="panel2" runat="server" ScrollBars="Both" Height="480px" Width="300px" BorderStyle="Outset">
                                            <b>Final Account Head Hierarchy </b>
                                            <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows" ExpandDepth="5" CssClass="form-control">
                                            </asp:TreeView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
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

    </script>
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
        function EnableDisableStatus(txtOpeningBalance) {
            var openingbalance = Number(txtOpeningBalance.value);
            var status = (document.getElementById("ctl00_ctp_trStatus"));
            var x = document.getElementById("ctl00_ctp_rfvStatus");
            var x1 = document.getElementById("ctl00_ctp_ddlDrCr");
            if (openingbalance > 0) {
                x1.disabled = false;
                x.disabled = false;
            }
            else {
                x1.disabled = true;
                x.disabled = true;
            }
        }
    </script>
    </div>
</asp:Content>
