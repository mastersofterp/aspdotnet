<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="VehicleBillEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleBillEntry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE BILL ENTRY</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updActivity" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlCommon" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Common Criteria</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rdblisttourtype" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rdblisttourtype_SelectedIndexChanged"
                                                AutoPostBack="true" TabIndex="1" ToolTip="Select College Bus or Tour">
                                                <asp:ListItem Selected="True" Value="1">College Bus&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">College Tour&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3">Contract&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="4">HOD's Car</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Supplier</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSuppiler" CssClass="form-control" data-select2-enable="true" ToolTip="Select Supplier" runat="server"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSuppiler_SelectedIndexChanged"
                                                AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlSuppiler" runat="server"
                                                ErrorMessage="Please Select Suppiler" ControlToValidate="ddlSuppiler" InitialValue="0"
                                                Display="None" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divVehicle" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Vehicle</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVehicle" CssClass="form-control" data-select2-enable="true" ToolTip="Select Vehicle" runat="server"
                                                AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3"
                                                OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtBillFromDate" runat="server" TabIndex="4" Style="z-index: 0;"
                                                    ToolTip="Enter Event Start Date" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtBillFromDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtBillFromDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtBillFromDate" IsValidEmpty="true"
                                                    InvalidValueMessage="From Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select From Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Submit" SetFocusOnError="true" />
                                                <%--<ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtBillFromDate" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtBillFromDate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                        ControlExtender="meeFromDate" ControlToValidate="txtBillFromDate" IsValidEmpty="true"
                                                        InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                                        ErrorMessage="Please Select Date." EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="submit" SetFocusOnError="true" />--%>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                                                    ErrorMessage="Please Enter From Date" ControlToValidate="txtBillFromDate"
                                                    Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgBillToDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtBillToDate" runat="server" Style="z-index: 0;" TabIndex="5"
                                                    ToolTip="Enter Event To Date" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgBillToDate" TargetControlID="txtBillToDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtBillToDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                    ControlExtender="MaskedEditExtender2" ControlToValidate="txtBillToDate" IsValidEmpty="true"
                                                    InvalidValueMessage="To Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select To Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Submit" SetFocusOnError="true" />

                                                <%--  <ajaxToolKit:CalendarExtender ID="ceBillToDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgBillToDate" TargetControlID="txtBillToDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeBillToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtBillToDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevBillToDate" runat="server"
                                                    ControlExtender="meeBillToDate" ControlToValidate="txtBillToDate" IsValidEmpty="true"
                                                    InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="submit" SetFocusOnError="true" />
                                                <asp:RequiredFieldValidator ID="rfvtxtBillToDate" runat="server"
                                                    ErrorMessage="Please Enter To Date" ControlToValidate="txtBillToDate"
                                                    Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ControlToCompare="txtBillFromDate" ControlToValidate="txtBillToDate" Display="None"
                                                    ErrorMessage="Bill Date is less than To date" ID="CompareValidator1" Operator="GreaterThanEqual"
                                                    Type="Date" runat="server" ValidationGroup="Submit" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Hire For</label>
                                            </div>
                                            <asp:TextBox ID="txtHireFor" runat="server" CssClass="form-control" ToolTip="Enter Hire For" TabIndex="6"
                                                onkeypress="return CheckAlphabet(event, this);" MaxLength="60"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlcollegetour" runat="server">
                                <div class="col-12">
                                    <div class=" sub-heading">
                                        <h5>Select Critera for College Tour</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-3">
                                            <label><span style="color: #FF0000">*</span>From Time :</label>
                                            <asp:TextBox ID="txttravellingfromTime" runat="server" onkeypress="return CheckAlphaNumeric(event,this);"
                                                CssClass="form-control" ToolTip="Enter From Time" TabIndex="7"></asp:TextBox>


                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txttravellingfromTime"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txttravellingfromTime"
                                                IsValidEmpty="false" ErrorMessage="From Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter From Time"
                                                InvalidValueMessage="From Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                                Text="*" ValidationGroup="Submit" ViewStateMode="Enabled"></ajaxToolKit:MaskedEditValidator>


                                            <asp:RequiredFieldValidator ID="rfvtxttravellingfromTime" runat="server"
                                                ErrorMessage="Please Enter From Time" ControlToValidate="txttravellingfromTime"
                                                Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <%-- <ajaxToolKit:MaskedEditExtender ID="meetravellingfromTime" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                OnInvalidCssClass="" Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txttravellingfromTime"
                                                AcceptAMPM="True">
                                            </ajaxToolKit:MaskedEditExtender>--%>
                                            <%--<asp:Label ID="lblTipp" runat="server" CssClass="form-control" Enabled="false"
                                                            meta:resourcekey="lblTippResource1"></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-md-3">
                                            <label><span style="color: #FF0000">*</span>To Time :</label>
                                            <asp:TextBox ID="txttravellingToTime" runat="server" onkeypress="return CheckAlphaNumeric(event,this);"
                                                CssClass="form-control" ToolTip="Enter To Time" TabIndex="8"></asp:TextBox>

                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txttravellingToTime"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txttravellingToTime"
                                                IsValidEmpty="false" ErrorMessage="To Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter To Time"
                                                InvalidValueMessage="To Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                                Text="*" ValidationGroup="Submit" ViewStateMode="Enabled"></ajaxToolKit:MaskedEditValidator>

                                            <asp:RequiredFieldValidator ID="rfvtxttravellingToTime" runat="server"
                                                ErrorMessage="Please Enter To Time" ControlToValidate="txttravellingToTime"
                                                Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <%--<ajaxToolKit:MaskedEditExtender ID="meetravellingToTime" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                OnInvalidCssClass="" Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txttravellingToTime"
                                                AcceptAMPM="True">
                                            </ajaxToolKit:MaskedEditExtender>--%>
                                            <%--<asp:Label ID="Label6" runat="server" CssClass="form-control" Enabled="false"
                                                            meta:resourcekey="lblTippResource1"></asp:Label>--%>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Hired By :</label>
                                            <asp:TextBox ID="txtHiredBy" runat="server" CssClass="form-control" ToolTip="Enter Hired By" TabIndex="15"
                                                onkeypress="return CheckAlphabet(event, this);" MaxLength="60"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Tour Purpose :</label>
                                            <asp:TextBox ID="txtTourPurpose" runat="server" CssClass="form-control" ToolTip="Enter Tour Purpose"
                                                onkeypress="return CheckAlphabet(event, this);" MaxLength="60" TabIndex="16"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Visit Place :</label>
                                            <asp:TextBox ID="txtVisitPlace" runat="server" CssClass="form-control" ToolTip="Enter Visit Place"
                                                onkeypress="return CheckAlphabet(event, this);" MaxLength="60" TabIndex="17"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Remark :</label>
                                            <asp:TextBox ID="txtTourRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark If Any"
                                                TextMode="SingleLine" TabIndex="18"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-md-12">
                                            <label><span style="color: #FF0000"></span>Gross  Amount :</label>
                                        </div>
                                        <div class="form-group row col-md-12">
                                            <div class="form-group col-md-2">
                                                <label>Bill Amt :</label>
                                                <asp:TextBox ID="txtBillAmt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Bill Amount"
                                                    onchange="BillAmountCalculte()" onkeypress="return CheckNumeric(event, this);" TabIndex="9"
                                                    OnTextChanged="txtBillAmt_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">

                                                <label>+</label>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Extra Time Amt :</label>
                                                <asp:TextBox ID="txtTimeAmt" runat="server" CssClass="form-control" ToolTip="Enter Extra Time Amount"
                                                    onkeypress="return CheckNumeric(event, this);" MaxLength="10" TabIndex="10"
                                                    onchange="BillAmountCalculte()"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">

                                                <label>+</label>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Extra Distance Amt :</label>
                                                <asp:TextBox ID="txtExtraDistanceAmt" runat="server" MaxLength="10" CssClass="form-control"
                                                    ToolTip="Enter Extra Distance Amount" TabIndex="11"
                                                    onchange="BillAmountCalculte()" onkeypress="return CheckNumeric(event, this);"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-1">

                                                <label>=</label>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Total Amt :</label>
                                                <asp:Label ID="lblTotalBillAmt" runat="server" Enabled="false" CssClass="form-control" TabIndex="12"
                                                    ToolTip="Total Amount"></asp:Label>
                                                <asp:HiddenField ID="hdnTotalTourAmount" runat="server" EnableViewState="true" />
                                                
                                                 
                                          
                                        </div>

                                        <div class="form-group col-md-3">
                                            <label>Paid Amount :</label>
                                            <asp:TextBox ID="txtpaidAmount" runat="server" CssClass="form-control" ToolTip="Enter Paid Amount" TabIndex="13"
                                                onkeypress="return CheckNumeric(event, this);" MaxLength="10" onchange="calculate();"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Balance Amount :</label>
                                            <asp:Label ID="lblBalanceAmount" runat="server" Enabled="false" CssClass="form-control" TabIndex="14"
                                                ToolTip="Balance Amount"></asp:Label>
                                            <asp:HiddenField ID="hdnBalanceAmount" runat="server" EnableViewState="true" />
                                        </div>

                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlCollegeBus" runat="server">
                                <div class="col-12">
                                    <div class=" sub-heading">
                                        <h5>Select Criteria for College Bus</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Route From</label>
                                            </div>
                                            <asp:TextBox ID="txtRouteFrom" runat="server" CssClass="form-control" ToolTip="Enter Route From" MaxLength="40"
                                                onkeypress="return CheckAlphabet(event, this);" TabIndex="19"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Route To </label>
                                            </div>
                                            <asp:TextBox ID="txtRouteTo" runat="server" CssClass="form-control" ToolTip="Enter Route To" MaxLength="40"
                                                onkeypress="return CheckAlphabet(event, this);" TabIndex="20"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bill Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtBillAmount" runat="server" CssClass="form-control" ToolTip="Enter Bill Amount"
                                                onkeypress="return CheckNumeric(event, this);" MaxLength="10" TabIndex="21"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBillAmount" Display="None" runat="server" SetFocusOnError="true"
                                                ValidationGroup="Submit" ErrorMessage="Please Enter Bill Amount..!!"
                                                ControlToValidate="txtBillAmount"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTxtExtmobileno" runat="server" ValidChars=".0123456789"
                                                FilterType="Custom" TargetControlID="txtBillAmount">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Extra Amount </label>
                                            </div>
                                            <asp:TextBox ID="txtExtraAmount" runat="server" CssClass="form-control" ToolTip="Enter Extra Amount"
                                                onkeypress="return CheckNumeric(event, this);" MaxLength="10" TabIndex="22"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Numbers" TargetControlID="txtExtraAmount" ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Hike Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtHikeAmount" runat="server" CssClass="form-control" ToolTip="Enter Hike Amount"
                                                onkeypress="return CheckNumeric(event, this);" MaxLength="10" TabIndex="23"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers" TargetControlID="txtHikeAmount" ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark If Any"
                                                TextMode="SingleLine" TabIndex="24"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="rdblisttourtype" />
                        </Triggers>
                    </asp:UpdatePanel>


                    <asp:Panel ID="pnlButtion" runat="server">
                        <div class="col-12 btn-footer">
                            <asp:Button ID="Button1" runat="server" Text="Submit" ValidationGroup="Submit" ToolTip="Click here to Submit"
                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" CausesValidation="true" TabIndex="25" />
                            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="26"
                                CssClass="btn btn-warning" ToolTip="Click here to reset" CausesValidation="false" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Submit" />
                        </div>
                    </asp:Panel>

                    <div class="col-12">
                        <asp:Panel ID="pnlCollegeBillEntry" runat="server">
                            <asp:ListView ID="lvBillEntryList" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle College Bus Bill Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>SUPPLIER NAME
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>HIRE FOR 
                                                    </th>
                                                    <th>BILL FROM DATE 
                                                    </th>
                                                    <th>BILL TO DATE 
                                                    </th>
                                                    <th>BILL AMOUNT
                                                    </th>
                                                    <th>HIKE AMOUNT
                                                    </th>
                                                    <th>EXTRA AMOUNT
                                                    </th>
                                                    <th>GROSS AMOUNT
                                                    </th>
                                                    <th>CREATION DATE
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
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("BILL_ID") %>' ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("SUPPILER_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("HIRE_FOR")%>
                                        </td>
                                        <td>
                                            <%# Eval("BILL_FROM_DATE","{0:dd-MMM-yyyy}")%>
                            
                                        </td>
                                        <td>
                                            <%# Eval("BILL_TO_DATE","{0:dd-MMM-yyyy}")%>

                                        </td>
                                        <td>
                                            <%# Eval("BILL_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("HIKE_PRICE")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("EXTRA_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("GROSS_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("AUDIT_DATE","{0:dd-MMM-yyyy}") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlCollegeTourEntry" runat="server" ScrollBars="Auto" Visible="false">
                            <asp:ListView ID="lvCollegeTourEntry" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle College Tour Bill Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>SUPPLIER 
                                                    </th>
                                                    <th>VEHICLE 
                                                    </th>
                                                    <th>HIRE FOR 
                                                    </th>
                                                    <th>FROM DATE 
                                                    </th>
                                                    <th>TO DATE 
                                                    </th>
                                                    <th>TOTAL TOUR AMT.
                                                    </th>
                                                    <th>PAID AMT.
                                                    </th>
                                                    <th>BALANCE AMT.
                                                    </th>
                                                    <th>VISIT PLACE
                                                    </th>
                                                    <th>HIRED BY
                                                    </th>
                                                    <th>TOUR PURPOSE
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
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("BILL_ID") %>' ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("SUPPILER_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("HIRE_FOR")%>
                                        </td>
                                        <td>
                                            <%# Eval("BILL_FROM_DATE","{0:dd-MMM-yyyy}")%>
                            
                                        </td>
                                        <td>
                                            <%# Eval("BILL_TO_DATE","{0:dd-MMM-yyyy}")%>

                                        </td>
                                        <td>
                                            <%# Eval("TOUR_TOTAL_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("PAID_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("BALANCE_AMOUNT")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("VISIT_PLACE")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("HIRED_BY")%>
                                  
                                        </td>
                                        <td>
                                            <%# Eval("TOUR_PURPOSE")%>
                                  
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>





        <%-- To Calculate Balance Amount--%>
        <script type="text/javascript" language="javascript">
            function BillAmountCalculte() {
                var bill_amount = document.getElementById('ctl00_ContentPlaceHolder1_txtBillAmt');
                var extra_KM_Amount = document.getElementById('ctl00_ContentPlaceHolder1_txtExtraDistanceAmt');
                var extra_Time_amount = document.getElementById('ctl00_ContentPlaceHolder1_txtTimeAmt');
                var TotalBill = document.getElementById('ctl00_ContentPlaceHolder1_lblTotalBillAmt');
                try {
                    var billamt = 0.00;
                    var extraKMAmt = 0.00;
                    var extraTimeAmt = 0.00;
                    if (!isNaN(parseFloat(bill_amount.value))) {
                        billamt = parseFloat(bill_amount.value);
                    }
                    if (!isNaN(parseFloat(extra_KM_Amount.value))) {
                        extraKMAmt = parseFloat(extra_KM_Amount.value);
                    }
                    if (!isNaN(parseFloat(extra_Time_amount.value))) {
                        extraTimeAmt = parseFloat(extra_Time_amount.value);
                    }

                    TotalBill.innerHTML = (billamt + extraKMAmt + extraTimeAmt).toString();
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalTourAmount').value = TotalBill.innerHTML;
                    //alert(billamt.toString());

                }
                catch (ex) {
                    alert('catch');
                    alert(ex.toString());
                }

            }

            function calculate() {

                var Total_Bill_Amount = document.getElementById("<%=lblTotalBillAmt.ClientID %>").innerHTML
                var Paid_Amount = document.getElementById('ctl00_ContentPlaceHolder1_txtpaidAmount').value;
                var result = document.getElementById('ctl00_ContentPlaceHolder1_lblBalanceAmount');

                if (parseFloat(Paid_Amount) <= parseFloat(Total_Bill_Amount)) {

                    var myResult = parseFloat(Total_Bill_Amount) - parseFloat(Paid_Amount);
                    result.style.display = 'inherit';
                    result.innerHTML = myResult.toString();
                    Total_Bill_Amount = result.innerHTML;
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnBalanceAmount').value = result.innerHTML;

                }
                else {
                    result.innerHTML = '';
                    alert('Paid Amount is not greater than Bill Amount');
                }

            }
        </script>
</asp:Content>


