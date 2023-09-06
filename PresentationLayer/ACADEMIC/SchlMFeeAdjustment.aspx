<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SchlMFeeAdjustment.aspx.cs" Inherits="ACADEMIC_SchlMFeeAdjustment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdRole" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div6" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="updProg" runat="server"
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

                        <asp:UpdatePanel ID="updFee" runat="server">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="col-12" id="divStudentSearch" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Enter Enrollment No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                                        Display="None" ErrorMessage="Please enter enrollment number." SetFocusOnError="true"
                                                        ValidationGroup="studSearch" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server"
                                                        CssClass="form-control" data-select2-enable="true" Enabled="true" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%# Eval("DD_NO") %>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                                        ValidationGroup="studSearch" />
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <%# (Eval("DD_DT").ToString() != string.Empty) ? ((DateTime)Eval("DD_DT")).ToShortDateString() : Eval("DD_DT") %>
                                                    <asp:Button ID="btnShowInfo" runat="server" Text="Show Information"
                                                        TabIndex="3" ValidationGroup="studSearch" CssClass="btn btn-primary" OnClick="btnShowInfo_Click" />

                                                    <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                                        CssClass="form-control" data-select2-enable="true" Enabled="true" Visible="False">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Regular</asp:ListItem>
                                                        <asp:ListItem Value="2">Repeater</asp:ListItem>
                                                        <asp:ListItem Value="3">Revaluation</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="studSearch" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divStudInfo" runat="server" style="display: none">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Student's Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblStudName" Font-Bold="true" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Gender :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSex" Font-Bold="true" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Date of Admission :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDateOfAdm" Font-Bold="true" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Payment Type :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Scholarship Amount :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSchamt" Font-Bold="true" runat="server" /></a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Degree :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDegree" CssClass="data_label" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Branch :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBranch" CssClass="data_label" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Year :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblYear" CssClass="data_label" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Semester :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSemester" CssClass="data_label" runat="server" /></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Batch :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBatch" CssClass="data_label" runat="server" /></a>
                                                        </li>
                                                        <%--<li class="list-group-item"><b>Scholarship Type :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblschtype" CssClass="data_label" runat="server" /></a>
                                                                        </li>--%>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="col-12" id="divReceipttype" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5></h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceipttype" AutoPostBack="true" AppendDataBoundItems="true" runat="server"
                                                        CssClass="form-control" data-select2-enable="true" Enabled="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Scholorship Type</label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlSchltypeMultiFee" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSchltypeMultiFee_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12" id="divFeeItems" runat="server" visible="false">
                                            <asp:ListView ID="lvFeeItems" runat="server">
                                                <LayoutTemplate>
                                                    <div id="divlvFeeItems">
                                                        <div class="sub-heading">
                                                            <h5>Available Fee Items</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeItems">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Fee Heads
                                                                    </th>
                                                                    <th>Demand Amount
                                                                    </th>
                                                                    <th>Adjust Amount
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
                                                            <%# Eval("FEE_LONGNAME")%>
                                                            <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                            <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblDAmount" runat="server" Text='<%# Eval("AMOUNT")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFeeItemAmount" onkeyup="IsNumeric(this);" onblur="UpdateTotalAndBalance();"
                                                                Style="text-align: right" runat="server" CssClass="form-control"
                                                                TabIndex="15" />
                                                            <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("AMOUNT") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Total Fee Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotalFeeAmount" Text="0" CssClass="form-control" runat="server" onkeydown="javascript:return false;" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Total D.D. Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotalDDAmount" Text="0" CssClass="form-control" Visible="false" runat="server"
                                                            onkeydown="javascript:return false;" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Total Cash Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotalCashAmt" Text="0" CssClass="form-control" Visible="false" runat="server"
                                                            onkeydown="javascript:return false;" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Excess/Deposit Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFeeBalance" Text="0" CssClass="form-control" runat="server" Visible="false" onkeydown="javascript:return false;" />

                                                        <%--<asp:CompareValidator ID="valFeeBalanceOrDiff" runat="server" ControlToValidate="txtFeeBalance"
                                                ErrorMessage="Total fee amount is not equals to total paid amount. Please check and adjust the fee item amounts."
                                                Operator="Equal" ValueToCompare="0" Display="None" />--%><%--commented by mahesh--%>
                                                    </div>

                                                    <div id="Div5" class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Visible="false" Rows="4" MaxLength="400"
                                                            TabIndex="133" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12" id="divFeeCriteria" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Fee Criteria</h5>
                                            </div>
                                            <div id="divHidFeeCriteria" runat="server" style="display: none;">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Payment Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPaymentType" AppendDataBoundItems="true" runat="server"
                                                            CssClass="form-control" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="valddlPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                            Display="None" ErrorMessage="Please select payment type." ValidationGroup="updateCritria"
                                                            InitialValue="0" SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Scholarship Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlScholarship" AppendDataBoundItems="true" runat="server"
                                                            CssClass="form-control" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="valddlScholarship" runat="server" ControlToValidate="ddlScholarship"
                                                            Display="None" ErrorMessage="Please select scholarship type." ValidationGroup="updateCritria"
                                                            InitialValue="0" SetFocusOnError="true" />
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnUpdateFeeCriteria" runat="server" Text="Update Fee Criteria"
                                                            ValidationGroup="updateCritria" CssClass="btn btn-primary" />
                                                        <asp:ValidationSummary ID="valSummary4" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="updateCritria" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12" id="divPreviousReceipts" runat="server" visible="false">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <asp:Repeater ID="lvPaidReceipts" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Previous Receipts Information</h5>
                                                            </div>
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Print
                                                                    </th>

                                                                    <th>Receipt Type
                                                                    </th>

                                                                    <th>Receipt No
                                                                    </th>

                                                                    <th>Date
                                                                    </th>

                                                                    <th>Semester
                                                                    </th>

                                                                    <th>Pay Type
                                                                    </th>

                                                                    <th>Amount
                                                                    </th>
                                                                </tr>
                                                                <thead>
                                                            <tbody>
                                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <%--  <tr class="item">--%>
                                                                <td>
                                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server"
                                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("RECIEPT_TITLE") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REC_NO") %>
                                                                </td>
                                                                <td>
                                                                    <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PAY_TYPE") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TOTAL_AMT") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" OnClick="btnSubmit_Click"
                                                Enabled="false" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnReport" runat="server" Text="Receipt Report" Visible="false" CausesValidation="false"
                                                Enabled="false" CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" Visible="false" OnClick="btnCancel_Click"
                                                CssClass="btn btn-warning" />
                                            <asp:Button ID="btnNewFee" runat="server" Text="New Receipt" Visible="false" CausesValidation="false"
                                                CssClass="btn btn-primary" />
                                            <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" CausesValidation="false"
                                                CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--   <asp:AsyncPostBackTrigger ControlID="btnShowInfo" />--%>
                                <%--<asp:PostBackTrigger ControlID="btnShowInfo" />--%>
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divModalboxContent" style="display: none; height: 540px">
    </div>

    <script type="text/javascript">
        function DevideTotalAmount() {
            try {
                var totalAmt = 0;
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null &&
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                    totalAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim());

                var dataRows = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(3);
                        var controls = dataCell.getElementsByTagName('input');
                        var originalAmt = controls.item(1).value;
                        if (originalAmt.trim() != '')
                            originalAmt = parseFloat(originalAmt);

                        if ((totalAmt - originalAmt) >= originalAmt) {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                            totalAmt = (totalAmt - originalAmt);
                        }
                        else {
                            if ((totalAmt - originalAmt) >= 0) {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                                totalAmt = (totalAmt - originalAmt);
                            }
                            else {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = totalAmt;
                                totalAmt = 0;
                            }
                        }
                    }
                }
                UpdateTotalAndBalance();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
        function UpdateTotalAndBalance() {
            try {

                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(2);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;

                    var totalPaidAmt = 0;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                        totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();
                }

            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>


    <script>
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }
    </script>

 
</asp:Content>

