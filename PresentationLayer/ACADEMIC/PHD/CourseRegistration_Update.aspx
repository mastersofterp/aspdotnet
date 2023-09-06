<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseRegistration_Update.aspx.cs" Inherits="ACADEMIC_PHD_CourseRegistration_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="myModal2" role="dialog" runat="server">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updEdit"
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

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:Panel ID="pnDisplay" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                                </h3>
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </div>

                            <div class="box-body">
                                <asp:UpdatePanel ID="updEdit" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search Criteria</label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">

                                                    <asp:Panel ID="pnltextbox" runat="server">
                                                        <div id="divtxt" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Search String</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="pnlDropdown" runat="server">
                                                        <div id="divDropDown" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </div>
                                                    </asp:Panel>

                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panellistview" runat="server">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5>Student List</h5>
                                                            </div>
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Name
                                                                            </th>
                                                                            <th style="display: none">IdNo
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Branch--%>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Semester--%>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Father Name
                                                                            </th>
                                                                            <th>Mother Name
                                                                            </th>
                                                                            <th>Mobile No.
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="display: none">
                                                                <%# Eval("idno")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FATHERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MOTHERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%#Eval("STUDENTMOBILE") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvStudent" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div id="divmain" runat="server" visible="false">
                                    <div class="accordion" id="accordionExample">
                                        <div class="card" runat="server" id="DivSutLog">
                                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                                <span class="title">General Information</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseOne" class="collapse show">
                                                <div class="card-body">
                                                    <div class="col-12" id="DivGenInfo" runat="server" visible="true">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>ID No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Enrollment No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Date of Joining :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbljoiningdate" runat="server" Font-Bold="True"></asp:Label>
                                                                            <asp:HiddenField ID="hfdegreenos" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Status :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblstatussup" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Supervisor Role :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblSuperRole" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>No.of DGC Member :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblNDM" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblnames" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Father Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblfathername" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Department :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                                                            <asp:HiddenField ID="hfDepartment" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Admission Batch :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbladmbatch" runat="server" Font-Bold="True"></asp:Label>
                                                                            <asp:HiddenField ID="hfadmbatch" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Scheme :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Area of Research :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblAoR" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3 ml-3">
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCollege">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                                ErrorMessage="Please Select School/Institute Name" InitialValue="0" Display="None" ValidationGroup="CRPhd"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvtxtSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" SetFocusOnError="True" InitialValue="0"
                                                ValidationGroup="CRPhd"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="CRPhd"></asp:RequiredFieldValidator>--%>
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Offered Courses</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOfferedCourse" runat="server"
                                                AppendDataBoundItems="True" CssClass="form-control x" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlOfferedCourse_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOfferedCourse"
                                        Display="None" ErrorMessage="Please Select Offered Courses" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="CRPhd"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trremark" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Total Credit Limit :</label>
                                        </div>
                                        <asp:TextBox ID="lblTotalCredit" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                            ForeColor="#000066"></asp:TextBox>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="CRPhd" />
                                        <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="CRPhd" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" Visible="false" OnClick="btnPrintRegSlip_Click" Enabled="true" CssClass="btn btn-info" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Current Semester Subjects</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblCurrentSubjects">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <%--<asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />--%>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <%--<th>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>--%>
                                                            <th>
                                                                <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                            <%-- <th>Course Category
                                                    </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow">

                                                    <td>
                                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                                            onclick="ChkHeader(1,'cbHeadReg','chkRegister');" Checked='<%# ( Eval("REGISTERED").ToString()=="1") ? true : false %>'
                                                            Enabled='<%# ( Eval("REGISTERED").ToString()=="1") ? false : true %>' />
                                                        <%--<asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" ToolTip='<%#Eval("GROUPNO") %>' Checked='<%# (Eval("STUD_COURSE_REG").ToString() == "1" || Eval("REGISTERED").ToString()=="1") ? true : false %>'
                                                               Enabled='<%# ( Eval("REGISTERED").ToString()=="1") ? false : true %>' OnCheckedChanged="chklvUniCoreSub_OnCheckedChanged" />--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <%--<td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                            </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <%-- <td>
                                                <%#Eval("CATEGORY")%>
                                            </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvBacklogSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Backlog Subjects</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblBacklogSubjects">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <%--<asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />--%>
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Subject Type
                                                            </th>
                                                            <th>Credits
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

                                                    <td>
                                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                                            onclick="ChkHeader(2,'cbHeadReg','chkRegister');" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () {
                bindDataTable();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            });
            function bindDataTable() {
                var myDT = $('#id1').DataTable({
                });
            }

        });
    </script>

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>

</asp:Content>

