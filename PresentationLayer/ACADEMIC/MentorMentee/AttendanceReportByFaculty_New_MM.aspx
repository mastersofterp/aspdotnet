<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceReportByFaculty_New_MM.aspx.cs" Inherits="ACADEMIC_MentorMentee_AttendanceReportByFaculty_New_MM" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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
    <asp:UpdatePanel ID="updSection" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true">School/Institute Name </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="ddlInstitute" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvInstitute1" runat="server" ControlToValidate="ddlInstitute" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlInstitute"
                                        Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlInstitute"
                                        Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Session</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true"
                                            CssClass="form-control"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="2">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true">Scheme</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Regulation" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" ErrorMessage="Please Select Regulation" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="form-control" TabIndex="5" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                            ValidationGroup="Submit" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType" SetFocusOnError="true"
                                            ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvddlSubject" runat="server" ControlToValidate="ddlSubject"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Subject" ValidationGroup="Daywise">
                                                            </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"> Section</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="8" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlSection"
                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalDateOfBirth1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="9" CssClass="form-control pull-right" AutoPostBack="true"
                                                placeholder="From Date" ToolTip="Please Select From Date" OnTextChanged="txtFromDate_TextChanged" />

                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalDateOfBirth1" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="OD"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalDateOfBirth2" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                                <asp:TextBox ID="txtTodate" runat="server" TabIndex="10" ValidationGroup="SubPercentage" placeholder="To Date" AutoPostBack="true"
                                                ToolTip="Please Select To Date" CssClass="form-control pull-right" OnTextChanged="txtTodate_TextChanged" />
                                            <asp:CompareValidator ID="cvtxtToDate" ValidationGroup="SubPercentage" ForeColor="Red" runat="server"
                                                                ControlToValidate="txtTodate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual" Type="Date"
                                                                Display="None" ErrorMessage="From date must be less than to date."></asp:CompareValidator>
                                             <asp:CompareValidator ID="CompareValidator1" ValidationGroup="OD" ForeColor="Red" runat="server"
                                                                ControlToValidate="txtTodate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual" Type="Date"
                                                                Display="None" ErrorMessage="From date must be less than to date."></asp:CompareValidator>
                                             <asp:CompareValidator ID="CompareValidator2" ValidationGroup="Cumulative" ForeColor="Red" runat="server"
                                                                ControlToValidate="txtTodate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual" Type="Date"
                                                                Display="None" ErrorMessage="From date must be less than to date."></asp:CompareValidator>
                                          
                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodate" PopupButtonID="imgCalDateOfBirth2" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="OD"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Operator</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOperator" runat="server" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentage" runat="server" MaxLength="3" Text="0" CssClass="form-control"
                                            TabIndex="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Percentage." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="101"
                                            MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Report In</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server"
                                            RepeatDirection="Horizontal" TabIndex="13">
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                       <%--<asp:LinkButton ID="btnSubjectwise" runat="server" TabIndex="14" OnClick="btnSubjectwise_Click" ValidationGroup="SubPercentage" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Subject Wise Report</asp:LinkButton>--%>

                                <asp:LinkButton ID="btnSubjectwiseExpected" runat="server" TabIndex="15"
                                    OnClick="btnSubjectwiseExpected_Click" ValidationGroup="SubPercentage" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Subject Wise Details</asp:LinkButton>

                                  <%--   <asp:LinkButton ID="btnAttDetails" runat="server" TabIndex="16"
                                    OnClick="btnAttDetails_Click" ValidationGroup="SubPercentage" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Attendance Details</asp:LinkButton>--%>

<%--                                 <asp:LinkButton ID="btnCumulativeAttendance" runat="server" TabIndex="17"
                                    OnClick="btnCumulativeAttendance_Click" ValidationGroup="Cumulative" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Cumulative Attendance</asp:LinkButton>--%>

<%--                                  <asp:LinkButton ID="btnAttReportWithOD" runat="server" TabIndex="18"
                                    OnClick="btnAttReportWithOD_Click" ValidationGroup="OD" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i>  Att. Report With OD</asp:LinkButton>--%>

                                <%-- <asp:LinkButton ID="btnFacultyIncompleteAttendance" runat="server" TabIndex="19"
                                    OnClick="btnFacultyIncompleteAttendance_Click" ValidationGroup="IncompleteFacultyAttendance" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Faculty Incomplete Attendance</asp:LinkButton>--%>
                               <%-- <asp:LinkButton ID="btnODExcelReport" runat="server" TabIndex="20" ToolTip="OD's Greater Than 63 Count" Visible="false"
                                    OnClick="btnODExcelReport_Click" ValidationGroup="OD" CssClass="btn btn-info">
                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Student OD Count Report</asp:LinkButton>--%>

                                <asp:LinkButton ID="btnSubjectwiseExpectedExcel" runat="server" TabIndex="21"
                                    OnClick="btnSubjectwiseExpectedExcel_Click" ValidationGroup="SubPercentage" CssClass="btn btn-info">
                                     <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Subject Wise Details Excel</asp:LinkButton>

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    class="btn btn-warning" TabIndex="22" OnClick="btnCancel_Click1" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="SubPercentage" Style="text-align: center" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Cumulative" Style="text-align: center" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    Style="text-align: center" ValidationGroup="IncompleteFacultyAttendance" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="OD" Style="text-align: center" />

                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>

          <%--  <asp:PostBackTrigger ControlID="btnSubjectwise" />--%>
            <asp:PostBackTrigger ControlID="btnSubjectwiseExpected" />
            <%--            <asp:PostBackTrigger ControlID="btnAttDetails" />--%>
          <%--  <asp:PostBackTrigger ControlID="btnCumulativeAttendance" />
            <asp:PostBackTrigger ControlID="btnAttReportWithOD" />--%>
            <%--<asp:PostBackTrigger ControlID="btnODExcelReport" />--%>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSubjectwiseExpectedExcel" />
             <asp:PostBackTrigger ControlID="txtFromDate" />
             <asp:PostBackTrigger ControlID="txtTodate" />
            <%--<asp:PostBackTrigger ControlID="btnPrintChallan" />
                                    <asp:PostBackTrigger ControlID="btnPrintRegSlip" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }
    </script>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>
    <div id="divMsg" runat="server">
    </div>
    <%--  Reset the sample so it can be played again --%>
</asp:Content>
