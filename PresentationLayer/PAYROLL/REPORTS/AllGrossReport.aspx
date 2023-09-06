<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AllGrossReport.aspx.cs" Inherits="PAYROLL_REPORTS_AllGrossReport" Title="" %>

<%--<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .bootstrap-duallistbox-container .buttons {
            display: flex;
        }

        :focus {
            outline: 0;
        }

        table {
            border: 1px solid #f4f4f4;
        }

        th {
            background-color: #fff !important;
            color: #333 !important;
            padding: 2px 8px;
            font-weight: 800 !important;
        }

        td {
            color: #333333 !important;
            background: #fff !important;
            padding: 2px 8px;
        }

        .arrow-top {
            margin-top: 75px;
        }

        .lbheight {
            height: 200px !important;
        }

        @media (max-width:576px) {
            .arrow-top {
                margin-top: 10px;
            }
        }
    </style>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">All Gross Reports</h3>
                        </div>
                        <div class="box-body">

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>All Gross Reports</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDt" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control" />



                                            <ajaxToolKit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgDate" TargetControlID="txtFromDt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                TargetControlID="txtFromDt">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mvDate" runat="server" ControlExtender="meDate"
                                                ControlToValidate="txtFromDt" Display="None" EmptyValueMessage="Please Enter From Date."
                                                ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDt" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgDate" TargetControlID="txtToDt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                TargetControlID="txtToDt">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meDate"
                                                ControlToValidate="txtToDt" Display="None" EmptyValueMessage="Please Enter To  Date."
                                                ErrorMessage="Please Select Valid Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Select College</label>
                                        </div>
                                        <%-- <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select College" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" data-select2-enable="true"
                                            AutoPostBack="true" CssClass="form-control"
                                            AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Staff</label>
                                        </div>
                                        <%--<asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Staff" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        <%--   <asp:DropDownList ID="ddlStaff" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" style="display:none"/>--%>

                                        <asp:ListBox ID="lstStaffFill" runat="server" Height="200px" CssClass="form-control" SelectionMode="Multiple" TabIndex="5"  style="height: 150px!important"
                                            AppendDataBoundItems="True"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstSfaffFill" runat="server" SetFocusOnError="true"
                                            ControlToValidate="lstStaffFill" Display="None" ErrorMessage="Please Select Staff from the list"
                                            ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>PayHead</label>
                                        </div>
                                        <%-- <asp:RequiredFieldValidator ID="rvfPayHead" runat="server" ControlToValidate="ddlPayHead"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select PayHead" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        <asp:DropDownList ID="ddlPayHead" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPayHead_SelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>




                            <div class="col-md-12 text-center">
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />

                                <asp:Button ID="btnGrossExcelReport" runat="server" Text="Gross Excel Report" TabIndex="25" CssClass="btn btn-info"
                                    ValidationGroup="submit" OnClick="btnGrossExcelReport_Click" />

                                <asp:Button ID="btnYearlyHeadwiseExcelReport" runat="server" Text="Yearly Headwise Excel Report" CssClass="btn btn-info" TabIndex="27"
                                    ValidationGroup="submit" OnClick="btnYearlyHeadwiseExcelReport_Click" />

                                <asp:Button ID="btnEmployeeYrGrossReport" runat="server" Text="Employeewise Yearly Gross Excel Report" CssClass="btn btn-info" TabIndex="27"
                                    OnClick="btnEmployeeYrGrossReport_Click" />


                                <asp:Button ID="btnDepatmentWiseYearlyGrossSalaryReport" runat="server" Text="Depatment Wise Yearly Gross Salary Report" CssClass="btn btn-info" TabIndex="27"
                                    ValidationGroup="submit" OnClick="btnDepatmentWiseYearlyGrossSalaryReport_Click" />
                            </div>




                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnYearlyHeadwiseExcelReport" />
            <asp:PostBackTrigger ControlID="btnGrossExcelReport" />
            <asp:PostBackTrigger ControlID="btnDepatmentWiseYearlyGrossSalaryReport" />
            <asp:PostBackTrigger ControlID="btnEmployeeYrGrossReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
