<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoubleDutyApproval.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Transactions_DoubleDutyApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Double Duty Approval</h3>
                  <%--  <asp:ImageButton ID="btnHelp" runat="server" />--%>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Staff Type </label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select Staff Type Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Shift"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalDt">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" Width="80px" TabIndex="3" ToolTip="Enter Start Date" />
                                        <asp:RequiredFieldValidator ID="rfvDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevDt" runat="server" ControlExtender="meeDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter From Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalToDt">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" Width="80px" TabIndex="4" ToolTip="Enter End Date" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Shift"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Date"
                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                            ValidationGroup="Shift" ControlToCompare="txtFromDt" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer mt-3">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Shift" TabIndex="5"
                                OnClick="btnShow_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Shift" TabIndex="5"
                                OnClick="btnSave_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="6"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                ValidationGroup="Shift" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnllistPending" runat="server">
                        <div class="col-12 mt-3">
                            <asp:ListView ID="lvPendingList" runat="server">
                                <EmptyDataTemplate>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Record Found" />
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Pending List For Double Duty Approval</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select" Enabled="true" runat="server" onclick="checkAllEmployees(this)" />
                                                    </th>
                                                    <th>Employee Name
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Shift Name
                                                    </th>
                                                    <th>In Time
                                                    </th>
                                                    <th>Out Time
                                                    </th>
                                                    <th>Work Hr
                                                    </th>
                                                </tr>
                                                <thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' ToolTip='<%#Eval("Idno")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftDate" runat="server" Text='<%# Eval("SHIFTDATE", "{0:dd/MM/yyyy}")%>' ToolTip='<%#Eval("SHIFTNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("SHIFTNAME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("INTIME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWkHr" runat="server" Text='<%# Eval("WORKHR")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem">
                                        <td>
                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' ToolTip='<%#Eval("Idno")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftDate" runat="server" Text='<%# Eval("SHIFTDATE", "{0:dd/MM/yyyy}")%>' ToolTip='<%#Eval("SHIFTNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("SHIFTNAME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("INTIME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWkHr" runat="server" Text='<%# Eval("WORKHR")%>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        //Calculating the total amount
        function totalamount(val) {
            if (ValidateNumeric(val)) {
                var txtMonthlyDedAmt = document.getElementById("ctl00_ctp_txtMonthlyDedAmt");
                var txtTotalAmount = document.getElementById("ctl00_ctp_txtTotalAmount");
                var txtOutStandingAmt = document.getElementById("ctl00_ctp_txtOutStandingAmt");
                var txtNoofInstPaid = document.getElementById("ctl00_ctp_txtNoofInstPaid");
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
        function checkAllEmployees(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }


    </script>

</asp:Content>


