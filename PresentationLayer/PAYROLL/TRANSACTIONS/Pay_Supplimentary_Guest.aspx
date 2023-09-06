<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Supplimentary_Guest.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Supplimentary_Guest" %>

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
                                <asp:Panel ID="pnlSupplimentaryBillDetails" runat="server">
                                        <div class="col-12">
	                                        <div class="row">
		                                        <div class="col-12">
		                                        <div class="sub-heading">
				                                        <h5>Supplementary Guest Details</h5>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Financial Year</label>
								                    </div>
                                                    <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                        ToolTip="Select Financial Year" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFinYear"
                                                        Display="None" ErrorMessage="Select Financial Year" SetFocusOnError="True"
                                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                          
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>PAN No.</label>
								                    </div>
                                                     <asp:TextBox ID="txtPanNo" MaxLength="10" AutoPostBack="true" OnTextChanged="txtPanNo_TextChanged" runat="server" CssClass="form-control" placeholder="Enter PAN No." ToolTip="Enter PAN No." TabIndex="2">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPANNo" runat="server" ControlToValidate="txtPanNo"
                                                        Display="None" ErrorMessage="Please Enter PAN No" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                               
                                                <%--<div class="col-md-2">
                                                            <br />
                                                            <asp:Button ID="btn" TabIndex="17" runat="server" Text="Search" ValidationGroup="payroll" CssClass="btn btn-primary"
                                                                OnClick="btn_Click" ToolTip="Click here to Save" />
                                                        </div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Supli.Bill Head</label>
								                    </div>
                                                     <asp:DropDownList ID="ddlSupliBillHead" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                        ToolTip="Enter Suplimentry Bill Head" TabIndex="3">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSupliBillHead" runat="server" ControlToValidate="ddlSupliBillHead"
                                                        Display="None" ErrorMessage="Please Select Supli.Bill Head" SetFocusOnError="True"
                                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                            
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Order No.</label>
								                    </div>
                                                     <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Enter Order No." CssClass="form-control" TabIndex="4" ToolTip="Enter Order NO">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                                        Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Order No." ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
							                    </div>                                              
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Emp.Name</label>
								                    </div>
                                                    <asp:TextBox ID="txtEmpName" runat="server" placeholder="Enter Employee Name" CssClass="form-control" ToolTip="Enter Code" TabIndex="5"></asp:TextBox>

                                                    <%--OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged"--%>
                                                    <asp:RequiredFieldValidator ID="rfvddlEmpName" runat="server" ControlToValidate="txtEmpName"
                                                        Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                            
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Designation</label>
								                    </div>
                                                     <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="6" data-select2-enable="true"
                                                        AutoPostBack="true" ToolTip="Select Designation ">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                                        Display="None" ErrorMessage="Please Select Designation" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                           
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Department</label>
								                    </div>
                                                     <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="7" data-select2-enable="true"
                                                        AutoPostBack="true" ToolTip="Select Department Name">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                            
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Code</label>
								                    </div>
                                                      <asp:TextBox ID="txtCode" runat="server" placeholder="Enter Code" CssClass="form-control" ToolTip="Enter Code" TabIndex="8"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                                        Display="None" ErrorMessage="Please Enter Code" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                             
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>BANK</label>
								                    </div>
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="9" data-select2-enable="true"
                                                        AutoPostBack="true" ToolTip="Select Bank Name">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="ddlBank"
                                                        Display="None" ErrorMessage="Please Select Bank Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                              
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>ACCOUNT No.</label>
								                    </div>
                                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" placeholder="Enter Account No."
                                                        ToolTip="Enter Bank Account Number" TabIndex="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAccNo" runat="server" ControlToValidate="txtAccNo"
                                                        Display="None" ErrorMessage="Enter Account No" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                             
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>IFSC</label>
								                    </div>
                                                    <asp:TextBox ID="txtIFSC" placeholder="Enter IFSC No." runat="server" CssClass="form-control" ToolTip="Enter IFSC Code" TabIndex="11">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvIFSC" runat="server" ControlToValidate="txtIFSC"
                                                        Display="None" ErrorMessage="Enter IFSC No" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                </div>                                                                                               
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Mobile No.</label>
								                    </div>
                                                    <asp:TextBox ID="txtMobNo" runat="server" CssClass="form-control" placeholder="Enter Mobile No."
                                                        MaxLength="10" ToolTip="Enter Mobile Number" TabIndex="12"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobNo"
                                                        Display="None" ErrorMessage="Enter Mobile Number" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                    </div>                                            
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Email</label>
								                    </div>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email Id"
                                                        ToolTip="Enter Email Number" TabIndex="13"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                                        Display="None" ErrorMessage="Enter Email " ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="payroll" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
							                    </div>                                             
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Supli.Bill Date</label>
								                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="ImaCalSupliBillDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>

                                                        <asp:TextBox ID="txtSupliBillDate" runat="server" CssClass="form-control" TabIndex="14"
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
                                                            InvalidValueMessage="  Supli.Bill Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            SetFocusOnError="True" TooltipMessage="Please Enter  Supli.Bill Date" ValidationGroup="payroll" />
                                                    </div>
							                    </div>                                               
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Remark</label>
								                    </div>
                                                    <asp:TextBox ID="txtRemark" placeholder="Enter Remark" runat="server" MaxLength="250" CssClass="form-control" TabIndex="15" TextMode="MultiLine"
                                                        ToolTip="Enter Remark"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                                        Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="payroll" SetFocusOnError="true"></asp:RequiredFieldValidator>
							                    </div>                                               
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Amount</label>
								                    </div>
                                                     <asp:TextBox ID="txtAmount" placeholder="Enter Amount." runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" Text="0"
                                                        onkeypress="return ValidateNumeric(this)" ToolTip="Enter Amount" TabIndex="16"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                        Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="true"></asp:RequiredFieldValidator>
							                    </div>                                              
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <label>Taxable Amount</label>
								                    </div>
                                                    <asp:TextBox ID="txtNetlAmount" runat="server" CssClass="form-control" Enabled="false" Text="0"
                                                        onkeypress="return ValidateNumeric(this)" OnTextChanged="txtNetlAmount_TextChanged" ToolTip="Enter Total Amount" TabIndex="17"></asp:TextBox>
                                                    <asp:HiddenField runat="server" ID="hdnTotAmt" />
							                    </div>                                             
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>Section</label>
								                    </div>
                                                     <asp:DropDownList ID="ddlsection" runat="server" Enabled="false" CssClass="form-control" AppendDataBoundItems="true" TabIndex="18" data-select2-enable="true"
                                                        AutoPostBack="true" ToolTip="Select Section ">
                                                    </asp:DropDownList>
							                    </div>                                           
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <sup>* </sup>
									                    <label>TDS %</label>
								                    </div>
                                                    <asp:TextBox ID="txtPersentage" placeholder="Enter TDS Percentage" runat="server" CssClass="form-control" Enabled="false" Text="0"
                                                        MaxLength="2" AutoPostBack="true" OnTextChanged="txtPersentage_TextChanged" onkeypress="return ValidateNumeric(this)" ToolTip="Enter Percentage" TabIndex="19"></asp:TextBox>
							                    </div>                                           
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <label>TDS Amount</label>
								                    </div>
                                                     <asp:TextBox ID="txtTDSAmt" runat="server" CssClass="form-control" Enabled="false" Text="0"
                                                        onkeypress="return ValidateNumeric(this)" TabIndex="20"></asp:TextBox>
							                    </div>                                            
                                                <div class="form-group col-lg-3 col-md-6 col-12">
								                    <div class="label-dynamic">
									                    <label>Net Amount</label>
								                    </div>
                                                     <asp:TextBox ID="txtTDSPaidAmt" runat="server" CssClass="form-control" Enabled="false" Text="0"
                                                        onkeypress="return ValidateNumeric(this)" TabIndex="21"></asp:TextBox>
							                    </div>                                           
                                            </div>              
                                        </div>
                                </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" TabIndex="22" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                    OnClick="butSubmit_Click" ToolTip="Click here to Save" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="23" OnClick="btnPrint_Click"
                                    CssClass="btn btn-info" ToolTip="Click here to Print" />
                                <asp:Button ID="butCancel" runat="server" Text="Cancel" TabIndex="24" OnClick="butCancel_Click"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                             </div>

                                <div class="col-12" id="divSupDetails" runat="server">
                                    <asp:Panel ID="pnlSupplBillDetail" runat="server">
                                        <asp:ListView ID="lvSuplBillDtl" runat="server">
                                            <EmptyDataTemplate>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" class="text-center mt-3"/>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                    <div class="sub-heading">
	                                                    <h5>Supplementary Guest</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>Financial Year</th>
                                                                <th>Suppli.Date
                                                                </th>
                                                                <th>Employee Name
                                                                </th>
                                                                <th>PAN NO.
                                                                </th>
                                                                <th>Ord.No
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                                <th>Taxable Amount
                                                                </th>
                                                                <th>TDS %
                                                                </th>
                                                                <th>TDS Amount
                                                                </th>
                                                                <th>Net Amount</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <th>
                                                        <asp:ImageButton ID="btnEditSuplBill" runat="server" ImageUrl="~/Images/edit.png"
                                                            OnClick="btnEditSuplBill_Click" CommandArgument='<%# Eval("SUPLGUESTID")%>' AlternateText="Edit Record" ToolTip="Edit Record" />
                                                    </td>
                                                 <th>
                                                     <%# Eval("FINANCIAL_YEAR")%>
                                                    </td>
                                                <td>
                                                    <%# Eval("SBDATE")%>
                                                    <%--SBDATE", "{0:dd/MM/yyyy}--%>
                                                </td>
                                                    <td>
                                                        <%# Eval("EMPLOYEENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORDNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NET_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TDS_PER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TDS_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TDS_NETAMOUNT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" id="hidTotalDeductionRecordsCount" runat="server" />
            <input type="hidden" id="hidEarningRecordsCount" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
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

