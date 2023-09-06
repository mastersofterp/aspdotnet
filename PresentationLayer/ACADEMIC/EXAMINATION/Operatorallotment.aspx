<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Operatorallotment.aspx.cs" Inherits="ACADEMIC_Operatorallotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: center">
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

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

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

   <%-- <asp:Panel ID="pnlMain" runat="server">--%>
       <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">EXAM MARK ENTRY OPERATOR ALLOCATION</h3>
                            </div>
                            
                                <div class="box-body">
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Session</label>
                                          <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True" ValidationGroup="teacherallot">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>College /School Name</label>
                                        <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true"  TabIndex="2" ValidationGroup="teacherallot"
                                        AutoPostBack="True"  OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Degree</label>
                                         <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"  TabIndex="3" ValidationGroup="teacherallot"
                                        AutoPostBack="True"  OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"  TabIndex="4" ValidationGroup="teacherallot"
                                        AutoPostBack="True"    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Scheme</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"  TabIndex="5"
                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Semester</label>
                                           <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"  TabIndex="6" ValidationGroup="teacherallot"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3" >
                                        <label><span style="color: red">*</span>Subject Type</label>
                                         <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true"  TabIndex="7"
                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>                           
                                    <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Course</label>
                                          <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true"  TabIndex="8" ValidationGroup="teacherallot"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-md-3" style="display: none">
                                         <label><span style="color: red">*</span>Section</label>
                                         <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="9" ValidationGroup="teacherallot">
                                             <asp:ListItem>Please Select</asp:ListItem>
                                         </asp:DropDownList>
                                         <%--<asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Internal/External</label>
                                        <asp:DropDownList ID="ddlInEx" runat="server" AppendDataBoundItems="true" TabIndex="10" ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Internal</asp:ListItem>
                                            <asp:ListItem Value="2">External</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvInEx" runat="server" ControlToValidate="ddlInEx"
                                            Display="None" ErrorMessage="Please Select Internal/External" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Select Operator</label>
                                        <asp:DropDownList ID="ddlOprator" runat="server" AppendDataBoundItems="true" TabIndex="11" ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Operator 1</asp:ListItem>
                                            <asp:ListItem Value="2">Operator 2</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOprator"
                                            Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3" style="margin-top:30px">
                                       <asp:CheckBox runat="server" ID="chkover" Text="Overrite exist one"   TabIndex="12"/>
                                    </div>
                                     <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>User Type</label>
                                          <asp:DropDownList ID="ddlTeachertype" runat="server"  TabIndex="13" AppendDataBoundItems="true"
                                        ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlTeachertype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTeachertype"
                                        Display="None" ErrorMessage="Please Select User Type" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-md-3">
                                        <label><span style="color: red">*</span>Operator</label>
                                          <asp:DropDownList ID="ddlTeacher" runat="server"  TabIndex="14" AppendDataBoundItems="true" 
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                        Display="None" ErrorMessage="Please Select Operator Name" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="col-md-12">
                                      <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>

                            <div class="box-footer">
                               <div style="text-align:center">
                                    <asp:Button ID="btnSave" runat="server" TabIndex="14" Text="Save" ValidationGroup="teacherallot"
                                        OnClick="btnSave_Click" ToolTip="SAVE" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnReport" runat="server" Text="Print Report"
                                        ToolTip="Report" OnClick="btnReport_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btnClear" runat="server" TabIndex="15" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                   </div>
                                <p></p>
                                <p></p>
                            
                                     <div class="col-md-12">
                                         <asp:Panel ID="pnlMain" runat="server" ScrollBars="Auto">
                                             <asp:ListView ID="lvDetails" runat="server">
                                            
                                            <%-- <asp:Repeater ID="lvDetails" runat="server">--%>
                                                 <%--<HeaderTemplate>--%>
                                                     <LayoutTemplate>
                                                     <table class="table table-hover table-bordered table-responsive">
                                                         <%-- cellpadding="0" cellspacing="0" class="display" style="width: 100%;"--%>
                                                         <thead>
                                                             <tr class="bg-light-blue">
                                                                <%-- <th>Edit </th>--%>
                                                                 <th>College Name </th>
                                                                 <th>Degree Name </th>
                                                                 <th>Branch Name </th>
                                                                 <th>Scheme Name </th>
                                                                 <th>Semester </th>
                                                                 <th>Course </th>
                                                                 <th>Operator </th>
                                                                 <th>Status </th>
                                                                 <th>Operator name </th>
                                                             </tr>
                                                             <thead>
                                                                 <tbody>
                                                                     <tr id="itemPlaceholder" runat="server" />
                                                                 </tbody>
                                                             </thead>
                                                         </thead>
                                                     </table>
                                                 <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                                 <ItemTemplate>
                                                     <tr>
                                                       <%-- <td>--%>
                                                             <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip='<%# Eval("OPID") %>' visible="false" />
                                                             <%--'<%# Eval("COURSENO")%>'--%><%--CommandArgument='<%# Eval("OPID") %>'--%>

                                                       <%--  </td>--%>
                                                         <td>
                                                             <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' />
                                                            <%-- <%# Eval("COLLEGE_NAME")%>--%>

                                                         </td>

                                                         <td>
                                                             <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREENAME") %>' />
                                                            <%-- <%# Eval("DEGREENAME")%>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("LONGNAME") %>' />
                                                             <%--<%# Eval("LONGNAME") %>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lblschemename" runat="server" Text='<%# Eval("schemename") %>' />
                                                             <%--<%# Eval("schemename") %>--%>

                                                         </td>
                                                         <td style="text-align:center;">
                                                              <asp:Label ID="lblsemester" runat="server" Text='<%# Eval("semesterNO") %>' />
                                                            <%-- <%# Eval("semesterNO") %>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lblcourse" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                             <%--<%# Eval("COURSE_NAME") %>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lbloperator" runat="server" Text='<%# Eval("OPRTR") %>' />
                                                            <%-- <%# Eval("OPRTR") %>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS") %>' />
                                                            <%-- <%# Eval("STATUS") %>--%>

                                                         </td>
                                                         <td>
                                                              <asp:Label ID="lbluafullname" runat="server" Text='<%# Eval("UA_FULLNAME") %>' />
                                                           <%--  <%# Eval("UA_FULLNAME") %>--%>

                                                         </td>
                                                     </tr>
                                                 </ItemTemplate>
                                           <%--  </asp:Repeater>--%>
                                               </asp:ListView>
                                         </asp:Panel>
                                    </div>                                                                 
                            </div>
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
  <%--  </asp:Panel>--%>

    <script type="text/javascript" language="javascript">
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
    </script>

</asp:Content>
