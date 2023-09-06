<%@ Page Language="C#" AutoEventWireup="true" CodeFile="principalHome.aspx.cs" Inherits="principalHome" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />

    <style>
        #header-fixed {
            position: fixed;
            top: 0px;
            display: none;
            background-color: white;
        }

        .box-tools.pull-right {
            display: none;
        }

        .chiller-theme .sidebar-wrapper {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding: 0;
            margin-top: 15px;
            padding-top: 40px !important;
        }

        .page-wrapper .page-content {
            height: auto;
        }

        .content {
            padding-bottom: 10px;
        }

        .scrollbar ul.fav-list li:hover {
            color: #212529;
            background-color: rgba(0,0,0,.075);
        }

        @media (min-width: 576px) and (max-width: 991px) {
            .content {
                padding-top: 0px;
                padding-bottom: 0px;
            }
        }

        .table-bordered > tbody > tr > td {
            text-align: left !important;
        }
        #tblAcdActivity.table-bordered > thead > tr > th {
            border-top: 0px solid #e6e9ed;
        }
    </style>
    <style>
        /* Customize website's scrollbar like Mac OS
        Not supports in Firefox and IE */
        /* total width */
        .scrollbar::-webkit-scrollbar {
            background-color: #fff;
            width: 5px;
            scrollbar-width: thin;
        }

        /* background of the scrollbar except button or resizer */
        .scrollbar::-webkit-scrollbar-track {
            background-color: #fff;
        }

            .scrollbar::-webkit-scrollbar-track:hover {
                background-color: #f4f4f4;
            }

        /* scrollbar itself */
        .scrollbar::-webkit-scrollbar-thumb {
            background-color: #babac0;
            border-radius: 2px;
            border: 2px solid #fff;
        }

            .scrollbar::-webkit-scrollbar-thumb:hover {
                background-color: #a0a0a5;
                border: 2px solid #f4f4f4;
            }

        /* set button(top and bottom of the scrollbar) */
        .scrollbar::-webkit-scrollbar-button {
            display: none;
        }
        /*::-webkit-scrollbar {
            width: 4px;
            height: 5px;
            background: #e5e5e5;
        }*/
    </style>

    
    <script>
        $(document).ready(function () {
            if ($(window).width() < 768) {
                //  $('.mobile-modal-btn').show();
                var $mbody = $('#myModalzAppleiedLeave').find('.modal-body');
                $('#img-clone').clone().appendTo('#myModalzAppleiedLeave .modal-body');
                $mbody.addClass('text-center');
                $mbody.find('img').attr('class', '');
                //    $('.mobile-box').hide();
            }
        })
    </script>

    <script>
        $(document).ready(function () {
            if ($(window).width() < 768) {
                //  $('.mobile-modal-btn').show();
                var $mbody = $('#myModalzApproved').find('.modal-body');
                $('#img-clone').clone().appendTo('#myModalzApproved .modal-body');
                $mbody.addClass('text-center');
                $mbody.find('img').attr('class', '');
                //    $('.mobile-box').hide();
            }
        })
    </script>

    <script>
        $(document).ready(function () {
            if ($(window).width() < 768) {
                //  $('.mobile-modal-btn').show();
                var $mbody = $('#myModalzPending').find('.modal-body');
                $('#img-clone').clone().appendTo('#myModalzPending .modal-body');
                $mbody.addClass('text-center');
                $mbody.find('img').attr('class', '');
                //    $('.mobile-box').hide();
            }
        })
    </script>

    <div class="container-fluid">
        <section class="statistics">
            <div class="gutters-sm">
                <div class="mybox-main row">
                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a1">
                            <i class="fa fa-male fa-fw icon-sm" style="background-color: #2f4554"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblMaleCount"></label>
                                </h3>
                                <span>Male</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a2">
                            <i class="fa fa-female fa-fw danger icon-sm" style="background-color: #255282"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblFemaleCount"></label>
                                </h3>
                                <span>Female</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a3">
                            <i class="fa fa-graduation-cap fa-fw warning icon-sm" style="background-color: #c23531"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblTotalStudent"></label>
                                </h3>
                                <span>Total Student</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a4">
                            <i class="fa fa-users fa-fw icon-sm" style="background-color: #e97b25"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblActiveUser"></label>
                                </h3>
                                <span>Active Users</span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </section>

        <div class="row equalHMRWrap flex gutters-sm">
            <div style="display: none">
                <div class="col-lg-2 col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Today's Time Table</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <table class="table table-hover small table-striped table-bordered nowrap">
                                <thead>
                                    <tr>
                                        <th>Time</th>
                                        <th>Subject</th>
                                        <th>Branch</th>
                                        <th>Semester</th>
                                        <th>Section</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>10:00 am</td>
                                        <td>Applied Mathematics</td>
                                        <td>Electronics</td>
                                        <td>1<sup>st</sup> Sem</td>
                                        <td>Sec A</td>
                                    </tr>
                                    <tr>
                                        <td>12:00 am</td>
                                        <td>Applied Mathematics</td>
                                        <td>Mechanical</td>
                                        <td>1<sup>st</sup> Sem</td>
                                        <td>Sec C</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-2 col-md-6 col-12">
                <div class="x_panel in-left a1">
                    <div class="x_title">
                        <h2>Admission Year</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small table-striped table-bordered nowrap" id="tblstudsCount">
                        </table>
                    </div>
                </div>
            </div>

            <%--<div class="col-lg-2 col-md-6 col-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Admission Fees <span id="lblYear" class="text-black"></span></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small" id="tblAdmFees">
                        </table>

                    </div>
                </div>
            </div>--%>

            <div class="col-lg-2 col-md-6 col-12">
                <div class="x_panel in-left a2">
                    <div class="x_title">
                        <h2>Staff Leaves</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small table-striped table-bordered nowrap">
                            <thead class="bg-primary">
                                <tr>
                                    <th>Leave Types</th>
                                    <th class="text-center">Total</th>
                                </tr>
                                <%-- <tr>
                                    <td>Applied Leaves</td>

                                </tr>
                                <tr>
                                    <td>Approved Leaves</td>
                                </tr>
                                <tr>
                                    <td>Pending Leaves</td>
                                </tr>--%>
                            </thead>
                            <tr>
                                <td>
                                    <a href="#">
                                        <div class='mobile-modal-btn' data-toggle="modal" data-target="#myModalzAppleiedLeave">Applied Leaves</div>
                                    </a>
                                </td>
                                <td>
                                    <label id="lblToTal_Applied"></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#">
                                        <div class="mobile-modal-btn" data-toggle="modal" data-target="#myModalzApproved">Approved Leaves </div>
                                    </a>
                                </td>
                                <td>
                                    <label id="lblApprove_Leave"></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#">
                                        <div class="mobile-modal-btn" data-toggle="modal" data-target="#myModalzPending">Pending Leaves</div>
                                    </a>
                                </td>
                                <td>
                                    <label id="lblPending_Leave"></label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModalzAppleiedLeave" role="dialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <%--style="background-color:red;"--%>
                            <%--<div class="col-md-9 col-sm-8 col-xs-12 flex" runat="server" visible="false" id="divleave">--%>
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Employee Applied Leave Details</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                    <table class="table table-hover small table-striped table-bordered nowrap">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th class="text-center">Name</th>
                                                <th class="text-center">Department</th>
                                                <th class="text-center">Leave</th>
                                                <th class="text-center">From Date</th>
                                                <th class="text-center">To Date</th>
                                                <th class="text-center">Approve</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tLeaveAll">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                    <div class="modal-body">
                    </div>
                    <%--<div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>

            </div>

            <div class="modal fade" id="myModalzApproved" role="dialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <%--<div class="col-md-9 col-sm-8 col-xs-12 flex" runat="server" visible="false" id="divleave">--%>
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Employee Approved Leave Details </h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                    <table class="table table-hover small table-striped table-bordered nowrap">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th class="text-center">Name</th>
                                                <th class="text-center">Department</th>
                                                <th class="text-center">Leave</th>
                                                <th class="text-center">From Date</th>
                                                <th class="text-center">To Date</th>
                                            </tr>
                                        </thead>
                                        <tbody id="TLeaveApproved">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                    <div class="modal-body">
                    </div>
                    <%--<div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>

            <div class="modal fade" id="myModalzPending" role="dialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <%--<div class="col-md-9 col-sm-8 col-xs-12 flex" runat="server" visible="false" id="divleave">--%>
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>Employee Pending Leave List</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                    <table class="table table-hover small table-striped table-bordered nowrap">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th class="text-center">Name</th>
                                                <th class="text-center">Department</th>
                                                <th class="text-center">Leave</th>
                                                <th class="text-center">From Date</th>
                                                <th class="text-center">To Date</th>
                                                <th class="text-center">Pending On</th>
                                            </tr>
                                        </thead>
                                        <tbody id="TPendingLeave">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                    <div class="modal-body">
                    </div>

                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>Quick Access</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar small">
                        <ul class="list-group with-border-bottom fav-list" id="ulQuickAccess">
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-12">
                <div class="x_panel in-right a1">
                    <div class="x_title">
                        <h2>Result Analysis <span id="lblResultSession" class="text-black analy" style="display: none"></span><span class="text-red analy" style="display: none"></span></h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small table-striped table-bordered nowrap " id="table-1">
                        </table>
                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-lg-4 col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>In / Out Time</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <table class="table table-hover small table-striped table-bordered nowrap">
                                <thead>
                                    <tr>
                                        <th>Day</th>
                                        <th>In-Time</th>
                                        <th>Out-Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Mon</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Tue</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Wed</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Thur</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Fri</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Sat</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>

                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-lg-4 col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Quick Access</h2>
                            <ul class="nav navbar-right panel_toolbox" style="display: none">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li><a href="#">Settings 1</a>
                                        </li>
                                        <li><a href="#">Settings 2</a>
                                        </li>
                                    </ul>
                                </li>
                                <li><a class="close-link"><i class="fa fa-close"></i></a>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <ul class="list-group with-border-bottom fav-list small">
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Online Fees Payment</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Grade Cards</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Subject Faculty Feedback</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Detail Search</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Grievance Application </a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Announcement</a></li>
                            </ul>

                        </div>

                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-lg-4 col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Notice</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <article class="media event">

                                <div class="media-body">
                                    <a class="title" href="#">Placement</a>
                                    <p>15 students from IT got selected in top MNC's.</p>
                                </div>
                            </article>
                            <article class="media event">
                                <div class="media-body">
                                    <a class="title" href="#">Item Two Title</a>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                </div>
                            </article>
                            <article class="media event">
                                <div class="media-body">
                                    <a class="title" href="#">Item Two Title</a>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row equalHMRWrap flex gutters-sm">
            <div class="col-lg-8 col-md-7 col-12">
                <div class="x_panel in-left a2">
                    <div class="x_title">
                        <h2>Academic Activities</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <div class="table-responsive" style="height: 170px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                            <table class="table table-hover small table-striped table-bordered nowrap" style="width: 100%;" id="tblAcdActivity">
                                <thead class="bg-light-blue bg-primary" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                    <tr>

                                        <%--<th class="text-center">College</th>--%>
                                        <th>Activity</th>
                                        <th>School-Session</th>
                                        <th>Start Date</th>
                                        <th>End Date</th>
                                        <th>Activity Status</th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyActivity">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-lg-4 col-md-6 col-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Tasks</h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">
                            <ul class="list-group with-border-bottom">
                                <li class="list-group-item"><a href="#">Approve Leaves</a></li>
                                <li class="list-group-item"><a href="#">Mark Entry <span class="ncount">1</span></a></li>
                                <li class="list-group-item"><a href="#">Attendance</a></li>
                                <li class="list-group-item"><a href="#">Upload Lecture Notes</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-5 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>Active Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <asp:ListView ID="lvActiveNotice" runat="server">
                            <LayoutTemplate>
                                <table class="table table-hover small table-striped table-bordered nowrap" id="tblNotice">
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                   <article class="media event">
                                        <a class="pull-left date">
                                            <p class="month"><%#Eval("MM")%></p>
                                            <p class="day"><%#Eval("DD")%></p>
                                        </a>
                                        <div class="media-body">
                                            <p><%#Eval("NEWSDESC") %></p>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                            
                                        </div>
                                    </article>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="x_title d-none" >
                        <h2>Expired Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-150 scrollbar d-none">
                        <asp:ListView ID="lvExpNotice" runat="server">
                            <LayoutTemplate>
                                <table class="table table-hover small table-striped table-bordered nowrap" id="tblExpNotice">
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <article class="media event">
                                        <a class="pull-left date">
                                            <p class="month"><%#Eval("MM")%></p>
                                            <p class="day"><%#Eval("DD")%></p>
                                        </a>
                                        <div class="media-body">
                                             <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                            <p><%#Eval("NEWSDESC") %></p>
                                        </div>
                                    </article>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var tableOffset = $("#table-1").offset().top;
        var $header = $("#table-1 > thead").clone();
        var $fixedHeader = $("#header-fixed").append($header);

        $(window).bind("scroll", function () {
            var offset = $(this).scrollTop();

            if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
                $fixedHeader.show();
            }
            else if (offset < tableOffset) {
                $fixedHeader.hide();
            }
        });
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             //alert('bye')
             //this.BindLinks();
             $.ajax({
                 type: "POST",
                 //url: "~/principalHome.aspx/ShowMaleCount",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowMaleCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessMaleCount,
                failure: function (response) {
                    alert(response)
                },
                error: function (response) {
                    alert(response)
                }
             });

             function OnSuccessMaleCount(response) {

                 var maleCount = response['d'];

                 $('#lblMaleCount').html(maleCount);
             };

             //added for female count
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/ShowFeMaleCount",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowFeMaleCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessfeMaleCount,
                failure: function (response) {
                }
            });
             function OnSuccessfeMaleCount(response) {
                 var femaleCount = response['d'];

                 $('#lblFemaleCount').html(femaleCount);
             };

             //added for ACTIVE USER count
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/ActiveUserCount",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ActiveUserCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessActiveUserCount,
                failure: function (response) {
                }
            });
             function OnSuccessActiveUserCount(response) {
                 var activeUserCount = response['d'];
                 $('#lblActiveUser').html(activeUserCount);
             };

             //added for total student count
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/TotalStudentCount",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/TotalStudentCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTotstudentCount,
                failure: function (response) {
                }
            });
             function OnSuccessTotstudentCount(response) {
                 var totStudentCount = response['d'];
                 $('#lblTotalStudent').html(totStudentCount);
             };

             // added for Students count
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/BindStudentsCount",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindStudentsCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessStudentsCount,
                failure: function (response) {
                }
            });
             function OnSuccessStudentsCount(response) {
                 var StudentCount = response['d'];
                 loadStudentDetails(StudentCount);
             };
             function loadStudentDetails(StudentCount) {
                 var html = '';
                 html += '<thead class="bg-primary"><tr><th>Year</th><th class="text-center">Count</th></tr></thead>';
                 html += '<tbody>';
                 if (StudentCount != null) {
                     if (StudentCount.length > 0) {
                         var TotalCount = 0;
                         $.each(StudentCount, function (row, item) {
                             TotalCount += Number(item.Count);
                         });
                         html += '</tbody>';
                         html += '<tfoot><tr class="text-danger">';
                         html += '<td> Total Students </td>';
                         html += '<td class="text-right">' + TotalCount + '</td>';
                         $.each(StudentCount, function (row, item) {
                             TotalCount += Number(item.Count);
                             html += '<tr>';
                             html += '<td>' + item.Year + '</td>';
                             html += '<td class="text-right">' + item.Count + '</td>';
                             html += '</tr>';
                         });
                         html += '</tr></tfoot>';
                     } else {
                         html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                     }
                 } else {
                     html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                 }
                 $('#tblstudsCount').append(html);
             };

             //Leave count
             $.ajax({

                 type: "POST",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindLeaveCount")%>',
                //url: "principalHome.aspx/BindLeaveCount",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessLeaveDetails,
                failure: function (response) {
                }
            });

             function OnSuccessLeaveDetails(response) {
                 // console.log("Data", response);
                 //debugger;
                 var ToTal_Applied = response['d'].ToTal_Applied;
                 var Approve_Leave = response['d'].Approve_Leave;
                 var Pending_Leave = response['d'].Pending_Leave;
                 //loadleavecount(leavecount);

                 $('#lblToTal_Applied').html(ToTal_Applied);
                 //alert(ToTal_Applied);
                 $('#lblApprove_Leave').html(Approve_Leave);
                 $('#lblPending_Leave').html(Pending_Leave);

             };

             // added for Leave Applied Leave Model Data

             $.ajax({
                 type: "POST",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowLeaveapprove")%>',
                //url: "principalHome.aspx/ShowLeaveapprove",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessApprovePendignLeaveDetails,
                failure: function (response) {
                }
            });
             function OnSuccessApprovePendignLeaveDetails(response) {
                 var ApprovePendignLeave = response['d'];
                 loadApprovePendignLeaveDetails(ApprovePendignLeave);
             };
             function loadApprovePendignLeaveDetails(ApprovePendignLeave) {

                 var html = '';
                 if (ApprovePendignLeave != null) {
                     if (ApprovePendignLeave.length > 0) {
                         $.each(ApprovePendignLeave, function (row, item) {
                             html += '<tr>';
                             html += '<td class="text-left">' + item.EmpName + '</td>';
                             html += '<td class="text-center">' + item.SUBDEPT + '</td>';
                             html += '<td class="text-center">' + item.LName + '</td>';
                             html += '<td class="text-center">' + item.From_date + '</td>';
                             html += '<td class="text-center">' + item.TO_DATE + '</td>';
                             html += '<td class="text-center"> <input type=button Value="Approve" class="btn btn-success" onclick="return UpdateDataLeave(' + item.LETRNO + ')" /></td>';
                             //html += '<td class="text-center">' + '<a href="ESTABLISHMENT/LEAVES/Transactions/LeaveApprovalforprincipal.aspx" target="_self" style="text-align: right" class="buttonStyle ui-corner-all btn btn-success">Go To Approval</a>' + '</td>';
                             html += '</tr>';
                         });
                     } else {
                         html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                     }
                 } else {
                     html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                 }
                 $('#tLeaveAll').append(html);
             };



             // added for Leave Applied Leave Model Data

             $.ajax({
                 type: "POST",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowLeaveapproveCount")%>',
                //url: "principalHome.aspx/ShowLeaveapproveCount",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessApproveLeaveDetails,
                failure: function (response) {
                }
            });
             function OnSuccessApproveLeaveDetails(response) {
                 var ApproveLeaveCount = response['d'];
                 loadApproveLeaveDetails(ApproveLeaveCount);
             };
             function loadApproveLeaveDetails(ApproveLeaveCount) {

                 var html = '';
                 if (ApproveLeaveCount != null) {
                     if (ApproveLeaveCount.length > 0) {
                         $.each(ApproveLeaveCount, function (row, item) {
                             html += '<tr>';
                             html += '<td class="text-left">' + item.EmpName + '</td>';
                             html += '<td class="text-center">' + item.SUBDEPT + '</td>';
                             html += '<td class="text-center">' + item.LName + '</td>';
                             html += '<td class="text-center">' + item.From_date + '</td>';
                             html += '<td class="text-center">' + item.TO_DATE + '</td>';
                             html += '</tr>';
                         });
                     } else {
                         html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                     }
                 } else {
                     html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                 }
                 $('#TLeaveApproved').append(html);
             };


             // added for Leave Applied Leave Model Data

             $.ajax({
                 type: "POST",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowPendingCount")%>',
                //url: "principalHome.aspx/ShowPendingCount",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessPendingLeaveDetails,
                failure: function (response) {
                }
            });
             function OnSuccessPendingLeaveDetails(response) {
                 var PendingLeaveCount = response['d'];
                 loadPendingLeaveDetails(PendingLeaveCount);
             };

             function loadPendingLeaveDetails(PendingLeaveCount) {

                 var html = '';
                 if (PendingLeaveCount != null) {
                     if (PendingLeaveCount.length > 0) {
                         $.each(PendingLeaveCount, function (row, item) {
                             html += '<tr>';
                             html += '<td class="text-left">' + item.EmpName + '</td>';
                             html += '<td class="text-center">' + item.SUBDEPT + '</td>';
                             html += '<td class="text-center">' + item.LName + '</td>';
                             html += '<td class="text-center">' + item.From_date + '</td>';
                             html += '<td class="text-center">' + item.TO_DATE + '</td>';
                             html += '<td class="text-center">' + item.Pending_on + '</td>';
                             html += '</tr>';
                         });
                     } else {
                         html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                     }
                 } else {
                     html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                 }
                 $('#TPendingLeave').append(html);
             };

             // added for Activity Data
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/BindActivityDetails",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindActivityDetails")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessActivityDetails,
                failure: function (response) {
                }
            });
             function OnSuccessActivityDetails(response) {
                 var ActivityData = response['d'];
                 loadActivityDetails(ActivityData);
             };
             function loadActivityDetails(ActivityData) {
                 var html = '';

                 if (ActivityData != null) {
                     if (ActivityData.length > 0) {
                         $.each(ActivityData, function (row, item) {
                             html += '<tr>';
                             html += '<td class="text-left">' + item.ActivityName + '</td>';
                             html += '<td class="text-center">' + item.SessionName + '</td>';
                             html += '<td class="text-center">' + item.StartDate + '</td>';
                             html += '<td class="text-center">' + item.EndDate + '</td>';
                             html += '<td class="text-center">' + item.ActivityStatus + '</td>';
                             html += '</tr>';
                         });
                     } else {
                         html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                     }
                 } else {
                     html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                 }
                 $('#tbodyActivity').append(html);
                 //alert('aa');
             };

             // Added For Admission Fees Data
             //$.ajax({
             //    type: "POST",
             //    url: "principalHome.aspx/BindAdmFeesDetails",
             //    data: '{}',
             //    contentType: "application/json; charset=utf-8",
             //    dataType: "json",
             //    success: OnSuccessAdmFeesDetails,
             //    failure: function (response) {
             //    }
             //});

             //function OnSuccessAdmFeesDetails(response) {
             //    var FeesData = response['d'];
             //    loadAdmFeesDetails(FeesData);
             //};
             //function loadAdmFeesDetails(FeesData) {
             //    $('#lblYear').hide();
             //    var totalCollection = 0.00;
             //    var html = '';
             //    html += '<thead class="bg-primary"><tr><th>Receipt</th><th class="text-right" style="padding-right:15px">Total</th></tr></thead>';
             //    html += '<tbody>';
             //    if (FeesData != null) {
             //        if (FeesData.length > 0) {
             //            $.each(FeesData, function (row, item) {
             //                $('#lblYear').text(' - (' + item.Year + ')');
             //                $('#lblYear').show();
             //                totalCollection += Number(item.Fee);
             //                html += '<tr>';
             //                html += '<td class="text-left">' + item.Receipt + '</td>';
             //                html += '<td class="text-right">&#8377; ' + item.Fee + '</td>';
             //                html += '</tr>';
             //            });
             //            html += '<tbody>';
             //            html += '<tfoot><tr class="text-danger">';
             //            html += '<td>Total Collection</td>';
             //            html += '<td class="text-right">&#8377 ' + totalCollection.toFixed(2) + '</td>';
             //            html += '</tr></tfoot>';
             //        } else {
             //            html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
             //        }
             //    } else {
             //        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
             //    }
             //    $('#tblAdmFees').append(html);
             //};

             //--------------------------------------------------------------------------------------------
             // Added For News Data
             //  $.ajax({
             //      type: "POST",
             //      //url: "principalHome.aspx/ShowNewsData",
             //      url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowNewsData")%>',
             //      data: '{}',
             //      contentType: "application/json; charset=utf-8",
             //      dataType: "json",
             //      success: OnSuccessNews,
             //      failure: function (response) {
             //          var html = '<div style="text-align:center; font-size:15px; font-weigth:bold" class="info">No records to display..</div>';
             //          $('#newsDiv').html(html);
             //      }
             //  });
             //  function OnSuccessNews(response) {
             //      loadNewsData(response['d']);
             //  };
             //  function loadNewsData(data) {
             //      var html = '';
             //      if (data != null) {
             //          if (data.length > 0) {
             //              $.each(data, function (row, item) {
             //                  if (item.Link != '') {
             //                      html += '<article class="media event"><a class="pull-left date"><p class="month">' + item.Month + '</p><p class="day">' + item.Day + '</p></a><div class="media-body"><a class="title" target="_blank" href=' + item.Link + '>' + item.Title + '</a><p>' + item.NewsDesc + '</p></div></article>';
             //                  }
             //                  else {
             //                      html += '<article class="media event"><a class="pull-left date"><p class="month">' + item.Month + '</p><p class="day">' + item.Day + '</p></a><div class="media-body"><a class="title" href="#">' + item.Title + '</a><p>' + item.NewsDesc + '</p></div></article>';
             //
             //                  }
             //              });
             //          } else {
             //              html += '<div style="font-size:15px" class="text-center" class="info" >No records to display..</div>';
             //          }
             //      } else {
             //          html += '<div style="font-size:15px" class="text-center" class="info">No records to display..</div>';
             //      }
             //      $('#newsDiv').html(html);
             //  };
             //--------------------------------------------------------------------------------------------


             // Added For Result analysis Data
             $.ajax({
                 type: "POST",
                 //url: "principalHome.aspx/ShowResultData",
                 url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowResultData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessResultData,
                failure: function (response) {
                }
            });
             function OnSuccessResultData(response) {
                 var resultdata = response['d'];
                 loadReslutAnalysisDetails(resultdata);
             };
             function loadReslutAnalysisDetails(resultdata) {
                 $('.analy').hide();
                 var html = '';
                 if (resultdata != null) {
                     if (resultdata.tHeader.length > 0) {
                         html += '<thead class="bg-primary"><tr><th with="20%">Degree/Branch</th>';
                         $.each(resultdata.tHeader, function (row, item) {
                             html += '<th class="text-center">' + item.Header + '</th>';
                         });
                         html += '</tr></thead>';
                         html += '<tbody>';

                         if (resultdata.tBody != null) {
                             if (resultdata.tBody.length > 0) {
                                 $.each(resultdata.tBody, function (row, item) {
                                     $('#lblResultSession').text('(' + item.SessionName + ')');
                                     $('.analy').show();
                                     html += '<tr>';
                                     html += '<td class="text-left bg-success" data-container="body"  data-toggle="tooltip" data-original-title="' + item.BranchName + ' - ' + item.DegreeName + '"><strong>' + item.BranchShortName + ' (' + item.DegreeName + ')</strong></td>';

                                     // if (resultdata.tHeader.length== 3) {
                                     //condition start--added by Nikhil V.Lambe on 01/12/2020 to specify between sem1,sem2,sem3,sem4
                                     if (resultdata.tHeader.length == 1) {
                                         html += '<td class="text-center">' + item.Sem1 + '</td>';
                                     }
                                     else if (resultdata.tHeader.length == 2) {
                                         html += '<td class="text-center">' + item.Sem1 + '</td>';
                                         html += '<td class="text-center">' + item.Sem2 + '</td>';
                                     }
                                     else if (resultdata.tHeader.length == 3) {
                                         html += '<td class="text-center">' + item.Sem1 + '</td>';
                                         html += '<td class="text-center">' + item.Sem2 + '</td>';
                                         html += '<td class="text-center">' + item.Sem3 + '</td>';
                                     }
                                     else if (resultdata.tHeader.length == 4) {
                                         html += '<td class="text-center">' + item.Sem1 + '</td>';
                                         html += '<td class="text-center">' + item.Sem2 + '</td>';
                                         html += '<td class="text-center">' + item.Sem3 + '</td>';
                                         html += '<td class="text-center">' + item.Sem4 + '</td>';
                                     }
                                     // condition end  


                                     html += '</tr>';
                                 });
                                 html += '<tbody>';
                             } else {
                                 html += '<tr><td colspan="5" style="font-weight:bold">There are no records available to display...</td></tr></tbody>';
                             }
                         } else {
                             html += '<tr><td colspan="5" style="font-weight:bold">There are no records available to display...</td></tr></tbody>';
                         }
                     } else {
                         html += '<thead class="bg-primary"><tr><th colspan="5">There are no records available to display...</th></tr></thead>';
                     }
                 } else {
                     html += '<thead class="bg-primary"><tr><th colspan="5">There are no records available to display...</th></tr></thead>';
                 }
                 $('#table-1').append(html);
                 $('[data-toggle="tooltip"]').tooltip({
                     placement: "top"
                 });
             };
             
         });
        
         </script>

