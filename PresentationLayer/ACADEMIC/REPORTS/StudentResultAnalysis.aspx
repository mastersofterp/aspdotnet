<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentResultAnalysis.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentResultList"
    Title="" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <link href="/Css/datepicker.css" rel="stylesheet" />
    <script src="/jquery/bootstrap-datepicker.js"></script>
    <script src="/jquery/jquery.inputmask.bundle.js"></script>--%>

    <style>
        input.masked {
            font-size: 16px;
            font-family: monospace;
            padding-right: 10px;
            background-color: transparent;
            text-transform: uppercase;
        }
    </style>

    <%--    <script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/latest/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
    <style>
        .box-header {
            position: relative;
            font-size: 14px;
        }

        .not-icon {
            cursor: pointer;
            transition: all .5s ease-in;
            float: right;
            padding: 5px 17px 5px 17px;
            background-color: #007bff;
            color: #fff;
            border-radius: 100%;
            /*border: 1px solid #ccc;*/
        }

        .head {
            color: #3c8dbc;
            margin: 0;
            padding: 5px 0px 8px 0px;
        }

        #selectrpt {
            display: none;
        }

        .fix-menu ul.list-group .list-group-item {
            padding: 7px 10px!important;
        }

        .box-header #selectrpt {
            width: auto;
            margin: auto;
            padding: 0px 10px 10px 10px;
            border: 2px solid #ccc;
            position: relative;
            margin-top: 0;
            box-shadow: 0 15px 25px rgba(0,0,0,.4);
        }

        .box-header.drop-down {
            display: none;
            background-color: #FFF;
            position: absolute;
            border: 1px solid #CCC;
            border-radius: 5px;
            top: 45px;
            right: 0;
            z-index: 1000;
            width: auto;
            text-align: justify;
        }

        .box-header #selectrpt .card {
            padding: 5px 10px 5px 10px;
            background-color: #fff;
            border: 1px solid #ccc;
            font-size: 14px;
            line-height: 1.7;
            position: relative;
        }

        .box-header .main {
            display: block;
            overflow: auto;
        }

        .nav i {
            margin-right: 10px;
        }

        .nav {
            /*color: #3c8dbc;
            font-weight: bold;*/
            cursor: pointer;
        }

        .nav2 {
            margin: 0;
            font-size: 14px;
        }

            .nav2 i {
                margin-right: 5px;
                color: #3c8dbc;
            }

        .box-title {
            margin-top: 0;
        }

        .borderred, .borderredthree, .borderredtwo, .threes {
            border: 1px solid #ff8080;
        }

        .red-color {
            border: 1px solid #ff8080 !important;
        }

        .form-control {
            border-top: none;
            border-left: none;
            border-right: none;
        }

        /*----------------*/
        .Selection-box {
            position: absolute;
            right: 10px;
            background: #007bff;
            height: 33px;
            border-radius: 17px;
            padding: 7px;
            display: inline-flex;
        }

        .Selection-btn {
            color: #fff;
            float: right;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            background: #3c8dbc;
            display: flex;
            justify-content: center;
            align-items: center;
            text-decoration: none;
        }

        .Selection-txt {
            border: none;
            background: none;
            outline: none;
            float: left;
            padding: 0;
            color: #fff;
            font-weight: bold;
            font-size: 16px;
            transition: 0.5s;
            cursor: pointer;
            width: 0px;
        }

        .Selection-box:hover > .Selection-txt {
            width: 178px;
            margin-left: 10px;
            padding: 0 6px;
            transition: 0.5s;
            /* top: 10px; */
            font-family: sans-serif;
            margin-top: -2px;
        }

        .Selection-box:hover > .not-icon {
            top: 0;
        }

        .Selection-box:hover > .Selection-btn {
            background: #fff;
        }

        .fa-info {
            border: 1px solid #000;
            border-radius: 100%;
            padding: 2px 3px 2px 3px;
            color: #3c8dbc;
        }

        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
            display: contents;
        }

            .tooltip img {
                height: 12px;
                width: 12px;
                margin-top: -2px;
            }

        #ctl00_ContentPlaceHolder1_txtDateOfIssue {
            width: 100px!important;
        }

        .tooltip .tooltiptext {
            visibility: hidden;
            width: auto;
            background-color: #fff;
            border: 1px solid #3c8dbc;
            text-align: justify;
            border-radius: 6px;
            padding: 5px;
            position: absolute;
            z-index: 12;
        }

        .tooltip:hover .tooltiptext {
            visibility: visible;
        }
    </style>
    <style>
        .box-header p {
            position: initial;
            right: 0px;
            top: 5px;
        }

        #not-bar {
            position: absolute!important;
            z-index: 22!important;
            right: 12px;
            top: 42px;
        }

        .list-group-item-new {
            padding: 0.35rem 0.15rem!important;
        }

        .list-group-item-new {
            position: relative;
            display: block;
            padding: 0.75rem 1.25rem;
            background-color: #fff;
            border-bottom: 1px solid rgba(0,0,0,.125);
        }

        .card-body {
            padding: 0.75rem;
        }
    </style>
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Reports</h3>

                            <div class="Selection-box" style="float: right; margin-top: -37px; overflow: hidden;">
                                <span class="Selection-txt">Mandatory Selection</span>
                                <a class="Selection-btn" href="#">
                                    <i class="fa fa-ellipsis-v fa-1x not-icon"></i>
                                </a>
                            </div>
                            <div id="not-bar" class="card">
                                <div class="drop-down selectrpt" id="selectrpt" style="width: 30rem">
                                    <div class="card-body" style="width: 30rem; height: 280px; overflow: auto">

                                        <div class="card-header">
                                            <span><i class="fa fa-star" style="color: red" aria-hidden="true"></i>Mandatory Selection</span>
                                        </div>
                                        <ul class="list-group list-group-flush">
                                            <li class=" nav selectzero list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Overall Internal Marks</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Section </p>
                                            </li>
                                            <li class=" nav selectone list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Internal Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Section </p>
                                            </li>
                                            <li class=" nav selecttwo  list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Tr Excel(Grade)</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	 </p>
                                            </li>
                                            <li class=" nav selectthree list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Faculty Wise Result Analysis</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	 </p>
                                            </li>
                                            <li class=" nav selectfour list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Analysis Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Section </p>
                                            </li>
                                            <li class=" nav selectfive list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Exam Fees Paid Excel Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session</p>
                                            </li>


                                            <!----------new btn----------->
                                            <li class="nav selectsix list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Degree Wise</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	</p>
                                            </li>
                                            <li class="nav selectseven list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Over All Percentage</a></b>

                                                <p class="nav2 mt-1">College & Scheme </p>
                                            </li>
                                            <li class="nav selecteight list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Over All Subject Percentage</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session </p>
                                            </li>
                                            <li class="nav selectnine list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Branch Semester wise Result Analysis</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session</p>
                                            </li>
                                            <li class="nav selectten list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Result Analysis</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session </p>
                                            </li>
                                            <li class="nav selecteleven list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Fail Student List</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	</p>
                                            </li>
                                            <li class="nav selecttwelve list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Course Wise Fail Student List</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Course </p>
                                            </li>
                                            <li class="nav selectthirteen list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Gpa Cgpa Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme </p>
                                            </li>

                                            <li class="nav selectfourteen list-group-item-new " id="pre_fourteen" runat="server"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Branch  Wise Result Analysis</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester  </p>
                                            </li>
                                            <li class="nav selectfifteen list-group-item-new " id="pre_fifteen" runat="server"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Substitute Exam Registration Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session </p>
                                            </li>


                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-md-3">

                                        <div class="label-dynamic">

                                            <sup>*</sup>

                                            <%--<label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" 
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Rank">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvCname1" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Branch">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="FormatIIReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="CourseWiseFailStudList">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="CourseWiseExamRegistartion">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="SubjectWiseResultanalysisReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="schemesession">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="AnalysisReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvrank" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Rank"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvOverallsession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Overall"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvsubOversession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Overallsub"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Backlogsub"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvCsession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Branch"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="FormatIIReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="CourseWiseFailStudList">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="CourseWiseExamRegistartion">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="SubjectWiseResultanalysisReport">
                                        </asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="schemesession">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--<div class="form-group col-lg-3 col-md-6 col-12" id="divClg" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="report"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="AnalysisReport"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Rank"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvoverallcol" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Overall"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvsubovercol" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Overallsub"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree"
                                            InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="AnalysisReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvdegree1" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Rank"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvoverdegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Overall"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvsuboverdegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Overallsub"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="AnalysisReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvBranch1" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Rank"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvoverbranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Overall"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvsuboverbranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Overallsub"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divScheme" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Regulation</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="AnalysisReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvregulation" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="Rank"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvsubover" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="Overallsub"></asp:RequiredFieldValidator>
                                    </div>--%>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSemester" runat="server">
                                        <div class="label-dynamic">

                                            <sup>*</sup>

                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Rank">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="AnalysisReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfcCsemester" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Branch">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="FormatIIReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSem" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="CourseWiseFailStudList">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSem" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="CourseWiseExamRegistartion">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlSem" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubjectWiseResultanalysisReport">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSection" runat="server">

                                        <div class="label-dynamic">

                                            <sup>*</sup>

                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Rank">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="AnalysisReport">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="FormatIIReport">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="SubjectWiseResultanalysisReport">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divexam" runat="server">

                                        <div class="label-dynamic">

                                            <sup></sup>

                                            <%--<label>Exam</label>--%>
                                            <asp:Label ID="lblDYddlExam" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1">

                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">CAT 1</asp:ListItem>
                                            <asp:ListItem Value="2">CAT 2</asp:ListItem>
                                            <asp:ListItem Value="3">CAT 3</asp:ListItem>
                                            <asp:ListItem Value="4">MODEL EXAM</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvddlExam" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" ValidationGroup="Rank">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" ValidationGroup="AnalysisReport">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divStudType" runat="server" style="display: none">

                                        <div class="label-dynamic">

                                            <label>Student Type</label>
                                            <%--<asp:Label ID="lblDYddlStudentType" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>

                                        <asp:DropDownList ID="ddlStudType1" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0" Selected="True">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Ex Student</asp:ListItem>
                                            <asp:ListItem Value="-1">Both</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server">
                                        <div class="label-dynamic">

                                            <sup>*</sup>

                                            <%--<label>Course</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0">
                                            <%--ValidationGroup="Rank"--%>
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0">
                                            <%--ValidationGroup="AnalysisReport"--%>
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlcourse" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="CourseWiseFailStudList">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlcourse" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="CourseWiseExamRegistartion">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlcourse" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="SubjectWiseResultanalysisReport">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divFromDate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="span2 form-control masked" data-date-format="dd/mm/yyyy" data-inputmask="'alias': 'date'"></asp:TextBox>
                                            <span class="input-group-addon" id="spnFromDate"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divToDate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="span2 form-control masked" data-date-format="dd/mm/yyyy" data-inputmask="'alias': 'date'"></asp:TextBox>
                                            <span class="input-group-addon" id="spnToDate"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server">

                                        <div class="label-dynamic">

                                            <%--<label>Student Type </label>--%>
                                            <asp:Label ID="lblDYddlStudentType" runat="server" Font-Bold="true"></asp:Label>

                                        </div>

                                        <asp:DropDownList ID="ddlStudType" runat="server" AppendDataBoundItems="True" Class="form-control" TabIndex="1">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Arrear</asp:ListItem>
                                            <%--<asp:ListItem Value="-1">Both</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStudType"
                                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1" ValidationGroup="FormatIIReport">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Subject Type </label>
                                        </div>
                                        <div class="input-group">
                                            <asp:RadioButton ID="rbtnCommonCourse" runat="server" GroupName="SubType" Text="Common" Checked="true" />&nbsp;&nbsp
                                            <asp:RadioButton ID="rbtnOpenEle" runat="server" GroupName="SubType" Text="Open Elective" />
                                        </div>
                                    </div>

                                    <%--<div class="form-group col-md-4">
                                        <label>Regulation Type</label>
                                        <asp:RadioButtonList ID="rdbRegulationType" runat="server" RepeatDirection="Horizontal" TabIndex="7" AutoPostBack="true">
                                            <asp:ListItem Value="1">&nbsp;&nbsp;Non-CBCS&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;CBCS&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rdbRegulationType"
                                            Display="None" ErrorMessage="Please Select Regulation Type" ValidationGroup="Backlogsub">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>


                                    <%--<div class="form-group col-md-4" id="div1" runat="server">
                                        <span style="color: Red">* </span></span><label> To Date :</label>
                                        <div id="reportrange" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc; width: 100%">
                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span></span> <i class="fa fa-caret-down"></i>
                                        </div>

                                        <script type="text/javascript">
                                            $(function () {

                                                var start = moment().subtract(29, 'days');
                                                var end = moment();

                                                function cb(start, end) {
                                                    $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                                                }

                                                $('#reportrange').daterangepicker({
                                                    startDate: start,
                                                    endDate: end,
                                                    ranges: {
                                                        'Today': [moment(), moment()],
                                                        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                                                        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                                                        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                                                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                                                        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                                                    }
                                                }, cb);

                                                cb(start, end);

                                            });
                                        </script>
                                    </div>--%>
                                </fieldset>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <%--    <asp:Button ID="btnAnalysis" runat="server" OnClick="btnAnalysis_Click" Text="Result Analysis Report" CssClass="btn btn-primary"
                                ValidationGroup="report" Visible="false" />
                            <asp:Button ID="btnInstitue" runat="server" Text="Institute Wise Result" CssClass="btn btn-primary"
                                ValidationGroup="report" OnClick="btnInstitue_Click" />
                            <asp:Button ID="btnSemester" runat="server" Text="Semester Wise Result" CssClass="btn btn-primary"
                                ValidationGroup="report" OnClick="btnSemester_Click" />
                            <asp:Button ID="btnSGPA" runat="server" Text="SGPA Wise Result" CssClass="btn btn-primary"
                                ValidationGroup="report" OnClick="btnSGPA_Click" />
                            <asp:Button ID="btnSubWiseRslt" runat="server" Text="Subject Wise Report" CssClass="btn btn-primary"
                                ValidationGroup="report" OnClick="btnSubWiseRslt_Click" Visible="false" />
                            <asp:Button ID="btnStatistical" runat="server" OnClick="btnStatistical_Click" Text="Statistic Report" CssClass="btn btn-primary"
                                ValidationGroup="report" />
                            <asp:Button ID="btnExamWiseReport" runat="server" OnClick="btnExamWiseReport_Click" Text="Examwise Report" CssClass="btn btn-primary"
                                ValidationGroup="report" Visible="false" />--%>
                                <%--  <asp:Button ID="btnRank" runat="server" OnClick="btnRank_Click" Text="Rank Report" ValidationGroup="Rank" CssClass="btn btn-info" style="display:none"/>--%>
                                <asp:Button ID="btnInternalMarkReg" runat="server" OnClick="btnInternalMarkReg_Click" ValidationGroup="Rank" Text="Overall Internal Mark" CssClass="btn btn-info" TabIndex="1" CausesValidation="false" />
                                <asp:Button ID="btnConsolidatedInternalTestMarkReport" runat="server" OnClick="btnConsolidatedInternalTestMarkReport_Click" Text="CAT wise Report" CssClass="btn btn-info" ValidationGroup="Rank" Visible="false" CausesValidation="false" />
                                <asp:Button ID="btnCATInternalMarks" runat="server" OnClick="btnCATInternalMarks_Click" Text="CAT Internal Marks" CssClass="btn btn-info" ValidationGroup="Rank" Visible="false" CausesValidation="false" />

                                <asp:Button ID="btnExelrpt" runat="server" Text="Internal Report" CssClass="btn btn-info" OnClick="btnExelrpt_Click" TabIndex="1" CausesValidation="false" />

                                <asp:Button ID="btnGraderpt" runat="server" Text="Format - II Report" CssClass="btn btn-info" OnClick="btnGraderpt_Click" TabIndex="1" ValidationGroup="FormatIIReport" />
                                <asp:Button ID="btntrexcel" runat="server" Text="TR EXCEL (GRADE)" CssClass="btn btn-info" OnClick="btntrexcel_Click" TabIndex="1" CausesValidation="false" />

                                <asp:Button ID="btnGradesheet" runat="server" Text="Analysis Report" CssClass="btn btn-info" OnClick="btnGradesheet_Click" TabIndex="1" Visible="true" CausesValidation="false" />

                                <asp:Button ID="btnFaculty" runat="server" Text="Faculty Wise Result Analysis" CssClass="btn btn-info" OnClick="btnFaculty_Click" TabIndex="1" CausesValidation="false" Visible="false" />
                                <asp:Button ID="btnAnalysis" runat="server" Text="Analysis Report" CssClass="btn btn-info" OnClick="btnAnalysis_Click" TabIndex="1" CausesValidation="false" Visible="false" />

                                <asp:Button ID="btnExamFeesPaid" runat="server" Text="Exam Fees Paid Excel Report" CssClass="btn btn-info" OnClick="btnExamFeesPaid_Click" TabIndex="1" ValidationGroup="schemesession"/>

                                <%-- <asp:Button ID="btnModelExam" runat="server" OnClick="btnModelExam_Click" ValidationGroup="report" Text="Model Exam Mark" CssClass="btn btn-info" style="display:none"/>
                            <asp:Button ID="btnCorrelationAnalysis" runat="server" OnClick="btnCorrelationAnalysis_Click" ValidationGroup="AnalysisReport" Text="Correlation Analysis" CssClass="btn btn-info" style="display:none"/>
                            <asp:Button ID="btnSubjectFaculty" runat="server" OnClick="btnSubjectFaculty_Click" ValidationGroup="report" Text="Subject Handling Faculty" CssClass="btn btn-info" style="display:none" />
                            <asp:Button ID="btnConsolidateTestMarkReprt" runat="server" OnClick="btnConsolidateTestMarkReprt_Click" Text="Consolidated Report" CssClass="btn btn-info" ValidationGroup="report" style="display:none" />



                           
                            <asp:Button ID="btnConsoHrReport" runat="server" OnClick="btnConsoHrReport_Click" Text="CAT wise Report (Att Hrs.)" CssClass="btn btn-info" ValidationGroup="report" style="display:none" />
                            <asp:Button ID="btnOverallPercentage" runat="server" OnClick="btnOverallPercentage_Click" ValidationGroup="Overall" Text="Over All Percentage" CssClass="btn btn-info" style="display:none"/>
                            <asp:Button ID="btnOverallSubpercentage" runat="server" OnClick="btnOverallSubpercentage_Click" ValidationGroup="Overallsub" Text="Over All Subject Percentage" CssClass="btn btn-info" style="display:none" />
                           
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />--%>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AnalysisReport" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Rank" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Overall" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Overallsub" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Backlogsub" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="FormatIIReport" />

                                <%-- <asp:Button ID="btnResultRemark" runat="server" OnClick="btnResultRemark_Click" Text="Student Result Remark" CssClass="btn btn-info" CausesValidation="false" style="display:none"/>--%>
                                <%--<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" CausesValidation="False" />--%>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnCategoryWise" runat="server" TabIndex="1" Text="Degree Wise" CssClass="btn btn-info" OnClick="btnCategoryWise_Click" CausesValidation="false" />
                                <asp:Button ID="btnOverAllPercentage" runat="server" TabIndex="1" Text="Over All Percentage" CssClass="btn btn-info" OnClick="btnOverAllPercentage_Click" CausesValidation="false" />
                                <asp:Button ID="btnOverAllSubjectPercentage" runat="server" TabIndex="1" Text="Over All Subject Percentage" CssClass="btn btn-info" OnClick="btnOverAllSubjectPercentage_Click" ValidationGroup="schemesession"/>
                                <asp:Button ID="btnBranchSemAnalysis" runat="server" TabIndex="1" Text="Branch Semester wise Result Analysis" CssClass="btn btn-info" OnClick="btnBranchSemAnalysis_Click" ValidationGroup="schemesession" />
                                <asp:Button ID="btnResultAnalysis" runat="server" TabIndex="1" Text="Result Analysis" CssClass="btn btn-info" OnClick="btnResultAnalysis_Click" ValidationGroup="schemesession" />

                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnFailStudentList" runat="server" TabIndex="1" Text="Fail Student List" CssClass="btn btn-info" OnClick="btnFailStudentList_Click" CausesValidation="false" />
                                <asp:Button ID="btnCourceWiseFailStudList" Text="Course Wise Fail Student List" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnCourceWiseFailStudList_Click" ValidationGroup="CourseWiseFailStudList" />
                                <%--ValidationGroup="Tabulation"--%>
                                <asp:Button ID="btnGetGpaReport" runat="server" TabIndex="1" Text="Gpa Cgpa Report" CssClass="btn btn-info" OnClick="btnGetGpaReport_Click" CausesValidation="false" />
                                <asp:Button ID="btnCGPAReport" runat="server" TabIndex="1" Text="CGPA Excel Report" CssClass="btn btn-info" Visible="false" OnClick="btnCGPAReport_Click" CausesValidation="false" />
                                <asp:Button ID="btnBranchWiseResultAnalysis" runat="server" TabIndex="1" Text="Branch Wise Result Analysis" CssClass="btn btn-info" ValidationGroup="Branch" OnClick="btnBranchWiseResultAnalysis_Click" />
                                <asp:Button ID="btnCourseWiseExamRegistartion" Text="Course Wise Exam Registartion" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnCourseWiseExamRegistartion_Click" ValidationGroup="CourseWiseExamRegistartion" />
                                <asp:Button ID="btnSubjectWiseResultanalysisReport" Text="Subject Wise Result Analysis Report" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnSubjectWiseResultanalysisReport_Click" ValidationGroup="SubjectWiseResultanalysisReport" />


                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Branch" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CourseWiseFailStudList" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CourseWiseExamRegistartion" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubjectWiseResultanalysisReport" CausesValidation="false" />

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubtituteexamexcel" Text="Substitute Registeration Excel" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnsubtituteexamexcel_Click" ValidationGroup="schemesession" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" CausesValidation="False" />
                                <asp:ValidationSummary ID="valsumschemesession" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="schemesession" CausesValidation="false" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btntrexcel" />
            <asp:PostBackTrigger ControlID="btnExamFeesPaid" />
            <asp:PostBackTrigger ControlID="btnCGPAReport" />
            <asp:PostBackTrigger ControlID="btnCourseWiseExamRegistartion" />
            <asp:PostBackTrigger ControlID="btnsubtituteexamexcel" />
            <%--  <asp:AsyncPostBackTrigger ControlID="btnSubWiseRslt" />--%>
            <%-- <asp:PostBackTrigger ControlID="btnStatistical" />
            <asp:PostBackTrigger ControlID="btnSGPA" />
            <asp:PostBackTrigger ControlID="btnSemester" />
            <asp:PostBackTrigger ControlID="btnInstitue" />
            <asp:PostBackTrigger ControlID="btnExamWiseReport" />
            <asp:AsyncPostBackTrigger ControlID="btnConsolidateTestMarkReprt" />
            <asp:AsyncPostBackTrigger ControlID="btnConsolidatedInternalTestMarkReport" />
            <asp:AsyncPostBackTrigger ControlID="btnRank" />
            <asp:AsyncPostBackTrigger ControlID="btnInternalMarkReg" />
            <asp:PostBackTrigger ControlID="btnCATInternalMarks" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <%--Consolidated_1Modal Starts Here--%>
    <div class="modal fade" id="ConsolidatedModel_1" role="dialog">
        <asp:UpdatePanel ID="updPopUp_1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
