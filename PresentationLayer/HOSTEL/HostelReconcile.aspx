<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelReconcile.aspx.cs" Inherits="HOSTEL_HostelReconcile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Challan Reconcile</h3>
                </div>

                <div class="box-body">
                    <div class="col-12" id="divStudentSearch" runat="server" visible="true">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Enter Roll No. </label>
                                </div>
                                <asp:TextBox ID="txtrollNo" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrollNo"
                                    Display="None" ErrorMessage="Please Enter Roll Number." SetFocusOnError="true"
                                    ValidationGroup="search" />
<%--                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />--%>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Information" OnClick="btnShow_Click" TabIndex="2" ValidationGroup="search" CssClass="btn btn-primary" />
<%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrollNo"
                                    Display="None" ErrorMessage="Please Enter Roll Number." SetFocusOnError="true"
                                    ValidationGroup="search" />--%>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />

                            </div>
                        </div>
                    </div>

                    <div id="divStudInfo" runat="server" visible="false" class="col-12">
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

                    <div id="divReconcile" runat="server" visible="false">
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Reconcile Process</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Session </label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                        Enabled="false">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Hostel :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblHostel" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Block :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBlock" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Room :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRoom" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Alloted Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAllotDate" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <asp:ListView ID="lvReconcile" runat="server" OnDataBound="lvReconcile_DataBound">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr No.
                                                </th>
                                                <th>DCR No.
                                                </th>
                                                <th>Receipt
                                                </th>
                                                <th>Amount
                                                </th>
                                                <th>Reconcile
                                                </th>
                                                <th>Date
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDcrNo" runat="server" Text='<%# Eval("DCR_NO") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblReceipt" runat="server" Text='<%# Eval("RECEIPT") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                            <asp:HiddenField ID="hfAmount" runat="server" Value='<%# Eval("TOTAL_AMT") %>' />
                                        </td>
                                        <td>
                                            <%-- <asp:CheckBox ID="chkRecon" runat="server" ToolTip='<%# Eval("RECON") %>' onclick="fees(this);" />--%>
                                            <asp:CheckBox ID="chkRecon" Text='<%# Eval("RECON") %>' runat="server" onclick="fees(this);" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRecDate" runat="server" Text='<%# Eval("REC_DT", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div runat="server" id="trPaytype">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Pay Type </label>
                                        </div>
                                        <div id="divPaytype" runat="server">
                                            <asp:UpdatePanel ID="UpdpayType" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPayType" runat="server" onblur="ValidatePayType(this); UpdateCash_DD_Amount();" CssClass="form-control"
                                                        MaxLength="1" ToolTip="Enter C for cash payment OR D for payment by demand draft."></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="txtPayType"
                                                        Display="None" ErrorMessage="Please Enter Payment Type Cash(C) or Demand draft(D)"
                                                        SetFocusOnError="true" ValidationGroup="submit" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reconcile Date </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtReconDate" runat="server" TabIndex="9" ToolTip=" Select Reconcile Date" CssClass="form-control"></asp:TextBox>
                                            <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtReconDate" PopupButtonID="imgFromDate" Enabled="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtReconDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" ErrorTooltipEnabled="false" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" EmptyValueMessage="Please Enter Reconcile Date."
                                                ControlExtender="meeDate" ControlToValidate="txtReconDate" IsValidEmpty="false"
                                                InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Reconcile Date." EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" id="trmsg" runat="server">
                                <span style="color: Red">Note : Please enter type of pay Type whether cash(C) or demand draft(D)
                                </span>
                            </div>


                            <div id="divDDDetails" runat="server" style="display: none">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Reconcile Process</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>DD/Check No.</label>
                                            </div>
                                            <asp:TextBox ID="txtDDNo" runat="server" TabIndex="6" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="7"
                                                CssClass="form-control" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDDDate" runat="server" ToolTip="Enter From Date" AutoPostBack="true" CssClass="form-control" />
                                                <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />
                                                       <%-- <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDDDate" PopupButtonID="imgFromDate" Enabled="true" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtDDDate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" ErrorTooltipEnabled="false" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" EmptyValueMessage="Please enter from date."
                                                            ControlExtender="meToDate" ControlToValidate="txtDDDate" IsValidEmpty="false"
                                                            InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="submit" SetFocusOnError="true" />--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDBank" AppendDataBoundItems="true" TabIndex="10" runat="server"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlDBank"
                                                Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                InitialValue="0" SetFocusOnError="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>City</label>
                                            </div>

                                            <asp:TextBox ID="txtDDCity" runat="server" TabIndex="9" CssClass="form-control" />
                                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="dd_info" />
                                        </div>
                                    </div>
                                </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                </div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function ValidatePayType(txtPayType) {
            try {
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {


                        txtPayType.value = "D";
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                            document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                            document.getElementById('<%= txtDDNo.ClientID%>').focus();
                        }
                    }
                    else {
                        if (txtPayType.value.toUpperCase() == 'C') {
                            txtPayType.value = "C";
                            if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                                document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                                //                                    document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";
                            }
                        }
                        else {
                            alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts.");
                            if (document.getElementById('<%= divDDDetails.ClientID %>') != null)
                                    document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";

                                txtPayType.value = "";
                                txtPayType.focus();
                            }
                        }
                    }
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
            }

            function UpdateCash_DD_Amount() {
                try {
                    var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
                    var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

                    if (txtPayType != null && txtPaidAmt != null) {
                        if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                        }
                        else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                            var totalDDAmt = 0.00;
                            var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                            if (dataRows != null) {
                                for (i = 1; i < dataRows.length; i++) {
                                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                    var dataCell = dataCellCollection.item(6);
                                    if (dataCell != null) {
                                        var txtAmt = dataCell.innerHTML.trim();
                                        totalDDAmt += parseFloat(txtAmt);
                                    }
                                }
                                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                                }
                            }
                        }
                    }
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
            }

            function fees(ctl) {
                if (ctl.checked) {
                    var a = ctl.id.split('ctrl');
                    a = a[1].split('_chkRecon');
                    var index = a[0];
                    var b = document.getElementById('ctl00_ContentPlaceHolder1_lvReconcile_ctrl' + index + '_hfAmount').value;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = b;
                }
                else
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = '';
            }
    </script>
</asp:Content>
