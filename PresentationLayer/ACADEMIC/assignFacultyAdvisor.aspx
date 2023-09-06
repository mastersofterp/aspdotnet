<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="assignFacultyAdvisor.aspx.cs" Inherits="assignFacultyAdvisor" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
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

    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--   <div class="nav-tabs-custom" id="Tabs">--%>
                            <%--<ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Class Advisor</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Faculty Advisor</a>
                                    </li>
                                </ul>--%>
                            <%-- <div class="tab-content" id="my-tab-content">--%>
                            <div class="tab-pane active" id="tab_1">
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>School/Institute</label>--%>
                                                        <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeNameClass" runat="server" TabIndex="1" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        ValidationGroup="Branch" ToolTip="Please Select School/Institute Name" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollegeNameClass"
                                                        Display="None" ErrorMessage="Please Select School/Institute Name " ValidationGroup="Show"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCollegeNameClass"
                                                        Display="None" ErrorMessage="Please Select School/Institute Name " ValidationGroup="Excel"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Department</label>--%>
                                                        <asp:Label ID="lblDYddlDeptName_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAssignDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeptNameClass"
                                                                Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Show">
                                                            </asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAssignDept"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Show">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree</label>--%>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegreeClass" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="2" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDegreeClass"
                                                        Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="Show">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDegreeClass"
                                                        Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Programme/Branch</label>--%>
                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranchClass" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlBranchClass"
                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="Show">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlBranchClass"
                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Semester</label>--%>
                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemesterClass" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSemesterClass"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="Show">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSemesterClass"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>&nbsp;  </sup>
                                                        <%--<label>Section</label>--%>
                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSectionClass" runat="server" AppendDataBoundItems="true" TabIndex="5" OnSelectedIndexChanged="ddlSectionClass_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlSectionClass"
                                                                Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="Show">
                                                            </asp:RequiredFieldValidator>--%>
                                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSectionClass"
                                                                Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="SubmitFA">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>&nbsp;  </sup>
                                                        <%--<label>Section</label>--%>
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true">Admission Batch</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="6" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShowClass" runat="server" OnClick="btnShow_Click" TabIndex="6" Text="Show Student"
                                                ValidationGroup="Show" CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="Show" />
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Department</label>--%>
                                                        <asp:Label ID="lblDYddlDeptName_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDeptNameClass" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeptNameClass"
                                                                Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Show">
                                                            </asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDeptNameClass"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Class Advisor</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdvisorClass" runat="server" AppendDataBoundItems="true" CausesValidation="True" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlAdvisorClass" Display="None" ErrorMessage="Please Select Class Advisor" InitialValue="0" ValidationGroup="Submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Total Selected Student</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotStud" runat="server" CssClass="form-control" Enabled="False" ValidationGroup="courseLink" />
                                                    <asp:HiddenField ID="hdnstud" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdnStudId" runat="server" Value="0" />
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnClassAdvisor" runat="server" CssClass="btn btn-primary" OnClick="btnAssignFA0_Click" TabIndex="6" Text="Assign Class Advisor" ValidationGroup="Submit" />
                                                    <asp:Button ID="btnReportClass" runat="server" Class="btn btn-info" CssClass="btn btn-primary" OnClick="btnPrint_Click" TabIndex="7" Text="Report(Excel)" ValidationGroup="Excel" />
                                                    <asp:Button ID="blnCancelClass" runat="server" CausesValidation="False" Class="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" Text="Cancel" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Submit" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Excel" />
                                                </div>
                                            </div>
                                        </div>

            <%--                            <div class="col-12 btn-footer">
                                            <asp:Label ID="Label1" runat="server" SkinID="Msglbl" />
                                        </div>--%>

                                        <div class="col-12">
                                            <asp:ListView ID="lvClassAdv" runat="server" OnItemDataBound="lvFaculty_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="exampleCls">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjectsCA(this)" />
                                                                </th>
                                                                <th>REG NO. </th>                                                                
                                                                <th>Name </th>
                                                                <th>Branch </th>
                                                                <th>Section</th>
                                                                <th>CA Name </th>
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
                                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjectsCA(this)" ToolTip='<%# Eval("IDNO")%>' />
                                                            <asp:Label ID="lblIdNo" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td><%# Eval("REGNO")%></td>
                                                        <td><%# Eval("NAME")%></td>
                                                        <td><%# Eval("BRANCH")%></td>
                                                        <td><%# Eval("SECTIONNAME")%></td>
                                                        <td><%# Eval("CLS_ADV_TEACHER_NAME")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_2" style="display: none">
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>School/Institute Name</label>--%>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="1" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        ValidationGroup="Branch" ToolTip="Please Select School/Institute Name" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlCollegeName"
                                                        Display="None" ErrorMessage="Please Select School/Institute Name " ValidationGroup="ShowFA"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree</label>--%>
                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                                        TabIndex="2" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="ShowFA">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="SubmitFA">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                      
                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="ShowFA">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvBranch2" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="SubmitFA">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Semester</label>--%>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="ShowFA">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="SubmitFA">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>&nbsp; </sup>
                                                        <%-- <label>Section</label>--%>
                                                        <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSectionFA" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSectionFA"
                                                                Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="ShowFA">
                                                            </asp:RequiredFieldValidator>--%>
                                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSectionFA"
                                                                Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="SubmitFA">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="6" Text="Show Student"
                                                ValidationGroup="ShowFA" CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="ShowFA" />
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Department</label>--%>
                                                        <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDeptName" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--   <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDeptName"
                                                                Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="ShowFA">
                                                            </asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDeptName"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="SubmitFA">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Faculty Advisor</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdvisor" runat="server" AppendDataBoundItems="true" CausesValidation="True" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAdvisor" runat="server" ControlToValidate="ddlAdvisor" Display="None" ErrorMessage="Please Select Faculty Advisor" InitialValue="0" ValidationGroup="SubmitFA">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>FA Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CausesValidation="True" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSection" Display="None" ErrorMessage="Please Select FA Section" InitialValue="0" ValidationGroup="SubmitFA">
                                                            </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Total Selected Student</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotChk" runat="server" CssClass="form-control" Enabled="False" ValidationGroup="courseLink" />
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnAssignFA0" runat="server" CssClass="btn btn-primary" OnClick="btnAssignFA0_Click" TabIndex="6" Text="Assign FA" ValidationGroup="SubmitFA" />
                                                    <asp:Button ID="btnPrint" runat="server" Class="btn btn-info" CssClass="btn btn-primary" OnClick="btnPrint_Click" TabIndex="7" Text="Report" ValidationGroup="SubmitFA" />
                                                     <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" OnClick="btnReport_Click" TabIndex="8" Text="Overall Report" />
                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Class="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" Text="Cancel" />
                                                    <asp:ValidationSummary ID="ValSubmit" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="SubmitFA" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblStatus2" runat="server" SkinID="Msglbl" />
                                        </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvFaculty" runat="server" OnItemDataBound="lvFaculty_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>Roll No</th>
                                                                <th>Name </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <%-- <th>FA Section</th>--%>
                                                                <th>FA Name </th>
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
                                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' />
                                                            <asp:Label ID="lblIdNo" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td><%# Eval("REGNO")%></td>
                                                        <td><%# Eval("ROLLNO")%></td>
                                                        <td><%# Eval("NAME")%></td>
                                                        <td><%# Eval("BRANCH")%></td>
                                                        <%--  <td><%# Eval("FAC_SEC")%></td>--%>
                                                        <td><%# Eval("FAC_TEACHER_NAME")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <%--          </div>--%>
                            <%-- </div>--%>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="TabName" runat="server" />
                <asp:HiddenField ID="hdnOrg" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="ddlCollegeName" />
            <asp:PostBackTrigger ControlID="ddlDegree" />
            <asp:PostBackTrigger ControlID="ddlBranch" />
            <asp:PostBackTrigger ControlID="ddlDeptName" />
            <asp:PostBackTrigger ControlID="ddlSemester" />
            <asp:PostBackTrigger ControlID="ddlAdvisor" />
            <asp:PostBackTrigger ControlID="btnAssignFA0" />
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnReportClass" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            var lbl = document.getElementById('<%= lblDynamicPageTitle.ClientID %>').innerText;
            //alert(lbl);
            if (lbl.indexOf('Class') > -1 || lbl.indexOf('CLASS') > -1) {
                //if (lbl == "CLASS ADVISOR") {
                $('#tab_1').show();
                $('#tab_2').hide();
            }
            else {
                $('#tab_1').hide();
                $('#tab_2').show();
            }
        });

    </script>
    <%-- <script>
        $(document).ready(function () {
            var hdnorg = document.getElementById('<%= hdnOrg.ClientID %>');
            if (hdnorg.value == 1) {
                var tabName = "tab_2";
                $('#Tabs a[href="#' + tabName + '"]').tab('show');
                $('#Tabs a:first').hide()
                //$('#tabs li > a[data_id=3]').parent().removeClass('active').css('display', 'none');
            }
        });

    </script>--%>
    <script language="javascript" type="text/javascript">
        function totSubjects(chk) {
            var chkID = document.getElementById(chk.id).parentElement;
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');
            var hdnStudId = document.getElementById('<%= hdnStudId.ClientID %>');
            var tooltip = chkID.title;
            if (chk.checked == true) {
                txtTot.value = Number(txtTot.value) + 1;
                if (hdnStudId.value != 0) {
                    hdnStudId.value = hdnStudId.value + ',' + tooltip;
                }
                else {
                    hdnStudId.value = tooltip;
                }
            }
            else {
                txtTot.value = Number(txtTot.value) - 1;
                var str = hdnStudId.value;
                hdnStudId.value = str.replace(tooltip, '');
            }
            hdnStudId.value = hdnStudId.value + ',';
        }

        function totSubjectsCA(chk) {

            var chkID = document.getElementById(chk.id).parentElement;
            var txtTotStud = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdnStudId = document.getElementById('<%= hdnStudId.ClientID %>');

            var tooltip = chkID.title;
            //alert(tooltip);
            //var lbl = document.getElementById("ctl00_ContentPlaceHolder1_lvClassAdv_ctrl" + i + "_lblIdNo");

            if (chk.checked == true) {
                txtTotStud.value = Number(txtTotStud.value) + 1;
                if (hdnStudId.value != 0) {
                    hdnStudId.value = hdnStudId.value + ',' + tooltip;
                }
                else {
                    hdnStudId.value = tooltip;
                }
            }
            else {
                txtTotStud.value = Number(txtTotStud.value) - 1;

                var str = hdnStudId.value;
                //alert(tooltip);
                hdnStudId.value = str.replace(tooltip, '');
            }
            hdnStudId.value = hdnStudId.value + ',';
        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');

            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var hdnStudId = document.getElementById('<%= hdnStudId.ClientID %>');
            var rows = document.getElementById("example").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            for (i = 0; i < rows; i++) {
                var ee = document.getElementById("ctl00_ContentPlaceHolder1_lvFaculty_ctrl" + i + "_cbRow").parentElement;
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvFaculty_ctrl" + i + "_cbRow");
                //var lbl = document.getElementById("ctl00_ContentPlaceHolder1_lvFaculty_ctrl" + i + "_lblIdNo");
                var tooltip = ee.title;
                //alert(tooltip)
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        //alert(lbl.innerText);
                        e.checked = true;

                        if (hdnStudId.value != 0) {
                            hdnStudId.value = hdnStudId.value + ',' + tooltip;
                        }
                        else {
                            hdnStudId.value = tooltip;
                        }
                    }
                    else {
                        e.checked = false;
                        hdnStudId.value = "0";
                    }
                }
            }
            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
            }
            else {
                txtTot.value = 0;
            }
            hdnStudId.value = hdnStudId.value + ',';
        }
        function totAllSubjectsCA(headchk) {

            var txtTotStud = document.getElementById('<%= txtTotStud.ClientID %>');

            var hdnstud = document.getElementById('<%= hdnstud.ClientID %>');
            var hdnStudId = document.getElementById('<%= hdnStudId.ClientID %>');
            var rows = document.getElementById("exampleCls").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            for (i = 0; i < rows; i++) {
                var ee = document.getElementById("ctl00_ContentPlaceHolder1_lvClassAdv_ctrl" + i + "_cbRow").parentElement;
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvClassAdv_ctrl" + i + "_cbRow");
                var tooltip = ee.title;

                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        if (hdnStudId.value != 0) {
                            hdnStudId.value = hdnStudId.value + ',' + tooltip;
                        }
                        else {
                            hdnStudId.value = tooltip;
                        }

                    }
                    else {
                        e.checked = false;
                        hdnStudId.value = "0";
                    }
                }
            }
            //alert(hdnStudId.value);
            if (headchk.checked == true) {

                txtTotStud.value = hdnstud.value;
            }
            else {

                txtTotStud.value = 0;
            }
            hdnStudId.value = hdnStudId.value + ',';
        }
    </script>
    <%-- <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example2').DataTable({
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]

            });
        }

    </script>--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