<script>
    $(document).ready(function () {
        //alert('error')
    });
</script>
    <script>
        function QuickAccess()
        {
            //alert('bye')
            $.ajax({
                type: "POST",
                //url: "principalHome.aspx/ShowQuickAccessData",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowQuickAccessData")%>',
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var data = response.d;
                var html = '';
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                //            /* *************************************************************************************************************
                //                Do not give the space in href
                //                Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                //                Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                //               ************************************************************************************************************* */
                            html += '<li class="list-group-item"><a href="' + item.Link + '?pageno=' + item.PageNo + '" "  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                                        LinkCount += 1;
                            //html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No records to display..</li>';
                 });
                    } else {
                        html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No records to display..</li>';
                }
                } else {
                    html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No records to display..</li>';
                }
                $('#ulQuickAccess').append(html);
            },
            failure: function (response) {
            }
            });

        }
    </script>
<script type="text/javascript">
    $(document).ready(function () {
        //alert('hii')

        QuickAccess();

        function UpdateDataLeave(id) {
            //var resp = confirm("Are you sure want to delete?")
            //if (resp == true) {
            var Obj = {};
            Obj.id = id;

            // alert(id);
            $.ajax({
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/Approvedleave")%>',
                //url: "principalHome.aspx/Approvedleave",
                type: "POST",
                data: JSON.stringify(Obj),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //$('#txttodo').val("");
                    //$('#txttodo').css('border-color', '');
                    // BindTodoData();
                    alert("Leave Approve Successfully");
                    // debugger;
                    //$("#myModalzAppleiedLeave").hide();
                    BinleaveCount();
                    BindLeaveDashboard();

                },
                error: function (errResponse) {
                    console.log(errResponse);
                    alert("Something went Wrong");
                    //$("#myModalzAppleiedLeave").hide();
                    //$('.myModalzAppleiedLeave').hide();
                    //BindTodoData();
                }
            });
        }

        var BindLeaveDashboard = function () {
            //alert('print');
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowLeaveapprove")%>',
                //url: "principalHome.aspx/ShowLeaveapprove",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessApprovePendignLeaveDetails,
                failure: function (response) {
                }
            });
            function OnSuccessApprovePendignLeaveDetails(response) {
                var ApprovePendignLeave = response['d'];
                loadApprovePendignLeaveDetails2(ApprovePendignLeave);
            };

            function loadApprovePendignLeaveDetails2(ApprovePendignLeave) {
                //$('#tLeaveAll tbody').empty();
                $('#tLeaveAll').empty();
                var html = '';
                if (ApprovePendignLeave != null) {
                    if (ApprovePendignLeave.length > 0) {
                        $.each(ApprovePendignLeave, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-left">' + item.EmpName + '</td>';
                            html += '<td class="text-center">' + item.SUBDEPT + '</td>';
                            html += '<td class="text-center">' + item.LName + '</td>';
                            html += '<td class="text-center">' + item.From_date + '</td>';
                            html += '<td class="text-center">' + item.TO_DATE + '</td>';
                            html += '<td class="text-center"> <input type=button Value="Approve" class="btn btn-success" onclick="return UpdateDataLeave(' + item.LETRNO + ')" /></td>';
                            //html += '<td class="text-center">' + '<a href="ESTABLISHMENT/LEAVES/Transactions/DashboardleaveApproval.aspx" target="_self" style="text-align: right" class="buttonStyle ui-corner-all btn btn-success">Go To Approval</a>' + '</td>';
                            //html += '<td class="text-center"> <input type=button Value="Approve" class="btn btn-success" onclick=UpdateData('+item.letr+')/></td>' Leave_Approval.aspx;

                            html += '</tr>';
                        });
                    } else {
                        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                    }
                } else {
                    html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                }
                $('#tLeaveAll').append(html);
            };
        }
        var BinleaveCount = function () {
            //alert('print2');
            $.ajax({

                type: "POST",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindLeaveCount")%>',
                //url: "principalHome.aspx/BindLeaveCount",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessLeaveDetails2,
                failure: function (response) {
                }
            });

            function OnSuccessLeaveDetails2(response) {
                // console.log("Data", response);
                //debugger;
                var ToTal_Applied = response['d'].ToTal_Applied;
                var Approve_Leave = response['d'].Approve_Leave;
                var Pending_Leave = response['d'].Pending_Leave;
                //loadleavecount(leavecount);

                $('#lblToTal_Applied').html(ToTal_Applied);
                //alert(ToTal_Applied);
                $('#lblApprove_Leave').html(Approve_Leave);
                $('#lblPending_Leave').html(Pending_Leave);

            };
        }

        /************************ Quick Access ************************/
       
      
        //function loadQLData(data) {
            
        //}
        /************************ Quick Access ************************/
    });
</script>
</asp:Content>
