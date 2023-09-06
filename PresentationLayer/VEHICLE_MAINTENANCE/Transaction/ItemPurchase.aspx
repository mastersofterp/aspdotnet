<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ItemPurchase.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_ItemPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEMS(FUEL/INDENT) PURCHASE ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <%--<div class=" sub-heading">
                                    <h5>Item Purchase Details</h5>
                                </div>--%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divitemtype" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItemType" runat="server" TabIndex="1" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Type" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Fuel</asp:ListItem>
                                            <asp:ListItem Value="2">Indent</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvItemType" ValidationGroup="Submit" ControlToValidate="ddlItemType" Display="None"
                                            ErrorMessage="Please Select Item Type." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divitemname">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItemName" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Name" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvItemName" ValidationGroup="Submit" ControlToValidate="ddlItemName" Display="None"
                                            ErrorMessage="Please Select Item Name." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnRate" runat="server" Value="0" />
                                    </div>

                                    <%--  //--------start----28-03-2023---------%>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divvendor" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vendor Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVendorName" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Name" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit" ControlToValidate="ddlVendorName" Display="None"
                                            ErrorMessage="Please Select Vendor Name." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    </div>

                                    <%--  //--------end----28-03-2023---------%>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpurchasedate">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Purchased Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtPDate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Enter Purchased Date "></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtPDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtPDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                ControlExtender="meeFromDate" ControlToValidate="txtPDate" IsValidEmpty="true"
                                                InvalidValueMessage="Purchased Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Purchased Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                                                ErrorMessage="Please Enter Purchased Date" ControlToValidate="txtPDate" Display="None" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <%-- //-------start----28-03-2023--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divcrn" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>CRN </label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtCRN" runat="server" MaxLength="50" TabIndex="4" CssClass="form-control"
                                                ToolTip="Enter CRN"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Enter CRN."
                                                ValidationGroup="Submit" ControlToValidate="txtCRN"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divpurchasefor" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Purchase For</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPurchaseFor" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Purchase For" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Transport</asp:ListItem>
                                            <asp:ListItem Value="2">Power Room</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit" ControlToValidate="ddlPurchaseFor" Display="None"
                                            ErrorMessage="Please Select Purchase For." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPurchasecoupnnumber" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Purchase Coupon Number</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtPurchaseCouponNumber" runat="server" MaxLength="50" TabIndex="4" CssClass="form-control"
                                                ToolTip="Enter Purchase Coupon Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Enter Purchase Coupon Number."
                                                ValidationGroup="Submit" ControlToValidate="txtPurchaseCouponNumber"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <%-- //-------end----28-03-2023--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divquantitypur">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Quantity Purchased </label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="8" TabIndex="4" CssClass="form-control"
                                                onchange="QuaAmount();" ToolTip="Enter Quantity Purchased"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Enter Purchased Quantity."
                                                ValidationGroup="Submit" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                TargetControlID="txtQuantity" ValidChars="0123456789."> <%--FilterType="Numbers"--%>
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <div class="input-group-addon">
                                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divrate">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Rate</label>
                                        </div>
                                        <asp:TextBox ID="txtRate" runat="server" MaxLength="10" TabIndex="5" CssClass="form-control"
                                            onchange="QuaAmount();" ToolTip="Enter Rate"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Rate."
                                            ValidationGroup="Submit" ControlToValidate="txtRate"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="." TargetControlID="txtRate">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divtotalamt">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Total Amount </label>
                                        </div>
                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control" ToolTip="Total Amount"
                                            TabIndex="6" Enabled="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Total Amount."
                                            ValidationGroup="Submit" ControlToValidate="txtAmt"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divrdoitemtype" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Item Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="RdoItemType" runat="server" ToolTip="Select Report In Type"
                                            RepeatDirection="Horizontal" TabIndex="6">
                                            <%-- <asp:ListItem Selected="True" Value="No Export">Normal Report</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="Fuel">Fuel&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="Indent">Indent&nbsp;&nbsp;</asp:ListItem>
                                            <%--<asp:ListItem Value="doc">MS-Word&nbsp;&nbsp;</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </div>

                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divfrom" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="img1233234">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="33" CssClass="form-control" ToolTip="Enter Purchased Date "></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="img1233234" TargetControlID="txtPDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                ControlExtender="meeFromDate" ControlToValidate="txtPDate" IsValidEmpty="true"
                                                InvalidValueMessage="From Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select From Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                ErrorMessage="Please Enter From Date" ControlToValidate="txtFromDate" Display="None" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfrom" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImgBntCalc">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter Fuel From Date"
                                                TabIndex="4" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="ImgBntCalc" TargetControlID="txtFromDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtFromDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                ControlToValidate="txtFromDate" IsValidEmpty="true" Display="None" Text="*"
                                                ErrorMessage="From Date is invalid (Enter dd/MM/yyyy Format)"
                                                InvalidValueMessage="From Date is invalid (Enter dd/MM/yyyy Format)" ValidationGroup="Submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                ErrorMessage="Please Enter From Date." ValidationGroup="Submit" ControlToValidate="txtFromDate"
                                                Display="None" Text="*">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divTo" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>TO Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="divToDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="34" CssClass="form-control" ToolTip="Enter Purchased Date "></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="divToDate" TargetControlID="txtToDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtToDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                ControlExtender="meeFromDate" ControlToValidate="txtToDate" IsValidEmpty="true"
                                                InvalidValueMessage="Purchased Date is invalid (Enter dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Purchased Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                ErrorMessage="Please Enter Purchased Date" ControlToValidate="txtToDate" Display="None" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divreportin" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Report In</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server" ToolTip="Select Report In Type"
                                            RepeatDirection="Horizontal" TabIndex="6">
                                            <%-- <asp:ListItem Selected="True" Value="No Export">Normal Report</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel&nbsp;&nbsp;</asp:ListItem>
                                            <%--<asp:ListItem Value="doc">MS-Word&nbsp;&nbsp;</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvPurItem" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Item Purchase Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>ITEM NAME
                                                            </th>
                                                            <th>PURCHASE DATE
                                                            </th>
                                                            <th>QUANTITY
                                                            </th>
                                                            <th>RATE
                                                            </th>
                                                            <th>TOTAL_AMT
                                                            </th>
                                                            <th>ITEM TYPE
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
                                                        CommandArgument='<%# Eval("PURCHASE_ID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="7" />
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PURCHASE_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("QUANTITY")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_TYPE").ToString() == "1" ? "Fuel" : "Indent"%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="8" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" OnClientClick="checkissuedate();" />
                                <asp:Button ID="btnreport_show" runat="server" Text="Show Report" TabIndex="10" CssClass="btn btn-info"
                                    OnClick="btnreport_show_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="11" CssClass="btn btn-info"
                                    OnClick="btnReport_Click" ToolTip="Click here to Show Report" Visible="false" />
                                <asp:Button ID="btnback" runat="server" Text="Back" TabIndex="12" CssClass="btn btn-info"
                                    OnClick="btnback_Click" ToolTip="Click here to Back Report" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="9" CssClass="btn btn-warning"
                                    CausesValidation="false" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="Submit" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function QuaAmount() {
            var rate = document.getElementById('<%=txtRate.ClientID%>').value;
            var quantity = document.getElementById('<%=txtQuantity.ClientID%>').value;
            document.getElementById('<%=txtAmt.ClientID%>').value = '';
            if (rate != '' && quantity != '') {
                var amount = parseFloat(rate * quantity);
                document.getElementById('<%=txtAmt.ClientID%>').value = amount;
            }
            else {
                alert('Please Enter Quantity.');
                document.getElementById('<%=txtQuantity.ClientID%>').value = '';

                window.setTimeout(function () {
                    document.getElementById('<%=txtQuantity.ClientID %>').focus();
                }, 0);
                return false;
            }
        }

        function checkissuedate() {
            if (document.getElementById('<%= txtPDate.ClientID %>').value != '') {
                var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                if (!(date_regex.test(document.getElementById('<%= txtPDate.ClientID %>').value))) {
                    alert("Purchased Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                    return false;
                }
            }

        }
    </script>
</asp:Content>
