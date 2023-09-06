<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Demo_ExamRegistration.aspx.cs" Inherits="ACADEMIC_Demo_ExamRegistration" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../plugins/jQuery/jQuery-2.2.0.min.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

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
                OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true">
                <asp:ListItem Value="M" Selected="True" Text="All Students"></asp:ListItem>
                <asp:ListItem Value="S" Text="Single Student"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updScheme"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updScheme" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>EXAM FORM FILLUP</b></h3>
                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold">
                                    Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body" style="margin-bottom:50px;">
                            <div id="divCourses" runat="server" visible="false">
                                <div class="col-md-12" id="tblSession" runat="server" visible="false">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-3">
                                        <label><span style="color: red;">*</span>Session Name</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" ControlToValidate="ddlSession" runat="server" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show" />
                                    </div>
                                    <div class="col-md-3">
                                        <label><span style="color: red;">*</span>Registration No </label>
                                        <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" />
                                    </div>
                                    <div class="col-md-12" style="margin-top: 25px">
                                        <p class="text-center">
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Font-Bold="true"
                                                ValidationGroup="Show" CssClass="btn btn-success" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" ValidationGroup="Show"
                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                                Display="None" ErrorMessage="Please enter Student Registration No." ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" />
                                    </div>
                                </div>

                                <div class="" id="tblInfo" runat="server" visible="false">
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
                                                <b>&nbsp;&nbsp;Registration. No. :&nbsp;</b><a class="">
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
                                                <b>&nbsp;&nbsp;&nbsp;</b><a class="">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label></a>
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
                                    <div class="form-group col-md-12" id="divphotosign" runat="server">
                                            <div class="form-group col-md-6">
                                                <div class="panel panel-info">
                                                    <div class="panel-body" ><%--style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);"--%>
                                                        <div style="color: Red; font-weight: bold">
                                                            Only Passport Size Photo Allowed
                                                        </div>
                                                        <br />
                                                        <%--<div class="box-footer">--%>
                                                        <label>Photo :</label>


                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" />
                                                        <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="35" onchange="previewFilePhoto()" />
                                                        <%--<asp:Button ID="btnPhotoUpload" runat="server" CssClass="btn btn-primary" Text="Upload" TabIndex="35" OnClick="btnPhotoUpload_Click" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        
                                            <div class="form-group col-md-6">
                                                <span style="color:red ;font-weight:bold"> Note :  Kinldy upload Photo and Signature in JPEG or  JPG format and Size should be less than 250kb.</span>
                                                <br />
                                                <br />
                                                <br />
                                                <div class="panel panel-info">
                                                    <div class="panel-body" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                                        <label>Signature :</label>
                                                        <asp:Image ID="ImgSign" runat="server" Width="150px" Height="40px" />
                                                        <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()" TabIndex="36" />
                                                       <%-- <asp:Button ID="btnSignUpload" CssClass="btn btn-primary" runat="server" Text="Upload" TabIndex="37" OnClick="btnSignUpload_Click" />--%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    <asp:Panel ID="pnlFeeDetails" runat="server" >
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6" style="display:none">
                                            <table class="table table-hover table-bordered table-responsive">
                                                <tr>
                                                    <th>Details 
                                                    </th>
                                                    <th style="text-align: center">Amount
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
                                                <tr style="display: none">
                                                    <td>Selected Course Fee
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblSelectedCourseFee" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnSelectedCourseFee" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>Late Fine
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblLateFine" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnLateFine" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>Backlog Fine
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblBacklogFine" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnBacklogFine" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnFinalBacklogFine" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Regular Fee
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblRegFee" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnRegFee" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trBacklogFee">
                                                    <td>Backlog Fee
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblBacklogFee" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnBacklogFee" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem1" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem2" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem3" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem4" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem5" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem6" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem7" runat="server" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField ID="hdnBacklogFeeSem8" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold;">Total Fee
                                                    </td>
                                                    <td style="font-weight: bold; text-align: center;">
                                                        <asp:Label ID="lblTotalFee" runat="server" Text="0"></asp:Label>
                                                        <asp:HiddenField ID="hdnTotalFee" runat="server" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:ListView ID="lvCurrentSubjects" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="box-header">
                                                            <h3 class="box-title new-header-lv" style="margin-left: -10px; font-size: 16px; font-weight: bold; margin-top: 14px;">Regular Courses</h3>
                                                        </div>
                                                        <div class="box-body well well-sm" style="margin-right: 0px; margin-left: 0px; margin-bottom: -38px;">
                                                            <div class="dataTables_header clearfix">
                                                                <div class="">
                                                                    <div id="table3" class="dataTables_length" runat="server">
                                                                        <label>
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <table id="tblCurrentSubjects" class="table table-hover table-bordered" style="width:100%">
                                                                <thead>
                                                                    <tr id="Tr1" runat="server" style="background-color: #F3F3F3;">
                                                                        <th>
                                                                            <%--<asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" class="chkheadcurrent"
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
                                                                        <%-- <th>Amount
                                                                        </th>--%>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow" class="bg-pista text-darkgray">
                                                        <td>
                                                            <%-- Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>'--%>
                                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                                onclick="ValidateFeeDetail();" Checked=true Enabled='<%# Convert.ToInt32( Session["usertype"])==2 ?false:true %>' /><%--Checked='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 || Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1)?true:false %>'--%>
                                                            <asp:HiddenField ID="hdnCourseRegister" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                            <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
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
                                                        <%--<td>
                                                <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr id="trCurRow" class="bg-light-red">
                                                        <td>
                                                            <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                                onclick="ValidateFeeDetail();" Checked=true Enabled='<%# Convert.ToInt32( Session["usertype"])==2 ?false:true %>' />
                                                            <asp:HiddenField ID="hdnCourseRegister" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                            <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
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
                                    </asp:Panel>
                                    <div class="col-md-12">
                                        <asp:ListView ID="lvBacklogSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="box-header">
                                                        <h3 class="box-title new-header-lv" style="margin-left: -10px; margin-bottom: 20px; font-size: 16px; font-weight: bold;">Backlog Courses</h3>
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
                                                            <tr style="background-color: #F3F3F3;">
                                                                <th>
                                                                    <%--<asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all"
                                                            onclick="SelectAll(this,2,'chkAccept'); Call();" />--%>
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
                                                                <%--<th>Amount
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
                                                <tr id="trCurRow" class="bg-pista text-darkgray">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                            onclick="backlogfeescheck();"  Checked=true Enabled='<%# Convert.ToInt32( Session["usertype"])==2 ?false:true %>' />
                                                        <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                        <%--  <asp:HiddenField ID="hdnBacklogCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%><%--onclick="ValidateFeeDetail();"--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                        <asp:HiddenField ID="hdsemmester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <%--  <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            <asp:HiddenField ID="hhh" runat="server" Value="10" />
                                        </td>--%>
                                                    <%--  <td>
                                        <asp:Label ID="lblCanInt" runat="server" Text='<%# Eval("NOINTER_MARK") %>' />
                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr id="trCurRow" class="bg-light-red">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration"
                                                            onclick="backlogfeescheck();"  Checked=true Enabled='<%# Convert.ToInt32( Session["usertype"])==2 ?false:true %>' />
                                                        <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                        <%--  <asp:HiddenField ID="hdnBacklogCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                        <asp:HiddenField ID="hdsemmester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <%-- <td>
                                            <asp:Label ID="lblCourseAmount" runat="server" Text='<%# Eval("COURSE_FEE") %>' />
                                            <asp:HiddenField ID="hhh" runat="server" Value="10" />
                                        </td>--%>
                                                    <%-- <td>
                                        <asp:Label ID="lblCanInt" runat="server" Text='<%# Eval("NOINTER_MARK") %>' />
                                    </td>--%>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>


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
                                    <div class="cl-md-12">
                                        <asp:ListView ID="lvReAppearedCourse" runat="server">
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
                                                            onclick="ChkHeader(3,'cbHead','chkAccept');" />
                                                        <asp:HiddenField ID="hdnReAppearedCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
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
                                                            onclick="ChkHeader(3,'cbHead','chkAccept');" />
                                                        <asp:HiddenField ID="hdnReAppearedCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
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
                                    <div class="col-md-12" style="display: none">
                                        <asp:ListView ID="lvAuditSubjects" runat="server">
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
                                                            onclick="ChkHeader(4,'cbHead','chkAccept');" />
                                                        <asp:HiddenField ID="hdnAuditCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
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
                                                            onclick="ChkHeader(4,'cbHead','chkAccept');" />
                                                        <asp:HiddenField ID="hdnAuditCourse" runat="server" Value='<%# Eval("COURSE_FEE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                                        <asp:HiddenField ID="hdsemmester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
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
                                    <div class="col-md-12 form-group">
                                        <asp:Label ID="lblNote" runat="server" Text="Note : Examination Fees For Regular Rs. 1200 and For Backlog Papers Rs. 1000 per Semester.
