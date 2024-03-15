<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="coursemaster.aspx.cs" Inherits="Administration_courseMaster" Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="UPDROLE" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- <asp:HiddenField ID="hidTAB" runat="server" ClientIDMode="Static" />--%>
            <div class="row" id="divMain" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Course Creation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Import Course Data</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdCCreation" runat="server" AssociatedUpdatePanelID="UPDCOURSE"
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
                                        <asp:UpdatePanel ID="UPDCOURSE" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />--%>
                                                <div class="box-body">

                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Selection Criteria</h5>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Degree </label>--%>
                                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged1">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="modify" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Department Name </label>--%>
                                                                        <asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged1">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDept"
                                                                        Display="None" ErrorMessage="Please Select Department Name" InitialValue="0" ValidationGroup="submit" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDept"
                                                                        Display="None" ErrorMessage="Please Select Department Name" InitialValue="0"
                                                                        ValidationGroup="modify" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Programme/Branch </label>--%>
                                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlBranch"
                                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0"
                                                                        ValidationGroup="modify" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlBranch"
                                                                        Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Path/Scheme </label>--%>
                                                                        <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                                                        Display="None" ErrorMessage="Please Select Scheme/Path" InitialValue="0" ValidationGroup="submit" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlScheme"
                                                                        Display="None" ErrorMessage="Please Select Scheme/Path" InitialValue="0" ValidationGroup="modify" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%-- <label>Semester </label>--%>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSem"
                                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSem"
                                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="modify" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Existing Courses / Paper </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlExtCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlExtCourse_SelectedIndexChanged"
                                                                        Enabled="False">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExtCourse"
                                                                        Display="None" ErrorMessage="Please Select Existing Course" InitialValue="0"
                                                                        ValidationGroup="modify" />
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" />
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="ModifyCourse" runat="server" OnClick="btnModifyCourse_Click" Text="Modify Existing Course"
                                                                    ValidationGroup="modify" CssClass="btn btn-primary" />
                                                                <asp:Button ID="btnCheckListReport" runat="server" Text="Course List Report" CssClass="btn btn-info"
                                                                    OnClick="btnCheckListReport_Click" />
                                                                <asp:Button ID="btnreportnew" runat="server" CausesValidation="false" OnClick="btnreportnew_Click"
                                                                    Text="Report (Excel)" CssClass="btn btn-info" />
                                                                <asp:Button ID="btnReset" runat="server" CausesValidation="false" OnClick="btnReset_Click"
                                                                    Text="Reset" CssClass="btn btn-warning" />
                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="modify" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="sub-heading">
                                                                <h5>Pre-Defined Mark Pattern for Respective Exams</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                <asp:Repeater ID="rtpScheme" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Marks</h5>
                                                                        </div>
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="display: none">Exam
                                                                                </th>

                                                                                <th>Exam Name
                                                                                </th>
                                                                                <th>Passing marks
                                                                                </th>
                                                                                <th>Maximum Marks
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>

                                                                            <td style="display: none">
                                                                                <asp:Label ID="lblFldName" Text=' <%# Eval("FLDNAME")%>' ToolTip=' <%# Eval("EXAMNO")%>'
                                                                                    runat="server"></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lblExamName" runat="server" Text='<%# Eval("EXAMNAME")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMinMarks" runat="server" TextMode="SingleLine" MaxLength="4"
                                                                                    Text='<%#Eval("MIN") %>' CssClass="form-control"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMinMarks" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                                                    TargetControlID="txtMinMarks">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMaxMarks" runat="server" TextMode="SingleLine" MaxLength="4"
                                                                                    Text='<%#Eval("MAX") %>' CssClass="form-control"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                                                    TargetControlID="txtMaxMarks">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 btn-footer" runat="server" id="trbtn">
                                                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="False" Text="Update" CssClass="btn btn-primary"
                                                            ToolTip="Update Default Marks" Visible="true" OnClick="btnUpdate_Click" />
                                                        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CausesValidation="False"
                                                            OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg" />
                                                    </div>

                                                    <asp:Panel ID="pnl_course" runat="server">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Course Details</h5>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Course Code </label>--%>
                                                                        <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCCode" runat="server" MaxLength="15" CssClass="form-control" OnTextChanged="txtCCode_TextChanged" AutoPostBack="true" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCCode"
                                                                        Display="None" ErrorMessage="Please Enter Course Code" ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%-- <label>Course Name </label>--%>
                                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCourseName" runat="server" MaxLength="150" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCourseName"
                                                                        Display="None" ErrorMessage="Please Enter Course Name" ValidationGroup="submit" />
                                                                </div>


                                                                <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <label>Course Short Name</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCourseshortname" runat="server" MaxLength="150" CssClass="form-control" />
                                                                    <%--      <asp:RequiredFieldValidator ID="rfvshortname" runat="server" ControlToValidate="txtCourseshortname"
                                                Display="None" ErrorMessage="Please Enter Course ShortName" ValidationGroup="submit" />--%>
                                                                </div>



                                                                <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <label>Specialization </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSpecialisation" runat="server" AppendDataBoundItems="true"
                                                                        CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Lecture </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtLectures" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                                        CssClass="form-control" />
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtLectures"
                                                                        Display="None" ErrorMessage="Please Enter Numeric Value for Lectures" Operator="DataTypeCheck"
                                                                        Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Tutorial </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTutorial" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                                        CssClass="form-control" />
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTutorial"
                                                                        Display="None" ErrorMessage="Please Enter Numeric Value for Tutorial" Operator="DataTypeCheck"
                                                                        Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Practical </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtPract" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                                        CssClass="form-control" />
                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtPract"
                                                                        Display="None" ErrorMessage="Please Enter Numeric Value for Practical" Operator="DataTypeCheck"
                                                                        Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Drawing </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDrawing" runat="server" MaxLength="3" onblur="AddLTP();"
                                                                        onkeyup="validateNumeric(this);" CssClass="form-control" />
                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server"
                                                                        ControlToValidate="txtDrawing" Display="None"
                                                                        ErrorMessage="Please Enter Numeric Value for Practical"
                                                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Total </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTotal" runat="server" Enabled="false" MaxLength="3" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <%--<label>Course Type </label>--%>
                                                                        <span style="color: red">*</span>
                                                                        <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlTP" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTP" runat="server" ControlToValidate="ddlTP"
                                                                        Display="None" ErrorMessage="Please Select Course Type" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Academic Council/ BoS Dept. </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlParentDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlParentDept"
                                                                        Display="None" ErrorMessage="Please Select Parent Department" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <%--<label>Semester </label>--%>
                                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Elective </label>
                                                                    </div>
                                                                    <asp:CheckBox ID="chkElective" runat="server" OnClick="ShowHideElecGroup(this);" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <label>Elective Group </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlElectiveGroup" runat="server" Enabled="false" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Global Elective </label>
                                                                    </div>
                                                                    <asp:CheckBox ID="chkGlobal" Checked="false" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Value Added</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="ChkValueAdded" Checked="false" runat="server" CssClass="form-control" />
                                                                </div>


                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Specialization</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="ChkSpecialization" Checked="false" runat="server" CssClass="form-control" />
                                                                </div>



                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>
                                                                            Consider For FeedBack
                                                                        </label>
                                                                    </div>
                                                                    <asp:CheckBox ID="ChkIsFeedBack" Checked="false" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Audit</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="chkAudit" Checked="false" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <%-- <label>Course Category </label>--%>
                                                                        <asp:Label ID="lblDYddlCourseCatg" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCElectiveGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>



                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Paper Hrs. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtPaper" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                                        MaxLength="2">
                                                                    </asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Credits </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTheory" runat="server" MaxLength="3" CssClass="form-control" />
                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtTheory"
                                                                        Display="None" ErrorMessage="Please Enter Numeric Value for Credit" Operator="DataTypeCheck"
                                                                        Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTheory"
                                                                        Display="None" ErrorMessage="Please Enter Credits"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                    <div class="label-dynamic">
                                                                        <label>Course Total Marks </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCourseTotalMarks" runat="server" MaxLength="5" CssClass="form-control" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ft" runat="server" ValidChars="01234567890."
                                                                        FilterMode="ValidChars" TargetControlID="txtCourseTotalMarks">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                    <div class="label-dynamic">
                                                                        <label>Prerequisite Credit Required </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtPreCredit" runat="server" MaxLength="3" CssClass="form-control" onkeyup="validateNumeric(this);" Visible="false" />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPreCredit"
                                                                        Display="None" ErrorMessage="Please Enter Numeric Value for Prerequisite Credit"
                                                                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="submit"></asp:CompareValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Ref. Material </label>
                                                                    </div>
                                                                    <asp:FileUpload ID="refMaterial" runat="server" ValidationGroup="submit" ToolTip="Select file to upload" />
                                                                    <asp:Button ID="btnUpload" runat="server" ValidationGroup="submit" OnClick="btnUpload_Click"
                                                                        Text="Upload" ToolTip="Click to Upload" CssClass="mt-1" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel3" runat="server">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <asp:Repeater ID="lvCourseMaterial" runat="server">
                                                                        <HeaderTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Course Material</h5>
                                                                            </div>
                                                                            <thead class="bg-light blue">
                                                                                <tr>
                                                                                    <th>Action
                                                                                    </th>
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%#Eval("courseno") %>' ToolTip='<%# Eval("FILENAME") %>'
                                                                                        OnClick="btnDelete_Click" />
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("FILENAME") %>
                                                                                </td>

                                                                                <asp:UpdatePanel runat="server" ID="upddownload">
                                                                                    <ContentTemplate>
                                                                                        <td>
                                                                                            <asp:Button ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("FILENAME") %>' OnClick="btnDownload_Click" CssClass="btn btn-primary" />
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:PostBackTrigger ControlID="btnDownload" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnShow" runat="server" CausesValidation="False" OnClientClick="return ShowHideMarks();"
                                                                Text="Pre-Defined Mark" class="btn btn-primary" Visible="false" />
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                                ValidationGroup="submit" class="btn btn-primary" />
                                                            <asp:Button ID="btnCancel" class="btn btn-warning"
                                                                runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlPreCorList" runat="server">
                                                                <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Prerequisite Course List</h5>
                                                                        </div>
                                                                        <asp:Panel ID="Panel1" runat="server">
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Select</th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>Lecture</th>
                                                                                        <th>Tutorial</th>
                                                                                        <th>Practical</th>
                                                                                        <th>Total Marks </th>
                                                                                        <th>Total Credits</th>
                                                                                        <th>External MaxMarks</th>
                                                                                        <th>External MinMarks</th>
                                                                                        <%-- <th>PM</th>--%>
                                                                                        <th>Internal MaxMarks </th>
                                                                                        <th>Internal MinMarks</th>


                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("COURSENO")%>' />
                                                                                <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("COURSENO")%>' Visible="false" />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CCode")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COURSE_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SUBNAME")%>
                                                                            </td>
                                                                            <td><%# Eval("LECTURE")%></td>
                                                                            <td><%# Eval("THEORY")%></td>
                                                                            </td>
                                                    <td><%# Eval("PRACTICAL")%></td>
                                                                            </td>
                                                    <td>
                                                        <%# Eval("TOTAL_MARKS")%>
                                                    </td>
                                                                            <td>
                                                                                <%# Eval("CREDITS")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ESEM_MAX")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ESEM_MIN")%>
                                                                            </td>
                                                                            <%-- <td>
                                                        <%# Eval("MINMARK")%>
                                                    </td>--%>
                                                                            <td>
                                                                                <%# Eval("MAXMARKS_I")%>
                                                                            </td>
                                                                            <td><%# Eval("MINMARK_I")%></td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="form-group col-lg-10 col-md-12 col-12">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note</h5>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Please check the Global Elective Checked Box If the elective group courses belong to Open Electives.</span>  </p>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div id="divMsg" runat="server">
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                                <asp:PostBackTrigger ControlID="ModifyCourse" />
                                                <asp:PostBackTrigger ControlID="btnCheckListReport" />
                                                <asp:PostBackTrigger ControlID="btnReset" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateImport" runat="server" AssociatedUpdatePanelID="updpnlImportData"
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
                                        <asp:UpdatePanel ID="updpnlImportData" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Import Course Data</h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-12">
                                                            <div class=" note-div">
                                                                <h5 class="heading">Note</h5>
                                                                <p style="color: red"><i class="fa fa-star" aria-hidden="true"></i><span>Kindly ensure that Branch, Department, Scheme and Elective master entries are available before clicking "Download Blank Excel Sheet".</span></p>
                                                            </div>
                                                        </div>

                                                        <%--<asp:Panel ID="pnl" runat="server">
                                                        --%>
                                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Admission Batch</label>--%>
                                                        <%--  <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control"  AutoPostBack="true"
                                                data-select2-enable="true" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Attach Excel File</label>
                                                            </div>
                                                            <asp:FileUpload ID="FUFile" runat="server" ToolTip="Select file to upload" TabIndex="1" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divRecords" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Already Saved Records</label>
                                                            </div>
                                                            <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-info" TabIndex="2"
                                                        Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" Enabled="true" OnClick="btnExport_Click"><i class="fa fa-file-excel-o"></i> Download Blank Excel Sheet</asp:LinkButton>

                                                    <asp:LinkButton ID="btnUploadexcel" runat="server" ValidationGroup="report" CssClass="btn btn-primary" TabIndex="3"
                                                        Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true" OnClick="btnUploadexcel_Click"><i class="fa fa-upload" ></i> Upload Excel</asp:LinkButton>
                                                    <%--   <asp:HiddenField ID="TabName" runat="server" />--%>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="report" Style="text-align: center" />
                                                </div>

                                                <div class="form-group col-12" id="divNote" runat="server" visible="false">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Excel Sheet Data is not imported, Please correct following data and upload the Excel again.</span></p>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:ListView ID="lvStudData" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Course List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr. No.</th>
                                                                        <th>Course Name
                                                                        </th>
                                                                        <th>ShortName
                                                                        </th>
                                                                        <th>Course Code
                                                                        </th>
                                                                        <th>Credits
                                                                        </th>
                                                                        <th>Subject Type                                                      
                                                                        </th>
                                                                        <th>Semester                                                      
                                                                        </th>
                                                                        <th>IsElective
                                                                        </th>
                                                                        <th>Scheme
                                                                        </th>
                                                                        <th>Department Name
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
                                                                <td><%# Container.DataItemIndex +1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblcourseName" runat="server" Text='<%# Eval("COURSENAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblshortname" runat="server" Text='<%# Eval("SHORTNAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("COURSECODE")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("CREDITS")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblsubname" runat="server" Text='<%# Eval("SUBJECTTYPE")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblelec" runat="server" Text='<%# Eval("ISELECTIVE")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbldept" runat="server" Text='<%# Eval("BOS_DEPT")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>

                                                <%--   </asp:Panel>--%>
                                                <%--     </div>--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUploadexcel" />
                                                <asp:PostBackTrigger ControlID="btnExport" />
                                                <asp:PostBackTrigger ControlID="btnreportnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function check() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtCCode").disabled = true;
            document.getElementById("ctl00_ContentPlaceHolder1_txtCourseName").disabled = true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ScalingZero(txt) {

            if (txt.value == '') {
                document.getElementById('ctl00_ContentPlaceHolder1_txtScaling').value = 0;
            }

        }
        function ShowHideMarks() {
            if (document.getElementById('pnlMarks').style.display == 'none')
                document.getElementById('pnlMarks').style.display = 'block';
            else
                document.getElementById('pnlMarks').style.display = 'none';

            return false;
        }

        function AddLTP() {
            var lec = document.getElementById("<%= txtLectures.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtLectures.ClientID %>").value;
            var tut = document.getElementById("<%= txtTutorial.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtTutorial.ClientID %>").value;
            var prac = document.getElementById("<%= txtPract.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtPract.ClientID %>").value;
            var draw = document.getElementById("<%= txtDrawing.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtDrawing.ClientID %>").value;
            //var total = document.getElementById("<%= txtTotal.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtTotal.ClientID %>").value;
            //total = parseInt(lec) + parseInt(tut) + parseInt(prac);

            document.getElementById("<%= txtTotal.ClientID %>").value = parseInt(lec) + parseInt(tut) + parseInt(prac) + parseInt(draw);
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function ShowHideElecGroup(chk) {
            debugger;
            if (chk.checked) {
                document.getElementById('<%= ddlElectiveGroup.ClientID %>').disabled = false;
                document.getElementById('<%= chkGlobal.ClientID %>').disabled = true;
                document.getElementById('<%= chkGlobal.ClientID %>').checked = false;
                document.getElementById('<%= chkGlobal.ClientID %>').disabled = false;
            }
            else {

                document.getElementById('<%= ddlElectiveGroup.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= ddlElectiveGroup.ClientID %>').disabled = true;
                document.getElementById('<%= chkGlobal.ClientID %>').disabled = true;
                document.getElementById('<%= chkGlobal.ClientID %>').checked = false;
            }
        }
    </script>

    <script>

        function tab() {
            $('#tab2').tab('show')
        };



    </script>



</asp:Content>
