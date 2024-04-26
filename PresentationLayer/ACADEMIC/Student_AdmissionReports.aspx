<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_AdmissionReports.aspx.cs" Inherits="ACADEMIC_Student_AdmissionReports"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStuAdmission"
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
    <asp:UpdatePanel ID="updStuAdmission" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT/ADMISSION REPORTS</h3>
                            <%-- <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-3 col-3">
                                        <div id="aDIV" runat="server">
                                            <div>
                                                <label>Report</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlHideStuReport" runat="server" Visible="false">
                                    <div class="row">

                                        <div class="form-group col-lg-6 col-md-6 col-12" id="ReportType" runat="server">
                                            <div class="sub-heading">
                                                <h5>Report Type</h5>
                                            </div>
                                            <asp:RadioButton ID="rdoVerticalReport" runat="server" Text="Vertical Report"
                                                Checked="True" GroupName="A" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoHorizontalReport" runat="server"
                                                Text="Horizontal Report" GroupName="A" />
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Single Student Record</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search by</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoEnrollmentNo" Text="Enrollment No"
                                                        Visible="false" runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                                     <asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                                         GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdoRollNo" Text="Roll No" runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                                     <asp:RadioButton ID="rdoIdNo" Checked="true" Text="Student Id" runat="server" GroupName="stud" />
                                                </div>

                                                <div class="form-group col-lg-6 col-md-9 col-12">
                                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" TabIndex="8" />
                                                    <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                                                        ErrorMessage="Please Enter Value" ValidationGroup="Search" SetFocusOnError="true" Display="None">
                                                    </asp:RequiredFieldValidator>

                                                </div>


                                                <div class="form-group col-lg-6 col-md-3 col-12">
                                                    <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"
                                                        ValidationGroup="Search" TabIndex="9" CssClass="btn btn-primary" />
                                                </div>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />


                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                                <%--         <asp:Panel ID="pnlHideAdmSummary" runat="server" Visible="false" >
                                --%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="AdmBatch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server" ControlToValidate="ddlAdmbatch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="SessionReport" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Acdyear" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup id="AcdMeandatory" runat="server" visible="false">* </sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAcdYear" runat="server" ControlToValidate="ddlAcdYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="AdmRegsiter" />
                                        <asp:RequiredFieldValidator ID="rfvAcdYear1" runat="server" ControlToValidate="ddlAcdYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="EnrollRegister" />
                                        <asp:RequiredFieldValidator ID="rfvAcdYear2" runat="server" ControlToValidate="ddlAcdYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="AllYearView" />
                                        <asp:RequiredFieldValidator ID="rfvAcdYear3" runat="server" ControlToValidate="ddlAcdYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="EnrollForm" />
                                        <asp:RequiredFieldValidator ID="rfvAcdYear4" runat="server" ControlToValidate="ddlAcdYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="BYC" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Year" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="AdmRegsiter" />
                                        <asp:RequiredFieldValidator ID="rfvYear1" runat="server" ControlToValidate="ddlYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="EnrollRegister" />
                                        <%--<asp:RequiredFieldValidator ID="rfvYear2" runat="server" ControlToValidate="ddlYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="AllYearView" />--%>
                                        <asp:RequiredFieldValidator ID="rfvYear3" runat="server" ControlToValidate="ddlYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="EnrollForm" />
                                         <asp:RequiredFieldValidator ID="rfvYear4" runat="server" ControlToValidate="ddlYear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="BYC" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Institute" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup id="ClgMandatory" runat="server" visible="false">* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select School/Institute Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ErrorMessage="Please Select School/Institute Name" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvCollege2" runat="server" ErrorMessage="Please Select School/Institute Name" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvCollege3" runat="server" ErrorMessage="Please Select School/Institute Name" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="View"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Sessionn" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session" ControlToValidate="ddlSession"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSession2" runat="server" ErrorMessage="Please Select Session" ControlToValidate="ddlSession"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="View"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Degree" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup id="DegreeMandatory" runat="server" visible="false">* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="3"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDegree2" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Branch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup id="BranchMandatory" runat="server" visible="false">* </sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch" ControlToValidate="ddlBranch"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="StudentType" runat="server" visible="false">
                                        <div class="label-dynamic">
                                         <%--   <label>Student Type</label>--%>
                                             <asp:Label ID="lblDYddlStudentType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0" Selected="True">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Ex Student</asp:ListItem>
                                            <asp:ListItem Value="-1">Both</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Semester" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSemester2" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester"
                                            ValidationGroup="SessionReport"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="RdoStuName" runat="server" visible="false">
                                        <asp:RadioButton ID="rdoStudentName1" runat="server" Text="Student Name"
                                            Checked="True" GroupName="B" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoFatherName" runat="server" Text="Father Name"
                                            GroupName="B" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="Rdoaddress" runat="server" visible="false">
                                        <asp:RadioButton ID="rdoLocalAddress" runat="server" Text="Local Address"
                                            Checked="True" GroupName="A" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                        <asp:RadioButton ID="rdoPermanentAddress" runat="server"
                                            Text="Permanent Address" GroupName="A" />
                                    </div>


                                    <div class="form-group col-lg-4 col-md-6 col-12" id="SummaryBy" runat="server" visible="false" style="max-height: 100px;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Summary By</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="chkReportType" runat="server" CellPadding="1" CellSpacing="8" RepeatColumns="2" AppendDataBoundItems="true" RepeatDirection="Horizontal" CssClass="checkbox-list-style">
                                                <asp:ListItem Value="0"> DEGREE &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1"> BATCH &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2"> BRANCH &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3"> CATEGORY &nbsp;&nbsp;</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                </div>
                                <%-- </asp:Panel>
                                --%>
                            </div>
                            <br />

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlfooter" runat="server" Visible="false">
                                    <%--<div class="form-group col-lg-6 col-md-3 col-12">
                                                    <asp:Button ID="Button1" Text="Search" runat="server" 
                                                        ValidationGroup="Search" TabIndex="9" CssClass="btn btn-primary" />
                                                </div>
                                      <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />--%>

                                    <asp:Button ID="btnStudentReport" runat="server" Text="Report" OnClick="btnStudentReport_Click"
                                        ValidationGroup="Search" Visible="False" CssClass="btn btn-info" />
                                    <asp:Button ID="btnAdmSummaryReport" runat="server" Text="Report" OnClick="btnAdmSummaryReport_Click" ValidationGroup="Report" Visible="false"
                                        CssClass="btn btn-info" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />

                                    <asp:Button ID="btnShow" runat="server" Text="Show" Visible="false"
                                        CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnShow_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />

                                    <asp:Button ID="btnSummaryrpt" runat="server" Text="Summary Report" Visible="false" ValidationGroup="SessionReport"
                                        OnClick="btnSummaryrpt_Click" CssClass="btn btn-info" />
                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SessionReport" />

                                    <asp:Button ID="btnView" runat="server" Text="View"
                                        ValidationGroup="View" CssClass="btn btn-primary" OnClick="btnView_Click" Visible="false" />
                                    <asp:Button ID="btnShowStrengthReports" runat="server" Text="Export In Excel" Visible="false"
                                        ValidationGroup="View" CssClass="btn btn-info" OnClick="btnShowStrengthReports_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="View" />

                                    <asp:Button ID="btnAdmRegisterView" runat="server" Text="View"
                                        ValidationGroup="AdmRegsiter" CssClass="btn btn-primary" OnClick="btnAdmRegisterView_Click" Visible="false" />
                                    <asp:Button ID="btnAdmRegister" runat="server" Text="Export In Excel"
                                        ValidationGroup="AdmRegsiter" CssClass="btn btn-primary" OnClick="btnAdmRegister_Click" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary6" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="AdmRegsiter" />

                                    <asp:Button ID="btnEnrollRegisterView" runat="server" Text="View"
                                        ValidationGroup="EnrollRegister" CssClass="btn btn-primary" OnClick="btnEnrollRegisterView_Click" isible="false" />
                                    <asp:Button ID="btnEnrollRegister" runat="server" Text="Export In Excel"
                                        ValidationGroup="EnrollRegister" CssClass="btn btn-primary" OnClick="btnEnrollRegister_Click" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary7" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EnrollRegister" />

                                    <asp:Button ID="btnAllYearView" runat="server" Text="View"
                                        ValidationGroup="AllYearView" CssClass="btn btn-primary" OnClick="btnAllYearView_Click" Visible="false" />
                                    <asp:Button ID="btnAllYearExcel" runat="server" Text="Export In Excel"
                                        ValidationGroup="AllYearView" CssClass="btn btn-primary" OnClick="btnAllYearExcel_Click" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary8" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="AllYearView" />

                                    <asp:Button ID="btnEnrollForm" runat="server" Text="View"
                                        ValidationGroup="EnrollForm" CssClass="btn btn-primary" OnClick="btnEnrollForm_Click" Visible="false" />
                                    <asp:Button ID="btnEnrollFormExcel" runat="server" Text="Export In Excel"
                                        ValidationGroup="EnrollForm" CssClass="btn btn-primary" OnClick="btnEnrollFormExcel_Click" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary9" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EnrollForm" />

                                    <asp:Button ID="btnBYCStrength" runat="server" Text="Report" OnClick="btnBYCStrength_Click"
                                        ValidationGroup="BYC" Visible="False" CssClass="btn btn-info" />
                                      <asp:ValidationSummary ID="ValidationSummary10" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="BYC" />


                                    <asp:Button ID="btnStudentCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnStudentCancel_Click"
                                        Visible="False" CssClass="btn btn-warning" />


                                </asp:Panel>
                            </div>


                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Report
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Batch
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                                        ImageUrl="~/Images/print.png" ToolTip="Show Report" OnClick="btnShowReport" />
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLMENTNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREENO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("YEARNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12" id="dvGrid" runat="server" visible="false">
                                <div style="height: 400px; overflow: auto;">
                                    <asp:Panel ID="panell" runat="server" Style="overflow-x: scroll;" Visible="false">
                                        <asp:GridView ID="lstDetails" runat="server" CellPadding="6" EmptyDataText="No Records Found" CssClass="Freezing" EnableModelValidation="True" ForeColor="#333333" GridLines="Horizontal" Height="105px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#1b7a87" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1B7A87" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvAllYearStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>S.No
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Roll No
                                                        </th>
                                                        <th>Identity No
                                                        </th>
                                                        <th>Enrollment No
                                                        </th>
                                                        <th>Gender
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Mother Name
                                                        </th>
                                                        <th>Admission
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <%# Eval("Sno")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Course")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Roll No")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Identity No")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Enrollment No")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Gender")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Mother Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:ListView ID="lvStuEnrollForm" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>S.No
                                                        </th>
                                                        <th>Enroll. No
                                                        </th>
                                                        <th>Sex
                                                        </th>
                                                        <th>Name of the Candidate
                                                        </th>
                                                        <th>Mother's Name
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Blind/Handicap
                                                        </th>
                                                        <th>Birth Date
                                                        </th>
                                                        <th>Admission Date
                                                        </th>
                                                        <th>Qualifying Exam
                                                        </th>
                                                        <th>Roll No
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <%# Eval("Sno")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Enroll No")%>
                                                </td>

                                                <td>
                                                    <%# Eval("Sex")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Name of the Candidate")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Mother's Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Category")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Blind/Handicap")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Birth Date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission Date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Qualifying Exam")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Roll No")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel4" runat="server">
                                    <asp:ListView ID="lvStuAdmRegister" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>S.No
                                                        </th>
                                                        <th>ERP Id
                                                        </th>
                                                        <th>DTE Id
                                                        </th>
                                                        <th>Enroll No
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Admission Date
                                                        </th>
                                                        <th>Admission Center
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Address
                                                        </th>
                                                        <th>District Name
                                                        </th>
                                                        <th>Mobile
                                                        </th>
                                                        <th>Eamil
                                                        </th>
                                                        <th>Father's Name
                                                        </th>
                                                        <th>Mother's Name
                                                        </th>
                                                        <th>Date of Birth
                                                        </th>
                                                        <th>Caste
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Qaulifying Exam
                                                        </th>
                                                        <th>Qaulifying Year
                                                        </th>
                                                        <th>Last Institute
                                                        </th>
                                                        <th>TC No and Date
                                                        </th>
                                                        <th>Reason To Leave
                                                        </th>
                                                        <th>Remark
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <%# Eval("Sno")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ERP Id")%>
                                                </td>

                                                <td>
                                                    <%# Eval("DTE Id")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Enroll No")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Branch")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission Date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission Center")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Address")%>
                                                </td>
                                                <td>
                                                    <%# Eval("District Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Mobile")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Email")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Father's Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Mother's Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Date of Birth")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Caste")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Category")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Qaulifying Exam")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Qaulifying Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Last Institute")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TC No and Date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Reason To Leave")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Remark")%>
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel5" runat="server">
                                    <asp:ListView ID="lvStuEnrollRegister" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>S.No
                                                        </th>
                                                        <th>Enroll No
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>MotherName
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Gender
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Blind/Hasndicap
                                                        </th>
                                                        <th>Date of Birth
                                                        </th>
                                                        <th>Admission Date
                                                        </th>
                                                        <th>Admission Center
                                                        </th>
                                                        <th>Qaulifying Exam
                                                        </th>

                                                        <th>Roll No
                                                        </th>
                                                        <th>Passing Year
                                                        </th>
                                                        <th>University
                                                        </th>
                                                        <th>Board
                                                        </th>
                                                        <th>Remark
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <%# Eval("Sno")%>
                                                </td>

                                                <td>
                                                    <%# Eval("Enroll No")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MotherName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Branch")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Gender")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Category")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Blind/Handicap")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Date of Birth")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission Date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Admission Center")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Qualifying Exam")%>
                                                </td>

                                                <td>
                                                    <%# Eval("Roll No")%>
                                                </td>

                                                <td>
                                                    <%# Eval("Passing Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("University")%>
                                                </td>


                                                <td>
                                                    <%# Eval("Board")%>
                                                </td>


                                                <td>
                                                    <%# Eval("Remark")%>
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudentRecords" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSummaryrpt" />
            <asp:PostBackTrigger ControlID="btnView" />
            <asp:PostBackTrigger ControlID="btnShowStrengthReports" />
            <asp:PostBackTrigger ControlID="btnAllYearExcel" />
            <asp:PostBackTrigger ControlID="btnEnrollFormExcel" />
            <asp:PostBackTrigger ControlID="btnAdmRegister" />
            <asp:PostBackTrigger ControlID="btnEnrollRegister" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>


