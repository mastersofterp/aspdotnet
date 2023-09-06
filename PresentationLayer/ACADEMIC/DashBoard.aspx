<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="ACADEMIC_DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel1"
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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ALL EXAM MARK ENTRY STATUS</h3>
                </div>
                <asp:UpdatePanel ID="updPanel1" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>School / Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" TabIndex="1" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolInstitute_OnSelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchoolInstitute" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="Show" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ReportShow"></asp:RequiredFieldValidator>--%>
                                    <%--</div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Session Name" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsess" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ReportShow"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Pattern</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPattern" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Pattern Name" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlPattern_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPattern" runat="server" ControlToValidate="ddlPattern"
                                            Display="None" ErrorMessage="Please Select Pattern" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPattern"
                                            Display="None" ErrorMessage="Please Select Pattern" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ReportShow"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="ReportShow"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSubExam" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sub Exam</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExam" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubExam" runat="server" ControlToValidate="ddlSubExam"
                                            Display="None" ErrorMessage="Please Select Sub Exam" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubExam"
                                        Display="None" ErrorMessage="Please Select Sub Exam" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="ReportShow"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Mark Entry Status" TabIndex="1"
                                    ValidationGroup="Show" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnExcel" runat="server" Text="Mark Entry Status(Excel)" CssClass="btn btn-info" TabIndex="1"
                                    ValidationGroup="ReportShow" OnClick="btnExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ReportShow" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                            <div class="form-group col-lg-8 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>School/Institute Name & Sub Exam Selection Is Not Mandatory For Marks Entry Status(Excel)</span></p>
                                </div>
                            </div>
                            <div class="col-12" runat="server" id="divExamMarksEntryStatus" visible="false">
                                <div class="sub-heading">
                                    <h5>Examination Mark Entry Status</h5>
                                </div>
                                <asp:Panel ID="pnlInfo" runat="server">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <table id="tblStudent" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Programme/Branch
                                                        </th>
                                                        <th>Course Code
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Faculty Name
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Appeared
                                                        </th>
                                                        <th>Mark Done
                                                        </th>
                                                        <th>Mark Not Done
                                                        </th>
                                                        <th>Mark Done Per
                                                        </th>
                                                        <th>Mark Not Done Per
                                                        </th>
                                                        <th>Lock Done
                                                        </th>
                                                        <th>Lock Not Done
                                                        </th>
                                                        <th>Lock Done Per
                                                        </th>
                                                        <th>Lock Not Done Per
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
                                                    <%# Container.DataItemIndex +1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPARTMENT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BRANCH") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("APPEARED_STUD") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MRKENTRYDONE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MRKENTRYNOTDONE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MRK_DONE_PER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MRK_NOTDONE_PER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("LOCKDONE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("LOCKNOTDONE") %>
                                                </td>
                                                <td>
                                                    <%#Eval ("LDONE_PER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("LNOTDONE_PER") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

