<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AllAttendance_Report.aspx.cs" Inherits="ACADEMIC_AllAttendance_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

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
                                <div class="sub-heading">
                                    <h5>Report Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Reports Name</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlReportName" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click"
                                            class="btn btn-warning" TabIndex="21" />
                                    </div>
                                </div>
                                <div class="sub-heading" id="divheading" runat="server" visible="false">
                                    <h5>Data Filters</h5>
                                </div>

                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlInstitute" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true">School/Institute Name </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="ddlInstitute" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>

                                        <%--<asp:RequiredFieldValidator ID="rfvInstitute1" runat="server" ControlToValidate="ddlInstitute" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSchool" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" ToolTip="Please Select School/Institute" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select School/Institute" InitialValue="0" ValidationGroup="Submit"  Visible="false" ></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSession" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Session</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            CssClass="form-control"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit"  Visible="false"></asp:RequiredFieldValidator>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlCollege" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>

                                        <asp:ListBox runat="server" ID="ddlCollege" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvddlCollegeMulti" ControlToValidate="ddlCollege" InitialValue=""
                                            Display="None" ValidationGroup="Submit" runat="server" ErrorMessage="Please Select College."  Visible="false"></asp:RequiredFieldValidator>
                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlCollege" InitialValue=""
                                            Display="None" ValidationGroup="Excel" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="ddlCollege" InitialValue=""
                                            Display="None" ValidationGroup="ShowStudent" runat="server" ErrorMessage="Please Select College."></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>

                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
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
                                            CssClass="form-control" data-select2-enable="true"
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
                                            TabIndex="4">
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


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSem" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            CssClass="form-control" TabIndex="5" data-select2-enable="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>--%>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>


                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlCourseType" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourseType" runat="server" AutoPostBack="True"
                                            ValidationGroup="Submit" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlCourseType" SetFocusOnError="true"
                                            ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSubjectType" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" data-select2-enable="true"
                                            ValidationGroup="Submit" TabIndex="1" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--                                <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                    ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator> --%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSubject" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvddlSubject" runat="server" ControlToValidate="ddlSubject"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Subject" ValidationGroup="Daywise">
                                                            </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlSection" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"> Section</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="8" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Submit"  Visible="false"></asp:RequiredFieldValidator>

                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>--%>


                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlSection"
                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="OD"></asp:RequiredFieldValidator>--%>


