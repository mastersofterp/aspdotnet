<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="logbook_report.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_logbook_report" Title="" %>

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
                            <h3 class="box-title">LOG BOOK REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Vehicle Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Vehicle Name" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Driver</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Driver Name" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>LogNo</label>
                                        </div>
                                        <asp:DropDownList ID="ddlLogNo" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Log Number" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Trip Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTripType" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Trip Type" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Passenger</label>
                                        </div>
                                        <asp:TextBox ID="txtPassenger" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Enter Passenger"
                                            TabIndex="5"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImgBntCalc">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                TabIndex="6" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="ImgBntCalc" TargetControlID="txtFDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFDate" ClearMaskOnLostFocus="true" />
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtFDate"
                                                IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*" ValidationGroup="Submit" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1896">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                TabIndex="7" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTDate" PopupButtonID="Image1896" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTDate"
                                                ClearMaskOnLostFocus="true" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtTDate" IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                                Text="*" ValidationGroup="Submit" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Report in</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server"
                                            RepeatDirection="Horizontal" ToolTip="Select Report Type" TabIndex="8">
                                            <%--<asp:ListItem Selected="True" Value="No Export">Normal Report</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                            <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                </div>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"/>
            <asp:PostBackTrigger ControlID="btnCancel"/>
        </Triggers>
    </asp:UpdatePanel>
    
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

