<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Monthly_Installment_Entry.aspx.cs" Inherits="PayRoll_Pay_Monthly_Installment_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MONTHLY INSTALLMENT AMOUNT ENTRY</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Select Criteria</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlSelection" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Order By</label>
                                    </div>
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                        ToolTip="Select Order By"
                                        runat="server" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                        <asp:ListItem Value="2">Name</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                        ToolTip="Select College Name"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                        ToolTip="Select Employee Name"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkStopInstall" runat="server" AutoPostBack="true" OnCheckedChanged="chkStopInstall_CheckedChanged" 
                                        ValidationGroup="ADD" Text="Show stop InstallMent Entry" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlEmpDetails" runat="server">
                        <div class="col-12">
	                        <div class="row">
		                        <div class="col-12">
		                        <div class="sub-heading">
				                        <h5>Employee Details</h5>
			                        </div>
		                        </div>
	                        </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>ID No & Name</label>
								    </div>
                                    <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
							    </div>                                  
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>Designation</label>
								    </div>
                                    <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
							    </div>                                  
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>Department</label>
								    </div>
                                    <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
							    </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnldeducationentry" runat="server">
                            <div class="sub-heading">
	                            <h5>Select Pay Head For Deducation</h5>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Pay Head</label>
								        </div>
                                        <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Pay Head" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select Payhead</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:CheckBox ID="chkStop" CssClass="legendPay" runat="server" Text="Stop" />
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Installment For Sub Payhead</label>
								        </div>
                                        <asp:DropDownList ID="ddlSubpayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Installment For Sub Payhead" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Monthly Deducted Amt</label>
								        </div>
                                        <asp:TextBox ID="txtMonthlyDedAmt" runat="server" MaxLength="9" CssClass="form-control" TabIndex="3" ToolTip="Enter Monthly Deducted Amt" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfMonthlyDedAmt" runat="server" ControlToValidate="txtMonthlyDedAmt"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Monthly Deducted Amt"
                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Total No.of Inst</label>
								        </div>
                                        <asp:TextBox ID="txtNoofInstallMent" runat="server" MaxLength="4" CssClass="form-control" TabIndex="4" ToolTip="Enter Total No.of Inst" onkeyup="return totalamount(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNoofInstallMent" runat="server" ControlToValidate="txtNoofInstallMent"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter  No.of Installment "
                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Total Amount</label>
								        </div>
                                            <asp:TextBox ID="txtTotalAmount" runat="server" MaxLength="9" CssClass="form-control" TabIndex="5" ToolTip="Enter Total Amount"></asp:TextBox>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Total No.of Paid.Inst</label>
								        </div>
                                        <asp:TextBox ID="txtNoofInstPaid" runat="server" MaxLength="6" CssClass="form-control" TabIndex="6" ToolTip="Enter  Total No.of Paid.Inst:" OnTextChanged="txtNoofInstPaid_TextChanged" AutoPostBack="true"></asp:TextBox>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Outstanding Amt</label>
								        </div>
                                        <asp:TextBox ID="txtOutStandingAmt" runat="server" MaxLength="6"
                                            CssClass="form-control" TabIndex="7" ToolTip="Enter Outstanding Amt"></asp:TextBox>
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
                                             <sup>* </sup>
									        <label>Bank</label>
								        </div>
                                            <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Select Bank" data-select2-enable="true">
                                        </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="rfvddlBank" runat="server" ControlToValidate="ddlBank"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank" ValidationGroup="payroll"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Bank Place</label>
								        </div>
                                            <asp:DropDownList ID="ddlBankPlace" AppendDataBoundItems="true" runat="server" ToolTip="Select Bank Place" CssClass="form-control" TabIndex="9" data-select2-enable="true">
                                        </asp:DropDownList>
                               <%--          <asp:RequiredFieldValidator ID="rfvBankPlace" runat="server" ControlToValidate="ddlBankPlace"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank Place" ValidationGroup="payroll"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdPolicyno" visible="false" runat="server">
								        <div class="label-dynamic">
									        <label>Policy.No</label>
								        </div>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdRdNo" visible="false" runat="server">
								        <div class="label-dynamic">
									        <label>Rd.No</label>
								        </div>
							        </div>
                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdAccountNO" runat="server">
								        <div class="label-dynamic">
                                             <sup>* </sup>
									        <label>Account No</label>
								        </div>
                                        <asp:TextBox ID="txtAccNo" runat="server" MaxLength="24" CssClass="form-control" TabIndex="10" ToolTip="Enter Account No" onkeypress="return isNumber(event,this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfAccNo" runat="server" ControlToValidate="txtAccNo"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Account No "
                                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbAccNo" runat="server"
                                                    TargetControlID="txtAccNo"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars="">
                                                </ajaxToolKit:FilteredTextBoxExtender>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Installment Drawn Date</label>
								        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="ImgInstallmentDrawnDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtInstallmentDrawnDate" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Enter Installment Drawn Date"></asp:TextBox>
                                            <%--<div class="input-group-addon">
                                                <asp:Image ID="ImgInstallmentDrawnDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>--%>
                                            <ajaxToolKit:CalendarExtender ID="cetxtInstallmentDrawnDate" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgInstallmentDrawnDate" TargetControlID="txtInstallmentDrawnDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtInstallmentDrawnDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please select Installment Drawn Date in (dd/MM/yyyy Format)"
                                                SetFocusOnError="True" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtInstallmentDrawnDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" TargetControlID="txtInstallmentDrawnDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtInstallmentDrawnDate" runat="server" ControlExtender="metxtInstallmentDrawnDate"
                                                ControlToValidate="txtInstallmentDrawnDate" Display="None" EmptyValueBlurredText="Empty"
                                                EmptyValueMessage="Please select Installment Drawn Date" InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="Installment Drawn Date is Invalid (Enter dd/MM/yyyy Format)"
                                                SetFocusOnError="True" TooltipMessage="Please select Installment Drawn Date"
                                                ValidationGroup="payroll" />
                                        </div>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Start Date</label>
								        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Enter Start Date" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"  ></asp:TextBox> <%--onchange="CompareDOJ2()"--%>
                                           <%-- <div class="input-group-addon">
                                                <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>--%>
                                            <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtStartDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                                SetFocusOnError="True" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtStartDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" TargetControlID="txtStartDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtStartDate" runat="server" ControlExtender="metxtStartDate"
                                                ControlToValidate="txtStartDate" Display="None" EmptyValueBlurredText="Empty"
                                                EmptyValueMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                TooltipMessage="Please Enter Inc.Date" ValidationGroup="payroll" />
                                        </div>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <sup>* </sup>
									        <label>Expiry Date</label>
								        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalExpiryDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Enter Expiry Date" />
                                            <%--<div class="input-group-addon">
                                                <asp:Image ID="imgCalExpiryDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>--%>
                                            <ajaxToolKit:CalendarExtender ID="cetxtExpiryDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtExpiryDate" PopupButtonID="imgCalExpiryDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                                Display="None" ErrorMessage="Please Select Expiry Date in (dd/MM/yyyy Format)"
                                                ValidationGroup="payroll" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtExpiryDate" runat="server" TargetControlID="txtExpiryDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="metxtExpiryDate"
                                                ControlToValidate="txtExpiryDate" EmptyValueMessage="Please Enter Expiry Date"
                                                InvalidValueMessage=" Expiry Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Inc.Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="payroll" SetFocusOnError="True" />
                                            <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtStartDate"
                                                ControlToValidate="txtExpiryDate" Type="Date" Operator="GreaterThanEqual"
                                                ErrorMessage="Start Date Should be Less than  Expiry Date." ValidationGroup="payroll" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                        </div>
							        </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Remarks</label>
								        </div>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TabIndex="14" ToolTip="Enter Remarks" TextMode="MultiLine" />
							        </div>
                                       
                                </div>
                            </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll" TabIndex="15" ToolTip="Click To Save"
                                CssClass="btn btn-primary" />
                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                CssClass="btn btn-warning" TabIndex="16" ToolTip="Click To Reset" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="payroll" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Click Add New To Enter Installment Details" CssClass="btn btn-primary" Text="Add New" TabIndex="17"
                                ValidationGroup="ADD" OnClick="btnAdd_Click"></asp:LinkButton>
                            <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ADD" />
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvMonthlyIstallment" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
	                                        <h5>Monthly InstallMent Amount Entry</h5>
                                        </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                            <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>PayHead
                                                </th>
                                                <th>Monthly Amt
                                                </th>
                                                <th>No.of Inst
                                                </th>
                                                <th>Tot.Amt
                                                </th>
                                                <th>Outstand Amt
                                                </th>
                                                <th>Expiry Date
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("INO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("INO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("PAYSHORT")%>
                                        </td>
                                        <td>
                                            <%# Eval("MONAMT")%>
                                        </td>
                                        <td>
                                            <%# Eval("INSTALNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("TOTAMT")%>
                                        </td>
                                        <td>
                                            <%# Eval("BAL_AMT")%>
                                        </td>
                                        <td>
                                            <%# Eval("EXPDT", "{0:dd/MM/yyyy}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>
                            <div class="vista-grid_datapager text-center d-none">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvMonthlyIstallment" PageSize="10"
                                    OnPreRender="dpPager_PreRender">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
                    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
            </div>
        </div>
    </div>


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

        //Calculating the total amount
        function totalamount(val) {
            if (ValidateNumeric(val)) {
                var txtMonthlyDedAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtMonthlyDedAmt");
                var txtTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalAmount");
                var txtOutStandingAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtOutStandingAmt");
                var txtNoofInstPaid = document.getElementById("ctl00_ContentPlaceHolder1_txtNoofInstPaid");
                txtTotalAmount.value = Number(val.value) * Number(txtMonthlyDedAmt.value);
                txtNoofInstPaid.value = 0;
                txtOutStandingAmt.value = txtTotalAmount.value;
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
    <script type="text/javascript">
        function isNumber(evt) {
            debugger
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
               
                alert("Only Enter Numbers");
                return false;
            }
            return true;
        }
    </script>
             <script type="text/javascript" language="javascript">
             function CompareDOJ1() {
                     debugger

                     var fdate = document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>');
             var edate = document.getElementById('<%=txtStartDate.ClientID%>');

             var fdate1 = document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>').value;
             var edate1 = document.getElementById('<%=txtStartDate.ClientID%>').value;
             if (document.getElementById('<%=txtStartDate.ClientID%>').value != "") {
                 if (fdate1 > edate1) {
                     alert('From Date should be less than To Date');
                     document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>').value = ""
                    return false;
                }
            }


        }
    </script>

    <script type="text/javascript" language="javascript">
                 function CompareDOJ2() {
                debugger
                var fdate = document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>');  // Installment drawn date 
                var edate = document.getElementById('<%=txtStartDate.ClientID%>');             // Installment Start Date
              if (document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>').value == "") {
                 alert('Enter Installment Drawn date');
                 document.getElementById('<%=txtStartDate.ClientID%>').value = "";
                 return false;
              }
              else {
                  var fdate1 = document.getElementById('<%=txtInstallmentDrawnDate.ClientID%>').value;  // Installment drawn date 
                  var edate1 = document.getElementById('<%=txtStartDate.ClientID%>').value;              // Installment Start Date
                 if (edate1 > fdate1) {
                     alert('Installment Start date should be less than Installment Drawn date');

                     document.getElementById('<%=txtStartDate.ClientID%>').value = "";
                     return false;
                 }
             }
         }
    </script>
    
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
