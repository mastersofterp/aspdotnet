<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Global_Offered_Courses.aspx.cs" Inherits="ACADEMIC_Global_Offered_Courses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href='<%=Page.ResolveUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>' rel="stylesheet" />--%>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <%--  <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>
    <%--<script src='<%=Page.ResolveUrl("~/plugins/multiselect/bootstrap-multiselect.js") %>'></script>--%>
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .disabled {
            background-color: #f0f0f0; /* Example background color for disabled state */
            color: #999; /* Example text color for disabled state */
            /* Add any other styles to visually indicate the disabled state */
        }

        .studentlist .dataTables_filter {
            display: none !important;
        }

        .studentlist .dt-buttons {
            display: none !important;
        }
    </style>


    <style>
        .fa-plus {
            font-weight: 900;
        }

        .subCard-header {
            background: #F0F3FF 0% 0% no-repeat padding-box;
            padding: 10px 0px;
            border-radius: 10px 10px 0px 0px;
        }

        .font-18 {
            font-size: 18px;
        }

        .fa-edit {
            color: #007bff;
            font-size: 16px;
        }

        #tdcollegesession .btn-group, #tdBranch .btn-group {
            width: 200px !important;
        }

        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>


    <%--  <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <%--<asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>

                    </h3>

                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Global Offered Course</a>
                            </li>
                            <li class="nav-item" id="liCourseTeacher" runat="server" visible="false">
                                <a class="nav-link" data-toggle="tab" href="#tab_5" tabindex="2">Course Teacher Allotment</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="3">Teacher Student Allotment</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="4">Attendance Configuration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="5">Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_7" tabindex="6">Revised Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_6" tabindex="7">Cancel Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_8" id="tab8" tabindex="8">TT Report</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPanel1"
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
                                <asp:UpdatePanel ID="updPanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College & Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Semester</label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-12 btn-footer">

                                                    <asp:Button ID="btnAd" runat="server" Text="Submit" ValidationGroup="offered" OnClientClick="return validateAssign();" OnClick="btnAd_Click" CssClass="btn btn-primary"></asp:Button>
                                                    <asp:Button ID="btnCancel0" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel0_Click" CssClass="btn btn-warning" />
                                                    <asp:Button ID="btnPrint" runat="server" Text="Report" Visible="false" ValidationGroup="Report" OnClick="btnPrint_Click" CssClass="btn btn-info"></asp:Button>

                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                                        <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Courses</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" id="tblCourseList" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Code </th>
                                                                            <th>CourseName </th>
                                                                            <th>Elective Group </th>
                                                                            <th>Credits </th>
                                                                            <th>Select </th>
                                                                            <th>Max Seat </th>
                                                                            <th><sup>* </sup>Offered To Semester</th>
                                                                            <th><sup>* </sup>Offered To College/session </th>
                                                                            <th>
                                                                                <sup>* </sup>
                                                                                Offered To
                                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Status
                                                                            </th>
                                                                            <%--  <th><sup>* </sup>Main Teacher </th>
                                                                            <th>Additional Teacher</th>--%>
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
                                                                        <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME")%>' />
                                                                        <%-- <%# Eval("COURSE_NAME")%>--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("GROUPNAME")%>' />
                                                                        <%-- <%# Eval("COURSE_NAME")%>--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCrdeit" runat="server" Text='<%# Eval("CREDITS")%>' />
                                                                        <%--<%# Eval("CREDITS") %>--%>
                                                                    </td>


                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:HiddenField ID="hf_chkSelect" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMaxSeat" runat="server" Enabled="false" MaxLength="3" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ListBox runat="server" ID="lstSemester" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                                                    </td>
                                                                    <td id="tdcollegesession">
                                                                        <asp:ListBox runat="server" ID="lstCollegeSession" AutoPostBack="true" Width="200px" SelectionMode="Multiple" CssClass="form-control multi-select-demo" OnSelectedIndexChanged="lstCollegeSession_SelectedIndexChanged"></asp:ListBox>
                                                                    </td>
                                                                    <td id="tdBranch">
                                                                        <asp:ListBox runat="server" ID="lstBranch" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                                                    </td>
                                                                    <td class='text-center; vertical-align:middle'>
                                                                        <label class="switch">
                                                                            <asp:CheckBox ID="chkActiveStatus" runat="server" Checked="true" />

                                                                            <span class="slider round"></span>
                                                                        </label>
                                                                        <%--<div class="switch form-inline" id="divswitch" runat="server">
                                                                            <input type="checkbox" id='rdActive_<%# Eval("COURSENO")%>'   onclick="return SetActiveStatus(this)" name="switch" class="activestatusclass" checked />
                                                                            <label data-on="Active" data-off="Inactive" for='rdActive_<%# Eval("COURSENO")%>'></label>
                                                                        </div>
                                                                        <asp:HiddenField ID="hdfActiveStatus" runat="server"/>--%>
                                                                    </td>
                                                                    <%--<td>
                                                                        <asp:DropDownList runat="server" ID="ddlMainTeacher" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlMainTeacher_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="ddlAdditionalTeacher" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="col-12" visible="false" id="dvNote" runat="server">
                                                            <asp:Label ID="lblNoteHeader" Font-Size="Medium" runat="server"></asp:Label>
                                                            <asp:Label ID="lblNote" ForeColor="Red" Font-Size="Medium" runat="server"></asp:Label>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlGLobalOfferedCourses" runat="server" Visible="false">
                                                        <asp:ListView ID="lvGLobalOfferedCourses" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Global Offered Courses</h5>
                                                                </div>
                                                                <table class="table table-bordered table-hover table-striped display" id="tblGLobalOfferedCourseList">
                                                                    <thead>
                                                                        <tr style="background-color: #F3F3F3;">
                                                                            <%--<th></th>--%>
                                                                            <th>Edit</th>
                                                                            <th>Code </th>
                                                                            <th>Course Name </th>
                                                                            <th>Elective Group</th>
                                                                            <th>Semester </th>
                                                                            <th>Offered Semester </th>
                                                                            <th>Scheme Name </th>
                                                                            <th>Offered
                                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Session Name</th>
                                                                            <th>Max Seat </th>
                                                                            <th>Status</th>
                                                                            <%-- <th>Main Teacher </th>
                                                                            <th>Additional Teacher </th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="bg-pista text-darkgray">

                                                                    <%-- <td>
                                                                        <asp:Button ID="btnInActive" runat="server" Text='In-Active' CssClass="btn btn-warning"
                                                                            OnClick="btnInActive_Click" OnClientClick="return confirm('Do you want to In-Active the record ? ');" ToolTip='<%# Eval("GROUPID")%>' />
                                                                    </td>--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ToolTip='<%# Eval("COURSENO")%>' CommandArgument='<%# Eval("GROUPID")%>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("GROUPNAME")%>' ToolTip='<%# Eval("GROUPNAME")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblOfferedToSem" runat="server" Text='<%# Eval("SEMESTER")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblOfferedSem" runat="server" Text='<%# Eval("OFFERED_SEMESTER")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEMENAME")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("BRANCHNAME")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("SESSION_NAME")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMaxSeat" runat="server" Text='<%# Eval("CAPACITY")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOBAL_OFFERED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                            Text='<%# (Convert.ToInt32(Eval("GLOBAL_OFFERED") ) == 1 ?  "Active" : "In-Active" )%>' />
                                                                    </td>
                                                                    <%--<td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("MAINTEACHER")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("ADDITIONALTEACHER")%>' />
                                                                    </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="col-12" visible="false" id="Div1" runat="server">
                                                            <asp:Label ID="Label1" Font-Size="Medium" runat="server"></asp:Label>
                                                            <asp:Label ID="Label2" ForeColor="Red" Font-Size="Medium" runat="server"></asp:Label>
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:PostBackTrigger ControlID="lvCourse" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_5">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updCourseTeacher"
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
                                <asp:UpdatePanel ID="updCourseTeacher" Visible="false" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSession_Tab3" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionCT" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="courseteacher" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSessionCT_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSessionCT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="courseteacher">
                                                    </asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Course</label>--%>
                                                        <asp:Label ID="lblDYddlCourse_Tab3" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourseCT" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="courseteacher" OnSelectedIndexChanged="ddlCourseCT_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCourseCT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="courseteacher">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Group / Section</label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlGlobalElectiveGroup" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="courseteacher" OnSelectedIndexChanged="ddlGlobalElectiveGroup_SelectedIndexChanged"
                                                        CssClass="form-control" AutoPostBack="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlGlobalElectiveGroup"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Group / Section" ValidationGroup="courseteacher">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Main Teacher</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlMainTeacherCT" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlMainTeacherCT_SelectedIndexChanged"
                                                        ValidationGroup="courseteacher" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlMainTeacherCT"
                                                        Display="None" ErrorMessage="Please Select Main Teacher" InitialValue="0" ValidationGroup="courseteacher"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">

                                                        <label>Additional Teacher</label>
                                                    </div>
                                                    <asp:ListBox ID="lstAdditionalTeacherCT" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="4"></asp:ListBox>

                                                </div>



                                            </div>
                                            <div class="col-12 btn-footer">

                                                <asp:Button ID="btnSubmitCT" runat="server" Text="Submit" OnClick="btnSubmitCT_Click" CssClass="btn btn-primary" ValidationGroup="courseteacher" />
                                                <asp:Button ID="btnClearCT" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClearCT_Click" CssClass="btn btn-warning"></asp:Button>

                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="courseteacher"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />


                                            <div class="col-12">

                                                <asp:Panel ID="Panel3" runat="server">

                                                    <asp:ListView ID="lvGlobalCourseTeacher" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Course Teacher Allotment List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblCourseTeacher">
                                                                <thead class="bg-light-blue" style="top: -15px !important">
                                                                    <tr>

                                                                        <th>
                                                                            <asp:Label ID="lblDYlvSession" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>

                                                                        <th>
                                                                            <asp:Label ID="lblDYlvCourse" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>Group / Section
                                                                        </th>
                                                                        <th>Main Teacher
                                                                        </th>
                                                                        <th>Additional Teacher
                                                                        </th>
                                                                        <%-- <th>Action</th>--%>
                                                                        <th>Status
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

                                                                <td>
                                                                    <%# Eval("SESSION_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COURSE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SECTIONNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("UA_FULLNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ADD_TEACHER_NAME")%>
                                                                </td>
                                                                <%-- <td >
                                                                    <asp:Button ID="btnEditCT" runat="server" Text='Edit' CssClass="btn btn-primary"
                                                                        OnClick="btnEditCT_Click" OnClientClick="return confirm('Do you want to Modify the record ? ');" CommandArgument='<%# Eval("UA_NO")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdfCTSection" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                                </td>--%>

                                                                <td>


                                                                    <asp:Button ID="btnInActiveCT" runat="server" Text='In-Active' CssClass="btn btn-warning"
                                                                        OnClick="btnInActiveCT_Click" OnClientClick="return confirm('Do you want to In-Active the record ? ');" CommandArgument='<%# Eval("UA_NO")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Scheme</label>--%>
                                                        <asp:Label ID="lblDYddlColgSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeSession" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="teacherallot" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollegeSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <%--  <asp:RequiredFieldValidator ID="rfvddlSchemeAT" runat="server" ControlToValidate="ddlCollegeSession"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select College & Session" ValidationGroup="teacherallotAT">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollegeSession"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select College & Session" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Scheme</label>--%>
                                                        <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="teacherallot" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallotAT">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Semester</label>--%>
                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemesterAT" runat="server" TabIndex="4" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSemesterAT_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Course</label>--%>
                                                        <asp:Label ID="lblDYddlCourse_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjectAT" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSubjectAT_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSubjectAT" runat="server" ControlToValidate="ddlSubjectAT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallotAT">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubjectAT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">

                                                <asp:Button ID="btnShowStudent" runat="server" Text="Show Student" ValidationGroup="teacherallotAT" OnClick="btnShowStudent_Click" CssClass="btn btn-success"></asp:Button>
                                                <asp:Button ID="btnSubmitAllotment" runat="server" Text="Submit" OnClick="btnSubmitAllotment_Click" CssClass="btn btn-primary" ValidationGroup="teacherallot" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClear_Click" CssClass="btn btn-warning"></asp:Button>

                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallotAT"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Section</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlsection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged"
                                                            ValidationGroup="teacherallot" AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlsection"
                                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Main Teacher</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged"
                                                            ValidationGroup="teacherallot" AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                                            Display="None" ErrorMessage="Please Select Main Teacher" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAddtionalTeacherTS" runat="server">
                                                        <div class="label-dynamic">

                                                            <label>Additional Teacher</label>
                                                        </div>
                                                        <asp:ListBox ID="lstAdditionalTeacher" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="4"></asp:ListBox>

                                                    </div>
                                                    <div id="Div2" class="form-group col-lg-2 col-md-6 col-12" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Total Selected Students</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotStud" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="studentlist">
                                                        <asp:ListView ID="lvStudents" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudents">
                                                                    <thead class="bg-light-blue" style="top: -15px !important;">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Student Name
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Semester
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>

                                                                            <th>Main Teacher
                                                                            </th>
                                                                            <th>Additional Teacher
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
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totStud(this);" ToolTip='<%# Eval("IDNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REGNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BRANCHNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNAME")%>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("SECTIONNAME")%>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("UA_FULLNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ADD_TEACHER_NAME")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updpanelconfig"
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
                                <asp:UpdatePanel ID="updpanelconfig" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College & Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeSchemeAttConfig" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeSchemeAttConfig_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollegeSchemeAttConfig"
                                                        Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="AttConfig"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionAttConfig" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSessionAttConfig_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSessionAttConfig"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="AttConfig"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:ListBox ID="lstSemesterAttConfig" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="4"></asp:ListBox>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="lstSemesterAttConfig"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="" ValidationGroup="AttConfig"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance Start Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="5" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                            ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Attendance Start Date" IsValidEmpty="false"
                                                            InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                            TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="AttConfig" SetFocusOnError="True" />
                                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                            Display="None" SetFocusOnError="True"
                                                            ValidationGroup="AttConfig" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance End Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtEndDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="6" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtEndDate" PopupButtonID="txtEndDate1" />

                                                        <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                            ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Attendance End Date"
                                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                            TooltipMessage="Please Enter Attendance End Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="AttConfig" SetFocusOnError="True" />

                                                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                            ControlToValidate="txtEndDate" Display="None"
                                                            ValidationGroup="AttConfig" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Attendance Lock By Day</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAttLockDay" runat="server" CssClass="form-control" TabIndex="7" MaxLength="3" />
                                                    <asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                                        ErrorMessage="Please Enter Attendance Lock By Day" ControlToValidate="txtAttLockDay"
                                                        Display="None" ValidationGroup="AttConfig" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtAttLockDay" />
                                                </div>

                                                 <%--(d-none) added by vipul T on date 04-03-2024 as per TNO:-55726 --%>
                                                <div class="form-group col-lg-2 col-md-6 col-6 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS Facility</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdSMSYes" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdSMSYes"></label>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-2 col-md-6 col-6 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Email Facility</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdEmailYes" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdEmailYes"></label>
                                                    </div>
                                                </div>
                                                <%--  end--%> 

                                                <div class="form-group col-lg-2 col-md-6 col-6" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Course Registration</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rbCRegBefore" name="switch" checked />
                                                        <label data-on="Before" data-off="After" for="rbCRegBefore"></label>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-2 col-md-6 col-6">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Teaching Plan</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdTeachYes" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdTeachYes"></label>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-2 col-md-6 col-6">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Check For Active</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <br />
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSunmitAttConfig" runat="server" Text="Submit" TabIndex="1" ValidationGroup="AttConfig" OnClientClick="return validate();" OnClick="btnSunmitAttConfig_Click" class="btn btn-primary" />
                                            <asp:Button ID="btnCancelAttConfig" runat="server" Text="Cancel" CausesValidation="false" TabIndex="1" OnClick="btnCancelAttConfig_Click" class="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="AttConfig" />
                                        </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvAttConfig" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Configuration List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab-le">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Edit</th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <%--   <th>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>--%>

                                                                    <th>Start Date</th>
                                                                    <th>End Date</th>
                                                                    <th>Att Lock Days</th>
                                                                    <th class="d-none">SMS Facility</th>
                                                                    <th class="d-none">Email Facility</th>

                                                                    <th>Teaching Plan</th>
                                                                    <th>Active</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditAttConfig" runat="server" CausesValidation="false" CommandArgument='<%# Eval("SESSIONID")%>' ImageUrl="~/Images/edit.png" OnClick="btnEditAttConfig_Click" ToolTip="Edit Record" />
                                                        </td>
                                                        <td><%#Eval("SESSION_NAME") %></td>
                                                        <%-- <td><%#Eval("SEMESTER")%></td>--%>
                                                        <td><%#Eval("START_DATE")%></td>
                                                        <td><%#Eval("END_DATE")%></td>
                                                        <td><%#Eval("LOCK_ATT_DAYS")%></td>
                                                       <%-- <td><%#Eval("SMS_FACILITY")%></td>
                                                        <td><%#Eval("EMAIL_FACILITY")%></td>--%>
                                                        <td><%#Eval("TEACHING_PLAN")%></td>
                                                        <td><%#Eval("ACTIVE")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:HiddenField ID="hfdSms" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfdEmail" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfdCourse" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfdTeaching" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="tab-pane fade" id="tab_4">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Scheme</label>--%>
                                                        <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeSchemeTimeTable" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="timetable" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollegeSchemeTimeTable_SelectedIndexChanged">
                                                        <asp:ListItem Value="0-0-0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollegeSchemeTimeTable"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="timetable">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionTimeTable" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSessionTimeTable_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSessionTimeTable"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="timetable"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTTSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTTSection_SelectedIndexChanged"
                                                        ValidationGroup="teacherallot" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlTTSection"
                                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="timetable"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Course</label>--%>
                                                        <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjectTimetable" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSubjectTimetable_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSubjectTimetable"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="timetable">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Slot Type</label>--%>
                                                        <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged"
                                                        TabIndex="10" CssClass="form-control slottype" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlSlotType"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type" ValidationGroup="timetable">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-3 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date Range</label>
                                                    </div>
                                                    <div id="picker" class="form-control" tabindex="3">
                                                        <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                    </div>
                                                </div>
                                                <%--  <div class="col-lg-8 col-md-6 col-12">
                                                    <div class="form-group text-right mt-3">
                                                        <button type="button" class="btn btn-outline-primary addMoreSchedules">
                                                            <i class="far fa-plus tippy " data-tippy-content="Add Schedule"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <div class="schedules-container"></div>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-12 box box-primary">
                                            <div class="sub-heading">
                                                <h5>Time Table Slots</h5>
                                            </div>
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Day</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAllDay" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="timeslot" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <%--  <asp:ListItem Value="1">Monday</asp:ListItem>
                                                        <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                                        <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                                        <asp:ListItem Value="4">Thursday</asp:ListItem>
                                                        <asp:ListItem Value="5">Friday</asp:ListItem>
                                                        <asp:ListItem Value="6">Saturday</asp:ListItem>
                                                        <asp:ListItem Value="7">Sunday</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Time Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTimeSlot" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlTimeSlot_SelectedIndexChanged"
                                                        ValidationGroup="timeslot" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <label>Room</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    </div>
                                                    <br />
                                                    <asp:Button ID="btnAddTimeSlot" runat="server" Text="Add" ValidationGroup="timeslot" OnClick="btnAddTimeSlot_Click" CssClass="btn btn-primary"></asp:Button>
                                                    <asp:Button ID="btnClearTimeSlot" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClearTimeSlot_Click" CssClass="btn btn-warning"></asp:Button>

                                                </div>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="timeslot"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <div class="col-12">
                                                    <asp:Panel ID="divTimeSlot" runat="server">
                                                        <asp:ListView ID="lvTimeSlotDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center">Edit
                                                                            </th>
                                                                            <th style="text-align: center">Delete
                                                                            </th>
                                                                            <th>Day
                                                                            </th>
                                                                            <th>Time Slot
                                                                            </th>
                                                                            <th>Room
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnEditTimeSlotDetail" runat="server" OnClick="btnEditTimeSlotDetail_Click"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("SRNO")%>' />
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnDeleteTimeSlotDetail" runat="server" OnClick="btnDeleteTimeSlotDetail_Click"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.png" ToolTip="Delete Record"
                                                                            OnClientClick="return confirm('Do You Want To Delete Time Slot Entry ?');" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DAYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SLOTNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ROOMNAME")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnSubmitTimeTable" runat="server" Text="Submit" ValidationGroup="timetable" OnClick="btnSubmitTimeTable_Click" CssClass="btn btn-primary"></asp:Button>
                                            <asp:Button ID="btnCancelTimeTable" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelTimeTable_Click" CssClass="btn btn-warning"></asp:Button>
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="timetable"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12">

                                            <asp:Panel ID="Panel2" runat="server">

                                                <asp:ListView ID="lvGlobalTimeTable" runat="server" OnItemDataBound="lvGlobalTimeTable_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading" id="div3" runat="server">
                                                            <h5>Time Table List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <td style="text-align: center; width: 5%">
                                                                    Edit
                                                                    </th>
                                                                    <td style="text-align: center; width: 5%">
                                                                    Show
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">
                                                                        <%--<asp:Label ID="lblDYddlColgScheme_Tab2" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        Session
                                                                    </th>
                                                                    <th style="text-align: center; width: 45%">
                                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">Section
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">Slot Type
                                                                    </th>
                                                                    <th style="text-align: center; width: 15%">Start - End Date
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>

                                                    </LayoutTemplate>

                                                    <ItemTemplate>
                                                        <table class="table table-hover table-bordered mb-0">
                                                            <tr id="MAIN" runat="server" class="col-md-12">
                                                                <td>
                                                                    <tr>
                                                                        <td style="text-align: center; width: 5%">
                                                                            <asp:ImageButton ID="btnEditTimeTable" runat="server" OnClick="btnEditTimeTable_Click"
                                                                                CommandArgument='<%# Eval("FACULTYNO") %>' AlternateText='<%# Eval("COURSENO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("ADDITIONALFALG")%>' CommandName='<%# Eval("STARTENDDATE")%>' />
                                                                            <asp:HiddenField ID="hdfalternateflag" runat="server" Value='<%# Eval("ADDITIONALFALG") %>' />
                                                                        </td>
                                                                        <td style="text-align: center; width: 5%">
                                                                            <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SESSION_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 45%">
                                                                            <%# Eval("COURSE_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SECTIONNAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SLOTTYPE_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 15%">
                                                                            <%# Eval("STARTENDDATE")%>
                                                                            <asp:HiddenField ID="hdfStartEndDate" runat="server" Value='<%# Eval("STARTENDDATE") %>' />
                                                                        </td>

                                                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                            Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                            ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                        </ajaxToolKit:CollapsiblePanelExtender>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">

                                                                <asp:ListView ID="lvDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Day
                                                                                        </th>
                                                                                        <th>Time Slot
                                                                                        </th>
                                                                                        <th>Room
                                                                                        </th>


                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>

                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("DAYNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SLOTNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ROOMNAME")%>
                                                                            </td>


                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvGlobalTimeTable" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_6">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updCancelTimeTable"
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
                                <asp:UpdatePanel ID="updCancelTimeTable" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession_Tab5" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionCancelTT" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSessionCancelTT_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSessionCancelTT"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Canceltimetable"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCancelTTSection" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        data-select2-enable="true" OnSelectedIndexChanged="ddlCancelTTSection_SelectedIndexChanged"
                                                        ValidationGroup="Canceltimetable" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlCancelTTSection"
                                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Canceltimetable"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Course</label>--%>
                                                        <asp:Label ID="lblDYddlCourse_Tab5" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourseCancelTT" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlCourseCancelTT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="Canceltimetable">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Slot Type</label>--%>
                                                        <asp:Label ID="lblDYddlSlotType_Tab5" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlSlotTypeCancelTT" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                        TabIndex="10" CssClass="form-control slottype" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlSlotTypeCancelTT"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type" ValidationGroup="Canceltimetable">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Time Table Start Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtCancelTTStartDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtCancelTTStartDate" runat="server" ValidationGroup="Canceltimetable" TabIndex="5" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtCancelTTStartDate" PopupButtonID="txtCancelTTStartDate1" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtCancelTTStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeStartDate"
                                                            ControlToValidate="txtCancelTTStartDate" EmptyValueMessage="Please Enter Time Table Start Date" IsValidEmpty="false"
                                                            InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                            TooltipMessage="Please Enter Time Table Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="Canceltimetable" SetFocusOnError="True" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCancelTTStartDate"
                                                            Display="None" SetFocusOnError="True"
                                                            ValidationGroup="Canceltimetable" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Time Table End Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtCancelTTEndDate1" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtCancelTTEndDate" runat="server" ValidationGroup="Canceltimetable" TabIndex="6" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtCancelTTEndDate" PopupButtonID="txtCancelTTEndDate1" />

                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtCancelTTEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeEndDate"
                                                            ControlToValidate="txtCancelTTEndDate" EmptyValueMessage="Please Enter Time Table End Date"
                                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                            TooltipMessage="Please Enter Time Table End Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Canceltimetable" SetFocusOnError="True" />

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" SetFocusOnError="True"
                                                            ControlToValidate="txtCancelTTEndDate" Display="None"
                                                            ValidationGroup="Canceltimetable" />
                                                    </div>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Cancellation Type</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoCancelType" runat="server" OnSelectedIndexChanged="rdoCancelType_SelectedIndexChanged"
                                                        AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="12">
                                                        <asp:ListItem Selected="True" Value="0">Time Table &nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="1">Attendance &nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2">Both &nbsp;</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Remark </label>
                                                    </div>
                                                    <asp:TextBox ID="txtCancelRemark" runat="server" CssClass="form-control" TabIndex="12" TextMode="MultiLine" MaxLength="128" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShowCancelTT" runat="server" Text="Show" ValidationGroup="Canceltimetable" OnClick="btnShowCancelTT_Click" CssClass="btn btn-primary"></asp:Button>
                                            <%--<asp:Button ID="btnSubmitCancelTT" runat="server" Text="Submit" ValidationGroup="timetable" OnClick="btnSubmitTimeTable_Click" CssClass="btn btn-primary"></asp:Button>--%>
                                            <asp:Button ID="btnCancelTT" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelTT_Click" CssClass="btn btn-warning"></asp:Button>
                                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="Canceltimetable"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12">

                                            <asp:Panel ID="Panel4" runat="server">

                                                <asp:ListView ID="lvCancelTimeTable" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading">
                                                                <h5>Datewise Time Table</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab-le1">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Time Table Date</th>
                                                                        <th>Day</th>
                                                                        <th>Slot Name</th>
                                                                        <th>Attendance Status</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("TIME_TABLE_DATE")%></td>
                                                            <td><%#Eval("DAY_NAME")%></td>
                                                            <td><%#Eval("SLOTNAME")%></td>
                                                            <td><%#Eval("ATT_STATUS")%></td>
                                                            <td>
                                                                <%-- <asp:Button ID="btnInActiveCancelTT" Enabled='<%# (Convert.ToInt32(Eval("ATT_NO") ) == 0 ?  true : false )%>' runat="server" Text='In-Active' CssClass="btn btn-warning"
                                                                    OnClick="btnInActiveCancelTT_Click" OnClientClick="return confirm('Do you want to In-Active the record ? ');" CommandArgument='<%# Eval("TIME_TABLE_DATE")%>' ToolTip='<%# Eval("SLOTNO")%>' />--%>
                                                                <asp:Button ID="btnInActiveCancelTT" runat="server" Text='In-Active' CssClass="btn btn-warning"
                                                                    OnClick="btnInActiveCancelTT_Click" OnClientClick="return confirm('Do you want to In-Active the record ? ');" CommandArgument='<%# Eval("TIME_TABLE_DATE")%>' ToolTip='<%# Eval("SLOTNO")%>' />
                                                                <asp:HiddenField ID="hdnAttno" runat="server" Value='<%# Convert.ToInt32(Eval("ATT_NO") ) %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_7">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="updReviseTimeTable"
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
                                <asp:UpdatePanel ID="updReviseTimeTable" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession_Tab6" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionRevisedTimeTable" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSessionRevisedTimeTable_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlSessionRevisedTimeTable"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="timetable6"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRevisedTTSection" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        data-select2-enable="true" OnSelectedIndexChanged="ddlRevisedTTSection_SelectedIndexChanged"
                                                        ValidationGroup="timetable6" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlRevisedTTSection"
                                                        Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="timetable6"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Course</label>--%>
                                                        <asp:Label ID="lblDYddlCourse6" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjectRevisedTimetable" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="teacherallot6" OnSelectedIndexChanged="ddlSubjectRevisedTimetable_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSubjectRevisedTimetable"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="timetable6">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Slot Type</label>--%>
                                                        <asp:Label ID="lblDYddlSlotType6" runat="server" Font-Bold="true"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlRevisedSlotType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlRevisedSlotType_SelectedIndexChanged"
                                                        TabIndex="10" CssClass="form-control slottype" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlRevisedSlotType"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type" ValidationGroup="timetable6">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Existing Dates </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExistingDates" runat="server" TabIndex="10" AppendDataBoundItems="True" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlExistingDates_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlExistingDates"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Existing Dates" ValidationGroup="timetable6">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start Date  </label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgStartDate" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtRevisedStartDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                            AutoPostBack="true" OnTextChanged="txtRevisedStartDate_TextChanged" data-mask="" Style="z-index: 0" TabIndex="11" />
                                                        <%-- onchange="test()" --%>
                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtRevisedStartDate"
                                                            Display="None" ErrorMessage="Please Enter Start Date" ValidationGroup="timetable6">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtRevisedStartDate" PopupButtonID="imgStartDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtRevisedStartDate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" EmptyValueMessage="Please Enter Date"
                                                            ControlExtender="meeStartDate" ControlToValidate="txtRevisedStartDate" IsValidEmpty="true"
                                                            InvalidValueMessage="Start Date is Invalid!" Display="None" TooltipMessage="Input a Start Date"
                                                            ErrorMessage="Please Enter Start Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>End Date </label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgEndDate" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtRevisedEndDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                            AutoPostBack="true" OnTextChanged="txtRevisedEndDate_TextChanged" data-mask="" Style="z-index: 0" TabIndex="12" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtRevisedEndDate"
                                                            Display="None" ErrorMessage="Please Enter End Date" ValidationGroup="timetable6">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtRevisedEndDate" PopupButtonID="imgEndDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtRevisedEndDate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter End Date" Visible="false"
                                                            ControlExtender="meeEndDate" ControlToValidate="txtRevisedEndDate" IsValidEmpty="true"
                                                            InvalidValueMessage="End Date is Invalid!" Display="None" TooltipMessage="Input a End Date"
                                                            ErrorMessage="Please Enter End Date." EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Remark </label>
                                                    </div>
                                                    <asp:TextBox ID="txtRevisedRemark" runat="server" CssClass="form-control" TabIndex="12" TextMode="MultiLine" MaxLength="128" />
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 box box-primary">
                                            <div class="sub-heading">
                                                <h5>Revised Time Table Slots</h5>
                                            </div>
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Day</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRevisedAllDay" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        ValidationGroup="timeslot6" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <%--  <asp:ListItem Value="1">Monday</asp:ListItem>
                                                        <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                                        <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                                        <asp:ListItem Value="4">Thursday</asp:ListItem>
                                                        <asp:ListItem Value="5">Friday</asp:ListItem>
                                                        <asp:ListItem Value="6">Saturday</asp:ListItem>
                                                        <asp:ListItem Value="7">Sunday</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Time Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRevisedTimeSlot" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlRevisedTimeSlot_SelectedIndexChanged"
                                                        ValidationGroup="timeslot6" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <label>Room</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRevisedRoom" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    </div>
                                                    <br />
                                                    <asp:Button ID="btnRevisedAddTimeSlot" runat="server" Text="Add" ValidationGroup="timeslot6" OnClick="btnRevisedAddTimeSlot_Click" CssClass="btn btn-primary"></asp:Button>
                                                    <asp:Button ID="btnRevisedClearTimeSlot" runat="server" CausesValidation="false" Text="Clear" OnClick="btnRevisedClearTimeSlot_Click" CssClass="btn btn-warning"></asp:Button>

                                                </div>
                                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="timeslot6"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <div class="col-12">
                                                    <asp:Panel ID="divRevisedTimeSlot" runat="server">
                                                        <asp:ListView ID="lvRevisedTimeSlotDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center">Edit
                                                                            </th>
                                                                            <th style="text-align: center">Delete
                                                                            </th>
                                                                            <th>Day
                                                                            </th>
                                                                            <th>Time Slot
                                                                            </th>
                                                                            <th>Room
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnEditRevisedTimeSlotDetail" runat="server" OnClick="btnEditRevisedTimeSlotDetail_Click"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("SRNO")%>' />
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnDeleteRevisedTimeSlotDetail" runat="server" OnClick="btnDeleteRevisedTimeSlotDetail_Click"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.png" ToolTip="Delete Record"
                                                                            OnClientClick="return confirm('Do You Want To Delete Time Slot Entry ?');" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DAYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SLOTNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ROOMNAME")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnSubmitRevisedTimeTable" runat="server" Text="Submit" ValidationGroup="timetable6" OnClick="btnSubmitRevisedTimeTable_Click" CssClass="btn btn-primary"></asp:Button>
                                            <asp:Button ID="btnCancelRevisedTimeTable" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelRevisedTimeTable_Click" CssClass="btn btn-warning"></asp:Button>

                                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="timetable6"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12">

                                            <asp:Panel ID="pnlRevisedTT" runat="server">

                                                <asp:ListView ID="lvGlobalRevisedTimeTable" runat="server" OnItemDataBound="lvGlobalRevisedTimeTable_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading" id="div3" runat="server">
                                                            <h5>Revised Time Table List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <%-- <td style="text-align: center; width: 5%">
                                                                    Edit
                                                                    </th>--%>
                                                                    <td style="text-align: center; width: 5%">
                                                                    Show
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">
                                                                        <%--<asp:Label ID="lblDYddlColgScheme_Tab2" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        Session
                                                                    </th>
                                                                    <th style="text-align: center; width: 45%">
                                                                        <asp:Label ID="lblDYtxtCourseName6" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">Section
                                                                    </th>
                                                                    <th style="text-align: center; width: 10%">Slot Type
                                                                    </th>
                                                                    <th style="text-align: center; width: 15%">Start - End Date
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>

                                                    </LayoutTemplate>

                                                    <ItemTemplate>
                                                        <table class="table table-hover table-bordered mb-0">
                                                            <tr id="MAIN6" runat="server" class="col-md-12">
                                                                <td>
                                                                    <tr>
                                                                        <%-- <td style="text-align: center; width: 5%">
                                                                            <asp:ImageButton ID="btnEditRevisedTimeTable" runat="server" OnClick="btnEditRevisedTimeTable_Click"
                                                                                CommandArgument='<%# Eval("FACULTYNO") %>' AlternateText='<%# Eval("COURSENO") %>' ImageUrl="~/images/edit1.gif" ToolTip='<%# Eval("ADDITIONALFALG")%>' CommandName='<%# Eval("STARTENDDATE")%>' />
                                                                           
                                                                        </td>--%>
                                                                        <td style="text-align: center; width: 5%">
                                                                            <asp:HiddenField ID="hdfalternateflagRevised" runat="server" Value='<%# Eval("ADDITIONALFALG") %>' />
                                                                            <asp:HiddenField ID="hdfRevisedFacultyNo" runat="server" Value='<%# Eval("FACULTYNO") %>' />
                                                                            <asp:HiddenField ID="hdfRevisedCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                            <asp:HiddenField ID="hdfRevisedStartEndDate" runat="server" Value='<%# Eval("STARTENDDATE") %>' />
                                                                            <asp:Panel ID="pnlDetailsRevised" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SESSION_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 45%">
                                                                            <%# Eval("COURSE_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SECTIONNAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 10%">
                                                                            <%# Eval("SLOTTYPE_NAME")%>
                                                                        </td>
                                                                        <td style="white-space: normal; width: 15%">
                                                                            <%# Eval("STARTENDDATE")%>
                                                                            <asp:HiddenField ID="hdfStartEndDateRevised" runat="server" Value='<%# Eval("STARTENDDATE") %>' />
                                                                        </td>

                                                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt26" runat="server" CollapseControlID="pnlDetailsRevised"
                                                                            Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetailsRevised"
                                                                            ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetailsRevised">
                                                                        </ajaxToolKit:CollapsiblePanelExtender>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlShowCDetailsRevised" runat="server" CssClass="collapsePanel">

                                                                <asp:ListView ID="lvDetailsRevised" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Day
                                                                                        </th>
                                                                                        <th>Time Slot
                                                                                        </th>
                                                                                        <th>Room
                                                                                        </th>


                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>

                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("DAYNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SLOTNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ROOMNAME")%>
                                                                            </td>


                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_8">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="updReport"
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
                                <asp:UpdatePanel ID="updReport" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlSession_Tab7" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionReport" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSessionReport"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report1"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlSessionReport"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="excelrpt"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSlotType_Tab6" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSlotTypeReport" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="15" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSlotTypeReport_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlSlotTypeReport"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type" ValidationGroup="report1">
                                                    </asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <label>Existing Dates </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExistingDatesReport" runat="server" TabIndex="10" AppendDataBoundItems="True" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlExistingDatesReport_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="FromDate" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDateReport" runat="server" TabIndex="16" CssClass="form-control pull-right"
                                                            placeholder="From Date" ToolTip="Please Select From Date" />

                                                        <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDateReport" PopupButtonID="FromDate" Enabled="True">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtFromDateReport" Enabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                            ControlToValidate="txtFromDateReport" Display="None" EmptyValueMessage="Please Enter From Date"
                                                            ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                            IsValidEmpty="false" SetFocusOnError="true" />

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtFromDateReport"
                                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="report1"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtFromDateReport"
                                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="excelrpt"></asp:RequiredFieldValidator>

                                                    </div>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="ToDate" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtTodateReport" runat="server" TabIndex="17" placeholder="To Date"
                                                            ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                                        <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodateReport" PopupButtonID="ToDate" Enabled="True">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodateReport" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtTodateReport" Display="None" EmptyValueMessage="Please Enter To Date"
                                                            ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="report1" />

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtTodateReport"
                                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="report1"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtTodateReport"
                                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="excelrpt"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnTTReportFormate1" runat="server" Text="Time Table Report" ValidationGroup="report1" OnClick="btnTTReportFormate1_Click" CssClass="btn btn-primary"></asp:Button>
                                                <asp:Button ID="btnReport" runat="server" Text="Classroom Allocation(Excel)" TabIndex="21" ValidationGroup="excelrpt" OnClick="btnReport_Click" CssClass="btn btn-info"></asp:Button>
                                                <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelReport_Click" CssClass="btn btn-warning"></asp:Button>
                                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="report1"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="excelrpt"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function tab() {
            $('#tab8').tab('show')
        };

        function SetStatSms(val) {
            $('[id*=rdSMSYes]').prop('checked', val);
        }
        function SetStatEmail(val) {
            $('[id*=rdEmailYes]').prop('checked', val);
        }
        function SetStatCourse(val) {
            $('#rbCRegBefore').prop('checked', val);
        }
        function SetStatTeaching(val) {
            $('#rdTeachYes').prop('checked', val);
        }
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function validate() {
          
            var schemeno = document.getElementById("ctl00_ContentPlaceHolder1_ddlCollegeSchemeAttConfig").value;
            var sessionno = document.getElementById("ctl00_ContentPlaceHolder1_ddlSessionAttConfig").value;
            var semester = document.getElementById("ctl00_ContentPlaceHolder1_lstSemesterAttConfig").value;
            var attLock = document.getElementById("ctl00_ContentPlaceHolder1_txtAttLockDay").value;
            var AttStartDate = $("#ctl00_ContentPlaceHolder1_txtStartDate").val();
            var AttEndDtae = document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value;
            //if (school == "0") {
            //    alert("Please Select School/Institute Name.");
            //    return false;
            //}
            //if (schemeno == "" || schemeno == 0) {
            //    alert("Please Select College & Scheme.");
            //    return false;
            //}
            if (sessionno == "" || sessionno == 0) {
                alert("Please Select Session.");
                return false;
            }
            //if (semester == "") {
            //    alert("Please Select at least one Semester.");
            //    return false;
            //}
            if (AttStartDate == "" || AttStartDate == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (AttEndDtae == "" || AttEndDtae == "DD/MM/YYYY") {
                alert("Please Enter Attendance End Date.");
                return false;
            }
            if (attLock == "") {
                alert("Please Select Attendance Lock By Day.");
                return false;
            }


            $('#hfdSms').val($('#rdSMSYes').prop('checked'));
            $('#hfdEmail').val($('#rdEmailYes').prop('checked'));
            $('#hfdCourse').val($('#rbCRegBefore').prop('checked'));
            $('#hfdTeaching').val($('#rdTeachYes').prop('checked'));
            $('#hfdActive').val($('#rdActive').prop('checked'));
        }
        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSunmitAttConfig').click(function () {
                    validate();
                });
            });
        
        });
    </script>
    <script>
        var allData = [];
        var addMoreSchedulesBtn;
        var scheduleContainer;

        $(document).ready(function(){

            scheduleContainer = document.querySelector('.schedules-container');
            
            addMoreSchedulesBtn = document.querySelector('.addMoreSchedules');     
            addMoreSchedulesBtn.addEventListener("click",()=>{
                makeMoreSchedules(scheduleContainer)
            });
            makeMoreSchedules(scheduleContainer);
    
            //initializeCalendar("month");

            //END 
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function(){

                scheduleContainer = document.querySelector('.schedules-container');
               
                addMoreSchedulesBtn = document.querySelector('.addMoreSchedules');     
                addMoreSchedulesBtn.addEventListener("click",()=>{
                    makeMoreSchedules(scheduleContainer)
                });
                makeMoreSchedules(scheduleContainer);
    
                //initializeCalendar("month");

                //END 
            });
        });

        function makeMoreSchedules(scheduleContainer) {
           
            const newElement = document.createElement('div');
            newElement.id = "divsubcard"
            
            newElement.className = "subCard mb-2 p-0 box box-primary"
            newElement.innerHTML = `
                <div class="subCard-header">
                    <div class="d-flex align-items-center col-md-6 col-lg-3 col-9">
                        <div class="label-dynamic mr-2"><b><sup>*</sup>Day</b></div>
                     
                         <asp:DropDownList ID="ddlDay" runat="server" class="form-control form-select">
                         <asp:ListItem value="0">Please Select</asp:ListItem>
                         <asp:ListItem value="1">Monday</asp:ListItem>
                         <asp:ListItem value="2">Tuesday</asp:ListItem>
                         <asp:ListItem value="3">Wednesday</asp:ListItem>
                         <asp:ListItem value="4">Thursday</asp:ListItem>
                         <asp:ListItem value="5">Friday</asp:ListItem>
                         <asp:ListItem value="6">Saturday</asp:ListItem>
                         <asp:ListItem value="7">Sunday</asp:ListItem>
                        
                     </asp:DropDownList>
                    </div>
                </div>
                <div class="subCard-body pr-3 pl-3 pb-3 d-flex flex-wrap mt-3">
                    <div class="col-lg-2 col-md-2 col-12 mt-0 mb-1 border rounded p-3 show-delete-on-hover" >
                        ${makeScheduleInternal()}  
            </div>         
            ${makeAddMoreSchedule()}
            </div>
            `;
            const addMoreSubSchedulesBtn = newElement.querySelector('.addMoreSubScheduleContent');
            const insertBeforeNode = addMoreSubSchedulesBtn.parentElement;

            const deleteBtn = newElement.querySelector('.delete-icon');
            deleteBtn.addEventListener('click',()=>{
                deleteBtn.closest('.show-delete-on-hover').remove();
        });

        scheduleContainer.appendChild(newElement);

        addMoreSubSchedulesBtn.addEventListener('click',()=>{
            // insertBefore
            const newSubElement = document.createElement('div');
        newSubElement.className = "col-lg-2 col-md-2 col-12 mb-1 mt-0 border rounded p-3 show-delete-on-hover";
        newSubElement.innerHTML = makeScheduleInternal();
        const deleteBtn = newSubElement.querySelector('.delete-icon');
        deleteBtn.addEventListener('click',()=>{
            deleteBtn.closest('.show-delete-on-hover').remove();
        });
        newElement.querySelector('.subCard-body').insertBefore(newSubElement,insertBeforeNode);
        });

    
        }


        function makeAddMoreSchedule(){
            return `
            <div class="col-lg-2 col-md-2 col-12 mt-0 mb-1 border rounded p-2 d-flex align-items-center justify-content-center" style="min-height:122px;">
                <i class="far fa-plus-square edit-icon addMoreSubScheduleContent tippy font-18" data-tippy-content="Add Another Class"></i>
            </div>
          `;
        }

        function makeScheduleInternal(){
            const newElement  = `   
                <i class="fas fa-times delete-icon font-16 float-right text-danger"></i>
                <div class="label-dynamic">
                    <sup>* </sup>
                    <label>Time Slot</label>
                </div>
                
                     <asp:DropDownList ID="ddlTime" runat="server" class="form-control mb-2 form-select form-control-sm py-0">
                         <asp:ListItem value="0">Please Select</asp:ListItem>
                       
                     </asp:DropDownList>
              

                <div class="label-dynamic mt-3">
                    <sup>* </sup>
                    <label>Room Number</label>
                </div>
                   <asp:DropDownList ID="ddlRoomNo" runat="server" class="form-control mb-2 form-select form-control-sm py-0">
                         <asp:ListItem value="0">Please Select</asp:ListItem>
                       
                     </asp:DropDownList>
    
            `;

            return newElement;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function(){
            jQuery("[id*=ctl00_ContentPlaceHolder1_ddlSlotType]").change(function (event) 
            { 
                //alert('a');
        
                $.ajax({  
                    type: "POST", 
                    url: '<%=Page.ResolveUrl("~/Global_Offered_Courses.aspx/FillTimeSlotDropdown")%>',
                    dataType: "json", 
                    contentType: "application/json", 
                    success: function (res)  
                    {  
                        $.each(res.d, function (data, value) {  
  
                            alert('b');
                            //jQuery("[id*=ctl00_ContentPlaceHolder1_ddlTime]").append($("<option></option>").val(value.Slotno).html(value.Slotname));  
                        })  
                    }  
  
       
                });
            });
    </script>

    <script>
            function validateAssign() {
                debugger;

                var college_id = $("[id*=ctl00_ContentPlaceHolder1_ddlCollege]").val();
                if (college_id == 0) {
                    alert('Please Select College & Scheme', 'Warning!');
                    $("[id*=ctl00_ContentPlaceHolder1_ddlCollege]").focus();
                    return false;
                }

                var ddlSemester = $("[id*=ctl00_ContentPlaceHolder1_ddlToSemester]").val(); //select2 - ctl00_ContentPlaceHolder1_ddlToSemester - container
                if (ddlSemester == 0) {
                    alert('Please Select  Offered To Semester', 'Warning!');
                    $(ddlSemester).focus();
                    return false;
                }


                var numberOfChecked = $('[id*=tblCourseList] input:chkSelect:checked').length;
                if (numberOfChecked == 0) {
                    alert('Please offer atleast one course.');
                    return false;
                }
                else {
                    return true;
                }
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnAd').click(function () {
                        validateAssign();

                        //$('#hfdActive').val($('#rdActive').prop('checked'));
                    });
                });
            });

          
    </script>



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

    <script type="text/javascript" language="javascript">
            function totAllSubjects(headchk) {
                var frm = document.forms[0]
                var count = 0;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    // alert("check1");
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            e.checked = true;
                            //count++;
                        }
                        else
                            e.checked = false;
                    }
                }
                if (headchk.checked == true) {
                    count = $('[id*=tblStudents] td').closest("tr").length;
                }
                document.getElementById('<%=txtTotStud.ClientID%>').value = count;
            }


    </script>
    <script>
            function totStud() {
                var numberToChecked = $('[id*=tblStudents] td input:checkbox:checked').length;
                document.getElementById('<%=txtTotStud.ClientID%>').value = numberToChecked;

            }
    </script>

    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
    <script type="text/javascript">
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    $('#picker').daterangepicker({
                        startDate: moment().subtract(00, 'days'),
                        endDate: moment(),
                        locale: {
                            format: 'DD MMM, YYYY'
                        },
                        //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                        ranges: {
                            //                    'Today': [moment(), moment()],
                            //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                            //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                            //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                            //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                            //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                        },
               
                    },
                function (start, end) {
                    debugger
                    $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                    document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                });

                    $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                    document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                });
            });

    </script>



    <script>
            function Setdate(date) {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $(document).ready(function () {
                        debugger;
                        var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                        var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");
                        //$('#date').html(date);
                        $('#date').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
                        document.getElementById('<%=hdnDate.ClientID%>').value = date;
                        //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                        $('#picker').daterangepicker({
                            startDate: startDate.format("MM/DD/YYYY"),
                            endDate: endtDate.format("MM/DD/YYYY"),
                            ranges: {
                            },
                        },
                function (start, end) {
                    debugger
                    $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                    document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                });

                    });
                });
    };
    </script>

</asp:Content>

