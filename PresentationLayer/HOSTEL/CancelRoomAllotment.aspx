<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CancelRoomAllotment.aspx.cs" Inherits="Hostel_CancelRoomAllotment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Cancel Room Allotment</h3>
                </div>

                <div class="box-body">
                    <div id="divStudentSearch" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session </label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Enter Reg. No. </label>
                                    </div>
                                    <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="20" TabIndex="2" CssClass="form-control" />

                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show Information" OnClick="btnShow_Click"
                                TabIndex="3" ValidationGroup="search" />
                            <asp:Button ID="btnSlip" runat="server" Text="Report" OnClick="btnSlip_Click"
                                TabIndex="4" ValidationGroup="search" Visible="false" CssClass="btn btn-info" />
                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                Display="None" ErrorMessage="Please Enter Registration No." SetFocusOnError="true"
                                ValidationGroup="search" />
                            <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="search" />
                        </div>
                    </div>

                    <div id="divStudInfo" runat="server" visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Student Information</h5>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Roll No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Degree :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDegree" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Student's Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Branch :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBranch" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Gender :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSex" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Date of Admission :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDateOfAdm" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Batch :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBatch" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Payment Type :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Year :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblYear" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div id="trRefund" runat="server" visible="false">
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Hostel Refund</h5>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Hostel Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblHostel" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Mess Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMess" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Allotted Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAllotDate" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Bank Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBankName" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Bank Acc. No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBAccNo" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Bank Branch :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div>
                                    <table class="table table-bordered nowrap " style="width: 100%">
                                        <tr>
                                            <td colspan="10" style="width: 100%">
                                                <fieldset class="fieldset">
                                                    <legend class="legend">Due</legend>
                                                    <table style="width: 100%">
                                                        <tr style="display: none;">
                                                            <td style="width: 11%;"></td>
                                                            <td style="width: 2px;">
                                                                <b></b>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                            <td style="width: 7%; text-align: center">
                                                                <b>Days</b>
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <b>X</b>
                                                            </td>
                                                            <td style="width: 7%; text-align: center">
                                                                <b>Percentage / Charges</b>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;=&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <b>Balance</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 11%;">Hostel Fee Paid
                                                            </td>
                                                            <td style="width: 2px;">
                                                                <b>:</b>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblHostelPaid" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 15%; text-align: left">
                                                                <asp:TextBox ID="txtHAmt" runat="server" ToolTip="Paid Amount" Width="60px" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                Amount
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" ValidChars="1234567890." TargetControlID="txtHAmt"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </td>
                                                            <td style="width: 2%;">X
                                                            </td>
                                                            <td style="width: 15%; text-align: left">
                                                                <asp:TextBox ID="txtPerc" runat="server" ToolTip="Percentage" Width="40px" onblur="HostelFee()" MaxLength="4"></asp:TextBox>
                                                                %
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" ValidChars="1234567890." TargetControlID="txtPerc"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </td>
                                                            <td>&nbsp;=&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtHostelAmount" runat="server" ToolTip="Refund" onblur="TotalAmt()" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" ValidChars="1234567890." TargetControlID="txtHostelAmount"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 11%;">Mess Fee Paid
                                                            </td>
                                                            <td style="width: 2px;">
                                                                <b>:</b>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblMessPaid" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 15%; text-align: left">
                                                                <asp:TextBox ID="txtMday" runat="server" ToolTip="Total Days" Width="60px" MaxLength="3"></asp:TextBox>
                                                                Days
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" ValidChars="1234567890" TargetControlID="txtMday"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </td>
                                                            <td>X
                                                            </td>
                                                            <td style="width: 7%; text-align: left">
                                                                <asp:TextBox ID="txtMCharge" runat="server" ToolTip="Percentage" Width="40px" onblur="MessFee()" MaxLength="6"></asp:TextBox>
                                                                Charges
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" ValidChars="1234567890." TargetControlID="txtMCharge"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </td>
                                                            <td>&nbsp;=&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMessAmount" runat="server" ToolTip="Refund" onblur="TotalAmt()" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" ValidChars="1234567890." TargetControlID="txtMessAmount"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 11%;"></td>
                                                            <td style="width: 2px;"></td>
                                                            <td style="width: 10%;"></td>
                                                            <td></td>
                                                            <td style="width: 15%; text-align: center"></td>
                                                            <td></td>
                                                            <td style="width: 7%; text-align: left">
                                                                <b>Mess Charges</b>
                                                            </td>
                                                            <td>&nbsp;=&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMessUse" runat="server" ToolTip="Mess Charges" onblur="TotalAmt()" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" ValidChars="1234567890." TargetControlID="txtMessUse"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 11%;"></td>
                                                            <td style="width: 2px;"></td>
                                                            <td style="width: 10%;"></td>
                                                            <td></td>
                                                            <td style="width: 15%; text-align: center"></td>
                                                            <td></td>
                                                            <td style="width: 7%; text-align: left">
                                                                <b>Other Charges</b>
                                                            </td>
                                                            <td>&nbsp;=&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOtherCharge" runat="server" ToolTip="Other Charges" onblur="TotalAmt()" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="1234567890." TargetControlID="txtOtherCharge"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="15">
                                                                <hr style="color: Olive" />
                                                            </td>
                                                        </tr>
                                                        <tr style="vertical-align: top">
                                                            <td style="width: 11%;">
                                                                <b>Total Paid </b>
                                                            </td>
                                                            <td style="width: 2px;">
                                                                <b>:</b>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblTotalPaid" runat="server" Font-Bold="true"></asp:Label>
                                                                <asp:HiddenField ID="hfdTotalPaid" runat="server" />
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 15%; text-align: center">
                                                                <b>-</b>
                                                            </td>
                                                            <td></td>
                                                            <td style="width: 7%; text-align: left">
                                                                <b>Total </b>
                                                            </td>
                                                            <td>&nbsp;<b>:</b>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal" runat="server" ToolTip="Total" Font-Bold="true" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="1234567890." TargetControlID="txtTotal"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="text-align: center; width: 10%">
                                                                <b>Refund </b>
                                                            </td>
                                                            <td style="text-align: center; width: 2px"><b>=</b></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRefundAmt" runat="server" ToolTip="Refund Amount" Font-Bold="true" Width="80px" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="1234567890." TargetControlID="txtRefundAmt"></ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 9%;">Cheque No.
                                            </td>
                                            <td style="width: 2px;">
                                                <b>:</b>
                                            </td>
                                            <td style="width: 20%; text-align: left;">
                                                <asp:TextBox ID="txtChequeNo" runat="server" ToolTip="Cheque No."></asp:TextBox>
                                            </td>
                                            <td style="width: 9%;">Amount Rs.
                                            </td>
                                            <td style="width: 2px;">
                                                <b>:</b>
                                            </td>
                                            <td style="width: 20%; text-align: left;">
                                                <asp:TextBox ID="txtCAmount" runat="server" ToolTip="Amount" Width="100px" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbCamount" runat="server" ValidChars="1234567890." TargetControlID="txtCAmount"></ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 9%;">Date
                                            </td>
                                            <td style="width: 2px;">
                                                <b>:</b>
                                            </td>
                                            <td style="width: 20%; text-align: left;">
                                                <asp:TextBox ID="txtCDate" runat="server" ToolTip="Date" Width="80px"></asp:TextBox>
                                                <asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                                <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCDate"
                                                    PopupButtonID="imgCalDDDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtCDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                    OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                    ControlToValidate="txtCDate" IsValidEmpty="False" EmptyValueMessage="date is required"
                                                    InvalidValueMessage="Date is invalid" EmptyValueBlurredText="*"
                                                    InvalidValueBlurredMessage="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvAllotmentDetails" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Room Allotment Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Hostel
                                                            </th>
                                                            <th>Block
                                                            </th>
                                                            <th>Room
                                                            </th>
                                                            <th>Allotment Date
                                                            </th>
                                                            <th>Session
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Hosteler
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
                                                    <%# Eval("HOSTEL_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BLOCK_NAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("ROOM_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ALLOTMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("HOSTEL_STATUS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("HOSTELER")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="divRemark" runat="server" visible="false">
                         <div id="divCancelHostel" runat="server" style="margin:10px;">
                            <asp:CheckBox ID="chkCancelHostel" runat="server"/><label Font-Bold="true">&nbsp;&nbsp;For Cancel Hostel</label>
                        </div>
                        <div>
                            <label><span style="color: Red;">*</span> Reason for Cancel Room : </label>
                            <br />
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="4" MaxLength="400"
                            TabIndex="12" Width="450px" />&nbsp;&nbsp;
                             <asp:RequiredFieldValidator ID="valRemark" ControlToValidate="txtRemark" runat="server"
                            Display="None" ErrorMessage="Please Enter Reason For Cancel Room." SetFocusOnError="true"
                            ValidationGroup="submit" />
                        </div>
                        
                    </div>
                    

                    <div class="box-footer">
                        <p class="text-center">

                            <asp:Button ID="btnCancelRoom" OnClick="btnCancelRoom_Click" CssClass="btn btn-primary" runat="server" Text="Submit"
                                TabIndex="8" Visible="false" ValidationGroup="submit" />
                            <asp:Button ID="btnReport" OnClick="btnReport_Click" CssClass="btn btn-info" runat="server" Text="Report"
                                TabIndex="8" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click"
                                TabIndex="9" Visible="false" />
                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function CancelRoomAllotment() {
            try {
                if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Do you really want to cancel this room allotment.")) {
                            __doPostBack("CancelRoomAllotment", "");
                        }
                    }
                    else
                        alert('Please enter reason of cancelling room allotment.');
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
            }

            function ValidateRecordSelection() {
                var isValid = false;
                try {
                    var tbl = document.getElementById('tblSearchResults');
                    if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                        for (i = 1; i < tbl.rows.length; i++) {
                            var dataRow = tbl.rows[i];
                            var dataCell = dataRow.firstChild;
                            var rdo = dataCell.firstChild;
                            if (rdo.checked) {
                                isValid = true;
                            }
                        }
                    }
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
                return isValid;
            }

            function ShowRemark(rdoSelect) {
                try {
                    if (rdoSelect != null && rdoSelect.nextSibling != null) {
                        var hidRemark = rdoSelect.nextSibling;
                        if (hidRemark != null)
                            document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
                            }
                        }
                        catch (e) {
                            alert("Error: " + e.description);
                        }
                    }

                    function HostelFee() {
                        var a = document.getElementById('<%= txtPerc.ClientID %>').value;
                        var b = document.getElementById('<%= txtHAmt.ClientID %>').value
                        var c = (Number(a) * Number(b)) / 100
                        document.getElementById('<%= txtHostelAmount.ClientID %>').value = c.toFixed(2);
                        TotalAmt();
                    }
                    function MessFee() {
                        var a = document.getElementById('<%= txtMday.ClientID %>').value;
                    var b = document.getElementById('<%= txtMCharge.ClientID %>').value
                    var c = Number(a) * Number(b)
                    document.getElementById('<%= txtMessAmount.ClientID %>').value = c.toFixed(2);
            TotalAmt();
        }
        function TotalAmt() {
            var a = document.getElementById('<%= txtHostelAmount.ClientID %>').value; if (a == "") a = "0.00";
            var b = document.getElementById('<%= txtMessAmount.ClientID %>').value; if (b == "") b = "0.00";
            var c = document.getElementById('<%= txtMessUse.ClientID %>').value; if (c == "") c = "0.00";
            var d = document.getElementById('<%= txtOtherCharge.ClientID %>').value; if (d == "") d = "0.00";
            var e = (Number(a) + Number(b) + Number(c) + Number(d)).toFixed(2);
            document.getElementById('<%= txtTotal.ClientID %>').value = e;

            // Refund
            var f = document.getElementById('<%= hfdTotalPaid.ClientID %>').value;
            document.getElementById('<%= txtRefundAmt.ClientID %>').value = (Number(f) - Number(e)).toFixed(2);
            document.getElementById('<%= txtCAmount.ClientID %>').value = document.getElementById('<%= txtRefundAmt.ClientID %>').value;
        }
    </script>
</asp:Content>
