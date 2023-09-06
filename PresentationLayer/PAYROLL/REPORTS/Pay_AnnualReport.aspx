<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_AnnualReport.aspx.cs" Inherits="PayRoll_Pay_AnnualReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ANNUAL SALARY SUMMARY REPORT</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Annual Summary Report</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalFromDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ToolTip="Enter From Date" CssClass="form-Control" />
                                        <%--<div class="input-group-addon">
                                            <asp:Image ID="imgCalFromDate" runat="server"
                                                Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                                        </div>--%>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate1" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                            ValidationGroup="Payroll" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalToDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Enter To Date"
                                            OnTextChanged="txtToDate_TextChanged" />
                                      <%--  <div class="input-group-addon">
                                            <asp:Image ID="imgCalToDate" runat="server"
                                                Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                                        </div>--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="imgCalToDate1" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                        <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                            Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                            ValidationGroup="Payroll" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trRadioBtnEmployee" runat="server">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:RadioButton ID="rdoParticularEmployee" runat="server" Checked="true"
                                        Text="Particular Employee" GroupName="Employee" onclick="DisableDropDownListParticularEmployee(true);"
                                        TabIndex="3" ToolTip="Particular Employee" />

                                    <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="Employee"
                                        TabIndex="4" ToolTip="All Employee" onclick="DisableDropDownListAllEmployee(true);" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College"
                                        AppendDataBoundItems="true" TabIndex="5" data-select2-enable="true">
                                    </asp:DropDownList>
                                </div>

                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Employee Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                         <sup>* </sup>
                                        <%--<label>Staff</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme/Staff"
                                        AppendDataBoundItems="true" TabIndex="6" data-select2-enable="true">
                                    </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaffNo"
                                            Display="None" ErrorMessage="Please Scheme/Staff" SetFocusOnError="true"
                                            ValidationGroup="Payroll" />

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True"
                                        CssClass="form-control" TabIndex="7" ToolTip="Select Employee" data-select2-enable="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                            OnClick="btnShowReport_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Report" />
                        <asp:Button ID="btnAnnualSummaryReport" runat="server" Text="Employee Wise Annual Summary Report"
                            OnClick="btnAnnualSummaryReport_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Export" />
                         <asp:Button ID="btnAnnualSumExport" runat="server" Text="Employee Wise Annual Summary Export"
                            OnClick="btnAnnualSummaryExport_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Export" />
                          <asp:Button ID="btnConsolidateAllHead" runat="server" Text="Consolidate All Head Export"
                            OnClick="btnConsoAllHead_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Export" />
                         <asp:Button ID="btnConsoIncomeHead" runat="server" Text="Consolidate Income Head Export"
                            OnClick="btnConsoIncomeHead_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Export" />
                         <asp:Button ID="btnConsoDedHead" runat="server" Text="Consolidate Deduction Head Export"
                            OnClick="btnConsoDedHead_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="8" ToolTip="Click To Show the Export" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" TabIndex="9" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        function DisableDropDownListAllEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = false;

        }
        function DisableDropDownListParticularEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = false;
        }

    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>

