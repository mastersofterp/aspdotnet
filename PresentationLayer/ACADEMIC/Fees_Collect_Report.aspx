<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Fees_Collect_Report.aspx.cs" Inherits="CourseWise_Registration" ViewStateEncryptionMode="Always" EnableViewStateMac="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeeTable"
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

        .checkbox-list-box {
            background-color: var(--main-white);
            border: 1px solid #adadad;
            padding-top: 3px;
            padding-bottom: 0px;
            padding-left: 7px;
            padding-right: 0px;
            min-height: 100%;
            height: 110px;
            overflow: auto;
            overflow-x: hidden;
        }
    </style>
    <asp:UpdatePanel ID="updFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">FEES REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <div id="reportSelection" runat="server" visible="false">
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
                                                <li class=" nav selectone list-group-item-new"><b><a href="#" class="card-link"><i class="fa fa-file"></i>DCR Excel Report</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date </p>
                                                </li>
                                                <li class=" nav selecttwo list-group-item-new"><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Wise DCR Format-II</a></b>
                                                    <p class="nav2 mt-1">No Selction </p>
                                                </li>
                                                <li class=" nav selectthree list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Wise DCR Report</a></b>

                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class="nav selectfour list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Ledger Report</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date </p>
                                                </li>
                                                <li class="nav selectfive  list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Ledger Excel</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date </p>
                                                </li>
                                                <%--  <li class="nav selectsix list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Result Report In Excel</a></b>

                                                <p class="nav2 mt-1">College & Scheme <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Session<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Semester	<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Section </p>
                                            </li>--%>
                                                <li class=" nav selectseven list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Ledger Excel Format II</a></b>

                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date </p>
                                                </li>
                                                <!----------new btn----------->
                                                <li class=" nav selecteight  list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Arrears Report (Excel)</a></b>

                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selectnine list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Arrears Report (PDF)</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selectten list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Student Arrears Headwise Report</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selecteleven list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Balance Report(Excel)</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selecttwelve list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Fee Collection Summery Report</a></b>
                                                     <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selectthirteen list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Excel Report</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                                <li class=" nav selectfourteen list-group-item-new "><b><a href="#" class="card-link"><i class="fa fa-file"></i>Online DCR Excel Report</a></b>
                                                    <p class="nav2 mt-1">From Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>To Date <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>Receipt Type </p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12" style="display:none">
                                        <asp:RadioButtonList ID="rblSelection" runat="server" AutoPostBack="true"
                                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                            <asp:ListItem Value="4" Selected="True">&nbsp;&nbsp;&nbsp;Demand Creation Fees collection&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%--  <asp:ListItem Value="5" >&nbsp;&nbsp;&nbsp;Excess Payment Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                            <%-- <asp:ListItem Value="1" Enable="false">&nbsp;&nbsp;&nbsp;Outstanding Report Semesterwise</asp:ListItem>--%>
                                        </asp:RadioButtonList>

                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">


                                     <div class="form-group col-lg-3 col-md-3 col-3">
                                        <div id="aDIV" runat="server">
                                            <div>
                                                <label>Report</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="form-group col-md-3 col-sm-3 col-3" id="acdyear" runat="server" visible="false">
                                         <div  >
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                             </div>
                                        <%-- <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="report">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-md-3 col-sm-3 col-3" id="fromDSpan" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date" id="txtfromdate">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" /> --%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="TextBox1" runat="server" TabIndex="3" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="TextBox1" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" OnClientDateSelectionChanged="checkDateFrom"/>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                        </div>
                                       <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="TextBox1"
                                            IsValidEmpty="False" EmptyValueMessage="From date is required" InvalidValueMessage="From date is invalid" ForeColor="Red"
                                            InvalidValueBlurredMessage="*" Display="None" ValidationGroup="Show" Enabled="true" />--%>
                                    </div>

                                    <div class="form-group col-md-3 col-sm-3 col-3" id="toDSpan" runat="server" visible="false">

                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date" id="txtToDateReport">
                                            <div class="input-group-addon" id="imgCalToDate">
                                                <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" />--%>
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="TextBox2" runat="server" TabIndex="4" CssClass="form-control" onchange="return checkDate();"/>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="TextBox2" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true"    />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox2"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                                        </div>
                                     <%--   <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate" ControlToValidate="TextBox2"
                                            IsValidEmpty="False" EmptyValueMessage="To date is required" InvalidValueMessage="To date is invalid" ForeColor="Red"
                                            InvalidValueBlurredMessage="*" Display="None" ValidationGroup="Show" />--%>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-3 col-3" id="degreed" runat="server" visible="false">
                                        <div id="Div3" runat="server">
                                            <div class="label-dynamic">
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-3 col-3" id ="branchd" runat="server" visible="false">
                                        <div id="Div4" runat="server">
                                            <div class="label-dynamic">
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                                data-select2-enable="true" TabIndex="6" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="semesterd" runat="server" visible="false">
                                        <asp:Panel ID="PnlFeesCollection" runat="server">

                                            <div id="trSemester" runat="server">
                                                <div class="label-dynamic">
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--   <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                            </div>

                                        </asp:Panel>
                                    </div>

                                    <div id="reciptd" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:Panel ID="pnlList" runat="server">
                                                <asp:ListView ID="lvAdTeacher" runat="server">
                                                    <LayoutTemplate>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkIDNo" runat="server" ToolTip='<%# Eval("RECIEPT_CODE") %>' Text='<%# Eval("RECIEPT_TITLE") %>' data-toggle="tooltip" CssClass="checkbox-list-style" TabIndex="8" /><br />
                                                                <%--<input type="checkbox" id="vehicle1" runat="server" name='<%# Eval("RECIEPT_CODE") %>' value='<%# Eval("RECIEPT_TITLE") %>'>--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-3 col-3" id="yeard" runat="server" visible="false">
                                        <div >
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="9"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-3 col-3" id="Statusd" runat="server" visible="false">
                                        <div >
                                            <div class="label-dynamic">
                                                <label>Admission Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmStatus" runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="10"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%-- <asp:ListItem Value="1">Admitted(Promoted)</asp:ListItem>
                                                <asp:ListItem Value="2">Not Promoted</asp:ListItem>
                                                <asp:ListItem Value="3">Admission Cancelled</asp:ListItem>
                                                <asp:ListItem Value="4">Eligible for T.C ( Final Year student Pass out only )</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-3 col-3" runat="server" id="divPaymentMode" visible="false">
                                        <div id="Div7" runat="server">
                                            <div class="label-dynamic">
                                                <label>Mode of Payment</label>
                                            </div>

                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="11"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                

                                    

                                </div>

                                <asp:Panel ID="PnlSemesterwiseOS" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="Show" onpaste="return false;" EnableViewState="true"
                                                    TabIndex="12" ToolTip="Please Enter Start Date" CssClass="form-control" Style="z-index: 0;" />
                                                <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="dvcal1"  />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="Show" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="Show" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal2" runat="server" class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="Show" TabIndex="13"  EnableViewState="true"
                                                    ToolTip="Please Enter To Date" CssClass="form-control" Style="z-index: 0;" />
                                                <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="dvcal2"/>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="True"
                                                    ErrorMessage="Please Enter To Date" ControlToValidate="txtToDate" Display="None"
                                                    ValidationGroup="Show" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                               InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Show" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer" id="pnlDemand" runat="server" visible="false">
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Show" Visible="false" TabIndex="13"  CssClass="btn btn-primary" OnClick="btnReport_Click1" />
                                <asp:Button ID="btnShow" runat="Server" Text="Show Data" ValidationGroup="Show" OnClick="btnShow_Click" TabIndex="14"
                                    CssClass="btn btn-primary" CommandName="btnShow" Visible="false"/>
                                <asp:Button ID="btnExcel" runat="Server" Text="DCR Excel Report" ValidationGroup="Show" TabIndex="15"
                                    OnClick="btnExcel_Click" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnDcrExcelFormatII" runat="Server" Text="Student Wise DCR Format-II" TabIndex="16"
                                    OnClick="btnDcrExcelFormatII_Click" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnstudexcel" runat="Server" Text=" Student Wise DCR Report" ValidationGroup="Show" TabIndex="17"
                                    OnClick="btnstudexcel_Click" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnstudLedgerReport" runat="server" OnClick="btnstudLedgerReport_Click" Text="Student Ledger Report" TabIndex="18"
                                    ValidationGroup="Show" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnStudentledgerExl" runat="server" OnClick="btnStudentledgerExl_Click" Text="Student Ledger Excel" TabIndex="19"
                                    ValidationGroup="Show" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnledgerExcelFormatII" runat="server" OnClick="btnledgerExcelFormatII_Click" Text="Student Ledger Excel Format II" TabIndex="20"
                                    ValidationGroup="Show" CssClass="btn btn-info" Visible="false" />
                                <%--<asp:Button ID="bntStudentArreasrs" runat="server" OnClick="bntStudentArrears_Click" Text="Student Arrears Report (Excel)" TabIndex="21"
                                    CssClass="btn btn-info" ValidationGroup="Show" Visible="false" />--%>
                                <asp:Button ID="btnStudentArrearPdf" runat="server" OnClick="btnStudentArrearPdf_Click" Text="Student Arrears Report (PDF)" TabIndex="22"
                                    CssClass="btn btn-info" ValidationGroup="Show" Visible="false" />
                                <asp:Button ID="btnStudentArrearsHeadwise" runat="server" OnClick="btnStudentArrearsHeadwise_Click" Text="Student Arrears Headwise Report  " TabIndex="23"
                                    CssClass="btn btn-info" ValidationGroup="Show" Visible="false" />
                                <asp:Button ID="btnBalanceReport" runat="server" OnClick="btnBalanceReport_Click" Text="Balance Report(Excel)" TabIndex="24"
                                    CssClass="btn btn-info" ValidationGroup="Show" Visible="false" />
                                <asp:Button ID="btnSummaryReport" runat="server" Text="Fee Collection Summery Report" OnClick="btnSummaryReport_Click" TabIndex="25"
                                    OnClientClick="return DateValidation();" Visible="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnstud" runat="Server" Text=" Excel Report" ValidationGroup="Show" TabIndex="26"
                                    OnClick="btnstud_Click" CssClass="btn btn-info" Visible="false"/>
                                <asp:Button ID="btnExcelConsolidated" runat="Server" Text="Consolidated Excel Report" ValidationGroup="Show" TabIndex="27"
                                    OnClick="btnExcelConsolidated_Click" CssClass="btn btn-info" Visible="false" />

                                <asp:Button ID="btnOverallOutstandingReport" runat="Server" Text="Overall Outstanding Report(EXCEL)" ValidationGroup="Show" TabIndex="28"
                                    OnClick="btnOverallOutstandingReport_Click" CssClass="btn btn-info" Visible="false" />

                                <asp:Button ID="btnOnlineDcrReport" runat="Server" Text="Online DCR Excel Report" ValidationGroup="Show" TabIndex="29"
                                    OnClick="btnOnlineDcrReport_Click" CssClass="btn btn-info" Visible="false"/>
                                <asp:Button ID="btnexcelPaymentModification" runat="server" Text="Payment Modification Excel Report" TabIndex="30"
                                    CssClass="btn btn-info" OnClick="btnexcelPaymentModification_Click" Visible="false"/>
                                <asp:Button ID="btnTallyIntegration" runat="server" Text="Tally Integration Report" CssClass="btn btn-info" OnClick="btnTallyIntegration_Click" ValidationGroup="Show" TabIndex="31" Visible="false"/>
                                <asp:Button ID="btnCancelrecieptsummary" runat="server" Visible="false" Text=" Canceled receipt summary report" CssClass="btn btn-info" ValidationGroup="Show" TabIndex="32" OnClick="btnCancelrecieptsummary_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="32"/>
                               <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />--%>
                            </div>

                            <div class="col-12 btn-footer" id="pnlSem" runat="server" visible="false">
                                <asp:Button ID="btnShowStud" runat="Server" Text="Show Data" ValidationGroup="Show" OnClick="btnShow_Click"
                                    CssClass="btn btn-primary" CommandName="btnShowStud" />
                                <asp:Button ID="btnOSUptoSemReport" runat="Server" Text="Outstanding - Upto Semester Excel Report" ValidationGroup="Show"
                                    OnClick="btnOSUptoSemReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnFutureOSReport" runat="Server" Text="Outstanding - Future Semester Excel Report" ValidationGroup="Show"
                                    OnClick="btnFutureOSReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:HiddenField ID="hdnRecno" runat="server" />
                                <asp:HiddenField ID="hdnFrfmDate" runat="server" />
                                <asp:HiddenField ID="hdnToDate" runat="server" />
                            </div>

                            <div class="col-12" id="divlvSemester" runat="server" visible="false">
                                <asp:ListView ID="lvSemesterFee" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Fees Details</h5>
                                        </div>
                                        <asp:Panel ID="pnlScroll" runat="server">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSemFees">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>Applied Fees
                                                        </th>
                                                        <th>Paid Fees
                                                        </th>
                                                        <th>Outstanding Fees
                                                        </th>
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
                                                <%# Eval("ENROLLNMENTNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TYPE")%>
                                            </td>
                                            <td>
                                                <%# Eval("APPLIED_FEE")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAID_FEE")%>
                                            </td>
                                            <td>
                                                <%# Eval("BAL_FEE")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12 btn-footer">
                                <%--<asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />--%>

                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </div>
                        </div>

                    </div>
                </div>
            </div> 
        </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnstudexcel" />
            <asp:PostBackTrigger ControlID="btnstud" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnDcrExcelFormatII" />
            <asp:PostBackTrigger ControlID="btnOSUptoSemReport" />
            <asp:PostBackTrigger ControlID="btnFutureOSReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
