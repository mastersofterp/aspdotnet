<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkOperatorallotment.aspx.cs" Inherits="ACADEMIC_BulkOperatorallotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                 <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <%--<script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:Panel ID="pnlMain" runat="server">
       <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>

                
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>EXAM MARK ENTRY OPERATOR ALLOCATION IN BULK</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

  <%--ANIMATION DATA START--%>


  <%--ANIMATION DATA CLOSE--%>
             
               <%-- <br />--%>

                   <div></div>



                   <%--<div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory
                </div>
                  </div>--%>
                <div class="box-body">
              <%--  <div style="width: 98%; padding-left: 10px">--%>
                    <fieldset ><%--class="fieldset"--%>
                       <%-- <legend class="legend">EXAM MARK ENTRY OPERATOR ALLOCATION IN BULK</legend>--%>


                        <%--<table width="100%">
                            <tr>--%>
                        <br />
                        <div class="form-group col-md-7">
                              

                                <%--<td class="form_left_label" style="width: 20%">
                                    <span class="validstar" style="color: red;">*</span>Session :
                                </td>--%>
                               <%-- <td align="left">--%>
                                    
                                <%--</td>--%>
                

                           <%-- CHANGES BY SUMIT--01112019 --%>

                         <div class="form-group col-md-12"><%--form-group col-md-3--%>
                               <label class="form-group col-md-4 " style="width: 36%"><span style="color: red;">*</span>Session :</label>
                                    <asp:DropDownList ID="ddlSession"  runat="server" class="form-group col-md-8"  TabIndex="1" ValidationGroup="teacherallot" Width="60%" AppendDataBoundItems="true" align="left">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="pendingreport">
                                    </asp:RequiredFieldValidator>
                             
                         <%--</div>--%>
                                <%--<div class="form_left_label" style="width:40%" rowspan="15" valign="top"> --%>

                                   <%-- <asp:UpdatePanel ID="updcour" runat="server"> <ContentTemplate>--%>
                                  
                                       <%-- </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lvCurrentSubjects" />
                                           
                                        </Triggers>
                                        </asp:UpdatePanel>--%>
                                  <%--    </td>
                            </tr>--%>

                                    </div> 
                               <%-- </div>--%>


                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>College /School Name :
                                </td>--%>
                        <div class="form-group col-md-12">
                                    <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>College  Name :</label>
                                <%--<td align="left">--%>
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" class="form-group col-md-8" TabIndex="2" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="60%" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College  Name " ValidationGroup="pendingreport">
                                    </asp:RequiredFieldValidator>
                                <%--</td>--%>
                           <%-- </tr>--%>
                            </div>

                            <%--<tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Degree :
                                </td>--%>
                        <div class="form-group col-md-12">
                                <%--<td align="left">--%>
                             <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Degree :</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" class="form-group col-md-8" AppendDataBoundItems="true"  TabIndex="3" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="60%" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree " ValidationGroup="pendingreport">
                                    </asp:RequiredFieldValidator>
                               <%-- </td>
                            </tr>--%>
                         </div>


                            <%--<tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Branch :
                                </td>--%>
                        <div class="form-group col-md-12">
                               <%-- <td align="left">--%> 
                            <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Branch :</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"  TabIndex="4" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="60%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                <%--</td>
                            </tr>--%>
                       </div>

                            <%--<tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Scheme :
                                </td>--%>
                        <div class="form-group col-md-12">
                                <%--<td align="left">--%>
                             <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Scheme :</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"  TabIndex="5" Width="60%"
                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                               <%-- </td>
                            </tr>--%>
                        </div>

                            <%--<tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Semester :
                                </td>--%>
                        <div class="form-group col-md-12">
                               <%-- <td align="left">--%>
                             <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Semester :</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"  TabIndex="6" Width="60%" ValidationGroup="teacherallot"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                               <%-- </td>
                            </tr>--%>
                        </div>

                        <div class="form-group col-md-12" style="display:none">
                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Subject Type:
                                </td>--%>
                              <%--  <td align="left">--%>
                             <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Subject Type :</label>
                                    <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true"  TabIndex="7" Width="60%"
                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1" class="form-group col-md-8">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                 <%--   <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>

                           
                            </tr>
                           </div>

                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Course :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true"  TabIndex="8" ValidationGroup="teacherallot"
                                        Width="30%" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>

                            <div class="form-group col-md-12" style="display: none">
                                <%--<td class="form_left_label">
                                    <span class="validstar">&nbsp</span>Section :
                                </td>--%>
                             <%--   <td align="left">--%>
                                <label class="form-group col-md-4"><span style="color: red;">*</span>Section :</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true"  TabIndex="9" Width="60%" ValidationGroup="teacherallot"
                                      class="form-group col-md-8" >
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                <%--</td>--%>
                            </div>

                            <div class="form-group col-md-12" style="display: none">
                                <%--<td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Internal/External :
                                </td>
                                <td>--%>
                                 <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Internal/External :</label>
                                    <asp:DropDownList ID="ddlInEx" runat="server" AppendDataBoundItems="true"  TabIndex="10" Width="60%" ValidationGroup="teacherallot"
                                       class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                 <%--   <asp:RequiredFieldValidator ID="rfvInEx" runat="server" ControlToValidate="ddlInEx"
                                        Display="None" ErrorMessage="Please Select Internal/External" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>

                               <%-- </td>--%>

                            </div>

                            <div class="form-group col-md-12">
                                 <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Exam Name :</label>
                                    <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true"  TabIndex="10" Width="60%" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged"
                                       class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                        <asp:ListItem Value="2">END SEM</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                        Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlExamName"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Exam Name " ValidationGroup="pendingreport">
                                    </asp:RequiredFieldValidator>

                               <%-- </td>--%>

                            </div>



                            <div class="form-group col-md-12" style="display: none">
                                <%--<td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Select Operator
                                </td>--%>
                             <%--   <td align="left">--%>
                                 <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Select Operator :</label>
                                    <asp:DropDownList ID="ddlOprator" runat="server" AppendDataBoundItems="true"  TabIndex="11" Width="60%" ValidationGroup="teacherallot"
                                        class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Operator 1</asp:ListItem>
                                        <asp:ListItem Value="2">Operator 2</asp:ListItem>
                                    </asp:DropDownList>
                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOprator"
                                        Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                            </div>

                            <div class="form-group col-md-12" style="display: none">
                                <%--<td class="form_left_label">&nbsp;
                                </td>
                                <td align="left">--%>
                                <label class="form-group col-md-4" style="width: 36%"></label>

                                    <asp:CheckBox runat="server" ID="chkover" Text=" Overwrite exist one"  TabIndex="12"  />
                                <%--</td>--%>
                            </div>

                            <div class="form-group col-md-12">
                                <%--<td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>User Type:
                                </td>--%>
                               <%-- <td align="left">--%>
                                 <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>User Type :</label>
                                    <asp:DropDownList ID="ddlTeachertype" runat="server"  TabIndex="13" AppendDataBoundItems="true" Width="60%"
                                        ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlTeachertype_SelectedIndexChanged" class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTeachertype"
                                        Display="None" ErrorMessage="Please Select User Type" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                             <%--  </td>--%>
                            </div>

                            <div class="form-group col-md-12">
                                <%--<td class="form_left_label">
                                    <span class="validstar" style="color: red;">*</span>Operator :
                                </td>--%>
                               <%-- <td align="left">--%>
                                 <label class="form-group col-md-4" style="width: 36%"><span style="color: red;">*</span>Operator :</label>
                                    <asp:DropDownList ID="ddlTeacher" runat="server"  TabIndex="14" AppendDataBoundItems="true" Width="60%"
                                        ValidationGroup="teacherallot" class="form-group col-md-8">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                        Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                              <%--  </td>--%>
                            </div>
                            <div>
                               <%-- <td class="form_left_label">&nbsp;
                                </td>--%>
                                <%--<td align="left">--%>
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                               <%-- </td>--%>
                            </div>
                            <div  align="center">
                                <%--<td>&nbsp;
                                </td>--%>
                                <%--<td align="left">--%>
                                    <asp:Button ID="btnSave" runat="server"  TabIndex="14" Text="Save" ValidationGroup="teacherallot"
                                        Width="100px" OnClick="btnSave_Click" CssClass="btn btn-success" ToolTip="SAVE" />&nbsp;&nbsp;
                                      <asp:Button ID="btnReport" runat="server" Text="Print Report"
                                        Width="100px" ToolTip="Report" OnClick="btnReport_Click" CssClass="btn btn-primary" />&nbsp;&nbsp;
                                  <asp:Button ID="btnPendingReport" runat="server" Text="Pending Allotment Report" ValidationGroup="pendingreport"
                                        Width="200px" ToolTip="Pending Allotment Report" OnClick="btnPendingReport_Click" CssClass="btn btn-primary" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClear" runat="server"  TabIndex="15" Text="Clear" Width="80px" OnClick="btnClear_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="pendingreport"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <%--</td>--%>
                            </div>

                       </div> <%--1112019--%>
                             <%--<tr>
                                 <td>
                                </td>
                                 <td>
                                </td>
                            </tr>--%>

                        <div class="form-group col-md-5">
                             <asp:Panel ID="pnlcour" runat="server" Width="100%"  Height="600 px" ToolTip="FeedBack Course List" > <%-- BorderColor="#CDCDCD" BorderWidth="1px" ScrollBars="Vertical"--%>
                                              
                             <asp:ListView ID="lvCurrentSubjects" runat="server"  >
                            <LayoutTemplate>
                              <div class="demo-grid">
                                  <h4>Selected Semester Subjects</h4>
                                   <%-- <div class="titlebar">
                                        Selected Semester Subjects</div>--%>
                                    <table class="table table-hover table-bordered table-responsive"><%--cellpadding="0" cellspacing="0" class="datatable"  style="width: 100% ; height:50px"--%>
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <%--<th>
                                                    <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="SelectAll(this,1,'chkAccept');" />
                                                </th>--%>
                                                <th>
                                                    <asp:CheckBox ID="cbHeadReg" CssClass="select_all" runat="server" Text="Select" ToolTip="Select/Select all" />
                                                </th>
                                                <th>
                                                    Course Code
                                                </th>
                                                <th width="45%">
                                                    Course Name
                                                </th>
                                              
                                             
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </thead>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="trCurRow" >
                                   <%-- <td>
                                        <asp:CheckBox ID="chkAccept" runat="server"  ToolTip="Click to select this subject for registration" onclick="ChkHeader(1,'cbHead','chkAccept');" />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>'/>
                                    </td>--%>



                                    <td>
                                        <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this subject for registration"
                                        /> 
                                    </td>

                                     <%--onclick="-ChkHeader(1,'cbHeadReg','chkRegister');"--%>

                                    <td >
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td width="45%">
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>                                                                       
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                       </asp:Panel>
                     </div>



                            <tr>
                                <td colspan="3" style="border-color:black" >
                                   <div class="col-md-12">
                                    <asp:Repeater ID="lvDetails" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-hover table-bordered table-responsive" style="width: 100%;"><%--cellpadding="0" cellspacing="0" class="display"--%>
                                                <thead>
                                                    <tr class="bg-light-blue">

                                                        <th align="left">College Name
                                                        </th>
                                                        <th align="left">Degree Name
                                                        </th>
                                                       
                                                        <th align="left">Branch Name
                                                        </th>
                                                        <th align="left">Scheme Name
                                                        </th>
                                                        <th align="left">Semester
                                                        </th>
                                                        <th align="left">Exam Name
                                                        </th>
                                                        <th align="left">Course
                                                        </th>
                                                      <%--  <th align="left">Operator
                                                        </th>
                                                        <th align="left">Status
                                                        </th>--%>
                                                         <th align="left">Operator name
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                    <thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="item">

                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>

                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                               
                                                <td>
                                                    <%# Eval("LONGNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("schemename") %>
                                                </td>
                                                <td>
                                                    <%# Eval("semesterNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("EXAMNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME") %>
                                                </td>
                                              <%--  <td>
                                                    <%# Eval("OPRTR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS") %>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("UA_FULLNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                              </tr>
                            </tr>
                        </table>
                               </div>
                    </fieldset>
                </div>
                <br />
              </div>
            </div>
          </div>
                
          </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnReport" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

   <%-- <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
        function SelectAll(headchk) {
        
            var frm =  document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 if (e.type == 'checkbox') {
                     if (headchk.checked == true) {
                         e.checked = true;                                        
                     }
                     else {
                         e.checked = false;
                     }
                    
                 }
                
             }

             if (headchk.checked == true) {
                 txtTot.value = hdfTot.value;
                 txtCredits.value = hdfCredits.value;
             }
             else
             {
                 txtTot.value = 0;
                 txtCredits.value = 0;
             }
         
         }


      
    </script>--%>


    <script type="text/javascript">

        //code to select all checkbox 

        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lvCurrentSubjects_cbHeadReg").click(function () {
                $(".demo-grid").find('input[type="checkbox"]').prop('checked', $(this).prop('checked'));
            });

            var parameter = Sys.WebForms.PageRequestManager.getInstance();

            parameter.add_endRequest(function () {
                $("#ctl00_ContentPlaceHolder1_lvCurrentSubjects_cbHeadReg").click(function () {
                   
                    $(".demo-grid").find('input[type="checkbox"]').prop('checked', $(this).prop('checked'));
                });

            });
        });
     
        </script>



    <%--//document.getElementById('<%=chkover.ClientID %>').checked = false;--%>

       <%--<script src="../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>--%>
<script type="text/javascript">

    $(document).ready(function () {
        debugger;
        var count = 0;
        $("[id$='cbHeadReg']").live('click', function () {
            
            $("[id$='chkRegister']").attr('checked', this.checked);
            
            //if ($("[id$='cbHead']").is(":not(:checked)")) {
            //    txtTot.value = 0;
            //}
            //else {
            //    txtTot.value = ($("[id$='chkAllot']").length);
            //}
        });

    });

    </script>

</asp:Content>





