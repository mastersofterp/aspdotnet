<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ModifyPaySlip.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_ModifyPaySlip"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SESSION CREATION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Modify PaySlip</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlSelect" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month &amp; Year</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMonthYear" runat="server" CssClass="form-control" ValidationGroup="payroll" TabIndex="1"
                                                        onblur="return checkdate(this);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                        Display="None" ErrorMessage="Please select Month And Year in (MM/YYYY Format)"
                                                        SetFocusOnError="True" ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select College" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll" InitialValue="0"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                    Display="None" ErrorMessage="Please Select Staff" ValidationGroup="payroll" InitialValue="0"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Employee</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmployeeNo" runat="server" TabIndex="4" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmployeeNo"
                                                    Display="None" ErrorMessage="Please Select Employee" ValidationGroup="payroll"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="5"
                                            OnClick="btnShow_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <div id="preloader">
                                                    <div id="loader-img">
                                                        <div id="loader">
                                                        </div>
                                                        <p class="saving">Processing! Please wait<span>.</span><span>.</span><span>.</span></p>
                                                    </div>
                                                </div>                                                
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlMonthlyChanges" runat="server">
                                    <asp:Panel ID="pnlBasicDetails" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblEmpName" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Date of Increment :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDOI" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Scale :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblScale" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Account No. :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAccNo" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Designation :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDesignation" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Basic :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBasic" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-md-6 col-12">
                                                <asp:Panel ID="pnlEarningHeads" runat="server">
                                                    <asp:ListView ID="lvEarningHeads" runat="server">
                                                        <EmptyDataTemplate>
                                                            No Rows In Earning Heads
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Basic</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtBasicPay" runat="server" CssClass="form-control" onkeyup="return Amount(this);"
                                                                            ToolTip="BASIC"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="txtBasicPay"
                                                                            Display="None" ErrorMessage="Don't keep blank amount in Basic (Enter 0)"
                                                                            ValidationGroup="payroll" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade Pay</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control" onkeyup="return Amount(this);"
                                                                            ToolTip="GRADEPAY"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGrade"
                                                                            Display="None" ErrorMessage="Don't keep blank amount in Grade Pay (Enter 0)"
                                                                            ValidationGroup="payroll" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>INCOME HEADS
                                                                        </th>
                                                                        <th>AMOUNT
                                                                        </th>
                                                                    </tr>
                                                                    <thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEarHead" runat="server" Text='<%# Eval("PAYSHORT")%>' ToolTip='<%# Eval("PayHead")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEAmount" runat="server" MaxLength="10" CssClass="form-control" onkeyup="return Amount(this);"
                                                                        Text='<%# Eval("amt")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="txtEAmount"
                                                                        Display="None" ErrorMessage="Please Don't keep blank amount in Income Payhead (Enter 0)"
                                                                        ValidationGroup="payroll" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group col-md-6 col-12">
                                                <asp:Panel ID="PnlDeductionHeads" runat="server">
                                                    <asp:ListView ID="lvDeductionHeads" runat="server">                                                        
                                                        <EmptyDataTemplate>
                                                            No Rows In Deduction Heads
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
	                                                            <h5>Total Deduction</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>DEDUCTION HEADS
                                                                        </th>
                                                                        <th>AMOUNT
                                                                        </th>
                                                                    </tr>
                                                                    <thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDedHead" runat="server" Text='<%# Eval("PAYSHORT")%>' ToolTip='<%# Eval("PayHead")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDAmount" runat="server" MaxLength="10" CssClass="form-control" onkeyup="return Amount(this);"
                                                                        Text='<%# Eval("amt")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="txtDAmount"
                                                                        Display="None" ErrorMessage="Please Don't keep blank amount in Deduction Payhead (Enter 0)"
                                                                        ValidationGroup="payroll" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Total Gross</label>
                                                </div>
                                                <asp:TextBox ID="txtGrossPay" runat="server" ToolTip="Enter Total Gross" TabIndex="6" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total Deduction</label>
                                            </div>
                                            <asp:TextBox ID="txtTotalDeduction" runat="server" ToolTip="Enter Toatl Deduction" TabIndex="7" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Net Pay</label>
                                            </div>
                                            <asp:TextBox ID="txtNetPay" runat="server" TabIndex="8" ToolTip="Enter Net Pay" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Click To Save" TabIndex="9" ValidationGroup="payroll" CssClass="btn btn-success"
                                            OnClick="btnSub_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to Reset" TabIndex="10" CausesValidation="false"
                                            OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                                    </div>
                                </asp:Panel>
                                <input type="hidden" id="hidEarningRecordsCount" runat="server" />
                                <input type="hidden" id="hidDeductionRecordsCount" runat="server" />
                            </asp:Panel>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">


        function ConfirmMessage() {
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead").value;
            var hidPayhead = document.getElementById("ctl00_ContentPlaceHolder1_hidPayhead").value;

            if (confirm("Do you want to save changes in " + hidPayhead + "->" + payHead)) {
                return true;
            }
            else {
                return false;
            }
        }


        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greate than 0 and less than 13");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        ; debugger
        function Amount(txt) {

            if (ValidateNumeric(txt) == true) {
                var Income_Amt = 0.00, Ded_Amt = 0.00, BasicPay = 0.00, GradePay = 0.00, GrossAmount = 0.00, Tot_Ded = 0.00, NetPay = 0.00;

                BasicPay = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_txtBasicPay").value;
                GradePay = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_txtGrade").value;
                GrossAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtGrossPay").value;
                Tot_Ded = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeduction").value;
                NetPay = document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value;

                for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidEarningRecordsCount").value) - 1; i++) {
                    Income_Amt = Number(Income_Amt) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_ctrl" + i + "_txtEAmount").value);
                }

                for (j = 0; j <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidDeductionRecordsCount").value) - 1; j++) {
                    Ded_Amt = Number(Ded_Amt) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_ctrl" + j + "_txtDAmount").value);
                }
                document.getElementById("ctl00_ContentPlaceHolder1_txtGrossPay").value = Number(Income_Amt) + Number(BasicPay) + Number(GradePay);
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeduction").value = Number(Ded_Amt).toFixed(2);
                document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtGrossPay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeduction").value);
            }

        }


        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }

        }

    </script>

</asp:Content>
