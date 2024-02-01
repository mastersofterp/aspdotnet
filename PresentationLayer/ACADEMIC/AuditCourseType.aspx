<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AuditCourseType.aspx.cs" Inherits="ACADEMIC_AuditCourseType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).bind("contextmenu", function (e) {
            e.preventDefault();
        });
        $(document).keydown(function (e) {
            if (e.which === 123) {
                return false;
            }
        });
    </script>

    <table cellpadding="0" cellspacing="0" width="100%">
    </table>

    <div class="box box-primary" runat="server" id="divsteps">
        <div id="divNote" runat="server" visible="true" style="border: 2px solid #00569d; background-color: #FFFFCC; padding: 20px; color: #990000;" class="col-md-12">
            <b>Note : </b>Steps to follow for Audit Course Registration.
        <div style="padding-left: 20px; padding-right: 20px" class="col-md-12">
            <p style="padding-top: 5px; padding-bottom: 5px;">
                1. Read the instructions carefully and Proceed to Course Registration.</b> 
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                2. A Course list of current semester of the student will be shown. 
                Select current semester  courses  one by one from the course list.
            </p>

            <p style="padding-top: 5px; padding-bottom: 5px;">
                3. Confirm all courses are properly checked and  click on submit button.
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                4. Download Registration slip by Clicking on Registration slip button.
            </p>

            <div class="col-md-12">
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Course Registration"
                        OnClick="btnProceed_Click" CssClass="btn btn-primary" ToolTip="Click Here To Proceed Course Registration" />
                </p>
            </div>
        </div>
        </div>
    </div>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:Panel runat="server" ID="pnlMain">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div id="divCourses" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                                    <div class="box-tools pull-right">
                                        <div style="color: Red; font-weight: bold">
                                            Note : * Marked fields are mandatory
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12" id="divmainsearch" runat="server">

                                    <%--Search Pannel Start by Rohit --%>
                                    <div id="myModal2" role="dialog" runat="server">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                        <asp:UpdatePanel ID="updEdit" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12" id="Divsearch" runat="server">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup><label>Search Criteria</label>
                                                            </div>

                                                            <%--onchange=" return ddlSearch_change();"--%>
                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                            <asp:Panel ID="pnltextbox" runat="server">
                                                                <div id="divtxt" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Search String</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                                <div id="divDropDown" runat="server" style="display: block">
                                                                    <div class="label-dynamic">
                                                                        <%-- <label id="lblDropdown"></label>--%>
                                                                        <sup>*</sup>
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
                                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                        <asp:Button ID="btnsearchstud" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                    </div>
                                                </div>

                                                <div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlLV" runat="server">
                                                            <asp:ListView ID="lvStudent" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="listViewGrid" class="vista-grid">
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Name
                                                                                        </th>
                                                                                        <th>IdNo
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
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
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <td>
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
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lvStudent" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Search Pannel End--%>
                                </div>




                                <div class="box-body" runat="server" id="divsession">

                                    <div id="divmaincoursereg" runat="server" visible="false">
                                        <div class="col-md-4" id="trSession_name" runat="server">
                                            <label><span style="color: red;">*</span>Session Name</label>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSession" runat="server" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show" />
                                        </div>
                                        <div class="col-md-4" id="trRollNo" runat="server" style="display: none">
                                            <label><span style="color: red;">*</span>Registration No.</label>
                                            <asp:TextBox ID="txtRollNo" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvSession" ControlToValidate="txtRollNo" runat="server"
                                                Display="None" ErrorMessage="Please Enter Registration No." ValidationGroup="Show" />
                                        </div>

                                        <div class="box-footer">
                                            <p class="text-center">
                                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                                    Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-primary" Visible="false" />
                                                <asp:Button ID="btnExcelReport" runat="server" Text="Report(Excel)" OnClick="btnExcelReport_Click1"
                                                    OnClientClick="return validateSession();" CssClass="btn btn-primary" Visible="false" />

                                                <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                                    Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-warning"
                                                    OnClick="btnCancel_Click" />
                                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Show" />
                                            </p>
                                            <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item">
                                                                <b>Registration No. :</b><a class="">
                                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Admission Batch :</b><a class="">
                                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Mother Name :</b><a class="">
                                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Father Name :</b><a class="">
                                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                                            </li>

                                                            <%-- <li class="list-group-item">
                                                                <b>Minimum Credits Limit :</b><a class="">
                                                                    <asp:Label ID="lblOfferedRegCreditsFrom" runat="server" Font-Bold="True"></asp:Label></a>
                                                                <asp:HiddenField ID="hdfDegreenoFrom" runat="server" />
                                                            </li>--%>
                                                            <li class="list-group-item">
                                                                <b>Credits Registration History :</b><a class="">
                                                                    <asp:Label ID="lblOfferedRegCredits" runat="server" Font-Bold="True"></asp:Label></a>
                                                                <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Audit Course Registration Credits :</b><a class="">
                                                                    <asp:Label ID="lblTotalRegCredits" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item">
                                                                <b>Student Name :</b><a class="">
                                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                                            </li>

                                                            <li class="list-group-item">
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                <b>:</b><a class="">
                                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Session :</b><a class="">
                                                                    <asp:Label ID="lblSess" runat="server" Font-Bold="True"></asp:Label></a>
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
                                                        <label>Audit Course Registration Credits</label>
                                                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0"
                                                            Style="text-align: center;"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note </h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">The registered credits are over and above the regular allowed credits and will be considered as 'Audit Mode Courses'.</span></span></p>
                                                    </div>
                                                </div>

                                                <div class="box-footer col-md-12">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClientClick="return validateAssign();" OnClick="btnSubmit_Click"
                                                            ValidationGroup="SUBMIT" />
                                                        <asp:Button ID="btnPrintChallan" runat="server" Text="Print Challan" OnClick="btnPrintChallan_Click" CssClass="btn btn-info" Visible="false" />
                                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                                            OnClick="btnPrintRegSlip_Click" Enabled="true" CssClass="btn btn-info" />
                                                        <asp:Button ID="btnPrePrintClallan" runat="server" Text="Re-Print Challan"
                                                            OnClick="btnPrePrintClallan_Click" CssClass="btn btn-info" Visible="false" />
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

                                                <div class="col-md-12 table table-responsive" style="display: none">
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
                                                                            <th>Intake
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
                                                                        OnCheckedChanged="chkCurrentSubjects_OnCheckedChanged" AutoPostBack="false" onclick="electivevalidatation(this);" ToolTip='<%# Eval("ELECT") %>' />
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
                                                                <td>
                                                                    <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' />
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>

                                                <div class="col-md-12 table table-responsive">
                                                    <asp:ListView ID="lvUniCoreSub" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">

                                                                <table id="tblUniCoreSub" class="table table-hover table-bordered table-striped ">
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
                                                                            <th>Intake
                                                                            </th>
                                                                            <th>Credits
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
                                                                    <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" ToolTip='<%#Eval("GROUPNO") %>' OnCheckedChanged="chklvUniCoreSub_OnCheckedChanged" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAudit" runat="server" Text='<%# Eval("IS_AUDIT") %>' ToolTip='<%# Eval("IS_AUDIT")%>' Visible="false" />
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
                                                                <td>
                                                                    <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' ToolTip='<%# Eval("INTAKE") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>

                                                <div class="col-md-12 table table-responsive">
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
                                                                            <th>Branch Name
                                                                            </th>
                                                                            <th>Course Type
                                                                            </th>
                                                                            <th style="display: none">Course Group
                                                                            </th>
                                                                            <th style="display: none">Section
                                                                            </th>
                                                                            <th style="display: none">Course Teacher
                                                                            </th>
                                                                            <th>Intake
                                                                            </th>
                                                                            <th>Credits
                                                                            </th>
                                                                            <%--<th>Available Seats
                                                                            </th>--%>
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
                                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%#Eval("GROUPNO") %>' OnCheckedChanged="chkGlobalSubjects_OnCheckedChanged" AutoPostBack="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAudit" runat="server" Text='<%# Eval("IS_AUDIT") %>' ToolTip='<%# Eval("IS_AUDIT")%>' Visible="false" />
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BRANCH_NAME") %>' />
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
                                                                <td>
                                                                    <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text='<%# Eval("INTAKE") %>' ToolTip='<%# Eval("CHOICEFOR") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                                </td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblAvailableSeats" runat="server" Text='<%# Eval("AvailableSeats") %>' />
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div class="col-md-12 table table-responsive">
                                                    <asp:ListView ID="lvValueAdded" runat="server" Enabled="false">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <table id="tblValueAddedSubjects" class="table table-hover table-bordered table-striped">
                                                                    <thead>
                                                                        <tr>
                                                                            <th colspan="9" style="text-align: left">Value Added / Specialization Courses  <%--Breadth/Open Elective/Program Elective Courses--%>
                                                                            </th>
                                                                            <%--  <th colspan="9" style="text-align: left"><asp:Label ID="lblNotesForvalueAddedCourses" runat="server" Text='You Have Only <%# Eval("CHOICE_FOR") %> Attempt to Submit Value Added / Specialization Courses' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    </th>--%>
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
                                                                            <th style="display: none">Section
                                                                            </th>
                                                                            <%-- <th style="display: none">Course Group
                                                                    </th>
                                                                    <th style="display: none">Section
                                                                    </th>
                                                                    <th style="display: none">Course Teacher
                                                                    </th>
                                                                    <th style="display: none">Intake
                                                                    </th>                                                                   
                                                                    <th>Available Seats
                                                                    </th>--%>
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
                                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%#Eval("GROUPID") %>'
                                                                        Checked="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GROUP_NAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                                </td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME") %>' ToolTip='<%# Eval("SectionNO") %>' />
                                                                </td>
                                                                <%--  <td style="display: none">
                                                            <asp:Label ID="lblgroupname" runat="server" Font-Bold="true" Text='<%# Eval("GROUPNAME") %>' ToolTip='<%# Eval("GROUPNO") %>' />
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
                                                        </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExcelReport" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <script type="text/javascript">



        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
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
                // alert(e);
            }
        }



        function CheckSelectionCount(chk) {
            debugger;
            var count = -2;
            var tbl = '';
            var list = '';
            var alltbl = ["tblUniCoreSub"];
            var countCheck1 = 0;
            var countCheck2 = 0;
            var countCheck3 = 0;
            var countCheck4 = 0;
            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {
                        list = 'lvUniCoreSub';
                        var dataRows = tbl.getElementsByTagName('tr');
                        for (j = 0; j < dataRows.length ; j++) {

                            var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                            //alert(chkid);
                            //alert(document.getElementById(chkid).parentElement.title);
                            if (document.getElementById(chkid).checked && document.getElementById(chkid).parentElement.title == 1) {

                                countCheck1++;

                                if (countCheck1 > 1) {
                                    alert('You Can Select Only One Course From Activity Group!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }

                            }
                            if (document.getElementById(chkid).checked && document.getElementById(chkid).parentElement.title == 2) {



                                countCheck2++;

                                if (countCheck2 > 1) {
                                    alert('You Can Select Only One Course From Electives Group!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }

                            }
                            if (document.getElementById(chkid).checked && document.getElementById(chkid).parentElement.title == 3) {



                                countCheck3++;

                                if (countCheck3 > 1) {
                                    alert('You Can Select Only One Course From Breadth Course!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }
                            }
                            if (document.getElementById(chkid).checked && document.getElementById(chkid).parentElement.title == 4) {

                                countCheck4++;

                                if (countCheck4 > 1) {
                                    alert('You Can Select Only One Course From Sessional Elective!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }



        function CheckSelectionCount1(chk) {
            debugger;
            var count = -2;
            var tbl = '';
            var list = '';
            var alltbl = ["tblUniCoreSub1"];
            var countCheck = 0;
            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {



                        list = 'lvUniCoreSub1';
                        var dataRows = tbl.getElementsByTagName('tr');
                        for (j = 0; j < dataRows.length ; j++) {

                            var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                            //alert(chkid);
                            if (document.getElementById(chkid).checked) {



                                countCheck++;

                                if (countCheck > 1) {
                                    alert('You Can Select Only One Course From Electives Group!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        function CheckSelectionCount2(chk) {
            debugger;
            var count = -2;
            var tbl = '';
            var list = '';
            var alltbl = ["tblUniCoreSub2"];
            var countCheck = 0;
            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {



                        list = 'lvUniCoreSub2';
                        var dataRows = tbl.getElementsByTagName('tr');
                        for (j = 0; j < dataRows.length ; j++) {

                            var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                            //alert(chkid);
                            if (document.getElementById(chkid).checked) {



                                countCheck++;

                                if (countCheck > 1) {
                                    alert('You Can Select Only One Course From Breadth Course!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }


        function CheckSelectionCount3(chk) {
            debugger;
            var count = -2;
            var tbl = '';
            var list = '';
            var alltbl = ["tblUniCoreSub3"];
            var countCheck = 0;
            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {



                        list = 'lvUniCoreSub3';
                        var dataRows = tbl.getElementsByTagName('tr');
                        for (j = 0; j < dataRows.length ; j++) {

                            var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                            //alert(chkid);
                            if (document.getElementById(chkid).checked) {



                                countCheck++;

                                if (countCheck > 1) {
                                    alert('You Can Select Only One Course From Sessional Elective!');
                                    document.getElementById(chkid).checked = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        function validateAssign() {
            //debugger;
            //var numberOfChecked = $('[id*=tblCurrentSubjects] input:checkbox:checked').length;
            //if (numberOfChecked == 0) {
            //    alert('Please select atleast one course from the course list for course registration..!!');
            //    return false;
            //}
            //else {

            //    var regcredits = $('#ctl00_ContentPlaceHolder1_lblTotalRegCredits').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblTotalRegCredits]").text();
            //    var maxcredits = $('#ctl00_ContentPlaceHolder1_lblOfferedRegCredits').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblOfferedRegCredits]").text();
            //    var mincredits = $('#ctl00_ContentPlaceHolder1_lblOfferedRegCreditsFrom').text(); //$("[id*=ctl00_ContentPlaceHolder1_lblOfferedRegCreditsFrom]").text();
            //    if ((parseFloat(maxcredits) >= parseFloat(regcredits) && parseFloat(mincredits) <= parseFloat(regcredits)) == false) {
            //        //alert("Total register credits should be equal to credits to register.");
            //        alert("Total register credits should be between Minimum Credits Limit and Maximum Credits Limit.");

            //        //$('[id*=tblCurrentSubjects] td').closest('tr').find(':checkbox').removeAttr('checked');
            //        // var count= $('[id*=tblCurrentSubjects] td').closest('tr').find(':checkbox').length;
            //        //("input[type=checkbox]").prop("checked", false);
            //        return false;

            //    }
            //    else {
            //        if (confirm('Are you sure you want to register for the selected courses?')) {
            //            return true;
            //        }
            //        else {
            //            return false;
            //        }
            //    }
            //}
            //return false;
        }



        function MutualExclusive(radio) {
            var dvData = document.getElementById("dvData");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        function MutualExclusiveGrp(radio) {
            var dvData = document.getElementById("dvDataGrp");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        //function CheckSelectionCount(chk) {
        //    var count = -2;
        //    var frm = document.forms[0]
        //    for (i = 0; i < document.forms[0].elements.length; i++) {
        //        var e = frm.elements[i];
        //        if (count == 2) {
        //            chk.checked = false;
        //            alert("You have reached maximum limit!");
        //            return;
        //        }
        //        else if (count < 2) {
        //            if (e.checked == true) {
        //                count += 1;
        //            }
        //        }
        //        else {
        //            return;
        //        }
        //    }
        //}

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }

        function showConfirm() {

            var txtOfferedTot = document.getElementById('<%=lblOfferedRegCredits.ClientID %>').innerText;
            var txtRegTot = document.getElementById('<%= lblTotalRegCredits.ClientID %>').innerText;

            if (Number(txtOfferedTot) == Number(txtRegTot)) {
                var ret = confirm('Do you really want to submit this Courses for Course Registration?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
            else {
                //alert("Credits Mismatch in Offered & Registered Credits, Please Check Selected Course.!");
                //return false;
            }
        }

        function ConfirmDelete() {
            var x = confirm("Are you sure you want to delete?");
            if (x)
                return true;
            else
                return false;
        }

        function calcredits() {
            var count = document.getElementById('<%=hdnCount.ClientID%>').value;
            var total_credits = 0;
            var credits;
            debugger;
            for (var i = 0; i < count; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_chkAccept');
                credits = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_hdnCredits').value;
                if (chk.type == 'checkbox') {
                    if (chk.checked) {
                        total_credits += parseFloat(credits);
                    }
                }
            }
            // alert(total_credits);
            // document.getElementById('<%--=lblTotalRegCredits.ClientID--%>').value = parseFloat (total_credits);
            var cre = document.getElementById('<%=lblTotalRegCredits.ClientID%>');//.value
            $("#<%=lblTotalRegCredits.ClientID%>").text(total_credits + '.00');


            // cre = total_credits;
            //alert(cre);
        }

        function validateSession() {
            var session = $("[id*=ctl00_ContentPlaceHolder1_ddlSession]").val();
            if (session == "0") {
                alert('Please select Session Name.');
                return false;
            }
            else
                return true;

        }

        function electivevalidatation(elect) {
            debugger;
            try {

                var numberOf = $('[id*=tblCurrentSubjects] input:checkbox').length;
                var myarray = Array();
                var myid = '' + elect.id + '';
                myarray = myid.split('_');

                var electcount = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + myarray[3] + '_hdfGropupno').value;
                var lblelectGroupName = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + myarray[3] + '_lblelectGroup').innerHTML;
                var hdfElectChoice = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + myarray[3] + '_hdfElectChoice').value;
                //alert(hdfElectChoice)
                var count = 0;
                for (var i = 0; i < numberOf; i++) {
                    var checkaccept = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_chkAccept');
                    var lblElective = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_lblElective').innerHTML;
                    var lelectcount = document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + i + '_hdfGropupno').value;
                    if (lblElective == "Yes" && checkaccept.checked && electcount == lelectcount) {
                        count++;
                    }
                }
                if (count > hdfElectChoice && hdfElectChoice > 0) {
                    if (hdfElectChoice == "1") {
                        alert('As per course registration criteria you can able to select any ' + hdfElectChoice + ' subject from ' + lblelectGroupName + '.');
                    }
                    else {
                        alert('As per course registration criteria you can able to select any ' + hdfElectChoice + ' subjects from ' + lblelectGroupName + '.');
                    }
                    document.getElementById(myid).checked = false;
                }
                else {
                    calcredits();
                }
            }
            catch (err) {
                alert('Error : ' + err.message);
            }
        }
    </script>


    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").hide();

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
                        //$("#<%= pnltextbox.ClientID %>").hide();

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
    <%--Search Box Script End--%>
</asp:Content>