<%--                                            <asp:LinkButton ID="lbtn_ConsoPrint" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoPrint_Click"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_ConsoExcel" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoExcel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>--%>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--   <asp:PostBackTrigger ControlID="lbtn_ConsoPrint" />
                <asp:PostBackTrigger ControlID="lbtn_ConsoExcel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Ends Here--%>

    <%--Consolidated_1Modal Starts Here--%>
    <div class="modal fade" id="ConsolidatedModel_2" role="dialog">
        <asp:UpdatePanel ID="updPopUp_2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
                                            <asp:LinkButton ID="lbtn_ConsoPrint_Internal" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoPrint_Internal_Click" CausesValidation="false"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_ConsoExcel_Internal" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoExcel_Internal_Click" CausesValidation="false"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtn_ConsoPrint_Internal" />
                <asp:PostBackTrigger ControlID="lbtn_ConsoExcel_Internal" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Ends Here--%>

    <%--Consolidated_2Modal Starts Here--%>
    <div class="modal fade" id="ConsolidatedModel_3" role="dialog">
        <asp:UpdatePanel ID="updPopUp_3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
                                          <%--  <asp:LinkButton ID="lbtn_ConsoPrint_HR" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoPrint_HR_Click"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_ConsoExcel_HR" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoExcel_HR_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>--%>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:PostBackTrigger ControlID="lbtn_ConsoPrint_HR" />
                <asp:PostBackTrigger ControlID="lbtn_ConsoExcel_HR" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Ends Here--%>

    <%--Rank Modal Starts Here--%>
    <div class="modal fade" id="RankModel" role="dialog">
        <asp:UpdatePanel ID="updPopUp_4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
                                          <%--  <asp:LinkButton ID="lbtn_Rank_Print" runat="server" CssClass="btn btn-info" OnClick="lbtn_Rank_Print_Click"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_Rank_Excel" runat="server" CssClass="btn btn-info" OnClick="lbtn_Rank_Excel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>--%>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:PostBackTrigger ControlID="lbtn_Rank_Print" />
                <asp:PostBackTrigger ControlID="lbtn_Rank_Excel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Ends Here--%>

    <%--Model Exam starts here --%>
    <div class="modal fade" id="Modelexam" role="dialog">
        <asp:UpdatePanel ID="updPopUp_5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
                                           <%-- <asp:LinkButton ID="lbtn_model_Print" runat="server" CssClass="btn btn-info" OnClick="lbtn_model_Print_Click"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_model_Excel" runat="server" CssClass="btn btn-info" OnClick="lbtn_model_Excel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>--%>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="lbtn_model_Print" />
                <asp:PostBackTrigger ControlID="lbtn_model_Excel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Model Exam Ends here--%>

    <%--Model Exam starts here --%>
    <div class="modal fade" id="ConsolidatedReportModel" role="dialog">
        <asp:UpdatePanel ID="updPopUp_6" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 400px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>User Option</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <center>
                                            <asp:LinkButton ID="lbtn_ConsoReport_Print" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoReport_Print_Click"><i class="fa fa-print" aria-hidden="true"></i> Print</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_ConsoReport_Excel" runat="server" CssClass="btn btn-info" OnClick="lbtn_ConsoReport_Excel_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel</asp:LinkButton>
                                        </center>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtn_ConsoReport_Print" />
                <asp:PostBackTrigger ControlID="lbtn_ConsoReport_Excel" />
                <asp:PostBackTrigger ControlID="btnExelrpt" />
                <asp:PostBackTrigger ControlID="btntrexcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Model Exam Ends here--%>

    <script>
        $(window).on("load", function () {
            $(document).ready(function () {
                $('.form-control').removeClass('form-control').addClass('form-control ');
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            $("body").on("click", ".Selection-box", function () {
                $(".selectrpt").fadeToggle("main");
                $(".nav").click(function () {
                    $(".selectrpt").css("display", "none");
                });

                $(".selectzero").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").addClass("red-color");


                });
                $(".selectone").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").addClass("red-color");


                });
                $(".selecttwo").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSection").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");


                });
                $(".selectthree").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").removeClass("red-color");
                    //$("#ctl00_ContentPlaceHolder1_ddlSection").addClass("red-color");

                });
                $(".selectfour").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").addClass("red-color");

                });
                $(".selectfive").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                });

                $(".selectsix").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");

                });

                $(".selectseven").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSession").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");

                });

                $(".selecteight").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");

                });

                $(".selectnine").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");


                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");

                });
                $(".selectten").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                });
                $(".selecteleven").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                });
                $(".selecttwelve").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").addClass("red-color");
                });

                $(".selectthirteen ").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSession").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                });

                $(".selectfourteen ").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").addClass("red-color");

                });
                $(".selectfifteen ").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlcourse").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSem").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSection").removeClass("red-color");
                });

            });
        });

    </script>

    <script>
        $(document).ready(function () {

            $(":input").inputmask();

            $('#spnFromDate').click(function (event) {
                $(<%=txtFromDate.ClientID%>).focus();
            });
            $('#spnToDate').click(function (event) {
                $(<%=txtToDate.ClientID%>).focus();
            });
            $(<%=txtFromDate.ClientID%>).datepicker();
            $(<%=txtToDate.ClientID%>).datepicker();

            $(<%=txtFromDate.ClientID%>).focusout(function () {
                if ($(<%=txtFromDate.ClientID%>).val().indexOf('d') > -1) {
                    alert("Please Enter Valid Date");
                    $(<%=txtFromDate.ClientID%>).val('');
                }
                else if ($(<%=txtFromDate.ClientID%>).val().indexOf('m') > -1) {
                    alert("Please Enter Valid Month");
                    $(<%=txtFromDate.ClientID%>).val('');
                }
                else if ($(<%=txtFromDate.ClientID%>).val().indexOf('y') > -1) {
                    alert("Please Enter Valid Year");
                    $(<%=txtFromDate.ClientID%>).val('');
                }
            });

            $(<%=txtToDate.ClientID%>).focusout(function () {
                if ($(<%=txtToDate.ClientID%>).val().indexOf('d') > -1) {
                    alert("Please Enter Valid Date");
                    $(<%=txtToDate.ClientID%>).val('');
                }
                else if ($(<%=txtToDate.ClientID%>).val().indexOf('m') > -1) {
                    alert("Please Enter Valid Month");
                    $(<%=txtToDate.ClientID%>).val('');
                }
                else if ($(<%=txtToDate.ClientID%>).val().indexOf('y') > -1) {
                    alert("Please Enter Valid Year");
                    $(<%=txtToDate.ClientID%>).val('');
                }
            });

            $(<%=txtFromDate.ClientID%>).datepicker().on('keydown', function (ev) {
                if (ev.keyCode === 9) { //tab
                    $(this).datepicker('hide');
                }
            });

            $(<%=txtToDate.ClientID%>).datepicker().on('keydown', function (ev) {
                if (ev.keyCode === 9) { //tab
                    $(this).datepicker('hide');
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {

                $(":input").inputmask();

                $('#spnFromDate').click(function (event) {
                    $(<%=txtFromDate.ClientID%>).focus();
                });
                $('#spnToDate').click(function (event) {
                    $(<%=txtToDate.ClientID%>).focus();
                });

                $(<%=txtFromDate.ClientID%>).datepicker();
                $(<%=txtToDate.ClientID%>).datepicker();

                $(<%=txtFromDate.ClientID%>).focusout(function () {
                    if ($(<%=txtFromDate.ClientID%>).val().indexOf('d') > -1) {
                        alert("Please Enter Valid Date");
                        $(<%=txtFromDate.ClientID%>).val('');
                    }
                    else if ($(<%=txtFromDate.ClientID%>).val().indexOf('m') > -1) {
                        alert("Please Enter Valid Month");
                        $(<%=txtFromDate.ClientID%>).val('');
                    }
                    else if ($(<%=txtFromDate.ClientID%>).val().indexOf('y') > -1) {
                        alert("Please Enter Valid Year");
                        $(<%=txtFromDate.ClientID%>).val('');
                    }
                });

                $(<%=txtToDate.ClientID%>).focusout(function () {
                    if ($(<%=txtToDate.ClientID%>).val().indexOf('d') > -1) {
                        alert("Please Enter Valid Date");
                        $(<%=txtToDate.ClientID%>).val('');
                    }
                    else if ($(<%=txtToDate.ClientID%>).val().indexOf('m') > -1) {
                        alert("Please Enter Valid Month");
                        $(<%=txtToDate.ClientID%>).val('');
                    }
                    else if ($(<%=txtToDate.ClientID%>).val().indexOf('y') > -1) {
                        alert("Please Enter Valid Year");
                        $(<%=txtToDate.ClientID%>).val('');
                    }
                });

                $(<%=txtFromDate.ClientID%>).datepicker().on('keydown', function (ev) {
                    if (ev.keyCode === 9) { //tab
                        $(this).datepicker('hide');
                    }
                });

                $(<%=txtToDate.ClientID%>).datepicker().on('keydown', function (ev) {
                    if (ev.keyCode === 9) { //tab
                        $(this).datepicker('hide');
                    }
                });

                $(<%=txtFromDate.ClientID%>).on('changeDate', function (ev) {
                    $(this).datepicker('hide');
                });

                $(<%=txtToDate.ClientID%>).on('changeDate', function (ev) {
                    $(this).datepicker('hide');
                });

            });

        });
    </script>

</asp:Content>
