<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FacultyWorkLoad.aspx.cs" Inherits="ACADEMIC_FacultyWorkLoad" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTech"
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

    <asp:UpdatePanel ID="updTech" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup id="clgspan" runat="server">* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Report"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" ValidationGroup="Submit" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession" ValidationGroup="Report" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup id="semspan" runat="server">* </sup>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester1" runat="server" ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup id="subspan" runat="server">* </sup>
                                            <%--<label>Subject Type</label>--%>
                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType1" runat="server" ControlToValidate="ddlSubjectType" Display="None"
                                            ErrorMessage="Please Select Course/Subject Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="frmdtspan" runat="server">* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="Show" onpaste="return false;"
                                                TabIndex="1" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="dvcal1" />
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="Report" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="todtspan" runat="server">* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="Show" TabIndex="1"
                                                ToolTip="Please Enter To Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="dvcal2" />
                                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter To Date" ControlToValidate="txtToDate" Display="None"
                                                ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate"
                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Report" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-info" TabIndex="1" OnClick="btnReport_Click" ValidationGroup="Report"><i class="fa fa-file"></i> Work Load Report</asp:LinkButton>
                                <asp:Button ID="btnCancel" runat="server" TabIndex="1" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                <asp:ValidationSummary ID="vsUpload" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="upload" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <%--<script type="text/javascript">
         function RunThisAfterEachAsyncPostback() {
             ConfirmToDelete();
         }
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
</asp:Content>