<%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>

                                    </div>









                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddltheorypractical" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Theory/Practical/Tutorial</label>
                                        </div>
                                        <asp:DropDownList ID="ddltheorypractical" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <asp:ListItem Value="3">Tutorial</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvLTP" runat="server" ControlToValidate="ddltheorypractical"
                                 Display="None" ErrorMessage="Please Select Theory or Practical or Tutorial Type course" InitialValue="0" ValidationGroup="SubPercentage" ></asp:RequiredFieldValidator>--%>
                                    </div>






                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlDepartment" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfddlDepartment" runat="server" ControlToValidate="ddlDepartment" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Submit"  Visible="false"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlFaculty" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" ValidationGroup="Submit" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlFaculty" runat="server"
                                            ControlToValidate="ddlFaculty" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Submit" Visible="false" ></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtFromDate" runat="server" visible="false">
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

                                            <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Submit"  Visible="false"></asp:RequiredFieldValidator>

                                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtTodate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalDateOfBirth2" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="10" ValidationGroup="submit" placeholder="To Date" AutoPostBack="true"
                                                ToolTip="Please Select To Date" CssClass="form-control pull-right" OnTextChanged="txtTodate_TextChanged" />

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

                                            <asp:RequiredFieldValidator ID="rfvtxtTodate" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Submit"  Visible="false"></asp:RequiredFieldValidator>

                                       <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>

                                        </div>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divddlOperator" runat="server" visible="false">
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

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtPercentage" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentage" runat="server" MaxLength="3" Text="0" CssClass="form-control"
                                            TabIndex="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Percentage." ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="101"
                                            MinimumValue="0" Type="Integer"  Visible="false"></asp:RangeValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 25px; display: none">
                                        <asp:RadioButton ID="rdoOpr" runat="server" AutoPostBack="true" Text="With Operator" TabIndex="1" OnCheckedChanged="rdoOpr_CheckedChanged" GroupName="GetPercent" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 25px;" id="divrdoPerBtn" runat="server" visible="false">
                                        <asp:RadioButton ID="rdoPerBtn" runat="server" TabIndex="1" AutoPostBack="true" Text=" Between Percentage" OnCheckedChanged="rdoPerBtn_CheckedChanged" GroupName="GetPercent" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtPercentageFrom" runat="server" visible="false">
                                        <div class="label-dynamic" id="lblperfrom" runat="server">
                                            <sup>* </sup>
                                            <label>Percentage From</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentageFrom" MaxLength="3" TabIndex="1" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtPercentageFrom" runat="server" ControlToValidate="txtPercentageFrom"
                                            Display="None" ErrorMessage="Please Enter From Percentage." ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtPercentageTo" runat="server" visible="false">
                                        <div class="label-dynamic" id="lblPerTo" runat="server">
                                            <sup>* </sup>
                                            <label>Percentage To</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentageTo" MaxLength="3" TabIndex="1" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPercentageTo" runat="server" ControlToValidate="txtPercentageTo" Display="None"
                                            ErrorMessage="Please Enter To Percentage " ValidationGroup="Submit" Visible="false" ></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :"></asp:Label>
                                        </p>
                                    </div>

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="REPORT" class="btn btn-primary" TabIndex="2" OnClick="btnReport_Click" ValidationGroup="Submit" Visible="false"/>
                                    <asp:Button ID="btnExcelReport" runat="server" Text="EXCEL(REPORT)" class="btn btn-primary" TabIndex="3" OnClick="btnExcelReport_Click" ValidationGroup="Submit" Visible="false" />
                                    <asp:Button ID="btnShow" runat="server" Text="SHOW" class="btn btn-primary" TabIndex="3" OnClick="btnShow_Click" ValidationGroup="Submit" Visible="false" />
                                     <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                </div>

                                <div class="col-12" id="divFacultyAttendanceStatus" runat="server" visible="false">
                                    <asp:Panel ID="pnlAttStatus" runat="server">
                                        <asp:ListView ID="lvAttStatus" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Faculty Attendance List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr. No.</th>
                                                            <th>Attendance Date</th>
                                                            <th>Slot</th>
                                                            <th>Topic</th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                            <th>Faculty Name</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow">
                                                    <td><%#Container.DataItemIndex+1 %></td>
                                                    <td><%#Eval("ATT_DATE") %></td>
                                                    <td><%#Eval("SLOT") %></td>
                                                    <td><%#Eval("TOPIC_COVERED") %></td>
                                                    <td><%#Eval("COURSE_NAME") %></td>
                                                    <td><%#Eval("UA_FULLNAME") %></td>
                                                    <td><%#Eval("STATUS_NAME") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" id="divShowAttendanceDetails" runat="server" visible="false">
                                    <asp:Panel ID="pnlAttendence" runat="server">
                                        <asp:ListView ID="lvAttendence" runat="server">
                                            <LayoutTemplate>
                                                <table id="divattendencelist" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>REGISTRATION NO</th>
                                                            <th>STUDENT NAME</th>
                                                            <th>SCHEME</th>
                                                            <th>SEMESTER</th>
                                                            <th>PROGRAMME/BRANCH</th>
                                                            <th>SECTION</th>
                                                            <th>OVERALL ATTENDENCE</th>
                                                            <th>OVERALL PRESENT</th>
                                                            <th>OVERALL ABSENT</th>
                                                            <th>TOTAL PERCENTAGE</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceHolder" runat="server">
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%-- <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" Visible="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                    <td><%#Eval("ENROLLMENT_NO")%></td>
                                                    <td><%#Eval("STUDENT_NAME")%></td>
                                                    <td><%#Eval("SCHEME")%></td>
                                                    <td><%#Eval("SEMESTER")%></td>
                                                    <td><%#Eval("BRANCH")%></td>
                                                    <td><%#Eval("SECTION")%></td>
                                                    <td><%#Eval("TOTAL_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_ATTENDED_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_ABSENT_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_PERCENTAGE")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" id="divShowAttendanceDetailsByPercent" runat="server" visible="false">
                                    <asp:Panel ID="pnlByPercent" runat="server">
                                        <asp:ListView ID="lvByPercent" runat="server">
                                            <LayoutTemplate>
                                                <table id="divAttendanceByPer" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>REGISTRATION NO</th>
                                                            <th>STUDENT NAME</th>
                                                            <th>SCHEME</th>
                                                            <th>SEMESTER</th>
                                                            <th>PROGRAM/BRANCH</th>
                                                            <th>SECTION</th>
                                                            <th>OVERALL ATTENDENCE</th>
                                                            <th>OVERALL PRESENT</th>
                                                            <th>OVERALL ABSENT</th>
                                                            <th>TOTAL PERCENTAGE</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceHolder" runat="server">
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%-- <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" Visible="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                    <td><%#Eval("ENROLLMENT_NO") %></td>
                                                    <td><%#Eval("STUDENT_NAME")%></td>
                                                    <td><%#Eval("SCHEME")%></td>
                                                    <td><%#Eval("SEMESTER")%></td>
                                                    <td><%#Eval("BRANCH")%></td>
                                                    <td><%#Eval("SECTION")%></td>
                                                    <td><%#Eval("TOTAL_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_ATTENDED_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_ABSENT_CLASSES")%></td>
                                                    <td><%#Eval("TOTAL_PERCENTAGE")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class=" col-12" id="divStudentAttendanceDetails" runat="server" visible="false">
                                    <div class="sub-heading" id="divlvStudentHeading" runat="server" >
                                        <h5>Student Attendance Details</h5>
                                    </div>
                                    <asp:ListView ID="lvStudAttendance" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example2">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Student Name</th>
                                                        <th>
                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Total Calsses</th>
                                                        <th>Present</th>
                                                        <th>Absent</th>
                                                        <th>Percentage</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BRANCH") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_CLASSES") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PRESENT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ABSENT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PERCENTAGE") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyItemTemplate>
                                            <p>No record found! </p>
                                        </EmptyItemTemplate>
                                    </asp:ListView>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>



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

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

