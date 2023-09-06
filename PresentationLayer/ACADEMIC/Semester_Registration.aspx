<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Semester_Registration.aspx.cs" Inherits="Semester_Registration" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .std-info {
            background: #fff;
            box-shadow: rgb(0 0 0 / 20%) 0px 2px 10px;
            border-radius: 5px;
            margin-bottom: 8px;
        }

        .std-dtl.std-info {
            box-shadow: rgb(0 0 0 / 20%) 0px 2px 5px;
        }

        .profile-user .list-group-item {
            border: 0px solid rgba(0,0,0,.125);
        }

        .profile-user.list-group-unbordered > .list-group-item {
            padding: 0.2rem 0.6rem 0.2rem 0.6rem;
        }

        .profile-user.list-group .list-group-item .sub-label {
            float: initial;
        }

        /*.profile-user {
            border-right: 2px solid #ccc;
        }*/
        .gray_quest {
            padding-inline-start: 10px;
        }

            .gray_quest li {
                padding-bottom: 10px;
            }
    </style>

    <!--v-->
    <!-- ===== chat css START ===== -->
    <style>
        .pay-opt {
            display: flex;
        }

        @media (max-width:767px) {
            .pay-opt {
                display: inline-block;
            }
        }

        .find_parent {
            padding: 23px 5px 0px 5px;
        }

        .send_btn {
            display: block;
            position: relative;
            /*transform: translateY(-42%);
            z-index: 999;
            cursor: pointer;*/
        }

            .send_btn i {
                position: absolute;
                bottom: 26px;
                right: 12px;
            }

            .send_btn .Button_Send {
                font-size: 16px;
                opacity: .6;
                width: 15px;
                height: 15px;
                color: #3080f7;
            }

                .send_btn .Button_Send:hover {
                    opacity: 1;
                }

        .dynamic_textarea {
            -webkit-transition: height 0.2s ease;
            -moz-transition: height 0.2s ease;
            transition: height 0.2s ease;
            width: 100%;
        }
    </style>

    <style>
        .user-adminChat {
            height: 300px;
            overflow-y: scroll;
            border: 2px solid #e5e7e8;
            padding: 10px;
            border-radius: 5px;
        }

        .userText {
            font-size: 14px;
            text-align: left;
            line-height: 15px;
            margin: 5px;
        }

            .userText .userTextSpan {
                /*border: 1px solid #98e65e;
                border: 1px solid #55c700;
                background: #00c292 !important;
                color: #fff;*/
                background: rgb(255, 255, 255);
                color: rgb(0, 0, 0);
                margin: 0;
                padding: 12px 16px;
                width: auto;
                max-width: 380px;
                position: relative;
                display: inline-block;
                word-break: break-word;
                line-height: 1.5;
                hyphens: auto;
                text-align: left;
                border-radius: 20px;
                border-bottom-left-radius: 1px;
                margin-top: 3px;
            }

        /*.userTextSpan::after {
            bottom: 100%;
            right: 7%;
            border: solid transparent;
            content: " ";
            height: 0;
            width: 0;
            position: absolute;
            pointer-events: none;
            border-bottom-color: #00c292;
            border-width: 7px;
            margin-left: -10px;
        }*/

        .user-time {
            color: #686a70 !important;
            font-size: 12px;
            margin-right: 6px;
        }

        .user-name {
            color: #326b99 !important;
            display: none;
        }

        .user-dot {
            font-size: 10px;
            color: #00c292;
        }

        .adminText p, .userText p {
            margin: 0 0 0px;
        }

        .adminText {
            margin: 5px 0px;
            text-align: right;
        }

            .adminText .userTextSpan {
                border: 1px solid #3080f7;
                background: #3080f7 !important;
                color: #fff;
                margin: 0;
                padding: 12px 16px;
                width: auto;
                max-width: 280px;
                position: relative;
                display: inline-block;
                word-break: break-word;
                line-height: 1.5;
                hyphens: auto;
                text-align: left;
                border-radius: 20px;
                border-bottom-right-radius: 1px;
                margin-top: 3px;
            }

        /*.adminText .userTextSpan::after {
                    bottom: 100%;
                    left: 20px;
                    border: solid transparent;
                    content: " ";
                    height: 0;
                    width: 0;
                    position: absolute;
                    pointer-events: none;
                    border-bottom-color: #48bdf9;
                    border-width: 7px;
                    margin-left: -10px;
                }*/

        .admin-time {
            color: #686a70 !important;
            font-size: 10px;
            margin-right: 0px;
        }

        .admin-name {
            color: #326b99 !important;
            font-size: 11px;
            display: none;
        }

        .admin-dot {
            color: #48bdf9;
            font-size: 10px;
            margin-right: 5px;
        }

        .hall-tickt {
            position: absolute;
            right: 0%;
        }

            .hall-tickt a {
                padding-right: 15px !important;
                border-bottom-right-radius: 0;
                border-top-right-radius: 0;
                margin-right: 0px !important;
            }

        .print-butn {
            position: absolute;
            right: 0%;
            top: 15%;
        }

            .print-butn a {
                padding-right: 15px !important;
                border-bottom-right-radius: 0;
                border-top-right-radius: 0;
                margin-right: 0px !important;
            }

        @media (min-width:767px) and (max-width:992px) {
            .print-butn {
                top: 17%;
                right: 0%;
            }

            .hall-tickt {
                top: 12%;
                right: 0%;
            }
        }

        @media (max-width:767px) {
            ctl00_StdentDiv {
                position: relative;
            }

            .hall-tickt {
                position: absolute;
                right: 0%;
                top: 24%;
            }

            .print-butn {
                position: absolute;
                right: 0%;
                top: 30%;
            }

            .progress {
                margin-top: -5px;
                margin-left: 25%;
            }
        }
    </style>

    <%-- <style>
        .u-btn {
            all: unset;
            cursor: pointer;
        }

        .chatbox {
            border-top-right-radius: 0.5em;
            border-top-left-radius: 0.5em;
            bottom: 0;
            position: fixed;
            right: 0em;
            bottom: 0.2rem;
            transform: translatey(35em);
            transition: all 400ms ease;
            width: 5em;
            z-index: 9999;
        }

        .chatbox--is-visible {
            transform: translatey(0);
            width: 34em;
            box-shadow: 0px 4px 16px rgb(0 0 0 / 30%);
            right: 0.5em;
            bottom: 0rem;
        }

            .chatbox--is-visible .chatbox__header {
                background: #fff;
                border-top-right-radius: 0.5em;
                border-top-left-radius: 0.5em;
                display: flex;
                justify-content: space-between;
                padding: 0.75em 1em;
                user-select: none;
            }

        .chatbox__header-cta-text {
            color: #212529;
            font-weight: 500;
            font-size: 1.4rem;
        }

        .head-chat {
            display: none;
        }

        .chatbox--is-visible .head-chat {
            display: inline-block;
        }

        .chatbox__header-cta-icon {
            color: #3080f7;
            font-size: 36px;
            margin-right: 0.5em;
        }

        .chatbox__header-cta-btn {
            display: none;
        }

        .chatbox--is-visible .chatbox__header-cta-btn {
            background: none;
            border: none;
            color: #aaa;
            padding: 0.5em;
            font-size: 22px;
            transition: all 300ms ease;
            display: inline-block;
        }

        .chatbox__display {
            background: #eaeef3;
            height: 30em;
            overflow: auto;
            padding: 0.75em;
        }

        .chatbox__display-chat {
            background: #fff;
            border-radius: 0.5em;
            color: #666;
            font-weight: 300;
            font-size: 0.9rem;
            line-height: 1.5;
            padding: 0.75em;
            text-align: justify;
        }

        .chatbox__form {
            display: flex;
        }

        .chatbox__form-input {
            border: none;
            color: #222;
            font-size: 0.9rem;
            font-weight: 300;
            padding: 1em 1em;
            width: 100%;
        }

            .chatbox__form-input:required {
                box-shadow: none;
            }

        .chatbox__form-submit {
            background: none;
            border: none;
            color: #aaa;
            /*padding: 1em;*/
        }

        @media (max-width:576px) {
            .chatbox {
                right: -1em;
            }

            .chatbox--is-visible {
                transform: translatey(0);
                width: 25em;
                box-shadow: 0px 4px 16px rgb(0 0 0 / 30%);
                right: 0.5em;
                bottom: 0rem;
            }

            .chatbox__header-cta-icon {
                color: #3080f7;
                font-size: 36px;
                margin-right: 0em;
            }

            .chatbox--is-visible .chatbox__header-cta-icon {
                margin-right: 0.5em;
            }
        }
    </style>--%>
    <!-- ===== chat css END ===== -->
    <asp:Panel runat="server" ID="pnlMain">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"><%--Semester Registration--%>
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12 mt-2 std-info std-dtl pt-2 pb-2">
                            <div class="row">
                                <div class="col-lg-10 col-md-10 col-12" id="divstuddetails" runat="server">
                                    <ul class="list-group list-group-unbordered profile-user">
                                        <li class="list-group-item"><b>Registration No :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudentID" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Program :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDegree" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Scheme :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudentCampus" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Current Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblYear" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Semester Admission Status:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSAStatus" Font-Bold="true" runat="server"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item" id="divnewnote" runat="server" visible="false">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p style="color: red">
                                                    <i class="fa fa-star" aria-hidden="true"></i><span><b>Your admission Process IS NOT COMPLETE unless you select your elective in the next window.</b>
                                                    </span>
                                                </p>
                                            </div>
                                        </li>
                                        <asp:Panel ID="pnlinstallmentdetails" runat="server" Visible="false">




                                            <li class="list-group-item" style="margin-left: -10px;"><b>Total Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalAmountinst" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <%--<li class="list-group-item"><b>E-Mail ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>--%>
                                            <li class="list-group-item" style="margin-left: -10px;"><b>Total Installment :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalInstallment" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>

                                        </asp:Panel>

                                    </ul>
                                </div>

                                <div class="col-lg-2 col-md-2 col-12 mt-2 text-center">
                                    <asp:ListView ID="lvSpecializationGroup" runat="server" Visible="false">
                                        <LayoutTemplate>

                                            <div class="sub-heading">
                                                <h5>Specialization Group Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%" id="tblHead">
                                                <thead class="bg-light-blue">
                                                    <tr class="header">
                                                        <th>Sr No.</th>
                                                        <th>Specialization Group
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
                                                    <asp:Label ID="lblsrnumb" runat="server" Text='<%# Container.DataItemIndex+1%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("GROUP_NAME") %> 
                                                    
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="divregfor" runat="server">
                            <div class="row">
                                <div class="sub-heading pt-3">
                                    <h5>I wish to Register for</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-4 col-12" id="divregisterfor" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Provisional Semester</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-4 col-12" id="divgroups" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%-- <label>Select Specialization Groups(Any 2)</label>--%>
                                        <asp:Label Font-Bold="True" ID="lblSpecializationGroup" runat="server"></asp:Label>
                                    </div>
                                    <asp:ListBox ID="ddlgroups" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-4 col-12 btn-footer" id="dvSemAdmWithoutPayment" runat="server">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:Button ID="btnSemAdmWithoutPayment" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SemAdmWithoutPayment_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="form-group col-lg-12 col-md-12 col-12" id="divnote" runat="server">
                            <div class=" note-div">
                                <h5 class="heading">Note</h5>
                                <p>
                                    <i class="fa fa-star" aria-hidden="true"></i><span>We have received your Semester Registration Payment Request and it is submitted to the Institute Finance Department for the Approval.
                                            Once it is approved, you will be notified.
                                    </span>
                                </p>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="row">
                                <asp:Panel ID="pnlfeedetails" runat="server">
                                    <div class="form-group col-lg-12 col-md-12 col-12 pl-lg-0" id="feedetails" runat="server">
                                        <asp:ListView ID="lvfeehead" runat="server">
                                            <LayoutTemplate>

                                                <div class="sub-heading">
                                                    <h5>Fees Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%" id="tblHead">
                                                    <thead class="bg-light-blue">
                                                        <tr class="header">
                                                            <th>Sr No.</th>
                                                            <th>Fees Head
                                                            </th>
                                                            <th style="text-align: right">Amount
                                                            </th>
                                                            <%--<th >
                                             <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                        </th>--%>
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
                                                        <asp:Label ID="lblsrnumb" runat="server" Text='<%# Container.DataItemIndex+1%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FEE_LONGNAME") %> 
                                                    
                                                    </td>
                                                    <td style="text-align: right">
                                                        <%# Eval("SEMESTER") %>
                                                    </td>
                                                    <%-- <td><%# Eval("Total") %></td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <div id="divamount" runat="server">
                                            <table id="tblamt" class="table table-striped table-bordered nowrap" style="width: 70%">
                                                <tr style="background-color: #eee !important;">
                                                    <th style="text-align: center">
                                                        <asp:Label ID="lbltotals" runat="server" Style="font-weight: bold;" Text="Total :"></asp:Label>
                                                    </th>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbltotalamount" runat="server" Enabled="false" Style="font-weight: bold; text-align: right;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="mt-4">
                                            <%--<div class="sub-heading">
                                            <h5>Fees Details</h5>
                                        </div>--%>
                                            <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%" id="tablefeedetails" visible="false" hidden runat="server">
                                                <tbody>
                                                    <tr>
                                                        <th>NEFT/RTGS,Cheque & DD for students fees collection
                                                        </th>
                                                        <th>
                                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal_feesdetails" runat="server">
                                                                View Details
                                                            </button>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>Grayquest Loan Facility
                                                        </th>
                                                        <th>
                                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal_loan" runat="server">
                                                                View Details
                                                            </button>
                                                        </th>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                        <br />
                        <div class="form-group col-lg-12 col-md-12 col-12" id="divsemregwithoutpayment" runat="server" visible="false" style="text-align: center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Click Here for Semester Registartion" CssClass="btn btn-primary" OnClick="btnsubmit_Click" Visible="false" />
                        </div>



                        <%-- <div class="form-group col-lg-8 col-md-8 col-12" id="DivSpecialization" runat="server">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="pay_method">
                                    <div class="sub-heading">
                                        <h5>Specialization Details</h5>
                                    </div>
                                    <div class="form-group col-lg-8 col-md-8 col-12">
                                        <asp:ListBox ID="ddlgroups" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlgroups_SelectedIndexChanged"></asp:ListBox>

                                    </div>
                                </div>
                                </asp:Panel>
                        </div>--%>


                        <%-- <div class="col-12">
                                    <asp:Panel ID="pnlSpecGroup" runat="server" Visible="true">
                                        <%-- <asp:ListView ID="LvSpecalCourse" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Specialization Group Courses</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select Course</th>
                                                            <th>Sr.No.</th>
                                                            <th>Course Name. 
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
                                                        <asp:CheckBox ID="chkadm" runat="server" />
                                                    </td>
                                                    <td>
                                                        <%# Container.DataItemIndex +1 %>
                                                    </td>
                                                    <td id="instadate" runat="server">
                                                        <asp:Label ID="lblDate" runat="server" Text="Test 1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>--%>
                        <%-- <div id="divSpeccourse" runat="server" visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select Courses</th>
                                                        <th>Sr.No</th>
                                                        <th>Course Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkadm" runat="server" />
                                                        </td>
                                                        <td>1 </td>
                                                        <td>C++ </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                        </td>
                                                        <td>2 </td>
                                                        <td>Python </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>--%>







                        <div class="col-12">
                            <asp:Panel ID="pnlInstallment" runat="server" Visible="true">
                                <asp:ListView ID="lvInstallment" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Installment Fees Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.</th>
                                                    <th>Installment No. 
                                                    </th>

                                                    <th>Due Date
                                                    </th>
                                                    <th>Installment Amount
                                                    </th>
                                                    <th>Late Fee + Re-Enrollment Fee
                                                    </th>
                                                    <th>Net Payable Fee
                                                    </th>
                                                    <th hidden>Paid Status</th>
                                                    <%--<th>Approved Status</th>--%>
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
                                                <%# Container.DataItemIndex +1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Eval("INSTALMENT_NO") %>'></asp:Label>
                                                <asp:HiddenField ID="hdfIdno" Value='<%#Eval("IDNO")%>' runat="server" />
                                            </td>
                                            <%--<td style="text-align: center">
                                                        <%# Eval("COLLEGE_CODE") %>
                                                    </td>--%>
                                            <td id="instadate" runat="server">
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DUE_DATE") %>'></asp:Label>
                                            </td>
                                            <td id="instamount" runat="server">
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("INSTALL_AMOUNT") %>'></asp:Label>
                                            </td>
                                            <td id="Td1" runat="server">
                                                <asp:Label ID="lblLateFee" runat="server" Text='<%# Eval("LATEFEE") %>'></asp:Label>
                                            </td>
                                            <td id="Td2" runat="server">
                                                <asp:Label ID="lblNetPayable" runat="server" Text='<%# Eval("TOTAL_DUE_AMOUNT") %>'></asp:Label>
                                            </td>
                                            <td style="color: red" hidden>
                                                <asp:Label ID="lblpaidstatus" runat="server" Text='<%# Eval("PAID_STATUS") %>'></asp:Label>
                                                <%-- <asp:Label ID="lblStatus" ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                    Text='<%# (Convert.ToBoolean(Eval("RECON") )== true ?  "Paid" : "Not Paid" )%>' runat="server"></asp:Label>--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnldiscountScholarship" runat="server" Visible="true">
                                <asp:ListView ID="lvDiscountScholarship" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Discount/Concession/Scholarship Fees Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.</th>
                                                    <th>Fees Description 
                                                    </th>
                                                    <th></th>
                                                    <th>Amount
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
                                                <%# Container.DataItemIndex +1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblapplicablefee" runat="server" Text='<%# Eval("HEAD") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("OPERATOR") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbldiscount" runat="server" Text='<%# Eval("ADJUSTMENT_AMOUNT") %>'></asp:Label>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <%--<div class="col-12">
                            <asp:Panel ID="pnldiscount" runat="server" Visible="true">
                                <asp:ListView ID="lvDiscount" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Discount/Concession Fees Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.</th>
                                                    <th>Applicable Fee 
                                                    </th>

                                                    <th>Discount Fee
                                                    </th>
                                                    <th>Net Payable Fee
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
                                                <%# Container.DataItemIndex +1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblapplicablefee" runat="server" Text='<%# Eval("APPLICATBLE_FEE") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldiscount" runat="server" Text='<%# Eval("DISCOUNT_FEES") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblnetpayable" runat="server" Text='<%# Eval("NET_PAYABLE") %>'></asp:Label>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>--%>


                        <%-- <div class="col-12">
                            <asp:Panel ID="pnlScholarship" runat="server" Visible="true">
                                <asp:ListView ID="lvScholarship" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Scholarship Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.</th>
                                                    <th>Total Fee 
                                                    </th>

                                                    <th>Scholarship Allotment Fee
                                                    </th>
                                                    <th>Net Payable Fee
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
                                                <%# Container.DataItemIndex +1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblapplicablefee" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldiscount" runat="server" Text='<%# Eval("SCHOLARSHIP_ALLOTMENT_AMOUNT") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblnetpayable" runat="server" Text='<%# Eval("PAYABLE_AMT") %>'></asp:Label>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>--%>

                        <br />

                        <div class="form-group col-lg-7 col-md-7 col-12" id="divpayment" runat="server">
                            <asp:Panel ID="pnlpaymentdetails" runat="server">
                                <div class="pay_method">
                                    <div class="sub-heading">
                                        <h5>Payment Method</h5>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <%--<asp:RadioButtonList ID="rdopayment" runat="server"  RepeatDirection="Horizontal" OnSelectedIndexChanged="rdopayment_SelectedIndexChanged" AutoPostBack="true">
                                                      <asp:ListItem Value="1"> Online Payment &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="2"> Offline Paymen t </asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                        <asp:RadioButton ID="rdonpayment" runat="server" Text="Online Payment" RepeatDirection="Horizontal" OnCheckedChanged="rdonpayment_CheckedChanged" AutoPostBack="true" GroupName="rdopayment" />
                                        <asp:RadioButton ID="rdoffpayment" runat="server" Text="Offline Payment" RepeatDirection="Horizontal" OnCheckedChanged="rdoffpayment_CheckedChanged" AutoPostBack="true" GroupName="rdopayment" />
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:Button ID="btnPayment" runat="server" Text="Pay Online" CssClass="btn btn-primary " ToolTip="Click to Payment." OnClick="btnPayment_Click" />

                                            <div id="dvmode" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Payment Mode</label>
                                                </div>
                                                <asp:DropDownList ID="ddlmode" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--<asp:ListItem Value="1">DD</asp:ListItem>--%>
                                                    <%--<asp:ListItem Value="2">Cheque</asp:ListItem>--%>
                                                    <%--<asp:ListItem Value="3">NEFT/RTGS</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="col-12" id="dd_details" runat="server">
                                            <div class="std-info pl-3 pr-3 pb-3">
                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>DD Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Bank Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlbank" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvbankdd" runat="server" ControlToValidate="ddlbank" Display="None"
                                                            ErrorMessage="Please Select Bank Name" ValidationGroup="submitdd" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtddbranch" runat="server" CssClass="form-control" MaxLength="30" />
                                                        <asp:RequiredFieldValidator ID="rfvddbranch" runat="server" ControlToValidate="txtddbranch" Display="None"
                                                            ErrorMessage="Please Enter Branch Name" ValidationGroup="submitdd" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtddbranch" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="0,1,2,3,4,5,6,7,8,9,~`!@#$%^*()_+=,./:;<>?'{}[]\|-&;'" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>DD Number </label>
                                                        </div>
                                                        <asp:TextBox ID="txtddno" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" MaxLength="6" />
                                                        <asp:RequiredFieldValidator ID="rfvddno" runat="server" ControlToValidate="txtddno" Display="None"
                                                            ErrorMessage="Please Enter DD Number" ValidationGroup="submitdd" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Total Amount </label>
                                                        </div>
                                                        <%-- <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Placeholder="383,500.00" Enabled="false"/>--%>
                                                        <asp:TextBox ID="txttotdd" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event,this)" />
                                                        <asp:RequiredFieldValidator ID="rfvtotdd" runat="server" ControlToValidate="txttotdd" Display="None"
                                                            ErrorMessage="Please Enter Total Amount" ValidationGroup="ys" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Issue</label>
                                                        </div>

                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtdatedd" runat="server"
                                                                CssClass="form-control" Style="z-index: 0;" OnTextChanged="txtdatedd_TextChanged" AutoPostBack="true" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatedd"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="txtdateddnew" runat="server" TargetControlID="txtdatedd"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="txtdateddnew"
                                                                ControlToValidate="txtdatedd" IsValidEmpty="False" EmptyValueMessage="Select Date Of Issue"
                                                                InvalidValueMessage="Date Of Issue is invalid"
                                                                Display="Dynamic" ValidationGroup="submitdd" />
                                                        </div>
                                                    </div>





                                                    <div class="form-group col-lg-4 col-md-4 col-12" id="ddinstallment" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Select Installment</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlinstallment" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlinstallment_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%-- <asp:ListItem Value="1">Installment-1-due date-10-10-2022-Amount-6000</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlinstallment" Display="None"
                                                            ErrorMessage="Please Select Installment" ValidationGroup="submitdd" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>


                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Button1_Click" ValidationGroup="submitdd" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submitdd" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Details For DD For Students Fees Collection</h5>
                                                        </div>
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Cheque  & DD should be drawn in favour of :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDDAccountName" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBankName" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>DD payable at/Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDDpayableat" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12" id="cheque_details" runat="server">
                                            <div class="std-info pl-3 pr-3 pb-3">
                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Cheque Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Bank Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlcheque" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvcheque" runat="server" ControlToValidate="ddlcheque" Display="None"
                                                            ErrorMessage="Please Select Bank Name" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtbranchcheque" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvbranchcheue" runat="server" ControlToValidate="txtbranchcheque" Display="None"
                                                            ErrorMessage="Please Enter Branch Name" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtbranchcheque" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="0,1,2,3,4,5,6,7,8,9,~`!@#$%^*()_+=,./:;<>?'{}[]\|-&;'" />

                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Cheque Number </label>
                                                        </div>
                                                        <asp:TextBox ID="txtchno" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" MaxLength="6" />
                                                        <asp:RequiredFieldValidator ID="rfvchno" runat="server" ControlToValidate="txtchno" Display="None"
                                                            ErrorMessage="Please Enter Cheque Number" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Total Amount </label>
                                                        </div>
                                                        <asp:TextBox ID="txttotalchq" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event,this)" />
                                                        <asp:RequiredFieldValidator ID="rfvchqtot" runat="server" ControlToValidate="txttotalchq" Display="None"
                                                            ErrorMessage="Please Enter Total Amount" ValidationGroup="gg" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <%--    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Issue</label>
                                                        </div>
                                                        <asp:TextBox ID="txtdatechq" runat="server" CssClass="form-control" type="date" OnTextChanged="txtdatechq_TextChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="rfvdatechq" runat="server" ControlToValidate="txtdatechq" Display="None"
                                                            ErrorMessage="Please Select Date of Issue" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>

                                                    </div>--%>



                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Issue</label>
                                                        </div>

                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <%--<asp:TextBox ID="txtdatechq" runat="server" CssClass="form-control" type="date" OnTextChanged="txtdatechq_TextChanged" AutoPostBack="true" />--%>
                                                            <asp:TextBox ID="txtdatechq" runat="server"
                                                                CssClass="form-control" Style="z-index: 0;" OnTextChanged="txtdatechq_TextChanged" AutoPostBack="true" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatechq"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="cheque" runat="server" TargetControlID="txtdatechq"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="cheque"
                                                                ControlToValidate="txtdatechq" IsValidEmpty="False" EmptyValueMessage="Select Date Of Issue"
                                                                InvalidValueMessage="Date Of Issue is invalid"
                                                                Display="Dynamic" ValidationGroup="submitchq" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvdatechq" runat="server" ControlToValidate="txtdatechq" Display="None"
                                                            ErrorMessage="Please Select Date of Issue" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>



                                                    <div class="form-group col-lg-4 col-md-4 col-12" id="chequeinstallment" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Select Installment</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlchequeinstallment" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlchequeinstallment_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%-- <asp:ListItem Value="1">Installment-1-due date-10-10-2022-Amount-6000</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlchequeinstallment" Display="None"
                                                            ErrorMessage="Please Select Installment" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Button2_Click" ValidationGroup="submitchq" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submitchq" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Details For Cheque For Students Fees Collection</h5>
                                                        </div>
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Cheque should be drawn in favour of :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChequeInfavOf" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChequeBankName" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Cheque payable at/Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChequepayableat" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12" id="NEFT_RTGS_details" runat="server">
                                            <div class="std-info pl-3 pr-3 pb-3">
                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>NEFT/RTGS Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Bank Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlbanknft" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvbanknft" runat="server" ControlToValidate="ddlbanknft" Display="None"
                                                            ErrorMessage="Please Select Bank Name" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtbranchnft" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvbranchnft" runat="server" ControlToValidate="txtbranchnft" Display="None"
                                                            ErrorMessage="Please Enter Branch Name" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtbranchnft" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="0,1,2,3,4,5,6,7,8,9,~`!@#$%^*()_+=,./:;<>?'{}[]\|-&;'" />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>UTR No</label>
                                                        </div>
                                                        <asp:TextBox ID="txttransid" runat="server" CssClass="form-control" MaxLength="30" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txttransid" FilterType="Custom" FilterMode="InvalidChars" InvalidChars=",~`!@#$%^*()_+=,./:;<>?'{}[]\|-&;'" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvtrasid" runat="server" ControlToValidate="txttransid" Display="None"
                                                                    ErrorMessage="Please Enter Transaction Id" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="">
                                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Total Amount </label>
                                                        </div>
                                                        <asp:TextBox ID="txttotalnft" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event,this)" />
                                                        <asp:RequiredFieldValidator ID="rfvtotalnft" runat="server" ControlToValidate="txttotalnft" Display="None"
                                                            ErrorMessage="Please Enter Total Amount" ValidationGroup="hh" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <%--<div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Payment</label>
                                                        </div>
                                                        <asp:TextBox ID="txtdatepaynft" runat="server" CssClass="form-control" type="date" OnTextChanged="txtdatepaynft_TextChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="rfvdatenft" runat="server" ControlToValidate="txtdatepaynft" Display="None"
                                                            ErrorMessage="Please Select Date of Payment" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </div>--%>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Payment</label>
                                                        </div>

                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <%--<asp:TextBox ID="txtdatechq" runat="server" CssClass="form-control" type="date" OnTextChanged="txtdatechq_TextChanged" AutoPostBack="true" />--%>
                                                            <asp:TextBox ID="txtdatepaynft" runat="server" TabIndex="4"
                                                                CssClass="form-control" Style="z-index: 0;" OnTextChanged="txtdatepaynft_TextChanged" AutoPostBack="true" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatepaynft"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="neft" runat="server" TargetControlID="txtdatepaynft"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="neft"
                                                                ControlToValidate="txtdatepaynft" IsValidEmpty="False" EmptyValueMessage="Select Date Of Payment"
                                                                InvalidValueMessage="Date Of Payment is invalid"
                                                                Display="Dynamic" ValidationGroup="submitnft" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvdatechq" runat="server" ControlToValidate="txtdatechq" Display="None"
                                                            ErrorMessage="Please Select Date of Issue" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-4 col-12" id="neftinstallment" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Select Installment</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlneftinstallment" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlneftinstallment_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%-- <asp:ListItem Value="1">Installment-1-due date-10-10-2022-Amount-6000</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlneftinstallment" Display="None"
                                                            ErrorMessage="Please Select Installment" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Upload Payment Slip</label>
                                                        </div>
                                                        <asp:FileUpload ID="Fuslip" runat="server" />
                                                        <asp:RequiredFieldValidator ID="rfvpayment" runat="server" ControlToValidate="Fuslip"
                                                            ErrorMessage="Please Upload Payment Slip" Display="None"
                                                            ValidationGroup="submitnft">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <p style="color: red">Please Upload Pdf or jpeg or png or Files Upto 200KB Only.</p>
                                                    </div>


                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Button3_Click" ValidationGroup="submitnft" />
                                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="submitnft" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Details For NEFT/RTGS For Students Fees Collection</h5>
                                                        </div>
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Holder Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAccountHolderName" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBankNameRTGS" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Account No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAccountNo" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>IFSC CODE :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblIFSCCode" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Branch :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="col-12" id="ChallanDetails" runat="server">
                                            <div class="std-info pl-3 pr-3 pb-3">
                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Challan Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Bank Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlChallanBank" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvChallanBank" runat="server" ControlToValidate="ddlChallanBank" Display="None"
                                                            ErrorMessage="Please Select Bank Name" ValidationGroup="submitChallan" SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtChallanBranch" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvChallanBranch" runat="server" ControlToValidate="txtChallanBranch" Display="None"
                                                            ErrorMessage="Please Enter Branch Name" ValidationGroup="submitChallan" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtChallanBranch" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="0,1,2,3,4,5,6,7,8,9,~`!@#$%^*()_+=,./:;<>?'{}[]\|-&;'" />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>Challan No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtChallanNo" runat="server" CssClass="form-control" MaxLength="30" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtChallanNo" FilterType="Custom" FilterMode="InvalidChars" InvalidChars=",~`!@#$%^*()_+=,.:;<>?'{}[]|&;'" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvtrasid" runat="server" ControlToValidate="txttransid" Display="None"
                                                                    ErrorMessage="Please Enter Transaction Id" ValidationGroup="submitnft" SetFocusOnError="True" InitialValue="">
                                                                </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Date Of Challan</label>
                                                        </div>

                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <%--<asp:TextBox ID="txtdatechq" runat="server" CssClass="form-control" type="date" OnTextChanged="txtdatechq_TextChanged" AutoPostBack="true" />--%>
                                                            <asp:TextBox ID="txtdatepaychallan" runat="server" TabIndex="4"
                                                                CssClass="form-control" Style="z-index: 0;" OnTextChanged="txtdatepaychallan_TextChanged" AutoPostBack="true" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatepaychallan"
                                                                PopupButtonID="imgCalDDDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meChallan" runat="server" TargetControlID="txtdatepaychallan"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meChallan"
                                                                ControlToValidate="txtdatepaychallan" IsValidEmpty="False" EmptyValueMessage="Select Date Of Payment"
                                                                InvalidValueMessage="Date Of Payment is invalid"
                                                                Display="Dynamic" ValidationGroup="submitChallan" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvdatechq" runat="server" ControlToValidate="txtdatechq" Display="None"
                                                            ErrorMessage="Please Select Date of Issue" ValidationGroup="submitchq" SetFocusOnError="True" InitialValue="">
                                                        </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Upload Challan</label>
                                                        </div>
                                                        <asp:FileUpload ID="FuChallan" runat="server" />
                                                        <asp:RequiredFieldValidator ID="rfvFuChallan" runat="server" ControlToValidate="FuChallan"
                                                            ErrorMessage="Please Upload Challan" Display="None"
                                                            ValidationGroup="submitChallan">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <p style="color: red">Please Upload Pdf Files Upto 200KB Only.</p>
                                                    </div>


                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitChallan" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitChallan_Click" ValidationGroup="submitChallan" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="submitChallan" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12 pt-3">
                                                        <div class="sub-heading">
                                                            <h5>Details For Challan For Students Fees Collection</h5>
                                                        </div>
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Holder Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChallanAccName" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChallanBankName" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Account No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChallanAccNo" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>IFSC CODE :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChallanIfsc" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Branch :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblChallanBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Label ID="lblChallanFileName" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnDownloadChallan" runat="server" Text="Download Challan" CssClass="btn btn-primary" OnClick="btnDownloadChallan_Click" />    
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>


                                    </div>
                                </div>
                                <%--<div class="col-12" id="installdetails" runat="server">
                                                <div class="sub-heading">
                                        <h5>Installment Details</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Installment No.</th>
                                                <th>Amount</th>
                                                <th>DueDate</th>
                                                <th>ExtraCharge/Discount</th>
                                                <th>Late Fee</th>
                                                <th>Total Paid</th>
                                                <th>Online</th>
                                                <th>Offline</th>
                                                <th>Remark</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1 </td>
                                                <td>150,000.00 </td>
                                                <td>01-07-2022 </td>
                                                <td>11,250.00 </td>
                                                <td>0.00 </td>
                                                <td>161,250.00 </td>
                                                <td>- </td>
                                                <td>- </td>
                                                <td>Payment Done </td>
                                            </tr>
                                            <tr>
                                                <td>2 </td>
                                                <td>150,000.00 </td>
                                                <td>01-07-2022 </td>
                                                <td>11,250.00 </td>
                                                <td>0.00 </td>
                                                <td>161,250.00 </td>
                                                <td>- </td>
                                                <td>- </td>
                                                <td>Payment Done </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                            </div>--%>
                            </asp:Panel>
                        </div>






                        <asp:HiddenField ID="hdfAmount" runat="server" />

                        <div id="divInstallmentPayment" class="box-footer" runat="server" visible="false">
                            <div class="row mb-3">
                                <%-- <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Roll No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>School/Institute Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCollegeName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>--%>
                                <%--<div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegreeName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Programme/Branch Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranchName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>--%>
                                <div class="col-lg-4 col-md-6 col-12">
                                </div>
                            </div>

                            <div class="col-12 btn-footer" style="display: none">
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click" TabIndex="6" CssClass="btn btn-warning" />
                            </div>
                        </div>

                        <div class="col-12 mt-4 table-responsive" id="previousreceipt" runat="server">
                            <asp:ListView ID="lvStudFeeDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Payment Details</h5>
                                    </div>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Receipt Type
                                                    </th>
                                                    <th id="recieptno">Receipt No
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Payment Type
                                                    </th>
                                                    <th hidden>Payment Mode
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <th>Payment Status
                                                    </th>
                                                    <th>Approval Status                                                    
                                                    </th>
                                                    <th id="idStatus">Print
                                                    </th>
                                                    <th id="idremark">Remark
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
                                            <asp:Label ID="lblRecieptType" runat="server" Text='<%# Eval("RECIEPT_TITLE")%>'></asp:Label>
                                        </td>

                                        <td id="tdRecipetNo" runat="server">
                                            <%# Eval("REC_NO")%>
                                        </td>
                                        <td>
                                            <%# Eval("REC_DT")%>

                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAYMENT TYPE")%>
                                        </td>
                                        <td hidden>
                                            <%# Eval("PAYMENT MODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("TOTAL_AMT") %>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='COMPLETE'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblActive1" Text='<%# Eval("APPROVED STATUS")%>' ForeColor='<%# Eval("APPROVED STATUS").ToString().Equals("APPROVED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                        </td>
                                        <td id="tdPrintRecipet" runat="server">
                                            <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                        </td>
                                        <td>
                                            <%# Eval("REMARK") %>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>


                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; top: 15% !important; left: 18% !important; right: 18% !important; position: fixed; z-index: 100001;">
                        <div class="container">
                            <div class="col-12">
                                <div class="row">
                                    <div class="sub-heading">
                                        <h5>UNDERTAKING BY STUDENT</h5>

                                    </div>

                                    <div class="DocumentList">
                                        <div class=" col-lg-12 col-md-6 col-12">
                                            <asp:Label ID="lblterms" runat="server">
                                                <p>
                                                   <span style="font-weight:bold">I. Attendance</span> 
                                                    <br />
                                                    1) I clearly understand that if I fail to keep the minimum 75% attendance, I shall not be granted terms.
                                                    <br />
                                                    2) I hereby declare that the information and document submitted by me in the form are true and in case any of it is subsequently found untrue, I am aware that my admission is liable to be cancelled.
                                                    <br />
                                                    3) I have read and understood the policies as mentioned in the Student Handbook and agree to abide by the same
                                                    <br /><br />
                                                   <span style="font-weight:bold"> II. Anti Ragging</span>
                                                    <br />
                                                    1) I will not indulge in any behavior or act that may come under the definition of ragging.
                                                    <br />
                                                    2) I will not participate in or abet or propagate ragging in any form.
                                                    <br /> 
                                                    3) I will not hurt anyone physically or psychologically or cause any other harm.
                                                    <br />
                                                    I solely affirm that I am aware that if found guilty, I am liable for punishment according to the regulations of the University,
                                                    <br />
                                                    I hereby declare that I have not been expelled or debarred from admission in any institution in the country on account of being found guilty of, abetting or being part of a conspiracy to promote ragging, and further affirm that, in case the declaration is found untrue, I am aware that my admission is liable to be cancelled.
                                                </p>
                                            </asp:Label>
                                            <div>
                                                <asp:CheckBox ID="chkAgree" runat="server" Text="I Accept" />
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="form-group col-lg-6 col-md-6 col-12">
                                        <span style="color: #FF0000; font-weight: bold">* Please scroll down for check terms & condtions</span>
                                    </div>--%>
                                    <div class="btn-footer col-12">
                                        <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="btnOk_Click" />
                                        <asp:Button ID="btnClose" runat="server" Text="CANCEL" CssClass="btn btn-danger" OnClick="btnClose_Click" />
                                    </div>

                                </div>
                                <ajaxToolKit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                    BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowScroll"
                                    TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="Panel1">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />

                            </div>
                        </div>
                    </asp:Panel>



                    <!-- The Modal fees collection details-->
                    <div class="modal fade" id="myModal_feesdetails">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">NEFT/RTGS,Cheque & DD for students fees collection</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body ml-0 mr-0">
                                    <div class="col-lg-12 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Details for NEFT/RTGS for students fees collection</h5>
                                        </div>
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Account Holder Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label1" runat="server" Text="MIT ACADEMY OF ENGINEERING" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Bank Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label2" runat="server" Text="HDFC BANK LTD" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Account No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label3" runat="server" Text="50100413320825" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>IFSC CODE :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label4" runat="server" Text="HDFC0002844" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label5" runat="server" Text="Dahanukar Colony Kothrud" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-12 mt-4">
                                        <div class="sub-heading">
                                            <h5>Cheque  & DD Details </h5>
                                        </div>
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Cheque  & DD should be drawn in favour of :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label6" runat="server" Text="MIT ACADEMY OF ENGINEERING" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>DD payable at :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label7" runat="server" Text="Pune" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Cheque Bouncing charges :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label8" runat="server" Text="1000/-" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>

                            </div>
                        </div>
                    </div>

                    <!-- The Modal Grayquest Loan Facility details-->
                    <div class="modal fade" id="myModal_loan">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">Grayquest Loan Facility</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body">
                                    <div class="col-12">
                                        <ul class="gray_quest">
                                            <li>This year, MIT Academy of Engineering and MIT Institute of Design has officially partnered with Grayquest and 
                                                is offering a new monthly fee payment option where parents can convert their annual fees to easy and convenient 
                                                monthly installments at very low cost and zero processing fees (no hidden costs).
                                            </li>
                                            <li>Interested parents are requested to register immediately by clicking the pay fee button below or click this link : 
                                                <a href="https://grayquest.com" target="_blank">https://grayquest.com</a>
                                            </li>
                                            <li>Parents Opting for Grayquest will be given a complementary critical illness insurance cover of Rs. 5 lakhs 
                                            </li>
                                            <li class="pb-0">For any query, Call: Mr. Swapnil: 7767879317
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
    <div id="divMsg" runat="server">
    </div>

    <!-- ===== Chat Coding ===== -->
    <section class="chatbox js-chatbox" style="display: none">
        <div class="chatbox__header js-chatbox-toggle" id="AskMe">
            <h3 class="chatbox__header-cta-text"><span class="chatbox__header-cta-icon"><i class="fa fa-comments"></i></span><span class="head-chat">Ask Me</span> <%--Let's chat--%></h3>
            <span class="chatbox__header-cta-btn u-btn"><i class="fa fa-times"></i></span>
        </div>
        <!-- End of .chatbox__header -->
        <div class="js-chatbox-display chatbox__display main_feature" id="DynamicBinding">
        </div>
        <!-- End of .chatbox__display -->
        <form class="js-chatbox-form chatbox__form">
            <%--<input  type="text" class="js-chatbox-input chatbox__form-input" placeholder="Type your message...">
                <button class="chatbox__form-submit u-btn Button_Send"><i class="fas fa-paper-plane"></i></button>--%>
            <textarea id="text" class="dynamic_textarea js-chatbox-input chatbox__form-input" rows="1" style="overflow: hidden;" placeholder="Enter any comment here...." maxlength="200"></textarea>
            <span class='chatbox__form-submit u-btn'>
                <span class="send_btn"><i class="fa fa-paper-plane Button_Send"></i></span>
            </span>
        </form>
        <!-- End of .chatbox__form -->
    </section>
    <!-- End of .chatbox -->
    <script>

        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

                return true;
            else
                return false;
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }



        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }



        function checkDOB() {
            //alert('hi');
            var selectDate = document.getElementById('<%=txtdatedd.ClientID%>').value;
            //alert(selectDate);
            var today = new Date();
            //var dd = String(today.getDate()).padStart(2, '0');
            //var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            //var yyyy = today.getFullYear();
            var selDate = selectDate.split("/");
            var selday = selDate[0];
            var selmonth = selDate[1] - 1;
            var selyear = selDate[2];
            var SDate = new Date(selyear, selmonth, selday);

            alert(today);
            alert(SDate);
            if (SDate > today) {
                alert("Future Date Is Not Acceptable.");

                return false;
            }

        }

    </script>

    <%--<script>
            const toggleChatboxBtn = document.querySelector(".js-chatbox-toggle");
