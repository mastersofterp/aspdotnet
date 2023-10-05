<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="fuelentry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_fuelentry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FUEL CONSUMPTION</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>Available Quantity
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
                            <asp:UpdatePanel ID="updActivity" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlFuel" runat="server">
                                        <%--<div class="panel panel-heading">Fuel & Indent Issue Entry</div>--%>
                                        <%-- <div class="panel panel-heading">Fuel Entry</div>--%>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRdo" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdblistVehicleType" runat="server" TabIndex="1" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="1">Fuels</asp:ListItem>
                                                        <asp:ListItem Value="2">Indent Items</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdblistVehicleTypes" runat="server" RepeatDirection="Horizontal" TabIndex="1"
                                                        OnSelectedIndexChanged="rdblistVehicleTypes_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Vehicle Type">
                                                        <asp:ListItem Selected="True" Value="1">College Vehicles</asp:ListItem>
                                                        <asp:ListItem Value="2">Contract Vehicles</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <%--  //---------start-----28-03-2023=------%>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divIssuetype" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Issue Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlIssueType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Issue Type" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Transport</asp:ListItem>
                                                        <asp:ListItem Value="2">Other than Transport</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlIssueType"
                                                        Display="None" ErrorMessage="Please Select Issue Type." ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divItemName" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlItem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Items" AutoPostBack="true" TabIndex="5" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ValidationGroup="ScheduleDtl" InitialValue="0"
                                                        ControlToValidate="ddlItem" Display="None" ErrorMessage="Please Select Item Name.">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAvailableQty" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Available Quantity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAvlQty" runat="server" TabIndex="6"
                                                        CssClass="form-control" ToolTip="Enter Available Quantity" MaxLength="3"></asp:TextBox>
                                                   <%-- <asp:Label ID="Label1" runat="server"></asp:Label>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ErrorMessage="Please Enter Available Quantity." ValidationGroup="ScheduleDtl" ControlToValidate="txtAvlQty"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <%--  //---------end-----28-03-2023=------%>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divVehicle" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Vehicle</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Vehicle" AutoPostBack="True" TabIndex="2" ErrorMessage="Please Select Vehicle."
                                                        OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" InitialValue="0" ControlToValidate="ddlVehicle"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDriver" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Driver</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Driver" TabIndex="3" AutoPostBack="false"
                                                        OnSelectedIndexChanged="ddlDriver_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFuelDate" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Issue Date </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgPdOn">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFuelDate" runat="server" TabIndex="4" CssClass="form-control"
                                                            ToolTip="Enter Issue Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtFuelDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFuelDate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                            ControlExtender="meeFromDate" ControlToValidate="txtFuelDate" IsValidEmpty="true"
                                                            InvalidValueMessage="Issue Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Issue Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="ScheduleDtl" SetFocusOnError="true" />
                                                        <%-- <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtFuelDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meePdDt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtFuelDate" OnInvalidCssClass="errordate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevpdsdt" runat="server" ControlExtender="meePdDt" Display="None"
                                                            ControlToValidate="txtFuelDate" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Issue Date"
                                                            InvalidValueMessage="Issue Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                            TooltipMessage="Please Enter Issue Date" ValidationGroup="ScheduleDtl" IsValidEmpty="false">
                                                        </ajaxToolKit:MaskedEditValidator>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                            ErrorMessage="Please Enter Issue Date." ValidationGroup="ScheduleDtl" ControlToValidate="txtFuelDate"
                                                            Display="None" Text="*">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <%--  <div class="form-group col-lg-3 col-md-6 col-12" id="divIssueDate" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Issue Date </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="Div3">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="TextBox1" runat="server" TabIndex="4" CssClass="form-control"
                                                            ToolTip="Enter Issue Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtFuelDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtFuelDate" OnInvalidCssClass="errordate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meePdDt" Display="None"
                                                            ControlToValidate="txtFuelDate" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Fuel Date"
                                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                            TooltipMessage="Please Enter Fuel Date" ValidationGroup="ScheduleDtl" IsValidEmpty="false">
                                                        </ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>--%>

                                                <%--  //---------start-----28-03-2023=------%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCouponNo" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Coupon No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCoupNo" runat="server" TabIndex="6"
                                                        CssClass="form-control" ToolTip="Enter Coupon No" MaxLength="20"></asp:TextBox>
                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ErrorMessage="Please Enter Coupon No." ValidationGroup="ScheduleDtl" ControlToValidate="txtCoupNo"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                    <%--  //---------end-----28-03-2023=------%>
                                                </div>


                                                <%--<div class="row">--%>
                                                <%------Start---29-03-2023-- other than transport Extra Fields---%>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDepartment" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Department" AutoPostBack="True" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDateofwithdrawal" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Date of withdrawal</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgDate11">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfWithdrawal" runat="server" TabIndex="4" CssClass="form-control"
                                                            ToolTip="Enter Date of withdrawal" Style="z-index: 0;"></asp:TextBox>

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgDate11" TargetControlID="txtDateOfWithdrawal" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDateOfWithdrawal" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                                                            ControlExtender="meeFromDate" ControlToValidate="txtDateOfWithdrawal" IsValidEmpty="true"
                                                            InvalidValueMessage="Purchased DateDate of withdrawal is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Purchased Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="ScheduleDtl" SetFocusOnError="true" />
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                                                            ErrorMessage="Please Enter Date of withdrawal" ControlToValidate="txtDateOfWithdrawal" Display="None" SetFocusOnError="True"
                                                            ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                                        <%-- <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtDateOfWithdrawal">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtDateOfWithdrawal" OnInvalidCssClass="errordate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meePdDt" Display="None"
                                                            ControlToValidate="txtDateOfWithdrawal" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date of withdrawal"
                                                            InvalidValueMessage="Date of withdrawal is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                            TooltipMessage="Please Enter Date of withdrawal" ValidationGroup="ScheduleDtl" IsValidEmpty="false">
                                                        </ajaxToolKit:MaskedEditValidator>--%>
                                                    </div>


                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDieselRequester" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Diesel Requester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDieselReq" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Diesel Requester" AutoPostBack="True" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divApprover" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Approver </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlApprover" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Select Approver" AutoPostBack="True" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <%------end---29-03-2023-----%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divQty" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Quantity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtQty" runat="server" TabIndex="6" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Quantity" MaxLength="6" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server"
                                                        ErrorMessage="Please Enter Quantity." ValidationGroup="ScheduleDtl" ControlToValidate="txtQty"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars=".0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtQty">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRate" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Rate</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRate" runat="server" TabIndex="7" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Rate" MaxLength="10" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnRate" runat="server" Value="0" />
                                                    <asp:RequiredFieldValidator ID="rfvRate" runat="server"
                                                        ErrorMessage="Please Enter Rate." ValidationGroup="ScheduleDtl" ControlToValidate="txtRate"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Total Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="7"
                                                        CssClass="form-control" ToolTip="Enter Rate" MaxLength="10"></asp:TextBox>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ErrorMessage="Please Enter Total Amount." ValidationGroup="ScheduleDtl" ControlToValidate="txtTotalAmount"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divstrmetread" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Start Meter Reading</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMeterReading" runat="server" TabIndex="8" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Start Meter Reading" MaxLength="15" onchange="SReadTextChange();"></asp:TextBox>
                                                    <asp:Label ID="lblmeterReading" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvMeterReading" runat="server"
                                                        ErrorMessage="Please Enter Start Meter Reading." ValidationGroup="ScheduleDtl" ControlToValidate="txtMeterReading"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divEndMetRead" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>End Meter Reading</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEndReading" runat="server" TabIndex="9" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter End Meter Reading" MaxLength="15" onchange="EReadTextChange();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEndMeterReading" runat="server"
                                                        ErrorMessage="Please Enter End Meter Reading." ValidationGroup="ScheduleDtl" ControlToValidate="txtEndReading"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divkms" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>No. of Kms</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNoOfKms" runat="server" TabIndex="10" onblur="CalculateAmount();" ReadOnly="true"
                                                        CssClass="form-control" ToolTip="No. of Kms" MaxLength="15"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNoOfKms" runat="server"
                                                        ErrorMessage="No. of Kms required." ValidationGroup="ScheduleDtl" ControlToValidate="txtNoOfKms"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divmilege" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Milege</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMilege" runat="server" TabIndex="11"
                                                        CssClass="form-control" ToolTip="Enter Milege" MaxLength="15" ReadOnly="true" onblur="CalculateAmount();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ErrorMessage="Please Enter Milege." ValidationGroup="ScheduleDtl" ControlToValidate="txtMilege"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAmount" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMilegeAmount" runat="server" TabIndex="12" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Amount" MaxLength="15" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMilegeAmt" runat="server"
                                                        ErrorMessage="Please enter amount" ValidationGroup="ScheduleDtl" ControlToValidate="txtMilegeAmount"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPurposeofwithdrawal" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Purpose of withdrawal</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPurposeofwithdrawal" runat="server" TabIndex="12"
                                                        CssClass="form-control" ToolTip="Enter Amount" MaxLength="100" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                        ErrorMessage="Please enter amount" ValidationGroup="ScheduleDtl" ControlToValidate="txtMilegeAmount"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divRemark" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Remark</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="13" CssClass="form-control"
                                                        ToolTip="Enter Remark If Any"></asp:TextBox>
                                                </div>


                                                <%--     -------start------upload Request letter-------01-04-2023--%>
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="UplReqLetter" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Upload Request letter <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                                    </div>
                                                    <asp:FileUpload ID="UploadrequestLetter" runat="server" EnableViewState="true" TabIndex="5"
                                                        ToolTip="Click here to Attach File" />

                                                    <asp:Label ID="lblPreAttach" runat="server" Text="Label" Visible="false"></asp:Label>
                                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                                </div>

                                                <%--     -------end------upload Request letter-------01-04-2023--%>
                                                <%-- </div>--%>
                                            </div>
                                            <%-- <div class="col-12" id="divMeterRead" runat="server">--%>
                                            <%--  <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divQty" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Quantity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtQty" runat="server" TabIndex="6" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Quantity" MaxLength="3" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server"
                                                        ErrorMessage="Please Enter Quantity." ValidationGroup="ScheduleDtl" ControlToValidate="txtQty"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                 <div class="form-group col-lg-3 col-md-6 col-12" >
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Rate</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRate" runat="server" TabIndex="7" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Rate" MaxLength="10" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnRate" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvRate" runat="server"
                                                        ErrorMessage="Please Enter Rate." ValidationGroup="ScheduleDtl" ControlToValidate="txtRate"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server" >
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Total Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="7" 
                                                        CssClass="form-control" ToolTip="Enter Rate" MaxLength="10"></asp:TextBox>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ErrorMessage="Please Enter Tital Amount." ValidationGroup="ScheduleDtl" ControlToValidate="txtTotalAmount"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Start Meter Reading</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMeterReading" runat="server" TabIndex="8" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Start Meter Reading" MaxLength="15" onchange="SReadTextChange();"></asp:TextBox>
                                                    <asp:Label ID="lblmeterReading" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvMeterReading" runat="server"
                                                        ErrorMessage="Please Enter Meter Reading." ValidationGroup="ScheduleDtl" ControlToValidate="txtMeterReading"
                                                        Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>End Meter Reading</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEndReading" runat="server" TabIndex="9" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter End Meter Reading" MaxLength="15" onchange="EReadTextChange();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEndMeterReading" runat="server"
                                                        ErrorMessage="Please Enter End Meter Reading." ValidationGroup="ScheduleDtl" ControlToValidate="txtEndReading"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>No. of Kms</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNoOfKms" runat="server" TabIndex="10" onblur="CalculateAmount();" ReadOnly="true"
                                                        CssClass="form-control" ToolTip="No. of Kms" MaxLength="15"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNoOfKms" runat="server"
                                                        ErrorMessage="No. of Kms required." ValidationGroup="ScheduleDtl" ControlToValidate="txtNoOfKms"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Milege</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMilege" runat="server" TabIndex="11"
                                                        CssClass="form-control" ToolTip="Enter Milege" MaxLength="15" ReadOnly="true" onblur="CalculateAmount();"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAmount" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMilegeAmount" runat="server" TabIndex="12" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Amount" MaxLength="15" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMilegeAmt" runat="server"
                                                        ErrorMessage="Please enter amount" ValidationGroup="ScheduleDtl" ControlToValidate="txtMilegeAmount"
                                                        Display="None" Text="*"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divRemark" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Remark</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="13" CssClass="form-control"
                                                        ToolTip="Enter Remark If Any"></asp:TextBox>
                                                </div>

                                            </div>--%>
                                            <%--</div>--%>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trAmt" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="17" onkeypress="return CheckNumeric(event,this);"
                                                        CssClass="form-control" ToolTip="Enter Amount" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Please Enter Amount"
                                                        ValidationGroup="ScheduleDtl" ControlToValidate="txtAmount" Display="None" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trLog" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Log No </label>
                                                    </div>
                                                    <asp:TextBox ID="txtLogNo" runat="server" onkeypress="return CheckNumeric(event,this);" TabIndex="14"
                                                        CssClass="form-control" ToolTip="Enter Log Number" MaxLength="20"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trBillNo" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Bill No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtBillNo" runat="server" TabIndex="15" CssClass="form-control" ToolTip="Enter Bill Number"
                                                        MaxLength="20" onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trBillDt" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Bill Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="Image1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtBillDate" runat="server" TabIndex="16" CssClass="form-control"
                                                            ToolTip="Enter Bill Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtBillDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" TargetControlID="txtBillDate" OnInvalidCssClass="errordate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                            ControlToValidate="txtBillDate" Display="None" EmptyValueBlurredText="Empty" SetFocusOnError="true"
                                                            InvalidValueBlurredMessage="Invalid Date" TooltipMessage="Please Enter Tour Date"
                                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            ValidationGroup="ScheduleDtl" IsValidEmpty="false"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlReport" runat="server" Visible="false">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Direct Fuel Entry</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImgBntCalc">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFDate" TabIndex="1" runat="server" CssClass="form-control"
                                                            ToolTip="Enter From Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlToValidate="txtFDate"
                                                            ControlExtender="medt" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            IsValidEmpty="true" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            Display="None" Text="*" ValidationGroup="Submit">                                                                       
                                                        </ajaxToolKit:MaskedEditValidator>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvFDate" ControlToValidate="txtFDate" Display="None"
                                                            ErrorMessage="Please Enter From Date " ValidationGroup="Report" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>To Date </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="Image1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtTDate" runat="server" TabIndex="2" CssClass="form-control"
                                                            ToolTip="Enter TO Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTDate" PopupButtonID="Image1">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtTDate" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                            ControlExtender="MaskedEditExtender1" ControlToValidate="txtTDate"
                                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvTDate" ControlToValidate="txtTDate" Display="None"
                                                            ErrorMessage="Please Enter To Date " ValidationGroup="Report" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShowDReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnShowDReport_Click"
                                                ToolTip="Click here to Show Direct Fuel Entry Report" ValidationGroup="Report"  Visible="false"/>
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click"
                                                CausesValidation="false" ToolTip="Click here to back" Visible="false"/>
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Report" />

                                        </div>

                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="rdblistVehicleType" />
                                    <asp:PostBackTrigger ControlID="rdblistVehicleTypes" />
                                    <asp:PostBackTrigger ControlID="btnBack" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
                <div class="" id="divButton" runat="server">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="ScheduleDtl"
                            OnClick="btnSubmit_Click" TabIndex="17" CausesValidation="true" ToolTip="Click here to Submit" 
                            UseSubmitBehavior="false" OnClientClick="this.disabled='true'; setTimeout('enableButton()', 10000) "/>
                        <%-- OnClientClick="checkissuedate();"--%>
                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                            TabIndex="19" CausesValidation="false" ToolTip="Click here to Show Report"  Visible="false"/>
                        <asp:Button ID="btnBalReport" runat="server" Text="Stock Balance Report" CssClass="btn btn-info" OnClick="btnBalReport_Click"
                            TabIndex="20" CausesValidation="false" ToolTip="Click here to Show Stock Balance Report" Visible="false" />
                        <asp:Button ID="btnDirectReport" runat="server" Text="Direct Fuel Entry Report" CssClass="btn btn-info" OnClick="btnDirectReport_Click"
                            TabIndex="20" CausesValidation="false" ToolTip="Click here to Show Direct Fuel Entry Report" Visible="false" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"
                            ValidationGroup="ScheduleDtl" HeaderText="Following Fields are mandatory" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                            TabIndex="18" CausesValidation="false" ToolTip="Click here to Reset" />

                    </div>
                    <div class="col-12">
                        <asp:UpdatePanel ID="updDocument" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvFuel" runat="server" OnItemDataBound="lvFuel_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Fuel Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th runat="server" id="thvehicle">VEHICLE
                                                            </th>
                                                            <th runat="server" id="thdriver">DRIVER
                                                            </th>
                                                            <th>ITEM NAME
                                                            </th>
                                                            <th runat="server" id="thissuedate">ISSUE DATE
                                                            </th>
                                                             <th runat="server" id="thdepartment" visible="false">DEPARTMENT
                                                            </th>
                                                             <th runat="server" id="thdateofwithdrawal" visible="false">DATE OF WITHDRAWAL
                                                            </th>
                                                            <th>QUANTITY
                                                            </th>
                                                             <th runat="server" id="thuploadletter" visible="false">
                                                                 UPLOADED LETTER
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("FEID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                    <asp:HiddenField id="hdItemId" runat="server" Value='<%# Eval("ITEM_ID")%>'/>
                                                    <asp:HiddenField id="hdFEID" runat="server" Value='<%# Eval("FEID")%>'/>
                                                </td>
                                                <td runat="server" id="tdvehicle">
                                                    
                                                     <asp:Label ID="lblvehicle" runat="server" text='<%# Eval("VEHICLE")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="tddriver">
                                                    
                                                     <asp:Label ID="lbldriver" runat="server" text='<%# ((Eval("DNAME").ToString() != string.Empty) ? Eval("DNAME") : "--")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_NAME")%>
                                                </td>
                                                <td runat="server" id="tdissuedate">
                                                    
                                                     <asp:Label ID="lblissuedate" runat="server" text='<%# Eval("FUELDATE", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                </td>
                                                 <td runat="server" id="tddepartment" visible="false">
                                                    <%# Eval("DEPARTMENT")%>                         
                                                </td>
                                                 <td runat="server" id="tddateofwithdrawal" visible="false">
                                                    <%# Eval("DATE_OF_WITHDRAWAL", "{0: dd-MM-yyyy}")%>  
                                                 </td>
                                                <td>
                                                    <%# Eval("QTY")%>                         
                                                </td>
                                                  <td runat="server" id="tduploadletter" visible="false">
                                                   <%-- <asp:ImageButton ID="btnDownload" runat="server" AlternateText="Download Letter" CausesValidation="false" CommandArgument='<%# Eval("FEID") %>' OnClick="btnDownload_Click" ImageUrl="~/Images/action_down.png" ToolTip="Download Letter" />--%>


                                                       <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="btnDownload_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UPLOAD_REQUEST_LETTER") %>' CommandArgument='<%# Eval("UPLOAD_REQUEST_LETTER") %>' Visible='<%# Convert.ToString(Eval("UPLOAD_REQUEST_LETTER"))==string.Empty?false:true %>'></asp:ImageButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                    <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                    <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvFuel" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
         <div id="divMsg" runat="server">
    </div>


        <script type="text/javascript">
            debugger;
            function enableButton() {
                // ctl00_ContentPlaceHolder1_btnSubmit
                $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);
              //  document.getElementById($('#ctl00_ContentPlaceHolder1_btnSubmit')).value = 'Submit';
             //   $('#ctl00_ContentPlaceHolder1_btnSubmit').text('Submit');
               // document.getElementById('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', true);

            }
