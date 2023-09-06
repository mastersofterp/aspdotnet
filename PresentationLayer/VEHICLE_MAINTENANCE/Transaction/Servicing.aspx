<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Servicing.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_Servicing"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE SERVICING</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlPersInfo" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Vehicle Information</h5>
                                    </div>
                                </div>
                                <div class="col-12" id="divServiceDetails">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vehicle</label>
                                            </div>
                                            <asp:DropDownList ID="ddl" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="1"
                                                AppendDataBoundItems="true" ToolTip="Select Vehicle">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDDlN" runat="server" ControlToValidate="ddl" Display="None"
                                                ErrorMessage="Please Select Vehicle." SetFocusOnError="true" ValidationGroup="Schedule" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Workshop</label>
                                            </div>
                                            <asp:DropDownList ID="ddlWorkshp" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="true" ToolTip="Select WorkShop" TabIndex="2"
                                                OnSelectedIndexChanged="ddlWorkshp_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvDDLW" runat="server" ControlToValidate="ddlWorkshp"
                                                Display="None" ErrorMessage="Please Select Workshop" SetFocusOnError="true" ValidationGroup="Schedule"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trW" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Workshop Address</label>
                                            </div>
                                            <asp:Label ID="lblWrkshpDtl" runat="server" ToolTip="WorkShop Address" CssClass="form-control"
                                                Enabled="false"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bill No.</label>
                                            </div>
                                            <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control" TabIndex="3" MaxLength="15"
                                                onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Bill Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBillNo" runat="server" ControlToValidate="txtBillno"
                                                Display="None" ErrorMessage="Please Enter Bill No" SetFocusOnError="true"
                                                ValidationGroup="Schedule"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bill Amount </label>
                                            </div>
                                            <asp:TextBox ID="txtBllAmt" MaxLength="9" CssClass="form-control" TabIndex="4" onkeyup="validateNumeric(this);"
                                                runat="server" Text="0.00" Enabled="False" ToolTip="Enter Bill Amount"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Paid On </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgPdOn">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtPaidDt" CssClass="form-control" ToolTip="Enter Paid On Date" TabIndex="5"
                                                    runat="server" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtPaidDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtPaidDt"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                                                        ControlToValidate="txtPaidDt" EmptyValueMessage="Please Enter Amount Paid Date" IsValidEmpty="false"
                                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                            
                                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Payment Mode</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbtnMode" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" TabIndex="6" ToolTip="Select Payment Mode"
                                                OnSelectedIndexChanged="rdbtnMode_SelectedIndexChanged">
                                                <asp:ListItem Value="C" Selected="True">Cash&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="B">Cheque</asp:ListItem>
                                                <asp:ListItem Value="O">Online Transfer</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <%--  <div class="form-group col-lg-6 col-md-6 col-12" id="trChewue" runat="server" visible="false">--%>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="row" id="trChewue" runat="server" visible="false">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Chq. Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgChqDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtChqDt" CssClass="form-control" ToolTip="Enter Cheque Date" TabIndex="7"
                                                            runat="server" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceeChqDt" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgChqDt" TargetControlID="txtChqDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChqDt"
                                                            ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" IsValidEmpty="false"
                                                            ControlExtender="MaskedEditExtender5" ControlToValidate="txtChqDt" Display="None"
                                                            EmptyValueMessage="Please Enter Cheque Date" Text="*"
                                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                                  
                                                        </ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Chq. No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtChqNo" CssClass="form-control" runat="server" MaxLength="6" TabIndex="8"
                                                        onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Cheque Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvChqno" runat="server" ControlToValidate="txtChqNo"
                                                        Display="None" ErrorMessage="Please Enter Cheque No" SetFocusOnError="true" ValidationGroup="Schedule">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                        </div>
                                        <%--  <div class="form-group col-lg-6 col-md-6 col-12" id="divOTransfer" runat="server" visible="false">--%>
                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divOTransfer" runat="server" visible="false">
                                            <div class="row">
                                                <%--<div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="TransferDate">--%>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <label><span style="color: #FF0000">*</span>Transfer Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgTrDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtTranDate" CssClass="form-control" ToolTip="Enter Transfer Date" TabIndex="9"
                                                            runat="server" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgTrDt" TargetControlID="txtTranDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTranDate"
                                                            ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" IsValidEmpty="false"
                                                            ControlExtender="MaskedEditExtender5" ControlToValidate="txtTranDate" Display="None"
                                                            EmptyValueMessage="Please Enter Transfer Date" Text="*"
                                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                                  
                                                        </ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>
                                                <%-- <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="TransactionNo">--%>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <label><span style="color: #FF0000">*</span>Transaction No. :</label>
                                                    <asp:TextBox ID="txtTransactioNo" CssClass="form-control" runat="server" MaxLength="100" TabIndex="10"
                                                        onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Transaction Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTrNo" runat="server" ControlToValidate="txtTransactioNo"
                                                        Display="None" ErrorMessage="Please Enter Transaction Number" SetFocusOnError="true" ValidationGroup="Schedule">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>IN Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgINDt">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtINDt" CssClass="form-control" runat="server" onchange="CheckDate()" TabIndex="11"
                                                    ValidationGroup="Schedule" ToolTip="Enter IN Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceeINDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgINDt" TargetControlID="txtINDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtINDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" IsValidEmpty="false"
                                                    ControlToValidate="txtINDt" EmptyValueMessage="Please Enter IN Date" Display="None"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                              
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:HiddenField ID="hdnDate" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Out Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgOTDt">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtOUTDt" CssClass="form-control" runat="server" onchange="CheckDateOut()" TabIndex="12"
                                                    ValidationGroup="Schedule" ToolTip="Enter Out Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceeOTDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgOTDt" TargetControlID="txtOUTDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" MaskType="Date"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtOUTDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtOUTDt" EmptyValueMessage="Please Enter OUT Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Schedule">                                                         
                                                </ajaxToolKit:MaskedEditValidator>
                                                <%-- <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtInDt" ControlToValidate="txtOUTDt"
                                                                ErrorMessage="Out Date Should Greater Than or Equals To InDate." runat="server"
                                                                Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="Schedule">
                                                            </asp:CompareValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>In Time</label>
                                            </div>
                                            <asp:TextBox ID="txtINTime" CssClass="form-control" ToolTip="Enter In Time(AM)" TabIndex="13" runat="server">
                                            </asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meeINT" runat="server" TargetControlID="txtINTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <asp:RequiredFieldValidator ID="rfvINTime" runat="server" ControlToValidate="txtINTime"
                                                Display="None" ErrorMessage="Please Enter IN Time." SetFocusOnError="true" ValidationGroup="Schedule">
                                            </asp:RequiredFieldValidator>
                                           <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtINTime"
                                                Mask="99:99" MaskType="Time"
                                                AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender2"
                                                ControlToValidate="txtINTime" Display="None" EmptyValueBlurredText="Empty"
                                                InvalidValueMessage="In Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter In Time"
                                                SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Schedule" />--%>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Out Time</label>
                                            </div>
                                            <asp:TextBox ID="txtOuttym" CssClass="form-control" ToolTip="Enter Out Time(PM)" TabIndex="14" runat="server" onchange="Check_Time();">
                                            </asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meeOTTime" runat="server" TargetControlID="txtOuttym"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <asp:RequiredFieldValidator ID="rfvOutTime" runat="server" ControlToValidate="txtOuttym"
                                                Display="None" ErrorMessage="Please Enter OUT Time." SetFocusOnError="true" ValidationGroup="Schedule">
                                            </asp:RequiredFieldValidator>
                                             <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="txtOuttym"
                                                Mask="99:99" MaskType="Time"
                                                AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender2"
                                                ControlToValidate="txtOuttym" Display="None" EmptyValueBlurredText="Empty"
                                                InvalidValueMessage="Out Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter Out Time"
                                                SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Schedule" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Next Visit Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgNVD">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtNxtVstDt" CssClass="form-control" runat="server" ValidationGroup="Schedule" TabIndex="15"
                                                    ToolTip="Enter Next Visit Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgNVD" TargetControlID="txtNxtVstDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtNxtVstDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender4"
                                                    ControlToValidate="txtNxtVstDt" EmptyValueMessage="Please Enter Next Visiting Date"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator ID="rfvNextVDt" runat="server" ControlToValidate="txtNxtVstDt"
                                                    Display="None" ErrorMessage="Please Enter Next Visit Date." SetFocusOnError="true" ValidationGroup="Schedule">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" CssClass="form-control" ToolTip="Enter Remark If Any"
                                                runat="server" TabIndex="16"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Work Order No.</label>
                                            </div>
                                            <asp:TextBox ID="txtWrkOrdrNo" CssClass="form-control" ToolTip="Enter Work order Number"
                                                runat="server" MaxLength="30" TabIndex="17"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server"
                                                FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                ValidChars="-/ " TargetControlID="txtWrkOrdrNo">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvWrkOrdrNo" runat="server" ControlToValidate="txtWrkOrdrNo"
                                                Display="None" ErrorMessage="Please Enter Work Order No." SetFocusOnError="true"
                                                ValidationGroup="Schedule">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bill Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgBllDt">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtBllDt" CssClass="form-control" ToolTip="Enter Bill Date" runat="server"
                                                    TabIndex="18"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceBllDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgBllDt" TargetControlID="txtBllDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtBllDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trbank" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bank Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBnkName" CssClass="form-control" TabIndex="19" runat="server" MaxLength="200"
                                                onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Bank Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBnkName" runat="server" ControlToValidate="txtBnkName"
                                                Display="None" ErrorMessage="Please Enter Bank Name" SetFocusOnError="true" ValidationGroup="Schedule">
                                            </asp:RequiredFieldValidator>
                                        </div>




                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Billing Details</h5>
                                    </div>
                                </div>

                                <div class="col-12" id="divServiceDateDetails">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:Panel ID="pnlTo" runat="server">
                                            <div class="col-12">
                                                <%-- <div class="sub-heading">
                                                    <h5>Item Details</h5>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Item Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtItem" runat="server" MaxLength="45" CssClass="form-control" TabIndex="20"
                                                            onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter Item Name" ValidationGroup="Add"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItem"
                                                            Display="None" ErrorMessage="Please Enter Item Name" SetFocusOnError="true" ValidationGroup="Add">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Quantity</label>
                                                        </div>
                                                        <asp:TextBox ID="txtQty" runat="server" MaxLength="3" onkeypress="return CheckNumeric(event,this);"
                                                            CssClass="form-control" ToolTip="Enter Quantity" TabIndex="21" ValidationGroup="Add"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQty"
                                                            Display="None" ErrorMessage="Please Enter Quantity" SetFocusOnError="true" ValidationGroup="Add">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Item Amt</label>
                                                        </div>
                                                        <asp:TextBox ID="txtItmAmt" onkeypress="return CheckNumeric(event,this);" runat="server" MaxLength="9"
                                                            CssClass="form-control" ToolTip="Enter Item Amount" TabIndex="22" ValidationGroup="Add"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAmt" runat="server" ControlToValidate="txtItmAmt"
                                                            Display="None" ErrorMessage="Please Enter Item Amount" SetFocusOnError="true" ValidationGroup="Add">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAddTo" runat="server" OnClick="btnAddTo_Click" Text="Add" ValidationGroup="Add"
                                                    CssClass="btn btn-primary" ToolTip="Click here to Add Item Details" TabIndex="23" />
                                                <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Add" />

                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlItemList" runat="server">
                                                    <asp:ListView ID="lvTo" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Item List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th>Action
                                                                            </th>
                                                                            <th>Item
                                                                            </th>
                                                                            <th>Qty
                                                                            </th>
                                                                            <th>Amount
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SrNo") %>'
                                                                        ImageUrl="~/Images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                                    <asp:ImageButton ID="btnDeletel" runat="server" CommandArgument='<%# Eval("SrNo") %>'
                                                                        ImageUrl="~/Images/delete.png" OnClick="btnDeletel_Click" ToolTip="Delete Record" />
                                                                </td>
                                                                <td id="IOTRANNO" runat="server">
                                                                    <%# Eval("ITEM") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("QTY") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("AMT") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </asp:Panel>
                                    </asp:Panel>
                                </div>

                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </p>
                                </div>

                            </asp:Panel>

                            <div class="col-12">
                                <asp:Panel ID="pnlAddHis" runat="server">
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnAddNew" runat="server" SkinID="LinkAddNew" OnClick="btnAddNew_Click" Text="Add New"
                                            ToolTip="Click here to Add New Vehicle Servicing Details" CssClass="btn btn-primary" TabIndex="24"></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlButton" runat="server">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Schedule" TabIndex="25"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                            CssClass="btn btn-primary" ToolTip="Click here to Go Back" TabIndex="27" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="26"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to reset" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Schedule"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" HeaderText="Following Fields Are Mandatory" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvDesg" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Vehicle Servicing Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>VEHICLE NAME
                                                            </th>
                                                            <th>WORKSHOP
                                                            </th>
                                                            <th>BILL AMOUNT
                                                            </th>
                                                            <th>SERVICING DATE
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
                                                        CommandArgument='<%# Eval("SIDNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# ((Eval("NAME").ToString() != string.Empty) ? Eval("NAME") : "--")%>
                                                </td>
                                                <td>
                                                    <%# ((Eval("WNAME").ToString() != string.Empty) ? Eval("WNAME") : "--")%>
                                                </td>
                                                <td>
                                                    <%# ((Eval("BILLAMT").ToString() != string.Empty) ? Eval("BILLAMT") : "--")%>
                                                </td>
                                                <td>
                                                    <%# Eval("WSINDT", "{0:dd-MMM-yyyy}")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>


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
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        //Shaikh Juned 26-08-2022

        function Check_Time() {
            var In_Date = document.getElementById('<%=txtINDt.ClientID%>').value;
            var Out_Date = document.getElementById('<%=txtOUTDt.ClientID%>').value;
            var time = document.getElementById('<%=txtOuttym.ClientID%>').value;
            var hours = Number(time.match(/^(\d+)/)[1]);
            var minutes = Number(time.match(/:(\d+)/)[1]);
            var AMPM = time.match(/\s(.*)$/)[1];
            if (AMPM == "PM" && hours < 12) hours = hours + 12;
            if (AMPM == "AM" && hours == 12) hours = hours - 12;
            var sHours = hours.toString();
            var sMinutes = minutes.toString();
            if (hours < 10) sHours = "0" + sHours;
            if (minutes < 10) sMinutes = "0" + sMinutes;
            var depatrtime = sHours + ":" + sMinutes;
            var time = document.getElementById('<%=txtINTime.ClientID%>').value;
            var hours1 = Number(time.match(/^(\d+)/)[1]);
            var minutes1 = Number(time.match(/:(\d+)/)[1]);
            var AMPM1 = time.match(/\s(.*)$/)[1];
            if (AMPM1 == "PM" && hours1 < 12) hours1 = hours1 + 12;
            if (AMPM1 == "AM" && hours1 == 12) hours1 = hours1 - 12;
            var sHours1 = hours1.toString();
            var sMinutes1 = minutes1.toString();
            if (hours1 < 10) sHours1 = "0" + sHours1;
            if (minutes1 < 10) sMinutes1 = "0" + sMinutes1;
            var arrivaltime = sHours1 + ":" + sMinutes1;
            if (In_Date == Out_Date && arrivaltime == depatrtime) {
                alert('In Time and out Time should not be equal on Same Date..');
                document.getElementById('<%=txtOuttym.ClientID%>').value = '';


             }

             if (In_Date == Out_Date && arrivaltime > depatrtime) {
                 alert('Out Time should be greater then In Time..');
                 document.getElementById('<%=txtOuttym.ClientID%>').value = '';

               }
           }



    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
