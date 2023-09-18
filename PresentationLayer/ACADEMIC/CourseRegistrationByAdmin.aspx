<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="CourseRegistrationByAdmin.aspx.cs" Inherits="ACADEMIC_CourseRegistrationByAdmin"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <asp:UpdatePanel runat="server" ID="upCourseRegistration" UpdateMode="Conditional">
        <ContentTemplate>
            <script>
                $(document).ready(function () {

                    $('#ctl00_ContentPlaceHolder1_lvStudentBulk_cbHead').on('click', function () {
                        // Get all rows with search applied

                        var rows = table.rows({ 'search': 'applied' }).nodes();

                        // Check/uncheck checkboxes for all rows in the table

                        $('input[type="checkbox"]', rows).prop('checked', this.checked);
                        var count = 0;

                        for (i = 0; i <= rows.length; i++) {
                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentBulk_cbHead').checked == true) {
                                document.getElementById('<%= txtTotStud.ClientID %>').value = count++;
                             }
                             else {
                                 document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
                             }
                         }

                     });



                     // Handle click on checkbox to set state of "Select all" control
                     $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {

                         // If checkbox is not checked
                         //if (!this.checked) {

                         var el = $('#ctl00_ContentPlaceHolder1_lvStudentBulk_cbHead').get(0);
                         var rows = table.rows({ 'search': 'applied' }).nodes();

                         // Check/uncheck checkboxes for all rows in the table
                         if (el && el.checked && ('indeterminate' in el)) {
                             // Set visual state of "Select all" control
                             // as 'indeterminate'
                             el.indeterminate = true;
                         }
                         var tot = document.getElementById('<%= txtTotStud.ClientID %>');
                //alert(this.checked)
                //for (i = 0; i <= rows.length; i++) {
                if (this.checked == true) {
                    tot.value = Number(tot.value) + 1;
                }
                else {
                    tot.value = Number(tot.value) - 1;
                    //document.getElementById('<%= txtTotStud.ClientID %>').value = count--;
                }
                //}
                // If "Select all" control is checked and has 'indeterminate' property

                //}
            });
                 });
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    $(document).ready(function () {

                        $('#ctl00_ContentPlaceHolder1_lvStudentBulk_cbHead').on('click', function () {

                            // Get all rows with search applied
                            var rows = table.rows({ 'search': 'applied' }).nodes();
                            // Check/uncheck checkboxes for all rows in the table
                            $('input[type="checkbox"]', rows).prop('checked', this.checked);


                        });

                        // Handle click on checkbox to set state of "Select all" control
                        $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {

                            // If checkbox is not checked
                            if (!this.checked) {
                                var el = $('#ctl00_ContentPlaceHolder1_lvStudentBulk_cbHead').get(0);
                                // If "Select all" control is checked and has 'indeterminate' property
                                if (el && el.checked && ('indeterminate' in el)) {
                                    // Set visual state of "Select all" control
                                    // as 'indeterminate'
                                    el.indeterminate = true;

                                }
                            }
                        });
                    });
                });
            </script>
            <%-- <asp:HiddenField ID="hidTAB" runat="server" ClientIDMode="Static" />--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server" visible="false">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">MODIFY SUBJECT REGISTRATION BY ADMIN</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Modify Course Registration</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Modify Bulk  Course Registration</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">

                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReg"
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

                                        <asp:UpdatePanel ID="updReg" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div id="divOptions" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
                                                    <div style="width: 100px; font-weight: bold; float: left;">Options :</div>
                                                    <div style="width: 500px;">
                                                        <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true">
                                                            <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                                                            <asp:ListItem Value="S" Text="Backlog"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="box-body">

                                                    <div class="col-12" id="divpnlSearch" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>Options</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                    OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true" CssClass="form-control">
                                                                    <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                                                                    <asp:ListItem Value="S" Text="Backlog"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<label>College</label>--%>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true">  </asp:Label>
                                                                    <b>&</b>
                                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute."
                                                                    CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <%--<label>Session Name</label>--%>
                                                                    <%--<asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                </div>
                                                                <%--<asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Enrollment No.</label>--%>
                                                                    <asp:Label ID="lblDYtxtEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="15" TabIndex="3" />
                                                            </div>
                                                            <%--<div class="row">--%>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Search Criteria</label>
                                                                </div>

                                                                <%--onchange=" return ddlSearch_change();"--%>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                                                    Display="None" ErrorMessage="Please select search string from the given list"
                                                                    SetFocusOnError="true" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                                <asp:Panel ID="pnltextbox" runat="server">
                                                                    <div id="divtxt" runat="server" style="display: block">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Search String</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSearchtring" runat="server" ControlToValidate="txtSearch" InitialValue="" Enabled="false"
                                                                            Display="None" ErrorMessage="Please Enter search string in the given text box"
                                                                            SetFocusOnError="true" ValidationGroup="submit" />
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                                    <div id="divDropDown" runat="server" style="display: block">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <%-- <label id="lblDropdown"></label>--%>
                                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvDDL" runat="server" ControlToValidate="ddlDropdown" InitialValue="0" Enabled="false"
                                                                            Display="None" ErrorMessage="Please select search string from the given list"
                                                                            SetFocusOnError="true" ValidationGroup="submit" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <%--</div>--%>

                                                            <div class="col-12 btn-footer">
                                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                                <asp:Button ID="btnSearchCriteria" runat="server" Text="Search" OnClick="btnSearchCriteria_Click" CssClass="btn btn-primary" ValidationGroup="submit" />
                                                                <asp:Button ID="btnCloseCriteria" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnCloseCriteria_Click"
                                                                    OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                                                <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                            </div>

                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlLV" runat="server">
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
                                                                                                <th>Sr.No.</th>
                                                                                                <th>Name
                                                                                                </th>
                                                                                                <%-- <th style="display: none">IdNo
                                                                            </th>--%>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                                </th>
                                                                                                <th>
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
                                                                                <td><%# Container.DataItemIndex +1 %></td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                        OnClick="lnkId_Click"></asp:LinkButton>
                                                                                </td>
                                                                                <%-- <td style="display: none">
                                                                <%# Eval("idno")%>
                                                            </td> comment this or delete this plz --%>
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
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer d-none">
                                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" TabIndex="4"
                                                            CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" TabIndex="5"
                                                            CssClass="btn btn-warning"
                                                            OnClick="btnCancel_Click" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please enter Student Enrollment No." />--%>
                                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" />
                                                    </div>

                                                    <div class="col-12" id="tblInfo" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Father Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="true" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mother Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="true" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>College Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Roll No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Enroll. No./Reg. No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Degree / Branch :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Semester :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>PH. No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblPH" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Scheme :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>Total Subjects :</b>
                                                                        <a class="sub-label">
                                                                            <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                                                                Style="text-align: center;"></asp:TextBox>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>Total Credits :</b>
                                                                        <a class="sub-label">
                                                                            <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text="0"
                                                                                Style="text-align: center;"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Admission Batch :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
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
                                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Credits
                                                                                </th>
                                                                                <th>Course Category
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
                                                                                onclick="ChkHeader(1,'cbHeadReg','chkRegister');" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
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
                                                                        <td> <%#Eval("CATEGORY")%>
                                                                        </td>
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
                                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />
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

                                                        <div class="col-12 mt-3">
                                                            <asp:ListView ID="lvAuditSubjects" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Audit Subjects</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblAuditSubjects">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,3,'chkRegister');" />
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
                                                                                onclick="ChkHeader(3,'cbHeadReg','chkRegister');" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
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

                                                        <div class="col-12 btn-footer mt-4">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                                                Enabled="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />
                                                            <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                                                OnClick="btnPrintRegSlip_Click" Enabled="false" CssClass="btn btn-info" />
                                                            <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="SUBMIT" />
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                                                        </div>
                                                    </div>


                                                    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                                    <div id="divMsg" runat="server">
                                                    </div>
                                                </div>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="upBulk" runat="server" AssociatedUpdatePanelID="updBulkReg"
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

                                        <asp:UpdatePanel ID="updBulkReg" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--  <label>College & Scheme</label>--%>
                                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                                ValidationGroup="offered" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                                Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="BulkSubmit">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session</label>
                                                                <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBulkSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                                                AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBulkSession_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlBulkSession" SetFocusOnError="true"
                                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Admission Batch</label>--%>
                                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"
                                                                AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="BulkSubmit" SetFocusOnError="true"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>School/Institute Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                AutoPostBack="True" ToolTip="Please Select School/Institute" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Scheme Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Programme/Branch</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Programme/Branch" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Scheme</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchm" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSchm_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlSchm"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Semester</label>--%>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" SetFocusOnError="true"
                                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Student Status</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                                                <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Section</label>--%>
                                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <asp:HiddenField ID="hftot" runat="server" />
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Total Students Selected</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                                Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                                                ForeColor="#000066"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DivMultipleSelect" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Offered Courses</label>
                                                            </div>
                                                            <asp:ListBox ID="lboOfferCourse" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"  AppendDataBoundItems="true"></asp:ListBox>
                                                            
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnBulkSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="BulkSubmit"
                                                            OnClientClick="return validateAssign();" OnClick="btnBulkSubmit_Click"
                                                            Enabled="false" />

                                                       <%-- <asp:Button ID="btnBulkReport" runat="server" Text="Registration Slip Report"
                                                            CssClass="btn btn-info" Visible="false"
                                                            ValidationGroup="BulkSubmit" />--%>

                                                        <asp:Button ID="btnBulkCancel" runat="server" CausesValidation="False" Text="Cancel"
                                                            CssClass="btn btn-warning" OnClick="btnBulkCancel_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ValidationGroup="BulkSubmit" ShowSummary="false" />
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-12">
                                                                <asp:Panel ID="pnlStudents" runat="server" Visible="true">
                                                                    <asp:ListView ID="lvStudentBulk" runat="server">
                                                                        <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr id="trRow">
                                                                                        <th>
                                                                                            <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAllBulk(this)" ToolTip="Select/Select all" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <%--<th style="text-align: left; width: 15%">
                                                                                Enroll. No.
                                                                            </th>--%>
                                                                                        <th>Roll No
                                                                                        </th>
                                                                                     <th>Student Name
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
                                                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblRollNoForbulk" runat="server" Text='<%# Eval("ROLLNO") %>'/>
                                                                                </td>
                                                                                <%--<td style="text-align: left; width: 25%">
                                                                        <asp:LinkButton ID="lblRegNo" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip="Click here to Display Registered Courses"
                                                                            CommandArgument='<%# Eval("IDNO") %>' OnClick="btnPrint_Click" ValidationGroup="submit"
                                                                            Font-Underline="false" ForeColor="Black" />
                                                                    </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                                        Visible="false" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <%--         <div class="col-md-6 col-12">
                                        <asp:Panel ID="pnlCourses" runat="server" Visible="true">
                                            <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="divlvPaidReceipts">
                                                        <div class="sub-heading">
                                                            <h5>Offered Courses</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Select
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label> - <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
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
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                            &nbsp;
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="altitem">
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>--%>
                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlStudentsReamin" runat="server">
                                                                    <asp:ListView ID="lvStudentsRemain" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List (Demand Not Found)</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr id="trRow">
                                                                                        <th>HT No.
                                                                                        </th>
                                                                                        <th>Student Name
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
                                                                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                                        Visible="false" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <%--<AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                        <td >
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' />
                                                        </td>
                                                        <td >
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>--%>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function SelectAllBulk(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentBulk_ctrl' + i + '_cbRow');
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
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
           //var offer = document.getElementById('<%= lboOfferCourse.ClientID %>').value;
           // alert(offer);
            if (txtTot == 0) {
                alert('Please Check atleast one student ');             
                return false;
                
            }
            //if (isNaN(offer)==0) {
            //    alert('Please Select Atleast one Offer Cousre');
            //    return false;
            //}
           
        }
        //function MultipleSelect(frm) {
        //    for (i = 0; i < frm.length; i++) {
        //        if (frm.elements[i].name.indexOf('lboOfferCourse') != -1) {
        //            if (frm.elements[i].checked) {
        //                return confirm('Are you sure you want to Offered Course')
        //            }
        //        }
        //    }
        //    alert('Please Select Atleast one Offer Cousre')
        //    return false
        //}

        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

    </script>

    <script type="text/javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
            }
            else if (headid == 2) {
                tbl = document.getElementById('tblBacklogSubjects');
                list = 'lvBacklogSubjects';
            }
            else {
                tbl = document.getElementById('tblAuditSubjects');
                list = 'lvAuditSubjects';
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
                    tbl = document.getElementById('tblCurrentSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + head;
                    list = 'lvCurrentSubjects';
                }
                else if (chklst == 2) {
                    tbl = document.getElementById('tblBacklogSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvBacklogSubjects_' + head;
                    list = 'lvBacklogSubjects';
                }
                else {
                    tbl = document.getElementById('tblAuditSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvAuditSubjects_' + head;
                    list = 'lvAuditSubjects';
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
            var ret = confirm('Do you Really want to Confirm/Submit this Subjects for Subject Registration?');
            if (ret == true)
                return true;
            else
                return false;
        }

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
                alert('Please select Criteria as you want search...')
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
                          alert('Please Enter Data you want to search..');
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

    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
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
    
</asp:Content>
