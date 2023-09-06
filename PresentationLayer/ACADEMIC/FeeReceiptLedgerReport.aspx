<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeReceiptLedgerReport.aspx.cs" Inherits="Academic_FeeReceiptLedgerReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FEE RECEIPT LEDGER</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Report Type </label>
                                </div>
                                <asp:RadioButton ID="rdoFilterByReceipt" Text="Receipt Report" Checked="true" GroupName="RptSubType"
                                    TabIndex="2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoFilterByChallan" Text="Challan Report" GroupName="RptSubType"
                                    TabIndex="2" runat="server" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>From Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="I1" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                    <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>To Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" />
                                    <%--<asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Receipt Type </label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="3" />
                                <%--<asp:CompareValidator ID="valCounter" runat="server" ControlToValidate="ddlCounterNo"
                                    Display="None" ErrorMessage="Please select counter number." Operator="NotEqual"
                                    SetFocusOnError="true" Type="String" ValidationGroup="report" ValueToCompare="Please Select" />--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Data Filters</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="4" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Year </label>
                                </div>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="6" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch </label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="5" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="7" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Single Student's Fee Ledger</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Search by </label>
                                </div>
                                <asp:RadioButton ID="rdoEnrollmentNo" Text="Enrollment No"
                                    Checked="true" runat="server" GroupName="stud" />
                                &nbsp;&nbsp;<asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                    GroupName="stud" />
                                &nbsp;&nbsp;<asp:RadioButton ID="rdoIdNo" Text="Student Id" runat="server" GroupName="stud" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Search </label>
                                </div>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" TabIndex="8" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary"
                                    TabIndex="9" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:ListView ID="lvStudentRecords" runat="server">
                            <LayoutTemplate>
                                <div id="listViewGrid" class="vista-grid">
                                    <div class="sub-heading">
                                        <h5>Search Results</h5>
                                    </div>

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Report
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Enrollment No.
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Branch
                                                </th>
                                                <th>Year
                                                </th>
                                                <th>Semester
                                                </th>
                                                <th>Batch
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
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                            ImageUrl="~/Images/print.png" ToolTip="Show Report" />
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ENROLLMENTNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("CODE")%>
                                    </td>
                                    <td>
                                        <%# Eval("SHORTNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("YEARNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("SEMESTERNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("BATCHNAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="data_label">
                                    -- No Student Record Found --
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <div class="col-12 btn-footer mt-4">
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info"
                            TabIndex="10" ValidationGroup="report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                            OnClick="btnCancel_Click" TabIndex="11" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="report" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
