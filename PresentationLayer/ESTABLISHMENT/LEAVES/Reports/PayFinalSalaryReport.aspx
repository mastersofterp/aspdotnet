<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayFinalSalaryReport.aspx.cs" Inherits="PAYROLL_REPORTS_PayFinalSalaryReport" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FINAL SALARY REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divAnnualSummaryRport">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                            ToolTip="Select College" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Payroll"
                                            SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Staff Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                            AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                            Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Payroll"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Department </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
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
                                            <sup></sup>
                                            <label>To Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalToDate" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" AutoPostBack="True"
                                                OnTextChanged="txtToDate_TextChanged" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                                ValidationGroup="Payroll" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-primary" OnClick="btnShowReport_Click"
                                        ValidationGroup="Payroll" TabIndex="6" />
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                        TabIndex="7" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

