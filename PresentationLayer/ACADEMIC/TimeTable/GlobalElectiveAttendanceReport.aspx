<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GlobalElectiveAttendanceReport.aspx.cs" Inherits="ACADEMIC_TimeTable_GlobalElectiveAttendanceReport" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0">
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

    <asp:UpdatePanel ID="updAttStatus" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <form role="form">
                            <div class="box-body">

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>

                                            <asp:ListBox runat="server" ID="ddlCollege" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" ControlToValidate="ddlCollege" InitialValue=""
                                                Display="None" ValidationGroup="Show" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCollege" InitialValue=""
                                                Display="None" ValidationGroup="Excel" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCollege" InitialValue=""
                                                Display="None" ValidationGroup="ShowStudent" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon" id="idPopup" runat="server">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit"
                                                    TabIndex="1" />
                                                <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="idPopup" TargetControlID="txtFromDate" Animated="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFromDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                    ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Show" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                    ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Excel" />
                                                <ajaxToolKit:MaskedEditValidator ID="meFromConsole" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                    ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="ShowStudent" />

                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon" id="idPopuptodate" runat="server">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodate" runat="server" TabIndex="1" ValidationGroup="submit" />
                                                <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="idPopuptodate"
                                                    TargetControlID="txtTodate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                    ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Show" />

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                    ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="ShowStudent" />

                                                <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                    ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Excel" />
                                              

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btsShowAvgAttendance" Visible="false" runat="server" Text="Show Average Attendance" ValidationGroup="Show" OnClick="btsShowAvgAttendance_Click" CssClass="btn btn-primary"></asp:Button>
                                             <asp:Button ID="btnExcelAvgAttendance" Visible="false" runat="Server" ToolTip="Average Attendance(Excel)" Text="Average Attendance(Excel)" TabIndex="3" ValidationGroup="Excel" OnClick="btnExcelAvgAttendance_Click"
                                                CssClass="btn btn-info" />
                                            <asp:Button ID="btnExcelCoursewise" runat="Server" ToolTip="Excel" Text="Global Elective Course-Wise Report(Excel)" TabIndex="3" ValidationGroup="Excel" OnClick="btnExcelCoursewise_Click"
                                                CssClass="btn btn-info" />
                                            <asp:Button ID="btnShowStudentWise" runat="server" Text="Show Student-Wise Attendance" ValidationGroup="ShowStudent" OnClick="btnShowStudentWise_Click" CssClass="btn btn-primary"></asp:Button>

                                            <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" TabIndex="4" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ShowStudent" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Excel" />
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </form>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelCoursewise" />
            <asp:PostBackTrigger ControlID="btnShowStudentWise" />
        </Triggers>

    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>