<%--            <asp:PostBackTrigger ControlID="bntStudentArrears" />--%>
            <asp:PostBackTrigger ControlID="btnStudentledgerExl" />
            <asp:PostBackTrigger ControlID="btnStudentArrearsHeadwise" />
            <asp:PostBackTrigger ControlID="btnBalanceReport" />
            <asp:PostBackTrigger ControlID="btnOnlineDcrReport" />
            <asp:PostBackTrigger ControlID="btnledgerExcelFormatII" />
            <asp:PostBackTrigger ControlID="btnExcelConsolidated" />
            <asp:PostBackTrigger ControlID="btnOverallOutstandingReport" />
            <asp:PostBackTrigger ControlID="btnexcelPaymentModification" />
            <asp:PostBackTrigger ControlID="btnTallyIntegration" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script>
        $(window).on("load", function () {
            $(document).ready(function () {
                $('.form-control').removeClass('form-control').addClass('form-control redborder redborder2 redborder3 redborder4');
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

                $(".selectone").click(function () {

                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");


                });
                $(".selecttwo").click(function () {
                    $("#txtfromdate").removeClass("red-color");
                    $("#txtToDateReport").removeClass("red-color");

                    $(".checkbox-list-box").removeClass("red-color");

                });
                $(".selectthree").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");


                });
                $(".selectfour").click(function () {
                    $(".checkbox-list-box").removeClass("red-color");

                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");


                });
                $(".selectfive").click(function () {
                    $(".checkbox-list-box").removeClass("red-color");

                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                });

                $(".selectsix").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                });

                $(".selectseven").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");

                    $(".checkbox-list-box").removeClass("red-color");

                });

                $(".selecteight").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });

                $(".selectnine").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });
                $(".selectten").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });
                $(".selecteleven").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });
                $(".selecttwelve").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                 
                });
                $(".selectthirteen").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });
                $(".selectfourteen").click(function () {
                    $("#txtfromdate").addClass("red-color");
                    $("#txtToDateReport").addClass("red-color");
                    $(".checkbox-list-box").addClass("red-color");

                });
            });
        });

    </script>
    <script language="javascript" type="text/javascript">
        function getVal() {
            debugger;
            if (Page_ClientValidate()) {
                var array = []
                var degreeNo = "";

                var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

                for (var i = 0; i < checkboxes.length; i++) {
                    if (degreeNo == undefined) {
                        degreeNo = checkboxes[i].value + ',';
                    }
                    else {

                        $('#<%= hdnRecno.ClientID %>').val(degreeNo);
                    }
                }
                if (degreeNo == "") {
                    alert("Please Select At least One Receipt Type !")
                    return false;
                }

            } else {
                // do something
            }
            //document.getElementById(inpHide).value = "degreeNo";
        }
        function accordion() {
            debugger;
            if ($('input[type=radio]:checked').length > 0) {
                var selval = $('input[type=radio]:checked').val();
                onreport();
                if (selval == "4") {
                    $("#<%=PnlSemesterwiseOS.ClientID%>").hide();
                    $("#<%=divfromdate.ClientID%>").hide();
                    $("#<%=divtodate.ClientID%>").hide();
                    $("#<%=PnlFeesCollection.ClientID%>").show();

                    $("#<%=PnlFeesCollection.ClientID%>").show();
                    $("#<%=PnlFeesCollection.ClientID%>").show();
                    $("#<%=PnlFeesCollection.ClientID%>").show();


                    $("#<%=btnOSUptoSemReport.ClientID%>").hide();
                    $("#<%=btnFutureOSReport.ClientID%>").hide();
                    $("#<%=btnExcel.ClientID%>").show();

                }
                if (selval == "1") {
                    $("#<%=PnlSemesterwiseOS.ClientID%>").show();
                    $("#<%=divfromdate.ClientID%>").show();
                    $("#<%=divtodate.ClientID%>").show();
                    $("#<%=PnlFeesCollection.ClientID%>").hide();

                    $("#<%=btnOSUptoSemReport.ClientID%>").show();
                    $("#<%=btnFutureOSReport.ClientID%>").show();
                    $("#<%=btnExcel.ClientID%>").hide();
                }
            }
        }
        function onreport() {
            debugger;
            var a = document.getElementById("ctl00_ContentPlaceHolder1_rblSelection_0");
            var b = document.getElementById("ctl00_ContentPlaceHolder1_rblSelection_1");
            if (a.checked) {
                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = true;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvFromDate").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvToDate").enabled = false;
            }
            if (b.checked) {

                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvFromDate").enabled = true;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvToDate").enabled = true;
            }
        }

        function DateValidation() {
            var datefrom = $("[id*=ctl00_ContentPlaceHolder1_TextBox1]").val();
            var dateto = $("[id*=ctl00_ContentPlaceHolder1_TextBox2]").val();
            if (datefrom == "" || datefrom == null) {
                alert('Please enter From Date.');
                return false;
            }
            else if (dateto == "" || dateto == null) {
                alert('Please enter To Date.');
                return false;
            }
            else
                return true;
        }
    </script>

        <script type="text/javascript">
            //function checkDate()
            //{
            //    alert(1);
            //    //debugger;
            //    var datefrom = $("[id*=ctl00_ContentPlaceHolder1_TextBox1]").val();
            //    var dateTo = $("[id*=ctl00_ContentPlaceHolder1_TextBox2]").val();
            //    var fromdate = new Date(datefrom)
            //    var todate = new Date(dateTo)
            //    //alert(datefrom);
            //    //alert(dateTo);
            //    alert(fromdate);
            //    alert(todate);

            //    // I change the < operator to >
            //    if (dateTo < datefrom)
            //    {
            //        alert("To Date should be greater than From Date");
            //        //sender._selectedDate = new Date();
            //        // set the date back to the current date
            //        dateTo.value('');
            //    }

            //}

            function checkDate(){
                var datefrom = $("[id*=ctl00_ContentPlaceHolder1_TextBox1]").val();
                var dateTo = $("[id*=ctl00_ContentPlaceHolder1_TextBox2]").val();
                var fromdate = new Date(datefrom)
                var todate = new Date(dateTo)               
                var dd = String(fromdate.getDate()).padStart(2, '0');
                var mm = String(fromdate.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = fromdate.getFullYear();
                var selDate = dateTo.split("/");
                var selday = selDate[0];
                var selmonth = selDate[1] - 1;
                var selyear = selDate[2];
                var SDate = new Date(selyear, selmonth, selday);



                var ddFrom = String(todate.getDate()).padStart(2, '0');
                var mmFrom = String(todate.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyyFrom = todate.getFullYear();
                var FromDate = datefrom.split("/");
                var Fromday = FromDate[0];
                var Frommonth = FromDate[1] - 1;
                var Fromyear = FromDate[2];
                var FDate = new Date(Fromyear, Frommonth, Fromday);



                if (SDate < FDate) {
                    alert('To Date Should Be Greater Than From Date.');
                    document.getElementById('<%=TextBox2.ClientID%>').value = "";
                    return true;
                }
                else {
                    return false;
                }
            }
           
    </script>

      <script type="text/javascript">
          function checkDateFrom(sender, args) {
              // I change the < operator to >
              if (sender._selectedDate > new Date()) {
                  alert("Unable to select future date !!!");
                  sender._selectedDate = new Date();
                  // set the date back to the current date
                  sender._textbox.set_Value('')
              }
          }
    </script>
</asp:Content>