</script>
    <script type="text/javascript">

        function SReadTextChange() {

            if (document.getElementById('<%=txtMeterReading.ClientID %>').value != '') {

                var SMReading = parseFloat(document.getElementById('<%=txtMeterReading.ClientID %>').value);
                var EMReading = 0;
                var tot = 0;
                if (document.getElementById('<%=txtEndReading.ClientID %>').value != '') {
                    EMReading = parseFloat(document.getElementById('<%=txtEndReading.ClientID %>').value);
                    if (EMReading > SMReading) {
                        tot = (EMReading - SMReading);
                    }
                    else {
                        alert('Start Reading Should be Smaller than End Reading.');
                        document.getElementById('<%=txtMeterReading.ClientID %>').value = '';
                        window.setTimeout(function () {
                            document.getElementById('<%=txtMeterReading.ClientID %>').focus();
                        }, 0);
                        return false;
                    }
                }
                document.getElementById('<%=txtNoOfKms.ClientID %>').value = tot;
            }

        }


        function EReadTextChange() {

            if (document.getElementById('<%=txtEndReading.ClientID %>').value != '') {
                var EMReading = parseFloat(document.getElementById('<%=txtEndReading.ClientID %>').value);
                var SMReading = parseFloat(document.getElementById('<%=txtMeterReading.ClientID %>').value);
                var tot = 0;
                if (EMReading != 0 && SMReading != 0) {
                    if (EMReading > SMReading) {
                        tot = (EMReading - SMReading);
                    }
                    else {

                        alert('Start Reading Should be Smaller than End Reading.');
                        document.getElementById('<%=txtEndReading.ClientID %>').value = '';
                        document.getElementById('<%=txtEndReading.ClientID %>').focus();
                        window.setTimeout(function () {
                            document.getElementById('<%=txtEndReading.ClientID %>').focus();
                        }, 0);
                        return false;

                    }
                }
                document.getElementById('<%=txtNoOfKms.ClientID %>').value = tot;

                var NoOfKms = parseFloat(document.getElementById('<%=txtNoOfKms.ClientID %>').value);
                var quantity = parseFloat(document.getElementById('<%=txtQty.ClientID %>').value);
                var milege = 0;
                if (NoOfKms != 0 && quantity != 0) {
                    milege = (NoOfKms / quantity);
                }
                document.getElementById('<%=txtMilege.ClientID %>').value = milege.toFixed(2);
            }
        }


        function CalculateAmount() {
            if (document.getElementById('<%=txtNoOfKms.ClientID %>').value != '') {
                if (document.getElementById('<%=txtQty.ClientID %>').value != '') {
                    var NoOfKms = parseFloat(document.getElementById('<%=txtNoOfKms.ClientID %>').value);
                    var quantity = parseFloat(document.getElementById('<%=txtQty.ClientID %>').value);
                    var milege = 0;
                    if (NoOfKms != 0 && quantity != 0) {
                        milege = (NoOfKms / quantity);

                    }
                    document.getElementById('<%=txtMilege.ClientID %>').value = milege.toFixed(2);
                }

            }

        }

        // function checkissuedate() {
        //     if (document.getElementById('<%= txtFuelDate.ClientID %>').value != '') {
        //         var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
        //         if (!(date_regex.test(document.getElementById('<%= txtFuelDate.ClientID %>').value))) {
        //             alert("Issue Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
        //             return true;
        //         }
        //     }
        //
        // }

    </script>
</asp:Content>

