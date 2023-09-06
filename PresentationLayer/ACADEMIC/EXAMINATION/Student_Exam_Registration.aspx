<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Exam_Registration.aspx.cs" 
    Inherits="ACADEMIC_EXAMINATION_Student_Exam_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js"></script>
    <script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script src="../../Content/jquery.dataTables.js"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <%--<script type="text/javascript" lang="javascript">

        $(document).ready(function() {

            $("[id$='cbHead']").live('click', function() {
                //alert('in');
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });

    </script>--%>
    <%--<script type="text/javascript">
         $(document).ready(function() {

         $(".chkheadcurrent").click(function() {
             var val = $(".chkheadcurrent").find(":checkbox").prop("checked");
                 if (val) {
                     $(".chkcurrent").find(":checkbox").prop("checked", true);
                 }
                 else {
                     $(".chkcurrent").find(":checkbox").prop("checked", false);
                 }
             });
             

             $(".chkheadbacklog").click(function() {
                 var val = $(".chkheadbacklog").find(":checkbox").prop("checked");
                 if (val) {
                     $(".chkbacklog").find(":checkbox").prop("checked", true);
                 }
                 else {
                     $(".chkbacklog").find(":checkbox").prop("checked", false);
                 }
             });
 
             $(".chkheadaudit").click(function() {
                 var val = $(".chkheadaudit").find(":checkbox").prop("checked");
                 if (val) {
                     $(".chkaudit").find(":checkbox").prop("checked", true);
                 }
                 else {
                     $(".chkaudit").find(":checkbox").prop("checked", false);
                 }
             });
             
         });
         
    </script>--%>
    <div id="divOptions" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
        <div style="width: 100px; font-weight: bold; float: left;">
            Options :
        </div>
        <div style="width: 500px; font-weight: bold;">
            <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                Font-Bold="true">
                <asp:ListItem Value="M" Selected="True" Text="All Students"></asp:ListItem>
                <asp:ListItem Value="S" Text="Single Student"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="row" style="margin-top: 10px; box-shadow: 0px 0px 20px 0px silver inset;">
        <div class="box box-warning">
            <div class="box-header with-border">
                <h3 class="box-title"><b>REGULAR EXAM REGISTRATION: </b></h3>
                <div class="pull-right">
                    <div style="color: Red; font-weight: bold;">
                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                    </div>
                </div>
            </div>
            <%-- <div style="color: Red; font-weight: bold">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
            </div>--%>

            <div class="box-body">
                <div id="divCourses" runat="server" visible="false">
                    <div class="col-md-12" id="tblSession" runat="server" visible="false">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <label>Session Name: <span style="color: red">*</span></label>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSession" runat="server" InitialValue="0"
                                Display="None" ErrorMessage="Please Enter Session." ValidationGroup="Show" />
                        </div>
                        <div class="col-md-3">
                            <label>Enrollment No: <span style="color: red">*</span> </label>
                            <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" />
                        </div>
                        <div class="col-md-12" style="margin-top: 25px">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" Font-Bold="true" OnClick="btnShow_Click"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" ValidationGroup="Show" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                    Display="None" ErrorMessage="Please Enter Student Enrollment No." ValidationGroup="Show" />
                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                        </div>
                    </div>

                    <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                        <div class="col-md-6">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Student Name :&nbsp;</b><a class="">
                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Father Name :&nbsp;</b><a class="">
                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="true" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Mother Name :&nbsp;</b><a class="">
                                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="true" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Enroll. No./ Roll No. :&nbsp;</b><a class="">
                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Admission Batch :&nbsp;</b><a class="">
                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-6">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Semester : &nbsp;</b><a class="">
                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Degree / Branch :&nbsp;</b><a class="">
                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;PH :&nbsp;</b><a class="">
                                        <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Scheme :&nbsp;</b><a class="">
                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>&nbsp;&nbsp;Payment Type :&nbsp;</b><a class="">
                                        <asp:Label ID="lblPtype" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-12" style="display: none">
                            <label>Total Subjects :</label>
                            <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                Style="text-align: center;"></asp:TextBox>
                            <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0"
                                Style="text-align: center;"></asp:TextBox>
                            <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                        </div>

                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <table class="table table-hover table-bordered table-responsive" style="display:none">
                                    <tr class="bg-light-blue">
                                        <th>Details 
                                        </th>
                                        <th>Amount
                                        </th>
                                    </tr>
                                    <tr style="display: none">
                                        <td>Comman Fee ((Grade Card + CAP) * No of Semesters)
                                        </td>
                                        <td style="font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblCommanFee" runat="server" Text="0"></asp:Label>
                                            <asp:HiddenField ID="hdnDefaultCommanFee" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hdnCommanFee" runat="server" Value="0"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> Exam Fee
                                        </td>
                                        <td style="font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblSelectedCourseFee" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnSelectedCourseFee" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>Late Fine
                                        </td>
                                        <td style="font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblLateFine" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnLateFine" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td>Backlog Fine
                                        </td>
                                        <td style="font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblBacklogFine" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnBacklogFine" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;">Total Fee
                                        </td>
                                        <td style="font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnTotalFee" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12">
                                <asp:ListView ID="lvCurrentSubjects" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="box-header">
                                                <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Current Semester Subjects</h3>
                                            </div>
                                            <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                                <div class="dataTables_header clearfix">
                                                    <div class="">
                                                        <div id="table3" class="dataTables_length" runat="server">
                                                            <label>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                <thead>
                                                    <tr id="Tr1" runat="server" class="bg-light-blue">
                                                        <th>
                                                            <%-- <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" class="chkheadcurrent"
                                                                onclick="SelectAll(this,1,'chkAccept'); Call();" />--%>
                                                        </th>
                                                        <th>Course Code
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Sub. Type
                                                        </th>
                                                        <th>Credits
                                                        </th>
                                                        <%--    <th>Amount
                                                        </th>--%>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <td>
                                                <%-- <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                    Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                    onclick="currentLvChk();" />--%>
                                                <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                    Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' />
                                                <%-- <asp:HiddenField ID="hdnCourseRegister" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>' />
                                                 <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                            </td>
                                            <td class="semester">
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                                <asp:HiddenField ID="hdsemmester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                            </td>
                                            <%--   <td>
                                                <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr id="trCurRow">
                                            <td>
                                                <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                    Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' />
                                                <%--    <asp:HiddenField ID="hdnCourseRegister" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                                   <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                            </td>
                                            <td class="semester">
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                                <asp:HiddenField ID="hdsemmester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                            </td>
                                            <%--     <td>
                                                <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            </td>--%>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                        <div class="col-md-12" style="display:none">
                            <asp:ListView ID="lvBacklogSubjects" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div>
                                        <div class="box-header">
                                            <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Backlog Subjects</h3>
                                        </div>
                                        <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                            <div class="dataTables_header clearfix">
                                                <div class="">
                                                    <div id="table3" class="dataTables_length" runat="server">
                                                        <label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <table id="tblBacklogSubjects" class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        <%--  <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all"
                                                            onclick="SelectAll(this,2,'chkAccept'); Call();" />--%>
                                                    </th>
                                                    <th>Course Code
                                                    </th>
                                                    <th>Course Name
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Subject Type
                                                    </th>
                                                    <th>Credits
                                                    </th>
                                                    <%--  <th>Amount
                                                    </th>--%>
                                                    <%--  <th>Carry Forward
                                                </th>--%>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr id="trCurRow">
                                        <td>
                                            <%-- <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                onclick="backlogLvChk();" />--%>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration" onclick="backlogLvChk(this);" />
                                           <%-- <asp:HiddenField ID="hdnBacklogCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <%--   <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            <asp:HiddenField ID="hhh" runat="server" Value="10" />
                                        </td>--%>
                                        <%--  <td>
                                        <asp:Label ID="lblCanInt" runat="server" Text='<%# Eval("NOINTER_MARK") %>' />
                                    </td>--%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr id="trCurRow">
                                        <td>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration" onclick="backlogLvChk(this);" />
                                           <%-- <asp:HiddenField ID="hdnBacklogCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <%--     <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            <asp:HiddenField ID="hhh" runat="server" Value="10" />
                                        </td>--%>
                                        <%-- <td>
                                        <asp:Label ID="lblCanInt" runat="server" Text='<%# Eval("NOINTER_MARK") %>' />
                                    </td>--%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                            <asp:HiddenField ID="hdnIsbacklog" runat="server" Value="0" />

                        </div>
                        <div class="col-md-2" style="display: none">
                            <asp:TextBox ID="txtnew" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <asp:TextBox ID="txtnonCBCSSem" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <asp:TextBox ID="txtTEAmount" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <asp:TextBox ID="txtschemetype" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <asp:TextBox ID="txtDegree" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="cl-md-12" style="display: none">
                            <asp:ListView ID="lvReAppearedCourse" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <%-- <div class="titlebar">
                                    Re-Appeared Course Subjects</div>--%>
                                        <div class="box-header">
                                            <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Re-Appeared Course Subjects</h3>
                                        </div>
                                        <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                            <div class="dataTables_header clearfix">
                                                <div class="">
                                                    <div id="table3" class="dataTables_length" runat="server">
                                                        <label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <table id="tblReAppearedSubjects" class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr style="background-color: #F3F3F3;">
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="SelectAll(this,3,'chkAccept');" />
                                                    </th>
                                                    <th>Course Code
                                                    </th>
                                                    <th>Course Name
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Subject Type
                                                    </th>
                                                    <th>Credits
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr id="trCurRow" class="bg-pista text-darkgray">
                                        <td>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                onclick="ChkHeader(3,'cbHead','chkAccept');" />
                                            <asp:HiddenField ID="hdnReAppearedCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr id="trCurRow" class="bg-light-red">
                                        <td>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                onclick="ChkHeader(3,'cbHead','chkAccept');" />
                                            <asp:HiddenField ID="hdnReAppearedCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-md-12" style="display: none">
                            <asp:ListView ID="lvAuditSubjects" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div>
                                        <%-- <div class="titlebar">
                                    Audit Subjects</div>--%>
                                        <div class="box-header">
                                            <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Audit Subjects</h3>
                                        </div>
                                        <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                            <div class="dataTables_header clearfix">
                                                <div class="">
                                                    <div id="table3" class="dataTables_length" runat="server">
                                                        <label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <table id="tblAuditSubjects" class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr style="background-color: #F3F3F3;">
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="SelectAll(this,4,'chkAccept');" />
                                                    </th>
                                                    <th>Course Code
                                                    </th>
                                                    <th>Course Name
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Subject Type
                                                    </th>
                                                    <th>Credits
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr id="trCurRow" class="bg-pista text-darkgray">
                                        <td>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                onclick="ChkHeader(4,'cbHead','chkAccept');" />
                                            <asp:HiddenField ID="hdnAuditCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr id="trCurRow" class="bg-light-red">
                                        <td>
                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'
                                                onclick="ChkHeader(4,'cbHead','chkAccept');" />
                                            <asp:HiddenField ID="hdnAuditCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO")%>'/>
                                               <asp:HiddenField ID="hdfscheme" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="col-md-12">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    Visible="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" CssClass="btn btn-success" />
                                <asp:Button ID="btnBackHOD" runat="server" Text="Back to Student List" CssClass="btn btn-primary" Visible = false/>
                                <asp:Button ID="btnChallan" runat="server" Text="Generate Challan" CssClass="btn btn-info" Visible="false" />
                                <%--<asp:Button ID="btnOnlinePayment" runat="server" Text="Online Payment"
                                    CssClass="btn btn-primary"  OnClientClick="SetTarget();" Visible="false"/>--%>
                                <asp:Button ID="btnOnlinePayment" runat="server" Text="Online Payment"
                                    CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnPrintRegSlip" runat="server" Text="Examination Form" OnClick="btnPrintRegSlip_Click"
                                    Visible="false" CssClass="btn btn-info" />

                                <%--       <p><a class="btn btn-primary"  href="http://192.168.0.106/GECA_ONLINE_PAYMENT_TEST/" style="color:white;">Online Payment</a></p>--%>
                            </p>

                        </div>
                    </div>

                </div>
                <div class="col-md-12" id="pnlDept" runat="server" visible="False">
                    <div class="form-group col-md-4">
                        <label>Session</label>
                        <asp:DropDownList ID="ddlSessionReg" runat="server" AppendDataBoundItems="true">
                            <%--AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"--%>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-2">
                        <label>Total Student</label>
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group col-md-2">
                        <label>Registered Student </label>
                        <asp:Label ID="lblRegistered" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group col-md-2">
                        <label>Remaining Student </label>
                        <asp:Label ID="lblPending" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <asp:ListView ID="lvStudent" runat="server">
                            <LayoutTemplate>
                                <div class="box-header">
                                    <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Student List</h3>
                                </div>
                                <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-top: -16px; margin-bottom: -38px;">
                                    <div class="dataTables_header clearfix">
                                        <div class="">
                                            <div id="table3" class="dataTables_length" runat="server">
                                                <label>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table class="table table-hover table-bordered table-responsive">
                                    <thead>
                                        <tr style="background-color: #F3F3F3;">
                                            <th style="width: 5%">Edit
                                            </th>
                                            <th style="width: 35%">Enrollment No./Student Name
                                            </th>
                                            <th>Course Codes
                                            </th>
                                            <th style="width: 10%">Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                                </div>                           
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="bg-pista text-darkgray">
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("REGNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIDNO" runat="server" Visible="false" ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                        <span style="font-size: 9pt">
                                            <%# Eval("REGNO") %>
                                        -
                                        <%# Eval("STUDNAME") %></span>
                                    </td>
                                    <td>
                                        <span style="font-size: 9pt">
                                            <%# Eval("CCODES") %></span>
                                    </td>
                                    <td>
                                        <span style="font-size: 9pt">
                                            <%--<%# Eval("STATUS")%></span>--%>
                                            <asp:Label ID="lblStatus" runat="server" Style="font-size: 10pt" Text='<%# Eval("STATUS")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="bg-light-red">
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("REGNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIDNO" runat="server" Visible="false" ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                        <span style="font-size: 9pt">
                                            <%# Eval("REGNO") %>
                                        -
                                        <%# Eval("STUDNAME") %></span>
                                    </td>
                                    <td>
                                        <span style="font-size: 9pt">
                                            <%# Eval("CCODES") %></span>
                                    </td>
                                    <td>
                                        <span style="font-size: 9pt">
                                            <%--<%# Eval("STATUS")%></span>--%>
                                            <asp:Label ID="lblStatus" runat="server" Style="font-size: 10pt" Text='<%# Eval("STATUS")%>'></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>
        </div>
    </div>









    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />

    <div id="divMsg" runat="server">
    </div>
    <div align="center" id="FreezePane" class="FreezePaneOff">
        <div id="InnerFreezePane" class="InnerFreezePane">
        </div>
    </div>
    <style type="text/css">
        .FreezePaneOff {
            visibility: hidden;
            display: none;
            position: absolute;
            top: -100px;
            left: -100px;
            opacity: .80;
        }

        .FreezePaneOn {
            position: fixed;
            top: 0px;
            left: 0px;
            visibility: visible;
            display: block;
            background-color: black;
            width: 100%;
            height: 100%;
            z-index: 999;
            -moz-opacity: 0.7;
            opacity: .70;
            filter: alpha(opacity=70);
            padding-top: 20%;
        }

        .InnerFreezePane {
            text-align: center;
            width: 66%;
            background-color: #171;
            color: White;
            font-size: large;
            border: dashed 2px #111;
            padding: 9px;
            opacity: .9;
        }
    </style>

    <script type="text/javascript" language="JavaScript">

        function FreezeScreen(msg) {
            scroll(0, 0);
            var outerPane = document.getElementById('FreezePane');
            var innerPane = document.getElementById('InnerFreezePane');
            if (outerPane) outerPane.className = 'FreezePaneOn';
            if (innerPane) innerPane.innerHTML = msg;
        }

        function showConfirm() {
            debugger;
            var validate = false;
            //if (Page_ClientValidate()) {
            //if (ValidatorOnSubmit()) {
            if (ValidateFeeDetail()) {
                var ret = confirm('Do you Really want to Confirm/Submit this Courses for Course Registration?');
                if (ret == true) {
                    FreezeScreen('Your Data is Being Processed...');
                    validate = true;
                }
                else
                    validate = false;
            }
            //}
            //}
            return validate;
        }

    </script>

    <script type="text/javascript">


        function SelectAll(headerid, headid, chk) {
            debugger;
            var tbl = '';
            var list = '';

            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
            }
            else if (headid == 2) {
                tbl = document.getElementById('tblBacklogSubjects');
                list = 'lvBacklogSubjects';
            }
            else if (headid == 3) {
                tbl = document.getElementById('tblReAppearedSubjects');
                list = 'lvReAppearedCourse';
            }
            else {
                tbl = document.getElementById('tblAuditSubjects');
                list = 'lvAuditSubjects';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }

                    Call();
                }
            }
            catch (e) {
                alert(e);
            }
        }



        //$(window).on('load', function () {
        //    //  debugger;
        //    document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = 0.00;
        //    Call();
        //    // ISBackLogFee();
        //});
    </script>


    <script>
        var OriginalBacklogAmt = 0;
        var minusString = '';
        var isBacklogGreaterThan2 = false;

        function currentLvChk() {
            if (document.getElementById('tblCurrentSubjects') != null) {
                dataRows = document.getElementById('tblCurrentSubjects').getElementsByTagName('tr');

                if (dataRows != null) {

                    for (i = 0; i < (dataRows.length - 1) ; i++) {
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_chkAccept').checked) {

                            $("#ctl00_ContentPlaceHolder1_lblSelectedCourseFee").text(Number(500));
                            $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val(Number(500));
                            var selectedCourseAmt = $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val();
                            var lateFineAmt = $("#ctl00_ContentPlaceHolder1_hdnLateFine").val();
                            var newbacklogFineAmt = $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val();

                            var totalFinalAmt = Number(selectedCourseAmt) + Number(newbacklogFineAmt) + Number(lateFineAmt);
                            $("#ctl00_ContentPlaceHolder1_lblTotalFee").text(Number(totalFinalAmt));
                            $("#ctl00_ContentPlaceHolder1_hdnTotalFee").val(Number(totalFinalAmt));

                        }
                    }
                }
            }
        }
        function BacklogCount() {
            debugger;
            if (document.getElementById('tblBacklogSubjects') != null) {
                var backlogCounter = 0;
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
                if (dataRows != null) {
                    var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
                    for (i = 0; i < (dataRows.length - 1) ; i++) {
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept').checked) {
                            backlogCounter = backlogCounter + 1;
                        }
                    }
                }
                return backlogCounter;
            }
        }
        function backlogLvChk(chk) {
            debugger;
            var count;
            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
                if (dataRows != null) {
                    count = BacklogCount();

                    //if (!isBacklogGreaterThan2) {
                        if (count <=2) {
                            var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
                            if (chk.checked) {
                                $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
                                $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
                            }
                            else {
                                $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
                                $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
                            }
                            var newbacklogFineAmt = $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val();
                            var selectedCourseAmt = $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val();
                            var lateFineAmt = $("#ctl00_ContentPlaceHolder1_hdnLateFine").val();

                            var totalFinalAmt = Number(selectedCourseAmt) + Number(newbacklogFineAmt) + Number(lateFineAmt);
                            $("#ctl00_ContentPlaceHolder1_lblTotalFee").text(Number(totalFinalAmt));
                            $("#ctl00_ContentPlaceHolder1_hdnTotalFee").val(Number(totalFinalAmt));
                        }
                    //}
                    else {
                        if (chk.checked) {
                            $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(500));
                            $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(500));
                        }
                        else {
                            var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
                            if (backlogFineAmt == 500) {
                                if (count <= 2) {
                                    $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
                                    $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
                                }
                                else {
                                    $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(500));
                                    $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(500));
                            }
                            }
                        }
                        var newbacklogFineAmt = $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val();
                        var selectedCourseAmt = $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val();
                        var lateFineAmt = $("#ctl00_ContentPlaceHolder1_hdnLateFine").val();

                        var totalFinalAmt = Number(selectedCourseAmt) + Number(newbacklogFineAmt) + Number(lateFineAmt);
                        $("#ctl00_ContentPlaceHolder1_lblTotalFee").text(Number(totalFinalAmt));
                        $("#ctl00_ContentPlaceHolder1_hdnTotalFee").val(Number(totalFinalAmt));
                    }
                }
            }
        }
    </script>

</asp:Content>

