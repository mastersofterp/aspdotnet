<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AmbulanceMaintanance.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_AmbulanceMaintanance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Ambulance Maintenance</h3>
                </div>
                <div>
                    <div class="box-body">
                       
                            <asp:Panel ID="pnlInsurance" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgtravellingDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" runat="server" TabIndex="1" Style="z-index: 0;"
                                                    ToolTip="Enter Date" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cetravellingDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgtravellingDate" TargetControlID="txtDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meetravellingDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevtravellingDate" runat="server"
                                                    ControlExtender="meetravellingDate" ControlToValidate="txtDate" IsValidEmpty="false"
                                                    InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="submit" SetFocusOnError="true" EmptyValueMessage="Please Select Date" />

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bill No.</label>
                                            </div>
                                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" MaxLength="50" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                                ControlToValidate="txtBillNo" ErrorMessage="Please Enter Bill No." SetFocusOnError="true"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Suppliers Name </label>
                                            </div>
                                            <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" MaxLength="50" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtSupplierName" Display="None"
                                                ErrorMessage="Please Enter Suppliers Name" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Rate</label>
                                            </div>
                                            <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="8" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtRate" Display="None"
                                                ErrorMessage="Please Enter Rate" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="8" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAmount" Display="None"
                                                ErrorMessage="Please Enter Amount" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>CGST</label>
                                            </div>
                                            <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" MaxLength="7" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCGST" Display="None"
                                                ErrorMessage="Please Enter CGST" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>SGST</label>
                                            </div>
                                            <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" MaxLength="7" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtSGST" Display="None"
                                                ErrorMessage="Please Enter CGST" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Total Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" MaxLength="8" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtTotalAmount" Display="None"
                                                ErrorMessage="Please Enter Total Amount" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Incharge</label>
                                            </div>
                                            <asp:TextBox ID="txtIncharge" runat="server" CssClass="form-control" MaxLength="50" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtIncharge" Display="None"
                                                ErrorMessage="Please Enter Incharge" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Discription</label>
                                            </div>
                                            <asp:TextBox ID="txtDiscription" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500" TabIndex="3" onkeyDown="checkTextAreaMaxLength(this,event,'500');" onkeyup="textCounter(this, this.form.remLen, 500);"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                        ToolTip="Click here to Submit" TabIndex="15" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="16"
                                        ToolTip="Click here to reset" OnClick="btnCancel_Click" />
                                    <%-- <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="17"
                          ToolTip="Click here to Show Report" />--%>

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </div>
                                <div class="col-12 mt-3">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvAmbulance" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Ambulance Maintenance Entry List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>EDIT
                                                                </th>
                                                                <th>DATE
                                                                </th>
                                                                <th>BILL NO.
                                                                </th>
                                                                <th>SUPPLIER NAME
                                                                </th>
                                                                <th>TOTAL AMOUNT
                                                                </th>
                                                                <th>INCHARGE
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AM_ID") %>'
                                                            ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;                                         
                                                    </td>
                                                    <td>
                                                        <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BILLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUPPLIERS_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("INCHARGE")%>
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
    
  

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }


        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }
    </script>

    <script type="text/javascript">

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
    </script>

</asp:Content>

