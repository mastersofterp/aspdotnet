<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Comprehensive_Stud_Report.aspx.cs" Inherits="ACADEMIC_Comprehensive_Stud_Report"
    Title="" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_rdolistSemester_0 {
            margin-right: 5px;
        }

        .bg-light-blue {
            border-top: 1px solid #e5e5e5;
        }
    </style>--%>
    <%--  <script>
        $.noconflict();
    </script>--%>
    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $.noConflict();
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

    <script>
        function SetUniqueRadioButton(current) {
            var tbl = document.getElementById('tblSearchResults');
            if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                for (i = 0; i < tbl.rows.length - 1; i++) {
                    var elm = document.getElementById('ctl00_ContentPlaceHolder1_rdolistsession_ctrl' + i + '_rdolistsession');
                    if (elm.type == 'radio') {
                        elm.checked = false;
                    }
                }
            }
            current.checked = true;
        }

    </script>



    <style>
        .row {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            /*margin-right: 0px;
            margin-left: 0px;*/
        }

        .sidebar-all {
            background-color: #ffffff;
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

        .nav-tabs.sidebar-menu .nav-item {
            margin-bottom: 0px;
            border-bottom: 1px solid #ccc;
            width: 100%;
        }

        .nav-tabs.sidebar-menu .nav-link {
            display: block;
            padding: 0.5rem 0.5rem;
            color: var(--dyanamic-tabs-color);
            font-weight: 600;
            border-top-left-radius: 0rem;
            border-top-right-radius: 0rem;
        }

        .nav-tabs-custom .nav-tabs .nav-item.show .nav-link, .nav-tabs-custom .nav-tabs .nav-link.active {
            color: var(--primary-color);
            background-color: var(--main-white);
            border-top: 1px solid #ccc;
            border-color: var(--main-white) var(--main-white) var(--main-white);
        }

        .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
            border-color: #ccc var(--main-white) var(--main-white);
            color: var(--primary-color);
        }

        #ctl00_ContentPlaceHolder1_divtabs {
            box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
            padding: 15px 10px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%-- <h3 class="box-title">COMPREHENSIVE STUDENT INFORMATION</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                    <%--Search Pannel Start by Swapnil --%>
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
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>

                                            <%--onchange=" return ddlSearch_change();"--%>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return Validate()"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <%-- <label id="lblDropdown"></label>--%>
                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-lg-3 col-md-12 col-12 btn-footer mt-3">
                                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                        </div>
                                    </div>
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
                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Sr. No.
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Adm. Status
                                                                        </th>
                                                                        <th style="display: none">IdNo
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
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
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
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
                                <asp:PostBackTrigger ControlID="Button1" />
                                <asp:PostBackTrigger ControlID="ddlSearch" />

                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <%--Search Pannel End--%>

                    <asp:UpdatePanel ID="updStudentInfo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="divStudent" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="nav-tabs-custom">
                                        <div class="row">
                                            <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                                <aside class="sidebar">
                                                    <!-- sidebar: style can be found in sidebar.less -->
                                                    <section class="sidebar-all">
                                                        <ul class="nav nav-tabs sidebar-menu" role="tablist">

                                                            <li class="nav-item">
                                                                <a class="nav-link active" data-toggle="tab" href="#tab_1" onclick="return Checktabid(this)">Student Information</a>
                                                            </li>
                                                            <li class="nav-item" runat="server" id="divYearWiseFees" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_YearWiseFees">Year Wise Fee Details</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_2" onclick="return Checktabid(this)">Fees Details</a>
                                                            </li>
                                                            <li class="nav-item d-none">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_3" onclick="return Checktabid(this)">Certificates Details</a>
                                                            </li>
                                                            <li class="nav-item" runat="server" id="divOtherFees" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_OtherFees">Other Fee Details</a>
                                                            </li>
                                                            <li class="nav-item d-none">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_4" onclick="return Checktabid(this)">Student Refund</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_5" onclick="return Checktabid(this)">Course Registered</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_6" onclick="return Checktabid(this)">Attendance Details</a>
                                                            </li>
                                                            <li class="nav-item d-none">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_7" onclick="return Checktabid(this)">Internal Marks Details</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_8" onclick="return Checktabid(this)">Result Details</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_9" onclick="return Checktabid(this)" runat="server" id="navReval">Revaluation Result Details</a>
                                                            </li>
                                                            <li class="nav-item" id="divrealease" runat="server" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_10">Intermediate Garde Realease</a>
                                                            </li>
                                                            <li class="nav-item" id="DivStudentData" runat="server" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_11">Student Promotion Status</a>
                                                            </li>
                                                            <li class="nav-item" id="divMITExcel" runat="server" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_12">Marks Details</a>
                                                            </li>
                                                            <li class="nav-item" id="divInternalMarks" runat="server" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_13" onclick="return Checktabid(this)">Internal Marks</a>
                                                            </li>
                                                            <li class="nav-item" id="divInternalMarks1" runat="server" visible="false">
                                                                <a class="nav-link" data-toggle="tab" href="#tab_14" onclick="return Checktabid(this)">Internal Marks</a>
                                                            </li>
                                                        </ul>
                                                    </section>
                                                </aside>

                                            </div>

                                            <div class="col-lg-10 col-md-8 col-12 mt-3">
                                                <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="tab-content" id="my-tab-content">
                                                    <div class="tab-pane active" id="tab_1">
                                                        <div id="divStudentInfo">
                                                            <div class="col-md-12">
                                                                <div class="sub-heading">
                                                                    <h5>Student Information</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-lg-6 col-md-6 col-12">
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Gender :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblGender" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Father's Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Mother's Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <%-- <li class="list-group-item"><b>Admission Status :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>--%>
                                                                            <li class="list-group-item"><b>
                                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Application ID :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblApplicationId" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Admission Batch :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Academic Year :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Admission Date :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAdmDate" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Admission Status :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAdmStatus" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCAN_1").ToString().Equals("CANCELLED")?System.Drawing.Color.Red:System.Drawing.Color.Green %>' Text='<%# Eval("ADMCAN_1")%>'></asp:Label>
                                                                                    <%--<asp:Label ID="lblAdmStatus" runat="server" Font-Bold="True"></asp:Label>--%>

                                                                                </a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Allotted category :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAllotedCategory" Font-Bold="true" runat="server"></asp:Label>
                                                                                    <%--<asp:Label ID="lblAdmStatus" runat="server" Font-Bold="True"></asp:Label>--%>

                                                                                </a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Admission Type :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAdmissionType" Font-Bold="true" runat="server"></asp:Label>
                                                                                    <%--<asp:Label ID="lblAdmStatus" runat="server" Font-Bold="True"></asp:Label>--%>
                                                                                </a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Current Year :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblCurrentYear" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Taluka :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblPTaluka" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>District :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblPDistrict" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>City :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblPCity" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Local Address :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblLAdd" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Permanent Address :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblPAdd" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Physical Handicapped :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblHandicap" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblBankName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Bank Account Number :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblBankAccountNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>IFSC Code :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblIfscCode" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Bank Address :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblBankAddress" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                    <div class="col-lg-6 col-md-6 col-12">
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>School/Institute Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblSchool" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Degree :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Branch :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Semester :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Scheme :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Division :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblSection" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Roll No. :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblRollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Mobile No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Alternate Mobile No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAlternateMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Aadhar Number :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAadharNumber" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Email ID :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Date of Birth :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Caste :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblCaste" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Payment Type :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Category :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Nationality :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Religion :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblReligion" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item" id="lifatheralive" runat="server" visible="false"><b>Father Alive :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFatherAlive" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Father Occupation :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFatherOccupation" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Father Income :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFatherIncome" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Mother Occupation :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblMotherOccupation" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Guardian name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblGuardianName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Father Mobile No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFatherMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Mother Mobile No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblMotherMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Faculty Advisor Name(Mentor) :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFacultyAdvname" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Class Advisor Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblClassAdvisorName" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Class Advisor Email Address :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblClsAdvEmailAddress" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Class Advisor Mobile No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblClsAdvMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                    <div class="col-lg-4 col-md-6 col-12">
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>Photo :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" /></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                    <%--<div class="col-12">--%>
                                                                    <%--<div class="row">--%>

                                                                    <%--</div>--%>
                                                                    <%--</div>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <div id="divRegistrationStatus" visible="false">
                                                                <asp:ListView ID="lvRegStatus" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Registration Status</h5>
                                                                        </div>
                                                                        <div class="sub-heading">
                                                                            <h5>Current Semester Registration Details</h5>
                                                                        </div>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Session
                                                                                        </th>
                                                                                        <th>Semester
                                                                                        </th>
                                                                                        <th>CCode
                                                                                        </th>
                                                                                        <th>Course Name
                                                                                        </th>
                                                                                        <th>Subject Type
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
                                                                                <%# Eval("SESSION") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SEMESTER") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CCODE") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COURSENAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SUBJECTTYPE") %>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <div id="divAttendance" style="display: block" runat="server" visible="false">
                                                                <div class="sub-heading">
                                                                    <h5>Student Attendance</h5>
                                                                </div>
                                                                <asp:ListView ID="lvAttendance" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Attendance Details</h5>
                                                                        </div>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Course Name
                                                                                        </th>
                                                                                        <th>Faculty Name
                                                                                        </th>
                                                                                        <th>Total Classes
                                                                                        </th>
                                                                                        <th>Present
                                                                                        </th>
                                                                                        <th>Absent
                                                                                        </th>
                                                                                        <th>Percentage
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
                                                                                <%# Eval("COURSENAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("UA_NAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TOTAL_CLASSES") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PRESENT") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ABSENT") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ATT_PERCENTAGE") %>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 table table-responsive" style="display: none">
                                                            <asp:GridView runat="server" Width="100%" OnRowDataBound="gvTestMark_RowDataBound"
                                                                OnRowCreated="gvTestMark_RowCreated" GridLines="None" CssClass="table table-hover table-bordered"
                                                                ID="gvTestMark">
                                                                <RowStyle HorizontalAlign="Center" ForeColor="Black"></RowStyle>
                                                                <HeaderStyle HorizontalAlign="left" CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_YearWiseFees">
                                                        <div id="divYearFees">
                                                            <div class="col-md-12">
                                                                <div class="sub-heading">
                                                                    <h5>Year Wise Fee Detail</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvYearFeesDetails" runat="server" Style="display: block;">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                    <tr>
                                                                                        <th>Year</th>
                                                                                        <th>Semester       <%--style="width:6%"--%>
                                                                                        </th>
                                                                                        <th>Branch
                                                                                        </th>
                                                                                        <th>Total Applied <%--style="width:10%"%>--%>
                                                                                        </th>
                                                                                        <th>Total paid    <%--style="width: 10%"--%>
                                                                                        </th>
                                                                                        <th>Balance <%--style="width:10%"--%>
                                                                                        </th>
                                                                                        <th>Excess    <%--style="width: 10%"--%>
                                                                                        </th>
                                                                                        <%--<th>View Receipt Details
                                                                    </th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("YEAR") %></td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("SEMESTER") %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("BRANCH") %>
                                                                            </td>
                                                                            </td>
                                                            <td><%--style="width:10%"--%>
                                                                <%# Eval("ADMFEE") %>
                                                            </td>
                                                                            <td><%--style="width: 10%"--%>
                                                                                <%# Eval("PADMFEE") %>
                                                                            </td>

                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("BADMFEE") %>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("EXCESS_AMOUNT") %>
                                                                            </td>
                                                                            <%--<td>--%>
                                                                            <%-- <%# Eval("REMARK") %>--%>
                                                                            <%--<asp:Button ID="btnView" runat="server" Text="View Receipt" Enabled='<%# (Convert.ToDecimal(Eval("PAID_AMOUNT"))>0) ? true: false %>' CommandArgument='<%# Eval("RECIEPT_CODE") %>' OnClick="btnView_Click" CssClass="btn btn-primary" />
                                                                <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />--%>
                                                                            <%--   </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_2">
                                                        <div id="divFees">
                                                            <div class="col-md-12">
                                                                <div class="sub-heading">
                                                                    <h5>Fees Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvFees" runat="server" Style="display: block;">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                    <tr>
                                                                                        <th>Year</th>
                                                                                        <th>Semester       <%--style="width:6%"--%>
                                                                                        </th>
                                                                                        <th>Branch
                                                                                        </th>
                                                                                        <th>Receipt Type   <%--style="width:15%"--%>
                                                                                        </th>
                                                                                        <th>Receipt No.   <%--style="width:15%"--%>
                                                                                        </th>
                                                                                        <th>Receipt Date   <%--style="width:15%"--%>
                                                                                        </th>
                                                                                        <th>Applied Amount <%--style="width:10%"%>--%>
                                                                                        </th>
                                                                                        <th>Amount paid    <%--style="width: 10%"--%>
                                                                                        </th>
                                                                                        <th>Excess Amount    <%--style="width: 10%"--%>
                                                                                        </th>
                                                                                        <th>Outstanding Amount <%--style="width:10%"--%>
                                                                                        </th>
                                                                                        <%--<th>View Receipt Details
                                                                    </th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("YEAR") %></td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("SEMESTER") %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("BRANCH") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("RECIEPT") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <asp:LinkButton runat="server" ID="lnkRecieptNo" Text='<%# Eval("REC_NO") %>' CommandArgument='<%# Eval("DCR_NO") %>' ToolTip='<%# Eval("CAN")%>' OnClick="lnkRecieptNo_Click"></asp:LinkButton>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("REC_DT") %>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("APPLIED_AMOUNT") %>
                                                                            </td>
                                                                            <td><%--style="width: 10%"--%>
                                                                                <%# Eval("PAID_AMOUNT") %>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("EXCESS_AMOUNT") %>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("OUTSTANDING_AMOUNT") %>
                                                                            </td>
                                                                            <%--<td>--%>
                                                                            <%-- <%# Eval("REMARK") %>--%>
                                                                            <%--<asp:Button ID="btnView" runat="server" Text="View Receipt" Enabled='<%# (Convert.ToDecimal(Eval("PAID_AMOUNT"))>0) ? true: false %>' CommandArgument='<%# Eval("RECIEPT_CODE") %>' OnClick="btnView_Click" CssClass="btn btn-primary" />
                                                                <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />--%>
                                                                            <%--   </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade d-none" id="tab_3">
                                                        <div class="col-12">
                                                            <div id="divCertificate">
                                                                <asp:ListView ID="lvCertificate" runat="server" class="mb-4">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Certificates Issued Details</h5>
                                                                        </div>
                                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                    <tr>
                                                                                        <th>Certificate Name
                                                                                        </th>
                                                                                        <th>Certificate No
                                                                                        </th>
                                                                                        <th>Issued Date
                                                                                        </th>
                                                                                        <th>Issued By
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div align="center">
                                                                            No Certificates Issued
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("CERTIFICATENAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CERTNO") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ISSUEDBY") %>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>

                                                            <div id="divRemark" class="mb-4" runat="server" visible="false">
                                                                <asp:ListView ID="lvRemark" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Remarks Details</h5>
                                                                        </div>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Remarks
                                                                                        </th>
                                                                                        <th>Given By Faculty
                                                                                        </th>
                                                                                        <th>Date
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div align="center">
                                                                            No Remarks Given
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td id="remak" runat="server">
                                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                                                            </td>
                                                                            <td id="UANO" runat="server">
                                                                                <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NO") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbluaName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                            </td>
                                                                            <td id="remarkDate" runat="server">
                                                                                <asp:Label ID="lblRemarkDate" runat="server" Text='<%# Eval("REMARK_DATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_OtherFees">
                                                        <div id="divOtherFee">
                                                            <div class="col-md-12">
                                                                <div class="sub-heading">
                                                                    <h5>Other Fee Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvOtherFees" runat="server" Style="display: block;">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                    <tr>
                                                                                        <th>Year</th>
                                                                                        <th>Semester       <%--style="width:6%"--%>
                                                                                        </th>
                                                                                        <th>Branch
                                                                                        </th>
                                                                                        <th>Receipt Type
                                                                                        </th>
                                                                                        <th>Receipt Date
                                                                                        </th>
                                                                                        <th>Total paid    <%--style="width: 10%"--%>
                                                                                        </th>
                                                                                        <th>Status
                                                                                        </th>

                                                                                        <%--<th>View Receipt Details
                                                                    </th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("YEAR") %></td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("SEMESTER") %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("BRANCH") %>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="lnkRecno" runat="server" Text='<%# Eval("RECNO") %>' OnClick="lnkRecno_Click" CommandArgument='<%# Eval("MISCDCRSRNO") %>'></asp:LinkButton>

                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("RECDT") %>
                                                                            </td>

                                                                            <td><%--style="width: 10%"--%>
                                                                                <%# Eval("PAIDFEES") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("REC_STATUS") %>
                                                                            </td>

                                                                            <%--<td>--%>
                                                                            <%-- <%# Eval("REMARK") %>--%>
                                                                            <%--<asp:Button ID="btnView" runat="server" Text="View Receipt" Enabled='<%# (Convert.ToDecimal(Eval("PAID_AMOUNT"))>0) ? true: false %>' CommandArgument='<%# Eval("RECIEPT_CODE") %>' OnClick="btnView_Click" CssClass="btn btn-primary" />
                                                                <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />--%>
                                                                            <%--   </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade d-none" id="tab_4">
                                                        <div id="divRefund" class="col-12">
                                                            <asp:ListView ID="lvRefund" runat="server" class="mb-4">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Fees Details</h5>
                                                                    </div>
                                                                    <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Sem.
                                                                                    </th>
                                                                                    <th>Payment Cat.
                                                                                    </th>
                                                                                    <th>Rec. Type
                                                                                    </th>
                                                                                    <th>Rec. No.
                                                                                    </th>
                                                                                    <th>Rec. Amt.
                                                                                    </th>
                                                                                    <th>Refunded Amt.
                                                                                    </th>
                                                                                    <th>Refundable Amt.
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
                                                                            <%# Eval("BRANCHSNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTER")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PTYPENAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RECIEPT_TITLE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REC_NO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DCR_AMT")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REFUND_AMT")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REFUNDABLE_AMT")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_5">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>



                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Course Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div id="divcourse" class="col-12">
                                                                    <asp:ListView ID="lvCourseReg" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                        <tr>
                                                                                            <th>Sr No.
                                                                                            </th>
                                                                                            <th>Semester
                                                                                            </th>
                                                                                            <th>CCode
                                                                                            </th>
                                                                                            <th>Course Name
                                                                                            </th>
                                                                                            <th>Subject Type
                                                                                            </th>
                                                                                            <th>Credits</th>
                                                                                            <th>Course Registration Status</th>
                                                                                            <th>Exam Registration Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: center; font-family: Arial; font-size: medium" class="info">
                                                                                No Record Found
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex +1 %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SEMESTER") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("CCODE") %>
                                                                                    <%--<asp:LinkButton ID="lnkccode" runat="server" Text='<%# Eval("CCODE") %>'
                                                                                        OnClick="lnkccode_Click" CommandArgument='<%# Eval("COURSENO") %>'
                                                                                        ToolTip='<%# Eval("COURSENO") %>'></asp:LinkButton>--%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("COURSENAME") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SUBJECTTYPE") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("CREDITS") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("REGISTERED") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("EXAM_REGISTERED") %> 
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_6">
                                                        <div id="divAttendanceDetails">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Attendance Details</h5>
                                                                        </div>
                                                                        <asp:ListView ID="lvAttendanceDetails" class="" runat="server" Style="display: block;">
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                            <tr>
                                                                                                <th>Course Code
                                                                                                </th>
                                                                                                <th>Course Name
                                                                                                </th>
                                                                                                <th>Subject Type
                                                                                                </th>
                                                                                                <th>Total Classes
                                                                                                </th>
                                                                                                <th>Total Present
                                                                                                </th>
                                                                                                <th>Total Attendance(%)
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; font-size: medium">
                                                                                    No Record Found
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <%--<%# Eval("COURSE CODE") %>--%>
                                                                                        <asp:LinkButton ID="lnkccode" runat="server" Text='<%# Eval("COURSE CODE") %>'
                                                                                            OnClick="lnkccode_Click" CommandArgument='<%# Eval("COURSENO") %>'
                                                                                            ToolTip='<%# Eval("COURSENO") %>'></asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("COURSENAME") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("SUBJECT TYPE") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("TOTAL CLASSES") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("TOTAL PRESENT") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("ATTENDANCE%") %>
                                                                                    </td>
                                                                                </tr>

                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <%--<Triggers>
                                                                <asp:PostBackTrigger ControlID="lvCourseReg" />
                                                            </Triggers>--%>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_7">
                                                        <div id="divIntern" class="hidden">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Internal Marks Details</h5>
                                                                </div>
                                                                <asp:ListView ID="lvInternalMarks" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="my-table">
                                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                    <tr>
                                                                                        <th>Course Code
                                                                                        </th>
                                                                                        <th>Course Name
                                                                                        </th>
                                                                                        <th>Subject Type
                                                                                        </th>
                                                                                        <th>CA-I</th>
                                                                                        <th>CA-II</th>
                                                                                        <th>CA-III</th>
                                                                                        <th>CA-IV</th>
                                                                                        <th>Attendance</th>
                                                                                        <th>PCA-I</th>
                                                                                        <th>PCA-II</th>
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
                                                                                <%# Eval("CCODE") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COURSE_NAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SUBNAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CA-I") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CA-II") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CA-III") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("CA-IV") %>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval ("ATTENDANCE") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PCA-I") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("PCA-II") %>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="tab-pane fade" id="tab_8">
                                                        <div id="divPreviousResult">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Result Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:RadioButtonList ID="rdolistSemester" runat="server" GroupName="Exam" Style="cursor: default; margin-right: 50px; font-weight: bold; font-size: x-large" RepeatDirection="Horizontal"
                                                                    OnClientClick="SetUniqueRadioButton(this)" OnSelectedIndexChanged="rdolistSemester_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:RadioButtonList>
                                                            </div>

                                                            <asp:Panel ID="pnlCollege" runat="server" Visible="true">
                                                                <div id="Table3" runat="server" class="col-12 mt-3">

                                                                    <asp:ListView ID="lvSession" runat="server" OnItemDataBound="lvSession_ItemDataBound">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Semesterwise History Details
                                                                                </h5>
                                                                            </div>

                                                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                        <tr>
                                                                                            <th style="width: 10%;">Show
                                                                                            </th>
                                                                                            <th style="width: 12%;">Session
                                                                                            </th>
                                                                                            <th style="width: 10%;">Section
                                                                                            </th>
                                                                                            <th style="width: 7%;">Registered Courses
                                                                                            </th>
                                                                                            <th style="width: 5%;">Registered Credits
                                                                                            </th>
                                                                                            <th style="width: 5%;">Earned Credits
                                                                                            </th>
                                                                                            <%--<th style="width:5%;">CumCr.
                                                                        </th>
                                                                        <th style="width:10%;">Result
                                                                        </th>--%>
                                                                                            <th style="width: 10%;">SGPA
                                                                                            </th>

                                                                                            <%--<th style="width:10%;">Cum.EGP
                                                                        </th>--%>
                                                                                            <th style="width: 10%;">CGPA
                                                                                            </th>
                                                                                            <th style="width: 10%;">Result Date
                                                                                            </th>
                                                                                            <th style="width: 10%;" id="printreport">Print
                                                                                            </th>


                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <%--<tr id="Tr7" runat="server" />--%>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>

                                                                            <table class="table table-hover table-bordered">
                                                                                <div id="MAIN" runat="server" class="col-md-12">
                                                                                   
                                                                                        <tr>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                                                </asp:Panel>
                                                                                                &nbsp;&nbsp;<asp:Label ID="lbIoNo" runat="server" Text='<%# Eval("SEMESTERNO") %>'
                                                                                                    ToolTip='<%# Eval ("IDNO") %>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 12%;">
                                                                                                <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSION_NAME")%>' ToolTip='<%# Eval("SESSIONNO") %>'></asp:Label>
                                                                                            </td>

                                                                                            <td style="width: 10%;">
                                                                                                <asp:Label ID="lblsectionname" runat="server" Text='<%# Eval("SECTIONNAME")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 8%;">
                                                                                                <asp:Label ID="lbltotsubreg" runat="server" Text='<%# Eval("TOTAL_SUBJ_REGD")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 7%;">
                                                                                                <asp:Label ID="lblRegCredits" runat="server" Text=' <%# Eval("REGD_CREDITS")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 5%;">
                                                                                                <asp:Label ID="lblEarncredits" runat="server" Text=' <%# Eval("EARN_CREDITS")%>'></asp:Label>
                                                                                            </td>
                                                                                            <%-- <td style="width:5%;">
                                                                                <asp:Label ID="lblCumCredits" runat="server" Text='<%# Eval("CUMMULATIVE_CREDITS") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblResult" runat="server" Text='<%# Eval("PASSFAIL") %>'></asp:Label>
                                                                            </td>--%>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:Label ID="lblSgpa" runat="server" Text='<%# Eval("SGPA") %>'></asp:Label>
                                                                                            </td>

                                                                                            <%--<td style="width:10%;">
                                                                                <asp:Label ID="lblCummegp" runat="server" Text='<%# Eval("CUMMULATIVE_CREDITS") %>'></asp:Label>
                                                                            </td>--%>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:Label ID="lblCummegp" runat="server" Text='<%# Eval("CGPA") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                                <%# Eval("RESULTDATE")%>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:ImageButton runat="server" ImageUrl="~/Images/print.png" ID="imgbtnpreview" OnClick="imgbtnpreview_Click" ToolTip='<%# Eval("SESSIONNO") %>' />
                                                                                            </td>

                                                                                            <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                                                Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                                                ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                                            </ajaxToolKit:CollapsiblePanelExtender>

                                                                                        </tr>
                                                                                  
                                                                                </div>
                                                                            </table>

                                                                            <div class="col-12">
                                                                                <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">
                                                                                    <div class="sub-heading">
                                                                                        <h5>Students Session wise Details 
                                                                                        </h5>
                                                                                    </div>

                                                                                    <asp:ListView ID="lvDetails" runat="server" Visible="false">
                                                                                        <LayoutTemplate>
                                                                                            <div class="table-responsive">
                                                                                                <table class="table table-hover table-bordered">
                                                                                                    <thead class="header bg-light-blue">
                                                                                                        <tr>
                                                                                                            <th>Course Name
                                                                                                            </th>
                                                                                                            <th>Exam Type
                                                                                                            </th>
                                                                                                            <th>Course Type
                                                                                                            </th>
                                                                                                            <th>Credits
                                                                                                            </th>
                                                                                                            <th>Grade Point
                                                                                                            </th>
                                                                                                            <th>Grade
                                                                                                            </th>
                                                                                                             <th>Earned Grade Points</th>
                                                                                                            <th>Result
                                                                                                            </th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </div>
                                                                                        </LayoutTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                                No Record Found
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval ("COURSENAME") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("PREVSTATUS") %>' ToolTip='<%# Eval("PREV_STATUS") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbltheory" runat="server" Text='<%# Eval("SUBTYPE") %>' ToolTip='<%# Eval("SUBID") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align:center;">
                                                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("CREDITS") %>'></asp:Label>
                                                                                                </td>

                                                                                                <td style="text-align:center;">
                                                                                                    <asp:Label ID="lblgdpoint" runat="server" Text='<%# Eval("GDPOINT") %>' ToolTip='<%# Eval("GDPOINT") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align:center;">
                                                                                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE") %>' ToolTip='<%# Eval("GRADE") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align:center;">
                                                                                                    <asp:Label ID="lblEgp" runat="server" Text='<%# Eval("EGP") %>' ToolTip='<%# Eval("EGP") %>'></asp:Label>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td style="text-align:center;">
                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RESULT") %>' ToolTip='<%# Eval("RESULT") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>

                                                                                    <%--Added by lalit On dated 24-05-2023--%>
                                                                                    <asp:ListView ID="lvMarksDetails" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <div class="table-responsive">
                                                                                                <table class="table table-hover table-bordered">
                                                                                                    <thead class="header bg-light-blue">
                                                                                                        <tr>
                                                                                                            <th>Sr.No
                                                                                                            </th>
                                                                                                            <th>Subject Name
                                                                                                            </th>
                                                                                                            <th>Max Marks
                                                                                                            </th>
                                                                                                            <th>Min Passing Marks
                                                                                                            </th>
                                                                                                            <th>Obtained Marks
                                                                                                            </th>
                                                                                                            <th>Remark
                                                                                                            </th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                    <tfoot>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                            <td><strong>Total :</strong></td>

                                                                                                            <td>
                                                                                                                <strong>
                                                                                                                    <asp:Label ID="lblTotalM" runat="server"></asp:Label>
                                                                                                                </strong>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <strong>
                                                                                                                    <asp:Label ID="Label20" runat="server"></asp:Label>
                                                                                                                </strong>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <strong>
                                                                                                                    <asp:Label ID="Label21" runat="server"></asp:Label>
                                                                                                                </strong>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <strong>
                                                                                                                    <asp:Label ID="Label22" runat="server"></asp:Label>
                                                                                                                </strong>
                                                                                                            </td>

                                                                                                        </tr>
                                                                                                    </tfoot>

                                                                                                </table>
                                                                                            </div>
                                                                                        </LayoutTemplate>

                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <%# Container.DataItemIndex +1 %>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <%--  <asp:Label ID="Label20" runat="server" Text='<%# Eval("PREVSTATUS") %>' ToolTip='<%# Eval("PREV_STATUS") %>'></asp:Label>--%>
                                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("COURSENAME") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbltheory" runat="server" Text='<%# Eval("MAXMARKS_E") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("MINMARKS") %>'></asp:Label>
                                                                                                </td>

                                                                                                <td>
                                                                                                    <asp:Label ID="lblgdpoint" runat="server" Text='<%# Eval("EXTERMARK") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("RESULT") %>'></asp:Label>
                                                                                                    <%-- <asp:HiddenField ID="hdftotal" runat="server" Value='<%# Eval("MAXMarks") %>' />--%>
                                                                                                </td>

                                                                                            </tr>

                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>

                                                                                </asp:Panel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <div class="col-12">
                                                                        <asp:Label ID="lblCopycase" runat="server" Text="" Visible="false"></asp:Label>
                                                                        <span style="color: red; font-style: italic">
                                                                            <asp:Label ID="lblCopycaseRemark" runat="server" Text=""></asp:Label></span>
                                                                        <asp:Label ID="lblGrievance" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </div>

                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_9">
                                                        <div id="divrevalresult" runat="server">

                                                            <div id="divreval1" runat="server" class="col-12">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Revaluation Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12">
                                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" GroupName="Exam" Style="cursor: default; margin-right: 50px; font-weight: bold; font-size: x-large" RepeatDirection="Horizontal"
                                                                        OnClientClick="SetUniqueRadioButton(this)" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <asp:Panel ID="pnlrevalresult" runat="server" Visible="false">
                                                                    <div class="col-12">
                                                                        <asp:ListView ID="lvRevalDetails" runat="server">
                                                                            <LayoutTemplate>
                                                                                <div class="sub-heading">
                                                                                    <h5>Student Semesterwise History Details(Revaluation)
                                                                                    </h5>
                                                                                </div>

                                                                                <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                            <tr>
                                                                                                <th>Course Name
                                                                                                </th>
                                                                                                <th>Exam Type
                                                                                                </th>
                                                                                                <th>Course Type
                                                                                                </th>
                                                                                                <th>Credits
                                                                                                </th>
                                                                                                <%--  <th>Original Mark
                                                                                            </th>                                                                                      
                                                                                            <th>Revaluation Mark
                                                                                            </th>  --%>
                                                                                                <th>GDPoint
                                                                                                </th>
                                                                                                <%--  <th>Old_Grade
                                                                                            </th>--%>
                                                                                                <th>Grade
                                                                                                </th>

                                                                                                <th>Result
                                                                                                </th>

                                                                                                <th>Revaluation Status
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server">
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <%--<div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                No Record Found
                                                                            </div>--%>
                                                                            </EmptyDataTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval ("COURSENAME") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("PREVSTATUS") %>' ToolTip='<%# Eval("PREV_STATUS") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbltheory" runat="server" Text='<%# Eval("SUBTYPE") %>' ToolTip='<%# Eval("SUBID") %>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("CREDITS") %>'></asp:Label>
                                                                                    </td>
                                                                                    <%--         <td>
                                                                                    <asp:Label ID="lbloriginalmark" runat="server" Text='<%# Eval("ORIGINAL_MARK") %>' ToolTip='<%# Eval("ORIGINAL_MARK") %>'></asp:Label>
                                                                                </td>
                                                                              
                                                                                <td>
                                                                                    <asp:Label ID="lblrevalmarks" runat="server" Text='<%# Eval("REVAL_MARK") %>' ToolTip='<%# Eval("REVAL_MARK") %>'></asp:Label>
                                                                                </td>--%>

                                                                                    <td>
                                                                                        <asp:Label ID="lblgdpoint" runat="server" Text='<%# Eval("GDPOINT") %>' ToolTip='<%# Eval("GDPOINT") %>'></asp:Label>
                                                                                    </td>

                                                                                    <%--<td>
                                                                                    <asp:Label ID="lbloldgrade" runat="server" Text='<%# Eval("OLD_GRADE") %>' ToolTip='<%# Eval("OLD_GRADE") %>'></asp:Label>
                                                                                </td>--%>

                                                                                    <td>
                                                                                        <asp:Label ID="lblnewgrade" runat="server" Text='<%# Eval("NEW_GRADE") %>' ToolTip='<%# Eval("NEW_GRADE") %>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblresult" runat="server" Text='<%# Eval("RESULT") %>' ToolTip='<%# Eval("RESULT") %>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblrevalstatus" runat="server" Text='<%# Eval("STATUS") %>' ToolTip='<%# Eval("STATUS") %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </asp:Panel>

                                                                <div class="col-12">
                                                                    <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
                                                                    <span style="color: red; font-style: italic">
                                                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label></span>
                                                                    <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_10">
                                                        <div id="divGraderealease">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Intermediate Garde Realease</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvGradeRealease" runat="server" Style="display: block;">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Reg no     
                                                                                        </th>
                                                                                        <th>Subject Code     
                                                                                        </th>
                                                                                        <th>Subject Name
                                                                                        </th>
                                                                                        <th>Grade Release Name 
                                                                                        </th>
                                                                                        <th>Grade 
                                                                                        </th>
                                                                                        <th>Grade Release Name 
                                                                                        </th>
                                                                                        <th>Grade
                                                                                        </th>
                                                                                        <th>Grade Release Name 
                                                                                        </th>
                                                                                        <th>Grade
                                                                                        </th>
                                                                                        <th>Grade Release Name 
                                                                                        </th>
                                                                                        <th>Grade  
                                                                                        </th>
                                                                                        <th>Grade Release Name 
                                                                                        </th>
                                                                                        <th>Grade   
                                                                                        </th>

                                                                                        <%--<th>View Receipt Details
                                                                    </th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("REGNO") %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("CCODE") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("COURSENAME") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADEREALSE1") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADE1") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADEREALSE2") %>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("GRADE2") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADEREALSE3") %>
                                                                            </td>
                                                                            <td><%--style="width: 10%"--%>
                                                                                <%# Eval("GRADE3") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADEREALSE4")%>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("GRADE4") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("GRADEREALSE5")%>
                                                                            </td>
                                                                            <td><%--style="width:10%"--%>
                                                                                <%# Eval("GRADE5") %>
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="tab-pane fade" id="tab_11">
                                                        <div>
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Student Promotion Status</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvProt" runat="server" Style="display: block;">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>SR NO     
                                                                                        </th>
                                                                                        <th>ACADEMIC YEAR     
                                                                                        </th>
                                                                                        <th>YEAR     
                                                                                        </th>
                                                                                        <th>REMARKS
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Container.DataItemIndex +1 %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("ACADEMIC_YEAR") %>
                                                                            </td>
                                                                            <td><%--style="width:6%"--%>
                                                                                <%# Eval("YEAR1") %>
                                                                            </td>
                                                                            <td><%--style="width:15%"--%>
                                                                                <%# Eval("REMARKS") %>
                                                                            </td>



                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_12">
                                                        <div>
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Marks Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12" id="div1">
                                                                <asp:Button ID="btnInternalMarks" runat="server" Text="Download Excel For(Marks)" CssClass="btn btn-primary" OnClick="btnInternalMarks_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_13">
                                                        <div>
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Internal Marks</h5>
                                                                </div>
                                                                <div id="divInternal">
                                                                    <div class="col-12">
                                                                        <asp:ListView ID="lvInternalData" runat="server" Style="display: block;">
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
                                                                                                <%--<th colspan="3" style="text-align: center">SUBJECT </th>--%>
                                                                                                <th id="tbl_Rule1" colspan="17" style="text-align: center">Internal Marks </th>
                                                                                                <%-- <th id="tbl_Rule2" colspan="1" style="text-align: center; display: none;">RULE 2 </th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>CCODE</th>
                                                                                                <th>SUBJECT NAME</th>
                                                                                                <th id="th0" style="text-align: center; display: none;"></th>
                                                                                                <th id="th1" style="text-align: center; display: none;"></th>
                                                                                                <th id="th2" style="text-align: center; display: none;"></th>
                                                                                                <th id="th3" style="text-align: center; display: none;"></th>
                                                                                                <th id="th4" style="text-align: center; display: none;"></th>
                                                                                                <th id="th5" style="text-align: center; display: none;"></th>
                                                                                                <th id="th6" style="text-align: center; display: none;"></th>
                                                                                                <th id="th7" style="text-align: center; display: none;"></th>
                                                                                                <th id="th8" style="text-align: center; display: none;"></th>
                                                                                                <th id="th9" style="text-align: center; display: none;"></th>
                                                                                                <th id="th10" style="text-align: center; display: none;"></th>
                                                                                                <th id="th11" style="text-align: center; display: none;"></th>
                                                                                                <th id="th12" style="text-align: center; display: none;"></th>
                                                                                                <th id="th13" style="text-align: center; display: none;"></th>
                                                                                                <th id="th14" style="text-align: center; display: none;"></th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; font-size: medium">
                                                                                    No Record Found
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <%#Eval("CCODE") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("COURSE_NAME") %>
                                                                                    </td>
                                                                                    <td id="td0" runat="server" style="display: none;">
                                                                                        <%-- <asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" Enabled="false" MaxLength="5" ></asp:TextBox>--%>
                                                                                        <asp:Label ID="Label5" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td1" runat="server" style="display: none;">
                                                                                        <%-- <asp:TextBox ID="txtCat1asn"  runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"></asp:TextBox>--%>
                                                                                        <asp:Label ID="Label6" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td2" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label7" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td3" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label8" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td4" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label9" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td5" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label10" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td6" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label11" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td7" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label12" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td8" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label13" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td9" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label14" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td10" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label15" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td11" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label16" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td12" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label17" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td13" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label18" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td14" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label19" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>

                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_14">
                                                        <div>
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Internal Marks</h5>
                                                                </div>
                                                                <div id="div3">
                                                                    <div class="col-12">
                                                                        <asp:ListView ID="lvInter" runat="server" Style="display: block;">
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
                                                                                                <%--<th colspan="3" style="text-align: center">SUBJECT </th>--%>
                                                                                                <th id="tbl_Rule1" colspan="17" style="text-align: center">Internal Marks </th>
                                                                                                <%-- <th id="tbl_Rule2" colspan="1" style="text-align: center; display: none;">RULE 2 </th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th id="th0" style="text-align: center; display: none;"></th>
                                                                                                <th id="th1" style="text-align: center; display: none;"></th>
                                                                                                <th id="th2" style="text-align: center; display: none;"></th>
                                                                                                <th id="th3" style="text-align: center; display: none;"></th>
                                                                                                <th id="th4" style="text-align: center; display: none;"></th>
                                                                                                <th id="th5" style="text-align: center; display: none;"></th>
                                                                                                <th id="th6" style="text-align: center; display: none;"></th>
                                                                                                <th id="th7" style="text-align: center; display: none;"></th>
                                                                                                <th id="th8" style="text-align: center; display: none;"></th>
                                                                                                <th id="th9" style="text-align: center; display: none;"></th>
                                                                                                <th id="th10" style="text-align: center; display: none;"></th>
                                                                                                <th id="th11" style="text-align: center; display: none;"></th>
                                                                                                <th id="th12" style="text-align: center; display: none;"></th>
                                                                                                <th id="th13" style="text-align: center; display: none;"></th>
                                                                                                <th id="th14" style="text-align: center; display: none;"></th>
                                                                                                 <th id="th15" style="text-align: center; display: none;"></th>
                                                                                                <th id="th16" style="text-align: center; display: none;"></th>
                                                                                                 <th id="th17" style="text-align: center; display: none;"></th>
                                                                                                <th id="th18" style="text-align: center; display: none;"></th>
                                                                                                 <th id="th19" style="text-align: center; display: none;"></th>
                                                                                                <th id="th20" style="text-align: center; display: none;"></th>
                                                                                               <th id="th21" style="text-align: center; display: none;"></th>
                                                                                                <th id="th22" style="text-align: center; display: none;"></th>
                                                                                                <th id="th23" style="text-align: center; display: none;"></th>
                                                                                                <th id="th24" style="text-align: center; display: none;"></th>
                                                                                                 <th id="th25" style="text-align: center; display: none;"></th>
                                                                                                
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; font-size: medium">
                                                                                    No Record Found
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td id="td0" runat="server" style="display: none;">
                                                                                        <%-- <asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" Enabled="false" MaxLength="5" ></asp:TextBox>--%>
                                                                                        <asp:Label ID="Label5" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td1" runat="server" style="display: none;">
                                                                                        <%-- <asp:TextBox ID="txtCat1asn"  runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"></asp:TextBox>--%>
                                                                                        <asp:Label ID="Label6" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td2" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label7" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td3" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label8" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td4" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label9" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td5" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label10" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td6" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label11" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td7" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label12" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td8" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label13" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td9" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label14" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                    <td id="td10" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label15" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td11" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label16" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td12" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label17" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                    <td id="td13" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label18" runat="server" Enabled="false" MaxLength="5"></asp:Label>

                                                                                    </td>
                                                                                     
                                                                                    <td id="td14" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label19" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                         <td id="td15" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label20" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     
                                                                                    <td id="td16" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label21" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td17" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label22" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td18" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label23" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td> 
                                                                                    <td id="td19" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label24" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td20" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label25" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td21" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label26" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td22" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label27" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td23" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label28" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td24" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label29" runat="server" Enabled="false" MaxLength="5"></asp:Label>
                                                                                    </td>
                                                                                     <td id="td25" runat="server" style="display: none;">
                                                                                        <asp:Label ID="Label30" runat="server" Enabled="false" MaxLength="5"></asp:Label>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdolistSemester" />
                             <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                            <asp:PostBackTrigger ControlID="btnInternalMarks" />

                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Receipt Info <i class="fa fa-info-circle"></i></h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPopUP"
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
                <asp:UpdatePanel ID="updPopUP" runat="server">
                    <ContentTemplate>
                        <div class="col-12">
                            <asp:Panel ID="pnlPopUp" runat="server">
                                <%--Height="300px" Width="720px" Style="overflow-x: hidden;" ScrollBars="Vertical"--%>
                                <%--   <div class="row">--%>
                                <asp:ListView ID="lvReceipt" runat="server" align="Center">
                                    <LayoutTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Semester
                                                        </th>
                                                        <th>Receipt No.
                                                        </th>
                                                        <th>Receipt Date
                                                        </th>
                                                        <th>Applied Amount
                                                        </th>
                                                        <th>Paid Amount
                                                        </th>
                                                        <th>Outstanding Amount
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
                                                <%#Eval("Semster") %>
                                            </td>
                                            <td>
                                                <%#Eval("REC_NO") %>
                                            </td>
                                            <td>
                                                <%#Eval("REC_DATE") %><%--<asp:Label ID="lblReceiptDate" runat="server" Text='<%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>'></asp:Label>--%>
                                                    
                                            </td>
                                            <td>
                                                <%#Eval("APPLIED_AMT") %>
                                                <%--<asp:Label ID="lblAppliedAmount" runat="server" Text='<%#Eval("APPLIED_AMOUNT") %>'></asp:Label>--%>
                                            </td>
                                            <td>
                                                <%#Eval("PAID_AMT") %>
                                            </td>
                                            <td>
                                                <%#Eval("BAL_AMT") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%-- </div>--%>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- The Modal -->
    <div class="modal" id="myModalCourse" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Attendance Detail</h4>
                    <button type="button" class="close" data-dismiss="modal"></button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <%--Height="300px" Width="720px" Style="overflow-x: hidden;" ScrollBars="Vertical"--%>
                                    <%--   <div class="row">--%>
                                    <asp:ListView ID="lvCourseAtt" runat="server" align="Center">
                                        <LayoutTemplate>
                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>Date
                                                            </th>
                                                            <th>Slot
                                                            </th>
                                                            <th>Status
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
                                                    <%#Eval("NO") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ATT_DATE") %>
                                                </td>
                                                <td>
                                                    <%#Eval("PERIOD") %><%--<asp:Label ID="lblReceiptDate" runat="server" Text='<%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>'></asp:Label>--%>
                                                    
                                                </td>
                                                <td>
                                                    <%#Eval("ATT_STATUS") %>
                                                    <%--<asp:Label ID="lblAppliedAmount" runat="server" Text='<%#Eval("APPLIED_AMOUNT") %>'></asp:Label>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%-- </div>--%>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
                <asp:HiddenField ID="hdfDyanamicTabId" runat="server" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showModalCourse() {
            $("#myModalCourse").modal('show');

        }

        $(function () {
            $("#lnkccode").click(function () {
                showModal();
            });
        });
    </script>
    <script>
        function toggleIcon(e) {
            $(e.target)
                .prev('.colapse-heading')
                .find(".more-less")
                .toggleClass('fa-minus fa-plus');
        }
        $('.colapse-panel').on('hide.bs.collapse', toggleIcon);
        $('.colapse-panel').on('show.bs.collapse', toggleIcon);
    </script>

    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }
    </script>
    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                //imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                //imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

        function toggleExpansion1(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "../IMAGES/plus.gif";
            }
        }
    </script>
    <div id="divMsg" runat="server">
    </div>

    <!--===<!--==== Table 100% width script ====-->
    <script>
        $('#accordion').on('shown.bs.collapse', function () {
            $($.fn.dataTable.tables(true)).DataTable()
               .columns.adjust();
        });
    </script>

    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            // $("#<%= pnltextbox.ClientID %>").hide();

            //$("#<%= pnlDropdown.ClientID %>").hide();
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
                s
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>

    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=Button1.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=Button1.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>
    <script>
        function TabShow(tabName) {
            //alert(tabName)
            //var tabName = "tab_8";
            $('#ctl00_ContentPlaceHolder1_divtabs a[href="#' + tabName + '"]').tab('show');
            $("#ctl00_ContentPlaceHolder1_divtabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
    <script>
        function Checktabid(tabid) {
            $("#ctl00_ContentPlaceHolder1_hdfDyanamicTabId").val($(tabid).attr("href").replace('#', ''));
        }
    </script>
    <%--Search Box Script End--%>
</asp:Content>
