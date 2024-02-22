<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BacklogExamRegReport.aspx.cs" Inherits="ACADEMIC_REPORTS_BacklogExamRegReport" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam">
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
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Backlog Exam Registration Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>

                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="Backlogsub" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Secheme" InitialValue="0" ValidationGroup="Backlogsub">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Secheme" InitialValue="0" ValidationGroup="BacklogCount">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Secheme" InitialValue="0" ValidationGroup="overallbacklog">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>

                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="show" TabIndex="1"
                                            CssClass="form-control" AutoPostBack="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Backlogsub"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="BacklogCount"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="overallbacklog"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="1" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Scheme</label>--%>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>*</sup>--%>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Backlogsub"
                                            InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="BacklogCount"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <%--<sup>*</sup>--%>

                                            <%--<label>Courses</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourses" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                            TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourses" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="Backlogsub"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="ReportType" runat="server">

                                        <div class="label-dynamic">
                                            <sup></sup>

                                            <label>Report Type</label>-
                                            
                                        </div>
                                        <asp:RadioButton runat="server" Font-Size="Small" ID="rdopdf" Text="PDF" AutoPostBack="true" GroupName="reporttype" Checked="true" OnCheckedChanged="rdopdf_CheckedChanged" TabIndex="1" />
                                        &nbsp;&nbsp;
                                             <asp:RadioButton runat="server" Font-Size="Small" ID="rdoexcel" Text="EXCEL" AutoPostBack="true" GroupName="reporttype" OnCheckedChanged="rdoexcel_CheckedChanged" TabIndex="1" />


                                    </div>
                                    <%-- <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class="note-div">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Backlog Student Status Report -><br />
                                                    <span style="color: green; font-weight: bold" class="ml-4">Degree->Branch->Scheme->Semester</span></span>
                                            </p>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1"
                                    CssClass="btn btn-primary" OnClick="btnReport_Click" ValidationGroup="Backlogsub" Visible="false" />
                                <asp:Button ID="btnBacklogStuStatus" runat="server" Text="Backlog Student Status Report" TabIndex="1"
                                    CssClass="btn btn-primary" OnClick="btnBacklogStuStatus_Click" ValidationGroup="Backlogsub" Visible="false" />
                                <asp:Button ID="btnBacklogSubjects" runat="server" Text="Student Backlog List" TabIndex="1" OnClick="btnBacklogSubjects_Click"
                                    CssClass="btn btn-primary" ValidationGroup="Backlogsub" ToolTip="Branch Selection is Optional." />

                                <asp:Button ID="btnsubjectArrearCount" runat="server" TabIndex="1" CssClass="btn btn-primary" ValidationGroup="BacklogCount" Text="Subject Wise Arrear Count List" OnClick="btnsubjectArrearCount_Click" />
                                <%--  <asp:Button ID="btnCommonSubject" runat="server"  Text="Common Subject List" CssClass="btn btn-info"
                                                TabIndex="11" ValidationGroup="Backlogsub" />--%>

                                <asp:Button ID="btnArrearReport" runat="server" Text="Subject Wise Arrear List" CssClass="btn btn-primary"
                                    TabIndex="1" ValidationGroup="Backlogsub" OnClick="btnArrearReport_Click" />
                                <p class="text-center">
                                    <asp:Button ID="btnBackLogExamRegFeesPaid" runat="server" Text="Exam Registration List" CssClass="btn btn-primary"
                                        TabIndex="1" ValidationGroup="Backlogsub" OnClick="btnBackLogExamRegFeesPaid_Click" />
                                    <asp:Button ID="btnResitReport" runat="server" Text="Resit/ Re-Examination Report" CssClass="btn btn-primary" Visible="false"
                                        TabIndex="1" ValidationGroup="Backlogsub" OnClick="btnResitReport_Click" />
                                    <asp:Button ID="btnoverallbacklog" runat="server" Text="Overall Backlog List" CssClass="btn btn-primary" Visible="true"
                                        TabIndex="1" ValidationGroup="overallbacklog" OnClick="btnoverallbacklog_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel"
                                        TabIndex="1" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Backlogsubject"  />--%>
                                </p>
                                <%-- <p class="text-center">
                                            <asp:Button ID="btnBacklogPracMarks" runat="server" OnClick="btnBacklogPracMarks_Click" Text="Backlog Practical Marks" 
                                                CssClass="btn btn-info" TabIndex="14" ValidationGroup="practicalbacklogreport" />
                                           

                                            <asp:Button ID="btnSubjectwiseDetailedArrear" runat="server" OnClick="btnSubjectwiseDetailedArrear_Click"
                                                 CssClass="btn btn-info" TabIndex="16"  ValidationGroup="ArrearDetailed" Text="SubjectWise Detailed Arrear List" /> 
                                            </p>
                                        <p class="text-center">
                                            <asp:Button ID="btnExcelSubjectRegList" runat="server" OnClick="btnExcelSubjectRegList_Click"
                                                 CssClass="btn btn-info" TabIndex="17"  ValidationGroup="practicalbacklogreport" Text="Subject Registered List Excel" /> 

                                             <asp:Button ID="btnGradeRegisterExcel" runat="server" OnClick="btnGradeRegisterExcel_Click"
                                                 CssClass="btn btn-info" TabIndex="17"  ValidationGroup="practicalbacklogreport" Text="Subject Registered List Excel With Grade" />--%>


                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Backlogsub" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="BacklogCount" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="overallbacklog" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>

            <asp:PostBackTrigger ControlID="btnArrearReport" />
            <asp:PostBackTrigger ControlID="btnBacklogSubjects" />
            <asp:PostBackTrigger ControlID="btnBackLogExamRegFeesPaid" />
            <asp:PostBackTrigger ControlID="btnsubjectArrearCount" />
            <asp:PostBackTrigger ControlID="btnResitReport" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-info {
            height: 26px;
        }
    </style>
</asp:Content>

