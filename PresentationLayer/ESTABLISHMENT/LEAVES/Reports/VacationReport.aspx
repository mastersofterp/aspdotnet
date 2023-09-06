<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VacationReport.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Reports_VacationReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">VACATION REPORT</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Selection Criteria</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" ToolTip="Select College Name">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College Name" SetFocusOnError="true" ValidationGroup="Shift"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaffType" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Shift"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Department">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control"
                                                    TabIndex="4" ToolTip="Enter From Date" Style="z-index: 0;" />
                                                <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                    ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter  From Date"
                                                    InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="5"
                                                    ToolTip="Enter To Date" Style="z-index: 0;" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt"
                                                    Display="None" ErrorMessage="Please Enter  To Date" ValidationGroup="Shift"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                    ControlToValidate="txtToDt" EmptyValueMessage="Please Enter  To Date"
                                                    InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter  To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                <%--<asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                            ValidationGroup="submit" ControlToCompare="txtFromDt" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="6"
                            ToolTip="Click here to Show Report" OnClick="btnShowReport_Click" ValidationGroup="Shift" />
                        <asp:Button ID="btnExport" runat="server" TabIndex="7"
                            OnClick="btnExport_Click" Text="Export" CssClass-="btn btn-info" ValidationGroup="Shift" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="8"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />                                        
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