const chatbox = document.querySelector(".js-chatbox");
const chatboxMsgDisplay = document.querySelector(".js-chatbox-display");
const chatboxForm = document.querySelector(".js-chatbox-form");

        // Use to create chat bubble when user submits text
        // Appends to display
const createChatBubble = input => {
    const chatSection = document.createElement("p");
        chatSection.textContent = input;
        chatSection.classList.add("chatbox__display-chat");

        chatboxMsgDisplay.appendChild(chatSection);
        };

        // Toggle the visibility of the chatbox element when clicked
        // And change the icon depending on visibility
        toggleChatboxBtn.addEventListener("click", () => {
            chatbox.classList.toggle("chatbox--is-visible");

        //if (chatbox.classList.contains("chatbox--is-visible")) {
        //    toggleChatboxBtn.innerHTML = '<i class="fa fa-times"></i>';
        //} else {
        //    toggleChatboxBtn.innerHTML = '<i class="fa fa-chevron-up"></i>';
        //}

        });

        // Form input using method createChatBubble
        // To append any user message to display
        chatboxForm.addEventListener("submit", e => {
            const chatInput = document.querySelector(".js-chatbox-input").value;

        createChatBubble(chatInput);

        e.preventDefault();
        chatboxForm.reset();
        });

    </script>--%>

    <%-- <script>
        $('#idStatus').hide(); $('#tdRecipetNo').hide();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#idStatus').hide();
            $('#tdRecipetNo').hide();
        });
    </script>--%>

    <%--  <script type="text/javascript">
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
    </script>--%>

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

    <script>
        $(document).ready(function () {
            function checkvalidation() {
                alert('in');
                // Get a reference to the listbox control
                $('#<%= ddlgroups.ClientID %> input[type="checkbox"]').on('change', function () {
                    alert('a');
                    var selectedCount = $('#<%= ddlgroups.ClientID %> input[type="checkbox"]:checked').length;

                    // Check if more than two checkboxes are selected
                    if (selectedCount > 2) {
                        // Uncheck the last checked checkbox
                        $(this).prop('checked', false);
                    }
                });

            }

        });
    </script>

</asp:Content>

