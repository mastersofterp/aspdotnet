<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Minor_Allotment.aspx.cs" Inherits="ACADEMIC_Minor_Allotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAllot"
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
        <%--AssociatedUpdatePanelID="updBulkReg"--%>
    </div>

    <asp:UpdatePanel ID="updAllot" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Minor Allotment </h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom ml-md-2">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Minor Allotment</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Minor Course Registration</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <div id="tab_1" class="tab-pane active">
                                        <div>
                                            <asp:UpdatePanel ID="updMinor" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>School</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSchool" runat="server" Display="None" ControlToValidate="ddlSchool" ErrorMessage="Please Select School!!!" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" Display="None" ControlToValidate="ddlDegree" ErrorMessage="Please Select Degree!!!" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Program</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" Display="None" ControlToValidate="ddlBranch" ErrorMessage="Please Select Branch!!!" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="4" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" Display="None" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester!!!" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Minor</label>
                                                                </div>
                                                                <asp:ListBox ID="lstMinor" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="lstBoxlstMinor" runat="server" Display="None" ControlToValidate="lstMinor" ErrorMessage="Please Select Minor!!!" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="validationMinor" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="submit" DisplayMode="List" />
                                                    </div>

                                                    <div id="minorVis" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Minor</label>
                                                        </div>
                                                        <asp:ListBox ID="lstMinor" runat="server" SelectionMode="Multiple" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"></asp:ListBox>
                                                        <asp:RequiredFieldValidator ID="lstBoxlstMinor" runat="server" Display="None" ControlToValidate="lstMinor" ErrorMessage="Please Select Minor!!!" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="col-12 mt-5">
                                                        <%--<div class="sub-heading">
                                                            <h5>Student Details</h5>
                                                        </div>--%>
                                                        
                                                        <asp:HiddenField ID="hdnChk" runat="server" />
                                                        <asp:Panel ID="pnlMinor" runat="server" Visible="false">
                                                            <div class="sub-heading">
                                                                <h5>Student Details</h5>
                                                            </div>
                                                            <asp:ListView ID="lvMinor" runat="server" Visible="true" OnItemDataBound="lvMinor_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                        <table id="example" class="table table-striped table-bordered display" style="width: 100%;">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="CheckAll(this);" /></th>
                                                                                    <th>Registration Number</th>
                                                                                    <th>Student Name</th>
                                                                                    <th>Degree</th>
                                                                                    <th>Program</th>
                                                                                    <th>Minor</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkAllot" runat="server" Checked='<%#Eval("MNR_GRP_ALLOTED").ToString().Equals("True")?true:false %>' ToolTip='<%#Eval("IDNO")%>'/></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudId" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%#Eval("IDNO")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%#Eval("CDB_NO")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' ToolTip='<%#Eval("DEGREENO")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("LONGNAME") %>' ToolTip='<%#Eval("BRANCHNO")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblMinor" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div id="tab_2" class="tab-pane">
                                        <div>
                                            <asp:UpdatePanel ID="updCourse" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Session</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlminorSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlminorSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                 <asp:RequiredFieldValidator ID="rfvddlminorSession" runat="server" Display="None" ControlToValidate="ddlminorSession" ErrorMessage="Please Select Session!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>College & Program</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" Display="None" ControlToValidate="ddlCollege" ErrorMessage="Please Select College & Program!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Scheme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" Display="None" ControlToValidate="ddlScheme" ErrorMessage="Please Select Scheme!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Admission Batch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" Display="None" ControlToValidate="ddlAdmBatch" ErrorMessage="Please Select Admission Batch!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemesterminor" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemesterminor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSemesterminor" runat="server" Display="None" ControlToValidate="ddlSemesterminor" ErrorMessage="Please Select Semester!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Section</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSection" runat="server" Display="None" ControlToValidate="ddlSection" ErrorMessage="Please Select Section!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Total Student Selected</label>
                                                                </div>
                                                                <asp:TextBox ID="txtTotalStd" runat="server" CssClass="form-control" Enabled="false" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Minor</label>
                                                                </div>
                                                                <%--<asp:DropDownList ID="ddlCourseMinor" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                                <asp:ListBox ID="ddlCourseMinor" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="rfvddlCourseMinor" runat="server" Display="None" ControlToValidate="ddlCourseMinor" ErrorMessage="Please Select Minor!!!" InitialValue="" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Course Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCourseType" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">All Courses</asp:ListItem>
                                                                    <asp:ListItem Value="2">Offered Courses</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvddlCourseType" runat="server" Display="None" ControlToValidate="ddlCourseType" ErrorMessage="Please Select Course Type!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShow2" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow2_Click" />
                                                        <asp:Button ID="btnSubmit2" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit2" OnClick="btnSubmit2_Click" />
                                                        <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel2_Click" />
                                                        <asp:ValidationSummary ID="ValidateCourse" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="submit2" DisplayMode="List" />
                                                    </div>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <%--<div class="col-12">
                                                            <asp:Panel ID="pnlCRegistration" runat="server" Visible="false">--%>
                                                                <%--<div class="row">--%>
                                                                <div class="col-lg-6 col-md-6 col-6"> <%--col-lg-6 col-md-6--%>
                                                                    <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <asp:HiddenField ID="hdfsub" runat="server" />
                                                                        <asp:ListView ID="lvCourse" runat="server" Visible="true" >
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive">
                                                                                    <table id="exampleCourse" class="table table-striped table-bordered display">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th><asp:CheckBox ID="chkCAll" runat="server" onclick="SelectAll(this);" /></th>
                                                                                                <th>Registration Number</th>
                                                                                                <th>Student Name</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>

                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="chkCAllot" runat="server" Checked='<%#Eval("MNR_CSR_ALLOT").ToString().Equals("1")?true:false %>' Enabled='<%#Eval("MNR_CSR_ALLOT").ToString().Equals("1")?false:true %>' ToolTip='<%#Eval("CDB_NO")%>' onclick="totStudents(this);"/></td> <%--Checked='<%#Eval("MNR_GRP_ALLOTED").ToString().Equals("True")?true:false %>'--%>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCStudId" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%#Eval("IDNO")%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ></asp:Label></td>
                                                                                
                                                                                    <%--<td><asp:Label ID="lblMinor" runat="server"></asp:Label></td>--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>

                                                                <div class="col-lg-6 col-md-6 col-6"> <%--col-lg-6 col-md-6--%>
                                                                    <asp:Panel ID ="pnlSubject" runat="server" Visible="false">
                                                                        <div class="sub-heading">
                                                                            <h5>Subject List</h5>
                                                                        </div>

                                                                        <asp:ListView ID="lvSubject" runat="server" Visible="true" >
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive">
                                                                                    <table id="exampleSubject" class="table table-striped table-bordered display">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>Select <%--<asp:CheckBox ID="chkCAll" runat="server" />--%></th>
                                                                                                <th>Subject</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>

                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="chkSAllot" runat="server" /></td> <%--Checked='<%#Eval("MNR_GRP_ALLOTED").ToString().Equals("True")?true:false %>' ToolTip='<%#Eval("MNR_OFFERED_COURSE_NO")%>'--%>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%#Eval("COURSENO")%>'></asp:Label></td>

                                                                                   <%-- <td><asp:Label ID="lblCStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ></asp:Label></td>--%>
                                                                                
                                                                                    <%--<td><asp:Label ID="lblMinor" runat="server"></asp:Label></td>--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                                <%--</div>--%>
                                                            <%--</asp:Panel>
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="ddlSchool" />
            <asp:PostBackTrigger ControlID="ddlDegree" />
            <asp:PostBackTrigger ControlID="ddlBranch" />
            <asp:PostBackTrigger ControlID="ddlminorSession" />
            <asp:PostBackTrigger ControlID="ddlCollege" />
            <asp:PostBackTrigger ControlID="ddlAdmBatch" />
            <asp:PostBackTrigger ControlID="ddlSemesterminor" />
            <asp:PostBackTrigger ControlID="ddlSection" />
        </Triggers>--%>
    </asp:UpdatePanel>
    
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
  
    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            //debugger
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotalStd.ClientID %>');
            var hftotal = document.getElementById('<%= hdfsub.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftotal) ; i++) {
                
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_chkCAllot');
                if (lst.type == 'checkbox') {
                    //alert("hi");
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotalStd.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotalStd.ClientID %>').value = 0;
            }
            //		var frm = document.forms[0]
            //		for (i=0; i < document.forms[0].elements.length; i++) {
            //			var e = frm.elements[i];
            //			if (e.type == 'checkbox') {
            //			    if (headchk.checked == true) {
            //			        if (e.disabled == false) { e.checked = true; }
            //			    }
            //			    else
            //			        e.checked = false;
            //			}
            //        }
            //        if (headchk.checked == true) {
            //            txtTot.value = hftot.value;
            //            }
            //        else {
            //            txtTot.value = 0;
            //        }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotalStd.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }

        function totStudents(chk) {
            debugger
            var txtTot = document.getElementById('<%= txtTotalStd.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function CheckAll(headchk) {
            //alert("Hi");
            var count = 0;
            var hftot = document.getElementById('<%= hdnChk.ClientID %>').value;
            for (i = 0; i < Number(hftot) ; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvMinor_ctrl' + i + '_chkAllot');
                //alert("Hi");
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z ]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

</script>
</asp:Content>

