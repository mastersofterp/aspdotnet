<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamDate.aspx.cs" Inherits="ACADEMIC_MASTERS_ExamDate" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }

        #ctl00_ContentPlaceHolder1_Panel2 th, #ctl00_ContentPlaceHolder1_Panel2 td {
            width: 200px !important;
        }

        #ctl00_ContentPlaceHolder1_Panel2 .select2.select2-container, #ctl00_ContentPlaceHolder1_Panel2 .btn-group, #ctl00_ContentPlaceHolder1_Panel2 .input-group {
            width: 180px !important;
        }

        #ctl00_ContentPlaceHolder1_Panel3 .select2.select2-container, #ctl00_ContentPlaceHolder1_Panel3 .btn-group {
            width: 220px !important;
        }

        #ctl00_ContentPlaceHolder1_lvtimetable_ctrl0_txtExamDate1 {
            width: 100px !important;
        }

        /*.tbl-reponsive {
            width: 100%;
            max-height: 440px;
            overflow-x: scroll;
            overflow: scroll;
            border-top: 1px solid #e5e5e5;
        }*/
        .tbl-reponsive {
            width: 100%;
            max-height: 400px;
            overflow: scroll;
        }



        @media (max-width: 1200px) {
            .tbl-panel2 {
                width: 100%;
                overflow-x: scroll;
            }
        }

        @media (max-width: 1200px) {
            .tbl-panel3 {
                width: 100%;
                overflow-x: scroll;
            }
        }
    </style>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExamdate"
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EXAM DAYS MANAGEMENT</h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Regular Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Global Elective/Value Added Time Table</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">Common Course</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
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
                                <asp:UpdatePanel ID="updExamdate" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3" id="divbody" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Institute/Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" TabIndex="1" runat="server" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfccolege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfccollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfccollege1" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Clash"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="rfvsession1" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="rfvsession3" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="Excel" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="rfvsession4" runat="server" ControlToValidate="ddlSession"
                                                        ValidationGroup="Clash" Display="None" ErrorMessage="Please Select session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" TabIndex="1" data-select2-enable="true" runat="server" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" TabIndex="1" runat="server" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select Semester"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvSem1" runat="server" ControlToValidate="ddlSemester"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select Semester"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvsem2" runat="server" ControlToValidate="ddlSemester"
                                                        ValidationGroup="Clash" Display="None" ErrorMessage="Please Select Semester"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjecttype" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlSubjecttype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubjecttype"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfcsubject" runat="server" ControlToValidate="ddlSubjecttype"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvsubject1" runat="server" ControlToValidate="ddlSubjecttype"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Clash"></asp:RequiredFieldValidator>
                                                </div>
                                                <div id="divsection" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                                        TabIndex="1" Height="16px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Section"
                                            SetFocusOnError="true" InitialValue="0" />--%>
                                                </div>

                                                <div id="divbatch" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged" AutoPostBack="True" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Section"
                                            SetFocusOnError="true" InitialValue="0" />--%>
                                                </div>




                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged1" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvExam1" runat="server" ControlToValidate="ddlExamName"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>

                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Sub Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlsubexamname" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlsubexamname_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvsubexam" runat="server" ControlToValidate="ddlsubexamname"
                                                        ValidationGroup="Show" Display="None" ErrorMessage="Please Select SubExam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="rfvsubexam1" runat="server" ControlToValidate="ddlsubexamname"
                                                        ValidationGroup="submit" Display="None" ErrorMessage="Please Select SubExam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Day Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                                        ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading"><i class="fa fa-star" aria-hidden="true"></i>Note</h5>
                                                        <p>
                                                            <span>1) Create SchemeWise Time Table.</span><br />
                                                            <span>2) For Sessionwise Time Table Use Global Elective Time Table.</span><br />
                                                            <span>3) Once Result Publish Time Table can not be Created/Modified.</span>
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="lblStudCnt" runat="server" Style="color: #990000"></asp:Label>
                                                    <asp:Panel ID="PanelLvDate" runat="server" ScrollBars="Auto">
                                                    </asp:Panel>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="divbuttons" runat="server">
                                            <asp:Button ID="btnShow" runat="server" Text="Show Courses" OnClick="btnShow_Click" CssClass="btn btn-primary" ValidationGroup="Show" TabIndex="1" />

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="1" />

                                            <asp:Button ID="btnViewLogin" runat="server" Text="View On Student Login" CssClass="btn btn-primary" OnClick="btnViewLogin_Click" ValidationGroup="Show" TabIndex="1" Visible="false" />

                                            <asp:Button ID="btnExamAttendence" runat="server" Text="Exam Attendence Report" CssClass="btn btn-info" OnClick="btnExamAttendence_Click" ValidationGroup="Show" TabIndex="1" />

                                            <asp:Button ID="btnReport" runat="server" Text="Exam Time Table Report" OnClick="btnReport_Click" ValidationGroup="Show" CssClass="btn btn-info" TabIndex="1" />

                                            <asp:Button ID="btnBranchWiserpt" runat="server" Text="Branchwise Time Table Report" OnClick="btnBranchWiserpt_Click" ValidationGroup="Show" CssClass="btn btn-info" TabIndex="1" />

                                            <asp:Button ID="btnExcel" runat="server" Text="Exam Time Table Excel" OnClick="btnExcel_Click" CssClass="btn btn-info" TabIndex="1" />
                                            <asp:Button ID="btnClashExcel" Visible="false" ValidationGroup="Clash" runat="server" Text="Exam Clash Excel Report" OnClick="btnClashExcel_Click" CssClass="btn btn-info" TabIndex="1" />

                                              <asp:Button ID="btnCoveringPage" runat="server" Text="Exam Covering Sheet" CssClass="btn btn-info" OnClick="btnCoveringPage_Click" ValidationGroup="Show" TabIndex="1" Visible="false" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />

                                            <asp:ValidationSummary ID="valsum" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="valclash" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Clash" />
                                            <asp:ValidationSummary ID="valexcel" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Excel" />
                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:ListView ID="lvCourse" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Course List</h5>
                                                        </div>
                                                        <div class="tbl-reponsive">
                                                            <table id="ID5" class="table table-striped table-bordered" style="width: 100%">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Select<asp:CheckBox ID="cbHead" runat="server" Text="Select" Visible="false" />
                                                                        </th>
                                                                        <th id="Th1" runat="server">Subject Code - Subject Name </th>
                                                                        <th>Student Count</th>
                                                                        <th>Exam Date </th>
                                                                        <th>Slot </th>
                                                                        <th id="BatchTheory1" style="display: none">Mode of Exam </th>
                                                                        <th>Action.</th>


                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow">
                                                            <td>
                                                                <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("STATUS").ToString() == "True" ? true : false %>' ToolTip='<%#Eval("CCODE")%>' TabIndex="1" />
                                                            </td>
                                                            <%-- <td><%# Eval("SUBJECT_NAME")%></td>--%>
                                                            <%--   <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("COURSENO")%>' ToolTip='<%# Eval("COURSENO")%>' Visible="false"></asp:Label>--%>
                                                            <td>
                                                                <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("SUBJECT_NAME")%>' ToolTip='<%# Eval("COURSENO")%>'></asp:Label>
                                                            </td>
                                                            <td><%# Eval("STUDENTCOUNT")%></td>
                                                            <td>

                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id='<%# "imgExamDate2" + (Container.DataItemIndex + 1) %>'></i>
                                                                    </div>

                                                                    <%--<asp:TextBox ID="txtExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit" ToolTip='<%# Eval("STATUS")%>' Enabled="false" TabIndex="11"/>--%>
                                                                    <asp:TextBox ID="txtExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit" ToolTip='<%# Container.DataItemIndex + 1 %>' Enabled="false" TabIndex="1" onblur="onTextBoxBlur(this);" AutoPostBack="true" OnTextChanged="txtExamDate_TextChanged" />



                                                                    <%--  <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate2" TargetControlID="txtExamDate" Enabled="true" OnClientDateSelectionChanged="checkDate" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                                                    <asp:RequiredFieldValidator ID="rfvExamDate" runat="server" ControlToValidate="txtExamDate" Display="None" ErrorMessage="Please select Exam Date!!" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                                    <%--   <asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("COURSENO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btndelete_Click"  />--%>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged">
                                                                    <%--SelectedValue='<%# Eval("SLOTNAME")%>' --%>
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_slotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                                <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot" Display="None"
                                                                    ErrorMessage="Please Select Slot" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />
                                                            </td>
                                                            <td id="ddlmodeexamhide" style="display: none">
                                                                <asp:DropDownList ID="ddlmodeexam" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1">
                                                                    <%--SelectedValue='<%# Eval("SLOTNAME")%>' --%>
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_modeexam" runat="server" Value='<%# Eval("ModeOfEXAMNO")%>' />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlmodeexam" Display="None"
                                                                    ErrorMessage="Please Select Mode Of Exam" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnEvalDelete" runat="server"
                                                                    ImageUrl="~/images/delete.gif" AlternateText="CANCEL RECORD" ToolTip='<%# Eval("EXDTNO")%>'
                                                                    OnClick="ibtnEvalDelete_Click" OnClientClick="return showConfirm();" CommandArgument='<%# Eval("EXDTNO")%>' TabIndex="1" />
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                    <%--      <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">

                                                <td>
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' />
                                                    <asp:HiddenField ID="hfdIntEx" runat="server" Value='<%# Eval("EXDTNO")%>' />



                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
                                                </td>

                                                <td>
                                                  

                                                    <asp:DropDownList ID="ddlDayNo" runat="server" AppendDataBoundItems="true" Width="110px"
                                                        TabIndex="12" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                      
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdfDay" runat="server" Value='<%# Eval("DAYNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExamDate" runat="server" TabIndex="12" ValidationGroup="submit"
                                                        OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" />
                                                    <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />
                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtExamDate" PopupButtonID="imgExamDate" CssClass="Calendar" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                        MaskType="Date" />
                                                    <asp:HiddenField ID="hdfDate" runat="server" Value='<%# Eval("EXAMDATE")%>' />

                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" Width="148px"
                                                        AutoPostBack="true" TabIndex="12">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdfSlot" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                    <%--  <asp:HiddenField ID="hfdIntEx" runat="server" Value='<%# Eval("SLOTNO")%>' />--%>
                                                </asp:ListView>
                                                <div id="divMsg" runat="Server">
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                        <asp:PostBackTrigger ControlID="btnClashExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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
                                        <div class="col-12 mt-3" id="div1" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession1" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSession1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession1"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSession1"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Session"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Pattern </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlpattern" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlpattern_SelectedIndexChanged" TabIndex="1" data-select2-enable="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvpattern" runat="server" ControlToValidate="ddlpattern"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlpattern"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjecttype1" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlSubjecttype1_SelectedIndexChanged" Height="16px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSubjecttype1"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show2"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSubjecttype1"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamName1" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlExamName1_SelectedIndexChanged" TabIndex="1" data-select2-enable="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlExamName1"
                                                        ValidationGroup="Show2" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlExamName1"
                                                        ValidationGroup="submit1" Display="None" ErrorMessage="Please Select Exam Name"
                                                        SetFocusOnError="true" InitialValue="0" />
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note</h5>
                                                            <p>
                                                                <i class="fa fa-star" aria-hidden="true"></i><span>1) Create Sessionwise Time Table .</span>
                                                                &nbsp <span>2) For SchemeWise Time Table Use Regular Time Table.</span>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- <div class="col-12">
                                                    <asp:Label ID="Label1" runat="server" Style="color: #990000"></asp:Label>
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                    </asp:Panel>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="div2" runat="server">
                                            <asp:Button ID="btnShow1" runat="server" Text="Show Courses" OnClick="btnShow1_Click" CssClass="btn btn-info" ValidationGroup="Show2" TabIndex="1" />
                                            <asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click" CssClass="btn btn-info" TabIndex="1" ValidationGroup="submit1" Enabled="false" />
                                            <asp:Button ID="btnViewLogin1" runat="server" Text="View On Student Login" CssClass="btn btn-info" OnClick="btnViewLogin1_Click" ValidationGroup="submit1" TabIndex="1" Visible="false" />
                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClick="btnCancel1_Click" CssClass="btn btn-warning" TabIndex="1" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="Show2" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit1" />
                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvCourse1" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Course List</h5>
                                                        </div>
                                                        <div class="tbl-reponsive">
                                                            <table id="ID5" class="table table-striped table-bordered" style="width: 100%">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Select<asp:CheckBox ID="cbHead" runat="server" Text="Select" Visible="false" />
                                                                        </th>
                                                                        <th id="Th1" runat="server">Subject Code - Subject Name </th>
                                                                        <th style="display: none">Student Count</th>
                                                                        <th>Exam Date</th>
                                                                        <th>Slot </th>
                                                                        <th id="BatchTheory1" style="display: none">Mode of Exam </th>
                                                                        <th>Action.</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow">
                                                            <td>
                                                                <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("STATUS").ToString() == "True" ? true : false %>' TabIndex="1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("SUBJECT_NAME")%>' ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                                            </td>
                                                            <td style="display: none"><%# Eval("STUDENTCOUNT")%></td>
                                                            <td>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id='<%# "imgExamDate" + (Container.DataItemIndex + 1) %>'></i>
                                                                    </div>

                                                                    <asp:TextBox ID="txtExamDate" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit1" ToolTip='<%# Container.DataItemIndex + 1 %>' TabIndex="1" onblur="onTextBoxBlur(this);" AutoPostBack="true" OnTextChanged="txtExamDate1_TextChanged" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate" TargetControlID="txtExamDate" Enabled="true" OnClientDateSelectionChanged="checkDate" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit1" />
                                                                    <asp:RequiredFieldValidator ID="rfvExamDate" runat="server" ControlToValidate="txtExamDate" Display="None" ErrorMessage="Please select Exam Date!!" ValidationGroup="Submit1"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" OnSelectedIndexChanged="ddlSlot1_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_slotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                                <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot" Display="None"
                                                                    ErrorMessage="Please Select Slot" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit1" />
                                                            </td>
                                                            <td id="ddlmodeexamhide" style="display: none">
                                                                <asp:DropDownList ID="ddlmodeexam" runat="server" AppendDataBoundItems="true" CausesValidation="true" TabIndex="1" Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_modeexam" runat="server" Value='<%# Eval("ModeOfEXAMNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnEvalDelete1" runat="server"
                                                                    ImageUrl="~/images/delete.gif" AlternateText="CANCEL RECORD" ToolTip='<%# Eval("CCODE")%>'
                                                                    OnClick="ibtnEvalDelete1_Click" OnClientClick="return showConfirm();" CommandArgument='<%# Eval("CCODE")%>' TabIndex="13" />

                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updcommon"
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
                                <asp:UpdatePanel ID="updcommon" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3" id="div4" runat="server">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession2" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession2_SelectedIndexChanged" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlsession2" runat="server" ControlToValidate="ddlSession2"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" ValidationGroup="submit2" />
                                                    <asp:RequiredFieldValidator ID="rfv1ddlsession2" runat="server" ControlToValidate="ddlSession2"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" ValidationGroup="show2" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Pattern </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlpattern1" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlpattern1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlpattern1" runat="server" ControlToValidate="ddlpattern1" Display="None"
                                                        ErrorMessage="Please Select Exam Pattern" SetFocusOnError="true" InitialValue="0" ValidationGroup="show2" />
                                                    <asp:RequiredFieldValidator ID="rfv1ddlpattern1" runat="server" ControlToValidate="ddlpattern1" Display="None"
                                                        ErrorMessage="Please Select Exam Pattern" SetFocusOnError="true" InitialValue="0" ValidationGroup="submit2" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjecttype2" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjecttype2_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSubjecttype2" runat="server" ControlToValidate="ddlSubjecttype2" Display="None"
                                                        ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit2" />
                                                    <asp:RequiredFieldValidator ID="rfv1ddlSubjecttype2" runat="server" ControlToValidate="ddlSubjecttype2"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="show2" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamName2" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlExamName2_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvexamshow" runat="server" ControlToValidate="ddlExamName2"
                                                        Display="None" ErrorMessage="Please Select Exam Name" SetFocusOnError="true" InitialValue="0" ValidationGroup="show2" />
                                                    <asp:RequiredFieldValidator ID="rfvexamsumbit" runat="server" ControlToValidate="ddlExamName2"
                                                        Display="None" ErrorMessage="Please Select Exam Name" SetFocusOnError="true" InitialValue="0" ValidationGroup="submit2" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Course Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcoursecat" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="1" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcoursecat_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="div5" runat="server">
                                            <asp:Button ID="btncourse2" runat="server" Text="Show Courses" CssClass="btn btn-info" TabIndex="1" OnClick="btncourse2_Click" ValidationGroup="show2" />
                                            <asp:Button ID="btnsubmit2" runat="server" Text="Submit" CssClass="btn btn-info" TabIndex="1" Enabled="false" OnClick="btnsubmit2_Click" ValidationGroup="submit2" />
                                            <asp:Button ID="btnviewstudlog2" runat="server" Text="View On Student Login" CssClass="btn btn-info" TabIndex="1" Visible="false" OnClick="btnviewstudlog2_Click" />
                                            <asp:Button ID="btncancel2" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="1" OnClick="btncancel2_Click" />
                                            <asp:ValidationSummary ID="valsumcommon" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                ValidationGroup="show2" />
                                            <asp:ValidationSummary ID="valsumcommon1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit2" />
                                        </div>

                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="col-12 mt-4" id="gridrow">
                                                <asp:ListView ID="lvcommoncourse" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="tbl-panel2">
                                                            <div class="sub-heading">
                                                                <h5>Course List</h5>
                                                            </div>
                                                            <table class="table table-bordered table-striped" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th class="d-none">SrNo</th>
                                                                        <th>Course</th>
                                                                        <th>Scheme</th>
                                                                        <th id="thsection">Section</th>
                                                                        <th>Exam Date</th>
                                                                        <th>Exam Slot</th>
                                                                        <th>Action</th>
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
                                                            <td class="d-none">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlcommoncourse" runat="server" CssClass="form-control" data-select2-enable="true" Style="width: 250px;" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlcommoncourse_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Please Select" Value="0"> </asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_course" runat="server" Value='<%# Eval("CCODE")%>' />
                                                                <asp:RequiredFieldValidator ID="rfvcommoncourse" runat="server" ControlToValidate="ddlcommoncourse" Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="true" ValidationGroup="submit2" />
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="ddlschemelist" AppendDataBoundItems="True" SelectionMode="Multiple" runat="server" CssClass="form-control multi-select-demo" Style="width: 250px;" OnSelectedIndexChanged="ddlschemelist_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                <asp:HiddenField ID="hdn_schemeno" runat="server" Value='<%# Eval("SCHEME")%>' />

                                                                <asp:RequiredFieldValidator ID="rfvSchemelist" runat="server" ControlToValidate="ddlschemelist" Display="None"
                                                                    ErrorMessage="Please Select Scheme" SetFocusOnError="true" ValidationGroup="submit2" />
                                                            </td>
                                                            <td id="ddlsectionrecord">
                                                                <asp:ListBox ID="ddlsectionlist" AppendDataBoundItems="True" SelectionMode="Multiple" runat="server" CssClass="form-control multi-select-demo" Style="width: 250px;"></asp:ListBox>
                                                                <asp:HiddenField ID="hdn_section" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                            </td>
                                                            <td>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id='<%# "imgExamDate1" + (Container.DataItemIndex + 1) %>'></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtExamDate1" runat="server" Text='<%# Eval("EXAMDATE")%>' ValidationGroup="submit1" TabIndex="1" OnTextChanged="txtExamDate1_TextChanged1" onblur="onTextBoxBlur(this);" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate1" TargetControlID="txtExamDate1" Enabled="true" OnClientDateSelectionChanged="checkDate" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate1" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate1" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit1" />
                                                                    <asp:RequiredFieldValidator ID="rfvExamDate1" runat="server" ControlToValidate="txtExamDate1" Display="None" ErrorMessage="Please select Exam Date!!" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </td>
                                                            <td style="width: 150px;">
                                                                <asp:DropDownList ID="ddlSlot1" runat="server" AppendDataBoundItems="true" CausesValidation="true" data-select2-enable="true" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdf_slotno1" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                                <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot1" Display="None"
                                                                    ErrorMessage="Please Select Slot" InitialValue="0" SetFocusOnError="true" ValidationGroup="submit2" />
                                                            </td>
                                                            <td style="text-align: center; width: 50px;">
                                                                <asp:ImageButton ID="imgaddcourse" runat="server" OnClick="imgaddcourse_Click" ImageUrl="~/Images/Addblue.png" Style="height: 20px; width: 20px;" Visible="false" />
                                                                <asp:ImageButton ID="imgbtnrowremove" runat="server" OnClick="imgbtnrowremove_Click" ImageUrl="~/Images/delete.png" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel3" runat="server">
                                            <div class="col-12 mt-4" id="gridrow2">
                                                <asp:ListView ID="lvtimetable" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="tbl-panel3">
                                                            <div class="sub-heading">
                                                                <h5>Time Table List</h5>
                                                            </div>
                                                            <div class="tbl-reponsive">
                                                                <table class="table table-striped table-bordered" style="width: 100%" id="tblTim">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Course</th>
                                                                            <th>Scheme</th>
                                                                            <th id="theadsection">Section</th>
                                                                            <th>Exam Date</th>
                                                                            <th>Exam Slot</th>
                                                                            <th>Cancel</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>

                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:ImageButton ID="imgbtnedit" CommandArgument=" <%# Container.DataItemIndex + 1 %>" runat="server" OnClick="imgbtnedit_Click" ImageUrl="~/Images/edit.png" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval("SUBJECT_NAME")%>' TabIndex="1" />
                                                                <asp:HiddenField ID="hdf_course" runat="server" Value='<%# Eval("CCODE")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="ddlschemelist" AppendDataBoundItems="True" SelectionMode="Multiple" runat="server" CssClass="form-control multi-select-demo" disabled="true"></asp:ListBox>
                                                                <asp:HiddenField ID="hdn_schemeno" runat="server" Value='<%# Eval("SCHEME")%>' />
                                                            </td>
                                                            <td id="tbodysection">
                                                                <asp:ListBox ID="ddlsectionlist" AppendDataBoundItems="True" SelectionMode="Multiple" runat="server" CssClass="form-control multi-select-demo" disabled="true"></asp:ListBox>
                                                                <asp:HiddenField ID="hdn_section" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                            </td>
                                                            <td>
                                                                <div class="input-group">
                                                                    <asp:Label ID="txtExamDate1" runat="server" Text='<%# (Eval("EXAMDATE").ToString() != string.Empty) ? (Eval("EXAMDATE", "{0:dd-MMM-yyyy}")) : Eval("EXAMDATE", "{0:dd-MMM-yyyy}")%>' TabIndex="1" />
                                                                </div>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblslotname" runat="server" Text='<%# Eval("SLOTNAME")%>' TabIndex="1" />
                                                                <asp:HiddenField ID="hdf_slotno1" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument=" <%# Container.DataItemIndex + 1 %>" OnClick="imgbtnDelete_Click" ImageUrl="~/Images/delete.png" OnClientClick="return showConfirm();" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" language="javascript">

        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").on("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (cbHead.is(":checked")) {
                        $(this).prop("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("select").removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).prop("disabled", "disabled");
                        $("select", td).prop("disabled", "disabled");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).prop("disabled", "disabled");
                    $("select", td).prop("disabled", "disabled");

                    // $("input[type=text]", td).val('');
                    //  $("select", td).val('');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.prop("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });

        //$(document).ready(function () {
        //    $("#chkAccept").click(function () {
        //        if ($(this).is(":checked")) {
        //            $("#dropdown").prop("disabled", true);
        //        } else {
        //            $("#dropdown").prop("disabled", false);
        //        }
        //    });
        //});
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id*=cbHead]").on("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (cbHead.is(":checked")) {
                        $(this).prop("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("select").removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).prop("disabled", "disabled");
                        $("select", td).prop("disabled", "disabled");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).prop("disabled", "disabled");
                    $("select", td).prop("disabled", "disabled");

                    // $("input[type=text]", td).val('');
                    //  $("select", td).val('');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.prop("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });

    </script>

    <script>
        function showConfirm() {
            var ret = confirm('Do you really want to cancel Time Table?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>


    <script>
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {

                alert("You cannot select a day earlier than today!")
                //    return false;
                //} else {
                //    return true;
                //
            }
        }
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

    <script type="text/javascript">
        function onTextBoxBlur(textBox) {
            // Get the UniqueID of the TextBox within the ListView
            var textBoxUniqueID = textBox.id;
            console.log(textBox.value);
            if (textBox.value !== "__/__/____") {
                // Call the __doPostBack function to initiate a postback
                __doPostBack(textBoxUniqueID, '');
            }
        }
    </script>

</asp:Content>

