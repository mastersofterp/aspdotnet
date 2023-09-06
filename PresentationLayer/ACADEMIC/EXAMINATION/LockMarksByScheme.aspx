<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LockMarksByScheme.aspx.cs" Inherits="ACADEMIC_LockMarksByScheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>LOCK/UNLOCK MARK ENTRY</b> </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--  <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="save">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="save"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="show" TabIndex="2" ToolTip="Please Select Institute"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none ">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" TabIndex="3" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="4" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" TabIndex="5" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="6" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubType" runat="server" AppendDataBoundItems="True" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged"
                                            ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <%--Added By Nikhil V.Lambe on 24/02/2021 for Sessional Sub Type in MAKAUT--%>
                                            <asp:ListItem Value="3">Sessional</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubType" runat="server" ControlToValidate="ddlSubType"
                                            Display="None" ErrorMessage="Please Select Course Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Exam Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged"
                                            ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSubExamType" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Exam Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExamType" runat="server" AppendDataBoundItems="True" TabIndex="7" data-select2-enable="true"
                                            ValidationGroup="show" CssClass="form-control" OnSelectedIndexChanged="ddlSubExamType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubExamType" runat="server" ControlToValidate="ddlSubExamType"
                                           ValidationGroup="save" Display="None" ErrorMessage="Please Select Sub Exam Type" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trStutype" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStuType" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="8" AutoPostBack="true" OnSelectedIndexChanged="ddlStuType_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select </asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStuType" runat="server" ControlToValidate="ddlStuType" Display="None" ErrorMessage="Please Select Student Type"
                                            InitialValue="-1" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trSubexam" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub-Exam Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExam" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked=<span style="color: green; font-weight: bold">Lock</span></span>  </p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked=<span style="color: green; font-weight: bold">UnLock</span></span>  </p>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Font-Bold="False" OnClick="btnShow_Click"
                                    TabIndex="9" Text="Show" ValidationGroup="save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSave" runat="server" Visible="false" Font-Bold="False" OnClick="btnSave_Click"
                                    TabIndex="11" Text="Lock/Unlock" ValidationGroup="save" CssClass="btn btn-primary" />

                                <asp:Button ID="btnShowStatus" runat="server" Font-Bold="False"
                                    OnClick="btnShowStatus_Click" TabIndex="8" Text="Show Status" Visible="false"
                                    ValidationGroup="Status" CssClass="btn btn-warning" />
                                <%--<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="12"
                                    Text="Cancel" CssClass="btn btn-warning" />--%>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="save" />
                                <asp:ValidationSummary ID="ValidationSummaryShow0" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Status" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                    <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Lock/Unlock Marks</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr No.</th>
                                                        <th>Course Code
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Course Type
                                                        </th>
                                                        <%--  <th>Faculty Name - Theory
                                                            </th>
                                                             <th>Faculty Name - Practical
                                                            </th>--%>
                                                        <th>
                                                            <asp:Label ID="lblFacName" runat="server"></asp:Label>
                                                        </th>
                                                        <th>Section
                                                        </th>
                                                        <%-- <th>Exam
                                                            </th>--%>
                                                        <%-- <th>Mark Entry Status
                                                            </th>--%>
                                                        <th>Lock/Unlock
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td>
                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsub_type" runat="server" Text='<%# Eval("SUBJECT_TYPE")%>'></asp:Label>
                                                </td>
                                                <%-- <td>
                                                    <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("FACULTY_NAME_THEORY")%>' ToolTip='<%# Eval("FACULTY_NO_THEORY")%>'></asp:Label>
                                                </td>--%>
                                                <%-- <td>
                                                    <asp:Label ID="lblfacultypractical" runat="server" Text='<%# Eval("FACULTY_NAME_PRACTICAL")%>' ToolTip='<%# Eval("FACULTY_NO_PRACTICAL")%>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("FACULTY_NAME")%>' ToolTip='<%# Eval("FACULTY_NO")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblsection" runat="server" Text='<%# Eval("SECTIONNAME")%>' ToolTip='<%# Eval("SECTIONNO")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnsection" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                </td>
                                                <%--  <td>
                                                    <asp:Label ID="Exam1" runat="server" Text='<%# Eval ("LOCKE") %>'></asp:Label>
                                                </td>--%>
                                                <%--<td>
                                                    <asp:Label ID="lblComplete" runat="server" Text="Complete" ></asp:Label>
                                                    <asp:Label ID="lblInComplete" runat="server" Text="InComplete" ></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:CheckBox ID="chklock" runat="server" TabIndex="10" ToolTip='Select to Lock' Checked='<%# Eval("LOCK").ToString() == "True" ? true : false %>' />

                                                </td>
                                            </tr>
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
       <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>--%>
        
    </asp:UpdatePanel>
     
      

 

    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)

                        if (e.enable = true)
                            e.checked = true;
                        else
                            e.checked = false;
                }
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
