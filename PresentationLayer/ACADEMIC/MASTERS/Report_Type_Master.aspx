<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Report_Type_Master.aspx.cs" Inherits="ACADEMIC_MASTERS_Report_Type_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>

    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCollegeScheme" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSession" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSemester" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCousrseType" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCousrse" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSection" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdFromDate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdToDate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdOperator" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdPercent" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSubjecttype" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTheory" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdBtnPercent" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCollegemultiple" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSchoolinst" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdDepartment" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdFaculty" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdExcelReport" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdShowlist" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSessionValidation" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="UPDPROG" runat="server" AssociatedUpdatePanelID="UPDROLE"
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


    <asp:UpdatePanel runat="server" ID="UPDROLE">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Report Name</label>
                                        </div>
                                        <asp:TextBox ID="txtReportName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="64" TabIndex="1"
                                            ToolTip="Please Enter Report Name" placeholder="Enter Report Name" />
                                        <asp:RequiredFieldValidator ID="rfvReportName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Report Name" ControlToValidate="txtReportName"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required College & Scheme</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdCollegeScheme" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdCollegeScheme"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Session</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSession" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSession"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Semester</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSemester" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSemester"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Course Type</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdCousrseType" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdCousrseType"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Course</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdCousrse" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdCousrse"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Section</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSection" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSection"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required From Date</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdFromDate" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdFromDate"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required To Date</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdToDate" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdToDate"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Operator</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdOperator" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdOperator"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Percentage</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdPercent" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdPercent"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Subject Type</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSubjecttype" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSubjecttype"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Theory/Practical/Tutorial</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdTheory" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdTheory"></label>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Between Percentage</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdBtnPercent" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdBtnPercent"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Multiple College</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdCollegemultiple" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdCollegemultiple"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required School/Institute</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSchoolinst" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSchoolinst"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Department</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdDepartment" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdDepartment"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Required Faculty</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdFaculty" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdFaculty"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Excel Report</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdExcelReport" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdExcelReport"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Session Validation Required</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdSessionValidation" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdSessionValidation"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Is Show Required</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdShowlist" name="switch" checked />
                                            <label data-on="Yes" data-off="No" for="rdShowlist"></label>
                                        </div>
                                    </div>

                                    

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="3" CssClass="btn btn-primary" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="4" CssClass="btn btn-warning"  />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">

                                <asp:Panel ID="pnlReport" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Report List</h5>
                                    </div>
                                    <asp:ListView ID="lvReport" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>Report Name
                                                        </th>
                                                        <th>Active Status
                                                        </th>
                                                        <th>Is College Scheme Status
                                                        </th>
                                                        <th>Is Session Status
                                                        </th>
                                                        <th>Is Semester Status
                                                        </th>
                                                        <th>Is Course Type Status
                                                        </th>
                                                        <th>Is Course Status
                                                        </th>
                                                        <th>Is Section Status
                                                        </th>
                                                        <th>Is From Date Status
                                                        </th>
                                                        <th>Is To Date Status
                                                        </th>
                                                        <th>Is Operator Status
                                                        </th>
                                                        <th>Is Percentage Status
                                                        </th>
                                                        <th>Is Subject Type Status
                                                        </th>
                                                        <th>Is Theory/Practical/Tutorial Status
                                                        </th>
                                                        <th>Is Between Percentage Status
                                                        </th>
                                                        <th>Is Multiple College Status
                                                        </th>
                                                        <th>Is School Institute Status
                                                        </th>
                                                        <th>Is Department Status
                                                        </th>
                                                        <th>Is Faculty Status
                                                        </th>
                                                        <th>Is Excel Report
                                                        </th>
                                                        <th>Is Session Validation
                                                        </th>
                                                        <th>Is Show Required
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        OnClick="btnEdit_Click"   CommandArgument='<%# Eval("REPORT_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        TabIndex="14" />
                                                </td>
                                                <td>
                                                    <%# Eval("REPORT_NAME") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" CssClass='<%# Eval("IS_COLLEGE_SCHEME_STATUS")%>' Text='<%# Eval("IS_COLLEGE_SCHEME_STATUS")%>' ForeColor='<%# Eval("IS_COLLEGE_SCHEME_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass='<%# Eval("IS_SESSION_STATUS")%>' Text='<%# Eval("IS_SESSION_STATUS")%>' ForeColor='<%# Eval("IS_SESSION_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" CssClass='<%# Eval("IS_SEMESTER_STATUS")%>' Text='<%# Eval("IS_SEMESTER_STATUS")%>' ForeColor='<%# Eval("IS_SEMESTER_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" CssClass='<%# Eval("IS_COURSE_TYPE_STATUS")%>' Text='<%# Eval("IS_COURSE_TYPE_STATUS")%>' ForeColor='<%# Eval("IS_COURSE_TYPE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" CssClass='<%# Eval("IS_COURSE_STATUS")%>' Text='<%# Eval("IS_COURSE_STATUS")%>' ForeColor='<%# Eval("IS_COURSE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" CssClass='<%# Eval("IS_SECTION_STATUS")%>' Text='<%# Eval("IS_SECTION_STATUS")%>' ForeColor='<%# Eval("IS_SECTION_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" CssClass='<%# Eval("IS_FROM_DATE_STATUS")%>' Text='<%# Eval("IS_FROM_DATE_STATUS")%>' ForeColor='<%# Eval("IS_FROM_DATE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" CssClass='<%# Eval("IS_TO_DATE_STATUS")%>' Text='<%# Eval("IS_TO_DATE_STATUS")%>' ForeColor='<%# Eval("IS_TO_DATE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" CssClass='<%# Eval("IS_OPERATOR_STATUS")%>' Text='<%# Eval("IS_OPERATOR_STATUS")%>' ForeColor='<%# Eval("IS_OPERATOR_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" CssClass='<%# Eval("IS_PERCENTAGE_STATUS")%>' Text='<%# Eval("IS_PERCENTAGE_STATUS")%>' ForeColor='<%# Eval("IS_PERCENTAGE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" CssClass='<%# Eval("IS_SUBJECT_TYPE_STATUS")%>' Text='<%# Eval("IS_SUBJECT_TYPE_STATUS")%>' ForeColor='<%# Eval("IS_SUBJECT_TYPE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" CssClass='<%# Eval("IS_THEORY_PRACTICAL_TUTORIAL_STATUS")%>' Text='<%# Eval("IS_THEORY_PRACTICAL_TUTORIAL_STATUS")%>' ForeColor='<%# Eval("IS_THEORY_PRACTICAL_TUTORIAL_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" CssClass='<%# Eval("IS_BETWEEN_PERCENTAGE_STATUS")%>' Text='<%# Eval("IS_BETWEEN_PERCENTAGE_STATUS")%>' ForeColor='<%# Eval("IS_BETWEEN_PERCENTAGE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" CssClass='<%# Eval("IS_MULTIPLE_COLLEGE_STATUS")%>' Text='<%# Eval("IS_MULTIPLE_COLLEGE_STATUS")%>' ForeColor='<%# Eval("IS_MULTIPLE_COLLEGE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" CssClass='<%# Eval("IS_SCHOOL_INSTITUTE_STATUS")%>' Text='<%# Eval("IS_SCHOOL_INSTITUTE_STATUS")%>' ForeColor='<%# Eval("IS_SCHOOL_INSTITUTE_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" CssClass='<%# Eval("IS_DEPARTMENT_STATUS")%>' Text='<%# Eval("IS_DEPARTMENT_STATUS")%>' ForeColor='<%# Eval("IS_DEPARTMENT_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" CssClass='<%# Eval("IS_FACULTY_STATUS")%>' Text='<%# Eval("IS_FACULTY_STATUS")%>' ForeColor='<%# Eval("IS_FACULTY_STATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>

                                                 <td>
                                                    <asp:Label ID="Label18" runat="server" CssClass='<%# Eval("ISEXCEL_REPORT")%>' Text='<%# Eval("ISEXCEL_REPORT")%>' ForeColor='<%# Eval("ISEXCEL_REPORT").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>

                                                 <td>
                                                    <asp:Label ID="Label19" runat="server" CssClass='<%# Eval("IS_FROMDT_RFV")%>' Text='<%# Eval("IS_FROMDT_RFV")%>' ForeColor='<%# Eval("IS_FROMDT_RFV").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>

                                                 <td>
                                                    <asp:Label ID="Label20" runat="server" CssClass='<%# Eval("IS_SHOW_REPORT")%>' Text='<%# Eval("IS_SHOW_REPORT")%>' ForeColor='<%# Eval("IS_SHOW_REPORT").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

     <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <%--  <label>PASSWORD</label>--%>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="password" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>





    <script>
        function SetStatActive(val) {
        
            $('#rdActive').prop('checked', val);
        } function SetStatCollegeScheme(val) {
            $('[id*=rdCollegeScheme]').prop('checked', val);
        } function SetStatSession(val) {
            $('[id*=rdSession]').prop('checked', val);
        } function SetStatSemester(val) {
            $('[id*=rdSemester]').prop('checked', val);
        } function SetStatCousrseType(val) {
            $('[id*=rdCousrseType]').prop('checked', val);
        } function SetStatCousrse(val) {
            $('[id*=rdCousrse]').prop('checked', val);
        } function SetStatSection(val) {
            $('[id*=rdSection]').prop('checked', val);
        } function SetStatFromDate(val) {
            $('[id*=rdFromDate]').prop('checked', val);
        } function SetStatToDate(val) {
            $('[id*=rdToDate]').prop('checked', val);
        } function SetStatOperator(val) {
            $('[id*=rdOperator]').prop('checked', val);
        } function SetStatPercent(val) {
            $('[id*=rdPercent]').prop('checked', val);
        } function SetStatSubjecttype(val) {
            $('[id*=rdSubjecttype]').prop('checked', val);
        } function SetStatTheory(val) {
            $('[id*=rdTheory]').prop('checked', val);
        } function SetStatBtnPercent(val) {
            $('[id*=rdBtnPercent]').prop('checked', val);
        } function SetStatCollegemultiple(val) {
            $('[id*=rdCollegemultiple]').prop('checked', val);
        } function SetStatSchoolinst(val) {
            $('[id*=rdSchoolinst]').prop('checked', val);
        } function SetStatDepartment(val) {
            $('[id*=rdDepartment]').prop('checked', val);
        } function SetStatFaculty(val) {
            $('[id*=rdFaculty]').prop('checked', val);
        }
        function SetStatExcelReport(val) {
            $('[id*=rdExcelReport]').prop('checked', val);
        }
        function SetStatShowlist(val) {
            $('[id*=rdShowlist]').prop('checked', val);
        }
        function SetStatSessionValidation(val) {
            $('[id*=rdSessionValidation]').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdCollegeScheme').val($('#rdCollegeScheme').prop('checked'));
            $('#hfdSession').val($('#rdSession').prop('checked'));
            $('#hfdSemester').val($('#rdSemester').prop('checked'));
            $('#hfdCousrseType').val($('#rdCousrseType').prop('checked'));
            $('#hfdCousrse').val($('#rdCousrse').prop('checked'));
            $('#hfdSection').val($('#rdSection').prop('checked'));
            $('#hfdFromDate').val($('#rdFromDate').prop('checked'));
            $('#hfdToDate').val($('#rdToDate').prop('checked'));
            $('#hfdOperator').val($('#rdOperator').prop('checked'));
            $('#hfdPercent').val($('#rdPercent').prop('checked'));
            $('#hfdSubjecttype').val($('#rdSubjecttype').prop('checked'));
            $('#hfdTheory').val($('#rdTheory').prop('checked'));
            $('#hfdBtnPercent').val($('#rdBtnPercent').prop('checked'));
            $('#hfdCollegemultiple').val($('#rdCollegemultiple').prop('checked'));
            $('#hfdSchoolinst').val($('#rdSchoolinst').prop('checked'));
            $('#hfdDepartment').val($('#rdDepartment').prop('checked'));
            $('#hfdFaculty').val($('#rdFaculty').prop('checked'));

            $('#hfdExcelReport').val($('#rdExcelReport').prop('checked'));
            $('#hfdShowlist').val($('#rdShowlist').prop('checked'));
            $('#hfdSessionValidation').val($('#rdSessionValidation').prop('checked'));

            var idtxtweb = $("[id$=txtReportName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Report Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

     <script type="text/javascript">
         $(window).on('load', function () {
             $('#myModalPopUp').modal('show');
         });
    </script>

</asp:Content>

