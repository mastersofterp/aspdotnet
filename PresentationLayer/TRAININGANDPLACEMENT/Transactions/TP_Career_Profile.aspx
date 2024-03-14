<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Career_Profile.aspx.cs" Inherits="EXAMINATION_Projects_Career_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
   
    <body>
       <form>

            <style>
                .nav-tabs-custom .nav.nav-tabs {
                    display: block;
                    box-shadow: rgba(0, 0, 0, 0.2) 0px 5px 10px;
                    padding: 8px;
                }

                .nav-tabs-custom .nav-tabs .nav-link {
                    border: 0px solid transparent;
                    border-left: 2px solid #e8eaed;
                    color: #255282;
                }

                    .nav-tabs-custom .nav-tabs .nav-item.show .nav-link, .nav-tabs-custom .nav-tabs .nav-link.active {
                        color: #0d70fd;
                        background-color: #fff;
                        border: 0px solid transparent;
                        border-left: 2px solid #255282;
                        border-color: #ffffff #fff #fff #0d70fd;
                        border-top-left-radius: 0rem;
                        border-top-right-radius: 0rem;
                        padding: .4rem 1rem;
                    }

                    .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
                        color: #0d70fd;
                        border: 0px solid transparent;
                        border-left: 2px solid #0d70fd;
                        border-color: #ffffff #fff #fff #0d70fd;
                        border-top-left-radius: 0rem;
                        border-top-right-radius: 0rem;
                    }

                @media (max-width: 767px) {
                    .nav-tabs-custom .nav-tabs .nav-link {
                        padding: .2rem .6rem;
                    }
                }

                input[type=checkbox], input[type=radio] {
                    margin: 0 4px 0 0;
                }
            </style>

            <style>
                /*FIle download*/
                .download-excel:hover {
                    text-decoration: none !important;
                }

                .download-excel .filelabel-download {
                    border-color: var(--main-secondary-modern);
                }

                    .download-excel .filelabel-download:hover i {
                        border-color: #1665c4;
                        text-decoration: none !important;
                    }

                    .download-excel .filelabel-download:hover {
                        border: 2px solid #6777ef;
                    }

                    .download-excel .filelabel-download .name {
                        color: #1665c4;
                    }

                    .download-excel .filelabel-download:hover .name {
                        color: #1665c4;
                        text-decoration: underline;
                    }

                /*File UPload*/
                .filelabel, .filelabel-download {
                    border: 2px dashed grey;
                    border-radius: 5px;
                    display: block;
                    padding: 30px;
                    transition: border 300ms ease;
                    cursor: pointer;
                    text-align: center;
                    margin: auto;
                }

                    .filelabel i, .filelabel-download i {
                        display: block;
                        font-size: 30px;
                        padding-bottom: 5px;
                    }

                    .filelabel i, .filelabel .title, .filelabel-download i {
                        color: grey;
                        transition: 200ms color;
                    }

                    .filelabel:hover {
                        border: 2px solid #1665c4;
                    }

                        .filelabel:hover i, .filelabel-download:hover i {
                            color: #1665c4;
                            text-decoration: none;
                        }

                    .filelabel .choosefile {
                        color: #1665c4;
                    }

                        .filelabel .choosefile:hover {
                            color: #1665c4;
                            cursor: pointer;
                            text-decoration: underline;
                        }

                    .filelabel .warning {
                        font-size: 12px;
                        font-weight: 600;
                        display: none;
                        color: red;
                    }

                .file-upload-all {
                    display: none !important;
                }
            </style>
            <script type="text/javascript">
                function fireServerButtonEvent() {
                    debugger;
                    var hdWorkExp = document.getElementById("ctl00_ContentPlaceHolder1_hdWorkExp").value;
                    // alert("tanu");
                    if (hdWorkExp.value == 0) {
                        alert('Please First Proced Basic Details');
                        return;
                    }
                }
            </script>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Career Profile</span></h3>
                        </div>
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-4 col-12">
                                                <ul class="nav nav-tabs" role="tablist">
                                                    <li class="nav-item">
                                                        <a class="nav-link active" data-toggle="tab" href="#tab_0">Basic Details </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_1">Exam Details </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <%--        <asp:LinkButton ID="btnlinkfirsttab" runat="server"  OnClick="btnlinkfirsttab_Click" Text="Work Experience"  ></asp:LinkButton>--%>
                                                        <a class="nav-link" data-toggle="tab" href="#tab_2" onclick="fireServerButtonEvent()">Work Experience </a>
                                                        <asp:HiddenField ID="hdWorkExp" runat="server" Value="0" />
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_3">Technical Skills </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_4">Projects </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_5">Certifications </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_6">Languages </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_7">Awards & Recognitions </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_8">Competitions </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_9">Training & Workshop </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_10">Test Scores </a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#tab_11">Build Resume </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-9 col-md-8 col-12 pl-0 pr-0">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab_0">
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Personal Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6 col-md-12 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>First Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblFirstName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Middle Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblMiddleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Last Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblLastName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Date Of Birth :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDOB" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Gender :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblGender" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Mobile No. :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblMobileNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Email ID :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblEmailID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-6 col-md-12 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item text-center">
                                                                            <%--<img src="../IMAGES/nophoto.jpg" style="width: 150px; height: 150px;" />--%>
                                                                            <asp:Image ID="btnImage" runat="server" Height="128px" Width="128px" ImageUrl="~/IMAGES/nophoto.jpg" />
                                                                        </li>
                                                                        <li class="list-group-item"><b>Current Program :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblCurrentProgram" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Semester :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSem" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 mt-4">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Address Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6 col-md-12 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Address :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lbnAddress" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>State :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblState" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>District :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDistrict" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-6 col-md-12 col-12">
                                                                    <ul class="list-group list-group-unbordered">

                                                                        <li class="list-group-item"><b>Taluka :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblTaluka" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>City/Village :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblCity" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Pin Code :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblPinCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 mt-4">
                                                            <div class="row">
                                                                <div class="col-12">

                                                                    <div class="sub-heading">
                                                                        <h5>Education Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12">
                                                                    <div class="table-responsive">

                                                                        <asp:Panel ID="pnlPreviousExam" runat="server" Width="80%">
                                                                            <asp:ListView ID="lvExam" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid">
                                                                                        <div class="titlebar">
                                                                                            Previous Qualification Details
                                                                                        </div>
                                                                                        <br />
                                                                                        <table class="table table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr class="bg-light-blue">
                                                                                                    <th>Education</th>
                                                                                                    <th>School/College Name</th>
                                                                                                    <th>Board/University</th>
                                                                                                    <th>Year Of Passing</th>
                                                                                                    <th>Marks in Percentage</th>
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

                                                                                        <td id="qualifyno" runat="server">
                                                                                            <asp:Label runat="server" ID="lblQualifyname" Text='<%# Eval("QUALIFYNAME") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblSchool"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblBoard" Text='<%# Eval("BOARD") %>'></asp:Label>
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR_OF_EXAM")%>' />
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblPercent" runat="server" Text='<%# Eval("PERCENTAGE")%>' />
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>



                                                                        <%--   <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Education</th>
                                                                        <th>School/College Name</th>
                                                                        <th>Board/University</th>
                                                                        <th>Year Of Passing</th>
                                                                        <th>Marks in Percentage</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>10th</td>
                                                                        <td>School name of 10th</td>
                                                                        <td>Maharashtra State Board</td>
                                                                        <td>July - 2015</td>
                                                                        <td>84%</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>12th</td>
                                                                        <td>School name of 12th</td>
                                                                        <td>Maharashtra State Board</td>
                                                                        <td>July - 2017</td>
                                                                        <td>64%</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Graduation</td>
                                                                        <td>College name of graduation</td>
                                                                        <td>RTM Nagpur University</td>
                                                                        <td>July - 2021</td>
                                                                        <td>62%</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 mt-4">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Current Progress</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12">

                                                                    <div class="table-responsive">

                                                                        <asp:Panel ID="Panel1" runat="server" Width="80%">
                                                                            <asp:ListView ID="LVCurrentProgress" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid">
                                                                                        <div class="titlebar">
                                                                                            Previous Qualification Details
                                                                                        </div>
                                                                                        <br />
                                                                                        <table class="table table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr class="bg-light-blue">
                                                                                                    <th>Semester</th>
                                                                                                    <th>SGPA</th>
                                                                                                    <th>CGPA</th>
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

                                                                                        <td id="qualifyno" runat="server">
                                                                                            <asp:Label runat="server" ID="lblSem" Text='<%# Eval("SEMESTERNO") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblSGPA" Text='<%# Eval("SGPA") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label runat="server" ID="lblCGPA" Text='<%# Eval("CGPA") %>'></asp:Label>
                                                                                        </td>


                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>




                                                                    </div>

                                                                    <%--  <div class="table-responsive">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Semester</th>
                                                                        <th>SGPA</th>
                                                                        <th>CGPA</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>1</td>
                                                                        <td>6.2</td>
                                                                        <td rowspan="6">6.2</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>2</td>
                                                                        <td>6.2</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>3</td>
                                                                        <td>6.2</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>4</td>
                                                                        <td>6.2</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>5</td>
                                                                        <td>6.2</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>6</td>
                                                                        <td>6.2</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <asp:CheckBox ID="chkConfirm" runat="server" Text=" I confirm " OnCheckedChanged="chkConfirm_CheckedChanged" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnProceed" runat="server" CssClass="btn btn-outline-info" OnClick="btnProceed_Click" Enabled="false">Proceed</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton11" runat="server" CssClass="btn btn-outline-danger" Visible="false">Cancel</asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_1">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="box-title">
                                                                    <h5>Exam Details</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12 mt-12">
                                                                        <div class="row">
                                                                            <div class="col-12 mt-3">
                                                                                <div class="sub-heading">
                                                                                    <h5>Arrear Details</h5>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-12">
                                                                                <ul class="list-group list-group-unbordered">
                                                                                    <li class="list-group-item"><b>History of Arrear :</b>
                                                                                        <a class="sub-label">
                                                                                            <asp:Label ID="lblHArrear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </a>
                                                                                    </li>
                                                                                    <li class="list-group-item"><b>Current Arrear :</b>
                                                                                        <a class="sub-label">
                                                                                            <asp:Label ID="lblCArrear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </a>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="col-12 mt-3">
                                                                                <div class="sub-heading">
                                                                                    <h5>Gap in Education Details</h5>
                                                                                </div>
                                                                            </div>
                                                                            <%-- <div class="col-lg-12 col-md-12 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>SGPA :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSGPA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>CGPA :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblCGPA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>--%>
                                                                            <asp:Panel ID="pnlExamGPA" runat="server" Width="80%">
                                                                                <asp:ListView ID="lvExamGPA" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div class="vista-grid">
                                                                                            <div class="titlebar">
                                                                                                <b>Semester wise SGPA CGPA Details</b>
                                                                                            </div>
                                                                                            <%--<br />--%>
                                                                                            <table class="table table-hover table-bordered">
                                                                                                <thead>
                                                                                                    <tr class="bg-light-blue">
                                                                                                        <th>Semester</th>
                                                                                                        <th>SGPA</th>
                                                                                                        <th>CGPA</th>
                                                                                                        <%-- <th>GAP</th>--%>
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
                                                                                            <td id="qualifyno" runat="server">
                                                                                                <asp:Label runat="server" ID="lblSem" Text='<%# Eval("SEMESTERNO") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblSGPA" Text='<%# Eval("SGPA") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="lblCGPA" Text='<%# Eval("CGPA") %>'></asp:Label>
                                                                                            </td>
                                                                                            <%--<td>
                                                                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("GAP") %>'></asp:Label>
                                                                            </td>--%>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </asp:Panel>
                                                                            <div class="form-group col-md-12 col-12 mt-3">
                                                                                <div class="row">
                                                                                    <div class="form-group col-md-4 col-12">
                                                                                        <div class="sub-heading">
                                                                                            <%--<sup>*</sup>--%>
                                                                                            <h7><b>Gap in Education </b></h7>
                                                                                        </div>
                                                                                        <asp:RadioButtonList ID="rbGap" runat="server" OnSelectedIndexChanged="rbGap_SelectedIndexChanged" AutoPostBack="true">
                                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                    <div class="col-md-8 col-12 mt-2" runat="server" id="divGap" visible="false">
                                                                                        <div class="label-dynamic">
                                                                                            <sup>*</sup>
                                                                                            <label>Gap Years </label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-12">
                                                                                            <asp:TextBox ID="txtGapYear" runat="server" ToolTip="Enter No of Gap Years" MaxLength="2" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" />
                                                                                            <asp:RequiredFieldValidator ID="rfvGapYears" runat="server" ControlToValidate="txtGapYear" ValidationGroup="Exam"
                                                                                                ErrorMessage="Please Enter Gap Year " SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                            <asp:HiddenField ID="hdnGapID" runat="server" Visible="false" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer" runat="server" id="divGapBtn" visible="false">
                                                            <asp:LinkButton ID="btnSubmitExamDetails" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitExamDetails_Click" OnClientClick="return validateProficiency();" ValidationGroup="Exam">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelExamDetails" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelExamDetails_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="Exam"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="ListView1" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Technical Skill List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Skill Name</th>
                                                                                <th>Proficiency</th>
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
                                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButton2_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("SKILLS")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButton2_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("SKILLS")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>

                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_2">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Work Experience</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <asp:CheckBox ID="chExperience" runat="server" text="Work Experience"/>
                                                        <asp:CheckBox ID="chInternship" runat="server" text="Internship"/>
                                                    </div>--%>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Work Type </label>
                                                                        </div>
                                                                        <%-- <asp:RadioButton ID="RdExperience" runat="server" Checked="True" GroupName="Type" Text="Work Experience" />
                                                          <asp:RadioButton ID="RdInternship" runat="server" GroupName="Type" Text="Internship"/>--%>

                                                                        <asp:RadioButtonList ID="RdWorkType" runat="server">
                                                                            <asp:ListItem Value="1">Work Experience</asp:ListItem>
                                                                            <asp:ListItem Value="2">Internship</asp:ListItem>

                                                                        </asp:RadioButtonList>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Company Name</label>
                                                                        </div>
                                                                        <%-- <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlCompany" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please select Company Name " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Job Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtJobTitle" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please Enter Job Title " SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Location</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtLocation" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please Enter Location " SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Job Sector</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCompanySector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlCompanySector" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please select Job Sector " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Job Type</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlJobType_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlJobType" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please select Job Type " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Position Type</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlPositionType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlPositionType" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please select Position Type " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-8 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Work Summary</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtDetails" ValidationGroup="WorkExp"
                                                                            ErrorMessage="Please Enter Work Summary" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Start Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStartPeriod" runat="server" CssClass="form-control"  />
                                                    </div>--%>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>Start Date </label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="imgDate1">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtStartPeriod" runat="server" TabIndex="8" ToolTip="Enter Event Start Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="imgDate1" TargetControlID="txtStartPeriod" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtStartPeriod" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtStartPeriod" IsValidEmpty="true"
                                                                                InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="WorkExp" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtStartPeriod" ValidationGroup="WorkExp"
                                                                                ErrorMessage="Please Enter Start Date" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>


                                                                    <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>End Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEndPeriod" runat="server" CssClass="form-control" />
                                                    </div>--%>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>End Date</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" runat="server" id="imgDate2">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtEndPeriod" runat="server" TabIndex="8" ToolTip="Enter Event Start Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="imgDate2" TargetControlID="txtEndPeriod" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndPeriod" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Valid Event End Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtEndPeriod" IsValidEmpty="true"
                                                                                InvalidValueMessage="End Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="WorkExp" SetFocusOnError="true" />

                                                                        </div>
                                                                    </div>


                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:CheckBox ID="chkCurrentlyWork" runat="server" Text="I am currently working here" OnCheckedChanged="chkCurrentlyWork_CheckedChanged" AutoPostBack="true" />
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Salary Type </label>
                                                                        </div>
                                                                        <%--<asp:RadioButton ID="rdoSalary" runat="server" Checked="True" GroupName="Type" Text="Salary" OnCheckedChanged="rdoSalary_CheckedChanged" AutoPostBack="true"/>
                                                                                <asp:RadioButton ID="rdoStipend" runat="server" GroupName="Type" Text="Stipend" OnCheckedChanged="rdoStipend_CheckedChanged" AutoPostBack="true"/>--%>


                                                                        <asp:RadioButtonList ID="RdSalaeyType" runat="server" OnSelectedIndexChanged="RdSalaeyType_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="1">Salary</asp:ListItem>
                                                                            <asp:ListItem Value="2">Stipend</asp:ListItem>

                                                                        </asp:RadioButtonList>
                                                                    </div>

                                                                    <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Upload Document<code></code></label>
                                                                            </div>
                                                                            <label class="filelabel ">
                                                                                <%-- <i class="fas fa-cloud-upload-alt"></i>--%>
                                                                                <sup>Note Valid files : (.pdf should be of 500 Kb size.)</sup>
                                                                                <%-- <span class="title">Note<span class="choosefile">choose file</span>.</span>--%>
                                                                                <asp:FileUpload ID="FileUploadWorkExp" runat="server" EnableViewState="true" TabIndex="5"
                                                                                    ToolTip="Click here to Attach File" />

                                                                                <%--  <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                                                                            </label>
                                                                        </div>
                                                                    </div>

                                                                    <%--  <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplWorkExp" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>Upload Relevant Document</label>
                                                                </div>
                                                                <asp:TextBox ID="RelevantDocWorkExperience" runat="server" type="file" />
                                                            </div>--%>

                                                                    <div class="col-12 mt-3">
                                                                        <div class="row">
                                                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divSalary" runat="server" visible="false">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Salary</label>
                                                                                </div>
                                                                                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" type="text" MaxLength="7" />
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtSalary" runat="server" ValidChars="0123456789."
                                                                                    FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtSalary">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divStipend" runat="server" visible="false">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Stipend</label>
                                                                                </div>
                                                                                <asp:TextBox ID="TxtStipend" runat="server" CssClass="form-control" type="text" MaxLength="7" />
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                                                                    FilterType="Custom" FilterMode="ValidChars" TargetControlID="TxtStipend">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divCurrency" runat="server" visible="false">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Currency</label>
                                                                                </div>
                                                                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                            <%--<div class="form-group col-lg-4 col-md-6 col-12" id="divPerAnnum" runat="server" visible="false">
                                                             <div class="label-dynamic">--%>
                                                                            <%-- <sup>* </sup>
                                                            <label>Per Annum</label>--%>
                                                                            <%--  </div>
                                                       <div>
                                                          <asp:Label ID="lblPerAnnum" runat="server" Text="Per Annum"> </asp:Label>
                                                       </div>
                                                    </div>--%>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Job Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                        <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Job Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCompanySector" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                        <%--    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Start Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStartPeriod" runat="server" CssClass="form-control" type="date" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>End Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEndPeriod" runat="server" CssClass="form-control" type="date" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:CheckBox ID="chkCurrentlyWork" runat="server" Text="I am currently working here" OnCheckedChanged="chkCurrentlyWork_CheckedChanged" AutoPostBack="true"/>
                                                    </div>

                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <div class="label-dynamic">
                                                                                    <sup></sup>
                                                                                    <label>Type </label>
                                                                                </div>
                                                                                <asp:RadioButton ID="rdoSalary" runat="server" Checked="True" GroupName="Type" Text="Salary" />
                                                                                <asp:RadioButton ID="rdoStipend" runat="server" GroupName="Type" Text="Stipend" />
                                                                            </div>--%>

                                                        <%--  <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Monthly Salary/Stipend</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMonthlySalary" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                        <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Details</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Upload Relevant Document</label>
                                                        </div>
                                                        <asp:TextBox ID="RelevantDocWorkExperience" runat="server" type="file" />
                                                    </div>
                                                </div>
                                            </div>--%>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitWorkExperience" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitWorkExperience_Click" ValidationGroup="WorkExp">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelWorkExperience" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelWorkExperience_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="WorkExp"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvWORKEXPERIENCE" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Work Experience List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Company Name</th>
                                                                                <th>Job Title</th>
                                                                                <th>Location</th>
                                                                                <th>Position Type</th>
                                                                                <th>Upload Document</th>
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
                                                                            <asp:ImageButton ID="LinkButton1" runat="server" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("WORKEXPNO") %>'></asp:ImageButton></td>
                                                                        <td>
                                                                            <%# Eval("COMPNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JOBTITLE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CMPLocation")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JOBROLETYPE")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnWorkExpPreview" runat="server" OnClick="imgbtnWorkExpPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("RelevantDocument") %>'
                                                                                CommandArgument='<%# Eval("RelevantDocument") %>' Visible='<%# Convert.ToString(Eval("RelevantDocument"))==string.Empty?false:true %>'></asp:ImageButton></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="LinkButton1" runat="server" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("WORKEXPNO") %>'></asp:ImageButton></td>
                                                                        <td>
                                                                            <%# Eval("COMPNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JOBTITLE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CMPLocation")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("JOBROLETYPE")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnWorkExpPreview" runat="server" OnClick="imgbtnWorkExpPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("RelevantDocument") %>'
                                                                                CommandArgument='<%# Eval("RelevantDocument") %>' Visible='<%# Convert.ToString(Eval("RelevantDocument"))==string.Empty?false:true %>'></asp:ImageButton></td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                        <%--<div class="col-12 mt-3">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Company Name</th>
                                                            <th>Job Title</th>
                                                            <th>Location</th>
                                                            <th>Position Type</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="LinkButton1" runat="server" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="LinkButton1_Click"></asp:ImageButton></td>
                                                            <td>Company Name</td>
                                                            <td>Job Title</td>
                                                            <td>Nagpur</td>
                                                            <td>Position Type</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>--%>
                                                    </div>

                                                    <div class="tab-pane fade" id="tab_3">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Technical Skills</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Skill Name</label>
                                                                        </div>
                                                                        <%--<asp:TextBox ID="txtSkillName" runat="server" CssClass="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlSkillName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlSkillName" ValidationGroup="TechSkil"
                                                                            ErrorMessage="Please Select Skill Name " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Proficiency</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlProficiency" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProficiency"
                                                                            Display="None" ErrorMessage="Please Select Proficiency." ValidationGroup="TechSkil"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplTech" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Upload Relevant Document</label>
                                                                        </div>
                                                                        <asp:TextBox ID="RelevantDocTechSkill" runat="server" type="file" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitTechSkill" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitTechSkill_Click" OnClientClick="return validateProficiency();" ValidationGroup="TechSkil">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelTechSkill" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelTechSkill_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="TechSkil"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lvtechSkill" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Technical Skill List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Skill Name</th>
                                                                                <th>Proficiency</th>
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
                                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButton2_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("SKILLS")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("ID") %>' OnClick="LinkButton2_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("SKILLS")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>

                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_4">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Projects</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Project Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProjectTitle"
                                                                            Display="None" ErrorMessage="Please Enter Project Title." ValidationGroup="show1"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Project Domain</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtProjectDomian" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProjectDomian"
                                                                            Display="None" ErrorMessage="Please Enter Project Domain." ValidationGroup="show1"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Guide/Supervisor Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSupervisorName" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSupervisorName"
                                                                            Display="None" ErrorMessage="Please Enter Guide/Supervisor Name." ValidationGroup="show1"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Start Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" type="date" />
                                                    </div>--%>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>Start Date </label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="StDiv">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtStartDate" runat="server" TabIndex="8" ToolTip="Enter Event Start Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="StDiv" TargetControlID="txtStartDate" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtStartDate" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtStartDate" IsValidEmpty="true"
                                                                                InvalidValueMessage="Start Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show1" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStartDate"
                                                                                Display="None" ErrorMessage="Please Enter Start Date." ValidationGroup="show1"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                    </div>
                                                                    <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>End Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" type="date" />
                                                    </div>--%>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="enddate">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>End Date </label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" runat="server" id="Div2">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtEndDate" runat="server" TabIndex="8" ToolTip="Enter Event End Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div2" TargetControlID="txtEndDate" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndDate" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter Valid Event End Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                                                InvalidValueMessage="End Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show1" SetFocusOnError="true" />
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEndDate"
                                                                    Display="None" ErrorMessage="Please Enter End Date." ValidationGroup="show1"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12 pr-0">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:CheckBox ID="chcurrwntlywork" runat="server" Text="I am currently working on this project" OnCheckedChanged="chcurrwntlywork_CheckedChanged" AutoPostBack="true" />
                                                                    </div>

                                                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup> </sup>
                                                                    <label>HR</label>
                                                                </div>
                                                                <asp:TextBox ID="txtHr" runat="server" CssClass="form-control" MaxLength="200" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDescription"
                                                                    Display="None" ErrorMessage="Please Enter Description." ValidationGroup="show1"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                             <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Company Location</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCompLoc" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtDescription"
                                                                    Display="None" ErrorMessage="Please Enter Description." ValidationGroup="show1"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Description</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDescription"
                                                                            Display="None" ErrorMessage="Please Enter Description." ValidationGroup="show1"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <%-- <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplProj" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Upload Relevant Document</label>
                                                        </div>
                                                        <asp:TextBox ID="RelevantDocProject" runat="server" type="file" />
                                                    </div>--%>
                                                                    <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Upload Document<code></code></label>
                                                                            </div>
                                                                            <label class="filelabel ">
                                                                                <%-- <i class="fas fa-cloud-upload-alt"></i>--%>
                                                                                <sup>Note Valid files : (.pdf should be of 500 Kb size.)</sup>
                                                                                <%-- <span class="title">Note<span class="choosefile">choose file</span>.</span>--%>
                                                                                <asp:FileUpload ID="FileUploadProject" runat="server" EnableViewState="true" TabIndex="5"
                                                                                    ToolTip="Click here to Attach File" />

                                                                                <%--  <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitProject" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitProject_Click" ValidationGroup="show1">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelProject" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelProject_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show1"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <%--  <div class="col-12 mt-3">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Project Title</th>
                                                            <th>Project Domian</th>
                                                            <th>Guide/Supervisor Name</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Project Title</td>
                                                            <td>Project Domian</td>
                                                            <td>Ajanta Mendis</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>--%>
                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lvProject" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Project List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Project Title</th>
                                                                                <th>Project Domain</th>
                                                                                <th>Guide/Supervisor Name</th>
                                                                                <th>Upload document</th>
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
                                                                            <asp:ImageButton ID="btnEditProject" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("P_ID") %>' OnClick="btnEditProject_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("PROJECT_TITLE")%></td>
                                                                        <td><%# Eval("PROJECT_DOMIAN")%></td>
                                                                        <td><%# Eval("GUIDE_SUPERVISOR_NAME")%></td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnProjectPreview" runat="server" OnClick="imgbtnProjectPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>'
                                                                                CommandArgument='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>' Visible='<%# Convert.ToString(Eval("UPLOAD_RELEVANT_DOCUMENT"))==string.Empty?false:true %>'></asp:ImageButton></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditProject" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("P_ID") %>' OnClick="btnEditProject_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("PROJECT_TITLE")%></td>
                                                                        <td><%# Eval("PROJECT_DOMIAN")%></td>
                                                                        <td><%# Eval("GUIDE_SUPERVISOR_NAME")%></td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnProjectPreview" runat="server" OnClick="imgbtnProjectPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>'
                                                                                CommandArgument='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>' Visible='<%# Convert.ToString(Eval("UPLOAD_RELEVANT_DOCUMENT"))==string.Empty?false:true %>'></asp:ImageButton></td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_5">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Certifications</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtTitle"
                                                                            Display="None" ErrorMessage="Please Enter Title." ValidationGroup="crtval"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Certified By</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtCertifiedBy" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtCertifiedBy"
                                                                            Display="None" ErrorMessage="Please Enter Certified By." ValidationGroup="crtval"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <%--  <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>From Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" type="date" />
                                                    </div>--%>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>From Date </label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div3">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" ToolTip="Enter Event From Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div3" TargetControlID="txtFromDate" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFromDate" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDate" IsValidEmpty="true"
                                                                                InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="crtval" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromDate"
                                                                                Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="crtval"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                    </div>

                                                                    <%--     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>To Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" type="date" />
                                                    </div>--%>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>To Date </label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" runat="server" id="Div4">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="8" ToolTip="Enter Event To Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div4" TargetControlID="txtToDate" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtToDate" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" EmptyValueMessage="Please Enter Valid Event To Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtToDate" IsValidEmpty="true"
                                                                                InvalidValueMessage="To Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="crtval" SetFocusOnError="true" />
                                                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="crtval"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12 pr-0">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:CheckBox ID="chkCertification" runat="server" Text="I am currently doing this course" OnCheckedChanged="chkCertification_CheckedChanged" AutoPostBack="true" />
                                                                    </div>

                                                                    <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Upload Document<code></code></label>
                                                                            </div>
                                                                            <label class="filelabel ">
                                                                                <%-- <i class="fas fa-cloud-upload-alt"></i>--%>
                                                                                <sup>Note Valid files : (.pdf should be of 500 Kb size.)</sup>
                                                                                <%-- <span class="title">Note<span class="choosefile">choose file</span>.</span>--%>
                                                                                <asp:FileUpload ID="FileUploadCertification" runat="server" EnableViewState="true" TabIndex="5"
                                                                                    ToolTip="Click here to Attach File" />

                                                                                <%--  <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />--%>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitCertification" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitCertification_Click" ValidationGroup="crtval">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelCertification" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelCertification_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="crtval"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lvcertfication" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Certification List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Title</th>
                                                                                <th>Certified By</th>
                                                                                <th>Grade</th>
                                                                                <th>Certification Document</th>
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
                                                                            <asp:ImageButton ID="btnEditCertificat" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("C_ID") %>' OnClick="btnEditCertificat_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("TITLE")%></td>
                                                                        <td><%# Eval("CERTIFIED_BY")%></td>
                                                                        <td><%# Eval("GRADE")%></td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnCertificationPreview" runat="server" OnClick="imgbtnCertificationPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>'
                                                                                CommandArgument='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>' Visible='<%# Convert.ToString(Eval("UPLOAD_RELEVANT_DOCUMENT"))==string.Empty?false:true %>'></asp:ImageButton></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditCertificat" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("C_ID") %>' OnClick="btnEditCertificat_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("TITLE")%></td>
                                                                        <td><%# Eval("CERTIFIED_BY")%></td>
                                                                        <td><%# Eval("GRADE")%></td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnCertificationPreview" runat="server" OnClick="imgbtnCertificationPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>'
                                                                                CommandArgument='<%# Eval("UPLOAD_RELEVANT_DOCUMENT") %>' Visible='<%# Convert.ToString(Eval("UPLOAD_RELEVANT_DOCUMENT"))==string.Empty?false:true %>'></asp:ImageButton></td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                            <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Title</th>
                                                            <th>Certified By</th>
                                                            <th>Grade</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Certificate Title</td>
                                                            <td>Certified By</td>
                                                            <td>A</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_6">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Languages</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <!--added by amit pandey -->
                                                                            <label>Spoken Language</label>
                                                                            <!-- end-->
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlLauguage" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlLauguage"
                                                                            Display="None" ErrorMessage="Please Select Language." ValidationGroup="crtlav"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Proficiency</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlProficiencyLanguage" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlProficiencyLanguage"
                                                                            Display="None" ErrorMessage="Please Select Proficiency." ValidationGroup="crtlav"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="Upllang" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Upload Relevant Document</label>
                                                                        </div>
                                                                        <asp:TextBox ID="RelevantDocLanguage" runat="server" type="file" />
                                                                    </div>
                                                                    <!-- added by amit pandey -->
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <asp:CheckBox ID="chkReadLang" runat="server" Text="Read" /> &nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkWriteLang" runat="server" Text="Write" /> &nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkSpeakLang" runat="server" Text="Speak" /> &nbsp;&nbsp;
                                                                    </div>
                                                                    <!-- end-->
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitLanguage" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitLanguage_Click" ValidationGroup="crtlav">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelLanguage" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelLanguage_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="crtlav"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lvLanguage" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Language List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Language</th>
                                                                                <th>Proficiency</th>
                                                                                <th>Read</th>
                                                                                <th>Write></th>
                                                                                <th>Speak</th>
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
                                                                            <asp:ImageButton ID="btnEditLanguage" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("L_ID") %>' OnClick="btnEditLanguage_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("LANGUAGES")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>
                                                                        <td><%# Eval("Read")%></td>
                                                                        <td><%# Eval("Write")%></td>
                                                                        <td><%# Eval("Speak")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditLanguage" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("L_ID") %>' OnClick="btnEditLanguage_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("LANGUAGES")%></td>
                                                                        <td><%# Eval("PROFICIENCY")%></td>
                                                                        <td><%# Eval("Read")%></td>
                                                                        <td><%# Eval("Write")%></td>
                                                                        <td><%# Eval("Speak")%></td>

                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                            <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Lauguage</th>
                                                            <th>Proficiency</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>English</td>
                                                            <td>Proficiency</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_7">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Awards & Recognitions</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Award Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAwardTitle" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <%-- <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date of Award</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAwardDate" runat="server" CssClass="form-control" type="date" />--%>
                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>Date of Award</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div5">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtAwardDate" runat="server" TabIndex="8" ToolTip="Please Enter Date of Award"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div5" TargetControlID="txtAwardDate" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtAwardDate" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" EmptyValueMessage="Please Enter Valid Date of Award"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtAwardDate" IsValidEmpty="true"
                                                                                InvalidValueMessage="Date of Award is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select Date of Award" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show1" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAwardDate"
                                                                                Display="None" ErrorMessage="Please Enter Date of Award." ValidationGroup="show2"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Given By</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGivenBy" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Level</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplAward" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Upload Relevant Document</label>
                                                                        </div>
                                                                        <asp:TextBox ID="RelevantDocAward" runat="server" type="file" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitAward" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitAward_Click" OnClientClick=" validateinvalidAwarddate();">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelAward" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelAward_Click">Cancel</asp:LinkButton>
                                                        </div>

                                                        <div class="col-12 mt-3">


                                                            <asp:ListView ID="lvAWARD" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Award And Recognitions List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Award Title</th>
                                                                                <th>Date of Award</th>
                                                                                <th>Given By</th>
                                                                                <th>Level</th>
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
                                                                            <asp:ImageButton ID="btnEditAward" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("AR_ID") %>' OnClick="btnEditAward_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("AWARD_TITLE")%></td>
                                                                        <td><%# Eval("DATE_OF_AWARD", "{0: dd-MM-yyyy}")%></td>
                                                                        <td><%# Eval("GIVEN_BY")%></td>
                                                                        <td><%# Eval("LEVELS")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditAward" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("AR_ID") %>' OnClick="btnEditAward_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("AWARD_TITLE")%></td>
                                                                        <td><%# Eval("DATE_OF_AWARD")%></td>
                                                                        <td><%# Eval("GIVEN_BY")%></td>
                                                                        <td><%# Eval("LEVELS")%></td>

                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                            <%--   <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Award Title</th>
                                                            <th>Date of Award</th>
                                                            <th>Given By</th>
                                                            <th>Level</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton6" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Award Name</td>
                                                            <td>26-01-2022</td>
                                                            <td>Ajanta Mandis</td>
                                                            <td>Level 2</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_8">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Competitions</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Competition Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtCompetitionTitle" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtCompetitionTitle"
                                                                            Display="None" ErrorMessage="Please Enter Competition Title." ValidationGroup="show4"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Organized By</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtOrganizedBy" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtOrganizedBy"
                                                                            Display="None" ErrorMessage="Please Enter Organized By." ValidationGroup="show4"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Level</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlLevel1" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlLevel1"
                                                                            Display="None" ErrorMessage="Please Select Level." ValidationGroup="show4" InitialValue="0"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <%--    <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>From Date</label>
                                                        </div>--%>
                                                                        <%--   <asp:TextBox ID="txtFromDateCompetition" runat="server" CssClass="form-control" type="date" />--%>

                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>From Date</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div6">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtFromDateCompetition" runat="server" TabIndex="8" ToolTip="Please Enter From Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div6" TargetControlID="txtFromDateCompetition" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFromDateCompetition" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" EmptyValueMessage="Please Enter Valid From Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDateCompetition" IsValidEmpty="true"
                                                                                InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select From Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show4" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFromDateCompetition"
                                                                                Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="show4"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <%--<div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>To Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtToDateCompetition" runat="server" CssClass="form-control" type="date" />--%>

                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>To Date</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div7">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtToDateCompetition" runat="server" TabIndex="8" ToolTip="Please Enter To Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div7" TargetControlID="txtToDateCompetition" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtToDateCompetition" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" EmptyValueMessage="Please Enter Valid To Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtToDateCompetition" IsValidEmpty="true"
                                                                                InvalidValueMessage="To Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select To Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show4" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtToDateCompetition"
                                                                                Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="show4"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Project Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtProjectTitleCompetition" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtProjectTitleCompetition"
                                                                            Display="None" ErrorMessage="Please Entry Project Title." ValidationGroup="show4"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Participation Status</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlParticipationStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlParticipationStatus"
                                                                            Display="None" ErrorMessage="Please Select Participation Status." ValidationGroup="show4" InitialValue="0"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplComp" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Upload Relevant Document</label>
                                                                        </div>
                                                                        <asp:TextBox ID="RelevantDocCompetition" runat="server" type="file" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitCompetition" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitCompetition_Click" ValidationGroup="show4">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelCompetition" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelCompetition_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="show4" ShowMessageBox="true"
                                                                ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lVCompetition" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Competition List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Competition Title</th>
                                                                                <th>Organized By</th>
                                                                                <th>Level</th>
                                                                                <th>Project Title</th>
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
                                                                            <asp:ImageButton ID="btnEditCompetition" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("CP_ID") %>' OnClick="btnEditCompetition_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("COMPETITION_TITLE")%></td>
                                                                        <td><%# Eval("ORGANIZED_BY")%></td>
                                                                        <td><%# Eval("LEVELS")%></td>
                                                                        <td><%# Eval("Project_Title")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditCompetition" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("CP_ID") %>' OnClick="btnEditCompetition_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("COMPETITION_TITLE")%></td>
                                                                        <td><%# Eval("ORGANIZED_BY")%></td>
                                                                        <td><%# Eval("LEVELS")%></td>
                                                                        <td><%# Eval("Project_Title")%></td>

                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>


                                                            <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Competition Title</th>
                                                            <th>Organized By</th>
                                                            <th>Level</th>
                                                            <th>Project Title</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton7" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Competition Title</td>
                                                            <td>Organized</td>
                                                            <td>Level 2</td>
                                                            <td>Project Name</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_9">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Training & Workshop</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Training Title</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtTrainingTitle" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtTrainingTitle"
                                                                            Display="None" ErrorMessage="Please Enter Training Title." ValidationGroup="show3"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Organized By</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtOrganized" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtOrganized"
                                                                            Display="None" ErrorMessage="Please Enter Organized By." ValidationGroup="show3"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Category</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlCategory"
                                                                            Display="None" ErrorMessage="Please Enter Category." ValidationGroup="show3" InitialValue="0"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>From Date</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div8">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtFromDateTraining" runat="server" TabIndex="8" ToolTip="Please Enter From Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div8" TargetControlID="txtFromDateTraining" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFromDateTraining" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" EmptyValueMessage="Please Enter Valid From Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDateTraining" IsValidEmpty="true"
                                                                                InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select From Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show3" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFromDateTraining"
                                                                                Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="show3"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>

                                                                        <%--<div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>From Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDateTraining" runat="server" CssClass="form-control" type="date" />--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>To Date</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div9">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtToDateTraining" runat="server" TabIndex="8" ToolTip="Please Enter To Date"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div9" TargetControlID="txtToDateTraining" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtToDateTraining" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" EmptyValueMessage="Please Enter Valid To Date"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtToDateTraining" IsValidEmpty="true"
                                                                                InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select To Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show3" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtToDateTraining"
                                                                                Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="show3"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>

                                                                        <%--<div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>To Date</label>
                                                        </div>
                                                        <asp:TextBox ID="txtToDateTraining" runat="server" CssClass="form-control" type="date" />--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="UplWorkShop" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Upload Relevant Document</label>
                                                                        </div>
                                                                        <asp:TextBox ID="RelevantDocTraining" runat="server" type="file" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitTraining" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitTraining_Click" ValidationGroup="show3">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelTraining" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelTraining_Click">Cancel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummaryDiscipline" runat="server" ValidationGroup="show3" ShowMessageBox="true"
                                                                ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                        <div class="col-12 mt-3">

                                                            <asp:ListView ID="lvTrainingAndPlacement" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Training And Workshop List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Training Title</th>
                                                                                <th>Organized By</th>
                                                                                <th>Category</th>
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
                                                                            <asp:ImageButton ID="btnEditTraining" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("TW_ID") %>' OnClick="btnEditTraining_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("TRAINING_TITLE")%></td>
                                                                        <td><%# Eval("ORGANIZED_BY")%></td>
                                                                        <td><%# Eval("CATEGORYS")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>

                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditTraining" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("TW_ID") %>' OnClick="btnEditTraining_Click"></asp:ImageButton></td>
                                                                        <td><%# Eval("TRAINING_TITLE")%></td>
                                                                        <td><%# Eval("ORGANIZED_BY")%></td>
                                                                        <td><%# Eval("CATEGORYS")%></td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>

                                                            <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Training Title</th>
                                                            <th>Organized By</th>
                                                            <th>Category</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton8" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Training Title</td>
                                                            <td>Organized</td>
                                                            <td>Category</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                        </div>

                                                    </div>

                                                    <div class="tab-pane fade" id="tab_10">
                                                        <div class="col-12 mt-3">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Test Scores</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Exam</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Qualification Status</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlQualificationStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>Year</label>
                                                                        </div>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon" id="Div10">
                                                                                <i class="fa fa-calendar text-blue"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtyear" runat="server" TabIndex="8" ToolTip="Please Enter year"
                                                                                CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender11" runat="server" Enabled="true" EnableViewState="true"
                                                                                Format="dd/MM/yyyy" PopupButtonID="Div10" TargetControlID="txtyear" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtyear" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" EmptyValueMessage="Please Enter Valid Year"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtyear" IsValidEmpty="true"
                                                                                InvalidValueMessage="Year is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                                                ErrorMessage="Please Select To Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                                ValidationGroup="show1" SetFocusOnError="true" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtyear"
                                                                                Display="None" ErrorMessage="Please Enter Year." ValidationGroup="show2"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                        <%--    <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Year</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Test Score/Grade</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtTestScore" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <%--<div class="label-dynamic">
                                                            <label>Upload Relevant Document</label>
                                                        </div>
                                                        <asp:TextBox ID="RelevantDocScore" runat="server" type="file" />
                                                    </div>--%>
                                                                        <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="Upltestscore" visible="false">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Upload Relevant Document <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                                                            </div>
                                                                            <asp:FileUpload ID="RelevantDocScore" runat="server" EnableViewState="true" TabIndex="5"
                                                                                ToolTip="Click here to Attach File" />

                                                                            <asp:Label ID="lblPreAttach" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitScore" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitScore_Click" OnClientClick=" validateinvaliddate();">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelScore" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelScore_Click">Cancel</asp:LinkButton>
                                                            </div>

                                                            <div class="col-12 mt-3">
                                                                <asp:ListView ID="lvtestscored" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Test Scores List</h5>
                                                                        </div>

                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Exam</th>
                                                                                    <th>Qualification Status</th>
                                                                                    <th>Year</th>
                                                                                    <th>Test Score</th>
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
                                                                                <asp:ImageButton ID="btnEditTestScore" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("TS_ID") %>' OnClick="btnEditTestScore_Click"></asp:ImageButton></td>
                                                                            <td><%# Eval("EXAMS")%></td>
                                                                            <td><%# Eval("QUALIFICATION")%></td>
                                                                            <td><%# Eval("Year", "{0: dd-MM-yyyy}")%></td>
                                                                            <td><%# Eval("TestScore")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr>

                                                                            <td>
                                                                                <asp:ImageButton ID="btnEditTestScore" runat="server" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o" ToolTip="Edit Record" CommandArgument='<%# Eval("TS_ID") %>' OnClick="btnEditTestScore_Click"></asp:ImageButton></td>
                                                                            <td><%# Eval("EXAMS")%></td>
                                                                            <td><%# Eval("QUALIFICATION")%></td>
                                                                            <td><%# Eval("Year")%></td>
                                                                            <td><%# Eval("TestScore")%></td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                </asp:ListView>

                                                                <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Exam</th>
                                                            <th>Qualification Status</th>
                                                            <th>Year</th>
                                                            <th>Test Score</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton9" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton></td>
                                                            <td>Exam Name</td>
                                                            <td>Qualification Status</td>
                                                            <td>2022</td>
                                                            <td>80</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                            </div>

                                                        </div>
                                                    </div>


                                                    <div class="tab-pane fade" id="tab_11">
                                                        <div class="col-12">
                                                            <div class="12">
                                                                <div class="sub-heading">
                                                                    <h5>Build Resume</h5>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-lg-6 col-md-6 col-12">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Download Resume <code></code></label>
                                                                            </div>
                                                                            <%-- <a href="#" class='download-excel'>--%>
                                                                            <asp:LinkButton ID="btnlncardkSelect" runat="server"
                                                                                ToolTip="Click here Generate Resume" OnClick="btnlncardkSelect_Click"
                                                                                TabIndex="2">
                                                                <label class="filelabel-download">
                                                                    <i class="fas fa-cloud-download-alt"></i>
                                                                    <span class='name'>Generate Resume</span>
                                                                </label>
                                                            <%--</a>--%>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <label>Upload Resume<code></code></label>
                                                                            </div>
                                                                            <label class="filelabel ">
                                                                                <i class="fas fa-cloud-upload-alt"></i>

                                                                                <span class="title">Click here to <span class="choosefile">choose file</span>.</span>
                                                                                <asp:FileUpload ID="UploadResume" runat="server" EnableViewState="true" TabIndex="5"
                                                                                    ToolTip="Click here to Attach File" />

                                                                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitResume" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitResume_Click">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelResume" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelResume_Click1">Cancel</asp:LinkButton>
                                                        </div>

                                                        <%--   <div class="col-12 mt-3">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>File Name</th>
                                                            <th>Date</th>
                                                            <th>Mode</th>
                                                            <th>Download</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>Name of file</td>
                                                            <td>26-01-2022</td>
                                                            <td>Generated</td>
                                                            <td><i class="fa fa-download" aria-hidden="true" style="color: #28a745;"></i></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Name of file</td>
                                                            <td>26-01-2022</td>
                                                            <td>Uploaded</td>
                                                            <td><i class="fa fa-download" aria-hidden="true" style="color: #28a745;"></i></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>--%>

                                                        <div class="col-12 mt-3">
                                                            <asp:ListView ID="lvResumeUpload" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Upload Resume List</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>File Name</th>
                                                                                <th>Date</th>
                                                                                <th>Mode</th>
                                                                                <th>Download</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>

                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>

                                                                        <td><%# Eval("FILENAME")%></td>
                                                                        <td><%# Eval("UploadDate", "{0: dd-MM-yyyy}")%></td>
                                                                        <%--td> </td>--%>
                                                                        <td>Uploaded</td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                                CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                         </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <%--   <AlternatingItemTemplate>
                                                   <tr>
                                                      
                                                     <td> <%# Eval("FILENAME")%></td>
                                                     <td> <%# Eval("")%></td>
                  
                                                        <td>Uploaded</td>
                                                     <td>   <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                 CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                    </td>
                                                    </tr>
                                                </AlternatingItemTemplate>--%>
                                                            </asp:ListView>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
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


                    <div id="divMsg" runat="server">
                    </div>
                    <script>
                        function TabShow(tabid) {
                            var tabName = tabid;
                            $('#Tabs a[href="#' + tabName + '"]').tab('show');
                            $("#Tabs a").click(function () {
                                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                            });
                        }
                    </script>
                    <script>
                        $("#UploadedFile").on('change', function (e) {
                            var labelVal = $(".title").text();
                            var oldfileName = $(this).val();
                            fileName = e.target.value.split('\\').pop();

                            if (oldfileName == fileName) { return false; }
                            var extension = fileName.split('.').pop();


                            if (extension == 'csv' || extension == 'xml' || extension == 'xlsx' || extension == 'xls') {
                                $(".filelabel i").removeClass().addClass('fas fa-file-excel');
                                $(".filelabel i, .filelabel .title").css({ 'color': '#208440' });
                                $(".filelabel").css({ 'border': ' 2px solid #208440' });
                                $(".filelabel .warning").css({ 'display': 'none' });
                            }

                            else {
                                $(".filelabel i").removeClass().addClass('fa fa-exclamation-triangle');
                                $(".filelabel i").css({ 'color': 'red' })
                                $(".filelabel .title").css({ 'color': '#1665c4' });
                                $(".filelabel").css({ 'border': ' 2px solid red' });
                                $(".filelabel .warning").css({ 'display': 'inline-block' });
                            }

                            if (fileName) {
                                if (fileName.length > 30) {
                                    $(".filelabel .title").text(fileName.slice(0, 10) + '...' + extension);
                                }
                                else {
                                    $(".filelabel .title").text(fileName);
                                }
                            }
                            else {
                                $(".filelabel .title").text(labelVal);
                            }
                        });
                    </script>
                    <script>
                        function validateProficiency() {


                            var txtProficiency = $("[id$=ddlSkillName]").attr("id");
                            var txtProficiency = document.getElementById(txtProficiency);

                            if (txtProficiency.value.length == 0) {
                                alert('Please Enter Proficiency ', 'Warning!');
                                //   $(txtCurrency).css('border-color', 'red');
                                $(txtProficiency).focus();
                                return false;
                            }
                        }

                        function validateinvaliddate() {

                            if (document.getElementById('<%= txtyear.ClientID %>').value != '') {
                        var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                        if (!(date_regex.test(document.getElementById('<%= txtyear.ClientID %>').value))) {
                            alert("Year Is Invalid (Enter In [dd/MM/yyyy] Format).");
                            return false;
                        }
                    }
                }

                function validateinvalidAwarddate() {

                    if (document.getElementById('<%= txtAwardDate.ClientID %>').value != '') {
                var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                if (!(date_regex.test(document.getElementById('<%= txtAwardDate.ClientID %>').value))) {
                    alert("Award Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                    return false;
                }
            }
        }
                    </script>
                    <script type="text/javascript">
                        function CheckNumeric(event, obj) {
                            var k = (window.event) ? event.keyCode : event.which;
                            //alert(k);
                            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                                obj.style.backgroundColor = "White";
                                return true;
                            }
                            if (k > 45 && k < 58) {
                                obj.style.backgroundColor = "White";
                                return true;

                            }
                            else {
                                alert('Please Enter numeric Value');
                                obj.focus();
                            }
                            return false;
                        }
                    </script>
                    </div>
                </div>
        </form>
    </body>
   

</asp:Content>

