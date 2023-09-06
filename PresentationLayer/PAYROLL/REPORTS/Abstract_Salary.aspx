<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Abstract_Salary.aspx.cs" Inherits="PayRoll_Abstract_Salary" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ABSTRACT SALARY REPORT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>View Salary as Staff Wise</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Month</label>
                                    </div>
                                    <asp:DropDownList ID="ddlMonthYear" runat="server" ToolTip="Select Month" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" data-select2-enable="true">
                                   
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                        ControlToValidate="ddlMonthYear"  ErrorMessage="Please Select Month/Year"
                                        ValidationGroup="Payroll"   InitialValue="0"  Display="None"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">

                                       <%-- <sup>* </sup>--%>

                                      

                                        <%--<sup>* </sup>--%>

                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                        TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                  <%--  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>

                                   <%-- <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"

                                    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"

                                    <%--<asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"

                                        ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="Payroll1" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="Payroll2" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trstaff" runat="server">
                                    <div class="label-dynamic">
                                       <%-- <sup>* </sup>--%>
                                        <%--<label>Staff</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme/Staff" AppendDataBoundItems="True" TabIndex="3" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                        ControlToValidate="ddlStaffNo"  ErrorMessage="Please Select Scheme/Staff"
                                        ValidationGroup="Payroll"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Employee Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trmultistaff" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select Two Staffs</label>
                                    </div>
                                    <asp:ListBox ID="lstStaffFill" runat="server" Height="60px" CssClass="form-control multiselect-list-style" SelectionMode="Multiple" TabIndex="5" ToolTip="Select two Staffs"
                                        AppendDataBoundItems="True"></asp:ListBox>
                                    <asp:RequiredFieldValidator ID="rfvlstSfaffFill" runat="server" SetFocusOnError="true"
                                        ControlToValidate="lstStaffFill" Display="None" ErrorMessage="Please Select Staff from the list"
                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>
                                            <asp:CheckBox ID="chkAbstarct" runat="server" Text="Cummulative Abstract" Visible="false" TabIndex="6" />
                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"

                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                            <asp:CheckBox ID="chkAbstractSummary" runat="server" Text="Abstract Summary" OnCheckedChanged="chkAbstractSummary_CheckedChanged"
                                        AutoPostBack="True" TabIndex="4" />
                                        </label>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSalaryRegRCPIT" runat="server" Text="Salary Register With Abstract(Format 1)"
                            ToolTip="Click to Show Salary Register With Abstract Report" OnClick="btnRegisterWithAbstractRCPIT_Click" CssClass="btn btn-info"
                            ValidationGroup="Payroll" TabIndex="5" />
                        <asp:Button ID="btnRegisterWithAbstract" runat="server" Text="Salary Register With Abstract"
                            ToolTip="Click to Show Salary Register With Abstract Report" OnClick="btnRegisterWithAbstract_Click" CssClass="btn btn-info"
                            ValidationGroup="Payroll" TabIndex="5" />

                        <asp:Button ID="btnShowAbstractExcel" runat="server" TabIndex="6" Text="Show To Export Into Excel File" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnShowAbstractExcel_Click" />

                        <asp:Button ID="btnRegisterWithDept" runat="server" Text="Salary Register Department Wise"
                            ToolTip="Click to Show Salary Register Department Wise Report" OnClick="btnRegisterWithDept_Click" CssClass="btn btn-info"
                            ValidationGroup="Payroll" TabIndex="7" />

                        <asp:Button ID="btnSuppliBill" runat="server" Text="Supplementary Bill Report"
                            ToolTip="Click to Show Supplementary Bill Report " CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnSuppliBill_Click" TabIndex="8" />

                        <asp:Button ID="btnSalarySummary" runat="server" Text="Salary Summary Report"
                            ToolTip="Click to Show Salary Summary Report" CssClass="btn btn-info"
                            ValidationGroup="Payroll1" OnClick="btnSalarySummary_Click" TabIndex="9" />

                        <asp:Button ID="btnSuppBill2" runat="server" Text="Supplementary Bill 2" ToolTip="Click to Show Supplementary Bill Report"
                            CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnSuppBill2_Click" TabIndex="10" />

                        <asp:Button ID="btnGrossDiff" runat="server" Text="Variation Statement Report"
                            CssClass="btn btn-info" TabIndex="11" OnClick="btnGrossDiff_Click" />
                           <asp:Button ID="btnexporttoexcelsalaryreg" runat="server" Text="Salary Register Export To Excel Format 1"
                            CssClass="btn btn-info" TabIndex="12" OnClick="btnexporttoexcelsalaryreg_Click" ValidationGroup="Payroll" />


                           <asp:Button ID="btnSalRegActualRateExport" runat="server" Text="Salary Register Export To Excel Actual Rate"
                            CssClass="btn btn-info" TabIndex="12" OnClick="btnSalRegActualRateExport_Click" ValidationGroup="Payroll" />

                          <asp:Button ID="BtnPaybillRec" runat="server" Text="Export Paybill Recovery"
                            CssClass="btn btn-info" TabIndex="12" OnClick="BtnPaybillRec_Click" ValidationGroup="Payroll" />
                        <asp:Button ID="btnPaybillIncome" runat="server" Text="Export Paybill Income"
                            CssClass="btn btn-info" TabIndex="12" OnClick="btnPaybillIncome_Click" ValidationGroup="Payroll" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            ToolTip="Click to Reset" CssClass="btn btn-warning" TabIndex="12" />
                    </div>
                    <div class="col-12" id="div_ExportToExcel" runat="server" visible="true">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="grdSelectFieldReport" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Earnings And Deducations" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%!important">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>College
                                                </th>
                                                <th>Scheme
                                                </th>
                                                <th>Staff
                                                </th>
                                                <th>Designation
                                                </th>
                                                <th>Department
                                                </th>
                                                <th>Employee Code
                                                </th>
                                                <th>
                                                  Old Employee Code
                                                </th>
                                                <th>Rule
                                                </th>
                                                <th>Pay Scal
                                                </th>
                                                <th>A/C
                                                </th>
                                                <th>Pan Card No.
                                                </th>
                                                <th>Actual Basic
                                                </th>

                                                <th>D.P
                                                </th>
                                                <th>AGP
                                                </th>
                                                <th>Basic
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH1" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH2" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH3" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH4" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH5" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH6" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH7" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH8" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH9" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH10" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH11" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH12" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH13" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH14" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH15" runat="server"></asp:Label>
                                                </th>
                                                <th>Gross Total
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH1" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH2" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH3" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH4" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH5" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH6" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH7" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH8" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH9" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH10" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH11" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH12" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH13" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH14" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH15" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH16" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH17" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH18" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH19" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH20" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH21" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH22" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH23" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH24" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH25" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH26" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH27" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH28" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH29" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH30" runat="server"></asp:Label>
                                                </th>
                                                <th>Total Deduction
                                                </th>
                                                <th>Net Amount
                                                </th>
                                                <th>Sign
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
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("STAFF")%>
                                        </td>

                                        <td>
                                            <%# Eval("EMPLOYEETYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDESIG")%>
                                        </td>

                                        <td>
                                            <%# Eval("SUBDEPT")%>
                                        </td>

                                        <td>
                                            <%# Eval("PFILENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("EmployeeId")%>
                                        </td>

                                        <td>
                                            <%# Eval("PAYRULE")%>
                                        </td>
                                        <td>
                                            <%# Eval("SCALE")%>
                                        </td>
                                        <td>
                                            <%# Eval("BANKACC_NO")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAN_NO")%>
                                        </td>
                                        <td>
                                            <%# Eval("BASIC")%>
                                        </td>

                                        <td>
                                            <%# Eval("DAAMT")%>
                                        </td>
                                        <td>
                                            <%# Eval("GRADEPAY")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAY")%>
                                        </td>
                                        <td>
                                            <%# Eval("I1")%>
                                        </td>
                                        <td>
                                            <%# Eval("I2")%>
                                        </td>
                                        <td>
                                            <%# Eval("I3")%>
                                        </td>
                                        <td>
                                            <%# Eval("I4")%>
                                        </td>
                                        <td>
                                            <%# Eval("I5")%>
                                        </td>
                                        <td>
                                            <%# Eval("I6")%>
                                        </td>
                                        <td>
                                            <%# Eval("I7")%>
                                        </td>
                                        <td>
                                            <%# Eval("I8")%>
                                        </td>
                                        <td>
                                            <%# Eval("I9")%>
                                        </td>
                                        <td>
                                            <%# Eval("I10")%>
                                        </td>
                                        <td>
                                            <%# Eval("I11")%>
                                        </td>
                                        <td>
                                            <%# Eval("I12")%>
                                        </td>
                                        <td>
                                            <%# Eval("I13")%>
                                        </td>
                                        <td>
                                            <%# Eval("I14")%>
                                        </td>
                                        <td>
                                            <%# Eval("I15")%>
                                        </td>
                                        <td>
                                            <%# Eval("GS")%>
                                        </td>
                                        <td>
                                            <%# Eval("D1")%>
                                        </td>
                                        <td>
                                            <%# Eval("D2")%>
                                        </td>
                                        <td>
                                            <%# Eval("D3")%>
                                        </td>
                                        <td>
                                            <%# Eval("D4")%>
                                        </td>
                                        <td>
                                            <%# Eval("D5")%>
                                        </td>
                                        <td>
                                            <%# Eval("D6")%>
                                        </td>
                                        <td>
                                            <%# Eval("D7")%>
                                        </td>
                                        <td>
                                            <%# Eval("D8")%>
                                        </td>
                                        <td>
                                            <%# Eval("D9")%>
                                        </td>
                                        <td>
                                            <%# Eval("D10")%>
                                        </td>
                                        <td>
                                            <%# Eval("D11")%>
                                        </td>
                                        <td>
                                            <%# Eval("D12")%>
                                        </td>
                                        <td>
                                            <%# Eval("D13")%>
                                        </td>
                                        <td>
                                            <%# Eval("D14")%>
                                        </td>
                                        <td>
                                            <%# Eval("D15")%>
                                        </td>
                                        <td>
                                            <%# Eval("D16")%>
                                        </td>
                                        <td>
                                            <%# Eval("D17")%>
                                        </td>
                                        <td>
                                            <%# Eval("D18")%>
                                        </td>
                                        <td>
                                            <%# Eval("D19")%>
                                        </td>
                                        <td>
                                            <%# Eval("D20")%>
                                        </td>
                                        <td>
                                            <%# Eval("D21")%>
                                        </td>
                                        <td>
                                            <%# Eval("D22")%>
                                        </td>
                                        <td>
                                            <%# Eval("D23")%>
                                        </td>
                                        <td>
                                            <%# Eval("D24")%>
                                        </td>
                                        <td>
                                            <%# Eval("D25")%>
                                        </td>
                                        <td>
                                            <%# Eval("D26")%>
                                        </td>
                                        <td>
                                            <%# Eval("D27")%>
                                        </td>
                                        <td>
                                            <%# Eval("D28")%>
                                        </td>
                                        <td>
                                            <%# Eval("D29")%>
                                        </td>
                                        <td>
                                            <%# Eval("D30")%>
                                        </td>
                                        <td>
                                            <%# Eval("TOT_DED")%>
                                        </td>
                                        <td>
                                            <%# Eval("NET_PAY")%>
                                        </td>
                                        <td></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <%--<asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                            ImageUrl="~/IMAGES/excel.jpeg" Height="50px" Width="50px" OnClick="imgbutExporttoexcel_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
