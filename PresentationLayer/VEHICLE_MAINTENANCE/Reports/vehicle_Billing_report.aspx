<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="vehicle_Billing_report.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_vehicle_Billing_report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE INFORMATION REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">

                                <asp:Panel ID="ReportType" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Report Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTripType" runat="server" OnSelectedIndexChanged="ddlTripType_SelectedIndexChanged"
                                                AutoPostBack="True" CssClass="form-control" data-select2-enable="true" ToolTip="Select Report Type" TabIndex="1">
                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">College Bus Bill Report</asp:ListItem>
                                                <asp:ListItem Value="2">College Tour Bill Report</asp:ListItem>
                                                <asp:ListItem Value="3">Contract Bill Report </asp:ListItem>
                                                <asp:ListItem Value="4">HOD Cars Bill Report</asp:ListItem>
                                                <%--<asp:ListItem Value="5">Vehicle Details</asp:ListItem>--%>
                                                <%--<asp:ListItem Value="6">Daily Attendance Report</asp:ListItem>--%>
                                                <asp:ListItem Value="7">Driver TA Report</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlTripType" runat="server" ErrorMessage="Please Select Report Type "
                                                ControlToValidate="ddlTripType" InitialValue="0" Display="None" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="row">
                                    <asp:Panel ID="panVehicleDetails" runat="server" Visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Vehicle Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal" TabIndex="2"
                                                OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Vehicle Type">
                                                <asp:ListItem Selected="True" Value="1">College Vehicles&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Hire Vehicles</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Vehicle Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Vehicle Name" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVeh" runat="server" ControlToValidate="ddlVehicle"
                                                Display="None" ErrorMessage="Please Select Vehicle" SetFocusOnError="True" ValidationGroup="Submit"
                                                Visible="False"></asp:RequiredFieldValidator>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <asp:Panel ID="panSupplierDetails" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Driver" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Driver Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDriverName" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Driver Name" TabIndex="4">
                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Supplier" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Supplier</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Supplier Name" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    TabIndex="6" />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ErrorMessage="Please Enter From Date"
                                                    ControlToValidate="txtFDate" Display="None" SetFocusOnError="True" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtFDate" Display="None">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1789">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                    TabIndex="7" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ErrorMessage="Please Enter To Date"
                                                    ControlToValidate="txtTDate" Display="None" SetFocusOnError="True" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTDate" PopupButtonID="Image1789">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTDate"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtTDate" Display="None">
                                                </ajaxToolKit:MaskedEditValidator>
                                                <%-- <asp:CompareValidator ControlToCompare="txtFDate" ControlToValidate="txtTDate" Display="None"
                                                                    ErrorMessage="From Date is less than To date" ID="CompareValidator1" Operator="GreaterThanEqual"
                                                                    Type="Date" runat="server" ValidationGroup="Submit" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanCommon" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Report In</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal" ToolTip="Select Report In Type"
                                                TabIndex="8" OnSelectedIndexChanged="rdoReportType_SelectedIndexChanged">
                                                <%-- <asp:ListItem Selected="True" Value="No Export">Normal Report</asp:ListItem>--%>
                                                <asp:ListItem Selected="True" Value="pdf">Adobe Reader&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="xls">MS-Excel&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="doc">MS-Word&nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Report" ValidationGroup="Submit" TabIndex="9"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-info" ToolTip="Click here to Show Report" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="true" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />

                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
