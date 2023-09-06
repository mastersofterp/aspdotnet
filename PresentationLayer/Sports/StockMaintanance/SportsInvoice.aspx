<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SportsInvoice.aspx.cs" Inherits="Sports_StockMaintanance_SportsInvoice" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../Css/master.css" rel="stylesheet" />--%>
    <script>
        function pastDateValidation() {
            var d = document.getElementById("startingOn").value;
            if (new Date(d) < new Date()) {
                alert(d);
                document.getElementById("txtExpiryDate").value = "";
            }

        }
        //Modified by Saahil Trivedi 18-02-2022
        function deleletconfig() {

            var del = confirm("Are you sure you want to Delete this Record?");
            if (del == true) {
                alert("Record Deleted Successfully.")
            } else {
                alert("Record Not Deleted")
            }
            return del;
        }
    </script>
   
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlMain"
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
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INVOICE ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Invoice Information</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice No.</label>
                                            </div>
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="50" Enabled="true" TabIndex="1"
                                                CssClass="form-control" ToolTip="Enter Invoice No."></asp:TextBox>
                                            <asp:HiddenField ID="HdfInvoicNo" runat="server" />
                                            <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="dropdownlist" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                                OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged" ToolTip="select Invoice" Visible="false" Width="185px">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvtxtInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                                                Display="None" ErrorMessage="Invoice No. Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImgtxtInvDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtInvDt" runat="server" CssClass="form-control" ToolTip="Enter Invoice Date." TabIndex="2"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtInvDt" runat="server" ControlToValidate="txtInvDt"
                                                    Display="None" ErrorMessage="Invoice Date Required" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgtxtInvDt" TargetControlID="txtInvDt">
                                                </ajaxToolKit:CalendarExtender>--%>
                                                 <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgtxtInvDt" 
                                                                TargetControlID="txtInvDt">  <%--OnClientDateSelectionChanged="checkDate1"--%>
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtInvDt">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                                                                ControlExtender="MaskedEditExtender5" ControlToValidate="txtInvDt" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Invoice Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="Invoice Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter Invoice Date" IsValidEmpty="false"
                                                                ValidationGroup="Submit"> <%----%>
                                                            </ajaxToolKit:MaskedEditValidator>
                                             
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>D.M.No.</label>
                                            </div>
                                            <asp:TextBox ID="txtDMNo" runat="server" CssClass="form-control" ToolTip="Enter Delivery Memo No." TabIndex="3"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDMNo" runat="server" TargetControlID="txtDMNo"
                                                FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="/\-*#()">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDMNo"
                                                Display="None" ErrorMessage="D.M. No. Is Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>D.M.Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalSDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDMDt" runat="server" CssClass="form-control" ToolTip="Enter Delivery Memo Date." TabIndex="4"></asp:TextBox>
                                                <%--<ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalSDate" TargetControlID="txtDMDt">
                                                </ajaxToolKit:CalendarExtender>--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImaCalSDate" 
                                                                TargetControlID="txtDMDt">  <%--OnClientDateSelectionChanged="checkDate1"--%>
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDMDt">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                                                                ControlExtender="MaskedEditExtender4" ControlToValidate="txtDMDt" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter D.M. Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="D.M. Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter D.M. Date" IsValidEmpty="false"
                                                                ValidationGroup="Submit"> <%----%>
                                                            </ajaxToolKit:MaskedEditValidator>


                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vendor Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="dropdownlist" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select Vendor"
                                                TabIndex="5">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Select Vendor From List" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Recieve Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImatxtRecDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtRecDt" runat="server" CssClass="form-control" ToolTip="Enter Recieve Date." TabIndex="6"></asp:TextBox>
                                              <%--  <asp:RequiredFieldValidator ID="rfvtxtRecDt" runat="server" ControlToValidate="txtRecDt"
                                                    Display="None" ErrorMessage="Recive Date Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImatxtRecDt" TargetControlID="txtRecDt">
                                                </ajaxToolKit:CalendarExtender>--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImatxtRecDt" 
                                                                TargetControlID="txtRecDt">  <%--OnClientDateSelectionChanged="checkDate1"--%>
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtRecDt">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                                                                ControlExtender="MaskedEditExtender3" ControlToValidate="txtRecDt" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Recieve Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="Recieve Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter Recieve Date" IsValidEmpty="false"
                                                                ValidationGroup="Submit"> <%----%>
                                                            </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>P.O.No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" ToolTip="Enter Puchase Order No." TabIndex="7" MaxLength="50"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePONo" runat="server" TargetControlID="txtPONo"
                                                FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="/\-*#()">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPONo"
                                                Display="None" ErrorMessage="P.O. No. Is Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>P.O.Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImatxtPODt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtPODt" runat="server" CssClass="form-control" ToolTip="Enter Purchase Order Date." TabIndex="8"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtPODt" runat="server" ControlToValidate="txtPODt"
                                                    Display="None" ErrorMessage="PO Date. Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImatxtPODt" TargetControlID="txtPODt">
                                                </ajaxToolKit:CalendarExtender>--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImatxtPODt" 
                                                                TargetControlID="txtPODt">  <%--OnClientDateSelectionChanged="checkDate1"--%>
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPODt">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                                ControlExtender="MaskedEditExtender2" ControlToValidate="txtPODt" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter P.O Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="P.O Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter P.O Date" IsValidEmpty="false"
                                                                ValidationGroup="Submit"> <%----%>
                                                            </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Item Information</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Name </label>
                                            </div>
                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" ToolTip="Enter Item Name" TabIndex="9" placeholder="Enter Character to Search" onkeypress="return CheckAlphabet(event, this);"></asp:TextBox>
                                            <asp:HiddenField ID="hfItemName" runat="server" />
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                Enabled="True" TargetControlID="txtItemName" CompletionSetCount="10" ServiceMethod="GetItemName"
                                                MinimumPrefixLength="2" CompletionInterval="100" OnClientItemSelected="ItemName"
                                                EnableCaching="false" 
                                                FirstRowSelected="false" 
                                                
                                                >
                                            </cc1:AutoCompleteExtender>

                                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName"
                                                Display="None" ErrorMessage="Enter Item Name." SetFocusOnError="True" ValidationGroup="StockItem"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Unit</label>
                                            </div>
                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Unit" AppendDataBoundItems="true" TabIndex="10">
                                                <asp:ListItem Enabled="true" Selected="True" Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlUnit"
                                                Display="None" SetFocusOnError="True" ErrorMessage="Select Unit" InitialValue="0" ValidationGroup="StockItem"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Total Qty.</label>
                                            </div>
                                            <asp:TextBox ID="txtTotQty" runat="server" CssClass="form-control" ToolTip="Enter Total Quantity" TabIndex="12" MaxLength="11"
                                                onkeypress="return CheckNumeric(event, this);" onblur="return calc();">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTotQty" runat="server" ControlToValidate="txtTotQty"
                                                Display="None" ErrorMessage="Enter total quantity" SetFocusOnError="True" ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                           <%-- <asp:CompareValidator ID="cvTotQty" runat="server" ControlToValidate="txtTotQty" Display="None"
                                                ErrorMessage="Enter Proper Value For Total Quantity." Operator="DataTypeCheck" Type="Integer"
                                                ValidationGroup="StockItem"></asp:CompareValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    TargetControlID="txtTotQty"
                                                                    FilterType="Custom"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="1234567890">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Rate</label>
                                            </div>
                                            <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="10" TabIndex="12"
                                                ToolTip="Enter Rate Per Unit No." onkeypress="return CheckNumeric(event, this);" onblur="return calc();">0</asp:TextBox><%--OnTextChanged="txtRate_TextChanged" --%>
                                            <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" ControlToValidate="txtRate"
                                                Display="None" SetFocusOnError="True" ErrorMessage="Enter Rate" ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                          <%--  <asp:CompareValidator ID="cmvtxtRate" runat="server" ControlToValidate="txtRate"
                                                Display="None" ErrorMessage="Enter Proper Value For Rate" Operator="DataTypeCheck"
                                                Type="Double" ValidationGroup="StockItem"></asp:CompareValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftb1" runat="server"
                                                                    TargetControlID="txtRate"
                                                                    FilterType="Custom"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="1234567890.">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAMT" runat="server" CssClass="form-control" ToolTip="Total Amount" MaxLength="25" Enabled="false"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Warranty Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" ToolTip="Select Warranty Date." TabIndex="13"></asp:TextBox>

                                               <%-- <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtExpiryDate" OnClientDateSelectionChanged="checkDate">
                                                </ajaxToolKit:CalendarExtender>--%>
                                               <%-- <ajaxToolKit:MaskedEditExtender ID="meeExpiryDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExpiryDate" />--%>
                                              <%--  <ajaxToolKit:MaskedEditValidator ID="mevExpiryDate" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                                    ControlExtender="meeExpiryDate" ControlToValidate="txtExpiryDate" IsValidEmpty="true"
                                                    InvalidValueMessage="Expiry Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="StockItem" SetFocusOnError="true" />--%>
                                                 <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1" OnClientDateSelectionChanged="checkDate"
                                                                TargetControlID="txtExpiryDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeExpiryDate" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpiryDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevExpiryDate" runat="server"
                                                                ControlExtender="meeExpiryDate" ControlToValidate="txtExpiryDate" Display="None"
                                                                EmptyValueBlurredText="Empty" 
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="Warranty Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter Warranty Date" IsValidEmpty="true"
                                                                ValidationGroup="StockItem">  <%--EmptyValueMessage="Please Enter Warranty Date"--%>
                                                            </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Delivery Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtMFGDate" runat="server" CssClass="form-control" ToolTip="Select Delivery Date." TabIndex="14"></asp:TextBox>

                                                <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtMFGDate" OnClientDateSelectionChanged="checkDate1">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtMFGDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Valid Date"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtMFGDate" IsValidEmpty="true"
                                                    InvalidValueMessage="Delivery Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="StockItem" SetFocusOnError="true" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image2" OnClientDateSelectionChanged="checkDate1"
                                                                TargetControlID="txtMFGDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtMFGDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                                ControlExtender="MaskedEditExtender1" ControlToValidate="txtMFGDate" Display="None"
                                                                EmptyValueBlurredText="Empty" 
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="Delivery Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter Delivery Date" IsValidEmpty="true"
                                                                ValidationGroup="StockItem"> <%--EmptyValueMessage="Please Enter Delivery Date"--%>
                                                            </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Item" TabIndex="15" ValidationGroup="StockItem" CssClass="btn btn-primary" ToolTip="Click here to Add" />
                                <asp:Button ID="bnCancel" runat="server" OnClick="bnCancel_Click" Text="Cancel" TabIndex="16" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" ValidationGroup="StockItem" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvItem" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <%--<div class="sub-heading">
                                                    Item List
                                                </div>--%>
                                                <div class="sub-heading">
                                                    <h5>ITEM LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Delete
                                                            </th>
                                                            <th>SRNO
                                                            </th>
                                                            <th>Item Name
                                                            </th>
                                                            <th>Unit
                                                            </th>
                                                            <th>Total Qty.
                                                            </th>
                                                            <th>Rate
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Warranty Date
                                                            </th>
                                                            <th>Delivery Date
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" OnClientClick ="return deleletconfig();"  />
                                                </td>
                                                <td>
                                                    <%# Eval("SRNO") %>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME") %>' />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblunit" runat="server" Text='<%# Eval("UNIT_NAME") %>' />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotQty" runat="server" Text='<%# Eval("TOT_QTY") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("AMOUNT") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExpiry" runat="server" Text='<%# Eval("EXPIRY_DATE","{0:dd-MMM-yyyy}") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMfg" runat="server" Text='<%# Eval("MFG_DATE","{0:dd-MMM-yyyy}") %>' />
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                            <div class="col-12 mt-2">
                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Gross Amt :</b>
                                                <a class="sub-label">
                                                    <asp:Label runat="server" ID="lblGrossAmt" Width="20%" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Item Qty :</b>
                                                <a class="sub-label">
                                                    <asp:Label runat="server" ID="lblItemQty" Width="20%" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel4" runat="server">
                                    <div class="sub-heading">
                                        <h5>Extra Charges OR Discount</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Additional Charges OR Discount 
                                                </th>
                                                <th>If Discount
                                                </th>
                                                <th>Percentage(%) 
                                                </th>
                                                <th>Amount
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtEcharge" CssClass="form-control" ToolTip="Enter Other Changes" runat="server" TabIndex="17" onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDiscount" runat="server" Checked="false" TabIndex="18" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEPer" runat="server" CssClass="form-control" ToolTip="Enter percentage" TabIndex="19" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEPer" runat="server" ControlToValidate="txtEPer"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Percntage At Row1 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEAmt" runat="server" CssClass="form-control" ToolTip="Enter Amount" TabIndex="20" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEAmt" runat="server" ControlToValidate="txtEAmt"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Amount At Row1 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtEcharge1" CssClass="form-control" ToolTip="Enter Other Changes" runat="server" TabIndex="21" onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDiscount1" runat="server" Checked="false" TabIndex="25" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEPer1" CssClass="form-control" ToolTip="Enter percentage" runat="server" TabIndex="22" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEPer1" runat="server" ControlToValidate="txtEPer1"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Percntage At Row2 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEAmt1" CssClass="form-control" ToolTip="Enter Amount" runat="server" TabIndex="23" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEAmt1" runat="server" ControlToValidate="txtEAmt1"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Amount At Row2 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtEcharge2" runat="server" CssClass="form-control" ToolTip="Enter Other Changes" TabIndex="25" onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDiscount2" runat="server" Checked="false" TabIndex="26" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEPer2" runat="server" CssClass="form-control" ToolTip="Enter percentage" TabIndex="27" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEPer2" runat="server" ControlToValidate="txtEPer2"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Percntage At Row3 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEAmt2" runat="server" CssClass="form-control" ToolTip="Enter Amount" TabIndex="28" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEAmt2" runat="server" ControlToValidate="txtEAmt2"
                                                        Display="None" Operator="DataTypeCheck" Type="Double" ErrorMessage="Additional Chrges or Discount Amount At Row3 having Invalid Value"
                                                        ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <asp:TextBox ID="txtEcharge3" runat="server" CssClass="form-control" onkeypress="return CheckAlphaNumeric(event, this);" TabIndex="30" ToolTip="Enter Other Changes"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDiscount3" runat="server" Checked="false" TabIndex="31" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEPer3" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event, this);" TabIndex="32" ToolTip="Enter percentage">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEPer3" runat="server" ControlToValidate="txtEPer3" Display="None" ErrorMessage="Additional Chrges or Discount Percntage At Row4 having Invalid Value" Operator="DataTypeCheck" Type="Double" ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEAmt3" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event, this);" TabIndex="33" ToolTip="Enter Amount">0</asp:TextBox>
                                                    <asp:CompareValidator ID="cmptxtEAmt3" runat="server" ControlToValidate="txtEAmt3" Display="None" ErrorMessage="Additional Chrges or Discount Amount At Row4 having Invalid Value" Operator="DataTypeCheck" Type="Double" ValidationGroup="Echarge"></asp:CompareValidator>
                                                </td>
                                        </tbody>
                                    </table>
                                    <div class="row mt-5">
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Net Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblNetAmt" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>

                                        </div>
                                        <div class="col-lg-2 col-md-6 col-12 mt-4">

                                            <asp:Button runat="server" ID="btnProcess" Text="Calculate" ValidationGroup="Echarge" TabIndex="35" OnClick="btnProcess_Click" CssClass="btn btn-primary" ToolTip="Click here to Calculate" />
                                            <asp:ValidationSummary ID="validsumary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Echarge" DisplayMode="List" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" ToolTip="Enter Remark." TabIndex="36"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemark"
                                                        Display="None" ErrorMessage="Remark Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="col-12 mt-3 btn-footer">
                                        <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" ValidationGroup="Submit" TabIndex="37" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                        <asp:Button ID="btnModify" runat="server" Text="Modify" OnClick="btnModify_Click" TabIndex="38" Visible="false" />
                                         <asp:Button ID="btnReport" runat="server" Text="Consolidate Report" OnClick="btnReport_Click" TabIndex="40" CssClass="btn btn-info" ToolTip="Click to get Report" />
                                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="39" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                                    
                                         </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvGazette" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Invoice List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Invoice Number
                                                            </th>
                                                            <th>Invoice Date
                                                            </th>
                                                            <th>D.M. No.
                                                            </th>
                                                            <th>D.M. Date
                                                            </th>
                                                            <th>Vendor Name
                                                            </th>
                                                            <th>Receive Date
                                                            </th>
                                                            <th>Print
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("INVTRNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp; <%----%>
                                                </td>
                                                <td>
                                                    <%# Eval("INVNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("INVDT","{0:dd MMM, yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DMNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DMDT","{0:dd MMM, yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RECDT","{0:dd MMM, yyyy}")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrintInvoice" runat="server" ToolTip="Print Report" CommandArgument='<%# Eval("INVTRNO")%>' Text="Print" CssClass="btn btn-primary" OnClick="btnPrintInvoice_Click" /> &nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvGazette" PageSize="15"
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];
        }

        function calc() {
            try {
                var rate = document.getElementById('<%= txtRate.ClientID %>').value;
                var qty = document.getElementById('<%= txtTotQty.ClientID %>').value;
                var totamt;
                //if (qty == "0")
                //{
                //    alert("Please Enter Quantity.");
                //    return;
                //}

                totamt = Number(rate) * Number(qty);
                document.getElementById('<%= txtAMT.ClientID %>').value = Number(totamt);
                // alert(totamt);
            }
            catch (e) {
                // e.message();
                message.innerHTML = "Error: " + e + ".";
            }
        }


        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select Past dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function checkDate1(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
         
        }
    </script>
</asp:Content>

