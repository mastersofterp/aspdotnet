<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TabulationChart.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TabulationChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="   ">
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
        .red-color + .select2-container {
            border: 1px solid #ff8080 !important;
        }

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

        .btn-info {
        }
    </style>
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Tabulation / Grade Card</h3>
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
                                            <span style="margin-left: -13px;"><i class="fa fa-star" style="color: red" aria-hidden="true"></i>Mandatory Selection</span>
                                        </div>
                                        <ul class="list-group list-group-flush">
                                            <%--<li class=" nav selectone list-group-item-new"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Show Students</a></b>
                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Student Type </p>
                                            </li>--%>
                                            <li class=" nav selecttwo list-group-item-new"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Grade Card</a></b>
                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i><span id="tab_year" runat="server">Year <i class="fa fa-arrow-circle-right" aria-hidden="true"></i></span>Student Type </p>
                                            </li>
                                            <li class=" nav selectthree list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Consolidate Grade Card</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Date Of Issue </p>
                                            </li>
                                            <li class="nav selectfour list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Consolidate Grade Card Without Header</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Student Type </p>
                                            </li>
                                            <li class="nav selectfive  list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>TR Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year	<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Student Type </p>
                                            </li>
                                            <%--  <li class="nav selectsix list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Result Report In Excel</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Section </p>
                                            </li>--%>
                                            <li class=" nav selectseven list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>TR Report (Excel)</a></b>

                                                <p class="nav2 mt-1">College & Scheme</p>
                                                <%--<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>--%>
                                                <%--Session--%>
                                            </li>


                                            <!----------new btn----------->
                                            <li class=" nav selecteleven  list-group-item-new " id="pre_eleven" runat="server"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Progression Report</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year</p>
                                            </li>
                                            <li class=" nav selecteight  list-group-item-new " id="pre_eight" runat="server"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Result Statistics</a></b>

                                                <p class="nav2 mt-1">Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Student Type </p>
                                            </li>
                                            <li class=" nav selectnine list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Grade Register</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Year  </p>
                                            </li>
                                            <li class=" nav selectten list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Grace Students</a></b>
                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Student Type </p>
                                            </li>



                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Demo</label>
                                        </div>
                                        <asp:TextBox ID="txtDemos" runat="server" CssClass="demo-use"></asp:TextBox>

                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control threess redborder redborder2 redborder3" data-select2-enable="true"
                                            ValidationGroup="Summary" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="ER"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="CPD"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Ledger"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="GradeCard"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="TRReport"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="ResultGazette"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" Display="None" runat="server" ControlToValidate="ddlClgname"
                                            InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="TRGradeReg"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" Display="None" runat="server" ControlToValidate="ddlClgname"
                                            InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="ConsoGradeCard"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="ddlClgname" Display="None"
                                            ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Consoli"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="ddlClgname" Display="None"
                                            ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="GradeCardIssueRegister"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFVddlClgname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="ProgressionReport"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFVddlClgnameTRExcel" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="TrExcel"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvcollegeforpassing" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="passingCrft"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-md-6">
                                        <label>Admission Batch</label>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddladmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="True" ValidationGroup="Summary">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="CPD"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Ledger"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="TrExcel"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="GradeCard"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="ResultExcel"
                                            SetFocusOnError="True" Enabled="false"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="TRReport"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="ResultGazette"
                                            SetFocusOnError="True" Enabled="false"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="ResultStatistics"
                                            SetFocusOnError="True" Enabled="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" Display="None" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="TRGradeReg" Enabled="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Consoli"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="UFMLIST"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="GradeCardIssueRegister"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" TabIndex="2" runat="server" AppendDataBoundItems="True" ValidationGroup="" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="Required2" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select  School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDegree" Display="None"
                                            ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme /Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None"
                                            ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBranch" Display="None"
                                            ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="Summary" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Ledger"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSemester" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="GradeCard"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="TRReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="ResultGazette"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" Display="None" runat="server" ControlToValidate="ddlSemester"
                                            InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="TRGradeReg"></asp:RequiredFieldValidator>
                                    </div>
                                    <div id="Yearid" runat="server" visible="false" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="Summary" CssClass="form-control threess redborder redborder2 redborder3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="ER"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="GradeCard"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="TRReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="ddlYear" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="ResultGazette"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" Display="None" runat="server" ControlToValidate="ddlYear"
                                            InitialValue="0" ErrorMessage="Please Select Year" ValidationGroup="TRGradeReg"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" Display="None" runat="server" ControlToValidate="ddlYear"
                                            InitialValue="0" ErrorMessage="Please Select Year" ValidationGroup="ConsoGradeCard"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RFVYearProgressionReport" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="ProgressionReport"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStuType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control redborder redborder2 redborder3" OnSelectedIndexChanged="ddlStuType_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            <asp:ListItem Value="2">PhotoCopy/Revaluation</asp:ListItem>
                                            <asp:ListItem Value="3">Redo/Resit</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvstudenttype" Display="None" runat="server" ControlToValidate="ddlStuType" SetFocusOnError="true"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" Display="None" runat="server" ControlToValidate="ddlStuType" SetFocusOnError="true"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="GradeCard"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" Display="None" runat="server" ControlToValidate="ddlStuType"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" Display="None" runat="server" ControlToValidate="ddlStuType"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" Display="None" runat="server" ControlToValidate="ddlStuType"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="TRReport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" Display="None" runat="server" ControlToValidate="ddlStuType"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="ResultGazette"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="None" runat="server" ControlToValidate="ddlStuType"
                                            InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="ResultStatistics"></asp:RequiredFieldValidator>
                                    </div>



                                    <div id="divYear" runat="server" visible="false" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYears" runat="server" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="ER" CssClass="form-control threess redborder redborder2 redborder3" data-select2-enable="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="ddlYears" Display="None"
                                            ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="ER"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Exam held in Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDeclareDate" TabIndex="7" ToolTip="Please Enter Declared Date" CssClass="form-control redborder redborder2 redborder3"></asp:TextBox>
                                            <%--<asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                            <%--<ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDeclareDate" PopupButtonID="imgExamDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDeclareDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MaskType="Date" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Declared"
                                            ControlExtender="meExamDate" ControlToValidate="txtDeclareDate" IsValidEmpty="false"
                                            InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Declared"
                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDeclareDate"
                                            Display="None" ErrorMessage="Please Select/Enter Declared Date" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDeclareDate"
                                            Display="None" ErrorMessage="Please Select Exam held in Date " ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtDeclareDate"
                                            Display="None" ErrorMessage="Please Select Exam held in Date " ValidationGroup="GradeCard"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDeclareDate"
                                            Display="None" ErrorMessage="Please Select Exam held in Date " ValidationGroup="TRReport"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Dateissue" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Date Of Publish</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgFromDate1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDateOfIssue" TabIndex="1" ToolTip="Please Enter Date Of Result Issue" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgFromDate1" TargetControlID="txtDateofIssue" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                TargetControlID="txtDateofIssue" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txtDateOfIssue"
                                                ErrorMessage="Please Select Date of Issue" ValidationGroup="ConsoGradeCard"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DatePublish" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Date Of Issue</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgPublishDate1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDateOfPublish" TabIndex="1" ToolTip="Please Enter Date Of Result Publication" CssClass="form-control" Style="width: 71%;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="cePublishdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgPublishDate1" TargetControlID="txtDateOfPublish" />
                                            <ajaxToolKit:MaskedEditExtender ID="meePublishDate" runat="server"
                                                TargetControlID="txtDateOfPublish" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="txtDateOfPublish"
                                                ErrorMessage="Please Select Date of Publish" ValidationGroup="ConsoGradeCard"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divprint" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Date Of Convocation</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgPrint">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtprint" TabIndex="1" ToolTip="Please Enter Date" CssClass="form-control" Style="width: 71%;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgPrint" TargetControlID="txtprint" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                TargetControlID="txtprint" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txtprint"
                                                ErrorMessage="Please Select Date" ValidationGroup="CPD"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Grade Card For</label>
                                        </div>
                                        <asp:DropDownList ID="ddlgradecardfor" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control redborder redborder2 redborder3" data-select2-enable="true"
                                            TabIndex="9">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">Non CBCS</asp:ListItem>
                                            <asp:ListItem Value="2">CBCS</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlgradecardfor" Display="None"
                                            ErrorMessage="Please Select Grade Card Format" InitialValue="0" ValidationGroup="GradeCard"></asp:RequiredFieldValidator>--%>
                                        <asp:Label ID="lblmsg" runat="server" Style="color: red;"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label id="lblScrutinized" runat="server">Scrutinized By</label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtScrutinized" TabIndex="1" ToolTip="Please Enter Date Of Result Publication" Visible="false" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlgradecardfor" Display="None"
                                            ErrorMessage="Please Select Grade Card Format" InitialValue="0" ValidationGroup="GradeCard"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:Label ID="Label1" runat="server" Style="color: red;"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="showstudents" runat="server" TabIndex="1" CssClass="btn btn-primary" Text="Show Students" OnClick="showstudents_Click" ValidationGroup="Show" />
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Cancel" TabIndex="1" CausesValidation="False" CssClass="btn btn-warning" />
                            </div>
                            <div class="form-group col-lg-10 col-md-12 col-12">
                                <%-- <div class="note-div">
                                    <h5 class="heading">Note (Please Select)</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Grade Card/Grade Card Without Header - <span style="color: green; font-weight: bold">Session->School/Institute Name->Degree->Programme/Branch->Semester->Student type</span></span>  </p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Tabulation Report (TR Report) - <span style="color: green; font-weight: bold">Session->School/Institute Name->Degree->Programme/Branch->Semester->Student type</span></span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Fail Student List/Coursewise Fail Student List - <span style="color: green; font-weight: bold">Session->School/Institute Name->Degree->Programme/Branch->Semester->Student type</span></span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Result report in excel - <span style="color: green; font-weight: bold">No selection criteria for result report in excel</span></span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Overall Backlog report in excel - <span style="color: green; font-weight: bold">No selection criteria for Overall Backlog report in excel</span></span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>TR Report(Excel) - <span style="color: green; font-weight: bold">Only  Session Selection is Required</span></span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Result Statistics - <span style="color: green; font-weight: bold">Only  Session and Student Type Selection is Required</span></span></p>
                                </div>--%>
                                <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:HiddenField ID="stuadmbatch" runat="server" />
                                <asp:Button ID="btnBranchwise" Text="Summary Result Sheet Branchwise" runat="server"
                                    TabIndex="1" CssClass="btn btn-info" Visible="false" />

                                <asp:Button ID="btnReport" Text="Summary Result Sheet" runat="server"
                                    TabIndex="1" CssClass="btn btn-info" Visible="false" />

                                <asp:Button ID="btnLedgerReport" Text="Ledger Report" runat="server"
                                    TabIndex="1" CssClass="btn btn-info" OnClick="btnLedgerReport_Click" CausesValidation="false" ValidationGroup="Ledger" />

                                <asp:Button ID="btnGradeCard" Text="Grade Card" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    OnClick="btnGradeCard_Click1"
                                    ValidationGroup="GradeCard" />
                                <asp:Button ID="btnGradesheet" Text="Grade Sheet in Excel" runat="server" ValidationGroup="Show" Visible="false" CssClass="btn btn-info"
                                    OnClick="btnGradesheet_Click" />
                                <asp:Button ID="btnConsoli" runat="server" Text="Consolidated Report(B4 Size)" ValidationGroup="Consoli"
                                    CssClass="btn btn-info" OnClick="btnConsoli_Click" />
                                <asp:Button ID="btnConsoliA4" runat="server" Text="Consolidated Report(A4 Size)" ValidationGroup="Consoli"
                                    CssClass="btn btn-info" OnClick="btnConsoliA4_Click" />

                                <asp:Button ID="btnConsolidateGradeCard" Visible="false" Text="Consolidate Grade Card" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnConsolidateGradeCard_Click"
                                    ValidationGroup="ConsoGradeCard" />

                                <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="1" CssClass="btn btn-info"
                                    OnClick="btnSendEmail_Click1" ToolTip="Send Card By Email"
                                    ValidationGroup="GradeCard" Visible="false" />

                                <asp:Button ID="btnGradeCardHeader" runat="server" TabIndex="1" Text="Grade Card Without Header"
                                    ValidationGroup="GradeCard" CssClass="btn btn-info" OnClick="btnGradeCardHeader_Click" />

                                <asp:Button ID="btnSummary" runat="server" Text="Result Checklist"
                                    ValidationGroup="Summary" TabIndex="1" CssClass="btn btn-info" Visible="false" />

                                <asp:Button ID="btnGradeCardBackReport" runat="server" TabIndex="1" Text="Grade Card Back Report"
                                    CssClass="btn btn-info col-2 me-1" Visible="false" OnClick="btnGradeCardBackReport_Click" />

                                <asp:Button ID="btnRankList" runat="server" Text="Rank List" ValidationGroup="Summary"
                                    TabIndex="1" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnufm" runat="server" Text="UFM List" ValidationGroup="UFMLIST"
                                    TabIndex="1" CssClass="btn btn-info" Visible="false" OnClick="btnufm_Click" />

                                <asp:Button ID="btntabulation" runat="server" Text="Tabulation Format 1"
                                    ValidationGroup="Tabulation" TabIndex="1" CssClass="btn btn-info" OnClick="btntabulation_Click" Visible="false" Height="26px" Width="175px" />

                                <asp:Button ID="btnResultStatement" Text="TR Report" runat="server"
                                    ValidationGroup="TRReport" TabIndex="1" CssClass="btn btn-info" OnClick="btnResultStatement_Click" />

                                <asp:Button ID="btnFailStudentList" runat="server" Text="Fail Student List"
                                    ValidationGroup="Tabulation" TabIndex="1" CssClass="btn btn-info" Visible="false" OnClick="btnFailStudentList_Click" />

                                <asp:Button ID="btnCourceWiseFailStudList" Text="Coursewise Fail Student List" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    ValidationGroup="Show" OnClick="btnCourceWiseFailStudList_Click" Visible="false" />

                                <asp:Button ID="btnResultExcel" Text="Result report in excel" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    ValidationGroup="ResultExcel" OnClick="btnResultExcel_Click" />
                                <%-- ValidationGroup="ResultExcel"--%>
                                <asp:Button ID="btnBacklogExcel" Text="Overall Backlog report in excel" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    OnClick="btnBacklogExcel_Click" ValidationGroup="ResultExcel" />
                                <%--  <asp:LinkButton ID="btnBacklogExcel1" Text="Overall Backlog report in excel" runat="server" TabIndex="25" CssClass="btn btn-info" OnClick="btnBacklogExcel1_Click" ValidationGroup="ResultExcel" />--%>
                                <%--Deepali--%>
                                <br />
                                <asp:Button ID="btnTRExcel" Text="TR Report (Excel)" runat="server" CssClass="btn btn-info" TabIndex="1" ValidationGroup="TrExcel" OnClick="btnTRExcel_Click" /><%--ValidationGroup="TrExcel"--%>
                                <asp:Button ID="btnProgrssionrpt" Text="Progression Report" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    ValidationGroup="ProgressionReport" OnClick="btnProgrssionrpt_Click" />
                                <%--Added By gaurav for Commeon code crescent--%>
                                <%-- <asp:Button ID="btncoursegrade" Text="FORMAT - II Report" runat="server" CssClass="btn btn-info" TabIndex="27" ValidationGroup="TrExcel" OnClick="btncoursegrade_Click" /><%--ValidationGroup="TrExcel"--%>
                                <%--<asp:Button ID="btngraderange" Text="Grade Range Report" runat="server" CssClass="btn btn-info" CausesValidation="false" TabIndex="26" OnClick="btngraderange_Click" />--%> <%--ValidationGroup="TrExcel"--%>
                                <%--<asp:Button ID="btnExcel" Text="FORMAT - II Excel Report" runat="server" CssClass="btn btn-info" TabIndex="28" ValidationGroup="TrExcel" OnClick="btnExcel_Click" />--%>
                                <%--<asp:Button ID="btnExamFeesPaid" Text="Exam Fees Paid Excel Report" runat="server" CssClass="btn btn-info" TabIndex="29" CausesValidation="false" OnClick="btnExamFeesPaid_Click" />--%>

                                <asp:Button ID="btnResultStatistics" runat="server" Text="Result Statistics" TabIndex="1" CssClass="btn btn-info" ValidationGroup="ResultStatistics" OnClick="btnResultStatistics_Click" />

                                <asp:Button ID="btnTabulationNew" Text="Tabulation New Format" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    ValidationGroup="Show" OnClick="btnTabulationNew_Click" Visible="false" />

                                <asp:Button ID="btnStatementMarks" Text="Statement Of Marks" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnStatementMarks_Click"
                                    Visible="false" ValidationGroup="GradeCard" />

                                <asp:Button ID="btnResultGazette" Text="Result Gazette" runat="server" TabIndex="1" CssClass="btn btn-info" OnClick="btnResultGazette_Click"
                                    Visible="false" ValidationGroup="ResultGazette" />


                                <asp:Button ID="btnGradeRegister" runat="server" Text="Grade Register" TabIndex="1" CssClass="btn btn-info" ValidationGroup="TRGradeReg" OnClick="btnGradeRegister_Click" />
                                <asp:Button ID="btnGrace" runat="server" TabIndex="31" CssClass="btn btn-info" Text="Grace Students" ValidationGroup="Show" OnClick="btnGrace_Click" />

                                <asp:Button ID="btncoursegrade" Text="FORMAT - II Report" runat="server" CssClass="btn btn-info" TabIndex="1" ValidationGroup="TrExcel" OnClick="btncoursegrade_Click" />
                                <asp:Button ID="btnCIAExcel" Text="CIA Result Analysis Excel" runat="server" TabIndex="1" Visible ="false" CssClass="btn btn-info" OnClick="btnCIAExcel_Click" ValidationGroup="TRGradeReg"/>
                                <br>

                                <asp:Button ID="btngraderange" Text="Grade Range Report" runat="server" CssClass="btn btn-info" CausesValidation="false" TabIndex="1" OnClick="btngraderange_Click" />
                                <asp:Button ID="btnExcel" Text="FORMAT - II Excel Report" runat="server" CssClass="btn btn-info" TabIndex="1" ValidationGroup="TrExcel" OnClick="btnExcel_Click" />
                                <asp:Button ID="btnExamFeesPaid" Text="Exam Fees Paid Excel Report" runat="server" CssClass="btn btn-info" TabIndex="1" CausesValidation="false" OnClick="btnExamFeesPaid_Click" />

                                <asp:Button ID="btnConvocationExcelReport" Text="Convocation Excel Report" runat="server" TabIndex="1" Visible="false" CssClass="btn btn-info" CausesValidation="false" OnClick="btnConvocationExcelReport_Click1" />
                                <asp:Button ID="btnCount" Text="Result Analysis Report" runat="server" TabIndex="1" Visible="false" CssClass="btn btn-info" OnClick="btnCount_Click" ValidationGroup="GradeCard" />

                                <asp:Button ID="btnSRNo" Text="SERIAL NUMBER" runat="server"
                                    TabIndex="1" CssClass="btn btn-info" Visible="false" OnClick="btnSRNo_Click" CausesValidation="false" />

                                <asp:Button ID="btnProvisionalDegree" runat="server" Text="Provisional Degree Certificate" TabIndex="1" Visible="false" CssClass="btn btn-info" ValidationGroup="TRGradeReg" OnClick="btnProvisionalDegree_Click" />
                                <asp:Button ID="btnLedger" runat="server" Text="Students Ledger Report" TabIndex="1" Visible="false" CssClass="btn btn-info" ValidationGroup="Ledger" OnClick="btnLedger_Click" />
                                <asp:Button ID="btnGradeCardIssueRegister" runat="server" Text="Grade Card Issue Register" TabIndex="1" Visible="false" CssClass="btn btn-info" ValidationGroup="GradeCardIssueRegister" OnClick="btnGradeCardIssueRegister_Click" />

                                <asp:Button ID="btnElibilityReport" Text="Eligibility Report" runat="server" TabIndex="1" CssClass="btn btn-info" Visible="false" ValidationGroup="ER" OnClick="btnElibilityReport_Click" />

                                <asp:Button ID="btnCertificate" Text="Degree Certificate" runat="server" TabIndex="1" CssClass="btn btn-info"
                                    Visible="false" ValidationGroup="CPD" OnClick="btnCertificate_Click" />

                                 <asp:Button ID="btnConsolidtedMPHRAM" Text="Consolidated (M.PHARM)" runat="server" TabIndex="32" CssClass="btn btn-info" Visible="false"
                                      ValidationGroup="ConsoGradeCard" OnClick="btnConsolidtedMPHRAM_Click" />
                                
                                <asp:ValidationSummary ID="ValidationSummary16" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="CPD" />
                                <asp:ValidationSummary ID="ValidationSummary15" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ER" />

                                <asp:ValidationSummary ID="vsSum1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ResultExcel" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary" />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="TRReport" />
                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ResultGazette" />
                                <asp:ValidationSummary ID="ValidationSummary7" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="TrExcel" />
                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ResultStatistics" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Tabulation" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="GradeCard" />
                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ConsoGradeCard" />
                                <asp:ValidationSummary ID="ValidationSummary12" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="UFMLIST" />
                                <asp:ValidationSummary ID="ValidationSummary9" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="TRGradeReg" />
                                <asp:ValidationSummary ID="ValidationSummary11" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Consoli" />
                                <asp:ValidationSummary ID="ValidationSummary13" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Ledger" />
                                <asp:ValidationSummary ID="VSProgressionReport" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ProgressionReport" />
                                <asp:ValidationSummary ID="ValidationSummary17" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="GradeCardIssueRegister" />
                                <asp:ValidationSummary ID="valpassingcrt" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="passingCrft" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                                    <%-- <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <h4>Select Student</h4>
                                                    <table id="tblStudent" class="table table-hover table-bordered table-responsive table-striped">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align:center;">Select
                                                                    <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" TabIndex="10" />
                                                                </th>
                                                                <th>USN No. </th>
                                                                <th>Student Name </th>
                                                                <th>Student Email </th>
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
                                                    <td style="text-align:center;">
                                                        <asp:CheckBox ID="chkStudent" runat="server" TabIndex="10" ToolTip="Select to view Tabulation Chart" />
                                                    </td>
                                                    <td><%# Eval("REGNO")%></td>
                                                    <asp:HiddenField ID="HiddenFieldenroll" runat="server" Value='<%# Eval("REGNO") %>' />
                                                    <td>
                                                        <asp:HiddenField ID="hdnadmbatch" runat="server" Value='<%# Eval("ADMBATCH")%>' />
                                                        <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                    </td>
                                                    <td><%# Eval("EMAILID_INS")%></td>
                                                    <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>--%>
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: left;">Select
                                                           <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" ToolTip='<%# Eval("REGNO") %>' Checked="false" />
                                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("REGNO") %>' />

                                                            </th>
                                                            <th>Enrollment No. </th>
                                                            <th>Student Name </th>
                                                            <th>Student Email </th>
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
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkStudent" runat="server" TabIndex="10" ToolTip="Select to view Tabulation Chart" />
                                                </td>
                                                <td><%# Eval("REGNO")%></td>
                                                <asp:HiddenField ID="HiddenFieldenroll" runat="server" Value='<%# Eval("REGNO") %>' />
                                                <td>
                                                    <asp:HiddenField ID="hdnadmbatch" runat="server" Value='<%# Eval("ADMBATCH")%>' />
                                                    <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                </td>
                                                <td><%# Eval("EMAILID_INS")%></td>
                                                <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnGradeCard" />--%>
            <asp:PostBackTrigger ControlID="btnGradeCardHeader" />
            <asp:PostBackTrigger ControlID="btnBranchwise" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnSummary" />
            <asp:PostBackTrigger ControlID="btnGradeCardBackReport" />
            <asp:PostBackTrigger ControlID="btnRankList" />
            <asp:PostBackTrigger ControlID="btntabulation" />
            <asp:PostBackTrigger ControlID="btnResultStatement" />
            <asp:PostBackTrigger ControlID="btnFailStudentList" />
            <asp:PostBackTrigger ControlID="btnCourceWiseFailStudList" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnResultExcel" />
            <asp:PostBackTrigger ControlID="btnStatementMarks" />
            <asp:PostBackTrigger ControlID="btnBacklogExcel" />
            <asp:PostBackTrigger ControlID="btnTRExcel" />
            <asp:PostBackTrigger ControlID="btnGradesheet" />
            <asp:PostBackTrigger ControlID="btnResultStatistics" />
            <asp:PostBackTrigger ControlID="btnConvocationExcelReport" />
            <asp:PostBackTrigger ControlID="btnProgrssionrpt" />
            <asp:PostBackTrigger ControlID="btnExamFeesPaid" />
            <asp:PostBackTrigger ControlID="btnProvisionalDegree" />
            <%--Added By Praful on 05_01_2023--%>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnSRNo" />
            <asp:PostBackTrigger ControlID="btnufm" />
            <asp:PostBackTrigger ControlID="btngraderange" />
            <asp:PostBackTrigger ControlID="btnGradeCardIssueRegister" />
            <asp:PostBackTrigger ControlID="btnElibilityReport" />
            <asp:PostBackTrigger ControlID="btnCertificate" />
            <asp:PostBackTrigger ControlID="btnCIAExcel" />
            <%--Added By Praful on 20_01_2023--%>
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(window).on("load", function () {
            $(document).ready(function () {
                $('.form-control').removeClass('form-control').addClass('form-control redborder redborder2 redborder3 redborder4');
            });
        });
    </script>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function totAll1(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>


    <%--<script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblStudent').DataTable({
                //scrollX: 'true'
                //"pageLength": 10
            });
        }
    </script>--%>

    <script>
        $(document).ready(function () {
            $("body").on("click", ".Selection-box", function () {
                $(".selectrpt").fadeToggle("main");
                $(".nav").click(function () {
                    $(".selectrpt").css("display", "none");
                });

                //$(".selectone").click(function () {

                //    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                //    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                //    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                //    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                //    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");

                //});
                $(".selecttwo").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");

                });
                $(".selectthree").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSession").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_txtDateOfIssue").addClass("red-color");

                });
                $(".selectfour").click(function () {
                    $("#ctl00_ContentPlaceHolder1_txtDateOfIssue").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");
                });
                $(".selectfive").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlSession").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");
                });
                $(".selectsix").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");
                });

                $(".selectseven").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlSession").removeClass("red-color");


                });

                $(".selecteight").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").removeClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").removeClass("red-color");


                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");
                });

                $(".selectnine").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");

                });
                $(".selectten").click(function () {
                    $("#ctl00_ContentPlaceHolder1_ddlYear").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSession").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlSemester").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlStuType").addClass("red-color");

                });
                $(".selecteleven").click(function () {
                    // $("#ctl00_ContentPlaceHolder1_ddlYear").removeClass("red-color");

                    $("#ctl00_ContentPlaceHolder1_ddlYear").addClass("red-color");
                    $("#ctl00_ContentPlaceHolder1_ddlClgname").addClass("red-color");

                });
            });
        });

    </script>

    <%-- <script>
        $(document).ready(function () {
            $(".nav.list-group-item").click(function () {
                //$(".select-clik.borderred").closest('.select2-container--default').find('.select2-selection--single').css("border", "1px solid #f00");
                // $(".select-clik.borderred").closest(".select2-selection--single").css("border", "1px solid #f00");
                $("#ctl00_ContentPlaceHolder1_ddlYear").find('span').attr(".select2-selection--single").css("border", "1px solid #f00");
            });
        });
    </script>--%>
</asp:Content>

