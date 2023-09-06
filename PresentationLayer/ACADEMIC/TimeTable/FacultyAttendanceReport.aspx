<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacultyAttendanceReport.aspx.cs" Inherits="ACADEMIC_TimeTable_FacultyAttendanceReport" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0">
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

    <asp:UpdatePanel ID="updAttStatus" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <form role="form">
                            <div class="box-body">

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="ddlSchoolInstitute" Display="None" ErrorMessage="Please Select College & Scheme."
                                                InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" SetFocusOnError="true" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session."
                                                InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server" ControlToValidate="ddlDepartment" Display="None" ErrorMessage="Please Select Department"
                                                InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" SetFocusOnError="true" ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester"
                                                InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" data-select2-enable="true"
                                                AutoPostBack="true" ValidationGroup="teacherallot" TabIndex="1" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSection" runat="server" SetFocusOnError="true" ControlToValidate="ddlSection" Display="None" ErrorMessage="Please Select Section"
                                                InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Subject</label>--%>
                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubject" Display="None"
                                                ErrorMessage="Please Select Subject" SetFocusOnError="true" InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divFaculty" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Faculty</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFaculty" Display="None"
                                                ErrorMessage="Please Select Faculty" SetFocusOnError="true" InitialValue="0" ValidationGroup="AttStatus">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnAttReport" runat="server" TabIndex="1" OnClick="btnAttReport_Click" CssClass="btn btn-primary" Text="Facultywise Attendance Status"
                                        ValidationGroup="AttStatus" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning" TabIndex="1" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="AttStatus" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlAttStatus" runat="server">
                                        <asp:ListView ID="lvAttStatus" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Faculty Attendance List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr. No.</th>
                                                            <th>Attendance Date</th>
                                                            <th>Slot</th>
                                                            <th>Topic</th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                            <th>Faculty Name</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow">
                                                    <td><%#Container.DataItemIndex+1 %></td>
                                                    <td><%#Eval("ATT_DATE") %></td>
                                                    <td><%#Eval("SLOT") %></td>
                                                    <td><%#Eval("TOPIC_COVERED") %></td>
                                                    <td><%#Eval("COURSE_NAME") %></td>
                                                    <td><%#Eval("UA_FULLNAME") %></td>
                                                    <td><%#Eval("STATUS_NAME") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </div>
                        </form>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
        </Triggers>

    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>
