<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="InvoiceEntry.aspx.cs" Inherits="Health_StockMaintenance_InvoiceEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INVOICE ENTRY FORM</h3>
                        </div>
                        <div class="box-body">
                            <asp:UpdatePanel ID="pnlup" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Invoice Information</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Invoice No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="50" ToolTip="Enter Invoice Number"
                                                        Enabled="true" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                    <asp:HiddenField ID="HdfInvoicNo" runat="server" />

                                                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="true" AutoPostBack="false" ToolTip="Select Invoice"
                                                        OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged"
                                                        Visible="false">
                                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvtxtInvoiceNo" runat="server" ControlToValidate="txtInvoiceNo"
                                                        Display="None" ErrorMessage="Invoice No Required" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Invoice Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImgtxtInvDt">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtInvDt" runat="server" ToolTip="Enter Invoice Date"
                                                            CssClass="form-control" Style="z-index: 0;" TabIndex="2"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtInvDt" runat="server" ControlToValidate="txtInvDt"
                                                            Display="None" ErrorMessage="Invoice Date Required" ValidationGroup="Submit" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImgtxtInvDt" TargetControlID="txtInvDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>D.M.No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDMNo" runat="server" CssClass="form-control"
                                                        ToolTip="Enter Delivery Memo No" TabIndex="3"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDMNo" runat="server" TargetControlID="txtDMNo"
                                                        FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars"
                                                        ValidChars="/\-*#()">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>D.M.Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImaCalSDate">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDMDt" runat="server" CssClass="form-control" ToolTip="Enter Delivery Memo Date"
                                                            TabIndex="4" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImaCalSDate" TargetControlID="txtDMDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Vendor Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control"
                                                        data-select2-enable="true" AppendDataBoundItems="true"
                                                        ToolTip="Select Vendor" TabIndex="5">
                                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                        Display="None" ErrorMessage="Select Vendor From List" InitialValue="0"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Receive Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImatxtRecDt">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtRecDt" runat="server" CssClass="form-control" ToolTip="Enter Recieve Date"
                                                            TabIndex="6" Style="z-index: 0;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtRecDt" runat="server" ControlToValidate="txtRecDt"
                                                            Display="None" ErrorMessage="Recive Date Required" ValidationGroup="Submit">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImatxtRecDt" TargetControlID="txtRecDt">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>P.O.No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" ToolTip="Enter Puchase Order No"
                                                        TabIndex="7" MaxLength="50"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbePONo" runat="server" TargetControlID="txtPONo"
                                                        FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters"
                                                        FilterMode="ValidChars" ValidChars="/\-*#()">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvtxtPONo" runat="server" ControlToValidate="txtPONo"
                                                                                Display="None" ErrorMessage="PO No. Required" ValidationGroup="Submit">
                                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>PO.Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImatxtPODt">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtPODt" runat="server" CssClass="form-control" ToolTip="Enter Purchase Order Date"
                                                            TabIndex="8" Style="z-index: 0;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtPODt" runat="server" ControlToValidate="txtPODt"
                                                            Display="None" ErrorMessage="PO Date. Required" ValidationGroup="Submit">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImatxtPODt" TargetControlID="txtPODt">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlItemInfo" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Item Information</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" TabIndex="9"
                                                        placeholder="Enter Character to Search" ToolTip="Enter Item Name"></asp:TextBox>
                                                    <asp:HiddenField ID="hfItemName" runat="server" />
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtItemName"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                        ServiceMethod="GetItemName" OnClientShowing="clientShowing" OnClientItemSelected="ItemName">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName"
                                                        Display="None" ErrorMessage="Enter Item Name." SetFocusOnError="True"
                                                        ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Unit</label>
                                                    </div>
                                                    <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Enter Unit"
                                                        MaxLength="10" onkeypress="return CheckNumeric(event, this);">0</asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Quantity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" TabIndex="11" MaxLength="10"
                                                        ToolTip="Enter quantity" onkeypress="return CheckNumeric(event, this);"
                                                        onblur="return calc1();">0</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQty"
                                                        Display="None" ErrorMessage="Enter quantity of item" SetFocusOnError="True"
                                                        ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Total Quantity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotQty" runat="server" CssClass="form-control" TabIndex="12" MaxLength="15"
                                                        ToolTip="Total quantity" onkeypress="return CheckNumeric(event, this);"
                                                        Enabled="false">0</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTotQty" runat="server" ControlToValidate="txtTotQty"
                                                        Display="None" ErrorMessage="Enter total quantity" SetFocusOnError="True"
                                                        ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cvTotQty" runat="server" ControlToValidate="txtTotQty" Display="None"
                                                        ErrorMessage="Enter Proper Value For Total Quantity." Operator="DataTypeCheck" Type="Integer"
                                                        ValidationGroup="StockItem"></asp:CompareValidator>
                                                    <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Rate</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="10" TabIndex="13"
                                                        ToolTip="Enter Rate Per Unit No" onkeypress="return CheckNumeric(event, this);"
                                                        onblur="return calc();">0</asp:TextBox><%--OnTextChanged="txtRate_TextChanged" --%>
                                                    <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" ControlToValidate="txtRate"
                                                        Display="None" SetFocusOnError="True" ErrorMessage="Enter Rate" ValidationGroup="StockItem">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cmvtxtRate" runat="server" ControlToValidate="txtRate"
                                                        Display="None" ErrorMessage="Enter Proper Value For Rate" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="StockItem"></asp:CompareValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAMT" runat="server" CssClass="form-control" MaxLength="25"
                                                        Enabled="false" ToolTip="Amount"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Batch No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtBatchNo" runat="server" CssClass="form-control" MaxLength="15" TabIndex="14"
                                                        ToolTip="Enter Batch No"
                                                        onkeypress="return CheckAlphaNumeric(event, this);">0</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvBatchNo" runat="server" ControlToValidate="txtBatchNo"
                                                        Display="None" SetFocusOnError="True" ErrorMessage="Enter Batch No."
                                                        ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Expiry Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="ImgBntCalc">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" ToolTip="Select Expiry Date"
                                                            TabIndex="15" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtExpiryDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeExpiryDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExpiryDate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevExpiryDate" runat="server"
                                                            ControlExtender="meeExpiryDate" ControlToValidate="txtExpiryDate" IsValidEmpty="true"
                                                            InvalidValueMessage="Expiry Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="StockItem" SetFocusOnError="true" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtExpiryDate" runat="server" ErrorMessage="Please Enter Expiry Date."
                                                            ControlToValidate="txtExpiryDate" SetFocusOnError="True"
                                                            Display="None" ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Mfg Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtMFGDate" runat="server" CssClass="form-control" ToolTip="Select Manufacturing Date"
                                                            TabIndex="16" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtMFGDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtMFGDate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                            ControlExtender="MaskedEditExtender1" ControlToValidate="txtMFGDate" IsValidEmpty="true"
                                                            InvalidValueMessage="Manufacturing Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="StockItem" SetFocusOnError="true" />
                                                        <asp:RequiredFieldValidator ID="rfvMFGDate" runat="server" ErrorMessage="Please Enter Manufacturing Date."
                                                            ControlToValidate="txtMFGDate"
                                                            Display="None" SetFocusOnError="True" ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Item"
                                                TabIndex="17" ValidationGroup="StockItem" CssClass="btn btn-outline-primary"
                                                ToolTip="Click here to Add Item Information" />
                                            <asp:Button ID="bnCancel" runat="server" OnClick="bnCancel_Click" Text="Cancel"
                                                TabIndex="18" CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="StockItem" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="pnlItemList" runat="server">
                                                <asp:ListView ID="lvItem" runat="server" Visible="false">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <div class="sub-heading">
                                                                <h5>Item List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Delete
                                                                        </th>
                                                                        <th>SRNO</th>
                                                                        <th>Item Name
                                                                        </th>
                                                                        <th>Unit
                                                                        </th>
                                                                        <th>Quantity
                                                                        </th>
                                                                        <th>Total Qty.
                                                                        </th>
                                                                        <th>Rate
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                        <th>Batch No.
                                                                        </th>
                                                                        <th>Expiry Date
                                                                        </th>
                                                                        <th>Mfg Date
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
                                                                    ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" ToolTip="Delete Record" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("SRNO") %>
                                                            </td>
                                                            <td>
                                                                <%--<td id="IOTRANNO" runat="server">--%>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblunit" runat="server" Text='<%# Eval("UNIT") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QUANTITY") %>' />
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
                                                                <asp:Label ID="lblBatch" runat="server" Text='<%# Eval("BATCHNO") %>' />
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
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Total Gross Amt :</b>
                                                            <a class="sub-label">
                                                                <asp:Label runat="server" ID="lblGrossAmt" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Total Item Qty :</b>
                                                            <a class="sub-label">
                                                                <asp:Label runat="server" ID="lblItemQty" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlExtraCharge" runat="server">
                                        <div class="col-12 mt-3">
                                            <div class="sub-heading">
                                                <h5>Extra Charges OR Discount</h5>
                                            </div>
                                            <div class="table table-responsive">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <td>Additional Charges OR Discount
                                                            </td>
                                                            <td>If Discount
                                                            </td>
                                                            <td>Percentage(%)
                                                            </td>
                                                            <td>Amount
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEcharge" CssClass="form-control" runat="server" TabIndex="19"
                                                                    onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Additional Charges OR Discount 1"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkDiscount" runat="server" Checked="false" ToolTip="Check If Discount" TabIndex="20" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEPer" runat="server" CssClass="form-control" TabIndex="21"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Percentage(%)">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEPer" runat="server" ControlToValidate="txtEPer"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Percntage At Row1 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEAmt" runat="server" CssClass="form-control" TabIndex="22"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Amount 1">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEAmt" runat="server" ControlToValidate="txtEAmt"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Amount At Row1 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEcharge1" CssClass="form-control" runat="server" TabIndex="24"
                                                                    onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Additional Charges OR Discount 2"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkDiscount1" runat="server" ToolTip="Check If Discount" Checked="false" TabIndex="25" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEPer1" runat="server" CssClass="form-control" TabIndex="26"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Percentage(%)">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEPer1" runat="server" ControlToValidate="txtEPer1"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Percntage At Row2 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="txtEAmt1" CssClass="form-control" runat="server" TabIndex="27"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Amount 2">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEAmt1" runat="server" ControlToValidate="txtEAmt1"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Amount At Row2 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEcharge2" runat="server" CssClass="form-control" TabIndex="29"
                                                                    onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Additional Charges OR Discount 3"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkDiscount2" runat="server" ToolTip="Check If Discount" Checked="false" TabIndex="30" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEPer2" runat="server" CssClass="form-control" TabIndex="31"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Percentage(%)">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEPer2" runat="server" ControlToValidate="txtEPer2"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Percntage At Row3 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEAmt2" runat="server" CssClass="form-control" TabIndex="32"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Amount 3">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEAmt2" runat="server" ControlToValidate="txtEAmt2"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Amount At Row3 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEcharge3" runat="server" CssClass="form-control" TabIndex="34"
                                                                    onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Additional Charges OR Discount 4"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkDiscount3" runat="server" ToolTip="Check If Discount" Checked="false" TabIndex="35" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEPer3" runat="server" CssClass="form-control" TabIndex="36"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Percentage(%)">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEPer3" runat="server" ControlToValidate="txtEPer3"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Percntage At Row4 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator></td>
                                                            <td>
                                                                <asp:TextBox ID="txtEAmt3" runat="server" CssClass="form-control" TabIndex="37"
                                                                    onkeypress="return CheckNumeric(event, this);" ToolTip="Enter Amount 4">0</asp:TextBox>
                                                                <asp:CompareValidator ID="cmptxtEAmt3" runat="server" ControlToValidate="txtEAmt3"
                                                                    Display="None" Operator="DataTypeCheck" Type="Double"
                                                                    ErrorMessage="Additional Chrges or Discount Amount At Row4 having Invalid Value"
                                                                    ValidationGroup="Echarge"></asp:CompareValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">Net Amount:</span>
                                                            </td>
                                                            <td>
                                                                <span style="font-weight: bold;">
                                                                    <asp:Label ID="lblNetAmt" runat="server"></asp:Label>
                                                                </span>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <asp:Button runat="server" ID="btnProcess" Text="Calculate" ValidationGroup="Echarge" TabIndex="23"
                                                                    OnClick="btnProcess_Click" CssClass="btn btn-outline-primary" />
                                                                <asp:ValidationSummary ID="validsumary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                                    ValidationGroup="Echarge" DisplayMode="List" />

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Remark</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" ToolTip="Enter Remark." TabIndex="39"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                                ValidationGroup="Submit" TabIndex="40" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnModify" runat="server" Text="Modify" OnClick="btnModify_Click"
                                                TabIndex="41" Visible="false" CssClass="btn btn-outline-primary" ToolTip="Click here to Modify" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                TabIndex="42" CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" />
                                            <asp:Button ID="btnReport" runat="server" Text="Consolidate Report" OnClick="btnReport_Click"
                                                TabIndex="43" CssClass="btn btn-outline-info" ToolTip="Click here to Show Report" />
                                            <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="Submit" />
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlReport" runat="server" Visible="false">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Invoice Report</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Invoice Number</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlInv" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlInv_SelectedIndexChanged"
                                                        ToolTip="Select Invoice Number">
                                                        <asp:ListItem Enabled="true" Selected="True" Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnRpt" runat="server" Text=" Show Report" CssClass="btn btn-outline-info"
                                                OnClick="btnRpt_Click" ToolTip="Click here to Show Report" />
                                            <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-outline-primary"
                                                OnClick="btnback_Click" ToolTip="Click here to Go Back" />

                                        </div>

                                    </asp:Panel>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlInvoiceList" runat="server">
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
                                                                    <th>DM No
                                                                    </th>
                                                                    <th>DM Date
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
                                                <EmptyDataTemplate>
                                                    <div class="data_label text-center">
                                                        <label>-- No Record Found --</label>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png"
                                                                CommandArgument='<%# Eval("INVTRNO")%>'
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
                                                            <asp:Button ID="btnPrintInvoice" runat="server" ToolTip="Print Report" CssClass="btn btn-info"
                                                                CommandArgument='<%# Eval("INVTRNO")%>' Text="Print" OnClick="btnPrintInvoice_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <div class="vista-grid_datapager d-none">
                                            <div class="text-center">
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
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:AsyncPostBackTrigger ControlID="btnReport" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnModify" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                        <div class="text-center">
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

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
                var qty = document.getElementById('<%= txtQty.ClientID %>').value;
                var totamt;
                totamt = Number(rate) * Number(qty);
                document.getElementById('<%= txtAMT.ClientID %>').value = Number(totamt);
                // alert(totamt);
            }
            catch (e) {
                // e.message();
                message.innerHTML = "Error: " + e + ".";
            }
        }

        function calc1() {
            try {

                var unit = document.getElementById('<%= txtUnit.ClientID %>').value;
                var qty = document.getElementById('<%= txtQty.ClientID %>').value;
                var totqty;
                totqty = Number(unit) * Number(qty);
                document.getElementById('<%= txtTotQty.ClientID %>').value = Number(totqty);
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
            var expAlphabet = /^[A-Za-z .]+$/;
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

    </script>
</asp:Content>

