<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="Course_Registration_Approval.aspx.cs" Inherits="ACADEMIC_Course_Registration_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlALLStudent .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlCoreSubject .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlUniCoreSub .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlGlobalSubject .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlValueAdded .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="uplReg"
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

    <asp:UpdatePanel ID="uplReg" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Text="Course Registration Approval"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divpnlSearch" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>College</label>--%>
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Session Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Degree Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblDegree" runat="server" Text="Degree" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Branch Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblddlBranch" runat="server" Text="Branch" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                                            data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                           <%-- <sup>*</sup>--%>
                                            <asp:Label ID="lblSeme" runat="server" Text="Semester" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="Label11" runat="server" Text="Filter by Registration Status" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlFilter" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control"
                                            data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">Core Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="2">Elective Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="3">Global Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="7">Specialization Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="4">Core Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="5">Elective Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="6">Global Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="8">Specialization Course Registration Pending</asp:ListItem>--%>

                                            <asp:ListItem Value="1">Core Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="2">Elective Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="3">Global Course Registration Done</asp:ListItem>
                                            <asp:ListItem Value="4">Specialization Course Registration Done</asp:ListItem>

                                            <asp:ListItem Value="5">Core Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="6">Elective Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="7">Global Course Registration Pending</asp:ListItem>
                                            <asp:ListItem Value="8">Specialization Course Registration Pending</asp:ListItem>

                                            <asp:ListItem Value="9">Core Course Approval Pending</asp:ListItem>
                                            <asp:ListItem Value="10">Elective Course Approval Pending</asp:ListItem>
                                            <asp:ListItem Value="11">Global Course Approval Pending</asp:ListItem>
                                            <asp:ListItem Value="12">Specialization Course Approval Pending</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFilter"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Filter by Registration Status" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                          <label>Select Student Type</label>
                                        </div>
                                       <asp:RadioButton runat="server" Checked="true" GroupName="course" Text="Course Registration Done by Student" AutoPostBack="true" ID="rdoCourseRegDone" OnCheckedChanged="rdoCourseRegDone_CheckedChanged"/>
                                       <asp:RadioButton runat="server" GroupName="course" Text="Course Registration Not Done by Student" ID="rdoCoursePending" OnCheckedChanged="rdoCoursePending_CheckedChanged" />

                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students List" TabIndex="5" OnClick="btnShow_Click"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                    TabIndex="6" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" TabIndex="7" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>
                                            <asp:ListView ID="lvApproveCourse" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblApproveCourse">
                                                        <thead class="bg-light-blue" style="top: -15px !important; height: 68px">
                                                            <tr>
                                                                <th style="width: 5%">Sr.No.</th>
                                                                <th style="width: 10%">
                                                                    <asp:CheckBox ID="cbApproveAll" runat="server" Text="" ToolTip="Approve all" onclick="SelectAll(this,1,'cbApprove');" />
                                                                    Approve all
                                                                </th>
                                                                <th style="width: 10%">Edit Record</th>
                                                                <th style="width: 10%">Semester</th>
                                                                <th style="width: 20%">PRN No.</th>
                                                                <th style="width: 55%">Student Name</th>
                                                                <%--   <th>Core Course Registration</th>
                                                            <th>Core Course Approval</th>
                                                            <th>Elective Course Registration</th>
                                                            <th>Selected Elective Subject</th>
                                                            <th>Elective Course Approval</th>
                                                            <th>Elective Reg. by Student</th>
                                                            <th>Global Course Registration</th>
                                                            <th>Selected Global Subject</th>
                                                            <th>Global Course Approval</th>
                                                            <th>Global Reg. by Student</th>--%>
                                                                <%--<th>Course Codes</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td style="width: 5%"><%# Container.DataItemIndex + 1 %></td>
                                                        <td style="width: 10%">
                                                            <asp:CheckBox ID="cbApprove" runat="server" Value='<%# Eval("IDNO") %>' Style="Height: 13px !important" ToolTip="Click to select this Course for Approval"
                                                                onclick="ChkHeader(1,'cbHeadReg','cbApprove');" />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" Style="Height: 13px !important" ImageUrl="~/images/edit1.gif"
                                                                CommandArgument='<%# Eval("IDNO")%>' AlternateText="Edit Record" ToolTip='<%# Eval("IDNO")%>'
                                                                OnClick="btnEdit_Click" />
                                                            <%--<asp:Image ID="imgEdit" runat="server"  ImageUrl="~/images/edit1.gif" AlternateText="Edit Record" />--%>
                                                        </td>
                                                        <td style="width: 10%"><%# Eval("SEMESTERNAME") %></td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' Style="Height: 13px !important" ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td style="width: 55%">
                                                            <asp:Label ID="lblStudentName" runat="server" Style="Height: 13px !important" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>
                                                        <%-- <td>
                                                        <asp:Label ID="Label8" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("CORE_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("CORE_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("CORE_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("CORE_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("ELECT_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("ELECTIVE_COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("ELECT_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_STUD_REG") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("ELECT_STUD_REG") )== 1 ?  "Yes" : "No" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("GLOBAL_COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOB_STUD_REG") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("GLOB_STUD_REG") )== 1 ?  "Yes" : "No" )%>' />
                                                    </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-6 mt-3" id="divCore" runat="server" visible="false">
                                        <asp:Panel ID="pnlCoreStudent" runat="server">
                                            <asp:ListView ID="lvStudentCore" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudCore">
                                                        <thead class="bg-light-blue" style="top: -15px !important; height: 68px">
                                                            <tr>
                                                                <th style="width: 5%">Sr.No.</th>
                                                                <th style="width: 20%">PRN No.</th>
                                                                <th style="width: 40%">Student Name</th>
                                                                <th style="width: 15%">Core Course Registration</th>
                                                                <th style="width: 20%">Core Course Approval</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td style="width: 5%"><%# Container.DataItemIndex + 1 %></td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label8" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("CORE_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("CORE_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                            <%--CORE_COURSE_REGISTRATION_STATUS--%>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="Label3" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("CORE_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("CORE_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                            <%--CORE_COURSE_APPROVED--%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-6 mt-3" id="divElect" runat="server" visible="false">
                                        <asp:Panel ID="pnlElectStudent" runat="server">
                                            <asp:ListView ID="lvStudentElect" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudElect">
                                                        <thead class="bg-light-blue" style="top: -15px !important; height: 68px">
                                                            <tr>
                                                                <th style="width: 5%">Sr.No.</th>
                                                                <th style="width: 15%">PRN No.</th>
                                                                <th style="width: 40%">Student Name</th>
                                                                <th style="width: 10%">Elective Course Registration</th>
                                                                <th style="width: 10%">Selected Elective Subject</th>
                                                                <th style="width: 10%">Elective Course Approval</th>
                                                                <th style="width: 10%">Elective Reg. by Student</th>


                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td style="width: 5%"><%# Container.DataItemIndex + 1 %></td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>

                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label9" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("ELECT_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("ELECTIVE_COURSENAME") %>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label4" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("ELECT_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label6" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("ELECT_STUD_REG") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("ELECT_STUD_REG") )== 1 ?  "Yes" : "No" )%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-6 mt-3" id="divGlobal" runat="server" visible="false">
                                        <asp:Panel ID="pnlGlobalStudent" runat="server">
                                            <asp:ListView ID="lvStudentGlobal" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudGlobal">
                                                        <thead class="bg-light-blue" style="top: -15px !important; height: 68px">
                                                            <tr>
                                                                <th style="width: 5%">Sr.No.</th>
                                                                <th style="width: 15%">PRN No.</th>
                                                                <th style="width: 40%">Student Name</th>
                                                                <th style="width: 10%">Global Course Registration</th>
                                                                <th style="width: 10%">Selected Global Subject</th>
                                                                <th style="width: 10%">Global Course Approval</th>
                                                                <th style="width: 10%">Global Reg. by Student</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td style="width: 5%"><%# Container.DataItemIndex + 1 %></td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label10" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_REGISTRATION_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_REGISTRATION_STATUS") )> 0 ?  "Registered" : "Pending" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("GLOBAL_COURSENAME") %>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label5" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("GLOBAL_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label7" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("GLOB_STUD_REG") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("GLOB_STUD_REG") )== 1 ?  "Yes" : "No" )%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-6 mt-3" id="divSpecialization" runat="server" visible="false">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvSpecialization" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudCore">
                                                        <thead class="bg-light-blue" style="top: -15px !important; height: 68px">
                                                            <tr>
                                                                <th style="width: 5%">Sr.No.</th>
                                                                <th style="width: 20%">PRN No.</th>
                                                                <th style="width: 55%">Student Name</th>
                                                                <th style="width: 55%">Semester Admission Status</th>
                                                                <th style="width: 10%">Specialization Course Registration</th>
                                                                <th style="width: 10%">Specialization Course Approval</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td style="width: 5%"><%# Container.DataItemIndex + 1 %></td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td style="width: 55%">
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>
                                                        <td style="width: 55%">
                                                            <asp:Label ID="Label15" runat="server" Text='<%# (Convert.ToInt32(Eval("SAFLAG") )== 1 ?  "Done" : "Pending" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label8" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("SPECIALIZATION_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("SPECIALIZATION_COURSE_APPROVED") )> 0 ?  "Registered" : "Pending" )%>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label3" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("SPECIALIZATION_COURSE_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("SPECIALIZATION_COURSE_APPROVED") )== 1 ?  "Approved" : "Not Approved" )%>' />
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12" id="dvStudentInfo" runat="server" visible="false">
                                <div id="div1" runat="server"></div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        <asp:Label ID="Label2" runat="server" Text="Course Details"></asp:Label></h3>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Student Name :</b><a class="">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Mother Name :</b><a class="">
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Father Name :</b><a class="">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Registration No. :</b><a class="">
                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Min Credits Limit :</b><a class="">
                                                    <asp:Label ID="lblOfferedRegCreditsFrom" runat="server" Font-Bold="True"></asp:Label></a>
                                                <asp:HiddenField ID="hdfDegreenoFrom" runat="server" />
                                            </li>
                                            <li class="list-group-item">
                                                <b>Max Credits Limit :</b><a class="">
                                                    <asp:Label ID="lblOfferedRegCredits" runat="server" Font-Bold="True"></asp:Label></a>
                                                <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Admission Batch :</b><a class="">
                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                <b>:</b><a class="">
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label><b>/</b>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"><b>:</b>
                                                </asp:Label>
                                                <a class="">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                <br />
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                <b>:</b><a class="">
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                <br />
                                            </li>
                                            <li class="list-group-item" style="display: none">
                                                <b>physical handicap:</b><a class="">
                                                    <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Total Register Credits :</b><a class="">
                                                    <asp:Label ID="lblTotalRegCredits" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-12" style="display: none">
                                    <div class="col-md-3">
                                        <label>Total Courses</label>
                                        <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                            Style="text-align: center;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Total Credits</label>
                                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0"
                                            Style="text-align: center;"></asp:TextBox>
                                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>
                                </div>
                                <div class="box-footer col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnCourseUptForStud" runat="server" Text="Update/APPROVE Course Registration" CssClass="btn btn-success" OnClientClick="return validateAssign();"
                                            OnClick="btnCourseUptForStud_Click" ValidationGroup="SUBMIT" />
                                        <asp:Button ID="btnPrintChallan" runat="server" Text="Print Challan" Visible="false" OnClick="btnPrintChallan_Click" CssClass="btn btn-info" />
                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                            OnClick="btnPrintRegSlip_Click" Enabled="false" Visible="false" CssClass="btn btn-info" />
                                        <asp:Button ID="btnPrePrintClallan" runat="server" Visible="false" Text="Re-Print Challan"
                                            OnClick="btnPrePrintClallan_Click" CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancelUptForStud" runat="server" Text="Back" OnClick="btnCancelUptForStud_Click" CssClass="btn btn-warning" />
                                        <asp:HiddenField ID="hdnCount" runat="server" Value="0" />
                                        <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="SUBMIT" />
                                        <div style="color: red; text-align: center; font: bold; display: none;">Note: Submission of selected course depends on current availability for course intake. </div>
                                    </p>

                                    <div class="col-md-12 table table-responsive" style="display: none">
                                        <asp:Repeater ID="lvHistory" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>&<asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>Grade
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbReport" runat="server" OnClick="lbReport_Click"><%# Eval("SESSION_NAME") %></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("CCODE") %>' />

                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                        <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                        <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                        <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <asp:Panel ID="pnlCoreSubject" runat="server">
                                        <asp:ListView ID="lvCurrentSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table id="tblCurrentSubjects" class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="9" style="text-align: left">Core Courses
                                                                </th>
                                                            </tr>
                                                            <tr class="bg-light-blue">
                                                                <th>Select</th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Course Type
                                                                </th>
                                                                <th>Credits
                                                                </th>
                                                                <th style="display: none">Elective
                                                                </th>
                                                                <th style="display: none">Elective Group
                                                                </th>
                                                                <th style="display: none">Section
                                                                </th>
                                                                <th style="display: none;">Course Teacher
                                                                </th>
                                                                <th style="display: none;">Intake
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
                                                <tr id="trCurRow">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" Checked='true' Enabled='false'
                                                            ToolTip='<%# Eval("ELECT") %>' />
                                                        <%-- OnCheckedChanged="chkCurrentSubjects_OnCheckedChanged" AutoPostBack="false" onclick="electivevalidatation(this);"--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                        <asp:HiddenField ID="hdnCredits" runat="server" Value='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblElective" runat="server" Text='<%# Convert.ToInt32(Eval("ELECT"))==1 ? "Yes" : "No" %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfGropupno" runat="server" Value='<%# Eval("GROUPNO") %>' />
                                                        <asp:HiddenField ID="hdfElectChoice" runat="server" Value='<%# Eval("ELECTIVE_CHOISEFOR") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblelectGroup" runat="server" Text='<%# Eval("GROUPNAME") %>'></asp:Label>
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME") %>' ToolTip='<%# Eval("SectionNO") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblCourseTeacher" runat="server" Text='<%# Eval("UA_FULLNAME") %>' ToolTip='<%# Eval("UA_NO") %>' />
                                                    </td>
                                                    <td style="display: none;">
                                                        <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-4" id="divgroups" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%--<label>Select Specialization Groups</label>--%>
                                        <asp:Label Font-Bold="True" ID="lblSpecializationGroup" runat="server"></asp:Label>
                                    </div>
                                    <asp:ListBox ID="ddlgroups" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlgroups_SelectedIndexChanged"></asp:ListBox>

                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <asp:Panel ID="pnlValueAdded" runat="server">
                                        <asp:ListView ID="lvValueAddedGroup" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table id="tblValueAddedGroup" class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="9" style="text-align: left">Value Added / Specialization Courses  <%--Breadth/Open Elective/Program Elective Courses--%>
                                                                </th>
                                                            </tr>
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th>Group Name
                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Credits
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
                                                <tr id="trCurRow">
                                                    <td>
                                                        <asp:CheckBox ID="chkValueAddedGroup" runat="server" ToolTip='<%#Eval("GROUPID") %>'
                                                            OnCheckedChanged="chkValueAddedGroup_CheckedChanged" AutoPostBack="true" Checked="true" Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("GROUP_NAME") %>' ToolTip='<%# Eval("GROUPID")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <asp:Panel ID="pnlUniCoreSub" runat="server">
                                        <asp:ListView ID="lvUniCoreSub" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <table id="tblUniCoreSub" class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="9" style="text-align: left;">Elective Courses
                                                                </th>
                                                            </tr>
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Course Type
                                                                </th>
                                                                <th>Course Group
                                                                </th>
                                                                <th style="display: none">Section
                                                                </th>
                                                                <th style="display: none">Course Teacher
                                                                </th>
                                                                <th style="display: none">Intake
                                                                </th>
                                                                <th>Credits
                                                                </th>
                                                                <th>Available Seats
                                                                </th>
                                                                <th>Exam Registration Status
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" ToolTip='<%#Eval("GROUPNO") %>' Enabled='<%# ( Eval("EXAM_REGISTERED").ToString()=="1") ? false : true %>'
                                                            Checked='<%# (Eval("STUD_COURSE_REG").ToString() == "1" || Eval("REGISTERED").ToString() == "1") ? true : false %>'
                                                            OnCheckedChanged="chklvUniCoreSub_OnCheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgroupname" runat="server" Font-Bold="true" Text='<%# Eval("GROUPNAME") %>' ToolTip='<%# Eval("GROUPNO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME") %>' ToolTip='<%# Eval("SectionNO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblCourseTeacher" runat="server" Text='<%# Eval("UA_FULLNAME") %>' ToolTip='<%# Eval("UA_NO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' ToolTip='<%# Eval("CHOICEFOR") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAvailableSeats" runat="server" Text='<%# Eval("AVAILABLESEATS") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" ToolTip='<%# Eval("EXAM_REGISTERED") %>' ForeColor='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 1 ?  "Done" : "" )%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <asp:Panel ID="pnlGlobalSubject" runat="server">
                                        <asp:ListView ID="lvGlobalSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table id="tblGlobalSubjects" class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="9" style="text-align: left">Global Courses  <%--Breadth/Open Elective/Program Elective Courses--%>
                                                                </th>
                                                            </tr>
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Course Type
                                                                </th>
                                                                <th style="display: none">Course Group
                                                                </th>
                                                                <th style="display: none">Section
                                                                </th>
                                                                <th style="display: none">Course Teacher
                                                                </th>
                                                                <th style="display: none">Intake
                                                                </th>
                                                                <th>Credits
                                                                </th>
                                                                <th>Available Seats
                                                                </th>
                                                                <th>Exam Registration Status
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
                                                <tr id="trCurRow">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%#Eval("GROUPNO") %>' Checked='<%# (Eval("STUD_COURSE_REG").ToString() == "1" || Eval("REGISTERED").ToString() == "1") ? true : false %>'
                                                            Enabled='<%# ( Eval("EXAM_REGISTERED").ToString()=="1") ? false : true %>' OnCheckedChanged="chkGlobalSubjects_OnCheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblgroupname" runat="server" Font-Bold="true" Text='<%# Eval("GROUPNAME") %>' ToolTip='<%# Eval("GROUPNO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME") %>' ToolTip='<%# Eval("SectionNO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblCourseTeacher" runat="server" Text='<%# Eval("UA_FULLNAME") %>' ToolTip='<%# Eval("UA_NO") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' ToolTip='<%# Eval("CHOICEFOR") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAvailableSeats" runat="server" Text='<%# Eval("AvailableSeats") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExamRegistred" runat="server" ToolTip='<%# Eval("EXAM_REGISTERED") %>' ForeColor='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 1 ?  "Done" : "" )%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblApproveCourse');
                list = 'lvApproveCourse';
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
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var headid = '';
                var tbl = '';
                var list = '';
                var chkcnt = 0;
                if (chklst == 1) {
                    tbl = document.getElementById('tblApproveCourse');
                    headid = 'ctl00_ContentPlaceHolder1_lvApproveCourse_' + head;
                    list = 'lvApproveCourse';
                }

                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                //if (chkcnt > 0)
                //    document.getElementById(headid).checked = true;
                //else
                //    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }
        function showConfirm() {
            var ret = false;
            var msg = '';
            if ($('#ctl00_ContentPlaceHolder1_ddlCollege').val() <= 0)
                msg += 'Please Select School/Institute \n';
            if ($('#ctl00_ContentPlaceHolder1_ddlSession').val() <= 0)
                msg += 'Please Select Session \n';
            if ($('#ctl00_ContentPlaceHolder1_ddlDegree').val() <= 0)
                msg += 'Please Select Degree \n';
            if ($('#ctl00_ContentPlaceHolder1_ddlBranch').val() <= 0)
                msg += 'Please Select Branch \n';
            if ($('#ctl00_ContentPlaceHolder1_ddlFilter').val() <= 0)
                msg += 'Please Select Filter by Registration Status \n';

            if (msg != '') {
                alert(msg);
                return false;
            }

            tbl = document.getElementById('tblApproveCourse');
            list = 'lvApproveCourse';
            var dataRows = tbl.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    var chk = $('#ctl00_ContentPlaceHolder1_lvApproveCourse_ctrl' + i + '_cbApprove').prop('checked');
                    if (chk = true) {
                        ret = true;
                        break;
                    }
                }
            }

            if (ret == false) {
                alert('Please Select at least One Student.');
                return false;
            }

            ret = confirm('Do you Really want to Confirm/Submit this Course for Course Approval?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function validateAssign() {
            debugger;
            var numberOfChecked = $('[id*=tblCurrentSubjects] input:checkbox:checked').length;

            if (numberOfChecked == 0)
                numberOfChecked = $('[id*=tblUniCoreSub] input:checkbox:checked').length;

            if (numberOfChecked == 0)
                numberOfChecked = $('[id*=tblGlobalSubjects] input:checkbox:checked').length;

            if (numberOfChecked == 0)
                numberOfChecked = $('[id*=tblValueAddedGroup] input:checkbox:checked').length;

            if (numberOfChecked == 0) {
                alert('Please select atleast one course from the course list for course registration..!!');
                return false;
            }
            else {

                var regcredits = $('#ctl00_ContentPlaceHolder1_lblTotalRegCredits').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblTotalRegCredits]").text();
                var maxcredits = $('#ctl00_ContentPlaceHolder1_lblOfferedRegCredits').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblOfferedRegCredits]").text();
                var mincredits = $('#ctl00_ContentPlaceHolder1_lblOfferedRegCreditsFrom').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblOfferedRegCreditsFrom]").text();
                if ((parseFloat(maxcredits) >= parseFloat(regcredits) && parseFloat(mincredits) <= parseFloat(regcredits)) == false) {
                    alert("Total register credits should be between Minimum Credits Limit and Maximum Credits Limit.");
                    return false;
                }
                else {
                    if (confirm('Are you sure you want to register and approved for the selected courses?')) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
            return false;
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

</asp:Content>
