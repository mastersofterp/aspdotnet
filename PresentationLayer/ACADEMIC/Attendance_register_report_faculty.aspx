<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Attendance_register_report_faculty.aspx.cs" Inherits="ACADEMIC_Attendance_register_report_faculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Core Subject </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Global Elective Subject </a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <%--Attendance Tab--%>
                            <div class="tab-pane active" id="tab_1">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Session</label>--%>
                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" AutoPostBack="true"
                                                            ValidationGroup="Submit" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true">School/Institute Name </asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged" data-select2-enable="true"
                                                            TabIndex="2">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>


                                                       <%-- <asp:ListBox runat="server" ID="ddlInstitute" AppendDataBoundItems="true"  TabIndex="2"
                                                            SelectionMode="Multiple" CssClass="form-control multi-select-demo" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:ListBox>--%>
                                                        <%--  <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="ddlInstitute" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true"
                                                            CssClass="form-control" TabIndex="3" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                            CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSubject" runat="server" ControlToValidate="ddlCourse"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                        <%--    <sup>*</sup>--%>
                                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                                            ValidationGroup="Submit" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                                            AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                       <%-- <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType" SetFocusOnError="true"
                                                            ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"> Section</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="True"
                                                            TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <%--   <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                <%--    Added by Vipul Tichakule on date 26-03-2024 as per Tno:- 56526--%>
                                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <asp:Label ID="lblBatch" runat="server" Font-Bold="true"> Batch</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>                                                   
                                                    </div>
                                                    <%-- end --%>



                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <%-- <sup>*</sup>--%>
                                                            <label>From Date</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="imgCalDateOfBirth1" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="7" CssClass="form-control pull-right" AutoPostBack="true"
                                                                placeholder="From Date" ToolTip="Please Select From Date" />

                                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupButtonID="imgCalDateOfBirth1" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                TargetControlID="txtFromDate" Enabled="True" />
                                                           <%-- <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="submit" />--%>

                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>*</sup>--%>
                                                            <label>To Date</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="imgCalDateOfBirth2" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>

                                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="8" ValidationGroup="submit" placeholder="To Date" AutoPostBack="true"
                                                                ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtTodate" PopupButtonID="imgCalDateOfBirth2" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                                            <%--<ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                               IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />--%>
                                                          <%--  <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="submit" />--%>

                                                            <%--asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTodate"
                                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="submit"></%--asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnReport" runat="server" Text="Faculty Attendance Register Report" ValidationGroup="submit"
                                                    TabIndex="9" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    TabIndex="10" CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="submit" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div id="div7" runat="server">
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Session</label>--%>
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="true">Session</asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionG" AppendDataBoundItems="true" AutoPostBack="true"
                                                                ValidationGroup="Submit" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSessionG_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionG" SetFocusOnError="true"
                                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true">Global Course</asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlGlobalCourse" runat="server" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlGlobalCourse_SelectedIndexChanged"
                                                                CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGlobalCourse"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="submit1">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>From Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalDateOfBirth3" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtFdate" runat="server" TabIndex="3" CssClass="form-control pull-right" AutoPostBack="true"
                                                                    placeholder="From Date" ToolTip="Please Select From Date" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFdate" PopupButtonID="imgCalDateOfBirth3" Enabled="True">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate2" runat="server" Mask="99/99/9999"
                                                                    MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                    TargetControlID="txtFdate" Enabled="True" />
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFromDate2"
                                                                    ControlToValidate="txtFdate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                                    ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="submit1" />

                                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFdate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="submit1"></asp:RequiredFieldValidator>--%>
                                                            </div>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="imgCalDateOfBirth4" runat="server">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtTdate" runat="server" TabIndex="4" ValidationGroup="submit1" placeholder="To Date" AutoPostBack="true"
                                                                    ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtTdate" PopupButtonID="imgCalDateOfBirth4" Enabled="True">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="meTDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTdate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="meTDate"
                                                                    ControlToValidate="txtTdate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                                    ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                    IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="submit1" />

                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTdate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="submit1"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnExcelReport" runat="server" Text="Global Elective Faculty Attendance Register Report" ValidationGroup="submit1"
                                                        TabIndex="5" CssClass="btn btn-info" OnClick="btnExcelReport_Click" />
                                                    <asp:Button ID="BtnCancel2" runat="server" OnClick="BtnCancel2_Click" Text="Cancel"
                                                        TabIndex="6" CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="submit1" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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

