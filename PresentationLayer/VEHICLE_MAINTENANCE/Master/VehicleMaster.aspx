<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="VehicleMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_VehicleMaster"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE DETAILS</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlPersInfo" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Vehicle Information</h5>
                            </div>
                        </div>
                        <%-- <div class="box-tools pull-right">
                                    <img id="img3" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divServiceDetails')" />
                                </div>--%>
                        <div class="col-12" id="divServiceDetails">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Make(Name of Vehicle)</label>
                                    </div>
                                    <asp:TextBox ID="txtVName" CssClass="form-control" runat="server" MaxLength="35"
                                        TabIndex="1" ToolTip="Enter Make*(Name of Vehicle)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVName" runat="server" ControlToValidate="txtVName"
                                        Display="None" ErrorMessage="Please Enter Name of the Vehicle." SetFocusOnError="true" ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" TargetControlID="txtVName"
                                        ValidChars=" /\-">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Year of Make</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image5">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtVMake" CssClass="form-control" runat="server" TabIndex="2"
                                            MaxLength="7" ToolTip="Enter Year of Make"></asp:TextBox>
                                      <%--  <asp:RequiredFieldValidator ID="rfvVDate" runat="server" ControlToValidate="txtVMake"
                                            Display="None" ErrorMessage="Please Enter Vehicle Make Date" SetFocusOnError="true"
                                            ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="true"
                                            EnableViewState="true" Format="MM/yyyy" PopupButtonID="Image5" TargetControlID="txtVMake">
                                        </ajaxToolKit:CalendarExtender>--%>

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVMake"
                                                                        Display="None" ErrorMessage="Please Enter Year of Make Date" SetFocusOnError="true"
                                                                 ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image5" TargetControlID="txtVMake">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtVMake" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mee2"
                                            ControlToValidate="txtVMake" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Year of Make Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Year of Make Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Year of Make Date" ValidationGroup="ScheduleDtl"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Model</label>
                                    </div>
                                    <asp:TextBox ID="txtVModel" runat="server" CssClass="form-control" MaxLength="25" TabIndex="5"
                                        onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter Model"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVModel" runat="server" ControlToValidate="txtVModel"
                                        Display="None" ErrorMessage="Please Enter Model of the Vehicle." SetFocusOnError="true"
                                        ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Reg-No.</label>
                                    </div>
                                    <asp:TextBox ID="txtVRegNo" runat="server" CssClass="form-control" MaxLength="20" ToolTip="Enter Registration Number"
                                        TabIndex="6" onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVReNo" runat="server" ControlToValidate="txtVRegNo"
                                        Display="None" ErrorMessage="Please Enter Vehicle Register No." SetFocusOnError="true"
                                        ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Engine No.</label>
                                    </div>
                                    <asp:TextBox ID="txtVEngineNo" runat="server" CssClass="form-control" MaxLength="25" TabIndex="9"
                                        onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter Engine Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVEnNo" runat="server" ControlToValidate="txtVEngineNo"
                                        Display="None" ErrorMessage="Please Enter Vehicle Engine No." SetFocusOnError="true"
                                        ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>RC Book No.</label>
                                    </div>
                                    <asp:TextBox ID="txtVRCBookNo" runat="server" CssClass="form-control" TabIndex="10"
                                        MaxLength="25" onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter RC Book Number"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>RC Validity(yrs)</label>
                                    </div>
                                    <asp:TextBox ID="txtRCValidity" runat="server" CssClass="form-control" ToolTip="Enter RC Validity(yrs)"
                                        onkeypress="return CheckNumeric(event,this);"
                                        MaxLength="3" TabIndex="13"></asp:TextBox>
                                      
                                    <%--<asp:RequiredFieldValidator ID="rfvRCValidity" runat="server" ControlToValidate="txtRCValidity"
                                                                        Display="None" ErrorMessage="Please Enter RC Validity in number of years" SetFocusOnError="true"
                                                                        ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="0123456789" TargetControlID="txtRCValidity">
                                          </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>PUC Expiry Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image4">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtPUCDt" runat="server" TabIndex="14" ToolTip="Enter PUC Expiry Date"
                                            CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvPuc" runat="server" ControlToValidate="txtPUCDt"
                                                                        Display="None" ErrorMessage="Please Enter PUC Date." SetFocusOnError="true" 
                                                                ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>--%>
                                      <%--  <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image4" TargetControlID="txtPUCDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="mee3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtPUCDt" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mev4" runat="server" ControlExtender="mee3"
                                            ControlToValidate="txtPUCDt" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Reg. Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Purchase Date" ValidationGroup="Schedule">
                                        </ajaxToolKit:MaskedEditValidator>--%>


                                       <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPUCDt"
                                                                        Display="None" ErrorMessage="Please Enter Year of Make Date" SetFocusOnError="true"
                                                                 ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image4" TargetControlID="txtPUCDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtPUCDt" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="mee2"
                                            ControlToValidate="txtPUCDt" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Reg. Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="PUC Expiry Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter PUC Expiry Date" ValidationGroup="ScheduleDtl"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Type </label>
                                    </div>
                                    <asp:DropDownList ID="ddlVehicleTypeAC" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="17"
                                        AppendDataBoundItems="true" ToolTip="Select Vehicle Type ">
                                        <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">AC</asp:ListItem>
                                        <asp:ListItem Value="2">Non AC</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVehicleTypeAC" runat="server" ErrorMessage="Please Select Vehicle Type "
                                        ControlToValidate="ddlVehicleTypeAC" InitialValue="0"
                                        Display="None" ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Number</label>
                                    </div>
                                    <asp:TextBox ID="txtVehNumber" runat="server" CssClass="form-control" ToolTip="Enter Vehicle Number"
                                        onkeypress="return CheckNumeric(event,this);" MaxLength="15" TabIndex="18"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVehNo" runat="server" ControlToValidate="txtVehNumber"
                                        Display="None" ErrorMessage="Please Enter Vehicle Number" SetFocusOnError="true"
                                        ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Permit No.</label>
                                    </div>
                                    <asp:TextBox ID="txtVPermit" CssClass="form-control" runat="server" ToolTip="Enter Permit Number"
                                        MaxLength="25" TabIndex="3" onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvVPrmtNo" runat="server" ControlToValidate="txtVPermit"
                                                                        Display="None" ErrorMessage="Please Enter Permit No." SetFocusOnError="true"
                                                                        ValidationGroup="ScheduleDtl">
                                                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Color</label>
                                    </div>
                                    <asp:TextBox ID="txtVColor" CssClass="form-control" runat="server" TabIndex="4"
                                        onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Color"
                                        MaxLength="18"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Reg. Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image123">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtVRegDate" CssClass="form-control" runat="server"
                                            TabIndex="7" ToolTip="Enter Registration Date" Style="z-index: 0;"></asp:TextBox>
                                      <%--  <asp:RequiredFieldValidator ID="rfvVRegDt" runat="server" ControlToValidate="txtVRegDate"
                                            Display="None" ErrorMessage="Please Enter Vehicle Reg. Date" SetFocusOnError="true"
                                            ValidationGroup="ScheduleDtl">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtVRegDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtVRegDate" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mev1" runat="server" ControlExtender="meeToDt"
                                            ControlToValidate="txtVRegDate" Display="None" EmptyValueBlurredText="Empty"
                                            EmptyValueMessage="Please Enter Reg. Date" InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                            TooltipMessage="Please Enter Reg. Date" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator>--%>

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVRegDate"
                                                                        Display="None" ErrorMessage="Please Enter Reg. Date" SetFocusOnError="true"
                                                                 ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image123" TargetControlID="txtVRegDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtVRegDate" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mee2"
                                            ControlToValidate="txtVRegDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Reg. Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Reg. Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Reg. Date" ValidationGroup="ScheduleDtl"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Chassis No. </label>
                                    </div>
                                    <asp:TextBox ID="txtVChasis" runat="server" CssClass="form-control" MaxLength="25" ToolTip="Enter Chassis Number"
                                        TabIndex="8" onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVChasisNo" runat="server" ControlToValidate="txtVChasis"
                                        Display="None" ErrorMessage="Please Enter Chasis No." SetFocusOnError="true"
                                        ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Purchase Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image11">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtVPurchsDt" CssClass="form-control" runat="server" Style="z-index: 0;"
                                            TabIndex="11" ToolTip="Enter Purchase Date"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvVPchsDt" runat="server" ControlToValidate="txtVPurchsDt"
                                            Display="None" ErrorMessage="Please Enter Purchase Date" SetFocusOnError="true"
                                            ValidationGroup="ScheduleDtl">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image11" TargetControlID="txtVPurchsDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="mee1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtVPurchsDt" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mev2" runat="server" ControlExtender="mee1"
                                            ControlToValidate="txtVPurchsDt" Display="None" EmptyValueBlurredText="Empty"
                                            EmptyValueMessage="Please Enter Reg. Date" InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                            TooltipMessage="Please Enter Purchase Date" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator>--%>

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtVPurchsDt"
                                                                        Display="None" ErrorMessage="Please Enter Purchase Date" SetFocusOnError="true"
                                                                 ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image11" TargetControlID="txtVPurchsDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtVPurchsDt" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="mee2"
                                            ControlToValidate="txtVPurchsDt" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Purchase Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Purchase Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Purchase Date" ValidationGroup="ScheduleDtl"></ajaxToolKit:MaskedEditValidator>

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>All India Permit No. </label>
                                    </div>
                                    <asp:TextBox ID="txtVIPermit" runat="server" ToolTip="Enter All India Permit Number" MaxLength="25" TabIndex="12"
                                        onkeypress="return CheckAlphaNumeric(event,this);" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>RC Expiry Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtRCDate" runat="server" TabIndex="15" CssClass="form-control"
                                            MaxLength="15" ToolTip="Enter RC Expiry Date" Style="z-index: 0;"></asp:TextBox>
                                        <%--  OnClientDateSelectionChanged="checkDateFuture"  --  onblur="checkDate();"--%>
                                         <asp:RequiredFieldValidator ID="rfvRCDt" runat="server" ControlToValidate="txtRCDate"
                                                                        Display="None" ErrorMessage="Please Enter RC Expiry Date" SetFocusOnError="true"
                                                                 ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image3" TargetControlID="txtRCDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="mee2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtRCDate" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mev3" runat="server" ControlExtender="mee2"
                                            ControlToValidate="txtRCDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Reg. Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="RC Expiry Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Purchase Date" ValidationGroup="ScheduleDtl"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Type Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVType" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="16"
                                        AppendDataBoundItems="true" ToolTip="Select Vehicle Type Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVType" runat="server" ErrorMessage="Please Select Vehicle Type Name."
                                        ControlToValidate="ddlVType" InitialValue="0"
                                        Display="None" ValidationGroup="ScheduleDtl"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Status</label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistStatus" runat="server" RepeatDirection="Horizontal" TabIndex="17"
                                        ToolTip="Select Status">
                                        <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="2">In Active</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>


                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <div class="sub-heading">
                                <h5>Vendor (Whom Purchase)</h5>
                            </div>
                            <%--    <div class="box-tools pull-right">
                                        <img id="img1" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divServiceDateDetails')" />
                                    </div>--%>
                        </div>

                        <div class="col-12" id="divServiceDateDetails">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vendor Name</label>
                                    </div>
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" TabIndex="10"
                                        MaxLength="60" ToolTip="Enter Vendor Name"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars="./(), " TargetControlID="txtVendorName">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvVndrName" runat="server" ControlToValidate="txtVendorName"
                                        Display="None" ErrorMessage="Please Vendor Name" SetFocusOnError="true" ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Vendor Address</label>
                                    </div>
                                    <asp:TextBox ID="txtVendorAdd" TextMode="MultiLine" TabIndex="19" ToolTip="Enter Vendor Address"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div id="trHireDetails" runat="server" visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>For Hire Define Rates</h5>
                                </div>
                            </div>
                            <div class="col-12" id="divBasicDetails">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Hire Rate(Per Km)</label>
                                        </div>
                                        <asp:TextBox ID="txtHireRate" CssClass="form-control" onkeypress="return CheckNumeric(event,this);"
                                            runat="server" MaxLength="20" ToolTip="Enter Hire Rate(Per Km)"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Waiting Charge</label>
                                        </div>
                                        <asp:TextBox ID="txtWaitCharge" CssClass="form-control" onkeypress="return CheckNumeric(event,this);"
                                            runat="server" MaxLength="21" ToolTip="Enter Waiting Charge"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Driver TA</label>
                                        </div>
                                        <asp:TextBox ID="txtDvrTA" CssClass="form-control" onkeypress="return CheckNumeric(event,this);"
                                            runat="server" MaxLength="22" ToolTip="Enter Driver Travelling Allowance"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Start.Log No.</label>
                                        </div>
                                        <asp:TextBox ID="txtStrtLgNo" CssClass="form-control" onkeypress="return CheckNumeric(event,this);"
                                            runat="server" MaxLength="23" ToolTip="Enter Start.Log No"></asp:TextBox>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>


                    </asp:Panel>

                    <div class="col-12">
                        <asp:Panel ID="pnlAddHis" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" SkinID="LinkAddNew" CssClass="btn btn-primary"
                                    OnClick="btnAddNew_Click" Text="Add New" ToolTip="Click here to Add New Vehicle Details"></asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlButtons" runat="server">
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="ScheduleDtl"
                                    OnClick="btnSave_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="24"  UseSubmitBehavior="false" OnClientClick="handleButtonClick()" />

                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Go Back" TabIndex="26" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" TabIndex="25" />&nbsp;
                       
                            <asp:Button ID="btnReport" runat="server" Text="RC Expiry Report" CausesValidation="false"
                                CssClass="btn btn-info" ToolTip="Click here to Show RC Expiry Report" OnClick="btnReport_Click"
                                TabIndex="27" OnClientClick="return UserDeleteConfirmation();" />
                                <asp:HiddenField ID="hdnexpiryinput" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ScheduleDtl"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvDesg" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>MODEL
                                                    </th>
                                                    <th>R.C. BOOK NO.
                                                    </th>
                                                    <th>REGISTERED NO.
                                                    </th>
                                                    <th>STATUS
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
                                                CommandArgument='<%# Eval("VIDNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# ((Eval("NAME").ToString() != string.Empty) ? Eval("NAME") : "--")%>
                                        </td>
                                        <td>
                                            <%# ((Eval("MODEL").ToString() != string.Empty) ? Eval("MODEL") : "--")%>
                                        </td>
                                        <td>
                                            <%# ((Eval("RCBOOKNO").ToString() != string.Empty) ? Eval("RCBOOKNO") : "--")%>
                                        </td>
                                        <td>
                                            <%# ((Eval("REGNO").ToString() != string.Empty) ? Eval("REGNO") : "--")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ACTIVESTATUS").ToString() == "1" ? "Active" : "In Active"  %>'></asp:Label>
                                            <%--  <%# ((Eval("ACTIVESTATUS").ToString() != string.Empty) ? Eval("ACTIVESTATUS") : "--")%>--%>
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

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
        }

        function checkDate() {
            var EnteredDate = document.getElementById("ctl00_ContentPlaceHolder1_txtRCDate").value; //for javascript

            //var EnteredDate = $("#ctl00_ContentPlaceHolder1_txtExpiryDate").val(); // For JQuery

            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {

                alert("You cannot select future dates.");
                window.setTimeout(function () {
                    document.getElementById("ctl00_ContentPlaceHolder1_txtRCDate").focus();
                    document.getElementById("ctl00_ContentPlaceHolder1_txtRCDate").value = today.format("dd/MM/yyyy");
                }, 0);

            }

        }
    </script>



    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
        }

        function UserDeleteConfirmation() {
            var num = prompt("Insert Number of Days", "Type no. of days here");

            n = parseInt(num, 10);

            if (isNaN(n)) {
                alert("The input cannot be parsed to an integer");
                return false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnexpiryinput').value = n;
                return true;
            }

        }
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtVMake").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtVMake").focus();
            }

        }

    </script>

      <script>
          function handleButtonClick() {
              var button = document.getElementById('<%= btnSave.ClientID %>');

            // Disable the button and update text
            button.disabled = true;
            button.value = "Please Wait...";

            // Enable the button after 10 seconds
            setTimeout(function () {
                button.disabled = false;
                button.value = "Submit";
            }, 10000); // 10000 milliseconds = 10 seconds
        }
</script>
   
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
