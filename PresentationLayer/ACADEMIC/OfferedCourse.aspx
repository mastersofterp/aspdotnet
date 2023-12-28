<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OfferedCourse.aspx.cs" Inherits="ACADEMIC_OfferedCourse"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <link href="<%=Page.ResolveClientUrl("~/plugins/datatable-responsive/css/fixedColumns.dataTables.min.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/datatable-responsive/js/dataTables.fixedColumns.min.js")%>"></script>
    <style>
        .dataTables_filter {
            display: none !important;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upCourseRegistration" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="Label1" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Offered Courses</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Elective Courses Intake</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Copy Offered Courses</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

                                        <asp:UpdatePanel ID="updpnl" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--  <label>College & Scheme</label>--%>
                                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1" CssClass="form-control"
                                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                                Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="offered">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Session</label>--%>
                                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server"
                                                                ControlToValidate="ddlSession" Display="None" InitialValue="0"
                                                                ErrorMessage="Please Select Session" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divDegree" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlDegree" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvDegree" ControlToValidate="ddlDegree" Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="offered"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divBranch" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Programme/Branch</label>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvBranch" ControlToValidate="ddlBranch" Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="offered"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Scheme</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlScheme" runat="server"
                                                                AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server"
                                            ControlToValidate="ddlScheme" Display="None" InitialValue="0"
                                            ErrorMessage="Please Select Scheme" ValidationGroup="offered"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">

                                                                <label>Semester</label>
                                                            </div>

                                                            <asp:ListBox ID="ddlSemester" runat="server" SelectionMode="Multiple"
                                                                CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>


                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Course" ValidationGroup="offered" TabIndex="3"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnViewOfferedCourse" runat="server" OnClick="btnViewOfferedCourse_Click" Text="View Offered Course" ValidationGroup="offered" TabIndex="3"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnAd" runat="server" OnClick="btnAd_Click" Text="Submit for Offered Course" ValidationGroup="offered" TabIndex="3"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnSubmitUnoffered" runat="server" OnClick="btnSubmitUnoffered_Click" Text="Submit for Un-Offered Course" ValidationGroup="offered" TabIndex="3"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Report" TabIndex="3"
                                                        CssClass="btn btn-info" />
                                                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Offered Courses(Excel)" TabIndex="3"
                                                        CssClass="btn btn-info" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="3"
                                                        CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="offered" />
                                                </div>

                                                <div class="col-12" id="divCourseDetail" runat="server" visible="false">
                                                    <asp:Panel ID="pnlCourse" runat="server">
                                                        <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Course List</h5>
                                                                </div>
                                                                <%--  <div class="" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">--%>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblCourseLst">
                                                                    <thead class="bg-light-blue" style="top: -15px !important;">
                                                                        <tr>
                                                                            <%--<th>Offered</th>--%>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAll(this)" ToolTip="Select/Select all" />
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>Course Category</th>
                                                                            <th>Elective Group</th>
                                                                            <th>Credits</th>
                                                                            <th>Course Semester</th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>Seq. No.</th>
                                                                            <th>Course Registration
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                               <%-- </div>--%>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%--Enabled='<%# (Convert.ToInt32(Eval("REGISTERED") ) == 1 ?  false : true )%>'--%>
                                                                        <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' onClick="totStudents(this);" />
                                                                    </td>
                                                                    <td><%# Eval("CCODE")%></td>
                                                                    <td><%# Eval("COURSE_NAME")%></td>
                                                                    <td><%# Eval("ELECT_TYPE")%></td>
                                                                    <td><%# Eval("GROUPNAME")%></td>
                                                                    <td><%# Eval("CREDITS") %></td>
                                                                    <td><%# Eval("SEMESTERNAME") %></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlsem" runat="server" Visible="false" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                        <%-- Text='<% #Eval("SEMESTERNO")%>'--%>
                                                                        <asp:Label ID="LblSemNo" runat="server" Text='<% #Eval("CCODE")%>' ToolTip='<% #Eval("OFFERED")%>' Visible="false"></asp:Label>
                                                                        <asp:ListBox ID="lstbxSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                                                    </td>
                                                                    <td><%--  <asp:TextBox ID="txtseqno" runat="server" ></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtseqno" runat="server" Text='<% #Eval("SRNO")%>' CssClass="form-control"> </asp:TextBox>
                                                                        <asp:Label ID="lblseqno" runat="server" Text='<% #Eval("SRNO")%>' Visible="false"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="rfysqno" runat="server" ControlToValidate="txtseqno" Display="None" ErrorMessage="Please selecr seq.no" InitialValue="0" ValidationGroup="val"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCourseRegisterd" runat="server" Text='<%# (Convert.ToInt32(Eval("REGISTERED") ) == 1 ?  "Done" : "Pending" )%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>


                                                <div class="col-12" id="divOfferdCourseDetails" runat="server" visible="false">
                                                    <asp:Panel ID="pnlOfferedCourse" runat="server">
                                                        <asp:ListView ID="lvOfferdCourse" runat="server" OnItemDataBound="lvOfferdCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Offered Course List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap tblCourse" style="width: 100%" id="tblCourseLst">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Un-Offered</th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>Course Category</th>
                                                                            <th>Elective Group</th>
                                                                            <th>Credits</th>

                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>

                                                                            <th>Course Registration Semesterwise
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
                                                                        <%--Enabled='<%# (Convert.ToInt32(Eval("REGISTERED") ) == 1 ?  false : true )%>'--%>
                                                                        <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' />
                                                                    </td>
                                                                    <td><%# Eval("CCODE")%></td>
                                                                    <td><%# Eval("COURSE_NAME")%></td>
                                                                    <td><%# Eval("ELECT_TYPE")%></td>
                                                                    <td><%# Eval("GROUPNAME")%></td>
                                                                    <td><%# Eval("CREDITS") %></td>

                                                                    <td>
                                                                        <asp:DropDownList ID="ddlsem" runat="server" Visible="false" CssClass="form-control">
                                                                        </asp:DropDownList>

                                                                        <asp:Label ID="LblSemNo" runat="server" Text='<% #Eval("CCODE")%>' Visible="false"></asp:Label>
                                                                        <asp:ListBox ID="lstbxSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("SEMESTER") %>
                                                    
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnPrint" />
                                                <asp:PostBackTrigger ControlID="btnExcel" />
                                                <asp:PostBackTrigger ControlID="btnAd" />
                                                <%--  <asp:PostBackTrigger ControlID="ddlSession" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="upBulk" runat="server" AssociatedUpdatePanelID="updElectInt"
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

                                        <asp:UpdatePanel ID="updElectInt" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Session</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionIntake" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Session" ValidationGroup="ElectIntake" TabIndex="1" OnSelectedIndexChanged="ddlSessionIntake_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlSessionIntake" runat="server"
                                                                ControlToValidate="ddlSessionIntake" Display="None" InitialValue="0"
                                                                ErrorMessage="Please Select Session" ValidationGroup="ElectIntake"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Semester</label>
                                                            </div>
                                                            <asp:ListBox ID="lboSemesterIntake" runat="server" SelectionMode="Multiple"
                                                                CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">

                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Total Students Selected</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                                Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                                                ForeColor="#000066"></asp:TextBox>

                                                        </div>
                                                        <asp:HiddenField ID="hftot" runat="server" />
                                                        <asp:HiddenField ID="hftotN" runat="server" />
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShowElectIntake" runat="server" OnClick="btnShowElectIntake_Click" Text="Show" ValidationGroup="ElectIntake" TabIndex="3"
                                                            CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnSubmitIntake" runat="server" OnClick="btnSubmitIntake_Click" OnClientClick="return ValidateCheckbox();" Text="Submit Capacity" ValidationGroup="ElectIntake" TabIndex="4"
                                                            CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnCancelIntake" runat="server" OnClick="btnCancelIntake_Click" Text="Cancel" TabIndex="5"
                                                            CssClass="btn btn-danger" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="ElectIntake" />
                                                    </div>

                                                    <div class="col-12 table table-responsive" id="divElectIntake" runat="server">
                                                        <asp:Panel ID="pnlElectIntake" runat="server">
                                                            <asp:ListView ID="lvElectIntakeCapacity" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Elective Intake Course List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered" id="tblElectIntakeCapacity">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="return SelectAll(this)" ToolTip="Select/Select all" />
                                                                                </th>
                                                                                <th>Course Name
                                                                                </th>
                                                                                <th>Course Code
                                                                                </th>
                                                                                <th>Scheme</th>
                                                                                <th>Semester</th>
                                                                                <th>Capacity</th>
                                                                                <th>Eligibility </br>(<>= CGPA)</th>
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
                                                                            <asp:CheckBox ID="ChkElectIntake" runat="server" ToolTip='<%# Eval("COURSENO") %>' onClick="totStudents(this);" />
                                                                        </td>
                                                                        <td><%# Eval("COURSE_NAME")%>
                                                                            <asp:HiddenField ID="hfdDegreeNo" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                                        </td>
                                                                        <td><%# Eval("CCODE")%>
                                                                            <asp:HiddenField ID="hfdsessionNo" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                                        </td>
                                                                        <td><%# Eval("SCHEMENAME")%>
                                                                            <asp:HiddenField ID="hfdSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                        </td>
                                                                        <td><%# Eval("SEMESTERNAME")%>
                                                                            <asp:HiddenField ID="hfdSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIntakeCapacity" runat="server" onkeyup="return IsNumeric(this);" Text='<%# Eval("CAPACITY") %>' MaxLength="3"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEligibility" runat="server" onkeyup="return IsNumeric(this);" Text='<%# Eval("ELIGIBILITY_CGPA") %>' MaxLength="5"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="updProg1" runat="server" AssociatedUpdatePanelID="updpnlCopyCourse"
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

                                        <asp:UpdatePanel ID="updpnlCopyCourse" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>School & Scheme</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollegeScheme" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlCollegeScheme_SelectedIndexChanged" AutoPostBack="true"
                                                                ValidationGroup="CopyOffered" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCollegeScheme" runat="server" ControlToValidate="ddlCollegeScheme" SetFocusOnError="true"
                                                                Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="CopyOffered">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionCopyCrs" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlSessionCopyCrs_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="ddlSessionCopyCrs" Display="None" InitialValue="0"
                                                                ErrorMessage="Please Select Session" ValidationGroup="CopyOffered"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Semester</label>
                                                            </div>
                                                            <asp:ListBox ID="ddlSemesterCopyCrs" runat="server" SelectionMode="Multiple" RepeatColumns="2" RepeatDirection="Horizontal"
                                                                CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="3"></asp:ListBox>
                                                            <%--  <asp:CheckBoxList ID="ddlSemesterCopyCrs" runat="server" RepeatColumns="2" Width="100%" RepeatDirection="Horizontal"  TabIndex="3"
                                                                CssClass="checkbox-list-style">
                                                        </asp:CheckBoxList>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvSessionCopyCrsTo" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>To Session</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionCopyCrsTo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="ddlSessionCopyCrsTo" Display="None" InitialValue="0"
                                                                ErrorMessage="Please Select To Session" ValidationGroup="CopyOfferedTo"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnViewCopyOfferedCourse" runat="server" Text="View Offered Course" ValidationGroup="CopyOffered" TabIndex="4"
                                                        CssClass="btn btn-primary" OnClick="btnViewCopyOfferedCourse_Click" />

                                                    <asp:Button ID="btnCopyOfferedCourse" runat="server" Text="Copy Offered Course" ValidationGroup="CopyOfferedTo" TabIndex="5"
                                                        CssClass="btn btn-primary" OnClick="btnCopyOfferedCourse_Click" Visible="false" />

                                                    <asp:Button ID="btnCopyCrs" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="6"
                                                        CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="CopyOffered" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="CopyOfferedTo" />
                                                </div>

                                                <div class="col-12 table-responsive" id="dvCopyOfferedCourse" runat="server">
                                                    <asp:Panel ID="pnlCopyOfferedCourse" runat="server" Visible="false">
                                                        <asp:HiddenField ID="hfOfferedCrs" runat="server" />
                                                        <asp:ListView ID="lvCopyOfferCourse" runat="server" OnItemDataBound="lvCopyOfferCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Course List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourseLst">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select All<br />
                                                                                <asp:CheckBox ID="chkBoxAll" runat="server" onclick="return SelectAllCopyOfferCrs(this)"></asp:CheckBox>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>Course Category</th>
                                                                            <th>Elective Group</th>
                                                                            <th>Credits</th>
                                                                            <th>Course Semester</th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                                            <th>Seq.No.</th>
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
                                                                        <asp:CheckBox ID="chkofferedCopyOffrCrs" runat="server" ToolTip='<%# Eval("COURSENO") %>' />
                                                                    </td>
                                                                    <td><%# Eval("CCODE")%></td>
                                                                    <td><%# Eval("COURSE_NAME")%></td>
                                                                    <td><%# Eval("ELECT_TYPE")%></td>
                                                                    <td><%# Eval("GROUPNAME")%></td>
                                                                    <td><%# Eval("CREDITS") %></td>
                                                                    <td><%# Eval("SEMESTERNAME") %></td>
                                                                    <td>
                                                                        <asp:Label ID="LblSemNoCopyOffrCrs" runat="server" Text='<% #Eval("CCODE")%>' Visible="false"></asp:Label>
                                                                        <%--<asp:Label ID="lblbxSemestertoCopyOffrCrs" runat="server" CssClass="form-control multi-select-demo"></asp:Label>--%>
                                                                        <asp:ListBox ID="lblbxSemestertoCopyOffrCrs" runat="server" AppendDataBoundItems="true"
                                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSeqNoCopyOffrCrs" runat="server" Text='<% #Eval("SRNO")%>' CssClass="form-control"> </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>


    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            var hftot = document.getElementById('<%= hftot.ClientID %>');
            //alert(hftot.value);
            for (i = 0; i < hftot.value; i++) {
                //alert('for');
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvElectIntakeCapacity_ctrl' + i + '_ChkElectIntake');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function IsNumeric(txt) {
            var ValidChars = ".0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }


        //function LoadImage() {
        //    document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        //}


        function SelectAllCopyOfferCrs(chk) {
            var hfOfferedCrs = document.getElementById('<%= hfOfferedCrs.ClientID %>');
            for (i = 0; i < hfOfferedCrs.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCopyOfferCourse_ctrl' + i + '_chkofferedCopyOffrCrs');
                if (lst.type == 'checkbox')
                    lst.checked = (chk.checked == true) ? true : false;
            }
        }



    </script>
    <script type="text/javascript">
        function ValidateCheckbox() {

            var count = 0;
            var numberOfChecked = $('[id*=tblElectIntakeCapacity] td input:checkbox:checked').length;
            if (numberOfChecked == 0) {
                alert("Please select atleast one Course.");
                return false;
            }
            else
                return true;

        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnShowElectIntake').click(function () {
                    ValidateCheckbox();
                });
            });
        });

    </script>


    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('.tblCourse').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination
                fixedColumns: true,
                fixedColumns: {
                    leftColumns: 2
                },

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('.tblCourse').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('.tblCourse').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('.tblCourse').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('.tblCourse').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination
                    fixedColumns: true,
                    fixedColumns: {
                        leftColumns: 2
                    },

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('.tblCourse').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('.tblCourse').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('.tblCourse').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {

            debugger;
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            var hftotN = document.getElementById('<%= hftotN.ClientID %>').value;
            // alert(hftotN);
            var count = 0;
            for (i = 0; i < Number(hftotN) ; i++) {

                //ctl00_ContentPlaceHolder1_lvCourse_ctrl0_chkoffered
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_chkoffered');
                //alert(lst.typechk);
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

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
           }
           else {
               document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
           }
       }

       function totStudents(chk) {
           var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

    </script>

</asp:Content>



