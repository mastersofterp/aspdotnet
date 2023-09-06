<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_UpdatePayHeadEmpWise.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_UpdatePayHeadEmpWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Update Pay Heads Employee Wise</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSupplimentaryBillDetails" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                               <%-- <label>Staff Type</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="1"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme/Staff"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Emp.Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" data-select2-enable="true" TabIndex="2"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmpName" runat="server" ControlToValidate="ddlEmpName"
                                                Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="payroll"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-md-6 col-12">
                                            <div id="div_otherinfo" runat="server" visible="false">
                                                <div id="tr_EmpOtherInfo" runat="server">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Designation :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDesignation" runat="server"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Department :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblDept" runat="server"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6 col-12">
                                            <div id="tr1" runat="server" visible="false">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Basic :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBasic" runat="server"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Grade Pay :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblGradePay" runat="server"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Scale :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScale" runat="server"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" id="tbl_Grid" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:Panel ID="pnlEarningHeads" runat="server">
                                                <asp:ListView ID="lvEarningHeads" runat="server">
                                                    <EmptyDataTemplate>
                                                        No Rows In Earning Heads
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Earning Heads</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>PayHead
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("PAYSHORT")%>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txEarningsAmt" runat="server" MaxLength="10" CssClass="form-control" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                    Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <%-- <AlternatingItemTemplate>
                                                                                <tr >
                                                                                    <td width="10%">
                                                                                        <%# Eval("PAYSHORT")%>
                                                                                    </td>
                                                                                    <td width="15%">
                                                                                        <asp:TextBox ID="txEarningsAmt" runat="server" CssClass="form-control" MaxLength="10" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                                            Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>--%>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="PnlDeductionHeads" runat="server">
                                                <asp:ListView ID="lvDeductionHeads" runat="server">
                                                    <EmptyDataTemplate>
                                                        No Rows In Deduction Heads
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                            <div class="sub-heading">
	                                                            <h5>Deduction Heads</h5>
                                                            </div>
                                                            
                                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>PayHead
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                    </tr>
                                                                    <thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        <%-- <div class="listview-container">
                                                                                    <div id="Div1">
                                                                                        <table >
                                                                                               
                                                                                        </table>
                                                                                    </div>
                                                                                </div>--%>
                                                                                </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("PAYSHORT")%>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txDeductionAmt" runat="server" CssClass="form-control" MaxLength="10" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                    Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <%-- <AlternatingItemTemplate>
                                                                                <tr>
                                                                                    <td  width="10%">
                                                                                        <%# Eval("PAYSHORT")%>
                                                                                    </td>
                                                                                    <td  width="15%">
                                                                                        <asp:TextBox ID="txDeductionAmt" runat="server" CssClass="form-control" MaxLength="10" ToolTip='<%# Eval("PAYHEAD")%>'
                                                                                            Text="0.00" onkeyup="return Amount(this);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </AlternatingItemTemplate>--%>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="tbl_buttons" runat="server">
                                    <asp:Button ID="butSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="payroll" OnClick="butSubmit_Click" />
                                    <asp:Button ID="butCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="butCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </asp:Panel>
                            <input type="hidden" id="hidTotalDeductionRecordsCount" runat="server" />
                            <input type="hidden" id="hidEarningRecordsCount" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="divOrdDetails" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function Amount(txt) {


            if (ValidateNumeric(txt) == true) {
                var DeductionAmount = 0.00, EarningsAmount = 0.00, SupliPay = 0.00, Dpay = 0.00;
                var GradePay = 0.00, GrossSalary = 0.00, NetPay = 0.00, TotalDeductions = 0.00;

                GradePay = document.getElementById("ctl00_ContentPlaceHolder1_txtGradePay").value;
                SupliPay = document.getElementById("ctl00_ContentPlaceHolder1_txtSupliPay").value;
                Dpay = document.getElementById("ctl00_ContentPlaceHolder1_txtDPay").value;
                GrossSalary = document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value;
                NetPay = document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value
                TotDeducation = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeductions").value

                //           if(Dpay > 0)
                //           {
                //             document.getElementById("ctl00_ContentPlaceHolder1_txtGradePay").value=0.00
                //           }           
                //           
                //           if(GradePay > 0)
                //           {
                //             document.getElementById("ctl00_ContentPlaceHolder1_txtDPay").value=0.00
                //           }


                for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidTotalDeductionRecordsCount").value) - 1; i++) {
                    DeductionAmount = Number(DeductionAmount) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_ctrl" + i + "_txDeductionAmt").value);
                }

                for (j = 0; j <= Number(document.getElementById("ctl00_ContentPlaceHolder1_hidEarningRecordsCount").value) - 1; j++) {
                    EarningsAmount = Number(EarningsAmount) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_ctrl" + j + "_txEarningsAmt").value);
                }

                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDeductions").value = DeductionAmount;
                document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value = Number(EarningsAmount) + Number(GradePay) + Number(SupliPay) + Number(Dpay);
                document.getElementById("ctl00_ContentPlaceHolder1_txtNetPay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtGrossSalary").value) - Number(DeductionAmount);
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

    </script>
</asp:Content>