Please Ensure to submit the Examination Fees in the University Portal to get the Exam Admit Card and to Appear in the Examination." Font-Bold="true" ForeColor="Red"></asp:Label>                                       
                                         <span style="color:red;font-weight:bold"></span>
                                    </div>
                                    <div class="col-md-12">
                                        <p class="text-center">

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                Visible="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" CssClass="btn btn-success" />
                                            <asp:Button ID="btnBackHOD" runat="server" Text="Back to Student List" OnClick="btnBackHOD_Click" Visible="false" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnChallan" runat="server" Text="Generate Challan" CssClass="btn btn-info" Visible="false" OnClick="btnChallan_Click" />
                                            <%--<asp:Button ID="btnOnlinePayment" runat="server" Text="Online Payment"
                                    CssClass="btn btn-primary"  OnClientClick="SetTarget();" Visible="false"/>--%>
                                            <asp:Button ID="btnOnlinePayment" runat="server" Text="Online Payment"
                                                CssClass="btn btn-primary" OnClick="btnOnlinePayment_Click" Visible="false" />
                                             <asp:Button ID="btnPaymentSlip" runat="server" Text="Online Payment Receipt" style="display:none" OnClick="btnPaymentSlip_Click"
                                                Visible="false" CssClass="btn btn-info" />
                                            <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click"
                                                Visible="false" CssClass="btn btn-info" />
                                            <asp:HiddenField ID="hdnBacklogCount" runat="server" />
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
                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
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
                                                        <th style="width: 35%">Registration No./Student Name
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("REGNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("REGNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
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

                        <%-- <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="22"
                                    Text="Report" CssClass="btn btn-info" />
                                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </p>

                        </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOnlinePayment" />
            <asp:PostBackTrigger ControlID="btnPaymentSlip" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

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
            var count = 0;
            var alltbl = ["tblCurrentSubjects"];
            var tbl = '';

            var count1 = 0;
            var alltbl1 = ["tblBacklogSubjects"];
            var tbl1 = '';

            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    for (j = 0; j < dataRows.length - 1; j++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + 'lvCurrentSubjects' + '_ctrl' + j + '_chkAccept';
                        //alert(chkid);
                        if (document.getElementById(chkid).checked) {
                            count++;
                        }
                    }
                }
            }


            for (k = 0; k < alltbl1.length; k++) {
                tbl1 = document.getElementById(alltbl1[k]);
                if (tbl1 != null) {
                    var dataRows = tbl1.getElementsByTagName('tr');
                    for (l = 0; l < dataRows.length - 1; l++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + 'lvBacklogSubjects' + '_ctrl' + l + '_chkAccept';
                        //alert(chkid);
                        if (document.getElementById(chkid).checked) {
                            count1++;
                        }
                    }
                }
            }

            if (count == 0 && count1 == 0) {

                alert('Please select atleast one course !');
                return false;
            }
           
            //if (count1 == 0) {

            //    alert('Please select atleast one backlog course !');
            //    return false;
            //}
            else {
                //var validate = false;
                var validate = true;
                // if (ValidateFeeDetail()) {
                //var ret = confirm('Do you Really want to Confirm/Submit this Courses for Exam Registration?');
                //if (ret == true) {
                //    //FreezeScreen('Your Data is Being Processed...');
                //    validate = true;
                //}
                //else {
                //    validate = false;
                //}
                //}
                return validate;
            }
        }

    </script>

    <script type="text/javascript">
        function ValidateFeeDetail() {
            debugger;
            var alltbl = ["tblCurrentSubjects", "tblBacklogSubjects"];
            var tbl = '';
            var list = '';
            var valid = true;
            var semesterCount = 0;
            var oldSemester = '';
            var currSemester = '';

            var totCoursefee = 0.0;
            var totCoursefeeBkg = 0.0;
            var backlogFee = 0.0;
            var backlogFeeS1 = 0.0;
            var backlogFeeS2 = 0.0;
            var backlogFeeS3 = 0.0;
            var backlogFeeS4 = 0.0;
            var backlogFeeS5 = 0.0;
            var backlogFeeS6 = 0.0;
            var backlogFeeS7 = 0.0;
            var backlogFeeS8 = 0.0;
            var totBacklogFee = 0.0;
            var selregAmt = 0.0;
            var totRegFee = 0.0;
            var totbacklogFee = 0.0;
            var backlogSemNo = 0.0;
            var count = 0;
            var checkcount = 0;
            try {
                for (i = 0; i < alltbl.length; i++) {
                    //alert('alltbl:' + alltbl.length);
                    tbl = document.getElementById(alltbl[i]);
                    if (tbl != null) {
                        var dataRows = tbl.getElementsByTagName('tr');
                        if (dataRows != null) {
                            if (alltbl[i] == 'tblCurrentSubjects') {
                                list = 'lvCurrentSubjects';
                                headid = 1;
                            }
                            else if (alltbl[i] == 'tblBacklogSubjects') {
                                list = 'lvBacklogSubjects';
                                headid = 2;
                            }


                            for (j = 0; j < dataRows.length - 1; j++) {
                                //alert('datarows:' + dataRows.length);
                                var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                                //alert(chkid);
                                if (document.getElementById(chkid).checked) {
                                    var selAmt = 0.0;

                                    if (headid == 1) {
                                        //selAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_lblRegFee').innerHTML.trim());
                                        //alert('regamt' + document.getElementById('ctl00_ContentPlaceHolder1_hdnRegFee').value);
                                        document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML = Number(document.getElementById('ctl00_ContentPlaceHolder1_hdnRegFee').value);
                                        selregAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML.trim());
                                        //alert(selAmt);
                                        //checkcount++;
                                        //backlogFee = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_lblBacklogFee').innerHTML.trim());
                                        backlogFee = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML.trim());
                                        //alert(backlogFee);
                                        //backlogFeeS1 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem1Fee').innerHTML.trim());
                                        //alert(backlogFeeS1);
                                        //backlogFeeS2 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem2Fee').innerHTML.trim());
                                        //alert(backlogFeeS2);
                                        //backlogFeeS3 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem3Fee').innerHTML.trim());
                                        //alert(backlogFeeS3);
                                        //backlogFeeS4 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem4Fee').innerHTML.trim());
                                        //alert(backlogFee);
                                        //backlogFeeS5 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem5Fee').innerHTML.trim());
                                        //backlogFeeS6 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem6Fee').innerHTML.trim());
                                        //backlogFeeS7 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem7Fee').innerHTML.trim());
                                        //backlogFeeS8 = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogSem8Fee').innerHTML.trim());
                                        //totCoursefee = selAmt + backlogFee + backlogFeeS1 + backlogFeeS2 + backlogFeeS3 + backlogFeeS4 + backlogFeeS5 + backlogFeeS6 + backlogFeeS7 + backlogFeeS8;
                                        totRegFee = selregAmt + backlogFee;
                                    }
                                    else if (headid == 2) {
                                        //alert('sem3' + document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem3').value);
                                        //backlogSemNo = document.getElementById('ctl00_ContentPlaceHolder1_hdsemmester').value;
                                        // backlogSemNo = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + j + '_hdsemmester').value;
                                        //alert('backlogSemNo' + document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + j + '_hdsemmester').value);
                                        //alert(count);
                                        //if (count != 1) {
                                        //    if (backlogSemNo == 1) {
                                        //        backlogFeeS1 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem1').value;
                                        //        count = 1;
                                        //    }
                                        //}
                                        //else {
                                        //    backlogFeeS1 = 0.0;
                                        //}


                                        ////alert(backlogFeeS1);
                                        //if (count != 2) {
                                        //    if (backlogSemNo == 2) {
                                        //        backlogFeeS2 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem2').value;
                                        //        count = 2;
                                        //    }
                                        //}
                                        //else {
                                        //    backlogFeeS2 = 0.0;
                                        //}
                                        ////alert(backlogFeeS2);
                                        //if (count != 3) {
                                        //    if (backlogSemNo == 3) {
                                        //        backlogFeeS3 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem3').value;
                                        //        count = 3;
                                        //    }
                                        //    else {
                                        //        backlogFeeS3 = 0.0;
                                        //    }
                                        //}

                                        ////alert(backlogFeeS3);
                                        //if (backlogSemNo == 4) {
                                        //    backlogFeeS4 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem4').value;
                                        //}
                                        //else {
                                        //    backlogFeeS4 = 0.0;
                                        //}
                                        //if (backlogSemNo == 5) {
                                        //    backlogFeeS5 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem5').value;
                                        //}
                                        //else {
                                        //    backlogFeeS5 = 0.0;
                                        //}
                                        //if (backlogSemNo == 6) {
                                        //    backlogFeeS6 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem6').value;
                                        //}
                                        //else {
                                        //    backlogFeeS6 = 0.0;
                                        //}
                                        //if (backlogSemNo == 7) {
                                        //    backlogFeeS7 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem7').value;
                                        //}
                                        //else {
                                        //    backlogFeeS7 = 0.0;
                                        //}
                                        //if (backlogSemNo == 8) {
                                        //    backlogFeeS8 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem8').value;
                                        //}
                                        //else {
                                        //    backlogFeeS8 = 0.0;
                                        //}
                                        //backlogFee = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML.trim());
                                        // backlogFee = parseFloat(Number(backlogFeeS1) + Number(backlogFeeS2) + Number(backlogFeeS3) + Number(backlogFeeS4) + Number(backlogFeeS5) + (backlogFeeS6) + (backlogFeeS7) + (backlogFeeS8));

                                        document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML = 0;
                                        totbacklogFee = parseFloat(selregAmt + backlogFee);
                                        //alert(totbacklogFee);
                                    }
                                }
                                else {
                                    if (headid == 1) {
                                        //  checkcount--;
                                        // alert('hai' + checkcount);
                                        //document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML = 0;
                                        //document.getElementById('ctl00_ContentPlaceHolder1_hdnRegFee').value = 0;
                                    }
                                }
                                if (document.getElementById(chkid).checked) {
                                    checkcount++;
                                    if (headid == 1)
                                        document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML = document.getElementById('ctl00_ContentPlaceHolder1_hdnRegFee').value;

                                }
                                else {
                                    // alert('Else checkcount' + checkcount);
                                }

                            }

                            if (checkcount == 0) {
                                //alert('hello');
                                document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML = 0;
                                // document.getElementById('ctl00_ContentPlaceHolder1_hdnRegFee').value = 0;
                            }
                        }
                    }
                }

                document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totRegFee;//+  parseFloat(document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML);
                document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totRegFee;//+ (document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML);
                backlogfeescheck();
            }
            catch (e) {
                alert(e);
                valid = false;
            }
            return valid;

        }


        function backlogfeescheck() {
            debugger;
            var backlogFeeS1 = 0.0;
            var backlogFeeS2 = 0.0;
            var backlogFeeS3 = 0.0;
            var backlogFeeS4 = 0.0;
            var backlogFeeS5 = 0.0;
            var backlogFeeS6 = 0.0;
            var backlogFeeS7 = 0.0;
            var backlogFeeS8 = 0.0;
            var hdnbackolgcount = document.getElementById('<%=hdnBacklogCount.ClientID%>');
             for (var i = 0 ; i < hdnbackolgcount.value; i++) {

                 var chkAccept = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept');
                 var hdnSemesterno = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_hdnSemesterno').value;

                 if (chkAccept.type == 'checkbox') {
                     if (chkAccept.checked) {
                         if (hdnSemesterno == 1) {
                             backlogFeeS1 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem1').value;
                             //count = 1;
                         }
                         else {
                             backlogFeeS1 = 0.0;
                         }
                         if (hdnSemesterno == 2) {
                             backlogFeeS2 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem2').value;
                             //count = 2;

                         }
                             //}
                         else {
                             //backlogFeeS2 = 0.0;
                         }

                         if (hdnSemesterno == 3) {
                             backlogFeeS3 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem3').value;
                             // alert(backlogFeeS3);
                             // count = 3;
                         }
                         else {
                             //   backlogFeeS3 = 0.0;
                         }
                         //}

                         //alert(backlogFeeS3);
                         if (hdnSemesterno == 4) {
                             backlogFeeS4 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem4').value;
                         }
                         else {
                             // backlogFeeS4 = 0.0;
                         }
                         if (hdnSemesterno == 5) {
                             backlogFeeS5 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem5').value;
                         }
                         else {
                             // backlogFeeS5 = 0.0;
                         }
                         if (hdnSemesterno == 6) {
                             backlogFeeS6 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem6').value;
                         }
                         else {
                             // backlogFeeS6 = 0.0;
                         }
                         if (hdnSemesterno == 7) {
                             backlogFeeS7 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem7').value;
                         }
                         else {
                             //backlogFeeS7 = 0.0;
                         }
                         if (hdnSemesterno == 8) {
                             backlogFeeS8 = document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFeeSem8').value;
                         }
                         else {
                             // backlogFeeS8 = 0.0;
                         }
                     }

                 }

             }
             var RegFee = 0;
             RegFee = document.getElementById('ctl00_ContentPlaceHolder1' + '_lblRegFee').innerHTML;
             var backlogFee = parseFloat(Number(backlogFeeS1) + Number(backlogFeeS2) + Number(backlogFeeS3) + Number(backlogFeeS4) + Number(backlogFeeS5) + Number(backlogFeeS6) + Number(backlogFeeS7) + Number(backlogFeeS8));
             document.getElementById('ctl00_ContentPlaceHolder1' + '_lblBacklogFee').innerHTML = backlogFee;
             document.getElementById('ctl00_ContentPlaceHolder1_hdnBacklogFee').value = backlogFee;
             document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = parseFloat(backlogFee) + parseFloat(RegFee);
             document.getElementById('ctl00_ContentPlaceHolder1_hdnFinalBacklogFine').value = parseFloat(backlogFee) + parseFloat(RegFee);
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



        $(window).on('load', function () {
            //  debugger;
            // document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = 0.00;
            //document.getElementById('ctl00_ContentPlaceHolder1_lblBacklogFine').innerHTML = 0.00;

            // Call();
            // ISBackLogFee();
        });

    </script>
    <script type="text/javascript">
        var Previous_Sem = 0;

        function Call() {
            debugger;

            var schemeno = document.getElementById('ctl00_ContentPlaceHolder1_txtschemetype').value;
            if (schemeno == 2) {
                //alert('if');
                ForNonCBCS();
            }
            else {

                ForCBCS(Previous_Sem);
                //alert(semesterno);
            }

        }



        function ForCBCS(Previous_Sem) {
            debugger;
            var TotalAmtCurrentSem = 0;
            var TotalAmtBackLog = 0;
            var FinalCount = 0;
            var latefee = 0;
            var toatalfinalamt = 0;
            var backlog_fine = 0;
            var backlogFeeAmt = 0;
            var selectedsem = 0;

            //var backlog_fine = 'ctl00_ContentPlaceHolder1_lblBacklogFine';
            // ---------------FOR  Current Semester Subjects ------------------//


            if (document.getElementById('tblCurrentSubjects') != null) {
                dataRows = document.getElementById('tblCurrentSubjects').getElementsByTagName('tr');
                //alert(dataRows.length);

                if (dataRows != null) {

                    for (i = 0; i < (dataRows.length - 1) ; i++) {

                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_chkAccept').checked) {

                            var status = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_lblCourseAmount').innerHTML;

                            TotalAmtCurrentSem = Number(TotalAmtCurrentSem) + Number(status);
                        }
                        else {
                            TotalAmtCurrentSem = Number(TotalAmtCurrentSem) - Number(status);
                        }
                    }
                }
            }

            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 0; i < (dataRows.length - 1) ; i++) {
                        var extamt1 = document.getElementById('<%=hdnBacklogFine.ClientID%>').value;
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept').checked) {
                            var attr = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_lblSemester').getAttribute('title');
                            var attr1 = document.getElementById('ctl00_ContentPlaceHolder1_lblSemester').getAttribute('title')
                            var extamtt = extamt1;
                            //alert(Previous_Sem)
                            //Previous_Sem = attr;
                            //alert(Previous_Sem)
                            //if (((attr == 2) && (attr1) == 2) || ((attr == 1) && (attr1) == 1) || ((attr == 2) && (attr1) == 4) || ((attr == 2 || attr == 4) && (attr1) == 6) || ((attr == 2 || attr == 4 || attr == 6) && (attr1) == 8) || ((attr == 1 || attr == 3) && (attr1) == 5) || ((attr == 1 || attr == 3 || attr == 5) && (attr1) == 7) || ((attr == 1) && (attr1) == 3)) {
                            //alert("1");
                            //  var status = document.getElementById('<%=lblBacklogFine.ClientID%>').innerHTML = extamt;
                            if (attr != Previous_Sem) {
                                var extamtttt = extamtt;
                                backlogFeeAmt = Number(backlogFeeAmt) + Number(TotalAmtBackLog) + Number(extamtttt);
                                //alert(backlogFeeAmt)
                            }
                            else {
                                //alert("else")
                            }
                            Previous_Sem = attr;
                        }
                    }
                }
            }
            else {
                TotalAmtBackLog = 0;
            }
            ///////////////////////////////////////////////////////
            //////////// FOR  Backlog Semester Subjects ///////////
            //////////////////////////////////////////////////////

            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');

                if (dataRows != null) {

                    for (i = 0; i < (dataRows.length - 1) ; i++) {

                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept').checked) {

                            var status = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_lblCourseAmount').innerHTML;
                            TotalAmtBackLog = Number(TotalAmtBackLog) + Number(status);

                        }
                    }
                }
            }
            else {
                TotalAmtBackLog = 0;
            }


            // -------------------------------------------------------------//
            FinalCount = Number(TotalAmtCurrentSem) + Number(TotalAmtBackLog);
            latefee = document.getElementById('<%=lblLateFine.ClientID%>').innerHTML;
            backlogFeeAmt1 = Number(backlogFeeAmt);
            toatalfinalamt = Number(latefee) + Number(FinalCount) + Number(backlogFeeAmt1);
            //blank = 0;
            document.getElementById('<%=lblSelectedCourseFee.ClientID%>').innerHTML = FinalCount;
            document.getElementById('<%=lblTotalFee.ClientID%>').innerHTML = toatalfinalamt;
            document.getElementById('<%=hdnTotalFee.ClientID%>').value = toatalfinalamt.toFixed();

            //document.getElementById('ctl00_ContentPlaceHolder1_lblBacklogFine').innerHTML = backlogFeeAmt;
            document.getElementById('<%=lblBacklogFine.ClientID%>').innerHTML = backlogFeeAmt1;
            document.getElementById('ctl00_ContentPlaceHolder1_hdnFinalBacklogFine').value = backlogFeeAmt1;
        }

        function ForNonCBCS() {
            var amt = 0;
            var lateFeeAmt = 0;
            var backlogFeeAmt = 0;
            var TotalAmtBackLog = 0;
            var FinalCount = 0;
            var toatalfinalamt = 0;
            var backlog_fine = 0;

            //  debugger;
            if (document.getElementById('tblCurrentSubjects') != null) {
                dataRows = document.getElementById('tblCurrentSubjects').getElementsByTagName('tr');
                //alert(dataRows.length);

                if (dataRows != null) {
                    amt = document.getElementById('ctl00_ContentPlaceHolder1_txtTEAmount').value;
                    //  alert(amt);
                }
                else {
                    amt = 0;
                }

            }
            lateFeeAmt = document.getElementById('ctl00_ContentPlaceHolder1_lblLateFine').innerHTML;
            //   alert(lateFeeAmt);

            // var backlogFeeAmt = document.getElementById('ctl00_ContentPlaceHolder1_lblBacklogFine').innerHTML;
            //    alert(backlogFeeAmt);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////////
            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');

                if (dataRows != null) {

                    for (i = 0; i < (dataRows.length - 1) ; i++) {
                        var extamt1 = document.getElementById('<%=hdnBacklogFine.ClientID%>').value;
                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept').checked) {
                                var attr = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_lblSemester').getAttribute('title');
                                var attr1 = document.getElementById('ctl00_ContentPlaceHolder1_lblSemester').getAttribute('title');
                                var extamtt = extamt1;
                                if (((attr == 1) && (attr1) == 1) || ((attr == 2) && (attr1) == 2) || ((attr == 2) && (attr1) == 4) || ((attr == 2 || attr == 4) && (attr1) == 6) || ((attr == 2 || attr == 4 || attr == 6) && (attr1) == 8) || ((attr == 1 || attr == 3) && (attr1) == 5) || ((attr == 1 || attr == 3 || attr == 5) && (attr1) == 7) || ((attr == 1) && (attr1) == 3)) {
                                    //alert("1");
                                    //  var status = document.getElementById('<%=lblBacklogFine.ClientID%>').innerHTML = extamt;
                                var extamtttt = extamtt;
                                backlogFeeAmt = Number(TotalAmtBackLog) + Number(extamtttt);
                            }
                        }
                    }
                }
            }
            else {
                TotalAmtBackLog = 0;

            }



                ///////////////////////////////////////////////////////
                //////////// FOR  Backlog Semester Subjects ///////////
                //////////////////////////////////////////////////////

            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');

                if (dataRows != null) {

                    for (i = 0; i < (dataRows.length - 1) ; i++) {

                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_chkAccept').checked) {

                            var status = document.getElementById('ctl00_ContentPlaceHolder1_lvBacklogSubjects_ctrl' + i + '_lblCourseAmount').innerHTML;
                            TotalAmtBackLog = Number(TotalAmtBackLog) + Number(status);

                        }
                    }
                }
            }
            else {
                TotalAmtBackLog = 0;
            }

                /////////////////////////////////////////////////////////////////////////////////////////

            document.getElementById('ctl00_ContentPlaceHolder1_lblSelectedCourseFee').innerHTML = amt;

            var finalTotal = Number(amt) + Number(lateFeeAmt) + Number(backlogFeeAmt) + Number(TotalAmtBackLog);

            document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = finalTotal;


            FinalCount = Number(amt) + Number(TotalAmtBackLog);
            lateFeeAmt = document.getElementById('<%=lblLateFine.ClientID%>').innerHTML;
            backlogFeeAmt1 = Number(backlogFeeAmt);
            toatalfinalamt = Number(lateFeeAmt) + Number(FinalCount) + Number(backlogFeeAmt1);
                //blank = 0;
            document.getElementById('<%=lblSelectedCourseFee.ClientID%>').innerHTML = FinalCount;
            document.getElementById('<%=lblTotalFee.ClientID%>').innerHTML = toatalfinalamt;
                document.getElementById('<%=hdnTotalFee.ClientID%>').value = toatalfinalamt.toFixed();

                //document.getElementById('ctl00_ContentPlaceHolder1_lblBacklogFine').innerHTML = backlogFeeAmt;
                document.getElementById('<%=lblBacklogFine.ClientID%>').innerHTML = backlogFeeAmt1;
            }

            //==================================================================================================================================================================
    </script>
    <script type="text/javascript">
       
    </script>


    <script type="text/javascript">

        function SetTarget() {
            //form.action = "https://14.139.110.183/GECA_ONLINE_PAYMENT";
            //form.target = "_blank";

            var form = document.createElement("form");
            form.method = "GET";
            form.action = "https://14.139.110.183/GECA_ONLINE_PAYMENT";
            form.target = "_blank";
            document.body.appendChild(form);
            form.submit();
        }

    </script>

     <script type="text/javascript">
         function previewFilePhoto() {
             debugger;
             var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        function previewFilePhoto2() {
            var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
     <script type="text/javascript">
         function pageLoad() {

             function previewFilePhoto() {
                 var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }

            function previewFilePhoto2() {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }


        }
    </script>

</asp:Content>
