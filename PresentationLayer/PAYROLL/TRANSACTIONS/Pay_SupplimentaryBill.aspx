<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_SupplimentaryBill.aspx.cs" Inherits="PayRoll_Transactions_Pay_SupplimentaryBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SUPPLEMENTARY BILL</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" CssClass="btn btn-primary" Text="Add New"
                                    OnClick="btnAdd_Click" TabIndex="1" ToolTip="Click here to AddNew Supplementary Bill"></asp:LinkButton>
                            </div>

                            <asp:Panel ID="pnlSupplimentaryBillDetails" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Supplementary Bill Details</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color: red">*</span>Supli.Bill Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalSupliBillDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtSupliBillDate" runat="server" CssClass="form-control" TabIndex="2"
                                                    ToolTip="Enter Suplimentry Bill Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cetxtSupliBillDate" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalSupliBillDate"
                                                    TargetControlID="txtSupliBillDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtSupliBillDate" runat="server" ControlToValidate="txtSupliBillDate"
                                                    Display="None" ErrorMessage="Please Select  Supli.Bill Date in(dd/mm/yyyy Format)"
                                                    SetFocusOnError="True" ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="metxtSupliBillDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtSupliBillDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevtxtSupliBillDate" runat="server" ControlExtender="metxtSupliBillDate"
                                                    ControlToValidate="txtSupliBillDate" Display="None" EmptyValueBlurredText="Empty"
                                                    EmptyValueMessage="Please Enter  Supli.Bill Date" InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Please Select Supli.Bill Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="True" TooltipMessage="Please Enter  Supli.Bill Date" ValidationGroup="payroll" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color: red">*</span>Supli.Bill Head</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSupliBillHead" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                ToolTip="Enter Suplimentry Bill Head" TabIndex="3">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlSupliBillHead" runat="server" ControlToValidate="ddlSupliBillHead"
                                                Display="None" ErrorMessage="Please Select Supli.Bill Head" SetFocusOnError="True"
                                                ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color: red">*</span>Order No.</label>
                                            </div>
                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Enter Order NO"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                                Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Order No." ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color: red">*</span>Emp.Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="5" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Employee Name">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmpName" runat="server" ControlToValidate="ddlEmpName"
                                                Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Designation</label>
                                            </div>
                                            <asp:Label ID="lblDesignation" runat="server" CssClass="form-control" Enabled="false" Font-Bold="true" TabIndex="6"
                                                ToolTip="Designations of Employee"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:Label ID="lblDepartMent" runat="server" CssClass="form-control" Font-Bold="true" TabIndex="7"
                                                ToolTip="Department of Employee" Enabled="false"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic Pay</label>
                                            </div>
                                            <asp:TextBox ID="txtSupliPay" runat="server" CssClass="form-control" Text="0.00" MaxLength="10"
                                                ToolTip="Enter Suplimentry Pay" onkeyup="return Amount(this);" TabIndex="8"></asp:TextBox>
                                            <%-- <asp:Button runat="server" ID="butcalculate" Text="Calculate" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>D.Pay</label>
                                            </div>
                                            <asp:TextBox ID="txtDPay" runat="server" CssClass="form-control" Text="0.00" MaxLength="10"
                                                ToolTip="Enter D.Pay" onkeyup="return Amount(this);" TabIndex="9"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Grade Pay</label>
                                            </div>
                                            <asp:TextBox ID="txtGradePay" runat="server" CssClass="form-control" MaxLength="10" Text="0.00"
                                                ToolTip="Enter Grade Pay" onkeyup="return Amount(this);" TabIndex="10"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Gross Salary</label>
                                            </div>
                                            <asp:TextBox ID="txtGrossSalary" runat="server" CssClass="form-control" Text="0.00" Enabled="false"
                                                ToolTip="Gross Salary" TabIndex="11"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total Deductions</label>
                                            </div>
                                            <asp:TextBox ID="txtTotalDeductions" runat="server" CssClass="form-control" Text="0.00" Enabled="false"
                                                ToolTip="Total Deductions" TabIndex="12"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>NetPay</label>
                                            </div>
                                            <asp:TextBox ID="txtNetPay" runat="server" CssClass="form-control" Text="0.00" Enabled="false"
                                                ToolTip="Net Pay" TabIndex="13"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color: red">*</span>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="250" CssClass="form-control" TabIndex="14"
                                                ToolTip="Enter Remark"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                                Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="payroll" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Add In Income Tax</label>
                                            </div>
                                            <asp:CheckBox ID="chkAddInIncomeTax" runat="server" TabIndex="15"
                                                ToolTip="If Check box is checked then add in income tax other wise not add in income tax" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Add In Other Income</label>
                                            </div>
                                            <asp:CheckBox ID="chkAddInOthIncome" runat="server" TabIndex="15"
                                                ToolTip="If Check box is checked then add in Other Income other wise add in Gross" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Percentage</label>
                                            </div>
                                            <asp:TextBox ID="txtPersentage" runat="server" MaxLength="2" CssClass="form-control" TabIndex="16" onkeypress="return ValidateNumeric(this)"
                                                ToolTip="Enter Income Tax Percentage"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnPersentage" runat="server" Text="Calculate" OnClick="btnPersentage_Click" CssClass="btn btn-primary" TabIndex="17" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <asp:Panel ID="pnlEarningHeads" runat="server">
                                            <asp:ListView ID="lvEarningHeads" runat="server">
                                                <EmptyDataTemplate>
                                                    No Rows In Earning Heads
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Earning Heads</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>PayHead
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("PAYSHORT")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txEarningsAmt" runat="server" MaxLength="10" TabIndex="18" CssClass="form-control" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <asp:Panel ID="PnlDeductionHeads" runat="server">

                                            <asp:ListView ID="lvDeductionHeads" runat="server">
                                                <EmptyDataTemplate>
                                                    No Rows In Deduction Heads
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Deduction Heads</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>PayHead
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("PAYSHORT")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txDeductionAmt" runat="server" CssClass="form-control" TabIndex="19" MaxLength="10" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer mt-4">
                                <asp:Button ID="butSubmit" TabIndex="20" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                    OnClick="butSubmit_Click" ToolTip="Click here to Save" />
                                <asp:Button ID="butCancel" runat="server" Text="Cancel" OnClick="butCancel_Click" TabIndex="21"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12" id="divOrdDetails" runat="server">
                                <%-- <div id="divOrdDetails" runat="server">--%>
                                <asp:Panel ID="pnl1" runat="server" Visible="true">
                                    <asp:ListView ID="lvOrderDetails" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Supplementary Bill Order Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Ord.No
                                                        </th>
                                                        <th>Supli.Head
                                                        </th>
                                                        <th>Ord.Date
                                                        </th>
                                                        <th>Tot.Dec
                                                        </th>
                                                        <th>Gross Salary
                                                        </th>
                                                        <th>NetPay
                                                        </th>
                                                        <th>DP.Amt
                                                        </th>
                                                        <th>GradePay
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEditOrderDetails" runat="server" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("ORDNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEditOrderDetails_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("ORDNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUPLHEAD")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ORDATE", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTDED")%>
                                                </td>
                                                <td>
                                                    <%# Eval("GS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NETPAY")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DPAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("GRADEPAY")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <%--</div>--%>
                                <asp:Panel ID="pnlSupplBillDetail" runat="server">
                                    <div class="col-12">
                                        <asp:ListView ID="lvSuplBillDtl" runat="server">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="d-block text-center mt-3" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Supplementary Bill Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Emp. Code</th>
                                                            <th>Employee Name
                                                            </th>
                                                            <th>Ord.No
                                                            </th>
                                                            <th>Supli.Head
                                                            </th>
                                                            <th>Ord.Date
                                                            </th>
                                                            <th>Tot.Dec
                                                            </th>
                                                            <th>Gross Salary
                                                            </th>
                                                            <th>NetPay
                                                            </th>
                                                            <th>DP.Amt
                                                            </th>
                                                            <th>GradePay
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center">
                                                        <asp:ImageButton ID="btnEditSuplBill" runat="server" ImageUrl="~/Images/edit.png"
                                                            CommandArgument='<%# Eval("SUPLTRXID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEditSuplBill_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("PFILENO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORDNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUPLHEAD")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SBDATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOT_DED")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("GS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NET_PAY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DPAMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("GRADEPAY")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                <input type="hidden" id="hidTotalDeductionRecordsCount" runat="server" />
                <input type="hidden" id="hidEarningRecordsCount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function Amount(txt) {


            if (ValidateNumeric(txt) == true) {
                var DeductionAmount = 0.00, EarningsAmount = 0.00, SupliPay = 0.00, Dpay = 0.00;
                var GradePay = 0.00, GrossSalary = 0.00, NetPay = 0.00, TotalDeductions = 0.00;

                GradePay = document.getElementById("ctl00_ContentPlaceHolder1_txtGradePay").value;
                SupliPay = document.getElementById("ctl00_ContentPlaceHolder1_txtSupliPay").value;
                Dpay = document.getElementById("ctl00_ContentPlaceHolder1_txtDPay").value;
                GrossSalary = document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value;
                NetPay = document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value
                TotDeducation = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeductions").value

                //           if(Dpay > 0)
                //           {
                //             document.getElementById("ctl00_ContentPlaceHolder1_txtGradePay").value=0.00
                //           }           
                //           
                //           if(GradePay > 0)
                //           {
                //             document.getElementById("ctl00_ContentPlaceHolder1_txtDPay").value=0.00
                //           }


                for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidTotalDeductionRecordsCount").value) - 1; i++) {
                    DeductionAmount = Number(DeductionAmount) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_ctrl" + i + "_txDeductionAmt").value);
                }

                for (j = 0; j <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidEarningRecordsCount").value) - 1; j++) {
                    EarningsAmount = Number(EarningsAmount) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_ctrl" + j + "_txEarningsAmt").value);
                }

                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeductions").value = DeductionAmount;
                document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value = Number(EarningsAmount) + Number(GradePay) + Number(SupliPay) + Number(Dpay);
                document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value) - Number(DeductionAmount);
            }

        }



        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }

        }

    </script>

</asp:Content>
